namespace ILedv8.UserControls
{
    partial class ucLedLaneConfigItem
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblLineName = new Label();
            label4 = new Label();
            label3 = new Label();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            numMaxVehicle = new Guna.UI2.WinForms.Guna2NumericUpDown();
            chlbVehicleType = new CheckedListBox();
            chlbLanes = new CheckedListBox();
            label1 = new Label();
            guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMaxVehicle).BeginInit();
            SuspendLayout();
            // 
            // lblLineName
            // 
            lblLineName.AutoSize = true;
            lblLineName.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblLineName.Location = new Point(8, 8);
            lblLineName.Margin = new Padding(2);
            lblLineName.Name = "lblLineName";
            lblLineName.Size = new Size(68, 21);
            lblLineName.TabIndex = 13;
            lblLineName.Text = "Dòng 1 :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label4.Location = new Point(8, 84);
            label4.Margin = new Padding(2);
            label4.Name = "label4";
            label4.Size = new Size(61, 21);
            label4.TabIndex = 13;
            label4.Text = "Loại xe";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label3.Location = new Point(8, 160);
            label3.Margin = new Padding(2);
            label3.Name = "label3";
            label3.Size = new Size(35, 21);
            label3.TabIndex = 13;
            label3.Text = "Làn";
            // 
            // guna2Panel1
            // 
            guna2Panel1.BorderColor = Color.Gray;
            guna2Panel1.BorderRadius = 8;
            guna2Panel1.BorderThickness = 1;
            guna2Panel1.Controls.Add(numMaxVehicle);
            guna2Panel1.Controls.Add(chlbVehicleType);
            guna2Panel1.Controls.Add(chlbLanes);
            guna2Panel1.Controls.Add(label3);
            guna2Panel1.Controls.Add(lblLineName);
            guna2Panel1.Controls.Add(label1);
            guna2Panel1.Controls.Add(label4);
            guna2Panel1.CustomizableEdges = customizableEdges3;
            guna2Panel1.Dock = DockStyle.Fill;
            guna2Panel1.Location = new Point(0, 0);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2Panel1.Size = new Size(788, 237);
            guna2Panel1.TabIndex = 15;
            // 
            // numMaxVehicle
            // 
            numMaxVehicle.BackColor = Color.Transparent;
            numMaxVehicle.BorderRadius = 8;
            numMaxVehicle.CustomizableEdges = customizableEdges1;
            numMaxVehicle.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            numMaxVehicle.Font = new Font("Segoe UI", 12F);
            numMaxVehicle.Location = new Point(144, 32);
            numMaxVehicle.Margin = new Padding(0);
            numMaxVehicle.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numMaxVehicle.Name = "numMaxVehicle";
            numMaxVehicle.ShadowDecoration.CustomizableEdges = customizableEdges2;
            numMaxVehicle.Size = new Size(128, 36);
            numMaxVehicle.TabIndex = 15;
            numMaxVehicle.UpDownButtonFillColor = Color.FromArgb(41, 97, 27);
            numMaxVehicle.UpDownButtonForeColor = Color.White;
            // 
            // chlbVehicleType
            // 
            chlbVehicleType.CheckOnClick = true;
            chlbVehicleType.ColumnWidth = 150;
            chlbVehicleType.Font = new Font("Segoe UI", 12F);
            chlbVehicleType.FormattingEnabled = true;
            chlbVehicleType.Location = new Point(144, 80);
            chlbVehicleType.MultiColumn = true;
            chlbVehicleType.Name = "chlbVehicleType";
            chlbVehicleType.Size = new Size(632, 28);
            chlbVehicleType.TabIndex = 14;
            // 
            // chlbLanes
            // 
            chlbLanes.CheckOnClick = true;
            chlbLanes.ColumnWidth = 150;
            chlbLanes.Font = new Font("Segoe UI", 12F);
            chlbLanes.FormattingEnabled = true;
            chlbLanes.Location = new Point(144, 120);
            chlbLanes.MultiColumn = true;
            chlbLanes.Name = "chlbLanes";
            chlbLanes.Size = new Size(632, 100);
            chlbLanes.TabIndex = 14;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label1.Location = new Point(8, 40);
            label1.Margin = new Padding(2);
            label1.Name = "label1";
            label1.Size = new Size(122, 21);
            label1.TabIndex = 13;
            label1.Text = "Số lượng tối đa";
            // 
            // ucLedLaneConfigItem
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(guna2Panel1);
            Name = "ucLedLaneConfigItem";
            Padding = new Padding(0, 0, 0, 16);
            Size = new Size(788, 253);
            guna2Panel1.ResumeLayout(false);
            guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numMaxVehicle).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Label lblLineName;
        private Label label4;
        private Label label3;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private CheckedListBox chlbLanes;
        private CheckedListBox chlbVehicleType;
        private Label label1;
        private Guna.UI2.WinForms.Guna2NumericUpDown numMaxVehicle;
    }
}
