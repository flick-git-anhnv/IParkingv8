using Kztek.Object.Entity.Device;

namespace Kztek.Control8.UserControls.ConfigUcs.ControllerConfigs
{
    public partial class UcControllerBarrieConfig : UserControl
    {
        #region Properties
        private readonly BarrieOpenModeConfig barrieOpenModeConfig;
        private readonly int barrieIndex;
        #endregion End Properties

        #region Forms
        public UcControllerBarrieConfig(BarrieOpenModeConfig config)
        {
            InitializeComponent();
            lblBarrieName.Text = "Barrie " + config.BarrieIndex;
            this.barrieIndex = config.BarrieIndex;
            this.barrieOpenModeConfig = config;
            cbBarrieOpenMode.Items.Clear();
            foreach (EmBarrieOpenMode item in Enum.GetValues(typeof(EmBarrieOpenMode)))
            {
                cbBarrieOpenMode.Items.Add(EmBarrieOpenModeExtension.ToDisplayString(item));
            }
            cbBarrieOpenMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBarrieOpenMode.SelectedIndex = (int)this.barrieOpenModeConfig.OpenMode;
        }
        #endregion End Forms

        #region Public Function
        public BarrieOpenModeConfig GetNewConfig()
        {
            return new BarrieOpenModeConfig()
            {
                BarrieIndex = this.barrieIndex,
                OpenMode = (EmBarrieOpenMode)cbBarrieOpenMode.SelectedIndex
            };
        }
        #endregion End Public Function
    }
}
