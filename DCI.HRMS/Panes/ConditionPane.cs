using System;
using System.ComponentModel;
using System.Windows.Forms;
using DCIBizPro.DTO.Common;
using DCIBizPro.Util.Diagnostics;
using DCI.HRMS.Util;

namespace DCI.HRMS.Panes
{
	/// <summary>
	/// Summary description for ConditionPane.
	/// </summary>
	public class ConditionPane : UserControl, IFormAction
	{
		private Container components = null;
		private TextBox txtMin;
		private TextBox txtMax;

		private ConditionValue m_Conditon = new ConditionValue();
		private ComboBox cboCondition;
		private DefaultValueCollection m_OperatorTypeList = new DefaultValueCollection();

		public ConditionPane()
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

		public ConditionValue Condition
		{
			set
			{
				this.m_Conditon = value;

				this.cboCondition.SelectedValue = Convert.ToString((int) this.m_Conditon.Operator);
				this.txtMin.Text = value.Minimum.ToString();
				this.txtMax.Text = value.Maximum.ToString();
			}
			get
			{
				this.Reload();
				return this.m_Conditon;
			}
		}

		public DefaultValueCollection OperatorTypeList
		{
			set
			{
				this.m_OperatorTypeList = value;
				try
				{
					this.cboCondition.DataBindings.Clear();
					this.cboCondition.DataSource = this.m_OperatorTypeList;
					this.cboCondition.DisplayMember = "DefaultDescription";
					this.cboCondition.ValueMember = "DefaultData";
					//this.cboCondition.SelectedIndex = 0;
				}
				catch (Exception ex)
				{
					EventLogHelper.logError(string.Format("Set operator type list encounted problem : {0}", ex.Message));
				}
			}
		}

		public OperatorType Operator
		{
			get
			{
				this.Reload();
				return this.m_Conditon.Operator;
			}
			set
			{
				this.m_Conditon.Operator = value;
				this.Display();
			}
		}

		public double Minimum
		{
			get
			{
				this.Reload();
				return this.m_Conditon.Minimum;
			}
			set
			{
				this.m_Conditon.Minimum = value;
				this.Display();
			}
		}

		public double Maximum
		{
			get
			{
				this.Reload();
				return this.m_Conditon.Maximum;
			}
			set
			{
				this.m_Conditon.Maximum = value;
				this.Display();
			}
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtMin = new System.Windows.Forms.TextBox();
			this.txtMax = new System.Windows.Forms.TextBox();
			this.cboCondition = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// txtMin
			// 
			this.txtMin.BackColor = System.Drawing.Color.Beige;
			this.txtMin.Location = new System.Drawing.Point(78, 0);
			this.txtMin.MaxLength = 7;
			this.txtMin.Name = "txtMin";
			this.txtMin.Size = new System.Drawing.Size(60, 20);
			this.txtMin.TabIndex = 1;
			this.txtMin.Text = "";
			this.txtMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtMin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMin_KeyDown);
			this.txtMin.Leave += new System.EventHandler(this.txtMin_Leave);
			this.txtMin.MouseLeave += new System.EventHandler(this.txtMin_MouseLeave);
			this.txtMin.Enter += new System.EventHandler(this.txtMin_Enter);
			// 
			// txtMax
			// 
			this.txtMax.BackColor = System.Drawing.Color.Beige;
			this.txtMax.Location = new System.Drawing.Point(138, 0);
			this.txtMax.MaxLength = 7;
			this.txtMax.Name = "txtMax";
			this.txtMax.Size = new System.Drawing.Size(60, 20);
			this.txtMax.TabIndex = 2;
			this.txtMax.Text = "";
			this.txtMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtMax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMax_KeyDown);
			this.txtMax.Leave += new System.EventHandler(this.txtMax_Leave);
			this.txtMax.MouseLeave += new System.EventHandler(this.txtMax_MouseLeave);
			this.txtMax.Enter += new System.EventHandler(this.txtMax_Enter);
			// 
			// cboCondition
			// 
			this.cboCondition.BackColor = System.Drawing.Color.Beige;
			this.cboCondition.Location = new System.Drawing.Point(0, 0);
			this.cboCondition.Name = "cboCondition";
			this.cboCondition.Size = new System.Drawing.Size(78, 21);
			this.cboCondition.TabIndex = 0;
			this.cboCondition.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboCondition_KeyDown);
			this.cboCondition.SelectedIndexChanged += new System.EventHandler(this.cboCondition_SelectedIndexChanged);
			this.cboCondition.MouseLeave += new System.EventHandler(this.cboCondition_MouseLeave);
			// 
			// ConditionPane
			// 
			this.Controls.Add(this.cboCondition);
			this.Controls.Add(this.txtMax);
			this.Controls.Add(this.txtMin);
			this.Name = "ConditionPane";
			this.Size = new System.Drawing.Size(204, 24);
			this.ResumeLayout(false);

		}

		#endregion

		#region IFormAction Members

		public void New()
		{
			// TODO:  Add ConditionPane.New implementation
		}

		public void Save()
		{
			// TODO:  Add ConditionPane.Save implementation
		}

		public void Delete()
		{
			// TODO:  Add ConditionPane.Delete implementation
		}

		public void Undo()
		{
			// TODO:  Add ConditionPane.Undo implementation
		}

		public void Redo()
		{
			// TODO:  Add ConditionPane.Redo implementation
		}

		public void Reset()
		{
			try
			{
				this.txtMin.Text = "0.0";
				this.txtMax.Text = "0.0";
				this.cboCondition.SelectedIndex = 0;
			}
			catch
			{
			}
		}

		public void Display()
		{
			this.DisableTextBox(this.m_Conditon.Operator);
			this.cboCondition.SelectedValue = Convert.ToString((int) this.m_Conditon.Operator);

			this.txtMin.Text = this.m_Conditon.Minimum.ToString();
			this.txtMax.Text = this.m_Conditon.Maximum.ToString();
		}

		public FormAction FormActionStatus
		{
			set
			{
			}
			get { return FormAction.Save; }
		}

		#endregion

		private void DisableTextBox(OperatorType oType)
		{
			switch (oType)
			{
				case OperatorType.Equals:
					this.m_Conditon.Minimum = 0.0f;
					this.txtMin.Enabled = false;
					break;
				case OperatorType.NotEquals:
					this.m_Conditon.Minimum = 0.0f;
					this.txtMin.Enabled = false;
					break;
				case OperatorType.MoreThan:
					this.m_Conditon.Minimum = 0.0f;
					this.txtMin.Enabled = false;
					break;
				case OperatorType.MoreThanOrEqual:
					this.m_Conditon.Minimum = 0.0f;
					this.txtMin.Enabled = false;
					break;
				case OperatorType.LessThan:
					this.m_Conditon.Minimum = 0.0f;
					this.txtMin.Enabled = false;
					break;
				case OperatorType.LessThanOrEqual:
					this.m_Conditon.Minimum = 0.0f;
					this.txtMin.Enabled = false;
					break;
				case OperatorType.Between:
					this.txtMin.Enabled = true;
					break;
			}

			this.txtMin.Text = this.m_Conditon.Minimum.ToString();
		}

		public void Reload()
		{
			try
			{
				this.m_Conditon.Operator = (OperatorType) Convert.ToInt32(this.cboCondition.SelectedValue.ToString());
			}
			catch
			{
			}
			try
			{
				this.m_Conditon.Minimum = Convert.ToDouble(this.txtMin.Text);
			}
			catch
			{
				this.m_Conditon.Minimum = 0.0f;
			}
			try
			{
				this.m_Conditon.Maximum = Convert.ToDouble(this.txtMax.Text);
			}
			catch
			{
				this.m_Conditon.Maximum = 0.0f;
			}
		}

		private void txtMax_Leave(object sender, EventArgs e)
		{
			this.Reload();
		}

		private void txtMin_Leave(object sender, EventArgs e)
		{
			this.Reload();
		}

		private void txtMax_MouseLeave(object sender, EventArgs e)
		{
			this.Reload();
		}

		private void cboCondition_MouseLeave(object sender, EventArgs e)
		{
			this.Reload();
		}

		private void txtMin_MouseLeave(object sender, EventArgs e)
		{
			this.Reload();
		}

		private void cboCondition_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				this.m_Conditon.Operator = (OperatorType) Convert.ToInt32(this.cboCondition.SelectedValue.ToString());
			}
			catch
			{
			}
			this.DisableTextBox(this.m_Conditon.Operator);
		}

		private void cboCondition_KeyDown(object sender, KeyEventArgs e)
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

/*
		private void KeyNumericOnly(object sender , KeyPressEventArgs e)
		{
			KeyPressManager.EnterNumericOnly(e);
		}
*/
	}
}