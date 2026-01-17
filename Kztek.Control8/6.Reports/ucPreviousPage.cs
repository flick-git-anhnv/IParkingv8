using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kztek.Control8.UserControls.ReportUcs
{
    public partial class ucPreviousPage : UserControl
    {
        public event EventHandler? ClickPage;
        public ucPreviousPage()
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
