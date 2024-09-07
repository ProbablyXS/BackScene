using BackScene.Utilities;
using System.Threading.Tasks;

namespace BackScene
{
    using Microsoft.Win32;
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Media;
    using System.Windows.Forms;

    public partial class Settings : Form
    {
        public bool _isRunning;

        public int FPS;

        public static bool isFading = false;
        public const int FadeDuration = 1000;
        public const float OpacityDecrement = 1.0f / FadeDuration;

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
                ShowLogscheckBox.Checked = iniConf.Read("show_logs", "BackScene") == "true";
                CloseMinimizescheckBox.Checked = iniConf.Read("close_minimizes", "BackScene") == "true";
                MuteAudiocheckBox.Checked = iniConf.Read("mute_audio", "BackScene") == "true";
                CleanMemorycheckBox.Checked = iniConf.Read("clean_memory", "BackScene") == "true";
                StartMinimizedcheckBox.Checked = iniConf.Read("start_minimized", "BackScene") == "true";
                PlayAtStartupcheckBox.Checked = iniConf.Read("play_at_startup", "BackScene") == "true";
                StartWithWindowscheckBox.Checked = iniConf.Read("start_with_windows", "BackScene") == "true";
                checkBox1.Checked = iniConf.Read("shuffle", "BackScene") == "true";
                checkBox2.Checked = iniConf.Read("limit_fps", "BackScene") == "true";

                if (iniConf.Read("fps", "BackScene") == "")
                {
                    iniConf.Write("fps", "60");
                }
                FPS = int.Parse(iniConf.Read("fps", "BackScene"));

                Processus.wallpaperPath = iniConf.Read("wallpaperPath", "BackScene");
                textBox1.Text = Processus.wallpaperPath;

                await Task.Delay(1000);
            }
        }

        public void StopConfigCheck()
        {
            _isRunning = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var showLogs = ShowLogscheckBox.Checked ? "true" : "false";
            iniConf.Write("show_logs", showLogs, "BackScene");

            if (ShowLogscheckBox.Checked)
            {
                Main.logsForm.Show();
            }
            else
            {
                Main.logsForm.Hide();
            }

            Main.logsForm.LogsWriteLine($"Console logs [{(ShowLogscheckBox.Checked ? "Enabled" : "Disabled")}]", false);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            var closeMinimizes = CloseMinimizescheckBox.Checked ? "true" : "false";
            iniConf.Write("close_minimizes", closeMinimizes, "BackScene");
            Main.logsForm.LogsWriteLine($"Close Minimizes [{(CloseMinimizescheckBox.Checked ? "Enabled" : "Disabled")}]", false);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            var muteAudio = MuteAudiocheckBox.Checked ? "true" : "false";
            iniConf.Write("mute_audio", muteAudio, "BackScene");
            Main.logsForm.LogsWriteLine($"Audio Mute [{(MuteAudiocheckBox.Checked ? "Enabled" : "Disabled")}]", false);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            var cleanMemory = CleanMemorycheckBox.Checked ? "true" : "false";
            iniConf.Write("clean_memory", cleanMemory, "BackScene");
            Main.logsForm.LogsWriteLine($"Clean Memory [{(CleanMemorycheckBox.Checked ? "Enabled" : "Disabled")}]", false);
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            var startMinimized = StartMinimizedcheckBox.Checked ? "true" : "false";
            iniConf.Write("start_minimized", startMinimized, "BackScene");
            Main.logsForm.LogsWriteLine($"Start Minimized [{(StartMinimizedcheckBox.Checked ? "Enabled" : "Disabled")}]", false);
        }

        private void PlayAtStartupcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var playAtStartup = PlayAtStartupcheckBox.Checked ? "true" : "false";
            var logMessage = PlayAtStartupcheckBox.Checked ? "Enabled" : "Disabled";

            iniConf.Write("play_at_startup", playAtStartup, "BackScene");
            Main.logsForm.LogsWriteLine($"Play at startup [{logMessage}]", false);
        }

        private void StartWithWindowscheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var playAtStartup = StartWithWindowscheckBox.Checked ? "true" : "false";
            var logMessage = StartWithWindowscheckBox.Checked ? "Enabled" : "Disabled";

            string exePath = Process.GetCurrentProcess().MainModule.FileName;
            string appName = Application.ProductName;

            if (playAtStartup == "true")
            {
                SetStartup(appName, exePath, true);
            }
            else
            {
                SetStartup(appName, exePath, false);
            }

            iniConf.Write("start_with_windows", playAtStartup, "BackScene");
            Main.logsForm.LogsWriteLine($"Start with windows [{logMessage}]", false);
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            var shuffle = checkBox1.Checked ? "true" : "false";
            iniConf.Write("shuffle", shuffle, "BackScene");
            Main.logsForm.LogsWriteLine($"Shuffle [{(checkBox1.Checked ? "Enabled" : "Disabled")}]", false);
        }

        public static void SetStartup(string appName, string exePath, bool add)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
            {
                if (key == null)
                {
                    throw new InvalidOperationException("Unable to access registry key.");
                }

                if (add)
                {
                    // Add the application to startup
                    key.SetValue(appName, exePath);
                    Console.WriteLine($"{appName} has been added to startup.");
                }
                else
                {
                    // Remove the application from startup
                    if (key.GetValue(appName) != null)
                    {
                        key.DeleteValue(appName);
                        Console.WriteLine($"{appName} has been removed from startup.");
                    }
                    else
                    {
                        Console.WriteLine($"{appName} was not found in startup.");
                    }
                }

            }
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            Main.logsForm.LogsWriteLine(Main.settingsForm.Name + " [closed]", false);
        }

        public bool Close_Minimizes()
        {
            if (CloseMinimizescheckBox.Checked)
            {
                return true;
            }
            return false;
        }

        public void textBox1_DragEnter(object sender, DragEventArgs e)
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

        public void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            string folderPath = "";

            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                folderPath = (string)e.Data.GetData(DataFormats.Text);
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    folderPath = files[0];
                }
            }

            if (!Processus.CheckWallpaperPath(folderPath)) return;

            iniConf.Write("wallpaperPath", folderPath, "BackScene");

            Processus.wallpaperPath = folderPath;

            var message = "Wallpaper Folder has been modified";
            _ = FadeOutLabel(message, Main.main.label4, false, true);
            Main.logsForm.LogsWriteLine(message, false);

        }

        public async Task FadeOutLabel(string message, Label label, bool error, bool sound)
        {
            if (isFading)
                return;

            isFading = true;

            if (sound)
            {
                var player = new SoundPlayer(error ? Properties.Resources.Rejected : Properties.Resources.Dropped);
                player.Play();
            }

            try
            {
                if (label.InvokeRequired)
                {
                    label.Invoke(new Action(() =>
                    {
                        label.Text = message;
                        label.Visible = true;
                    }));
                }
                else
                {
                    label.Text = message;
                    label.Visible = true;
                }

                float opacity = 1.0f;

                for (int i = 0; i < FadeDuration; i += 50)
                {
                    opacity -= OpacityDecrement * (50.0f / FadeDuration);
                    if (opacity < 0) opacity = 0;

                    if (label.InvokeRequired)
                    {
                        label.Invoke(new Action(() =>
                        {
                            label.ForeColor = Color.FromArgb((int)(opacity * 255), label.ForeColor);
                        }));
                    }
                    else
                    {
                        label.ForeColor = Color.FromArgb((int)(opacity * 255), label.ForeColor);
                    }

                    await Task.Delay(50);
                }

                if (label.InvokeRequired)
                {
                    label.Invoke(new Action(() =>
                    {
                        label.Visible = false;
                        label.Text = "";
                    }));
                }
                else
                {
                    label.Visible = false;
                    label.Text = "";
                }
            }
            catch (Exception ex)
            {
                Main.logsForm.LogsWriteLine(ex.Message, true);
            }
            finally
            {
                isFading = false;
            }
        }


        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MovingForm.ReleaseCapture();
                MovingForm.SendMessage(this.Handle, MovingForm.WM_NCLBUTTONDOWN, (IntPtr)MovingForm.HT_CAPTION, IntPtr.Zero);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            _ = AnimationForms.MinimizeForm(this, false);
        }

        private void Settings_Activated(object sender, EventArgs e)
        {
            _ = AnimationForms.OpenForm(this);
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            var limitFPS = checkBox2.Checked ? "true" : "false";
            iniConf.Write("limit_fps", limitFPS, "BackScene");
            Main.logsForm.LogsWriteLine($"Limit FPS [{(checkBox2.Checked ? "Enabled" : "Disabled")}]", false);
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var limitFPS = e.ClickedItem.Text;
            iniConf.Write("fps", limitFPS, "BackScene");
            Main.logsForm.LogsWriteLine($"Limit FPS set to [{limitFPS}]", false);
        }

        private void checkBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (checkBox2.Checked)
            {
                var screenLocation = checkBox2.PointToScreen(new Point(0, checkBox2.Height));
                contextMenuStrip1.Show(screenLocation);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _ = AnimationForms.MinimizeForm(this, false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _ = AnimationForms.MinimizeForm(this, false);
        }

        private void button_MouseAction(object sender, EventArgs e, bool isHover)
        {
            if (sender is Button button)
            {
                if (button == button1)
                {
                    button.BackgroundImage = isHover ? Properties.Resources.close_hover : Properties.Resources.close_normal;
                }
            }
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button_MouseAction(sender, e, true);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button_MouseAction(sender, e, false);
        }
    }
}
