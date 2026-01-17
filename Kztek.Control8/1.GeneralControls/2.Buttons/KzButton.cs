using Guna.UI2.WinForms;
using iParkingv8.Ultility;

namespace Kztek.Control8.Controls
{
    public class KzButton : Guna2Button
    {
        public Func<object?, Task<bool>>? OnClickAsync;

        public KzButton() : base()
        {
            this.BorderColor = ButtonColorManagement.BUTTON_NORMAL_BORDER_COLOR;
            this.ForeColor = ButtonColorManagement.BUTTON_NORMAL_FORE_COLOR;
            this.BackColor = ButtonColorManagement.BUTTON_NORMAL_BACK_COLOR;
            this.FillColor = ButtonColorManagement.BUTTON_NORMAL_FILL_COLOR;
            this.BorderThickness = 1;
            this.BorderRadius = 8;
            this.Font = new Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);

            this.GotFocus += KzButton_GotFocus;
            this.LostFocus += KzButton_LostFocus;
            this.Click += KzButton_Click;
        }

        private void KzButton_LostFocus(object? sender, EventArgs e)
        {
            this.BorderColor = ButtonColorManagement.BUTTON_NORMAL_BORDER_COLOR;
            this.ForeColor = ButtonColorManagement.BUTTON_NORMAL_FORE_COLOR;
            this.BackColor = ButtonColorManagement.BUTTON_NORMAL_BACK_COLOR;
            this.FillColor = ButtonColorManagement.BUTTON_NORMAL_FILL_COLOR;
        }
        private void KzButton_GotFocus(object? sender, EventArgs e)
        {
            this.BorderColor = ButtonColorManagement.BUTTON_FOCUS_BORDER_COLOR;
            this.ForeColor = ButtonColorManagement.BUTTON_FOCUS_FORE_COLOR;
            this.BackColor = ButtonColorManagement.BUTTON_FOCUS_BACK_COLOR;
            this.FillColor = ButtonColorManagement.BUTTON_FOCUS_FILL_COLOR;
        }
        private async void KzButton_Click(object? sender, EventArgs e)
        {
            if (this.Enabled == false) return;
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            if (OnClickAsync != null)
            {
                await OnClickAsync(sender);
            }

            this.Enabled = true;
            this.Focus();
            this.Cursor = Cursors.Default;
        }
    }
}
