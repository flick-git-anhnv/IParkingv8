//----------------------------------------------------------------------------
//  Copyright (C) 2004-2011 by EMGU. All rights reserved.       
//----------------------------------------------------------------------------

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

using SimpleLPR2;
using System.Xml;

namespace Futech.LPR
{
    public class FutechLPR2 : ILPR
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

        private ISimpleLPR _lpr;
        private IProcessor _proc;
        private List<Candidate> _curCands;
        private Rectangle[] scanRectangle = null; // Thiet lap vung nhan dang

        // Constructor
        public FutechLPR2()
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
                SimpleLPR2.VersionNumber ver = _lpr.versionNumber;
                return string.Format("Version {0}.{1}.{2}.{3}", ver.A, ver.B, ver.C, ver.D);
            }
        }

        // Methods
        public void Init(bool bVideo) // True ~ Video Mode, False ~ Still Image
        {
            try
            {
                _lpr = SimpleLPR.Setup();

                //SimpleLPR2.VersionNumber ver = _lpr.versionNumber;
                //this.Text = string.Format("SimpleLPR UI --- Version {0}.{1}.{2}.{3}", ver.A, ver.B, ver.C, ver.D);

                if (_proc == null)
                {
                    //string strKey = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                    //      "<license>" +
                    //          "<e k=\"PRODUCT\" v=\"SimpleLPR\" />" +
                    //          "<e k=\"CITY\" v=\"Ha Noi\" />" +
                    //          "<e k=\"COMPANY\" v=\"Futech Jsc\" />" +
                    //          "<e k=\"COUNTRY\" v=\"Viet Nam\" />" +
                    //          "<e k=\"EMAIL\" v=\"thanh.dinhcong@futech.vn\" />" +
                    //          "<e k=\"ENCODING\" v=\"UTF8\" />" +
                    //          "<e k=\"FAX\" v=\"+84 4 355 27 206\" />" +
                    //          "<e k=\"FIRSTNAME\" v=\"Thanh\" />" +
                    //          "<e k=\"ISO_CODE\" v=\"VN\" />" +
                    //          "<e k=\"LANGUAGE_ID\" v=\"1\" />" +
                    //          "<e k=\"LASTNAME\" v=\"Dinh Cong\" />" +
                    //          "<e k=\"NLALLOW\" v=\"YES\" />" +
                    //          "<e k=\"PHONE\" v=\"+84 4 355 27 205\" />" +
                    //          "<e k=\"PRODUCT_ID\" v=\"300183841\" />" +
                    //          "<e k=\"PURCHASE_DATE\" v=\"12/06/2012\" />" +
                    //          "<e k=\"PURCHASE_ID\" v=\"417040905\" />" +
                    //          "<e k=\"QUANTITY\" v=\"1\" />" +
                    //          "<e k=\"REG_NAME\" v=\"Futech Jsc\" />" +
                    //          "<e k=\"RUNNING_NO\" v=\"1\" />" +
                    //          "<e k=\"STREET\" v=\"65/172 Lac Long Quan, tay Ho, Ha Noi, Viet Nam\" />" +
                    //          "<e k=\"ZIP\" v=\"+84\" />" +
                    //          "<b v=\"tN2ZUeWkp+J7aNoJQJoGTA==\" />" +
                    //          "<b v=\"F8QL52PM1QsyTntjAhsMFg==\" />" +
                    //          "<b v=\"bGzZQfJkpvJeugnm2kwKgQ==\" />" +
                    //    "</license>";
                    //string strKey = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                    //"<license>" +
                    //  "<e k=\"PRODUCT\" v=\"SimpleLPR\" />" +
                    //  "<e k=\"CITY\" v=\"Hanoi\" />" +
                    //  "<e k=\"COMPANY\" v=\"Futech JSC\" />" +
                    //  "<e k=\"COUNTRY\" v=\"Viet Nam\" />" +
                    //  "<e k=\"EMAIL\" v=\"thanh.bk46@gmail.com\" />" +
                    //  "<e k=\"ENCODING\" v=\"UTF8\" />" +
                    //  "<e k=\"FIRSTNAME\" v=\"Thanh\" />" +
                    //  "<e k=\"ISO_CODE\" v=\"VN\" />" +
                    //  "<e k=\"LANGUAGE_ID\" v=\"1\" />" +
                    //  "<e k=\"LASTNAME\" v=\"Dinh Cong\" />" +
                    //  "<e k=\"NLALLOW\" v=\"NO\" />" +
                    //  "<e k=\"PRODUCT_ID\" v=\"300183841\" />" +
                    //  "<e k=\"PROMOTION_COUPON_CODE\" v=\"8PG6Y6KGRNCC6GZ5\" />" +
                    //  "<e k=\"PROMOTION_NAME\" v=\"License Renewal\" />" +
                    //  "<e k=\"PURCHASE_DATE\" v=\"12/12/2013\" />" +
                    //  "<e k=\"PURCHASE_ID\" v=\"457335445\" />" +
                    //  "<e k=\"QUANTITY\" v=\"1\" />" +
                    //  "<e k=\"REG_NAME\" v=\"Futech JSC\" />" +
                    //  "<e k=\"RUNNING_NO\" v=\"1\" />" +
                    //  "<e k=\"STREET\" v=\"Hanoi, Vietnam\" />" +
                    //  "<e k=\"ZIP\" v=\"+84\" />" +
                    //  "<b v=\"WwAXxdkXuxX2L9ZuN3N0qA==\" />" +
                    //  "<b v=\"VoRoTNPlFAaIkEdiomVOng==\" />" +
                    //  "<b v=\"U8EGVw3607J6/ltq2wwMkw==\" />" +
                    //"</license>";
                    string strKey = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                        "<license>" +
                          "<e k=\"PRODUCT\" v=\"SimpleLPR\" />" +
                          "<e k=\"CITY\" v=\"Ha Noi\" />" +
                          "<e k=\"COUNTRY\" v=\"Viet Nam\" />" +
                          "<e k=\"EMAIL\" v=\"thanh.bk46@gmail.com\" />" +
                          "<e k=\"ENCODING\" v=\"UTF8\" />" +
                          "<e k=\"FIRSTNAME\" v=\"Dinh\" />" +
                          "<e k=\"ISO_CODE\" v=\"VN\" />" +
                          "<e k=\"ITEM_NAME\" v=\"SimpleLPR\" />" +
                          "<e k=\"LANGUAGE_ID\" v=\"1\" />" +
                          "<e k=\"LASTNAME\" v=\"Thanh\" />" +
                          "<e k=\"NLALLOW\" v=\"NO\" />" +
                          "<e k=\"PHONE\" v=\"+84 0983090189\" />" +
                          "<e k=\"PRODUCT_ID\" v=\"300183841\" />" +
                          "<e k=\"PRODUCT_NAME\" v=\"SimpleLPR\" />" +
                          "<e k=\"PURCHASE_DATE\" v=\"14/09/2017\" />" +
                          "<e k=\"PURCHASE_ID\" v=\"543969313\" />" +
                          "<e k=\"QUANTITY\" v=\"1\" />" +
                          "<e k=\"REG_NAME\" v=\"Dinh Thanh\" />" +
                          "<e k=\"RUNNING_NO\" v=\"1\" />" +
                          "<e k=\"STREET\" v=\"65/172 Lac Long Quan, Tay Ho, Ha Noi, Viet Nam\" />" +
                          "<e k=\"ZIP\" v=\"+84\" />" +
                          "<b v=\"G+40F10vtTH+hAObFHtIbw==\" />" +
                          "<b v=\"76gkIiM6nL/kIygk6B+iGQ==\" />" +
                          "<b v=\"HTGt/vQXsm6QgTBiYMsIDw==\" />" +
                        "</license>";

                    System.Text.UTF8Encoding encText = new System.Text.UTF8Encoding();
                    byte[] btKey = encText.GetBytes(strKey);

                    _lpr.set_productKey(btKey);
                    //_lpr.set_productKey("key.xml");

                    _proc = _lpr.createProcessor();
                }
                _lpr.set_countryWeight("Vietnam", 1.0f);
                _lpr.realizeCountryWeights();
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
                Stopwatch watch = Stopwatch.StartNew(); // time the detection process // using System.Diagnostics;

                Bitmap lastFrame = bmp1.Clone(new Rectangle(0, 0, bmp1.Width, bmp1.Height), PixelFormat.Format24bppRgb);

                try
                {
                    _curCands = _proc.analyze(lastFrame, (uint)maxCharHeight); // 120
                }
                catch (System.Exception ex)
                { 
                    //MessageBox.Show("lpr: " + ex.Message);
                }

                watch.Stop(); //stop the timer
                recognitionTime = (int)watch.Elapsed.TotalMilliseconds; // Thoi gian nhan dang ms

                if (_curCands != null && _curCands.Count > 0)
                {
                    Candidate bestCand = _curCands[_curCands.Count - 1];

                    for (int i = 0; i < _curCands.Count - 1; ++i)
                    {
                        if (_curCands[i].confidence > bestCand.confidence)// && !_curCands[i].text.Contains("777") && !_curCands[i].text.Contains("111"))
                            bestCand = _curCands[i];

                        LicensePlate licensePlate = new LicensePlate();
                        //licensePlate.Text = GetCorrectPlate(_curCands[i].text);
                        licensePlate.Text = _curCands[i].text;
                        //licensePlate.Bitmap = bmp;
                        licensePlate.ConfidenceLevel = _curCands[i].confidence;
                        //licensePlate.Left = x1;
                        //licensePlate.Top = y1;
                        //licensePlate.Right = x2;
                        //licensePlate.Bottom = y2;
                        licensePlateList.Add(licensePlate);
                    }

                    int x1 = bestCand.elements[0].bbox.Left, y1 = bestCand.elements[0].bbox.Top, x2 = bestCand.elements[0].bbox.Right, y2 = bestCand.elements[0].bbox.Bottom;
                    foreach (Element et in bestCand.elements)
                    {
                        if (x1 > et.bbox.Left)
                            x1 = et.bbox.Left;
                        if (y1 > et.bbox.Top)
                            y1 = et.bbox.Top;
                        if (x2 < et.bbox.Right)
                            x2 = et.bbox.Right;
                        if (y2 < et.bbox.Bottom)
                            y2 = et.bbox.Bottom;
                    }

                    if (x1 >= 10)
                        x1 = x1 - 10;
                    if (y1 >= 10)
                        y1 = y1 - 10;
                    if (x2 <= lastFrame.Width - 10)
                        x2 = x2 + 10;
                    if (y2 <= lastFrame.Height - 10)
                        y2 = y2 + 10;

                    Bitmap bmp = null;
                    Rectangle rec = new Rectangle(x1, y1, x2 - x1, y2 - y1);
                    bmp = bmp1.Clone(rec, bmp1.PixelFormat);

                    LicensePlate bestLicensePlate = new LicensePlate();
                    //bestLicensePlate.Text = GetCorrectPlate(bestCand.text);
                    bestLicensePlate.Text = bestCand.text;
                    bestLicensePlate.Bitmap = bmp;
                    bestLicensePlate.ConfidenceLevel = bestCand.confidence;
                    bestLicensePlate.Left = x1;
                    bestLicensePlate.Top = y1;
                    bestLicensePlate.Right = x2;
                    bestLicensePlate.Bottom = y2;

                    // Xac dinh loai bien
                    #region Xac dinh loai bien
                    try
                    {
                        if (bestCand.brightBackground)
                        {
                            bestLicensePlate.BrightBackground = 0;
                            bestLicensePlate.ColorBackground = Color.White;
                            bestLicensePlate.ColorText = Color.Black;
                        }
                        else
                        {
                            int RedColor = 0, GreenColor = 0, BlueColor = 0;
                            for (int i = 0; i < bmp.Height; i++)
                                for (int j = 0; j < bmp.Width; j++)
                                {
                                    RedColor += bmp.GetPixel(j, i).R;
                                    GreenColor += bmp.GetPixel(j, i).G;
                                    BlueColor += bmp.GetPixel(j, i).B;
                                }

                            RedColor = RedColor / (bmp.Height * bmp.Width);
                            GreenColor = GreenColor / (bmp.Height * bmp.Width);
                            BlueColor = BlueColor / (bmp.Height * bmp.Width);

                            if (RedColor >= 150 && BlueColor < 150)
                            {
                                bestLicensePlate.BrightBackground = 2;
                                bestLicensePlate.ColorBackground = Color.Red;
                                bestLicensePlate.ColorText = Color.White;
                            }
                            else
                            {
                                bestLicensePlate.BrightBackground = 1;
                                bestLicensePlate.ColorBackground = Color.FromArgb(0, 50, 255);
                                bestLicensePlate.ColorText = Color.White;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("BrightBackground: " + ex.Message);
                    }
                    #endregion

                    licensePlateList.Add(bestLicensePlate);

                    if (bestLicensePlate.Text == "")
                        licensePlateList = null;
                }

                lastFrame.Dispose();
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
                Bitmap lastFrame = (Bitmap)Bitmap.FromFile(fileName);

                Stopwatch watch = Stopwatch.StartNew(); // time the detection process // using System.Diagnostics;

                try
                {
                    _curCands = _proc.analyze(fileName, (uint)maxCharHeight); // 120
                }
                catch (System.Exception ex) 
                { 
                    //MessageBox.Show("00-LPR: " + ex.Message);
                }

                watch.Stop(); //stop the timer
                recognitionTime = (int)watch.Elapsed.TotalMilliseconds; // Thoi gian nhan dang ms

                try
                {
                    if (_curCands != null && _curCands.Count > 0)
                    {
                        Candidate bestCand = _curCands[_curCands.Count - 1];

                        for (int i = 0; i < _curCands.Count - 1; ++i)
                        {
                            if (_curCands[i].confidence > bestCand.confidence)// && !_curCands[i].text.Contains("777") && !_curCands[i].text.Contains("111"))
                                bestCand = _curCands[i];

                            LicensePlate licensePlate = new LicensePlate();
                           // licensePlate.Text = GetCorrectPlate(_curCands[i].text);
                            licensePlate.Text = _curCands[i].text;
                            //licensePlate.Bitmap = bmp;
                            licensePlate.ConfidenceLevel = _curCands[i].confidence;
                            //licensePlate.Left = x1;
                            //licensePlate.Top = y1;
                            //licensePlate.Right = x2;
                            //licensePlate.Bottom = y2;
                            licensePlateList.Add(licensePlate);
                        }

                        int x1 = bestCand.elements[0].bbox.Left, y1 = bestCand.elements[0].bbox.Top, x2 = bestCand.elements[0].bbox.Right, y2 = bestCand.elements[0].bbox.Bottom;
                        foreach (Element et in bestCand.elements)
                        {
                            if (x1 > et.bbox.Left)
                                x1 = et.bbox.Left;
                            if (y1 > et.bbox.Top)
                                y1 = et.bbox.Top;
                            if (x2 < et.bbox.Right)
                                x2 = et.bbox.Right;
                            if (y2 < et.bbox.Bottom)
                                y2 = et.bbox.Bottom;
                        }

                        if (x1 >= 10)
                            x1 = x1 - 10;
                        if (y1 >= 10)
                            y1 = y1 - 10;
                        if (x2 <= lastFrame.Width - 10)
                            x2 = x2 + 10;
                        if (y2 <= lastFrame.Height - 10)
                            y2 = y2 + 10;

                        Bitmap bmp = null;
                        Rectangle rec = new Rectangle(x1, y1, x2 - x1, y2 - y1);
                        bmp = lastFrame.Clone(rec, lastFrame.PixelFormat);

                        LicensePlate bestLicensePlate = new LicensePlate();
                       // bestLicensePlate.Text = GetCorrectPlate(bestCand.text);
                        bestLicensePlate.Text = bestCand.text;
                        bestLicensePlate.Bitmap = bmp;
                        bestLicensePlate.ConfidenceLevel = bestCand.confidence;
                        bestLicensePlate.Left = x1;
                        bestLicensePlate.Top = y1;
                        bestLicensePlate.Right = x2;
                        bestLicensePlate.Bottom = y2;

                        // Xac dinh loai bien
                        #region Xac dinh loai bien
                        if (bestCand.brightBackground)
                        {
                            bestLicensePlate.BrightBackground = 0;
                            bestLicensePlate.ColorBackground = Color.White;
                            bestLicensePlate.ColorText = Color.Black;
                        }
                        else
                        {
                            int RedColor = 0, GreenColor = 0, BlueColor = 0;
                            for (int i = 0; i < bmp.Height; i++)
                                for (int j = 0; j < bmp.Width; j++)
                                {
                                    RedColor += bmp.GetPixel(j, i).R;
                                    GreenColor += bmp.GetPixel(j, i).G;
                                    BlueColor += bmp.GetPixel(j, i).B;
                                }

                            RedColor = RedColor / (bmp.Height * bmp.Width);
                            GreenColor = GreenColor / (bmp.Height * bmp.Width);
                            BlueColor = BlueColor / (bmp.Height * bmp.Width);

                            if (RedColor >= 150 && BlueColor < 150)
                            {
                                bestLicensePlate.BrightBackground = 2;
                                bestLicensePlate.ColorBackground = Color.Red;
                                bestLicensePlate.ColorText = Color.White;
                            }
                            else
                            {
                                bestLicensePlate.BrightBackground = 1;
                                bestLicensePlate.ColorBackground = Color.FromArgb(0, 50, 255);
                                bestLicensePlate.ColorText = Color.White;
                            }
                        }
                        #endregion

                        licensePlateList.Add(bestLicensePlate);

                        if (bestLicensePlate.Text == "")
                            licensePlateList = null;
                    }
                }
                catch (Exception ex)
                { 
                    //MessageBox.Show("01-LPR: " + ex.Message); 
                }

                lastFrame.Dispose();
            }

            GC.Collect();
            return licensePlateList;
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
