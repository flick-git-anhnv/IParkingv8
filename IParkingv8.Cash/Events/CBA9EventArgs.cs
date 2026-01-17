using IParkingv8.Cash.Model;

namespace IParkingv8.Cash.Events
{
    public delegate void ConnectCBA9EventHandle(bool isConnect);

    public delegate Task PollCBA9EventHandle(object sender, CashResult cashResult);
    
}
