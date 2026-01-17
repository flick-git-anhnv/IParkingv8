using Kztek.Control8.UserControls;
using Kztek.Tool.LogHelpers.LocalData;

namespace IParkingv8.UserControls
{
    public interface ILaneIn : ILane
    {
        IKZUIEventImageListIn ucEventImageListIn { get; set; }
        tblEntryLog EntryLog { get; set; }
    }
}
