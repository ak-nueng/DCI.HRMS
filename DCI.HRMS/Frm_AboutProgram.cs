using System;
using System.ComponentModel;
using System.Windows.Forms;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace DCI.HRMS
{
	/// <summary>
	/// Summary description for FrmAboutUs.
	/// </summary>
	public class FrmAboutProgram : Form
	{
		private Panel panel1;
		private Button ultraButton1;
		private Label label2;
		private Label label1;
		private Label label3;
		private PictureBox ultraPictureBox1;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public FrmAboutProgram()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAboutProgram));
            this.panel1 = new System.Windows.Forms.Panel();
            this.ultraPictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ultraButton1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.ultraPictureBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(386, 335);
            this.panel1.TabIndex = 0;
            // 
            // ultraPictureBox1
            // 
            this.ultraPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("ultraPictureBox1.Image")));
            this.ultraPictureBox1.Location = new System.Drawing.Point(-2, -5);
            this.ultraPictureBox1.Name = "ultraPictureBox1";
            this.ultraPictureBox1.Size = new System.Drawing.Size(386, 303);
            this.ultraPictureBox1.TabIndex = 4;
            this.ultraPictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 301);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(364, 32);
            this.label3.TabIndex = 3;
            this.label3.Text = "Copyright 2001-2006 Daikin Compressor Industries Ltd. All rights reserved.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(4, 260);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "DCI Xtra Suite 2006 R2.";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(148, 260);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Version. 2.0.0";
            // 
            // ultraButton1
            // 
            this.ultraButton1.Location = new System.Drawing.Point(300, 341);
            this.ultraButton1.Name = "ultraButton1";
            this.ultraButton1.Size = new System.Drawing.Size(75, 23);
            this.ultraButton1.TabIndex = 1;
            this.ultraButton1.Text = "OK";
            this.ultraButton1.Click += new System.EventHandler(this.ultraButton1_Click);
            // 
            // FrmAboutProgram
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(386, 371);
            this.Controls.Add(this.ultraButton1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAboutProgram";
            this.Text = "About DCI Xtra Suite 2005.";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraPictureBox1)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private void ultraButton1_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}