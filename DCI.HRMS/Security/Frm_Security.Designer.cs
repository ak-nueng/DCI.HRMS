namespace DCI.HRMS.Security
{
    partial class Frm_Security
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
            this.components = new System.ComponentModel.Container();
            this.trvMenu = new System.Windows.Forms.TreeView();
            this.menuImages = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // trvMenu
            // 
            this.trvMenu.ImageKey = "Menu";
            this.trvMenu.Location = new System.Drawing.Point(56, 45);
            this.trvMenu.Name = "trvMenu";
            this.trvMenu.SelectedImageKey = "administration";
            this.trvMenu.ShowNodeToolTips = true;
            this.trvMenu.Size = new System.Drawing.Size(148, 179);
            this.trvMenu.TabIndex = 1;
            // 
            // menuImages
            // 
            this.menuImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.menuImages.ImageSize = new System.Drawing.Size(20, 20);
            this.menuImages.TransparentColor = System.Drawing.Color.Empty;
            // 
            // Frm_Security
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 585);
            this.Controls.Add(this.trvMenu);
            this.Name = "Frm_Security";
            this.Text = "Frm_Security";
            this.Load += new System.EventHandler(this.Frm_Security_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView trvMenu;
        private System.Windows.Forms.ImageList menuImages;
    }
}