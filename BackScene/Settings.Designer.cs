
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
            this.components = new System.ComponentModel.Container();
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ShowLogscheckBox
            // 
            this.ShowLogscheckBox.AutoSize = true;
            this.ShowLogscheckBox.BackColor = System.Drawing.Color.White;
            this.ShowLogscheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShowLogscheckBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.ShowLogscheckBox.ForeColor = System.Drawing.Color.IndianRed;
            this.ShowLogscheckBox.Location = new System.Drawing.Point(32, 54);
            this.ShowLogscheckBox.Name = "ShowLogscheckBox";
            this.ShowLogscheckBox.Size = new System.Drawing.Size(86, 19);
            this.ShowLogscheckBox.TabIndex = 0;
            this.ShowLogscheckBox.Text = "Show logs";
            this.ShowLogscheckBox.UseVisualStyleBackColor = false;
            this.ShowLogscheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // CloseMinimizescheckBox
            // 
            this.CloseMinimizescheckBox.AutoSize = true;
            this.CloseMinimizescheckBox.BackColor = System.Drawing.Color.White;
            this.CloseMinimizescheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseMinimizescheckBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.CloseMinimizescheckBox.ForeColor = System.Drawing.Color.IndianRed;
            this.CloseMinimizescheckBox.Location = new System.Drawing.Point(32, 94);
            this.CloseMinimizescheckBox.Name = "CloseMinimizescheckBox";
            this.CloseMinimizescheckBox.Size = new System.Drawing.Size(128, 19);
            this.CloseMinimizescheckBox.TabIndex = 1;
            this.CloseMinimizescheckBox.Text = "Close minimizes";
            this.CloseMinimizescheckBox.UseVisualStyleBackColor = false;
            this.CloseMinimizescheckBox.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // MuteAudiocheckBox
            // 
            this.MuteAudiocheckBox.AutoSize = true;
            this.MuteAudiocheckBox.BackColor = System.Drawing.Color.White;
            this.MuteAudiocheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MuteAudiocheckBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.MuteAudiocheckBox.ForeColor = System.Drawing.Color.IndianRed;
            this.MuteAudiocheckBox.Location = new System.Drawing.Point(190, 55);
            this.MuteAudiocheckBox.Name = "MuteAudiocheckBox";
            this.MuteAudiocheckBox.Size = new System.Drawing.Size(93, 19);
            this.MuteAudiocheckBox.TabIndex = 2;
            this.MuteAudiocheckBox.Text = "Mute audio";
            this.MuteAudiocheckBox.UseVisualStyleBackColor = false;
            this.MuteAudiocheckBox.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.AllowDrop = true;
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.textBox1.ForeColor = System.Drawing.Color.IndianRed;
            this.textBox1.Location = new System.Drawing.Point(135, 164);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(196, 16);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "wallpaperPath";
            this.textBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop);
            this.textBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox1_DragEnter);
            // 
            // CleanMemorycheckBox
            // 
            this.CleanMemorycheckBox.AutoSize = true;
            this.CleanMemorycheckBox.BackColor = System.Drawing.Color.White;
            this.CleanMemorycheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CleanMemorycheckBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.CleanMemorycheckBox.ForeColor = System.Drawing.Color.IndianRed;
            this.CleanMemorycheckBox.Location = new System.Drawing.Point(32, 114);
            this.CleanMemorycheckBox.Name = "CleanMemorycheckBox";
            this.CleanMemorycheckBox.Size = new System.Drawing.Size(107, 19);
            this.CleanMemorycheckBox.TabIndex = 4;
            this.CleanMemorycheckBox.Text = "Clean memory";
            this.CleanMemorycheckBox.UseVisualStyleBackColor = false;
            this.CleanMemorycheckBox.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
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
            this.StartMinimizedcheckBox.BackColor = System.Drawing.Color.White;
            this.StartMinimizedcheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartMinimizedcheckBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.StartMinimizedcheckBox.ForeColor = System.Drawing.Color.IndianRed;
            this.StartMinimizedcheckBox.Location = new System.Drawing.Point(32, 74);
            this.StartMinimizedcheckBox.Name = "StartMinimizedcheckBox";
            this.StartMinimizedcheckBox.Size = new System.Drawing.Size(128, 19);
            this.StartMinimizedcheckBox.TabIndex = 6;
            this.StartMinimizedcheckBox.Text = "Start minimized";
            this.StartMinimizedcheckBox.UseVisualStyleBackColor = false;
            this.StartMinimizedcheckBox.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
            // 
            // PlayAtStartupcheckBox
            // 
            this.PlayAtStartupcheckBox.AutoSize = true;
            this.PlayAtStartupcheckBox.BackColor = System.Drawing.Color.White;
            this.PlayAtStartupcheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PlayAtStartupcheckBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.PlayAtStartupcheckBox.ForeColor = System.Drawing.Color.IndianRed;
            this.PlayAtStartupcheckBox.Location = new System.Drawing.Point(190, 95);
            this.PlayAtStartupcheckBox.Name = "PlayAtStartupcheckBox";
            this.PlayAtStartupcheckBox.Size = new System.Drawing.Size(128, 19);
            this.PlayAtStartupcheckBox.TabIndex = 7;
            this.PlayAtStartupcheckBox.Text = "Play at startup";
            this.PlayAtStartupcheckBox.UseVisualStyleBackColor = false;
            this.PlayAtStartupcheckBox.CheckedChanged += new System.EventHandler(this.PlayAtStartupcheckBox_CheckedChanged);
            // 
            // StartWithWindowscheckBox
            // 
            this.StartWithWindowscheckBox.AutoSize = true;
            this.StartWithWindowscheckBox.BackColor = System.Drawing.Color.White;
            this.StartWithWindowscheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartWithWindowscheckBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.StartWithWindowscheckBox.ForeColor = System.Drawing.Color.IndianRed;
            this.StartWithWindowscheckBox.Location = new System.Drawing.Point(32, 134);
            this.StartWithWindowscheckBox.Name = "StartWithWindowscheckBox";
            this.StartWithWindowscheckBox.Size = new System.Drawing.Size(149, 19);
            this.StartWithWindowscheckBox.TabIndex = 8;
            this.StartWithWindowscheckBox.Text = "Start with windows";
            this.StartWithWindowscheckBox.UseVisualStyleBackColor = false;
            this.StartWithWindowscheckBox.CheckedChanged += new System.EventHandler(this.StartWithWindowscheckBox_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.checkBox2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.StartWithWindowscheckBox);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.PlayAtStartupcheckBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.StartMinimizedcheckBox);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.CleanMemorycheckBox);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.MuteAudiocheckBox);
            this.panel1.Controls.Add(this.ShowLogscheckBox);
            this.panel1.Controls.Add(this.CloseMinimizescheckBox);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(341, 188);
            this.panel1.TabIndex = 10;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Main_MouseDown);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.BackColor = System.Drawing.Color.White;
            this.checkBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox2.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.checkBox2.ForeColor = System.Drawing.Color.IndianRed;
            this.checkBox2.Location = new System.Drawing.Point(190, 114);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(86, 19);
            this.checkBox2.TabIndex = 13;
            this.checkBox2.Text = "Limit FPS";
            this.checkBox2.UseVisualStyleBackColor = false;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged_1);
            this.checkBox2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.checkBox2_MouseClick);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.IndianRed;
            this.label5.Location = new System.Drawing.Point(29, 35);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.TabIndex = 12;
            this.label5.Text = "BackScene";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.IndianRed;
            this.label4.Location = new System.Drawing.Point(187, 35);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(32, 18);
            this.label4.TabIndex = 11;
            this.label4.Text = "MPV";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.White;
            this.checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.checkBox1.ForeColor = System.Drawing.Color.IndianRed;
            this.checkBox1.Location = new System.Drawing.Point(190, 75);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 19);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "Shuffle";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.IndianRed;
            this.label3.Location = new System.Drawing.Point(35, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 19);
            this.label3.TabIndex = 11;
            this.label3.Text = "SETINGS";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = global::BackScene.Properties.Resources.title;
            this.pictureBox1.Location = new System.Drawing.Point(8, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(27, 19);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.BackColor = System.Drawing.Color.White;
            this.contextMenuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.contextMenuStrip1.DropShadowEnabled = false;
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(62, 92);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(61, 22);
            this.toolStripMenuItem2.Text = "15";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(61, 22);
            this.toolStripMenuItem3.Text = "30";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(61, 22);
            this.toolStripMenuItem4.Text = "45";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(61, 22);
            this.toolStripMenuItem5.Text = "60";
            // 
            // Settings
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(345, 192);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Settings";
            this.Opacity = 0D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Settings";
            this.Activated += new System.EventHandler(this.Settings_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.Load += new System.EventHandler(this.Settings_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox1_DragEnter);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Main_MouseDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
    }
}