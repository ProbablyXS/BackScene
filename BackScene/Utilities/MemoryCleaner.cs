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

        private static string processName = "mpv";

        public static async Task StartCleanMem()
        {
            if (_isRunning) return;

            _isRunning = true;

            Main.logsForm.LogsWriteLine($"Clean Memory for {processName.ToUpper()} process [started]", false);

            while (_isRunning)
            {
                await Task.Delay(30000);

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
        }

        public static void StopCleanMem()
        {
            _isRunning = false;
        }
    }
}
