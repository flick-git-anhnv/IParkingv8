//// FFmpegDecoder.cs - Sử dụng GPU (CUDA/NVDEC) để decode H.265 từ luồng RTSP
//using FFmpeg.AutoGen;
//using System;
//using System.Drawing;
//using System.Drawing.Imaging;
//using System.Runtime.InteropServices;
//using System.Threading;
//using System.Windows.Forms;

//namespace FFmpegDecoder_H265
//{
//    public unsafe class FFmpegGpuDecoder
//    {
//        private Thread decodeThread;
//        private bool isRunning;
//        private PictureBox displayBox;

//        public FFmpegGpuDecoder()
//        {
//            ffmpeg.av_log_set_level(ffmpeg.AV_LOG_ERROR);
//            ffmpeg.avdevice_register_all();
//            ffmpeg.avformat_network_init();
//        }

//        public void StartDecoding(string url, PictureBox pictureBox)
//        {
//            if (isRunning) return;

//            isRunning = true;
//            displayBox = pictureBox;

//            decodeThread = new Thread(() => DecodeThread(url));
//            decodeThread.IsBackground = true;
//            decodeThread.Start();
//        }

//        public void StopDecoding()
//        {
//            //isRunning = false;
//            //decodeThread?.Join();

//            isRunning = false;

//            // Đợi luồng giải mã kết thúc
//            if (decodeThread != null && decodeThread.IsAlive)
//            {
//                decodeThread?.Join(2000); // Chờ tối đa 2s để tránh treo ứng dụng
//            }
//        }

//        private void DecodeThread(string url)
//        {
//            AVFormatContext* pFormatContext = ffmpeg.avformat_alloc_context();
//            if (ffmpeg.avformat_open_input(&pFormatContext, url, null, null) != 0)
//            {
//                MessageBox.Show("Không mở được stream.");
//                return;
//            }

//            if (ffmpeg.avformat_find_stream_info(pFormatContext, null) != 0)
//            {
//                MessageBox.Show("Không tìm thấy thông tin stream.");
//                return;
//            }

//            AVCodec* pCodec = null;
//            int videoStreamIndex = -1;
//            for (int i = 0; i < pFormatContext->nb_streams; i++)
//            {
//                if (pFormatContext->streams[i]->codecpar->codec_type == AVMediaType.AVMEDIA_TYPE_VIDEO)
//                {
//                    videoStreamIndex = i;
//                    pCodec = ffmpeg.avcodec_find_decoder_by_name("hevc_cuvid");
//                    break;
//                }
//            }

//            if (pCodec == null)
//            {
//                MessageBox.Show("Không tìm thấy codec GPU (hevc_cuvid).");
//                return;
//            }

//            AVCodecContext* pCodecContext = ffmpeg.avcodec_alloc_context3(pCodec);
//            ffmpeg.avcodec_parameters_to_context(pCodecContext, pFormatContext->streams[videoStreamIndex]->codecpar);

//            if (ffmpeg.avcodec_open2(pCodecContext, pCodec, null) != 0)
//            {
//                MessageBox.Show("Không mở được codec.");
//                return;
//            }

//            SwsContext* pSwsContext = ffmpeg.sws_getContext(
//                pCodecContext->width,
//                pCodecContext->height,
//                AVPixelFormat.AV_PIX_FMT_NV12, // định dạng từ GPU
//                pCodecContext->width,
//                pCodecContext->height,
//                AVPixelFormat.AV_PIX_FMT_BGR24,
//                ffmpeg.SWS_BICUBIC,
//                null, null, null);

//            if (pSwsContext == null)
//            {
//                MessageBox.Show("Lỗi khởi tạo SwsContext.");
//                return;
//            }

//            AVPacket* pPacket = ffmpeg.av_packet_alloc();
//            AVFrame* pFrame = ffmpeg.av_frame_alloc();
//            AVFrame* pSwFrame = ffmpeg.av_frame_alloc();
//            AVFrame* pFrameRGB = ffmpeg.av_frame_alloc();

//            int rgbBufferSize = ffmpeg.av_image_get_buffer_size(
//                AVPixelFormat.AV_PIX_FMT_BGR24,
//                pCodecContext->width,
//                pCodecContext->height,
//                1);

//            byte* rgbBuffer = (byte*)ffmpeg.av_malloc((ulong)rgbBufferSize);

//            byte_ptrArray4 dataPtr = new byte_ptrArray4();
//            int_array4 linesizePtr = new int_array4();

//            int fillResult = ffmpeg.av_image_fill_arrays(
//                ref dataPtr,
//                ref linesizePtr,
//                rgbBuffer,
//                AVPixelFormat.AV_PIX_FMT_BGR24,
//                pCodecContext->width,
//                pCodecContext->height,
//                1);

//            if (fillResult < 0)
//            {
//                Console.WriteLine("❌ av_image_fill_arrays failed");
//                return;
//            }

//            for (uint i = 0; i < 4; i++)
//            {
//                pFrameRGB->data[i] = dataPtr[i];
//                pFrameRGB->linesize[i] = linesizePtr[i];
//            }

//            while (isRunning && ffmpeg.av_read_frame(pFormatContext, pPacket) >= 0)
//            {
//                if (pPacket->stream_index == videoStreamIndex)
//                {
//                    ffmpeg.avcodec_send_packet(pCodecContext, pPacket);
//                    if (ffmpeg.avcodec_receive_frame(pCodecContext, pFrame) == 0)
//                    {
//                        if (pFrame->format == (int)AVPixelFormat.AV_PIX_FMT_CUDA)
//                        {
//                            if (ffmpeg.av_hwframe_transfer_data(pSwFrame, pFrame, 0) < 0)
//                            {
//                                Console.WriteLine("❌ Chuyển từ HW frame thất bại");
//                                continue;
//                            }
//                        }
//                        else
//                        {
//                            ffmpeg.av_frame_ref(pSwFrame, pFrame);
//                        }

//                        int scaleLines = ffmpeg.sws_scale(
//                            pSwsContext,
//                            pSwFrame->data,
//                            pSwFrame->linesize,
//                            0,
//                            pCodecContext->height,
//                            pFrameRGB->data,
//                            pFrameRGB->linesize);

//                        if (scaleLines > 0)
//                        {
//                            Bitmap bmp = new Bitmap(
//                                pCodecContext->width,
//                                pCodecContext->height,
//                                pFrameRGB->linesize[0],
//                                PixelFormat.Format24bppRgb,
//                                (IntPtr)pFrameRGB->data[0]);

//                            displayBox.Invoke(new Action(() =>
//                            {
//                                displayBox.Image?.Dispose();
//                                displayBox.Image = (Bitmap)bmp.Clone();
//                            }));

//                            bmp.Dispose();

//                            ffmpeg.av_frame_unref(pSwFrame);
//                            ffmpeg.av_frame_unref(pFrame);
//                        }
//                    }
//                }
//                ffmpeg.av_packet_unref(pPacket);
//            }

//            ffmpeg.av_frame_free(&pFrame);
//            ffmpeg.av_frame_free(&pSwFrame);
//            ffmpeg.av_frame_free(&pFrameRGB);
//            ffmpeg.av_packet_free(&pPacket);
//            ffmpeg.avcodec_free_context(&pCodecContext);
//            ffmpeg.avformat_close_input(&pFormatContext);
//            ffmpeg.av_free(rgbBuffer);
//            ffmpeg.sws_freeContext(pSwsContext);
//        }
//    }

//}
