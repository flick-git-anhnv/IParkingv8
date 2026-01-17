namespace Kztek.Control8.UserControls
{
    partial class KZUI_UcResult
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
            lblMessage = new FontAwesome.Sharp.IconButton();
            SuspendLayout();
            // 
            // lblMessage
            // 
            lblMessage.AutoEllipsis = true;
            lblMessage.BackColor = Color.FromArgb(17, 141, 87);
            lblMessage.Dock = DockStyle.Fill;
            lblMessage.FlatAppearance.BorderSize = 0;
            lblMessage.FlatStyle = FlatStyle.Flat;
            lblMessage.Font = new Font("Segoe UI Semibold", 19F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblMessage.ForeColor = Color.White;
            lblMessage.IconChar = FontAwesome.Sharp.IconChar.CheckCircle;
            lblMessage.IconColor = Color.White;
            lblMessage.IconFont = FontAwesome.Sharp.IconFont.Auto;
            lblMessage.IconSize = 32;
            lblMessage.ImageAlign = ContentAlignment.MiddleRight;
            lblMessage.Location = new Point(0, 0);
            lblMessage.Margin = new Padding(0);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(363, 34);
            lblMessage.TabIndex = 4;
            lblMessage.Text = "BIỂN SỐ KHÔNG HỢP LỆ";
            lblMessage.TextImageRelation = TextImageRelation.ImageBeforeText;
            lblMessage.UseVisualStyleBackColor = false;
            // 
            // KZUI_UcResult
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(lblMessage);
            Margin = new Padding(0);
            Name = "KZUI_UcResult";
            Size = new Size(363, 34);
            ResumeLayout(false);
        }

        #endregion
        private FontAwesome.Sharp.IconButton lblMessage;
    }
}
