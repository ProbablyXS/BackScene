namespace BackScene
{
    using BackScene.Properties;
    using BackScene.Utilities;
    using Microsoft.Win32;
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Media;
    using System.Reflection;
    using System.ServiceProcess;
    using System.Threading;
    using System.Threading.Tasks;
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

            foreach (Control control in base.Controls)
            {
                DisableTabStopAndFocus(control);
            }

            DisplayComboBox.SelectedIndexChanged -= DisplayComboBox_SelectedIndexChanged;

            DisplayComboBox.Items.Clear();
            var screens = Screen.AllScreens;
            for (int i = 0; i < screens.Length; i++)
            {
                DisplayComboBox.Items.Add($"Display {i + 1} ({screens[i].Bounds.Width}x{screens[i].Bounds.Height})");
            }

            DisplayComboBox.SelectedIndex = Convert.ToInt32(iniConf.Read("display", "BackScene"));

            DisplayComboBox.SelectedIndexChanged += DisplayComboBox_SelectedIndexChanged;
        }


        public async void StartConfigCheck()
        {
            if (_isRunning)
            {
                return;
            }
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
                if (!DisplayComboBox.DroppedDown && DisplayComboBox.SelectedIndex != -1)
                {
                    DisplayComboBox.SelectedIndex = Convert.ToInt32(iniConf.Read("display", "BackScene"));
                }
                checkBox3.Checked = iniConf.Read("pause_on_fullscreen", "BackScene") == "true";
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
            string value = (ShowLogscheckBox.Checked ? "true" : "false");
            iniConf.Write("show_logs", value, "BackScene");
            if (ShowLogscheckBox.Checked)
            {
                Main.logsForm.Show();
            }
            else
            {
                Main.logsForm.Hide();
            }
            Main.logsForm.LogsWriteLine("Console logs [" + (ShowLogscheckBox.Checked ? "Enabled" : "Disabled") + "]", error: false);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            string value = (CloseMinimizescheckBox.Checked ? "true" : "false");
            iniConf.Write("close_minimizes", value, "BackScene");
            Main.logsForm.LogsWriteLine("Close Minimizes [" + (CloseMinimizescheckBox.Checked ? "Enabled" : "Disabled") + "]", error: false);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            string value = (MuteAudiocheckBox.Checked ? "true" : "false");
            iniConf.Write("mute_audio", value, "BackScene");
            Main.logsForm.LogsWriteLine("Audio Mute [" + (MuteAudiocheckBox.Checked ? "Enabled" : "Disabled") + "]", error: false);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            string value = (CleanMemorycheckBox.Checked ? "true" : "false");
            iniConf.Write("clean_memory", value, "BackScene");
            Main.logsForm.LogsWriteLine("Clean Memory [" + (CleanMemorycheckBox.Checked ? "Enabled" : "Disabled") + "]", error: false);
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            string value = (StartMinimizedcheckBox.Checked ? "true" : "false");
            iniConf.Write("start_minimized", value, "BackScene");
            Main.logsForm.LogsWriteLine("Start Minimized [" + (StartMinimizedcheckBox.Checked ? "Enabled" : "Disabled") + "]", error: false);
        }

        private void PlayAtStartupcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string value = (PlayAtStartupcheckBox.Checked ? "true" : "false");
            string text = (PlayAtStartupcheckBox.Checked ? "Enabled" : "Disabled");
            iniConf.Write("play_at_startup", value, "BackScene");
            Main.logsForm.LogsWriteLine("Play at startup [" + text + "]", error: false);
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            string value = (checkBox1.Checked ? "true" : "false");
            iniConf.Write("shuffle", value, "BackScene");
            Main.logsForm.LogsWriteLine("Shuffle [" + (checkBox1.Checked ? "Enabled" : "Disabled") + "]", error: false);
        }

        public void SetStartup(string appName, string exePath, bool add)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", writable: true);
            if (add)
            {
                if (MessageBox.Show("Do you want to set the program to high priority?", "Set Priority", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {

                        string serviceName = Assembly.GetExecutingAssembly().GetName().Name;
                        string arguments = $"create {serviceName} binPath= \"{exePath}\" start= auto";

                        // Create the service
                        ExecuteCommand("sc", arguments);

                        // Start the service
                        ExecuteCommand("sc", $"start {serviceName}");

                        return;
                    }
                    catch
                    {
                        return;
                    }
                }
                if (registryKey == null)
                {
                    throw new InvalidOperationException("Unable to access registry key.");
                }
                string value = "cmd /c start \"\" /high \"" + exePath + "\"";
                registryKey.SetValue(appName, value);
                Console.WriteLine(appName + " has been added to startup with high priority.");
            }
            else
            {
                if (registryKey.GetValue(appName) != null)
                {
                    registryKey.DeleteValue(appName);
                    Console.WriteLine(appName + " has been removed from startup.");
                }
                else
                {
                    Console.WriteLine(appName + " was not found in startup.");
                }
                string name = Assembly.GetExecutingAssembly().GetName().Name;
                StopAndDeleteService(name, exePath);
            }
        }

        private void StopAndDeleteService(string serviceName, string exePath)
        {
            try
            {
                // 1. Delete the scheduled task via schtasks command
                try
                {
                    string taskName = "BackSceneLauncherTask";
                    ExecuteCommand("schtasks", $"/Delete /TN \"{taskName}\" /F");
                    Console.WriteLine($"Scheduled task deleted: {taskName}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to delete scheduled task: {ex.Message}");
                }


                using (ServiceController serviceController = new ServiceController(serviceName))
                {
                    Console.WriteLine("Deleting service: " + serviceName + "...");
                    ExecuteCommand("sc", "delete " + serviceName);

                    if (serviceController.Status == ServiceControllerStatus.Running || serviceController.Status == ServiceControllerStatus.Paused)
                    {
                        Console.WriteLine("Stopping service: " + serviceName + "...");
                        ExecuteCommand("sc", "stop " + serviceName);
                        serviceController.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10.0));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void ExecuteCommand(string fileName, string arguments)
        {
            using (Process process = Process.Start(new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                Verb = "runas"
            }))
            {
                string message = process.StandardOutput.ReadToEnd();
                process.StandardError.ReadToEnd();
                process.WaitForExit();
                if (process.ExitCode == 0)
                {
                    Main.logsForm.LogsWriteLine(message, error: false);
                }
            }
        }


        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
            Main.logsForm.LogsWriteLine(Main.settingsForm.Name + " [closed]", error: false);
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
            string text = "";
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                text = (string)e.Data.GetData(DataFormats.Text);
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (array.Length != 0)
                {
                    text = array[0];
                }
            }
            if (Processus.CheckWallpaperPath(text))
            {
                iniConf.Write("wallpaperPath", text, "BackScene");
                Processus.wallpaperPath = text;
                string message = "Wallpaper Folder has been modified";
                FadeOutLabel(message, Main.main.label4, error: false, sound: true);
                Main.logsForm.LogsWriteLine(message, error: false);
            }
        }

        public async Task FadeOutLabel(string message, Label label, bool error, bool sound)
        {
            if (isFading)
            {
                return;
            }
            isFading = true;
            if (sound)
            {
                new SoundPlayer(error ? Resources.Rejected : Resources.Dropped).Play();
            }
            try
            {
                if (label.InvokeRequired)
                {
                    label.Invoke((Action)delegate
                    {
                        label.Text = message;
                        label.Visible = true;
                    });
                }
                else
                {
                    label.Text = message;
                    label.Visible = true;
                }
                float opacity = 1f;
                for (int i = 0; i < 1000; i += 50)
                {
                    opacity -= 5.00000024E-05f;
                    if (opacity < 0f)
                    {
                        opacity = 0f;
                    }
                    if (label.InvokeRequired)
                    {
                        label.Invoke((Action)delegate
                        {
                            label.ForeColor = Color.FromArgb((int)(opacity * 255f), label.ForeColor);
                        });
                    }
                    else
                    {
                        label.ForeColor = Color.FromArgb((int)(opacity * 255f), label.ForeColor);
                    }
                    await Task.Delay(50);
                }
                if (label.InvokeRequired)
                {
                    label.Invoke((Action)delegate
                    {
                        label.Visible = false;
                        label.Text = "";
                    });
                }
                else
                {
                    label.Visible = false;
                    label.Text = "";
                }
            }
            catch (Exception ex)
            {
                Main.logsForm.LogsWriteLine(ex.Message, error: true);
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
                MovingForm.SendMessage(base.Handle, 161, (IntPtr)2, IntPtr.Zero);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            AnimationForms.MinimizeForm(this, minimize: false);
        }

        private void Settings_Activated(object sender, EventArgs e)
        {
            AnimationForms.OpenForm(this);
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            string value = (checkBox2.Checked ? "true" : "false");
            iniConf.Write("limit_fps", value, "BackScene");
            Main.logsForm.LogsWriteLine("Limit FPS [" + (checkBox2.Checked ? "Enabled" : "Disabled") + "]", error: false);
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string text = e.ClickedItem.Text;
            iniConf.Write("fps", text, "BackScene");
            Main.logsForm.LogsWriteLine("Limit FPS set to [" + text + "]", error: false);
        }

        private void checkBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (checkBox2.Checked)
            {
                Point screenLocation = checkBox2.PointToScreen(new Point(0, checkBox2.Height));
                contextMenuStrip1.Show(screenLocation);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AnimationForms.MinimizeForm(this, minimize: false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AnimationForms.MinimizeForm(this, minimize: false);
            Main.main.Show();
        }

        private void button_MouseAction(object sender, EventArgs e, bool isHover)
        {
            if (sender is Button button && button == button1)
            {
                button.BackgroundImage = (isHover ? Resources.close_hover : Resources.close_normal);
            }
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button_MouseAction(sender, e, isHover: true);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button_MouseAction(sender, e, isHover: false);
        }

        private void StartWithWindowscheckBox_Click(object sender, EventArgs e)
        {
            string text = (StartWithWindowscheckBox.Checked ? "true" : "false");
            string text2 = (StartWithWindowscheckBox.Checked ? "Enabled" : "Disabled");
            string exePath = AppDomain.CurrentDomain.BaseDirectory + "BackSceneService.exe";
            string productName = Application.ProductName;
            if (text == "true")
            {
                iniConf.Write("start_with_windows", text, "BackScene");
                SetStartup(productName, exePath, add: true);
            }
            else
            {
                iniConf.Write("start_with_windows", text, "BackScene");
                SetStartup(productName, exePath, add: false);
            }
            Main.logsForm.LogsWriteLine("Start with windows [" + text2 + "]", error: false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    string selectedPath = folderBrowserDialog.SelectedPath;
                    if (Processus.CheckWallpaperPath(selectedPath))
                    {
                        iniConf.Write("wallpaperPath", selectedPath, "BackScene");
                        Processus.wallpaperPath = selectedPath;
                        string message = "Wallpaper Folder has been modified";
                        FadeOutLabel(message, Main.main.label4, error: false, sound: true);
                        Main.logsForm.LogsWriteLine(message, error: false);
                    }
                }
        }

        private void DisableTabStopAndFocus(Control ctrl)
        {
            // Skip the DisplayComboBox or any other interactive control
            if (ctrl == DisplayComboBox)
                return;

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


        private void checkBox3_CheckedChanged_1(object sender, EventArgs e)
        {
            string value = (checkBox3.Checked ? "true" : "false");
            iniConf.Write("pause_on_fullscreen", value, "BackScene");
            Main.logsForm.LogsWriteLine("Pause on fullscreen [" + (checkBox3.Checked ? "Enabled" : "Disabled") + "]", error: false);
        }

        private CancellationTokenSource _cts;
        private RedBorderManager _redBorderManager = new RedBorderManager();

        private async void DisplayComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
            _cts = new CancellationTokenSource();

            int selectedIndex = DisplayComboBox.SelectedIndex;
            iniConf.Write("display", selectedIndex.ToString(), "BackScene");
            Main.logsForm.LogsWriteLine($"Display [{selectedIndex}] selected", error: false);

            if (selectedIndex >= 0 && selectedIndex < Screen.AllScreens.Length)
            {
                _redBorderManager.Show(Screen.AllScreens[selectedIndex]);

                try
                {
                    await Task.Delay(1200, _cts.Token);
                    _redBorderManager.Hide();
                }
                catch (TaskCanceledException)
                {
                }
            }
            else
            {
                _redBorderManager.Hide();
            }
        }
    }
}
