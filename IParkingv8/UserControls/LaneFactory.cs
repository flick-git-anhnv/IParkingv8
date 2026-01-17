using iParkingv8.Object.ConfigObjects.LaneConfigs;
using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Objects.Devices;
using IParkingv8.LaneUIs;
using IParkingv8.LaneUIs._1920x1080;
using IParkingv8.LaneUIs.In;
using IParkingv8.LaneUIs.KioskIn;
using IParkingv8.LaneUIs.KioskOut;
using IParkingv8.LaneUIs.Out;
using Kztek.Control8.UserControls;
using RabbitMQ.Client;

namespace IParkingv8.UserControls
{
    public class LaneFactory
    {
        public static ILane? CreateLane(Lane lane, int laneCount, LaneDisplayConfig? laneDisplay, IModel? channel, KZUI_UcEventRealtime ucEventRealtime)
        {
            // Fixme: size
            var screenWidth = Screen.AllScreens[0].Bounds.Width;
            switch ((EmLaneType)lane.Type)
            {
                case EmLaneType.LANE_IN:
                    if (laneCount <= 1)
                    {
                        if (screenWidth < 1366)
                        {
                            return new UcInSmallSize(lane, ucEventRealtime);
                        }
                        else if (screenWidth >= 1366 && screenWidth <= 2000)
                        {
                            return new UcInMediumSize(lane, ucEventRealtime);
                        }
                        else if (screenWidth > 2000)
                        {
                            return new UcInMediumSize(lane, ucEventRealtime);
                        }
                    }
                    else if (laneCount == 2)
                    {
                        if (screenWidth <= 1366)
                        {
                            return new UcInMediumSmallSize(lane, ucEventRealtime);
                        }
                        else
                        {
                            return new UcInMediumSize(lane, ucEventRealtime);
                        }
                    }
                    else
                        return new UcInSmallSize(lane, ucEventRealtime);
                    break;
                case EmLaneType.LANE_OUT:
                    if (laneCount <= 1)
                    {
                        if (screenWidth < 1366)
                        {
                            return new UcOutSmallSize(lane, laneDisplay, ucEventRealtime);
                        }
                        else if (screenWidth >= 1366 && screenWidth <= 2000)
                        {
                            return new UcOutMediumSize(lane, laneDisplay, ucEventRealtime);
                        }
                        else if (screenWidth > 2000)
                        {
                            return new UcOutMediumSize(lane, laneDisplay, ucEventRealtime);
                        }
                    }
                    else if (laneCount == 2)
                    {
                        if (screenWidth <= 1366)
                        {
                            return new UcOutMediumSmallSize(lane, laneDisplay, ucEventRealtime);
                        }
                        else
                        {
                            return new UcOutMediumSize(lane, laneDisplay, ucEventRealtime);
                        }
                    }
                    else
                        return new UcOutSmallSize(lane, laneDisplay, ucEventRealtime);
                    break;
                case EmLaneType.KIOSK_IN:
                    //return new frmBaseLaneKioskIn(lane, laneDisplay);
                    return new UcKioskIn(lane);
                case EmLaneType.KIOSK_OUT:
                    return new UcKioskOut(lane);
            }
            return null;
        }
    }
}
