using BackScene.Utilities;
using System.Threading.Tasks;

namespace BackScene
{

    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public partial class Settings : Form
    {
        private bool _isRunning;

        Clsini iniConf = new Clsini(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"config.ini"));

        public Settings()
        {
            InitializeComponent();
        }

        public async void StartConfigCheck()
        {

            if (_isRunning) return;

            _isRunning = true;

            while (_isRunning)
            {
                checkBox1.Checked = iniConf.Read("show_logs", "BackScene") == "true";

                checkBox2.Checked = iniConf.Read("close_minimizes", "BackScene") == "true";

                checkBox3.Checked = iniConf.Read("audio_mute", "BackScene") == "true";

                checkBox5.Checked = iniConf.Read("start_minimized", "BackScene") == "true";

                textBox1.Text = iniConf.Read("wallpaperPath", "BackScene");

                await Task.Delay(10);
            }
        }

        public void StopConfigCheck()
        {
            _isRunning = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                iniConf.Write("show_logs", "true", "BackScene");

                Main.logsForm.Show();
                Main.logsForm.LogsWriteLine("Logs console [enabled]", false);
            }
            else
            {
                iniConf.Write("show_logs", "false", "BackScene");
                Main.logsForm.Hide();
                Main.logsForm.LogsWriteLine("Logs console [disabled]", false);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                iniConf.Write("close_minimizes", "true", "BackScene");
                Main.logsForm.LogsWriteLine("Close Minimizes [enabled]", false);
            }
            else
            {
                iniConf.Write("close_minimizes", "false", "BackScene");
                Main.logsForm.LogsWriteLine("Close Minimizes [disabled]", false);
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                iniConf.Write("audio_mute", "true", "BackScene");
                Main.logsForm.LogsWriteLine("Audio Mute [enabled]", false);
            }
            else
            {
                iniConf.Write("audio_mute", "false", "BackScene");
                Main.logsForm.LogsWriteLine("Audio Mute [disabled]", false);
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                iniConf.Write("clean_memory", "true", "BackScene");
                Main.logsForm.LogsWriteLine("Clean Memory [enabled]", false);
            }
            else
            {
                iniConf.Write("clean_memory", "false", "BackScene");
                Main.logsForm.LogsWriteLine("Clean Memory [disabled]", false);
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                iniConf.Write("start_minimized", "true", "BackScene");
                Main.logsForm.LogsWriteLine("Start Minimized [enabled]", false);
            }
            else
            {
                iniConf.Write("start_minimized", "false", "BackScene");
                Main.logsForm.LogsWriteLine("Start Minimized [disabled]", false);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Processus.wallpaperPath = textBox1.Text.Trim();
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Prevent the form from closing
            e.Cancel = true;
            // Hide the form instead of closing it
            this.Hide();

            Main.logsForm.LogsWriteLine(Main.settingsForm.Name + " [closed]", false);
        }

        public bool Close_Minimizes()
        {
            if (checkBox2.Checked)
            {
                return true;
            }
            return false;
        }

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text) || e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                // Handle text drop
                string text = (string)e.Data.GetData(DataFormats.Text);
                iniConf.Write("wallpaperPath", text, "BackScene");
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Handle file drop (e.g., read file content into the TextBox)
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    iniConf.Write("wallpaperPath", files[0], "BackScene");
                }
            }

            Main.logsForm.LogsWriteLine("Wallpaper Folder has been modified", false);

        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }
    }
}
