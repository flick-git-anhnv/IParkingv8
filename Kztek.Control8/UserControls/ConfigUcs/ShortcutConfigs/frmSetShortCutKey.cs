namespace Kztek.Control8.UserControls.ConfigUcs.ShortcutConfigs
{
    public partial class frmSetShortCutKey : Form
    {
        #region PROPERTIES
        private Keys keySet;
        public Keys KeySet
        {
            get => keySet; set
            {
                keySet = value;
                lblCurrentKeySetValue.Text = keySet.ToString();
            }
        }
        #endregion END PROPERTIES

        #region FORMS
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            this.KeySet = keyData;
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public frmSetShortCutKey(Keys keySet)
        {
            InitializeComponent();
            this.KeySet = keySet;
            this.Load += FrmSetShortCutKey_Load;
        }

        private void FrmSetShortCutKey_Load(object? sender, EventArgs e)
        {
            btnConfirm.Click += BtnOk1_Click;
            btnCancel.Click += BtnCancel_Click;

            lblTitle.Padding = new Padding(12 * 2);
        }

        private void BtnOk1_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            this.keySet = Keys.None;
            this.DialogResult = DialogResult.OK;
        }

        #endregion END FORMS
    }
}
