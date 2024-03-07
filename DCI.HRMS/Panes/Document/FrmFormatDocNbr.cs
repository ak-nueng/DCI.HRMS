using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
//using DCIBizPro.Business.Document;
//using DCIBizPro.DTO.Document;
//using DCIBizPro.Util.Text;
using DCI.HRMS.Common;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTabControl;

namespace DCI.HRMS.Document
{
	/// <summary>
	/// Summary description for FrmRunNbr.
	/// </summary>
	public class FrmFormatDocNbr : BaseForm, IFormChildAction
	{
		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;
		private UltraTabControl tab;
		private UltraTabPageControl ultraTabPageControl1;
		private Label label2;
		private Label label3;
		private Label label4;
		private Label label5;
		private Label label6;
		private Label label7;
		private Label label8;
		private Label label9;
		private Label label10;
		private TextBox txtYearNbrPrefix;
		private TextBox txtDayNbrPrefix;
		private TextBox txtMonthNbrPrefix;
		private TextBox txtRunNbrDigit;
		private TextBox txtNextId;
		private TextBox txtRemark;
		private Label lblPrefix;
		private Label lblKey;
		private Label lblRunId;
		private ListView lvDocNbr;
		private ColumnHeader colId;
		private ColumnHeader colDocument;
		private ColumnHeader colNextId;
		private ArrayList m_FormatDocList;
		private RunningNumber m_RunNbr;
		private Label lblSampleId;
		private GroupBox groupBox1;
		private UltraOptionSet chkResetOption;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public FrmFormatDocNbr()
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
			Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
			Infragistics.Win.ValueListItem DCI.HRMS = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkResetOption = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
			this.lblSampleId = new System.Windows.Forms.Label();
			this.lblRunId = new System.Windows.Forms.Label();
			this.lblKey = new System.Windows.Forms.Label();
			this.lblPrefix = new System.Windows.Forms.Label();
			this.txtRemark = new System.Windows.Forms.TextBox();
			this.txtNextId = new System.Windows.Forms.TextBox();
			this.txtRunNbrDigit = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.txtMonthNbrPrefix = new System.Windows.Forms.TextBox();
			this.txtDayNbrPrefix = new System.Windows.Forms.TextBox();
			this.txtYearNbrPrefix = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tab = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			this.lvDocNbr = new System.Windows.Forms.ListView();
			this.colId = new System.Windows.Forms.ColumnHeader();
			this.colDocument = new System.Windows.Forms.ColumnHeader();
			this.colNextId = new System.Windows.Forms.ColumnHeader();
			this.ultraTabPageControl1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.chkResetOption)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.tab)).BeginInit();
			this.tab.SuspendLayout();
			this.SuspendLayout();
			// 
			// ultraTabPageControl1
			// 
			this.ultraTabPageControl1.Controls.Add(this.groupBox1);
			this.ultraTabPageControl1.Controls.Add(this.lblSampleId);
			this.ultraTabPageControl1.Controls.Add(this.lblRunId);
			this.ultraTabPageControl1.Controls.Add(this.lblKey);
			this.ultraTabPageControl1.Controls.Add(this.lblPrefix);
			this.ultraTabPageControl1.Controls.Add(this.txtRemark);
			this.ultraTabPageControl1.Controls.Add(this.txtNextId);
			this.ultraTabPageControl1.Controls.Add(this.txtRunNbrDigit);
			this.ultraTabPageControl1.Controls.Add(this.label10);
			this.ultraTabPageControl1.Controls.Add(this.txtMonthNbrPrefix);
			this.ultraTabPageControl1.Controls.Add(this.txtDayNbrPrefix);
			this.ultraTabPageControl1.Controls.Add(this.txtYearNbrPrefix);
			this.ultraTabPageControl1.Controls.Add(this.label9);
			this.ultraTabPageControl1.Controls.Add(this.label8);
			this.ultraTabPageControl1.Controls.Add(this.label7);
			this.ultraTabPageControl1.Controls.Add(this.label6);
			this.ultraTabPageControl1.Controls.Add(this.label5);
			this.ultraTabPageControl1.Controls.Add(this.label4);
			this.ultraTabPageControl1.Controls.Add(this.label3);
			this.ultraTabPageControl1.Controls.Add(this.label2);
			this.ultraTabPageControl1.Location = new System.Drawing.Point(1, 23);
			this.ultraTabPageControl1.Name = "ultraTabPageControl1";
			this.ultraTabPageControl1.Size = new System.Drawing.Size(456, 274);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkResetOption);
			this.groupBox1.Location = new System.Drawing.Point(264, 128);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(180, 68);
			this.groupBox1.TabIndex = 19;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Reset ID Option";
			// 
			// chkResetOption
			// 
			this.chkResetOption.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
			this.chkResetOption.CheckedIndex = 0;
			this.chkResetOption.ItemAppearance = appearance1;
			DCI.HRMS.DataValue = "Y";
			valueListItem1.DisplayText = "Reset when year change.";
			valueListItem2.DataValue = "M";
			valueListItem2.DisplayText = "Reset when month change.";
			this.chkResetOption.Items.Add(valueListItem1);
			this.chkResetOption.Items.Add(valueListItem2);
			this.chkResetOption.ItemSpacingVertical = 5;
			this.chkResetOption.Location = new System.Drawing.Point(12, 20);
			this.chkResetOption.Name = "chkResetOption";
			this.chkResetOption.Size = new System.Drawing.Size(160, 36);
			this.chkResetOption.TabIndex = 0;
			this.chkResetOption.Text = "Reset when year change.";
			// 
			// lblSampleId
			// 
			this.lblSampleId.AutoSize = true;
			this.lblSampleId.BackColor = System.Drawing.Color.Transparent;
			this.lblSampleId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.lblSampleId.Location = new System.Drawing.Point(268, 176);
			this.lblSampleId.Name = "lblSampleId";
			this.lblSampleId.Size = new System.Drawing.Size(0, 17);
			this.lblSampleId.TabIndex = 18;
			// 
			// lblRunId
			// 
			this.lblRunId.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lblRunId.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblRunId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.lblRunId.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblRunId.Location = new System.Drawing.Point(152, 8);
			this.lblRunId.Name = "lblRunId";
			this.lblRunId.Size = new System.Drawing.Size(104, 23);
			this.lblRunId.TabIndex = 17;
			this.lblRunId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblKey
			// 
			this.lblKey.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lblKey.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.lblKey.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblKey.Location = new System.Drawing.Point(152, 32);
			this.lblKey.Name = "lblKey";
			this.lblKey.Size = new System.Drawing.Size(104, 23);
			this.lblKey.TabIndex = 16;
			this.lblKey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblPrefix
			// 
			this.lblPrefix.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lblPrefix.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblPrefix.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.lblPrefix.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblPrefix.Location = new System.Drawing.Point(152, 56);
			this.lblPrefix.Name = "lblPrefix";
			this.lblPrefix.Size = new System.Drawing.Size(104, 23);
			this.lblPrefix.TabIndex = 15;
			this.lblPrefix.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtRemark
			// 
			this.txtRemark.BackColor = System.Drawing.Color.Beige;
			this.txtRemark.Location = new System.Drawing.Point(152, 200);
			this.txtRemark.MaxLength = 4000;
			this.txtRemark.Multiline = true;
			this.txtRemark.Name = "txtRemark";
			this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtRemark.Size = new System.Drawing.Size(292, 68);
			this.txtRemark.TabIndex = 14;
			this.txtRemark.Text = "";
			// 
			// txtNextId
			// 
			this.txtNextId.BackColor = System.Drawing.Color.Beige;
			this.txtNextId.Location = new System.Drawing.Point(152, 176);
			this.txtNextId.MaxLength = 25;
			this.txtNextId.Multiline = true;
			this.txtNextId.Name = "txtNextId";
			this.txtNextId.Size = new System.Drawing.Size(104, 23);
			this.txtNextId.TabIndex = 13;
			this.txtNextId.Text = "";
			// 
			// txtRunNbrDigit
			// 
			this.txtRunNbrDigit.BackColor = System.Drawing.Color.Beige;
			this.txtRunNbrDigit.Location = new System.Drawing.Point(152, 152);
			this.txtRunNbrDigit.MaxLength = 1;
			this.txtRunNbrDigit.Multiline = true;
			this.txtRunNbrDigit.Name = "txtRunNbrDigit";
			this.txtRunNbrDigit.Size = new System.Drawing.Size(104, 23);
			this.txtRunNbrDigit.TabIndex = 12;
			this.txtRunNbrDigit.Text = "";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.BackColor = System.Drawing.Color.Transparent;
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.label10.Location = new System.Drawing.Point(8, 156);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(126, 17);
			this.label10.TabIndex = 11;
			this.label10.Text = "Running Number Digit";
			// 
			// txtMonthNbrPrefix
			// 
			this.txtMonthNbrPrefix.BackColor = System.Drawing.Color.Beige;
			this.txtMonthNbrPrefix.Location = new System.Drawing.Point(152, 104);
			this.txtMonthNbrPrefix.MaxLength = 1;
			this.txtMonthNbrPrefix.Multiline = true;
			this.txtMonthNbrPrefix.Name = "txtMonthNbrPrefix";
			this.txtMonthNbrPrefix.Size = new System.Drawing.Size(104, 23);
			this.txtMonthNbrPrefix.TabIndex = 10;
			this.txtMonthNbrPrefix.Text = "";
			// 
			// txtDayNbrPrefix
			// 
			this.txtDayNbrPrefix.BackColor = System.Drawing.Color.Beige;
			this.txtDayNbrPrefix.Location = new System.Drawing.Point(152, 128);
			this.txtDayNbrPrefix.MaxLength = 1;
			this.txtDayNbrPrefix.Multiline = true;
			this.txtDayNbrPrefix.Name = "txtDayNbrPrefix";
			this.txtDayNbrPrefix.Size = new System.Drawing.Size(104, 23);
			this.txtDayNbrPrefix.TabIndex = 9;
			this.txtDayNbrPrefix.Text = "";
			// 
			// txtYearNbrPrefix
			// 
			this.txtYearNbrPrefix.BackColor = System.Drawing.Color.Beige;
			this.txtYearNbrPrefix.Location = new System.Drawing.Point(152, 80);
			this.txtYearNbrPrefix.MaxLength = 1;
			this.txtYearNbrPrefix.Multiline = true;
			this.txtYearNbrPrefix.Name = "txtYearNbrPrefix";
			this.txtYearNbrPrefix.Size = new System.Drawing.Size(104, 23);
			this.txtYearNbrPrefix.TabIndex = 8;
			this.txtYearNbrPrefix.Text = "";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.BackColor = System.Drawing.Color.Transparent;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.label9.Location = new System.Drawing.Point(8, 204);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(48, 17);
			this.label9.TabIndex = 7;
			this.label9.Text = "Remark";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.BackColor = System.Drawing.Color.Transparent;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.label8.Location = new System.Drawing.Point(8, 180);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(46, 17);
			this.label8.TabIndex = 6;
			this.label8.Text = "Next ID";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.BackColor = System.Drawing.Color.Transparent;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.label7.Location = new System.Drawing.Point(8, 132);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(109, 17);
			this.label7.TabIndex = 5;
			this.label7.Text = "Day Number Prefix";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.BackColor = System.Drawing.Color.Transparent;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.label6.Location = new System.Drawing.Point(8, 108);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(121, 17);
			this.label6.TabIndex = 4;
			this.label6.Text = "Month Number Prefix";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.label5.Location = new System.Drawing.Point(8, 84);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(113, 17);
			this.label5.TabIndex = 3;
			this.label5.Text = "Year Number Prefix";
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.label4.Location = new System.Drawing.Point(8, 60);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 23);
			this.label4.TabIndex = 2;
			this.label4.Text = "Prefix";
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.label3.Location = new System.Drawing.Point(8, 36);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 23);
			this.label3.TabIndex = 1;
			this.label3.Text = "Key";
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.label2.Location = new System.Drawing.Point(8, 12);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 23);
			this.label2.TabIndex = 0;
			this.label2.Text = "Running ID";
			// 
			// tab
			// 
			this.tab.Controls.Add(this.ultraTabSharedControlsPage1);
			this.tab.Controls.Add(this.ultraTabPageControl1);
			this.tab.Location = new System.Drawing.Point(336, 4);
			this.tab.Name = "tab";
			this.tab.SharedControlsPage = this.ultraTabSharedControlsPage1;
			this.tab.Size = new System.Drawing.Size(460, 300);
			this.tab.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage;
			this.tab.TabButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2003ToolbarButton;
			this.tab.TabIndex = 6;
			ultraTab1.Key = "Run";
			ultraTab1.TabPage = this.ultraTabPageControl1;
			ultraTab1.Text = "Run";
			this.tab.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[]
				{
					ultraTab1
				});
			// 
			// ultraTabSharedControlsPage1
			// 
			this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(456, 274);
			// 
			// lvDocNbr
			// 
			this.lvDocNbr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lvDocNbr.Columns.AddRange(new System.Windows.Forms.ColumnHeader[]
				{
					this.colId,
					this.colDocument,
					this.colNextId
				});
			this.lvDocNbr.FullRowSelect = true;
			this.lvDocNbr.GridLines = true;
			this.lvDocNbr.Location = new System.Drawing.Point(4, 8);
			this.lvDocNbr.MultiSelect = false;
			this.lvDocNbr.Name = "lvDocNbr";
			this.lvDocNbr.Size = new System.Drawing.Size(324, 292);
			this.lvDocNbr.TabIndex = 7;
			this.lvDocNbr.View = System.Windows.Forms.View.Details;
			this.lvDocNbr.SelectedIndexChanged += new System.EventHandler(this.lvDocNbr_SelectedIndexChanged);
			// 
			// colId
			// 
			this.colId.Text = "ID";
			// 
			// colDocument
			// 
			this.colDocument.Text = "Document";
			this.colDocument.Width = 120;
			// 
			// colNextId
			// 
			this.colNextId.Text = "Next ID";
			this.colNextId.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.colNextId.Width = 130;
			// 
			// FrmFormatDocNbr
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(804, 314);
			this.Controls.Add(this.lvDocNbr);
			this.Controls.Add(this.tab);
			this.Name = "FrmFormatDocNbr";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.FrmRunNbr_Load);
			this.Closed += new System.EventHandler(this.FrmFormatDocNbr_Closed);
			this.ultraTabPageControl1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) (this.chkResetOption)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.tab)).EndInit();
			this.tab.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		#region IFormChildAction Members

		public void Open()
		{
			this.lvDocNbr.Items.Clear();
			this.ActionStatus = ActionStatus.None;
			this.Clear();
			this.AdvanceSearch();
		}

		public void New()
		{
			// TODO:  Add FrmRunNbr.New implementation
		}

		public void Save()
		{
			if (this.ActionStatus == ActionStatus.Save)
			{
				if (this.m_RunNbr != null)
				{
					try
					{
						string[] checkEmpty = new string[]
							{
								this.txtDayNbrPrefix.Text,
								this.txtMonthNbrPrefix.Text,
								this.txtYearNbrPrefix.Text,
								this.txtNextId.Text,
								this.txtRunNbrDigit.Text
							};

						if (StringHelper.CheckStringEmpty(checkEmpty))
							throw new Exception("กรุณากรอกข้อมูลให้เรียบร้อย");

						this.m_RunNbr.LenDayPrefix = Convert.ToInt32(this.txtDayNbrPrefix.Text);
						this.m_RunNbr.LenMonthPrefix = Convert.ToInt32(this.txtMonthNbrPrefix.Text);
						this.m_RunNbr.LenYearPrefix = Convert.ToInt32(this.txtYearNbrPrefix.Text);
						this.m_RunNbr.NextId = Convert.ToInt32(this.txtNextId.Text);
						this.m_RunNbr.LenRunId = Convert.ToInt32(this.txtRunNbrDigit.Text);
						this.m_RunNbr.Remark = this.txtRemark.Text;
						this.m_RunNbr.ResetOption = this.chkResetOption.Value.ToString();
						DocumentControl.saveFormat(this.m_RunNbr, ApplicationContext.Info.AccountId);
						MessageBox.Show(this, "บันทึกข้อมูลเรียบร้อย", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

						this.Reload();
					}
					catch (Exception ex)
					{
						MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		public void Delete()
		{
			// TODO:  Add FrmRunNbr.Delete implementation
		}

		public void NormalSearch()
		{
			this.m_RunNbr = new RunningNumber();
		}

		public void AdvanceSearch()
		{
			this.lvDocNbr.Items.Clear();
			try
			{
				m_FormatDocList = DocumentControl.findAllFormat();
				if (m_FormatDocList.Count > 0)
				{
					foreach (RunningNumber nbr in m_FormatDocList)
					{
						ListViewItem item = new ListViewItem(nbr.FormatId.ToString());
						item.SubItems.Add(nbr.Key);
						item.SubItems.Add(nbr.ToString(true));
						this.lvDocNbr.Items.Add(item);
					}
				}
			}
			catch
			{
			}
		}

		public void Undo()
		{
			// TODO:  Add FrmRunNbr.Undo implementation
		}

		public void Redo()
		{
			// TODO:  Add FrmRunNbr.Redo implementation
		}

		public void Reload()
		{
			this.AdvanceSearch();
		}

		public void Clear()
		{
			this.tab.Tabs[0].Text = "Document";
			this.txtDayNbrPrefix.Text = "";
			this.txtMonthNbrPrefix.Text = "";
			this.txtNextId.Text = "";
			this.txtRemark.Text = "";
			this.txtRunNbrDigit.Text = "";
			this.txtYearNbrPrefix.Text = "";
			this.lblSampleId.Text = "";
		}

		public void Display()
		{
			// TODO:  Add FrmRunNbr.Display implementation038320769
			if (this.m_RunNbr != null)
			{
				this.tab.Tabs[0].Text = "Document : " + this.m_RunNbr.Key;
				this.lblRunId.Text = this.m_RunNbr.FormatId.ToString();
				this.lblPrefix.Text = this.m_RunNbr.Prefix;
				this.lblKey.Text = this.m_RunNbr.Key;
				this.txtDayNbrPrefix.Text = this.m_RunNbr.LenDayPrefix.ToString();
				this.txtMonthNbrPrefix.Text = this.m_RunNbr.LenMonthPrefix.ToString();
				this.txtYearNbrPrefix.Text = this.m_RunNbr.LenYearPrefix.ToString();
				this.txtRunNbrDigit.Text = this.m_RunNbr.LenRunId.ToString();
				this.txtNextId.Text = this.m_RunNbr.NextId.ToString();
				this.txtRemark.Text = this.m_RunNbr.Remark;
				this.lblSampleId.Text = this.m_RunNbr.ToString(true);
				this.chkResetOption.Value = this.m_RunNbr.ResetOption;
			}
			else
			{
				this.Clear();
			}
		}


		public void EnableControl(ActionStatus status)
		{
		}

		#endregion

		private void FrmRunNbr_Load(object sender, EventArgs e)
		{
			this.IsOpened = true;
			this.Open();
		}

		private void lvDocNbr_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.m_FormatDocList != null && this.m_FormatDocList.Count > 0)
			{
				int formatId = 0;
				try
				{
					formatId = Convert.ToInt32(this.lvDocNbr.SelectedItems[0].Text);
				}
				catch
				{
				}
				foreach (RunningNumber nbr in this.m_FormatDocList)
				{
					if (nbr.FormatId == formatId)
					{
						this.ActionStatus = ActionStatus.Save;
						this.m_RunNbr = nbr;
						this.Display();
						break;
					}
				}
			}
			else
			{
				this.Clear();
			}
		}

		private void FrmFormatDocNbr_Closed(object sender, EventArgs e)
		{
			this.IsOpened = false;
		}
	}
}