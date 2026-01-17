using iParkingv5.Controller.CardControllers;
using Kztek.Object;
using KztekObject.Cards;
using System.Numerics;

namespace iParkingv5.Controller
{
    public class CardFactory
    {
        public enum EM_CardType
        {
            Proximity_Left,
            ProximityRight,
            Mifare,
            ultra,
        }

        public static ICardFactory CreateCardController(int CardType)
        {
            switch (CardType)
            {
                case (int)EM_CardType.Mifare:
                    return new MifareController(CardType);
                case (int)EM_CardType.Proximity_Left:
                    return new ProximityController(CardType);
                case (int)EM_CardType.ProximityRight:
                    return new ProximityController(CardType);
                default:
                    return null;
            }
        }

        public static string StandardlizedCardNumber(string PreferCard, CardFormatConfig cardFormatConfig)
        {
            try
            {
                //if (cardFormatConfig.InputFormat == cardFormatConfig.OutputFormat)
                //{
                //    return PreferCard;
                //}
                string cardNumberDec = "";
                string baseOutput = "";
                cardNumberDec = HEX_TO_DEC(PreferCard);
                switch (cardFormatConfig.OutputFormat)
                {
                    case CardFormat.EmCardFormat.DECIMA:
                        baseOutput = cardNumberDec;
                        break;
                    case CardFormat.EmCardFormat.HEXA:
                        baseOutput = DEC_TO_HEX(cardNumberDec);
                        break;
                    case CardFormat.EmCardFormat.REHEXA:
                        baseOutput = reverseHex(PreferCard);
                        break;
                    case CardFormat.EmCardFormat.REDECIMA:
                        baseOutput = DEC_TO_REDEC(cardNumberDec);
                        break;
                    case CardFormat.EmCardFormat.XXX_XXXXX:
                        int item1 = (int)long.Parse(cardNumberDec) / 65536;
                        baseOutput = item1.ToString("000") + ":" +
                                  (long.Parse(cardNumberDec) - item1 * 65536).ToString("00000");
                        break;
                    case CardFormat.EmCardFormat.PROXI_MA_SAU:
                        int _item1 = (int)long.Parse(cardNumberDec) / 65536;
                        baseOutput = _item1.ToString("000") +
                                  (long.Parse(cardNumberDec) - _item1 * 65536).ToString("00000");
                        break;
                    default:
                        break;
                }

                switch (cardFormatConfig.OutputOption)
                {
                    case CardFormat.EmCardFormatOption.Toi_Gian:
                        //if (baseOutput.Length == 16)
                        {
                            while (baseOutput.Substring(0, 1) == "0")
                            {
                                baseOutput = baseOutput.Substring(1);
                            }
                        }
                        return baseOutput;
                    case CardFormat.EmCardFormatOption.Min_8:
                        while (baseOutput.Length < 8)
                        {
                            baseOutput = "0" + baseOutput;
                        }
                        return baseOutput;
                    case CardFormat.EmCardFormatOption.Min_10:
                        while (baseOutput.Length < 10)
                        {
                            baseOutput = "0" + baseOutput;
                        }
                        return baseOutput;
                    default:
                        return baseOutput;
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                return PreferCard;
            }
        }

        static string REHEXA_TO_DEC(string rehexa)
        {
            string hex = reverseHex(rehexa);
            return HEX_TO_DEC(hex);
        }
        static string HEX_TO_DEC(string hex)
        {
            if (string.IsNullOrEmpty(hex))
            {
                return "";
            }
            try
            {
                return Convert.ToUInt64(hex, 16).ToString();
            }
            catch (Exception)
            {
                return BigInteger.Parse(hex, System.Globalization.NumberStyles.HexNumber).ToString();
            }
        }
        static string REDEC_TO_DEC(string redec)
        {
            if (string.IsNullOrEmpty(redec))
            {
                return "";
            }
            string rehexa = Convert.ToInt64(redec).ToString("X");
            string hex = reverseHex(rehexa);
            return HEX_TO_DEC(hex);
        }
        static string DEC_TO_REDEC(string redec)
        {
            if (string.IsNullOrEmpty(redec))
            {
                return "";
            }
            string hex = Convert.ToInt64(redec).ToString("X");
            string rehex = reverseHex(hex);
            return HEX_TO_DEC(rehex);
        }

        static string DEC_TO_HEX(string dec)
        {
            string hexa = Convert.ToInt64(dec).ToString("X");
            return hexa;
        }
        static string DEC_TO_REHEX(string dec)
        {
            string hexa = Convert.ToInt64(dec).ToString("X");
            return reverseHex(hexa);
        }
        static string reverseHex(string baseHex)
        {
            if (string.IsNullOrEmpty(baseHex))
            {
                return "";
            }
            while (baseHex.Length % 2 != 0)
            {
                baseHex = "0" + baseHex;
            }
            int count = baseHex.Length / 2;
            string output = "";
            for (int i = count - 1; i >= 0; i--)
            {
                output += baseHex.Substring(i * 2, 2);
            }
            return output;
        }
    }
}
