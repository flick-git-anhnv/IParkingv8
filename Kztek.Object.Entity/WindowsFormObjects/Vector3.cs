using System;
using System.Drawing;

namespace Kztek.Object
{
    public class Vector3
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector3(double x, double y)
        {
            X = x;
            Y = y;
            Z = 0.0;
        }

        public Vector3(double x, double y, double z) : this(x, y)
        {
            Z = z;
        }

        public static Vector3 Zero => new Vector3(0.0, 0.0, 0.0);
        public PointF ToPointF => new PointF((float)X, (float)Y);

        public double DistanceFrom(Vector3 v)
        {
            double dx = v.X - X;
            double dy = v.Y - Y;
            double dz = v.Z - Z;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

    }
}
