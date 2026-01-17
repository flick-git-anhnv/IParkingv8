namespace Kztek.Object
{
    public class CheckInResponse
    {
        public bool IsSuccess { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;
        public Staff? StaffInfo { get; set; }
    }
}
