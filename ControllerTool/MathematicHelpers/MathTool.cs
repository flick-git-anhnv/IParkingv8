using System;

namespace Kztek.Tool
{
    public class MathTool
    {
        public static long HexToDec(string hexNumber)
        {
            return Convert.ToInt64(hexNumber, 16);
        }
        public static string HexToBin(string hexNumber, int binaryLength)
        {
            string binData = Convert.ToString(HexToDec(hexNumber), 2);
            while (binData.Length < 8)
            {
                binData = "0" + binData;
            }
            return binData;
        }
    }
}
