using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackScene.Utilities
{
    public class MPVController : IDisposable
    {
        private struct RECT
        {
            public int Left;

            public int Top;

            public int Right;

            public int Bottom;
        }

        public NamedPipeClientStream _pipeClient;

        private const string PipeName = "mpvsocket";

        private readonly object _lock = new object();

        private bool _disposed;

        private bool _disposing;

        private bool _stopped;

        private CancellationTokenSource _cts;

        private Task _receiveTask;

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        public MPVController()
        {
            _pipeClient = CreatePipeClient();
            _cts = new CancellationTokenSource();
            _receiveTask = Task.Run(() => ReceiveCommandsAsync(_cts.Token));
            Task.Run(() => MonitorFullscreenAppsAsync(_cts.Token));
        }

        private NamedPipeClientStream CreatePipeClient()
        {
            return new NamedPipeClientStream(".", "mpvsocket", PipeDirection.InOut, PipeOptions.Asynchronous);
        }

        private async Task EnsureConnectedAsync()
        {
            lock (_lock)
            {
                if (_disposing || _pipeClient.IsConnected)
                {
                    return;
                }
                if (_pipeClient != null)
                {
                    _pipeClient.Dispose();
                }
                _pipeClient = CreatePipeClient();
            }
            try
            {
                Main.logsForm.LogsWriteLine("Reconnecting to mpv IPC server...", error: false);
                await _pipeClient.ConnectAsync();
                Main.logsForm.LogsWriteLine("Reconnected to mpv IPC server.", error: false);
            }
            catch (Exception ex)
            {
                if (!_disposing)
                {
                    Main.logsForm.LogsWriteLine("Failed to connect to mpv IPC server: " + ex.Message, error: true);
                }
                throw;
            }
        }

        public async Task SendCommandToMPV(string commandName, object[] parameters)
        {
            if (!Processus.CheckIfAlreadyStarted())
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(commandName))
            {
                throw new ArgumentException("Command name cannot be null or whitespace.", "commandName");
            }
            try
            {
                await EnsureConnectedAsync();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("{\"command\": [\"").Append(commandName).Append("\"");
                foreach (object obj in parameters)
                {
                    if (obj is string value)
                    {
                        stringBuilder.Append(", \"").Append(value).Append("\"");
                    }
                    else if (obj is bool flag)
                    {
                        stringBuilder.Append(", ").Append(flag.ToString().ToLower());
                    }
                    else
                    {
                        stringBuilder.Append(", ").Append(obj);
                    }
                }
                stringBuilder.Append("]}\n");
                string command = stringBuilder.ToString();
                byte[] bytes = Encoding.UTF8.GetBytes(command);
                await _pipeClient.WriteAsync(bytes, 0, bytes.Length);
                await _pipeClient.FlushAsync();
                Main.logsForm.LogsWriteLine("Sent command to mpv: " + command, error: false);
                string text = await ReadResponseAsync();
                Main.logsForm.LogsWriteLine("Received response from mpv: " + text, error: false);
            }
            catch (Exception ex)
            {
                Main.logsForm.LogsWriteLine("An error occurred while sending command: " + ex.Message, error: true);
                Main._mpvController.Dispose();
            }
        }

        private async Task<string> ReadResponseAsync()
        {
            byte[] buffer = new byte[1024];
            int count = await _pipeClient.ReadAsync(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer, 0, count);
        }

        private async Task ReceiveCommandsAsync(CancellationToken cancellationToken)
        {
            _ = 3;
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (!_pipeClient.IsConnected || _stopped)
                    {
                        await EnsureConnectedAsync();
                        continue;
                    }
                    if (!Main.main.Visible)
                    {
                        break;
                    }
                    string text = await ReadResponseAsync();
                    if (!string.IsNullOrEmpty(text))
                    {
                        Main.logsForm.LogsWriteLine("Received command from mpv: " + text, error: false);
                        if (text.Contains("\"event\":\"start-file\"") && text.Contains("\"playlist_entry_id\":"))
                        {
                            await Task.Delay(100);
                            string message = "Now playing file: " + await GetCurrentFileNameAsync();
                            Main.settingsForm.FadeOutLabel(message, Main.main.label4, error: false, sound: false);
                            Main.logsForm.LogsWriteLine(message, error: false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Main.logsForm.LogsWriteLine("An error occurred while receiving commands: " + ex.Message, error: true);
            }
        }

        public async Task<string> GetCurrentFileNameAsync()
        {
            byte[] commandBytes = Encoding.UTF8.GetBytes("{\"command\": [\"get_property\", \"path\"]}\n");
            try
            {
                await EnsureConnectedAsync();
                await _pipeClient.WriteAsync(commandBytes, 0, commandBytes.Length);
                await _pipeClient.FlushAsync();
                return Path.GetFileName(ParseFileNameFromResponse(await ReadResponseAsync()));
            }
            catch (Exception ex)
            {
                Main.logsForm.LogsWriteLine("An error occurred while getting the file name: " + ex.Message, error: true);
                throw;
            }
        }

        private string ParseFileNameFromResponse(string response)
        {
            int num = response.IndexOf("\"data\":\"", StringComparison.OrdinalIgnoreCase);
            if (num != -1)
            {
                num += "\"data\":\"".Length;
                int num2 = response.IndexOf("\"", num);
                if (num2 != -1)
                {
                    return response.Substring(num, num2 - num);
                }
            }
            return "Unknown";
        }

        private bool IsFullscreenAppRunning()
        {
            IntPtr foregroundWindow = GetForegroundWindow();
            if (foregroundWindow == IntPtr.Zero)
            {
                return false;
            }
            GetWindowRect(foregroundWindow, out var lpRect);
            int width = Screen.PrimaryScreen.Bounds.Width;
            int height = Screen.PrimaryScreen.Bounds.Height;
            GetWindowThreadProcessId(foregroundWindow, out var processId);
            string value = Process.GetProcessById((int)processId).ProcessName.ToLower();
            if (new string[4] { "explorer", "taskmgr", "mpvcontroller", "mpv" }.Contains(value))
            {
                return false;
            }
            if (lpRect.Left == 0 && lpRect.Top == 0 && lpRect.Right == width)
            {
                return lpRect.Bottom == height;
            }
            return false;
        }

        private async Task MonitorFullscreenAppsAsync(CancellationToken cancellationToken)
        {
            bool isPausedBySystem = false;
            try
            {
                while (!cancellationToken.IsCancellationRequested && Main.settingsForm.checkBox3.Checked)
                {
                    bool flag = IsFullscreenAppRunning();
                    if (flag && !isPausedBySystem)
                    {
                        SendCommandToMPV("set_property", new object[2] { "pause", true });
                        Main.logsForm.LogsWriteLine("Fullscreen app detected. Video paused.", error: false);
                        isPausedBySystem = true;
                    }
                    else if (!flag && isPausedBySystem)
                    {
                        SendCommandToMPV("set_property", new object[2] { "pause", false });
                        Main.logsForm.LogsWriteLine("No fullscreen app found. Video resumed.", error: false);
                        isPausedBySystem = false;
                    }
                    await Task.Delay(1000);
                }
            }
            catch (Exception ex)
            {
                Main.logsForm.LogsWriteLine("An error occurred while monitoring fullscreen apps: " + ex.Message, error: true);
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _stopped = true;
                _pipeClient.Close();
                _disposing = true;
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                try
                {
                    _cts.Cancel();
                    if (_receiveTask != null)
                    {
                        try
                        {
                            _receiveTask.Wait(_cts.Token);
                        }
                        catch (OperationCanceledException)
                        {
                        }
                        catch (AggregateException ex2)
                        {
                            foreach (Exception innerException in ex2.InnerExceptions)
                            {
                                Main.logsForm.LogsWriteLine("Error while waiting for receive task: " + innerException.Message, error: true);
                            }
                        }
                    }
                    if (_pipeClient != null)
                    {
                        if (_pipeClient.IsConnected)
                        {
                            _pipeClient.Close();
                        }
                        _pipeClient.Dispose();
                    }
                }
                catch (Exception ex3)
                {
                    Main.logsForm.LogsWriteLine("Error during disposal: " + ex3.Message, error: true);
                }
            }
            _disposed = true;
        }
    }
}