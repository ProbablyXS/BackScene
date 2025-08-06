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

    internal class Processus
    {

        private const int SWP_NOZORDER = 0x0004;
        private const int SWP_NOACTIVATE = 0x0010;
        private static readonly IntPtr HWND_TOP = IntPtr.Zero;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(
            IntPtr hWnd, IntPtr hWndInsertAfter,
            int X, int Y, int cx, int cy, uint uFlags);


        public static Process mpvProcess;

        public static string mpvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tools\\mpv\\mpv.exe");

        public static string wpPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tools\\weebp\\wp.exe");

        public static string refreshPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tools\\weebp\\refresh.exe");

        public static string wallpaperPath;

        public static void StartMpvProcess()
        {
            if (CheckIfMvpAlreadyStarted() || !CheckWallpaperPath(wallpaperPath))
            {
                return;
            }
            if (mpvProcess == null || mpvProcess.HasExited)
            {
                try
                {
                    if (!CheckMpvPath())
                    {
                        string message = "An error occurred: Check the MPV directory";
                        Main.settingsForm.FadeOutLabel(message, Main.main.label4, error: true, sound: true);
                        Main.logsForm.LogsWriteLine(message, error: true);
                    }
                    else if (!CheckWpPath())
                    {
                        string message2 = "An error occurred: Check the WP directory";
                        Main.settingsForm.FadeOutLabel(message2, Main.main.label4, error: true, sound: true);
                        Main.logsForm.LogsWriteLine(message2, error: true);
                    }
                    else
                    {
                        AnimationForms.MinimizeForm(Main.settingsForm, minimize: false);
                        StartMpv();
                    }
                    return;
                }
                catch (Exception ex)
                {
                    Main.logsForm.LogsWriteLine("An error occurred: " + ex.Message, error: true);
                    return;
                }
            }
            Main.logsForm.LogsWriteLine($"{mpvProcess.ProcessName.ToUpper()} is already running with handle: {mpvProcess.Handle}", error: true);
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
            Main.logsForm.LogsWriteLine("MPV is already running, please close it first", error: true);
            return true;
        }

        public static bool CheckIfMvpAlreadyStarted()
        {
            Process[] processesByName = Process.GetProcessesByName("mpv");
            if (processesByName.Length != 0)
            {
                Process[] array = processesByName;
                int num = 0;
                if (num < array.Length)
                {
                    _ = array[num];
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

        private static async void StartMpv()
        {
            string text = " " + wallpaperPath;
            string text2 = (Main.settingsForm.MuteAudiocheckBox.Checked ? " --mute=yes" : string.Empty);
            text2 += (Main.settingsForm.checkBox1.Checked ? " --shuffle=yes" : string.Empty);
            text2 += (Main.settingsForm.checkBox2.Checked ? $" --vf-add=fps={Main.settingsForm.FPS}" : string.Empty);
            string text3 = text.Trim();
            string text4 = "\"" + text3 + "\"";
            string arguments = "--player-operation-mode=pseudo-gui --fullscreen --osc=no --show-in-taskbar=no --terminal=no --loop-playlist=inf --hwdec=auto --border=no --input-ipc-server=\\\\.\\pipe\\mpvsocket" + text2 + " " + text4;
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = mpvPath,
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = false
            };
            process.Start();
            int selectedDisplayIndex = Main.settingsForm.DisplayComboBox.SelectedIndex;
            SetAsWallpaper(process, selectedDisplayIndex);
            if (Main.settingsForm.CleanMemorycheckBox.Checked)
            {
                MemoryCleaner.StartCleanMem();
            }
            string message = process.ProcessName.ToUpper() + " process [started]";
            Main.settingsForm.FadeOutLabel(message, Main.main.label4, error: false, sound: true);
            Main.logsForm.LogsWriteLine(message, error: false);
            Main._mpvController = new MPVController();
        }

        public static async Task SendCommandToMPV(string commandName, object[] parameters)
        {
            using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "mpvsocket", PipeDirection.InOut))
            {
                _ = 6;
                try
                {
                    Main.logsForm.LogsWriteLine("Connecting to mpv IPC server...", error: false);
                    await pipeClient.ConnectAsync();
                    Main.logsForm.LogsWriteLine("Connected to mpv IPC server.", error: false);

                    if (commandName == "set_property" && parameters.Length != 0 && parameters[0].ToString() == "mute")
                    {
                        string s = "{\"command\": [\"get_property\", \"mute\"]}\n";
                        byte[] bytes = Encoding.UTF8.GetBytes(s);
                        await pipeClient.WriteAsync(bytes, 0, bytes.Length);
                        await pipeClient.FlushAsync();
                        byte[] buffer = new byte[256];
                        int count = await pipeClient.ReadAsync(buffer, 0, buffer.Length);
                        string @string = Encoding.UTF8.GetString(buffer, 0, count);
                        Main.logsForm.LogsWriteLine("Received response from mpv: " + @string, error: false);
                        bool flag = @string.Contains("\"data\":true");
                        parameters[1] = !flag;
                    }

                    string command2 = "{\"command\": [\"" + commandName + "\"";
                    foreach (object obj in parameters)
                    {
                        command2 = ((!(obj is string)) ? ((!(obj is bool)) ? (command2 + $", {obj}") : (command2 + ", " + obj.ToString().ToLower())) : (command2 + $", \"{obj}\""));
                    }
                    command2 += "]}\n";
                    byte[] bytes2 = Encoding.UTF8.GetBytes(command2);
                    await pipeClient.WriteAsync(bytes2, 0, bytes2.Length);
                    await pipeClient.FlushAsync();
                    Main.logsForm.LogsWriteLine("Sent command to mpv: " + command2, error: false);

                    byte[] responseBuffer = new byte[256];
                    int count2 = await pipeClient.ReadAsync(responseBuffer, 0, responseBuffer.Length);
                    string string2 = Encoding.UTF8.GetString(responseBuffer, 0, count2);
                    Main.logsForm.LogsWriteLine("Received response from mpv: " + string2, error: false);
                }
                catch (Exception ex)
                {
                    Main.logsForm.LogsWriteLine("An error occurred: " + ex.Message, error: true);
                }
            }
        }

        public static void SetAsWallpaper(Process mpvProcess, int screenIndex)
        {
            // Wait for MPV window to be ready
            while (mpvProcess.MainWindowHandle == IntPtr.Zero && !mpvProcess.HasExited)
            {
                Thread.Sleep(100);
            }
            if (mpvProcess.HasExited)
            {
                Console.WriteLine("MPV exited before window handle was available.");
                return;
            }

            IntPtr mainWindowHandle = mpvProcess.MainWindowHandle;

            // Run wp-headless refresh or any wallpaper embedding tool you are using
            Process process2 = new Process();
            process2.StartInfo = new ProcessStartInfo
            {
                FileName = refreshPath,
                Arguments = $"0x{mainWindowHandle.ToInt32():X}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
            process2.Start();

            // Read output and wait
            string output = process2.StandardOutput.ReadToEnd();
            string error = process2.StandardError.ReadToEnd();
            process2.WaitForExit();

            if (!string.IsNullOrEmpty(output))
                Console.WriteLine("wp-headless output: " + output);
            if (!string.IsNullOrEmpty(error))
                Console.WriteLine("wp-headless error: " + error);

            // Now Move the MPV window to correct wallpaper position
            Screen[] screens = Screen.AllScreens;

            if (screenIndex < 0 || screenIndex >= screens.Length)
            {
                screenIndex = 0; // fallback
            }

            int screenWidth = screens[screenIndex].Bounds.Width;
            int screenHeight = screens[screenIndex].Bounds.Height;

            // Calculate X offset in wallpaper space:
            int xOffset = 0;
            for (int i = 0; i < screenIndex; i++)
            {
                xOffset += screens[i].Bounds.Width;
            }

            int x = xOffset;
            int y = 0; // Assuming horizontal layout. For vertical, you'd also sum Y.

            Console.WriteLine($"Moving MPV to X={x}, Y={y}, Width={screenWidth}, Height={screenHeight}");

            SetWindowPos(mainWindowHandle, HWND_TOP, x, y, screenWidth, screenHeight, SWP_NOZORDER | SWP_NOACTIVATE);

            Console.WriteLine("MPV has been repositioned as wallpaper.");
        }

        public static bool CheckWallpaperPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return RejectAndLog("The wallpaper path is null, empty, or consists only of white-space characters.");
            }
            string[] validExtensions = new string[13]
            {
            ".mp4", ".avi", ".mkv", ".webm", ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".mp3",
            ".wav", ".aac", ".flac"
            };
            if (File.Exists(path))
            {
                if (validExtensions.Contains(Path.GetExtension(path).ToLower()))
                {
                    return true;
                }
                return RejectAndLog("The file is not a valid video file.");
            }
            if (!Directory.Exists(path))
            {
                return RejectAndLog("The wallpaper path does not exist or is not a valid directory.");
            }
            try
            {
                if ((from file in Directory.GetFiles(path)
                     where validExtensions.Contains(Path.GetExtension(file).ToLower())
                     select file).ToArray().Length != 0)
                {
                    return true;
                }
                return RejectAndLog("The wallpaper path does not contain any video, image, or music files.");
            }
            catch (Exception ex)
            {
                return RejectAndLog("An error occurred while checking the wallpaper path: " + ex.Message);
            }
        }

        private static bool RejectAndLog(string message)
        {
            RejectWallpaperFolder();
            Main.logsForm.LogsWriteLine(message, error: true);
            return false;
        }

        private static void RejectWallpaperFolder()
        {
            string message = "Wallpaper Folder has been rejected";
            Main.settingsForm.FadeOutLabel(message, Main.main.label4, error: true, sound: true);
            Main.logsForm.LogsWriteLine(message, error: true);
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
            Process[] processesByName = Process.GetProcessesByName("mpv");
            if (processesByName.Length != 0)
            {
                Process[] array = processesByName;
                foreach (Process process in array)
                {
                    try
                    {
                        process.Kill();
                        Main.logsForm.LogsWriteLine($"Successfully terminated process: {process.ProcessName} (ID: {process.Id})", error: false);
                        Main._mpvController.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Main.logsForm.LogsWriteLine($"Failed to terminate process: {process.ProcessName} (ID: {process.Id}). Exception: {ex.Message}", error: true);
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
                    Main.logsForm.LogsWriteLine("MPV [closed]", error: false);
                }
                catch (Exception ex)
                {
                    Main.logsForm.LogsWriteLine("Error while closing mpv: " + ex.Message, error: true);
                }
            }
            CloseMpvIfRunning();
            Main._mpvController.Dispose();
            Main.logsForm.LogsWriteLine("Stopped MPV Controller", error: false);
        }
    }
}