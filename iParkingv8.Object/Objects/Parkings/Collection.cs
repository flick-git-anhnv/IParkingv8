using iParkingv8.Object.Enums.ParkingEnums;
using iParkingv8.Object.Enums.Parkings;
using iParkingv8.Object.Objects.Bases;
using iParkingv8.Ultility.Style;

namespace iParkingv8.Object.Objects.Parkings
{
    public class Collection
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public bool Enabled { get; set; } = false;


        public EmVehicleType VehicleType { get; set; }
        public List<Attributes> Attributes { get; set; } = [];
        public string GetAttributeValue(string code)
        {
            foreach (var item in Attributes)
            {
                if (item.Code.Equals(code, StringComparison.CurrentCultureIgnoreCase))
                {
                    return item.Value;
                }
            }
            return string.Empty;
        }

        public EmPlateNumberValidate GetPlateNumberValidationLevel()
        {
            try
            {
                string config = GetAttributeValue("plate_number.validation.level");
                EmPlateNumberValidate result = string.IsNullOrEmpty(config) ?
                    EmPlateNumberValidate.APPROXIMATELY :
                    (EmPlateNumberValidate)Enum.Parse(typeof(EmPlateNumberValidate), config, true);
                return result;
            }
            catch (Exception)
            {
                return EmPlateNumberValidate.APPROXIMATELY;
            }
        }
        public EmPlateNumberMethod GetCheckPlateNumberMethod()
        {
            try
            {
                string config = GetAttributeValue("plate_number.validation.method");
                EmPlateNumberMethod result = string.IsNullOrEmpty(config) ? EmPlateNumberMethod.BOTH : (EmPlateNumberMethod)Enum.Parse(typeof(EmPlateNumberMethod), config, true);
                return result;
            }
            catch (Exception)
            {
                return EmPlateNumberMethod.BOTH;
            }
        }

        public EmVehicleType GetVehicleType()
        {
            return VehicleType;
        }
        public EmAccessKeyGroupType GetAccessKeyGroupType()
        {
            try
            {
                string config = GetAttributeValue("type");
                if (string.IsNullOrEmpty(config))
                {
                    return EmAccessKeyGroupType.UNKNOWN;
                }
                EmAccessKeyGroupType result = EmAccessKeyGroupType.DAILY;
                _ = Enum.TryParse(config, out result);
                return result;
            }
            catch (Exception)
            {
                return EmAccessKeyGroupType.UNKNOWN;
            }

        }
        public string GetAccessKeyGroupTypeName()
        {
            EmAccessKeyGroupType type = GetAccessKeyGroupType();
            return type switch
            {
                EmAccessKeyGroupType.MONTHLY => KZUIStyles.CurrentResources.CollectionMonth,
                EmAccessKeyGroupType.DAILY => KZUIStyles.CurrentResources.CollectionDaily,
                EmAccessKeyGroupType.VIP => KZUIStyles.CurrentResources.CollectionVIP,
                _ => "",
            };
        }

        public string GetVehicleTypeName()
        {
            try
            {
                var vehicleType = GetVehicleType();
                return vehicleType switch
                {
                    EmVehicleType.CAR => KZUIStyles.CurrentResources.VehicleTypeCar,
                    EmVehicleType.MOTORBIKE => KZUIStyles.CurrentResources.VehicleTypeMotor,
                    EmVehicleType.BIKE => KZUIStyles.CurrentResources.VehicleTypeBike,
                    _ => "",
                };
            }
            catch (Exception)
            {
                return "";
            }
        }
        public bool GetEntryByLoop()
        {
            try
            {
                string config = GetAttributeValue("entry_by_loop");
                return Convert.ToBoolean(config);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool GetExitByLoop()
        {
            try
            {
                string config = GetAttributeValue("exit_by_loop");
                return Convert.ToBoolean(config);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<string>? GetActiveLanes()
        {
            try
            {
                string config = GetAttributeValue("lanes");
                if (string.IsNullOrEmpty(config))
                {
                    return [];
                }
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(config);
            }
            catch (Exception)
            {
                return [];
            }
        }
    }
}
