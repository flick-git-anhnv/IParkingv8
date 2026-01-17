using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Futech.Socket
{
    public partial class frmMain : Form
    {
        private SocketServer socketServer = null;
        private SocketClient socketClient = null;

        // This delegate enables asynchronous calls for setting
        private delegate void SetListBoxCallback(string message);

        private string receivePath = "C:\\temp\\";

        public frmMain()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Server
            if (socketServer != null && socketServer.IsRunning)
            {
                socketServer.StopListen();
                socketServer = null;
            }

            // Client
            if (socketClient != null && socketClient.Connected)
            {
                socketClient.Disconnect();
                socketClient = null;
            }
        }

        /// <summary>
        /// Server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "Start")
            {
                btnStart.Text = "Stop";
                socketServer = new SocketServer(txtServerIP.Text, int.Parse(txtServerPort.Text));
                socketServer.ClientConnectedEvent += new ClientConnectedEventHandler(ClientConnected);
                //socketServer.DataReceivedEvent += new DataReceivedEventHandler(DataReceived);
                socketServer.DataReceivedInByteArrayEvent += new DataReceivedInByteArrayEventHandler(DataReceivedInByteArray);
                //socketServer.DataReceivedInStringEvent += new DataReceivedInStringEventHandler(DataReceivedInString);
                socketServer.SocketErrorEvent += new SocketErrorEventHandler(SocketError);

                socketServer.StartListen();

                if (socketServer.IsRunning)
                    ShowMessageOnServer("Server Started");
            }
            else
            {
                btnStart.Text = "Start";
                socketServer.StopListen();
                socketServer = null;
            }
        }

        private void ClientConnected(int clientNumber, string clientIP)
        {
            ShowMessageOnServer("Client: " + clientNumber + " - " + clientIP.Substring(0, clientIP.IndexOf(":")) + " Connected");
        }

        private void DataReceivedInByteArray(SocketPacket asyncState, byte[] dataReceivedByteArray, int dataReceivedLength)
        {
            if (socketServer != null)
            {
                if (dataReceivedLength <= -1)
                {
                    if (socketServer.SaveFile(receivePath, dataReceivedByteArray, dataReceivedLength))
                        ShowMessageOnServer("Client: " + "Receive File Ok");
                }
                else
                {
                    Message message = new Message(dataReceivedByteArray, dataReceivedLength);
                    for (int i = 0; i < message.messageNumber; i++)
                    {
                        switch (message.MessageCommand[i])
                        {
                            case Command.PersonalMessage: // Du lieu can gui tu Client
                                ShowMessageOnServer("Client " + asyncState.clientNumber + " Say: " + message.MessageDetail[i]);
                                socketServer.SendMessage("Ok", asyncState);
                                ShowMessageOnServer("Server Say: " + "Ok");
                                break;
                            case Command.Exit: // Yeu cau exit tu client
                                this.Close();
                                break;
                            case Command.CheckStatus: // Client gui thong tin de kiem tra trang thai may chu
                                ShowMessageOnServer("Client " + asyncState.clientNumber + " Check Status");
                                break;
                            default:
                                ShowMessageOnServer("Client " + asyncState.clientNumber + " Say: " + message.MessageDetail[i]);
                                break;
                        }
                    }
                }
            }
        }

        private void SocketError(string errorString)
        {
            ShowMessageOnServer(errorString);
        }

        private void btnSendToClient_Click(object sender, EventArgs e)
        {
            if (socketServer != null && socketServer.IsRunning)
            {
                if (txtSendToClient.Text != "")
                {
                    Message message = new Message();
                    message.Sender[0] = "Server";
                    message.Receiver[0] = "Client";
                    message.MessageCommand[0] = Command.PersonalMessage;
                    message.MessageDetail[0] = txtSendToClient.Text;
                    //socketServer.SendMessage(txtSendToClient.Text);
                    socketServer.SendMessage(message);
                    ShowMessageOnServer("Server Say: " + txtSendToClient.Text);
                    txtSendToClient.Text = "";
                }
            }
        }


        private void btnSendFileToClient_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK && System.IO.File.Exists(openFileDialog1.FileName))
            {
                socketServer.SendFile(openFileDialog1.FileName);
            }
        }

        private void ShowMessageOnServer(string message)
        {
            try
            {
                if (this.listMessageServer.InvokeRequired)
                {
                    SetListBoxCallback d = new SetListBoxCallback(ShowMessageOnServer);
                    this.Invoke(d, new object[] { message });
                }
                else
                {
                    listMessageServer.Items.Add(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:      ") + message);
                    listMessageServer.SetSelected(listMessageServer.Items.Count - 1, true);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text == "Connect")
            {
                socketClient = new SocketClient(txtServerIP.Text, int.Parse(txtServerPort.Text));
                socketClient.ServerConnectedEvent += new ServerConnectedEventHandler(ServerConnected);
                socketClient.DataReceivedInByteArrayEvent += new DataReceivedInByteArrayEventHandler(DataReceivedInByteArray1);
                //socketClient.DataReceivedInStringEvent += new DataReceivedInStringEventHandler(DataReceivedInString1);
                socketClient.SocketErrorEvent += new SocketErrorEventHandler(SocketError1);

                if (socketClient.Connect())
                    btnConnect.Text = "Disconnect";
            }
            else
            {
                btnConnect.Text = "Connect";
                socketClient.Disconnect();
            }
        }

        private void ServerConnected(string serverIP, int serverPort)
        {
            ShowMessageOnClient("Start connect to server: " + serverIP);
        }

        private void DataReceivedInByteArray1(SocketPacket asyncState, byte[] dataReceivedByteArray, int dataReceivedLength)
        {
            if (socketClient != null)
            {
                if (dataReceivedLength >= 1024)
                {
                    if (socketClient.SaveFile(receivePath, dataReceivedByteArray, dataReceivedLength))
                        ShowMessageOnClient("Client: " + "Receive File Ok");
                }
                else
                {
                    Message message = new Message(dataReceivedByteArray, dataReceivedLength);
                    for (int i = 0; i < message.messageNumber; i++)
                    {
                        switch (message.MessageCommand[i])
                        {
                            case Command.PersonalMessage:
                                ShowMessageOnClient("Server Say: " + message.MessageDetail[i]);
                                break;
                            case Command.CheckStatus:
                                ShowMessageOnClient(message.MessageDetail[i]);
                                break;
                            default:
                                ShowMessageOnClient("Server Say: " + message.MessageDetail[i]);
                                break;
                        }
                    }
                }
            }
        }

        private void SocketError1(string errorString)
        {
            ShowMessageOnClient(errorString);
        }

        private void btnSendToServer_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {
                if (txtSendToServer.Text != "")
                {
                    Message message = new Message();
                    message.Sender[0] = "Client";
                    message.Receiver[0] = "Server";
                    message.MessageCommand[0] = Command.PersonalMessage;
                    message.MessageDetail[0] = txtSendToServer.Text + " --> " + i;
                    //socketClient.SendMessage(txtSendToServer.Text);
                    socketClient.SendMessage(message);
                    ShowMessageOnClient("Client Say: " + txtSendToServer.Text);
                    //txtSendToServer.Text = "";
                }
            }
        }


        private void btnSendFileToServer_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK && System.IO.File.Exists(openFileDialog1.FileName))
            {
                socketClient.SendFile(openFileDialog1.FileName);
            }
        }

        private void ShowMessageOnClient(string message)
        {
            try
            {
                if (this.listMessageServer.InvokeRequired)
                {
                    SetListBoxCallback d = new SetListBoxCallback(ShowMessageOnClient);
                    this.Invoke(d, new object[] { message });
                }
                else
                {
                    listMessageClient.Items.Add(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:      ") + message);
                    listMessageClient.SetSelected(listMessageClient.Items.Count - 1, true);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void btnChooseFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                receivePath = folderBrowserDialog1.SelectedPath + "\\";
                lbReceivePath.Text = receivePath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //HttpRequestResponse tinyclient = new HttpRequestResponse("http://192.168.3.33:1703/?LicenseKey=YGTLJSEEWPGTEBZLFU9YN0HKSA4TXZU8TNRW2Q6XICWWAWF+9+LTGEF3WEZE3SOB&UserCode=NETPOS3HIT7VQKYRAJAX9XS5PARLOQP84UMYK7TT");
            //string str = tinyclient.SendRequest("hello");
        }
    }
}