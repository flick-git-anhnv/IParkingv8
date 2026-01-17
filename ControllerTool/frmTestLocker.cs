using Kztek.Tool;
using System.Diagnostics;

namespace ControllerTool
{
    public partial class frmTestLocker : Form
    {
        public frmTestLocker()
        {
            InitializeComponent();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void TestLocker_Click(object sender, EventArgs e)
        {

        }
        CancellationTokenSource cts;
        private async void button1_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            string ip = txtlockerIp.Text;
            var readers = txtLockerIndex.Text.Split(',').ToList();
            var delay = int.Parse(txtLockerDelay.Text);
            cts = new CancellationTokenSource();
            while (!cts.IsCancellationRequested)
            {
                foreach (var readerStr in readers)
                {
                    string cmd = $"SetRelay?/Relay={int.Parse(readerStr)}/State=ON";
                    Stopwatch stopwatch = new Stopwatch();
                    string response = await UdpTools.ReceiveSocketResponseAsync(ip, 100, cmd, true);
                    dgvResponse.Rows.Insert(0, DateTime.Now.ToString("HH:mm:ss"), readerStr, response, stopwatch.ElapsedMilliseconds);
                    if (dgvResponse.Rows.Count >= 1000)
                    {
                        dgvResponse.Rows.RemoveAt(dgvResponse.RowCount - 1);
                    }
                    dgvResponse.CurrentCell = dgvResponse.Rows[0].Cells[0];
                    await Task.Delay(delay, cts.Token);
                }
                await Task.Delay(delay, cts.Token);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            cts?.Cancel();
            btnStop.Enabled = false;
            btnStart.Enabled = true;
        }
    }
}
