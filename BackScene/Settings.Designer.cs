
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
            this.StartWithWindowscheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ShowLogscheckBox
            // 
            this.ShowLogscheckBox.AutoSize = true;
            this.ShowLogscheckBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.ShowLogscheckBox.ForeColor = System.Drawing.Color.IndianRed;
            this.ShowLogscheckBox.Location = new System.Drawing.Point(8, 40);
            this.ShowLogscheckBox.Name = "ShowLogscheckBox";
            this.ShowLogscheckBox.Size = new System.Drawing.Size(89, 19);
            this.ShowLogscheckBox.TabIndex = 0;
            this.ShowLogscheckBox.Text = "Show logs";
            this.ShowLogscheckBox.UseVisualStyleBackColor = true;
            this.ShowLogscheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // CloseMinimizescheckBox
            // 
            this.CloseMinimizescheckBox.AutoSize = true;
            this.CloseMinimizescheckBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.CloseMinimizescheckBox.ForeColor = System.Drawing.Color.IndianRed;
            this.CloseMinimizescheckBox.Location = new System.Drawing.Point(8, 85);
            this.CloseMinimizescheckBox.Name = "CloseMinimizescheckBox";
            this.CloseMinimizescheckBox.Size = new System.Drawing.Size(131, 19);
            this.CloseMinimizescheckBox.TabIndex = 1;
            this.CloseMinimizescheckBox.Text = "Close minimizes";
            this.CloseMinimizescheckBox.UseVisualStyleBackColor = true;
            this.CloseMinimizescheckBox.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // MuteAudiocheckBox
            // 
            this.MuteAudiocheckBox.AutoSize = true;
            this.MuteAudiocheckBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.MuteAudiocheckBox.ForeColor = System.Drawing.Color.IndianRed;
            this.MuteAudiocheckBox.Location = new System.Drawing.Point(181, 40);
            this.MuteAudiocheckBox.Name = "MuteAudiocheckBox";
            this.MuteAudiocheckBox.Size = new System.Drawing.Size(96, 19);
            this.MuteAudiocheckBox.TabIndex = 2;
            this.MuteAudiocheckBox.Text = "Mute audio";
            this.MuteAudiocheckBox.UseVisualStyleBackColor = true;
            this.MuteAudiocheckBox.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.AllowDrop = true;
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.ForeColor = System.Drawing.Color.IndianRed;
            this.textBox1.Location = new System.Drawing.Point(141, 165);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(196, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "wallpaperPath";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop);
            this.textBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox1_DragEnter);
            // 
            // CleanMemorycheckBox
            // 
            this.CleanMemorycheckBox.AutoSize = true;
            this.CleanMemorycheckBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.CleanMemorycheckBox.ForeColor = System.Drawing.Color.IndianRed;
            this.CleanMemorycheckBox.Location = new System.Drawing.Point(8, 108);
            this.CleanMemorycheckBox.Name = "CleanMemorycheckBox";
            this.CleanMemorycheckBox.Size = new System.Drawing.Size(110, 19);
            this.CleanMemorycheckBox.TabIndex = 4;
            this.CleanMemorycheckBox.Text = "Clean memory";
            this.CleanMemorycheckBox.UseVisualStyleBackColor = true;
            this.CleanMemorycheckBox.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.IndianRed;
            this.label1.Location = new System.Drawing.Point(9, 166);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Wallpaper Folder:";
            // 
            // StartMinimizedcheckBox
            // 
            this.StartMinimizedcheckBox.AutoSize = true;
            this.StartMinimizedcheckBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.StartMinimizedcheckBox.ForeColor = System.Drawing.Color.IndianRed;
            this.StartMinimizedcheckBox.Location = new System.Drawing.Point(8, 63);
            this.StartMinimizedcheckBox.Name = "StartMinimizedcheckBox";
            this.StartMinimizedcheckBox.Size = new System.Drawing.Size(131, 19);
            this.StartMinimizedcheckBox.TabIndex = 6;
            this.StartMinimizedcheckBox.Text = "Start minimized";
            this.StartMinimizedcheckBox.UseVisualStyleBackColor = true;
            this.StartMinimizedcheckBox.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
            // 
            // PlayAtStartupcheckBox
            // 
            this.PlayAtStartupcheckBox.AutoSize = true;
            this.PlayAtStartupcheckBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.PlayAtStartupcheckBox.ForeColor = System.Drawing.Color.IndianRed;
            this.PlayAtStartupcheckBox.Location = new System.Drawing.Point(181, 63);
            this.PlayAtStartupcheckBox.Name = "PlayAtStartupcheckBox";
            this.PlayAtStartupcheckBox.Size = new System.Drawing.Size(131, 19);
            this.PlayAtStartupcheckBox.TabIndex = 7;
            this.PlayAtStartupcheckBox.Text = "Play at startup";
            this.PlayAtStartupcheckBox.UseVisualStyleBackColor = true;
            this.PlayAtStartupcheckBox.CheckedChanged += new System.EventHandler(this.PlayAtStartupcheckBox_CheckedChanged);
            // 
            // StartWithWindowscheckBox
            // 
            this.StartWithWindowscheckBox.AutoSize = true;
            this.StartWithWindowscheckBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.StartWithWindowscheckBox.ForeColor = System.Drawing.Color.IndianRed;
            this.StartWithWindowscheckBox.Location = new System.Drawing.Point(181, 86);
            this.StartWithWindowscheckBox.Name = "StartWithWindowscheckBox";
            this.StartWithWindowscheckBox.Size = new System.Drawing.Size(152, 19);
            this.StartWithWindowscheckBox.TabIndex = 8;
            this.StartWithWindowscheckBox.Text = "Start with windows";
            this.StartWithWindowscheckBox.UseVisualStyleBackColor = true;
            this.StartWithWindowscheckBox.CheckedChanged += new System.EventHandler(this.StartWithWindowscheckBox_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.IndianRed;
            this.label2.Location = new System.Drawing.Point(315, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "C";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // Settings
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(345, 192);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.StartWithWindowscheckBox);
            this.Controls.Add(this.PlayAtStartupcheckBox);
            this.Controls.Add(this.StartMinimizedcheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CleanMemorycheckBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.MuteAudiocheckBox);
            this.Controls.Add(this.CloseMinimizescheckBox);
            this.Controls.Add(this.ShowLogscheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.Load += new System.EventHandler(this.Settings_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox1_DragEnter);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Main_MouseDown);
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
        public System.Windows.Forms.CheckBox StartWithWindowscheckBox;
        private System.Windows.Forms.Label label2;
    }
}