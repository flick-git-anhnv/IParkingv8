using Kztek.Control8.UserControls;

namespace IParkingv8.UserControls
{
    public interface ILaneOut : ILane
    {
        IKZUIEventImageListOut UcEventImageListOut { get; set; }
        IKZUI_UcPlate UcPlateOut { get; set; }
    }
}
