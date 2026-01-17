//using System;
//using System.Drawing;
//using System.Drawing.Imaging;
//using System.Runtime.InteropServices;
//using System.Threading;
//using System.Windows.Forms;
//using FFmpeg.AutoGen;

//namespace FFmpegDecoder_H265
//{
//    public unsafe class FFmpegDecoder
//    {
//        private Thread decodeThread;
//        private bool isRunning;
//        private PictureBox displayBox;

//        public FFmpegDecoder()
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
//            isRunning = false;
//            decodeThread?.Join();
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
//                    pCodec = ffmpeg.avcodec_find_decoder(pFormatContext->streams[i]->codecpar->codec_id);
//                    break;
//                }
//            }

//            if (pCodec == null)
//            {
//                MessageBox.Show("Không tìm thấy codec.");
//                return;
//            }

//            Console.WriteLine($"Codec ID: {pCodec->id}");
//            Console.WriteLine($"Codec name: {ffmpeg.avcodec_get_name(pCodec->id)}");


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
//                pCodecContext->pix_fmt,
//                pCodecContext->width,
//                pCodecContext->height,
//                AVPixelFormat.AV_PIX_FMT_BGR24,
//                ffmpeg.SWS_BICUBIC,
//                null, null, null);

//            AVPacket* pPacket = ffmpeg.av_packet_alloc();
//            AVFrame* pFrame = ffmpeg.av_frame_alloc();
//            AVFrame* pFrameRGB = ffmpeg.av_frame_alloc();

//            int bufferSize = ffmpeg.av_image_get_buffer_size(AVPixelFormat.AV_PIX_FMT_BGR24, pCodecContext->width, pCodecContext->height, 1);
//            byte* buffer = (byte*)ffmpeg.av_malloc((ulong)bufferSize);

            

//            int rgbBufferSize = ffmpeg.av_image_get_buffer_size(
//            AVPixelFormat.AV_PIX_FMT_BGR24,
//            pCodecContext->width,
//            pCodecContext->height,
//            1);

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
//                1
//            );

//            if (fillResult < 0)
//            {
//                Console.WriteLine("❌ av_image_fill_arrays failed");
//            }
//            else
//            {
//                for (uint i = 0; i < 4; i++)
//                {
//                    pFrameRGB->data[i] = dataPtr[i];
//                    pFrameRGB->linesize[i] = linesizePtr[i];
//                }

//                pFrameRGB->width = pCodecContext->width;
//                pFrameRGB->height = pCodecContext->height;
//                pFrameRGB->format = (int)AVPixelFormat.AV_PIX_FMT_BGR24;
//            }


//            while (isRunning && ffmpeg.av_read_frame(pFormatContext, pPacket) >= 0)
//            {
//                if (pPacket->stream_index == videoStreamIndex)
//                {
//                    ffmpeg.avcodec_send_packet(pCodecContext, pPacket);
//                    if (ffmpeg.avcodec_receive_frame(pCodecContext, pFrame) == 0)
//                    {
//                        int scaleLines =  ffmpeg.sws_scale(pSwsContext, pFrame->data, pFrame->linesize, 0, pCodecContext->height, pFrameRGB->data, pFrameRGB->linesize);

//                        Console.WriteLine($"Scaled lines: {scaleLines}");

//                        if (scaleLines > 0)
//                        {
//                            Bitmap bmp = new Bitmap(pCodecContext->width, pCodecContext->height, pFrameRGB->linesize[0], PixelFormat.Format24bppRgb, (IntPtr)pFrameRGB->data[0]);

//                            Console.WriteLine($"📸 Frame received: {bmp.Size.Width} - {bmp.Size.Height}");


//                            displayBox.Invoke(new Action(() =>
//                            {
//                                displayBox.Image?.Dispose();
//                                displayBox.Image = (Bitmap)bmp.Clone();
//                            }));
//                            bmp.Dispose();
//                            ffmpeg.av_frame_unref(pFrame);
//                        }
//                    }
//                }
//                ffmpeg.av_packet_unref(pPacket);
//            }

//            ffmpeg.av_frame_free(&pFrame);
//            ffmpeg.av_frame_free(&pFrameRGB);
//            ffmpeg.av_packet_free(&pPacket);
//            ffmpeg.avcodec_free_context(&pCodecContext);
//            ffmpeg.avformat_close_input(&pFormatContext);
//            ffmpeg.av_free(buffer);
//            ffmpeg.sws_freeContext(pSwsContext);
//        }
//    }
//}