using BackScene.Utilities;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BackScene
{
    public partial class Logs : Form
    {

        public Logs()
        {
            InitializeComponent();
        }

        public void DisplayMessage(string message, bool error)
        {
            if (Main.settingsForm.ShowLogscheckBox.Checked)
            {
                LogsWriteLine(message, error);
            }
            else
            {
                MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public void LogsWriteLine(string message, bool error)
        {
            Color color = error ? Color.Red : Color.Cyan;

            Log(message, color);
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

            richTextBox1.SelectionColor = richTextBox1.ForeColor;
            richTextBox1.ScrollToCaret();
        }


        private void Logs_FormClosing(object sender, FormClosingEventArgs e)
        {
            Main.settingsForm.ShowLogscheckBox.Checked = false;
            e.Cancel = true;

            this.Hide();
        }

        private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MovingForm.ReleaseCapture();
                MovingForm.SendMessage(this.Handle, MovingForm.WM_NCLBUTTONDOWN, (IntPtr)MovingForm.HT_CAPTION, IntPtr.Zero);
            }
        }

        private void richTextBox1_Enter(object sender, EventArgs e)
        {
            //disable blinking cursor text
            //ActiveControl = null;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private const int WM_VSCROLL = 0x0115;
        private const int SB_LINEDOWN = 1;
        private const int SB_LINEUP = 0;

        private void RichTextBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                SendMessage(richTextBox1.Handle, WM_VSCROLL, (IntPtr)SB_LINEUP, IntPtr.Zero);
            }
            else if (e.Delta < 0)
            {
                SendMessage(richTextBox1.Handle, WM_VSCROLL, (IntPtr)SB_LINEDOWN, IntPtr.Zero);
            }
        }
    }
}
