using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Futech.Socket_v2
{
    public class SocketClient
    {
        private AsyncCallback receiveCallBack;
        private Socket clientSocket;
        private string serverIP = "127.0.0.1";
        private int serverPort = 50000;
        private bool isRunning;
        public event ServerConnectedEventHandler ServerConnected;
        public event SocketErrorEventHandler SocketErrorEvent;
        public event DataReceivedInStringEventHandler DataReceivedInStringEvent;
        public event DataReceivedInByteArrayEventHandler DataReceivedInByteArrayEvent;
        public string ServerIP
        {
            get
            {
                return this.serverIP;
            }
            set
            {
                this.serverIP = value;
            }
        }
        public int ServerPort
        {
            get
            {
                return this.serverPort;
            }
            set
            {
                this.serverPort = value;
            }
        }
        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
            set
            {
                this.isRunning = value;
            }
        }
        public SocketClient()
        {
        }
        public SocketClient(string serverIP, int serverPort)
        {
            this.serverIP = serverIP;
            this.serverPort = serverPort;
        }
        public bool Connect()
        {
            try
            {
                this.clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress address = IPAddress.Parse(this.serverIP);
                IPEndPoint remoteEP = new IPEndPoint(address, this.serverPort);
                this.clientSocket.Connect(remoteEP);
                if (this.clientSocket.Connected)
                {
                    if (this.ServerConnected != null)
                    {
                        this.ServerConnected(this.serverIP, this.serverPort);
                    }
                    this.isRunning = true;
                    this.WaitForData();
                    return true;
                }
            }
            catch (SocketException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Connection failed, is the server running?\n" + ex.Message);
                }
            }
            catch (Exception ex2)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while connect to server." + ex2.Message);
                }
            }
            return false;
        }
        private void WaitForData()
        {
            try
            {
                if (this.receiveCallBack == null)
                {
                    this.receiveCallBack = new AsyncCallback(this.OnDataReceived);
                }
                SocketPacket socketPacket = new SocketPacket();
                if (this.clientSocket != null)
                {
                    socketPacket.clientSocket = this.clientSocket;
                    if(socketPacket.dataBuffer != null)
                        this.clientSocket.BeginReceive(socketPacket.dataBuffer, 0, socketPacket.dataBuffer.Length, SocketFlags.None, this.receiveCallBack, socketPacket);
                }
            }
            catch(ObjectDisposedException ode)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Object Dispose:" + ode.Message);
                }
            }
            catch (SocketException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while wait to data from server." + ex.Message);
                }
            }
        }
        private void OnDataReceived(IAsyncResult asynResult)
        {
            try
            {
                SocketPacket socketPacket = (SocketPacket)asynResult.AsyncState;
                if (socketPacket.clientSocket != null && socketPacket.clientSocket.Connected)
                {
                    int num = socketPacket.clientSocket.EndReceive(asynResult);
                    if (num > 0)
                    {
                        if (this.DataReceivedInStringEvent != null)
                        {
                            string @string = Encoding.UTF8.GetString(socketPacket.dataBuffer, 0, num);
                            this.DataReceivedInStringEvent(@string);
                        }
                        if (this.DataReceivedInByteArrayEvent != null)
                        {
                            byte[] array = new byte[num];
                            Array.Copy(socketPacket.dataBuffer, 0, array, 0, num);
                            this.DataReceivedInByteArrayEvent(array, num, this.serverIP);
                        }
                    }
                    this.WaitForData();
                }
            }
            catch (SocketException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while receiving data from server." + ex.Message);
                }
            }
        }
        public void SendMessage(string message)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(message);
                if (this.clientSocket != null && this.clientSocket.Connected)
                {
                    this.clientSocket.Send(bytes);
                }
            }
            catch (SocketException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while sending data to server." + ex.Message);
                }
            }
        }
        public void SendMessage(byte[] message)
        {
            try
            {
                if (this.clientSocket != null && this.clientSocket.Connected)
                {
                    this.clientSocket.Send(message);
                }
            }
            catch (SocketException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while sending data to server." + ex.Message);
                }
            }
            finally
            {
                message = null;
            }
        }
        public void SendFile(string fileName)
        {
            try
            {
                string str = "";
                fileName = fileName.Replace("\\", "/");
                while (fileName.IndexOf("/") > -1)
                {
                    str += fileName.Substring(0, fileName.IndexOf("/") + 1);
                    fileName = fileName.Substring(fileName.IndexOf("/") + 1);
                }
                byte[] bytes = Encoding.UTF8.GetBytes(fileName);
                byte[] bytes2 = BitConverter.GetBytes(bytes.Length);
                byte[] array = File.ReadAllBytes(str + fileName);
                byte[] array2 = new byte[4 + bytes.Length + array.Length];
                bytes2.CopyTo(array2, 0);
                bytes.CopyTo(array2, 4);
                array.CopyTo(array2, 4 + bytes.Length);
                this.SendMessage(array2);
            }
            catch (Exception ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while sending file to server." + ex.Message);
                }
            }
        }
        public void SendFile(string fileName, byte[] fileData)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(fileName);
                byte[] bytes2 = BitConverter.GetBytes(bytes.Length);
                byte[] array = new byte[4 + bytes.Length + fileData.Length];
                bytes2.CopyTo(array, 0);
                bytes.CopyTo(array, 4);
                fileData.CopyTo(array, 4 + bytes.Length);
                this.SendMessage(array);
            }
            catch (Exception ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while sending file to server." + ex.Message);
                }
            }
        }

        public void SendByte(byte[] byteArray)
        {
            try
            {
                if (this.clientSocket != null && this.clientSocket.Connected)
                {
                    this.clientSocket.Send(byteArray, byteArray.Length, 0);
                }
            }
            catch (SocketException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while sending data to server." + ex.Message);
                }
            }
            finally
            {
                byteArray = null;
            }
        }

        public void SaveFile(string receivedPath, byte[] clientData, int receivedBytesLen, ref string fileName)
        {
            try
            {
                int num = BitConverter.ToInt32(clientData, 0);
                fileName = Encoding.UTF8.GetString(clientData, 4, num);
                BinaryWriter binaryWriter = new BinaryWriter(File.Open(receivedPath + "/" + fileName, FileMode.Append));
                binaryWriter.Write(clientData, 4 + num, receivedBytesLen - 4 - num);
                binaryWriter.Close();
            }
            catch (Exception ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while saving file." + ex.Message);
                }
            }
        }
        public bool Disconnect()
        {
            try
            {
                this.isRunning = false;
                if (this.clientSocket != null)
                {
                    this.clientSocket.Close();
                    this.clientSocket = null;
                }
                return true;
            }
            catch (Exception ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while disconnect to server." + ex.Message);
                }
            }
            return false;
        }
    }
}
