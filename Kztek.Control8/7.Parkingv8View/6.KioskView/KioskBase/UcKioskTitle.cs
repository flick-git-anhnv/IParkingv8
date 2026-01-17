using Kztek.Object;
using static Kztek.Control8.UserControls.DialogUcs.KioskOut.UcLanguage;

namespace Kztek.Control8.UserControls.DialogUcs.KioskOut
{
    public partial class ucKioskTitle : UserControl
    {
        public ucKioskTitle()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
              ControlStyles.UserPaint |
              ControlStyles.DoubleBuffer, true);

            picLogo.Anchor = AnchorStyles.None; // Bỏ cố định
            picLogo.Left = (this.ClientSize.Width - picLogo.Width) / 2;
            this.SizeChanged += UcKioskTitle_SizeChanged;
        }

        private void UcKioskTitle_SizeChanged(object? sender, EventArgs e)
        {
            picLogo.Left = (this.ClientSize.Width - picLogo.Width) / 2;
        }

        public void Init(Image? oemImage, OnLanguageChangedEventHandler? onLanguageChangedEvent, EventHandler? onOpenSettingPage)
        {
            picLogo.Image = oemImage;
            this.ucLanguage1.OnLanguageChangedEvent += onLanguageChangedEvent;
            lblTime.DoubleClick += onOpenSettingPage;
            timerUpdateTime.Enabled = true;
            timerUpdateTime.Tick += TimerUpdateTime_Tick;
        }

        private void TimerUpdateTime_Tick(object? sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void picLogo_DoubleClick(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }

        public void NotifyCardEvent()
        {
            picCardEvent.BeginInvoke(new Action(() =>
            {
                picCardEvent.Visible = true;
                picLoopEvent.Visible = false;
            }));
        }
        public void NotifyLoopEvent()
        {
            picCardEvent.BeginInvoke(new Action(() =>
            {
                picCardEvent.Visible = false;
                picLoopEvent.Visible = true;
            }));
        }
        public void NotifyNoneEvent()
        {
            picCardEvent.BeginInvoke(new Action(() =>
            {
                picCardEvent.Visible = false;
                picLoopEvent.Visible = false;
            }));
        }
    }
}
