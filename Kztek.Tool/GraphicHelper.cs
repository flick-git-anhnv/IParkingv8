using System.Drawing;
using System.Drawing.Drawing2D;

namespace Kztek.Tool
{
    public static class GraphicHelper
    {
        /// <summary>
        /// Vẽ rect bo góc tròn.
        /// </summary>
        public static void DrawRoundedRectangle(Graphics g, Pen pen, Rectangle rect, int radius)
        {
            if (radius <= 0)
            {
                g.DrawRectangle(pen, rect);
                return;
            }

            using (GraphicsPath path = new GraphicsPath())
            {
                int d = radius * 2;

                path.AddArc(rect.X, rect.Y, d, d, 180, 90);
                path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
                path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
                path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
                path.CloseFigure();

                g.DrawPath(pen, path);
            }
        }

        /// <summary>
        /// Tạo GraphicsPath bo góc tròn cho rect float (dùng cho fill label/rect).
        /// </summary>
        public static GraphicsPath RoundedRect(RectangleF bounds, float radius)
        {
            float d = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
