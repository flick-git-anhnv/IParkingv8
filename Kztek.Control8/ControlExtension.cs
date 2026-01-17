using iParkingv8.Ultility.dictionary;
using Kztek.Object;
using static iParkingv8.Ultility.dictionary.KzDictionary;

namespace Kztek.Control8
{
    public static class ControlExtension
    {
        public static void SetLocation(this Control control, int x, int y)
        {
            if (control == null) return;
            if (control.InvokeRequired)
            {
                control.Invoke(new Action(() =>
                {
                    control.Location = new Point(x, y);
                    control.Refresh();
                }));
                return;
            }
            control.Location = new Point(x, y);
            control.Refresh();
        }
        public static void SetSize(this Control control, int width, int height)
        {
            if (control == null) return;
            if (control.InvokeRequired)
            {
                control.Invoke(new Action(() =>
                {
                    control.Size = new Size(width, height);
                    control.Refresh();
                }));
                return;
            }
            control.Size = new Size(width, height);
            control.Refresh();
        }

        public static void SetText(this Control control, string text)
        {
            if (control == null) return;
            if (control.InvokeRequired)
            {
                control.Invoke(new Action(() =>
                {
                    control.Text = text;
                    control.Refresh();
                }));
                return;
            }
            control.Text = text;
            control.Refresh();
        }
        public static void SetImage(this PictureBox pictureBox, Image? img)
        {
            if (pictureBox == null) return;
            if (pictureBox.InvokeRequired)
            {
                pictureBox.Invoke(new Action(() =>
                {
                    pictureBox.Image?.Dispose(); // Giải phóng ảnh cũ tránh leak
                    pictureBox.Image = img != null ? new Bitmap(img) : null; // Clone ảnh
                    pictureBox.Refresh();
                }));
                return;
            }
            pictureBox.Image?.Dispose();
            pictureBox.Image = img != null ? new Bitmap(img) : null;
            pictureBox.Refresh();
        }

        public static List<Control> GetAllControls(this Control parent)
        {
            List<Control> controls = [];

            foreach (Control ctrl in parent.Controls)
            {
                controls.Add(ctrl);
                if (ctrl.HasChildren)
                {
                    controls.AddRange(GetAllControls(ctrl));
                }
            }

            return controls;
        }
    }
}
