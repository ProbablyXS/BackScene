using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackScene.Utilities
{
    class Processus
    {
        public static Process mpvProcess;

        public static string mpvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"tools\mpv\mpv.exe");
        public static string wpPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"tools\weebp\wp-headless.exe");
        public static string wallpaperPath;

        public static void StartMpvProcess()
        {

            if (CheckIfMvpAlreadyStarted()) return;
            if (!CheckWallpaperPath(wallpaperPath)) return;

            if (mpvProcess == null || mpvProcess.HasExited)
            {
                try
                {
                    if (!CheckMpvPath())
                    {
                        string message = "An error occurred: Check the MPV directory";
                        _ = Main.settingsForm.FadeOutLabel(message, Main.main.label4, true, true);
                        Main.logsForm.LogsWriteLine(message, true);
                        return;
                    }

                    if (!CheckWpPath())
                    {
                        string message = "An error occurred: Check the WP directory";
                        _ = Main.settingsForm.FadeOutLabel(message, Main.main.label4, true, true);
                        Main.logsForm.LogsWriteLine(message, true);
                        return;
                    }

                    StartMpv();
                }
                catch (Exception ex)
                {
                    Main.logsForm.LogsWriteLine($"An error occurred: {ex.Message}", true);
                }
            }
            else
            {
                Main.logsForm.LogsWriteLine($"{mpvProcess.ProcessName.ToUpper()} is already running with handle: {mpvProcess.Handle}", true);
            }
        }

        public static bool CheckIfAlreadyStarted()
        {
            if (mpvProcess == null || mpvProcess.HasExited)
            {
                if (CheckIfMvpAlreadyStarted())
                {
                    return true;
                }

                return false;
            }
            else
            {
                Main.logsForm.LogsWriteLine($"MPV is already running, please close it first", true);
                return true;
            }
        }

        //Second Method
        public static bool CheckIfMvpAlreadyStarted()
        {
            Process[] processes = Process.GetProcessesByName("mpv");

            if (processes.Length > 0)
            {
                foreach (Process process in processes)
                {
                    try
                    {
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            else
            {
                Console.WriteLine("No 'mpv.exe' process found.");
            }

            return false;
        }

        //// P/Invoke declarations
        //[DllImport("user32.dll", SetLastError = true)]
        //private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        //[DllImport("user32.dll", SetLastError = true)]
        //private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        //[DllImport("user32.dll")]
        //private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        //private static readonly IntPtr HWND_TOP = IntPtr.Zero;
        //private const int SWP_NOSIZE = 0x0001;
        //private const int SWP_NOACTIVATE = 0x0010;
        //private const int SW_SHOW = 5;
        //private const int MaxRetries = 20;
        //private const int DelayMilliseconds = 500; // Adjust delay as needed

        //private static async Task<IntPtr> WaitForWindowHandleAsync(string windowClass, int maxRetries, int delayMilliseconds)
        //{
        //    IntPtr hWnd = IntPtr.Zero;
        //    for (int i = 0; i < maxRetries; i++)
        //    {
        //        hWnd = FindWindow(windowClass, null);
        //        if (hWnd != IntPtr.Zero)
        //            return hWnd;

        //        await Task.Delay(delayMilliseconds);
        //    }
        //    return hWnd;
        //}

        private static async void StartMpv()
        {
            string baseArguments = "--player-operation-mode=pseudo-gui --osc=no --show-in-taskbar=no --terminal=no --loop-playlist=inf --hwdec=auto --border=no --input-ipc-server=\\\\.\\pipe\\mpvsocket";
            string wallpaperArgument = $" {wallpaperPath}";
            string additionalArguments = Main.settingsForm.MuteAudiocheckBox.Checked ? " --mute=yes" : string.Empty;
            additionalArguments += Main.settingsForm.checkBox1.Checked ? " --shuffle=yes" : string.Empty;
            additionalArguments += Main.settingsForm.checkBox2.Checked ? $" --vf-add=fps={Main.settingsForm.FPS}" : string.Empty;

            string trimmedWallpaperArgument = wallpaperArgument.Trim();
            string formattedWallpaperArgument = $"\"{trimmedWallpaperArgument}\"";
            string arguments = $"{baseArguments}{additionalArguments} {formattedWallpaperArgument}";

            var mpvProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = mpvPath,
                    Arguments = arguments,
                    UseShellExecute = false,
                    CreateNoWindow = false
                }
            };

            mpvProcess.Start();
            SetAsWallpaper(mpvProcess);

            //// Wait for the window handle to become available
            //IntPtr hWnd = await WaitForWindowHandleAsync("mpv", MaxRetries, DelayMilliseconds);

            //if (hWnd != IntPtr.Zero)
            //{
            //    // Assuming the second screen is on the right side of the primary screen
            //    var screens = Screen.AllScreens;
            //    if (screens.Length > 1)
            //    {
            //        var secondaryScreen = screens[0];
            //        var bounds = secondaryScreen.Bounds;

            //        // Move and resize the window to fit the secondary screen
            //        SetWindowPos(hWnd, HWND_TOP, bounds.X, bounds.Y, bounds.Width, bounds.Height, SWP_NOSIZE | SWP_NOACTIVATE);
            //        ShowWindow(hWnd, SW_SHOW);
            //    }
            //}
            //else
            //{
            //    // Handle the case where the window was not found
            //    MessageBox.Show("Failed to find the window.");
            //}

            if (Main.settingsForm.CleanMemorycheckBox.Checked)
            {
                _ = MemoryCleaner.StartCleanMem();
            }

            string message = mpvProcess.ProcessName.ToUpper() + " process [started]";
            _ = Main.settingsForm.FadeOutLabel(message, Main.main.label4, false, true);
            Main.logsForm.LogsWriteLine(message, false);

            Main._mpvController = new MPVController();

            Main.settingsForm.Hide();

        }

        public static async Task SendCommandToMPV(string commandName, object[] parameters)
        {
            string pipeName = @"\\.\pipe\mpvsocket";

            using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "mpvsocket", PipeDirection.InOut))
            {
                try
                {
                    Main.logsForm.LogsWriteLine("Connecting to mpv IPC server...", false);
                    await pipeClient.ConnectAsync();
                    Main.logsForm.LogsWriteLine("Connected to mpv IPC server.", false);

                    if (commandName == "set_property" && parameters.Length > 0 && parameters[0].ToString() == "mute")
                    {
                        string getMuteStateCommand = "{\"command\": [\"get_property\", \"mute\"]}\n";
                        byte[] getMuteStateCommandBytes = Encoding.UTF8.GetBytes(getMuteStateCommand);
                        await pipeClient.WriteAsync(getMuteStateCommandBytes, 0, getMuteStateCommandBytes.Length);
                        await pipeClient.FlushAsync();

                        byte[] buffer = new byte[256];
                        int bytesRead = await pipeClient.ReadAsync(buffer, 0, buffer.Length);
                        string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        Main.logsForm.LogsWriteLine("Received response from mpv: " + response, false);

                        bool isMuted = response.Contains("\"data\":true");

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

                    // Write the command to mpv
                    byte[] commandBytes = Encoding.UTF8.GetBytes(command);
                    await pipeClient.WriteAsync(commandBytes, 0, commandBytes.Length);
                    await pipeClient.FlushAsync();
                    Main.logsForm.LogsWriteLine($"Sent command to mpv: {command}", false);

                    // Read the response from mpv
                    byte[] responseBuffer = new byte[256];
                    int responseBytesRead = await pipeClient.ReadAsync(responseBuffer, 0, responseBuffer.Length);
                    string finalResponse = Encoding.UTF8.GetString(responseBuffer, 0, responseBytesRead);
                    Main.logsForm.LogsWriteLine("Received response from mpv: " + finalResponse, false);
                }
                catch (Exception ex)
                {
                    Main.logsForm.LogsWriteLine("An error occurred: " + ex.Message, true);
                }
            }
        }


        private static void SetAsWallpaper(Process mpvProcess)
        {

            using (var wpProcess = new Process())
            {
                wpProcess.StartInfo = new ProcessStartInfo
                {
                    FileName = wpPath,
                    Arguments = "add --wait --fullscreen --class mpv",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                wpProcess.Start();

                try
                {
                    string output = wpProcess.StandardOutput.ReadToEnd();
                    string error = wpProcess.StandardError.ReadToEnd();

                    wpProcess.WaitForExit();

                    if (!string.IsNullOrEmpty(output))
                    {
                        Console.WriteLine($"wp-headless output: {output}");
                    }

                    if (!string.IsNullOrEmpty(error))
                    {
                        Console.WriteLine($"wp-headless error: {error}");
                    }
                }
                catch (Exception e)
                {
                    Main.logsForm.LogsWriteLine($"An error has occurred with {wpProcess.ProcessName.ToUpper()} {e.Message}", true);
                    return;
                }

                Main.logsForm.LogsWriteLine(mpvProcess.ProcessName.ToUpper() + " have been set in background", false);
            }
        }

        public static bool CheckWallpaperPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return RejectAndLog("The wallpaper path is null, empty, or consists only of white-space characters.");
            }

            if (!Directory.Exists(path))
            {
                return RejectAndLog("The wallpaper path does not exist or is not a valid directory.");
            }

            try
            {
                string[] validExtensions = { ".mp4", ".avi", ".mkv", ".webm",
                                     ".jpg", ".jpeg", ".png", ".bmp", ".gif",
                                     ".mp3", ".wav", ".aac", ".flac" };

                var files = Directory.GetFiles(path)
                                     .Where(file => validExtensions.Contains(Path.GetExtension(file).ToLower()))
                                     .ToArray();

                if (files.Length > 0)
                {
                    return true;
                }
                else
                {
                    return RejectAndLog("The wallpaper path does not contain any video, image, or music files.");
                }
            }
            catch (Exception ex)
            {
                return RejectAndLog($"An error occurred while checking the wallpaper path: {ex.Message}");
            }
        }

        private static bool RejectAndLog(string message)
        {
            RejectWallpaperFolder();
            //Main.logsForm.DisplayMessage(message, true);
            Main.logsForm.LogsWriteLine(message, true);
            return false;
        }

        private static void RejectWallpaperFolder()
        {
            string message = "Wallpaper Folder has been rejected";
            _ = Main.settingsForm.FadeOutLabel(message, Main.main.label4, true, true);

            Main.logsForm.LogsWriteLine(message, true);
        }

        public static bool CheckMpvPath()
        {
            if (File.Exists(mpvPath))
            {
                return true;
            }

            return false;
        }

        public static bool CheckWpPath()
        {
            if (File.Exists(wpPath))
            {
                return true;
            }

            return false;
        }

        public static void CloseMpvIfRunning()
        {
            Process[] processes = Process.GetProcessesByName("mpv");

            if (processes.Length > 0)
            {
                foreach (Process process in processes)
                {
                    try
                    {
                        process.Kill();
                        Main.logsForm.LogsWriteLine($"Successfully terminated process: {process.ProcessName} (ID: {process.Id})", false);
                        Main._mpvController.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Main.logsForm.LogsWriteLine($"Failed to terminate process: {process.ProcessName} (ID: {process.Id}). Exception: {ex.Message}", true);
                    }
                }
            }
            else
            {
                Console.WriteLine("No 'mpv.exe' process found.");
            }
        }

        public static void CloseMpvProcess()
        {

            if (mpvProcess != null && !mpvProcess.HasExited)
            {
                try
                {
                    mpvProcess.Kill();
                    mpvProcess.WaitForExit();
                    mpvProcess.Close();
                    mpvProcess = null;
                    MemoryCleaner.StopCleanMem();
                    Main.logsForm.LogsWriteLine("MPV [closed]", false);
                }
                catch (Exception ex)
                {
                    Main.logsForm.LogsWriteLine("Error while closing mpv: " + ex.Message, true);
                }
            }

            //Method 2
            CloseMpvIfRunning();

            Main._mpvController.Dispose();
            Main.logsForm.LogsWriteLine("Stopped MPV Controller", false);
        }
    }
}
