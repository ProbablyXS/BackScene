
using System.Windows.Forms;

namespace BackScene
{
    partial class Logs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackScene.Logs));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            base.SuspendLayout();
            this.richTextBox1.BackColor = System.Drawing.Color.Black;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Consolas", 14.25f);
            this.richTextBox1.ForeColor = System.Drawing.Color.Black;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox1.Size = new System.Drawing.Size(707, 350);
            this.richTextBox1.TabIndex = 8;
            this.richTextBox1.TabStop = false;
            this.richTextBox1.Text = "";
            this.richTextBox1.Enter += new System.EventHandler(richTextBox1_Enter);
            this.richTextBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(richTextBox1_MouseDown);
            this.richTextBox1.MouseWheel += new System.Windows.Forms.MouseEventHandler(RichTextBox1_MouseWheel);
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(707, 350);
            base.Controls.Add(this.richTextBox1);
            this.DoubleBuffered = true;
            base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            base.Name = "Logs";
            base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "BackScene: Console Logs";
            base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Logs_FormClosing);
            base.ResumeLayout(false);
        }

        #endregion

        public RichTextBox richTextBox1;
    }
}