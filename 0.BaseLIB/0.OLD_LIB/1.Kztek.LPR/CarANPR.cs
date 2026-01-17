using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
namespace Kztek.LPR
{
	public class CarANPR : ILPRProcessing
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
		public CarANPR()
		{
			this.simpleLPR3 = new SimpleLPRX64_v3();
			this.simpleLPR3.IsOnlyForCar = true;
			this.simpleLPR3.UsingGPU = this.usingGPU;
			this.simpleLPR3.CudaDeviceID = this.cudaDeviceID;
			this.simpleLPR3.LPRCompleted += new LPRCompletedEventHandler(this.simpleLPRProcessing_LPRCompleted);
			this.simpleLPR3.ErrorEvent += new ErrorEventHandler(this.simpleLPR3_ErrorEvent);

			this.simpleLPR2 = new SimpleLPRX64_v2();
			this.simpleLPR2.IsOnlyForCar = true;
			this.simpleLPR2.UsingGPU = this.usingGPU;
			this.simpleLPR2.CudaDeviceID = this.cudaDeviceID;
			this.simpleLPR2.ErrorEvent += new ErrorEventHandler(this.simpleLPR3_ErrorEvent);
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
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}

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
				this.HieuChinhBienSoOto(ref lprObject);
				if (lprObject.plateNumberSub.Count > 0)
				{
					for (int i = 0; i < lprObject.plateNumberSub.Count; i++)
					{
						string value = lprObject.plateNumberSub[i];
						this.HieuChinhBienSoOto(ref value);
						lprObject.plateNumberSub[i] = value;
					}
				}
				this.LPRCompleted(sender, ref lprObject);
			}
		}
		private void simpleLPR3_ErrorEvent(object sender, ErrorEventArgs e)
		{
			if (this.NewError != null)
			{
				this.NewError(sender, e);
			}
		}
		private void simpleLPR2_ErrorEvent(object sender, ErrorEventArgs e)
		{
			if (this.NewError != null)
			{
				this.NewError(sender, e);
			}
		}
		~CarANPR()
		{
			this.simpleLPR3.LPRCompleted -= new LPRCompletedEventHandler(this.simpleLPRProcessing_LPRCompleted);
			this.simpleLPR3.ErrorEvent -= new ErrorEventHandler(this.simpleLPR3_ErrorEvent);
			this.simpleLPR2.LPRCompleted -= new LPRCompletedEventHandler(this.simpleLPRProcessing_LPRCompleted);
			this.simpleLPR2.ErrorEvent -= new ErrorEventHandler(this.simpleLPR3_ErrorEvent);
			this.CloseLPREngine();
			this.simpleLPR3 = null;
			this.simpleLPR2 = null;
		}
		public bool Analyze(ref LPRObject lprObject)
		{
			if (this.simpleLPR3.Analyze(ref lprObject))
			{
				string plateNumberTemp = "";
				System.Drawing.Bitmap plateImageTemp = null;
				if ((string.IsNullOrEmpty(lprObject.plateNumber) || lprObject.plateNumber.Contains("F") || lprObject.plateNumber.Length <= 4 || (!lprObject.plateNumber.Contains("-") && lprObject.plateNumber.Contains(" "))) && this.enableSimpleLPR2 && this.simpleLPR2 != null)
				{
					if (lprObject.plateNumberSub.Count > 0)
						plateNumberTemp = lprObject.plateNumberSub[0];
					if (lprObject.plateImageSub.Count > 0)
						plateImageTemp = lprObject.plateImageSub[0];


					Bitmap bitmapResize = null;
					int width = lprObject.vehicleImage.Width;
					int height = lprObject.vehicleImage.Height;
					bitmapResize = OpenCVTools.CropImage(lprObject.vehicleImage, new Rectangle(new Point(20, 20), new Size(width - 40, height - 40)));
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
								this.HieuChinhBienSoOto(ref lPRObject_Resize);
							}
							lprObject.preProcessingImage = bitmapResize;
							lprObject.plateNumber = lPRObject_Resize.plateNumber;
							lprObject.plateImage = lPRObject_Resize.plateImage;
							lprObject.plateLocation = lPRObject_Resize.plateLocation;
							lprObject.confidence = lPRObject_Resize.confidence;
						}
					}
				}
				if (lprObject.plateNumber == "")
				{
					if (lprObject.plateNumberSub.Count > 0)
					{
						lprObject.plateNumber = plateNumberTemp;
						lprObject.plateImage = plateImageTemp;
						this.HieuChinhBienSoOto(ref lprObject);
					}
				}

				if (lprObject.plateNumberSub.Count > 0)
				{
					for (int i = 0; i < lprObject.plateNumberSub.Count; i++)
					{
						string value = lprObject.plateNumberSub[i];
						this.HieuChinhBienSoOto(ref value);
						lprObject.plateNumberSub[i] = value;
					}
				}
				return true;
			}
			return false;
		}

		public bool Analyze2(ref LPRObject lprObject)
		{
			{
				bool flag = false;
				while (true)
				{
					if (flag) break;
					flag = true;
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
							this.HieuChinhBienSoOto(ref lPRObject);
						}
						lprObject.preProcessingImage = bitmap;
						lprObject.plateNumber = lPRObject.plateNumber;
						lprObject.plateImage = lPRObject.plateImage;
						lprObject.plateLocation = lPRObject.plateLocation;
						lprObject.confidence = lPRObject.confidence;
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
								this.HieuChinhBienSoOto(ref lPRObject_Resize);
							}
							lprObject.preProcessingImage = bitmapResize;
							lprObject.plateNumber = lPRObject_Resize.plateNumber;
							lprObject.plateImage = lPRObject_Resize.plateImage;
							lprObject.plateLocation = lPRObject_Resize.plateLocation;
							lprObject.confidence = lPRObject.confidence;
						}
						#endregion
					}
				}


				if (lprObject.plateImageSub != null && lprObject.plateNumberSub.Count > 0)
				{
					for (int i = 0; i < lprObject.plateNumberSub.Count; i++)
					{
						string value = lprObject.plateNumberSub[i];
						this.HieuChinhBienSoOto(ref value);
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
		private void HieuChinhBienSoOto(ref LPRObject lprObject)
		{
			if (lprObject != null)
			{
				string plateNumber = lprObject.plateNumber;
				this.HieuChinhBienSoOto(ref plateNumber);
				if (!string.IsNullOrEmpty(plateNumber))
				{
					// Check format Car PlateNumber
					if (!plateNumber.Contains("-"))
					{
						// Khong co dau gach ngang
						if (plateNumber.Contains(" "))
						{
							if (plateNumber.IndexOf(' ') == 2)
							{
								if (plateNumber[1] >= 'A' && plateNumber[1] <= 'Z')
								{
									if (plateNumber[0] >= '0' && plateNumber[0] <= 'Z')
										plateNumber = "5" + plateNumber.Substring(0, plateNumber.IndexOf(' ')) + "-" + plateNumber.Substring(plateNumber.IndexOf(' ') + 1);
								}
								else
									plateNumber = plateNumber.Substring(0, plateNumber.IndexOf(' ')) + "H-" + plateNumber.Substring(plateNumber.IndexOf(' ') + 1);
							}
							else if (plateNumber.IndexOf(' ') == 3)
								plateNumber = plateNumber.Substring(0, plateNumber.IndexOf(' ')) + "-" + plateNumber.Substring(plateNumber.IndexOf(' ') + 1);
							else
							{
								if (plateNumber.IndexOf(' ') == 1 && plateNumber.LastIndexOf(' ') == 3)
								{
									plateNumber = plateNumber.Substring(0, 1) + "0" + plateNumber.Substring(2, 1) + "-" + plateNumber.Substring(plateNumber.LastIndexOf(' ') + 1);
								}
								else
								{
									int firstIndex = plateNumber.IndexOf(' ');
									int lastIndex = plateNumber.LastIndexOf(' ');
									if (firstIndex == lastIndex)
									{
										plateNumber = plateNumber.Replace(" ", "0");
										this.HieuChinhBienSoOto(ref plateNumber);
									}
									else
										plateNumber = plateNumber.Replace(" ", "0");
									if (plateNumber.Length >= 4 && Char.IsLetter(plateNumber[2]) && !Char.IsLetter(plateNumber[3]))
									{
										plateNumber = plateNumber.Substring(0, 3) + "-" + plateNumber.Substring(3);
									}
								}

							}
							plateNumber = plateNumber.Replace(" ", "");
						}
						else
						{
							Int64 num = 0;
							if(Int64.TryParse(plateNumber, out num))
							{
								// Do nothing
							}
							else
								plateNumber = "";
						}
					}
					else
					{
						// Bien so co 1 gach ngang
						if (plateNumber.IndexOf('-') == plateNumber.LastIndexOf('-'))
						{
							// Sau dau gach ngang it hon 4 ky tu
							if (plateNumber.Length > plateNumber.LastIndexOf('-') && plateNumber.Substring(plateNumber.LastIndexOf('-') + 1).Length < 4)
								plateNumber = "";
							else
							{
								int suffix = 0;
								int.TryParse(plateNumber.Substring(0, plateNumber.IndexOf('-') - 1), out suffix);
								if (suffix < 10) plateNumber = "";
							}
						}
						else
						{
							// Co 2 dau gach ngang
							string[] array = plateNumber.Split('-');
							if (array.Length == 3)
							{
								// Thanh phan o giua phai la cung so hoac cung chu
								bool isOK = true;
								for (int i = 0; i < array[1].Length - 1; i++)
								{
									if (Char.IsLetter(array[1][i]) != Char.IsLetter(array[1][i + 1]))
									{
										isOK = false;
										break;
									}
								}
								if (!isOK)
								{
									if (array[1].Length == 2 && Char.IsLetter(array[1][0]) && !Char.IsLetter(array[1][1]))
									{
										if (array[2].Length <= 4)
											plateNumber = array[0] + array[1][0] + "-" + array[1][1] + array[2];
										else
											plateNumber = array[0] + array[1][0] + "-" + array[1][1] + array[2].Substring(0, 4);
									}
									else
									{
										if (array[2].Length <= 4)
											plateNumber = array[2];
										else
											plateNumber = array[2].Substring(0, 4);
									}
								}
							}
							else if (array.Length == 4)
							{
								bool isOK = true;
								for (int i = 0; i < array[1].Length - 1; i++)
								{
									if (Char.IsLetter(array[1][i]) != Char.IsLetter(array[1][i + 1]))
									{
										isOK = false;
										break;
									}
								}
								if (!isOK)
									plateNumber = "";
								else
								{
									for (int i = 0; i < array[2].Length - 1; i++)
									{
										if (Char.IsLetter(array[2][i]) != Char.IsLetter(array[2][i + 1]))
										{
											isOK = false;
											break;
										}
									}
								}
								if (!isOK) plateNumber = "";
							}
							else
								plateNumber = "";
						}
					}
				}
				if (plateNumber == "" && lprObject.plateNumberSub.Count > 0)
				{
					plateNumber = lprObject.plateNumberSub[0];
					if (lprObject.plateImageSub.Count > 0)
						lprObject.plateImage = lprObject.plateImageSub[0];
					#region Hiệu chỉnh lần nữa
					// Check format Car PlateNumber
					if (!plateNumber.Contains("-"))
					{
						// Khong co dau gach ngang
						if (plateNumber.Contains(" "))
						{
							if (plateNumber.IndexOf(' ') == 2)
								plateNumber = plateNumber.Substring(0, plateNumber.IndexOf(' ')) + "H-" + plateNumber.Substring(plateNumber.IndexOf(' ') + 1);
							else if (plateNumber.IndexOf(' ') == 3)
								plateNumber = plateNumber.Substring(0, plateNumber.IndexOf(' ')) + "-" + plateNumber.Substring(plateNumber.IndexOf(' ') + 1);
							else
							{
								if (plateNumber.IndexOf(' ') == 1 && plateNumber.LastIndexOf(' ') == 3)
								{
									plateNumber = plateNumber.Substring(0, 1) + "0" + plateNumber.Substring(2, 1) + "-" + plateNumber.Substring(plateNumber.LastIndexOf(' ') + 1);
								}
								else
									plateNumber = plateNumber.Substring(plateNumber.LastIndexOf(' ') + 1);

							}
						}
						else
							plateNumber = "";
					}
					else
					{
						// Bien so co 1 gach ngang
						if (plateNumber.IndexOf('-') == plateNumber.LastIndexOf('-'))
						{
							// Sau dau gach ngang it hon 4 ky tu
							if (plateNumber.Length > plateNumber.LastIndexOf('-') && plateNumber.Substring(plateNumber.LastIndexOf('-') + 1).Length < 4)
								plateNumber = "";
							else
							{
								int suffix = 0;
								int.TryParse(plateNumber.Substring(0, plateNumber.IndexOf('-') - 1), out suffix);
								if (suffix < 10) plateNumber = "";
							}
						}
						else
						{
							// Co 2 dau gach ngang
							string[] array = plateNumber.Split('-');
							if (array.Length == 3)
							{
								// Thanh phan o giua phai la cung so hoac cung chu
								bool isOK = true;
								for (int i = 0; i < array[1].Length - 1; i++)
								{
									if (Char.IsLetter(array[1][i]) != Char.IsLetter(array[1][i + 1]))
									{
										isOK = false;
										break;
									}
								}
								if (!isOK)
								{
									if (array[1].Length == 2 && Char.IsLetter(array[1][0]) && !Char.IsLetter(array[1][1]))
									{
										if (array[2].Length <= 4)
											plateNumber = array[0] + array[1][0] + "-" + array[1][1] + array[2];
										else
											plateNumber = array[0] + array[1][0] + "-" + array[1][1] + array[2].Substring(0, 4);
									}
									else
									{
										if (array[2].Length <= 4)
											plateNumber = array[2];
										else
											plateNumber = array[2].Substring(0, 4);
									}
								}
							}
							else if (array.Length == 4)
							{
								bool isOK = true;
								for (int i = 0; i < array[1].Length - 1; i++)
								{
									if (Char.IsLetter(array[1][i]) != Char.IsLetter(array[1][i + 1]))
									{
										isOK = false;
										break;
									}
								}
								if (!isOK)
									plateNumber = "";
								else
								{
									for (int i = 0; i < array[2].Length - 1; i++)
									{
										if (Char.IsLetter(array[2][i]) != Char.IsLetter(array[2][i + 1]))
										{
											isOK = false;
											break;
										}
									}
								}
								if (!isOK) plateNumber = "";
							}
							else
								plateNumber = "";
						}
					}
					#endregion
				}
				lprObject.plateNumber = plateNumber;
			}
		}
		private void HieuChinhBienSoOto(ref string plateNumber)
		{
			if (!string.IsNullOrEmpty(plateNumber) && (plateNumber.Length != 8 || plateNumber.IndexOf("-") != 2 || plateNumber.LastIndexOf("-") != 5) && (plateNumber.Length != 6 || plateNumber.LastIndexOf("-") != 2))
			{
				plateNumber = ANPRTools.FixCarPlateNumber(plateNumber);
			}
		}
	}
}
