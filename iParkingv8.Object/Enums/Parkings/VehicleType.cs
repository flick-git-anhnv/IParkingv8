using iParkingv8.Ultility.Style;

namespace iParkingv8.Object.Enums.Parkings
{
    public enum EmVehicleType
    {
        CAR,
        MOTORBIKE,
        BIKE
    }
    public static class VehicleType
    {
        public static string ToDisplayString(EmVehicleType vehicleType)
        {
            return vehicleType switch
            {
                EmVehicleType.CAR => KZUIStyles.CurrentResources.VehicleTypeCar,
                EmVehicleType.MOTORBIKE => KZUIStyles.CurrentResources.VehicleTypeMotor,
                EmVehicleType.BIKE => KZUIStyles.CurrentResources.VehicleTypeBike,
                _ => string.Empty,
            };
        }
    }
}
