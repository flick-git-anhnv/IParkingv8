using iParkingv8.Object.Objects.Payments;
using IParkingv8.Cash.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestCash
{
    public partial class frmTestCash : Form
    {
        CBA9ControllersV2 cashController;
        public frmTestCash()
        {
            InitializeComponent();



            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            guna2ComboBox1.Items.Clear();
            guna2ComboBox1.Items.AddRange(ports);

            if (guna2ComboBox1.Items.Count > 0)
                guna2ComboBox1.SelectedIndex = 0;


        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            PaymentKioskConfig config = new PaymentKioskConfig()
            {
                CashComport = guna2ComboBox1.Text,
            };
            cashController = new CBA9ControllersV2(config);

            SetText(await cashController.Connect());
        }
        private void SetText(bool isConnect)
        {
            IsConnect = isConnect;
            label1.Text = isConnect ? "Connect!" : "Disconnect!";
            label1.ForeColor = isConnect ? Color.Green : Color.Red;
        }

        private async void btnPollingStart_Click(object sender, EventArgs e)
        {
            cashController.PollingStart();
        }
        private void btnPollingStop_Click(object sender, EventArgs e)
        {
            cashController.PollingStop();
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            if (cashController.EnableValidator22())
            {
                SetText(true);
                label2.Text = "Ra lệnh Enable thành công - " + DateTime.Now;
            }
            else
            {
                label2.Text = "Ra lệnh Enable thất bại - " + DateTime.Now;

            }
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            if (cashController.DisableValidator())
            {
                SetText(false);
                label2.Text = "Ra lệnh Disable thành công - " + DateTime.Now;

            }
            else
            {
                label2.Text = "Ra lệnh Disable thất bai - " + DateTime.Now;
            }

        }

        private void btnStartTimer_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void btnStopTimer_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
        bool IsConnect = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (IsConnect)
            {
                btnDisable_Click(null, null);
            }
            else
            {
                btnEnable_Click(null, null);
            }
        }

        
    }
}
