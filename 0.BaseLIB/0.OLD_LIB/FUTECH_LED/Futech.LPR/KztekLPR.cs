using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using PlateRecognizer;
using Futech.Tools;

namespace Futech.LPR
{
    public class KztekLPR : ILPR
    {
        private int contrastLevel = 2; // 0-Unknown, 1-Low, 2-Medium, 3-High,  ~ Medium
        private bool deinterlace = false; // ~ False
        private bool deinterlacedSource = false; // ~ False
        private int deviationAnge = 30; // ~ 30
        private int fps = 25; // Only Video Mode
        private bool histogramEquualization = false; // ~ False
        private int maxCharHeight = 120; // ~ 64
        private int minCharHeight = 8; // ~ 8
        private int motionDetectionTriggering = 0; // 0-100 ~ 0 Only Video Mode
        private int plateColorSchema = 1; // 0-Unknown, 1-BlackOnWhite, 2-WhiteOnBlack, 3-All ~ BlackOnWhite
        private int platePresenceTime = 1000; // ~ 1000 (1 sec) Only Video Mode
        private int preciseMode = 0; // Normal, Mode1, Mode2, Night ~ Normal
        private int rotateAngle = 0; // ~ 0

        private Rectangle[] scanRectangle = null; // Thiet lap vung nhan dang
        public KztekLPR()
        { }

        // Properties
        // Medium, Low, High ~ Medium
        public int ContrastLevel
        {
            get { return contrastLevel; }
            set
            {
                contrastLevel = value;
            }
        }
        // ~ False
        public bool Deinterlace
        {
            get { return deinterlace; }
            set
            {
                deinterlace = value;
            }
        }
        // ~ False
        public bool DeinterlacedSource
        {
            get { return deinterlacedSource; }
            set
            {
                deinterlacedSource = value;
            }
        }
        // ~ 30
        public int DeviationAnge
        {
            get { return deviationAnge; }
            set
            {
                deviationAnge = value;
            }
        }
        // Only Video Mode
        public int Fps
        {
            get { return fps; }
            set { fps = value; }
        }
        // ~ False
        public bool HistogramEquualization
        {
            get { return histogramEquualization; }
            set { histogramEquualization = value; }
        }
        // ~ 64
        public int MaxCharHeight
        {
            get { return maxCharHeight; }
            set { maxCharHeight = value; }
        }
        // ~ 8
        public int MinCharHeight
        {
            get { return minCharHeight; }
            set
            {
                minCharHeight = value;
            }
        }
        // 0-100 ~ 0 Only Video Mode
        public int MotionDetectionTriggering
        {
            get { return motionDetectionTriggering; }
            set
            {
                motionDetectionTriggering = value;
            }
        }
        // BlackOnWhite, WhiteOnBlack, All ~ BlackOnWhite
        public int PlateColorSchema
        {
            get { return plateColorSchema; }
            set
            {
                plateColorSchema = value;
            }
        }
        // ~ 1000 (1 sec) Only Video Mode
        public int PlatePresenceTime
        {
            get { return platePresenceTime; }
            set
            {
                platePresenceTime = value;
            }
        }
        // Normal, Mode1, Mode2, Night ~ Normal
        public int PreciseMode
        {
            get { return preciseMode; }
            set
            {
                preciseMode = value;
            }
        }
        // ~ 0
        public int RotateAngle
        {
            get { return rotateAngle; }
            set
            {
                rotateAngle = value;
            }
        }

        // Version
        public string GetVersion
        {
            get
            {
                //SimpleLPR2.VersionNumber ver = _lpr.versionNumber;
                //return string.Format("Version {0}.{1}.{2}.{3}", ver.A, ver.B, ver.C, ver.D);
                return "1.0";
            }
        }

        // Methods
        public void Init(bool bVideo) // True ~ Video Mode, False ~ Still Image
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        public void ReadFromBitmap(int hBmp, int customData)
        {

        }

        public LicensePlateCollection ReadFromBitmap(Bitmap bmp1, int customData, ref int recognitionTime)
        {
            LicensePlateCollection licensePlateList = new LicensePlateCollection();
            recognitionTime = 0;

            if (bmp1 != null)
            {
                string token = "";
                Bitmap lastFrame = bmp1.Clone(new Rectangle(0, 0, bmp1.Width, bmp1.Height), PixelFormat.Format24bppRgb);
                Stopwatch watch = Stopwatch.StartNew(); // time the detection process // using System.Diagnostics;
                byte[] data = PlateReader.GetByteArray(lastFrame);
                try
                {
                    // PlateReaderResult plateReaderResult = PlateReader.Read(postUrl, file, null, token);
                    PlateReaderResult plateReaderResult = PlateReader.Read(APIUrl, data, token);

                    watch.Stop();
                    recognitionTime = (int)watch.Elapsed.TotalMilliseconds; // Thoi gian nhan dang ms

                    if (plateReaderResult == null)
                    {
                        recognitionTime = 9999;
                    }
                    else
                    {
                        licensePlateList = GetBestResult(plateReaderResult, lastFrame);
                    }

                }
                catch (Exception ex)
                {
                    LogHelper.Log(logType: LogHelper.EmLogType.ERROR,
                              objectLogType: LogHelper.EmObjectLogType.System,
                              obj: ex);
                }


            }

            GC.Collect();
            return licensePlateList;
        }

        public void ReadFromBuffer(int pBuffer, int width, int height, int stride, int bpp, int customData)
        {
        }

        public void ReadFromDIB(int hDIB, int customData)
        {
        }

        public LicensePlateCollection ReadFromFile(string fileName, int customData, ref int recognitionTime)
        {
            LicensePlateCollection licensePlateList = new LicensePlateCollection();
            recognitionTime = 0;

            if (File.Exists(fileName))
            {
                Stopwatch watch = Stopwatch.StartNew();

                string token = "";

                Bitmap lastFrame = (Bitmap)(Image.FromFile(fileName));

                try
                {
                    PlateReaderResult plateReaderResult = PlateReader.Read(APIUrl, fileName, null, token);

                    if (plateReaderResult != null)// && plateReaderResult.Results.Count > 0)
                    {
                        licensePlateList = GetBestResult(plateReaderResult, lastFrame);
                    }

                    watch.Stop(); //stop the timer
                    recognitionTime = (int)watch.Elapsed.TotalMilliseconds; // Thoi gian nhan dang ms
                }
                catch (Exception ex)
                {
                    LogHelper.Log(logType: LogHelper.EmLogType.ERROR, objectLogType: LogHelper.EmObjectLogType.System, obj: ex);
                }
            }
            GC.Collect();
            return licensePlateList;
        }

        private LicensePlateCollection GetBestResult(PlateReaderResult plateReaderResult, Bitmap bmp)
        {
            try
            {
                LicensePlateCollection licensePlateList = new LicensePlateCollection();
                var bestplate = new LicensePlate();

                if (plateReaderResult.Results != null)
                {
                    foreach (var result in plateReaderResult.Results)
                    {
                        LicensePlate plate = new LicensePlate
                        {
                            Text = result.Plate.ToUpper(),
                            Bitmap = bmp.Clone(new Rectangle(result.Box.Xmin, result.Box.Ymin, result.Box.Xmax - result.Box.Xmin, result.Box.Ymax - result.Box.Ymin), PixelFormat.Format24bppRgb),
                            ConfidenceLevel = (float)result.Dscore
                        };

                        if (result.Dscore > bestplate.ConfidenceLevel)
                            bestplate = plate;

                        licensePlateList.Add(plate);
                    }
                    licensePlateList.Maxcalls = plateReaderResult.usage.Max_calls;
                    licensePlateList.QueryTimes = plateReaderResult.usage.Calls;
                }
                // (plateReaderResult.usage.Calls + 5 == plateReaderResult.usage.Max_calls) bestplate.IsMaxcalls = true;

                licensePlateList.Add(bestplate);
                return licensePlateList;
            }
            catch
            {
                throw;
            }
        }

        public void ReadFromMemFile(object fileData, int dataLen, int customData)
        {
        }

        public string GetCorrectPlate(string plate)
        {
            string temp = "";
            for (int i = 0; i < plate.Length; i++)
            {
                if (i == plate.Length - 3 && plate[i].ToString() == "-")
                    temp = temp + ".";
                else if (i == 0 || i == 1 || i == 4)
                {
                    if (plate[i].ToString() == "Z")
                        temp = temp + "2";
                    else if (plate[i].ToString() == "S")
                        temp = temp + "5";
                    else if ((plate[i].ToString() == "T" && i == 4) || plate[i].ToString() == "I")
                        temp = temp + "1";
                    else if (plate[i].ToString() == "B" || plate[i].ToString() == "R")
                        temp = temp + "8";
                    else if (plate[i].ToString() == "L")
                        temp = temp + "4";
                    else if (plate[i].ToString() == "G")
                        temp = temp + "6";
                    else temp = temp + plate[i];
                }
                else
                    temp = temp + plate[i];
            }

            switch (temp.Substring(0, 2))
            {
                case "02":
                //case "12":
                //case "17":
                //case "19":
                //case "32":
                case "42":
                    //case "82":
                    //case "94":
                    //case "97":
                    //case "99":
                    temp = "92" + temp.Substring(2, temp.Length - 2);
                    break;
                case "13":
                //case "23":
                //case "63":
                //    temp = "43" + temp.Substring(2, temp.Length - 2);
                //    break;
                case "AA":
                case "AT":
                case "A1":
                case "44":
                case "AB":
                    temp = "";
                    break;
            }

            string[] lprvn = LPREngine.LoadLPRVN();
            bool islprvn = false;
            if (temp.Length >= 2)
            {
                //for (int i = 16; i < lprvn.Length; i++)
                //{
                //    if (temp.Substring(0, 2) == lprvn[i])
                //    {
                //        islprvn = true;
                //        break;
                //    }
                //}

                if (temp.Contains("11.1") || temp.Contains("1.11") ||
                    ((temp.Contains("11") || temp.Contains(".11") || temp.Contains("11.") || temp.Contains(".77") || temp.Contains("77.") || temp.Contains(".44") || temp.Contains("44.")) && temp.Substring(0, 2) == "11") ||
                    temp.Contains("77.7") || temp.Contains("7.77") ||
                    ((temp.Contains("77") || temp.Contains(".77") || temp.Contains("77.") || temp.Contains(".11") || temp.Contains("11.") || temp.Contains(".44") || temp.Contains("44.")) && temp.Substring(0, 2) == "77") ||
                    temp.Contains("7777"))
                {
                    temp = "";
                }
            }

            //if (!islprvn)
            //    temp = "";

            return temp;
        }

        public void Reset()
        {
        }

        // http://www.iso.org/iso/about/iso_members.htm
        // Viet Nam: VN
        public void SetCountryCode(string countryCode)
        {
        }

        public void SetMaskFromBitmap(int hBitmap)
        {
        }

        public void SetMaskFromBitmap(Bitmap Bitmap)
        {
        }

        public void SetMaskFromFile(string imageFileName)
        {
        }

        public void SetScanRectangle(int left, int top, int right, int bottom)
        {
        }

        public Rectangle[] ScanRectangle
        {
            get { return scanRectangle; }
            set { scanRectangle = value; }
        }
        public string APIUrl { get; set; }
    }
}
