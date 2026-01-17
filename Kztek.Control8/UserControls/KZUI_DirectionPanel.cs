using Guna.UI2.WinForms;
using System.ComponentModel;

namespace Kztek.Control8.UserControls
{
    public class KZUI_DirectionPanel : Guna2Panel
    {
        private int spacing = 8;
        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Spacing"), Description("Cài đặt khoảng cách giữa các control")]
        public int Spacing
        {
            get => spacing;
            set
            {
                RefreshUI(value);
            }
        }

        private bool spaceBetween = false;
        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Space Between"), Description("Cài đặt chỉ áp dụng khoảng cách giữa các control")]
        public bool SpaceBetween
        {
            get => spaceBetween;
            set
            {
                spaceBetween = value;
                RefreshUI(spacing);
            }
        }

        private bool autoPaint = false;
        [Browsable(true)]
        [DefaultValue(false)]
        [Category("★ KZUI"), DisplayName("★ KZUI Auto Paint"), Description("Tự đông cập nhật hiển thị trong DESIGN MODE")]
        public bool AutoPaint
        {
            get => autoPaint;
            set
            {
                autoPaint = value;
                if (value)
                {
                    this.Paint += DirectionPanel_Paint;
                    this.SizeChanged -= KZUI_DirectionPanel_SizeChanged;
                }
                else
                {
                    this.Paint -= DirectionPanel_Paint;
                    this.SizeChanged += KZUI_DirectionPanel_SizeChanged;
                }
            }
        }

        private EmControlDirection controlDirection = EmControlDirection.VERTICAL;
        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Control Direction"), Description("Hướng hiển thị")]
        public EmControlDirection KZUI_ControlDirection
        {
            get => this.controlDirection;
            set
            {
                ChangeDirection(value);
            }
        }

        private CustomizableSpacing? customizableSpacing;
        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Customizable Spacing TOP,BOTTOM,LEFT,RIGHT"), Description("Custom Padding")]
        public string CustomSpacing
        {
            get => customizableSpacing?.ToString() ?? "";
            set
            {
                this.customizableSpacing = CustomizableSpacing.TryParse(value);
                RefreshUI(spacing);
            }
        }

        public KZUI_DirectionPanel() : base()
        {
            this.DoubleBuffered = true;
            RefreshUI(Spacing);
            this.AutoPaint = false;
        }

        private void KZUI_DirectionPanel_SizeChanged(object? sender, EventArgs e)
        {
            RefreshUI(spacing);
        }

        private void DirectionPanel_Paint(object? sender, PaintEventArgs e)
        {
            RefreshUI(spacing);
        }
        public void ChangeDirection(EmControlDirection controlDirection)
        {
            this.controlDirection = controlDirection;
            switch (controlDirection)
            {
                case EmControlDirection.HORIZONTAL:
                    this.Padding = this.customizableSpacing == null ?
                        new Padding(Spacing, Spacing, 0, Spacing) :
                        new Padding(customizableSpacing.Left, customizableSpacing.Top,
                        customizableSpacing.Right, customizableSpacing.Bottom);

                    if (SpaceBetween)
                    {
                        for (int i = 0; i < this.Controls.Count; i++)
                        {
                            Control control = this.Controls[i];
                            control.Dock = DockStyle.Left;
                            if (i == 0)
                            {
                                control.Padding = new Padding(0, 0, 0, 0);
                            }
                            else
                            {
                                control.Padding = new Padding(0, 0, Spacing, 0);
                            }
                        }
                    }
                    else
                    {
                        foreach (Control control in this.Controls)
                        {
                            control.Dock = DockStyle.Left;
                            control.Padding = new Padding(0, 0, Spacing, 0);
                        }
                    }
                    break;

                case EmControlDirection.VERTICAL:
                    this.Padding = this.customizableSpacing == null ?
                    new Padding(Spacing, spacing, spacing, 0) :
                    new Padding(customizableSpacing.Left, customizableSpacing.Top,
                    customizableSpacing.Right, customizableSpacing.Bottom);

                    if (spaceBetween)
                    {
                        for (int i = 0; i < this.Controls.Count; i++)
                        {
                            Control control = this.Controls[i];
                            control.Dock = DockStyle.Top;
                            if (i == 0)
                            {
                                control.Padding = new Padding(0, 0, 0, 0);
                            }
                            else
                            {
                                control.Padding = new Padding(0, 0, 0, spacing);
                            }
                        }
                    }
                    else
                        foreach (Control control in this.Controls)
                        {
                            control.Dock = DockStyle.Top;
                            control.Padding = new Padding(0, 0, 0, spacing);
                        }
                    break;
                default:
                    break;
            }
            ResponsiveControl();
        }

        private void ResponsiveControl()
        {
            if (this.Controls.Count == 0)
            {
                return;
            }
            if (this.controlDirection == EmControlDirection.VERTICAL)
            {
                int fixedHeight = 0;
                int controlResponsiveCount = 0;
                foreach (Control control in this.Controls)
                {
                    if (control.Visible)
                    {
                        if (control.MaximumSize.Height > 0)
                            fixedHeight += control.MaximumSize.Height;
                        else
                        {
                            controlResponsiveCount++;
                        }
                    }
                }
                if (controlResponsiveCount == 0)
                {
                    controlResponsiveCount = 1;
                }
                if (SpaceBetween)
                {
                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        var control = this.Controls[i];
                        if (i == 0)
                        {
                            control.Height = (this.Height - this.Padding.Top - this.Padding.Bottom + spacing - fixedHeight) / controlResponsiveCount - spacing;
                        }
                        else
                        {
                            control.Height = (this.Height - this.Padding.Top - this.Padding.Bottom + spacing - fixedHeight) / controlResponsiveCount;
                        }
                    }
                }
                else
                {
                    foreach (Control control in this.Controls)
                    {
                        control.Height = (this.Height - this.Padding.Top - this.Padding.Bottom - fixedHeight) / controlResponsiveCount;
                    }
                }

            }
            else
            {
                int fixedWidth = 0;
                int controlResponsiveCount = 0;
                foreach (Control control in this.Controls)
                {
                    if (control.Visible)
                    {
                        if (control.MaximumSize.Width > 0)
                            fixedWidth += control.MaximumSize.Width;
                        else
                        {
                            controlResponsiveCount++;
                        }
                    }
                }
                if (controlResponsiveCount == 0)
                {
                    controlResponsiveCount = 1;
                }

                if (SpaceBetween)
                {
                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        var control = this.Controls[i];
                        if (i == 0)
                        {
                            control.Width = (this.Width - this.Padding.Right - this.Padding.Left + spacing - fixedWidth) / controlResponsiveCount - spacing;
                        }
                        else
                        {
                            control.Width = (this.Width - this.Padding.Right - this.Padding.Left + spacing - fixedWidth) / controlResponsiveCount;
                        }
                    }
                }
                else
                {
                    foreach (Control control in this.Controls)
                    {
                        control.Width = (this.Width - this.Padding.Right - this.Padding.Left - fixedWidth) / controlResponsiveCount;
                    }
                }
            }
        }
        public void RefreshUI(int spacing)
        {
            this.spacing = spacing;
            switch (controlDirection)
            {
                case EmControlDirection.HORIZONTAL:
                    this.Padding = this.customizableSpacing == null ?
                        new Padding(Spacing, Spacing, 0, Spacing) :
                        new Padding(customizableSpacing.Left, customizableSpacing.Top,
                        customizableSpacing.Right, customizableSpacing.Bottom);

                    if (SpaceBetween)
                    {
                        for (int i = 0; i < this.Controls.Count; i++)
                        {
                            Control control = this.Controls[i];
                            control.Dock = DockStyle.Left;
                            if (i == 0)
                            {
                                control.Padding = new Padding(0, 0, 0, 0);
                            }
                            else
                            {
                                control.Padding = new Padding(0, 0, Spacing, 0);
                            }
                        }
                    }
                    else
                    {
                        foreach (Control control in this.Controls)
                        {
                            control.Dock = DockStyle.Left;
                            control.Padding = new Padding(0, 0, Spacing, 0);
                        }
                    }
                    break;

                case EmControlDirection.VERTICAL:
                    this.Padding = this.customizableSpacing == null ?
                    new Padding(Spacing, spacing, spacing, 0) :
                    new Padding(customizableSpacing.Left, customizableSpacing.Top,
                                customizableSpacing.Right, customizableSpacing.Bottom);

                    if (spaceBetween)
                    {
                        for (int i = 0; i < this.Controls.Count; i++)
                        {
                            Control control = this.Controls[i];
                            control.Dock = DockStyle.Top;
                            if (i == 0)
                            {
                                control.Padding = new Padding(0, 0, 0, 0);
                            }
                            else
                            {
                                control.Padding = new Padding(0, 0, 0, spacing);
                            }
                        }
                    }
                    else
                        foreach (Control control in this.Controls)
                        {
                            control.Dock = DockStyle.Top;
                            control.Padding = new Padding(0, 0, 0, spacing);
                        }
                    break;
                default:
                    break;
            }
            ResponsiveControl();
        }
        public void RefreshUI()
        {
            switch (controlDirection)
            {
                case EmControlDirection.HORIZONTAL:
                    this.Padding = this.customizableSpacing == null ?
                        new Padding(Spacing, Spacing, 0, Spacing) :
                        new Padding(customizableSpacing.Left, customizableSpacing.Top,
                        customizableSpacing.Right, customizableSpacing.Bottom);

                    if (SpaceBetween)
                    {
                        for (int i = 0; i < this.Controls.Count; i++)
                        {
                            Control control = this.Controls[i];
                            control.Dock = DockStyle.Left;
                            if (i == 0)
                            {
                                control.Padding = new Padding(0, 0, 0, 0);
                            }
                            else
                            {
                                control.Padding = new Padding(0, 0, Spacing, 0);
                            }
                        }
                    }
                    else
                    {
                        foreach (Control control in this.Controls)
                        {
                            control.Dock = DockStyle.Left;
                            control.Padding = new Padding(0, 0, Spacing, 0);
                        }
                    }
                    break;

                case EmControlDirection.VERTICAL:
                    this.Padding = this.customizableSpacing == null ?
                    new Padding(Spacing, spacing, spacing, 0) :
                    new Padding(customizableSpacing.Left, customizableSpacing.Top,
                                customizableSpacing.Right, customizableSpacing.Bottom);

                    if (spaceBetween)
                    {
                        for (int i = 0; i < this.Controls.Count; i++)
                        {
                            Control control = this.Controls[i];
                            control.Dock = DockStyle.Top;
                            if (i == 0)
                            {
                                control.Padding = new Padding(0, 0, 0, 0);
                            }
                            else
                            {
                                control.Padding = new Padding(0, 0, 0, spacing);
                            }
                        }
                    }
                    else
                        foreach (Control control in this.Controls)
                        {
                            control.Dock = DockStyle.Top;
                            control.Padding = new Padding(0, 0, 0, spacing);
                        }
                    break;
                default:
                    break;
            }
            ResponsiveControl();
        }

    }
}
