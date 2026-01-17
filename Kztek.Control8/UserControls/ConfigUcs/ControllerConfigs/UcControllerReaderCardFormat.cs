using iParkingv8.Object;
using iParkingv8.Object.Objects.Bases;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Ultility.Style;
using Kztek.Object;
using static Kztek.Object.CardFormat;

namespace Kztek.Control8.UserControls.ConfigUcs.ControllerConfigs
{
    public partial class UcControllerReaderCardFormat : UserControl
    {
        #region Properties
        private readonly CardFormatConfig cardFormatConfig;
        private readonly int readerIndex;
        private readonly List<Collection> collections;
        #endregion End Properties

        #region Forms
        public UcControllerReaderCardFormat(CardFormatConfig config, List<Collection> accessKeyCollection)
        {
            InitializeComponent();
            Translate();
            lblReaderName.Text = "Reader " + config.ReaderIndex;
            this.collections = accessKeyCollection;
            this.readerIndex = config.ReaderIndex;
            this.cardFormatConfig = config;
            txtCloseBarrieIndex.Text = config.CloseBarrieIndex;
            foreach (EmCardFormat item in Enum.GetValues(typeof(CardFormat.EmCardFormat)))
            {
                cbOutputFormat.Items.Add(CardFormat.ToString(item));
            }
            foreach (EmCardFormatOption item in Enum.GetValues(typeof(CardFormat.EmCardFormatOption)))
            {
                cbConfigOption.Items.Add(item.ToDisplayString());
            }

            cbOutputFormat.SelectedIndex = (int)this.cardFormatConfig.OutputFormat;
            cbConfigOption.SelectedIndex = (int)this.cardFormatConfig.OutputOption;

            cbIdentityGroup.Items.Add(new ListItem() { Name = "", Value = "" });
            foreach (var item in this.collections)
            {
                ListItem li = new()
                {
                    Name = item.Id.ToString(),
                    Value = item.Name
                };
                cbIdentityGroup.Items.Add(li);
            }
            cbIdentityGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            cbIdentityGroup.SelectedIndex = 0;
            cbIdentityGroup.DisplayMember = "Value";
            cbIdentityGroup.ValueMember = "Name";

            if (!string.IsNullOrEmpty(this.cardFormatConfig.CardGroupId))
            {
                for (int i = 0; i < cbIdentityGroup.Items.Count; i++)
                {
                    if (((ListItem)cbIdentityGroup.Items[i]!).Name == this.cardFormatConfig.CardGroupId)
                    {
                        cbIdentityGroup.SelectedIndex = i;
                        break;
                    }
                }
            }

            cbDailyCardRegister.Items.Add(new ListItem() { Name = "", Value = "" });
            foreach (var item in this.collections)
            {
                ListItem li = new()
                {
                    Name = item.Id.ToString(),
                    Value = item.Name
                };
                cbDailyCardRegister.Items.Add(li);
            }
            cbDailyCardRegister.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDailyCardRegister.SelectedIndex = 0;
            cbDailyCardRegister.DisplayMember = "Value";
            cbDailyCardRegister.ValueMember = "Name";

            if (!string.IsNullOrEmpty(this.cardFormatConfig.DailyCardGroupIdManual))
            {
                for (int i = 0; i < cbDailyCardRegister.Items.Count; i++)
                {
                    if (((ListItem)cbDailyCardRegister.Items[i]!).Name == this.cardFormatConfig.DailyCardGroupIdManual)
                    {
                        cbDailyCardRegister.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
        #endregion End Forms

        #region Controls In Form

        #endregion End Controls In Form

        #region Private Function

        #endregion End Private Function

        #region Public Function
        public CardFormatConfig GetNewConfig()
        {
            return new CardFormatConfig()
            {
                ReaderIndex = this.readerIndex,
                OutputFormat = (EmCardFormat)cbOutputFormat.SelectedIndex,
                OutputOption = (EmCardFormatOption)cbConfigOption.SelectedIndex,
                CardGroupId = cbIdentityGroup.SelectedItem == null ? "" : ((ListItem)cbIdentityGroup.SelectedItem).Name,
                DailyCardGroupIdManual = cbDailyCardRegister.SelectedItem == null ? "" : ((ListItem)cbDailyCardRegister.SelectedItem).Name,
                CloseBarrieIndex = txtCloseBarrieIndex.Text,
            };
        }
        #endregion End Public Function

        public void Translate()
        {
            lblOutputFormatTitle.Text = KZUIStyles.CurrentResources.CardOutputFormat;
            lblAdditionalTitle.Text = KZUIStyles.CurrentResources.CardOutputAdditional;
            lblAccessKeyCollectionTitle.Text = KZUIStyles.CurrentResources.AccessKeyCollection;
        }
    }
}
