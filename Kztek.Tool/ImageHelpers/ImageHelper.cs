using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Kztek.Tool
{
    /// <summary>
    /// Built with System.Drawing.Image - 8.0.10
    /// </summary>
    public static class ImageHelper
    {
        public static Image? GetResizeImage(Image? source, int controlWidth, int controlHeight)
        {
            if (source == null || source.Width == 0 || source.Height == 0)
                return null;

            int displayWidth = Math.Max(100, controlWidth);
            int displayHeight = Math.Max(100, controlHeight);

            var bmp = new Bitmap(displayWidth, displayHeight);
            using (var g = Graphics.FromImage(bmp))
            {
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

                g.DrawImage(source, new Rectangle(0, 0, displayWidth, displayHeight));
            }
            return bmp; // ảnh mới hoàn toàn, độc lập với source
        }


        public static Bitmap? ResizeImage(this Image? imgToResize, int width, int height)
        {
            try
            {
                if (imgToResize == null)
                {
                    return null;
                }

                int sourceWidth = imgToResize.Width;
                int sourceHeight = imgToResize.Height;

                float nPercent = 0;
                float nPercentW = 0;
                float nPercentH = 0;

                nPercentW = ((float)width / (float)sourceWidth);
                nPercentH = ((float)height / (float)sourceHeight);

                if (nPercentH < nPercentW)
                    nPercent = nPercentH;
                else
                    nPercent = nPercentW;

                int destWidth = (int)(sourceWidth * nPercent);
                int destHeight = (int)(sourceHeight * nPercent);

                Bitmap b = new Bitmap(destWidth, destHeight, PixelFormat.Format32bppArgb);
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                    // ⭐ Không tô nền → giữ alpha
                    g.Clear(Color.Transparent);

                    g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
                }
                //Graphics g = Graphics.FromImage((Image)b);
                //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                //g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
                //g.Dispose();

                return b;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public static async Task<List<byte>> ImageToByteArrayAsync(this Image? imageIn)
        {
            try
            {
                if (imageIn != null)
                {
                    MemoryStream ms = new MemoryStream();
                    imageIn.Save(ms, ImageFormat.Jpeg);
                    byte[] bytearray = ms.ToArray();
                    return bytearray.ToList();
                }
                else
                {
                    return new List<byte>();
                }
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"Convert Image - Error", ex));
            }
            return new List<byte>();
        }
        public static List<byte> ImageToByteArray(this Image? imageIn)
        {
            try
            {
                if (imageIn != null)
                {
                    MemoryStream ms = new MemoryStream();
                    imageIn.Save(ms, ImageFormat.Jpeg);
                    byte[] bytearray = ms.ToArray();
                    return bytearray.ToList();
                }
                else
                {
                    return new List<byte>();
                }
            }
            catch (Exception ex)
            {
            }
            return new List<byte>();
        }

        public static Image? Base64ToImage(string base64Str)
        {
            if (string.IsNullOrEmpty(base64Str))
            {
                return null;
            }
            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64Str);
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    Image image = Image.FromStream(ms, true);
                    return image;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string FileToBase64(string imagePath)
        {
            string base64String = string.Empty;
            if (File.Exists(imagePath))
            {
                using (var imageStream = new FileStream(imagePath, FileMode.Open))
                {
                    var buffer = new byte[imageStream.Length];
                    imageStream.Read(buffer, 0, (int)imageStream.Length);
                    base64String = Convert.ToBase64String(buffer);
                }
            }
            return base64String;
        }

        public static Bitmap? CropBitmap(this Bitmap source, Rectangle? cutRect)
        {
            try
            {
                if (cutRect == null)
                {
                    return source;
                }
                Rectangle _cutRect = (Rectangle)cutRect;
                Bitmap cutBmp = new Bitmap(_cutRect.Width, _cutRect.Height);
                using (Graphics g = Graphics.FromImage(cutBmp))
                {
                    g.DrawImage(source, 0, 0, _cutRect, GraphicsUnit.Pixel);
                }
                return cutBmp;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public static Bitmap? CropBitmapF(Bitmap source, RectangleF cutRect)
        {
            try
            {
                if (cutRect == null)
                {
                    return source;
                }
                RectangleF _cutRect = (RectangleF)cutRect;
                Bitmap cutBmp = new Bitmap((int)_cutRect.Width, (int)_cutRect.Height);
                using (Graphics g = Graphics.FromImage(cutBmp))
                {
                    g.DrawImage(source, 0, 0, _cutRect, GraphicsUnit.Pixel);
                }
                return cutBmp;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public static Bitmap? DrawRect(this Bitmap? bitmap, int x, int y,
                                       int width, int height, Color color)
        {
            if (bitmap == null) return null;
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                using (Pen pen = new Pen(color, 5))
                {
                    g.DrawRectangle(pen, x, y, width, height);
                }
            }

            return bitmap;
        }
        public static Bitmap? DrawRect(this Bitmap? bitmap, int x, int y,
                                       int width, int height, Color color, int rectSize)
        {
            if (bitmap == null) return null;
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                using (Pen pen = new Pen(color, rectSize))
                {
                    g.DrawRectangle(pen, x, y, width, height);
                }
            }

            return bitmap;
        }
        public static Bitmap? DrawRoundRect(this Bitmap? bitmap,
                                        int x, int y,
                                        int width, int height,
                                        int radius,
                                        Color color,
                                        int rectSize)
        {
            if (bitmap == null) return null;

            // Giới hạn radius để không vượt quá chiều rộng/cao
            int maxRadius = Math.Min(width, height) / 2;
            if (radius > maxRadius) radius = maxRadius;
            if (radius < 1) radius = 1;

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                using (Pen pen = new Pen(color, rectSize))
                using (GraphicsPath path = CreateRoundedRectanglePath(
                           new Rectangle(x, y, width, height), radius))
                {
                    g.DrawPath(pen, path);
                }
            }

            return bitmap;
        }

        private static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int radius)
        {
            int diameter = radius * 2;
            var path = new GraphicsPath();

            // Góc trên trái
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            // Góc trên phải
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            // Góc dưới phải
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            // Góc dưới trái
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);

            path.CloseFigure();
            return path;
        }
        public static Image? RotateImage(this Image? img, float rotationAngle)
        {
            if (img == null)
            {
                return img;
            }
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            Graphics gfx = Graphics.FromImage(bmp);

            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            gfx.RotateTransform(rotationAngle);

            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            gfx.DrawImage(img, new Point(0, 0));

            gfx.Dispose();

            return bmp;
        }

        public static Image? GetCloneImageFromPath(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            try
            {
                var img = Image.FromFile(filePath);
                var bmp = new Bitmap(img);
                img.Dispose();
                return bmp;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Get byte array from image
        public static byte[]? GetByteArrayFromImage(Image? image, ImageFormat format)
        {
            if (image == null)
                return null;

            try
            {
                using var ms = new MemoryStream();
                image.Save(ms, format);
                return ms.ToArray();
            }
            catch
            {
                // log nếu cần
                return null;
            }
        }
        public static byte[] GetJpegBytes(Bitmap bmp, long quality = 70L)
        {
            using var ms = new MemoryStream();
            var jpgEncoder = ImageCodecInfo.GetImageEncoders()
                .First(c => c.FormatID == ImageFormat.Jpeg.Guid);

            var encParams = new EncoderParameters(1);
            encParams.Param[0] = new EncoderParameter(
                System.Drawing.Imaging.Encoder.Quality,
                quality);

            bmp.Save(ms, jpgEncoder, encParams);
            return ms.ToArray();
        }

        public static byte[]? GetRawBytesFromBitmap(Bitmap bmp, out int stride)
        {
            stride = 0;
            if (bmp == null) return null;

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            var data = bmp.LockBits(rect, ImageLockMode.ReadOnly, bmp.PixelFormat);

            try
            {
                stride = data.Stride;
                int bytes = Math.Abs(data.Stride) * bmp.Height;
                byte[] buffer = new byte[bytes];
                Marshal.Copy(data.Scan0, buffer, 0, bytes);
                return buffer;
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }

        public static async Task<List<byte>> GetByteArrayFromImageAsync(this Image? image)
        {
            try
            {
                if (image == null || image.Width <= 0 || image.Height <= 0)
                    return new List<byte>();

                using var copy = CloneToBitmap(image);
                using var ms = new MemoryStream();
                copy.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray().ToList();
            }
            catch
            {
                return new List<byte>();
            }
        }

        // Get image from byte array
        public static Image GetImageFromByteArray(byte[] bytearray)
        {
            try
            {
                if (bytearray != null)
                {
                    MemoryStream mem = new MemoryStream();
                    mem.Write(bytearray, 0, bytearray.Length);
                    Image image = Image.FromStream(mem);
                    return image;
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public static string ImageToBase64(string imagePath)
        {
            string base64String = string.Empty;
            if (File.Exists(imagePath))
            {
                using (var imageStream = new FileStream(imagePath, FileMode.Open))
                {
                    var buffer = new byte[imageStream.Length];
                    imageStream.Read(buffer, 0, (int)imageStream.Length);
                    base64String = Convert.ToBase64String(buffer);
                }
            }
            return base64String;
        }
        public static void ImageToBase64(Image? img, out string base64, out int size, ImageFormat format)
        {
            if (img == null)
            {
                base64 = "";
                size = 0;
                return;
            }
            base64 = "";
            size = 0;
            byte[] byteDatas = GetByteArrayFromImage(img, format);
            if (byteDatas == null)
            {
                return;
            }
            size = byteDatas.Length;
            base64 = ConvertByteArrayToBase64(byteDatas);
        }
        public static string ConvertByteArrayToBase64(byte[] byteArray)
        {
            try
            {
                if (byteArray != null && byteArray.Length > 0)
                {
                    return Convert.ToBase64String(byteArray);
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
            }
            return "";
        }
        private static Bitmap CloneToBitmap(Image src)
        {
            var bmp = new Bitmap(src.Width, src.Height, PixelFormat.Format32bppPArgb);
            using var g = Graphics.FromImage(bmp);
            g.CompositingMode = CompositingMode.SourceCopy;
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.SmoothingMode = SmoothingMode.None;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.DrawImageUnscaled(src, 0, 0);
            return bmp;
        }
        public static List<byte> GetListByteArrayFromImage(this Image? image)
        {
            try
            {
                if (image != null)
                {
                    MemoryStream ms = new MemoryStream();
                    image.Save(ms, ImageFormat.Jpeg);
                    byte[] bytearray = ms.ToArray();
                    return bytearray.ToList();
                }
                else
                {
                    return new List<byte>();
                }
            }
            catch (Exception ex)
            {
            }
            return new List<byte>();
        }

        public static async Task<Image?> GetImageFromUrlAsync(string? url)
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    return null;
                }
                using HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(2);
                byte[] imageBytes = await client.GetByteArrayAsync(url);
                using var ms = new System.IO.MemoryStream(imageBytes);
                return Image.FromStream(ms);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<Bitmap?> DownloadBitmapAsync(string? url)
        {
            return await Task.Run(() => DownloadBitmapInternalAsync(url));
        }

        public static async Task<Bitmap?> DownloadBitmapInternalAsync(string? url)
        {
            if (string.IsNullOrWhiteSpace(url)) return null;

            try
            {
                using var client = new HttpClient(new HttpClientHandler
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.None,
                });
                client.Timeout = TimeSpan.FromSeconds(5);
                using var resp = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                resp.EnsureSuccessStatusCode();

                await using var ns = await resp.Content.ReadAsStreamAsync();
                using var img = Image.FromStream(ns, useEmbeddedColorManagement: false, validateImageData: false);
                var bmp = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb);
                using (var g = Graphics.FromImage(bmp)) g.DrawImageUnscaled(img, 0, 0);
                return bmp;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public static Bitmap Zoom(Bitmap originalBitmap, float zoomFactor)
        {
            int newWidth = (int)(originalBitmap.Width * zoomFactor);
            int newHeight = (int)(originalBitmap.Height * zoomFactor);

            Bitmap zoomedBitmap = new Bitmap(newWidth, newHeight);

            using (Graphics g = Graphics.FromImage(zoomedBitmap))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(originalBitmap, new Rectangle(0, 0, newWidth, newHeight));
            }

            return zoomedBitmap;
        }

        public static byte[] Base64ToBytes(string base64)
        {
            if (string.IsNullOrWhiteSpace(base64))
                throw new ArgumentException("Base64 is null/empty");

            // 1) Strip DataURL prefix nếu có: data:image/jpeg;base64,....
            int comma = base64.IndexOf(',');
            if (base64.StartsWith("data:", StringComparison.OrdinalIgnoreCase) && comma >= 0)
                base64 = base64[(comma + 1)..];

            // 2) Remove whitespace/newlines
            base64 = base64.Trim()
                           .Replace("\r", "")
                           .Replace("\n", "")
                           .Replace(" ", "");

            // 3) Support URL-safe base64 (- _)
            base64 = base64.Replace('-', '+').Replace('_', '/');

            // 4) Fix padding
            int mod = base64.Length % 4;
            if (mod != 0)
                base64 = base64.PadRight(base64.Length + (4 - mod), '=');

            // 5) Decode
            return Convert.FromBase64String(base64);
        }

    }
}
