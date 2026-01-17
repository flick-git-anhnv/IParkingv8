using iParkingv5.LedDisplay.Enums;

namespace ILedv8.UserControls
{
    public partial class ucLedLine : UserControl
    {
        public int lineIndex = 1;
        public ucLedLine(int lineIndex)
        {
            InitializeComponent();
            this.lineIndex = lineIndex;
            lblLineName.Text = "Dòng " + lineIndex.ToString();
        }

        public void UpdateCount(int count, string format, int fontSize, EmLedColor color, bool isSuccess, string errorMessage)
        {
            lblCount.Invoke(new Action(() =>
            {
                lblCount.Text = count.ToString(format);
                lblCount.Font = new Font(lblCount.Font.FontFamily, fontSize * 2, lblCount.Font.Style);
                switch (color)
                {
                    case EmLedColor.RED:
                        lblCount.ForeColor = Color.DarkRed;
                        lblCount.BackColor = Color.White;
                        break;
                    case EmLedColor.GREEN:
                        lblCount.ForeColor = Color.DarkGreen;
                        lblCount.BackColor = Color.White;
                        break;
                    case EmLedColor.YELLOW:
                        lblCount.ForeColor = Color.Yellow;
                        lblCount.BackColor = Color.White;
                        break;
                    case EmLedColor.BLUE:
                        lblCount.ForeColor = Color.Blue;
                        lblCount.BackColor = Color.White;
                        break;
                    case EmLedColor.PURPLE:
                        lblCount.ForeColor = Color.Purple;
                        lblCount.BackColor = Color.White;
                        break;
                    case EmLedColor.GREEN_BLUE:
                        lblCount.ForeColor = Color.FromArgb(0, 192, 192);
                        lblCount.BackColor = Color.White;
                        break;
                    case EmLedColor.WHITE:
                        lblCount.ForeColor = Color.White;
                        lblCount.BackColor = Color.Black;
                        break;
                    default:
                        break;
                }
                if (isSuccess)
                {
                    pictureBox1.Image = Properties.Resources.ball_green;
                }
                else
                {
                    pictureBox1.Image = Properties.Resources.ball_red;
                }
                toolTip1.SetToolTip(pictureBox1, errorMessage);
                toolTip1.SetToolTip(lblCount, errorMessage);
                toolTip1.SetToolTip(lblLineName, errorMessage);
            }));
        }

    }
}
