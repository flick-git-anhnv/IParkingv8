using FontAwesome.Sharp;
using iParkingv8.Ultility;
using System.ComponentModel;

namespace Kztek.Control8.UserControls
{
    public partial class KZUI_UcResult : UserControl
    {
        #region Properties
        public enum EmResultType
        {
            SUCCESS, WARNING, ERROR, PROCESS
        }
        private EmResultType ResultType = EmResultType.SUCCESS;
        private IconChar SuccessIcon = IconChar.CheckCircle;
        private IconChar WarningIcon = IconChar.Warning;
        private IconChar ErrorIcon = IconChar.Close;

        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI_IsSuccessResult"), Description("Indicates whether the result is successful.")]
        public EmResultType KZUI_ResultType
        {
            get => ResultType;
            set
            {
                ResultType = value;
                SetResult(value);
            }
        }

        private EmControlSizeMode sizeMode = EmControlSizeMode.MEDIUM;
        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Size Mode"), Description("Chế độ hiển thị -  SMALL : 24px -  MEDIUM : 32px -  LARGE : 40 px")]
        private EmControlSizeMode KZUI_ControlSizeMode
        {
            get => sizeMode;
            set
            {
                sizeMode = value;
                switch (sizeMode)
                {
                    case EmControlSizeMode.SMALL:
                        lblMessage.Font = new Font(lblMessage.Font.Name, SizeManagement.SMALL_FONT_SIZE, lblMessage.Font.Style, GraphicsUnit.Pixel);
                        this.MinimumSize = this.MaximumSize = new Size(0, SizeManagement.SMALL_HEIGHT);
                        break;
                    case EmControlSizeMode.MEDIUM:
                        lblMessage.Font = new Font(lblMessage.Font.Name, SizeManagement.MEDIUM_FONT_SIZE, lblMessage.Font.Style, GraphicsUnit.Pixel);
                        this.MinimumSize = this.MaximumSize = new Size(0, SizeManagement.MEDIUM_HEIGHT);
                        break;
                    case EmControlSizeMode.LARGE:
                        lblMessage.Font = new Font(lblMessage.Font.Name, SizeManagement.LARRGE_FONT_SIZE, lblMessage.Font.Style, GraphicsUnit.Pixel);
                        this.MinimumSize = this.MaximumSize = new Size(0, SizeManagement.LARRGE_HEIGHT);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region Forms
        public KZUI_UcResult()
        {
            InitializeComponent();
            lblMessage.ForeColor = Color.White;
            lblMessage.AutoEllipsis = false;
            this.ForeColor = Color.White;
            this.BackColor = ColorManagement.ControlBackgroud;
            this.SizeChanged += UcResult_SizeChanged;
        }
        private void UcResult_SizeChanged(object? sender, EventArgs e)
        {
            lblMessage.MaximumSize = new Size(this.Width - this.Padding.Left - this.Padding.Right, this.Height);
            switch (sizeMode)
            {
                case EmControlSizeMode.SMALL:
                    lblMessage.Font = new Font(lblMessage.Font.Name, 12, lblMessage.Font.Style, GraphicsUnit.Pixel);
                    lblMessage.Height = SizeManagement.SMALL_HEIGHT;
                    lblMessage.IconSize = 15;

                    break;
                case EmControlSizeMode.MEDIUM:
                    lblMessage.Font = new Font(lblMessage.Font.Name, 16, lblMessage.Font.Style, GraphicsUnit.Pixel);
                    lblMessage.Height = SizeManagement.MEDIUM_HEIGHT;
                    lblMessage.IconSize = 20;
                    break;
                case EmControlSizeMode.LARGE:
                    lblMessage.Font = new Font(lblMessage.Font.Name, 18, lblMessage.Font.Style, GraphicsUnit.Pixel);
                    lblMessage.Height = SizeManagement.LARRGE_HEIGHT;
                    lblMessage.IconSize = 32;
                    break;
                default:
                    break;
            }

        }
        public void Init(EmControlSizeMode controlSizeMode, string oemName)
        {
            lblMessage.Text = oemName;
            lblMessage.IconChar = IconChar.None;
            this.KZUI_ControlSizeMode = controlSizeMode;
        }
        #endregion

        #region Private Function
        private void SetResult(EmResultType resultType)
        {
            switch (resultType)
            {
                case EmResultType.SUCCESS:
                    if (lblMessage.IconChar != SuccessIcon)
                    {
                        lblMessage.IconChar = SuccessIcon;
                        lblMessage.BackColor = ColorManagement.SuccessColor;
                    }
                    break;
                case EmResultType.WARNING:
                    if (lblMessage.IconChar != WarningIcon)
                    {
                        lblMessage.IconChar = WarningIcon;
                        lblMessage.BackColor = ColorManagement.ErrorColor;
                    }
                    break;
                case EmResultType.ERROR:
                    if (lblMessage.IconChar != ErrorIcon)
                    {
                        lblMessage.IconChar = ErrorIcon;
                        lblMessage.BackColor = ColorManagement.ErrorColor;
                    }
                    break;
                case EmResultType.PROCESS:
                    if (lblMessage.IconChar != IconChar.None)
                    {
                        lblMessage.IconChar = IconChar.None;
                        lblMessage.BackColor = ColorManagement.ProcessColor;
                    }
                    break;
                default:
                    break;
            }
            if (lblMessage.FlatAppearance.MouseOverBackColor != lblMessage.BackColor)
            {
                lblMessage.FlatAppearance.MouseOverBackColor = lblMessage.BackColor;
                lblMessage.FlatAppearance.MouseDownBackColor = lblMessage.BackColor;
            }
            //panelMain.Refresh();
        }
        #endregion

        #region Public Function
        public void DisplayResult(EmResultType resultType, string message)
        {
            if (this.IsHandleCreated && lblMessage.IsHandleCreated && lblMessage.InvokeRequired)
            {
                lblMessage.Invoke(new Action(() =>
                {
                    DisplayResultInternal(resultType, message);
                }));
            }
            else
            {
                DisplayResultInternal(resultType, message);
            }
        }
        private void DisplayResultInternal(EmResultType resultType, string message)
        {
            SetResult(resultType);

            if (message.Contains("\r\n"))
            {
                this.MinimumSize = new Size(0, 80);
                if (this.Parent is not null)
                {
                    this.Parent.MinimumSize = new Size(0, 80);
                    this.Parent.Height = 60;
                    lblMessage.IconSize = 32;
                }
                lblMessage.Text = message;
                lblMessage.Font = new Font(lblMessage.Font.Name, SizeManagement.MEDIUM_FONT_SIZE, lblMessage.Font.Style, GraphicsUnit.Pixel);
                lblMessage.Refresh();
            }
            else
            {
                this.MinimumSize = new Size(0, 32);
                if (this.Parent is not null)
                {
                    this.Parent.MinimumSize = new Size(0, 32);
                    this.Parent.Height = 32;
                    lblMessage.Font = new Font(lblMessage.Font.Name, 16, lblMessage.Font.Style, GraphicsUnit.Pixel);
                    lblMessage.Height = SizeManagement.MEDIUM_HEIGHT;
                    lblMessage.IconSize = 20;
                }
                lblMessage.Text = message;
                lblMessage.Refresh();
            }
        }

        #endregion
    }
}
