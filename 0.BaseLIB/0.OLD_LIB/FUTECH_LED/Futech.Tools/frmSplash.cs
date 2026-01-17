using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Futech.Tools
{
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            InitializeComponent();
        }

        private string status = "Please wait...";
        public string Status
        {
            set 
            { 
                status = value;
                DisplayStatus();
            }
        }

        private void DisplayStatus()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(this.DisplayStatus));
                return;
            }
            labelStatus.Text = status;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}