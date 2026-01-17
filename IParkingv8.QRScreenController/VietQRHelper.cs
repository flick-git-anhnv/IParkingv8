using System.Text;

namespace iParkingv8.Object.Objects.Payments
{
    public static class VietQRHelper
    {
        private static string Pad2(string value)
        {
            return value.Length < 2 ? "0" + value : value;
        }

        private static string FormatEMV(string id, string value)
        {
            return id + Pad2(value.Length.ToString()) + value;
        }

        public static string GenerateVietQRString(string bankCode, string accountNumber, string amount = "", string description = "")
        {
            bankCode = bankCode.Trim().ToUpper();
            accountNumber = accountNumber.Trim();
            amount = amount.Trim();
            description = description.Trim();

            if (string.IsNullOrWhiteSpace(bankCode) || string.IsNullOrWhiteSpace(accountNumber))
                return null;

            string payloadFormat = FormatEMV("00", "01");
            string pointOfInitiation = FormatEMV("01", string.IsNullOrEmpty(amount) ? "11" : "12");

            string guid = FormatEMV("00", "A000000727");
            string accInfo = FormatEMV("00", bankCode);
            string stk = FormatEMV("01", accountNumber);
            string tk = FormatEMV("01", accInfo + stk);
            string qrIBFTTA = FormatEMV("02", "QRIBFTTA");
            string merchantAccount = FormatEMV("38", guid + tk + qrIBFTTA);

            string countryCode = FormatEMV("58", "VN");
            string currency = FormatEMV("53", "704");
            string transAmount = string.IsNullOrEmpty(amount) ? "" : FormatEMV("54", amount);
            string addInfo = string.IsNullOrEmpty(description) ? "" : FormatEMV("62", FormatEMV("08", description));

            string qrData = payloadFormat + pointOfInitiation + merchantAccount + currency + transAmount + countryCode + addInfo + "6304";

            string crc = ComputeCRC16CCITT(qrData);
            return qrData + crc;
        }

        private static string ComputeCRC16CCITT(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            ushort crc = 0xFFFF;

            foreach (byte b in bytes)
            {
                crc ^= (ushort)(b << 8);
                for (int i = 0; i < 8; i++)
                {
                    crc = (ushort)((crc & 0x8000) != 0 ? ((crc << 1) ^ 0x1021) : (crc << 1));
                }
            }

            return crc.ToString("X4");
        }
    }

}
