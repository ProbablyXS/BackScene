using BackScene.Utilities;
using System;
using System.Windows.Forms;

namespace BackScene
{
    public partial class Main : Form
    {

        public static Settings settingsForm;
        public static Logs logsForm;

        public Main(Settings settingsFrm, Logs logsFrm)
        {
            InitializeComponent();

            settingsForm = settingsFrm;
            logsForm = logsFrm;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            logsForm.LogsWriteLine(Application.ProductName + " [opened]", false);

            if (settingsForm.checkBox5.Checked)
            {
                logsForm.LogsWriteLine(Application.ProductName + " is minimized", false);
            }

            // open logs if is true
            if (Main.settingsForm.checkBox1.Checked)
            {
                Main.logsForm.BringToFront();
                Main.logsForm.Show();
            }

            // start minimized
            if (Main.settingsForm.checkBox5.Checked)
            {
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
            }
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
            // Check if the CheckBox is checked
            if (settingsForm.Close_Minimizes())
            {
                //Prevent the form from closing
                e.Cancel = true;
                //Hide the form instead
                this.Hide();

                Main.logsForm.LogsWriteLine(this.Name + " [minimized]", false);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Processus.CheckIfAlreadyStarted()) 
            {
                MessageBox.Show("The process is currently running in the background. Please close it before proceeding.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Check if the settings form instance already exists
            if (settingsForm == null || settingsForm.IsDisposed)
            {
                // Create a new instance if it doesn't exist or has been disposed
                settingsForm = new Settings();
            }

            // Check if the settings form is currently active
            if (settingsForm.Visible)
            {
                // The form is already open
                settingsForm.BringToFront(); // Bring the existing form to the front
            }
            else
            {
                logsForm.LogsWriteLine(settingsForm.Name + " [opened]", false);
                // Show the settings form if it is not currently visible
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
    }
}
