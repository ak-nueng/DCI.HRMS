using System;
using System.ComponentModel;
using System.Windows.Forms;
using DCIBizPro.DTO.Common;
using DCI.HRMS.Util;

namespace DCI.HRMS.Panes
{
	/// <summary>
	/// Summary description for MQConditionPane.
	/// </summary>
	public class MQConditionPane : UserControl
	{
		private Label lblText;
		private ComboBox cboOperator;
		private TextBox txtMin;
		private TextBox txtMax;
		private Label lblOperator;
		private Label lblMin;
		private Label lblMax;
		private bool readOnly = false;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public MQConditionPane()
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
			this.cboOperator = new System.Windows.Forms.ComboBox();
			this.txtMin = new System.Windows.Forms.TextBox();
			this.txtMax = new System.Windows.Forms.TextBox();
			this.lblOperator = new System.Windows.Forms.Label();
			this.lblMin = new System.Windows.Forms.Label();
			this.lblMax = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblText
			// 
			this.lblText.AutoSize = true;
			this.lblText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(222)));
			this.lblText.Location = new System.Drawing.Point(4, 4);
			this.lblText.Name = "lblText";
			this.lblText.Size = new System.Drawing.Size(0, 16);
			this.lblText.TabIndex = 0;
			// 
			// cboOperator
			// 
			this.cboOperator.Items.AddRange(new object[] {
															 " =",
															 "!= ",
															 ">",
															 ">= ",
															 "<",
															 "<= ",
															 "? and ?"});
			this.cboOperator.Location = new System.Drawing.Point(120, 0);
			this.cboOperator.Name = "cboOperator";
			this.cboOperator.Size = new System.Drawing.Size(84, 21);
			this.cboOperator.TabIndex = 0;
			this.cboOperator.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboOperator_KeyDown);
			this.cboOperator.SelectedIndexChanged += new System.EventHandler(this.cboOperator_SelectedIndexChanged);
			// 
			// txtMin
			// 
			this.txtMin.Location = new System.Drawing.Point(204, 0);
			this.txtMin.MaxLength = 20;
			this.txtMin.Name = "txtMin";
			this.txtMin.Size = new System.Drawing.Size(64, 20);
			this.txtMin.TabIndex = 1;
			this.txtMin.Text = "0";
			this.txtMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtMin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMin_KeyDown);
			this.txtMin.Enter += new System.EventHandler(this.txtMin_Enter);
			// 
			// txtMax
			// 
			this.txtMax.Location = new System.Drawing.Point(268, 0);
			this.txtMax.MaxLength = 20;
			this.txtMax.Name = "txtMax";
			this.txtMax.Size = new System.Drawing.Size(64, 20);
			this.txtMax.TabIndex = 2;
			this.txtMax.Text = "0";
			this.txtMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtMax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMax_KeyDown);
			this.txtMax.Enter += new System.EventHandler(this.txtMax_Enter);
			// 
			// lblOperator
			// 
			this.lblOperator.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(222)));
			this.lblOperator.ForeColor = System.Drawing.Color.RoyalBlue;
			this.lblOperator.Location = new System.Drawing.Point(120, 4);
			this.lblOperator.Name = "lblOperator";
			this.lblOperator.Size = new System.Drawing.Size(84, 16);
			this.lblOperator.TabIndex = 3;
			this.lblOperator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblOperator.Visible = false;
			// 
			// lblMin
			// 
			this.lblMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(222)));
			this.lblMin.ForeColor = System.Drawing.Color.RoyalBlue;
			this.lblMin.Location = new System.Drawing.Point(208, 4);
			this.lblMin.Name = "lblMin";
			this.lblMin.Size = new System.Drawing.Size(60, 16);
			this.lblMin.TabIndex = 4;
			this.lblMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblMin.Visible = false;
			// 
			// lblMax
			// 
			this.lblMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(222)));
			this.lblMax.ForeColor = System.Drawing.Color.RoyalBlue;
			this.lblMax.Location = new System.Drawing.Point(272, 4);
			this.lblMax.Name = "lblMax";
			this.lblMax.Size = new System.Drawing.Size(60, 16);
			this.lblMax.TabIndex = 5;
			this.lblMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblMax.Visible = false;
			// 
			// MQConditionPane
			// 
			this.Controls.Add(this.lblMax);
			this.Controls.Add(this.lblMin);
			this.Controls.Add(this.lblOperator);
			this.Controls.Add(this.txtMax);
			this.Controls.Add(this.txtMin);
			this.Controls.Add(this.cboOperator);
			this.Controls.Add(this.lblText);
			this.Name = "MQConditionPane";
			this.Size = new System.Drawing.Size(332, 24);
			this.ResumeLayout(false);

		}

		#endregion

		[DefaultValue(false)]
		public bool ReadOnly
		{
			get { return readOnly; }
			set
			{
				readOnly = value;

				if(readOnly)
				{
					cboOperator.Visible = false;
					txtMax.Visible = false;
					txtMin.Visible = false;
					
					lblOperator.Visible = true;
					lblMax.Visible = true;
					lblMin.Visible = true;
				}else{
					lblMax.Visible = false;
					lblMin.Visible = true;
					lblOperator.Visible = false;

					txtMin.Visible = true;
					txtMax.Visible = true;
					cboOperator.Visible = true;
					
					OperatorType o = Operator;
					Operator = o;
				}
			}
		}

		[DefaultValue(OperatorType.Equals)]
		public OperatorType Operator
		{
			set
			{
				int n = (int) value;
				if(cboOperator.SelectedIndex != n)
					this.cboOperator.SelectedIndex = n;	
			}
			get
			{
				if (cboOperator.SelectedIndex < 0)
					cboOperator.SelectedIndex = 0;

				return (OperatorType) (cboOperator.SelectedIndex);
			}
		}

		public string Topic
		{
			set { this.lblText.Text = value; }
			get { return this.lblText.Text; }
		}

		public double Max
		{
			set
			{
				this.txtMax.Text = value.ToString();
				this.lblMax.Text = txtMax.Text;
			}
			get { return Convert.ToDouble(txtMax.Text); }
		}

		public double Min
		{
			set
			{
				this.txtMin.Text = value.ToString();
				this.lblMin.Text = txtMin.Text;
			}
			get { return Convert.ToDouble(txtMin.Text); }
		}

		public void SetCondition(ConditionValue condition)
		{
			this.Max = condition.Maximum;
			this.Min = condition.Minimum;
			this.Operator = condition.Operator;
		}

		public ConditionValue GetCondition()
		{
			ConditionValue condition = new ConditionValue();
			condition.Minimum = Convert.ToDouble(txtMin.Text);
			condition.Maximum = Convert.ToDouble(txtMax.Text);
			condition.Operator = (OperatorType) (cboOperator.SelectedIndex);

			if (condition.Operator == OperatorType.Between)
			{
				if (condition.Minimum > condition.Maximum)
					condition.Minimum = 0.0f;
			}
			return condition;
		}

		public void Clear()
		{
			this.Max = 0.0f;
			this.Min = 0.0f;
			this.Operator = OperatorType.Equals;
		}

		private void cboOperator_KeyDown(object sender, KeyEventArgs e)
		{
			KeyPressManager.Enter(e);
		}

		private void txtMin_KeyDown(object sender, KeyEventArgs e)
		{
			KeyPressManager.Enter(e);
		}

		private void txtMax_KeyDown(object sender, KeyEventArgs e)
		{
			KeyPressManager.Enter(e);
		}

		private void txtMin_Enter(object sender, EventArgs e)
		{
			try
			{
				KeyPressManager.SelectAllTextBox(sender);
			}
			catch
			{
			}
		}

		private void txtMax_Enter(object sender, EventArgs e)
		{
			try
			{
				KeyPressManager.SelectAllTextBox(sender);
			}
			catch
			{
			}
		}

		private void cboOperator_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			OperatorType o = (OperatorType)this.cboOperator.SelectedIndex;

			txtMax.Enabled = true;
			txtMin.Enabled = true;
			txtMin.Visible = true;
			lblMin.Visible = false;
			
			if (o != OperatorType.Between)
			{
				txtMin.Visible = false;
				txtMin.Enabled = true;
				lblMin.Visible = true;
				this.Min = 0.0f;
			}
			this.lblOperator.Text = this.cboOperator.SelectedItem.ToString();

			if(readOnly)
			{
				lblMin.Visible = true;
				lblMax.Visible = true;
				lblOperator.Visible = true;

				txtMin.Visible = false;
				txtMax.Visible = false;
				cboOperator.Visible = false;
			}
		}
	}
}