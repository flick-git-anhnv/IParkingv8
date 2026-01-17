using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Futech.App
{
    public partial class frmMain : Form
    {
        private System.IO.Ports.SerialPort serialPort1 = new System.IO.Ports.SerialPort();


        public frmMain()
        {
            InitializeComponent();
        }
        

        private void btnVideo_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmVideo frm = new frmVideo();
            frm.ShowDialog();
            this.Show();
        }

        private void btnLpr_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLpr frm = new frmLpr();
            frm.ShowDialog();
            this.Show();
        }

        private void btnObjects_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmObjects frm = new frmObjects();
            frm.ShowDialog();
            this.Show();
        }

        private void btnSocket_Click(object sender, EventArgs e)
        {
            this.Hide();
            Futech.Socket.frmMain frm = new Futech.Socket.frmMain();
            frm.ShowDialog();
            this.Show();
        }

        string MESSID = "";
        byte[] buffer = new byte[28];
        int index = 0;
        string viewraw = "";
        private delegate void NewSerialDataReceivedEventCallBack(object sender, System.IO.Ports.SerialDataReceivedEventArgs e);
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                if (this.listBox1.InvokeRequired)
                {
                    NewSerialDataReceivedEventCallBack d = new NewSerialDataReceivedEventCallBack(serialPort1_DataReceived);
                    this.Invoke(d, new object[] { sender, e });
                }
                else
                {
                    while (serialPort1.BytesToRead > 0)
                    {
                        buffer[index] = (Byte)serialPort1.ReadByte();
                        index = index + 1;
                    }
                    if (index == 12)
                    {   
                        viewraw = System.Text.Encoding.UTF8.GetString(buffer);
                        // 10 so dau + " " + 8 so cuoi
                        MESSID = viewraw.Substring(1, 10);
                        string ID1, ID2;
                        // 10 so dau
                        uint CardNumber = uint.Parse(MESSID.Substring(4, 6), System.Globalization.NumberStyles.HexNumber);
                        listBox1.Items.Add(CardNumber.ToString());
                        // 10 so cuoi
                        string CardNumber2 = uint.Parse(MESSID.Substring(4, 2), System.Globalization.NumberStyles.HexNumber) +
                            "" + uint.Parse(MESSID.Substring(6, 4), System.Globalization.NumberStyles.HexNumber);
                        listBox1.Items.Add(CardNumber2);
                        // Chuyen doi tu so the sau ra so the dung truoc (8 so cuoi thanh 10 so dau)
                        //CardNumber = 21156250;
                        //string strCardNumber1 = CardNumber.ToString("00000000");
                        //int CardNumber11, CardNumber12;
                        //CardNumber12 = int.Parse(strCardNumber1.Substring(3, 5));
                        //CardNumber11 = int.Parse(strCardNumber1.Substring(0, 3));
                        //string strCardNumber2_Hex = CardNumber11.ToString("X") + CardNumber12.ToString("X");
                        //int CardNumber2 = int.Parse(strCardNumber2_Hex, System.Globalization.NumberStyles.HexNumber);
                        //listBox1.Items.Add(CardNumber2.ToString());

                        MESSID = "";
                        index = 0;
                        buffer = new byte[12];
                        viewraw = "";
                    }

                    if (index > 12)
                    {
                        MESSID = "";
                        index = 0;
                        buffer = new byte[12];
                        viewraw = "";
                    }
                }
            }
            catch (Exception ex)
            {
                index = 0;
                MessageBox.Show(ex.Message);
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = txtComPort.Text;
            serialPort1.BaudRate = 9600;
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            if (serialPort1.IsOpen)
                serialPort1.Close();
            serialPort1.Open();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                this.serialPort1.DataReceived -= new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
                serialPort1.Close();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}