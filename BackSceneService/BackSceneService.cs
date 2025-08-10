using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace BackSceneService
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer;
        private Process currentProcess;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer(10000); // 10 sec
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            StartApplicationViaTaskScheduler();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (currentProcess == null || currentProcess.HasExited)
            {
                StartApplicationViaTaskScheduler();
            }
        }

        private bool IsProcessRunning(string processName)
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(processName);
                foreach (Process proc in processes)
                {
                    try
                    {
                        string exePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BackScene.exe");
                        if (string.Equals(proc.MainModule.FileName, exePath, StringComparison.OrdinalIgnoreCase))
                        {
                            currentProcess = proc; // Update reference
                            return true;
                        }
                    }
                    catch
                    {
                        // access denied or system process -> ignore
                    }
                }
            }
            catch
            {
                // access denied at process level -> ignore
            }
            return false;
        }

        private string GetLoggedInUser()
        {
            try
            {
                // Search for the first active user (not SYSTEM, not GUEST)
                using (var searcher = new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem"))
                {
                    foreach (var mo in searcher.Get())
                    {
                        var user = mo["UserName"] as string;
                        if (!string.IsNullOrEmpty(user))
                            return user;
                    }
                }
            }
            catch
            {
                // ignore
            }
            return null;
        }

        private void StartApplicationViaTaskScheduler()
        {
            string exePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BackScene.exe");
            string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.log");

            if (!File.Exists(exePath))
            {
                File.WriteAllText(logPath, $"[{DateTime.Now}] BackScene.exe file not found.");
                return;
            }

            if (IsProcessRunning("BackScene"))
            {
                File.WriteAllText(logPath, $"[{DateTime.Now}] Application is already running.");
                return;
            }

            try
            {
                string taskName = "BackSceneLauncherTask";

                // Delete any existing task
                RunSchtasksCommand($"/Delete /TN \"{taskName}\" /F");

                // Get current logged-in user (DOMAIN\User or MACHINE\User)
                string user = GetLoggedInUser();
                if (string.IsNullOrEmpty(user))
                {
                    // SYSTEM account
                    RunSchtasksCommand($"/Create /TN \"{taskName}\" /SC ONCE /TR \"\\\"{exePath}\\\"\" /ST 00:00 /RL HIGHEST /F");
                    File.AppendAllText(logPath, $"[{DateTime.Now}] Task created to run as SYSTEM.\r\n");
                }
                else
                {
                    // Interactive user
                    RunSchtasksCommand($"/Create /TN \"{taskName}\" /SC ONCE /TR \"\\\"{exePath}\\\"\" /ST 00:00 /RL HIGHEST /F /IT /RU \"{user}\"");
                    File.AppendAllText(logPath, $"[{DateTime.Now}] Task created for user: {user}\r\n");
                }

                // Run the task immediately
                RunSchtasksCommand($"/Run /TN \"{taskName}\"");

                File.AppendAllText(logPath, $"[{DateTime.Now}] Task launched successfully.\r\n");
            }
            catch (Exception ex)
            {
                File.AppendAllText(logPath, $"[{DateTime.Now}] Task Scheduler error: {ex}\r\n");
            }
        }

        private void RunSchtasksCommand(string arguments)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "schtasks",
                Arguments = arguments,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using (Process proc = Process.Start(psi))
            {
                proc.WaitForExit();
            }
        }

        protected override void OnStop()
        {
            timer?.Stop();

            string[] targets = { "BackScene", "mpv" };

            foreach (string target in targets)
            {
                try
                {
                    foreach (var proc in Process.GetProcessesByName(target))
                    {
                        try
                        {
                            proc.Kill();
                            proc.WaitForExit(5000); // wait up to 5 seconds
                            proc.Dispose();
                        }
                        catch
                        {
                            // Ignore failures for individual processes
                        }
                    }
                }
                catch
                {
                    // Ignore top-level failures for this process name
                }
            }
        }

    }
}
