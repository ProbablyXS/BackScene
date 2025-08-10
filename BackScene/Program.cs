using BackScene.Utilities;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace BackScene
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            if (IsAlreadyRunning())
            {
                // Already running — exit silently
                return;
            }

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Settings settings = new Settings();
            Logs logs = new Logs();

            Main main = new Main(settings, logs);

            logs.LogsWriteLine($"Checking [config.ini]", false);
            settings.StartConfigCheck();

            Application.Run(main);
        }

        private static bool IsAlreadyRunning()
        {
            string processName = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length > 1;
        }

        public static void OnProcessExit(object sender, EventArgs e)
        {
            Processus.CloseMpvProcess();
        }
    }
}
