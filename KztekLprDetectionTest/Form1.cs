using iParkingv5.LprDetecter.LprDetecters;

namespace KztekLprDetectionTest
{
    public partial class Form1 : Form
    {
        private Image? selectedImage = null;
        private bool isDrawing = false;

        public Form1()
        {
            InitializeComponent();

            this.Load += Form1_Load;
        }

        private async void Form1_Load(object? sender, EventArgs e)
        {
            var LprConfig = new Kztek.Object.LprConfig()
            {
                LPRDetecterType = Kztek.Object.LprDetecter.EmLprDetecter.AmericalLpr,
                Url = "http://kztek-lpr.demo.kztek.io.vn/alpr"
            };
            AppData.LprDetecter = LprFactory.CreateLprDetecter(LprConfig, null);
            await AppData.LprDetecter!.CreateLprAsync(LprConfig);
            //AppData.carANPR1.NewError += carANPR_NewError;
            //string s = "demo";
            //AppData.carANPR1.LPREngineProductKey = s;
            //AppData.carANPR1.EnableLPREngine2 = true;
            //AppData.carANPR1.CreateLPREngine();

            //AppData.motoANPR1.NewError += carANPR_NewError;
            //AppData.motoANPR1.LPREngineProductKey = s;
            //AppData.motoANPR1.EnableLPREngine2 = true;
            //AppData.motoANPR1.CreateLPREngine();

            //AppData.motorANPRAI.CreateLPREngine();
            //AppData.carANPRAI.CreateLPREngine();
        }
        //private void carANPR_NewError(object sender, Kztek.LPR.ErrorEventArgs e)
        //{
        //    //MessageBox.Show("Car ANPR error: " + e.ErrorString);
        //}

        private void btnCreateLPR_Click(object sender, EventArgs e)
        {

        }

        List<string> files = new List<string>();
        int lastIndex = 0;
        private async void btnDetect_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveErrorPic_Click(object sender, EventArgs e)
        {
            //string resultPath = txtOutputPath.Text;
            //Directory.CreateDirectory(resultPath + "\\" + DateTime.Now.ToString("yyyy_MM_dd") + "\\FALSE");
            //string detectPlate = dgvData.CurrentRow.Cells[4].Value?.ToString();
            //if (!string.IsNullOrEmpty(detectPlate))
            //{
            //    string savePath = resultPath + "\\" + DateTime.Now.ToString("yyyy_MM_dd") + "\\FALSE\\" + detectPlate + ".jpg";
            //    picInput.Image.Save(savePath, ImageFormat.Jpeg);
            //}
        }

        bool isStop = false;
        private void btnStop_Click(object sender, EventArgs e)
        {
            isStop = true;
        }
    }
}
