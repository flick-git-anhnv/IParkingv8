namespace Kztek.Control8.UserControls
{
    partial class KZUI_UcImageListInOut
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KZUI_UcImageListInOut));
            panelMain = new KZUI_DirectionPanel();
            kzuI_UcImageListOut = new KZUI_UcImageList();
            kzuI_UcImageListIn = new KZUI_UcImageList();
            panelMain.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.Controls.Add(kzuI_UcImageListOut);
            panelMain.Controls.Add(kzuI_UcImageListIn);
            panelMain.CustomizableEdges = customizableEdges1;
            panelMain.CustomSpacing = "0,0,0,0";
            panelMain.Dock = DockStyle.Fill;
            panelMain.KZUI_ControlDirection = EmControlDirection.HORIZONTAL;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.ShadowDecoration.CustomizableEdges = customizableEdges2;
            panelMain.Size = new Size(602, 454);
            panelMain.SpaceBetween = true;
            panelMain.Spacing = 4;
            panelMain.TabIndex = 0;
            // 
            // kzuI_UcImageListOut
            // 
            kzuI_UcImageListOut.BackColor = Color.White;
            kzuI_UcImageListOut.Dock = DockStyle.Left;
            kzuI_UcImageListOut.KZUI_ControlDirection = EmControlDirection.VERTICAL;
            kzuI_UcImageListOut.KZUI_ControlSizeMode = EmControlSizeMode.SMALL;
            kzuI_UcImageListOut.KZUI_DefaultImage = (Image)resources.GetObject("kzuI_UcImageListOut.KZUI_DefaultImage");
            kzuI_UcImageListOut.KZUI_PanoramaImage = null;
            kzuI_UcImageListOut.KZUI_SpaceBetween = false;
            kzuI_UcImageListOut.KZUI_Title = "ẢNH RA";
            kzuI_UcImageListOut.KZUI_TitlePanorama = "";
            kzuI_UcImageListOut.KZUI_TitleVehicle = "";
            kzuI_UcImageListOut.KZUI_VehicleImage = null;
            kzuI_UcImageListOut.Location = new Point(303, 0);
            kzuI_UcImageListOut.Name = "kzuI_UcImageListOut";
            kzuI_UcImageListOut.Size = new Size(299, 454);
            kzuI_UcImageListOut.TabIndex = 0;
            // 
            // kzuI_UcImageListIn
            // 
            kzuI_UcImageListIn.BackColor = Color.White;
            kzuI_UcImageListIn.Dock = DockStyle.Left;
            kzuI_UcImageListIn.KZUI_ControlDirection = EmControlDirection.VERTICAL;
            kzuI_UcImageListIn.KZUI_ControlSizeMode = EmControlSizeMode.SMALL;
            kzuI_UcImageListIn.KZUI_DefaultImage = (Image)resources.GetObject("kzuI_UcImageListIn.KZUI_DefaultImage");
            kzuI_UcImageListIn.KZUI_PanoramaImage = null;
            kzuI_UcImageListIn.KZUI_SpaceBetween = false;
            kzuI_UcImageListIn.KZUI_Title = "ẢNH VÀO";
            kzuI_UcImageListIn.KZUI_TitlePanorama = "";
            kzuI_UcImageListIn.KZUI_TitleVehicle = "";
            kzuI_UcImageListIn.KZUI_VehicleImage = null;
            kzuI_UcImageListIn.Location = new Point(0, 0);
            kzuI_UcImageListIn.Name = "kzuI_UcImageListIn";
            kzuI_UcImageListIn.Padding = new Padding(0, 0, 4, 0);
            kzuI_UcImageListIn.Size = new Size(303, 454);
            kzuI_UcImageListIn.TabIndex = 0;
            // 
            // KZUI_UcImageListInOut
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(panelMain);
            Name = "KZUI_UcImageListInOut";
            Size = new Size(602, 454);
            panelMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private KZUI_DirectionPanel panelMain;
        private KZUI_UcImageList kzuI_UcImageListOut;
        private KZUI_UcImageList kzuI_UcImageListIn;
    }
}
