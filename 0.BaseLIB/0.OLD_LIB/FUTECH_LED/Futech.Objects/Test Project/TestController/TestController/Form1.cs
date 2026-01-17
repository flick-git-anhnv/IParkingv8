using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Futech.Objects;

namespace TestController
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < 100; i++)
                cbxCOM.Items.Add("COM" + i.ToString());
            cbxCOM.SelectedIndex = 0;

            //string x1 = "38"; int x1_int = Convert.ToInt32(x1, 16);
            //string x2 = "45"; int x2_int = Convert.ToInt32(x2, 16);
            //string str_Byte1 = Convert.ToChar(x1_int).ToString() + Convert.ToChar(x2_int).ToString();
            //MessageBox.Show(str_Byte1);
            //string s = "B8B3E49B";
            //MessageBox.Show(Convert.ToInt64(s, 16).ToString());
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnStop_Click(null, null);
        }

        private void new_CardEvent(object sender, CardEventArgs e)
        {
            SetText("New card: ReaderIndex: " + + e.ReaderIndex + "  |  "  + e.CardNumber + "  |  " + e.Date + "  |  " + e.Time);
        }

        //Input Event
        void line_InputEvent(object sender, InputEventArgs e)
        {
            SetText("New input: InputIndex: " + e.Inputport + "  |  " + e.LineID + "  |  " + e.EventDate + "  |  " + e.EventTime + "  | " + e.EventStatus);
        }

        private void SetText(string text)
        {
            if (lstReceivedMessage.InvokeRequired)
            {
                lstReceivedMessage.Invoke(new MethodInvoker(delegate { SetText(text); }));

                return;
            }
            if (lstReceivedMessage.Items.Count > 100) lstReceivedMessage.Items.Clear();
            lstReceivedMessage.Items.Add(text);
            lstReceivedMessage.SelectedIndex = lstReceivedMessage.Items.Count - 1;
        }

        Line line = new Line();
        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStop_Click(null, null);
            line = new Line();
            line.Code = "abc";
            line.LineTypeID = 64; // SC2000
            line.ComPort = cbxCOM.Text;
            line.BaudRate = 9600;
            line.CommunicationType = 0;
            Controller controller = new Controller();
            ControllerType controllertype = new ControllerType();
            controller.Address = 1;
            controller.Reader1Type = 1;//reader1Type for left screen
            controller.Reader2Type = 2;//reader2Type for right screen
            controller.LineCode = line.Code;
            controller.Description = "";
            controllertype.ID = 0;
            controllertype.LineTypeID = line.LineTypeID; // 

            line.ControllerTypes.Add(controllertype);
            //controller.ControllerTypeID = line.LineTypeID;//why?
            controller.Name = "name";
            controller.Code = "code";
            line.Controllers.Add(controller);


            line.CardEvent += new CardEventHandler(new_CardEvent);
            line.InputEvent += new InputEventHandler(line_InputEvent);
            line.Start();

            
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (line != null)
            {
                line.SignalToStop();
                System.Threading.Thread.Sleep(1000);
                line.Stop();
                line.CardEvent -= new CardEventHandler(new_CardEvent);
                line.InputEvent -= new InputEventHandler(line_InputEvent);
                line = null;
            }
        }

        private void btnRelay_Click(object sender, EventArgs e)
        {
            IControllerSettingPage controllerSettingPage = line.GetControllerSettingPage;
            controllerSettingPage.Unlock((int)numericUpDown1.Value);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(line != null && line.Controllers != null && line.Controllers.Count > 0)
                lblStatus.Text = line.Controllers[0].IsConnect ? "Connected" : "Disconnected";
        }
    }
}
