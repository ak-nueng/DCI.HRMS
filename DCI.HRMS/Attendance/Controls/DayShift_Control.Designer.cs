using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
namespace DCI.HRMS.Attendance.Controls
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
            this.lblDay.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.lblDay.Location = new System.Drawing.Point(-5, 0);
            this.lblDay.Name = "lblDay";
            this.lblDay.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.lblDay.Size = new System.Drawing.Size(29, 20);
            this.lblDay.TabIndex = 5;
            this.lblDay.Text = "Mo";
            this.lblDay.Values.ExtraText = "";
            this.lblDay.Values.Image = null;
            this.lblDay.Values.Text = "Mo";
            // 
            // lblDate
            // 
            this.lblDate.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.lblDate.Location = new System.Drawing.Point(-2, 19);
            this.lblDate.Name = "lblDate";
            this.lblDate.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.lblDate.Size = new System.Drawing.Size(24, 20);
            this.lblDate.TabIndex = 6;
            this.lblDate.Text = "01";
            this.lblDate.Values.ExtraText = "";
            this.lblDate.Values.Image = null;
            this.lblDate.Values.Text = "01";
            // 
            // txtShift
            // 
            this.txtShift.InputControlStyle = ComponentFactory.Krypton.Toolkit.InputControlStyle.Standalone;
            this.txtShift.Location = new System.Drawing.Point(0, 37);
            this.txtShift.MaxLength = 1;
            this.txtShift.Name = "txtShift";
            this.txtShift.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.txtShift.Size = new System.Drawing.Size(18, 20);
            this.txtShift.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.txtShift.TabIndex = 7;
            this.txtShift.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtShift.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtShift_KeyDown);
            this.txtShift.Enter += new System.EventHandler(this.txtShift_Enter);
            this.txtShift.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtShift_KeyPress);
            this.txtShift.TextChanged += new System.EventHandler(this.txtShift_TextChanged);
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

        private KryptonLabel lblDay;
        private KryptonLabel lblDate;
        private KryptonTextBox txtShift;

    }
}
