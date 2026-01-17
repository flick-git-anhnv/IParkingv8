using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
namespace Kztek.LPR
{
    public class VehicleANPR : ILPRProcessing
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
        public VehicleANPR()
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
            //this.simpleLPR2.LPRCompleted += new LPRCompletedEventHandler(this.simpleLPRProcessing_LPRCompleted);
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
                string plateNumber = lprObject.plateNumber;
                if (!string.IsNullOrEmpty(plateNumber))
                {
                    if (lprObject.enableRawFormat)
                    {
                        // Keep result
                    }
                    else
                    {
                        if (ANPRTools.IsMotorPlateNumber(plateNumber))
                        {
                            lprObject.isMotorPlateNumber = true;
                            this.HieuChinhBienSoXeMay(ref plateNumber);
                        }
                        else
                        {
                            lprObject.isMotorPlateNumber = false;
                            this.HieuChinhBienSoOto(ref plateNumber);
                        }
                    }
                    lprObject.plateNumber = plateNumber;
                }

                // Sub
                if (lprObject.plateNumberSub.Count > 0)
                {
                    lprObject.isMotorPlateNumberSub.Clear();

                    for (int i = 0; i < lprObject.plateNumberSub.Count; i++)
                    {
                        string value = lprObject.plateNumberSub[i];
                        if (lprObject.enableRawFormat)
                        {
                            // Keep result
                            lprObject.plateNumberSub[i] = value;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(value))
                            {
                                if (ANPRTools.IsMotorPlateNumber(value))
                                {
                                    lprObject.isMotorPlateNumberSub.Add(true);
                                    this.HieuChinhBienSoXeMay(ref value);
                                }
                                else
                                {
                                    lprObject.isMotorPlateNumberSub.Add(false);
                                    this.HieuChinhBienSoOto(ref value);
                                }
                                lprObject.plateNumberSub[i] = value;
                            }
                            else
                                lprObject.isMotorPlateNumberSub.Add(false);
                        }
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
        ~VehicleANPR()
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
                if (string.IsNullOrEmpty(lprObject.plateNumber) && this.enableSimpleLPR2 && this.simpleLPR2 != null)
                {
                    this.simpleLPR2.Analyze(ref lprObject);
                }

                string plateNumber = lprObject.plateNumber;
                if (lprObject.enableRawFormat)
                {
                    // Keep result
                }
                else
                {
                    if (!string.IsNullOrEmpty(plateNumber))
                    {
                        if (ANPRTools.IsMotorPlateNumber(plateNumber))
                        {
                            lprObject.isMotorPlateNumber = true;
                            this.HieuChinhBienSoXeMay(ref plateNumber);
                        }
                        else
                        {
                            lprObject.isMotorPlateNumber = false;
                            this.HieuChinhBienSoOto(ref plateNumber);
                        }
                        lprObject.plateNumber = plateNumber;
                    }
                }

                // Sub
                if (lprObject.plateNumberSub.Count > 0)
                {
                    lprObject.isMotorPlateNumberSub.Clear();

                    for (int i = 0; i < lprObject.plateNumberSub.Count; i++)
                    {
                        string value = lprObject.plateNumberSub[i];
                        if (lprObject.enableRawFormat)
                        {
                            // Keep result
                            lprObject.plateNumberSub[i] = value;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(value))
                            {
                                if (ANPRTools.IsMotorPlateNumber(value))
                                {
                                    lprObject.isMotorPlateNumberSub.Add(true);
                                    this.HieuChinhBienSoXeMay(ref value);
                                }
                                else
                                {
                                    lprObject.isMotorPlateNumberSub.Add(false);
                                    this.HieuChinhBienSoOto(ref value);
                                }
                                lprObject.plateNumberSub[i] = value;
                            }
                            else
                                lprObject.isMotorPlateNumberSub.Add(false);
                        }
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

        private void HieuChinhBienSoOto(ref string plateNumber)
        {
            if (!string.IsNullOrEmpty(plateNumber) && (plateNumber.Length != 8 || plateNumber.IndexOf("-") != 2 || plateNumber.LastIndexOf("-") != 5) && (plateNumber.Length != 6 || plateNumber.LastIndexOf("-") != 2))
            {
                plateNumber = ANPRTools.FixCarPlateNumber(plateNumber);

                if (!string.IsNullOrEmpty(plateNumber))
                {
                    // Check format Car PlateNumber
                    if (!plateNumber.Contains("-"))
                    {
                        // Khong co dau gach ngang
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
                                if (!isOK) plateNumber = "";
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
            }
        }

        private void HieuChinhBienSoXeMay(ref string plateNumber)
        {
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
