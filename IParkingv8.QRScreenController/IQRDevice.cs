using System.IO.Ports;

namespace IParkingv8.QRScreenController
{
    public interface IQRDevice
    {
        SerialPort serialPort { get; set; }
        Task<bool> DisplayQRAsync(string bankName, string accountNumber, long money, string description);
        Task<bool> OpenAsync();
        Task<bool> CloseAsync();
        Task<bool> OpenHomePageAsync();
    }
}
