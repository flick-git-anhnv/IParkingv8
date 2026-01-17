using iParkingv8.Object.ConfigObjects.LaneConfigs;
using iParkingv8.Object.Objects.Payments;
using System.IO.Ports;

namespace IParkingv8.QRScreenController.QRView
{
    public class QRViewDevice : IQRDevice
    {
        public SerialPort serialPort { get; set; }
        public QRViewDevice(LaneOptionalConfig paymentConfig)
        {
            if (!string.IsNullOrEmpty(paymentConfig.StaticQRComport))
            {
                this.serialPort = new SerialPort
                {
                    BaudRate = paymentConfig.StaticQRBaudrate,
                    Parity = Parity.None,
                    DataBits = 8,
                    StopBits = StopBits.One,
                    Handshake = Handshake.None,
                    PortName = paymentConfig.StaticQRComport,
                };
            }
           
        }
        public async Task<bool> OpenAsync()
        {
            if (this.serialPort is null)
            {
                return true;
            }
            return await Task.Run(() =>
            {
                if (serialPort.IsOpen)
                {
                    return true;
                }
                try
                {
                    serialPort.Open();
                    return serialPort.IsOpen;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public async Task<bool> CloseAsync()
        {
            if (this.serialPort is null)
            {
                return true;
            }
            return await Task.Run(() =>
            {
                try
                {
                    if (serialPort.IsOpen)
                    {
                        serialPort.Close();
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }
        public async Task<bool> DisplayQRAsync(string bankName, string accountNumber, long money, string description)
        {
            Enum.TryParse<EmNapasBankCode>(bankName, out var bankCode);
            return await Task.Run(async () =>
            {
                int bank = (int)bankCode;
                string displayQR = VietQRHelper.GenerateVietQRString(bank.ToString(), accountNumber, money.ToString(), description);

                string cmd = GenerateQRCodeCMD(displayQR, bankCode, accountNumber, money, description);

                if (!this.serialPort.IsOpen)
                {
                    await OpenAsync();
                }
                if (!this.serialPort.IsOpen)
                {
                    return false;
                }
                try
                {
                    this.serialPort.WriteLine(cmd);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            });

        }
        public async Task<bool> OpenHomePageAsync()
        {
            return await Task.Run(async () =>
            {
                string cmd = $"JUMP(0);\r\n";

                if (!this.serialPort.IsOpen)
                {
                    await OpenAsync();
                }
                if (!this.serialPort.IsOpen)
                {
                    return false;
                }
                try
                {
                    this.serialPort.WriteLine(cmd);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            });

        }

        private string GenerateQRCodeCMD(string displayQR, EmNapasBankCode bankCode, string accountNumber, long money, string description)
        {
            return $"JUMP(1);QBAR(0,{displayQR});SET_TXT(0,{bankCode});SET_TXT(1,STK: {accountNumber});SET_TXT(2,{money:N0});\r\n";
        }
    }
}
