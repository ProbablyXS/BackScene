using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class RedBorderManager : IDisposable
{
    private RedBorderForm _redBorderForm;

    public void Show(Screen screen)
    {
        if (_redBorderForm != null)
        {
            _redBorderForm.Close();
            _redBorderForm.Dispose();
            _redBorderForm = null;
        }

        _redBorderForm = new RedBorderForm(screen.Bounds);
        _redBorderForm.Show();
    }

    public void Hide()
    {
        if (_redBorderForm != null)
        {
            _redBorderForm.Close();
            _redBorderForm.Dispose();
            _redBorderForm = null;
        }
    }

    public void Dispose()
    {
        Hide();
    }

    private class RedBorderForm : Form
    {
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int WS_EX_LAYERED = 0x80000;
        private const int WS_EX_NOACTIVATE = 0x8000000;
        private const int GWL_EXSTYLE = -20;

        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        public RedBorderForm(Rectangle bounds)
        {
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Location = bounds.Location;
            Size = bounds.Size;
            TopMost = true;

            // Définir le style de fenêtre (transparente, pas de focus)
            int exStyle = GetWindowLong(this.Handle, GWL_EXSTYLE);
            exStyle |= WS_EX_LAYERED | WS_EX_TRANSPARENT | WS_EX_NOACTIVATE;
            SetWindowLong(this.Handle, GWL_EXSTYLE, exStyle);

            // Couleur de transparence
            this.BackColor = Color.Lime;
            this.TransparencyKey = Color.Lime;

            this.Paint += RedBorderForm_Paint;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= WS_EX_NOACTIVATE | WS_EX_LAYERED | WS_EX_TRANSPARENT;
                return cp;
            }
        }

        private void RedBorderForm_Paint(object sender, PaintEventArgs e)
        {
            int borderThickness = 5;
            using (Pen pen = new Pen(Color.Red, borderThickness))
            {
                e.Graphics.DrawRectangle(pen, new Rectangle(borderThickness / 2, borderThickness / 2, Width - borderThickness, Height - borderThickness));
            }
        }
    }
}
