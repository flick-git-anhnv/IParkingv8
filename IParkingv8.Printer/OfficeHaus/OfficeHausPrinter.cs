using IParkingv8.Printer.DefaultPrinters;

namespace IParkingv8.Printer.OfficeHaus
{
    public class OfficeHausPrinter : DefaultPrinter
    {
        public void PrintQR(string baseContentstring, string base64QR, string userId)
        {
            string printContent = baseContentstring.Replace("$image_data", base64QR);
             printContent = printContent.Replace("$user_id", userId);
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
    }
}
