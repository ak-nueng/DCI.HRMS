using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DCI.HRMS.Controls
{
	/// <summary>
	/// Summary description for BulletLable.
	/// </summary>
	public class BulletLabel : UserControl
	{
		private Color hoverFontColor;
		private Color oldFontColor;
		private Label lblText;
		private Panel bullet;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public BulletLabel()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblText = new System.Windows.Forms.Label();
			this.bullet = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// lblText
			// 
			this.lblText.AutoSize = true;
			this.lblText.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
			this.lblText.Location = new System.Drawing.Point(12, 0);
			this.lblText.Name = "lblText";
			this.lblText.Size = new System.Drawing.Size(26, 17);
			this.lblText.TabIndex = 2;
			this.lblText.Text = "Text";
			this.lblText.Click += new System.EventHandler(this.lblText_Click);
			this.lblText.MouseHover += new System.EventHandler(this.BulletLabel_MouseHover);
			this.lblText.MouseLeave += new System.EventHandler(this.BulletLabel_MouseLeave);
			// 
			// bullet
			// 
			this.bullet.BackColor = System.Drawing.Color.DeepSkyBlue;
			this.bullet.Location = new System.Drawing.Point(0, 6);
			this.bullet.Name = "bullet";
			this.bullet.Size = new System.Drawing.Size(4, 4);
			this.bullet.TabIndex = 1;
			this.bullet.MouseHover += new System.EventHandler(this.BulletLabel_MouseHover);
			this.bullet.MouseLeave += new System.EventHandler(this.BulletLabel_MouseLeave);
			// 
			// BulletLabel
			// 
			this.Controls.Add(this.lblText);
			this.Controls.Add(this.bullet);
			this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.Name = "BulletLabel";
			this.Size = new System.Drawing.Size(192, 18);
			this.MouseHover += new System.EventHandler(this.BulletLabel_MouseHover);
			this.MouseLeave += new System.EventHandler(this.BulletLabel_MouseLeave);
			this.ResumeLayout(false);

		}

		#endregion

		private void BulletLabel_MouseHover(object sender, EventArgs e)
		{
			this.lblText.ForeColor = this.hoverFontColor;
			this.Cursor = Cursors.Hand;
		}

		private void BulletLabel_MouseLeave(object sender, EventArgs e)
		{
			this.lblText.ForeColor = this.oldFontColor;
			this.Cursor = Cursors.Default;
		}

		private void BulletLabel_Click(object sender, EventArgs e)
		{
		}

		private void lblText_Click(object sender, EventArgs e)
		{
			this.OnClick(EventArgs.Empty);
		}

		public Color HoverColor
		{
			get { return this.hoverFontColor; }
			set
			{
				this.oldFontColor = this.lblText.ForeColor;
				this.hoverFontColor = value;
			}
		}

		public override Font Font
		{
			get { return this.lblText.Font; }
			set { this.lblText.Font = value; }
		}

		public override string Text
		{
			get { return this.lblText.Text; }
			set { this.lblText.Text = value; }
		}

		public string Caption
		{
			get { return this.lblText.Text; }
			set { this.lblText.Text = value; }
		}


		public Color BulletColor
		{
			get { return this.bullet.BackColor; }
			set { this.bullet.BackColor = value; }
		}

	}
}