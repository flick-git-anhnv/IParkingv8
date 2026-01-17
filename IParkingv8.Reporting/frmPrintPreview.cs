using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace IParkingv8.Reporting
{
    public partial class frmPrintPreview : Form, KzITranslate
    {
        #region Properties
        private string printContent;
        private WebBrowser webBrowserControl;
        #endregion

        #region Forms
        public frmPrintPreview(string printContent, int defaultCopiesCount)
        {
            InitializeComponent();
            Translate();
            InitUI(defaultCopiesCount);
            InitPropeties(printContent);
        }

        private void frmPrintPreview_Load(object sender, EventArgs e)
        {
            PrinterSettings printerSettings = new PrinterSettings();
            string defaultPrinterName = printerSettings.PrinterName;

            PrinterSettings.StringCollection printers = PrinterSettings.InstalledPrinters;
            foreach (string printer in printers)
            {
                cbPrints.Items.Add(printer);
                if (printer == defaultPrinterName)
                {
                    cbPrints.SelectedIndex = cbPrints.Items.Count - 1;
                }
            }
            if (cbPrints.SelectedIndex < 0)
            {
                cbPrints.SelectedIndex = cbPrints.Items.Count > 0 ? 0 : -1;
            }

            webBrowserControl = new WebBrowser();
            webBrowserControl.DocumentText = printContent;
            webBrowserControl.DocumentCompleted += WebBrowserControl_DocumentCompleted;
            webBrowserControl.Dock = DockStyle.Fill;
            webBrowserControl.BringToFront();

            panelContent.Controls.Add(webBrowserControl);
            webBrowserControl.ScrollBarsEnabled = false;
        }
        public static class myPrinters
        {
            [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool SetDefaultPrinter(string Name);
        }
        #endregion

        #region Controls In Form
        private async Task<bool> BtnPrint_Click(object? sender)
        {
            myPrinters.SetDefaultPrinter(cbPrints.Text);
            PrintReport(WbPrint_DocumentCompleted);
            this.Close();
            return true;
        }
        private async Task<bool> BtnCancel_Click(object? sender)
        {
            this.Close();
            return true;
        }
        private void WebBrowserControl_DocumentCompleted(object? sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowserControl.Document.Body.Style = "overflow:hidden";
        }
        #endregion

        #region Private Funciton
        private void PrintReport(WebBrowserDocumentCompletedEventHandler WbPrint_DocumentCompleted)
        {
            WebBrowser wbPrint = new WebBrowser();
            wbPrint.DocumentCompleted += WbPrint_DocumentCompleted;
            wbPrint.DocumentText = printContent;
        }
        public void WbPrint_DocumentCompleted(object? sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                var browser = (WebBrowser)sender;
                for (int i = 0; i < numCopies.Value; i++)
                {
                    browser.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        public void Translate()
        {
            lblPrinter.Text = KZUIStyles.CurrentResources.Printer;
            lblQuantity.Text = KZUIStyles.CurrentResources.Quantity;

            btnPrint.Text = KZUIStyles.CurrentResources.Print;
            btnCancel.Text = KZUIStyles.CurrentResources.Cancel;
        }
        private void InitUI(int defaultCopiesCount)
        {
            numCopies.Value = defaultCopiesCount;

            btnCancel.OnClickAsync += BtnCancel_Click;
            btnPrint.OnClickAsync += BtnPrint_Click;
        }
        private void InitPropeties(string printContent)
        {
            this.printContent = printContent;
        }
    }
}
