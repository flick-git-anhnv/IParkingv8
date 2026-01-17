using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Kztek.Cameras
{
    // Check camera delegate
    public delegate bool CheckCameraHandler(Camera camera);

    public partial class CameraDescriptionPage : UserControl, IWizardPage
    {
        // state changed event
        public event EventHandler StateChanged;
        // reset event
        public event EventHandler Reset;

        public CameraDescriptionPage()
        {
            InitializeComponent();
            cbxVideoSource.SelectedIndex = 0;
        }

        // Camera property
        private Camera camera = null;
        public Camera Camera
        {
            get { return camera; }
            set
            {
                if (value != null)
                {
                    camera = value;

                    txtName.Text = camera.Name;
                    txtDescription.Text = camera.Description;
                    cbxVideoSource.SelectedIndex = (int)camera.CameraType;

                    //cbxVideoSource.Enabled = (camera.ID == 0);
                }
            }
        }

        public int CameraType
        {
            get { return cbxVideoSource.SelectedIndex; }
        }

        // CheckCameraFunction property
        private CheckCameraHandler checkCameraFunction;
        public CheckCameraHandler CheckCameraFunction
        {
            set { checkCameraFunction = value; }
        }

        // PageName property
        public string PageName
        {
            get { return "Tên và chủng loại Camera"; }
        }

        // PageDescription property
        public string PageDescription
        {
            get { return "1. Chọn loại Camera - Nhập tên Camera"; }
        }

        // Completed property
        private bool completed = false;
        public bool Completed
        {
            get { return completed; }
        }

        // Show the page
        public void Display()
        {
            txtName.Focus();
            txtName.SelectionStart = txtName.TextLength;
        }

        // Apply the page
        public bool Apply()
        {
            string name = txtName.Text.Trim();
            if (checkCameraFunction != null)
            {
                Camera tmpCamera = new Camera();
                tmpCamera.ID = camera.ID;
                tmpCamera.Name = name;

                // check camera
                if (checkCameraFunction(tmpCamera) == false)
                {
                    Color tmp = this.txtName.BackColor;

                    // highligh name edit box
                    this.txtName.BackColor = Color.LightCoral;
                    // error message
                    MessageBox.Show(this, "Đã tồn tại Camera với tên này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    // restore & focus name edit box
                    this.txtName.BackColor = tmp;
                    this.txtName.Focus();

                    return false;
                }
            }

            // update camera name and description
            camera.Name = name;
            camera.Description = txtDescription.Text;
            camera.CameraType = (CameraType)cbxVideoSource.SelectedIndex;

            return true;
        }

        // Update state
        private void UpdateState()
        {
            completed = ((txtName.TextLength != 0) && (cbxVideoSource.SelectedIndex >= 0));

            if (StateChanged != null)
                StateChanged(this, new EventArgs());
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            UpdateState();
        }

        private void cbxVideoSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateState();

            if (Reset != null)
                Reset(this, new EventArgs());
        }
    }
}
