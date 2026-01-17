using Kztek.Control8.Controls;

namespace IParkingv8.Reporting
{
    partial class frmPrintPreview
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrintPreview));
            lblPrinter = new KzLabel();
            cbPrints = new ComboBox();
            btnPrint = new KzButton();
            btnCancel = new KzButton();
            panelPrintOption = new Panel();
            numCopies = new NumericUpDown();
            lblQuantity = new KzLabel();
            panel1 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            panelContent = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            printDocument1 = new System.Drawing.Printing.PrintDocument();
            printDialog1 = new PrintDialog();
            panelPrintOption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numCopies).BeginInit();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblPrinter
            // 
            lblPrinter.AutoSize = true;
            lblPrinter.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblPrinter.Location = new Point(4, 12);
            lblPrinter.Margin = new Padding(4, 0, 4, 0);
            lblPrinter.Name = "lblPrinter";
            lblPrinter.Size = new Size(59, 21);
            lblPrinter.TabIndex = 0;
            lblPrinter.Text = "Máy In";
            // 
            // cbPrints
            // 
            cbPrints.DropDownStyle = ComboBoxStyle.DropDownList;
            cbPrints.FormattingEnabled = true;
            cbPrints.Location = new Point(68, 8);
            cbPrints.Name = "cbPrints";
            cbPrints.Size = new Size(279, 29);
            cbPrints.TabIndex = 1;
            // 
            // btnPrint
            // 
            btnPrint.BackColor = Color.White;
            btnPrint.BorderColor = Color.FromArgb(41, 97, 27);
            btnPrint.BorderRadius = 8;
            btnPrint.BorderThickness = 1;
            btnPrint.CustomizableEdges = customizableEdges1;
            btnPrint.FillColor = Color.White;
            btnPrint.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnPrint.ForeColor = Color.FromArgb(41, 97, 27);
            btnPrint.Location = new Point(552, 4);
            btnPrint.Name = "btnPrint";
            btnPrint.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnPrint.Size = new Size(88, 33);
            btnPrint.TabIndex = 0;
            btnPrint.Text = "In";
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.White;
            btnCancel.BorderColor = Color.FromArgb(41, 97, 27);
            btnCancel.BorderRadius = 8;
            btnCancel.BorderThickness = 1;
            btnCancel.CustomizableEdges = customizableEdges3;
            btnCancel.DefaultAutoSize = true;
            btnCancel.Dock = DockStyle.Right;
            btnCancel.FillColor = Color.White;
            btnCancel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnCancel.ForeColor = Color.FromArgb(41, 97, 27);
            btnCancel.Location = new Point(801, 0);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnCancel.Size = new Size(72, 58);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Đóng";
            // 
            // panelPrintOption
            // 
            panelPrintOption.Controls.Add(numCopies);
            panelPrintOption.Controls.Add(cbPrints);
            panelPrintOption.Controls.Add(lblPrinter);
            panelPrintOption.Controls.Add(lblQuantity);
            panelPrintOption.Controls.Add(btnPrint);
            panelPrintOption.Dock = DockStyle.Top;
            panelPrintOption.Location = new Point(0, 0);
            panelPrintOption.Name = "panelPrintOption";
            panelPrintOption.Size = new Size(873, 46);
            panelPrintOption.TabIndex = 4;
            // 
            // numCopies
            // 
            numCopies.Location = new Point(464, 8);
            numCopies.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numCopies.Name = "numCopies";
            numCopies.Size = new Size(74, 29);
            numCopies.TabIndex = 2;
            numCopies.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblQuantity.Location = new Point(380, 12);
            lblQuantity.Margin = new Padding(4, 0, 4, 0);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(76, 21);
            lblQuantity.TabIndex = 0;
            lblQuantity.Text = "Số lượng";
            // 
            // panel1
            // 
            panel1.Controls.Add(btnCancel);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 559);
            panel1.Name = "panel1";
            panel1.Size = new Size(873, 58);
            panel1.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 699F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(panelContent, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 46);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(873, 513);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // panelContent
            // 
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(90, 3);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(693, 507);
            panelContent.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 554);
            panel2.Name = "panel2";
            panel2.Size = new Size(873, 5);
            panel2.TabIndex = 7;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 46);
            panel3.Name = "panel3";
            panel3.Size = new Size(873, 5);
            panel3.TabIndex = 8;
            // 
            // printDialog1
            // 
            printDialog1.UseEXDialog = true;
            // 
            // frmPrintPreview
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(873, 617);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(panel1);
            Controls.Add(panelPrintOption);
            Font = new Font("Segoe UI", 12F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "frmPrintPreview";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Phiếu in";
            Load += frmPrintPreview_Load;
            panelPrintOption.ResumeLayout(false);
            panelPrintOption.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numCopies).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private KzLabel lblPrinter;
        private ComboBox cbPrints;
        private KzButton btnPrint;
        private KzButton btnCancel;
        private Panel panelPrintOption;
        private NumericUpDown numCopies;
        private KzLabel lblQuantity;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panelContent;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private PrintDialog printDialog1;
    }
}