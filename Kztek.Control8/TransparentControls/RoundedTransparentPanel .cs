using System.Drawing.Drawing2D;

namespace Kztek.Control8.UIHelpers
{
    // RoundedTransparentPanel.cs
    public class RoundedTransparentPanel : Panel
    {
        public int CornerRadius { get; set; } = 32;
        public int BorderThickness { get; set; } = 3;
        public Color BorderColor { get; set; } = Color.White;

        public RoundedTransparentPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint |
                     ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (Parent != null)
            {
                var s = e.Graphics.Save();
                e.Graphics.TranslateTransform(-Left, -Top);
                var pea = new PaintEventArgs(e.Graphics, new Rectangle(Location, Parent.ClientSize));
                InvokePaintBackground(Parent, pea);
                InvokePaint(Parent, pea);
                e.Graphics.Restore(s);
                return;
            }
            base.OnPaintBackground(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (CornerRadius > 0 && BorderThickness > 0)
                using (var path = GetRoundRect(ClientRectangle, CornerRadius))
                using (var pen = new Pen(BorderColor, BorderThickness) { Alignment = PenAlignment.Inset })
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.DrawPath(pen, path);
                }
        }
        static GraphicsPath GetRoundRect(Rectangle r, int radius)
        {
            int d = radius * 2, w = r.Width - 1, h = r.Height - 1;
            var p = new GraphicsPath();
            if (radius <= 0) { p.AddRectangle(new Rectangle(0, 0, w, h)); return p; }
            p.AddArc(0, 0, d, d, 180, 90); p.AddArc(w - d, 0, d, d, 270, 90);
            p.AddArc(w - d, h - d, d, d, 0, 90); p.AddArc(0, h - d, d, d, 90, 90);
            p.CloseFigure(); return p;
        }
    }

    // MessageBandPanel.cs — vẽ dải nền mờ phía sau text
    public class MessageBandPanel : RoundedTransparentPanel
    {
        public int Opacity { get; set; } = 140;          // 0..255
        public Color Fill = Color.FromArgb(241, 242, 246); // màu dải mờ
        public MessageBandPanel() { BorderThickness = 0; CornerRadius = 16; }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using var b = new SolidBrush(Color.FromArgb(Opacity, Fill));
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var r = ClientRectangle; r.Inflate(-0, -0);
            using var path = new GraphicsPath();
            int rds = CornerRadius * 2;
            if (CornerRadius > 0)
            {
                path.AddArc(r.X, r.Y, rds, rds, 180, 90);
                path.AddArc(r.Right - rds, r.Y, rds, rds, 270, 90);
                path.AddArc(r.Right - rds, r.Bottom - rds, rds, rds, 0, 90);
                path.AddArc(r.X, r.Bottom - rds, rds, rds, 90, 90);
                path.CloseFigure();
                e.Graphics.FillPath(b, path);
            }
            else e.Graphics.FillRectangle(b, r);
        }
    }

}
