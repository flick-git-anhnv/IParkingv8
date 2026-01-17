using iParkingv8.Ultility.Style;
using System.ComponentModel;

namespace Kztek.Control8.UserControls.ReportUcs
{
    public partial class ucNavigator : UserControl
    {
        public delegate void OnPageChange(int pageIndex);
        public event OnPageChange onPageChangeEvent;

        private int maxPage = 0;
        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Max Page"), Description("Thiết lập số trang tối đa")]
        public int KZUI_MaxPage
        {
            get => maxPage;
            set
            {
                maxPage = value;
                if (maxPage == 0)
                {
                    this.Visible = false;
                }
                else
                {
                    this.Visible = true;
                }
                if (maxPage == 1)
                {
                    ucNextPage1.Enabled = false;
                    ucPreviousPage1.Enabled = false;
                }
                foreach (ucPage page in panelPages.Controls.OfType<ucPage>())
                {
                    if (page.KZUI_PageIndex > maxPage)
                    {
                        page.Enabled = false;
                    }
                    else
                    {
                        page.Enabled = true;
                    }
                }
            }
        }

        private int currentPage = 1;
        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Current Page"), Description("Thiết lập số trang hiện tại")]
        public int KZUI_CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                ucPreviousPage1.Enabled = currentPage != 1;
                ucNextPage1.Enabled = currentPage != maxPage;

                foreach (ucPage page in panelPages.Controls.OfType<ucPage>())
                {
                    if (page.KZUI_PageIndex == currentPage)
                    {
                        page.IsActivePage = true;
                        this.ActiveControl = page;
                    }
                    else
                    {
                        page.IsActivePage = false;
                    }
                }
            }
        }

        private int totalRecord = 0;
        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Current Page"), Description("Thiết lập số trang hiện tại")]
        public int KZUI_TotalRecord
        {
            get => totalRecord;
            set
            {
                totalRecord = value;
                lblTotal.Text = KZUIStyles.CurrentResources.Total + totalRecord;
            }
        }

        public ucNavigator()
        {
            InitializeComponent();
            ucNextPage1.ClickPage += UcNextPage1_Click;
            ucPreviousPage1.ClickPage += UcPreviousPage1_Click;

            ucPage2.ClickPage += Page_ClickPage;
            ucPage3.ClickPage += Page_ClickPage;
            ucPage4.ClickPage += Page_ClickPage;
            ucPage1.ClickPage += UcPage1_ClickPage;
            ucPage5.ClickPage += UcPage5_ClickPage;
        }

        private void UcPage5_ClickPage(object? sender, EventArgs e)
        {
            if (currentPage == ((ucPage)sender).KZUI_PageIndex) return;

            var pageIndex = ((ucPage)sender).KZUI_PageIndex;

            foreach (ucPage page in panelPages.Controls.OfType<ucPage>())
            {
                page.KZUI_PageIndex += Math.Min(4, maxPage - pageIndex);
            }
            this.KZUI_CurrentPage = pageIndex;
            onPageChangeEvent?.Invoke(currentPage);
        }
        private void UcPage1_ClickPage(object? sender, EventArgs e)
        {
            if (currentPage == ((ucPage)sender).KZUI_PageIndex) return;

            var pageIndex = ((ucPage)sender).KZUI_PageIndex;
            foreach (ucPage page in panelPages.Controls.OfType<ucPage>())
            {
                page.KZUI_PageIndex -= Math.Min(4, ucPage1.KZUI_PageIndex - 1);
            }
            this.KZUI_CurrentPage = pageIndex;
            onPageChangeEvent?.Invoke(currentPage);
        }
        private void Page_ClickPage(object? sender, EventArgs e)
        {
            if (currentPage == ((ucPage)sender).KZUI_PageIndex) return;
            this.KZUI_CurrentPage = ((ucPage)sender).KZUI_PageIndex;
            onPageChangeEvent?.Invoke(currentPage);
        }
        private void UcPreviousPage1_Click(object? sender, EventArgs e)
        {
            if (ucPage1.KZUI_PageIndex > 1)
            {
                foreach (ucPage page in panelPages.Controls.OfType<ucPage>())
                {
                    page.KZUI_PageIndex--;
                }
            }
            this.KZUI_CurrentPage = currentPage - 1;
            onPageChangeEvent?.Invoke(currentPage);
        }
        private void UcNextPage1_Click(object? sender, EventArgs e)
        {
            if (this.KZUI_CurrentPage == ucPage5.KZUI_PageIndex)
            {
                foreach (ucPage page in panelPages.Controls.OfType<ucPage>())
                {
                    page.KZUI_PageIndex++;
                }
            }
            this.KZUI_CurrentPage = currentPage + 1;
            onPageChangeEvent?.Invoke(currentPage);
        }
        public void Reset()
        {
            this.maxPage = 0;
            this.currentPage = 0;
            ucPage1.KZUI_PageIndex = 1;
            ucPage2.KZUI_PageIndex = 2;
            ucPage3.KZUI_PageIndex = 3;
            ucPage4.KZUI_PageIndex = 4;
            ucPage5.KZUI_PageIndex = 5;
            this.KZUI_TotalRecord = 0;
        }
    }
}