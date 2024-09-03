using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;

namespace BackScene.Utilities
{
    public class MPVController : IDisposable
    {
        private NamedPipeClientStream _pipeClient;
        private const string PipeName = @"\\.\pipe\mpvsocket";
        private readonly object _lock = new object();

        public MPVController()
        {
            _pipeClient = new NamedPipeClientStream(".", "mpvsocket", PipeDirection.InOut);
        }

        private async Task EnsureConnectedAsync()
        {
            lock (_lock)
            {
                if (_pipeClient.IsConnected)
                    return;
            }

            Main.logsForm.LogsWriteLine("Reconnecting to mpv IPC server...", false);
            _pipeClient = new NamedPipeClientStream(".", "mpvsocket", PipeDirection.InOut);
            await _pipeClient.ConnectAsync();
            Main.logsForm.LogsWriteLine("Reconnected to mpv IPC server.", false);
        }

        public async Task SendCommandToMPV(string commandName, object[] parameters)
        {
            try
            {
                await EnsureConnectedAsync();

                if (commandName == "set_property" && parameters.Length > 0 && parameters[0].ToString() == "mute")
                {
                    bool isMuted = await GetMuteStateAsync();
                    parameters[1] = !isMuted;
                }

                string command = $"{{\"command\": [\"{commandName}\"";
                foreach (var param in parameters)
                {
                    if (param is string)
                    {
                        command += $", \"{param}\"";
                    }
                    else if (param is bool)
                    {
                        command += $", {param.ToString().ToLower()}";
                    }
                    else
                    {
                        command += $", {param}";
                    }
                }
                command += "]}\n";

                byte[] commandBytes = Encoding.UTF8.GetBytes(command);
                await _pipeClient.WriteAsync(commandBytes, 0, commandBytes.Length);
                await _pipeClient.FlushAsync();
                Main.logsForm.LogsWriteLine($"Sent command to mpv: {command}", false);

                byte[] buffer = new byte[256];
                int bytesRead = await _pipeClient.ReadAsync(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Main.logsForm.LogsWriteLine("Received response from mpv: " + response, false);
            }
            catch (Exception ex)
            {
                Main.logsForm.LogsWriteLine("An error occurred: " + ex.Message, true);
            }
        }

        private async Task<bool> GetMuteStateAsync()
        {
            string getMuteStateCommand = "{\"command\": [\"get_property\", \"mute\"]}\n";
            byte[] commandBytes = Encoding.UTF8.GetBytes(getMuteStateCommand);
            await EnsureConnectedAsync();
            await _pipeClient.WriteAsync(commandBytes, 0, commandBytes.Length);
            await _pipeClient.FlushAsync();

            byte[] buffer = new byte[256];
            int bytesRead = await _pipeClient.ReadAsync(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            return response.Contains("\"data\":true");
        }

        public void Dispose()
        {
            if (_pipeClient != null)
            {
                if (_pipeClient.IsConnected)
                {
                    _pipeClient.Close();
                }
                _pipeClient.Dispose();
            }
        }
    }
}