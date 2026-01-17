using System;
using System.Collections.Generic;
using System.Text;

namespace iParkingv5.LedDisplay.LEDs
{
    public enum EmLedDirectionDisplayMode
    {
        Entrance,
        Exit,
        NoEntry,
    }
    public class LedDirectionDisplayMode
    {
        public static string GetDisplayStr(EmLedDirectionDisplayMode emLedDirectionDisplayMode)
        {
            switch (emLedDirectionDisplayMode)
            {
                case EmLedDirectionDisplayMode.Entrance:
                    return "Hiển thị chứ \"Lối vào\"";
                case EmLedDirectionDisplayMode.Exit:
                    return "Hiển thị chứ \"Lối ra, Exit\"";
                case EmLedDirectionDisplayMode.NoEntry:
                    return "Hiển thị dấu X màu đỏ";
                default:
                    return "";
            }
        }
    }
}
