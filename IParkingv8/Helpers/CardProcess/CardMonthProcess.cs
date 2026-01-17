using iParkingv8.Object.Enums;
using iParkingv8.Object.Enums.Parkings;
using iParkingv8.Object.Objects.Parkings;
using IParkingv8.UserControls;
using Kztek.Control8.Forms;

namespace IParkingv8.Helpers.CardProcess
{
    public class LastPlateValidate
    {
        //AccessKeyCode,LastPlateValidateDetail
        public Dictionary<string, LastPlateValidateDetail> Data { get; set; } = new Dictionary<string, LastPlateValidateDetail>();
    }

    public class LastPlateValidateDetail
    {
        public AccessKey? LastVehicle { get; set; }
        public string PlateNumber { get; set; } = string.Empty;
        public DateTime LastSelectTime { get; set; }
    }
    public class CardMonthProcess
    {
        //LaneId, LastPlateValidate
        public static Dictionary<string, LastPlateValidate> LastPlateValidates = new Dictionary<string, LastPlateValidate>();
        public static async Task<MonthCardValidate> ValidateAccessKeyByCode(AccessKey accessKey, string detectPlate,
                                                                            ILane lane, UcSelectVehicles ucSelectVehicles)
        {
            MonthCardValidate monthCardValidate = new()
            {
                MonthCardValidateType = EmMonthCardValidateType.CARD_NO_VEHICLE,
                RegisterVehicle = null,
            };
            monthCardValidate.UpdatePlate = detectPlate;
            var plateRule = accessKey.Collection!.GetCheckPlateNumberMethod();
            var plateLevel = accessKey.Collection.GetPlateNumberValidationLevel();

            if (accessKey.Metrics == null || accessKey.Metrics.Count == 0)
            {
                monthCardValidate.MonthCardValidateType = EmMonthCardValidateType.CARD_NO_VEHICLE;
                return monthCardValidate;
            }

            var registeredVehicle = accessKey.Metrics.Where(e => e.Code == detectPlate).FirstOrDefault();

            if (registeredVehicle != null)
            {
                monthCardValidate.RegisterVehicle = registeredVehicle;
                monthCardValidate.IsCheckByPlate = true;
            }
            else
            {
                monthCardValidate.RegisterVehicle = accessKey.Metrics[0];
            }

            monthCardValidate.MonthCardValidateType = EmMonthCardValidateType.SUCCESS;

            if (accessKey.Metrics.Count == 1)
            {
                monthCardValidate.IsCheckByPlate = false;
                return monthCardValidate;
            }
            else if (accessKey.Metrics.Count > 1 &&
                     plateLevel != EmPlateNumberValidate.NONE &&
                     lane.Lane.auto_open_barrier != EmAutoOpenBarrier.ALWAYS)
            {
                monthCardValidate.IsCheckByPlate = true;

                if (registeredVehicle == null)
                {
                    if (LastPlateValidates.ContainsKey(lane.Lane.Id))
                    {
                        if (LastPlateValidates[lane.Lane.Id].Data != null && LastPlateValidates[lane.Lane.Id].Data.ContainsKey(accessKey.Code))
                        {
                            DateTime lastSelectTime = LastPlateValidates[lane.Lane.Id].Data[accessKey.Code].LastSelectTime;
                            if ((DateTime.Now - lastSelectTime).TotalSeconds < 30)
                            {
                                monthCardValidate.UpdatePlate = LastPlateValidates[lane.Lane.Id].Data[accessKey.Code].PlateNumber;
                                monthCardValidate.RegisterVehicle = LastPlateValidates[lane.Lane.Id].Data[accessKey.Code].LastVehicle;
                                return monthCardValidate;
                            }
                            else
                            {
                                LastPlateValidates[lane.Lane.Id].Data.Remove(accessKey.Code);
                            }
                        }
                    }

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
                        monthCardValidate.MonthCardValidateType = EmMonthCardValidateType.NOT_CONFIRM_VEHICLE;
                        return monthCardValidate;
                    }

                    monthCardValidate.UpdatePlate = selectedVehicle.Code;
                    monthCardValidate.RegisterVehicle = selectedVehicle;
                    if (!LastPlateValidates.ContainsKey(lane.Lane.Id))
                    {
                        LastPlateValidates.Add(lane.Lane.Id, new LastPlateValidate());
                    }
                    if (!LastPlateValidates[lane.Lane.Id].Data.ContainsKey(accessKey.Code))
                    {
                        LastPlateValidates[lane.Lane.Id].Data.Add(accessKey.Code, new LastPlateValidateDetail
                        {
                            LastVehicle = monthCardValidate.RegisterVehicle,
                            PlateNumber = detectPlate,
                            LastSelectTime = DateTime.Now
                        });
                    }
                    return monthCardValidate;
                }
                else
                {
                    if (!LastPlateValidates.ContainsKey(lane.Lane.Id))
                    {
                        LastPlateValidates.Add(lane.Lane.Id, new LastPlateValidate());
                    }
                    if (!LastPlateValidates[lane.Lane.Id].Data.ContainsKey(accessKey.Code))
                    {
                        LastPlateValidates[lane.Lane.Id].Data.Add(accessKey.Code, new LastPlateValidateDetail
                        {
                            LastVehicle = registeredVehicle,
                            PlateNumber = detectPlate,
                            LastSelectTime = DateTime.Now
                        });
                    }
                    else
                    {
                        if (LastPlateValidates[lane.Lane.Id].Data[accessKey.Code].PlateNumber != detectPlate)
                        {
                            LastPlateValidates[lane.Lane.Id].Data[accessKey.Code].LastVehicle = registeredVehicle;
                            LastPlateValidates[lane.Lane.Id].Data[accessKey.Code].PlateNumber = detectPlate;
                            LastPlateValidates[lane.Lane.Id].Data[accessKey.Code].LastSelectTime = DateTime.Now;
                        }
                    }
                    monthCardValidate.RegisterVehicle = registeredVehicle;
                }

            }
            return monthCardValidate;
        }
    }
}
