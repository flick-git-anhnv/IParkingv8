using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ILedv8
{
    public partial class ucGridview<T> : UserControl where T : Control
    {
        private List<T> children;
        private int maxCrossAxisExtent;
        private double childAspectRatio;
        private int crossAxisSpacing;
        private int mainAxisSpacing;
        private int padding = 10;
        public ucGridview(List<T> children, int maxCrossAxisExtent, double childAspectRatio, int crossAxisSpacing, int mainAxisSpacing, int padding = 10)
        {
            InitializeComponent();
            this.BackColor = Color.White;
            panelGridview.BackColor = Color.White;
            this.children = children;
            this.maxCrossAxisExtent = maxCrossAxisExtent;
            this.childAspectRatio = childAspectRatio;
            this.crossAxisSpacing = crossAxisSpacing;
            this.mainAxisSpacing = mainAxisSpacing;
            this.padding = padding;

            panelGridview.Padding = new Padding(padding);

            foreach (Control child in children)
            {
                child.MaximumSize = new Size(maxCrossAxisExtent, (int)(maxCrossAxisExtent * childAspectRatio));
                panelGridview.Controls.Add(child);
            }
            this.MinimumSize = new Size(padding * 2 + mainAxisSpacing, crossAxisSpacing);
            panelGridview.AutoScroll = true;
            this.SizeChanged += UcGridview_SizeChanged;
        }

        private void UcGridview_SizeChanged(object sender, EventArgs e)
        {
            if (panelGridview.VerticalScroll.Visible)
            {
                panelGridview.VerticalScroll.Value = 0;
            }

            if (panelGridview.HorizontalScroll.Visible)
            {
                panelGridview.HorizontalScroll.Value = 0;
            }

            int clientWidth = this.Width - this.padding * 2;
            int maxControlInRow = clientWidth / (maxCrossAxisExtent + mainAxisSpacing / 2) + 1;

            int childWidth = (clientWidth - (maxControlInRow - 1) * mainAxisSpacing) / (maxControlInRow);

            for (int i = 0; i < children.Count; i++)
            {
                children[i].Width = childWidth;
                children[i].Height = (int)(childWidth * this.childAspectRatio);

                int rowIndex = i / maxControlInRow;
                int columnIndex = i % maxControlInRow;

                int xLocation = padding + columnIndex * children[i].Width + columnIndex * mainAxisSpacing;
                int yLocation = padding + rowIndex * children[i].Height + rowIndex * crossAxisSpacing;

                children[i].Location = new Point(xLocation, yLocation);
            }
            panelGridview.PerformLayout();
        }
    }
}
