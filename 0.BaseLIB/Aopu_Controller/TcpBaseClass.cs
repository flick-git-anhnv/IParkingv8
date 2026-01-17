using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpipIntface.Code.Client
{
    public class TcpBaseClass
    {
        public Boolean IsconnectSuccess = false; //Asynchronous connection status, set by the asynchronous connection callback function
        public int StartTick = 0;

        protected byte[] BufferRX = new byte[1024];
        protected byte[] BufferTX = new byte[1024];

        protected byte isHeartTime = 0;
        protected String SockErrorStr = null;

        #region Delegate event declaration
        public delegate void delSocketDisconnected();
        public  event delSocketDisconnected OnDisconnected;

        public delegate void TOnRxTxDataHandler(byte rt, byte[] buffRX, int len);   //Declare delegation
        public event TOnRxTxDataHandler OnRxTxDataEvent;        //Declare event  
        #endregion

        virtual public void SetTcpTick()
        {
        }

        virtual public bool OpenIP(string ip, int port)
        {
            return false;
        }

        virtual public bool CloseTcpip()
        {
            return false;
        }

        protected void DoOnRxTxDataEvent(byte rt, byte[] buffRX, int len)
        {
            if (OnRxTxDataEvent != null)
            {
                OnRxTxDataEvent(rt, buffRX, len);
            }
        }

        virtual public byte DoSendData(byte[] buffTX, int WriteNum)
        {
            return 0;
        }
        protected void DosocketDisconnected()
        {
            if (OnDisconnected != null)
            {
                OnDisconnected();
            }
        }
    }
}
