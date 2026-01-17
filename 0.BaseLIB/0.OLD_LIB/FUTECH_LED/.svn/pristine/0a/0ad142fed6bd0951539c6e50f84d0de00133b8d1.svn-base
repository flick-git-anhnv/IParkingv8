using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

using System.Threading;

using DTKANPRLib;


namespace Futech.LPR
{
    public class DTKLPR : ILPR
    {
        [DllImport("DTKANPR.dll", CharSet = CharSet.Unicode), PreserveSig]
        private static extern int CreateANPREngine(ref ANPREngine engine);
        [DllImport("DTKANPR.dll", CharSet = CharSet.Unicode), PreserveSig]
        private static extern int DestroyANPREngine(ANPREngine engine);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        private ANPREngine engine = null;

        private int contrastLevel = 2; // 0-Unknown, 1-Low, 2-Medium, 3-High,  ~ Medium
        private bool deinterlace = false; // ~ False
        private bool deinterlacedSource = false; // ~ False
        private int deviationAnge = 30; // ~ 30
        private int fps = 25; // Only Video Mode
        private bool histogramEquualization = false; // ~ False
        private int maxCharHeight = 64; // ~ 64
        private int minCharHeight = 8; // ~ 8
        private int motionDetectionTriggering = 0; // 0-100 ~ 0 Only Video Mode
        private int plateColorSchema = 1; // 0-Unknown, 1-BlackOnWhite, 2-WhiteOnBlack, 3-All ~ BlackOnWhite
        private int platePresenceTime = 1000; // ~ 1000 (1 sec) Only Video Mode
        private int preciseMode = 0; // Normal, Mode1, Mode2, Night ~ Normal
        private int rotateAngle = 0; // ~ 0
        private Rectangle[] scanRectangle = null; // Thiet lap vung nhan dang
        // Constructor
        public DTKLPR()
        {

        }

        // Events

        // Properties
        // Medium, Low, High ~ Medium
        public int ContrastLevel
        {
            get { return contrastLevel; }
            set
            {
                contrastLevel = value;
                engine.ContrastLevel = (ImageContrastLevelEnum)contrastLevel;
            }
        }
        // ~ False
        public bool Deinterlace
        {
            get { return deinterlace; }
            set
            {
                deinterlace = value;
                engine.Deinterlace = deinterlace;
            }
        }
        // ~ False
        public bool DeinterlacedSource
        {
            get { return deinterlacedSource; }
            set
            {
                deinterlacedSource = value;
                engine.DeinterlacedSource = deinterlacedSource;
            }
        }
        // ~ 30
        public int DeviationAnge
        {
            get { return deviationAnge; }
            set
            {
                deviationAnge = value;
                engine.DeviationAngle = deviationAnge;
            }
        }
        // Only Video Mode
        public int Fps
        {
            get { return fps; }
            set 
            { 
                fps = value; 
            }
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
            set 
            { 
                maxCharHeight = value;
                engine.MaxCharHeight = maxCharHeight;
            }
        }
        // ~ 8
        public int MinCharHeight
        {
            get { return minCharHeight; }
            set
            {
                minCharHeight = value;
                engine.MinCharHeight = minCharHeight;
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
                engine.PlateColorSchema = (PlateColorSchemaEnum)plateColorSchema;
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
                engine.PreciseMode = (PreciseModeEnum)preciseMode;
            }
        }
        // ~ 0
        public int RotateAngle
        {
            get { return rotateAngle; }
            set
            {
                rotateAngle = value;
                engine.RotateAngle = rotateAngle;
            }
        }

        // Version
        public string GetVersion
        {
            get { return "Version 1.2.28"; }
        }

        // Methods
        public void Init(bool bVideo) // True ~ Video Mode, False ~ Still Image
        {
            // Create ANPR Engine object
            if (CreateANPREngine(ref engine) != 0)
            {
                MessageBox.Show("Unable to create ANPR engine");
                return;
            }

            // Initialize engine
            try
            {
                engine.Init(bVideo); // che do nhan dang: anh (false) hoac video (true)
                // Chon vung nhan dang
                string filename = Application.StartupPath + @"\MaskFile\mask.bmp";
                if (File.Exists(filename))
                    engine.SetMaskFromFile(filename);
            }
            catch (COMException ex)
            {
                MessageBox.Show(ex.Message + " (ErrorCode = " + ex.ErrorCode.ToString() + ")");
            }
            engine.LicenseManager.AddLicenseKey("HXJ1CM2G6IWIUK2VG8QA");
        }

        public void Dispose()
        {
            DestroyANPREngine(engine);
        }

        public void ReadFromBitmap(int hBmp, int customData)
        {
        }

        public LicensePlateCollection ReadFromBitmap(Bitmap lastFrame, int customData, ref int recognitionTime)
        {
            LicensePlate bestLicensePlate = new LicensePlate();
            LicensePlateCollection licensePlateList = new LicensePlateCollection();
            recognitionTime = 0;

            try
            {
                if (lastFrame!=null)
                {
                    Bitmap bmp = (Bitmap)lastFrame;
                    IntPtr hBitmap = bmp.GetHbitmap();
                    engine.ReadFromBitmap((int)hBitmap, 0);
                    DeleteObject(hBitmap);
                    bmp.Dispose();

                    if (engine.Plates != null)
                    {
                        for (int i = 0; i < engine.Plates.Count; i++)
                        {
                            Plate plate = engine.Plates.get_Item(i);
                            if (plate != null)
                            {
                                if (plate.Text.Length > bestLicensePlate.Text.Length && plate.Text.Length <= 9)
                                {
                                    bestLicensePlate.Text = plate.Text;
                                    bestLicensePlate.Bitmap = null;
                                    bestLicensePlate.ConfidenceLevel = (float)plate.ConfidenceLevel;
                                    bestLicensePlate.Left = plate.left;
                                    bestLicensePlate.Top = plate.top;
                                    bestLicensePlate.Right = plate.right;
                                    bestLicensePlate.Bottom = plate.bottom;
                                }

                                LicensePlate licensePlate = new LicensePlate();
                                licensePlate.Text = plate.Text;
                                licensePlate.Bitmap = null;
                                licensePlate.ConfidenceLevel = (float)plate.ConfidenceLevel;
                                licensePlate.Left = plate.left;
                                licensePlate.Top = plate.top;
                                licensePlate.Right = plate.right;
                                licensePlate.Bottom = plate.bottom;
                                licensePlateList.Add(licensePlate);

                                Marshal.ReleaseComObject(plate);
                                plate = null;
                            }
                        }

                        if (bestLicensePlate.Text.Length > 0)
                        {
                            //bestLicensePlate.Text = GetCorrectPlate(bestLicensePlate.Text);
                            bestLicensePlate.Text = bestLicensePlate.Text;
                            licensePlateList.Add(bestLicensePlate);
                        }

                        recognitionTime = engine.RecognitionTime;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            LicensePlate bestLicensePlate = new LicensePlate();
            LicensePlateCollection licensePlateList = new LicensePlateCollection();
            recognitionTime = 0;

            try
            {
                if (File.Exists(fileName))
                {
                    engine.ReadFromFile(fileName, customData);

                    if (engine.Plates != null)
                    {
                        for (int i = 0; i < engine.Plates.Count; i++)
                        {
                            Plate plate = engine.Plates.get_Item(i);
                            if (plate != null)
                            {
                                if (plate.Text.Length > bestLicensePlate.Text.Length && plate.Text.Length <= 9)
                                {
                                    bestLicensePlate.Text = plate.Text;
                                    bestLicensePlate.Bitmap = null;
                                    bestLicensePlate.ConfidenceLevel = (float)plate.ConfidenceLevel;
                                    bestLicensePlate.Left = plate.left;
                                    bestLicensePlate.Top = plate.top;
                                    bestLicensePlate.Right = plate.right;
                                    bestLicensePlate.Bottom = plate.bottom;
                                }

                                LicensePlate licensePlate = new LicensePlate();
                                licensePlate.Text = plate.Text;
                                licensePlate.Bitmap = null;
                                licensePlate.ConfidenceLevel = (float)plate.ConfidenceLevel;
                                licensePlate.Left = plate.left;
                                licensePlate.Top = plate.top;
                                licensePlate.Right = plate.right;
                                licensePlate.Bottom = plate.bottom;
                                licensePlateList.Add(licensePlate);

                                Marshal.ReleaseComObject(plate);
                                plate = null;
                            }
                        }

                        if (bestLicensePlate.Text.Length > 0)
                        {
                            //bestLicensePlate.Text = GetCorrectPlate(bestLicensePlate.Text);
                            bestLicensePlate.Text = bestLicensePlate.Text;
                            licensePlateList.Add(bestLicensePlate);
                        }

                        recognitionTime = engine.RecognitionTime;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            GC.Collect();
            return licensePlateList;
        }

        private string GetCorrectPlate(string plate)
        {
            string temp = "";

            for (int i = 0; i < plate.Length; i++)
            {
                if (plate[i].ToString() == "O")
                    temp = temp + "0";
                else
                    if ((i != 2 || plate.Length <= 5) && plate[i].ToString().ToUpper() == "Z")
                        temp = temp + "2";
                    else
                        if ((i != 2 || plate.Length <= 5) && plate[i].ToString().ToUpper() == "I")
                            temp = temp + "1";
                        else
                            if ((i != 2 || plate.Length <= 5 || i!= 0) && (plate[i].ToString().ToUpper() == "D" || plate[i].ToString().ToUpper() == "U" || plate[i].ToString().ToUpper() == "Q"))
                                temp = temp + "0";
                            else
                                if ((i != 2 || plate.Length <= 5) && (plate[i].ToString().ToUpper() == "B" || plate[i].ToString().ToUpper() == "R"))
                                    temp = temp + "8";
                                else
                                    if ((i != 2 || plate.Length <= 5) && plate[i].ToString().ToUpper() == "S")
                                        temp = temp + "5";
                                    //else
                                        //if ((i != 2 || plate.Length <= 5) && plate[i].ToString().ToUpper() == "G")
                                        //    temp = temp + "6";
                                        else
                                            if ((i != 2 || plate.Length <= 5) && (plate[i].ToString().ToUpper() == "L" || plate[i].ToString().ToUpper() == "?"))
                                                temp = temp + "4";
                                            else
                                            {
                                                if ((plate.Length == 8 || plate.Length == 9) && i == 2)
                                                {
                                                    if (plate[2].ToString() == "8" || plate[2].ToString() == "9")
                                                        temp = temp + "R";
                                                    else
                                                        if (plate[2].ToString() == "2")
                                                            temp = temp + "Z";
                                                        else
                                                            if (plate[2].ToString() == "1")
                                                                temp = temp + "I";
                                                            else
                                                                if (plate[2].ToString() == "0")
                                                                    temp = temp + "O";
                                                                else
                                                                    if (plate[2].ToString() == "5")
                                                                        temp = temp + "S";
                                                                    else
                                                                        if (plate[2].ToString() == "6")
                                                                            temp = temp + "G";
                                                                        else
                                                                            if (plate[2].ToString() == "4")
                                                                                temp = temp + "L";
                                                                            else
                                                                                if (plate[2].ToString() == "W")
                                                                                    temp = temp + "M";
                                                                                else
                                                                                    if (plate[2].ToString() == "I" || plate[2].ToString() == "?")
                                                                                        temp = temp + "Y";
                                                                                    else
                                                                                        temp = temp + plate[i];
                                                }
                                                else
                                                    temp = temp + plate[i];
                                            }
            }

            if (temp.Length == 8)
                temp = temp.Substring(0, 2) + "-" + temp.Substring(2, 2) + "-" + temp.Substring(4, 4);// temp.Substring(0, temp.Length - 4) + "-" + temp.Substring(temp.Length - 4, 4);
            else
                if (temp.Length == 9)
                    temp = temp.Substring(0, 2) + "-" + temp.Substring(2, 2) + "-" + temp.Substring(4, 5); //temp.Substring(0, temp.Length - 5) + "-" + temp.Substring(temp.Length - 5, 5);
                else
                    if (temp.Length == 7)
                        temp = temp.Substring(0, 3) + "-" + temp.Substring(3, temp.Length - 3);

            return temp;
        }

        public void ReadFromMemFile(object fileData, int dataLen, int customData)
        {
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
            set
            {
                scanRectangle = value;
            }
        }
        public string APIUrl { get; set; }
    }
}
