using iParkingv8.Object.Objects.Reports;

namespace IParkingv8.Printer.DefaultPrinters
{
    public class DefaultPrinter : IPrinter
    {
        #region Public Function
        public void PrintOffliceInvoice(string baseContent, string cardName, string cardGroupName, Image img,
                                      DateTime dateTimeIn, DateTime dateTimeOut,
                                      string plate = "", string moneyStr = "",
                                      long moneyInt = 0, string receiveBillCode = "")
        {
        }

        public void PrintOnlineInvoice(string baseContent, string cardName, string cardGroupName, Image img,
                                      DateTime dateTimeIn, DateTime dateTimeOut,
                                      string plate = "", string moneyStr = "",
                                      long moneyInt = 0, string receiveBillCode = "")
        {
        }

        public void PrintPhieuThu(string baseContent, string cardName, string cardGroupName, Image img,
                                      DateTime dateTimeIn, DateTime dateTimeOut,
                                      string plate = "", string moneyStr = "",
                                      long moneyInt = 0, string receiveBillCode = "")
        {
            string printContent = GetPhieuThuContent(baseContent, cardName, cardGroupName, img, dateTimeIn, dateTimeOut, plate, moneyStr, moneyInt);
            var wbPrint = new WebBrowser();
            wbPrint.DocumentCompleted += WbPrint_DocumentCompleted;
            wbPrint.DocumentText = printContent;
        }
        private void WbPrint_DocumentCompleted(object? sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                var browser = (WebBrowser)sender!;
                browser.Print();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion End Public Function

        #region Private Function
        private string GetPhieuThuContent(string baseContent, string cardName, string cardGroupName, Image img,
                                      DateTime dateTimeIn, DateTime dateTimeOut,
                                      string plate = "", string moneyStr = "",
                                      long moneyInt = 0, string receiveBillCode = "")
        {
            //baseContent = baseContent.Replace("$companyTaxCode", StaticPool.TaxCode);
            //baseContent = baseContent.Replace("$companyTaxCode", StaticPool.TaxCode);
            //baseContent = baseContent.Replace("$companyAddress", StaticPool.CompanyAddress);
            //baseContent = baseContent.Replace("$companyName", StaticPool.CompanyName);

            baseContent = baseContent.Replace("$day", DateTime.Now.Day.ToString("00"));
            baseContent = baseContent.Replace("$month", DateTime.Now.Month.ToString("00"));
            baseContent = baseContent.Replace("$year", DateTime.Now.Year.ToString("0000"));

            baseContent = baseContent.Replace("$datetimeIn", dateTimeIn.ToString("dd/MM/yyyy HH:mm:ss"));
            baseContent = baseContent.Replace("$datetimeOut", dateTimeOut.ToString("dd/MM/yyyy HH:mm:ss"));
            baseContent = baseContent.Replace("$plateNumber", plate);
            baseContent = baseContent.Replace("$cardName", cardName);
            baseContent = baseContent.Replace("$cardGroupName", cardGroupName);
            var ParkingTime = dateTimeOut - dateTimeIn;
            string formattedTime = "";
            if (ParkingTime.TotalDays > 1)
            {
                formattedTime = string.Format("{0} ngày {1} giờ {2} phút {3} giây", ParkingTime.Days, ParkingTime.Hours, ParkingTime.Minutes, ParkingTime.Seconds);
            }
            else
            {
                formattedTime = string.Format("{0} giờ {1} phút {2} giây", ParkingTime.Hours, ParkingTime.Minutes, ParkingTime.Seconds);
            }
            baseContent = baseContent.Replace("$parking_time", formattedTime);
            if (img != null)
            {
                try
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        img.Save(m, img.RawFormat);
                        byte[] imageBytes = m.ToArray();

                        // Convert byte[] to Base64 String
                        string base64String = Convert.ToBase64String(imageBytes);
                        baseContent = baseContent.Replace("$image_data", base64String);
                    }
                }
                catch (Exception)
                {
                }
            }

            baseContent = baseContent.Replace("$money", moneyStr);
            return baseContent;
        }

        public string PrintRevenue(string templatePath, SearchRevenueReportResponse revenue)
        {
            return "";
        }

        public string PrintShiftHandOver(string baseContent, ShiftHandOverReport report, string user, DateTime startTime, DateTime endTime, long realFeeSecurity)
        {
            return "";
        }
        #endregion End Private Function

    }
}
