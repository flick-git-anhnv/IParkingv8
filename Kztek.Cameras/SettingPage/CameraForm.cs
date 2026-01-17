using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kztek.Cameras
{
    public partial class CameraForm : Wizard
    {
        private Camera camera = new Camera();
        private CameraDescriptionPage page1 = new CameraDescriptionPage();
        private CameraSettingPage page2 = new CameraSettingPage();

        // Camera property
        public Camera Camera
        {
            get { return camera; }
            set
            {
                camera = value;
                page1.Camera = camera;
                page2.Camera = camera;
            }
        }

        // CheckCameraFunction property
        public CheckCameraHandler CheckCameraFunction
        {
            set { page1.CheckCameraFunction = value; }
        }

        // Construction
        public CameraForm()
        {
            InitializeComponent();

            this.AddPage(page1);
            this.AddPage(page2);
            this.Text = "Thiết lập Camera";

            page1.Camera = camera;
            page2.Camera = camera;

            this.ActiveControl = page1;
        }

        // On page changing
        protected override void OnPageChanging(int page)
        {
            if (page == 1)
            {
                // switching to camera settings
                page2.CameraType = page1.CameraType;
            }
            base.OnPageChanging(page);
        }

        // Reset event ocuren on page
        protected override void OnResetOnPage(int page)
        {
            if (page == 0)
            {
                page2.CameraType = -1;
            }
        }

        // On finish
        protected override void OnFinish()
        {
        }
    }
}
