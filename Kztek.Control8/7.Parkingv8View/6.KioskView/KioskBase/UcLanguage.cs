using iParkingv8.Ultility.Style;
using System.Globalization;

namespace Kztek.Control8.UserControls.DialogUcs.KioskOut
{
    public partial class UcLanguage : UserControl
    {
        public delegate void OnLanguageChangedEventHandler();

        public event OnLanguageChangedEventHandler? OnLanguageChangedEvent;
        private readonly Color ActiveColor = Color.FromArgb(242, 102, 51);
        private readonly Color DisableColor = Color.White;

        public UcLanguage()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
              ControlStyles.UserPaint |
              ControlStyles.DoubleBuffer, true);

            btnVi.Click += BtnVi_Click;
            btnEn.Click += BtnEn_Click;
        }

        private void BtnEn_Click(object? sender, EventArgs e)
        {
            CultureInfo culture = new("en-US");
            KZUIStyles.CultureInfo = culture;
            btnVi.FillColor = DisableColor;
            btnVi.ForeColor = Color.Black;
            btnEn.FillColor = ActiveColor;
            btnEn.ForeColor = Color.White;
            OnLanguageChangedEvent?.Invoke();
        }
        private void BtnVi_Click(object? sender, EventArgs e)
        {
            CultureInfo culture = new("vi-VN");
            KZUIStyles.CultureInfo = culture;
            btnVi.FillColor = ActiveColor;
            btnVi.ForeColor = Color.White;
            btnEn.FillColor = DisableColor;
            btnEn.ForeColor = Color.Black;
            OnLanguageChangedEvent?.Invoke();
        }
    }
}
