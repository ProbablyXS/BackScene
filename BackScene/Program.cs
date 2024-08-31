using BackScene.Utilities;
using System;
using System.Windows.Forms;

namespace BackScene
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Register the exit event handler
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Settings settings = new Settings();
            Logs logs = new Logs();

            Main main = new Main(settings, logs);

            settings.StartConfigCheck();

            Application.Run(main);
        }

        // Event handler for process exit
        public static void OnProcessExit(object sender, EventArgs e)
        {
            Processus.CloseMpvProcess();
        }
    }
}
