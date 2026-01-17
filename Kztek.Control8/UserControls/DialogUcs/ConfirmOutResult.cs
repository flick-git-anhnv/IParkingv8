using iParkingv8.Object.Objects.Events;
using iParkingv8.Object.Objects.Kiosk;

namespace Kztek.Control8.UserControls.DialogUcs
{
    public class ConfirmOutResult : BaseDialogResult
    {
        public string UpdatePlate { get; set; } = string.Empty;
        public ExitData? ExitData { get; set; }
        public bool IsPayByQR { get; set; } = false;
    }
}
