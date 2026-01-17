using iParkingv5.LedDisplay.Enums;
using iParkingv5.LedDisplay.LEDs;
using iParkingv8.Object.Objects.Devices;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Ultility;
using Kztek.Object;
using Kztek.Tool;

namespace IParkingv8.Helpers
{
    public static class LedHelper
    {
        public static void DisplayDefaultLed(string laneId)
        {
            foreach (Lane item in AppData.Lanes)
            {
                if (item.Id == laneId)
                {
                    if (!item.DisplayLed)
                    {
                        return;
                    }
                }
            }
            foreach (Led item in AppData.Leds)
            {
                IDisplayLED? led = LedFactory.CreateLed(item);

                if (led != null)
                {
                    if (item.type == (int)EmLedType.DIRECTION_LED_16_32 ||
                        item.type == (int)EmLedType.DIRECTION_LED_32_64)
                    {
                        LedDisplayConfig? ledConfig = NewtonSoftHelper<LedDisplayConfig>.DeserializeObjectFromPath(
                                                    IparkingingPathManagement.laneLedConfigPath(laneId, item.id));
                        if (ledConfig != null)
                        {
                            ParkingData parkingData = new()
                            {
                                Plate = "",
                                CardNo = "",
                                CardNumber = "",
                                CardType = "",
                                DatetimeIn = DateTime.Now,
                                DatetimeOut = null,
                                EventStatus = "",
                                Money = "0"
                            };
                            led.Connect(item);
                            led.SendToLED(parkingData, ledConfig);
                        }
                    }
                    else
                    {
                        LedDisplayConfig? ledConfig = NewtonSoftHelper<LedDisplayConfig>.DeserializeObjectFromPath(
                                                    IparkingingPathManagement.laneLedDefaultConfigPath(laneId, item.id));
                        if (ledConfig != null)
                        {
                            ParkingData parkingData = new()
                            {
                                Plate = "",
                                CardNo = "",
                                CardNumber = "",
                                CardType = "",
                                DatetimeIn = DateTime.Now,
                                DatetimeOut = null,
                                EventStatus = "",
                                Money = "0"
                            };
                            led.Connect(item);
                            led.SendToLED(parkingData, ledConfig);
                        }
                    }
                }
            }

        }
        public static void DisplayLed(string plate, DateTime datetimeIn, AccessKey? identity, string message, string laneId, string fee)
        {
            foreach (Lane item in AppData.Lanes)
            {
                if (item.Id == laneId)
                {
                    if (!item.DisplayLed)
                    {
                        return;
                    }
                }
            }
            foreach (Led item in AppData.Leds)
            {
                if (item.type == (int)EmLedType.DIRECTION_LED_32_64 ||
                    item.type == (int)EmLedType.DIRECTION_LED_16_32)
                {
                    continue;
                }
                IDisplayLED? led = LedFactory.CreateLed(item);
                if (led != null)
                {
                    LedDisplayConfig? ledConfig = NewtonSoftHelper<LedDisplayConfig>.DeserializeObjectFromPath(
                                                    IparkingingPathManagement.laneLedConfigPath(laneId, item.id));
                    if (ledConfig != null)
                    {
                        ParkingData parkingData = new()
                        {
                            Plate = plate,
                            CardNo = identity?.Name ?? "",
                            CardNumber = identity?.Code ?? "",
                            CardType = identity?.Collection?.GetAccessKeyGroupTypeName() ?? "",
                            DatetimeIn = datetimeIn,
                            DatetimeOut = null,
                            EventStatus = message,
                            Money = fee
                        };
                        Task.Run(new Action(() =>
                        {
                            led.Connect(item);
                            led.SendToLED(parkingData, ledConfig);
                        }));
                    }
                }
            }
        }
    }
}
