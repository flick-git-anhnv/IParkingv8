using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace Kztek.Cameras
{
    /// <summary>
    /// Lớp Camera mô tả cấu hình và điều khiển một nguồn video:
    /// - Kết nối / ngắt kết nối camera
    /// - Ghi hình, snapshot
    /// - Phát hiện chuyển động
    /// - Vùng LPR, vùng cảnh báo...
    /// </summary>
    public class Camera
    {
        /// <summary>
        /// Đối tượng player thực tế hiển thị/stream video (KzPlayer, AnvPlayer, HikPlayer, VideoSourcePlayer...).
        /// </summary>
        public iCameraSourcePlayer videoSourcePlayer;

        #region ==== THÔNG TIN CAMERA CƠ BẢN ====

        private string id;
        private string name = "Camera";
        private string code = "C";

        private int groupID;
        private string videoSource = "video source";

        private int httpPort;
        private int rtspPort = 554;
        private int serverPort;

        /// <summary>
        /// Kênh logic của camera / đầu ghi.
        /// Ví dụ với NVR: Channel 1, 2, 3...
        /// </summary>
        private int chanel;

        /// <summary>
        /// Chỉ số luồng (stream) trên cùng một channel.
        /// Ví dụ:
        /// 0 = main stream (full HD),
        /// 1 = sub stream (preview nhẹ),
        /// 2 = stream khác...
        /// </summary>
        private int streamIndex = 1;

        private string login = "";
        private string password = "";

        private CameraType cameraType;
        private StreamType streamType = StreamType.H264;
        private ProtocolType protocolType;

        private string resolution = "640x480";
        private string quality = "Standard";
        private int frameRate = 25;
        private string description = "";

        /// <summary>
        /// ID định danh camera trong hệ thống (GUID, mã DB...).
        /// </summary>
        public string ID
        {
            get => id;
            set => id = value;
        }

        /// <summary>
        /// Tên hiển thị của camera.
        /// </summary>
        public string Name
        {
            get => name;
            set => name = value;
        }

        /// <summary>
        /// Mã rút gọn của camera (code nội bộ).
        /// </summary>
        public string Code
        {
            get => code;
            set => code = value;
        }

        /// <summary>
        /// Nhóm camera (nếu dùng phân nhóm).
        /// </summary>
        public int GroupID
        {
            get => groupID;
            set => groupID = value;
        }

        /// <summary>
        /// Địa chỉ nguồn video:
        /// - Với IP: IP hoặc hostname (ví dụ 192.168.1.10).
        /// - Với capture device: tên device.
        /// </summary>
        public string VideoSource
        {
            get => videoSource;
            set => videoSource = value;
        }

        /// <summary>
        /// Cổng HTTP dùng cho một số camera (Hikvision, v.v...).
        /// </summary>
        public int HttpPort
        {
            get => httpPort;
            set => httpPort = value;
        }

        /// <summary>
        /// Cổng RTSP (mặc định 554).
        /// </summary>
        public int RtspPort
        {
            get => rtspPort;
            set => rtspPort = value;
        }

        /// <summary>
        /// Cổng server (nếu dùng SDK riêng: Hikvision, v.v...).
        /// </summary>
        public int ServerPort
        {
            get => serverPort;
            set => serverPort = value;
        }

        /// <summary>
        /// Channel logic của camera (camera thứ mấy trên đầu ghi / kênh thứ mấy).
        /// </summary>
        public int Chanel
        {
            get => chanel;
            set => chanel = value;
        }

        /// <summary>
        /// Chỉ định luồng stream của channel hiện tại (0 = main, 1 = sub, ... tuỳ từng hãng).
        /// </summary>
        public int StreamIndex
        {
            get => streamIndex;
            set => streamIndex = value;
        }

        /// <summary>
        /// Tài khoản login cho camera (IP/RTSP/HTTP).
        /// </summary>
        public string Login
        {
            get => login;
            set => login = value;
        }

        /// <summary>
        /// Mật khẩu login cho camera.
        /// </summary>
        public string Password
        {
            get => password;
            set => password = value;
        }

        /// <summary>
        /// Loại camera (HIKVISION, Dahua, VideoCaptureDevice, Custom...).
        /// </summary>
        public CameraType CameraType
        {
            get => cameraType;
            set => cameraType = value;
        }

        /// <summary>
        /// Kiểu stream (H264, MJPEG, H265...).
        /// </summary>
        public StreamType StreamType
        {
            get => streamType;
            set => streamType = value;
        }

        /// <summary>
        /// Kiểu giao thức (RTSP, HTTP...).
        /// </summary>
        public ProtocolType ProtocolType
        {
            get => protocolType;
            set => protocolType = value;
        }

        /// <summary>
        /// Độ phân giải mong muốn: "WxH" (ví dụ "1280x720").
        /// </summary>
        public string Resolution
        {
            get => resolution;
            set => resolution = value;
        }

        /// <summary>
        /// Chất lượng video (Standard, High, Low...) nếu cần.
        /// </summary>
        public string Quality
        {
            get => quality;
            set => quality = value;
        }

        /// <summary>
        /// Frame rate mong muốn (FPS).
        /// </summary>
        public int FrameRate
        {
            get => frameRate;
            set => frameRate = value;
        }

        /// <summary>
        /// Ghi chú mô tả camera.
        /// </summary>
        public string Description
        {
            get => description;
            set => description = value;
        }

        #endregion

        #region ==== PTZ ====

        /// <summary>
        /// Đối tượng điều khiển PTZ (nếu camera hỗ trợ).
        /// </summary>
        private PTZSource ptz;

        #endregion

        #region ==== RECORDING (GHI HÌNH) ====

        /// <summary>
        /// Trạng thái ghi hình hiện tại của videoSourcePlayer.
        /// </summary>
        public bool IsRecording
        {
            get
            {
                if (videoSourcePlayer != null)
                {
                    return videoSourcePlayer.IsRecording;
                }
                return false;
            }
        }

        private bool enableRecording;
        private int recordingRate = 5;
        private string recordingResolution = "";
        private string recordingCodec = "div4";
        private string recordingFolder = "";
        private string recordingCameraFolder = "";
        private int recordingVideoLength = 30;
        private int recordingMethod;

        /// <summary>
        /// Bật/tắt ghi hình cho camera này.
        /// </summary>
        public bool EnableRecording
        {
            get => enableRecording;
            set => enableRecording = value;
        }

        /// <summary>
        /// Frame rate khi ghi hình.
        /// </summary>
        public int RecordingRate
        {
            get => recordingRate;
            set => recordingRate = value;
        }

        /// <summary>
        /// Độ phân giải ghi hình (có thể khác với Resolution preview).
        /// </summary>
        public string RecordingResolution
        {
            get => recordingResolution;
            set => recordingResolution = value;
        }

        /// <summary>
        /// Codec dùng để ghi hình (ví dụ div4).
        /// </summary>
        public string RecordingCodec
        {
            get => recordingCodec;
            set => recordingCodec = value;
        }

        /// <summary>
        /// Thư mục gốc lưu file ghi hình.
        /// </summary>
        public string RecordingFolder
        {
            get => recordingFolder;
            set => recordingFolder = value;
        }

        /// <summary>
        /// Thư mục riêng của camera trong RecordingFolder.
        /// </summary>
        public string RecordingCameraFolder
        {
            get => recordingCameraFolder;
            set => recordingCameraFolder = value;
        }

        /// <summary>
        /// Độ dài mỗi file video (giây).
        /// </summary>
        public int RecordingVideoLength
        {
            get => recordingVideoLength;
            set => recordingVideoLength = value;
        }

        /// <summary>
        /// Phương thức ghi hình (theo thời gian, theo motion, manual...).
        /// </summary>
        public int RecordingMethod
        {
            get => recordingMethod;
            set => recordingMethod = value;
        }

        #endregion

        #region ==== LPR – NHẬN DIỆN BIỂN SỐ ====

        /// <summary>
        /// Danh sách vùng detect LPR đã parse sẵn.
        /// </summary>
        public List<Rectangle> lprDetectRegions = new List<Rectangle>();

        private string lprRegions = "";

        private int minPlateWidth;
        private int minPlateHeight;

        private int maxPlateWidth;
        private int maxPlateHeight;

        private float minConfidenceDay;
        private float minConfidenceNight;

        private bool enableCheckPlateRegion;
        private bool enableCheckPlateSize;
        private bool enableCheckPlateConfidence;

        /// <summary>
        /// Chuỗi mô tả vùng LPR dạng "x,y,w,h;x,y,w,h;...".
        /// Set sẽ lưu text và không parse ngay (parse chỗ khác nếu cần).
        /// </summary>
        public string LPRRegions
        {
            get => lprRegions;
            set
            {
                lprRegions = value ?? "";
            }
        }

        public int MinPlateWidth
        {
            get => minPlateWidth;
            set => minPlateWidth = value;
        }

        public int MinPlateHeight
        {
            get => minPlateHeight;
            set => minPlateHeight = value;
        }

        public int MaxPlateWidth
        {
            get => maxPlateWidth;
            set => maxPlateWidth = value;
        }

        public int MaxPlateHeight
        {
            get => maxPlateHeight;
            set => maxPlateHeight = value;
        }

        public float MinConfidenceDay
        {
            get => minConfidenceDay;
            set => minConfidenceDay = value;
        }

        public float MinConfidenceNight
        {
            get => minConfidenceNight;
            set => minConfidenceNight = value;
        }

        public bool EnableCheckPlateRegion
        {
            get => enableCheckPlateRegion;
            set => enableCheckPlateRegion = value;
        }

        public bool EnableCheckPlateSize
        {
            get => enableCheckPlateSize;
            set => enableCheckPlateSize = value;
        }

        public bool EnableCheckPlateConfidence
        {
            get => enableCheckPlateConfidence;
            set => enableCheckPlateConfidence = value;
        }

        #endregion

        #region ==== PHÁT HIỆN CHUYỂN ĐỘNG ====

        private int diMonitorInterval = 1000;
        private bool enableMotionDetection;
        private int motionAlarmLevel = 15;
        private string motionRegions = "";
        private bool displayMotionRegion;
        private int motionDetectionInterval = 100;
        private Rectangle[] motionRectangles;

        /// <summary>
        /// Khoảng thời gian đọc DI (nếu có) theo ms.
        /// </summary>
        public int DIMonitorInterval
        {
            get => diMonitorInterval;
            set => diMonitorInterval = value;
        }

        /// <summary>
        /// Bật/tắt giám sát chuyển động.
        /// </summary>
        public bool EnableMotionDetection
        {
            get => enableMotionDetection;
            set
            {
                enableMotionDetection = value;
                UpdateMotionDetection();
            }
        }

        /// <summary>
        /// Ngưỡng báo động chuyển động (0–100).
        /// </summary>
        public int MotionAlarmLevel
        {
            get => motionAlarmLevel;
            set
            {
                motionAlarmLevel = value;
                UpdateMotionDetection();
            }
        }

        /// <summary>
        /// Chuỗi cấu hình vùng chuyển động "x,y,w,h;x,y,w,h;...".
        /// Set sẽ parse thành MotionRectangles.
        /// </summary>
        public string MotionRegions
        {
            get => motionRegions;
            set
            {
                motionRegions = value;
                motionRectangles = GetMotionRectangles();
                UpdateMotionDetection();
            }
        }

        /// <summary>
        /// Có hiển thị vùng motion trên video preview hay không.
        /// </summary>
        public bool DisplayMotionRegion
        {
            get => displayMotionRegion;
            set
            {
                displayMotionRegion = value;
                UpdateMotionDetection();
            }
        }

        /// <summary>
        /// Có suppress noise (lọc nhiễu) khi detect motion hay không.
        /// </summary>
        public bool SuppressNoise
        {
            get => suppressNoise;
            set => suppressNoise = value;
        }

        /// <summary>
        /// Khoảng thời gian giữa hai lần phân tích motion (ms).
        /// </summary>
        public int MotionDetectionInterval
        {
            get => motionDetectionInterval;
            set
            {
                motionDetectionInterval = value;
                UpdateMotionDetection();
            }
        }

        /// <summary>
        /// Mảng vùng chuyển động đã parse từ MotionRegions.
        /// </summary>
        public Rectangle[] MotionRectangles => motionRectangles;

        #endregion

        #region ==== VÙNG GƯƠNG MẶT / VÙNG BÁO TRỐNG / VÙNG CẢNH BÁO ====

        // Tuỳ hệ thống mà bạn sẽ mở rộng thêm.
        public List<Rectangle> motionDetectRegions = new List<Rectangle>();
        public List<Rectangle> faceDetectRegions = new List<Rectangle>();

        private bool enableAlarmMonitoring;
        private int alarmMonitoringInterval = 1000;

        /// <summary>
        /// Bật/tắt giám sát vùng cảnh báo (Alarm).
        /// </summary>
        public bool EnableAlarmMonitoring
        {
            get => enableAlarmMonitoring;
            set => enableAlarmMonitoring = value;
        }

        /// <summary>
        /// Khoảng thời gian check alarm (ms).
        /// </summary>
        public int AlarmMonitoringInterval
        {
            get => alarmMonitoringInterval;
            set => alarmMonitoringInterval = value;
        }

        #endregion

        #region ==== NGUỒN ALARM, SNAPSHOT, XOAY, OVERLAY ====

        private IAlarmSource alarmSource;
        private SnapshotSource snapshot;

        private bool flipX;
        private bool flipY;
        private bool rotare90;

        private bool showOverlayFrameRate;
        private string textOverlay = "";
        private TextOverlayLocation textOverlayLocation = TextOverlayLocation.BottomLeft;
        private Color textOverlayColor = Color.Blue;
        private int textOverlaySize = 12;
        private string runtimeOverlay = "";

        private bool embededDateTime;
        private int displayRate = 6;
        private bool fastRendering;
        private bool enableAUXControl;
        private bool enableIRControl;
        private string timeToStartIR = "18:00";
        private string timeToStopIR = "06:00";
        private bool suppressNoise = true;

        private string deinterlaceFilterName = "Deinterlace Filter";
        private bool usingDeinterlaceFilter;
        private int usingPlugins;
        private int cachingTime = 200;

        private int panSpeed = 150;
        private int tiltSpeed = 150;
        private int zoomSpeed = 3;

        private bool automaticCaptureImage;
        private DateTime lastTimeCaptureByMotion = DateTime.Now;

        private System.Timers.Timer snapshotTimer = new System.Timers.Timer();

        private string specialParam = "0000";

        /// <summary>
        /// Lật ngang hình ảnh.
        /// </summary>
        public bool FlipX
        {
            get => flipX;
            set
            {
                flipX = value;
                if (videoSourcePlayer != null)
                {
                    videoSourcePlayer.FlipX = flipX;
                }
            }
        }

        /// <summary>
        /// Lật dọc hình ảnh.
        /// </summary>
        public bool FlipY
        {
            get => flipY;
            set
            {
                flipY = value;
                if (videoSourcePlayer != null)
                {
                    videoSourcePlayer.FlipY = flipY;
                }
            }
        }

        /// <summary>
        /// Xoay 90 độ.
        /// </summary>
        public bool Rotare90
        {
            get => rotare90;
            set
            {
                rotare90 = value;
                if (videoSourcePlayer != null)
                {
                    videoSourcePlayer.Rotare90 = rotare90;
                }
            }
        }

        /// <summary>
        /// FPS thực tế đọc từ player.
        /// </summary>
        public float FPS
        {
            get
            {
                if (videoSourcePlayer != null)
                {
                    return videoSourcePlayer.FPS;
                }
                return 0f;
            }
        }

        /// <summary>
        /// BPS (bitrate) thực tế đọc từ player.
        /// </summary>
        public float BPS
        {
            get
            {
                if (videoSourcePlayer != null)
                {
                    return videoSourcePlayer.BPS;
                }
                return 0f;
            }
        }

        /// <summary>
        /// Camera có đang chạy stream không.
        /// </summary>
        public bool IsRunning
        {
            get
            {
                if (videoSourcePlayer != null)
                {
                    return videoSourcePlayer.IsRunning;
                }
                return false;
            }
        }

        /// <summary>
        /// Thời điểm nhận frame cuối cùng.
        /// </summary>
        public DateTime LastTimeReceivedFrame
        {
            get
            {
                if (videoSourcePlayer != null)
                {
                    return videoSourcePlayer.LastTimeReceivedFrame;
                }
                return DateTime.Now;
            }
            set
            {
                if (videoSourcePlayer != null)
                {
                    videoSourcePlayer.LastTimeReceivedFrame = value;
                }
            }
        }

        /// <summary>
        /// Hiển thị FPS lên overlay hay không.
        /// </summary>
        public bool ShowOverlayFrameRate
        {
            get => showOverlayFrameRate;
            set
            {
                showOverlayFrameRate = value;
                if (videoSourcePlayer != null)
                {
                    videoSourcePlayer.ShowOverlayFrameRate = showOverlayFrameRate;
                }
            }
        }

        /// <summary>
        /// Chuỗi text overlay lên hình.
        /// </summary>
        public string TextOverlay
        {
            get => textOverlay;
            set
            {
                textOverlay = value;
                if (videoSourcePlayer != null)
                {
                    videoSourcePlayer.TextOverlay = textOverlay;
                }
            }
        }

        /// <summary>
        /// Vị trí text overlay.
        /// </summary>
        public TextOverlayLocation TextOverlayLocation
        {
            get => textOverlayLocation;
            set
            {
                textOverlayLocation = value;
                if (videoSourcePlayer != null)
                {
                    videoSourcePlayer.TextOverlayLocation = textOverlayLocation;
                }
            }
        }

        /// <summary>
        /// Màu text overlay.
        /// </summary>
        public Color TextOverlayColor
        {
            get => textOverlayColor;
            set
            {
                textOverlayColor = value;
                if (videoSourcePlayer != null)
                {
                    videoSourcePlayer.TextOverlayColor = textOverlayColor;
                }
            }
        }

        /// <summary>
        /// Cỡ font text overlay.
        /// </summary>
        public int TextOverlaySize
        {
            get => textOverlaySize;
            set
            {
                textOverlaySize = value;
                if (videoSourcePlayer != null)
                {
                    videoSourcePlayer.TextOverlaySize = textOverlaySize;
                }
            }
        }

        /// <summary>
        /// Text overlay runtime, có thể dùng để append thêm thông tin trong lúc chạy.
        /// </summary>
        public string RuntimeOverlay
        {
            get => runtimeOverlay;
            set => runtimeOverlay = value;
        }

        /// <summary>
        /// Bật chèn ngày giờ lên video.
        /// </summary>
        public bool EmbededDateTime
        {
            get => embededDateTime;
            set
            {
                embededDateTime = value;
                if (videoSourcePlayer != null)
                {
                    videoSourcePlayer.EmbededDateTime = value;
                }
            }
        }

        /// <summary>
        /// Tốc độ hiển thị (frame cho UI, có thể thấp hơn frame ghi).
        /// </summary>
        public int DisplayRate
        {
            get => displayRate;
            set
            {
                displayRate = value;
                if (videoSourcePlayer != null)
                {
                    videoSourcePlayer.DisplayRate = displayRate;
                }
            }
        }

        /// <summary>
        /// Bật chế độ render nhanh (có thể giảm chất lượng hiển thị).
        /// </summary>
        public bool FastRendering
        {
            get => fastRendering;
            set
            {
                fastRendering = value;
                if (videoSourcePlayer != null)
                {
                    videoSourcePlayer.FastRendering = fastRendering;
                }
            }
        }

        public bool EnableAUXControl
        {
            get => enableAUXControl;
            set => enableAUXControl = value;
        }

        public bool EnableIRControl
        {
            get => enableIRControl;
            set => enableIRControl = value;
        }

        public string TimeToStartIR
        {
            get => timeToStartIR;
            set => timeToStartIR = value;
        }

        public string TimeToStopIR
        {
            get => timeToStopIR;
            set => timeToStopIR = value;
        }

        public string DeinterlaceFilterName
        {
            get => deinterlaceFilterName;
            set => deinterlaceFilterName = value;
        }

        public bool UsingDeinterlaceFilter
        {
            get => usingDeinterlaceFilter;
            set => usingDeinterlaceFilter = value;
        }

        public int UsingPlugins
        {
            get => usingPlugins;
            set => usingPlugins = value;
        }

        public int CachingTime
        {
            get => cachingTime;
            set => cachingTime = value;
        }

        public int PanSpeed
        {
            get => panSpeed;
            set => panSpeed = value;
        }

        public int TiltSpeed
        {
            get => tiltSpeed;
            set => tiltSpeed = value;
        }

        public int ZoomSpeed
        {
            get => zoomSpeed;
            set => zoomSpeed = value;
        }

        public bool AutomaticCaptureImage
        {
            get => automaticCaptureImage;
            set => automaticCaptureImage = value;
        }

        public DateTime LastTimeCaptureByMotion
        {
            get => lastTimeCaptureByMotion;
            set => lastTimeCaptureByMotion = value;
        }

        /// <summary>
        /// Tham số mở rộng đặc biệt tuỳ từng camera (4 ký tự).
        /// </summary>
        public string SpecialParam
        {
            get => specialParam;
            set => specialParam = value;
        }

        #endregion

        #region ==== SỰ KIỆN PUBLIC ====

        /// <summary>
        /// Raised khi có frame mới từ camera.
        /// </summary>
        //public event NewFrameEventHandler NewFrame;

        /// <summary>
        /// Raised khi có alarm từ nguồn alarm (nếu dùng).
        /// </summary>
        public event EventHandler Alarm;

        /// <summary>
        /// Raised trong quá trình phân tích motion, trả về mức độ motion.
        /// </summary>
        public event MotionAnalyzingEventHandler MotionAnalyzing;

        /// <summary>
        /// Raised khi có event từ AlarmSource.
        /// </summary>
        public event AlarmSourceEventHandler NewAlarmSourceEvent;

        /// <summary>
        /// Raised khi writer ghi file xong.
        /// </summary>
        public event VideoWriterFinishEventHandler VideoWriterFinish;

        /// <summary>
        /// Raised khi có lỗi trong quá trình xử lý camera.
        /// </summary>
        public event CameraErrorEventHandler CameraError;

        /// <summary>
        /// Raised khi có sự kiện snapshot.
        /// </summary>
        public event CameraSnapshotEventHandler CameraSnapshotEvent;

        #endregion

        #region ==== START / STOP / FRAME API ====

        /// <summary>
        /// Bắt đầu kết nối và phát stream camera.
        /// </summary>
        /// <param name="isMotionDetection">
        /// true: bật phát hiện chuyển động (motion detection) trong VideoSourcePlayer/AnvPlayer; 
        /// false: không phân tích chuyển động.
        /// </param>
        /// <param name="alarmLevel">
        /// Ngưỡng báo động (0–100). Được truyền cho AnvPlayer dùng để so sánh mức độ chuyển động.
        /// </param>
        /// <param name="cameraSDK">
        /// 0 = sử dụng KzPlayer; khác 0 = sử dụng AnvPlayer (có hỗ trợ motion detection).
        /// </param>
        /// <param name="delayBetweenTwoMotion">
        /// Khoảng trễ giữa hai lần raise event chuyển động liên tiếp (ms) trong AnvPlayer.
        /// </param>
        public void Start(bool isMotionDetection, double alarmLevel, int cameraSDK, int delayBetweenTwoMotion)
        {
            this.enableMotionDetection = isMotionDetection;
            CloseVideoSource();

            switch (cameraType)
            {
                case CameraType.VideoCaptureDevice:
                    {
                        //IVideoSource captureDevice = new VideoCaptureDevice();
                        //((VideoCaptureDevice)captureDevice).DeinterlaceFilterName = deinterlaceFilterName;
                        //((VideoCaptureDevice)captureDevice).UsingDeInterlaceFilter = usingDeinterlaceFilter;
                        //videoSourcePlayer = new VideoSourcePlayer();

                        //if (captureDevice != null)
                        //{
                        //    captureDevice.Source = videoSource;

                        //    if (frameRate > 0)
                        //    {
                        //        captureDevice.DesiredFrameRate = frameRate;
                        //    }

                        //    int width = 0;
                        //    int height = 0;
                        //    string[] tmpSize = resolution.Split(new char[1] { 'x' });
                        //    if (tmpSize.Length == 2 &&
                        //        (!int.TryParse(tmpSize[0], out width) || !int.TryParse(tmpSize[1], out height)))
                        //    {
                        //        width = 0;
                        //        height = 0;
                        //    }

                        //    Size desiredFrameSize = new Size(width, height);
                        //    if (desiredFrameSize.Width > 0 && desiredFrameSize.Height > 0)
                        //    {
                        //        captureDevice.DesiredFrameSize = desiredFrameSize;
                        //    }

                        //    OpenVideoSource(captureDevice);
                        //}
                        break;
                    }

                case CameraType.HIKVISION:
                    {
                        //var videoStream3 = new CameraStreamInfo();
                        //videoSourcePlayer = new HikPlayer();
                        //((HikPlayer)videoSourcePlayer).Login = login;
                        //((HikPlayer)videoSourcePlayer).Password = password;
                        //((HikPlayer)videoSourcePlayer).Channel = chanel;
                        //((HikPlayer)videoSourcePlayer).HttpURL = videoSource;
                        //((HikPlayer)videoSourcePlayer).HttpPort = httpPort;
                        //((HikPlayer)videoSourcePlayer).ServerPort = serverPort;

                        //if (videoStream3 != null)
                        //{
                        //    OpenVideoSource(videoStream3);
                        //}
                        break;
                    }

                case CameraType.Avigilon:
                    {
                        var videoStream4 = new CameraStreamInfo();

                        videoSourcePlayer = new AnvPlayer(isMotionDetection, alarmLevel)
                        {
                            Resolution = this.Resolution,
                            delayBetweenTwoMotion = delayBetweenTwoMotion
                        };

                        videoStream4.Source = $"rtsp://{login}:{password}@{videoSource}:{RtspPort}/rtsp/defaultPrimary?streamType=m";

                        if (videoStream4 != null)
                        {
                            OpenVideoSource(videoStream4);
                        }
                        break;
                    }

                case CameraType.Enster:
                    {
                        var videoStream4 = new CameraStreamInfo();

                        videoSourcePlayer = new AnvPlayer(isMotionDetection, alarmLevel)
                        {
                            Resolution = this.Resolution,
                            delayBetweenTwoMotion = delayBetweenTwoMotion
                        };

                        if (string.IsNullOrEmpty(password))
                        {
                            // rtsp://IP:554/user=admin_password=tlJwpbo6_channel=1_stream=1.sdp?real_stream
                            videoStream4.Source =
                                $"rtsp://{videoSource}:{RtspPort}/user=admin_password=tlJwpbo6_channel=1_stream=1.sdp?real_stream";
                        }
                        else
                        {
                            videoStream4.Source = $"rtsp://{login}:{password}@{videoSource}:{RtspPort}/1{chanel}";
                        }

                        if (videoStream4 != null)
                        {
                            OpenVideoSource(videoStream4);
                        }
                        break;
                    }

                case CameraType.Vivantek:
                    {
                        var videoStream4 = new CameraStreamInfo();

                        videoSourcePlayer = new AnvPlayer(isMotionDetection, alarmLevel)
                        {
                            Resolution = this.Resolution,
                            delayBetweenTwoMotion = delayBetweenTwoMotion
                        };

                        // rtsp://user:pass@ip:port/main
                        videoStream4.Source = $"rtsp://{login}:{password}@{videoSource}:{RtspPort}/main";

                        if (videoStream4 != null)
                        {
                            OpenVideoSource(videoStream4);
                        }
                        break;
                    }

                case CameraType.Enster1:
                    {
                        var videoStream4 = new CameraStreamInfo();

                        videoSourcePlayer = new AnvPlayer(isMotionDetection, alarmLevel)
                        {
                            Resolution = this.Resolution,
                            delayBetweenTwoMotion = delayBetweenTwoMotion
                        };

                        videoStream4.Source =
                            $"rtsp://{login}:{password}@{videoSource}:{RtspPort}/channel=1_stream=0&protocol=unicast.sdp?real_stream";

                        if (videoStream4 != null)
                        {
                            OpenVideoSource(videoStream4);
                        }
                        break;
                    }

                case CameraType.Hanse:
                    {
                        var videoStream5 = new CameraStreamInfo();

                        videoSourcePlayer = new AnvPlayer(isMotionDetection, alarmLevel)
                        {
                            Resolution = this.Resolution,
                            delayBetweenTwoMotion = delayBetweenTwoMotion
                        };

                        videoStream5.Source = $"rtsp://{login}:{password}@{videoSource}:{RtspPort}/stream{chanel}";

                        if (videoStream5 != null)
                        {
                            OpenVideoSource(videoStream5);
                        }
                        break;
                    }

                case CameraType.Tiandy:
                    {
                        var videoStream6 = new CameraStreamInfo();
                        videoStream6.Source = $"rtsp://{login}:{password}@{videoSource}:{RtspPort}/{chanel}/1";

                        videoSourcePlayer = new AnvPlayer(isMotionDetection, alarmLevel)
                        {
                            Resolution = this.Resolution,
                            delayBetweenTwoMotion = delayBetweenTwoMotion
                        };

                        if (videoStream6 != null)
                        {
                            OpenVideoSource(videoStream6);
                        }
                        break;
                    }

                case CameraType.DMAX:
                    {
                        var videoStreamDmax = new CameraStreamInfo();

                        videoSourcePlayer = new AnvPlayer(isMotionDetection, alarmLevel)
                        {
                            Resolution = this.Resolution,
                            delayBetweenTwoMotion = delayBetweenTwoMotion
                        };

                        videoStreamDmax.Source =
                            $"rtsp://{login}:{password}@{videoSource}:{RtspPort}/1/stream{chanel}";

                        if (videoStreamDmax != null)
                        {
                            OpenVideoSource(videoStreamDmax);
                        }
                        break;
                    }

                case CameraType.Dahua:
                    {
                        var videoStream7 = new CameraStreamInfo();

                        videoSourcePlayer = new AnvPlayer(isMotionDetection, alarmLevel)
                        {
                            Resolution = this.Resolution,
                            delayBetweenTwoMotion = delayBetweenTwoMotion
                        };

                        string chanel_str = (chanel > 0) ? chanel.ToString() : "1";
                        // subtype=0 : main stream, subtype=1 : sub-stream
                        videoStream7.Source =
                            $"rtsp://{login}:{password}@{videoSource}:{RtspPort}/cam/realmonitor?channel={chanel_str}&subtype=0";

                        if (videoStream7 != null)
                        {
                            OpenVideoSource(videoStream7);
                        }
                        break;
                    }

                case CameraType.HIKVISION2:
                    {
                        var videoStream8 = new CameraStreamInfo();

                        videoSourcePlayer = new AnvPlayer(isMotionDetection, alarmLevel)
                        {
                            Resolution = this.Resolution,
                            delayBetweenTwoMotion = delayBetweenTwoMotion
                        };

                        // /PSIA/streaming/channels/{channel}0{streamIndex}
                        videoStream8.Source =
                            $"rtsp://{login}:{password}@{videoSource}:{RtspPort}/PSIA/streaming/channels/{((chanel <= 0) ? 1 : chanel)}0{streamIndex}";

                        if (videoStream8 != null)
                        {
                            OpenVideoSource(videoStream8);
                        }
                        break;
                    }

                case CameraType.CNB:
                    {
                        var videoStream8 = new CameraStreamInfo();

                        videoSourcePlayer = new AnvPlayer(isMotionDetection, alarmLevel)
                        {
                            Resolution = this.Resolution,
                            delayBetweenTwoMotion = delayBetweenTwoMotion
                        };

                        // /media/video{channel}0{streamIndex}
                        videoStream8.Source =
                            $"rtsp://{login}:{password}@{videoSource}:{RtspPort}/media/video{((chanel <= 0) ? 1 : chanel)}0{streamIndex}";

                        if (videoStream8 != null)
                        {
                            OpenVideoSource(videoStream8);
                        }
                        break;
                    }

                case CameraType.HANET:
                    {
                        var videoStream8 = new CameraStreamInfo();

                        videoSourcePlayer = new AnvPlayer(isMotionDetection, alarmLevel)
                        {
                            Resolution = this.Resolution,
                            delayBetweenTwoMotion = delayBetweenTwoMotion
                        };

                        // Demo: fixed stream
                        videoStream8.Source = "rtsp://192.168.20.50:554/user:hanet;pwd:hanet123-main264";

                        if (videoStream8 != null)
                        {
                            OpenVideoSource(videoStream8);
                        }
                        break;
                    }

                case CameraType.PELCO:
                    {
                        var videoStream4 = new CameraStreamInfo();

                        videoSourcePlayer = new AnvPlayer(isMotionDetection, alarmLevel)
                        {
                            Resolution = this.Resolution,
                            delayBetweenTwoMotion = delayBetweenTwoMotion
                        };

                        videoStream4.Source =
                            $"rtsp://{login}:{password}@{videoSource}:{RtspPort}/rtsp/defaultPrimary?streamType=u";

                        if (videoStream4 != null)
                        {
                            OpenVideoSource(videoStream4);
                        }
                        break;
                    }

                case CameraType.Bosch:
                    {
                        var videoStream4 = new CameraStreamInfo();

                        videoSourcePlayer = new AnvPlayer(isMotionDetection, alarmLevel)
                        {
                            Resolution = this.Resolution,
                            delayBetweenTwoMotion = delayBetweenTwoMotion
                        };

                        videoStream4.Source =
                            $"rtsp://{login}:{password}@{videoSource}:{RtspPort}/rtsp_tunnel?p=0&line=1&inst=1&vcd=2";

                        if (videoStream4 != null)
                        {
                            OpenVideoSource(videoStream4);
                        }
                        break;
                    }

                case CameraType.Custom:
                    {
                        //// Ở đây bạn đang dùng capture device local
                        //IVideoSource captureDevice = new VideoCaptureDevice();
                        //((VideoCaptureDevice)captureDevice).DeinterlaceFilterName = deinterlaceFilterName;
                        //((VideoCaptureDevice)captureDevice).UsingDeInterlaceFilter = usingDeinterlaceFilter;
                        //videoSourcePlayer = new VideoSourcePlayer();

                        //if (captureDevice != null)
                        //{
                        //    captureDevice.Source = videoSource;

                        //    if (frameRate > 0)
                        //    {
                        //        captureDevice.DesiredFrameRate = frameRate;
                        //    }

                        //    int width = 0;
                        //    int height = 0;
                        //    string[] tmpSize = resolution.Split(new char[1] { 'x' });
                        //    if (tmpSize.Length == 2 &&
                        //        (!int.TryParse(tmpSize[0], out width) || !int.TryParse(tmpSize[1], out height)))
                        //    {
                        //        width = 0;
                        //        height = 0;
                        //    }

                        //    Size desiredFrameSize = new Size(width, height);
                        //    if (desiredFrameSize.Width > 0 && desiredFrameSize.Height > 0)
                        //    {
                        //        captureDevice.DesiredFrameSize = desiredFrameSize;
                        //    }

                        //    OpenVideoSource(captureDevice);
                        //}
                        break;
                    }

                case CameraType.ZKteco:
                    {
                        var videoStream6 = new CameraStreamInfo();
                        videoStream6.Source =
                            $"rtsp://{login}:{password}@{videoSource}:{RtspPort}/ch01.264";

                        videoSourcePlayer = new AnvPlayer(isMotionDetection, alarmLevel)
                        {
                            Resolution = this.Resolution,
                            delayBetweenTwoMotion = delayBetweenTwoMotion
                        };

                        if (videoStream6 != null)
                        {
                            OpenVideoSource(videoStream6);
                        }
                        break;
                    }

                case CameraType.Hanwha:
                    {
                        var videoStreamHanwha = new CameraStreamInfo();

                        videoSourcePlayer = new AnvPlayer(isMotionDetection, alarmLevel)
                        {
                            Resolution = this.Resolution,
                            delayBetweenTwoMotion = delayBetweenTwoMotion
                        };

                        // rtsp://user:password@ip:port/profile1/media.smp
                        videoStreamHanwha.Source =
                            $"rtsp://{login}:{password}@{videoSource}:{RtspPort}/profile1/media.smp";

                        if (videoStreamHanwha != null)
                        {
                            OpenVideoSource(videoStreamHanwha);
                        }
                        break;
                    }

                case CameraType.IPRO:
                    {
                        var videoStreamIPro = new CameraStreamInfo();

                        videoSourcePlayer = new AnvPlayer(isMotionDetection, alarmLevel)
                        {
                            Resolution = this.Resolution,
                            delayBetweenTwoMotion = delayBetweenTwoMotion
                        };

                        // rtsp://user:password@ip:port/ONVIF/MediaInput?profile=def_profile{channel}
                        videoStreamIPro.Source =
                            $"rtsp://{login}:{password}@{videoSource}:{RtspPort}/ONVIF/MediaInput?profile=def_profile{chanel}";

                        if (videoStreamIPro != null)
                        {
                            OpenVideoSource(videoStreamIPro);
                        }
                        break;
                    }

                case CameraType.VideoFile:
                    // TODO: Implement nếu dùng file.
                    break;
            }
        }

        /// <summary>
        /// Gửi tín hiệu dừng (SignalToStop) cho VideoSourcePlayer nhưng không giải phóng resource.
        /// </summary>
        public void SignalToStop()
        {
            if (videoSourcePlayer != null)
            {
                videoSourcePlayer.SignalToStop();
            }
        }

        /// <summary>
        /// Dừng camera và giải phóng các resource liên quan (PTZ, snapshot, videoSourcePlayer).
        /// </summary>
        public void Stop()
        {
            CloseVideoSource();
        }

        /// <summary>
        /// Lấy frame hiện tại từ videoSourcePlayer (nếu có).
        /// </summary>
        /// <returns>
        /// Bitmap chứa frame hiện tại; trả về null nếu videoSourcePlayer chưa chạy hoặc không có frame.
        /// </returns>
        public Bitmap GetCurrentVideoFrame()
        {
            if (videoSourcePlayer != null)
            {
                return videoSourcePlayer.GetCurrentVideoFrame2();
            }
            return null;
        }

        /// <summary>
        /// Bọc hàm <see cref="GetCurrentVideoFrame"/> dưới dạng async, 
        /// dùng khi không muốn block UI thread.
        /// </summary>
        public async Task<Bitmap> GetCurrentVideoFrameAsync()
        {
            return await Task.Run(() => GetCurrentVideoFrame());
        }

        /// <summary>
        /// Lưu frame hiện tại ra file (nếu có) với đường dẫn chỉ định.
        /// </summary>
        /// <param name="path">Đường dẫn file đầy đủ (bao gồm tên file và phần mở rộng).</param>
        public void SaveCurrentVideoFrame(string path)
        {
            if (videoSourcePlayer != null)
            {
                videoSourcePlayer.SaveCurrentVideoFrame(path);
            }
        }

        #endregion

        #region ==== STREAM INTERNAL HELPERS ====

        /// <summary>
        /// Thiết lập kích thước frame mong muốn (DesiredFrameSize) cho nguồn video.
        /// </summary>
        /// <param name="resolution">
        /// Chuỗi độ phân giải dạng "WxH", ví dụ "640x480", "1280x720".
        /// Nếu parse lỗi sẽ fallback về 640x480.
        /// </param>
        /// <param name="videoStream">
        /// Đối tượng <see cref="IVideoSource"/> sẽ được gán thuộc tính <c>DesiredFrameSize</c>.
        /// </param>
        //private void SetDesiredFrameSize(string resolution, ref IVideoSource videoStream)
        //{
        //    int width = 640;
        //    int height = 480;
        //    string[] tmpSize = resolution.Split(new char[1] { 'x' });

        //    if (tmpSize.Length == 2 && (!int.TryParse(tmpSize[0], out width) || !int.TryParse(tmpSize[1], out height)))
        //    {
        //        width = 640;
        //        height = 480;
        //    }

        //    videoStream.DesiredFrameSize = new Size(width, height);
        //}

        /// <summary>
        /// Khởi tạo VideoSourcePlayer, bind event, cấu hình overlay và bắt đầu stream.
        /// </summary>
        /// <param name="source">Nguồn video (IVideoSource) đã cấu hình URL/Source.</param>
        private void OpenVideoSource(CameraStreamInfo source)
        {
            try
            {
                if (videoSourcePlayer == null)
                {
                    videoSourcePlayer = new AnvPlayer();
                }

                //SetDesiredFrameSize(resolution, ref source);
                source.Login = login;
                source.Password = password;
                videoSourcePlayer.RecordingFolder = recordingFolder;
                videoSourcePlayer.RecordingVideoLength = recordingVideoLength;
                videoSourcePlayer.VideoSource = source;
                videoSourcePlayer.ShowOverlayFrameRate = showOverlayFrameRate;
                videoSourcePlayer.TextOverlay = textOverlay;
                videoSourcePlayer.TextOverlayLocation = textOverlayLocation;
                videoSourcePlayer.TextOverlayColor = textOverlayColor;
                videoSourcePlayer.FlipX = flipX;
                videoSourcePlayer.FlipY = flipY;
                videoSourcePlayer.Rotare90 = rotare90;
                videoSourcePlayer.EmbededDateTime = embededDateTime;
                videoSourcePlayer.DisplayRate = displayRate;
                videoSourcePlayer.FastRendering = fastRendering;

                videoSourcePlayer.NewFrame += videoSourcePlayer_NewFrame;

                videoSourcePlayer.Start();
                UpdateMotionDetection();
            }
            catch (Exception ex)
            {
                CameraError?.Invoke(this, ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        /// <summary>
        /// Dừng videoSourcePlayer, PTZ, snapshot, hủy event và giải phóng tham chiếu.
        /// </summary>
        private void CloseVideoSource()
        {
            try
            {
                if (videoSourcePlayer == null)
                {
                    return;
                }

                videoSourcePlayer.StopMotionDetection();

                videoSourcePlayer.NewFrame -= videoSourcePlayer_NewFrame;
                videoSourcePlayer.SignalToStop();

                if (ptz != null)
                {
                    ptz.SignalToStop();
                }

                if (snapshot != null)
                {
                    snapshot.SignalToStop();
                }

                for (int i = 0; i < 20; i++)
                {
                    if (videoSourcePlayer != null && videoSourcePlayer.IsRunning)
                    {
                        Thread.Sleep(25);
                    }
                }

                if (videoSourcePlayer != null && videoSourcePlayer.IsRunning)
                {
                    videoSourcePlayer.Stop();
                    ptz?.Stop();
                    snapshot?.Stop();
                }

                videoSourcePlayer = null;
                ptz = null;
                snapshot = null;
            }
            catch (Exception ex)
            {
                CameraError?.Invoke(this, ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        #endregion

        #region ==== EVENT HANDLERS NỘI BỘ ====

        /// <summary>
        /// Event nội bộ: nhận frame mới từ VideoSourcePlayer và raise lại event NewFrame ra ngoài.
        /// </summary>
        private void videoSourcePlayer_NewFrame(object sender, ref Bitmap image)
        {
            //NewFrame?.Invoke(this, new NewFrameEventArgs(new Bitmap(image)));
        }
        #endregion

        #region ==== MOTION DETECTION HELPERS ====

        /// <summary>
        /// Cập nhật cấu hình phát hiện chuyển động (motion detection) cho VideoSourcePlayer.
        /// </summary>
        private void UpdateMotionDetection()
        {
            if (videoSourcePlayer != null)
            {
                videoSourcePlayer.StopMotionDetection();
                videoSourcePlayer.MotionAlarmLevel = motionAlarmLevel;
                videoSourcePlayer.MotionDetectionInterval = motionDetectionInterval;
                videoSourcePlayer.SuppressNoise = suppressNoise;
                videoSourcePlayer.MotionRectangles = motionRectangles;
                videoSourcePlayer.DisplayMotionRegion = displayMotionRegion;

                // Tránh gắn nhiều lần
                videoSourcePlayer.MotionAnalyzing -= VideoSourcePlayer_MotionAnalyzing;
                videoSourcePlayer.MotionAnalyzing += VideoSourcePlayer_MotionAnalyzing;

                if (enableMotionDetection)
                {
                    videoSourcePlayer.StartMotionDetection();
                }
            }
        }

        /// <summary>
        /// Event nội bộ: callback mức độ chuyển động (0–100) từ VideoSourcePlayer.
        /// Forward ra event MotionAnalyzing.
        /// </summary>
        private void VideoSourcePlayer_MotionAnalyzing(object sender, float motionLevel)
        {
            MotionAnalyzing?.Invoke(this, motionLevel);
        }

        /// <summary>
        /// Parse chuỗi <see cref="motionRegions"/> thành mảng Rectangle dùng cho MotionRectangles.
        /// </summary>
        /// <returns>Mảng Rectangle hoặc null nếu không có cấu hình.</returns>
        public Rectangle[] GetMotionRectangles()
        {
            Rectangle[] motionRectangles = null;

            if (!string.IsNullOrEmpty(motionRegions))
            {
                List<Rectangle> rectangles = new List<Rectangle>();

                string[] items = motionRegions.Split(new char[1] { ';' });
                for (int i = 0; i < items.Length; i++)
                {
                    string[] rect = items[i].Split(new char[1] { ',' });
                    if (rect.Length != 4)
                    {
                        continue;
                    }

                    try
                    {
                        Rectangle rectangle = new Rectangle(
                            int.Parse(rect[0]),
                            int.Parse(rect[1]),
                            int.Parse(rect[2]),
                            int.Parse(rect[3])
                        );
                        rectangles.Add(rectangle);
                    }
                    catch (Exception ex)
                    {
                        CameraError?.Invoke(this, "GetMotionRectangle: " + ex.Message);
                    }
                }

                motionRectangles = new Rectangle[rectangles.Count];
                rectangles.CopyTo(motionRectangles);
            }

            return motionRectangles;
        }

        /// <summary>
        /// Convert mảng Rectangle thành chuỗi cấu hình vùng chuyển động (motionRegions).
        /// </summary>
        /// <param name="rectangles">Danh sách vùng chuyển động.</param>
        /// <returns>
        /// Chuỗi cấu hình dạng "x,y,w,h;x,y,w,h;..." hoặc string.Empty nếu không có vùng.
        /// </returns>
        public string GetMotionRegions(Rectangle[] rectangles)
        {
            string text = "";

            if (rectangles != null && rectangles.Length != 0)
            {
                for (int i = 0; i < rectangles.Length; i++)
                {
                    Rectangle rectangle = rectangles[i];

                    text += $"{rectangle.X},{rectangle.Y},{rectangle.Width},{rectangle.Height}";

                    if (i < rectangles.Length - 1)
                    {
                        text += ";";
                    }
                }
            }

            return text;
        }

        #endregion
    }
}
