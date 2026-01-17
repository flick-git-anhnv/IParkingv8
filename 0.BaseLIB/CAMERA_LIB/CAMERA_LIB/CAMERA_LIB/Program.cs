using Kztek.Cameras.Players.FFMPEG.Enums;
using System;
using System.Windows.Forms;

namespace CAMERA_LIB
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IntPtr _pHwDeviceContext = IntPtr.Zero;
            IntPtr _pFormatContext = IntPtr.Zero;

            //// --- Bước 1: Thiết lập và tạo Hardware Device Context ---
            //if (ANV.Cameras.PINVOKE.FFMPEG.av_hwdevice_ctx_create(ref _pHwDeviceContext, AVHWDeviceType.AV_HWDEVICE_TYPE_D3D11VA, null, IntPtr.Zero, 0) < 0)
            //    throw new ApplicationException("Không thể tạo D3D11VA device context.");

            //// --- Bước 2: Mở file và tìm stream ---
            //_pFormatContext = ANV.Cameras.PINVOKE.FFMPEG.avformat_alloc_context();
            //IntPtr options = IntPtr.Zero;
            //if (ANV.Cameras.PINVOKE.FFMPEG.avformat_open_input(ref _pFormatContext, "rtsp://admin:Kztek123456@192.168.21.195:554/1/1", IntPtr.Zero, ref options) != 0)
            //    throw new ApplicationException($"Không thể mở input: rtsp://admin:Kztek123456@192.168.21.195:554/1/1");

            //if (ANV.Cameras.PINVOKE.FFMPEG.avformat_find_stream_info(_pFormatContext, IntPtr.Zero) < 0)
            //    throw new ApplicationException("Không thể tìm thông tin stream.");

            Application.Run(new Form1());
        }
    }
}
