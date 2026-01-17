using Kztek.Object;
using Kztek.Tool;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace iParkingv5.LedDisplay.LEDs.KztekLeds
{
    public class DirectionLed32_64 : IDisplayLED
    {
        public SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        public Led led { get; set; }
        public string Comport { get; set; } = string.Empty;
        public int Baudrate { get; set; }

        public bool Connect(Led led)
        {
            this.Comport = led.comport;
            this.Baudrate = led.baudrate;
            this.led = led;
            return true;
        }
        public async void SendToLED(ParkingData parkingData, LedDisplayConfig ledDisplayConfig)
        {
            await semaphoreSlim.WaitAsync();
            await DisplayData(ledDisplayConfig.DirectionDisplayMode);
            semaphoreSlim.Release();
            GC.Collect();
            return;
        }

        public async Task<bool> DisplayData(EmLedDirectionDisplayMode emLedDirectionDisplayMode)
        {
            string changeScreenCmd = SetCurrentScreenCmd(emLedDirectionDisplayMode);
            SystemUtils.logger.SaveDeviceLog(new DeviceLog()
            {
                DeviceId = this.led.id,
                DeviceName = this.led.name,
                Cmd = changeScreenCmd,
                Description = "START",
            });
            string result = string.Empty;
            await Task.Run(() =>
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = this.led.id,
                    DeviceName = this.led.name,
                    Cmd = changeScreenCmd,
                    Description = "START",
                });
                result = UdpTools.ExecuteCommand_UTF8(this.Comport, this.Baudrate, changeScreenCmd);

                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = this.led.id,
                    DeviceName = this.led.name,
                    Cmd = changeScreenCmd,
                    Response = result,
                    Description = "END",
                });
            });
            return result.Contains("OK");
        }
        public string SetCurrentScreenCmd(EmLedDirectionDisplayMode emLedDirectionDisplayMode)
        {
            string cmd = "";
            switch (emLedDirectionDisplayMode)
            {
                case EmLedDirectionDisplayMode.Entrance:
                    cmd = "PrintLed?/Entrance";
                    break;
                case EmLedDirectionDisplayMode.Exit:
                    cmd = "PrintLed?/Exit";
                    break;
                case EmLedDirectionDisplayMode.NoEntry:
                    cmd = "PrintLed?/NoEntry";
                    break;
                default:
                    break;
            }
            return cmd;
        }
    }
}