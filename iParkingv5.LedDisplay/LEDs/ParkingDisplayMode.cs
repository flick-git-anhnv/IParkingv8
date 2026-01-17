using System;

namespace iParkingv5.LedDisplay.LEDs
{
    public class ParkingData
    {
        public string CardNumber { get; set; } = string.Empty;
        public string CardNo { get; set; } = string.Empty;
        public string CardType { get; set; } = string.Empty;
        public string EventStatus { get; set; } = string.Empty;
        public string Plate { get; set; } = string.Empty;
        public DateTime? DatetimeIn { get; set; }
        public DateTime? DatetimeOut { get; set; }
        public string Money { get; set; }

        public ParkingData() : this("", "", "", "", "", DateTime.Now, null, "0")
        {

        }
        public ParkingData(string cardNumber, string cardNo, string cardType, string eventStatus, string plate, DateTime? datetimeIn, DateTime? datetimeOut, string money)
        {
            CardNumber = cardNumber;
            CardNo = cardNo;
            CardType = cardType;
            EventStatus = eventStatus;
            Plate = plate;
            DatetimeIn = datetimeIn;
            DatetimeOut = datetimeOut;
            Money = money;
        }
        public ParkingData(string plate, string cardType, string eventStatus, DateTime dateTimeIn)
            : this("", "", cardType, eventStatus, plate, dateTimeIn, null, "")
        {
        }
    }
}
