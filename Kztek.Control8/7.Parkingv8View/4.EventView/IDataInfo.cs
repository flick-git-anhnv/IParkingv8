using iParkingv8.Object.Objects.Devices;
using iParkingv8.Object.Objects.Parkings;
using static iParkingv8.Object.ConfigObjects.LaneConfigs.LaneDirectionConfig;

namespace Kztek.Control8.UserControls.ucDataGridViewInfo
{
    public interface IDataInfo
    {
        bool KZUI_IsDisplayMoney { get; set; }
        bool KZUI_IsDisplayTitle { get; set; }
        void ClearView();
        void DisplayEventInfo(DateTime? datetimein,
                              DateTime? datetimeout,
                              AccessKey? accessKey,
                              Collection? collection,
                              AccessKey? vehicle,
                              long fee = 0,
                              string note = "",
                              bool isDisplayTitle = true,
                              bool isDisplayCustomerInfo = true);
        EmControlSizeMode KZUI_ControlSizeMode { get; set; }
        void SetDuration(long duration);
    }

    public class DataHelper
    {
        public static IDataInfo CreateDataInfor(EmViewOption option, Lane lane)
        {
            IDataInfo ucView = null;

            switch (option)
            {
                case EmViewOption.OnlyData:
                    ucView = new ucEntryInfor(lane);
                    break;
                case EmViewOption.DataAndMoney:
                    ucView = new ucExitInfor(lane);
                    break;
            }
            if (ucView is not null)
            {
                ucView.KZUI_IsDisplayMoney = option == EmViewOption.DataAndMoney;
            }
            return ucView;
        }
    }
    public class DataInfoModel
    {
        public string Identity { get; set; } = "";
        public string VehicleType { get; set; } = "";
        public DateTime? DateTimeIn { get; set; }
        public DateTime? DateTimeOut { get; set; }
        public string PlateRegister { get; set; } = "";
        public string CustomerInfo { get; set; } = "";
        public DateTime? DateExpired { get; set; }
        public long? Money { get; set; } = 0;
    }
}
