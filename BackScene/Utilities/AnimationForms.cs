using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

public static class AnimationForms
{
    public static async Task MinimizeForm(Form frm, bool minimize)
    {
        try
        {
            await Task.Delay(100);

            while (frm.Opacity >= 0)
            {
                await Task.Delay(8);

                if (frm.Opacity <= 1)
                {
                    frm.Opacity -= 0.1;

                    if (frm.Opacity == 0)
                    {
                        await Task.Delay(100);

                        if (minimize)
                        {
                            frm.WindowState = FormWindowState.Minimized;
                        }
                        else
                        {
                            frm.Hide();
                        }
                        return;
                    }
                }
            }
        }
        catch (Exception)
        {

        }
    }

    public static async Task OpenForm(Form frm)
    {
        try
        {
            await Task.Delay(100);

            while (frm.Opacity <= 1)
            {
                await Task.Delay(8);

                if (frm.Opacity <= 1)
                {
                    frm.Opacity += 0.1;

                    if (frm.Opacity == 1)
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }
        catch (Exception)
        {

        }
    }

    public static async Task CloseForm(Form frm)
    {
        try
        {
            await Task.Delay(100);

            while (frm.Opacity >= 0)
            {
                await Task.Delay(8);

                if (frm.Opacity <= 1)
                {
                    frm.Opacity -= 0.1;

                    if (frm.Opacity == 0)
                    {
                        await Task.Delay(100);

                        Environment.Exit(0);
                    }
                }
            }
        }
        catch (Exception)
        {

        }
    }
}