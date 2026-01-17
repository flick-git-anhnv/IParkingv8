using Futech.DisplayLED.Model;
using Futech.Tools;
using Kztek.LedController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Futech.DisplayLED.Viettel
{
    public class ViettelLed : IDisplayLED
    {
        #region : Connection
        public event LedConnectStatusChangeEventHandler ledConnectStatusChangeEvent;
        private bool isConnect = false;
        public bool IsConnect
        {
            get => isConnect;
            set
            {
                if (isConnect != value)
                {
                    this.isConnect = value;
                    ledConnectStatusChangeEvent?.Invoke(this, new LedConnectStatusChangeEventArgs()
                    {
                        SenderId = "",
                        SenderName = "",
                        IsConnect = value
                    });
                }
            }
        }
        #endregion: End Connection
        #region Properties
        private int _RowNumber = 1;
        private string _IPAddress;
        public string IPAddress { get => _IPAddress; set => _IPAddress = value; }

        private int communicationtype = 0;
        public int CommunicationType
        {
            get { return communicationtype; }
            set { communicationtype = value; }
        }
        private int address = 0;
        public int Address
        {
            get { return address; }
            set { address = value; }
        }
        public int RowNumber { get => _RowNumber; set => _RowNumber = value; }
        #endregion

        private bool isMultipleColor = false;
        private ViettelLedhelper ViettelLedhelper;
        public ViettelLed(bool isMultipleColor)
        {
            this.isMultipleColor = isMultipleColor;
            ViettelLedhelper = new ViettelLedhelper(this.isMultipleColor);
        }

        public bool Connect(string comport, int baudrate)
        {
            try
            {
                return ViettelLedhelper.Connect(comport, baudrate, this.isMultipleColor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        private void Led_ErrorEvent(object sender, ErrorEventArgs e)
        {
            LogHelperv2.Logger_SystemError($"LED error: {e.ErrorString} at {e.FunctionName}", LogHelperv2.SaveLogFolder);
        }

        public async void SendToLED(string dtimein, string dtimout, string plate, string money, int cardtype, string cardstate)
        {
            try
            {
                List<string> displayDatas = new List<string>();
                string cardTypeText = "Thẻ tháng";
                if (cardtype == 1)
                {
                    cardTypeText = "Xin chào";
                }

                //preprocessing data
                dtimein = String.IsNullOrEmpty(dtimein) ? String.Empty : dtimein.Remove(0, 5).Replace("/", "-"); // bo nam
                dtimout = String.IsNullOrEmpty(dtimout) ? String.Empty : dtimout.Remove(0, 5).Replace("/", "-"); // bo nam
                if (!String.IsNullOrEmpty(dtimout) && dtimout.Substring(0, 2) == dtimein.Substring(0, 2)) //vao ra trong ngay
                {
                    dtimein = dtimein.Remove(0, 6); // bo ngay thang
                    dtimout = dtimout.Remove(0, 6); // bo ngay thang
                }
                if (String.IsNullOrEmpty(dtimein) && String.IsNullOrEmpty(dtimout))  //custom script
                {
                    if (!String.IsNullOrEmpty(cardstate))
                    {
                        displayDatas.Add("");
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(dtimout))//su kien vao
                    {
                        //displayDatas.Add(plate + "-" + dtimein);
                        //displayDatas.Add(cardTypeText);
                        displayDatas.Add("Xin Mời Vào");
                    }
                    else
                    {
                        if (money != "00")
                        {
                            //displayDatas.Add(money);
                            //displayDatas.Add(cardTypeText);
                            displayDatas.Add(money);
                            displayDatas.Add("LỐI RA");
                        }
                        else
                        {
                            //displayDatas.Add(cardTypeText);
                            //displayDatas.Add();
                            displayDatas.Add("Miễn phí");
                            displayDatas.Add("LỐI RA");
                        }

                        //displayDatas.Add($"Vào: {dtimein}");
                        //displayDatas.Add($"Ra: {dtimout}");
                        //displayDatas.Add(ConstantText.Xinmoiqua);
                    }
                }

                for (int i = 0; i < displayDatas.Count; i++)
                {
                    this.ViettelLedhelper.SetDisplay(displayDatas[i],  1, 1);
                    await Task.Delay(10000);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Close()
        {
            try
            {

            }
            catch (Exception ex)
            {
                LogHelperv2.Logger_SystemError($"{ ex.Message}", LogHelperv2.SaveLogFolder);
            }
        }

        public void SetParkingSlot(int slotNo, byte arrow, byte color)
        {
            return;
        }
    }
}
