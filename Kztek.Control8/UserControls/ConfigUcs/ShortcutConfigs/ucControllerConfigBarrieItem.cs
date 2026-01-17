namespace Kztek.Control8.UserControls.ConfigUcs.ShortcutConfigs
{
    public partial class ucControllerConfigBarrieItem : UserControl
    {
        #region Properties
        public int barrieIndex { get; set; }
        public Keys? keySet { get; set; }
        #endregion End Properties

        #region Forms
        public ucControllerConfigBarrieItem(int barrieIndex, Keys? keySet)
        {
            InitializeComponent();
            this.barrieIndex = barrieIndex;
            this.keySet = keySet;
            this.Load += UcControllerConfigBarrieItem_Load;
        }


        private void UcControllerConfigBarrieItem_Load(object? sender, EventArgs e)
        {
            picChangeConfig.Click += PicChangeConfig_MouseClick;
            lblBarrieName.Text = "Barrie " + this.barrieIndex;
            if (this.keySet != null && this.keySet != Keys.None)
            {
                lblCurrentConfig.Text = this.keySet.ToString();
            }
            else
            {
                lblCurrentConfig.Text = "Hãy cấu hình phím tắt";
            }
        }
        #endregion End Forms

        #region Controls In Form
        private void PicChangeConfig_MouseClick(object? sender, EventArgs e)
        {
            frmSetShortCutKey frm = new frmSetShortCutKey(Keys.Enter);
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                this.keySet = frm.KeySet;
                if (this.keySet != null && this.keySet != Keys.None)
                {
                    lblCurrentConfig.Text = this.keySet.ToString();
                }
                else
                {
                    lblCurrentConfig.Text = "Hãy cấu hình phím tắt";
                }
            }
            frm.Dispose();
        }
        #endregion End Controls In Form
    }
}
