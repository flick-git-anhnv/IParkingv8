using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;

namespace Kztek.Scale
{
    public class D2008Scale : IScale
    {
        #region Fields

        private SerialPort serialPort;
        private bool isConnected;

        // ---- Default theo ảnh bạn gửi: 9600, 7E1 ----
        private string comPort = "COM1";
        private int baudRate = 9600;
        private int dataBits = 7;
        private int parity = 0;   // 0: Even | 1: Odd | 2: None
        private int stopBits = 1; // 1: One  | 2: Two

        public int receivedTimeOut = 200;
        private bool isStable = true;

        private readonly List<byte> _buffer = new List<byte>();

        private const byte STX = 0x02;
        private const byte ETX = 0x03;

        // 02 + sign + 8 digits + status + 03 = 12 bytes
        private const int FRAME_LENGTH = 12;

        #endregion

        #region Events

        public event ScaleEventHandler ScaleEvent;
        public event ErrorEventHandler ErrorEvent;
        public event DataReceivedEventHandler DataReceivedEvent;

        #endregion

        #region Properties

        public string ComPort { get => comPort; set => comPort = value; }
        public int BaudRate { get => baudRate; set => baudRate = value; }
        public int DataBits { get => dataBits; set => dataBits = value; }
        public int Parity { get => parity; set => parity = value; }
        public int StopBits { get => stopBits; set => stopBits = value; }
        public int ReceivedTimeOut { get => receivedTimeOut; set => receivedTimeOut = value; }
        public bool IsConnected => isConnected;

        public bool IsStable
        {
            get => isStable;
            set => isStable = value;
        }

        #endregion

        #region Connection

        public bool Connect() => Connect(comPort, baudRate);

        public bool Connect(string portName, int baudRate)
        {
            try
            {
                this.comPort = portName;
                this.baudRate = baudRate;

                serialPort = new SerialPort
                {
                    PortName = portName,
                    BaudRate = baudRate,
                    DataBits = dataBits,
                    ReadBufferSize = 4096,
                    WriteBufferSize = 4096,
                    ReadTimeout = receivedTimeOut,   // set theo property
                    WriteTimeout = receivedTimeOut,
                    DtrEnable = false,               // nhiều cân không cần DTR/RTS
                    RtsEnable = false
                };

                serialPort.Parity = parity switch
                {
                    0 => System.IO.Ports.Parity.Even,
                    1 => System.IO.Ports.Parity.Odd,
                    _ => System.IO.Ports.Parity.None
                };

                serialPort.StopBits = stopBits == 2
                    ? System.IO.Ports.StopBits.Two
                    : System.IO.Ports.StopBits.One;

                serialPort.DataReceived += serialPort_DataReceived;
                serialPort.Open();

                isConnected = true;
                return true;
            }
            catch (Exception ex)
            {
                ErrorEvent?.Invoke(this, ex.ToString());
                return false;
            }
        }

        public bool Disconnect()
        {
            try
            {
                if (serialPort != null)
                {
                    serialPort.DataReceived -= serialPort_DataReceived;

                    if (serialPort.IsOpen)
                        serialPort.Close();

                    serialPort.Dispose();
                    serialPort = null;
                }

                isConnected = false;
                return true;
            }
            catch (Exception ex)
            {
                ErrorEvent?.Invoke(this, ex.ToString());
                return false;
            }
        }

        public bool TestConnection() => serialPort != null && serialPort.IsOpen;

        #endregion

        #region Polling

        public void PollingStart()
        {
            isConnected = serialPort != null && serialPort.IsOpen;
        }

        public void PollingStop() { }
        public void SignalToStop() { }

        #endregion

        #region Serial Handling

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (serialPort == null || !serialPort.IsOpen) return;

                int count = serialPort.BytesToRead;
                if (count <= 0) return;

                byte[] bytes = new byte[count];
                int read = serialPort.Read(bytes, 0, count);
                if (read <= 0) return;

                lock (_buffer)
                {
                    _buffer.AddRange(bytes);
                    ProcessBuffer();
                }

                isConnected = true;
            }
            catch (Exception ex)
            {
                ErrorEvent?.Invoke(this, ex.ToString());
            }
        }

        public void ProcessBuffer()
        {
            // Debug raw bytes dạng decimal
            DataReceivedEvent?.Invoke(this, string.Join(',', _buffer));

            while (true)
            {
                // tìm STX đầu tiên
                int stxIndex = _buffer.IndexOf(STX);
                if (stxIndex < 0)
                {
                    _buffer.Clear();
                    return;
                }

                // bỏ rác trước STX
                if (stxIndex > 0)
                    _buffer.RemoveRange(0, stxIndex);

                if (_buffer.Count < FRAME_LENGTH)
                    return;

                // kiểm tra ETX đúng vị trí
                if (_buffer[FRAME_LENGTH - 1] != ETX)
                {
                    _buffer.RemoveAt(0);
                    continue;
                }

                byte[] frame = _buffer.GetRange(0, FRAME_LENGTH).ToArray();
                _buffer.RemoveRange(0, FRAME_LENGTH);

                DataReceivedEvent?.Invoke(this, BitConverter.ToString(frame));
                ParseFrame(frame);
            }
        }

        private void ParseFrame(byte[] frame)
        {
            try
            {
                // Format (theo data bạn capture):
                // [0] STX
                // [1] '+' hoặc '-'
                // [2..9] 8 digits ASCII
                // [10] status (ví dụ: 'B')
                // [11] ETX

                if (frame == null || frame.Length != FRAME_LENGTH) return;
                if (frame[0] != STX || frame[FRAME_LENGTH - 1] != ETX) return;

                char sign = (char)frame[1];
                bool isMinus = sign == '-';

                string digits = Encoding.ASCII.GetString(frame, 2, 6);     // FIX: 8 digits
                char status = (char)frame[10];                             // FIX: lấy status

                if (!long.TryParse(digits, out long gross))
                    return;

                // Suy luận theo dữ liệu bạn gửi: 'B' thường là trạng thái ổn định (Stable)
                // (Nếu sau này bạn có spec chính xác thì map lại tại đây)
                isStable = (status == 'B');

                ScaleEventArgs args = new ScaleEventArgs
                {
                    Gross = (int)gross,
                    IsMinusValue = isMinus,
                    DecimalValue = 0
                    // Nếu ScaleEventArgs của bạn có IsStable/Status thì set thêm ở đây
                };

                ScaleEvent?.Invoke(this, args);
            }
            catch (Exception ex)
            {
                ErrorEvent?.Invoke(this, ex.ToString());
            }
        }

        #endregion
    }

    internal static class ListExtensions
    {
        public static int IndexOf<T>(this List<T> list, T value)
        {
            return list == null ? -1 : list.IndexOf(value);
        }
    }
}
