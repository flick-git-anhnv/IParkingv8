using iParkingv5.Objects.Events;
using iParkingv8.Object.ConfigObjects.LaneConfigs;
using iParkingv8.Object.Objects.Devices;
using Kztek.Control8.Forms;
using Kztek.Control8.UserControls;
using Kztek.Control8.UserControls.ucDataGridViewInfo;

namespace IParkingv8.UserControls
{
    public interface ILane
    {
        bool IsBusy { get; set; }
        KZUI_UcLaneTitle UcLaneTitle { get; set; }

        KZUI_UcCameraList UcCameraList { get; set; }

        KZUI_UcResult UcResult { get; set; }

        IKZUI_UcPlate UcPlateIn { get; set; }

        IDataInfo ucEventInfoNew { get; set; }

        KZUI_Function UcAppFunctions { get; set; }

        public Lane Lane { get; set; }
        List<InputEventArgs> LastInputEventDatas { get; set; }
        List<CardEventArgs> LastCardEventDatas { get; set; }

        void InitUI();
        Task OnNewEvent(EventArgs e);
        Task OnNewStatus(EventArgs e);
        void OnKeyPress(Keys keys);

        public LaneDisplayConfig GetLaneDisplayConfig();

        event OnChangeLaneEvent? OnChangeLaneEvent;
        UcSelectVehicles ucSelectVehicles { get; set; }
        void Stop();

        void ChangeLaneDirectionConfig(LaneDirectionConfig config);
        void LoadViewSetting(LaneDisplayConfig config);
    }
}
