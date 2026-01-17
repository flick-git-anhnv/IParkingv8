using SimpleLPR2;
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
	public class SimpleLPRX64_v2
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
				if(this.lprEngineProductKey == "demo")
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
            this.lpr = SimpleLPR.Setup();
            this.processors = new Stack<IProcessor>();
            this.CreateProcessor(true);
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

					this.cIProcessor = 0;
					this.processors.Clear();
				}
				
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
						this.ErrorEvent(this, new ErrorEventArgs("Abort LPRv2: " + this.cPending));
					}
					result = false;
				}
			}
            catch (FormatException fex)
            {
                if (this.ErrorEvent != null)
                {
                    this.ErrorEvent(this, new ErrorEventArgs("LPRv2 - formatEx: " + fex.Message));
                }
            }
            catch (Exception ex)
			{
				if (this.ErrorEvent != null)
				{
					this.ErrorEvent(this, new ErrorEventArgs(string.Concat(new object[]
					{
						"LPRv2: ",
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
            if (this.processors != null)
            {
                try
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    this.SetCountryCode();
                    System.Collections.Generic.List<Candidate> list = processor.analyze(fileName, 120);
                    stopwatch.Stop();
                    recognizeTime = stopwatch.Elapsed.Milliseconds;
                    if (list != null && list.Count > 0)
                    {
                        Candidate bestCand = list[0];
                        for (int i = 1; i < list.Count; i++)
                        {
                            if (list[i].confidence > bestCand.confidence)
                            {
                                bestCand = list[i];
                            }
                        }
                        plateNumber = bestCand.text;
                        confidence = bestCand.confidence;
                        isBlackOnWhite = bestCand.brightBackground;
                        if (plateNumber != "")
                        {
                            plateImage = this.GetPlateImage(vehicleImage, bestCand, ref plateLocation);
                        }
                    }
                    return true;
                }
                catch (FormatException fex)
                {
                    if (this.ErrorEvent != null)
                    {
                        this.ErrorEvent(this, new ErrorEventArgs("LPRv2 1 - formatEx: " + fex.Message));
                    }
                }
                catch (System.Exception ex)
                {
                    if (this.ErrorEvent != null)
                    {
                        this.ErrorEvent(this, new ErrorEventArgs("LPRv2 1: " + ex.Message));
                    }
                }
                finally
                {
                    System.GC.Collect();
                }
                return false;
            }
            return false;
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
                    this.SetCountryCode();
                    System.Collections.Generic.List<Candidate> list = processor.analyze(fileName, 120);

                    if (list != null && list.Count > 0)
                    {
                        Candidate bestCand = list[0];
                        int bestIndex = 0;
                        for (int i = 1; i < list.Count; i++)
                        {
                            if ( list[i].confidence > bestCand.confidence)
                            {
                                bestCand = list[i];
                                bestIndex = i;
                            }
                        }
                        plateNumber = bestCand.text;
                        confidence = bestCand.confidence;
                        isBlackOnWhite = bestCand.brightBackground;
                        if (plateNumber != "")
                        {
                            plateImage = this.GetPlateImage(vehicleImage, bestCand, ref plateLocation);
                        }

                        plateNumberSub = new List<string>();
                        plateImageSub = new List<Bitmap>();
                        plateLocationSub = new List<Rectangle>();
                        confidenceSub = new List<float>();
                        isBlackOnWhiteSub = new List<bool>();

                        for (int j = 0; j < list.Count; j++)
                        {
                            if (list[j].text != plateNumber)
                            {
                                plateNumberSub.Add(list[j].text);
                                Rectangle plate_Location_Sub = new Rectangle();
                                Bitmap plate_Img_Sub = this.GetPlateImage(vehicleImage, bestCand, ref plate_Location_Sub);
                                plateImageSub.Add(plate_Img_Sub);
                                plateLocationSub.Add(plate_Location_Sub);
                                confidenceSub.Add(list[j].confidence);
                                isBlackOnWhiteSub.Add(list[j].brightBackground);
                            }
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
                        this.ErrorEvent(this, new ErrorEventArgs("LPRv2 2 - formatEx: " + fex.Message));
                    }
                }
                catch (Exception ex)
				{
					if (this.ErrorEvent != null)
					{
						this.ErrorEvent(this, new ErrorEventArgs("LPRv2 2: " + ex.Message));
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
            if (this.processors != null)
            {
                try
                {
                    vehicleImage = this.FormatImage(vehicleImage, PixelFormat.Format24bppRgb);
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    this.SetCountryCode();
                    System.Collections.Generic.List<Candidate> list = processor.analyze(vehicleImage, 120);
                    stopwatch.Stop();
                    recognizeTime = stopwatch.Elapsed.Milliseconds;
                    if (list != null && list.Count > 0)
                    {
                        Candidate bestCand = list[0];
                        for (int i = 1; i < list.Count; i++)
                        {
                            if (list[i].confidence > bestCand.confidence)
                            {
                                bestCand = list[i];
                            }
                        }
                        plateNumber = bestCand.text;
                        confidence = bestCand.confidence;
                        isBlackOnWhite = bestCand.brightBackground;
                        if (plateNumber != "")
                        {
                            plateImage = this.GetPlateImage(vehicleImage, bestCand, ref plateLocation);
                        }
                    }
                    return true;
                }
                catch (FormatException fex)
                {
                    if (this.ErrorEvent != null)
                    {
                        this.ErrorEvent(this, new ErrorEventArgs("LPRv2 3 - formatEx: " + fex.Message));
                    }
                }
                catch (System.Exception ex)
                {
                    if (this.ErrorEvent != null)
                    {
                        this.ErrorEvent(this, new ErrorEventArgs("LPRv2 3: " + ex.Message));
                    }
                }
                finally
                {
                    System.GC.Collect();
                }
                return false;
            }
            return false;
        }
		private bool Analyze(IProcessor processor, Bitmap vehicleImage, bool enableRawResult, ref string plateNumber, ref Bitmap plateImage, ref Rectangle plateLocation, ref int recognizeTime, ref float confidence, ref bool isBlackOnWhite, ref List<string> plateNumberSub, ref List<Bitmap> plateImageSub, ref List<Rectangle> plateLocationSub, ref List<float> confidenceSub, ref List<bool> isBlackOnWhiteSub)
		{
            bool result = false;
            if (this.processors != null)
            {
                try
                {
                    vehicleImage = this.FormatImage(vehicleImage, PixelFormat.Format24bppRgb);
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    this.SetCountryCode();
                    System.Collections.Generic.List<Candidate> list = processor.analyze(vehicleImage, 120);
                    stopwatch.Stop();
                    recognizeTime = stopwatch.Elapsed.Milliseconds;
                    if (list != null && list.Count > 0)
                    {						
						for (int i = 0; i < list.Count; i++)
						{
							plateNumberSub.Add(list[i].text);
							confidenceSub.Add(list[i].confidence);
							isBlackOnWhiteSub.Add(list[i].brightBackground);
							if (plateNumber != "")
							{
								plateImageSub.Add(this.GetPlateImage(vehicleImage, list[i], ref plateLocation));
							}
						}
					}

                    if ((string.IsNullOrEmpty(plateNumber) & enableRawResult) && plateNumberSub != null && plateNumberSub.Count > 0)
                    {
                        plateNumber = plateNumberSub[0];
						plateNumberSub.RemoveAt(0);

						if (plateImageSub != null && plateImageSub.Count > 0)
						{
							plateImage = plateImageSub[0];
							plateImageSub.RemoveAt(0);
						}
						if (plateLocationSub != null && plateLocationSub.Count > 0)
						{
							plateLocation = plateLocationSub[0];
							plateLocationSub.RemoveAt(0);
						}
						if (confidenceSub != null && confidenceSub.Count > 0)
						{
							confidence = confidenceSub[0];
							confidenceSub.RemoveAt(0);
						}
						if (isBlackOnWhiteSub != null && isBlackOnWhiteSub.Count > 0)
						{
							isBlackOnWhite = isBlackOnWhiteSub[0];
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
                        this.ErrorEvent(this, new ErrorEventArgs("LPRv2 4 - formatEx: " + fex.Message));
                    }
                }
                catch (Exception ex)
                {
                    if (this.ErrorEvent != null)
                    {
                        this.ErrorEvent(this, new ErrorEventArgs("LPRv2 4: " + ex.Message));
                    }
                }
                finally
                {
                    GC.Collect();
                }
            }
            return result;
        }

        private System.Drawing.Bitmap GetPlateImage(System.Drawing.Bitmap vehicleImage, Candidate bestCand, ref System.Drawing.Rectangle plateLocation)
        {
            System.Drawing.Bitmap result = null;
            try
            {
                if (vehicleImage != null)
                {
                    int count = bestCand.elements.Count;
                    int num = vehicleImage.Width;
                    int index = count - 1;
                    int num2 = 0;
                    int index2 = 0;
                    int num3 = vehicleImage.Height;
                    int index3 = count - 1;
                    int num4 = 0;
                    int index4 = 0;
                    for (int i = 0; i < count; i++)
                    {
                        Element element = bestCand.elements[i];
                        if (element.bbox.X < num)
                        {
                            num = element.bbox.X;
                            index = i;
                        }
                        if (element.bbox.X + element.bbox.Width > num2)
                        {
                            num2 = element.bbox.X + element.bbox.Width;
                            index2 = i;
                        }
                        if (element.bbox.Y < num3)
                        {
                            num3 = element.bbox.Y;
                            index3 = i;
                        }
                        if (element.bbox.Y + element.bbox.Height > num4)
                        {
                            num4 = element.bbox.Y + element.bbox.Height;
                            index4 = i;
                        }
                    }
                    int arg_138_0 = num;
                    System.Drawing.Rectangle bbox = bestCand.elements[index].bbox;
                    int num5 = arg_138_0 - bbox.Width * 2 / 3;
                    int arg_15B_0 = num3;
                    System.Drawing.Rectangle bbox2 = bestCand.elements[index3].bbox;
                    int num6 = arg_15B_0 - bbox2.Height / 3;
                    int arg_17F_0 = num2;
                    System.Drawing.Rectangle bbox3 = bestCand.elements[index].bbox;
                    int arg_1A0_0 = arg_17F_0 + bbox3.Width * 2 / 3;
                    System.Drawing.Rectangle bbox4 = bestCand.elements[index2].bbox;
                    int num7 = arg_1A0_0 + bbox4.Width * 2 / 3 - num;
                    int arg_1C5_0 = num4;
                    System.Drawing.Rectangle bbox5 = bestCand.elements[index3].bbox;
                    int arg_1E4_0 = arg_1C5_0 + bbox5.Height / 3;
                    System.Drawing.Rectangle bbox6 = bestCand.elements[index4].bbox;
                    int num8 = arg_1E4_0 + bbox6.Height / 3 - num3;
                    if (num5 < 0)
                    {
                        num5 = 0;
                    }
                    if (num6 < 0)
                    {
                        num6 = 0;
                    }
                    if (num5 + num7 > vehicleImage.Width)
                    {
                        num7 = vehicleImage.Width - num5;
                    }
                    if (num6 + num8 > vehicleImage.Height)
                    {
                        num8 = vehicleImage.Height - num6;
                    }
                    if (num5 >= 0 && num6 >= 0 && num7 >= 0 && num8 >= 0)
                    {
                        plateLocation = new System.Drawing.Rectangle(num5, num6, num7, num8);
                        result = this.CropImage(vehicleImage, plateLocation);
                    }
                }
            }
            catch (System.Exception ex)
            {
                if (this.ErrorEvent != null)
                {
                    this.ErrorEvent(this, new ErrorEventArgs("LPR: " + ex.ToString()));
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
