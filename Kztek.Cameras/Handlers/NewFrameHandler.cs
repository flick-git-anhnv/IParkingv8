using System;
using System.Drawing;
namespace Kztek.Cameras
{
    public delegate void NewFrameHandler(object sender, ref System.Drawing.Bitmap image);
}