using BackScene.Properties;
using BackScene.Utilities;
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
            contextMenuStrip2.Renderer = null;
            foreach (Control control in base.Controls)
            {
                DisableTabStopAndFocus(control);
            }
            _mpvController = new MPVController();
            main = this;
            settingsForm = settingsFrm;
            logsForm = logsFrm;
        }

        private void DisableTabStopAndFocus(Control ctrl)
        {
            ctrl.TabStop = false;
            ctrl.GotFocus += delegate (object sender, EventArgs e)
            {
                if (sender is Control control)
                {
                    control.Parent.Focus();
                }
            };
            if (ctrl.Controls.Count <= 0)
            {
                return;
            }
            foreach (Control control2 in ctrl.Controls)
            {
                DisableTabStopAndFocus(control2);
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            logsForm.LogsWriteLine(Application.ProductName + " [Opened]", error: false);
            if (settingsForm.ShowLogscheckBox.Checked)
            {
                logsForm.BringToFront();
                logsForm.Show();
            }
            if (settingsForm.StartMinimizedcheckBox.Checked)
            {
                base.WindowState = FormWindowState.Minimized;
                base.ShowInTaskbar = false;
                Hide();
                logsForm.LogsWriteLine(Application.ProductName + " is minimized", error: false);
            }
            else
            {
                AnimationForms.OpenForm(this);
            }
            if (settingsForm.PlayAtStartupcheckBox.Checked)
            {
                Processus.StartMpvProcess();
            }
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
                AnimationForms.MinimizeForm(this, minimize: false);
                logsForm.LogsWriteLine(base.Name + " [Minimized]", error: false);
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
                logsForm.DisplayMessage("The process is currently running in the background. Please close it before proceeding.", error: true);
                return;
            }
            if (settingsForm == null || settingsForm.IsDisposed)
            {
                settingsForm = new Settings();
            }
            settingsForm.Location = base.Location;
            AnimationForms.MinimizeForm(this, minimize: false);
            if (settingsForm.Visible)
            {
                settingsForm.BringToFront();
                return;
            }
            logsForm.LogsWriteLine(settingsForm.Name + " [Opened]", error: false);
            settingsForm.ShowDialog();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (settingsForm == null || !settingsForm.Visible)
            {
                base.WindowState = FormWindowState.Normal;
                base.ShowInTaskbar = true;
                Show();
                AnimationForms.OpenForm(this);
            }
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
                MovingForm.SendMessage(base.Handle, 161, (IntPtr)2, IntPtr.Zero);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            AnimationForms.MinimizeForm(this, minimize: true);
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
            AnimationForms.MinimizeForm(this, minimize: true);
        }

        public void button5_MouseHover(object sender, EventArgs e)
        {
            button_MouseAction(sender, e, isHover: true);
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button_MouseAction(sender, e, isHover: false);
        }

        private void button_MouseAction(object sender, EventArgs e, bool isHover)
        {
            if (sender is Button button)
            {
                if (button == button5)
                {
                    button.BackgroundImage = (isHover ? Resources.hide_over : Resources.hide_normal);
                }
                else if (button == button4)
                {
                    button.BackgroundImage = (isHover ? Resources.close_hover : Resources.close_normal);
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
            _mpvController.SendCommandToMPV("playlist_next", new object[0]);
        }

        private void previousToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _mpvController.SendCommandToMPV("playlist_prev", new object[0]);
        }

        private void playToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _mpvController.SendCommandToMPV("set_property", new object[2] { "pause", false });
        }

        private void pauseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _mpvController.SendCommandToMPV("set_property", new object[2] { "pause", true });
        }

        private void muteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _mpvController.SendCommandToMPV("set_property", new object[2] { "mute", true });
        }

        private void unmuteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mpvController.SendCommandToMPV("set_property", new object[2] { "mute", false });
        }

        private void showToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            base.WindowState = FormWindowState.Normal;
            base.ShowInTaskbar = true;
            Show();
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
