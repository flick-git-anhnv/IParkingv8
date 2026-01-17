using System;
using System.Net.Sockets;

namespace Futech.Socket_v2
{
    public class SocketPacket
    {
        public static int maxDataLength = 10485760;
        public Socket clientSocket;
        public int clientNumber;
        public byte[] dataBuffer = new byte[SocketPacket.maxDataLength];
        public SocketPacket()
        {
            this.dataBuffer = new byte[SocketPacket.maxDataLength];
        }
        public SocketPacket(Socket clientSocket, int clientNumber)
        {
            this.clientSocket = clientSocket;
            this.clientNumber = clientNumber;
            this.dataBuffer = new byte[SocketPacket.maxDataLength];
        }
    }
}
