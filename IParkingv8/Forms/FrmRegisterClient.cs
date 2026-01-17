using iParkingv5.Controller;
using iParkingv5.Objects.Events;
using iParkingv8.Object.Enums.ParkingEnums;
using iParkingv8.Object.Objects.Bases;
using iParkingv8.Object.Objects.Devices;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Ultility;
using Kztek.Object;
using Kztek.Tool;

namespace IParkingv8.Forms
{
    public partial class FrmRegisterClient : Form
    {
        public static string preferCollectionId = "";
        public static string preferCollectionName = "";
        public EmAccessKeyType AccessKeyType = EmAccessKeyType.CARD;
        public class InforIn
        {
            public string Name { get; set; } = string.Empty;
            public string PlateNumber { get; set; } = string.Empty;
            public string Reason { get; set; } = string.Empty;
            public EmCustomerType CustomerType { get; set; }
        }
        public enum EmCustomerType
        {
            Bike,
            Vehicle
        }
        public static class CustomerType
        {
            public static string ToDisplayString(EmCustomerType customerType)
            {
                return customerType switch
                {
                    EmCustomerType.Bike => "Đi bộ",
                    EmCustomerType.Vehicle => "Ô tô",
                    _ => string.Empty,
                };
            }
        }

        public Lane lane { get; set; }
        public string SelectIdentity
        {
            get;
            set;
        } = string.Empty;
        public string SelectIdentityId { get; set; } = string.Empty;
        public string ControllerId { get; set; } = string.Empty;
        public int Reader { get; set; }
        public iParkingv5.Controller.IController Controller;
        public InforIn? RegisterCustomerInfo { get; set; }
        public FrmRegisterClient(Lane lane, iParkingv5.Controller.IController controller, string detectPlate)
        {
            InitializeComponent();

            this.lane = lane;
            this.Controller = controller;

            foreach (var item in AppData.DailyAccessKeyCollections)
            {
                cbAccessKeyColleciton.Items.Add(new ListItem()
                {
                    Name = item.Name,
                    Value = item.Id,
                });
            }
            cbAccessKeyColleciton.DisplayMember = "Name";
            cbAccessKeyColleciton.ValueMember = "Value";
            this.KeyPreview = true;

            cbReason.SelectedIndex = 1;
            cbCustomerType.SelectedIndex = 1;
            txbPlateNumber.Text = detectPlate;
            StartControllers();
            this.FormClosed += FrmRegisterIn_FormClosed;
            this.KeyDown += FrmRegisterClient_KeyDown;
            cbAccessKeyColleciton.SelectedIndexChanged += CbAccessKeyColleciton_SelectedIndexChanged;
            if (!string.IsNullOrEmpty(preferCollectionName))
            {
                cbAccessKeyColleciton.SelectedIndex = cbAccessKeyColleciton.FindStringExact(preferCollectionName);
            }
            else
                cbAccessKeyColleciton.SelectedIndex = cbAccessKeyColleciton.Items.Count > 0 ? 0 : -1;
        }

        private void FrmRegisterClient_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnConfirm.PerformClick();
            }
        }

        private void CbAccessKeyColleciton_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cbAccessKeyColleciton.SelectedItem is null)
            {
                return;
            }
            preferCollectionName = cbAccessKeyColleciton.Text;
            preferCollectionId = ((ListItem)cbAccessKeyColleciton.SelectedItem).Value;
        }

        private void FrmRegisterIn_FormClosed(object? sender, FormClosedEventArgs e)
        {
            CloseController();
        }

        private async void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SelectIdentity))
            {
                if (string.IsNullOrEmpty(txbPlateNumber.Text))
                {
                    MessageBox.Show("Hãy nhập thông tin biển số đăng ký", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                var fakeIdentity = new AccessKey()
                {
                    Code = txbPlateNumber.Text,
                    Name = txbPlateNumber.Text + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + "Temp",
                    Collection = new Collection()
                    {
                        Id = preferCollectionId,
                    },
                    Type = EmAccessKeyType.VEHICLE,
                };
                var response = await AppData.ApiServer.DataService.AccessKey.CreateAsync(fakeIdentity);
                if (response == null || response.Item1 == null || string.IsNullOrEmpty(response.Item1.Id))
                {
                    MessageBox.Show("Đăng ký không thành công, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                this.AccessKeyType = EmAccessKeyType.VEHICLE;
                this.SelectIdentity = response.Item1.Code;
                this.SelectIdentityId = response.Item1.Id;
                this.Reader = -1;
                RegisterCustomerInfo = new()
                {
                    Name = txbName.Text,
                    PlateNumber = txbPlateNumber.Text,
                    Reason = cbReason.Text,
                    CustomerType = (EmCustomerType)cbCustomerType.SelectedIndex,
                };

                CloseController();

                this.DialogResult = DialogResult.OK;
                return;
            }

            this.AccessKeyType = EmAccessKeyType.CARD;
            RegisterCustomerInfo = new()
            {
                Name = txbName.Text,
                PlateNumber = txbPlateNumber.Text,
                Reason = cbReason.Text,
                CustomerType = (EmCustomerType)cbCustomerType.SelectedIndex,
            };

            CloseController();

            this.DialogResult = DialogResult.OK;
        }

        private void Controller_CardEvent(object sender, CardEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                try
                {
                    ControllerId = e.DeviceId;
                    Reader = e.ReaderIndex;

                    //var configPath = IparkingingPathManagement.laneControllerReaderConfigPath(this.lane.Id, e.DeviceId, e.ReaderIndex);
                    //var config = NewtonSoftHelper<CardFormatConfig>.DeserializeObjectFromPath(configPath) ??
                    //                new CardFormatConfig()
                    //                {
                    //                    ReaderIndex = e.ReaderIndex,
                    //                    OutputFormat = CardFormat.EmCardFormat.HEXA,
                    //                    OutputOption = CardFormat.EmCardFormatOption.Min_8,
                    //                };
                    //// Thêm option standard lại card
                    //e.PreferCard = CardFactory.StandardlizedCardNumber(e.PreferCard, config);

                    //while (e.PreferCard.Length < 8)
                    //{
                    //    e.PreferCard = "0" + e.PreferCard;
                    //}
                    //e.PreferCard = baseCard;
                    string baseCard = e.PreferCard;

                    lblLoadingStatus.Text = $"{DateTime.Now:HH:mm:ss} READER: {e.ReaderIndex} Button: {e.ButtonIndex}, CARD: {e.PreferCard} Controller: " + e.DeviceName;

                    txbCardNumber.Text = baseCard;
                    txbReader.Text = e.ReaderIndex.ToString();

                    SelectIdentity = baseCard;
                }
                catch (Exception ex)
                {
                }
            }));
        }
        private void Controller_ErrorEvent(object sender, ControllerErrorEventArgs e)
        {
            SystemUtils.logger.SaveDeviceLog(new DeviceLog()
            {
                DeviceName = e.DeviceName,
                Cmd = e.CMD,
                Description = e.ErrorString,
                Response = e.ErrorFunc,
            });
            lblLoadingStatus.BeginInvoke(new Action(() =>
            {
                lblLoadingStatus.Text = $"Nhận sự kiện Error {e.CMD} Controller " + e.DeviceName;
            }));
        }

        private void CloseController()
        {
            this.Controller.CardEvent -= Controller_CardEvent;
            this.Controller.ErrorEvent -= Controller_ErrorEvent;
        }
        private void StartControllers()
        {
            this.Controller.CardEvent += Controller_CardEvent;
            this.Controller.ErrorEvent += Controller_ErrorEvent;
        }
    }
}