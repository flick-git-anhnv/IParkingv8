// This updated version of VideoStreamDecoderIntptr uses GPU decoding via FFmpeg hardware acceleration (e.g., D3D11VA or CUVID)
// Ensure that your FFmpeg build supports GPU decoders, and adapt FFMPEG bindings accordingly

using ANV.Cameras.Enums;
using ANV.Cameras.Objects;
using ANV.Cameras.PINVOKE;
using Kztek.Cameras.Players.FFMPEG.Enums;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

public class HwVideoStreamDecoder : IDisposable
{
    private IntPtr fmtCtx;
    private IntPtr _pCodecContext;
    private IntPtr _pHwDeviceCtx;
    private IntPtr _pFrame;
    private IntPtr _lastFrame;
    private IntPtr _pPacket;
    private int _streamIndex;

    AVPixelFormat pixelFormat;
    int width = 0;
    int height = 0;

    private string url;

    public HwVideoStreamDecoder(string url)
    {
        this.url = url;
    }

    public bool Connect()
    {
        IntPtr options = IntPtr.Zero;
        if (!FFMPEG.SetParam(5000000, "tcp", ref options)) return false;
        if (!FFMPEG.OpenRTSP(url, out fmtCtx, ref options)) return false;
        if (FFMPEG.avformat_find_stream_info(fmtCtx, IntPtr.Zero) < 0) return false;

        AVFormatContext formatContext = Marshal.PtrToStructure<AVFormatContext>(fmtCtx);
        IntPtr codecCtx = GetHwAcceleratedCodec(formatContext);
        if (codecCtx == IntPtr.Zero) return false;

        AVCodecContext codecContext = Marshal.PtrToStructure<AVCodecContext>(codecCtx);
        width = codecContext.width;
        height = codecContext.height;
        pixelFormat = codecContext.pix_fmt;

        _pPacket = FFMPEG.av_packet_alloc();
        _pFrame = FFMPEG.av_frame_alloc();
        return true;
    }

    private IntPtr GetHwAcceleratedCodec(AVFormatContext formatContext)
    {
        for (int i = 0; i < formatContext.nb_streams; i++)
        {
            IntPtr streamPtr = Marshal.ReadIntPtr(formatContext.streams, i * IntPtr.Size);
            if (streamPtr == IntPtr.Zero) continue;

            AVStream stream = Marshal.PtrToStructure<AVStream>(streamPtr);
            AVCodecParameters codecpar = Marshal.PtrToStructure<AVCodecParameters>(stream.codecpar);
            if (codecpar.codec_type != AVMediaType.AVMEDIA_TYPE_VIDEO) continue;

            string codecName = codecpar.codec_id == AVCodecID.AV_CODEC_ID_HEVC ? "hevc_cuvid" :
                               codecpar.codec_id == AVCodecID.AV_CODEC_ID_H264 ? "h264_cuvid" : null;

            //IntPtr codec = FFMPEG.avcodec_find_decoder(codecpar.codec_id);

            IntPtr codec = IntPtr.Zero;
            codec = FFMPEG.avcodec_find_decoder_by_name(codecName); 
            if (codec == IntPtr.Zero) continue;

            IntPtr codecCtx = FFMPEG.avcodec_alloc_context3(codec);
            _pCodecContext = codecCtx;
            FFMPEG.avcodec_parameters_to_context(codecCtx, stream.codecpar);

            //if (FFMPEG.av_hwdevice_ctx_create(ref _pHwDeviceCtx, AVHWDeviceType.AV_HWDEVICE_TYPE_D3D11VA, null, IntPtr.Zero, 0) < 0)
            if (FFMPEG.av_hwdevice_ctx_create(ref _pHwDeviceCtx, AVHWDeviceType.AV_HWDEVICE_TYPE_CUDA, null, IntPtr.Zero, 0) < 0)
                    return IntPtr.Zero;

            Marshal.WriteIntPtr(codecCtx, Marshal.OffsetOf<AVCodecContext>("hw_device_ctx").ToInt32(), _pHwDeviceCtx);

            IntPtr temp = IntPtr.Zero;
            if (FFMPEG.avcodec_open2(codecCtx, codec, ref temp) < 0) return IntPtr.Zero;

            _streamIndex = i;
            return codecCtx;
        }

        return IntPtr.Zero;
    }

    public Bitmap TryDecodeFrame(CancellationToken token, int destWidth, int destHeight, int timeoutMs = 10000)
    {
        const int AVERROR_EAGAIN = -11;
        const int AVERROR_EOF = -541478725;

        var start = Environment.TickCount;
        while (!token.IsCancellationRequested)
        {
            //if (Environment.TickCount - start > timeoutMs)
            //    throw new TimeoutException();

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

            if (FFMPEG.avcodec_send_packet(_pCodecContext, _pPacket) < 0)
            {
                FFMPEG.av_packet_unref(_pPacket);
                continue;
            }
            FFMPEG.av_packet_unref(_pPacket);

            while (true)
            {
                int v = FFMPEG.avcodec_receive_frame(_pCodecContext, _pFrame);
                if (v == AVERROR_EAGAIN || v == AVERROR_EOF)
                    break;
                if (v < 0)
                    break;

                start = Environment.TickCount;

                IntPtr swFrame = FFMPEG.av_frame_alloc();
                AVFrame frameStruct = Marshal.PtrToStructure<AVFrame>(_pFrame);
                int v1 = 0;

                if (frameStruct.format == (int)AVPixelFormat.AV_PIX_FMT_CUDA ||
                    frameStruct.format == (int)AVPixelFormat.AV_PIX_FMT_DXVA2_VLD ||
                    frameStruct.format == (int)AVPixelFormat.AV_PIX_FMT_D3D11)
                {
                    v1 = FFMPEG.av_hwframe_transfer_data(swFrame, _pFrame, 0);
                }
                else
                {
                    v1 = FFMPEG.av_frame_ref(swFrame, _pFrame);
                }

                if (v1 < 0)
                {
                    FFMPEG.av_frame_free(ref swFrame);
                    continue;
                }

                if (_lastFrame != IntPtr.Zero)
                {
                    FFMPEG.av_frame_unref(_lastFrame);
                    FFMPEG.av_frame_free(ref _lastFrame);
                }

                _lastFrame = CloneFrame(swFrame);
                Bitmap bmp = ConvertFrameToBitmap(swFrame, destWidth, destHeight);
                FFMPEG.av_frame_free(ref swFrame);
                return bmp;
            }

            Task.Delay(1).Wait();
        }

        throw new OperationCanceledException();
    }

    private Bitmap ConvertFrameToBitmap(IntPtr frame, int _width, int _height)
    {
        AVFrame avframe = Marshal.PtrToStructure<AVFrame>(frame);

        IntPtr convertContext = FFMPEG.sws_getContext(
            avframe.width, avframe.height, (int)(AVPixelFormat)avframe.format,
            _width, _height, (int)AVPixelFormat.AV_PIX_FMT_BGR24,
            0x200, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

        Bitmap bitmap = new Bitmap(_width, _height, PixelFormat.Format24bppRgb);
        BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, _width, _height), ImageLockMode.WriteOnly, bitmap.PixelFormat);

        IntPtr[] srcData = new IntPtr[] { avframe.data0, avframe.data1, avframe.data2, avframe.data3 };
        int[] srcStride = new int[] { avframe.linesize0, avframe.linesize1, avframe.linesize2, avframe.linesize3 };
        int[] dstStride = new int[] { bmpData.Stride };

        FFMPEG.sws_scale(convertContext, srcData, srcStride, 0, avframe.height, new IntPtr[] { bmpData.Scan0 }, dstStride);

        bitmap.UnlockBits(bmpData);
        FFMPEG.sws_freeContext(convertContext);
        return bitmap;
    }

    private IntPtr CloneFrame(IntPtr srcFrame)
    {
        IntPtr newFrame = FFMPEG.av_frame_alloc();
        if (FFMPEG.av_frame_ref(newFrame, srcFrame) < 0)
        {
            FFMPEG.av_frame_free(ref newFrame);
            return IntPtr.Zero;
        }
        return newFrame;
    }

    public void Dispose()
    {
        FFMPEG.av_packet_free(ref _pPacket);
        FFMPEG.av_frame_free(ref _pFrame);
        FFMPEG.av_frame_free(ref _lastFrame);
        FFMPEG.avcodec_free_context(ref _pCodecContext);
        FFMPEG.avformat_close_input(ref fmtCtx);
        if (_pHwDeviceCtx != IntPtr.Zero)
        {
            FFMPEG.av_buffer_unref(ref _pHwDeviceCtx);
            _pHwDeviceCtx = IntPtr.Zero;
        }
    }
}
