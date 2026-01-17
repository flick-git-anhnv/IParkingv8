using Kztek.Tool;

namespace ControllerTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            txtIP1.Text = Properties.Settings.Default.IP1;
            txtIP2.Text = Properties.Settings.Default.IP2;
            txtIP3.Text = Properties.Settings.Default.IP3;
            txtIP4.Text = Properties.Settings.Default.IP4;

            txtIP1.TextChanged += TxtIP1_TextChanged;
            txtIP2.TextChanged += TxtIP2_TextChanged;
            txtIP3.TextChanged += TxtIP3_TextChanged;
            txtIP4.TextChanged += TxtIP4_TextChanged;
        }

        private void TxtIP4_TextChanged(object? sender, EventArgs e)
        {
            Properties.Settings.Default.IP4 = txtIP4.Text;
        }

        private void TxtIP3_TextChanged(object? sender, EventArgs e)
        {
            Properties.Settings.Default.IP3 = txtIP4.Text;
        }

        private void TxtIP2_TextChanged(object? sender, EventArgs e)
        {
            Properties.Settings.Default.IP2 = txtIP4.Text;
        }

        private void TxtIP1_TextChanged(object? sender, EventArgs e)
        {
            Properties.Settings.Default.IP1 = txtIP4.Text;
        }

        private async void btnIP1_barrie1_Click(object sender, EventArgs e)
        {
            label1.Text = "Kết quả: -";
            string response = await UdpTools.ReceiveSocketResponseAsync(txtIP1.Text, 100, openDoorCMD(1), true);
            bool isSuccess = response.Contains("OK");
            label1.Text = isSuccess ? $"Mở {txtIP1.Text} barrie 1 thành công" : $"Mở {txtIP1.Text} barrie 1 lỗi";
        }
        private async void btnIP1_barrie2_Click(object sender, EventArgs e)
        {
            label1.Text = "Kết quả: -";
            string response = await UdpTools.ReceiveSocketResponseAsync(txtIP1.Text, 100, openDoorCMD(2), true);
            bool isSuccess = response.Contains("OK");
            label1.Text = isSuccess ? $"Mở {txtIP1.Text} barrie 2 thành công" : $"Mở {txtIP1.Text} barrie 2 lỗi";
        }

        private async void btnIP2_barrie1_Click(object sender, EventArgs e)
        {
            label1.Text = "Kết quả: -";
            string response = await UdpTools.ReceiveSocketResponseAsync(txtIP2.Text, 100, openDoorCMD(1), true);
            bool isSuccess = response.Contains("OK");
            label1.Text = isSuccess ? $"Mở {txtIP2.Text} barrie 1 thành công" : $"Mở {txtIP2.Text} barrie 1 lỗi";
        }
        private async void btnIP2_barrie2_Click(object sender, EventArgs e)
        {
            label1.Text = "Kết quả: -";
            string response = await UdpTools.ReceiveSocketResponseAsync(txtIP2.Text, 100, openDoorCMD(2), true);
            bool isSuccess = response.Contains("OK");
            label1.Text = isSuccess ? $"Mở {txtIP2.Text} barrie 2 thành công" : $"Mở {txtIP2.Text} barrie 2 lỗi";
        }

        private async void btnIP3_barrie1_Click(object sender, EventArgs e)
        {
            label1.Text = "Kết quả: -";
            string response = await UdpTools.ReceiveSocketResponseAsync(txtIP3.Text, 100, openDoorCMD(1), true);
            bool isSuccess = response.Contains("OK");
            label1.Text = isSuccess ? $"Mở {txtIP3.Text} barrie 1 thành công" : $"Mở {txtIP3.Text} barrie 1 lỗi";
        }
        private async void btnIP3_barrie2_Click(object sender, EventArgs e)
        {
            label1.Text = "Kết quả: -";
            string response = await UdpTools.ReceiveSocketResponseAsync(txtIP3.Text, 100, openDoorCMD(2), true);
            bool isSuccess = response.Contains("OK");
            label1.Text = isSuccess ? $"Mở {txtIP3.Text} barrie 2 thành công" : $"Mở {txtIP3.Text} barrie 2 lỗi";
        }

        private async void btnIP4_barrie1_Click(object sender, EventArgs e)
        {
            label1.Text = "Kết quả: -";
            string response = await UdpTools.ReceiveSocketResponseAsync(txtIP4.Text, 100, openDoorCMD(1), true);
            bool isSuccess = response.Contains("OK");
            label1.Text = isSuccess ? $"Mở {txtIP4.Text} barrie 1 thành công" : $"Mở {txtIP4.Text} barrie 1 lỗi";
        }
        private async void btnIP4_barrie2_Click(object sender, EventArgs e)
        {
            label1.Text = "Kết quả: -";
            string response = await UdpTools.ReceiveSocketResponseAsync(txtIP4.Text, 100, openDoorCMD(1), true);
            bool isSuccess = response.Contains("OK");
            label1.Text = isSuccess ? $"Mở {txtIP4.Text} barrie 2 thành công" : $"Mở {txtIP4.Text} barrie 2 lỗi";
        }

        string openDoorCMD(int relayIndex) => $"SetRelay?/Relay={relayIndex:00}/State=ON";

        private async void btnSend_Click(object sender, EventArgs e)
        {
            //siticoneOverlay1.Show = true;
            //this.siticoneOverlay1.ShowOverlay();
            label1.Text = "Kết quả: -";
            string response = await UdpTools.ReceiveSocketResponseAsync(txtIP.Text, 100, txtCMD.Text, true);
            bool isSuccess = response.Contains("OK");
            label1.Text = response;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int delay = int.Parse(txtLockerDelay.Text);
            var indexs = txtLockerIndex.Text.Split(",");
            foreach (var item in indexs)
            {

            }
        }
    }
}
