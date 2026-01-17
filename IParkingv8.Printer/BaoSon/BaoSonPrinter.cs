using iParkingv8.Object.Objects.Reports;

namespace IParkingv8.Printer.BaoSon
{
    internal class BaoSonPrinter : IPrinter
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
        public class ReportForSecurity
        {
            public long TotalCount { get; set; }
            public long TotalAmount { get; set; }
            public long TotalDiscount { get; set; }
            public long TotalPaid { get; set; }
            public long RealMoney { get; set; }
        }
        public string PrintRevenue(string baseContent, SearchRevenueReportResponse data)
        {
            Dictionary<string, ReportForSecurity> report = new Dictionary<string, ReportForSecurity>();
            foreach (var item in data.Data)
            {
                if (report.ContainsKey(item.Identifier))
                {
                    report[item.Identifier].TotalCount += item.Count;
                    report[item.Identifier].TotalAmount += item.Amount;
                    report[item.Identifier].TotalDiscount += item.Discount;
                    report[item.Identifier].TotalPaid += item.EntryAmount;
                    report[item.Identifier].RealMoney += Math.Max(item.Amount - item.Discount - item.EntryAmount, 0);
                }
                else
                {
                    report.Add(item.Identifier, new ReportForSecurity()
                    {
                        TotalCount = item.Count,
                        TotalAmount = item.Amount,
                        TotalDiscount = item.Discount,
                        TotalPaid = item.EntryAmount,
                        RealMoney = Math.Max(item.Amount - item.Discount - item.EntryAmount, 0)
                    });
                }
            }

            string tickets = "";
            int i = 1;
            foreach (KeyValuePair<string, ReportForSecurity> item in report.OrderBy(r => r.Key))
            {
                string revenueItems = GetRevenueItems(i.ToString(), item.Key, item.Value.TotalCount, item.Value.TotalAmount, item.Value.TotalDiscount, item.Value.TotalPaid, item.Value.RealMoney);
                tickets += revenueItems;
                i++;
            }
            string total = GetRevenueItems("", "Tổng", data.TotalCount, data.TotalAmount, data.TotalDiscount, data.TotalEntryAmount, data.TotalExitAmount);
            tickets += total;
            baseContent = baseContent.Replace("{$ticketListInHtml}", tickets);

            //var wbPrint = new WebBrowser();
            //wbPrint.DocumentCompleted += WbPrint_DocumentCompleted;
            //wbPrint.DocumentText = baseContent;

            return baseContent;
        }

        public string GetRevenueItems(string stt, string name, long count, long fee, long discount, long paid, long realFee)
        {
            return $@"<tr>
            <td width=""5%"" align=""center"" style=""padding: 2px; font-size: 10px; border: 0.5px solid black"">{stt}</td>
            <td width=""45%"" align=""left"" style=""padding: 2px; font-size: 10px; border: 0.5px solid black"">{name}</td>
            <td width=""10%"" align=""center"" style=""padding: 2px; font-size: 10px; border: 0.5px solid black"">{count}</td>
            <td width=""10%"" align=""center"" style=""padding: 2px; font-size: 10px; border: 0.5px solid black"">{fee.ToString("N0")}</td>
            <td width=""10%"" align=""center"" style=""padding: 2px; font-size: 10px; border: 0.5px solid black"">{discount.ToString("N0")}</td>
            <td width=""10%"" align=""center"" style=""padding: 2px; font-size: 10px; border: 0.5px solid black"">{paid.ToString("N0")}</td>
            <td width=""10%"" align=""center"" style=""padding: 2px; font-size: 10px; border: 0.5px solid black"">{realFee.ToString("N0")}</td>
          </tr>";
        }

        public string PrintShiftHandOver(string baseContent, ShiftHandOverReport report, string user, DateTime startTime, DateTime endTime, long realFeeSecurity)
        {
            long totalVao = 0;
            long totalRa = 0;
            long totalAmount = 0;
            long totalRealFee = 0;
            long totalDiscount = 0;
            int i = 0;
            string tickets = "";
            foreach (var item in report.Report)
            {
                totalVao += item.Value.Vao;
                totalRa += item.Value.Ra;

                totalAmount += item.Value.Amount;
                totalRealFee += item.Value.RealFee;
                totalDiscount += item.Value.Discount;

                i++;
                string revenueItems = GetShiftHandOverItems(i.ToString(), item.Key, item.Value.Vao, item.Value.Ra, item.Value.Amount, item.Value.RealFee, item.Value.Discount);
                tickets += revenueItems;
            }
            string total = GetShiftHandOverItems("", "Tổng", totalVao, totalRa, totalAmount, totalRealFee, totalDiscount);
            tickets += total;
            baseContent = baseContent.Replace("{$ticketListInHtml}", tickets);
            baseContent = baseContent.Replace("{$startTime}", startTime.ToString("dd/MM/yyyy, HH:mm"));
            baseContent = baseContent.Replace("{$endTime}", endTime.ToString("dd/MM/yyyy, HH:mm"));
            baseContent = baseContent.Replace("{$user}", user);
            return baseContent;
        }
        public string GetShiftHandOverItems(string stt, string name, long vao, long ra, long amount, long realFee, long discount)
        {
            return $@"<tr>
            <td width=""5%"" align=""center"" style=""padding: 2px; font-size: 10px; border: 0.5px solid black"">{stt}</td>
            <td width=""45%"" align=""left"" style=""padding: 2px; font-size: 10px; border: 0.5px solid black"">{name}</td>
            <td width=""10%"" align=""center"" style=""padding: 2px; font-size: 10px; border: 0.5px solid black"">{vao}</td>
            <td width=""10%"" align=""center"" style=""padding: 2px; font-size: 10px; border: 0.5px solid black"">{ra}</td>
            <td width=""10%"" align=""center"" style=""padding: 2px; font-size: 10px; border: 0.5px solid black"">{amount.ToString("N0")}</td>
            <td width=""10%"" align=""center"" style=""padding: 2px; font-size: 10px; border: 0.5px solid black"">{realFee.ToString("N0")}</td>
            <td width=""10%"" align=""center"" style=""padding: 2px; font-size: 10px; border: 0.5px solid black"">{discount.ToString("N0")}</td>
          </tr>";
        }
        #endregion End Private Function

    }
}