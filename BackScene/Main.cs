﻿using BackScene.Utilities;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace BackScene
{
    public partial class Main : Form
    {

        //By default borderless forms are not designed to be minimized, which means when the form’s FormBorderStyle property is set to None you will notice that clicking the application box in taskbar does not minimize the form.
        //This can be fixed by overriding CreateParams and adding the WS_MINIMIZEBOX style to the Window and CS_DBLCLKS to the Window class styles.
        //Simply place the following code inside your Form’s class which you want to enable the minimize functionality using the taskbar.
        const int WS_MINIMIZEBOX = 0x20000;
        const int CS_DBLCLKS = 0x8;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= WS_MINIMIZEBOX;
                cp.ClassStyle |= CS_DBLCLKS;
                return cp;
            }
        }

        public static MPVController _mpvController;

        public static Main main;
        public static Settings settingsForm;
        public static Logs logsForm;

        public Main(Settings settingsFrm, Logs logsFrm)
        {
            InitializeComponent();

            _mpvController = new MPVController();

            main = this;
            settingsForm = settingsFrm;
            logsForm = logsFrm;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            logsForm.LogsWriteLine(Application.ProductName + " [Opened]", false);

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
            else { AnimationForms.OpenForm(this); }

            if (Main.settingsForm.PlayAtStartupcheckBox.Checked) Processus.StartMpvProcess();
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
                AnimationForms.MinimizeForm(this, false);
                Main.logsForm.LogsWriteLine(this.Name + " [Minimized]", false);
            }
            else
            {
                Processus.CloseMpvProcess();
                AnimationForms.CloseForm(this);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenSettings();
        }

        private void OpenSettings()
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

            settingsForm.Location = this.Location;

            if (settingsForm.Visible)
            {
                settingsForm.BringToFront();
            }
            else
            {
                logsForm.LogsWriteLine(settingsForm.Name + " [Opened]", false);
                settingsForm.ShowDialog();
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Show();
            AnimationForms.OpenForm(this);
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Processus.CloseMpvProcess();
            Application.Exit();
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
            AnimationForms.MinimizeForm(this, true);
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

        private void Main_Activated(object sender, EventArgs e)
        {
            AnimationForms.OpenForm(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClosingForm();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AnimationForms.MinimizeForm(this, true);
        }

        public void button5_MouseHover(object sender, EventArgs e)
        {
            button_MouseAction(sender, e, true);
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button_MouseAction(sender, e, false);
        }

        private void button_MouseAction(object sender, EventArgs e, bool isHover)
        {
            if (sender is Button button)
            {
                if (button == button5)
                {
                    button.BackgroundImage = isHover ? Properties.Resources.hide_over : Properties.Resources.hide_normal;
                }
                else if (button == button4)
                {
                    button.BackgroundImage = isHover ? Properties.Resources.close_hover : Properties.Resources.close_normal;
                }
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Processus.StartMpvProcess();
        }

        private void stopToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Processus.CloseMpvProcess();
        }

        private void nextToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _ = _mpvController.SendCommandToMPV("playlist_next", new object[] { });
        }

        private void previousToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _ = _mpvController.SendCommandToMPV("playlist_prev", new object[] { });
        }

        private void playToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _ = _mpvController.SendCommandToMPV("set_property", new object[] { "pause", false });
        }

        private void pauseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _ = _mpvController.SendCommandToMPV("set_property", new object[] { "pause", true });
        }

        private void muteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _ = _mpvController.SendCommandToMPV("set_property", new object[] { "mute", true });
        }

        private void unmuteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ = _mpvController.SendCommandToMPV("set_property", new object[] { "mute", false });
        }

        private void showToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Show();
        }

        private void settingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenSettings();
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/ProbablyXS/BackScene");
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Processus.CloseMpvProcess();
            Environment.Exit(0);
        }
    }
}
