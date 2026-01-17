using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kztek.Control8.TransparentControls
{
    public class TransparentPanel : Panel
    {
        public TransparentPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint |
                     ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (Parent == null) { base.OnPaintBackground(e); return; }

            var state = e.Graphics.Save();
            e.Graphics.TranslateTransform(-Left, -Top);

            var pea = new PaintEventArgs(e.Graphics, new Rectangle(Location, Parent.ClientSize));
            InvokePaintBackground(Parent, pea);
            InvokePaint(Parent, pea);

            e.Graphics.Restore(state);
            // Không gọi base để tránh fill nền đặc
        }
    }
}
