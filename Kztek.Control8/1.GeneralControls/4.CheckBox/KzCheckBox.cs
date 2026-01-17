using Guna.UI2.WinForms;
using iParkingv8.Ultility;

namespace Kztek.Control8._1.GeneralControls._4.CheckBox
{
    public class KzCheckBox : Guna2CheckBox
    {
        public KzCheckBox() : base()
        {
            this.CheckedState.BorderColor = CheckBoxColorManagement.CHECKED_STATE_BORDER_COLOR;
            this.CheckedState.FillColor = CheckBoxColorManagement.CHECKED_STATE_FILL_COLOR;
            this.CheckedState.BorderRadius = 2;
            this.CheckedState.BorderThickness = 1;

            this.UncheckedState.BorderColor = CheckBoxColorManagement.UNCHECKED_STATE_BORDER_COLOR;
            this.UncheckedState.FillColor = CheckBoxColorManagement.UNCHECKED_STATE_FILL_COLOR;
            this.UncheckedState.BorderThickness = 1;
            this.UncheckedState.BorderRadius = 2;
            this.Font = new Font("Segoe UI", 12F);
        }
    }
}
