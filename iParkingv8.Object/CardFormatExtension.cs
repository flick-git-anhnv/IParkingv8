using iParkingv8.Ultility.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Kztek.Object.CardFormat;

namespace iParkingv8.Object
{
    public static class CardFormatExtension
    {
        public static string ToDisplayString(this EmCardFormatOption option) =>
            option switch
            {
                EmCardFormatOption.Toi_Gian => KZUIStyles.CurrentResources.FormatToiGian,
                EmCardFormatOption.Min_8 => KZUIStyles.CurrentResources.FormatToiThieu8KyTu,
                EmCardFormatOption.Min_10 => KZUIStyles.CurrentResources.FormatToiThieu10KyTu,
                _ => ""
            };
    }
}
