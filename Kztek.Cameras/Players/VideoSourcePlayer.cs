//using AForge.Imaging;
//using AForge.Vision.Motion;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Threading;
//using System.Timers;
//using System.Windows.Forms;
//namespace Kztek.Cameras
//{
//    public class VideoSourcePlayer : Control, iCameraSourcePlayer
//    {
//        public List<Rectangle> lprDetectRegions { get; set; } = new List<Rectangle>();
//        public List<Rectangle> motionDetectRegions { get; set; } = new List<Rectangle>();
//        public List<Rectangle> faceDetectRegions { get; set; } = new List<Rectangle>();

//        private const int statLength = 15;
//        private System.Drawing.Bitmap currentFrame;
//        private string lastMessage;
//        public MotionDetector detector;
//        private System.Timers.Timer alarmTimer = new System.Timers.Timer();
//        private int flash;
//        private System.Timers.Timer motionDetectionTimer = new System.Timers.Timer();
//        private BackgroundWorker motionDetectionWorker;
//        private BackgroundWorker recordingWorker;
//        private bool needSizeUpdate;
//        private bool firstFrameNotProcessed = true;
//        private volatile bool requestedToStop;
//        private DateTime lastTimeReceivedFrame = DateTime.Now;
//        private DateTime _lastRedraw = DateTime.MinValue;
//        private System.Timers.Timer timer = new System.Timers.Timer();
//        //private IVideoWriter writer;
//        private string currentPartition = "";
//        private int videoLengthIndex;
//        private System.Timers.Timer videoLengthTimer = new System.Timers.Timer();
//        private System.Timers.Timer addFrameTimer = new System.Timers.Timer();
//        private object sync = new object();
//        private object syncToWrite = new object();
//        private Control parent;
//        private CameraStreamInfo videoSource;
//        private System.Drawing.Color motionRectangleColor = System.Drawing.Color.Green;
//        private int motionRectangleLineWidth = 2;
//        private System.Drawing.Rectangle[] motionRectangles;
//        private bool suppressNoise = true;
//        private int motionDetectionInterval = 40;
//        private int motionAlarmLevel = 15;
//        private bool displayMotionRegion;
//        private bool flipX;
//        private bool flipY;
//        private bool rotare90;
//        private bool autosize;
//        private System.Drawing.Color borderColor = System.Drawing.Color.Black;
//        private string textOverlay = "";
//        private TextOverlayLocation textOverlayLocation = TextOverlayLocation.BottomLeft;
//        private System.Drawing.Color textOverlayColor = System.Drawing.Color.Blue;
//        private int textOverlaySize = 12;
//        private bool embededDateTime;
//        private bool showOverlayFrameRate;
//        private int displayRate = 6;
//        private bool fastRendering;
//        private int recordingRate = 5;
//        private string recordingResolution = "";
//        private string recordingCodec = "div4";
//        private string recordingFolder = "";
//        private string recordingCameraFolder = "";
//        private int recordingVideoLength = 30;
//        private int recordingMethod;
//        private float fps;
//        private float bps;
//        private int videoWidth = 320;
//        private int videoHeight = 240;
//        private bool isRecording;
//        private int flashTime = 2;
//        private int statIndex;
//        private int statReady;
//        private int[] statCount = new int[15];
//        private long[] statReceived = new long[15];
//        private Queue frameToRecords = new Queue();
//        private bool needQueueForRecording;
//        private DateTime currentRecStartTime = DateTime.Now;
//        private string currentVideoFile = "";
//        private int recWidth;
//        private int recHeight;
//        private bool isMotionDetecting;
//        private bool needQueueForMotionDetection;
//        private Queue frameToMotionDetections = new Queue();
//        private IContainer components;
//        public event NewFrameHandler NewFrame;
//        public event EventHandler Alarm;
//        public event MotionAnalyzingEventHandler MotionAnalyzing;
//        public event VideoWriterFinishEventHandler VideoWriterFinish;
//        public event PlayingFinishedEventHandler PlayingFinished;
//        public event VideoSourceErrorEventHandler VideoSourceError;
//        public event OnMouseDownHandler OnMouseDownEvent;
//        public event OnMouseUpHandler OnMouseUpEvent;
//        public event OnMouseWheelHandler OnMouseWheelEvent;
//        public event OnMouseMoveHandler OnMouseMoveEvent;
//        public event OnDoubleClickHandler OnDoubleClickEvent;
//        // event
//        public event EventHandler OnCameraMouseDown;
//        public event EventHandler OnCameraDoubleClick;
//        [Browsable(false)]
//        public IVideoSource VideoSource
//        {
//            get
//            {
//                return this.videoSource;
//            }
//            set
//            {
//                lock (this.sync)
//                {
//                    if (this.videoSource != null)
//                    {
//                        this.videoSource.NewFrame -= new NewFrameEventHandler(this.videoSource_NewFrame);
//                        this.videoSource.VideoSourceError -= new VideoSourceErrorEventHandler(this.videoSource_VideoSourceError);
//                        this.videoSource.PlayingFinished -= new PlayingFinishedEventHandler(this.videoSource_PlayingFinished);
//                    }
//                    if (this.currentFrame != null)
//                    {
//                        this.currentFrame.Dispose();
//                        this.currentFrame = null;
//                    }
//                    this.videoSource = value;
//                    if (this.videoSource != null)
//                    {
//                        this.videoSource.NewFrame += new NewFrameEventHandler(this.videoSource_NewFrame);
//                        this.videoSource.VideoSourceError += new VideoSourceErrorEventHandler(this.videoSource_VideoSourceError);
//                        this.videoSource.PlayingFinished += new PlayingFinishedEventHandler(this.videoSource_PlayingFinished);
//                    }
//                    this.lastMessage = null;
//                    this.needSizeUpdate = true;
//                    this.firstFrameNotProcessed = true;
//                    this.statIndex = (this.statReady = 0);
//                    base.Invalidate();
//                }
//            }
//        }

//        #region Motion
//        [DefaultValue(typeof(System.Drawing.Color), "Green")]
//        public System.Drawing.Color MotionRectangleColor
//        {
//            get
//            {
//                return this.motionRectangleColor;
//            }
//            set
//            {
//                this.motionRectangleColor = value;
//                base.Invalidate();
//            }
//        }
//        [DefaultValue(1)]
//        public int MotionRectangleLineWidth
//        {
//            get
//            {
//                return this.motionRectangleLineWidth;
//            }
//            set
//            {
//                this.motionRectangleLineWidth = value;
//                base.Invalidate();
//            }
//        }
//        public System.Drawing.Rectangle[] MotionRectangles
//        {
//            get
//            {
//                return this.motionRectangles;
//            }
//            set
//            {
//                this.motionRectangles = value;
//            }
//        }
//        public bool SuppressNoise
//        {
//            get
//            {
//                return this.suppressNoise;
//            }
//            set
//            {
//                this.suppressNoise = value;
//            }
//        }
//        public int MotionDetectionInterval
//        {
//            get
//            {
//                return this.motionDetectionInterval;
//            }
//            set
//            {
//                this.motionDetectionInterval = value;
//            }
//        }
//        public int MotionAlarmLevel
//        {
//            set
//            {
//                this.motionAlarmLevel = value;
//            }
//        }
//        public bool DisplayMotionRegion
//        {
//            get
//            {
//                return this.displayMotionRegion;
//            }
//            set
//            {
//                this.displayMotionRegion = value;
//            }
//        }
//        #endregion

//        public bool FlipX
//        {
//            get
//            {
//                return this.flipX;
//            }
//            set
//            {
//                this.flipX = value;
//            }
//        }
//        public bool FlipY
//        {
//            get
//            {
//                return this.flipY;
//            }
//            set
//            {
//                this.flipY = value;
//            }
//        }
//        public bool Rotare90
//        {
//            get
//            {
//                return this.rotare90;
//            }
//            set
//            {
//                this.rotare90 = value;
//            }
//        }

//        [DefaultValue(false)]
//        public bool AutoSizeControl
//        {
//            get
//            {
//                return this.autosize;
//            }
//            set
//            {
//                this.autosize = value;
//                this.UpdatePosition();
//            }
//        }
//        [DefaultValue(typeof(System.Drawing.Color), "Black")]
//        public System.Drawing.Color BorderColor
//        {
//            get
//            {
//                return this.borderColor;
//            }
//            set
//            {
//                this.borderColor = value;
//                base.Invalidate();
//            }
//        }
//        public string TextOverlay
//        {
//            get
//            {
//                return this.textOverlay;
//            }
//            set
//            {
//                this.textOverlay = value;
//            }
//        }
//        public TextOverlayLocation TextOverlayLocation
//        {
//            get
//            {
//                return this.textOverlayLocation;
//            }
//            set
//            {
//                this.textOverlayLocation = value;
//            }
//        }
//        public System.Drawing.Color TextOverlayColor
//        {
//            get
//            {
//                return this.textOverlayColor;
//            }
//            set
//            {
//                this.textOverlayColor = value;
//            }
//        }
//        public int TextOverlaySize
//        {
//            get
//            {
//                return this.textOverlaySize;
//            }
//            set
//            {
//                this.textOverlaySize = value;
//            }
//        }
//        public bool EmbededDateTime
//        {
//            get
//            {
//                return this.embededDateTime;
//            }
//            set
//            {
//                this.embededDateTime = value;
//            }
//        }
//        public bool ShowOverlayFrameRate
//        {
//            get
//            {
//                return this.showOverlayFrameRate;
//            }
//            set
//            {
//                this.showOverlayFrameRate = value;
//            }
//        }
//        public int DisplayRate
//        {
//            set
//            {
//                this.displayRate = value;
//            }
//        }
//        public bool FastRendering
//        {
//            set
//            {
//                this.fastRendering = value;
//            }
//        }
//        public int RecordingRate
//        {
//            get
//            {
//                return this.recordingRate;
//            }
//            set
//            {
//                this.recordingRate = value;
//            }
//        }
//        public string RecordingResolution
//        {
//            get
//            {
//                return this.recordingResolution;
//            }
//            set
//            {
//                this.recordingResolution = value;
//            }
//        }
//        public string RecordingCodec
//        {
//            get
//            {
//                return this.recordingCodec;
//            }
//            set
//            {
//                this.recordingCodec = value;
//            }
//        }
//        public string RecordingFolder
//        {
//            get
//            {
//                return this.recordingFolder;
//            }
//            set
//            {
//                this.recordingFolder = value;
//            }
//        }
//        public string RecordingCameraFolder
//        {
//            get
//            {
//                return this.recordingCameraFolder;
//            }
//            set
//            {
//                this.recordingCameraFolder = value;
//            }
//        }
//        public int RecordingVideoLength
//        {
//            get
//            {
//                return this.recordingVideoLength;
//            }
//            set
//            {
//                this.recordingVideoLength = value;
//            }
//        }
//        public int RecordingMethod
//        {
//            get
//            {
//                return this.recordingMethod;
//            }
//            set
//            {
//                this.recordingMethod = value;
//            }
//        }
//        public float FPS
//        {
//            get
//            {
//                return this.fps;
//            }
//        }
//        public float BPS
//        {
//            get
//            {
//                return this.bps;
//            }
//        }
//        public int VideoWidth
//        {
//            get
//            {
//                return this.videoWidth;
//            }
//        }
//        public int VideoHeight
//        {
//            get
//            {
//                return this.videoHeight;
//            }
//        }
//        [Browsable(false)]
//        public bool IsRunning
//        {
//            get
//            {
//                bool flag = false;
//                bool result;
//                try
//                {
//                    Monitor.Enter(this, ref flag);
//                    result = (this.videoSource != null && this.videoSource.IsRunning);
//                }
//                finally
//                {
//                    if (flag)
//                    {
//                        Monitor.Exit(this);
//                    }
//                }
//                return result;
//            }
//        }
//        public bool IsRecording
//        {
//            get
//            {
//                return this.isRecording;
//            }
//        }
//        public DateTime LastTimeReceivedFrame
//        {
//            get
//            {
//                return this.lastTimeReceivedFrame;
//            }
//            set
//            {
//                this.lastTimeReceivedFrame = value;
//            }
//        }
//        public VideoSourcePlayer()
//        {
//            this.InitializeComponent();
//            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
//            this.timer.Interval = 1000.0;
//            this.timer.Elapsed += new ElapsedEventHandler(this.timer_Elapsed);
//        }
//        public void Start()
//        {
//            this.requestedToStop = false;
//            lock (this.sync)
//            {
//                if (this.videoSource != null)
//                {
//                    this.firstFrameNotProcessed = true;
//                    this.statIndex = (this.statReady = 0);
//                    this.videoSource.Start();
//                    this.timer.Start();
//                    base.Invalidate();
//                }
//            }
//        }
        
//        public void Stop()
//        {
//            this.requestedToStop = true;
//            lock (this.sync)
//            {
//                if (this.videoSource != null)
//                {
//                    this.timer.Stop();
//                    this.videoSource.Stop();
//                    if (this.currentFrame != null)
//                    {
//                        this.currentFrame.Dispose();
//                        this.currentFrame = null;
//                    }
//                    base.Invalidate();
//                }
//            }
//        }
//        public void SignalToStop()
//        {
//            this.requestedToStop = true;
//            lock (this.sync)
//            {
//                if (this.videoSource != null)
//                {
//                    this.videoSource.SignalToStop();
//                }
//            }
//        }
//        public void StartRecord()
//        {
//        }
//        public void StopRecord()
//        {
//        }
//        public void StartMotionDetection()
//        {
//            this.InitializeMotionDetectionWorker();
//            this.detector = new MotionDetector(new TwoFramesDifferenceDetector(this.suppressNoise), null);
//            this.detector.MotionZones = this.motionRectangles;
//            this.alarmTimer.AutoReset = true;
//            this.alarmTimer.SynchronizingObject = this;
//            this.alarmTimer.Elapsed += new ElapsedEventHandler(this.alarmTimer_Tick);
//            GC.KeepAlive(this.alarmTimer);
//            this.alarmTimer.Start();
//            if (this.motionDetectionInterval > 0)
//            {
//                this.motionDetectionTimer.Interval = (double)this.motionDetectionInterval;
//                this.motionDetectionTimer.AutoReset = true;
//                this.motionDetectionTimer.SynchronizingObject = this;
//                this.motionDetectionTimer.Elapsed += new ElapsedEventHandler(this.motionDetectionTimer_Tick);
//                GC.KeepAlive(this.motionDetectionTimer);
//                this.motionDetectionTimer.Start();
//                this.isMotionDetecting = true;
//            }
//        }
//        public void StopMotionDetection()
//        {
//            this.isMotionDetecting = false;
//            this.motionDetectionTimer.Elapsed -= new ElapsedEventHandler(this.motionDetectionTimer_Tick);
//            this.motionDetectionTimer.Stop();
//            this.CloseMotionDetectionWorker();
//            this.alarmTimer.Elapsed -= new ElapsedEventHandler(this.alarmTimer_Tick);
//            this.alarmTimer.Stop();
//            if (this.detector != null)
//            {
//                this.detector.Reset();
//                this.detector = null;
//            }
//        }
//        public void SaveCurrentVideoFrame(string path)
//        {
//            lock (this.sync)
//            {
//                if (this.currentFrame != null)
//                {
//                    using (System.Drawing.Bitmap bmp = (System.Drawing.Bitmap)this.currentFrame.Clone())
//                    {
//                        bmp.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
//                    }
//                }
//            }
//        }
//        public System.Drawing.Bitmap GetCurrentVideoFrame()
//        {
//            System.Drawing.Bitmap result;
//            lock (this.sync)
//            {
//                result = ((this.currentFrame == null) ? null : new System.Drawing.Bitmap(this.currentFrame));
//            }
//            return result;
//        }
//        public System.Drawing.Bitmap GetCurrentVideoFrame2()
//        {
//            System.Drawing.Bitmap result;
//            lock (this.sync)
//            {
//                result = ((this.currentFrame == null) ? null : AForge.Imaging.Image.Clone(this.currentFrame));
//            }
//            return result;
//        }
//        public UnmanagedImage GetCurrentUnmanagedVideoFrame()
//        {
//            UnmanagedImage result;
//            lock (this.sync)
//            {
//                result = ((this.currentFrame == null) ? null : UnmanagedImage.FromManagedImage((System.Drawing.Bitmap)this.currentFrame.Clone()));
//            }
//            return result;
//        }
//        public void Flash(int seconds)
//        {
//            this.flashTime = seconds;
//        }
//        public bool SetDeviceDateTime(DateTime dateTime)
//        {
//            return false;
//        }
//        public bool GetDeviceDateTime(ref DateTime dateTime)
//        {
//            return false;
//        }
//        public void WaitForStop()
//        {
//            this.requestedToStop = true;
//            lock (this.sync)
//            {
//                if (this.videoSource != null)
//                {
//                    this.videoSource.WaitForStop();
//                    if (this.currentFrame != null)
//                    {
//                        this.currentFrame.Dispose();
//                        this.currentFrame = null;
//                    }
//                    base.Invalidate();
//                }
//            }
//        }
//        private void timer_Elapsed(object sender, EventArgs e)
//        {
//            if (this.videoSource != null)
//            {
//                this.statCount[this.statIndex] = this.videoSource.FramesReceived;
//                this.statReceived[this.statIndex] = this.videoSource.BytesReceived;
//                this.videoSource.FramesReceived = 0;
//                this.videoSource.BytesReceived = 0L;
//                if (++this.statIndex >= 15)
//                {
//                    this.statIndex = 0;
//                }
//                if (this.statReady < 15)
//                {
//                    this.statReady++;
//                }
//                this.fps = 0f;
//                this.bps = 0f;
//                for (int i = 0; i < this.statReady; i++)
//                {
//                    this.fps += (float)this.statCount[i];
//                    this.bps += (float)this.statReceived[i];
//                }
//                this.fps /= (float)this.statReady;
//                this.bps /= (float)(this.statReady * 1024);
//                if (this.fps > 30f)
//                {
//                    this.fps = 30f;
//                }
//                this.statCount[this.statIndex] = 0;
//                this.statCount[this.statIndex] = 0;
//                if (this.flashTime != 0)
//                {
//                    this.borderColor = ((this.flashTime % 2 == 1) ? System.Drawing.Color.Black : System.Drawing.Color.Red);
//                    this.flashTime--;
//                }
//            }
//        }
//        private void addFrameTimer_Tick(object sender, EventArgs e)
//        {
//            lock (this.syncToWrite)
//            {
//                try
//                {
//                    if (this.recordingFolder == "" || this.recordingFolder == null)
//                    {
//                        this.CloseVideoWriter();
//                    }
//                    else
//                    {
//                        this.needQueueForRecording = true;
//                        if (!this.recordingWorker.IsBusy)
//                        {
//                            this.recordingWorker.RunWorkerAsync();
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    if (this.VideoSourceError != null)
//                    {
//                        this.VideoSourceError(this, new VideoSourceErrorEventArgs("Error while recording camera.\nDetail: " + ex.ToString()));
//                    }
//                }
//                finally
//                {
//                    GC.Collect();
//                }
//            }
//        }
//        private void CloseVideoWriter()
//        {
//            lock (this.syncToWrite)
//            {
//                try
//                {
//                    this.videoLengthIndex = 0;
//                    if (this.writer != null)
//                    {
//                        this.writer.Close();
//                        this.writer.Dispose();
//                        this.writer = null;
//                        if (this.VideoWriterFinish != null && this.currentVideoFile != "")
//                        {
//                            this.VideoWriterFinish(this, new VideoWriterFinishEventArgs(this.currentRecStartTime, DateTime.Now, this.currentVideoFile));
//                            this.currentVideoFile = "";
//                        }
//                    }
//                }
//                catch
//                {
//                }
//                finally
//                {
//                    GC.Collect();
//                }
//            }
//        }
//        private void videoLengthTimer_Tick(object sender, EventArgs e)
//        {
//            this.videoLengthIndex++;
//            if (this.videoLengthIndex >= this.recordingVideoLength)
//            {
//                this.CloseVideoWriter();
//            }
//        }

//        private System.Drawing.Bitmap ResizeBitmap(System.Drawing.Bitmap frame, int recWidth, int recHeight)
//        {
//            try
//            {
//                System.Drawing.Bitmap result;
//                if (frame == null)
//                {
//                    result = frame;
//                    return result;
//                }
//                if (frame.Width == recWidth && frame.Height == recHeight)
//                {
//                    result = frame;
//                    return result;
//                }
//                System.Drawing.Bitmap r = new System.Drawing.Bitmap(recWidth, recHeight);
//                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(r))
//                {
//                    g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
//                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
//                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
//                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
//                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
//                    g.DrawImage(frame, 0, 0, recWidth, recHeight);
//                }
//                frame.Dispose();
//                frame = null;
//                result = r;
//                return result;
//            }
//            catch (Exception ex)
//            {
//                if (this.VideoSourceError != null)
//                {
//                    this.VideoSourceError(this, new VideoSourceErrorEventArgs("Error while resize image: " + ex.ToString()));
//                }
//            }
//            finally
//            {
//                GC.Collect();
//            }
//            return null;
//        }

//        private void motionDetectionTimer_Tick(object sender, EventArgs e)
//        {
//            if (this.statReady == 15 && this.fps > 0f && this.motionDetectionInterval < (int)Math.Floor((double)(1000f / this.fps)))
//            {
//                this.motionDetectionInterval = (int)Math.Ceiling((double)(1000f / this.fps));
//                this.motionDetectionTimer.Interval = (double)this.motionDetectionInterval;
//            }
//            if (this.detector != null && this.motionDetectionWorker != null)
//            {
//                this.needQueueForMotionDetection = true;
//                if (!this.motionDetectionWorker.IsBusy)
//                {
//                    this.motionDetectionWorker.RunWorkerAsync();
//                }
//            }
//        }
//        private void InitializeMotionDetectionWorker()
//        {
//            this.motionDetectionWorker = new BackgroundWorker();
//            this.motionDetectionWorker.WorkerSupportsCancellation = true;
//            this.motionDetectionWorker.WorkerReportsProgress = true;
//            this.motionDetectionWorker.DoWork += new DoWorkEventHandler(this.motionDetectionWorker_DoWork);
//            this.motionDetectionWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.motionDetectionWorker_RunWorkerCompleted);
//            this.motionDetectionWorker.ProgressChanged += new ProgressChangedEventHandler(this.motionDetectionWorker_ProgressChanged);
//        }
//        private void motionDetectionWorker_DoWork(object sender, DoWorkEventArgs e)
//        {
//            try
//            {
//                if (this.detector != null)
//                {
//                    BackgroundWorker worker = sender as BackgroundWorker;
//                    if (!worker.CancellationPending)
//                    {
//                        while (this.frameToMotionDetections.Count > 0)
//                        {
//                            using (System.Drawing.Bitmap bmp = (System.Drawing.Bitmap)this.frameToMotionDetections.Dequeue())
//                            {
//                                float motionLevel = this.detector.ProcessFrame(bmp);
//                                float alarmLevel = (float)this.motionAlarmLevel / 1000f;
//                                if (this.MotionAnalyzing != null)
//                                {
//                                    this.MotionAnalyzing(this, motionLevel);
//                                }
//                                if (motionLevel > alarmLevel)
//                                {
//                                    this.flash = (int)(2.0 * (1000.0 / this.alarmTimer.Interval));
//                                    if (this.Alarm != null)
//                                    {
//                                        this.Alarm(this, new EventArgs());
//                                    }
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                if (this.VideoSourceError != null)
//                {
//                    this.VideoSourceError(this, new VideoSourceErrorEventArgs("Error while motion analysis: " + ex.ToString()));
//                }
//            }
//        }
//        private void motionDetectionWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
//        {
//        }
//        private void motionDetectionWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
//        {
//        }
//        private void CloseMotionDetectionWorker()
//        {
//            if (this.motionDetectionWorker != null)
//            {
//                this.motionDetectionWorker.DoWork -= new DoWorkEventHandler(this.motionDetectionWorker_DoWork);
//                this.motionDetectionWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(this.motionDetectionWorker_RunWorkerCompleted);
//                this.motionDetectionWorker.ProgressChanged -= new ProgressChangedEventHandler(this.motionDetectionWorker_ProgressChanged);
//                this.motionDetectionWorker.CancelAsync();
//                this.motionDetectionWorker = null;
//            }
//        }
//        private void alarmTimer_Tick(object sender, EventArgs e)
//        {
//            if (this.flash != 0)
//            {
//                this.borderColor = ((this.flash % 2 == 1) ? System.Drawing.Color.Black : System.Drawing.Color.Red);
//                this.motionRectangleColor = ((this.flash % 2 == 1) ? System.Drawing.Color.Green : System.Drawing.Color.Red);
//                this.flash--;
//            }
//        }
//        private void VideoSourcePlayer_Paint(object sender, PaintEventArgs e)
//        {
//            try
//            {
//                if (this.needSizeUpdate || this.firstFrameNotProcessed)
//                {
//                    this.UpdatePosition();
//                    this.needSizeUpdate = false;
//                }
//                lock (this.sync)
//                {
//                    System.Drawing.Graphics g = e.Graphics;
//                    System.Drawing.Rectangle rect = base.ClientRectangle;
//                    System.Drawing.Pen borderPen = new System.Drawing.Pen(this.borderColor, 1f);
//                    if (this.fastRendering)
//                    {
//                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
//                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
//                        g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
//                        g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
//                        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
//                    }
//                    g.DrawRectangle(borderPen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
//                    if (this.videoSource != null && this.displayRate != -1)
//                    {
//                        if (this.currentFrame != null && this.lastMessage == null)
//                        {
//                            g.DrawImage(this.currentFrame, rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2);
//                            if (this.displayMotionRegion && this.motionRectangles != null && this.motionRectangles.Length > 0)
//                            {
//                                System.Drawing.Rectangle[] modifyRects = new System.Drawing.Rectangle[this.motionRectangles.Length];
//                                for (int i = 0; i < this.motionRectangles.Length; i++)
//                                {
//                                    modifyRects[i] = new System.Drawing.Rectangle
//                                    {
//                                        X = this.motionRectangles[i].X * (rect.Width - 2) / this.currentFrame.Width + rect.X + 1,
//                                        Y = this.motionRectangles[i].Y * (rect.Height - 2) / this.currentFrame.Height + rect.Y + 1,
//                                        Width = this.motionRectangles[i].Width * (rect.Width - 2) / this.currentFrame.Width + rect.X + 1,
//                                        Height = this.motionRectangles[i].Height * (rect.Height - 2) / this.currentFrame.Height + rect.Y + 1
//                                    };
//                                }
//                                System.Drawing.Pen pen = new System.Drawing.Pen(this.motionRectangleColor, (float)this.motionRectangleLineWidth);
//                                g.DrawRectangles(pen, modifyRects);
//                                pen.Dispose();
//                            }
//                            this.firstFrameNotProcessed = false;
//                            string tmp = this.textOverlay;
//                            if (this.showOverlayFrameRate)
//                            {
//                                tmp = this.textOverlay + " " + this.fps.ToString("F2") + " FPS";
//                            }
//                            if (tmp != "")
//                            {
//                                System.Drawing.SolidBrush textOverlayBrush = new System.Drawing.SolidBrush(this.textOverlayColor);
//                                System.Drawing.Font textOverlayFont = new System.Drawing.Font("Microsoft Sans Serif", (float)this.textOverlaySize, System.Drawing.FontStyle.Bold);
//                                System.Drawing.PointF overlayLocation = new System.Drawing.PointF(5f, 5f);
//                                if (this.textOverlayLocation == TextOverlayLocation.TopLeft)
//                                {
//                                    overlayLocation = new System.Drawing.PointF(5f, 5f);
//                                }
//                                else
//                                {
//                                    if (this.textOverlayLocation == TextOverlayLocation.TopRight)
//                                    {
//                                        overlayLocation = new System.Drawing.PointF((float)base.Width - g.MeasureString(tmp, textOverlayFont).Width - 5f, 5f);
//                                    }
//                                    else
//                                    {
//                                        if (this.textOverlayLocation == TextOverlayLocation.BottomLeft)
//                                        {
//                                            overlayLocation = new System.Drawing.PointF(5f, (float)(base.Height - textOverlayFont.Height - 5));
//                                        }
//                                        else
//                                        {
//                                            if (this.textOverlayLocation == TextOverlayLocation.BottomRight)
//                                            {
//                                                overlayLocation = new System.Drawing.PointF((float)base.Width - g.MeasureString(tmp, textOverlayFont).Width - 5f, (float)base.Height - g.MeasureString(tmp, textOverlayFont).Height - 5f);
//                                            }
//                                        }
//                                    }
//                                }
//                                g.DrawString(tmp, textOverlayFont, textOverlayBrush, overlayLocation);
//                                textOverlayBrush.Dispose();
//                            }
//                            if (this.isRecording)
//                            {
//                                System.Drawing.SolidBrush recBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
//                                g.FillEllipse(recBrush, new System.Drawing.Rectangle(base.Width - 20, 4, 16, 16));
//                                recBrush.Dispose();
//                            }
//                        }
//                        else
//                        {
//                            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(this.ForeColor);
//                            g.DrawString((this.lastMessage == null) ? "Connecting ..." : this.lastMessage, this.Font, drawBrush, new System.Drawing.PointF(5f, 5f));
//                            drawBrush.Dispose();
//                        }
//                    }
//                    borderPen.Dispose();
//                }
//            }
//            catch (Exception ex)
//            {
//                if (this.VideoSourceError != null)
//                {
//                    this.VideoSourceError(this, new VideoSourceErrorEventArgs("VideoSourcePlayer_Paint: " + ex.Message));
//                }
//            }
//        }
//        private void UpdatePosition()
//        {
//            lock (this.sync)
//            {
//                if (this.autosize && this.Dock != DockStyle.Fill && base.Parent != null)
//                {
//                    System.Drawing.Rectangle rc = base.Parent.ClientRectangle;
//                    int width = 320;
//                    int height = 240;
//                    if (this.currentFrame != null)
//                    {
//                        width = this.currentFrame.Width;
//                        height = this.currentFrame.Height;
//                    }
//                    base.SuspendLayout();
//                    base.Location = new System.Drawing.Point((rc.Width - width - 2) / 2, (rc.Height - height - 2) / 2);
//                    base.Size = new System.Drawing.Size(width + 2, height + 2);
//                    base.ResumeLayout();
//                }
//            }
//        }
//        private void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
//        {
//            try
//            {
//                if (!this.requestedToStop)
//                {
//                    lock (this.sync)
//                    {
//                        if (this.currentFrame != null)
//                        {
//                            this.currentFrame.Dispose();
//                            this.currentFrame = null;
//                        }
//                        this.currentFrame = (System.Drawing.Bitmap)eventArgs.Frame.Clone();
//                        this.videoWidth = this.currentFrame.Width;
//                        this.videoHeight = this.currentFrame.Height;
//                        if (this.flipX)
//                        {
//                            this.currentFrame.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipX);
//                        }
//                        if (this.flipY)
//                        {
//                            this.currentFrame.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipX);
//                        }
//                        if (this.rotare90)
//                        {
//                            this.currentFrame.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);
//                        }
//                        if (this.embededDateTime)
//                        {
//                            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(this.currentFrame);
//                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
//                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
//                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
//                            g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
//                            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
//                            System.Drawing.Font drawFont = new System.Drawing.Font("Verdana", (float)this.textOverlaySize, System.Drawing.FontStyle.Regular);
//                            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(this.textOverlayColor);
//                            string dateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
//                            System.Drawing.SizeF dateTimeSize = g.MeasureString(dateTime, drawFont);
//                            float posX = 5f;
//                            float posY = (float)this.currentFrame.Height - dateTimeSize.Height - 5f;
//                            g.DrawString(dateTime, drawFont, drawBrush, new System.Drawing.PointF(posX, posY));
//                            g.Dispose();
//                            drawBrush.Dispose();
//                            drawFont.Dispose();
//                        }
//                        this.lastMessage = null;
//                        this.lastTimeReceivedFrame = DateTime.Now;
//                        if (this.isRecording && this.needQueueForRecording)
//                        {
//                            this.needQueueForRecording = false;
//                            this.frameToRecords.Enqueue(new System.Drawing.Bitmap(this.currentFrame));
//                        }
//                        if (this.isMotionDetecting && this.needQueueForMotionDetection)
//                        {
//                            this.needQueueForMotionDetection = false;
//                            this.frameToMotionDetections.Enqueue((System.Drawing.Bitmap)this.currentFrame.Clone());
//                        }
//                        if (this.NewFrame != null)
//                        {
//                            this.NewFrame(this, ref this.currentFrame);
//                        }
//                    }
//                    if (this.displayRate > 0)
//                    {
//                        if (this._lastRedraw < DateTime.Now.AddMilliseconds((double)(-(double)(1000 / this.displayRate))))
//                        {
//                            base.Invalidate();
//                            this._lastRedraw = DateTime.Now;
//                        }
//                    }
//                    else
//                    {
//                        base.Invalidate();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                if (this.VideoSourceError != null)
//                {
//                    this.VideoSourceError(this, new VideoSourceErrorEventArgs("videoSource_NewFrame: " + ex.Message));
//                }
//            }
//        }
//        private void videoSource_VideoSourceError(object sender, VideoSourceErrorEventArgs eventArgs)
//        {
//            this.CloseVideoWriter();
//            this.lastMessage = eventArgs.Description;
//            if (this.VideoSourceError != null)
//            {
//                this.VideoSourceError(this, eventArgs);
//            }
//            base.Invalidate();
//        }
//        private void videoSource_PlayingFinished(object sender, ReasonToFinishPlaying reason)
//        {
//            switch (reason)
//            {
//                case ReasonToFinishPlaying.EndOfStreamReached:
//                    this.lastMessage = "Video has finished";
//                    break;
//                case ReasonToFinishPlaying.StoppedByUser:
//                    this.lastMessage = "Video was stopped";
//                    break;
//                default:
//                    this.lastMessage = "Video has finished for unknown reason";
//                    break;
//            }
//            if (this.PlayingFinished != null)
//            {
//                this.PlayingFinished(this, reason);
//            }
//            base.Invalidate();
//        }
//        private void VideoSourcePlayer_ParentChanged(object sender, EventArgs e)
//        {
//            if (this.parent != null)
//            {
//                this.parent.SizeChanged -= new EventHandler(this.parent_SizeChanged);
//            }
//            this.parent = base.Parent;
//            if (this.parent != null)
//            {
//                this.parent.SizeChanged += new EventHandler(this.parent_SizeChanged);
//            }
//        }
//        private void parent_SizeChanged(object sender, EventArgs e)
//        {
//            this.UpdatePosition();
//        }
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && this.components != null)
//            {
//                this.components.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//        private void InitializeComponent()
//        {
//            base.SuspendLayout();
//            base.Paint += new PaintEventHandler(this.VideoSourcePlayer_Paint);
//            base.ParentChanged += new EventHandler(this.VideoSourcePlayer_ParentChanged);
//            base.ResumeLayout(false);
//        }

//        public Bitmap GetCurrentVideoFrame(int destWidth, int destHeight)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
