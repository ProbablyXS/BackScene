using System;
using System.Diagnostics;
using System.IO;
using System.Media;

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
            if (!CheckWallpaperPath()) return;

            if (mpvProcess == null || mpvProcess.HasExited)
            {
                try
                {
                    if (!CheckMpvPath())
                    {
                        Main.logsForm.LogsWriteLine($"An error occurred: Check the MPV directory", true);
                        return;
                    }

                    if (!CheckWpPath())
                    {
                        Main.logsForm.LogsWriteLine($"An error occurred: Check the WP directory", true);
                        return;
                    }

                    StartMpv();
                    SetAsWallpaper();

                    SoundPlayer player = new SoundPlayer(Properties.Resources.Dropped);
                    player.Play();
                    Main.settingsForm.Hide();
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

        private static void StartMpv()
        {
            string baseArguments = "--player-operation-mode=pseudo-gui --force-window=yes shuffle=yes --terminal=no --loop-playlist=inf --hwdec=auto --border=no --input-ipc-server=\\\\.\\pipe\\mpvsocket";
            string wallpaperArgument = $" {wallpaperPath}";
            string additionalArguments = Main.settingsForm.MuteAudiocheckBox.Checked ? " --mute=yes" : string.Empty;

            string trimmedWallpaperArgument = wallpaperArgument.Trim();
            string formattedWallpaperArgument = $"\"{trimmedWallpaperArgument}\"";
            string arguments = $"{baseArguments}{additionalArguments} {formattedWallpaperArgument}";

            mpvProcess = new Process
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

            if (Main.settingsForm.CleanMemorycheckBox.Checked)
            {
                _ = MemoryCleaner.StartCleanMem();
            }

            string message = mpvProcess.ProcessName.ToUpper() + " process [started]";
            Main.settingsForm.FadeOutLabel(message, Main.main.label4);
            Main.logsForm.LogsWriteLine(message, false);
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
            if (string.IsNullOrWhiteSpace(wallpaperPath))
            {
                Main.logsForm.DisplayMessage("The wallpaper path is null, empty, or consists only of white-space characters.", true);
                return false;
            }

            try
            {
                if (Directory.Exists(wallpaperPath))
                {
                    // Check if the directory is not empty
                    var files = Directory.GetFiles(wallpaperPath);
                    var directories = Directory.GetDirectories(wallpaperPath);

                    if (files.Length > 0 || directories.Length > 0)
                    {
                        return true;
                    }
                    else
                    {
                        Main.logsForm.DisplayMessage("The wallpaper path exists but is empty.", true);
                        return false;
                    }
                }
                else
                {
                    Main.logsForm.DisplayMessage("The wallpaper path does not exist or is not a valid directory.", true);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Main.logsForm.LogsWriteLine($"An error occurred while checking the wallpaper path: {ex.Message}", true);
                return false;
            }
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
        }
    }
}
