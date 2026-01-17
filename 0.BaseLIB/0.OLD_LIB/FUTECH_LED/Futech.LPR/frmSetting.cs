using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace Futech.LPR
{
    public partial class frmSetting : Form
    {
        public frmSetting(ILPR ilpr)
        {
            InitializeComponent();

            this.Text = this.Text + " - " + ilpr.GetVersion;

            txtMinCharHeight.Text = ilpr.MinCharHeight.ToString();
            txtMaxCharHeight.Text = ilpr.MaxCharHeight.ToString();

            cbPlateColorShema.SelectedIndex = ilpr.PlateColorSchema;
            cbContrastLevel.SelectedIndex = ilpr.ContrastLevel;
            cbPreciseMode.SelectedIndex = ilpr.PreciseMode;

            chbDeinterlace.Checked = ilpr.Deinterlace;
            chbDeinterlacedSource.Checked = ilpr.DeinterlacedSource;

            txtDeviationAngle.Text = ilpr.DeviationAnge.ToString();
            txtRotateAngle.Text = ilpr.RotateAngle.ToString();

            txtFps.Text = ilpr.Fps.ToString();
            txtPlatePresenceTime.Text = ilpr.PlatePresenceTime.ToString();
        }

        private void frmSetting_Load(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // Get the current configuration file.
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            KeyValueConfigurationCollection appSettings = config.AppSettings.Settings;

            // MinCharHeight = 8;
            if (appSettings["MinCharHeight"] != null)
                appSettings.Remove("MinCharHeight");
            appSettings.Add("MinCharHeight", txtMinCharHeight.Text.ToString());

            // MaxCharHeight = 64;
            if (appSettings["MaxCharHeight"] != null)
                appSettings.Remove("MaxCharHeight");
            appSettings.Add("MaxCharHeight", txtMaxCharHeight.Text.ToString());

            // PlateColorSchema = 1; 0-Unknown, 1-BlackOnWhite, 2-WhiteOnBlack, 3-All ~ BlackOnWhite
            if (appSettings["PlateColorSchema"] != null)
                appSettings.Remove("PlateColorSchema");
            appSettings.Add("PlateColorSchema", cbPlateColorShema.SelectedIndex.ToString());

            // ContrastLevel = 2; 0-Unknown, 1-Low, 2-Medium, 3-High,  ~ Medium
            if (appSettings["ContrastLevel"] != null)
                appSettings.Remove("ContrastLevel");
            appSettings.Add("ContrastLevel", cbContrastLevel.SelectedIndex.ToString());

            // PreciseMode = 0; Normal, Mode1, Mode2, Night ~ Normal
            if (appSettings["PreciseMode"] != null)
                appSettings.Remove("PreciseMode");
            appSettings.Add("PreciseMode", cbPreciseMode.SelectedIndex.ToString());

            // Deinterlace = false;
            if (appSettings["Deinterlace"] != null)
                appSettings.Remove("Deinterlace");
            appSettings.Add("Deinterlace", chbDeinterlace.Checked.ToString());

            // DeinterlacedSource = false;
            if (appSettings["DeinterlacedSource"] != null)
                appSettings.Remove("DeinterlacedSource");
            appSettings.Add("DeinterlacedSource", chbDeinterlacedSource.Checked.ToString());

            // DeviationAnge = 30;
            if (appSettings["DeviationAnge"] != null)
                appSettings.Remove("DeviationAnge");
            appSettings.Add("DeviationAnge", txtDeviationAngle.Text.ToString());

            // RotateAngle = 0;
            if (appSettings["RotateAngle"] != null)
                appSettings.Remove("RotateAngle");
            appSettings.Add("RotateAngle", txtRotateAngle.Text.ToString());

            // Fps = 25; Only Video Mode
            if (appSettings["Fps"] != null)
                appSettings.Remove("Fps");
            appSettings.Add("Fps", txtFps.Text.ToString());

            // PlatePresenceTime = 1000;
            if (appSettings["PlatePresenceTime"] != null)
                appSettings.Remove("PlatePresenceTime");
            appSettings.Add("PlatePresenceTime", txtPlatePresenceTime.Text.ToString());

            config.Save(ConfigurationSaveMode.Minimal, true);

            this.DialogResult = DialogResult.OK;
        }
    }
}