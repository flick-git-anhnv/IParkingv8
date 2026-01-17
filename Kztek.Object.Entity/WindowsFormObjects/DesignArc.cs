namespace Kztek.Object
{
    public class DesignArc
    {
        public Vector3 Center { get; set; }
        public double Radius { get; set; }
        public double StartAngle { get; set; }
        public double EndAngle { get; set; }
        public double Thickness { get; set; }

        public DesignArc(Vector3 center, double radius, double startAngle, double endAngle)
        {
            Center = center;
            Radius = radius;
            StartAngle = startAngle;
            EndAngle = endAngle;
            Thickness = 0;
        }

        public DesignArc() : this(Vector3.Zero, 1, 0, 180)
        { }

        public double Diameter => Radius * 2;
    }
}
