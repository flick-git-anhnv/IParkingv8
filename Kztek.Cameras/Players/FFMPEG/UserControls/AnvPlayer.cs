using AForge.Imaging;
using AForge.Vision.Motion;
using Kztek.Cameras.Players.FFMPEG;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kztek.Cameras
{
    public class AnvPlayer : UserControl, iCameraSourcePlayer
    {
        public List<Rectangle> lprDetectRegions { get; set; } = new List<Rectangle>();
        public List<Rectangle> motionDetectRegions { get; set; } = new List<Rectangle>();
        public List<Rectangle> faceDetectRegions { get; set; } = new List<Rectangle>();

        public string currentPartition = "";
        public int delayBetweenTwoMotion = 1000;
        private System.Timers.Timer videoLengthTimer = new System.Timers.Timer();
        private int videoLengthIndex;
        private string currentVideoFile = "";
        private string httpURL = "";
        private int tcpPort = 80;
        private int channel;
        private string login = "admin";
        private string password = "admin";
        private CameraStreamInfo videoSource;

        private bool suppressNoise = true;
        private bool displayMotionRegion;
        private bool flipX;
        private bool flipY;
        private bool rotare90;
        private System.Drawing.Color borderColor = System.Drawing.Color.Black;
        private string textOverlay = "";
        private TextOverlayLocation textOverlayLocation = TextOverlayLocation.BottomLeft;
        private System.Drawing.Color textOverlayColor = System.Drawing.Color.Blue;
        private int textOverlaySize = 12;
        private bool embededDateTime;
        private bool showOverlayFrameRate;
        private int displayRate;
        private bool fastRendering;
        private int recordingMethod;
        private int recordingRate = 5;
        private string recordingResolution = "";
        private string recordingCodec = "div4";
        private string recordingFolder = "";
        private string recordingCameraFolder = "";
        private int recordingVideoLength = 60;
        private float fps;
        private float bps;
        private bool isRecording;
        private bool isPlaying;
        private DateTime lastTimeReceivedFrame = DateTime.Now;
        private bool prepareToStop;
        private DateTime lastTimeSnapshot = DateTime.MinValue;
        private int captureIndex = 1;
        private System.Drawing.Bitmap lastFrame;
        private bool readyToCapture = true;
        private DateTime currentRecStartTime = DateTime.Now;
        private DateTime lastTimeUpdateInfo = DateTime.Now;
        private int resolution_Width = 1280;
        private int resolution_Height = 720;

        public event NewFrameHandler NewFrame;
        public event EventHandler Alarm;
        public event MotionAnalyzingEventHandler MotionAnalyzing;
        public event VideoWriterFinishEventHandler VideoWriterFinish;

        public event OnMouseDownHandler OnMouseDownEvent;
        public event OnMouseUpHandler OnMouseUpEvent;
        public event OnMouseWheelHandler OnMouseWheelEvent;
        public event OnMouseMoveHandler OnMouseMoveEvent;
        public event OnDoubleClickHandler OnDoubleClickEvent;

        // event
        public event EventHandler OnCameraMouseDown;
        public event EventHandler OnCameraDoubleClick;

        public CameraStreamInfo VideoSource
        {
            get
            {
                return this.videoSource;
            }
            set
            {
                this.videoSource = value;
                this.httpURL = this.videoSource.Source;
                this.login = this.videoSource.Login;
                this.password = this.videoSource.Password;
            }
        }

        public bool SuppressNoise
        {
            get
            {
                return this.suppressNoise;
            }
            set
            {
                this.suppressNoise = value;
            }
        }

        public bool DisplayMotionRegion
        {
            get
            {
                return this.displayMotionRegion;
            }
            set
            {
                this.displayMotionRegion = value;
            }
        }

        public bool FlipX
        {
            get
            {
                return this.flipX;
            }
            set
            {
                this.flipX = value;
            }
        }

        public bool FlipY
        {
            get
            {
                return this.flipY;
            }
            set
            {
                this.flipY = value;
            }
        }

        public bool Rotare90
        {
            get
            {
                return this.rotare90;
            }
            set
            {
                this.rotare90 = value;
            }
        }

        [DefaultValue(typeof(System.Drawing.Color), "Black")]
        public System.Drawing.Color BorderColor
        {
            get
            {
                return this.borderColor;
            }
            set
            {
                this.borderColor = value;
                this.BackColor = this.borderColor;
                base.Invalidate();
            }
        }

        public string TextOverlay
        {
            get
            {
                return this.textOverlay;
            }
            set
            {
                this.textOverlay = value;
            }
        }

        public TextOverlayLocation TextOverlayLocation
        {
            get
            {
                return this.textOverlayLocation;
            }
            set
            {
                this.textOverlayLocation = value;
            }
        }

        public System.Drawing.Color TextOverlayColor
        {
            get
            {
                return this.textOverlayColor;
            }
            set
            {
                this.textOverlayColor = value;
            }
        }

        public int TextOverlaySize
        {
            get
            {
                return this.textOverlaySize;
            }
            set
            {
                this.textOverlaySize = value;
            }
        }

        public bool EmbededDateTime
        {
            get
            {
                return this.embededDateTime;
            }
            set
            {
                this.embededDateTime = value;
            }
        }

        public bool ShowOverlayFrameRate
        {
            get
            {
                return this.showOverlayFrameRate;
            }
            set
            {
                this.showOverlayFrameRate = value;
            }
        }

        public int DisplayRate
        {
            set
            {
                this.displayRate = value;
            }
        }

        public bool FastRendering
        {
            set
            {
                this.fastRendering = value;
            }
        }

        public int RecordingMethod
        {
            get
            {
                return this.recordingMethod;
            }
            set
            {
                this.recordingMethod = value;
            }
        }

        public int RecordingRate
        {
            get
            {
                return this.recordingRate;
            }
            set
            {
                this.recordingRate = value;
            }
        }

        public string RecordingResolution
        {
            get
            {
                return this.recordingResolution;
            }
            set
            {
                this.recordingResolution = value;
            }
        }

        public string RecordingCodec
        {
            get
            {
                return this.recordingCodec;
            }
            set
            {
                this.recordingCodec = value;
            }
        }

        public string RecordingFolder
        {
            get
            {
                return this.recordingFolder;
            }
            set
            {
                this.recordingFolder = value;
            }
        }

        public string RecordingCameraFolder
        {
            get
            {
                return this.recordingCameraFolder;
            }
            set
            {
                this.recordingCameraFolder = value;
            }
        }

        public int RecordingVideoLength
        {
            get
            {
                return this.recordingVideoLength;
            }
            set
            {
                this.recordingVideoLength = value;
            }
        }

        public float FPS
        {
            get
            {
                return this.fps;
            }
        }

        public float BPS
        {
            get
            {
                return this.bps;
            }
        }

        public bool IsRecording
        {
            get
            {
                return this.isRecording;
            }
            private set
            {
                this.isRecording = value;
            }
        }

        public bool IsRunning
        {
            get
            {
                return this.isPlaying;
            }
        }

        public DateTime LastTimeReceivedFrame
        {
            get
            {
                return this.lastTimeReceivedFrame;
            }
            set
            {
                this.lastTimeReceivedFrame = value;
            }
        }

        public string Resolution
        {
            get
            {
                return this.resolution_Height + "x" + this.resolution_Height;
            }
            set
            {
                string resolution = value;
                if (resolution.Contains(("x")))
                {
                    string[] resols = resolution.Split('x');
                    if (resols.Length == 2)
                    {
                        int.TryParse(resols[0], out resolution_Width);
                        int.TryParse(resols[1], out resolution_Height);
                        if (resolution_Width < 640) resolution_Width = 640;
                        if (resolution_Height < 480) resolution_Height = 480;
                    }
                }
            }
        }


        [Browsable(true)]
        public string RtspURL { get; set; }
        private VideoStreamDecoderIntptr _decoder;
        private Bitmap? _currentImage;
        private CancellationTokenSource? _cts;
        public static object _frameLock = new object();

        #region Constructor
        public AnvPlayer() : this(false, 0)
        {

        }
        public AnvPlayer(bool isMotionDetection, double alarmLevel)
        {
            Width = 640;
            Height = 480;
            this.isMotionDetection = isMotionDetection;
            this.alarmLevel = alarmLevel;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.UpdateStyles();

            if (this.isMotionDetection)
            {
                this.motionDetector = new MotionDetector(new TwoFramesDifferenceDetector(true), null);
            }
        }
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);

        //    var img = _currentImage;
        //    if (img == null)
        //    {
        //        e.Graphics.DrawString("No image", Font, Brushes.Gray, new PointF(10, 10));
        //        return;
        //    }
        //    try
        //    {
        //        e.Graphics.CompositingMode = CompositingMode.SourceCopy;
        //        e.Graphics.InterpolationMode = InterpolationMode.Bilinear;
        //        e.Graphics.DrawImage(img, new Rectangle(0, 0, Width, Height));
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var img = Volatile.Read(ref _currentImage);
            if (img == null || img.Width <= 0 || img.Height <= 0 || Width <= 0 || Height <= 0)
            {
                string text = "Connecting...";
                using (var format = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                })
                {
                    // Vẽ nền mờ
                    using (Brush backBrush = new SolidBrush(Color.FromArgb(80, Color.Black)))
                    {
                        e.Graphics.FillRectangle(backBrush, ClientRectangle);
                    }

                    // Vẽ chữ
                    e.Graphics.DrawString(text, Font, Brushes.White, ClientRectangle, format);
                }
                return;
            }

            // Vẽ frame
            e.Graphics.CompositingMode = CompositingMode.SourceCopy;
            e.Graphics.SmoothingMode = SmoothingMode.None;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
            e.Graphics.InterpolationMode = InterpolationMode.Bilinear;

            e.Graphics.DrawImage(img, ClientRectangle);

            // 🔽 Sau khi vẽ ảnh, vẽ overlay các vùng config

            // Dùng smoothing cho overlay để viền đẹp hơn
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.CompositingMode = CompositingMode.SourceOver;
            int wImg = img.Width;
            int hImg = img.Height;

            // ==== snapshot vùng detect để tránh race với thread khác ====
            List<Rectangle> lprSnapshot;
            List<Rectangle> motionSnapshot;
            List<Rectangle> faceSnapshot;

            lock (_regionsLock)
            {
                lprSnapshot = new List<Rectangle>(lprDetectRegions);
                motionSnapshot = new List<Rectangle>(motionDetectRegions);
                faceSnapshot = new List<Rectangle>(faceDetectRegions);
            }

            DrawRegionList(e.Graphics, lprSnapshot, Color.LimeGreen, "LPR", wImg, hImg);
            DrawRegionList(e.Graphics, motionSnapshot, Color.OrangeRed, "LOOP", wImg, hImg);
            DrawRegionList(e.Graphics, faceSnapshot, Color.DeepSkyBlue, "FACE", wImg, hImg);
        }

        private readonly object _regionsLock = new object();
        public void SetFaceRegions(IEnumerable<Rectangle> regions)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)(() => SetFaceRegions(regions)));
                return;
            }

            lock (_regionsLock)
            {
                faceDetectRegions.Clear();
                faceDetectRegions.AddRange(regions);
            }

            Invalidate(); // vẽ lại để thấy overlay
        }

        private void DrawRegionList(
                 Graphics g,
                 List<Rectangle> regions,
                 Color color,
                 string tag,
                 int imgWidth,
                 int imgHeight)
        {
            if (regions == null || regions.Count == 0)
                return;
            if (imgWidth <= 0 || imgHeight <= 0)
                return;

            using var pen = new Pen(color, 2f);
            using var fill = new SolidBrush(Color.FromArgb(40, color));
            using var textBrush = new SolidBrush(Color.White);
            using var bgBrush = new SolidBrush(Color.FromArgb(180, 0, 0, 0));
            var font = this.Font;

            foreach (var r in regions)
            {
                if (r.Width <= 0 || r.Height <= 0)
                    continue;

                Rectangle viewRect = ScaleRectToView(r, ClientRectangle, imgWidth, imgHeight);
                if (viewRect.Width <= 0 || viewRect.Height <= 0)
                    continue;

                // Fill trong
                g.FillRectangle(fill, viewRect);
                // Viền
                g.DrawRectangle(pen, viewRect);

                // Label: TAG
                string text = tag;
                var textSize = g.MeasureString(text, font);

                var labelRect = new RectangleF(
                    viewRect.X,
                    viewRect.Y - textSize.Height - 2,
                    textSize.Width + 6,
                    textSize.Height + 2);

                if (labelRect.Y < 0)
                    labelRect.Y = viewRect.Y + 2;

                g.FillRectangle(bgBrush, labelRect);
                g.DrawString(text, font, textBrush, labelRect.X + 3, labelRect.Y + 1);
            }
        }

        /// <summary>
        /// Scale rect từ toạ độ ảnh (imgWidth x imgHeight)
        /// sang toạ độ control (viewRect),
        /// PHÙ HỢP với cách DrawImage(img, viewRect) hiện tại (non-uniform).
        /// </summary>
        private static Rectangle ScaleRectToView(Rectangle src, Rectangle viewRect, int imgWidth, int imgHeight)
        {
            if (imgWidth <= 0 || imgHeight <= 0 || viewRect.Width <= 0 || viewRect.Height <= 0)
                return Rectangle.Empty;

            // non-uniform scale, giống hệt DrawImage(img, viewRect)
            float scaleX = (float)viewRect.Width / imgWidth;
            float scaleY = (float)viewRect.Height / imgHeight;

            float x = viewRect.X + src.X * scaleX;
            float y = viewRect.Y + src.Y * scaleY;
            float w = src.Width * scaleX;
            float h = src.Height * scaleY;

            return new Rectangle(
                (int)Math.Round(x),
                (int)Math.Round(y),
                (int)Math.Round(w),
                (int)Math.Round(h));
        }

        #endregion

        #region Public Function
        public void Start()
        {
            SetRtspUrl(this.VideoSource.Source);
            Connect(delayBetweenTwoMotion);
        }
        public void Stop()
        {
            _cts?.Cancel();
            _cts = null;

            _decoder?.Dispose();
            _decoder = null;

            var old = Interlocked.Exchange(ref _currentImage, null);
            old?.Dispose();

            isPlaying = false;
        }
        public void SignalToStop()
        {
            _cts?.Cancel();
        }

        public Bitmap GetCurrentImage()
        {
            return this._decoder?.GetFullCurrentFrame();
        }
        public Bitmap GetCurrentVideoFrame(int destWidth, int destHeight)
        {
            return this._decoder?.GetCurrentFrame(destWidth, destHeight);
        }

        public Bitmap GetCurrentVideoFrame()
        {
            return GetCurrentImage();
        }
        public Bitmap GetCurrentVideoFrame2()
        {
            return GetCurrentImage();
        }
        public void SaveCurrentVideoFrame(string path)
        {
            var _currentFrame = GetCurrentImage();
            if (_currentFrame is null)
            {
                return;
            }
            try
            {
                _currentFrame.Save(path, ImageFormat.Png);
            }
            catch (Exception)
            {
            }
        }
        #endregion

        private int decodeTimeoutInSecond = 5;
        #region Polling
        private async Task RunStreamLoop(CancellationToken token, int delayBetweenTwoMotion)
        {
            this.isPlaying = true;
            while (!token.IsCancellationRequested)
            {
                try
                {
                    _decoder?.Dispose();
                    _decoder = new VideoStreamDecoderIntptr(RtspURL, this.isMotionDetection);
                    if (!_decoder.Connect())
                    {
                        await Task.Delay(1000, token);
                        continue;
                    }
                    DateTime lastFrameTime = DateTime.Now;

                    while (!token.IsCancellationRequested)
                    {
                        var bmp = _decoder.TryDecodeFrame(token, this.Width, this.Height, delayBetweenTwoMotion, 5000);
                        if (bmp != null)
                        {
                            lastFrameTime = DateTime.Now;
                        }

                        if ((DateTime.Now - lastFrameTime).TotalSeconds > decodeTimeoutInSecond)
                            throw new Exception("Mất kết nối - không nhận được frame");

                        await Task.Delay(10, token);
                    }
                }
                catch (Exception ex)
                {
                    await Task.Delay(1000, token);
                }
                finally
                {
                }
            }

            this.isPlaying = false;
        }
        private async Task RunUpdateImage(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    if (_decoder != null && _decoder.frameQueue.TryDequeue(out var bmp))
                    {
                        if (IsHandleCreated && !IsDisposed)
                        {
                            // event trước, UI sau
                            Bitmap cloneForEvent = (Bitmap)bmp.Clone();
                            NewFrame?.Invoke(this, ref cloneForEvent);
                            cloneForEvent.Dispose();

                            BeginInvoke((Action)(() =>
                            {
                                try
                                {
                                    var old = Interlocked.Exchange(ref _currentImage, bmp);
                                    old?.Dispose();
                                    Invalidate();
                                }
                                catch { bmp.Dispose(); }
                                finally
                                {
                                }
                            }));
                        }
                        else
                        {
                            bmp.Dispose();
                        }
                    }
                }
                catch { /* ignore lỗi lẻ */ }

                await Task.Delay(5, token); // tránh vòng lặp 100% CPU
            }
        }
        private async Task RunMotionLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    if (_decoder != null && _decoder._mdQueue.TryDequeue(out var bmp))
                    {

                        // 1) Motion detection (background, dùng bản copy nhỏ)
                        try
                        {
                            try { CheckMotionDetection(bmp); }
                            catch { }
                            finally { bmp.Dispose(); }
                        }
                        catch { /* bỏ qua lỗi nhỏ */ }
                    }
                }
                catch { /* ignore lỗi lẻ */ }

                await Task.Delay(5, token); // tránh vòng lặp 100% CPU
            }
        }
        #endregion

        #region Private Function 
        /// <summary>
        /// Truyền vào link RTSP để kết nối
        /// </summary>
        /// <param name="url"></param>
        private void SetRtspUrl(string url)
        {
            this.RtspURL = url;
        }
        /// <summary>
        /// Bắt đầu kết nối
        /// </summary>
        private void Connect(int delayBetweenTwoMotion)
        {
            SignalToStop();
            Stop();

            _cts = new CancellationTokenSource();
            Task.Run(() => RunStreamLoop(_cts.Token, delayBetweenTwoMotion));
            Task.Run(() => RunUpdateImage(_cts.Token));
            Task.Run(() => RunSaveImageforVideo(_cts.Token));

            if (isMotionDetection)
            {
                Task.Run(() => RunMotionLoop(_cts.Token));
            }
        }


        private static Bitmap CloneBitmapFast(Bitmap source)
        {
            if (source == null)
                return null;

            try
            {
                var rect = new Rectangle(0, 0, source.Width, source.Height);
                var clone = new Bitmap(source.Width, source.Height, source.PixelFormat);

                var srcData = source.LockBits(rect, ImageLockMode.ReadOnly, source.PixelFormat);
                var dstData = clone.LockBits(rect, ImageLockMode.WriteOnly, clone.PixelFormat);

                int bytes = Math.Abs(srcData.Stride) * source.Height;
                byte[] buffer = new byte[bytes];

                // Copy dữ liệu từ ảnh gốc sang buffer
                System.Runtime.InteropServices.Marshal.Copy(srcData.Scan0, buffer, 0, bytes);
                // Copy dữ liệu từ buffer sang ảnh clone
                System.Runtime.InteropServices.Marshal.Copy(buffer, 0, dstData.Scan0, bytes);

                source.UnlockBits(srcData);
                clone.UnlockBits(dstData);

                return clone;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region MOTION DETECTION
        private Color motionRectangleColor = Color.Green;
        private int motionRectangleLineWidth = 1;
        private Rectangle[] motionRectangles;
        private int motionAlarmLevel = 15;
        private int motionDetectionInterval = 40;

        [DefaultValue(typeof(Color), "Green")]
        public Color MotionRectangleColor
        {
            get
            {
                return this.motionRectangleColor;
            }
            set
            {
                this.motionRectangleColor = value;
                base.Invalidate();
            }
        }
        [DefaultValue(1)]
        public int MotionRectangleLineWidth
        {
            get
            {
                return this.motionRectangleLineWidth;
            }
            set
            {
                this.motionRectangleLineWidth = value;
                base.Invalidate();
            }
        }
        public Rectangle[] MotionRectangles
        {
            get
            {
                return this.motionRectangles;
            }
            set
            {
                this.motionRectangles = value;
            }
        }
        public int MotionAlarmLevel
        {
            set
            {
                this.motionAlarmLevel = value;
            }
        }
        public int MotionDetectionInterval
        {
            get
            {
                return this.motionDetectionInterval;
            }
            set
            {
                this.motionDetectionInterval = value;
            }
        }

        private bool isMotionDetection = false;
        private readonly double alarmLevel = 0;
        private readonly MotionDetector motionDetector;

        public void StartMotionDetection()
        {
            //isMotionDetection = true;
        }
        public void StopMotionDetection()
        {
            //a = "Stop";
            //isMotionDetection = false;
        }
        public void CheckMotionDetection(Bitmap bitmap)
        {
            try
            {
                using Bitmap bmp = (Bitmap)bitmap.Clone();
                UnmanagedImage currentVideoFrame = UnmanagedImage.FromManagedImage(bmp);
                if (this.motionDetector != null && currentVideoFrame != null)
                {
                    float motionLevel = motionDetector.ProcessFrame(currentVideoFrame);
                    float alarmLevel = (float)this.alarmLevel / 1000f;
                    Task.Run(new Action(() =>
                    {
                        if (this.MotionAnalyzing != null)
                        {
                            this.MotionAnalyzing(this, motionLevel);
                        }
                    }));
                }
                currentVideoFrame?.Dispose();
            }
            catch (Exception ex)
            {
            }
        }
        public UnmanagedImage GetCurrentUnmanagedVideoFrame()
        {
            return null;
        }
        #endregion End MOTION DETECTION

        #region RECORD

        // Biến quản lý việc ghi hình thủ công
        // --- CÁC BIẾN TOÀN CỤC ---
        private Process? _recordProcess = null;
        private Stream? _recordStream = null;
        private object _recordLock = new object();

        // Biến theo dõi thời gian để cắt file
        private DateTime _currentSegmentStartTime;

        public void StartRecord()
        {
            lock (_recordLock)
            {
                if (IsRecording) return;
                IsRecording = true;
            }

            StartFFmpegProcess();

            OnNewFrame += RecordingHandler;
        }

        public void StopRecord()
        {
            lock (_recordLock)
            {
                if (!IsRecording) return;
                IsRecording = false;
            }

            OnNewFrame -= RecordingHandler;

            Task.Run(() => StopFFmpegProcess());
        }

        private void StartFFmpegProcess()
        {
            try
            {
                if (!Directory.Exists(recordingFolder))
                {
                    Directory.CreateDirectory(recordingFolder);
                }
                string dateFolder = DateTime.Now.ToString("yyyyMMdd");
                string fullFolderPath = Path.Combine(recordingFolder, dateFolder);

                if (!Directory.Exists(fullFolderPath))
                {
                    Directory.CreateDirectory(fullFolderPath);
                }

                string fileName = $"{DateTime.Now:HHmmss}.mp4";
                string pathSave = Path.Combine(fullFolderPath, fileName);

                float recordFps = averageFps > 5 ? averageFps : 25.0f;

                string args = $"-y -f image2pipe -c:v mjpeg -r {recordFps} -i - -c:v libx264 -pix_fmt yuv420p -preset ultrafast -loglevel error \"{pathSave}\"";

                var processInfo = new ProcessStartInfo
                {
                    FileName = "ffmpeg",
                    Arguments = args,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardInput = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = false
                };

                _recordProcess = new Process { StartInfo = processInfo };

                _recordProcess.ErrorDataReceived += (s, e) => { /* Debug.WriteLine(e.Data); */ };

                _recordProcess.Start();
                _recordProcess.BeginErrorReadLine();

                _recordStream = _recordProcess.StandardInput.BaseStream;

                _currentSegmentStartTime = DateTime.Now;

                Debug.WriteLine($"[REC] Started segment: {pathSave}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error starting FFmpeg: {ex.Message}");
                IsRecording = false; // Dừng toàn bộ nếu lỗi khởi tạo
            }
        }

        private void StopFFmpegProcess()
        {
            try
            {
                if (_recordStream != null)
                {
                    _recordStream.Flush();
                    _recordStream.Close();
                    _recordStream.Dispose();
                    _recordStream = null;
                }

                if (_recordProcess != null)
                {
                    if (!_recordProcess.WaitForExit(15000)) // Đợi tối đa 15s
                    {
                        _recordProcess.Kill();
                    }
                    _recordProcess.Dispose();
                    _recordProcess = null;
                }
                Debug.WriteLine("[REC] Segment saved.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error stopping FFmpeg: {ex.Message}");
            }
        }

        private void RecordingHandler(object sender, FrameEventArgs e)
        {
            if (!IsRecording) return;

            lock (_recordLock)
            {
                if (_recordStream != null && (DateTime.Now - _currentSegmentStartTime).TotalMinutes >= recordingVideoLength)
                {
                    Debug.WriteLine("[REC] Time limit reached. Rotating file...");

                    StopFFmpegProcess();

                    StartFFmpegProcess();
                }

                if (_recordStream != null && _recordStream.CanWrite)
                {
                    try
                    {
                        if (e.FrameStream is byte[] bytes)
                        {
                            _recordStream.Write(bytes, 0, bytes.Length);
                        }
                    }
                    catch
                    {
                        // Bỏ qua lỗi ghi lẻ tẻ
                    }
                }
            }
        }


        private BoundedConcurrentQueue<MemoryStream> pre_imgs = new(110);
        private event NewFrameEventforSaveHandler? OnNewFrame;
        private float averageFps = 0;
        private DateTime lastFrameTime = DateTime.MinValue;
        private Queue<float> last5Deltas = new Queue<float>();
        private int maxDeltas = 25;

        /// <summary>
        /// KienNN 16/12/2025
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task RunSaveImageforVideo(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    Bitmap bitmap = (Bitmap)this.GetCurrentImage()?.Clone();
                    if (bitmap != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        bitmap.Save(ms, ImageFormat.Jpeg);
                        pre_imgs.Enqueue(ms);

                        OnNewFrame?.Invoke(this, new FrameEventArgs(ms.ToArray()));

                        bitmap.Dispose();
                    }

                    // Tính FPS thực tế
                    DateTime now = DateTime.Now;

                    if (lastFrameTime != DateTime.MinValue)
                    {
                        float deltaSeconds = (float)(now - lastFrameTime).TotalSeconds;

                        // Lưu delta vào queue
                        last5Deltas.Enqueue(deltaSeconds);
                        if (last5Deltas.Count > maxDeltas)
                        {
                            last5Deltas.Dequeue();
                        }

                        // Tính trung bình
                        float avgDelta = last5Deltas.Average();
                        if (avgDelta > 0)
                        {
                            averageFps = (float)(1.0 / avgDelta);
                        }
                    }

                    lastFrameTime = now;
                }
                catch { /* ignore lỗi lẻ */ }

                await Task.Delay(40, token); // tránh vòng lặp 100% CPU
            }
        }

        /// <summary>
        /// KienNN 16/12/2025
        /// 
        /// </summary>
        /// <param name="pathSave"></param>
        public async void TakeSnapshot(string? pathSave)
        {
            // 1. Chuẩn bị đường dẫn
            if (string.IsNullOrEmpty(pathSave) || !pathSave.EndsWith(".mp4"))
            {
                pathSave = Path.Combine("D:", DateTime.Now.ToString("yyyyMMdd_HHmmss_ffff") + ".mp4");
            }

            if (pre_imgs.Count == 0) return;

            // 2. Clone ngay dữ liệu Pre-buffer tại thời điểm gọi
            var pre_snapshot = pre_imgs.ToArray();

            // 3. Chuẩn bị container cho Post-buffer
            var post_snapshot = new List<MemoryStream>();

            // Lấy FPS hiện tại để dùng cho video này (tránh việc fps biến động trong lúc chờ)

            // --- KEY CHANGE: Dùng TaskCompletionSource để đợi tín hiệu thay vì while loop ---
            var tcs = new TaskCompletionSource<bool>();

            // 4. Tạo Handler cục bộ
            NewFrameEventforSaveHandler handler = null;
            handler = (sender, e) =>
            {
                // Kiểm tra NGAY TRONG sự kiện. Nếu đủ rồi thì thôi, không add nữa.
                if (post_snapshot.Count < 110)
                {
                    post_snapshot.Add(new MemoryStream(e.FrameStream));
                }

                // Nếu vừa đủ số lượng thì báo hiệu "XONG" ngay lập tức
                if (post_snapshot.Count >= 110)
                {
                    // Báo cho Task bên dưới biết là đã đủ hàng
                    tcs.TrySetResult(true);
                }

            };

            // 5. Đăng ký sự kiện
            OnNewFrame += handler;

            // 6. Đợi cho đến khi đủ frame (hoặc timeout 15s để tránh treo nếu cam mất kết nối)
            // Task này không tốn CPU, nó chỉ nằm chờ tín hiệu từ handler
            await Task.WhenAny(tcs.Task, Task.Delay(15000));

            // 7. Gỡ sự kiện ngay lập tức
            OnNewFrame -= handler;

            float currentFps = averageFps > 0 ? averageFps : 15;

            // 8. Tách việc GHI FILE ra một luồng riêng biệt (Fire and Forget)
            // Việc này giúp hàm TakeSnapshot kết thúc nhanh, sẵn sàng cho lần gọi tiếp theo
            // mà không bị việc ghi đĩa làm chậm.
            _ = Task.Run(() =>
            {
                //SaveVideoLogic(pathSave, pre_snapshot, post_snapshot.ToArray(), currentFps);
                SaveVideoWithFFmpeg(pathSave, pre_snapshot, post_snapshot.ToArray(), currentFps);
            });
        }

        /// <summary>
        /// KienNN 16/12/2025
        /// 
        /// </summary>
        /// <param name="pathSave"></param>
        /// <param name="pre"></param>
        /// <param name="post"></param>
        /// <param name="fps"></param>
        private void SaveVideoWithFFmpeg(string pathSave, MemoryStream[] pre, MemoryStream[] post, float fps)
        {
            // Thêm -loglevel warning để ít spam log hơn
            string args = $"-y -f image2pipe -c:v mjpeg -r {fps} -i - -c:v libx264 -pix_fmt yuv420p -preset ultrafast -loglevel warning \"{pathSave}\"";

            var processInfo = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = args,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardInput = true,
                RedirectStandardError = true, // BẮT BUỘC PHẢI CÓ
                RedirectStandardOutput = false
            };

            using (var process = new Process { StartInfo = processInfo })
            {
                // --- BƯỚC 1: Xử lý Log để tránh tắc đường (Quan trọng nhất) ---
                process.ErrorDataReceived += (sender, e) =>
                {
                    // Có thể bỏ qua hoặc log ra Debug nếu cần kiểm tra
                    // if (e.Data != null) Debug.WriteLine("FFmpeg: " + e.Data); 
                };

                process.Start();

                // Bắt đầu hứng log lỗi ngay lập tức, chạy song song với việc ghi
                process.BeginErrorReadLine();

                // --- BƯỚC 2: Ghi dữ liệu ---
                // Sử dụng khối using để đảm bảo stream được Close() tự động ngay khi xong
                using (var stdin = process.StandardInput.BaseStream)
                {
                    try
                    {
                        // Ghi Pre
                        foreach (var item in pre)
                        {
                            item.WriteTo(stdin);
                        }

                        // Ghi Post
                        foreach (var item in post)
                        {
                            item.WriteTo(stdin);
                            item.Dispose();
                        }

                        // Mẹo nhỏ: Flush để đẩy nốt những byte cuối cùng đi
                        stdin.Flush();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Lỗi ghi Pipe: {ex.Message}");
                    }
                }
                // Khi code chạy đến dòng này, khối 'using' kết thúc -> stdin.Close() được gọi.
                // -> FFmpeg nhận được tín hiệu EOF (End Of File) -> Nó bắt đầu kết thúc video.

                // --- BƯỚC 3: Đợi FFmpeg dọn dẹp và tắt hẳn ---
                // Đợi tối đa 5 giây. Nếu quá 5s mà nó chưa xong thì cưỡng chế tắt.
                if (!process.WaitForExit(5000))
                {
                    Debug.WriteLine("FFmpeg bị kẹt -> Kill process");
                    process.Kill();
                }
            }
        }

        #endregion End RECORD

        public void Flash(int seconds)
        {
            return;
        }
        public bool SetDeviceDateTime(DateTime dateTime)
        {
            return true;
        }
        public bool GetDeviceDateTime(ref DateTime dateTime)
        {
            return true;
        }
    }

    public delegate void NewFrameEventforSaveHandler(object sender, FrameEventArgs e);

    public class FrameEventArgs
    {
        public byte[] FrameStream { get; }
        public FrameEventArgs(byte[] frameStream)
        {
            FrameStream = frameStream;
        }
    }
}

