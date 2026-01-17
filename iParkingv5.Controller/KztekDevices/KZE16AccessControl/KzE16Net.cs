using iParkingv5.Controller.KztekDevices.KZE02NETController;
using iParkingv5.Objects.Events;
using iParkingv6.Objects.Datas;
using Kztek.Object;
using Kztek.Tool;
using System.Text.RegularExpressions;
using static Kztek.Object.InputTupe;

namespace iParkingv5.Controller.KztekDevices.KZE16AccessControl
{
    public class KzE16Net : KzE02Net
    {
        private Thread thread = null;
        private ManualResetEvent stopEvent = null;
        //public override async Task<bool> OpenDoor(int timeInMilisecond, int relayIndex)
        //{
        //    string comport = this.ControllerInfo.Comport;
        //    int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
        //    string openRelayCmd = $"SetRelayPulse?/Relay={relayIndex:00}";

        //    this.IsBusy = true;
        //    string response = string.Empty;
        //    await Task.Run(() =>
        //    {
        //        response = UdpTools.ExecuteCommand(comport, baudrate, openRelayCmd, 500, UdpTools.STX, Encoding.ASCII);
        //    });
        //    this.IsBusy = false;
        //    if (UdpTools.IsSuccess(response, "OK"))
        //    {
        //        return true;
        //    }
        //    else if (UdpTools.IsSuccess(response, "ERR"))
        //    {
        //        return false;
        //    }
        //    OnErrorEvent(new ControllerErrorEventArgs()
        //    {
        //        ErrorString = response,
        //        ErrorFunc = "OpenDoor",
        //        CMD = openRelayCmd
        //    });
        //    return false;
        //}

        public override void PollingStart()
        {
            if (thread == null)
            {
                // create events
                stopEvent = new ManualResetEvent(false);
                // start thread
                thread = new Thread(new ThreadStart(WorkerThread));
                thread.Start();
            }
        }
        public override void PollingStop()
        {
            if (this.Running)
            {
                SignalToStop();
                while (thread.IsAlive)
                {
                    if (WaitHandle.WaitAll(
                        (new ManualResetEvent[] { stopEvent }),
                        100,
                        true))
                    {
                        WaitForStop();
                        break;
                    }
                    Application.DoEvents();
                }
            }
        }

        // Signal thread to stop work
        public void SignalToStop()
        {
            // stop thread
            if (thread != null)
            {
                // signal to stop
                stopEvent.Set();
            }
        }
        // Wait for thread stop
        public void WaitForStop()
        {
            if (thread != null)
            {
                // wait for thread stop
                thread.Join();

                Free();
            }
        }
        private void Free()
        {
            thread = null;
            // release events
            stopEvent.Close();
            stopEvent = null;
        }

        public async void WorkerThread()
        {
            while (stopEvent != null)
            {
                if (stopEvent.WaitOne(0, true))
                {
                    return;
                }
                try
                {
                    string comport = this.ControllerInfo.Comport;
                    int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
                    string getEventCmd = KZTEK_CMD.GetCardEventCMD();
                    this.IsBusy = true;
                    string response = await UdpTools.ReceiveSocketResponseAsync(this.ControllerInfo.Id, this.ControllerInfo.Name, comport, baudrate, getEventCmd, false);
                    this.IsBusy = false;
                    // Trang thai thiet bij
                    this.ControllerInfo.IsConnect = response != "" && !response.Contains("exception", StringComparison.CurrentCultureIgnoreCase);
                    //AccessCardGrant: Char(2) + GetEvent?/Style=Card/UserID=100/LenCard=4/Card=7C19F640/Reader=01/DateTime=YYYYMMDDhhmmss/CardState=U/AccessState=1/Door=00/StateMSG=00 + char(3)
                    //AccessCardDenie: Char(2) + GetEvent?/Style=Card/UserID=Null/LenCard=4/Card=7C19F640/Reader=01/DateTime=YYYYMMDDhhmmss/CardState=U/AccessState=1/Door=00/StateMSG=00 + char(3)
                    //InputEvent     : Char(2) + GetEvent?/Style=input/Input=INPUT1/DateTime=YYYYMMDDhhmmss + char(3)
                    //NoEvent        : Char(2) + GetEvent?/NotEvent + char(3)
                    //response = "GetCardEvent?/UserID=NULL/LenCard=3/Card=BDE223/Reader=02/DateTime=20240511062908";
                    if (response != "" && (response.Contains("GetCardEvent?/")) && !response.Contains("NotEvent"))
                    {
                        string[] data = response.Split('/');
                        Dictionary<string, string> map = GetEventContent(data);
                        bool isCardEvent = response.Contains("Card");
                        if (isCardEvent)
                        {
                            await CallCardEvent(this.ControllerInfo, map);
                        }
                        else
                        {
                            await CallInputEvent(this.ControllerInfo, map);
                        }
                    }
                    await Task.Delay(100);
                }
                catch (Exception ex)
                {
                }
            }
        }
        private async Task CallInputEvent(Bdk controller, Dictionary<string, string> map)
        {
            InputEventArgs ie = new InputEventArgs
            {
                DeviceId = controller.Id
            };
            string str_inputName = map.ContainsKey("input") ? map["input"] : "";
            if (!string.IsNullOrEmpty(str_inputName))
            {
                string str_inputIndex = str_inputName.Replace("INPUT", "");
                ie.InputIndex = Regex.IsMatch(str_inputIndex, @"^\d+$") ? int.Parse(str_inputIndex) : -1;
            }
            if (ie.InputIndex == 1 || ie.InputIndex == 2)
            {
                ie.InputType = EmInputType.Exit;
            }
            else if (ie.InputIndex == 3 || ie.InputIndex == 4)
            {
                ie.InputType = InputTupe.EmInputType.Loop;
            }
            await DeleteCardEvent();
            OnInputEvent(ie);
        }
        private async Task CallCardEvent(Bdk controller, Dictionary<string, string> map)
        {
            try
            {
                CardEventArgs e = new CardEventArgs
                {
                    DeviceId = controller.Id,
                    AllCardFormats = new List<string>(),
                };
                string cardNumberHEX = map.ContainsKey("card") ? map["card"] : "";
                e.PreferCard = cardNumberHEX;
                string str_readerIndex = map.ContainsKey("reader") ? map["reader"] : "";
                e.ReaderIndex = Regex.IsMatch(str_readerIndex, @"^\d+$") ? Convert.ToInt32(str_readerIndex) : -1;
                string cardState = map.ContainsKey("cardstate") ? map["cardstate"] : "";
                if (cardState == "R")
                {
                    string door = map.ContainsKey("door") ? map["door"] : "";
                    if (!string.IsNullOrEmpty(door))
                    {
                        if (door == "01")
                        {
                            e.Doors = "1";
                        }
                        if (door == "02")
                        {
                            e.Doors = "2";
                        }
                    }
                }
                else
                {
                    e.Doors = "";
                }
                OnCardEvent(e);
                await DeleteCardEvent();
            }
            catch (Exception)
            {
                await DeleteCardEvent();
            }
        }
        public override async Task DeleteCardEvent()
        {
            string comport = this.ControllerInfo.Comport;
            int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
            string cmd = KZTEK_CMD.DeleteCardEventCMD();
            await UdpTools.ReceiveSocketResponseAsync(this.ControllerInfo.Id, this.ControllerInfo.Name, comport, baudrate, cmd, false);
            //UdpTools.ExecuteCommand(comport, baudrate, cmd, 500, UdpTools.STX, Encoding.ASCII);
        }

        public override async Task<bool> OpenDoor(int timeInMilisecond, int relayIndex)
        {
            string comport = this.ControllerInfo.Comport;
            int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
            string openRelayCmd = KZTEK_CMD.OpenRelayCMD(relayIndex);

            this.IsBusy = true;
            string response = await UdpTools.ReceiveSocketResponseAsync(this.ControllerInfo.Id, this.ControllerInfo.Name, comport, baudrate, openRelayCmd, true);
            this.IsBusy = false;
            if (UdpTools.IsSuccess(response, "OK"))
            {
                return true;
            }
            else if (UdpTools.IsSuccess(response, "ERR"))
            {
                return false;
            }
            OnErrorEvent(new ControllerErrorEventArgs()
            {
                ErrorString = response,
                ErrorFunc = "OpenDoor",
                CMD = openRelayCmd
            });
            return false;
        }
        //public bool DeleteCard(string userId, string cardNumber, out string errorMessage, out int errorCode)
        //{
        //    string comport = this.ControllerInfo.Comport;
        //    int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
        //    string deleteCMD = KZTEK_CMD.DeleteUserCMD(int.Parse(userId));
        //    string response = UdpTools.ExecuteCommand(comport, baudrate, deleteCMD, 500, UdpTools.STX, Encoding.ASCII);
        //    if (UdpTools.IsSuccess(response, "OK"))
        //    {
        //        errorMessage = string.Empty;
        //        errorCode = -1;
        //        return true;
        //    }
        //    errorCode = -1;
        //    errorMessage = "Device return false";
        //    return false;
        //}
        //public bool DownloadCard(string userId, string cardNumber, EM_CardType cardType, int timezoneId, string doors, out string errorMessage, out int errorCode)
        //{
        //    ICardFactory cardFactory = CardFactory.CreateCardController((int)cardType);
        //    string _cardNumber = cardNumber; //cardFactory.GetCardHexNumber(cardNumber);
        //    int cardlen = 4;// cardFactory.CardLen();
        //    string comport = this.ControllerInfo.Comport;
        //    int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
        //    string door = int.Parse(doors).ToString("00");
        //    if (door == "")
        //    {
        //        errorCode = -1;
        //        errorMessage = "UNKNOWN";
        //        return false;
        //    }
        //    string downloadCMD = KZTEK_CMD.DownloadUserCMD(Convert.ToInt32(userId), _cardNumber, cardlen, timezoneId, door);
        //    string response = UdpTools.ExecuteCommand(comport, baudrate, downloadCMD, 500, UdpTools.STX, Encoding.ASCII);
        //    bool result = UdpTools.IsSuccess(response, "OK");
        //    errorCode = -1;
        //    errorMessage = "UNKNOWN";
        //    return result;
        //}

    }
}
