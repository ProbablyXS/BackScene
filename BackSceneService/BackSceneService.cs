using Microsoft.Win32.TaskScheduler;
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
                using (TaskService ts = new TaskService())
                {
                    string taskName = "BackSceneLauncherTask";

                    // Delete existing task
                    var existingTask = ts.GetTask(taskName);
                    if (existingTask != null)
                    {
                        ts.RootFolder.DeleteTask(taskName);
                    }

                    TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = "Launch BackScene.exe via Task Scheduler";

                    // Trigger: when task is registered
                    td.Triggers.Add(new RegistrationTrigger());

                    // Action: run the executable
                    td.Actions.Add(new ExecAction(exePath, null, null));

                    // Retrieve currently logged-in user (e.g., "DOMAIN\\User")
                    string user = GetLoggedInUser();

                    if (string.IsNullOrEmpty(user))
                    {
                        File.AppendAllText(logPath, $"[{DateTime.Now}] Unable to determine the logged-in user. The task will be created with the SYSTEM user.\r\n");
                        td.Principal.UserId = null; // SYSTEM
                        td.Principal.LogonType = TaskLogonType.ServiceAccount;
                        td.Principal.RunLevel = TaskRunLevel.Highest;
                    }
                    else
                    {
                        td.Principal.UserId = user;
                        td.Principal.LogonType = TaskLogonType.InteractiveToken;
                        td.Principal.RunLevel = TaskRunLevel.Highest;
                        File.AppendAllText(logPath, $"[{DateTime.Now}] Logged-in user detected: {user}\r\n");
                    }

                    ts.RootFolder.RegisterTaskDefinition(taskName, td);

                    var task = ts.GetTask(taskName);

                    if (task != null)
                    {
                        task.Run();
                        File.AppendAllText(logPath, $"[{DateTime.Now}] Task launched successfully.\r\n");
                    }
                    else
                    {
                        File.AppendAllText(logPath, $"[{DateTime.Now}] Failed to retrieve task after registration.\r\n");
                    }
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(logPath, $"[{DateTime.Now}] Task Scheduler error: {ex}\r\n");
            }
        }

        protected override void OnStop()
        {
            timer?.Stop();
            if (currentProcess != null && !currentProcess.HasExited)
            {
                try
                {
                    currentProcess.Kill();
                    currentProcess.Dispose();
                }
                catch
                {
                    // Ignore exceptions
                }
            }
        }
    }
}
