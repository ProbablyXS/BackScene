
namespace BackScene
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.ShowLogscheckBox = new System.Windows.Forms.CheckBox();
            this.CloseMinimizescheckBox = new System.Windows.Forms.CheckBox();
            this.MuteAudiocheckBox = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.CleanMemorycheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StartMinimizedcheckBox = new System.Windows.Forms.CheckBox();
            this.PlayAtStartupcheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ShowLogscheckBox
            // 
            this.ShowLogscheckBox.AutoSize = true;
            this.ShowLogscheckBox.Checked = true;
            this.ShowLogscheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowLogscheckBox.Location = new System.Drawing.Point(12, 12);
            this.ShowLogscheckBox.Name = "ShowLogscheckBox";
            this.ShowLogscheckBox.Size = new System.Drawing.Size(75, 17);
            this.ShowLogscheckBox.TabIndex = 0;
            this.ShowLogscheckBox.Text = "Show logs";
            this.ShowLogscheckBox.UseVisualStyleBackColor = true;
            this.ShowLogscheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // CloseMinimizescheckBox
            // 
            this.CloseMinimizescheckBox.AutoSize = true;
            this.CloseMinimizescheckBox.Location = new System.Drawing.Point(12, 58);
            this.CloseMinimizescheckBox.Name = "CloseMinimizescheckBox";
            this.CloseMinimizescheckBox.Size = new System.Drawing.Size(99, 17);
            this.CloseMinimizescheckBox.TabIndex = 1;
            this.CloseMinimizescheckBox.Text = "Close minimizes";
            this.CloseMinimizescheckBox.UseVisualStyleBackColor = true;
            this.CloseMinimizescheckBox.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // MuteAudiocheckBox
            // 
            this.MuteAudiocheckBox.AutoSize = true;
            this.MuteAudiocheckBox.Checked = true;
            this.MuteAudiocheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MuteAudiocheckBox.Location = new System.Drawing.Point(314, 12);
            this.MuteAudiocheckBox.Name = "MuteAudiocheckBox";
            this.MuteAudiocheckBox.Size = new System.Drawing.Size(79, 17);
            this.MuteAudiocheckBox.TabIndex = 2;
            this.MuteAudiocheckBox.Text = "Mute audio";
            this.MuteAudiocheckBox.UseVisualStyleBackColor = true;
            this.MuteAudiocheckBox.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.AllowDrop = true;
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(105, 141);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(353, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "wallpaperPath";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop);
            this.textBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox1_DragEnter);
            // 
            // CleanMemorycheckBox
            // 
            this.CleanMemorycheckBox.AutoSize = true;
            this.CleanMemorycheckBox.Checked = true;
            this.CleanMemorycheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CleanMemorycheckBox.Location = new System.Drawing.Point(12, 81);
            this.CleanMemorycheckBox.Name = "CleanMemorycheckBox";
            this.CleanMemorycheckBox.Size = new System.Drawing.Size(173, 17);
            this.CleanMemorycheckBox.TabIndex = 4;
            this.CleanMemorycheckBox.Text = "Clean memory (Recommanded)";
            this.CleanMemorycheckBox.UseVisualStyleBackColor = true;
            this.CleanMemorycheckBox.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.Location = new System.Drawing.Point(9, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Wallpaper Folder:";
            // 
            // StartMinimizedcheckBox
            // 
            this.StartMinimizedcheckBox.AutoSize = true;
            this.StartMinimizedcheckBox.Location = new System.Drawing.Point(12, 35);
            this.StartMinimizedcheckBox.Name = "StartMinimizedcheckBox";
            this.StartMinimizedcheckBox.Size = new System.Drawing.Size(96, 17);
            this.StartMinimizedcheckBox.TabIndex = 6;
            this.StartMinimizedcheckBox.Text = "Start minimized";
            this.StartMinimizedcheckBox.UseVisualStyleBackColor = true;
            this.StartMinimizedcheckBox.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
            // 
            // PlayAtStartupcheckBox
            // 
            this.PlayAtStartupcheckBox.AutoSize = true;
            this.PlayAtStartupcheckBox.Checked = true;
            this.PlayAtStartupcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PlayAtStartupcheckBox.Location = new System.Drawing.Point(314, 35);
            this.PlayAtStartupcheckBox.Name = "PlayAtStartupcheckBox";
            this.PlayAtStartupcheckBox.Size = new System.Drawing.Size(93, 17);
            this.PlayAtStartupcheckBox.TabIndex = 7;
            this.PlayAtStartupcheckBox.Text = "Play at startup";
            this.PlayAtStartupcheckBox.UseVisualStyleBackColor = true;
            this.PlayAtStartupcheckBox.CheckedChanged += new System.EventHandler(this.PlayAtStartupcheckBox_CheckedChanged);
            // 
            // Settings
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(470, 166);
            this.Controls.Add(this.PlayAtStartupcheckBox);
            this.Controls.Add(this.StartMinimizedcheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CleanMemorycheckBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.MuteAudiocheckBox);
            this.Controls.Add(this.CloseMinimizescheckBox);
            this.Controls.Add(this.ShowLogscheckBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.Load += new System.EventHandler(this.Settings_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox1_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.CheckBox ShowLogscheckBox;
        public System.Windows.Forms.CheckBox CloseMinimizescheckBox;
        public System.Windows.Forms.CheckBox MuteAudiocheckBox;
        public System.Windows.Forms.CheckBox CleanMemorycheckBox;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox StartMinimizedcheckBox;
        public System.Windows.Forms.CheckBox PlayAtStartupcheckBox;
    }
}