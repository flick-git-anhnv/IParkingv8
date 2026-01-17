using System;
using System.Collections.Generic;
using System.Drawing;
namespace Kztek.LPR
{
	public class LPRObject
	{
		public string cmd = "";
		public DateTime eventDateTime = DateTime.Now;
		public string clientIP = "";
		public object tag2;
		public string id = "";
		public string fileName = "";
		public Bitmap vehicleImage;
		public string plateNumber = "";
		public Bitmap plateImage;
		public Rectangle plateLocation = Rectangle.Empty;
		public int recognizeTime;
		public float confidence;
		public bool isBlackOnWhite = true;
		public object tag;
		public bool enableRawFormat;
		public bool enableMultiplePlateNumber;
        public bool isMotorPlateNumber = false;
		public List<string> plateNumberSub = new List<string>();
		public List<Bitmap> plateImageSub = new List<Bitmap>();
		public List<Rectangle> plateLocationSub = new List<Rectangle>();
		public List<float> confidenceSub = new List<float>();
		public List<bool> isBlackOnWhiteSub = new List<bool>();
        public List<bool> isMotorPlateNumberSub = new List<bool>();
        public Bitmap preProcessingImage;
		public LPRObject()
		{
		}
		public LPRObject(Bitmap vehicleImage)
		{
			this.vehicleImage = vehicleImage;
		}
		public LPRObject(string fileName)
		{
			this.fileName = fileName;
		}
		public LPRObject(Bitmap vehicleImage, bool isMultiplePlateNumber)
		{
			this.vehicleImage = vehicleImage;
			this.enableMultiplePlateNumber = isMultiplePlateNumber;
		}
		public LPRObject(string fileName, bool isMultiplePlateNumber)
		{
			this.fileName = fileName;
			this.enableMultiplePlateNumber = isMultiplePlateNumber;
		}
		public LPRObject(Bitmap vehicleImage, bool isMultiplePlateNumber, bool enableRawFormat)
		{
			this.vehicleImage = vehicleImage;
			this.enableMultiplePlateNumber = isMultiplePlateNumber;
			this.enableRawFormat = enableRawFormat;
		}
		public LPRObject(string fileName, bool isMultiplePlateNumber, bool enableRawFormat)
		{
			this.fileName = fileName;
			this.enableMultiplePlateNumber = isMultiplePlateNumber;
			this.enableRawFormat = enableRawFormat;
		}
	}
}
