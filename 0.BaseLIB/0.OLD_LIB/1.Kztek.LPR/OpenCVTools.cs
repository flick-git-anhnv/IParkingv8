using AForge.Imaging.Filters;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace Kztek.LPR
{
	public static class OpenCVTools
	{
		public static Bitmap GetBinaryImage(Bitmap source)
		{
			Bitmap result = null;
			try
			{
				if (source != null)
				{
					using (Mat mat = BitmapConverter.ToMat(source))
					{
						using (Mat mat2 = new Mat())
						{
							Cv2.CvtColor(mat, mat2, ColorConversionCodes.BGR2GRAY, 0);
							Cv2.AdaptiveThreshold(mat2, mat2, 255.0, 0, 0, 29, 0.0);
							Cv2.CvtColor(mat2, mat, ColorConversionCodes.GRAY2BGR, 0);
							result = BitmapConverter.ToBitmap(mat);
						}
					}
				}
			}
			catch(FileNotFoundException fex)
			{
				OpenCVTools.SaveLog("Get Binary Image Error - File not found: " + fex.ToString());
			}
			catch (Exception ex)
			{
				OpenCVTools.SaveLog("Get Binary Image Error: " + ex.ToString());
			}
			return result;
		}
		public static Bitmap GetBinaryImageFromFile(string fileName)
		{
			Bitmap result = null;
			try
			{
				if (!string.IsNullOrEmpty(fileName))
				{
					using (Mat mat = new Mat(fileName, ImreadModes.Color))
					{
						using (Mat mat2 = new Mat())
						{
							Cv2.CvtColor(mat, mat2, ColorConversionCodes.BGR2GRAY, 0);
							Cv2.AdaptiveThreshold(mat2, mat2, 255.0, 0, 0, 29, 0.0);
							Cv2.CvtColor(mat2, mat, ColorConversionCodes.GRAY2BGR, 0);
							result = BitmapConverter.ToBitmap(mat);
						}
					}
				}
			}
			catch (Exception ex)
			{
				OpenCVTools.SaveLog("Get Binary Image From File Error: " + ex.ToString());
			}
			return result;
		}
		public static Bitmap CropImage(Bitmap source, Rectangle rectangle)
		{
			Bitmap bmp = new Bitmap(rectangle.Width, rectangle.Height);
			Graphics g = Graphics.FromImage(bmp);
			g.DrawImage(source, 0, 0, rectangle, GraphicsUnit.Pixel);
			return bmp;
		}
		public static Bitmap RotareImage(Bitmap bitmap, double angle)
		{
			try
			{
				if (bitmap != null && angle != 0.0)
				{
					Bitmap expr_29 = new Bitmap(bitmap.Width, bitmap.Height, bitmap.PixelFormat);
					Graphics expr_2F = Graphics.FromImage(expr_29);
					expr_2F.TranslateTransform((float)bitmap.Width / 2f, (float)bitmap.Height / 2f);
					expr_2F.RotateTransform((float)angle);
					expr_2F.TranslateTransform(-(float)bitmap.Width / 2f, -(float)bitmap.Height / 2f);
					expr_2F.DrawImage(bitmap, new PointF(0, 0));
					expr_2F.Dispose();
					return expr_29;
				}
			}
			catch (Exception ex)
			{
				OpenCVTools.SaveLog("Rotate Image Error: " + ex.ToString());
			}
			return bitmap;
		}
		public static Bitmap ResizeImage(Bitmap bmp, int width, int height)
		{
			try
			{
				if (bmp != null && width > 0 && height > 0)
				{
					Bitmap expr_18 = new Bitmap(width, height, bmp.PixelFormat);
					Graphics expr_1E = Graphics.FromImage(expr_18);
					expr_1E.DrawImage(bmp, 0, 0, width, height);
					expr_1E.Dispose();
					return expr_18;
				}
			}
			catch (Exception ex)
			{
				OpenCVTools.SaveLog("Resize Image Error: " + ex.ToString());
			}
			return bmp;
		}
		public static Bitmap InvertImage(Bitmap srcImage)
		{
			try
			{
				if (srcImage != null)
				{
					new Invert().ApplyInPlace(srcImage);
				}
			}
			catch (Exception ex)
			{
				OpenCVTools.SaveLog("Invert Image Error: " + ex.ToString());
			}
			return srcImage;
		}
		public static void SaveLog(string logEvent)
		{
			try
			{
				string text = Application.StartupPath + "\\logs";
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				using (StreamWriter streamWriter = new StreamWriter(text + "\\image_processing_logs_" + DateTime.Now.ToString("yyyyMMdd") + ".txt", true))
				{
					streamWriter.WriteLine();
					streamWriter.Write(string.Concat(new string[]
					{
						"Date: ",
						DateTime.Now.ToString("dd/MM/yyyy"),
						" - Time: ",
						DateTime.Now.ToString("HH:mm:ss"),
						" - Message: ",
						logEvent
					}));
					streamWriter.WriteLine();
				}
			}
			catch
			{
			}
		}
	}
}
