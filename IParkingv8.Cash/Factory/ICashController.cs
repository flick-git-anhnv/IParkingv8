using IParkingv8.Cash.Events;

namespace IParkingv8.Cash.Factory
{
    public interface ICashController
    {
        //bool Connect(string comport, int baudrate, int communicationType);
        //bool Disconnect();
        //// CardEvent 
        //event CardEventHandler? CardEvent;
        //// InputEvent 
        //event InputEventHandler? InputEvent;
        Task<bool> Connect();
        //Task<bool> DisConnect();

        void PollingStart();
        Task PollingStop();
        bool RejectMoney();

        event PollCBA9EventHandle PollEvent;
    }
}
