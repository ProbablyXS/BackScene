using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
            if (!CheckWallpaperPath(wallpaperPath)) return;

            if (mpvProcess == null || mpvProcess.HasExited)
            {
                try
                {
                    if (!CheckMpvPath())
                    {
                        string message = "An error occurred: Check the MPV directory";
                        Main.settingsForm.FadeOutLabel(message, Main.main.label4, true);
                        Main.logsForm.LogsWriteLine(message, true);
                        return;
                    }

                    if (!CheckWpPath())
                    {
                        string message = "An error occurred: Check the WP directory";
                        Main.settingsForm.FadeOutLabel(message, Main.main.label4, true);
                        Main.logsForm.LogsWriteLine(message, true);
                        return;
                    }

                    StartMpv();
                    SetAsWallpaper();

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
            Main.settingsForm.FadeOutLabel(message, Main.main.label4, false);
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
            Main.settingsForm.FadeOutLabel(message, Main.main.label4, true);

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
