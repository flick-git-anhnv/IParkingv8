using iParkingv5.LedDisplay.Enums;
using Kztek.Object;
using Kztek.Tool;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace iParkingv5.LedDisplay.LEDs.HuiduLeds
{
    public class HuiduLed : IDisplayLED
    {
        #region Properties

        public SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        public Led led { get; set; }
        public string Comport { get; set; } = string.Empty;
        public int Baudrate { get; set; }
        int m_nSendType;                // Send type
        IntPtr m_pSendParams;           // Params
        int nAreaID1 = 0;
        int nAreaID2 = 0;
        IntPtr pNULL = new IntPtr(0);

        #endregion

        public bool Connect(Led led)
        {
            this.Comport = led.comport;
            this.Baudrate = led.baudrate;
            this.led = led;

            m_nSendType = 0;
            string strParams = led.comport;
            m_pSendParams = Marshal.StringToHGlobalUni(strParams);

            return true;
        }

        public async void SendToLED(ParkingData parkingData, LedDisplayConfig ledDisplayConfig)
        {
            await semaphoreSlim.WaitAsync();

            IntPtr pNULL = new IntPtr(0);

            foreach (var item in ledDisplayConfig.LedDisplaySteps)
            {
                foreach (KeyValuePair<int, LineConfig> itemDetail in item.Value.DisplayDatas)
                {
                    switch (itemDetail.Value.DisplayValue)
                    {
                        case EmLedDisplayValue.NONE:
                            itemDetail.Value.Data = string.Empty;
                            break;
                        case EmLedDisplayValue.CARD_NUMBER:
                            itemDetail.Value.Data = parkingData.CardNumber;
                            break;
                        case EmLedDisplayValue.CARD_NO:
                            itemDetail.Value.Data = parkingData.CardNo;
                            break;
                        case EmLedDisplayValue.CARD_TYPE:
                            itemDetail.Value.Data = parkingData.CardType;
                            break;
                        case EmLedDisplayValue.EVENT_STATUS:
                            itemDetail.Value.Data = parkingData.EventStatus;
                            break;
                        case EmLedDisplayValue.PLATE:
                            itemDetail.Value.Data = parkingData.Plate;
                            break;
                        case EmLedDisplayValue.DATETIME_IN:
                            itemDetail.Value.Data = parkingData.DatetimeIn?.ToString("HH:mm:ss") ?? "";
                            break;
                        case EmLedDisplayValue.DATETIMEOUT:
                            itemDetail.Value.Data = parkingData.DatetimeIn?.ToString("HH:mm:ss") ?? "";
                            break;
                        case EmLedDisplayValue.MONEY:
                            string temp = TextFormatingTool.GetMoneyFormat(parkingData.Money.ToString());
                            itemDetail.Value.Data = temp.Substring(0, temp.Length - 2).Trim();
                            break;
                        case EmLedDisplayValue.Option:
                            break;
                        default:
                            itemDetail.Value.Data = string.Empty;
                            break;
                    }
                }
                await DisplayData(item.Value.DisplayDatas);
                await Task.Delay(TimeSpan.FromMilliseconds(item.Value.DelayTime));
            }
            //END:
            {
                semaphoreSlim.Release();
                GC.Collect();
                return;
            }
        }

        public async Task<bool> DisplayData(Dictionary<int, LineConfig> data)
        {
            IntPtr pNULL = new IntPtr(0);

            int nErrorCode = -1;
            // 1. Create a screen
            int nColor = 1;
            int nGray = 1;
            int nCardType = 0;

            int nWidth = 64;
            int nHeight = 32;


            int nRe = CSDKExport.Hd_CreateScreen(nWidth, nHeight, nColor, nGray, nCardType, pNULL, 0);
            if (nRe != 0)
            {
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
            }

            // 2. Add program to screen
            int nProgramID = CSDKExport.Hd_AddProgram(pNULL, 0, 0, pNULL, 0);
            if (nProgramID == -1)
            {
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
            }

            int nAreaWidth = 64;
            int nAreaHeight = 32;

            // 3. Add Area to program
            nAreaID1 = CSDKExport.Hd_AddArea(nProgramID, 0, 0, 64, 16, pNULL, 0, 0, pNULL, 0);
            nAreaID2 = CSDKExport.Hd_AddArea(nProgramID, 0, 16, 64, 16, pNULL, 0, 0, pNULL, 0);
            if (nAreaID1 == -1 || nAreaID2 == -1)
            {
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
            }

            IntPtr pFontName = Marshal.StringToHGlobalUni("Arial");
            int nTextColor = CSDKExport.Hd_GetColor(255, 0, 0);
            int nTextStyle = 0x0004 | 0x0100;

            List<int> dataIds = new List<int>() { nAreaID1, nAreaID2 };
            int i = 0;
            foreach (KeyValuePair<int, LineConfig> item in data)
            {
                IntPtr pText = Marshal.StringToHGlobalUni(item.Value.Data);
                var nAreaItemID = CSDKExport.Hd_AddSimpleTextAreaItem((i > dataIds.Count ? dataIds[0] : dataIds[i]), pText, nTextColor, 0, nTextStyle, pFontName,
                  item.Value.FontSize, 0, 30, 201, 3, pNULL, 0);
                if (nAreaItemID == -1)
                {
                    Marshal.FreeHGlobal(pText);
                    Marshal.FreeHGlobal(pFontName);
                    return false;
                }
                Marshal.FreeHGlobal(pText);
                i++;
            }
            Marshal.FreeHGlobal(pFontName);

            // 5. Send to device 
            nRe = CSDKExport.Hd_SendScreen(m_nSendType, m_pSendParams, pNULL, pNULL, 0);
            if (nRe != 0)
            {
                nErrorCode = CSDKExport.Hd_GetSDKLastError();
            }
            return true;
        }

        public bool DisplayCount(int carCount, int motobikeCount)
        {
            try
            {
                IntPtr pNULL = new IntPtr(0);

                int nErrorCode = -1;
                // 1. Create a screen
                int nColor = 1;
                int nGray = 1;
                int nCardType = 0;

                int nWidth = 64;
                int nHeight = 32;


                int nRe = CSDKExport.Hd_CreateScreen(nWidth, nHeight, nColor, nGray, nCardType, pNULL, 0);
                if (nRe != 0)
                {
                    nErrorCode = CSDKExport.Hd_GetSDKLastError();
                }

                // 2. Add program to screen
                int nProgramID = CSDKExport.Hd_AddProgram(pNULL, 0, 0, pNULL, 0);
                if (nProgramID == -1)
                {
                    nErrorCode = CSDKExport.Hd_GetSDKLastError();
                }

                int nAreaWidth = 64;
                int nAreaHeight = 32;

                // 3. Add Area to program
                nAreaID1 = CSDKExport.Hd_AddArea(nProgramID, 0, 0, 64, 16, pNULL, 0, 0, pNULL, 0);
                nAreaID2 = CSDKExport.Hd_AddArea(nProgramID, 0, 16, 64, 16, pNULL, 0, 0, pNULL, 0);
                if (nAreaID1 == -1 || nAreaID2 == -1)
                {
                    nErrorCode = CSDKExport.Hd_GetSDKLastError();
                }

                IntPtr pFontName = Marshal.StringToHGlobalUni("Arial");
                int nTextColor = CSDKExport.Hd_GetColor(255, 0, 0);
                int nTextStyle = 0x0004 | 0x0100;

                List<int> dataIds = new List<int>() { nAreaID1, nAreaID2 };
                List<string> displayData = new List<string>() { carCount.ToString(), motobikeCount.ToString() };
                int i = 0;
                foreach (var item in displayData)
                {
                    IntPtr pText = Marshal.StringToHGlobalUni(item);
                    var nAreaItemID = CSDKExport.Hd_AddSimpleTextAreaItem((i > dataIds.Count ? dataIds[0] : dataIds[i]), pText, nTextColor, 0, nTextStyle, pFontName, 12, 0, 30, 201, 3, pNULL, 0);
                    if (nAreaItemID == -1)
                    {
                        Marshal.FreeHGlobal(pText);
                        Marshal.FreeHGlobal(pFontName);
                        return false;
                    }
                    Marshal.FreeHGlobal(pText);
                    i++;
                }
                Marshal.FreeHGlobal(pFontName);

                // 5. Send to device 
                nRe = CSDKExport.Hd_SendScreen(m_nSendType, m_pSendParams, pNULL, pNULL, 0);
                if (nRe != 0)
                {
                    nErrorCode = CSDKExport.Hd_GetSDKLastError();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
