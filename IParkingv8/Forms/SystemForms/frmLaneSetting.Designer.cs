namespace IParkingv8.Forms.SystemForms
{
    partial class FrmLaneSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLaneSetting));
            tabControl1 = new TabControl();
            tabLed = new TabPage();
            tabCamera = new TabPage();
            tabShortcut = new TabPage();
            tabDisplaySetting = new TabPage();
            tabController = new TabPage();
            tabOption = new TabPage();
            tabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabLed);
            tabControl1.Controls.Add(tabCamera);
            tabControl1.Controls.Add(tabShortcut);
            tabControl1.Controls.Add(tabDisplaySetting);
            tabControl1.Controls.Add(tabController);
            tabControl1.Controls.Add(tabOption);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Segoe UI", 12F);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(800, 450);
            tabControl1.TabIndex = 0;
            // 
            // tabLed
            // 
            tabLed.Location = new Point(4, 30);
            tabLed.Name = "tabLed";
            tabLed.Padding = new Padding(3);
            tabLed.Size = new Size(792, 416);
            tabLed.TabIndex = 0;
            tabLed.Text = "Led";
            tabLed.UseVisualStyleBackColor = true;
            // 
            // tabCamera
            // 
            tabCamera.Location = new Point(4, 30);
            tabCamera.Name = "tabCamera";
            tabCamera.Padding = new Padding(3);
            tabCamera.Size = new Size(792, 416);
            tabCamera.TabIndex = 1;
            tabCamera.Text = "Camera";
            tabCamera.UseVisualStyleBackColor = true;
            // 
            // tabShortcut
            // 
            tabShortcut.Location = new Point(4, 30);
            tabShortcut.Name = "tabShortcut";
            tabShortcut.Size = new Size(792, 416);
            tabShortcut.TabIndex = 2;
            tabShortcut.Text = "Phím tắt";
            tabShortcut.UseVisualStyleBackColor = true;
            // 
            // tabDisplaySetting
            // 
            tabDisplaySetting.Location = new Point(4, 30);
            tabDisplaySetting.Name = "tabDisplaySetting";
            tabDisplaySetting.Padding = new Padding(3);
            tabDisplaySetting.Size = new Size(792, 416);
            tabDisplaySetting.TabIndex = 4;
            tabDisplaySetting.Text = "Hiển thị";
            tabDisplaySetting.UseVisualStyleBackColor = true;
            // 
            // tabController
            // 
            tabController.Location = new Point(4, 30);
            tabController.Name = "tabController";
            tabController.Size = new Size(792, 416);
            tabController.TabIndex = 3;
            tabController.Text = "Bộ điều khiển";
            tabController.UseVisualStyleBackColor = true;
            // 
            // tabOption
            // 
            tabOption.Location = new Point(4, 30);
            tabOption.Name = "tabOption";
            tabOption.Padding = new Padding(3);
            tabOption.Size = new Size(792, 416);
            tabOption.TabIndex = 5;
            tabOption.Text = "Tùy Chọn";
            tabOption.UseVisualStyleBackColor = true;
            // 
            // frmLaneSetting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frmLaneSetting";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cấu hình làn";
            WindowState = FormWindowState.Maximized;
            tabControl1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabLed;
        private TabPage tabCamera;
        private TabPage tabShortcut;
        private TabPage tabController;
        private TabPage tabDisplaySetting;
        private TabPage tabOption;
    }
}