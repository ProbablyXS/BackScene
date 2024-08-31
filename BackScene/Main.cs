using BackScene.Utilities;
using System;
using System.Windows.Forms;

namespace BackScene
{
    public partial class Main : Form
    {

        public static Main main;
        public static Settings settingsForm;
        public static Logs logsForm;

        public Main(Settings settingsFrm, Logs logsFrm)
        {
            InitializeComponent();

            main = this;
            settingsForm = settingsFrm;
            logsForm = logsFrm;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            logsForm.LogsWriteLine(Application.ProductName + " [opened]", false);

            // open logs if is true
            if (Main.settingsForm.ShowLogscheckBox.Checked)
            {
                Main.logsForm.BringToFront();
                Main.logsForm.Show();
            }

            // start minimized
            if (Main.settingsForm.StartMinimizedcheckBox.Checked)
            {
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                logsForm.LogsWriteLine(Application.ProductName + " is minimized", false);
            }

            if (Main.settingsForm.PlayAtStartupcheckBox.Checked) Processus.StartMpvProcess();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Processus.CloseMpvProcess();
            Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Processus.StartMpvProcess();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Processus.CloseMpvProcess();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            ClosingForm();
        }

        public void ClosingForm()
        {
            if (settingsForm.Close_Minimizes())
            {
                this.Hide();
                Main.logsForm.LogsWriteLine(this.Name + " [Minimized]", false);
            }
            else
            {
                Processus.CloseMpvProcess();
                Environment.Exit(0);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Processus.CheckIfAlreadyStarted())
            {
                Main.logsForm.DisplayMessage("The process is currently running in the background. Please close it before proceeding.", true);
                return;
            }

            if (settingsForm == null || settingsForm.IsDisposed)
            {
                settingsForm = new Settings();
            }

            if (settingsForm.Visible)
            {
                settingsForm.BringToFront();
            }
            else
            {
                logsForm.LogsWriteLine(settingsForm.Name + " [Opened]", false);
                settingsForm.Show();
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Show();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Processus.CloseMpvProcess();
            Application.Exit();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Show();
        }

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MovingForm.ReleaseCapture();
                MovingForm.SendMessage(this.Handle, MovingForm.WM_NCLBUTTONDOWN, (IntPtr)MovingForm.HT_CAPTION, IntPtr.Zero);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            ClosingForm();
        }

        private void Main_DragDrop(object sender, DragEventArgs e)
        {
            settingsForm.textBox1_DragDrop(sender, e);
        }

        private void Main_DragEnter(object sender, DragEventArgs e)
        {
            settingsForm.textBox1_DragEnter(sender, e);
        }
    }
}
