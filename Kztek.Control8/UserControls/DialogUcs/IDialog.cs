using iParkingv8.Object.ConfigObjects.LaneConfigs;

namespace Kztek.Control8.UserControls.DialogUcs
{
    public interface IDialog<Request, Result> where Request : class
                                              where Result : class
    {
        LaneOptionalConfig LaneOptionalConfig { get; set; }
        Task<Result> ShowDialog(Request request, Form parent);
        void Cancel();
    }

    public interface IKioskNotifyDialog<Request, Result> where Request : class
                                                         where Result : class
    {
        LaneOptionalConfig LaneOptionalConfig { get; set; }
        Task<Result> ShowDialog(Request request, Form? masked = null);
        void CloseDialog();
    }
}
