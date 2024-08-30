using System;
using System.Diagnostics;
using System.IO;

namespace BackScene.Utilities
{
    class Processus
    {
        public static Process mpvProcess;

        // Path to mpv executable
        public static string mpvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"tools\mpv\mpv.exe");

        // Path to mpv executable
        public static string wpPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"tools\weebp\wp-headless.exe");

        // Path to wallpaper folder
        public static string wallpaperPath;

        public static void StartMpvProcess()
        {

            if (CheckIfMvpAlreadyStarted()) return;
            if (!CheckWallpaperPath()) return;

            if (mpvProcess == null || mpvProcess.HasExited)
            {
                try
                {
                    Main.settingsForm.Hide();

                    StartMpv();
                    SetAsWallpaper();
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
            // Get all processes with the name "mpv"
            Process[] processes = Process.GetProcessesByName("mpv");

            // Check if any processes were found
            if (processes.Length > 0)
            {
                foreach (Process process in processes)
                {
                    try
                    {
                        // Attempt to close the process
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

        private static void StartMpv()
        {
            // Prepare the base arguments
            string baseArguments = "--player-operation-mode=pseudo-gui --force-window=yes shuffle=yes --terminal=no --loop-playlist=inf --input-ipc-server=\\\\.\\pipe\\mpvsocket";

            // Add the wallpaper path
            string wallpaperArgument = $" {wallpaperPath}";

            // Check if checkbox1 is checked and modify arguments accordingly
            string additionalArguments = Main.settingsForm.checkBox3.Checked ? " --mute=yes" : string.Empty;

            // Trim any extra spaces from the directory path
            string trimmedWallpaperArgument = wallpaperArgument.Trim();

            // Ensure the directory path is enclosed in double quotes
            string formattedWallpaperArgument = $"\"{trimmedWallpaperArgument}\"";

            // Concatenate the arguments
            string arguments = $"{baseArguments}{additionalArguments} {formattedWallpaperArgument}";

            mpvProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = mpvPath,
                    Arguments = arguments,
                    UseShellExecute = false,
                    CreateNoWindow = false // Set to true if you don't want a window to be visible
                }
            };

            mpvProcess.Start();

            if (Main.settingsForm.checkBox4.Checked)
            {
                _ = MemoryCleaner.StartCleanMem();
            }

            Main.logsForm.LogsWriteLine(mpvProcess.ProcessName.ToUpper() + " process [started]", false);
        }

        private static void SetAsWallpaper()
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

                // Capture and display output and errors

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

        public static bool CheckWallpaperPath()
        {
            if (string.IsNullOrEmpty(wallpaperPath))
            {
                Main.logsForm.LogsWriteLine("The wallpaper path is null or empty.", true);
                return false;
            }

            if (Directory.Exists(wallpaperPath))
            {
                Main.logsForm.LogsWriteLine("The wallpaper path loaded.", false);
                return true;
            }
            else
            {
                Main.logsForm.LogsWriteLine("The wallpaper path does not exist or is not a valid directory.", true);
            }

            return false;
        }

        public static void CloseMpvIfRunning()
        {
            // Get all processes with the name "mpv"
            Process[] processes = Process.GetProcessesByName("mpv");

            // Check if any processes were found
            if (processes.Length > 0)
            {
                foreach (Process process in processes)
                {
                    try
                    {
                        // Attempt to close the process
                        process.Kill();
                        Main.logsForm.LogsWriteLine($"Successfully terminated process: {process.ProcessName} (ID: {process.Id})", false);
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions that may occur (e.g., access denied)
                        Main.logsForm.LogsWriteLine($"Failed to terminate process: {process.ProcessName} (ID: {process.Id}). Exception: {ex.Message}", true);
                    }
                }
            }
            else
            {
                Console.WriteLine("No 'mpv.exe' process found.");
            }
        }

        // Method to close the mpv process
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
        }
    }
}
