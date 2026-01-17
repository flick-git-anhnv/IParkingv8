using Kztek.Control8.Controls;

namespace Kztek.Control8.UserControls.ConfigUcs.ControllerConfigs
{
    partial class UcControllerCardFormat
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panel1 = new Panel();
            cbController = new ComboBox();
            btnSave = new KzButton();
            lblDevice = new KzLabel();
            panelReaderConfigs = new Panel();
            tabControl1 = new TabControl();
            tabReaderConfig = new TabPage();
            tabBarrieConfig = new TabPage();
            panelBarrieConfigs = new Panel();
            panel1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabReaderConfig.SuspendLayout();
            tabBarrieConfig.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(cbController);
            panel1.Controls.Add(btnSave);
            panel1.Controls.Add(lblDevice);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new Size(649, 51);
            panel1.TabIndex = 0;
            // 
            // cbController
            // 
            cbController.DropDownStyle = ComboBoxStyle.DropDownList;
            cbController.FormattingEnabled = true;
            cbController.Location = new Point(88, 12);
            cbController.Name = "cbController";
            cbController.Size = new Size(263, 29);
            cbController.TabIndex = 1;
            cbController.SelectedIndexChanged += CbController_SelectedIndexChanged;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.White;
            btnSave.BorderColor = Color.FromArgb(41, 97, 27);
            btnSave.BorderRadius = 8;
            btnSave.BorderThickness = 1;
            btnSave.CustomizableEdges = customizableEdges1;
            btnSave.FillColor = Color.White;
            btnSave.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnSave.ForeColor = Color.FromArgb(41, 97, 27);
            btnSave.Location = new Point(357, 12);
            btnSave.Name = "btnSave";
            btnSave.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnSave.Size = new Size(112, 29);
            btnSave.TabIndex = 2;
            btnSave.Text = "Lưu";
            btnSave.Click += BtnSave_Click;
            // 
            // lblDevice
            // 
            lblDevice.AutoSize = true;
            lblDevice.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblDevice.Location = new Point(7, 15);
            lblDevice.Margin = new Padding(4, 0, 4, 0);
            lblDevice.Name = "lblDevice";
            lblDevice.Size = new Size(65, 21);
            lblDevice.TabIndex = 0;
            lblDevice.Text = "Thiết bị";
            // 
            // panelReaderConfigs
            // 
            panelReaderConfigs.AutoScroll = true;
            panelReaderConfigs.BackColor = Color.White;
            panelReaderConfigs.Dock = DockStyle.Fill;
            panelReaderConfigs.Location = new Point(3, 3);
            panelReaderConfigs.Margin = new Padding(4);
            panelReaderConfigs.Name = "panelReaderConfigs";
            panelReaderConfigs.Size = new Size(635, 287);
            panelReaderConfigs.TabIndex = 1;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabReaderConfig);
            tabControl1.Controls.Add(tabBarrieConfig);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 51);
            tabControl1.Margin = new Padding(0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(649, 327);
            tabControl1.TabIndex = 3;
            // 
            // tabReaderConfig
            // 
            tabReaderConfig.Controls.Add(panelReaderConfigs);
            tabReaderConfig.Location = new Point(4, 30);
            tabReaderConfig.Name = "tabReaderConfig";
            tabReaderConfig.Padding = new Padding(3);
            tabReaderConfig.Size = new Size(641, 293);
            tabReaderConfig.TabIndex = 0;
            tabReaderConfig.Text = "Cấu hình đầu đọc";
            tabReaderConfig.UseVisualStyleBackColor = true;
            // 
            // tabBarrieConfig
            // 
            tabBarrieConfig.Controls.Add(panelBarrieConfigs);
            tabBarrieConfig.Location = new Point(4, 30);
            tabBarrieConfig.Name = "tabBarrieConfig";
            tabBarrieConfig.Padding = new Padding(3);
            tabBarrieConfig.Size = new Size(641, 293);
            tabBarrieConfig.TabIndex = 1;
            tabBarrieConfig.Text = "Cấu hình barrie";
            tabBarrieConfig.UseVisualStyleBackColor = true;
            // 
            // panelBarrieConfigs
            // 
            panelBarrieConfigs.AutoScroll = true;
            panelBarrieConfigs.BackColor = Color.White;
            panelBarrieConfigs.Dock = DockStyle.Fill;
            panelBarrieConfigs.Location = new Point(3, 3);
            panelBarrieConfigs.Margin = new Padding(4);
            panelBarrieConfigs.Name = "panelBarrieConfigs";
            panelBarrieConfigs.Size = new Size(635, 287);
            panelBarrieConfigs.TabIndex = 2;
            // 
            // UcControllerCardFormat
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(tabControl1);
            Controls.Add(panel1);
            Font = new Font("Segoe UI", 12F);
            Margin = new Padding(4);
            Name = "UcControllerCardFormat";
            Size = new Size(649, 378);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabReaderConfig.ResumeLayout(false);
            tabBarrieConfig.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private KzLabel lblDevice;
        private Panel panelReaderConfigs;
        private ComboBox cbController;
        private KzButton btnSave;
        private TabControl tabControl1;
        private TabPage tabReaderConfig;
        private TabPage tabBarrieConfig;
        private Panel panelBarrieConfigs;
    }
}
