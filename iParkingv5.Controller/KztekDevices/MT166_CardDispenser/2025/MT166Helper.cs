using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParkingv5.Controller.KztekDevices.MT166_CardDispenser._2025
{
    public static class MT166Helper
    {
        public static string HexToBinary(string hex)
        {
            string binary = string.Empty;
            foreach (char hexChar in hex)
            {
                binary += Convert.ToString(Convert.ToInt32(hexChar.ToString(), 16), 2).PadLeft(4, '0');
            }
            return binary.PadLeft(8, '0');
        }
    }
}
