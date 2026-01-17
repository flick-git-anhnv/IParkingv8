namespace Futech.Socket
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Net.Sockets;

    // Client
    public delegate void ServerConnectedEventHandler(string serverIP, int serverPort);
    public delegate void ClientRequestCurrentImageHandler(int cameraIndex);
    public delegate void ClientSendPTZCommandHandler(string ptzCommand);

    // Server
    public delegate void ClientConnectedEventHandler(int clientNumber, string clientIP);

    // Common
    public delegate void DataReceivedEventHandler(SocketPacket asyncState, string dataReceivedString, string remoteIP);
    public delegate void DataReceivedInByteArrayEventHandler(SocketPacket asyncState, byte[] dataReceivedByteArray, int dataReceivedLength);
    public delegate void DataReceivedInStringEventHandler(SocketPacket asyncState, string dataReceivedString);

    /// <summary>Exception data event. This event is called when the data is incorrect</summary>
    public delegate void SocketErrorEventHandler(string errorString);

	public enum Command
	{
        // Login Detail: <none>
		Login			= 0,
        // PersonalMessage Detail: message
		PersonalMessage	= 1,
        // Client List: separated by @
        ClientList = 2,
		Conference		= 3,
        // Logout Detail: <none>
        Logout = 4,
        // Show Message
        ShowMessage = 5,
        // Warning
        Warning = 6,
        // Exit
        Exit = 7,
        // Shutdown
        Shutdown = 8,
        // Check Status
        CheckStatus = 9
	};

	public class Common
	{
        public const string ServerName = "Server";
		public const string All			= "All";
		public const string Conference	= "Conference";
	}

    public class SocketPacket
    {
        public int clientNumber;
        public Socket clientSocket;
        public byte[] dataBuffer;

        public SocketPacket()
        {
            this.dataBuffer = new byte[1024 * 1024 * 4]; // 4MB
        }

        public SocketPacket(Socket clientSocket, int clientNumber)
        {
            this.dataBuffer = new byte[1024 * 5000];
            this.clientSocket = clientSocket;
            this.clientNumber = clientNumber;
        }
    }

	public class Message
	{
		/*
		 * Message format
		 * Mr.Thanh|<Sender>|<Receiver>|<Command>|<CommandDetails>
		 * 
		 * Sample
		 * Mr. Thanh|sacredgas|laney_18_boggs|00|Hello World
		 * 
		 */ 
        private string[] strSender = new string[1];
        private string[] strReceiver = new string[1];
        private Command[] cmdMessageCommand = new Command[1];
        private string[] strMessageDetail = new string[1];
        public int messageNumber = 1;

		public Message ()
		{

		}

        public Message(byte[] rawMessage)
		{
			string strRawStringMessage 
				= System.Text.Encoding.ASCII.GetString (rawMessage);            
			string [] strRawStringMessageArray
				= strRawStringMessage.Split(new char []{'|'});

            if (strRawStringMessageArray.Length == 6)
            {
                this.strSender[0] = strRawStringMessageArray[1];
                this.strReceiver[0] = strRawStringMessageArray[2];
                this.cmdMessageCommand[0] = (Command)Convert.ToInt32(strRawStringMessageArray[3]);
                this.MessageDetail[0] = strRawStringMessageArray[4];
            }
            else
            {
                this.MessageDetail[0] = strRawStringMessage;
            }
		}

        public Message(byte[] rawMessage, int rawMessageLength)
        {
            string strRawStringMessage
                = System.Text.Encoding.ASCII.GetString(rawMessage, 0, rawMessageLength);
            string[] strRawStringMessageArray
                = strRawStringMessage.Split(new char[] { '|' });
            if (strRawStringMessageArray.Length != 1 && (strRawStringMessageArray.Length - 1) % 5 == 0)
            {
                messageNumber = (strRawStringMessageArray.Length - 1) / 5;
                this.strSender = new string[messageNumber];
                this.strReceiver = new string[messageNumber];
                this.cmdMessageCommand = new Command[messageNumber];
                this.strMessageDetail = new string[messageNumber];
                int k = 0;
                for (int i = 0; i < strRawStringMessageArray.Length - 1; i = i + 5)
                {
                    this.strSender[k] = strRawStringMessageArray[i + 1];
                    this.strReceiver[k] = strRawStringMessageArray[i + 2];
                    this.cmdMessageCommand[k] = (Command)Convert.ToInt32(strRawStringMessageArray[i + 3]);
                    this.strMessageDetail[k] = strRawStringMessageArray[i + 4];
                    k = k + 1;
                }
            }
            else
            {
                this.MessageDetail[0] = strRawStringMessage;
            }
        }

		public string[] Sender
		{
			get{ return strSender;}
			set{ strSender = value;}
		}

		public string[] Receiver
		{
			get{ return strReceiver;}
			set{ strReceiver = value;}
		}
		public Command[] MessageCommand
		{
			get{ return cmdMessageCommand ;}
			set{ cmdMessageCommand = value;}
		}
		public string[] MessageDetail
		{
			get{ return strMessageDetail ;}
			set{ strMessageDetail = value;}
		}        

		public byte [] GetRawMessage ()
		{
			System.Text.StringBuilder sbMessage
                = new System.Text.StringBuilder("Hello Mr.Thanh");
			sbMessage.Append("|");
			sbMessage.Append(strSender[0]);
			sbMessage.Append("|");
			sbMessage.Append(strReceiver[0]);
			sbMessage.Append("|");
			sbMessage.Append((int)cmdMessageCommand[0]);
			sbMessage.Append("|");
			sbMessage.Append(strMessageDetail[0]);
			sbMessage.Append("|");

			return System.Text.Encoding.ASCII.GetBytes(sbMessage.ToString());
		}
	}
}