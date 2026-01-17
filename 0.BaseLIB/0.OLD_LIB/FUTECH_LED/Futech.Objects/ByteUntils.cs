using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Futech.Objects
{
    public static class ByteUntils
    {
        // Convert from Decimal to Base
        private const int base10 = 10;
        private static char[] cHexa = new char[] { 'A', 'B', 'C', 'D', 'E', 'F' };
        private static int[] iHexaNumeric = new int[] { 10, 11, 12, 13, 14, 15 };
        private static int[] iHexaIndices = new int[] { 0, 1, 2, 3, 4, 5 };
        private const int asciiDiff = 48;

        public static string DecimalToBase(int iDec, int numbase)
        {
            try
            {
                string strBin = "";
                int[] result = new int[32];
                int MaxBit = 32;
                for (; iDec > 0; iDec /= numbase)
                {
                    int rem = iDec % numbase;
                    result[--MaxBit] = rem;
                }
                for (int i = 0; i < result.Length; i++)
                    if ((int)result.GetValue(i) >= base10)
                        strBin += cHexa[(int)result.GetValue(i) % base10];
                    else
                        strBin += result.GetValue(i);
                strBin = strBin.Substring(30, 2);
                return strBin;
            }
            catch
            {
                return "0";
            }
        }

        //convert from Base to Decimal
        public static int BaseToDecimal(string sBase, int numbase)
        {
            try
            {
                int dec = 0;
                int b;
                int iProduct = 1;
                string sHexa = "";
                if (numbase > base10)
                    for (int i = 0; i < cHexa.Length; i++)
                        sHexa += cHexa.GetValue(i).ToString();
                for (int i = sBase.Length - 1; i >= 0; i--, iProduct *= numbase)
                {
                    string sValue = sBase[i].ToString();
                    if (sValue.IndexOfAny(cHexa) >= 0)
                        b = iHexaNumeric[sHexa.IndexOf(sBase[i])];
                    else
                        b = (int)sBase[i] - asciiDiff;
                    dec += (b * iProduct);
                }
                return dec;
            }
            catch
            {
                return 0;
            }
        }

        //function CRC. B15 => CRC = (Asc(B0) + Asc(B1) + .... + Asc(B15)) Mod 256
        public static byte CRC(byte[] buffer, int count)
        {
            int temp = 0;
            for (int i = 0; i < count - 1; i++)
            {
                temp = temp + BaseToDecimal(DecimalToBase(Convert.ToInt32(buffer[i]), 16), 16);
            }
            temp = temp % 256;
            return Convert.ToByte(DecimalToBase(Convert.ToInt32(temp), 16), 16);
        }

        // Get byte array from image
        public static byte[] GetByteArray(Image image)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                image.Save(ms, ImageFormat.Jpeg);
                byte[] bytearray = ms.ToArray();
                return bytearray;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occur while get byte array from image: " + ex.Message);
                return null;
            }
        }

        // Get image from byte array
        public static Image GetImage(byte[] bytearray)
        {
            try
            {
                MemoryStream mem = new MemoryStream();
                mem.Write(bytearray, 0, bytearray.Length);
                Image image = Image.FromStream(mem);
                return image;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occur while get image from byte array: " + ex.Message);
                return null;
            }
        }
    }
}
