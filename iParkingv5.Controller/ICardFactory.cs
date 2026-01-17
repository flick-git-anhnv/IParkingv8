using System;
using System.Collections.Generic;
using System.Text;

namespace iParkingv5.Controller
{
    public interface ICardFactory
    {
        string GetCardHexNumber(string cardNumber);
        int CardLen();
    }
}
