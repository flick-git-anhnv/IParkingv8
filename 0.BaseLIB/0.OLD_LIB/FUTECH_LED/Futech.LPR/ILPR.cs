using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Futech.LPR
{
    public interface ILPR
    {
        // Events

        // Properties
        int ContrastLevel { get;set;} // Medium, Low, High ~ Medium

        bool Deinterlace { get;set;} // ~ False

        bool DeinterlacedSource { get;set;} // ~ False

        int DeviationAnge { get;set;} // ~ 30

        int Fps { get;set;} // Only Video Mode

        bool HistogramEquualization { get;set;} // ~ False

        int MaxCharHeight { get;set;} // ~ 64

        int MinCharHeight { get;set;} // ~ 8

        int MotionDetectionTriggering { get;set;} // 0-100 ~ 0 Only Video Mode

        int PlateColorSchema { get;set;} // BlackOnWhite, WhiteOnBlack, All ~ BlackOnWhite

        int PlatePresenceTime { get;set;} // ~ 1000 (1 sec) Only Video Mode

        int PreciseMode { get;set;} // Normal, Mode1, Mode2, Night ~ Normal

        int RotateAngle { get;set;} // ~ 0

        string GetVersion { get;} // Phien ban engine nhan dang bienn so xe

        // Methods
        void Init(bool bVideo); // True ~ Video Mode, False ~ Still Image

        void Dispose();  // 

        void ReadFromBitmap(int hBmp, int customData);

        LicensePlateCollection ReadFromBitmap(Bitmap lastFrame, int customData, ref int recognitionTime);

        void ReadFromBuffer(int pBuffer, int width, int height, int stride, int bpp, int customData);

        void ReadFromDIB(int hDIB, int customData);

        LicensePlateCollection ReadFromFile(string fileName, int customData, ref int recognitionTime);

        void ReadFromMemFile(object fileData, int dataLen, int customData);

        void Reset();

        void SetCountryCode(string countryCode);

        void SetMaskFromBitmap(int hBitmap);

        void SetMaskFromBitmap(Bitmap Bitmap);

        void SetMaskFromFile(string imageFileName);

        void SetScanRectangle(int left, int top, int right, int bottom);

        Rectangle[] ScanRectangle { get;set;}

        string APIUrl { get; set; }
    }
}
