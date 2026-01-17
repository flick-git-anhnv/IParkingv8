namespace iParkingv8.Object.Objects.Kiosk
{
    public class BaseKioskResult : BaseDialogResult
    {
        public EmKioskUserType kioskUserType;
    }

    public enum EmKioskUserType
    {
        Customer,
        System
    }
}
