
using System.Windows.Forms;

namespace BackScene
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackScene.Main));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.nextToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.previousToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.playToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.muteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.unmuteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
            base.SuspendLayout();
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip2;
            this.notifyIcon1.Icon = (System.Drawing.Icon)resources.GetObject("notifyIcon1.Icon");
            this.notifyIcon1.Text = "BackScene";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(notifyIcon1_DoubleClick);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[12]
            {
                this.startToolStripMenuItem1, this.stopToolStripMenuItem1, this.nextToolStripMenuItem1, this.previousToolStripMenuItem1, this.playToolStripMenuItem1, this.pauseToolStripMenuItem1, this.muteToolStripMenuItem1, this.unmuteToolStripMenuItem, this.showToolStripMenuItem1, this.settingsToolStripMenuItem1,
                this.helpToolStripMenuItem1, this.exitToolStripMenuItem1
            });
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip2.Size = new System.Drawing.Size(181, 290);
            this.startToolStripMenuItem1.Name = "startToolStripMenuItem1";
            this.startToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.startToolStripMenuItem1.Text = "Start";
            this.startToolStripMenuItem1.Click += new System.EventHandler(startToolStripMenuItem_Click);
            this.stopToolStripMenuItem1.Name = "stopToolStripMenuItem1";
            this.stopToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.stopToolStripMenuItem1.Text = "Stop";
            this.stopToolStripMenuItem1.Click += new System.EventHandler(stopToolStripMenuItem1_Click);
            this.nextToolStripMenuItem1.Name = "nextToolStripMenuItem1";
            this.nextToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.nextToolStripMenuItem1.Text = "Next";
            this.nextToolStripMenuItem1.Click += new System.EventHandler(nextToolStripMenuItem1_Click);
            this.previousToolStripMenuItem1.Name = "previousToolStripMenuItem1";
            this.previousToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.previousToolStripMenuItem1.Text = "Previous";
            this.previousToolStripMenuItem1.Click += new System.EventHandler(previousToolStripMenuItem1_Click);
            this.playToolStripMenuItem1.Name = "playToolStripMenuItem1";
            this.playToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.playToolStripMenuItem1.Text = "Play";
            this.playToolStripMenuItem1.Click += new System.EventHandler(playToolStripMenuItem1_Click);
            this.pauseToolStripMenuItem1.Name = "pauseToolStripMenuItem1";
            this.pauseToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.pauseToolStripMenuItem1.Text = "Pause";
            this.pauseToolStripMenuItem1.Click += new System.EventHandler(pauseToolStripMenuItem1_Click);
            this.muteToolStripMenuItem1.Name = "muteToolStripMenuItem1";
            this.muteToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.muteToolStripMenuItem1.Text = "Mute";
            this.muteToolStripMenuItem1.Click += new System.EventHandler(muteToolStripMenuItem1_Click);
            this.unmuteToolStripMenuItem.Name = "unmuteToolStripMenuItem";
            this.unmuteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.unmuteToolStripMenuItem.Text = "Unmute";
            this.unmuteToolStripMenuItem.Click += new System.EventHandler(unmuteToolStripMenuItem_Click);
            this.showToolStripMenuItem1.Name = "showToolStripMenuItem1";
            this.showToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.showToolStripMenuItem1.Text = "Show";
            this.showToolStripMenuItem1.Click += new System.EventHandler(showToolStripMenuItem1_Click);
            this.settingsToolStripMenuItem1.Name = "settingsToolStripMenuItem1";
            this.settingsToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.settingsToolStripMenuItem1.Text = "Settings";
            this.settingsToolStripMenuItem1.Click += new System.EventHandler(settingsToolStripMenuItem1_Click);
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.helpToolStripMenuItem1.Text = "Help";
            this.helpToolStripMenuItem1.Click += new System.EventHandler(helpToolStripMenuItem1_Click);
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(exitToolStripMenuItem1_Click);
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderSize = 3;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Consolas", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.button1.ForeColor = System.Drawing.Color.IndianRed;
            this.button1.Location = new System.Drawing.Point(10, 149);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 29);
            this.button1.TabIndex = 1;
            this.button1.TabStop = false;
            this.button1.Text = "Settings";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(button1_Click);
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.BorderSize = 3;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Consolas", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.button2.ForeColor = System.Drawing.Color.IndianRed;
            this.button2.Location = new System.Drawing.Point(153, 149);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 29);
            this.button2.TabIndex = 2;
            this.button2.TabStop = false;
            this.button2.Text = "Start";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(button2_Click);
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button3.FlatAppearance.BorderSize = 3;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Consolas", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.button3.ForeColor = System.Drawing.Color.IndianRed;
            this.button3.Location = new System.Drawing.Point(245, 149);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(86, 29);
            this.button3.TabIndex = 3;
            this.button3.TabStop = false;
            this.button3.Text = "Stop";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(button3_Click);
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Consolas", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.label1.ForeColor = System.Drawing.Color.IndianRed;
            this.label1.Location = new System.Drawing.Point(63, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 44);
            this.label1.TabIndex = 5;
            this.label1.Text = "Back\r\nScene";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(Main_MouseDown);
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Consolas", 9.75f, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.IndianRed;
            this.label4.Location = new System.Drawing.Point(11, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(318, 89);
            this.label4.TabIndex = 8;
            this.label4.Text = "Info";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Visible = false;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(341, 188);
            this.panel1.TabIndex = 9;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(Main_MouseDown);
            this.button5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.button5.BackColor = System.Drawing.Color.White;
            this.button5.BackgroundImage = BackScene.Properties.Resources.hide_normal;
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button5.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Consolas", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.button5.ForeColor = System.Drawing.Color.IndianRed;
            this.button5.Location = new System.Drawing.Point(292, 9);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(16, 18);
            this.button5.TabIndex = 10;
            this.button5.TabStop = false;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(button5_Click);
            this.button5.MouseLeave += new System.EventHandler(button5_MouseLeave);
            this.button5.MouseHover += new System.EventHandler(button5_MouseHover);
            this.button4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.BackgroundImage = BackScene.Properties.Resources.close_normal;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Consolas", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.button4.ForeColor = System.Drawing.Color.IndianRed;
            this.button4.Location = new System.Drawing.Point(316, 11);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(13, 13);
            this.button4.TabIndex = 9;
            this.button4.TabStop = false;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(button4_Click);
            this.button4.MouseLeave += new System.EventHandler(button5_MouseLeave);
            this.button4.MouseHover += new System.EventHandler(button5_MouseHover);
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = BackScene.Properties.Resources.title;
            this.pictureBox1.Location = new System.Drawing.Point(14, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(47, 44);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(Main_MouseDown);
            this.AllowDrop = true;
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.IndianRed;
            base.ClientSize = new System.Drawing.Size(345, 192);
            base.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.Name = "Main";
            base.Opacity = 0.0;
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BackScene";
            base.Activated += new System.EventHandler(Main_Activated);
            base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Main_FormClosing);
            base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(Main_FormClosed);
            base.Load += new System.EventHandler(Main_Load);
            base.DragDrop += new System.Windows.Forms.DragEventHandler(Main_DragDrop);
            base.DragEnter += new System.Windows.Forms.DragEventHandler(Main_DragEnter);
            base.MouseDown += new System.Windows.Forms.MouseEventHandler(Main_MouseDown);
            this.contextMenuStrip2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
            base.ResumeLayout(false);
        }

        #endregion

        private NotifyIcon notifyIcon1;

        private ToolTip toolTip1;

        private Button button1;

        private Button button2;

        private Button button3;

        private PictureBox pictureBox1;

        private Label label1;

        public Label label4;

        private Button button4;

        private Button button5;

        internal Panel panel1;

        private ContextMenuStrip contextMenuStrip2;

        private ToolStripMenuItem startToolStripMenuItem1;

        private ToolStripMenuItem stopToolStripMenuItem1;

        private ToolStripMenuItem nextToolStripMenuItem1;

        private ToolStripMenuItem previousToolStripMenuItem1;

        private ToolStripMenuItem playToolStripMenuItem1;

        private ToolStripMenuItem pauseToolStripMenuItem1;

        private ToolStripMenuItem muteToolStripMenuItem1;

        private ToolStripMenuItem showToolStripMenuItem1;

        private ToolStripMenuItem settingsToolStripMenuItem1;

        private ToolStripMenuItem helpToolStripMenuItem1;

        private ToolStripMenuItem exitToolStripMenuItem1;

        private ToolStripMenuItem unmuteToolStripMenuItem;
    }
}

