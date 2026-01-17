using Guna.UI2.WinForms;
using System.ComponentModel;

namespace Kztek.Control8.UserControls.ReportUcs
{
    public partial class ucPage : UserControl
    {
        public event EventHandler? ClickPage;
        public Color PageColor { get => btnPage.ForeColor; set { btnPage.ForeColor = value; } }

        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Page Index"), Description("Cài đặt index của page")]
        public int KZUI_PageIndex
        {
            get => int.Parse(btnPage.Text);
            set
            {
                btnPage.Text = value.ToString();
            }
        }

        private bool isActive = true;
        public bool IsActivePage
        {
            get => isActive;
            set
            {
                isActive = value;
                btnPage.BorderColor = value ? Color.Green : Color.Black;
                PageColor = value ? Color.Green : Color.Black;
            }
        }

        public ucPage()
        {
            InitializeComponent();
            this.Click += UcNextPage_Click;
            btnPage.Click += Guna2Button1_Click;
        }

        private void Guna2Button1_Click(object? sender, EventArgs e)
        {
            ClickPage?.Invoke(this, EventArgs.Empty);
        }

        private void UcNextPage_Click(object? sender, EventArgs e)
        {
            ClickPage?.Invoke(this, EventArgs.Empty);
        }
    }
}
