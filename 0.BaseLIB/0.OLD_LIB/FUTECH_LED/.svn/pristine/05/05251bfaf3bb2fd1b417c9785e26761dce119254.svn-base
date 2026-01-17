using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Futech.Objects;

namespace Futech.App
{
    public partial class frmObjects : Form
    {
        private Line line = new Line();
        private Controller controller = new Controller();
        private ControllerType controllertype = new ControllerType();

        private Line line2 = new Line();
        private Controller controller2 = new Controller();
        private ControllerType controllertype2 = new ControllerType();

        private string comport;
        public string Comport
        {
            get { return comport; }
            set { comport = value; }
        }

        public frmObjects()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            controller.Address = 1;
            controller.LineCode = "HID#VID_FFFF&PID_0035&MI_00#7&13434ee2&0&0000#";
            controller.Description = "HID#VID_FFFF&PID_0035&MI_00#7&13434ee2&0&0000#";
            controllertype.ID = 0;
            controllertype.LineTypeID = 61; // Pegasus PUA-310


            //controller2.Address = 2;
            //controller2.LineCode = "HID#VID_FFFF&PID_0035&MI_00#7&2cb6fd84&0&0000#"; //HID#VID_413C&PID_2107#6&30e011f3&0&0000#
            //controller2.Description = "HID#VID_FFFF&PID_0035&MI_00#7&2cb6fd84&0&0000#";
            //controllertype2.ID = 0;
            //controllertype2.LineTypeID = 45; // Pegasus PUA-310

            line.ControllerTypes.Add(controllertype);
            line.BaudRate = 51211;// 51211;
            line.CommunicationType = 1; // Ket noi qua RS232
            line.ComPort = "169.254.9.15";// "192.168.1.50";// "386864925"; //; @"HID\VID_FFFF&PID_0035&MI_01\7&44C4FBB&0&0000";
                                           //;HID\VID_FFFF&PID_0035&MI_00\7&C6CC1FD&0&0000";//txtComPort.Text;
                                           //line.DelayTime = 100;
            line.Controllers.Add(controller);
            //line.Controllers.Add(controller2);
            line.ID = 0;
            line.LineTypeID = 61;
            line.CardEvent += new CardEventHandler(axStarInterface_CardEvent);
            line.InputEvent += new InputEventHandler(line_InputEvent);
            listBox1.Items.Add(line.Start());


            //controller2.Address = 2;
            //controller2.LineCode = "02";
            //controllertype2.ID = 0;
            //controllertype2.LineTypeID = 46; // Pegasus PUA-310

            //line2.ControllerTypes.Add(controllertype2);
            //line2.BaudRate = 8000;
            //line2.CommunicationType = 0; // Ket noi qua RS232
            //line2.ComPort = "HID#VID_FFFF&PID_0035&MI_00#7&2cb6fd84&0&0000#";//txtComPort.Text;
            ////line.DelayTime = 100;
            //line2.Controllers.Add(controller2);

            //line2.ID = 0;
            //line2.LineTypeID = 46;
            //line2.CardEvent += new CardEventHandler(axStarInterface_CardEvent);
            //line2.InputEvent += new InputEventHandler(line_InputEvent);
            //listBox1.Items.Add(line2.Start());

        }
        private delegate void InputEventCallback(object sender, InputEventArgs e);
        void line_InputEvent(object sender, InputEventArgs e)
        {
            if (listBox1.InvokeRequired)
            {
                InputEventCallback d = new InputEventCallback(line_InputEvent);
                this.Invoke(d, new object[] { sender, e });
            }
            else
            {
                // MessageBox.Show("a");
                listBox1.Items.Add(DateTime.Now.ToString("yyyy/MM/dd HH:mm") + " input event port: " + e.Inputport + ", eventtype:" + e.EventType);
                listBox1.SetSelected(listBox1.Items.Count - 1, true);
            }
            //throw new Exception("The method or operation is not implemented.");
        }

        private void btnDisconect_Click(object sender, EventArgs e)
        {
            // Stop Reader
            line.SignalToStop();
            line.Stop();
            line.CardEvent -= new CardEventHandler(axStarInterface_CardEvent);
            line.InputEvent -= new InputEventHandler(line_InputEvent);
        }

        private void btnConfigSettingPage_Click(object sender, EventArgs e)
        {
            ConfigSettingPage frm = new ConfigSettingPage();
            frm.ShowDialog();
        }

        private delegate void NewCardEventCallBack(object sender, CardEventArgs e);
        private void axStarInterface_CardEvent(object sender, CardEventArgs e)
        {
            try
            {
                if (this.listBox1.InvokeRequired)
                {
                    NewCardEventCallBack d = new NewCardEventCallBack(axStarInterface_CardEvent);

                    this.Invoke(d, new object[] { sender, e });
                }
                else
                {

                    listBox1.Items.Add(e.CardNumber + " at " + e.Date + " " + e.Time + "----" + e.ReaderIndex);
                    listBox1.SetSelected(listBox1.Items.Count - 1, true);
                    //MessageBox.Show("a");
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmObjects_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnDisconect_Click(null, null);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            IControllerSettingPage controllerSettingPage = line.GetControllerSettingPage;
            if (controllerSettingPage != null)
            {
                if (controllerSettingPage.IsConnect)
                {
                    //tsController.Image = Properties.Resources.ball_green;
                    label1.Text = "Connect";
                }
                else
                {
                    //tsController.Image = Properties.Resources.ball_red;
                    label1.Text = "DisConnect";
                }
            }
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            IControllerSettingPage controllerSettingPage = line.GetControllerSettingPage;
            if (keyData == Keys.F1)
            {
                controllerSettingPage.Unlock2(1, 1);
            }
            if (keyData == Keys.F2)
            {
                controllerSettingPage.Unlock2(2, 1);
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnOpenDoor_Click(object sender, EventArgs e)
        {
            IControllerSettingPage controllersettingpage = line.GetControllerSettingPage;
            if (controllersettingpage != null)
            {
                controllersettingpage.Unlock2(int.Parse(cbDoors.Text), 100);
            }
        }

        private void frmObjects_Load(object sender, EventArgs e)
        {
            cbDoors.Text = "1";
        }
    }
}