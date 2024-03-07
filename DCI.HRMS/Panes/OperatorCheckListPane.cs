using System;
using System.ComponentModel;
using System.Windows.Forms;
using DCI.HRMS.Util;

namespace DCI.HRMS.Panes
{
	public class OperatorCheckListPane : UserControl
	{
		private Panel panel1;
		private LabelCaptionPane labelCaptionPane2;
		private LabelCaptionPane labelCaptionPane1;
		private LabelCaptionPane labelCaptionPane3;
		private Label label7;
		private Label label6;
		private Label label5;
		private Label label4;
		private Label label3;
		private Label label2;
		private Label label1;
		private TextBox txtOPOilChk;
		private TextBox txtOPTempChk;
		private TextBox txtOPPipeChk;
		private TextBox txtOPSerialChk;
		private TextBox txtOPN2ChargeChk;
		private TextBox txtOPRunTestChk;
		private TextBox txtOPPiraniChk;
		private IContainer components = null;

		public OperatorCheckListPane()
		{
			// This call is required by the Windows Form Designer.
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

		public BorderStyle FrameBorderStyle
		{
			get { return this.panel1.BorderStyle; }
			set { this.panel1.BorderStyle = value; }
		}

		public void Reset()
		{
			this.txtOPPipeChk.Text = "";
			this.txtOPN2ChargeChk.Text = "";
			this.txtOPOilChk.Text = "";
			this.txtOPPiraniChk.Text = "";
			this.txtOPRunTestChk.Text = "";
			this.txtOPSerialChk.Text = "";
			this.txtOPTempChk.Text = "";
		}

		#region Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.labelCaptionPane2 = new DCI.HRMS.Panes.LabelCaptionPane();
			this.labelCaptionPane1 = new DCI.HRMS.Panes.LabelCaptionPane();
			this.labelCaptionPane3 = new DCI.HRMS.Panes.LabelCaptionPane();
			this.txtOPPipeChk = new System.Windows.Forms.TextBox();
			this.txtOPSerialChk = new System.Windows.Forms.TextBox();
			this.txtOPN2ChargeChk = new System.Windows.Forms.TextBox();
			this.txtOPRunTestChk = new System.Windows.Forms.TextBox();
			this.txtOPPiraniChk = new System.Windows.Forms.TextBox();
			this.txtOPOilChk = new System.Windows.Forms.TextBox();
			this.txtOPTempChk = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.labelCaptionPane2);
			this.panel1.Controls.Add(this.labelCaptionPane1);
			this.panel1.Controls.Add(this.labelCaptionPane3);
			this.panel1.Controls.Add(this.txtOPPipeChk);
			this.panel1.Controls.Add(this.txtOPSerialChk);
			this.panel1.Controls.Add(this.txtOPN2ChargeChk);
			this.panel1.Controls.Add(this.txtOPRunTestChk);
			this.panel1.Controls.Add(this.txtOPPiraniChk);
			this.panel1.Controls.Add(this.txtOPOilChk);
			this.panel1.Controls.Add(this.txtOPTempChk);
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(192, 204);
			this.panel1.TabIndex = 22;
			// 
			// labelCaptionPane2
			// 
			this.labelCaptionPane2.Active = false;
			this.labelCaptionPane2.ActiveGradientHighColor = System.Drawing.Color.FromArgb(((System.Byte) (255)), ((System.Byte) (225)), ((System.Byte) (155)));
			this.labelCaptionPane2.ActiveGradientLowColor = System.Drawing.Color.FromArgb(((System.Byte) (255)), ((System.Byte) (165)), ((System.Byte) (78)));
			this.labelCaptionPane2.ActiveTextColor = System.Drawing.Color.Black;
			this.labelCaptionPane2.AllowActive = false;
			this.labelCaptionPane2.AntiAlias = false;
			this.labelCaptionPane2.Caption = "Operator";
			this.labelCaptionPane2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
			this.labelCaptionPane2.InactiveGradientHighColor = System.Drawing.Color.FromArgb(((System.Byte) (90)), ((System.Byte) (135)), ((System.Byte) (215)));
			this.labelCaptionPane2.InactiveGradientLowColor = System.Drawing.Color.FromArgb(((System.Byte) (3)), ((System.Byte) (55)), ((System.Byte) (145)));
			this.labelCaptionPane2.InactiveTextColor = System.Drawing.Color.White;
			this.labelCaptionPane2.Location = new System.Drawing.Point(120, 0);
			this.labelCaptionPane2.Name = "labelCaptionPane2";
			this.labelCaptionPane2.Size = new System.Drawing.Size(66, 20);
			this.labelCaptionPane2.TabIndex = 37;
			// 
			// labelCaptionPane1
			// 
			this.labelCaptionPane1.Active = false;
			this.labelCaptionPane1.ActiveGradientHighColor = System.Drawing.Color.FromArgb(((System.Byte) (255)), ((System.Byte) (225)), ((System.Byte) (155)));
			this.labelCaptionPane1.ActiveGradientLowColor = System.Drawing.Color.FromArgb(((System.Byte) (255)), ((System.Byte) (165)), ((System.Byte) (78)));
			this.labelCaptionPane1.ActiveTextColor = System.Drawing.Color.Black;
			this.labelCaptionPane1.AllowActive = false;
			this.labelCaptionPane1.AntiAlias = false;
			this.labelCaptionPane1.Caption = "        Check list";
			this.labelCaptionPane1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
			this.labelCaptionPane1.InactiveGradientHighColor = System.Drawing.Color.FromArgb(((System.Byte) (90)), ((System.Byte) (135)), ((System.Byte) (215)));
			this.labelCaptionPane1.InactiveGradientLowColor = System.Drawing.Color.FromArgb(((System.Byte) (3)), ((System.Byte) (55)), ((System.Byte) (145)));
			this.labelCaptionPane1.InactiveTextColor = System.Drawing.Color.White;
			this.labelCaptionPane1.Location = new System.Drawing.Point(-1, 0);
			this.labelCaptionPane1.Name = "labelCaptionPane1";
			this.labelCaptionPane1.Size = new System.Drawing.Size(115, 20);
			this.labelCaptionPane1.TabIndex = 38;
			// 
			// labelCaptionPane3
			// 
			this.labelCaptionPane3.Active = false;
			this.labelCaptionPane3.ActiveGradientHighColor = System.Drawing.Color.FromArgb(((System.Byte) (255)), ((System.Byte) (225)), ((System.Byte) (155)));
			this.labelCaptionPane3.ActiveGradientLowColor = System.Drawing.Color.FromArgb(((System.Byte) (255)), ((System.Byte) (165)), ((System.Byte) (78)));
			this.labelCaptionPane3.ActiveTextColor = System.Drawing.Color.Black;
			this.labelCaptionPane3.AllowActive = false;
			this.labelCaptionPane3.AntiAlias = false;
			this.labelCaptionPane3.Caption = "";
			this.labelCaptionPane3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
			this.labelCaptionPane3.InactiveGradientHighColor = System.Drawing.Color.FromArgb(((System.Byte) (90)), ((System.Byte) (135)), ((System.Byte) (215)));
			this.labelCaptionPane3.InactiveGradientLowColor = System.Drawing.Color.FromArgb(((System.Byte) (3)), ((System.Byte) (55)), ((System.Byte) (145)));
			this.labelCaptionPane3.InactiveTextColor = System.Drawing.Color.White;
			this.labelCaptionPane3.Location = new System.Drawing.Point(-1, 0);
			this.labelCaptionPane3.Name = "labelCaptionPane3";
			this.labelCaptionPane3.Size = new System.Drawing.Size(240, 20);
			this.labelCaptionPane3.TabIndex = 36;
			// 
			// txtOPPipeChk
			// 
			this.txtOPPipeChk.Location = new System.Drawing.Point(114, 102);
			this.txtOPPipeChk.MaxLength = 5;
			this.txtOPPipeChk.Name = "txtOPPipeChk";
			this.txtOPPipeChk.Size = new System.Drawing.Size(66, 20);
			this.txtOPPipeChk.TabIndex = 3;
			this.txtOPPipeChk.Text = "";
			this.txtOPPipeChk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtOPPipeChk.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOPChk_KeyDown);
			this.txtOPPipeChk.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyNumericOnly);
			this.txtOPPipeChk.Enter += new System.EventHandler(this.TextBox_Enter);
			// 
			// txtOPSerialChk
			// 
			this.txtOPSerialChk.Location = new System.Drawing.Point(114, 174);
			this.txtOPSerialChk.MaxLength = 5;
			this.txtOPSerialChk.Name = "txtOPSerialChk";
			this.txtOPSerialChk.Size = new System.Drawing.Size(66, 20);
			this.txtOPSerialChk.TabIndex = 6;
			this.txtOPSerialChk.Text = "";
			this.txtOPSerialChk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtOPSerialChk.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOPChk_KeyDown);
			this.txtOPSerialChk.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyNumericOnly);
			this.txtOPSerialChk.Enter += new System.EventHandler(this.TextBox_Enter);
			// 
			// txtOPN2ChargeChk
			// 
			this.txtOPN2ChargeChk.Location = new System.Drawing.Point(114, 150);
			this.txtOPN2ChargeChk.MaxLength = 5;
			this.txtOPN2ChargeChk.Name = "txtOPN2ChargeChk";
			this.txtOPN2ChargeChk.Size = new System.Drawing.Size(66, 20);
			this.txtOPN2ChargeChk.TabIndex = 5;
			this.txtOPN2ChargeChk.Text = "";
			this.txtOPN2ChargeChk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtOPN2ChargeChk.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOPChk_KeyDown);
			this.txtOPN2ChargeChk.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyNumericOnly);
			this.txtOPN2ChargeChk.Enter += new System.EventHandler(this.TextBox_Enter);
			// 
			// txtOPRunTestChk
			// 
			this.txtOPRunTestChk.Location = new System.Drawing.Point(114, 126);
			this.txtOPRunTestChk.MaxLength = 5;
			this.txtOPRunTestChk.Name = "txtOPRunTestChk";
			this.txtOPRunTestChk.Size = new System.Drawing.Size(66, 20);
			this.txtOPRunTestChk.TabIndex = 4;
			this.txtOPRunTestChk.Text = "";
			this.txtOPRunTestChk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtOPRunTestChk.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOPChk_KeyDown);
			this.txtOPRunTestChk.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyNumericOnly);
			this.txtOPRunTestChk.Enter += new System.EventHandler(this.TextBox_Enter);
			// 
			// txtOPPiraniChk
			// 
			this.txtOPPiraniChk.Location = new System.Drawing.Point(114, 54);
			this.txtOPPiraniChk.MaxLength = 5;
			this.txtOPPiraniChk.Name = "txtOPPiraniChk";
			this.txtOPPiraniChk.Size = new System.Drawing.Size(66, 20);
			this.txtOPPiraniChk.TabIndex = 1;
			this.txtOPPiraniChk.Text = "";
			this.txtOPPiraniChk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtOPPiraniChk.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOPChk_KeyDown);
			this.txtOPPiraniChk.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyNumericOnly);
			this.txtOPPiraniChk.Enter += new System.EventHandler(this.TextBox_Enter);
			// 
			// txtOPOilChk
			// 
			this.txtOPOilChk.Location = new System.Drawing.Point(114, 78);
			this.txtOPOilChk.MaxLength = 5;
			this.txtOPOilChk.Name = "txtOPOilChk";
			this.txtOPOilChk.Size = new System.Drawing.Size(66, 20);
			this.txtOPOilChk.TabIndex = 2;
			this.txtOPOilChk.Text = "";
			this.txtOPOilChk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtOPOilChk.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOPChk_KeyDown);
			this.txtOPOilChk.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyNumericOnly);
			this.txtOPOilChk.Enter += new System.EventHandler(this.TextBox_Enter);
			// 
			// txtOPTempChk
			// 
			this.txtOPTempChk.Location = new System.Drawing.Point(114, 30);
			this.txtOPTempChk.MaxLength = 5;
			this.txtOPTempChk.Name = "txtOPTempChk";
			this.txtOPTempChk.Size = new System.Drawing.Size(66, 20);
			this.txtOPTempChk.TabIndex = 0;
			this.txtOPTempChk.Text = "";
			this.txtOPTempChk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtOPTempChk.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOPChk_KeyDown);
			this.txtOPTempChk.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyNumericOnly);
			this.txtOPTempChk.Enter += new System.EventHandler(this.TextBox_Enter);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
			this.label7.Location = new System.Drawing.Point(6, 102);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(64, 17);
			this.label7.TabIndex = 28;
			this.label7.Text = "Label Check";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
			this.label6.Location = new System.Drawing.Point(6, 150);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(66, 17);
			this.label6.TabIndex = 27;
			this.label6.Text = "N2 charge : ";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
			this.label5.Location = new System.Drawing.Point(6, 174);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(75, 17);
			this.label5.TabIndex = 26;
			this.label5.Text = "Serial check : ";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
			this.label4.Location = new System.Drawing.Point(6, 126);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(79, 17);
			this.label4.TabIndex = 25;
			this.label4.Text = "Running test : ";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
			this.label3.Location = new System.Drawing.Point(6, 54);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(76, 17);
			this.label3.TabIndex = 24;
			this.label3.Text = "Pirani Vacuum";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
			this.label2.Location = new System.Drawing.Point(6, 78);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(66, 17);
			this.label2.TabIndex = 23;
			this.label2.Text = "Oil charge : ";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
			this.label1.Location = new System.Drawing.Point(5, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(86, 17);
			this.label1.TabIndex = 22;
			this.label1.Text = "Tempperature : ";
			// 
			// OperatorCheckListPane
			// 
			this.Controls.Add(this.panel1);
			this.Name = "OperatorCheckListPane";
			this.Size = new System.Drawing.Size(192, 204);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private void txtOPChk_KeyDown(object sender, KeyEventArgs e)
		{
			KeyPressManager.Enter(e);
		}

		private void TextBox_Enter(object sender, EventArgs e)
		{
			try
			{
				KeyPressManager.SelectAllTextBox(sender);
			}
			catch
			{
			}
		}

		private void KeyNumericOnly(object sender, KeyPressEventArgs e)
		{
			KeyPressManager.EnterNumericOnly(e);
		}

		public string OPCheckTempperature
		{
			set { this.txtOPTempChk.Text = value; }
			get { return this.txtOPTempChk.Text; }
		}

		public string OPCheckOilChargeQty
		{
			set { this.txtOPOilChk.Text = value; }
			get { return this.txtOPOilChk.Text; }
		}

		public string OPCheckPiraniGuage
		{
			set { this.txtOPPiraniChk.Text = value; }
			get { return this.txtOPPiraniChk.Text; }
		}

		public string OPCheckRunningTest
		{
			set { this.txtOPRunTestChk.Text = value; }
			get { return this.txtOPRunTestChk.Text; }
		}

		public string OPCheckN2Charge
		{
			set { this.txtOPN2ChargeChk.Text = value; }
			get { return this.txtOPN2ChargeChk.Text; }
		}

		public string OPCheckInsulator
		{
			set { this.txtOPPipeChk.Text = value; }
			get { return this.txtOPPipeChk.Text; }
		}

		public string OPCheckSerial
		{
			set { this.txtOPSerialChk.Text = value; }
			get { return this.txtOPSerialChk.Text; }
		}

		public void Clear()
		{
			foreach(Control c in this.Controls)
			{
				if(c is TextBox)
				{
					TextBox t = (TextBox)c;
					t.Text = string.Empty;
				}
			}
		}
	}
}