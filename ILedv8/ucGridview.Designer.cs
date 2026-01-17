namespace ILedv8
{
    partial class ucGridview<T> where T : Control
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
            this.panelGridview = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelGridview
            // 
            this.panelGridview.BackColor = System.Drawing.SystemColors.Control;
            this.panelGridview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGridview.Location = new System.Drawing.Point(0, 0);
            this.panelGridview.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelGridview.Name = "panelGridview";
            this.panelGridview.Size = new System.Drawing.Size(876, 627);
            this.panelGridview.TabIndex = 0;
            // 
            // ucGridview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelGridview);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ucGridview";
            this.Size = new System.Drawing.Size(876, 627);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelGridview;
    }
}

