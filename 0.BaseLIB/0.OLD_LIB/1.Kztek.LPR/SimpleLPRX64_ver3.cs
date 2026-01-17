using SimpleLPR3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
namespace Kztek.LPR
{
	public class SimpleLPRX64_v3
	{
		private ISimpleLPR lpr;
		private Stack<IProcessor> processors;
		private int cPending;
		private string lprEngineProductKey = "";
		private string countryCode = "vietnam";
		private bool usingGPU = true;
		private int cudaDeviceID;
		private int cIProcessor = 16;
		private bool isOnlyForCar;
		private List<float> contrastSensitivityFactor;
		public event LPRCompletedEventHandler LPRCompleted;
		public event ErrorEventHandler ErrorEvent;

		public string LPREngineProductKey
		{
			get
			{
				return this.lprEngineProductKey;
			}
			set
			{
				this.lprEngineProductKey = value;
				if (this.lprEngineProductKey == "demo")
				{
					string path = Application.StartupPath + CryptorEngineLPR.Decrypt("LJybG + 8sLAnkd6fbJDzt9N7cfHQCkOWuzyNk3FsqREU =", true);
					if (System.IO.File.Exists(path))
					{
						this.lprEngineProductKey = CryptorEngineLPR.Decrypt(System.IO.File.ReadAllText(path), true);
					}
				}
			}
		}
		public string CountryCode
		{
			get
			{
				return this.countryCode;
			}
			set
			{
				this.countryCode = value;
			}
		}
		public bool UsingGPU
		{
			get
			{
				return this.usingGPU;
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
				return this.cIProcessor;
			}
			set
			{
				this.cIProcessor = value;
			}
		}
		public bool IsOnlyForCar
		{
			get
			{
				return this.isOnlyForCar;
			}
			set
			{
				this.isOnlyForCar = value;
			}
		}
		public int NumOfProcessorPending
		{
			get
			{
				return this.cPending;
			}
		}
		public List<float> ContrastSensitivityFactor
		{
			set
			{
				this.contrastSensitivityFactor = value;
			}
		}
		public void CreateLPREngine()
		{
			EngineSetupParms engineSetupParms;
			engineSetupParms.cudaDeviceId = -1; // Use CPU
			engineSetupParms.enableImageProcessingWithGPU = false;
			engineSetupParms.enableClassificationWithGPU = false;
			engineSetupParms.maxConcurrentImageProcessingOps = 0;  // Use the default value.  
			if (!this.usingGPU)
			{
				engineSetupParms.cudaDeviceId = -1;
			}
			else
			{
				engineSetupParms.cudaDeviceId = this.cudaDeviceID;
			}
			this.lpr = SimpleLPR.Setup(engineSetupParms);
			this.processors = new Stack<IProcessor>();
			this.usingGPU = (engineSetupParms.cudaDeviceId != -1);
			this.CreateProcessor(engineSetupParms.cudaDeviceId == -1);
		}
		private void CreateProcessor(bool bCPU)
		{
			try
			{
				byte[] key = ANPRTools.GetKey(this.lprEngineProductKey);
				if (key != null)
				{
					this.lpr.set_productKey(key);
				}
				this.SetCountryCode();
				if (this.cIProcessor <= 0)
				{
					int num = bCPU ? 8 : 2;
					this.cIProcessor = (Environment.ProcessorCount + num - 1) / num;
				}
				this.processors.Clear();
				for (int i = 0; i < this.cIProcessor; i++)
				{
					this.processors.Push(this.lpr.createProcessor());
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("CreateProcessor-v3: " + ex.ToString());
				if (this.ErrorEvent != null)
				{
					this.ErrorEvent(this, new ErrorEventArgs("Create LPR engine error: " + ex.ToString()));
				}
			}
			finally
			{
				GC.Collect();
			}
		}
		public void SetCountryCode()
		{
			for (uint num = 0u; num < this.lpr.numSupportedCountries; num += 1u)
			{
				string text = this.lpr.get_countryCode(num);
				if (this.isOnlyForCar)
				{
					this.lpr.set_countryWeight(text, text.ToLower().Contains(this.countryCode) ? 1f : 0f);
				}
				else
				{
					if (text == "numerical_4-digit")
					{
						this.lpr.set_countryWeight(text, 0.1f);
					}
					else
					{
						if (text == "numerical_5-digit")
						{
							this.lpr.set_countryWeight(text, 0.1f);
						}
						else
						{
							if (text.ToLower().Contains(this.countryCode))
							{
								this.lpr.set_countryWeight(text, 0.8f);
							}
							else
							{
								this.lpr.set_countryWeight(text, 0f);
							}
						}
					}
				}
			}
			this.lpr.realizeCountryWeights();
		}
		public void CloseLPREngine()
		{
			try
			{
				if (this.processors != null)
				{
					using (Stack<IProcessor>.Enumerator enumerator = this.processors.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							enumerator.Current.Dispose();
						}
					}
				}
				this.cIProcessor = 0;
				this.processors.Clear();
				if (this.lpr != null)
				{
					this.lpr.Dispose();
				}
			}
			catch (Exception ex)
			{
				if (this.ErrorEvent != null)
				{
					this.ErrorEvent(this, new ErrorEventArgs("Close LPR engine error: " + ex.ToString()));
				}
			}
			finally
			{
				GC.Collect();
			}
		}
		public bool Analyze(ref LPRObject lprObject)
		{
			bool result = false;
			try
			{
				if (this.cPending < this.cIProcessor && this.processors.Count > 0)
				{
					Monitor.Enter(this);
					this.cPending++;
					IProcessor processor = this.processors.Pop();
					Monitor.Exit(this);
					string plateNumber = "";
					Bitmap plateImage = null;
					Rectangle empty = Rectangle.Empty;
					int recognizeTime = 0;
					float confidence = 0f;
					bool isBlackOnWhite = true;
					List<string> plateNumberSub = new List<string>();
					List<Bitmap> plateImageSub = new List<Bitmap>();
					List<Rectangle> plateLocationSub = new List<Rectangle>();
					List<float> confidenceSub = new List<float>();
					List<bool> isBlackOnWhiteSub = new List<bool>();
					if (lprObject.fileName != "")
					{
						if (lprObject.enableMultiplePlateNumber)
						{
							if (this.Analyze(processor, lprObject.fileName, lprObject.vehicleImage, lprObject.enableRawFormat, ref plateNumber, ref plateImage, ref empty, ref recognizeTime, ref confidence, ref isBlackOnWhite, ref plateNumberSub, ref plateImageSub, ref plateLocationSub, ref confidenceSub, ref isBlackOnWhiteSub))
							{
								lprObject.plateNumber = plateNumber;
								lprObject.plateImage = plateImage;
								lprObject.plateLocation = empty;
								lprObject.recognizeTime = recognizeTime;
								lprObject.confidence = confidence;
								lprObject.isBlackOnWhite = isBlackOnWhite;
								lprObject.plateNumberSub = plateNumberSub;
								lprObject.plateImageSub = plateImageSub;
								lprObject.plateLocationSub = plateLocationSub;
								lprObject.confidenceSub = confidenceSub;
								lprObject.isBlackOnWhiteSub = isBlackOnWhiteSub;
								result = true;
							}
						}
						else
						{
							if (this.Analyze(processor, lprObject.fileName, lprObject.vehicleImage, lprObject.enableRawFormat, ref plateNumber, ref plateImage, ref empty, ref recognizeTime, ref confidence, ref isBlackOnWhite))
							{
								lprObject.plateNumber = plateNumber;
								lprObject.plateImage = plateImage;
								lprObject.plateLocation = empty;
								lprObject.recognizeTime = recognizeTime;
								lprObject.confidence = confidence;
								lprObject.isBlackOnWhite = isBlackOnWhite;
								result = true;
							}
						}
					}
					else
					{
						if (lprObject.enableMultiplePlateNumber)
						{
							if (this.Analyze(processor, lprObject.vehicleImage, lprObject.enableRawFormat, ref plateNumber, ref plateImage, ref empty, ref recognizeTime, ref confidence, ref isBlackOnWhite, ref plateNumberSub, ref plateImageSub, ref plateLocationSub, ref confidenceSub, ref isBlackOnWhiteSub))
							{
								lprObject.plateNumber = plateNumber;
								lprObject.plateImage = plateImage;
								lprObject.plateLocation = empty;
								lprObject.recognizeTime = recognizeTime;
								lprObject.confidence = confidence;
								lprObject.isBlackOnWhite = isBlackOnWhite;
								lprObject.plateNumberSub = plateNumberSub;
								lprObject.plateImageSub = plateImageSub;
								lprObject.plateLocationSub = plateLocationSub;
								lprObject.confidenceSub = confidenceSub;
								lprObject.isBlackOnWhiteSub = isBlackOnWhiteSub;
								result = true;
							}
						}
						else
						{
							if (this.Analyze(processor, lprObject.vehicleImage, lprObject.enableRawFormat, ref plateNumber, ref plateImage, ref empty, ref recognizeTime, ref confidence, ref isBlackOnWhite))
							{
								lprObject.plateNumber = plateNumber;
								lprObject.plateImage = plateImage;
								lprObject.plateLocation = empty;
								lprObject.recognizeTime = recognizeTime;
								lprObject.confidence = confidence;
								lprObject.isBlackOnWhite = isBlackOnWhite;
								result = true;
							}
						}
					}
					Monitor.Enter(this);
					this.processors.Push(processor);
					Monitor.Pulse(this);
					this.cPending--;
					Monitor.Exit(this);
				}
				else
				{
					if (this.ErrorEvent != null)
					{
						this.ErrorEvent(this, new ErrorEventArgs("Abort LPR: " + this.cPending));
					}
					result = false;
				}
			}
			catch (FormatException fex)
			{
				if (this.ErrorEvent != null)
				{
					this.ErrorEvent(this, new ErrorEventArgs("LPR - formatEx: " + fex.Message));
				}
			}
			catch (Exception ex)
			{
				if (this.ErrorEvent != null)
				{
					this.ErrorEvent(this, new ErrorEventArgs(string.Concat(new object[]
					{
						"LPR: ",
						this.cPending,
						".",
						ex.Message
					})));
				}
			}
			return result;
		}
		private bool Analyze(IProcessor processor, string fileName, Bitmap vehicleImage, bool enableRawResult, ref string plateNumber, ref Bitmap plateImage, ref Rectangle plateLocation, ref int recognizeTime, ref float confidence, ref bool isBlackOnWhite)
		{
			bool result = false;
			if (this.processors != null)
			{
				try
				{
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					int num = 0;
					List<Candidate> list = processor.analyze(fileName);
					while (true)
					{
						bool flag = false;
						if (list != null && list.Count > 0)
						{
							CountryMatch countryMatch = new CountryMatch();
							countryMatch.confidence = -1f;
							countryMatch.text = "";
							for (int i = 0; i < list.Count; i++)
							{
								if (list[i].matches.Count > 1 && list[i].matches[0].confidence > countryMatch.confidence)
								{
									countryMatch = list[i].matches[0];
									plateLocation = list[i].bbox;
									isBlackOnWhite = list[i].brightBackground;
								}
							}
							if (countryMatch.confidence > 0f)
							{
								plateNumber = countryMatch.text;
								confidence = countryMatch.confidence;
								if (vehicleImage != null && plateLocation != null && plateLocation != Rectangle.Empty)
									plateImage = this.CropImage(vehicleImage, plateLocation);
							}
							else
							{
								if (enableRawResult)
								{
									plateNumber = "";
									confidence = 0f;
									plateLocation = Rectangle.Empty;
									for (int j = 0; j < list.Count; j++)
									{
										if (list[j].matches.Count > 0 && list[j].matches[0].text.Length >= 4 && this.IsValidRawFormat(list[j].matches[0].text) && list[j].matches[0].text.Length > plateNumber.Length)
										{
											plateNumber = list[j].matches[0].text;
											confidence = list[j].matches[0].confidence;
											isBlackOnWhite = list[j].brightBackground;
											Candidate candidate = list[j];
											int num2 = candidate.bbox.Width / plateNumber.Length;
											candidate = list[j];
											int num3 = candidate.bbox.X - 2 * num2;
											if (num3 < 0)
											{
												num3 = 0;
											}
											candidate = list[j];
											int num4 = candidate.bbox.Width + 3 * num2;
											if (num4 > vehicleImage.Width)
											{
												num4 = vehicleImage.Width;
											}
											int arg_310_0 = num3;
											candidate = list[j];
											int arg_310_1 = candidate.bbox.Y;
											int arg_310_2 = num4;
											candidate = list[j];
											plateLocation = new Rectangle(arg_310_0, arg_310_1, arg_310_2, candidate.bbox.Height);
											flag = true;
										}
									}
									if (plateLocation != Rectangle.Empty)
									{
										plateImage = this.CropImage(vehicleImage, plateLocation);
									}
								}
							}
						}

						if (!(string.IsNullOrEmpty(plateNumber) | flag) || this.contrastSensitivityFactor == null || this.contrastSensitivityFactor.Count <= 0 || num >= this.contrastSensitivityFactor.Count)
						{
							break;
						}
						processor.contrastSensitivityFactor = this.contrastSensitivityFactor[num];
						num++;
						list = processor.analyze(fileName);
						processor.contrastSensitivityFactor = 0.9178571f;

					}
					stopwatch.Stop();
					recognizeTime = stopwatch.Elapsed.Milliseconds;
					result = true;
				}
				catch (FormatException fex)
				{
					if (this.ErrorEvent != null)
					{
						this.ErrorEvent(this, new ErrorEventArgs("LPR 1 - formatEx: " + fex.Message));
					}
				}
				catch (Exception ex)
				{
					if (this.ErrorEvent != null)
					{
						this.ErrorEvent(this, new ErrorEventArgs("LPR 1: " + ex.Message));
					}
				}
				finally
				{
					GC.Collect();
				}
			}
			return result;
		}
		private bool Analyze(IProcessor processor, string fileName, Bitmap vehicleImage, bool enableRawResult, ref string plateNumber, ref Bitmap plateImage, ref Rectangle plateLocation, ref int recognizeTime, ref float confidence, ref bool isBlackOnWhite, ref List<string> plateNumberSub, ref List<Bitmap> plateImageSub, ref List<Rectangle> plateLocationSub, ref List<float> confidenceSub, ref List<bool> isBlackOnWhiteSub)
		{
			bool result = false;
			if (this.processors != null)
			{
				try
				{
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					int num = 0;
					List<Candidate> list = processor.analyze(fileName);
					while (true)
					{
						if (list != null && list.Count > 0)
						{
							CountryMatch countryMatch = new CountryMatch();
							countryMatch.confidence = -1f;
							countryMatch.text = "";
							plateNumberSub = new List<string>();
							plateImageSub = new List<Bitmap>();
							plateLocationSub = new List<Rectangle>();
							confidenceSub = new List<float>();
							isBlackOnWhiteSub = new List<bool>();

							for (int i = 0; i < list.Count; i++)
							{
								if (list[i].matches.Count > 1 && list[i].matches[0].confidence > countryMatch.confidence)
								{
									countryMatch = list[i].matches[0];
									plateLocation = list[i].bbox;
									isBlackOnWhite = list[i].brightBackground;
								}
							}
							if (countryMatch.confidence > 0f)
							{
								plateNumber = countryMatch.text;
								confidence = countryMatch.confidence;
								plateImage = this.CropImage(vehicleImage, plateLocation);
							}
							else
							{
								if (list.Count > 0 && list[0].matches.Count > 0)
								{
									string strplate = (list[0].matches[0].text);
									// Check format Car
									if (ANPRTools.IsValidCarPlateNumber(strplate))
									{
										plateNumberSub.Add(list[0].matches[0].text);
										plateImageSub.Add(this.CropImage(vehicleImage, list[0].bbox));
										plateLocationSub.Add(list[0].bbox);
										confidenceSub.Add(list[0].matches[0].confidence);
										isBlackOnWhiteSub.Add(list[0].brightBackground);
									}
								}
							}

							for (int j = 0; j < list.Count; j++)
							{
								if (list[j].matches.Count > 1 && list[j].matches[0].text != plateNumber)
								{
									plateNumberSub.Add(list[j].matches[0].text);
									plateImageSub.Add(this.CropImage(vehicleImage, list[j].bbox));
									plateLocationSub.Add(list[j].bbox);
									confidenceSub.Add(list[j].matches[0].confidence);
									isBlackOnWhiteSub.Add(list[j].brightBackground);
								}
							}
							if (enableRawResult)
							{
								for (int k = 0; k < list.Count; k++)
								{
									if (list[k].matches.Count > 0 && list[k].matches[0].text.Length >= 4 && this.IsValidRawFormat(list[k].matches[0].text) && ANPRTools.IsValidCarPlateNumber(list[k].matches[0].text) && list[k].matches[0].text != plateNumber && !plateNumberSub.Contains(list[k].matches[0].text))
									{
										Candidate candidate = list[k];
										int num2 = candidate.bbox.Width / list[k].matches[0].text.Length;
										candidate = list[k];
										int num3 = candidate.bbox.X - 2 * num2;
										if (num3 < 0)
										{
											num3 = 0;
										}
										candidate = list[k];
										int num4 = candidate.bbox.Width + 3 * num2;
										if (num4 > vehicleImage.Width)
										{
											num4 = vehicleImage.Width;
										}
										int arg_3E1_1 = num3;
										candidate = list[k];
										int arg_3E1_2 = candidate.bbox.Y;
										int arg_3E1_3 = num4;
										candidate = list[k];
										Rectangle rectangle = new Rectangle(arg_3E1_1, arg_3E1_2, arg_3E1_3, candidate.bbox.Height);
										plateNumberSub.Add(list[k].matches[0].text);
										plateImageSub.Add(this.CropImage(vehicleImage, rectangle));
										plateLocationSub.Add(rectangle);
										confidenceSub.Add(list[k].matches[0].confidence);
										isBlackOnWhiteSub.Add(list[k].brightBackground);
									}
								}
							}
						}
						if (!string.IsNullOrEmpty(plateNumber) || this.contrastSensitivityFactor == null || this.contrastSensitivityFactor.Count <= 0 || num >= this.contrastSensitivityFactor.Count)
						{
							break;
						}
						processor.contrastSensitivityFactor = this.contrastSensitivityFactor[num];
						num++;
						list = processor.analyze(fileName);
						processor.contrastSensitivityFactor = 0.9178571f;
					}
					if ((string.IsNullOrEmpty(plateNumber) & enableRawResult) && plateNumberSub != null && plateNumberSub.Count > 0)
					{
						if (ANPRTools.IsValidCarPlateNumber(plateNumberSub[0]))
						{
							plateNumber = plateNumberSub[0];
							plateImage = plateImageSub[0];
							plateLocation = plateLocationSub[0];
							confidence = confidenceSub[0];
							isBlackOnWhite = isBlackOnWhiteSub[0];
							plateNumberSub.RemoveAt(0);
							plateImageSub.RemoveAt(0);
							plateLocationSub.RemoveAt(0);
							confidenceSub.RemoveAt(0);
							isBlackOnWhiteSub.RemoveAt(0);
						}
					}
					stopwatch.Stop();
					recognizeTime = stopwatch.Elapsed.Milliseconds;
					result = true;
				}
				catch (FormatException fex)
				{
					if (this.ErrorEvent != null)
					{
						this.ErrorEvent(this, new ErrorEventArgs("LPR 2 - formatEx: " + fex.Message));
					}
				}
				catch (Exception ex)
				{
					if (this.ErrorEvent != null)
					{
						this.ErrorEvent(this, new ErrorEventArgs("LPR 2: " + ex.Message));
					}
				}
				finally
				{
					GC.Collect();
				}
			}
			return result;
		}
		private bool Analyze(IProcessor processor, Bitmap vehicleImage, bool enableRawResult, ref string plateNumber, ref Bitmap plateImage, ref Rectangle plateLocation, ref int recognizeTime, ref float confidence, ref bool isBlackOnWhite)
		{
			bool result = false;
			if (processor != null && vehicleImage != null && vehicleImage.Width > 0 && vehicleImage.Height > 0)
			{
				try
				{
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					vehicleImage = this.FormatImage(vehicleImage, PixelFormat.Format24bppRgb);
					int num = 0;
					List<Candidate> list = processor.analyze(vehicleImage);
					while (true)
					{
						bool flag = false;
						if (list != null && list.Count > 0)
						{
							CountryMatch countryMatch = new CountryMatch();
							countryMatch.confidence = -1f;
							countryMatch.text = "";
							for (int i = 0; i < list.Count; i++)
							{
								if (list[i].matches.Count > 1 && list[i].matches[0].confidence > countryMatch.confidence)
								{
									countryMatch = list[i].matches[0];
									plateLocation = list[i].bbox;
									isBlackOnWhite = list[i].brightBackground;
								}
							}
							if (countryMatch.confidence > 0f)
							{
								plateNumber = countryMatch.text;
								confidence = countryMatch.confidence;
								plateImage = this.CropImage(vehicleImage, plateLocation);
							}
							else
							{
								if (enableRawResult)
								{
									plateNumber = "";
									confidence = 0f;
									plateLocation = Rectangle.Empty;
									for (int j = 0; j < list.Count; j++)
									{
										if (list[j].matches.Count > 0 && list[j].matches[0].text.Length >= 4 && this.IsValidRawFormat(list[j].matches[0].text) && list[j].matches[0].text.Length > plateNumber.Length)
										{
											plateNumber = list[j].matches[0].text;
											confidence = list[j].matches[0].confidence;
											isBlackOnWhite = list[j].brightBackground;
											Candidate candidate = list[j];
											int num2 = candidate.bbox.Width / plateNumber.Length;
											candidate = list[j];
											int num3 = candidate.bbox.X - 2 * num2;
											if (num3 < 0)
											{
												num3 = 0;
											}
											candidate = list[j];
											int num4 = candidate.bbox.Width + 3 * num2;
											if (num4 > vehicleImage.Width)
											{
												num4 = vehicleImage.Width;
											}
											int arg_336_0 = num3;
											candidate = list[j];
											int arg_336_1 = candidate.bbox.Y;
											int arg_336_2 = num4;
											candidate = list[j];
											plateLocation = new Rectangle(arg_336_0, arg_336_1, arg_336_2, candidate.bbox.Height);
											flag = true;
										}
									}
									if (plateLocation != Rectangle.Empty)
									{
										plateImage = this.CropImage(vehicleImage, plateLocation);
									}
								}
							}
						}
						if (!(string.IsNullOrEmpty(plateNumber) | flag) || this.contrastSensitivityFactor == null || this.contrastSensitivityFactor.Count <= 0 || num >= this.contrastSensitivityFactor.Count)
						{
							break;
						}
						processor.contrastSensitivityFactor = this.contrastSensitivityFactor[num];
						num++;
						list = processor.analyze(vehicleImage);
						processor.contrastSensitivityFactor = 0.9178571f;
					}
					stopwatch.Stop();
					recognizeTime = stopwatch.Elapsed.Milliseconds;
					result = true;
				}
				catch (FormatException fex)
				{
					if (this.ErrorEvent != null)
					{
						this.ErrorEvent(this, new ErrorEventArgs("LPR 3 - formatEx: " + fex.Message));
					}
				}
				catch (Exception ex)
				{
					if (this.ErrorEvent != null)
					{
						this.ErrorEvent(this, new ErrorEventArgs("LPR 3: " + ex.Message));
					}
				}
				finally
				{
					GC.Collect();
				}
			}
			return result;
		}
		private bool Analyze(IProcessor processor, Bitmap vehicleImage, bool enableRawResult, ref string plateNumber, ref Bitmap plateImage, ref Rectangle plateLocation, ref int recognizeTime, ref float confidence, ref bool isBlackOnWhite, ref List<string> plateNumberSub, ref List<Bitmap> plateImageSub, ref List<Rectangle> plateLocationSub, ref List<float> confidenceSub, ref List<bool> isBlackOnWhiteSub)
		{
			bool result = false;
			if (processor != null && vehicleImage != null && vehicleImage.Width > 0 && vehicleImage.Height > 0)
			{
				try
				{
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					vehicleImage = this.FormatImage(vehicleImage, PixelFormat.Format24bppRgb);
					int num = 0;
					List<Candidate> list = processor.analyze(vehicleImage);
					while (true)
					{
						if (list != null && list.Count > 0)
						{
							CountryMatch countryMatch = new CountryMatch();
							countryMatch.confidence = -1f;
							countryMatch.text = "";
							for (int i = 0; i < list.Count; i++)
							{
								if (list[i].matches.Count > 1 && list[i].matches[0].confidence > countryMatch.confidence)
								{
									countryMatch = list[i].matches[0];
									plateLocation = list[i].bbox;
									isBlackOnWhite = list[i].brightBackground;
								}
							}
							if (countryMatch.confidence > 0f)
							{
								plateNumber = countryMatch.text;
								confidence = countryMatch.confidence;
								plateImage = this.CropImage(vehicleImage, plateLocation);
							}
							plateNumberSub = new List<string>();
							plateImageSub = new List<Bitmap>();
							plateLocationSub = new List<Rectangle>();
							confidenceSub = new List<float>();
							isBlackOnWhiteSub = new List<bool>();
							for (int j = 0; j < list.Count; j++)
							{
								if (list[j].matches.Count > 1 && list[j].matches[0].text != plateNumber)
								{
									plateNumberSub.Add(list[j].matches[0].text);
									plateImageSub.Add(this.CropImage(vehicleImage, list[j].bbox));
									plateLocationSub.Add(list[j].bbox);
									confidenceSub.Add(list[j].matches[0].confidence);
									isBlackOnWhiteSub.Add(list[j].brightBackground);
								}
							}
							if (enableRawResult)
							{
								for (int k = 0; k < list.Count; k++)
								{
									if (list[k].matches.Count > 0 && list[k].matches[0].text.Length >= 4 && this.IsValidRawFormat(list[k].matches[0].text) && list[k].matches[0].text != plateNumber && !plateNumberSub.Contains(list[k].matches[0].text))
									{
										Candidate candidate = list[k];
										int num2 = candidate.bbox.Width / list[k].matches[0].text.Length;
										candidate = list[k];
										int num3 = candidate.bbox.X - 2 * num2;
										if (num3 < 0)
										{
											num3 = 0;
										}
										candidate = list[k];
										int num4 = candidate.bbox.Width + 3 * num2;
										if (num4 > vehicleImage.Width)
										{
											num4 = vehicleImage.Width;
										}
										int arg_407_1 = num3;
										candidate = list[k];
										int arg_407_2 = candidate.bbox.Y;
										int arg_407_3 = num4;
										candidate = list[k];
										Rectangle rectangle = new Rectangle(arg_407_1, arg_407_2, arg_407_3, candidate.bbox.Height);
										plateNumberSub.Add(list[k].matches[0].text);
										plateImageSub.Add(this.CropImage(vehicleImage, rectangle));
										plateLocationSub.Add(rectangle);
										confidenceSub.Add(list[k].matches[0].confidence);
										isBlackOnWhiteSub.Add(list[k].brightBackground);
									}
								}
							}
						}
						if (!string.IsNullOrEmpty(plateNumber) || this.contrastSensitivityFactor == null || this.contrastSensitivityFactor.Count <= 0 || num >= this.contrastSensitivityFactor.Count)
						{
							break;
						}
						processor.contrastSensitivityFactor = this.contrastSensitivityFactor[num];
						num++;
						list = processor.analyze(vehicleImage);
						processor.contrastSensitivityFactor = 0.9178571f;
					}
					if ((string.IsNullOrEmpty(plateNumber) & enableRawResult) && plateNumberSub != null && plateNumberSub.Count > 0)
					{
						plateNumber = plateNumberSub[0];
						plateImage = plateImageSub[0];
						plateLocation = plateLocationSub[0];
						confidence = confidenceSub[0];
						isBlackOnWhite = isBlackOnWhiteSub[0];
						plateNumberSub.RemoveAt(0);
						plateImageSub.RemoveAt(0);
						plateLocationSub.RemoveAt(0);
						confidenceSub.RemoveAt(0);
						isBlackOnWhiteSub.RemoveAt(0);
					}
					stopwatch.Stop();
					recognizeTime = stopwatch.Elapsed.Milliseconds;
					result = true;
				}
				catch (FormatException fex)
				{
					if (this.ErrorEvent != null)
					{
						this.ErrorEvent(this, new ErrorEventArgs("LPR 4 - formatEx: " + fex.Message));
					}
				}
				catch (Exception ex)
				{
					if (this.ErrorEvent != null)
					{
						this.ErrorEvent(this, new ErrorEventArgs("LPR 4: " + ex.Message));
					}
				}
				finally
				{
					GC.Collect();
				}
			}
			return result;
		}
		public void AnalyzeAsync(ref LPRObject lprObject)
		{
			Monitor.Enter(this);
			while (this.cPending >= this.cIProcessor)
			{
				Monitor.Wait(this);
			}
			this.cPending++;
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.process), lprObject);
			Monitor.Exit(this);
		}
		private void process(object obj)
		{
			try
			{
				bool flag = false;
				LPRObject lPRObject = (LPRObject)obj;
				Monitor.Enter(this);
				IProcessor processor = this.processors.Pop();
				Monitor.Exit(this);
				string plateNumber = "";
				Bitmap plateImage = null;
				Rectangle empty = Rectangle.Empty;
				int recognizeTime = 0;
				float confidence = 0f;
				bool isBlackOnWhite = true;
				List<string> plateNumberSub = new List<string>();
				List<Bitmap> plateImageSub = new List<Bitmap>();
				List<Rectangle> plateLocationSub = new List<Rectangle>();
				List<float> confidenceSub = new List<float>();
				List<bool> isBlackOnWhiteSub = new List<bool>();
				if (lPRObject.fileName != "")
				{
					if (lPRObject.enableMultiplePlateNumber)
					{
						if (this.Analyze(processor, lPRObject.fileName, lPRObject.vehicleImage, lPRObject.enableRawFormat, ref plateNumber, ref plateImage, ref empty, ref recognizeTime, ref confidence, ref isBlackOnWhite, ref plateNumberSub, ref plateImageSub, ref plateLocationSub, ref confidenceSub, ref isBlackOnWhiteSub))
						{
							lPRObject.plateNumber = plateNumber;
							lPRObject.plateImage = plateImage;
							lPRObject.plateLocation = empty;
							lPRObject.recognizeTime = recognizeTime;
							lPRObject.confidence = confidence;
							lPRObject.isBlackOnWhite = isBlackOnWhite;
							lPRObject.plateNumberSub = plateNumberSub;
							lPRObject.plateImageSub = plateImageSub;
							lPRObject.plateLocationSub = plateLocationSub;
							lPRObject.confidenceSub = confidenceSub;
							lPRObject.isBlackOnWhiteSub = isBlackOnWhiteSub;
							flag = true;
						}
					}
					else
					{
						if (this.Analyze(processor, lPRObject.fileName, lPRObject.vehicleImage, lPRObject.enableRawFormat, ref plateNumber, ref plateImage, ref empty, ref recognizeTime, ref confidence, ref isBlackOnWhite))
						{
							lPRObject.plateNumber = plateNumber;
							lPRObject.plateImage = plateImage;
							lPRObject.plateLocation = empty;
							lPRObject.recognizeTime = recognizeTime;
							lPRObject.confidence = confidence;
							lPRObject.isBlackOnWhite = isBlackOnWhite;
							flag = true;
						}
					}
				}
				else
				{
					if (lPRObject.enableMultiplePlateNumber)
					{
						if (this.Analyze(processor, lPRObject.vehicleImage, lPRObject.enableRawFormat, ref plateNumber, ref plateImage, ref empty, ref recognizeTime, ref confidence, ref isBlackOnWhite, ref plateNumberSub, ref plateImageSub, ref plateLocationSub, ref confidenceSub, ref isBlackOnWhiteSub))
						{
							lPRObject.plateNumber = plateNumber;
							lPRObject.plateImage = plateImage;
							lPRObject.plateLocation = empty;
							lPRObject.recognizeTime = recognizeTime;
							lPRObject.confidence = confidence;
							lPRObject.isBlackOnWhite = isBlackOnWhite;
							lPRObject.plateNumberSub = plateNumberSub;
							lPRObject.plateImageSub = plateImageSub;
							lPRObject.plateLocationSub = plateLocationSub;
							lPRObject.confidenceSub = confidenceSub;
							lPRObject.isBlackOnWhiteSub = isBlackOnWhiteSub;
							flag = true;
						}
					}
					else
					{
						if (this.Analyze(processor, lPRObject.vehicleImage, lPRObject.enableRawFormat, ref plateNumber, ref plateImage, ref empty, ref recognizeTime, ref confidence, ref isBlackOnWhite))
						{
							lPRObject.plateNumber = plateNumber;
							lPRObject.plateImage = plateImage;
							lPRObject.plateLocation = empty;
							lPRObject.recognizeTime = recognizeTime;
							lPRObject.confidence = confidence;
							lPRObject.isBlackOnWhite = isBlackOnWhite;
							flag = true;
						}
					}
				}
				Thread.Sleep(1);
				Application.DoEvents();
				Monitor.Enter(this);
				this.processors.Push(processor);
				this.cPending--;
				Monitor.Pulse(this);
				Monitor.Exit(this);
				if (flag && this.LPRCompleted != null)
				{
					this.LPRCompleted(this, ref lPRObject);
				}
			}
			catch (Exception ex)
			{
				if (this.ErrorEvent != null)
				{
					this.ErrorEvent(this, new ErrorEventArgs(string.Concat(new object[]
					{
						"Async Process LPR: ",
						this.cPending,
						". ",
						ex.Message
					})));
				}
			}
		}
		public Bitmap FormatImage(Bitmap source, PixelFormat format)
		{
			try
			{
				Bitmap result;
				if (source.PixelFormat == format)
				{
					result = source;
					return result;
				}
				int width = source.Width;
				int height = source.Height;
				Bitmap expr_23 = new Bitmap(width, height, format);
				Graphics expr_29 = Graphics.FromImage(expr_23);
				expr_29.DrawImage(source, 0, 0, width, height);
				expr_29.Dispose();
				result = expr_23;
				return result;
			}
			catch (FormatException fex)
			{
				if (this.ErrorEvent != null)
				{
					this.ErrorEvent(this, new ErrorEventArgs("FormatImage Error - core: " + fex.ToString()));
				}
			}
			catch (Exception ex)
			{
				if (this.ErrorEvent != null)
				{
					this.ErrorEvent(this, new ErrorEventArgs("FormatImage Error: " + ex.ToString()));
				}
			}
			finally
			{
				GC.Collect();
			}
			return source;
		}
		public Bitmap CropImage(Bitmap img, Rectangle cropArea)
		{
			Bitmap result = null;
			try
			{
				if (img != null)
				{
					lock (img)
					{
						if (cropArea != Rectangle.Empty)
						{
							int num = cropArea.X - 25;
							if (num < 0)
							{
								num = 0;
							}
							int num2 = cropArea.Y - 20;
							if (num2 < 0)
							{
								num2 = 0;
							}
							int num3 = cropArea.Width + 50;
							if (num + num3 > img.Width)
							{
								num3 = img.Width - num;
							}
							int num4 = cropArea.Height + 40;
							if (num2 + num4 > img.Height)
							{
								num4 = img.Height - num2;
							}
							cropArea = new Rectangle(num, num2, num3, num4);
							Bitmap bitmap = img.Clone(cropArea, img.PixelFormat);
							if (bitmap != null)
							{
								result = bitmap;
							}
							else
							{
								result = img;
							}
						}
						else
						{
							result = img;
						}
					}
				}
			}
			catch
			{
				result = img;
			}
			finally
			{
				GC.Collect();
			}
			return result;
		}

		public Bitmap CropImage1(Bitmap img, Rectangle cropArea)
		{
			Bitmap result = null;
			try
			{
				if (img != null)
				{
					lock (img)
					{
						if (cropArea != Rectangle.Empty)
						{
							int num = cropArea.X - 25;
							if (num < 0)
							{
								num = 0;
							}
							int num2 = cropArea.Y - 20;
							if (num2 < 0)
							{
								num2 = 0;
							}
							int num3 = cropArea.Width + 50;
							if (num + num3 > img.Width)
							{
								num3 = img.Width - num;
							}
							int num4 = cropArea.Height + 40;
							if (num2 + num4 > img.Height)
							{
								num4 = img.Height - num2;
							}
							cropArea = new Rectangle(num, num2, num3, num4);
							Bitmap bitmap = img.Clone(cropArea, img.PixelFormat);
							if (bitmap != null)
							{
								result = bitmap;
							}
							else
							{
								result = img;
							}
						}
						else
						{
							result = img;
						}
					}
				}
			}
			catch
			{
				result = img;
			}
			finally
			{
				GC.Collect();
			}
			return result;
		}

		private bool IsValidRawFormat(string plateNumber)
		{
			bool result = false;
			if (plateNumber.Length >= 4)
			{
				string[] array = plateNumber.Split(new char[]
				{
					' '
				});
				if (array != null && array.Length != 0)
				{
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						if (array2[i].Length >= 4)
						{
							result = true;
							break;
						}
					}
				}
			}
			return result;
		}
	}
}
