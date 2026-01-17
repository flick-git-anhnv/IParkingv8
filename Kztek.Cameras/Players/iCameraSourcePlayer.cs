using AForge.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
namespace Kztek.Cameras
{
    public interface iCameraSourcePlayer
    {
        event NewFrameHandler NewFrame;
        event EventHandler Alarm;
        event MotionAnalyzingEventHandler MotionAnalyzing;
        event VideoWriterFinishEventHandler VideoWriterFinish;
        //event PlayingFinishedEventHandler PlayingFinished;
        //event VideoSourceErrorEventHandler VideoSourceError;

        // Mouse Event
        event OnMouseDownHandler OnMouseDownEvent;
        event OnMouseUpHandler OnMouseUpEvent;
        event OnMouseWheelHandler OnMouseWheelEvent;
        event OnMouseMoveHandler OnMouseMoveEvent;
        event OnDoubleClickHandler OnDoubleClickEvent;

        // event
        event EventHandler OnCameraMouseDown;
        event EventHandler OnCameraDoubleClick;

        CameraStreamInfo VideoSource
        {
            get;
            set;
        }
        System.Drawing.Color MotionRectangleColor
        {
            set;
        }
        int MotionRectangleLineWidth
        {
            set;
        }
        System.Drawing.Rectangle[] MotionRectangles
        {
            set;
        }
        int MotionAlarmLevel
        {
            set;
        }
        int MotionDetectionInterval
        {
            set;
        }
        bool SuppressNoise
        {
            set;
        }
        bool DisplayMotionRegion
        {
            set;
        }
        bool FlipX
        {
            set;
        }
        bool FlipY
        {
            set;
        }
        bool Rotare90
        {
            set;
        }
        System.Drawing.Color BorderColor
        {
            set;
        }
        string TextOverlay
        {
            get;
            set;
        }
        TextOverlayLocation TextOverlayLocation
        {
            get;
            set;
        }
        System.Drawing.Color TextOverlayColor
        {
            get;
            set;
        }
        int TextOverlaySize
        {
            get;
            set;
        }
        bool EmbededDateTime
        {
            get;
            set;
        }
        bool ShowOverlayFrameRate
        {
            get;
            set;
        }
        int DisplayRate
        {
            set;
        }
        bool FastRendering
        {
            set;
        }
        int RecordingMethod
        {
            set;
        }
        int RecordingRate
        {
            set;
        }
        string RecordingResolution
        {
            set;
        }
        string RecordingCodec
        {
            set;
        }
        string RecordingFolder
        {
            set;
        }
        string RecordingCameraFolder
        {
            set;
        }
        int RecordingVideoLength
        {
            set;
        }
        float FPS
        {
            get;
        }
        float BPS
        {
            get;
        }
        bool IsRecording
        {
            get;
        }
        bool IsRunning
        {
            get;
        }
        DateTime LastTimeReceivedFrame
        {
            get;
            set;
        }
        void Start();
        void SignalToStop();
        void Stop();
        void StartRecord();
        void StopRecord();
        void StartMotionDetection();
        void StopMotionDetection();
        void SaveCurrentVideoFrame(string path);
        System.Drawing.Bitmap GetCurrentVideoFrame();
        System.Drawing.Bitmap GetCurrentVideoFrame(int destWidth, int destHeight);
        System.Drawing.Bitmap GetCurrentVideoFrame2();
        UnmanagedImage GetCurrentUnmanagedVideoFrame();
        void Flash(int seconds);
        bool SetDeviceDateTime(DateTime dateTime);
        bool GetDeviceDateTime(ref DateTime dateTime);

        List<Rectangle> lprDetectRegions { get; set; }
        List<Rectangle> motionDetectRegions { get; set; }
        List<Rectangle> faceDetectRegions { get; set; }
    }
}
