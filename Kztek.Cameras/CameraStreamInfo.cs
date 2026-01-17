using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kztek.Cameras
{
    public class CameraStreamInfo
    {
        public string Source { get; set; }

        public int HttpPort { get; set; }

        public int ServerPort { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public int DesiredFrameRate { get; set; }

        public Size DesiredFrameSize { get; set; }

        public int FramesReceived { get; set; }

        public long BytesReceived { get; set; }

        public bool IsRunning { get; }

        //public void Start();
        //public void SignalToStop();
        //public void WaitForStop();
        //public void Stop();
    }
}
