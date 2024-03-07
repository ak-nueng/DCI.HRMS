using System.Windows.Forms;
namespace DCI.HRMS.ATT.Controls
{
    partial class DayShift_Control
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
            this.lblDay = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblDate = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtShift = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.SuspendLayout();
            // 
            // lblDay
            // 
            this.lblDay.Location = new System.Drawing.Point(-3, 0);
            this.lblDay.Name = "lblDay";
            this.lblDay.Size = new System.Drawing.Size(23, 19);
            this.lblDay.TabIndex = 5;
            this.lblDay.Text = "Su";
            this.lblDay.Values.ExtraText = "";
            this.lblDay.Values.Image = null;
            this.lblDay.Values.Text = "Su";
            // 
            // lblDate
            // 
            this.lblDate.Location = new System.Drawing.Point(-2, 19);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(22, 19);
            this.lblDate.TabIndex = 6;
            this.lblDate.Text = "01";
            this.lblDate.Values.ExtraText = "";
            this.lblDate.Values.Image = null;
            this.lblDate.Values.Text = "01";
            // 
            // txtShift
            // 
            this.txtShift.Location = new System.Drawing.Point(0, 38);
            this.txtShift.Name = "txtShift";
            this.txtShift.Size = new System.Drawing.Size(18, 21);
            this.txtShift.TabIndex = 7;
            // 
            // DayShift_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtShift);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblDay);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "DayShift_Control";
            this.Size = new System.Drawing.Size(18, 61);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblDay;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblDate;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtShift;

    }
}
