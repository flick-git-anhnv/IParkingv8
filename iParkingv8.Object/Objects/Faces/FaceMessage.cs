namespace iParkingv8.Object.Objects.Faces
{
    public class FaceMessage
    {
        public string UserId { get; set; } = string.Empty;
        public string DeviceId { get; set; } = string.Empty;
        public DateTime EventTime { get; set; } = DateTime.Now;
    }
}
