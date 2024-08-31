using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BackScene.Utilities
{
    class MemoryCleaner
    {
        private static bool _isRunning;

        [DllImport("psapi.dll")]
        static extern int EmptyWorkingSet(IntPtr hProcess);

        private static string mpvProcessName = "mpv";
        private static string ownerProcessName = Process.GetCurrentProcess().ProcessName;

        public static async Task StartCleanMem()
        {
            if (_isRunning) return;

            _isRunning = true;

            Main.logsForm.LogsWriteLine($"Clean Memory for {mpvProcessName.ToUpper()} and {ownerProcessName.ToUpper()} processes [started]", false);

            while (_isRunning)
            {
                await Task.Delay(30000);

                CleanMemoryForProcess(mpvProcessName);
                CleanMemoryForProcess(ownerProcessName);
            }
        }

        private static void CleanMemoryForProcess(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);

            foreach (Process process in processes)
            {
                try
                {
                    Console.WriteLine($"Attempting to clean memory for process: {process.ProcessName} (PID: {process.Id})");

                    // Empty the working set
                    if (EmptyWorkingSet(process.Handle) != 0)
                    {
                        Console.WriteLine($"Successfully cleaned memory for process: {process.ProcessName} (PID: {process.Id})");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to clean memory for process: {process.ProcessName} (PID: {process.Id})");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        public static void StopCleanMem()
        {
            _isRunning = false;
        }
    }
}
