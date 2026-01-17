using iParkingv5.Lpr.LprDetecters.KztekLprs;
using iParkingv5.Lpr.Objects;
using iParkingv5.LprDetecter.LprDetecters;
using Kztek.Object;
using Kztek.Tool;
using System.Diagnostics;
using static Kztek.Object.LprDetecter;

namespace iParking.ConfigurationManager.UserControls
{
    public partial class UcLprOption : UserControl
    {
        //KztekLprStandardlone standardlone = new KztekLprStandardlone();
        public UcLprOption(LprConfig? lprConfig)
        {
            InitializeComponent();
            //standardlone.CreateLprAsync(new LprConfig()
            //{
            //    LPRDetecterType = EmLprDetecter.KztekLpr
            //});
            LoadLprType();
            cbLprType.SelectedIndexChanged += CbLprType_SelectedIndexChanged;
            if (lprConfig != null)
            {
                int selectedLprType = (int)lprConfig.LPRDetecterType >= cbLprType.Items.Count ? -1 : (int)lprConfig.LPRDetecterType;
                cbLprType.SelectedIndex = selectedLprType;
                txtLprUrl.Text = lprConfig.Url;
                txtUsername.Text = lprConfig.Username;
                txtPassword.Text = lprConfig.Password;
                txtRedetectPlateTimes.Text = lprConfig.RetakePhotoTimes.ToString();
                txtRedetectPlateDelayTime.Text = lprConfig.RetakePhotoDelay.ToString();
            }
        }
        private void LoadLprType()
        {
            foreach (EmLprDetecter item in Enum.GetValues(typeof(EmLprDetecter)))
            {
                cbLprType.Items.Add(item.ToString());
            }
        }

        private void CbLprType_SelectedIndexChanged(object? sender, EventArgs e)
        {
            //switch ((EmLprDetecter)cbLprType.SelectedIndex)
            //{
            //    case EmLprDetecter.KztekLpr:
            //        panelLprInfo.Enabled = false;
            //        break;
            //    case EmLprDetecter.AmericalLpr:
            //        panelLprInfo.Enabled = true;
            //        break;
            //    default:
            //        break;
            //}
        }
        #region Public Function
        public LprConfig GetConfig()
        {
            _ = int.TryParse(txtRedetectPlateTimes.Text, out int RedetectPlateTimes);
            _ = int.TryParse(txtRedetectPlateDelayTime.Text, out int RedetectPlateDelayTime);
            return new LprConfig()
            {
                LPRDetecterType = (EmLprDetecter)cbLprType.SelectedIndex,
                Url = txtLprUrl.Text,
                Username = txtUsername.Text,
                Password = txtPassword.Text,
                RetakePhotoTimes = RedetectPlateTimes,
                RetakePhotoDelay = RedetectPlateDelayTime,
            };
        }
        #endregion End Public Function

        private async void btnOpenAdvanceMode_Click(object sender, EventArgs e)
        {
            txtPlateNumber.Text = "_ _ _ _ _ _ _ _";
            OpenFileDialog ofd = new()
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tif;*.tiff|All Files|*.*",
                Multiselect = false,
                Title = "Chọn logo hiển thị phần mềm"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var engine = LprFactory.CreateLprDetecter(new LprConfig()
                {
                    LPRDetecterType = (EmLprDetecter)cbLprType.SelectedIndex,
                    Url = txtLprUrl.Text,
                    Username = txtUsername.Text,
                    Password = txtPassword.Text,
                }, null);
                //await engine.CreateLprAsync(new LprConfig()
                //{
                //    LPRDetecterType = (EmLprDetecter)cbLprType.SelectedIndex,
                //    Url = txtLprUrl.Text,
                //    Username = txtUsername.Text,
                //    Password = txtPassword.Text,
                //});
                Image originalImage = Image.FromFile(ofd.FileName);
                var timestamp = new Stopwatch();
                timestamp.Start();
                //DetectLprResult? result = cbLprType.SelectedIndex == (int)EmLprDetecter.KztekLpr?
                //            await standardlone!.GetPlateNumberAsync(originalImage, false, null, 0):
                DetectLprResult? result = await engine!.GetPlateNumberAsync(originalImage, false, null, 0);
                timestamp.Stop();

                txtPlateNumber.Text = result.PlateNumber + " - " + timestamp.ElapsedMilliseconds + "ms";
                Bitmap? bmp = (Bitmap)originalImage;
                if (result.BoundingBox != null)
                {
                    bmp = ImageHelper.DrawRect((Bitmap?)originalImage, result.BoundingBox.Xmin, result.BoundingBox.Ymin,
                                                        result.BoundingBox.Xmax - result.BoundingBox.Xmin,
                                                        result.BoundingBox.Ymax - result.BoundingBox.Ymin, Color.Red, 10);
                }
                picTest.Image = bmp;
            }
        }
    }
}