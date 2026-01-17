using iParkingv5.LedDisplay.Enums;
using Kztek.Object;
using Kztek.Tool;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace iParkingv5.LedDisplay.LEDs.KztekLeds
{
    public abstract class KztekBaseLed : IDisplayLED
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

            //if (ledDisplayConfig.LedId != this.led.id)
            //{
            //    goto END;
            //}
            foreach (var item in ledDisplayConfig.LedDisplaySteps)
            {
                foreach (KeyValuePair<int, LineConfig> itemDetail in item.Value.DisplayDatas)
                {
                    switch (itemDetail.Value.DisplayValue)
                    {
                        case EmLedDisplayValue.NONE:
                            itemDetail.Value.Data = string.Empty;
                            break;
                        case EmLedDisplayValue.CARD_NUMBER:
                            itemDetail.Value.Data = parkingData.CardNumber;
                            break;
                        case EmLedDisplayValue.CARD_NO:
                            itemDetail.Value.Data = parkingData.CardNo;
                            break;
                        case EmLedDisplayValue.CARD_TYPE:
                            itemDetail.Value.Data = parkingData.CardType;
                            break;
                        case EmLedDisplayValue.EVENT_STATUS:
                            itemDetail.Value.Data = parkingData.EventStatus;
                            break;
                        case EmLedDisplayValue.PLATE:
                            itemDetail.Value.Data = parkingData.Plate;
                            break;
                        case EmLedDisplayValue.DATETIME_IN:
                            itemDetail.Value.Data = parkingData.DatetimeIn?.ToString("HH:mm:ss") ?? "";
                            break;
                        case EmLedDisplayValue.DATETIMEOUT:
                            itemDetail.Value.Data = parkingData.DatetimeIn?.ToString("HH:mm:ss") ?? "";
                            break;
                        case EmLedDisplayValue.MONEY:
                            //string temp = TextFormatingTool.GetMoneyFormat(parkingData.Money.ToString());
                            itemDetail.Value.Data = parkingData.Money.ToString();
                            break;
                        case EmLedDisplayValue.Option:
                            break;
                        default:
                            itemDetail.Value.Data = string.Empty;
                            break;
                    }
                }
                await DisplayData(item.Value.DisplayDatas);
                await Task.Delay(TimeSpan.FromMilliseconds(item.Value.DelayTime));
            }

            //END:
            {
                semaphoreSlim.Release();
                GC.Collect();
                return;
            }
        }

        public async Task<bool> DisplayData(Dictionary<int, LineConfig> data)
        {
            string changeScreenCmd = SetCurrentScreenCmd(data);
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
        public string SetCurrentScreenCmd(Dictionary<int, LineConfig> datas)
        {
            string cmd = "SetScreenCurrent?";
            cmd += "/NumLine=" + datas.Count;

            for (int i = 0; i < datas.Count; i++)
            {
                int lineIndex = i + 1;
                int effect = (int)datas[lineIndex].Effect;
                int speed = datas[lineIndex].Speed == 0 ? 5 : datas[lineIndex].Speed;
                int fontsize = datas[lineIndex].FontSize;
                string data = datas[lineIndex].Data;
                int color = (int)datas[lineIndex].DisplayColor;
                cmd += "/Effect" + lineIndex + "=" + effect;
                cmd += "/Speed" + lineIndex + "=" + speed;
                cmd += "/FontSize" + lineIndex + "=" + fontsize;
                cmd += "/Text" + lineIndex + "=";
                cmd += "<Colour" + lineIndex + "=" + color + ">";
                cmd += data;
            }
            return cmd;
        }
    }
}
