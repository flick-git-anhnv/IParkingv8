using iParkingv8.Object.Enums;
using iParkingv8.Object.Enums.Parkings;
using iParkingv8.Object.Objects.Parkings;
using IParkingv8.UserControls;
using Kztek.Control8.Forms;

namespace IParkingv8.Helpers.CardProcess
{
    public class CardVipProcess
    {
        public static async Task<VipCardValidate> ValidateAccessKeyByCode(AccessKey accessKey, string detectPlate,
                                                                          ILane lane, UcSelectVehicles ucSelectVehicles)
        {
            VipCardValidate vipCardValidate = new()
            {
                VipCardValidateType = EmVipCardValidateType.CARD_NO_VEHICLE,
                RegisterVehicle = null,
                UpdatePlate = detectPlate
            };
            var plateRule = accessKey.Collection!.GetCheckPlateNumberMethod();
            var plateLevel = accessKey.Collection.GetPlateNumberValidationLevel();

            if (accessKey.Metrics == null || accessKey.Metrics.Count == 0)
            {
                //if (plateLevel == EmPlateNumberValidate.NONE || plateRule == EmPlateNumberMethod.ENTRY_PLATE_NUMBER)
                {
                    vipCardValidate.VipCardValidateType = EmVipCardValidateType.CARD_NO_VEHICLE;
                }
                return vipCardValidate;
            }

            var registeredVehicle = accessKey.Metrics.Where(e => e.Code == detectPlate).FirstOrDefault();

            if (registeredVehicle != null)
            {
                vipCardValidate.RegisterVehicle = registeredVehicle;
                vipCardValidate.IsCheckByPlate = true;
            }
            else
            {
                vipCardValidate.RegisterVehicle = accessKey.Metrics[0];
            }

            vipCardValidate.VipCardValidateType = EmVipCardValidateType.SUCCESS;

            if (accessKey.Metrics.Count == 1)
            {
                vipCardValidate.IsCheckByPlate = false;
                return vipCardValidate;
            }
            if (accessKey.Metrics.Count > 1 && plateLevel != EmPlateNumberValidate.NONE &&
                                               lane.Lane.auto_open_barrier != EmAutoOpenBarrier.ALWAYS)
            {
                vipCardValidate.IsCheckByPlate = true;

                if (registeredVehicle == null)
                {
                    AccessKey? selectedVehicle = null;
                    if (((Control)lane).InvokeRequired)
                    {
                        selectedVehicle = await ((Control)lane).Invoke(async () => await ucSelectVehicles.SelectVehicleAsync(accessKey.Metrics));
                    }
                    else
                    {
                        selectedVehicle = await ucSelectVehicles.SelectVehicleAsync(accessKey.Metrics);
                    }
                    if (selectedVehicle is null)
                    {
                        vipCardValidate.VipCardValidateType = EmVipCardValidateType.NOT_CONFIRM_VEHICLE;
                        return vipCardValidate;
                    }

                    vipCardValidate.UpdatePlate = selectedVehicle.Code;
                    vipCardValidate.RegisterVehicle = selectedVehicle;
                    return vipCardValidate;
                }
                else
                {
                    vipCardValidate.RegisterVehicle = registeredVehicle;
                }
            }
            return vipCardValidate;
        }
    }
}
