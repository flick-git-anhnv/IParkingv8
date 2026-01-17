namespace Kztek.Object.Entity.Device
{
    public enum EmBarrieOpenMode
    {
        ALL,
        CAR_ONLY,
        NOT_CAR
    }

    public static class EmBarrieOpenModeExtension
    {
        public static string ToDisplayString(this EmBarrieOpenMode value)
        {
            return value switch
            {
                EmBarrieOpenMode.ALL => "Tất cả",
                EmBarrieOpenMode.CAR_ONLY => "Ô tô",
                EmBarrieOpenMode.NOT_CAR => "Không phải ô tô",
                _ => "Unknown",
            };
        }
    }
}
