using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kztek.LedController;
using System.Threading;
using System.Windows.Forms;
using Futech.Tools;

namespace Futech.DisplayLED
{
    class KPS_001 : IDisplayLED
    {
        private enum Em_KpsFont
        {
            _10_pt_bold = 0,
            _11_pt_bold = 1,
            _12_pt_bold = 2,
            _13_pt_bold = 3,
            _14_pt_bold = 4,
            _9_pt_regular = 5,
            _10_pt_regular = 6,
            _11_pt_regular = 7,
            _12_pt_regular = 8,
            _13_pt_regular = 9,
            _14_pt_regular = 10,
            _8_pt_regular = 11,
            _8_pt_bold = 12,
            _8_pt_regular_2 = 13,
        }

        private enum Em_Shift
        {
            Stand = 0,
            LeftShift = 1,
            RightShift = 2,
        }

        private int max_character_display = 8;

        private SerialPort serialPort;
        private int _DisplayDuration = 5000;
        private int _RowNumber = 1;
        private int communicationtype = 0;
        private int address = 0;
        private List<string> script = new List<string>();
        private List<string> defaultScreen = new List<string>();
        private CancellationTokenSource tokenSource = new CancellationTokenSource();
        private Task Displaytask;
        public int DisplayDuration { get => _DisplayDuration; }
        public int CommunicationType { get => communicationtype; set => communicationtype = value; }
        public int Address { get => address; set => address = value; }
        public int RowNumber { get => _RowNumber; set => _RowNumber = value; }

        public KPS_001()
        {

        }

        public void Close()
        {
            serialPort.Close();
        }

        public bool Connect(string comport, int baudrate)
        {
            try
            {
                serialPort = new SerialPort()
                {
                    PortName = comport,
                    BaudRate = baudrate,
                    DataBits = 8,
                    Parity = Parity.None,
                    StopBits = StopBits.One,
                };
                serialPort.Open();
                serialPort.DataReceived += SerialPort_DataReceived;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            (sender as SerialPort).ReadExisting();
        }

        public void SendToLED(string dateTimeIn, string dateTimeOut, string plate, string money, int cardtype, string cardstate)
        {
            try
            {
                ClearScrren();
                SetScreen("", 0);

                //string cardTypeText = "The Thang";
                //if (cardtype == 1)
                //{
                //    cardTypeText = "Xin Chao";
                //}

                //preprocessing data
                if (DateTime.TryParse(dateTimeIn, out DateTime dtDateTimeIn) && DateTime.TryParse(dateTimeOut, out DateTime dtDateTimeOut))
                {
                    if (dtDateTimeOut.Year != dtDateTimeIn.Year)
                    {
                        dateTimeIn = dtDateTimeIn.ToString("HH:mm dd/MM/yyyy");
                        dateTimeOut = dtDateTimeOut.ToString("HH:mm dd/MM/yyyy");
                    }
                    else if (dtDateTimeIn.DayOfYear.Equals(dtDateTimeOut.DayOfYear))
                    {
                        dateTimeIn = dtDateTimeIn.ToString("HH:mm");
                        dateTimeOut = dtDateTimeOut.ToString("HH:mm");
                    }
                    else
                    {
                        dateTimeIn = dtDateTimeIn.ToString("HH:mm dd/MM");
                        dateTimeOut = dtDateTimeOut.ToString("HH:mm dd/MM");
                    }
                }
                else if (DateTime.TryParse(dateTimeIn, out dtDateTimeIn))
                {
                    dateTimeIn = dtDateTimeIn.ToString("HH:mm dd/MM");
                }
                else //custom script
                {
                    script = GetScript(cardstate);
                    DisplayData();
                    return;
                }

                if (String.IsNullOrEmpty(dateTimeOut))//su kien vao
                {
                    script.Add(plate);
                    //script.Add(dateTimeIn);
                    //script.Add(cardTypeText);
                    defaultScreen.AddRange(GetScript(ConstantText.Xinmoivao));
                }
                else
                {
                    if (money != "00" && !string.IsNullOrEmpty(money) && money != "0")
                    {
                        script.Add(money);
                        //script.Add(cardTypeText);
                        //script.Add(plate);
                    }
                    else
                    {
                        //script.Add(cardTypeText);
                        script.Add("0000");
                    }

                    //script.Add($"Vào: {dateTimeIn}");
                    //script.Add($"Ra: {dateTimeOut}");
                    script.Add(ConstantText.Xinmoiqua);
                    defaultScreen.AddRange(GetScript(ConstantText.Xinchao));
                }
                DisplayData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }

        private void DisplayData()
        {
            if (Displaytask != null && !Displaytask.IsCompleted)
            {
                tokenSource.Cancel();
            }

            tokenSource = new CancellationTokenSource();
            Displaytask = Display();
            return;
        }

        private List<string> GetScript(string text)
        {
            var rows = text.Split('\n').ToList();
            return rows;
        }

        public void SetParkingSlot(int slotNo, byte arrow, byte color)
        {
            throw new NotImplementedException();
        }

        private Task Display()
        {
            var task = Task.Run(async () =>
            {
                int rowInLed = 0;
                CancellationToken CancellationToken = tokenSource.Token;
                if (script != null)
                {
                    for (int i = 0; i < script.Count; i += RowNumber)
                    {
                        if (i + RowNumber <= script.Count)
                        {
                            if (!CancellationToken.IsCancellationRequested)
                            {
                                rowInLed = 0;
                                foreach (var line in script.GetRange(i, RowNumber))
                                {
                                    if (i == 0)
                                    {
                                        SetScreenStand(line, rowInLed);
                                    }
                                    else
                                        SetScreen(line, rowInLed);
                                    rowInLed++;
                                    if (rowInLed == RowNumber)
                                    {
                                        rowInLed = 0;
                                    }
                                    await Task.Delay(DisplayDuration, CancellationToken);
                                }
                            }
                        }

                        if (i < script.Count && (i + RowNumber) > script.Count) //dòng lẻ (led 2 dòng, script dài 5 => lẻ 1)
                        {
                            rowInLed = 0;
                            foreach (var line in script.GetRange(i, script.Count - i - 1))
                            {
                                if (!CancellationToken.IsCancellationRequested)
                                {
                                    SetScreen(line, rowInLed);
                                    rowInLed++;
                                    if (rowInLed == RowNumber)
                                    {
                                        rowInLed = 0;
                                    }
                                    await Task.Delay(DisplayDuration, CancellationToken);
                                }
                            }
                        }
                    }

                    await Task.Delay(DisplayDuration, CancellationToken);
                    rowInLed = 0;
                    foreach (var line in defaultScreen)
                    {
                        if (rowInLed < RowNumber)
                        {
                            if (!CancellationToken.IsCancellationRequested)
                            {
                                SetScreen(line, rowInLed);
                            }
                        }
                        rowInLed++;
                    }
                }
            });
            return task;
        }

        private void SetScreen(string message, int rowIndex)
        {
            try
            {
                //if (message == "00")
                //data = GetChangeScreenByteArrayData(message, rowIndex, 0, 2, Em_KpsFont._8_pt_regular, Em_Shift.RightShift);
                //else
                byte[] data = GetChangeScreenByteArrayData(message, rowIndex, 0, 2, Em_KpsFont._8_pt_regular, Em_Shift.RightShift);

                //string test = "";
                //foreach(byte a in data)
                //{
                //    test += a.ToString() + " ";
                //}
                //SystemUI.SaveLogFile("SEND TO LED " + message + "WITH CMD" + test);
                serialPort.Write(data, 0, data.Length);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SetScreenStand(string message, int rowIndex)
        {
            try
            {
                //if (message == "00")
                //data = GetChangeScreenByteArrayData(message, rowIndex, 0, 2, Em_KpsFont._8_pt_regular, Em_Shift.RightShift);
                //else
                byte[] data = GetChangeScreenByteArrayData(message, rowIndex, 0, 2, Em_KpsFont._8_pt_regular, Em_Shift.Stand);
                serialPort.Write(data, 0, data.Length);
            }
            catch (Exception)
            {
                throw;
            }
        }

        byte[] GetChangeScreenByteArrayData(string data, int rowIndex, int xLocation, int yLocation, Em_KpsFont font, Em_Shift shift)
        {
            try
            {
                string initData = "TEXTP" + rowIndex + "," + data + "," + xLocation + "," + yLocation + "," + (int)font + ",1," + (int)shift;
                byte[] dataArray = Encoding.ASCII.GetBytes(initData);
                initData = "$" + initData;
                int crc = checkSum(dataArray);
                string crc_hex = Convert.ToString(crc, 16);
                initData += "*";
                initData += crc_hex;
                initData += "#";
                return Encoding.ASCII.GetBytes(initData);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ClearScrren()
        {
            script.Clear();
            defaultScreen.Clear();
            for (int i = 0; i <= RowNumber; i++)
            {
                byte[] data = GetClearByte(i);
                serialPort.Write(data, 0, data.Length);
            }
        }

        private byte[] GetClearByte(int lineIndex)
        {
            string initData = "CLEAR,P" + lineIndex;
            byte[] dataArray = Encoding.ASCII.GetBytes(initData);
            initData = "$" + initData;
            int crc = checkSum(dataArray);
            string crc_hex = Convert.ToString(crc, 16);
            initData += "*";
            initData += crc_hex;
            initData += "#";
            return Encoding.ASCII.GetBytes(initData);
        }

        private int checkSum(byte[] buffer)
        {
            int crc = 0;
            for (int i = 0; i < buffer.Length; i++)
            {
                crc ^= buffer[i];
            }
            return crc;
        }
    }
}
