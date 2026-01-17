using iParkingv8.Object.Objects.Parkings;

namespace IParkingv8.Helpers.CardProcess
{
    public enum EmVipCardValidateType
    {
        SUCCESS,
        CARD_NO_VEHICLE,
        NOT_CONFIRM_VEHICLE
    }
    public class VipCardValidate
    {
        public EmVipCardValidateType VipCardValidateType { get; set; }
        public AccessKey? RegisterVehicle { get; set; } = null;
        public bool IsCheckByPlate { get; set; } = false;
        public string UpdatePlate { get; set; } = string.Empty;
    }
}