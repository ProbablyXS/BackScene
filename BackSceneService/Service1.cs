//WORKING WITH WINDOWS 10


using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Timers;

namespace BackSceneService
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer;
        private Process currentProcess; // Conserver une référence au processus en cours

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer
            {
                Interval = 10000 // Intervalle de 10 secondes pour l'exemple
            };
            timer.Elapsed += OnElapsedTime;
            timer.Start();

            StartApplication();
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            // Vérifiez si l'application est déjà en cours d'exécution
            if (currentProcess == null || currentProcess.HasExited)
            {
                StartApplication();
            }
        }

        private void StartApplication()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BackScene.exe");
            if (!File.Exists(path))
            {
                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.log"), "Le fichier BackScene.exe est introuvable.");
                return;
            }

            // Vérifiez si le processus est déjà en cours d'exécution
            if (IsProcessRunning("BackScene"))
            {
                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.log"), "L'application est déjà en cours d'exécution.");
                return;
            }

            IntPtr userToken = IntPtr.Zero;
            if (!WTSQueryUserToken(WTSGetActiveConsoleSessionId(), out userToken))
            {
                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.log"), $"Échec de l'obtention du jeton utilisateur. Erreur : {Marshal.GetLastWin32Error()}");
                return;
            }

            var startupInfo = new STARTUPINFO
            {
                cb = Marshal.SizeOf(typeof(STARTUPINFO)),
                dwFlags = 0x00000001, // STARTF_USESHOWWINDOW
                wShowWindow = 5 // SW_SHOW
            };

            var processInfo = new PROCESS_INFORMATION();

            bool result = CreateProcessAsUser(
                userToken,
                null,
                path,
                IntPtr.Zero,
                IntPtr.Zero,
                false,
                0,
                IntPtr.Zero,
                null,
                ref startupInfo,
                out processInfo
            );

            if (!result)
            {
                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.log"), $"Échec de la création du processus. Erreur : {Marshal.GetLastWin32Error()}");
            }
            else
            {
                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.log"), "L'application a été lancée avec succès.");
                currentProcess = Process.GetProcessById(processInfo.dwProcessId); // Conservez la référence du processus
                CloseHandle(processInfo.hProcess);
                CloseHandle(processInfo.hThread);
            }

            CloseHandle(userToken);
        }

        protected override void OnStop()
        {
            timer.Stop();
            // Fermez le processus si il est en cours d'exécution
            if (currentProcess != null && !currentProcess.HasExited)
            {
                currentProcess.Kill();
                currentProcess.Dispose();
            }
        }

        private bool IsProcessRunning(string processName)
        {
            foreach (Process proc in Process.GetProcessesByName(processName))
            {
                // Vérifiez si le processus en cours est le même que celui lancé par ce service
                if (proc.MainModule.FileName.Equals(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BackScene.exe"), StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        [DllImport("Wtsapi32.dll", SetLastError = true)]
        private static extern bool WTSQueryUserToken(int sessionId, out IntPtr Token);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool CreateProcessAsUser(
            IntPtr hToken,
            string lpApplicationName,
            string lpCommandLine,
            IntPtr lpProcessAttributes,
            IntPtr lpThreadAttributes,
            bool bInheritHandles,
            uint dwCreationFlags,
            IntPtr lpEnvironment,
            string lpCurrentDirectory,
            [In] ref STARTUPINFO lpStartupInfo,
            out PROCESS_INFORMATION lpProcessInformation
        );

        [StructLayout(LayoutKind.Sequential)]
        public struct STARTUPINFO
        {
            public int cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public int dwX;
            public int dwY;
            public int dwXSize;
            public int dwYSize;
            public int dwXCountChars;
            public int dwYCountChars;
            public int dwFillAttribute;
            public int dwFlags;
            public short wShowWindow;
            public short cbReserved2;
            public byte lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int WTSGetActiveConsoleSessionId();
    }
}
