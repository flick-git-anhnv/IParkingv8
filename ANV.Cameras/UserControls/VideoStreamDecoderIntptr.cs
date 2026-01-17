using ANV.Cameras.Enums;
using ANV.Cameras.Objects;
using ANV.Cameras.PINVOKE;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

public  class VideoStreamDecoderIntptr : IDisposable
{
    #region Properties
    private IntPtr fmtCtx;
    private IntPtr _pCodecContext;
    private IntPtr _pFrame;
    private IntPtr _pPacket;
    private int _streamIndex;

    AVPixelFormat pixelFormat;
    int width = 0;
    int height = 0;

    private string url;
    #endregion

    #region Constructor
    public VideoStreamDecoderIntptr(string url)
    {
        this.url = url;
    }
    #endregion

    #region Public Function
    public bool Connect()
    {
        //B1: Thiết lập tùy chọn kết nối
        IntPtr options = IntPtr.Zero;
        if (!FFMPEG.SetParam(5000000, "tcp", ref options))
        {
            return false;
        }

        //B2: Mở kết nối
        if (!FFMPEG.OpenRTSP(url, out fmtCtx, ref options))
        {
            return false;
        }

        //B3: Tìm codec
        if (FFMPEG.avformat_find_stream_info(fmtCtx, IntPtr.Zero) < 0)
        {
            return false;
        }
        AVFormatContext formatContext = Marshal.PtrToStructure<AVFormatContext>(fmtCtx);
        IntPtr codecCtx = GetCodec(formatContext);
        if (codecCtx == IntPtr.Zero)
        {
            return false;
        }

        AVCodecContext codecContext = Marshal.PtrToStructure<AVCodecContext>(codecCtx);
        width = codecContext.width;
        height = codecContext.height;
        pixelFormat = codecContext.pix_fmt;

        //B4: Cấp phát Packet và Frame
        _pPacket = FFMPEG.av_packet_alloc();
        _pFrame = FFMPEG.av_frame_alloc();

        return true;
    }
    public Bitmap TryDecodeFrame(CancellationToken token, int timeoutMs = 10000)
    {
        var startTime = DateTime.Now;

        while (!token.IsCancellationRequested)
        {
            if ((DateTime.Now - startTime).TotalMilliseconds > timeoutMs)
                throw new TimeoutException("Đọc frame quá lâu - vượt quá timeout");

            if (FFMPEG.av_read_frame(fmtCtx, _pPacket) < 0)
            {
                Task.Delay(10).Wait();
                continue;
            }
            var packet = Marshal.PtrToStructure<AVPacket>(_pPacket);
            if (packet.stream_index != _streamIndex)
            {
                FFMPEG.av_packet_unref(_pPacket);
                continue;
            }

            int sendPacketResult = FFMPEG.avcodec_send_packet(_pCodecContext, _pPacket);
            FFMPEG.av_packet_unref(_pPacket);
            if (sendPacketResult < 0)
                continue;

            int receiveResult = FFMPEG.avcodec_receive_frame(_pCodecContext, _pFrame);
            if (receiveResult == 0)
            {
                var bitmap = ConvertFrameToBitmap(_pFrame, width, height);
                FFMPEG.av_frame_unref(_pFrame);
                return bitmap;
            }

            Task.Delay(1).Wait();
        }

        throw new OperationCanceledException();
    }
    public void Dispose()
    {
        IntPtr pPacket = _pPacket;
        FFMPEG.av_packet_free(ref pPacket);
        _pPacket = IntPtr.Zero;

        IntPtr pFrame = _pFrame;
        FFMPEG.av_frame_free(ref pFrame);
        _pFrame = IntPtr.Zero;

        IntPtr pCodecContext = _pCodecContext;
        FFMPEG.avcodec_free_context(ref pCodecContext);
        _pCodecContext = IntPtr.Zero;

        IntPtr pFormatContext = fmtCtx;
        FFMPEG.avformat_close_input(ref pFormatContext);
        fmtCtx = IntPtr.Zero;
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
    private Bitmap? ConvertFrameToBitmap(IntPtr frame, int width, int height)
    {
        if (frame == IntPtr.Zero)
            throw new ArgumentNullException(nameof(frame));
        if (width <= 0 || height <= 0)
            throw new ArgumentException("Invalid width or height.");

        AVFrame avframe = Marshal.PtrToStructure<AVFrame>(frame);

        try
        {
            IntPtr convertContext = FFMPEG.sws_getContext(
                width, height, (int)(AVPixelFormat)avframe.format,
                this.width, height, (int)AVPixelFormat.AV_PIX_FMT_BGR24,
                0x200, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

            if (convertContext == IntPtr.Zero)
                throw new InvalidOperationException("sws_getContext failed.");

            var bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            var bmpData = bitmap.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly, bitmap.PixelFormat);

            IntPtr[] srcData = new IntPtr[]
            {
            avframe.data0, avframe.data1, avframe.data2, avframe.data3
            };
            int[] srcStride = new int[]
            {
            avframe.linesize0, avframe.linesize1, avframe.linesize2, avframe.linesize3
            };
            int[] dstStride = new int[] { bmpData.Stride };

            FFMPEG.sws_scale(convertContext, srcData, srcStride, 0, height, new IntPtr[] { bmpData.Scan0 }, dstStride);
            bitmap.UnlockBits(bmpData);

            FFMPEG.sws_freeContext(convertContext);
            return bitmap;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi chuyển frame: " + ex.Message);
            return null;
        }
    }
    #endregion
}
