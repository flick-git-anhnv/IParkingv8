using Futech.DisplayLED.SocketHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Futech.DisplayLED.Viettel
{
    public class ViettelLedhelper
    {
        #region: Properties
        private Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public string IpAddress { get; set; } = string.Empty;
        public int Port { get; set; } = 100;
        private bool isMultipleColor = false;
        #endregion: End Properties


        public ViettelLedhelper(bool isMultipleColor)
        {
            this.isMultipleColor = isMultipleColor;
        }
        #region: Connect
        public bool Connect(string ip, int port, bool isMultipleColor)
        {
            this.IpAddress = ip;
            this.Port = port;
            this.isMultipleColor = isMultipleColor;
            _clientSocket.Connect(ip, port);
            return _clientSocket.Connected;
        }

        public bool Disconnect(string ip, int port)
        {
            return true;
        }
        #endregion: End Connect

        #region: Network Infor
        public void SetDisplay(string data, int row, int color)
        {
            string cmd = createCMD(row, color, data);
            //MessageBox.Show("SEND IP" + this.IpAddress + " - " + cmd);
            byte[] buffer = Encoding.UTF8.GetBytes(cmd);
            _clientSocket.Send(buffer);

            ////MessageBox.Show("SEND IP" + this.IpAddress + " - " + cmd);
            //string response = UdpTools.ExecuteCommand_Ascii(this.IpAddress, this.Port, cmd);
        }
        #endregion: End Network Infor

        private string createCMD(int row, int color, string data)
        {
            if (this.isMultipleColor)
                return $"*[H{row}][C{color}]{data}[!]";
            else
                return $"*[H{row}]{data}[!]";
        }

    }
}
