using System;

namespace Kztek.Object
{
    public class DesignLine
    {
        public DesignLine(Vector3 startPoint, Vector3 endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Thickness = 0;
        }

        public DesignLine() : this(Vector3.Zero, Vector3.Zero) { }

        public Vector3 StartPoint { get; set; }
        public Vector3 EndPoint { get; set; }
        public double Thickness { get; set; }

        public double Length
        {
            get
            {
                double dx = EndPoint.X - StartPoint.X;
                double dy = EndPoint.Y - StartPoint.Y;
                double dz = EndPoint.Z - StartPoint.Z;
                return Math.Sqrt(dx * dx + dy * dy + dz * dz);
            }
        }
    }
}
