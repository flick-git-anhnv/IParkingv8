using iParkingv8.Ultility.dictionary;
using System.Collections.Concurrent;
using System.Globalization;

namespace iParkingv8.Ultility.Style
{
    public class KZUIStyles
    {
        private static CultureInfo _cultureInfo = CultureInfo.GetCultureInfo("vi-VN");
        public static CultureInfo CultureInfo
        {
            get => _cultureInfo;
            set
            {
                if (value == null) return;

                if (value.LCID != _cultureInfo.LCID)
                {
                    if (BuiltInResources.TryGetValue(value.LCID, out var cultureInfo))
                    {
                        _cultureInfo = cultureInfo.CultureInfo;
                    }
                    else
                    {
                        _cultureInfo = CultureInfo.GetCultureInfo("vi-VN");
                    }
                }
            }
        }

        public static readonly ConcurrentDictionary<int, UIBuiltInResources> BuiltInResources = new();
        public static UIBuiltInResources CurrentResources => BuiltInResources[CultureInfo.LCID];

    }
}
