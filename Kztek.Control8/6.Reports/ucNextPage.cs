namespace Kztek.Control8.UserControls.ReportUcs
{
    public partial class ucNextPage : UserControl
    {
        public event EventHandler? ClickPage;
        public ucNextPage()
        {
            InitializeComponent();
            this.Click += UcNextPage_Click;
            guna2Button1.Click += Guna2Button1_Click;
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
