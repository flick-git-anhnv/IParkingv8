using iParkingv5.LprDetecter.LprDetecters;
using iParkingv8.Object.ConfigObjects.CameraConfigs;
using iParkingv8.Object.Objects.Devices;
using iParkingv8.Ultility;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using Kztek.Tool;

namespace Kztek.Control8.UserControls.ConfigUcs.CameraConfigs
{
    public partial class UcCameraConfig : UserControl, KzITranslate
    {
        #region Properties
        private Camera? currentCamera;
        private string laneId = string.Empty;
        private IEnumerable<Camera> cameras = Enumerable.Empty<Camera>();
        private ILpr LprDetecter;

        private Rectangle? LprConfig = null;
        private Rectangle? LoopConfig = null;
        #endregion End Properties

        #region Forms
        public UcCameraConfig(IEnumerable<Camera> cameras, string laneId, ILpr LprDetecter)
        {
            InitializeComponent();
            InitProperties(cameras, laneId, LprDetecter);
            Translate();

            this.Load += UcCameraConfig_Load;
        }

        private void UcCameraConfig_Load(object? sender, EventArgs e)
        {
            InitUI();
        }
        #endregion End Forms

        #region Controls In Form
        private void CbCamera_SelectedIndexChanged(object? sender, EventArgs e)
        {
            btnLiveview.Visible = true;
            currentCamera = null;
            LprConfig = null;
            foreach (Camera camera in this.cameras)
            {
                if (camera.Name == cbCamera.Text)
                {
                    currentCamera = camera;

                    ClearView();

                    var lprConfig = NewtonSoftHelper<CameraConfig>.DeserializeObjectFromPath(IparkingingPathManagement.laneCameraConfigPath(laneId, this.currentCamera!.Id));
                    if (lprConfig != null)
                    {
                        LprConfig = lprConfig.DetectRegion == null ?
                            null : new Rectangle(lprConfig.DetectRegion.X, lprConfig.DetectRegion.Y, lprConfig.DetectRegion.Width, lprConfig.DetectRegion.Height);
                        numAngle.Value = lprConfig.RotateAngle;
                    }

                    var virtualLoopConfig = NewtonSoftHelper<CameraConfig>.DeserializeObjectFromPath(IparkingingPathManagement.laneCameraLoopConfigPath(laneId, this.currentCamera!.Id));
                    if (virtualLoopConfig != null)
                    {
                        LoopConfig = virtualLoopConfig.DetectRegion == null ?
                            null : new Rectangle(virtualLoopConfig.DetectRegion.X, virtualLoopConfig.DetectRegion.Y, virtualLoopConfig.DetectRegion.Width, virtualLoopConfig.DetectRegion.Height);
                    }
                    break;
                }
            }
        }

        private void BtnLiveview_Click(object? sender, EventArgs e)
        {
            if (!IsExistCamera())
            {
                MessageBox.Show(KZUIStyles.CurrentResources.ChooseCameraRequired, KZUIStyles.CurrentResources.InfoTitle,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            btnDraw.Visible = true;
            btnCarLprDetect.Visible = true;
            btnMotorLprDetect.Visible = true;
            btnSave.Visible = true;
            btnClearConfig.Visible = true;
            btnVirtualLoop.Visible = true;

            foreach (var item in panelCamera.Controls.OfType<KZUI_UcCameraView>())
            {
                item.Stop();
                item.Dispose();
            }
            panelCamera.Controls.Clear();

            var uc = new KZUI_UcCameraView();
            uc.Init(currentCamera!, EmControlSizeMode.MEDIUM, iParkingv8.Object.Enums.Devices.EmCameraPurpose.Panorama, 1);
            uc.StartViewer(false, 1);
            uc.Font = panelCamera.Font;
            panelCamera.Controls.Add(uc);

            uc.Dock = DockStyle.Fill;
            uc.BringToFront();
        }

        private async void BtnDetectCarLpr_Click(object? sender, EventArgs e)
        {
            if (!IsExistCamera())
            {
                MessageBox.Show(KZUIStyles.CurrentResources.ChooseCameraRequired, KZUIStyles.CurrentResources.InfoTitle,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Image? image = ((KZUI_UcCameraView)panelCamera.Controls[0])!.GetFullCurrentImage();
            picCutVehicleImage.Image = ImageHelper.RotateImage(image, (int)numAngle.Value);
            if (image == null)
            {
                MessageBox.Show(KZUIStyles.CurrentResources.GetCameraImageError, KZUIStyles.CurrentResources.ErrorTitle,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (LprConfig != null)
            {
                Bitmap? bitmapCut = ImageHelper.CropBitmap((Bitmap)image, (Rectangle)LprConfig!);

                var result = await LprDetecter.GetPlateNumberAsync(bitmapCut, true, null, (int)numAngle.Value, false);
                lblDetectPlate.Text = result.PlateNumber;
                picCutVehicleImage.Image = ImageHelper.RotateImage(image, (int)numAngle.Value);
                picLprImage.Image = result.LprImage;
            }
            else
            {
                var result = await LprDetecter.GetPlateNumberAsync(image, true, null, (int)numAngle.Value, false);
                lblDetectPlate.Text = result.PlateNumber;
                picCutVehicleImage.Image = ImageHelper.RotateImage(image, (int)numAngle.Value);
                picLprImage.Image = result.LprImage;
            }
        }
        private async void BtnDetectMotorLpr_Click(object sender, EventArgs e)
        {
            if (!IsExistCamera())
            {
                MessageBox.Show(KZUIStyles.CurrentResources.ChooseCameraRequired, KZUIStyles.CurrentResources.InfoTitle,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Image? image = ((KZUI_UcCameraView)panelCamera.Controls[0])?.GetFullCurrentImage();
            if (image == null)
            {
                MessageBox.Show(KZUIStyles.CurrentResources.GetCameraImageError, KZUIStyles.CurrentResources.ErrorTitle,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (LprConfig != null)
            {
                Bitmap? bitmapCut = ImageHelper.CropBitmap((Bitmap)image, (Rectangle)LprConfig!);

                var result = await this.LprDetecter.GetPlateNumberAsync(bitmapCut, false, null, (int)numAngle.Value, false);
                lblDetectPlate.Text = result.PlateNumber;
                picCutVehicleImage.Image = ImageHelper.RotateImage(image, (int)numAngle.Value);
                picLprImage.Image = result.LprImage;
            }
            else
            {
                var result = await this.LprDetecter.GetPlateNumberAsync(image, false, null, (int)numAngle.Value, false);
                lblDetectPlate.Text = result.PlateNumber;
                picCutVehicleImage.Image = ImageHelper.RotateImage(image, (int)numAngle.Value);
                picLprImage.Image = result.LprImage;
            }
        }

        private void BtnDrawLprRegion_Click(object? sender, EventArgs e)
        {
            if (!IsExistCamera())
            {
                MessageBox.Show(KZUIStyles.CurrentResources.ChooseCameraRequired, KZUIStyles.CurrentResources.InfoTitle,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Image? image = ((KZUI_UcCameraView)panelCamera.Controls[0])?.GetFullCurrentImage();
            if (image == null)
            {
                MessageBox.Show(KZUIStyles.CurrentResources.GetCameraImageError, KZUIStyles.CurrentResources.ErrorTitle,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FrmCameraConfigSet frmCameraConfigSet = new(image, LprConfig);
            frmCameraConfigSet.ShowDialog();
            if (frmCameraConfigSet.DialogResult == DialogResult.OK)
            {
                this.LprConfig = frmCameraConfigSet.GetSaveConfig();
                panelCamera.Controls[0].Invalidate();
            }
        }
        private void BtnDrawVirtualLoopRegion_Click(object sender, EventArgs e)
        {
            if (!IsExistCamera())
            {
                MessageBox.Show(KZUIStyles.CurrentResources.ChooseCameraRequired, KZUIStyles.CurrentResources.InfoTitle,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Image? image = ((KZUI_UcCameraView)panelCamera.Controls[0])?.GetFullCurrentImage();
            if (image == null)
            {
                MessageBox.Show(KZUIStyles.CurrentResources.GetCameraImageError, KZUIStyles.CurrentResources.ErrorTitle,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FrmCameraVirtualLoopConfigSet frmCameraConfigSet = new(image, LoopConfig);
            frmCameraConfigSet.ShowDialog();
            if (frmCameraConfigSet.DialogResult == DialogResult.OK)
            {
                this.LoopConfig = frmCameraConfigSet.GetSaveConfig();
                panelCamera.Controls[0].Invalidate();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            CameraConfig lprConfig = new()
            {
                RotateAngle = (int)numAngle.Value,
                DetectRegion = this.LprConfig == null ? null : new CameraDetectRegion()
                {
                    X = this.LprConfig.Value.X,
                    Y = this.LprConfig.Value.Y,
                    Width = this.LprConfig.Value.Width,
                    Height = this.LprConfig.Value.Height,
                }
            };
            NewtonSoftHelper<CameraConfig>.SaveConfig(lprConfig, IparkingingPathManagement.laneCameraConfigPath(laneId, this.currentCamera!.Id));


            CameraConfig virtuaLoopConfig = new()
            {
                RotateAngle = (int)numAngle.Value,
                DetectRegion = this.LoopConfig == null ? null : new CameraDetectRegion()
                {
                    X = this.LoopConfig.Value.X,
                    Y = this.LoopConfig.Value.Y,
                    Width = this.LoopConfig.Value.Width,
                    Height = this.LoopConfig.Value.Height,
                }
            };
            NewtonSoftHelper<CameraConfig>.SaveConfig(virtuaLoopConfig, IparkingingPathManagement.laneCameraLoopConfigPath(laneId, this.currentCamera!.Id));
            MessageBox.Show(KZUIStyles.CurrentResources.SaveConfigSuccess, KZUIStyles.CurrentResources.InfoTitle,
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void BtnClearConfig_Click(object sender, EventArgs e)
        {
            string lprConfigPath = IparkingingPathManagement.laneCameraConfigPath(laneId, this.currentCamera!.Id);
            string loopConfigPath = IparkingingPathManagement.laneCameraLoopConfigPath(laneId, this.currentCamera!.Id);
            if (File.Exists(lprConfigPath))
            {
                File.Delete(lprConfigPath);
            }
            if (File.Exists(loopConfigPath))
            {
                File.Delete(loopConfigPath);
            }
            this.LprConfig = null;
            this.LoopConfig = null;

            MessageBox.Show(KZUIStyles.CurrentResources.ClearConfigSuccess, KZUIStyles.CurrentResources.InfoTitle,
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion End Controls In Form

        #region Private Function
        private bool IsExistCamera()
        {
            return currentCamera != null;
        }
        #endregion End Private Function

        private void InitProperties(IEnumerable<Camera> cameras, string laneId, ILpr LprDetecter)
        {
            this.cameras = cameras;
            this.laneId = laneId;
            this.LprDetecter = LprDetecter;
        }
        public void Translate()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(Translate);
                return;
            }

            lblCamera.Text = KZUIStyles.CurrentResources.Camera;
            lblAngle.Text = KZUIStyles.CurrentResources.Angle;
            btnLiveview.Text = KZUIStyles.CurrentResources.Liveview;

            btnCarLprDetect.Text = KZUIStyles.CurrentResources.CarLprDetect;
            btnMotorLprDetect.Text = KZUIStyles.CurrentResources.MotorLprDetect;

            btnSave.Text = KZUIStyles.CurrentResources.Save;
            btnClearConfig.Text = KZUIStyles.CurrentResources.Clear;
        }
        private void InitUI()
        {
            btnLiveview.Visible = false;

            foreach (Camera camera in this.cameras)
            {
                cbCamera.Items.Add(camera.Name);
            }

            cbCamera.SelectedIndexChanged += CbCamera_SelectedIndexChanged;
            btnLiveview.Click += BtnLiveview_Click;
            btnCarLprDetect.Click += BtnDetectCarLpr_Click;
            btnDraw.Click += BtnDrawLprRegion_Click;
        }
        private void ClearView()
        {
            foreach (var item in panelCamera.Controls.OfType<KZUI_UcCameraView>())
            {
                item.Stop();
                item.Dispose();
            }
            panelCamera.Controls.Clear();

            lblDetectPlate.Text = "";
            picLprImage.Image = null;
            picCutVehicleImage.Image = null;

            btnDraw.Visible = false;
            btnCarLprDetect.Visible = false;
            btnMotorLprDetect.Visible = false;
            btnSave.Visible = false;
            btnClearConfig.Visible = false;
            btnVirtualLoop.Visible = false;
        }
    }
}