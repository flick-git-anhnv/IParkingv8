using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Futech.Objects
{
    public partial class DS3000SetupPage : Form, IControllerSetupPage
    {
        public DS3000SetupPage()
        {
            InitializeComponent();
        }

        // Controller Name property
        private string controllerName = "";
        public string ControllerName
        {
            get { return controllerName; }
            set { controllerName = value; }
        }

        // Address of Controller
        private int address = 0;
        public int Address
        {
            get { return address; }
            set { address = value; }
        }

        private int controllerTypeID = 0;
        public int ControllerTypeID
        {
            get { return controllerTypeID; }
            set { controllerTypeID = value; }
        }

        // Reader1 Type
        public int Reader1Type
        {
            get { return cbxReader1Type.SelectedIndex; }
            set { cbxReader1Type.SelectedIndex = value; }
        }

        // Reader2 Type
        public int Reader2Type
        {
            get { return cbxReader2Type.SelectedIndex; }
            set { cbxReader2Type.SelectedIndex = value; }
        }

        public bool TimeAttendance
        {
            get { return chkTimeAttendance.Checked; }
            set { chkTimeAttendance.Checked = value; }
        }

        // Description property
        public string Description
        {
            get { return txtDescription.Text.Trim(); }
            set { txtDescription.Text = value; }
        }

        private Line currentLine = null;
        public Line CurrentLine
        {
            set { currentLine = value; }
        }

        private ControllerTypeCollection controllerTypes = new ControllerTypeCollection();
        public ControllerTypeCollection ControllerTypes
        {
            set { controllerTypes = value; }
        }

        // all controller
        private ControllerCollection controllers = new ControllerCollection();
        public ControllerCollection Controllers
        {
            set { controllers = value; }
        }

        private void DS3000SetupPage_Load(object sender, EventArgs e)
        {
            txtControllerName.Text = controllerName;

            if (currentLine != null)
            {
                foreach (Controller controller in controllers)
                {
                    if (controller.LineID == currentLine.ID && controller.Address != address)
                    {
                        cbxAddress.Items.Remove(controller.Address.ToString("00"));
                    }
                }

                if (address >= 0)
                {
                    cbxAddress.Text = address.ToString("00");
                }
                else if (cbxAddress.Items.Count > 0)
                    cbxAddress.SelectedIndex = 0;

                cbxControllerType.Items.Clear();
                foreach (ControllerType controllerType in controllerTypes)
                {
                    if (controllerType.LineTypeID == currentLine.LineTypeID)
                    {
                        cbxControllerType.Items.Add(controllerType.Name);
                        if (controllerType.ID == controllerTypeID)
                            cbxControllerType.Text = controllerType.Name;
                    }
                }
            }

            if (cbxControllerType.Text == "")
                cbxControllerType.SelectedIndex = 0;

            if (cbxReader1Type.Text == "")
                cbxReader1Type.SelectedIndex = 0;

            if (cbxReader2Type.Text == "")
                cbxReader2Type.SelectedIndex = 1;

            UpdateState();
        }

        private void txtControllerName_TextChanged(object sender, EventArgs e)
        {
            UpdateState();
        }

        private void UpdateState()
        {
            if (txtControllerName.Text != "" && cbxControllerType.Text != "")
                btnOK.Enabled = true;
            else
                btnOK.Enabled = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Check controller Name
            if (txtControllerName.Text != controllerName)
            {
                if (controllers.GetControllerByName(txtControllerName.Text) != null)
                {
                    MessageBox.Show("Đã tồn tại Bộ điều khiển '" + txtControllerName.Text + "'. Hãy nhập tên khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Check controller Address
            if (cbxAddress.Text == "")
            {
                MessageBox.Show("Bạn hãy chọn địa chỉ cho Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            controllerName = txtControllerName.Text;
            address = Convert.ToInt32(cbxAddress.Text);

            ControllerType controllerType = controllerTypes.GetControllerTypeByName(cbxControllerType.Text);
            if (controllerType != null)
                controllerTypeID = controllerType.ID;

            // close the dialog
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}