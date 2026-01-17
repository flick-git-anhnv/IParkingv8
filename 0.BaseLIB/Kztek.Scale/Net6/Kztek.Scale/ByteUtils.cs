using System;
using System.Collections.Generic;
using System.Text;

namespace Kztek.Scale
{
    public class ByteUtils
    {
        private static char[] cHexa = new char[]
        {
            'A',
            'B',
            'C',
            'D',
            'E',
            'F'
        };
        private static int[] iHexaNumeric = new int[]
        {
            10,
            11,
            12,
            13,
            14,
            15
        };
        private static int[] iHexaIndices = new int[]
        {
            0,
            1,
            2,
            3,
            4,
            5
        };

        public static string DecimalToBase(int iDec, int numbase)
        {
            string result;
            try
            {
                string text = "";
                int[] array = new int[32];
                int num = 32;
                while (iDec > 0)
                {
                    int num2 = iDec % numbase;
                    array[--num] = num2;
                    iDec /= numbase;
                }
                for (int i = 0; i < array.Length; i++)
                {
                    if ((int)array.GetValue(i) >= 10)
                    {
                        text += cHexa[(int)array.GetValue(i) % 10];
                    }
                    else
                    {
                        text += array.GetValue(i);
                    }
                }
                text = text.Substring(30, 2);
                result = text;
            }
            catch
            {
                result = "0";
            }
            return result;
        }
        public static int BaseToDecimal(string sBase, int numbase)
        {
            int result;
            try
            {
                int num = 0;
                int num2 = 1;
                string text = "";
                if (numbase > 10)
                {
                    for (int i = 0; i < cHexa.Length; i++)
                    {
                        text += cHexa.GetValue(i).ToString();
                    }
                }
                int j = sBase.Length - 1;
                while (j >= 0)
                {
                    string text2 = sBase[j].ToString();
                    int num3;
                    if (text2.IndexOfAny(cHexa) >= 0)
                    {
                        num3 = iHexaNumeric[text.IndexOf(sBase[j])];
                    }
                    else
                    {
                        num3 = (int)(sBase[j] - '0');
                    }
                    num += num3 * num2;
                    j--;
                    num2 *= numbase;
                }
                result = num;
            }
            catch
            {
                result = 0;
            }
            return result;
        }
    }
}
