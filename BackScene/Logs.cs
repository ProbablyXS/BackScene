using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BackScene
{
    public partial class Logs : Form
    {
        // Import Windows API functions
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        private const int WM_NCLBUTTONDOWN = 0x00A1;
        private const int HT_CAPTION = 0x0002;

        public Logs()
        {
            InitializeComponent();
        }

        private void Logs_Load(object sender, EventArgs e)
        {

        }

        public void LogsWriteLine(string message, bool error)
        {

            if (error)
            {
                Log(message, Color.Red);
            }
            else
            {
                Log(message, Color.Cyan);
            }
        }

        public void Log(string message, Color color)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.BeginInvoke(new Action(() => AppendText(message, color)));
            }
            else
            {
                AppendText(message, color);
            }
        }

        private void AppendText(string message, Color color)
        {
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.SelectionLength = 0;
            richTextBox1.SelectionColor = color;
            richTextBox1.AppendText(message + Environment.NewLine);
            richTextBox1.SelectionColor = richTextBox1.ForeColor; // Reset to default color
            richTextBox1.ScrollToCaret(); // Scroll to the end
        }

        private void Logs_FormClosing(object sender, FormClosingEventArgs e)
        {
            Main.settingsForm.checkBox1.Checked = false;
            // Prevent the form from closing
            e.Cancel = true;
            // Hide the form instead of closing it
            this.Hide();
        }

        private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, (IntPtr)HT_CAPTION, IntPtr.Zero);
            }
        }

        private void richTextBox1_Enter(object sender, EventArgs e)
        {
            //disable blinking cursor text
            ActiveControl = null;
        }
    }
}
