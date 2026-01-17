using iParkingv5.Controller;
using iParkingv5.Controller.KztekDevices.MT166_CardDispenser;
using iParkingv5.Controller.ZktecoDevices.PUSH;
using iParkingv8.Object.Enums.Parkings;
using iParkingv8.Object.Objects.Devices;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Ultility;
using IParkingv8.Forms;
using IParkingv8.UserControls;
using Kztek.Object.Entity.Device;
using Kztek.Tool;

namespace IParkingv8.Helpers
{
    public class ControllerHelper
    {
        public static async Task OpenBarrie(ILane lane, Collection collection, string deviceId, string baseLog)
        {
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLog} - Open Barrie"));
            if (collection.VehicleType == EmVehicleType.CAR && AppData.AppConfig.IsOpenAllBarrieForCar)
            {
                await OpenAllBarrie(lane);
            }
            else
            {
                await OpenBarrieByControllerId(deviceId, lane, collection.VehicleType);
            }
        }

        public static async Task OpenBarrieByControllerId(string controllerId, ILane ilane, EmVehicleType? vehicleType)
        {
            int retryOpenBarrieTimes = 3;
            if (string.IsNullOrEmpty(controllerId))
            {
                controllerId = ilane.Lane.ControlUnits.Where(e => e.Barriers.Count > 0)?.FirstOrDefault()?.Id ?? string.Empty;
                if (string.IsNullOrEmpty(controllerId))
                {
                    return;
                }
            }

            var controller = AppData.IControllers.Where(e => e.ControllerInfo.Id.Equals(controllerId, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (controller == null)
            {
                return;
            }

            var controlInLane = ilane.Lane.ControlUnits.Where(e => e.Id.Equals(controllerId, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (controlInLane == null || controlInLane.Barriers.Count == 0)
            {
                return;
            }

            foreach (var barrie in controlInLane.Barriers)
            {
                var configPath = IparkingingPathManagement.laneControllerBarrieConfigPath(ilane.Lane.Id, controllerId, barrie);
                var config = NewtonSoftHelper<BarrieOpenModeConfig>.DeserializeObjectFromPath(configPath) ??
                                new BarrieOpenModeConfig()
                                {
                                    BarrieIndex = barrie,
                                    OpenMode = EmBarrieOpenMode.ALL,
                                };
                bool isAllowOpenBarrie = false;
                if (vehicleType is null)
                {
                    isAllowOpenBarrie = true;
                }
                else
                {
                    isAllowOpenBarrie = config.OpenMode switch
                    {
                        EmBarrieOpenMode.ALL => true,
                        EmBarrieOpenMode.CAR_ONLY => vehicleType == EmVehicleType.CAR,
                        EmBarrieOpenMode.NOT_CAR => vehicleType != EmVehicleType.CAR,
                        _ => true,
                    };
                }

                if (!isAllowOpenBarrie)
                {
                    continue;
                }
                for (int i = 0; i < retryOpenBarrieTimes; i++)
                {
                    if (controller.GetType() == typeof(ZktecoPUSH))
                    {
                        FrmMain.AddCMD(controller.ControllerInfo.Code, barrie);
                    }
                    else
                    {
                        bool isOpenSuccess = await controller.OpenDoor(100, barrie);
                        if (isOpenSuccess)
                        {
                            break;
                        }
                    }
                }
            }
        }
        public static async Task OpenAllBarrie(ILane _iLane)
        {
            int retryOpenBarrieTimes = 3;
            foreach (IController controller in AppData.IControllers)
            {
                foreach (ControllerInLane controllerInLane in _iLane.Lane.ControlUnits)
                {
                    if (!controller.ControllerInfo.Id.Equals(controllerInLane.Id, StringComparison.CurrentCultureIgnoreCase))
                    {
                        continue;
                    }


                    if (controllerInLane.Barriers.Count == 0)
                    {
                        continue;
                    }

                    foreach (var barrie in controllerInLane.Barriers)
                    {
                        if (controller.GetType() == typeof(ZktecoPUSH))
                        {
                            FrmMain.AddCMD(controller.ControllerInfo.Code, barrie);
                        }
                        else
                            for (int i = 0; i < retryOpenBarrieTimes; i++)
                            {
                                bool isOpenSuccess = await controller.OpenDoor(100, barrie);
                                if (isOpenSuccess)
                                {
                                    SystemUtils.logger.SaveDeviceLog(
                                                new DeviceLog()
                                                {
                                                    DeviceId = controller.ControllerInfo.Id,
                                                    DeviceName = controller.ControllerInfo.Name,
                                                    Cmd = "Open Barrie " + barrie,
                                                    Description = "True"
                                                }
                                            );
                                    break;
                                }

                                SystemUtils.logger.SaveDeviceLog(
                                                new DeviceLog()
                                                {
                                                    DeviceId = controller.ControllerInfo.Id,
                                                    DeviceName = controller.ControllerInfo.Name,
                                                    Cmd = "Open Barrie " + barrie,
                                                    Description = "False"
                                                }
                                            );
                            }
                    }
                }
            }
        }

        public static async Task CollecCard(ILane iLane, string deviceId)
        {
            foreach (IController controller in AppData.IControllers)
            {
                if (controller is not ICardDispenser && controller is not ICardDispenserv2)
                {
                    continue;
                }

                foreach (ControllerInLane controllerInLane in iLane.Lane.ControlUnits)
                {
                    if (!controllerInLane.Id.Equals(deviceId, StringComparison.InvariantCultureIgnoreCase))
                    {
                        continue;
                    }
                    if (controllerInLane.Readers.Count > 0)
                    {
                        if (controller is ICardDispenser)
                        {
                            await ((ICardDispenser)controller).CollectCard();
                            continue;
                        }
                        else
                        {
                            await ((ICardDispenserv2)controller).CollectCard();
                            continue;
                        }
                    }
                }
            }
        }
        public static async Task RejectCard(ILane _iLane, string deviceId)
        {
            foreach (IController controller in AppData.IControllers)
            {
                if (controller is not ICardDispenser && controller is not ICardDispenserv2)
                {
                    continue;
                }
                foreach (ControllerInLane controllerInLane in _iLane.Lane.ControlUnits)
                {
                    if (!controllerInLane.Id.Equals(deviceId, StringComparison.InvariantCultureIgnoreCase))
                    {
                        continue;
                    }

                    if (controllerInLane.Readers.Count > 0)
                    {
                        if (controller is ICardDispenser)
                        {
                            await ((ICardDispenser)controller).RejectCard();
                            continue;
                        }
                        else
                        {
                            await ((ICardDispenserv2)controller).RejectCard();
                            continue;
                        }
                    }
                }
            }
        }
        public static async void RaLenhNhaThe(string controllerId)
        {
            foreach (IController controller in AppData.IControllers)
            {
                if (controller is not ICardDispenser && controller is not ICardDispenserv2)
                {
                    continue;
                }
                if (!controller.ControllerInfo.Id.Equals(controllerId, StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }
                if (controller is ICardDispenser)
                {
                    await ((ICardDispenser)controller).DispenseCard();
                }
                else
                {
                    await ((ICardDispenserv2)controller).DispenseCard();
                }
            }
        }

        public static async void RaLenhThuThe(string controllerId)
        {
            foreach (IController controller in AppData.IControllers)
            {
                if (controller is not ICardDispenserv2)
                {
                    continue;
                }
                if (!controller.ControllerInfo.Id.Equals(controllerId, StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }
                await ((ICardDispenserv2)controller).DispenseCardToRecycle();
            }
        }
    }
}
