using System;
using System.Runtime.InteropServices;
using System.Text;

namespace BackScene.Utilities
{
    internal class DesktopIntegration
    {
        // Constants for Windows API
        private const int GWL_STYLE = -16;
        private const int GWL_EXSTYLE = -20;
        private const int WS_CAPTION = 0x00C00000;
        private const int WS_THICKFRAME = 0x00040000;
        private const int WS_EX_APPWINDOW = 0x00040000;
        private const int WS_EX_TOOLWINDOW = 0x00000080;
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOZORDER = 0x0004;
        private const uint SWP_FRAMECHANGED = 0x0020;

        private const int SHCNE_ASSOCCHANGED = 0x8000000;
        private const int SHCNF_FLUSH = 0x1000;
        private const int SHCNE_UPDATEDIR = 0x00001000;
        private const int SHCNF_IDLIST = 0x0000;

        private const uint WM_PAINT = 0x000F;
        private const uint WM_SETTINGCHANGE = 0x001A;
        private const uint WM_CLOSE = 0x0010;
        private const uint WM_COMMAND = 0x0111;
        private const int WM_ERASEBKGND = 0x0014;

        private const int REFRESH_COMMAND_ID = 0xF5;  // Command ID for Refresh (F5)

        // Delegate for window enumeration callback
        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        // RECT structure for window positioning
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left, Top, Right, Bottom;
        }

        #region WinAPI Imports

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
            int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("shell32.dll")]
        private static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool DestroyWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);

        [DllImport("user32.dll")]
        private static extern bool UpdateWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SendMessageTimeout(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam,
            SendMessageTimeoutFlags fuFlags, uint uTimeout, out IntPtr lpdwResult);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        #endregion

        [Flags]
        private enum SendMessageTimeoutFlags : uint
        {
            SMTO_NORMAL = 0x0000,
            SMTO_BLOCK = 0x0001,
            SMTO_ABORTIFHUNG = 0x0002,
            SMTO_NOTIMEOUTIFNOTHUNG = 0x0008
        }

        /// <summary>
        /// Main method to integrate your window into the desktop's WorkerW and refresh the desktop icons.
        /// </summary>
        /// <param name="appWindowHandle">Handle to your application's window</param>
        /// <returns>True on success, false otherwise</returns>
        public static bool IntegrateWindowIntoWorkerW(IntPtr appWindowHandle)
        {
            if (appWindowHandle == IntPtr.Zero)
            {
                Console.WriteLine("Error: Invalid application window handle.");
                return false;
            }

            // Step 0: Close all existing WorkerW windows to ensure clean state
            RefreshDesktopOnly();

            System.Threading.Thread.Sleep(1000); // Allow system to process

            // Step 1: Retrieve the WorkerW handle
            IntPtr workerWHandle = GetWorkerWHandle();
            if (workerWHandle == IntPtr.Zero)
            {
                Console.WriteLine("Error: WorkerW window not found.");
                return false;
            }

            // Step 2: Set WorkerW as the parent of your application window
            IntPtr setParentResult = SetParent(appWindowHandle, workerWHandle);
            if (setParentResult == IntPtr.Zero)
            {
                Console.WriteLine("Error: Failed to set parent window.");
                return false;
            }

            // Step 3: Remove window borders and decorations for seamless integration
            RemoveWindowBorders(appWindowHandle);

            // Step 4: Position and resize your window to cover the WorkerW area
            PositionWindowToCoverWorkerW(appWindowHandle, workerWHandle);

            // Step 5: Notify the shell of association changes
            SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_FLUSH, IntPtr.Zero, IntPtr.Zero);

            // Step 6: Invalidate and update WorkerW to force repaint
            InvalidateRect(workerWHandle, IntPtr.Zero, true);
            UpdateWindow(workerWHandle);

            // Step 7: Post WM_PAINT message to WorkerW
            PostMessage(workerWHandle, WM_PAINT, IntPtr.Zero, IntPtr.Zero);

            // Step 8: Post global WM_SETTINGCHANGE message to refresh shell settings
            PostMessage(IntPtr.Zero, WM_SETTINGCHANGE, IntPtr.Zero, IntPtr.Zero);

            // Step 9: Explicitly refresh desktop icons
            RefreshDesktopIcons();

            return true;
        }

        /// <summary>
        /// Refreshes the desktop by invalidating and repainting the desktop icons.
        /// It locates the WorkerW window that hosts the desktop icons (SHELLDLL_DefView)
        /// and forces a redraw of the SysListView32 (FolderView) window.
        /// Additionally, it sends a shell notification to update associations, ensuring
        /// the desktop and start menu remain functional and up to date.
        /// </summary>
        public static void RefreshDesktopOnly()
        {
            IntPtr progman = FindWindow("Progman", null);
            IntPtr shellViewWin = FindWindowEx(progman, IntPtr.Zero, "SHELLDLL_DefView", null);

            if (shellViewWin == IntPtr.Zero)
            {
                IntPtr workerw = IntPtr.Zero;
                do
                {
                    workerw = FindWindowEx(IntPtr.Zero, workerw, "WorkerW", null);
                    shellViewWin = FindWindowEx(workerw, IntPtr.Zero, "SHELLDLL_DefView", null);
                } while (shellViewWin == IntPtr.Zero && workerw != IntPtr.Zero);
            }

            if (shellViewWin != IntPtr.Zero)
            {
                IntPtr sysListView = FindWindowEx(shellViewWin, IntPtr.Zero, "SysListView32", "FolderView");
                if (sysListView != IntPtr.Zero)
                {
                    // Invalidate and repaint desktop icons
                    InvalidateRect(sysListView, IntPtr.Zero, true);
                    UpdateWindow(sysListView);
                }

                // Notify shell about assoc changed (forces refresh)
                SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_FLUSH, IntPtr.Zero, IntPtr.Zero);
            }
        }




        /// <summary>
        /// Retrieves the WorkerW window handle behind desktop icons.
        /// </summary>
        /// <returns>Handle to WorkerW window or IntPtr.Zero if not found</returns>
        private static IntPtr GetWorkerWHandle()
        {
            IntPtr progman = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Progman", null);
            if (progman == IntPtr.Zero)
                return IntPtr.Zero;

            // Send message to Progman to create WorkerW
            IntPtr result;
            SendMessageTimeout(progman, 0x052C, IntPtr.Zero, IntPtr.Zero,
                SendMessageTimeoutFlags.SMTO_NORMAL, 1000, out result);

            IntPtr workerW = IntPtr.Zero;

            // Enumerate all windows to find SHELLDLL_DefView
            EnumWindows((hWnd, lParam) =>
            {
                IntPtr shellView = FindWindowEx(hWnd, IntPtr.Zero, "SHELLDLL_DefView", null);
                if (shellView != IntPtr.Zero)
                {
                    workerW = FindWindowEx(IntPtr.Zero, hWnd, "WorkerW", null);
                    return false; // Stop enumeration
                }
                return true; // Continue enumeration
            }, IntPtr.Zero);

            if (workerW != IntPtr.Zero)
                return workerW;

            // Fallback: Find WorkerW child of Progman
            workerW = FindWindowEx(progman, IntPtr.Zero, "WorkerW", null);
            return workerW;
        }

        /// <summary>
        /// Removes window borders and styles for a borderless appearance.
        /// </summary>
        private static void RemoveWindowBorders(IntPtr hWnd)
        {
            int style = GetWindowLong(hWnd, GWL_STYLE);
            int exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);

            style &= ~WS_CAPTION;
            style &= ~WS_THICKFRAME;

            exStyle &= ~WS_EX_APPWINDOW;
            exStyle |= WS_EX_TOOLWINDOW;

            SetWindowLong(hWnd, GWL_STYLE, style);
            SetWindowLong(hWnd, GWL_EXSTYLE, exStyle);

            SetWindowPos(hWnd, IntPtr.Zero, 0, 0, 0, 0,
                SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);
        }

        /// <summary>
        /// Positions and resizes a window to completely cover the WorkerW area.
        /// </summary>
        private static void PositionWindowToCoverWorkerW(IntPtr appWnd, IntPtr workerW)
        {
            if (GetWindowRect(workerW, out RECT rect))
            {
                int width = rect.Right - rect.Left;
                int height = rect.Bottom - rect.Top;

                SetWindowPos(appWnd, IntPtr.Zero, rect.Left, rect.Top, width, height, SWP_NOZORDER);
            }
        }

        /// <summary>
        /// Refreshes the desktop icons by sending appropriate messages to the shell.
        /// </summary>
        public static void RefreshDesktopIcons()
        {
            IntPtr progman = FindWindow("Progman", null);
            IntPtr shellViewWin = FindWindowEx(progman, IntPtr.Zero, "SHELLDLL_DefView", null);

            if (shellViewWin == IntPtr.Zero)
            {
                // Search inside WorkerW windows if not found under Progman
                IntPtr workerw = IntPtr.Zero;
                do
                {
                    workerw = FindWindowEx(IntPtr.Zero, workerw, "WorkerW", null);
                    shellViewWin = FindWindowEx(workerw, IntPtr.Zero, "SHELLDLL_DefView", null);
                } while (shellViewWin == IntPtr.Zero && workerw != IntPtr.Zero);
            }

            if (shellViewWin != IntPtr.Zero)
            {
                // Find the desktop icons window (SysListView32)
                IntPtr sysListView = FindWindowEx(shellViewWin, IntPtr.Zero, "SysListView32", "FolderView");
                if (sysListView != IntPtr.Zero)
                {
                    // Invalidate and force repaint
                    InvalidateRect(sysListView, IntPtr.Zero, true);
                    SendMessage(sysListView, WM_PAINT, IntPtr.Zero, IntPtr.Zero);
                    SendMessage(sysListView, WM_ERASEBKGND, IntPtr.Zero, IntPtr.Zero);
                }

                // Send Refresh command (F5)
                SendMessage(shellViewWin, WM_COMMAND, new IntPtr(REFRESH_COMMAND_ID), IntPtr.Zero);

                // Notify shell of directory update
                SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero);
                SHChangeNotify(SHCNE_UPDATEDIR, SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero);
            }
        }
    }
}
