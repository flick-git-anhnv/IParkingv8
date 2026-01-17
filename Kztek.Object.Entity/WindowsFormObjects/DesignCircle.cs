namespace Kztek.Object
{
    public class DesignCircle
    {
        public Vector3 Center { get; set; }
        public double Radius { get; set; }
        public double Thickness { get; set; }

        public DesignCircle() : this(Vector3.Zero, 1.0f)
        { }

        public DesignCircle(Vector3 center, double radius)
        {
            Center = center;
            Radius = radius;
            Thickness = 0;
        }

        //Đường Kính
        public double Diameter => 2 * Radius;
    }
}
