using System;
using System.Runtime.InteropServices;

namespace BackScene.Utilities
{
    class MovingForm
    {

        // Import Windows API functions
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        public const int WM_NCLBUTTONDOWN = 0x00A1;
        public const int HT_CAPTION = 0x0002;



    }
}
