using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kztek.LPR
{
	public class MotorANPR : ILPRProcessing
	{
		private SimpleLPRX64_v3 simpleLPR3;
		private SimpleLPRX64_v2 simpleLPR2;
		private string lprEngineProductKey = "";
		private bool usingGPU = true;
		private int cudaDeviceID;
		private int cIProcessor = 16;
		private bool enableSimpleLPR2;
		private List<float> contrastSensitivityFactor;
		public event LPRCompletedEventHandler LPRCompleted;
		public event ErrorEventHandler NewError;

		public string LPREngineProductKey
		{
			get
			{
				return this.lprEngineProductKey;
			}
			set
			{
				this.lprEngineProductKey = value;
			}
		}
		public bool UsingGPU
		{
			get
			{
				return this.simpleLPR3 != null && this.simpleLPR3.UsingGPU;
			}
			set
			{
				this.usingGPU = value;
			}
		}
		public int CudaDeviceID
		{
			get
			{
				return this.cudaDeviceID;
			}
			set
			{
				this.cudaDeviceID = value;
			}
		}
		public int NumOfProcessor
		{
			get
			{
				if (this.simpleLPR3 != null)
				{
					return this.simpleLPR3.NumOfProcessor;
				}
				return 0;
			}
			set
			{
				this.cIProcessor = value;
			}
		}
		public int NumOfProcessorPending
		{
			get
			{
				if (this.simpleLPR3 != null)
				{
					return this.simpleLPR3.NumOfProcessorPending;
				}
				return 0;
			}
		}
		public bool IsBusy
		{
			get
			{
				return this.NumOfProcessorPending >= this.NumOfProcessor;
			}
		}
		public bool EnableLPREngine2
		{
			set
			{
				this.enableSimpleLPR2 = value;
			}
		}
		public List<float> ContrastSensitivityFactor
		{
			set
			{
				this.contrastSensitivityFactor = value;
				if (this.simpleLPR3 != null)
				{
					this.simpleLPR3.ContrastSensitivityFactor = value;
				}
			}
		}
		public MotorANPR()
		{
			this.simpleLPR3 = new SimpleLPRX64_v3();
			this.simpleLPR3.IsOnlyForCar = false;
			this.simpleLPR3.UsingGPU = this.usingGPU;
			this.simpleLPR3.CudaDeviceID = this.cudaDeviceID;
			this.simpleLPR3.LPRCompleted += new LPRCompletedEventHandler(this.simpleLPRProcessing_LPRCompleted);
			this.simpleLPR3.ErrorEvent += new ErrorEventHandler(this.simpleLPRProcessing_ErrorEvent);

			this.simpleLPR2 = new SimpleLPRX64_v2();
			this.simpleLPR2.IsOnlyForCar = false;
			this.simpleLPR2.UsingGPU = this.usingGPU;
			this.simpleLPR2.CudaDeviceID = this.cudaDeviceID;
			//this.simpleLPR2.LPRCompleted += new LPRCompletedEventHandler(this.simpleLPRProcessing_LPRCompleted);
			this.simpleLPR2.ErrorEvent += new ErrorEventHandler(this.simpleLPRProcessing_ErrorEvent);
		}
		public void CreateLPREngine()
		{
			try
			{
				this.simpleLPR2.UsingGPU = this.usingGPU;
				this.simpleLPR2.NumOfProcessor = this.cIProcessor;
				this.simpleLPR2.LPREngineProductKey = this.lprEngineProductKey;
				this.simpleLPR2.ContrastSensitivityFactor = this.contrastSensitivityFactor;
				this.simpleLPR2.CreateLPREngine();
			}
			catch
			{ }

			this.simpleLPR3.UsingGPU = this.usingGPU;
			this.simpleLPR3.NumOfProcessor = this.cIProcessor;
			this.simpleLPR3.LPREngineProductKey = this.lprEngineProductKey;
			this.simpleLPR3.ContrastSensitivityFactor = this.contrastSensitivityFactor;
			this.simpleLPR3.CreateLPREngine();
			this.usingGPU = this.simpleLPR3.UsingGPU;
			this.cIProcessor = this.simpleLPR3.NumOfProcessor;


		}
		public void CloseLPREngine()
		{
			this.simpleLPR3.CloseLPREngine();
			this.simpleLPR2.CloseLPREngine();
		}
		private void simpleLPRProcessing_LPRCompleted(object sender, ref LPRObject lprObject)
		{
			if (this.LPRCompleted != null)
			{
				this.HieuChinhBienSoXeMay(ref lprObject);
				if (lprObject.plateNumberSub.Count > 0)
				{
					for (int i = 0; i < lprObject.plateNumberSub.Count; i++)
					{
						string value = lprObject.plateNumberSub[i];
						this.HieuChinhBienSoXeMay(ref value);
						lprObject.plateNumberSub[i] = value;
					}
				}
				this.LPRCompleted(sender, ref lprObject);
			}
		}
		private void simpleLPRProcessing_ErrorEvent(object sender, ErrorEventArgs e)
		{
			if (this.NewError != null)
			{
				this.NewError(sender, e);
			}
		}
		~MotorANPR()
		{
			this.simpleLPR2.LPRCompleted -= new LPRCompletedEventHandler(this.simpleLPRProcessing_LPRCompleted);
			this.simpleLPR2.ErrorEvent -= new ErrorEventHandler(this.simpleLPRProcessing_ErrorEvent);
			this.simpleLPR3.LPRCompleted -= new LPRCompletedEventHandler(this.simpleLPRProcessing_LPRCompleted);
			this.simpleLPR3.ErrorEvent -= new ErrorEventHandler(this.simpleLPRProcessing_ErrorEvent);
			this.CloseLPREngine();
			this.simpleLPR2 = null;
			this.simpleLPR3 = null;
		}

		public bool Analyze(ref LPRObject lprObject)
		{
			try
			{
				LPRObject lprObject_1 = new LPRObject(lprObject.vehicleImage);
				if (this.simpleLPR3.Analyze(ref lprObject_1))
				{
					if (lprObject_1 != null && lprObject_1.plateNumber != "")
					{
						this.HieuChinhBienSoXeMay(ref lprObject_1);
					}
					if (string.IsNullOrEmpty(lprObject_1.plateNumber))
					{
						Bitmap bitmapResize = null;
						int width = lprObject_1.vehicleImage.Width;
						int height = lprObject_1.vehicleImage.Height;
						bitmapResize = OpenCVTools.CropImage(lprObject_1.vehicleImage, new Rectangle(new Point(20, 20), new Size(width - 40, height - 40)));
						if (bitmapResize != null)
						{
							LPRObject lPRObject_Resize = new LPRObject(bitmapResize);
							if (this.simpleLPR3.Analyze(ref lPRObject_Resize))
							{
								if (string.IsNullOrEmpty(lPRObject_Resize.plateNumber) && this.enableSimpleLPR2 && this.simpleLPR2 != null)
								{
									this.simpleLPR2.Analyze(ref lPRObject_Resize);
								}
								if (lPRObject_Resize != null && lPRObject_Resize.plateNumber != "")
								{
									this.HieuChinhBienSoXeMay(ref lPRObject_Resize);
								}
								lprObject_1.preProcessingImage = bitmapResize;
								lprObject_1.plateNumber = lPRObject_Resize.plateNumber;
								lprObject_1.plateImage = lPRObject_Resize.plateImage;
								lprObject_1.plateLocation = lPRObject_Resize.plateLocation;
								lprObject_1.confidence = lPRObject_Resize.confidence;
							}
						}
					}
				}

				#region Convert Color and recog again
				Bitmap bitmap = null;
				try
				{
					using (Bitmap newBmp = new Bitmap(lprObject.vehicleImage))
					{
						if (lprObject != null && newBmp != null)
						{
							bitmap = OpenCVTools.GetBinaryImage(newBmp);
						}
						else
						{
							if (!string.IsNullOrEmpty(lprObject.fileName))
							{
								bitmap = OpenCVTools.GetBinaryImageFromFile(lprObject.fileName);
							}
						}
					}
				}
				catch
				{
				}
				if (bitmap != null)
				{
					LPRObject lprObject_ConvertColor = new LPRObject(bitmap);
					if (this.simpleLPR3.Analyze(ref lprObject_ConvertColor))
					{
						if (string.IsNullOrEmpty(lprObject_ConvertColor.plateNumber) && this.enableSimpleLPR2 && this.simpleLPR2 != null)
						{
							this.simpleLPR2.Analyze(ref lprObject_ConvertColor);
						}
						if (lprObject_ConvertColor != null && lprObject_ConvertColor.plateNumber != "")
						{
							this.HieuChinhBienSoXeMay(ref lprObject_ConvertColor);
						}
						lprObject.preProcessingImage = bitmap;
						lprObject.plateNumber = lprObject_ConvertColor.plateNumber;
						lprObject.plateImage = lprObject_ConvertColor.plateImage;
						lprObject.plateLocation = lprObject_ConvertColor.plateLocation;
						lprObject.confidence = lprObject_ConvertColor.confidence;
					}
				}
				#endregion

				string s1 = lprObject_1 != null ? lprObject_1.plateNumber : ""; s1 = s1.Replace(" ", "").Replace("-", "").Replace(".", "");
				string s2 = lprObject != null ? lprObject.plateNumber : ""; s2 = s2.Replace(" ", "").Replace("-", "").Replace(".", "");
				if (s1.Length > s2.Length)
				{
					lprObject.preProcessingImage = lprObject_1.vehicleImage;
					lprObject.plateNumber = lprObject_1.plateNumber;
					lprObject.plateImage = lprObject_1.plateImage;
					lprObject.plateLocation = lprObject_1.plateLocation;
					lprObject.confidence = lprObject_1.confidence;
				}
				Console.WriteLine("Finish Kz.LPR");
				return true;
			}
			catch
			{ }
			return false;
		}

		private async Task<LPRObject> Analyze_Sub1(LPRObject lprObject)
		{
			LPRObject lprObject_1 = null; ;
			await Task.Run(() =>
			{
				lprObject_1 = new LPRObject(lprObject.vehicleImage);
				if (this.simpleLPR3.Analyze(ref lprObject_1))
				{
					if (lprObject_1 != null && lprObject_1.plateNumber != "")
					{
						this.HieuChinhBienSoXeMay(ref lprObject_1);
					}
					if (string.IsNullOrEmpty(lprObject_1.plateNumber))
					{
						Bitmap bitmapResize = null;
						int width = lprObject_1.vehicleImage.Width;
						int height = lprObject_1.vehicleImage.Height;
						bitmapResize = OpenCVTools.CropImage(lprObject_1.vehicleImage, new Rectangle(new Point(20, 20), new Size(width - 40, height - 40)));
						if (bitmapResize != null)
						{
							LPRObject lPRObject_Resize = new LPRObject(bitmapResize);
							if (this.simpleLPR3.Analyze(ref lPRObject_Resize))
							{
								if (string.IsNullOrEmpty(lPRObject_Resize.plateNumber) && this.enableSimpleLPR2 && this.simpleLPR2 != null)
								{
									this.simpleLPR2.Analyze(ref lPRObject_Resize);
								}
								if (lPRObject_Resize != null && lPRObject_Resize.plateNumber != "")
								{
									this.HieuChinhBienSoXeMay(ref lPRObject_Resize);
								}
								lprObject_1.preProcessingImage = bitmapResize;
								lprObject_1.plateNumber = lPRObject_Resize.plateNumber;
								lprObject_1.plateImage = lPRObject_Resize.plateImage;
								lprObject_1.plateLocation = lPRObject_Resize.plateLocation;
								lprObject_1.confidence = lPRObject_Resize.confidence;
							}
						}
					}
				}
			});
			return lprObject_1;
		}

		private async Task<LPRObject> Analyze_Sub2(LPRObject lprObject)
		{
			LPRObject lprObject_1 = null; ;
			await Task.Run(() =>
			{
				lprObject_1 = new LPRObject(lprObject.vehicleImage);

				#region Convert Color and recog again
				Bitmap bitmap = null;
				try
				{
					if (lprObject_1 != null && lprObject_1.vehicleImage != null)
					{
						bitmap = OpenCVTools.GetBinaryImage(lprObject_1.vehicleImage);
					}
				}
				catch
				{
				}
				if (bitmap != null)
				{
					LPRObject lprObject_ConvertColor = new LPRObject(bitmap);
					if (this.simpleLPR3.Analyze(ref lprObject_ConvertColor))
					{
						if (string.IsNullOrEmpty(lprObject_ConvertColor.plateNumber) && this.enableSimpleLPR2 && this.simpleLPR2 != null)
						{
							this.simpleLPR2.Analyze(ref lprObject_ConvertColor);
						}
						if (lprObject_ConvertColor != null && lprObject_ConvertColor.plateNumber != "")
						{
							this.HieuChinhBienSoXeMay(ref lprObject_ConvertColor);
						}
						lprObject_1.preProcessingImage = bitmap;
						lprObject_1.plateNumber = lprObject_ConvertColor.plateNumber;
						lprObject_1.plateImage = lprObject_ConvertColor.plateImage;
						lprObject_1.plateLocation = lprObject_ConvertColor.plateLocation;
						lprObject_1.confidence = lprObject_ConvertColor.confidence;
					}
				}
				#endregion
			});
			return lprObject_1;
		}

		public async Task<LPRObject> Analyze_Async(LPRObject lprObject)
		{
			LPRObject lPRObject_Result = new LPRObject();
			var task1 = Analyze_Sub1(lprObject);
			var task2 = Analyze_Sub2(lprObject);
			var result = await Task.WhenAll(task1, task2);
			if (result[0] != null && result[1] != null)
			{
				string s1 = result[0] != null ? result[0].plateNumber : ""; s1 = s1.Replace(" ", "").Replace("-", "").Replace(".", "");
				string s2 = result[1] != null ? result[1].plateNumber : ""; s2 = s2.Replace(" ", "").Replace("-", "").Replace(".", "");
				if (s1.Length > s2.Length)
				{
					lPRObject_Result.preProcessingImage = result[0].vehicleImage;
					lPRObject_Result.plateNumber = result[0].plateNumber;
					lPRObject_Result.plateImage = result[0].plateImage;
					lPRObject_Result.plateLocation = result[0].plateLocation;
					lPRObject_Result.confidence = result[0].confidence;
				}
				else
				{
					lPRObject_Result.preProcessingImage = result[1].vehicleImage;
					lPRObject_Result.plateNumber = result[1].plateNumber;
					lPRObject_Result.plateImage = result[1].plateImage;
					lPRObject_Result.plateLocation = result[1].plateLocation;
					lPRObject_Result.confidence = result[1].confidence;
				}
			}
			return lPRObject_Result;
		}

		public bool Analyze2(ref LPRObject lprObject)
		{
			//if (this.simpleLPR3.Analyze(ref lprObject))
			{
				bool flag = false;
				while (true)
				{
					if (flag) break;
					flag = true;

					//if (lprObject != null && lprObject.plateNumber != "")
					//{
					//	this.HieuChinhBienSoXeMay(ref lprObject);
					//}
					//if (lprObject != null && lprObject.plateNumber.Length >= 5 && lprObject.plateNumber != "11111")
					//{
					//	break;
					//}


					// Recog with lpr2
					//if (string.IsNullOrEmpty(lprObject.plateNumber) && this.enableSimpleLPR2 && this.simpleLPR2 != null)
					//{
					//	this.simpleLPR2.Analyze(ref lprObject);
					//}
					//if (lprObject != null && lprObject.plateNumber != "" && lprObject.plateNumber != "11111")
					//{
					//	this.HieuChinhBienSoXeMay(ref lprObject);
					//	break;
					//}

					#region Convert Color and recog again
					Bitmap bitmap = null;
					try
					{
						if (lprObject != null && lprObject.vehicleImage != null)
						{
							bitmap = OpenCVTools.GetBinaryImage(lprObject.vehicleImage);
						}
						else
						{
							if (!string.IsNullOrEmpty(lprObject.fileName))
							{
								bitmap = OpenCVTools.GetBinaryImageFromFile(lprObject.fileName);
							}
						}
					}
					catch
					{
					}
					if (bitmap == null)
					{
						break;
					}

					LPRObject lPRObject = new LPRObject(bitmap);
					if (this.simpleLPR3.Analyze(ref lPRObject))
					{
						if (string.IsNullOrEmpty(lPRObject.plateNumber) && this.enableSimpleLPR2 && this.simpleLPR2 != null)
						{
							this.simpleLPR2.Analyze(ref lPRObject);
						}
						if (lPRObject != null && lPRObject.plateNumber != "")
						{
							this.HieuChinhBienSoXeMay(ref lPRObject);
						}
						lprObject.preProcessingImage = bitmap;
						lprObject.plateNumber = lPRObject.plateNumber;
						lprObject.plateImage = lPRObject.plateImage;
						lprObject.plateLocation = lPRObject.plateLocation;
					}
					#endregion

					#region CropImage and recog Again
					if (string.IsNullOrEmpty(lprObject.plateNumber))
					{
						Bitmap bitmapResize = null;
						int width = lprObject.vehicleImage.Width;
						int height = lprObject.vehicleImage.Height;
						bitmapResize = OpenCVTools.CropImage(lprObject.vehicleImage, new Rectangle(new Point(20, 20), new Size(width - 40, height - 40)));
						if (bitmapResize == null)
							break;
						LPRObject lPRObject_Resize = new LPRObject(bitmapResize);
						if (this.simpleLPR3.Analyze(ref lPRObject_Resize))
						{
							if (string.IsNullOrEmpty(lPRObject_Resize.plateNumber) && this.enableSimpleLPR2 && this.simpleLPR2 != null)
							{
								this.simpleLPR2.Analyze(ref lPRObject_Resize);
							}
							if (lPRObject_Resize != null && lPRObject_Resize.plateNumber != "")
							{
								this.HieuChinhBienSoXeMay(ref lPRObject_Resize);
							}
							lprObject.preProcessingImage = bitmapResize;
							lprObject.plateNumber = lPRObject_Resize.plateNumber;
							lprObject.plateImage = lPRObject_Resize.plateImage;
							lprObject.plateLocation = lPRObject_Resize.plateLocation;
						}
						#endregion
					}
				}


				if (lprObject.plateImageSub != null && lprObject.plateNumberSub.Count > 0)
				{
					for (int i = 0; i < lprObject.plateNumberSub.Count; i++)
					{
						string value = lprObject.plateNumberSub[i];
						this.HieuChinhBienSoXeMay(ref value);
						lprObject.plateNumberSub[i] = value;
					}
				}



				return true;
			}
			return false;
		}

		public bool Analyze_Old(ref LPRObject lprObject)
		{
			if (this.simpleLPR3.Analyze(ref lprObject))
			{
				bool flag = false;
				while (true)
				{
					if (lprObject != null && lprObject.plateNumber != "")
					{
						this.HieuChinhBienSoXeMay(ref lprObject);
					}
					if (lprObject != null && lprObject.plateNumber.Length > 5 || flag)
					{
						break;
					}
					flag = true;

					// Recog with lpr2
					if (string.IsNullOrEmpty(lprObject.plateNumber) && this.enableSimpleLPR2 && this.simpleLPR2 != null)
					{
						this.simpleLPR2.Analyze(ref lprObject);
					}
					if (lprObject != null && lprObject.plateNumber != "")
					{
						this.HieuChinhBienSoXeMay(ref lprObject);
						break;
					}


					Bitmap bitmap = null;
					if (lprObject != null && lprObject.vehicleImage != null)
					{
						bitmap = OpenCVTools.GetBinaryImage(lprObject.vehicleImage);
					}
					else
					{
						if (!string.IsNullOrEmpty(lprObject.fileName))
						{
							bitmap = OpenCVTools.GetBinaryImageFromFile(lprObject.fileName);
						}
					}
					if (bitmap == null)
					{
						break;
					}

					LPRObject lPRObject = new LPRObject(bitmap);
					if (this.simpleLPR3.Analyze(ref lPRObject))
					{
						if (string.IsNullOrEmpty(lprObject.plateNumber) && this.enableSimpleLPR2 && this.simpleLPR2 != null)
						{
							this.simpleLPR2.Analyze(ref lPRObject);
						}
						if (lprObject != null && lprObject.plateNumber != "")
						{
							this.HieuChinhBienSoXeMay(ref lPRObject);
						}
						lprObject.preProcessingImage = bitmap;
						lprObject.plateNumber = lPRObject.plateNumber;
						lprObject.plateImage = lPRObject.plateImage;
						lprObject.plateLocation = lPRObject.plateLocation;
					}
				}


				if (lprObject.plateImageSub != null && lprObject.plateNumberSub.Count > 0)
				{
					for (int i = 0; i < lprObject.plateNumberSub.Count; i++)
					{
						string value = lprObject.plateNumberSub[i];
						this.HieuChinhBienSoXeMay(ref value);
						lprObject.plateNumberSub[i] = value;
					}
				}



				return true;
			}
			return false;
		}
		public void AnalyzeAsync(ref LPRObject lprObject)
		{
			this.simpleLPR3.AnalyzeAsync(ref lprObject);
		}
		private void HieuChinhBienSoXeMay(ref LPRObject lprObject)
		{
			if (lprObject != null)
			{
				string plateNumber = lprObject.plateNumber;
				this.HieuChinhBienSoXeMay(ref plateNumber);
				lprObject.plateNumber = plateNumber;
			}
		}
		private void HieuChinhBienSoXeMay(ref string plateNumber)
		{
			return;
			if (!string.IsNullOrEmpty(plateNumber))
			{
				if ((plateNumber.Length == 8 && plateNumber.IndexOf("-") == 2 && plateNumber.LastIndexOf("-") == 5) || (plateNumber.Length == 6 && plateNumber.LastIndexOf("-") == 2))
				{
					plateNumber = "";
				}
				else
				{
					plateNumber = ANPRTools.FixMotorPlateNumber(plateNumber);
				}
				if (plateNumber.Length >= 8 && plateNumber.LastIndexOf("-") == 3)
				{
					plateNumber = plateNumber.Substring(4);
				}
			}
		}
	}
}
