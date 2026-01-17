using Kztek.Control8.KioskOut.PaymentPresenter.VISA;

namespace Kztek.Control8.UserControls.DialogUcs.KioskOut
{
    public partial class UcVisaView : UserControl, IVisaView
    {
        private readonly MaskedUserControl dialog;
        public UcVisaView(MaskedUserControl dialog)
        {
            InitializeComponent();
            this.dialog = dialog;
        }

        public event EventHandler OnBackClicked
        {
            add { btnBack.Click += value; }
            remove { btnBack.Click -= value; }
        }

        public void GotResult()
        {
            this.Hide();
            this.dialog.Hide();
        }

        public void ShowView()
        {
            this.Visible = true;
            this.BringToFront();
            //this.dialog.ShowForm(this);
        }

        public void SetLblTransactionTitle(string text) => lblTransationTitle.Text = text;
        public void SetLblTransactionId(string text) => lblTransactionId.Text = text;
        public void SetLblTitle(string text) => lblTitle.Text = text;
        public void SetLblGuide(string text) => lblGuide.Text = text;

        public void SetBtnBackText(string text) => btnBack.Text = text;
    }
}
