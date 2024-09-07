using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackScene.Utilities
{
    public class MPVController : IDisposable
    {
        public NamedPipeClientStream _pipeClient;
        private const string PipeName = "mpvsocket";
        private readonly object _lock = new object();
        private bool _disposed = false;
        private bool _disposing = false;
        private bool _stopped = false;
        private CancellationTokenSource _cts;
        private Task _receiveTask;

        public MPVController()
        {
            _pipeClient = CreatePipeClient();
            _cts = new CancellationTokenSource();
            _receiveTask = Task.Run(() => ReceiveCommandsAsync(_cts.Token));
        }

        private NamedPipeClientStream CreatePipeClient()
        {
            return new NamedPipeClientStream(".", PipeName, PipeDirection.InOut, PipeOptions.Asynchronous);
        }

        private async Task EnsureConnectedAsync()
        {
            lock (_lock)
            {
                if (_disposing) return;

                if (_pipeClient.IsConnected)
                    return;

                if (_pipeClient != null)
                {
                    _pipeClient.Dispose();
                }

                _pipeClient = CreatePipeClient();
            }

            try
            {
                Main.logsForm.LogsWriteLine("Reconnecting to mpv IPC server...", false);
                await _pipeClient.ConnectAsync();
                Main.logsForm.LogsWriteLine("Reconnected to mpv IPC server.", false);
            }
            catch (Exception ex)
            {
                if (!_disposing)
                {
                    Main.logsForm.LogsWriteLine("Failed to connect to mpv IPC server: " + ex.Message, true);
                }
                throw;
            }
        }


        public async Task SendCommandToMPV(string commandName, object[] parameters)
        {
            if (!Processus.CheckIfAlreadyStarted()) return;

            if (string.IsNullOrWhiteSpace(commandName))
            {
                throw new ArgumentException("Command name cannot be null or whitespace.", nameof(commandName));
            }

            try
            {
                await EnsureConnectedAsync();

                var commandBuilder = new StringBuilder();
                commandBuilder.Append("{\"command\": [\"").Append(commandName).Append("\"");

                foreach (var param in parameters)
                {
                    if (param is string strParam)
                    {
                        commandBuilder.Append(", \"").Append(strParam).Append("\"");
                    }
                    else if (param is bool boolParam)
                    {
                        commandBuilder.Append(", ").Append(boolParam.ToString().ToLower());
                    }
                    else
                    {
                        commandBuilder.Append(", ").Append(param);
                    }
                }
                commandBuilder.Append("]}\n");

                string command = commandBuilder.ToString();
                byte[] commandBytes = Encoding.UTF8.GetBytes(command);

                await _pipeClient.WriteAsync(commandBytes, 0, commandBytes.Length);
                await _pipeClient.FlushAsync();

                Main.logsForm.LogsWriteLine($"Sent command to mpv: {command}", false);

                string response = await ReadResponseAsync();
                Main.logsForm.LogsWriteLine("Received response from mpv: " + response, false);
            }
            catch (Exception ex)
            {
                Main.logsForm.LogsWriteLine("An error occurred while sending command: " + ex.Message, true);
                Main._mpvController.Dispose();
            }
        }


        private async Task<string> ReadResponseAsync()
        {
            byte[] buffer = new byte[1024];
            int bytesRead = await _pipeClient.ReadAsync(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer, 0, bytesRead);
        }

        private async Task ReceiveCommandsAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (_pipeClient.IsConnected && !_stopped)
                    {
                        string response = await ReadResponseAsync();
                        if (!string.IsNullOrEmpty(response))
                        {
                            Main.logsForm.LogsWriteLine("Received command from mpv: " + response, false);

                            if (response.Contains("\"event\":\"start-file\"") && response.Contains("\"playlist_entry_id\":"))
                            {
                                await Task.Delay(100);
                                string currentFileName = await GetCurrentFileNameAsync();

                                string message = "Now playing file: " + currentFileName;
                                _ = Main.settingsForm.FadeOutLabel(message, Main.main.label4, false, false);
                                Main.logsForm.LogsWriteLine(message, false);
                            }
                        }
                    }
                    else
                    {
                        await EnsureConnectedAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Main.logsForm.LogsWriteLine("An error occurred while receiving commands: " + ex.Message, true);
            }
        }



        public async Task<string> GetCurrentFileNameAsync()
        {
            const string getFileNameCommand = "{\"command\": [\"get_property\", \"path\"]}\n";
            byte[] commandBytes = Encoding.UTF8.GetBytes(getFileNameCommand);

            try
            {
                await EnsureConnectedAsync();
                await _pipeClient.WriteAsync(commandBytes, 0, commandBytes.Length);
                await _pipeClient.FlushAsync();

                string response = await ReadResponseAsync();
                string fullPath = ParseFileNameFromResponse(response);
                string fileName = Path.GetFileName(fullPath);

                return fileName;
            }
            catch (Exception ex)
            {
                Main.logsForm.LogsWriteLine("An error occurred while getting the file name: " + ex.Message, true);
                throw;
            }
        }


        private string ParseFileNameFromResponse(string response)
        {
            const string dataPrefix = "\"data\":\"";
            int startIndex = response.IndexOf(dataPrefix, StringComparison.OrdinalIgnoreCase);
            if (startIndex != -1)
            {
                startIndex += dataPrefix.Length;
                int endIndex = response.IndexOf("\"", startIndex);
                if (endIndex != -1)
                {
                    return response.Substring(startIndex, endIndex - startIndex);
                }
            }
            return "Unknown";
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _stopped = true;
            _pipeClient.Close();
            _disposing = true;

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

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
                        catch (AggregateException aggEx)
                        {
                            foreach (var ex in aggEx.InnerExceptions)
                            {
                                Main.logsForm.LogsWriteLine("Error while waiting for receive task: " + ex.Message, true);
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
                catch (Exception ex)
                {
                    Main.logsForm.LogsWriteLine("Error during disposal: " + ex.Message, true);
                }
            }

            _disposed = true;
        }
    }
}
