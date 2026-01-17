using iParkingv8.Object.ConfigObjects.LaneConfigs;
using IParkingv8.UserControls;

namespace Kztek.Control8.UserControls.ConfigUcs.LaneConfigs
{
    public interface IPreviewControl
    {
        void RefreshConfig(LaneDirectionConfig config);
        void Init(ILane lane);
    }
}
