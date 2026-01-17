using Kztek.Tool;
using System.Text;
using System.Text.RegularExpressions;

namespace iParkingv5.Controller.KztekDevices.BR_CTRL
{
    public enum EmBarriePosition : int
    {
        UNKNOWN = 0,
        ĐÃ_ĐÓNG = 1,
        ĐÃ_MỞ = 2,
        Ở_GIỮA_VÀ_DỪNG = 3,
        Ở_GIỮA_VÀ_ĐANG_ĐÓNG = 4,
        Ở_GIỮA_VÀ_ĐANG_MỞ = 5,
    }
    public class BarrieChangeStatusEventArgs : EventArgs
    {
        public string ControllerID { get; set; } = string.Empty;
        public string ControllerName { get; set; } = string.Empty;
        public int NewPosition { get; set; } = 0;
        public string NewErrorStatus { get; set; } = string.Empty;
    }
    public delegate void OnBarrieChangeStatusEventHandler(object sender, BarrieChangeStatusEventArgs e);

    public class BR_LAN_CTRL : BaseKzDevice, I_BR_CTRL
    {
        private EmBarriePosition barriePosition = EmBarriePosition.UNKNOWN;
        private string barrieErrorStr = "";
        public event OnBarrieChangeStatusEventHandler? OnBarrieChangeStatusChangeEvent;

        private Thread thread = null;
        private ManualResetEvent stopEvent = null;
        public bool Running
        {
            get
            {
                if (thread != null)
                {
                    if (thread.Join(0) == false)
                        return true;

                    Free();
                }
                return false;
            }
        }
        private void Free()
        {
            thread = null;
            stopEvent.Close();
            stopEvent = null;
        }
        public void SignalToStop()
        {
            // stop thread
            if (thread != null)
            {
                // signal to stop
                stopEvent.Set();
            }
        }

        public bool CloseBarrie()
        {
            string cmd = $"SetBarrier?/Cmd=Close";
            string response = string.Empty;
            response = UdpTools.ExecuteCommand(this.ControllerInfo.Comport, GetBaudrate(this.ControllerInfo.Baudrate), cmd, 500, UdpTools.STX, Encoding.ASCII);
            // Trang thai thiet bij
            this.ControllerInfo.IsConnect = response.Contains("OK");
            return response.Contains("OK");
        }
        public bool OpenBarrie()
        {
            string cmd = $"SetBarrier?/Cmd=Open";
            string response = string.Empty;
            response = UdpTools.ExecuteCommand(this.ControllerInfo.Comport, GetBaudrate(this.ControllerInfo.Baudrate), cmd, 500, UdpTools.STX, Encoding.ASCII);
            // Trang thai thiet bij
            this.ControllerInfo.IsConnect = response.Contains("OK");
            return response.Contains("OK");
        }
        public bool StopBarrie()
        {
            string cmd = $"SetBarrier?/Cmd=Stop";
            string response = string.Empty;
            response = UdpTools.ExecuteCommand(this.ControllerInfo.Comport, GetBaudrate(this.ControllerInfo.Baudrate), cmd, 500, UdpTools.STX, Encoding.ASCII);
            // Trang thai thiet bij
            this.ControllerInfo.IsConnect = response.Contains("OK");
            return response.Contains("OK");
        }

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
        public void WaitForStop()
        {
            if (thread != null)
            {
                thread.Join();
                Free();
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

        public override async Task DeleteCardEvent()
        {
        }

        public override async Task<bool> OpenDoor(int timeInMilisecond, int relayIndex)
        {
            return OpenBarrie();
        }
        public async void WorkerThread()
        {
            while (stopEvent != null)
            {
                if (!stopEvent.WaitOne(0, true))
                {
                    try
                    {
                        await PollingGetBarrieStatus();
                    }
                    catch
                    {
                        GC.Collect();
                    }
                }
            }
        }
        private async Task PollingGetBarrieStatus()
        {
            string comport = this.ControllerInfo.Comport;
            int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
            string getEventCmd = "GetBarrierStatus?/";
            string response = string.Empty;
            await Task.Run(() =>
            {
                response = UdpTools.ExecuteCommand(comport, baudrate, getEventCmd, 500, UdpTools.STX, Encoding.ASCII);
            });
            // Trang thai thiet bij
            this.ControllerInfo.IsConnect = response != "" && !response.Contains("exception", StringComparison.CurrentCultureIgnoreCase);
            //response = GetBarrierStatus?/Position=1/Error=00000000
            if (response != "" && response.Contains("GetBarrierStatus?/"))
            {
                string[] data = response.Split('/');
                Dictionary<string, string> map = GetEventContent(data);

                string strBarriePosition = map.ContainsKey("position") ? map["position"] : "";
                int _barriePosition = Regex.IsMatch(strBarriePosition, @"^\d+$") ? Convert.ToInt32(strBarriePosition) : -1;

                string strBarrieError = map.ContainsKey("error") ? map["error"] : "";

                bool isHaveChange = (int)this.barriePosition != _barriePosition ||
                                    strBarrieError != this.barrieErrorStr;
                if (isHaveChange)
                {
                    if (OnBarrieChangeStatusChangeEvent != null)
                    {
                        this.barriePosition = (EmBarriePosition)_barriePosition;
                        this.barrieErrorStr = strBarrieError;
                        BarrieChangeStatusEventArgs e = new BarrieChangeStatusEventArgs()
                        {
                            ControllerID = this.ControllerInfo.Id,
                            ControllerName = this.ControllerInfo.Name,
                            NewPosition = (int)barriePosition,
                            NewErrorStatus = strBarrieError,
                        };
                        OnBarrieChangeStatusChangeEvent?.Invoke(this, e);
                    }
                }
            }
            await Task.Delay(300);
        }
    }
}
