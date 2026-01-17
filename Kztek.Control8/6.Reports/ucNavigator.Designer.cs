namespace Kztek.Control8.UserControls.ReportUcs
{
    partial class ucNavigator
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelPages = new Panel();
            ucPage5 = new ucPage();
            ucPage4 = new ucPage();
            ucPage3 = new ucPage();
            ucPage2 = new ucPage();
            ucPage1 = new ucPage();
            ucNextPage1 = new ucNextPage();
            ucPreviousPage1 = new ucPreviousPage();
            tableLayoutPanel1 = new TableLayoutPanel();
            lblTotal = new Label();
            panelPages.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panelPages
            // 
            panelPages.Controls.Add(ucPage5);
            panelPages.Controls.Add(ucPage4);
            panelPages.Controls.Add(ucPage3);
            panelPages.Controls.Add(ucPage2);
            panelPages.Controls.Add(ucPage1);
            panelPages.Controls.Add(ucNextPage1);
            panelPages.Controls.Add(ucPreviousPage1);
            panelPages.Dock = DockStyle.Fill;
            panelPages.Location = new Point(347, 0);
            panelPages.Margin = new Padding(0);
            panelPages.Name = "panelPages";
            panelPages.Size = new Size(369, 41);
            panelPages.TabIndex = 0;
            // 
            // ucPage5
            // 
            ucPage5.BackColor = Color.White;
            ucPage5.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            ucPage5.IsActivePage = true;
            ucPage5.KZUI_PageIndex = 5;
            ucPage5.Location = new Point(272, 5);
            ucPage5.Margin = new Padding(0);
            ucPage5.Name = "ucPage5";
            ucPage5.PageColor = Color.FromArgb(41, 97, 27);
            ucPage5.Size = new Size(48, 32);
            ucPage5.TabIndex = 2;
            // 
            // ucPage4
            // 
            ucPage4.BackColor = Color.White;
            ucPage4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            ucPage4.IsActivePage = true;
            ucPage4.KZUI_PageIndex = 4;
            ucPage4.Location = new Point(216, 5);
            ucPage4.Margin = new Padding(0);
            ucPage4.Name = "ucPage4";
            ucPage4.PageColor = Color.FromArgb(41, 97, 27);
            ucPage4.Size = new Size(48, 32);
            ucPage4.TabIndex = 2;
            // 
            // ucPage3
            // 
            ucPage3.BackColor = Color.White;
            ucPage3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            ucPage3.IsActivePage = true;
            ucPage3.KZUI_PageIndex = 3;
            ucPage3.Location = new Point(160, 5);
            ucPage3.Margin = new Padding(0);
            ucPage3.Name = "ucPage3";
            ucPage3.PageColor = Color.FromArgb(41, 97, 27);
            ucPage3.Size = new Size(48, 32);
            ucPage3.TabIndex = 2;
            // 
            // ucPage2
            // 
            ucPage2.BackColor = Color.White;
            ucPage2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            ucPage2.IsActivePage = true;
            ucPage2.KZUI_PageIndex = 2;
            ucPage2.Location = new Point(104, 5);
            ucPage2.Margin = new Padding(0);
            ucPage2.Name = "ucPage2";
            ucPage2.PageColor = Color.FromArgb(41, 97, 27);
            ucPage2.Size = new Size(48, 32);
            ucPage2.TabIndex = 2;
            // 
            // ucPage1
            // 
            ucPage1.BackColor = Color.White;
            ucPage1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            ucPage1.IsActivePage = true;
            ucPage1.KZUI_PageIndex = 1;
            ucPage1.Location = new Point(48, 5);
            ucPage1.Margin = new Padding(0);
            ucPage1.Name = "ucPage1";
            ucPage1.PageColor = Color.FromArgb(41, 97, 27);
            ucPage1.Size = new Size(48, 32);
            ucPage1.TabIndex = 2;
            // 
            // ucNextPage1
            // 
            ucNextPage1.BackColor = Color.White;
            ucNextPage1.Location = new Point(328, 5);
            ucNextPage1.Margin = new Padding(0);
            ucNextPage1.Name = "ucNextPage1";
            ucNextPage1.Size = new Size(32, 32);
            ucNextPage1.TabIndex = 1;
            // 
            // ucPreviousPage1
            // 
            ucPreviousPage1.BackColor = Color.White;
            ucPreviousPage1.Location = new Point(8, 5);
            ucPreviousPage1.Name = "ucPreviousPage1";
            ucPreviousPage1.Size = new Size(32, 32);
            ucPreviousPage1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 369F));
            tableLayoutPanel1.Controls.Add(panelPages, 1, 0);
            tableLayoutPanel1.Controls.Add(lblTotal, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(716, 41);
            tableLayoutPanel1.TabIndex = 7;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Dock = DockStyle.Right;
            lblTotal.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblTotal.Location = new Point(252, 0);
            lblTotal.Name = "lblTotal";
            lblTotal.Padding = new Padding(0, 0, 3, 5);
            lblTotal.Size = new Size(92, 41);
            lblTotal.TabIndex = 1;
            lblTotal.Text = "Tổng số: 100";
            lblTotal.TextAlign = ContentAlignment.BottomRight;
            // 
            // ucNavigator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(0);
            Name = "ucNavigator";
            Size = new Size(716, 41);
            panelPages.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelPages;
        private ucPage ucPage5;
        private ucPage ucPage4;
        private ucPage ucPage3;
        private ucPage ucPage2;
        private ucPage ucPage1;
        private ucNextPage ucNextPage1;
        private ucPreviousPage ucPreviousPage1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblTotal;
    }
}
