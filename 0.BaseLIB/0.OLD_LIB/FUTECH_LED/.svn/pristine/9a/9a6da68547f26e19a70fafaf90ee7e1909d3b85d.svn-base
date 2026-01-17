using System;
using System.Threading;								// Sleeping
using System.Net;									// Used to local machine info
using System.Net.Sockets;							// Socket namespace
using System.Collections;							// Access to the Array list
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace Futech.Socket_v2
{
    public class SocketServer
    {
        private Socket serverSocket;
        private AsyncCallback receiveCallback;
        private ArrayList clientSockets = ArrayList.Synchronized(new ArrayList());
        private int clientCount;
        private string serverIP = "127.0.0.1";
        private int serverPort = 50000;
        private bool isRunning;
        public event SocketErrorEventHandler SocketErrorEvent;
        public event ClientConnectedEventHandler ClientConnectedEvent;
        public event ClientDisconnectedEventHandler ClientDisconnectedEvent;
        public event DataReceivedInStringEventHandler DataReceivedInStringEvent;
        public event DataReceivedInByteArrayEventHandler DataReceivedInByteArrayEvent;
        public event DataReceivedEventHandler DataReceivedEvent;
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
        public int ClientCount
        {
            get
            {
                return this.clientCount;
            }
        }
        public SocketServer()
        {
        }
        public SocketServer(string serverIP, int serverPort)
        {
            this.serverIP = serverIP;
            this.serverPort = serverPort;
        }
        public void StartListen()
        {
            try
            {
                if (clientSockets == null) clientSockets = ArrayList.Synchronized(new ArrayList());

                this.clientCount = 0;
                this.serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint localEP = new IPEndPoint(IPAddress.Any, this.serverPort);
                this.serverSocket.Bind(localEP);
                this.serverSocket.Listen(4);
                this.serverSocket.BeginAccept(new AsyncCallback(this.OnClientConnect), null);
                this.isRunning = true;
            }
            catch (SocketException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Socket Exception - start Listen: " + ex.Message);
                }
            }
            catch (Exception ex1)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while start Listen:" + ex1.Message);
                }
            }
        }
        private void OnClientConnect(IAsyncResult asyncResult)
        {
            try
            {
                if (this.serverSocket != null && asyncResult != null)
                {
                    Socket socket = this.serverSocket.EndAccept(asyncResult);
                    if (this.clientSockets != null && socket != null)
                    {
                        if (!this.clientSockets.Contains(socket))
                        {
                            this.clientSockets.Add(socket);

                            Interlocked.Increment(ref this.clientCount);

                            this.WaitForData(socket, this.clientCount);

                            if (socket != null && socket.Connected)
                            {
                                if (this.ClientConnectedEvent != null)
                                {
                                    this.ClientConnectedEvent(this.clientCount, socket.RemoteEndPoint.ToString());
                                }
                            }
                            this.serverSocket.BeginAccept(new AsyncCallback(this.OnClientConnect), null);
                        }
                    }
                    else
                    {
                        if (socket == null) socket.Dispose();
                    }
                }
            }
            catch (ObjectDisposedException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("OnClientConnect - ObjectDisposedException: " + ex.Message);
                }
            }
            catch (SocketException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("OnClientConnect: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("OnClientConnect: " + ex.Message);
                }
            }
        }

        private void WaitForData(Socket clientSocket, int clientNumber)
        {
            bool isRemoveClient = false;
            try
            {
                if (clientSocket != null)
                {
                    if (this.receiveCallback == null)
                    {
                        this.receiveCallback = new AsyncCallback(this.OnDataReceived);
                    }

                    if (clientSocket != null && clientSocket.Connected)
                    {
                        SocketPacket socketPacket = new SocketPacket(clientSocket, clientNumber);
                        if (socketPacket != null && clientSocket != null && clientSocket.Connected)
                            clientSocket.BeginReceive(socketPacket.dataBuffer, 0, socketPacket.dataBuffer.Length, SocketFlags.None, this.receiveCallback, socketPacket);
                    }
                }
                else
                {
                    if (this.SocketErrorEvent != null)
                    {
                        this.SocketErrorEvent("WaitForData - Socket Client NULL");
                        isRemoveClient = true;
                    }
                }
            }
            catch (ObjectDisposedException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("WaitForData - ObjectDisposedException: " + ex.Message);
                }
            }
            catch (SocketException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("WaitForData - SocketException :" + ex.Message);

                    if (ex.ToString().Contains("connection was forcibly close") || ex.ToString().Contains("connection was aborted") || ex.ToString().Contains("party did not properly respond after a period of time"))
                    {
                        if (this.ClientDisconnectedEvent != null && clientSocket != null)
                        {
                            this.ClientDisconnectedEvent(clientNumber, clientSocket.RemoteEndPoint.ToString());
                        }
                        isRemoveClient = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("WaitForData - SocketException: " + ex.Message);

                    if (ex.ToString().Contains("connection was forcibly close") || ex.ToString().Contains("connection was aborted") || ex.ToString().Contains("party did not properly respond after a period of time"))
                    {
                        if (this.ClientDisconnectedEvent != null && clientSocket != null)
                        {
                            this.ClientDisconnectedEvent(clientNumber, clientSocket.RemoteEndPoint.ToString());
                        }

                        isRemoveClient = true;
                    }
                }
            }
            finally
            {
                if (isRemoveClient)
                {
                    if (clientSockets.Contains(clientSocket))
                    {
                        if (this.clientCount >= 1)
                            Interlocked.Decrement(ref this.clientCount);
                        this.clientSockets.Remove(clientSocket);
                        //clientSocket.Dispose();
                        clientSocket = null;
                    }
                }
                GC.Collect();
            }
        }
        private void OnDataReceived(IAsyncResult asynResult)
        {
            Thread.Sleep(1);
            bool isRemoveClient = false;
            if (asynResult != null)
            {
                SocketPacket socketPacket = (SocketPacket)asynResult.AsyncState;

                try
                {
                    if (socketPacket != null && socketPacket.clientSocket != null)
                    {
                        int num = socketPacket.clientSocket.EndReceive(asynResult);
                        string @string = "";
                        if (num > 0)
                        {
                            @string = Encoding.UTF8.GetString(socketPacket.dataBuffer);
                            if (this.DataReceivedInStringEvent != null)
                            {
                                this.DataReceivedInStringEvent(@string);
                            }
                            if (this.DataReceivedInByteArrayEvent != null)
                            {
                                this.DataReceivedInByteArrayEvent(socketPacket.dataBuffer, num, socketPacket.clientSocket.RemoteEndPoint.ToString());
                            }
                            if (this.DataReceivedEvent != null)
                            {
                                this.DataReceivedEvent(@string, socketPacket.clientSocket.RemoteEndPoint.ToString());
                            }
                        }
                        if ((!@string.ToUpper().Contains("CLIENT STOP")) && socketPacket != null && socketPacket.clientSocket != null)
                        {
                            this.WaitForData(socketPacket.clientSocket, socketPacket.clientNumber);
                        }
                        else
                        {
                            isRemoveClient = true;
                        }
                        @string = null;
                    }
                }
                catch (ObjectDisposedException ex)
                {
                    if (this.SocketErrorEvent != null)
                    {
                        this.SocketErrorEvent("OnDataReceived - ObjectDisposedException: " + ex.Message);
                    }
                    isRemoveClient = true;
                }
                catch (SocketException ex2)
                {
                    if (ex2.ErrorCode == 10054)
                    {
                        string errorString = "OndataReceive - SocketException 10054: Client " + socketPacket.clientNumber + " disconnected\n";
                        if (this.SocketErrorEvent != null)
                        {
                            this.SocketErrorEvent(errorString);
                        }
                        if (this.clientSockets != null && socketPacket != null && this.clientSockets.Count > 0 && this.clientSockets.Count > (socketPacket.clientNumber - 1) && this.clientSockets[socketPacket.clientNumber - 1] != null)
                        {
                            if (this.ClientDisconnectedEvent != null)
                                this.ClientDisconnectedEvent(socketPacket.clientNumber, ((Socket)(this.clientSockets[socketPacket.clientNumber - 1])).RemoteEndPoint.ToString());

                        }
                        isRemoveClient = true;
                    }
                    else
                    {
                        if (this.SocketErrorEvent != null)
                        {
                            this.SocketErrorEvent("OndataReceive - SocketException: " + ex2.Message);
                        }

                        if (ex2.ToString().Contains("connection was forcibly close") || ex2.ToString().Contains("connection was aborted") || ex2.ToString().Contains("party did not properly respond after a period of time"))
                        {
                            if (this.ClientDisconnectedEvent != null)
                            {
                                this.ClientDisconnectedEvent(socketPacket.clientNumber, ((Socket)(this.clientSockets[socketPacket.clientNumber - 1])).RemoteEndPoint.ToString());
                            }
                            isRemoveClient = true;
                        }

                        GC.Collect();
                    }
                }
                catch (Exception ex3)
                {
                    if (this.SocketErrorEvent != null)
                    {
                        this.SocketErrorEvent("OndataReceive - Exception: " + ex3.Message);
                    }

                    if (ex3.ToString().Contains("connection was forcibly close") || ex3.ToString().Contains("connection was aborted") || ex3.ToString().Contains("party did not properly respond after a period of time"))
                    {
                        if (this.ClientDisconnectedEvent != null)
                        {
                            this.ClientDisconnectedEvent(socketPacket.clientNumber, ((Socket)(this.clientSockets[socketPacket.clientNumber - 1])).RemoteEndPoint.ToString());
                        }
                        isRemoveClient = true;
                    }
                    GC.Collect();
                }
                finally
                {
                    if (isRemoveClient && socketPacket != null)
                    {
                        if (clientSockets.Contains(socketPacket.clientSocket))
                        {
                            this.clientSockets.Remove(socketPacket.clientSocket);
                            socketPacket.clientSocket = null;
                            if (this.clientCount >= 1)
                                Interlocked.Decrement(ref this.clientCount);
                            socketPacket = null;
                        }
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
        }
        public bool RemoveClient(string clientIP)
        {
            bool ret = false;
            try
            {
                if (clientIP != "")
                {
                    int clientIndex = -1;
                    for (int i = 0; i < this.clientSockets.Count; i++)
                    {
                        if (this.clientSockets[i] != null)
                        {
                            if (((Socket)this.clientSockets[i]).RemoteEndPoint.ToString() == clientIP)
                            {
                                clientIndex = i;

                                break;
                            }
                        }
                    }
                    if (clientIndex >= 0)
                    {
                        Socket sk = (Socket)this.clientSockets[clientIndex];
                        if (this.clientSockets.Contains(sk))
                        {
                            this.clientSockets.Remove(sk);
                            sk.Dispose();
                            sk = null;
                            GC.Collect();
                            if (this.clientCount >= 1)
                                Interlocked.Decrement(ref this.clientCount);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while remove client " + clientIP + " from list." + ex.Message);
                }
            }
            return ret;
        }

        public bool RemoveClient(int clientIndex)
        {
            bool ret = false;
            try
            {
                if (clientIndex >= 0 && this.clientSockets != null && this.clientSockets.Count > clientIndex)
                {
                    Socket sk = (Socket)this.clientSockets[clientIndex];
                    if (this.clientSockets.Contains(sk))
                    {
                        this.clientSockets.Remove(sk);
                        sk.Dispose();
                        sk = null;
                        GC.Collect();
                        if (this.clientCount >= 1)
                            Interlocked.Decrement(ref this.clientCount);
                    }
                }
            }
            catch (Exception ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while remove client [" + clientIndex + "] from list." + ex.Message);
                }
            }
            return ret;
        }

        public bool RemoveAllClient()
        {
            bool ret = false;
            try
            {
                for (int i = 0; i < this.clientSockets.Count; i++)
                {
                    if (i >= 0)
                    {
                        Socket sk = (Socket)this.clientSockets[i];
                        if (this.clientSockets.Contains(sk))
                        {
                            this.clientSockets.Remove(sk);
                            sk.Dispose();
                            sk = null;
                            GC.Collect();
                            if (this.clientCount >= 1)
                                Interlocked.Decrement(ref this.clientCount);
                            System.Threading.Thread.Sleep(10);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while remove all client." + ex.Message);
                }
            }
            return ret;
        }

        public List<string> GetListClient()
        {
            List<string> ret = new List<string>();
            foreach (Socket socket in this.clientSockets)
            {
                if (socket != null && socket.Connected)
                {
                    ret.Add(socket.RemoteEndPoint.ToString());
                }
            }

            if (ret.Count > 0) return ret;
            return null;
        }

        public void SendMessage(string message)
        {
            try
            {
                if (this.clientSockets != null)
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(message);
                    for (int i = 0; i < this.clientSockets.Count; i++)
                    {
                        try
                        {
                            if (this.clientSockets[i] != null && ((Socket)this.clientSockets[i]).Connected)
                            {
                                ((Socket)this.clientSockets[i]).Send(bytes);
                            }
                        }
                        catch (SocketException exS)
                        {
                            if (exS.ToString().Contains("connection was forcibly close") || exS.ToString().Contains("connection was aborted"))
                            {
                                if (this.ClientDisconnectedEvent != null)
                                {
                                    this.ClientDisconnectedEvent(i, ((Socket)this.clientSockets[i]).RemoteEndPoint.ToString());
                                }

                                Socket sk = (Socket)this.clientSockets[i];
                                if (this.clientSockets.Contains(sk))
                                {
                                    this.clientSockets.Remove(sk);
                                    sk.Dispose();
                                    sk = null;
                                    if (this.clientCount >= 1)
                                        Interlocked.Decrement(ref this.clientCount);
                                    i -= 1;
                                }
                            }

                            if (this.SocketErrorEvent != null)
                            {
                                this.SocketErrorEvent("Error while send message to client." + exS.ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            if (ex.ToString().Contains("connection was forcibly close") || ex.ToString().Contains("connection was aborted"))
                            {
                                this.clientSockets.RemoveAt(i);
                                if (this.clientCount >= 1)
                                    Interlocked.Decrement(ref this.clientCount);
                                i -= 1;
                            }
                            if (this.SocketErrorEvent != null)
                            {
                                this.SocketErrorEvent("Error while send message to client." + ex.ToString());
                            }
                        }
                    }
                }
            }
            catch (SocketException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while send message to client." + ex.Message);
                }
            }
        }
        public void SendMessage(string message, string clientIP)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(message);
                foreach (Socket socket in this.clientSockets)
                {
                    if (socket != null && socket.Connected && socket.RemoteEndPoint.ToString().Contains(clientIP))
                    {
                        socket.Send(bytes);
                    }
                }
            }
            catch (SocketException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while send message to client." + ex.Message);
                }
            }
        }
        public void SendMessage(byte[] message)
        {
            try
            {
                foreach (Socket socket in this.clientSockets)
                {
                    if (socket != null && socket.Connected)
                    {
                        socket.Send(message);
                    }
                }
            }
            catch (SocketException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while send message to client." + ex.Message);
                }
            }
            finally
            {
                message = null;
            }
        }
        public void SendMessage(byte[] message, string clientIP)
        {
            try
            {
                foreach (Socket socket in this.clientSockets)
                {
                    if (socket != null && socket.Connected && socket.RemoteEndPoint.ToString().Contains(clientIP))
                    {
                        socket.Send(message);
                    }
                }
            }
            catch (SocketException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while send message to client." + ex.Message);
                }
            }
            finally
            {
                message = null;
            }
        }
        public void SendMessage(string message, int clientNumber)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(message);
                int count = this.clientSockets.Count;
                if (count > 0 && count > clientNumber && clientNumber >= 0)
                {
                    Socket socket = (Socket)this.clientSockets[clientNumber];
                    if (socket != null && socket.Connected)
                        socket.Send(bytes);
                    else
                    {
                        if (this.clientSockets.Contains(socket))
                        {
                            this.clientSockets.Remove(socket);
                            socket.Dispose();
                            socket = null;
                            if (this.clientCount >= 1)
                                Interlocked.Decrement(ref this.clientCount);
                        }
                    }
                }
            }
            catch (SocketException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent(string.Concat(new object[]
                    {
                        "Error while send message to client: ",
                        clientNumber,
                        "\n",
                        ex.Message
                    }));
                }
            }
        }
        public void SendMessage(byte[] message, int clientNumber)
        {
            try
            {
                int count = this.clientSockets.Count;
                if (count > 0 && count > clientNumber && clientNumber >= 0)
                {
                    Socket socket = (Socket)this.clientSockets[clientNumber];
                    if (socket != null && socket.Connected)
                    {
                        socket.Send(message);
                    }
                }
            }
            catch (SocketException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent(string.Concat(new object[]
                    {
                        "Error while send message to client: ",
                        clientNumber,
                        "\n",
                        ex.Message
                    }));
                }
            }
        }
        public void SendFile(string fileName)
        {
            try
            {
                string str = "";
                while (fileName.IndexOf("\\") > -1)
                {
                    str += fileName.Substring(0, fileName.IndexOf("\\") + 1);
                    fileName = fileName.Substring(fileName.IndexOf("\\") + 1);
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
                    this.SocketErrorEvent("Error while sending file to client." + ex.Message);
                }
            }
        }
        public void SendFile(string fileName, string clientIP)
        {
            try
            {
                string str = "";
                while (fileName.IndexOf("\\") > -1)
                {
                    str += fileName.Substring(0, fileName.IndexOf("\\") + 1);
                    fileName = fileName.Substring(fileName.IndexOf("\\") + 1);
                }
                byte[] bytes = Encoding.UTF8.GetBytes(fileName);
                byte[] bytes2 = BitConverter.GetBytes(bytes.Length);
                byte[] array = File.ReadAllBytes(str + fileName);
                byte[] array2 = new byte[4 + bytes.Length + array.Length];
                bytes2.CopyTo(array2, 0);
                bytes.CopyTo(array2, 4);
                array.CopyTo(array2, 4 + bytes.Length);
                this.SendMessage(array2, clientIP);
            }
            catch (Exception ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while sending file to client." + ex.Message);
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
                    this.SocketErrorEvent("Error while sending file to client." + ex.Message);
                }
            }
        }
        public void SendFile(string fileName, byte[] fileData, string clientIP)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(fileName);
                byte[] bytes2 = BitConverter.GetBytes(bytes.Length);
                byte[] array = new byte[4 + bytes.Length + fileData.Length];
                bytes2.CopyTo(array, 0);
                bytes.CopyTo(array, 4);
                fileData.CopyTo(array, 4 + bytes.Length);
                this.SendMessage(array, clientIP);
            }
            catch (Exception ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while sending file to client." + ex.Message);
                }
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
                    this.SocketErrorEvent("Error while saving file data." + ex.Message);
                }
            }
        }
        public void StopListen()
        {
            this.CloseSockets();
        }
        private void CloseSockets()
        {
            this.isRunning = false;
            if (this.serverSocket != null)
            {
                SendMessage("CLOSE_CONNECTION");
                for (int i = 0; i < 10; i++)
                {
                    System.Threading.Thread.Sleep(100);
                    RemoveAllClient();
                }
                this.clientSockets.Clear();
                this.serverSocket.Close();
                this.serverSocket.Dispose();
                this.serverSocket = null;
                //clientSockets = null;
            }
            GC.Collect();
        }
        public static string GetFileType(string path)
        {
            string text = SocketServer.GetHeaderInfo(path).ToUpper();
            if (text.StartsWith("FFD8FFE0"))
            {
                return "JPG";
            }
            if (text.StartsWith("49492A"))
            {
                return "TIFF";
            }
            if (text.StartsWith("424D"))
            {
                return "BMP";
            }
            if (text.StartsWith("474946"))
            {
                return "GIF";
            }
            if (text.StartsWith("89504E470D0A1A0A"))
            {
                return "PNG";
            }
            return "";
        }
        public static string GetHeaderInfo(string path)
        {
            if (File.Exists(path))
            {
                byte[] array = new byte[8];
                BinaryReader binaryReader = new BinaryReader(new FileStream(path, FileMode.Open));
                binaryReader.Read(array, 0, array.Length);
                binaryReader.Close();
                StringBuilder stringBuilder = new StringBuilder();
                byte[] array2 = array;
                for (int i = 0; i < array2.Length; i++)
                {
                    byte b = array2[i];
                    stringBuilder.Append(b.ToString("X2"));
                }
                return stringBuilder.ToString();
            }
            return "";
        }
        public static string GetFileType(byte[] data)
        {
            string text = SocketServer.GetHeaderInfo(data).ToUpper();
            if (text.StartsWith("FFD8FFE0"))
            {
                return "JPG";
            }
            if (text.StartsWith("49492A"))
            {
                return "TIFF";
            }
            if (text.StartsWith("424D"))
            {
                return "BMP";
            }
            if (text.StartsWith("474946"))
            {
                return "GIF";
            }
            if (text.StartsWith("89504E470D0A1A0A"))
            {
                return "PNG";
            }
            return "";
        }
        public static string GetHeaderInfo(byte[] data)
        {
            if (data.Length > 8)
            {
                byte[] array = new byte[8];
                Array.Copy(data, array, 8);
                StringBuilder stringBuilder = new StringBuilder();
                byte[] array2 = array;
                for (int i = 0; i < array2.Length; i++)
                {
                    byte b = array2[i];
                    stringBuilder.Append(b.ToString("X2"));
                }
                return stringBuilder.ToString();
            }
            return "";
        }

    }
}

#region Backup
/*
using System;
using System.Threading;								// Sleeping
using System.Net;									// Used to local machine info
using System.Net.Sockets;							// Socket namespace
using System.Collections;							// Access to the Array list
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace Futech.Socket_v2
{
    public class SocketServer
    {
        private Socket serverSocket;
        private AsyncCallback receiveCallback;
        private ArrayList clientSockets = ArrayList.Synchronized(new ArrayList());
        private int clientCount;
        private string serverIP = "127.0.0.1";
        private int serverPort = 50000;
        private bool isRunning;
        public event SocketErrorEventHandler SocketErrorEvent;
        public event ClientConnectedEventHandler ClientConnectedEvent;
        public event ClientDisconnectedEventHandler ClientDisconnectedEvent;
        public event DataReceivedInStringEventHandler DataReceivedInStringEvent;
        public event DataReceivedInByteArrayEventHandler DataReceivedInByteArrayEvent;
        public event DataReceivedEventHandler DataReceivedEvent;
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
        public int ClientCount
        {
            get
            {
                return this.clientCount;
            }
        }
        public SocketServer()
        {
        }
        public SocketServer(string serverIP, int serverPort)
        {
            this.serverIP = serverIP;
            this.serverPort = serverPort;
        }
        public void StartListen()
        {
            try
            {
                if (clientSockets == null) clientSockets = ArrayList.Synchronized(new ArrayList());

                this.clientCount = 0;
                this.serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint localEP = new IPEndPoint(IPAddress.Any, this.serverPort);
                this.serverSocket.Bind(localEP);
                this.serverSocket.Listen(4);
                this.serverSocket.BeginAccept(new AsyncCallback(this.OnClientConnect), null);
                this.isRunning = true;
            }
            catch (SocketException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Socket Exception - start Listen: " + ex.Message);
                }
            }
            catch (Exception ex1)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while start Listen:" + ex1.Message);
                }
            }
        }
        private void OnClientConnect(IAsyncResult asyncResult)
        {
            try
            {
                if (this.serverSocket != null)
                {
                    Socket socket = this.serverSocket.EndAccept(asyncResult);
                    if (this.clientSockets != null && socket != null)
                    {
                        this.clientSockets.Add(socket);

                        Interlocked.Increment(ref this.clientCount);

                        this.WaitForData(socket, this.clientCount);
                        if (this.ClientConnectedEvent != null)
                        {
                            this.ClientConnectedEvent(this.clientCount, socket.RemoteEndPoint.ToString());
                        }
                        this.serverSocket.BeginAccept(new AsyncCallback(this.OnClientConnect), null);
                    }
                }
            }
            catch (ObjectDisposedException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("OnClientConnect - ObjectDisposedException: " + ex.Message);
                }
            }
            catch (SocketException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("OnClientConnect: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("OnClientConnect: " + ex.Message);
                }
            }
        }

        private void WaitForData(Socket clientSocket, int clientNumber)
        {
            bool isRemoveClient = false;
            try
            {
                if (this.receiveCallback == null)
                {
                    this.receiveCallback = new AsyncCallback(this.OnDataReceived);
                }
                if (clientSocket.Connected)
                {
                    SocketPacket socketPacket = new SocketPacket(clientSocket, clientNumber);
                    clientSocket.BeginReceive(socketPacket.dataBuffer, 0, socketPacket.dataBuffer.Length, SocketFlags.None, this.receiveCallback, socketPacket);
                }
            }
            catch (ObjectDisposedException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("WaitForData - ObjectDisposedException: " + ex.Message);
                }
            }
            catch (SocketException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("WaitForData - SocketException :" + ex.Message);

                    if (ex.ToString().Contains("connection was forcibly close") || ex.ToString().Contains("connection was aborted") || ex.ToString().Contains("party did not properly respond after a period of time"))
                    {
                        if (this.ClientDisconnectedEvent != null)
                        {
                            this.ClientDisconnectedEvent(clientNumber, clientSocket.RemoteEndPoint.ToString());
                        }

                        isRemoveClient = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("WaitForData - SocketException: " + ex.Message);

                    if (ex.ToString().Contains("connection was forcibly close") || ex.ToString().Contains("connection was aborted") || ex.ToString().Contains("party did not properly respond after a period of time"))
                    {
                        if (this.ClientDisconnectedEvent != null)
                        {
                            this.ClientDisconnectedEvent(clientNumber, clientSocket.RemoteEndPoint.ToString());
                        }

                        isRemoveClient = true;
                    }
                }
            }
            finally
            {
                if (isRemoveClient)
                {
                    if (clientSockets.Contains(clientSocket))
                    {
                        Interlocked.Decrement(ref this.clientCount);
                        this.clientSockets.Remove(clientSocket);
                        clientSocket.Dispose();
                        clientSocket = null;
                    }
                }
                GC.Collect();
            }
        }
        private void OnDataReceived(IAsyncResult asynResult)
        {
            Thread.Sleep(1);
            bool isRemoveClient = false;
            if (asynResult != null)
            {
                SocketPacket socketPacket = (SocketPacket)asynResult.AsyncState;

                try
                {
                    if (socketPacket != null && socketPacket.clientSocket != null)
                    {
                        int num = socketPacket.clientSocket.EndReceive(asynResult);
                        if (num > 0)
                        {
                            string @string = Encoding.UTF8.GetString(socketPacket.dataBuffer);
                            if (this.DataReceivedInStringEvent != null)
                            {
                                this.DataReceivedInStringEvent(@string);
                            }
                            if (this.DataReceivedInByteArrayEvent != null)
                            {
                                this.DataReceivedInByteArrayEvent(socketPacket.dataBuffer, num, socketPacket.clientSocket.RemoteEndPoint.ToString());
                            }
                            if (this.DataReceivedEvent != null)
                            {
                                this.DataReceivedEvent(@string, socketPacket.clientSocket.RemoteEndPoint.ToString());
                            }
                            @string = null;
                        }
                        this.WaitForData(socketPacket.clientSocket, socketPacket.clientNumber);
                    }
                }
                catch (ObjectDisposedException ex)
                {
                    if (this.SocketErrorEvent != null)
                    {
                        this.SocketErrorEvent("OnDataReceived - ObjectDisposedException: " + ex.Message);
                    }
                    isRemoveClient = true;
                }
                catch (SocketException ex2)
                {
                    if (ex2.ErrorCode == 10054)
                    {
                        string errorString = "OndataReceive - SocketException 10054: Client " + socketPacket.clientNumber + " disconnected\n";
                        if (this.SocketErrorEvent != null)
                        {
                            this.SocketErrorEvent(errorString);
                        }
                        if (this.clientSockets != null && socketPacket != null && this.clientSockets.Count > 0 && this.clientSockets.Count > (socketPacket.clientNumber - 1) && this.clientSockets[socketPacket.clientNumber - 1] != null)
                        {
                            if (this.ClientDisconnectedEvent != null)
                                this.ClientDisconnectedEvent(socketPacket.clientNumber, ((Socket)(this.clientSockets[socketPacket.clientNumber - 1])).RemoteEndPoint.ToString());

                        }
                        isRemoveClient = true;
                    }
                    else
                    {
                        if (this.SocketErrorEvent != null)
                        {
                            this.SocketErrorEvent("OndataReceive - SocketException: " + ex2.Message);
                        }

                        if (ex2.ToString().Contains("connection was forcibly close") || ex2.ToString().Contains("connection was aborted") || ex2.ToString().Contains("party did not properly respond after a period of time"))
                        {
                            if (this.ClientDisconnectedEvent != null)
                            {
                                this.ClientDisconnectedEvent(socketPacket.clientNumber, ((Socket)(this.clientSockets[socketPacket.clientNumber - 1])).RemoteEndPoint.ToString());
                            }
                            isRemoveClient = true;
                        }

                        GC.Collect();
                    }
                }
                catch (Exception ex3)
                {
                    if (this.SocketErrorEvent != null)
                    {
                        this.SocketErrorEvent("OndataReceive - Exception: " + ex3.Message);
                    }

                    if (ex3.ToString().Contains("connection was forcibly close") || ex3.ToString().Contains("connection was aborted") || ex3.ToString().Contains("party did not properly respond after a period of time"))
                    {
                        if (this.ClientDisconnectedEvent != null)
                        {
                            this.ClientDisconnectedEvent(socketPacket.clientNumber, ((Socket)(this.clientSockets[socketPacket.clientNumber - 1])).RemoteEndPoint.ToString());
                        }
                        isRemoveClient = true;
                    }
                    GC.Collect();
                }
                finally
                {
                    if (isRemoveClient && socketPacket != null)
                    {
                        if (clientSockets.Contains(socketPacket.clientSocket))
                        {
                            this.clientSockets.Remove(socketPacket.clientSocket);
                            socketPacket.clientSocket = null;
                            Interlocked.Decrement(ref this.clientCount);
                            socketPacket = null;
                        }
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
        }
        public bool RemoveClient(string clientIP)
        {
            bool ret = false;
            try
            {
                if (clientIP != "")
                {
                    int clientIndex = -1;
                    for (int i = 0; i < this.clientSockets.Count; i++)
                    {
                        if (this.clientSockets[i] != null)
                        {
                            if (((Socket)this.clientSockets[i]).RemoteEndPoint.ToString() == clientIP)
                            {
                                clientIndex = i;

                                break;
                            }
                        }
                    }
                    if (clientIndex >= 0)
                    {
                        Socket sk = (Socket)this.clientSockets[clientIndex];
                        if (this.clientSockets.Contains(sk))
                        {
                            this.clientSockets.Remove(sk);
                            sk.Dispose();
                            sk = null;
                            GC.Collect();
                            Interlocked.Decrement(ref this.clientCount);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while remove client " + clientIP + " from list." + ex.Message);
                }
            }
            return ret;
        }

        public List<string> GetListClient()
        {
            List<string> ret = new List<string>();
            foreach (Socket socket in this.clientSockets)
            {
                if (socket != null && socket.Connected)
                {
                    ret.Add(socket.RemoteEndPoint.ToString());
                }
            }

            if (ret.Count > 0) return ret;
            return null;
        }

        public void SendMessage(string message)
        {
            try
            {
                if (this.clientSockets != null)
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(message);
                    for (int i = 0; i < this.clientSockets.Count; i++)
                    {
                        try
                        {
                            if (this.clientSockets[i] != null && ((Socket)this.clientSockets[i]).Connected)
                            {
                                ((Socket)this.clientSockets[i]).Send(bytes);
                            }
                        }
                        catch (SocketException exS)
                        {
                            if (exS.ToString().Contains("connection was forcibly close") || exS.ToString().Contains("connection was aborted"))
                            {
                                if (this.ClientDisconnectedEvent != null)
                                {
                                    this.ClientDisconnectedEvent(i, ((Socket)this.clientSockets[i]).RemoteEndPoint.ToString());
                                }

                                Socket sk = (Socket)this.clientSockets[i];
                                if (this.clientSockets.Contains(sk))
                                {
                                    this.clientSockets.Remove(sk);
                                    sk.Dispose();
                                    sk = null;
                                    Interlocked.Decrement(ref this.clientCount);
                                    i -= 1;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            if (ex.ToString().Contains("connection was forcibly close") || ex.ToString().Contains("connection was aborted"))
                            {
                                this.clientSockets.RemoveAt(i);
                                Interlocked.Decrement(ref this.clientCount);
                                i -= 1;
                            }
                        }
                    }
                }
            }
            catch (SocketException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while send message to client." + ex.Message);
                }
            }
        }
        public void SendMessage(string message, string clientIP)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(message);
                foreach (Socket socket in this.clientSockets)
                {
                    if (socket != null && socket.Connected && socket.RemoteEndPoint.ToString().Contains(clientIP))
                    {
                        socket.Send(bytes);
                    }
                }
            }
            catch (SocketException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while send message to client." + ex.Message);
                }
            }
        }
        public void SendMessage(byte[] message)
        {
            try
            {
                foreach (Socket socket in this.clientSockets)
                {
                    if (socket != null && socket.Connected)
                    {
                        socket.Send(message);
                    }
                }
            }
            catch (SocketException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while send message to client." + ex.Message);
                }
            }
            finally
            {
                message = null;
            }
        }
        public void SendMessage(byte[] message, string clientIP)
        {
            try
            {
                foreach (Socket socket in this.clientSockets)
                {
                    if (socket != null && socket.Connected && socket.RemoteEndPoint.ToString().Contains(clientIP))
                    {
                        socket.Send(message);
                    }
                }
            }
            catch (SocketException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while send message to client." + ex.Message);
                }
            }
            finally
            {
                message = null;
            }
        }
        public void SendMessage(string message, int clientNumber)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(message);
                int count = this.clientSockets.Count;
                if (count > 0 && count > clientNumber && clientNumber >= 0)
                {
                    Socket socket = (Socket)this.clientSockets[clientNumber];
                    socket.Send(bytes);
                }
            }
            catch (SocketException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent(string.Concat(new object[]
                    {
                        "Error while send message to client: ",
                        clientNumber,
                        "\n",
                        ex.Message
                    }));
                }
            }
        }
        public void SendMessage(byte[] message, int clientNumber)
        {
            try
            {
                int count = this.clientSockets.Count;
                if (count > 0 && count > clientNumber && clientNumber >= 0)
                {
                    Socket socket = (Socket)this.clientSockets[clientNumber];
                    if (socket != null && socket.Connected)
                    {
                        socket.Send(message);
                    }
                }
            }
            catch (SocketException ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent(string.Concat(new object[]
                    {
                        "Error while send message to client: ",
                        clientNumber,
                        "\n",
                        ex.Message
                    }));
                }
            }
        }
        public void SendFile(string fileName)
        {
            try
            {
                string str = "";
                while (fileName.IndexOf("\\") > -1)
                {
                    str += fileName.Substring(0, fileName.IndexOf("\\") + 1);
                    fileName = fileName.Substring(fileName.IndexOf("\\") + 1);
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
                    this.SocketErrorEvent("Error while sending file to client." + ex.Message);
                }
            }
        }
        public void SendFile(string fileName, string clientIP)
        {
            try
            {
                string str = "";
                while (fileName.IndexOf("\\") > -1)
                {
                    str += fileName.Substring(0, fileName.IndexOf("\\") + 1);
                    fileName = fileName.Substring(fileName.IndexOf("\\") + 1);
                }
                byte[] bytes = Encoding.UTF8.GetBytes(fileName);
                byte[] bytes2 = BitConverter.GetBytes(bytes.Length);
                byte[] array = File.ReadAllBytes(str + fileName);
                byte[] array2 = new byte[4 + bytes.Length + array.Length];
                bytes2.CopyTo(array2, 0);
                bytes.CopyTo(array2, 4);
                array.CopyTo(array2, 4 + bytes.Length);
                this.SendMessage(array2, clientIP);
            }
            catch (Exception ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while sending file to client." + ex.Message);
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
                    this.SocketErrorEvent("Error while sending file to client." + ex.Message);
                }
            }
        }
        public void SendFile(string fileName, byte[] fileData, string clientIP)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(fileName);
                byte[] bytes2 = BitConverter.GetBytes(bytes.Length);
                byte[] array = new byte[4 + bytes.Length + fileData.Length];
                bytes2.CopyTo(array, 0);
                bytes.CopyTo(array, 4);
                fileData.CopyTo(array, 4 + bytes.Length);
                this.SendMessage(array, clientIP);
            }
            catch (Exception ex)
            {
                if (this.SocketErrorEvent != null)
                {
                    this.SocketErrorEvent("Error while sending file to client." + ex.Message);
                }
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
                    this.SocketErrorEvent("Error while saving file data." + ex.Message);
                }
            }
        }
        public void StopListen()
        {
            this.CloseSockets();
        }
        private void CloseSockets()
        {
            this.isRunning = false;
            if (this.serverSocket != null)
            {
                SendMessage("CLOSE_CONNECTION");
                System.Threading.Thread.Sleep(100);
                this.clientSockets.Clear();
                this.serverSocket.Close();
                this.serverSocket.Dispose();
                this.serverSocket = null;
                //clientSockets = null;
            }
            GC.Collect();
        }
        public static string GetFileType(string path)
        {
            string text = SocketServer.GetHeaderInfo(path).ToUpper();
            if (text.StartsWith("FFD8FFE0"))
            {
                return "JPG";
            }
            if (text.StartsWith("49492A"))
            {
                return "TIFF";
            }
            if (text.StartsWith("424D"))
            {
                return "BMP";
            }
            if (text.StartsWith("474946"))
            {
                return "GIF";
            }
            if (text.StartsWith("89504E470D0A1A0A"))
            {
                return "PNG";
            }
            return "";
        }
        public static string GetHeaderInfo(string path)
        {
            if (File.Exists(path))
            {
                byte[] array = new byte[8];
                BinaryReader binaryReader = new BinaryReader(new FileStream(path, FileMode.Open));
                binaryReader.Read(array, 0, array.Length);
                binaryReader.Close();
                StringBuilder stringBuilder = new StringBuilder();
                byte[] array2 = array;
                for (int i = 0; i < array2.Length; i++)
                {
                    byte b = array2[i];
                    stringBuilder.Append(b.ToString("X2"));
                }
                return stringBuilder.ToString();
            }
            return "";
        }
        public static string GetFileType(byte[] data)
        {
            string text = SocketServer.GetHeaderInfo(data).ToUpper();
            if (text.StartsWith("FFD8FFE0"))
            {
                return "JPG";
            }
            if (text.StartsWith("49492A"))
            {
                return "TIFF";
            }
            if (text.StartsWith("424D"))
            {
                return "BMP";
            }
            if (text.StartsWith("474946"))
            {
                return "GIF";
            }
            if (text.StartsWith("89504E470D0A1A0A"))
            {
                return "PNG";
            }
            return "";
        }
        public static string GetHeaderInfo(byte[] data)
        {
            if (data.Length > 8)
            {
                byte[] array = new byte[8];
                Array.Copy(data, array, 8);
                StringBuilder stringBuilder = new StringBuilder();
                byte[] array2 = array;
                for (int i = 0; i < array2.Length; i++)
                {
                    byte b = array2[i];
                    stringBuilder.Append(b.ToString("X2"));
                }
                return stringBuilder.ToString();
            }
            return "";
        }

    }
}
*/
#endregion