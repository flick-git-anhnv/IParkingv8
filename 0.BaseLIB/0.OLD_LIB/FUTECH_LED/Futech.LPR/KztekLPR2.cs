using Futech.LPR.Model;
using Futech.Tools;
using Newtonsoft.Json;
using PlateRecognizer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Futech.LPR
{
    class KztekLPR2 : ILPR
    {
        public int ContrastLevel { get ; set ; }
        public bool Deinterlace { get ; set ; }
        public bool DeinterlacedSource { get ; set ; }
        public int DeviationAnge { get ; set ; }
        public int Fps { get ; set ; }
        public bool HistogramEquualization { get ; set ; }
        public int MaxCharHeight { get ; set ; }
        public int MinCharHeight { get ; set ; }
        public int MotionDetectionTriggering { get ; set ; }
        public int PlateColorSchema { get ; set ; }
        public int PlatePresenceTime { get ; set ; }
        public int PreciseMode { get ; set ; }
        public int RotateAngle { get ; set ; }

        public string GetVersion { get => "KztekLPR_PRO version 1.0"; }

        public Rectangle[] ScanRectangle { get ; set ; }
        private string Apiurl = "http://localhost:21210";
        public string APIUrl { get => Apiurl; set => Apiurl = value ; }
        private HttpClient _client = new HttpClient();
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(bool bVideo)
        {
            _client.Timeout = TimeSpan.FromMilliseconds(3000);
        }

        public void ReadFromBitmap(int hBmp, int customData)
        {
            
        }

        public LicensePlateCollection ReadFromBitmap(Bitmap lastFrame, int customData, ref int recognitionTime)
        {
            try
            {

                LicensePlateCollection licensePlateList = new LicensePlateCollection();

                var bestplate = new LicensePlate();

                var requestModel = new LprRequestModel()
                {
                    ImageData = PlateReader.GetByteArray(lastFrame),
                    VehicleType = customData
                };

                var request = new HttpRequestMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json"),
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"{Apiurl}/lprinfo"),
                };

                var task = Task.Run(async () => await SendDataAsync(request));
                task.Wait();
                var result = task.Result;
                if(!String.IsNullOrEmpty(result.PlateImageBase64))
                    bestplate.Bitmap = (Bitmap)ImageFromBase64(result.PlateImageBase64);
                bestplate.ConfidenceLevel = (float)result.Confidence;
                bestplate.Text = result.PlateNumber;
                bestplate.IsSuccess = result.IsSuccess;
                licensePlateList.Add(bestplate);
                return licensePlateList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<LprResponseModel> SendDataAsync(HttpRequestMessage request)
        {
            try
            {
                var response = await _client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    try
                    {
                        LprResponseModel responseModel = JsonConvert.DeserializeObject<LprResponseModel>(responseContent);
                        responseModel.IsSuccess = true;
                        return responseModel;
                    }
                    catch (JsonException jex)
                    {
                        LogHelper.Log(logType: LogHelper.EmLogType.ERROR, objectLogType: LogHelper.EmObjectLogType.System, obj: jex);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Log(logType: LogHelper.EmLogType.ERROR, objectLogType: LogHelper.EmObjectLogType.System, obj: ex);
                    }
                    return new LprResponseModel();
                }
                else
                {
                    LogHelper.Log(logType: LogHelper.EmLogType.ERROR, objectLogType: LogHelper.EmObjectLogType.System, description: "KztekLPR2 Read from bitmap: connection error");
                    return new LprResponseModel();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(logType: LogHelper.EmLogType.ERROR, objectLogType: LogHelper.EmObjectLogType.System, obj: ex);
                throw;
            }

        }

        public void ReadFromBuffer(int pBuffer, int width, int height, int stride, int bpp, int customData)
        {
            
        }

        public void ReadFromDIB(int hDIB, int customData)
        {
           
        }

        public LicensePlateCollection ReadFromFile(string fileName, int customData, ref int recognitionTime)
        {
            return null;
        }

        public void ReadFromMemFile(object fileData, int dataLen, int customData)
        {
           
        }

        public void Reset()
        {
            
        }

        public void SetCountryCode(string countryCode)
        {
        }

        public void SetMaskFromBitmap(int hBitmap)
        {
        }

        public void SetMaskFromBitmap(Bitmap Bitmap)
        {
        }

        public void SetMaskFromFile(string imageFileName)
        {
        }

        public void SetScanRectangle(int left, int top, int right, int bottom)
        {
        }
        private Image ImageFromBase64(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }
            return image;
        }
    }
}
