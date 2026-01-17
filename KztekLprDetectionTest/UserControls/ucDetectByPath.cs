using iParkingv5.LprDetecter.LprDetecters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KztekLprDetectionTest.UserControls
{
    public partial class ucDetectByPath : UserControl
    {
        public ucDetectByPath()
        {
            InitializeComponent();
            btnStartTraining.Click += BtnStartTraining_Click;
            txtInputPath.Text = Properties.Settings.Default.inputPath;
            txtOutputPath.Text = Properties.Settings.Default.outputPath;
            dgvData.SelectionChanged += DgvData_SelectionChanged;
        }
        bool isStop = false;
        int lastIndex = 0;
        private async void BtnStartTraining_Click(object? sender, EventArgs e)
        {
            var LprConfig = new Kztek.Object.LprConfig()
            {
                LPRDetecterType = Kztek.Object.LprDetecter.EmLprDetecter.AmericalLpr,
                Url = txtUrl.Text, //"http://kztek-lpr.demo.kztek.io.vn/alpr"
            };//
            AppData.LprDetecter = LprFactory.CreateLprDetecter(LprConfig, null);
            await AppData.LprDetecter!.CreateLprAsync(LprConfig);

            string rootPath = txtInputPath.Text;
            string resultPath = txtOutputPath.Text;
            if (!isStop)
            {
                Properties.Settings.Default.inputPath = rootPath;
                Properties.Settings.Default.outputPath = resultPath;
                Properties.Settings.Default.Save();
                if (!Directory.Exists(rootPath))
                {
                    MessageBox.Show("Đường dẫn vào không tồn tại");
                    return;
                }

                if (!Directory.Exists(resultPath))
                {
                    MessageBox.Show("Đường dẫn ra không tồn tại");
                    return;
                }
                try
                {
                    Directory.Delete(resultPath + "\\" + DateTime.Now.ToString("yyyy_MM_dd") + "\\LPR");
                    Directory.Delete(resultPath + "\\" + DateTime.Now.ToString("yyyy_MM_dd") + "\\ERROR");
                }
                catch (Exception)
                {
                }
            }
            dgvData.Rows.Clear();
            Directory.CreateDirectory(resultPath + "\\" + DateTime.Now.ToString("yyyy_MM_dd") + "\\LPR");
            Directory.CreateDirectory(resultPath + "\\" + DateTime.Now.ToString("yyyy_MM_dd") + "\\ERROR");
            var files = Directory.GetFiles(rootPath, "*", SearchOption.AllDirectories).OrderBy(t => t).OrderBy(t => t.Length).ToList();
            var sw = Stopwatch.StartNew();
            isStop = false;

            for (int i = lastIndex; i < files.Count; i += 1)
            {
                string? file = files[i];
                if (!isStop)
                {
                    try
                    {
                        using (Image img = Image.FromFile(file))
                        {
                            sw = Stopwatch.StartNew();
                            var detectResult = await AppData.LprDetecter.GetPlateNumberAsync(img, chbIsCar.Checked, null, (int)numRotate.Value, false);
                            var detectTime = sw.ElapsedMilliseconds;
                            string savePath = string.Empty;
                            string plate = detectResult.PlateNumber;
                            if (string.IsNullOrEmpty(detectResult.PlateNumber))
                            {
                                savePath = Path.Combine(resultPath, DateTime.Now.ToString("yyyy_MM_dd"), "ERROR", $"{Path.GetFileNameWithoutExtension(file)}_{plate.Replace("-", "").Replace(" ", "")}.jpg");
                                img?.Save(savePath, ImageFormat.Jpeg);
                            }
                            else
                            {
                                savePath = Path.Combine(resultPath, DateTime.Now.ToString("yyyy_MM_dd"), "LPR", $"{Path.GetFileNameWithoutExtension(file)}_{plate.Replace("-", "").Replace(" ", "")}.jpg");

                                var rect = new Rectangle(detectResult.BoundingBox.Xmin, detectResult.BoundingBox.Ymin,
                                                          detectResult.BoundingBox.Xmax - detectResult.BoundingBox.Xmin,
                                                          detectResult.BoundingBox.Ymax - detectResult.BoundingBox.Ymin
                                                          );

                                var crop = CropBitmap(img as Bitmap, rect);
                                crop.Save(savePath, ImageFormat.Jpeg);
                                detectResult?.LprImage?.Dispose();
                            }

                            this.Invoke(new Action(() =>
                            {
                                bool isSame = plate.Replace("-", "").Replace(" ", "").Replace(".", "") == detectResult.OriginalPlate.Replace("-", "").Replace(" ", "").Replace(".", "");
                                dgvData.Rows.Add(dgvData.Rows.Count + 1, file, savePath, plate, detectResult.OriginalPlate, isSame, sw.ElapsedMilliseconds + " ms", Path.GetFileNameWithoutExtension(file));
                                dgvData.CurrentCell = dgvData.Rows[dgvData.RowCount - 1].Cells[0];
                                Application.DoEvents();
                            }));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing file {file}: {ex.Message}");
                    }
                }
                else
                {
                    lastIndex = i;
                    break;
                }
            }
        }
        private void DgvData_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvData.CurrentRow == null)
            {
                return;
            }

            string inputPath = dgvData.CurrentRow.Cells[1].Value?.ToString();
            string outputPath = dgvData.CurrentRow.Cells[2].Value?.ToString();

            try
            {
                if (!string.IsNullOrEmpty(inputPath) && File.Exists(inputPath))
                {
                    using (Image input = Image.FromFile(inputPath))
                    {
                        picInput.Image = RotateImage(input, (int)numRotate.Value);
                    }
                }
                else
                {
                    picInput.Image = null;
                }

                if (!string.IsNullOrEmpty(outputPath) && File.Exists(outputPath))
                {
                    using (Image output = Image.FromFile(outputPath))
                    {
                        picOutput.Image = (Image)output.Clone();
                    }
                }
                else
                {
                    picOutput.Image = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading images: {ex.Message}");
            }
        }
        public static Image RotateImage(Image img, float rotationAngle)
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
        private void btnSaveErrorPic_Click(object sender, EventArgs e)
        {
            string resultPath = txtOutputPath.Text;
            Directory.CreateDirectory(resultPath + "\\" + DateTime.Now.ToString("yyyy_MM_dd") + "\\FALSE");
            string detectPlate = dgvData.CurrentRow.Cells[4].Value?.ToString();
            string stt = dgvData.CurrentRow.Cells[0].Value?.ToString();
            if (!string.IsNullOrEmpty(detectPlate))
            {
                string savePath = resultPath + "\\" + DateTime.Now.ToString("yyyy_MM_dd") + "\\FALSE\\" + detectPlate + "_" + stt + ".jpg";
                picInput.Image.Save(savePath, ImageFormat.Jpeg);
            }
        }


        static Bitmap? CropBitmap(Bitmap source, Rectangle? cutRect)
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
            catch (Exception ex)
            {
                return null;
            }

        }

        private void btnStartTraining_Click_1(object sender, EventArgs e)
        {

        }
    }
}
