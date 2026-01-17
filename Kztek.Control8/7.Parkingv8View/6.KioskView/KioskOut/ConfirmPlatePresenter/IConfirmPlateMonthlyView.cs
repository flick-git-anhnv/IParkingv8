using iParkingv8.Object.Objects.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kztek.Control8.KioskOut.ConfirmPlatePresenter
{
    public interface IConfirmPlateMonthlyView
    {
        event EventHandler OnBackClicked;

        // Các phương thức hiển thị dữ liệu và trạng thái
        void DisplayEventInfo(ExitData exitData, Image? displayImage, string errorMessage, Control parent);
        void CloseDialog();
        void ClearView();
        void CenterControlLocation();

        public void SetTitleText(string text);
        public void SetSubTitleText(string text);

        public void SetAccessKeyNameTitleText(string text);
        public void SetTimeInTitleText(string text);
        public void SetPlateInTitleText(string text);
        public void SetRegisterPlateTitleText(string text);
        public void SetVehicleTypeTitleText(string text);
        public void SetTimeOutTitleText(string text);
        public void SetPlateOutTitleText(string text);
        public void SetCustomerTitleText(string text);

        public void SetBtnBackToMainText(string text);
    } 
}