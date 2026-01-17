using System;

namespace Kztek.Scale
{
    public interface IScale
    {
        event ScaleEventHandler ScaleEvent;
        event ErrorEventHandler ErrorEvent;
        event DataReceivedEventHandler DataReceivedEvent;

        string ComPort
        {
            get;
            set;
        }
        int BaudRate
        {
            get;
            set;
        }
        int ReceivedTimeOut
        {
            get;
            set;
        }
        int DataBits
        {
            get;
            set;
        }
        int Parity
        {
            get;
            set;
        }
        int StopBits
        {
            get;
            set;
        }
        bool IsConnected
        {
            get;
        }

        bool IsStable
        {
            get;
            set;
        }
        bool Connect();
        bool Connect(string comPort, int baudRate);
        bool Disconnect();
        void PollingStart();
        void SignalToStop();
        void PollingStop();
        bool TestConnection();


    }
}
