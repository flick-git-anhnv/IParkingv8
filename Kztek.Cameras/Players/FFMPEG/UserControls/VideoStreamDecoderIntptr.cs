using ANV.Cameras.Enums;
using ANV.Cameras.Objects;
using ANV.Cameras.PINVOKE;
using Kztek.Cameras;
using Kztek.Cameras.Players.FFMPEG;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

public class VideoStreamDecoderIntptr : IDisposable
{
    public event NewFrameHandler NewFrame;

    #region Properties
    private IntPtr fmtCtx;
    private IntPtr _pCodecContext;
    private IntPtr _pFrame;
    private IntPtr _lastFrame;
    private IntPtr _pPacket;
    private int _streamIndex;

    int width = 0;
    int height = 0;

    private string url;

    public BoundedConcurrentQueue<Bitmap> frameQueue = new(15);
    public BoundedConcurrentQueue<Bitmap> frameQueueforSave = new(2);
    // Queue cho motion (giới hạn nhỏ để không backlog)
    public readonly BoundedConcurrentQueue<Bitmap> _mdQueue = new(15);

    // CTS dùng chung để stop worker (nếu cần dùng ngoài)
    private CancellationTokenSource? _cts;
    private bool _isMotionDetection = false;
    #endregion

    #region Constructor
    public VideoStreamDecoderIntptr(string url, bool isMotionDetection)
    {
        this._isMotionDetection = isMotionDetection;
        this.url = url;
    }
    #endregion

    #region Public Function
    public bool Connect()
    {
        IntPtr options = IntPtr.Zero;
        try
        {
            if (!FFMPEG.SetParam(5000000, "tcp", ref options))
                return false;

            if (!FFMPEG.OpenRTSP(url, out fmtCtx, ref options))
                return false;

            if (FFMPEG.avformat_find_stream_info(fmtCtx, IntPtr.Zero) < 0)
                return false;

            AVFormatContext formatContext = Marshal.PtrToStructure<AVFormatContext>(fmtCtx);
            IntPtr codecCtx = GetCodec(formatContext);
            if (codecCtx == IntPtr.Zero)
                return false;

            AVCodecContext codecContext = Marshal.PtrToStructure<AVCodecContext>(codecCtx);
            width = codecContext.width;
            height = codecContext.height;

            _pPacket = FFMPEG.av_packet_alloc();
            _pFrame = FFMPEG.av_frame_alloc();

            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            // cleanup partial when failed
            Dispose();
            return false;
        }
        finally
        {
            if (options != IntPtr.Zero)
            {
                FFMPEG.av_dict_free(ref options);
                options = IntPtr.Zero;
            }
        }
    }

    const int AVERROR_EAGAIN = -11;         // EAGAIN
    const int AVERROR_EOF = -541478725;     // EOF

    private int lastGetMotionImage = 0;

    /// <summary>
    /// Đọc packet – decode – scale frame, trả về frame đầu tiên tạo được
    /// đồng thời push các frame vào frameQueue / _mdQueue cho thread khác dùng.
    /// </summary>
    public Bitmap? TryDecodeFrame(
        CancellationToken token,
        int destWidth,
        int destHeight,
        int delayGetMotion = 1000,
        int timeoutMs = 10000)
    {
        // Ép kích thước >= 1 để tránh tạo Bitmap(0,0)
        destWidth = Math.Max(1, destWidth);
        destHeight = Math.Max(1, destHeight);

        int start = Environment.TickCount;
        Bitmap? firstProduced = null;

        while (!token.IsCancellationRequested)
        {
            // Timeout tổng
            if (Environment.TickCount - start > timeoutMs)
                throw new TimeoutException();

            // 1) Đọc packet
            int rd = FFMPEG.av_read_frame(fmtCtx, _pPacket);
            if (rd < 0)
            {
                // Hết dữ liệu tạm thời → nghỉ nhẹ rồi đọc tiếp
                Thread.Sleep(5);
                continue;
            }

            try
            {
                // Lọc stream video
                AVPacket pkt = Marshal.PtrToStructure<AVPacket>(_pPacket);
                if (pkt.stream_index != _streamIndex)
                {
                    continue; // sẽ unref ở finally
                }

                // 2) Gửi packet vào decoder
                int send = FFMPEG.avcodec_send_packet(_pCodecContext, _pPacket);
                if (send < 0 && send != AVERROR_EAGAIN)
                {
                    // Lỗi khác EAGAIN: bỏ qua packet này
                    continue;
                }

                // 3) Rút hết frame sẵn có
                while (true)
                {
                    int r = FFMPEG.avcodec_receive_frame(_pCodecContext, _pFrame);
                    if (r == 0)
                    {
                        // Giữ bản gốc để chụp ảnh full-res khi cần
                        lock (_frameLock)
                        {
                            if (_lastFrame != IntPtr.Zero)
                            {
                                FFMPEG.av_frame_unref(_lastFrame);
                                FFMPEG.av_frame_free(ref _lastFrame);
                                _lastFrame = IntPtr.Zero;
                            }
                            _lastFrame = CloneFrame(_pFrame);
                        }

                        var bmp = ConvertFrameToBitmap(_pFrame, destWidth, destHeight);
                        if (bmp != null)
                        {
                            // Enqueue cho UI thread sử dụng
                            frameQueue.Enqueue(bmp, b => b.Dispose());

                            // Trả về frame đầu tiên để caller biết “có frame”
                            if (firstProduced == null)
                                firstProduced = bmp;
                        }

                        // Cho motion detection (ảnh nhỏ, rẻ hơn)
                        var now = Environment.TickCount;
                        if (now - lastGetMotionImage >= delayGetMotion && this._isMotionDetection)
                        {
                            AVFrame avframe = Marshal.PtrToStructure<AVFrame>(_pFrame);
                            int w = 320;
                            int h = Math.Max(1, avframe.height * w / avframe.width);
                            var motionBmp = ConvertFrameToBitmap(_pFrame, w, h);
                            if (motionBmp != null)
                            {
                                lastGetMotionImage = now;
                                _mdQueue.Enqueue(motionBmp, b => b.Dispose());
                            }
                        }

                        // Giải phóng frame
                        FFMPEG.av_frame_unref(_pFrame);
                        // Tiếp tục vòng while để lấy thêm frame (nếu có) cho cùng packet
                        continue;
                    }
                    else if (r == AVERROR_EAGAIN)
                    {
                        // Cần packet mới
                        break;
                    }
                    else if (r == AVERROR_EOF)
                    {
                        // Decoder báo EOF
                        break;
                    }
                    else
                    {
                        // Lỗi khác
                        break;
                    }
                }
            }
            finally
            {
                // LUÔN unref packet sau khi xử lý
                FFMPEG.av_packet_unref(_pPacket);
            }

            // Nếu đã tạo được ít nhất 1 bitmap thì trả về (giữ hành vi cũ)
            if (firstProduced != null)
                return firstProduced;

            // Nghỉ nhẹ để tránh quay quá nhanh
            Thread.Sleep(1);
        }

        throw new OperationCanceledException();
    }

    public void Dispose()
    {
        // 1) Dọn queue bitmap
        if (frameQueue != null)
        {
            while (frameQueue.TryDequeue(out var bmp))
                bmp.Dispose();
        }

        if (_mdQueue != null)
        {
            while (_mdQueue.TryDequeue(out var bmp))
                bmp.Dispose();
        }

        // 2) Free last cloned frame
        lock (_frameLock)
        {
            if (_lastFrame != IntPtr.Zero)
            {
                FFMPEG.av_frame_unref(_lastFrame);
                FFMPEG.av_frame_free(ref _lastFrame);
                _lastFrame = IntPtr.Zero;
            }
        }

        // 3) Dọn frame/packet còn đang giữ
        if (_pFrame != IntPtr.Zero)
        {
            FFMPEG.av_frame_unref(_pFrame);
            IntPtr pFrame = _pFrame;
            FFMPEG.av_frame_free(ref pFrame);
            _pFrame = IntPtr.Zero;
        }

        if (_pPacket != IntPtr.Zero)
        {
            FFMPEG.av_packet_unref(_pPacket);
            IntPtr pPacket = _pPacket;
            FFMPEG.av_packet_free(ref pPacket);
            _pPacket = IntPtr.Zero;
        }

        // 4) Flush rồi free codec
        if (_pCodecContext != IntPtr.Zero)
        {
            FFMPEG.avcodec_flush_buffers(_pCodecContext);
            IntPtr pCodecContext = _pCodecContext;
            FFMPEG.avcodec_free_context(ref pCodecContext);
            _pCodecContext = IntPtr.Zero;
        }

        // 5) Đóng input
        if (fmtCtx != IntPtr.Zero)
        {
            IntPtr pFormatContext = fmtCtx;
            FFMPEG.avformat_close_input(ref pFormatContext);
            fmtCtx = IntPtr.Zero;
        }

        // 6) Free sws context
        if (_swsCtx != IntPtr.Zero)
        {
            FFMPEG.sws_freeContext(_swsCtx);
            _swsCtx = IntPtr.Zero;
        }

        GC.SuppressFinalize(this);
    }
    #endregion

    #region Private Function
    private IntPtr GetCodec(AVFormatContext formatContext)
    {
        IntPtr codecCtx = IntPtr.Zero;
        for (int i = 0; i < formatContext.nb_streams; i++)
        {
            // lấy AVStream* từ AVStream**
            IntPtr streamPtr = Marshal.ReadIntPtr(formatContext.streams, i * IntPtr.Size);
            if (streamPtr == IntPtr.Zero) continue;

            AVStream stream = Marshal.PtrToStructure<AVStream>(streamPtr);
            AVCodecParameters codecpar = Marshal.PtrToStructure<AVCodecParameters>(stream.codecpar);

            if (codecpar.codec_type != AVMediaType.AVMEDIA_TYPE_VIDEO)
                continue;

            IntPtr codec = FFMPEG.avcodec_find_decoder(codecpar.codec_id);
            if (codec == IntPtr.Zero) continue;

            codecCtx = FFMPEG.avcodec_alloc_context3(codec);
            _pCodecContext = codecCtx;

            FFMPEG.avcodec_parameters_to_context(codecCtx, stream.codecpar);

            IntPtr temp = IntPtr.Zero;
            FFMPEG.avcodec_open2(codecCtx, codec, ref temp);

            this._streamIndex = i;
            break;
        }

        return codecCtx;
    }

    // ====== SWSCALE CACHE ======
    private IntPtr _swsCtx = IntPtr.Zero;
    private int _swsSrcW, _swsSrcH, _swsDstW, _swsDstH, _swsSrcFmt, _swsDstFmt;
    private int _swsFlags = SwsFlags.SWS_BILINEAR;

    private readonly object _frameLock = new object();

    ///// <summary>
    ///// Convert AVFrame → Bitmap (Format24bppRgb) dùng sws_getCachedContext.
    ///// </summary>
    //private Bitmap? ConvertFrameToBitmap(IntPtr frame, int dstW, int dstH)
    //{
    //    if (frame == IntPtr.Zero) return null;

    //    var av = Marshal.PtrToStructure<AVFrame>(frame);

    //    // Kiểm tra dữ liệu nguồn hợp lệ
    //    if (av.data0 == IntPtr.Zero ||
    //        av.width <= 0 || av.height <= 0 ||
    //        dstW <= 0 || dstH <= 0)
    //    {
    //        return null;
    //    }

    //    const AVPixelFormat DST_FMT = AVPixelFormat.AV_PIX_FMT_BGR24;

    //    // Tạo / cache context SWS
    //    if (_swsCtx == IntPtr.Zero ||
    //        _swsSrcW != av.width || _swsSrcH != av.height ||
    //        _swsDstW != dstW || _swsDstH != dstH ||
    //        _swsSrcFmt != (int)av.format || _swsDstFmt != (int)DST_FMT)
    //    {
    //        _swsCtx = FFMPEG.sws_getCachedContext(
    //            _swsCtx,
    //            av.width, av.height, (int)(AVPixelFormat)av.format,
    //            dstW, dstH, (int)DST_FMT,
    //            _swsFlags,
    //            IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

    //        _swsSrcW = av.width; _swsSrcH = av.height;
    //        _swsDstW = dstW; _swsDstH = dstH;
    //        _swsSrcFmt = (int)av.format; _swsDstFmt = (int)DST_FMT;

    //        if (_swsCtx == IntPtr.Zero)
    //            return null;
    //    }

    //    var bmp = new Bitmap(dstW, dstH, PixelFormat.Format24bppRgb);
    //    var rect = new Rectangle(0, 0, dstW, dstH);
    //    var data = bmp.LockBits(rect, ImageLockMode.WriteOnly, bmp.PixelFormat);

    //    try
    //    {
    //        IntPtr[] srcData =
    //        {
    //            av.data0, av.data1, av.data2, av.data3
    //        };
    //        int[] srcStride =
    //        {
    //            av.linesize0, av.linesize1, av.linesize2, av.linesize3
    //        };

    //        IntPtr[] dstData = { data.Scan0 };
    //        int[] dstStride = { data.Stride };

    //        int scaled = FFMPEG.sws_scale(
    //            _swsCtx,
    //            srcData, srcStride,
    //            0, av.height,
    //            dstData, dstStride);

    //        if (scaled <= 0)
    //        {
    //            bmp.Dispose();
    //            return null;
    //        }

    //        return bmp;
    //    }
    //    finally
    //    {
    //        bmp.UnlockBits(data);
    //    }
    //}

    private readonly object _swsLock = new object();
    private unsafe Bitmap? ConvertFrameToBitmap(IntPtr frame, int dstW, int dstH)
    {
        lock (_swsLock)
        {
            if (frame == IntPtr.Zero) return null;
            var av = Marshal.PtrToStructure<AVFrame>(frame);

            if (av.data0 == IntPtr.Zero ||
                av.width <= 0 || av.height <= 0 ||
                dstW <= 0 || dstH <= 0)
            {
                return null;
            }

            const AVPixelFormat DST_FMT = AVPixelFormat.AV_PIX_FMT_BGR24;

            if (_swsCtx == IntPtr.Zero ||
                _swsSrcW != av.width || _swsSrcH != av.height ||
                _swsDstW != dstW || _swsDstH != dstH ||
                _swsSrcFmt != (int)av.format || _swsDstFmt != (int)DST_FMT)
            {
                _swsCtx = FFMPEG.sws_getCachedContext(
                    _swsCtx,
                    av.width, av.height, (int)(AVPixelFormat)av.format,
                    dstW, dstH, (int)DST_FMT,
                    _swsFlags,
                    IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

                _swsSrcW = av.width; _swsSrcH = av.height;
                _swsDstW = dstW; _swsDstH = dstH;
                _swsSrcFmt = (int)av.format; _swsDstFmt = (int)DST_FMT;

                if (_swsCtx == IntPtr.Zero)
                    return null;
            }

            var bmp = new Bitmap(dstW, dstH, PixelFormat.Format24bppRgb);
            var rect = new Rectangle(0, 0, dstW, dstH);
            var data = bmp.LockBits(rect, ImageLockMode.WriteOnly, bmp.PixelFormat);

            try
            {
                IntPtr* srcData = stackalloc IntPtr[4];
                int* srcStride = stackalloc int[4];

                srcData[0] = av.data0;
                srcData[1] = av.data1;
                srcData[2] = av.data2;
                srcData[3] = av.data3;

                srcStride[0] = av.linesize0;
                srcStride[1] = av.linesize1;
                srcStride[2] = av.linesize2;
                srcStride[3] = av.linesize3;

                IntPtr* dstData = stackalloc IntPtr[1];
                int* dstStride = stackalloc int[1];

                dstData[0] = data.Scan0;
                dstStride[0] = data.Stride;

                int scaled = FFMPEG.sws_scale(
                    _swsCtx,
                    srcData, srcStride,
                    0, av.height,
                    dstData, dstStride);

                if (scaled <= 0)
                {
                    bmp.Dispose();
                    return null;
                }

                return bmp;
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }
    }

    public Bitmap? GetCurrentFrame(int destWidth, int destHeight)
    {
        lock (_frameLock)
        {
            if (_lastFrame == IntPtr.Zero)
                return null;

            try
            {
                AVFrame avframe = Marshal.PtrToStructure<AVFrame>(_lastFrame);
                return ConvertFrameToBitmap(_lastFrame, destWidth, destHeight);
            }
            catch
            {
                return null;
            }
        }
    }
    public Bitmap? GetFullCurrentFrame()
    {
        lock (_frameLock)
        {
            if (_lastFrame == IntPtr.Zero)
                return null;

            try
            {
                AVFrame avframe = Marshal.PtrToStructure<AVFrame>(_lastFrame);
                return ConvertFrameToBitmap(_lastFrame, avframe.width, avframe.height);
            }
            catch
            {
                return null;
            }
        }
    }

    private IntPtr CloneFrame(IntPtr srcFrame)
    {
        if (srcFrame == IntPtr.Zero) return IntPtr.Zero;

        IntPtr newFrame = FFMPEG.av_frame_alloc();
        if (newFrame == IntPtr.Zero) return IntPtr.Zero;

        int ret = FFMPEG.av_frame_ref(newFrame, srcFrame);
        if (ret < 0)
        {
            FFMPEG.av_frame_free(ref newFrame);
            return IntPtr.Zero;
        }

        return newFrame;
    }
    #endregion
}
