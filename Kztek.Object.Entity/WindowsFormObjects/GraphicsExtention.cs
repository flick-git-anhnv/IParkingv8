using System.Drawing;

namespace Kztek.Object
{
    public static class GraphicsExtention
    {
        private static float Height;
        public static void SetParameters(this Graphics g, float height)
        {
            Height = height;
        }

        public static void SetTransform(this Graphics g)
        {
            g.PageUnit = GraphicsUnit.Millimeter;
            g.TranslateTransform(0, Height);
            g.ScaleTransform(1.0f, 1.0f);
        }
        public static void DrawPoint(this Graphics g, Pen pen, DesignPoint point)
        {
            g.SetTransform();
            PointF p = point.Position.ToPointF;
            g.DrawEllipse(pen, p.X - 2, p.Y - 2, 4, 4);
            g.ResetTransform();
        }

        public static void DrawLine(this Graphics g, Pen pen, DesignLine line)
        {
            g.SetTransform();
            g.DrawLine(pen, line.StartPoint.ToPointF, line.EndPoint.ToPointF);
            g.ResetTransform();
        }

        public static void DrawCircle(this Graphics g, Pen pen, DesignCircle circle)
        {
            float x = (float)(circle.Center.X - circle.Radius);
            float y = (float)(circle.Center.Y - circle.Radius);
            float d = (float)circle.Diameter;
            g.SetTransform();
            g.DrawEllipse(pen, x, y, d, d);
            g.ResetTransform();
        }

        public static void DrawEllipse(this Graphics g, Pen pen, DesignEllipse ellipse)
        {
            g.SetTransform();
            g.TranslateTransform(ellipse.Center.ToPointF.X, ellipse.Center.ToPointF.Y);
            g.RotateTransform((float)ellipse.Rotation);
            g.DrawEllipse(pen, (float)ellipse.MajorAxis, (float)ellipse.MinorAxis, (float)ellipse.MajorAxis * 2, (float)ellipse.MinorAxis * 2);
            g.ResetTransform();
        }

        public static void DrawArc(this Graphics g, Pen pen, DesignArc arc)
        {
            float x = (float)(arc.Center.X - arc.Radius);
            float y = (float)(arc.Center.Y - arc.Radius);
            float d = (float)arc.Diameter;
            RectangleF rectF = new RectangleF(x, y, d, d);
            g.SetTransform();
            g.DrawArc(pen, rectF, (float)arc.StartAngle, (float)arc.EndAngle);

            g.ResetTransform();
        }
    }
}
