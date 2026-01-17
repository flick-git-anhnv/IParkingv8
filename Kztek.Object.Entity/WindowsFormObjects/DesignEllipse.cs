namespace Kztek.Object
{
    public class DesignEllipse
    {
        public DesignEllipse(Vector3 center, double majorAxis, double minorAxis)
        {
            Center = center;
            MajorAxis = majorAxis;
            MinorAxis = minorAxis;
            StartAngle = 0;
            EndAngle = 360;
            Thickness = 0;
            Rotation = 0;
        }

        public Vector3 Center { get; set; }
        public double MajorAxis { get; set; }
        public double MinorAxis { get; set; }
        public double Rotation { get; set; }
        public double StartAngle { get; set; }
        public double EndAngle { get; set; }
        public double Thickness { get; set; }

    }
}
