using Kztek.Cameras;

namespace Kztek.Control8.UserControls
{
    public class KZUI_PictureBox : PictureBox
    {
        // Ảnh nguồn full-res mà control OWN
        private Image? _sourceOwned;
        // Ảnh mặc định (cũng là ảnh lỗi) full-res mà control OWN
        private Image? _defaultOwned;
        // Ảnh đã resize để hiển thị mà control OWN
        private Image? _resizedOwned;

        public KZUI_PictureBox()
        {
            // Để PictureBox không tự scale nữa (ta chủ động tạo _resizedOwned)
            SizeMode = PictureBoxSizeMode.Normal;
            DoubleClick += KZUI_PictureBox_DoubleClick;
            SizeChanged += (_, __) => RefreshResize();
        }

        /// <summary>
        /// Ảnh mặc định & ảnh lỗi. Sẽ được clone để control OWN.
        /// Khi set xong, nếu chưa có KZUI_Image thì hiển thị default.
        /// </summary>
        public Image? KZUI_DefaultImage
        {
            get => _defaultOwned;
            set
            {
                var newOwned = value != null ? (Image)value.Clone() : null;
                SwapOwned(ref _defaultOwned, newOwned);
                // Nếu chưa có ảnh nguồn → dùng default để hiển thị
                if (_sourceOwned == null)
                    RefreshResize();
            }
        }

        /// <summary>
        /// Ảnh nguồn cần hiển thị (full-res). Sẽ được clone để control OWN.
        /// Nếu null → hiển thị KZUI_DefaultImage.
        /// </summary>
        public Image? KZUI_Image
        {
            get => _sourceOwned;
            set
            {
                if (value == null)
                {
                    SwapOwned(ref _sourceOwned, null);
                }
                else
                {
                    var cloned = (Image)value.Clone();
                    SwapOwned(ref _sourceOwned, cloned);
                }
                RefreshResize();
            }
        }

        /// <summary>
        /// Double-click: mở form xem ảnh với bản full chất lượng (clone).
        /// </summary>
        private void KZUI_PictureBox_DoubleClick(object? sender, EventArgs e)
        {
            var full = _sourceOwned ?? _defaultOwned;
            if (full == null) return;

            // Ở đây giả định bạn có frmViewImage với property CurrentImage.
            // Nếu không có, bạn có thể thay thế bằng 1 form tạm hiển thị ảnh.
            var frm = new frmViewImage
            {
                CurrentImage = (Image)full.Clone(), // form OWN clone này
                TopMost = true
            };
            frm.FormClosed += (s, args) =>
            {
                try { frm.CurrentImage?.Dispose(); } catch { }
                frm.CurrentImage = null;
                frm.Dispose();
            };
            frm.Show();
            frm.BringToFront();
        }

        /// <summary>
        /// Tạo/bố trí lại ảnh đã resize để hiển thị lên PictureBox.
        /// </summary>
        private void RefreshResize()
        {
            void Do()
            {
                var basis = _sourceOwned ?? _defaultOwned;
                Image? newResized = null;

                if (basis != null && Width > 0 && Height > 0)
                {
                    // Tạo 1 bitmap đúng bằng kích thước control và vẽ giữ tỉ lệ (kiểu "Zoom")
                    newResized = CreateStretchedBitmap(basis, Width, Height);
                }

                SwapOwned(ref _resizedOwned, newResized);

                // Gán cho PictureBox.Image (base) — không dispose "base.Image" vì chính là _resizedOwned
                base.Image = _resizedOwned;
                Refresh();
            }

            if (IsHandleCreated && InvokeRequired) BeginInvoke((Action)Do);
            else Do();
        }

        /// <summary>
        /// Tạo bitmap mới (w x h) và vẽ ảnh vào theo kiểu Zoom, căn giữa.
        /// </summary>
        private static Bitmap CreateZoomedBitmap(Image source, int w, int h)
        {
            var bmp = new Bitmap(w, h);
            using (var g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.Clear(Color.Transparent);

                float ratioX = (float)w / source.Width;
                float ratioY = (float)h / source.Height;
                float ratio = Math.Min(ratioX, ratioY);

                int drawW = (int)(source.Width * ratio);
                int drawH = (int)(source.Height * ratio);
                int drawX = (w - drawW) / 2;
                int drawY = (h - drawH) / 2;

                g.DrawImage(source, new Rectangle(drawX, drawY, drawW, drawH));
            }
            return bmp;
        }
        private static Bitmap CreateStretchedBitmap(Image source, int w, int h)
        {
            var bmp = new Bitmap(w, h);
            using (var g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //g.Clear(Color.Transparent);

                // Stretch: vẽ toàn bộ ảnh vào khung w x h, không giữ tỉ lệ
                g.DrawImage(source, new Rectangle(0, 0, w, h));
            }
            return bmp;
        }

        /// <summary>
        /// Thay đối tượng OWN: dispose cái cũ, gán cái mới.
        /// </summary>
        private static void SwapOwned(ref Image? targetOwned, Image? newOwned)
        {
            if (!ReferenceEquals(targetOwned, newOwned))
            {
                targetOwned?.Dispose();
                targetOwned = newOwned;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DoubleClick -= KZUI_PictureBox_DoubleClick;
                SizeChanged -= (_, __) => RefreshResize();

                // Dispose theo thứ tự: resized -> source -> default
                _resizedOwned?.Dispose(); _resizedOwned = null;
                _sourceOwned?.Dispose(); _sourceOwned = null;
                _defaultOwned?.Dispose(); _defaultOwned = null;
            }
            base.Dispose(disposing);
        }
    }
}
