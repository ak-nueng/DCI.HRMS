using System;
using System.ComponentModel;
using System.Windows.Forms;
using DCIBizPro.Business.Common;
using DCIBizPro.Business.Document;
using DCIBizPro.DTO.Common;
using DCIBizPro.DTO.Production;

namespace DCI.HRMS.Panes
{
	/// <summary>
	/// Summary description for ManualQualityPane.
	/// </summary>
	public class ManualQualityPane : UserControl, IFormAction
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		private DefaultValueCollection m_OperatorType;
		private Panel panel1;
		private LabelCaptionPane labelCaptionPane4;
		private LabelCaptionPane labelCaptionPane3;
		private LabelCaptionPane labelCaptionPane2;
		private LabelCaptionPane labelCaptionPane1;
		private ComboBox cboStatus;
		private Label label14;
		private Label label13;
		private Label label12;
		private Label label4;
		private Label label11;
		private ConditionPane cdtInvsFlowChk;
		private ConditionPane cdtN2SuctionChk;
		private Label label10;
		private Label label9;
		private Label label8;
		private Label label7;
		private Label label6;
		private Label label5;
		private Label label3;
		private Label label2;
		private Label label1;
		private ConditionPane cdtCompTimeChk;
		private ConditionPane cdtPowerChk;
		private ConditionPane cdtSoundChk;
		private ConditionPane cdtLeak;
		private ConditionPane cdtCurrentAmp;
		private ConditionPane cdtFinalWg;
		private ConditionPane cdtWgBefore;
		private ConditionPane cdtOilChrgQty;
		private ConditionPane cdtVacuumTimeSec;
		private ConditionPane cdtTemp;
		private Label lblAssemblyLine;
		private Label lblModelCode;
		private Label lblFinalWg;
		private ManualQualityInfo m_ManualQuality = new ManualQualityInfo();

		public ManualQualityPane()
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

		public ManualQualityInfo ManualQuality
		{
			set
			{
				this.m_ManualQuality = value;
				this.Display();
			}
			get
			{
				this.Reload();
				return this.m_ManualQuality;
			}
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.cdtVacuumTimeSec = new DCI.HRMS.Panes.ConditionPane();
			this.labelCaptionPane4 = new DCI.HRMS.Panes.LabelCaptionPane();
			this.labelCaptionPane3 = new DCI.HRMS.Panes.LabelCaptionPane();
			this.labelCaptionPane2 = new DCI.HRMS.Panes.LabelCaptionPane();
			this.labelCaptionPane1 = new DCI.HRMS.Panes.LabelCaptionPane();
			this.cboStatus = new System.Windows.Forms.ComboBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.cdtInvsFlowChk = new DCI.HRMS.Panes.ConditionPane();
			this.cdtN2SuctionChk = new DCI.HRMS.Panes.ConditionPane();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.lblFinalWg = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cdtCompTimeChk = new DCI.HRMS.Panes.ConditionPane();
			this.cdtPowerChk = new DCI.HRMS.Panes.ConditionPane();
			this.cdtSoundChk = new DCI.HRMS.Panes.ConditionPane();
			this.cdtLeak = new DCI.HRMS.Panes.ConditionPane();
			this.cdtCurrentAmp = new DCI.HRMS.Panes.ConditionPane();
			this.cdtFinalWg = new DCI.HRMS.Panes.ConditionPane();
			this.cdtWgBefore = new DCI.HRMS.Panes.ConditionPane();
			this.cdtOilChrgQty = new DCI.HRMS.Panes.ConditionPane();
			this.cdtTemp = new DCI.HRMS.Panes.ConditionPane();
			this.lblAssemblyLine = new System.Windows.Forms.Label();
			this.lblModelCode = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.cdtVacuumTimeSec);
			this.panel1.Controls.Add(this.labelCaptionPane4);
			this.panel1.Controls.Add(this.labelCaptionPane3);
			this.panel1.Controls.Add(this.labelCaptionPane2);
			this.panel1.Controls.Add(this.labelCaptionPane1);
			this.panel1.Controls.Add(this.cboStatus);
			this.panel1.Controls.Add(this.label14);
			this.panel1.Controls.Add(this.label13);
			this.panel1.Controls.Add(this.label12);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.label11);
			this.panel1.Controls.Add(this.cdtInvsFlowChk);
			this.panel1.Controls.Add(this.cdtN2SuctionChk);
			this.panel1.Controls.Add(this.label10);
			this.panel1.Controls.Add(this.label9);
			this.panel1.Controls.Add(this.label8);
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.lblFinalWg);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.cdtCompTimeChk);
			this.panel1.Controls.Add(this.cdtPowerChk);
			this.panel1.Controls.Add(this.cdtSoundChk);
			this.panel1.Controls.Add(this.cdtLeak);
			this.panel1.Controls.Add(this.cdtCurrentAmp);
			this.panel1.Controls.Add(this.cdtFinalWg);
			this.panel1.Controls.Add(this.cdtWgBefore);
			this.panel1.Controls.Add(this.cdtOilChrgQty);
			this.panel1.Controls.Add(this.cdtTemp);
			this.panel1.Controls.Add(this.lblAssemblyLine);
			this.panel1.Controls.Add(this.lblModelCode);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(340, 384);
			this.panel1.TabIndex = 0;
			this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
			// 
			// cdtVacuumTimeSec
			// 
			this.cdtVacuumTimeSec.FormActionStatus = DCI.HRMS.FormAction.Save;
			this.cdtVacuumTimeSec.Location = new System.Drawing.Point(140, 208);
			this.cdtVacuumTimeSec.Maximum = 0;
			this.cdtVacuumTimeSec.Minimum = 0;
			this.cdtVacuumTimeSec.Name = "cdtVacuumTimeSec";
			this.cdtVacuumTimeSec.Operator = DCIBizPro.DTO.Common.OperatorType.Equals;
			this.cdtVacuumTimeSec.Size = new System.Drawing.Size(208, 24);
			this.cdtVacuumTimeSec.TabIndex = 6;
			// 
			// labelCaptionPane4
			// 
			this.labelCaptionPane4.Active = false;
			this.labelCaptionPane4.ActiveGradientHighColor = System.Drawing.Color.FromArgb(((System.Byte) (255)), ((System.Byte) (225)), ((System.Byte) (155)));
			this.labelCaptionPane4.ActiveGradientLowColor = System.Drawing.Color.FromArgb(((System.Byte) (255)), ((System.Byte) (165)), ((System.Byte) (78)));
			this.labelCaptionPane4.ActiveTextColor = System.Drawing.Color.Black;
			this.labelCaptionPane4.AllowActive = false;
			this.labelCaptionPane4.AntiAlias = false;
			this.labelCaptionPane4.Caption = "Max";
			this.labelCaptionPane4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
			this.labelCaptionPane4.InactiveGradientHighColor = System.Drawing.Color.FromArgb(((System.Byte) (90)), ((System.Byte) (135)), ((System.Byte) (215)));
			this.labelCaptionPane4.InactiveGradientLowColor = System.Drawing.Color.FromArgb(((System.Byte) (3)), ((System.Byte) (55)), ((System.Byte) (145)));
			this.labelCaptionPane4.InactiveTextColor = System.Drawing.Color.White;
			this.labelCaptionPane4.Location = new System.Drawing.Point(288, 63);
			this.labelCaptionPane4.Name = "labelCaptionPane4";
			this.labelCaptionPane4.Size = new System.Drawing.Size(52, 20);
			this.labelCaptionPane4.TabIndex = 68;
			// 
			// labelCaptionPane3
			// 
			this.labelCaptionPane3.Active = false;
			this.labelCaptionPane3.ActiveGradientHighColor = System.Drawing.Color.FromArgb(((System.Byte) (255)), ((System.Byte) (225)), ((System.Byte) (155)));
			this.labelCaptionPane3.ActiveGradientLowColor = System.Drawing.Color.FromArgb(((System.Byte) (255)), ((System.Byte) (165)), ((System.Byte) (78)));
			this.labelCaptionPane3.ActiveTextColor = System.Drawing.Color.Black;
			this.labelCaptionPane3.AllowActive = false;
			this.labelCaptionPane3.AntiAlias = false;
			this.labelCaptionPane3.Caption = "Min";
			this.labelCaptionPane3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
			this.labelCaptionPane3.InactiveGradientHighColor = System.Drawing.Color.FromArgb(((System.Byte) (90)), ((System.Byte) (135)), ((System.Byte) (215)));
			this.labelCaptionPane3.InactiveGradientLowColor = System.Drawing.Color.FromArgb(((System.Byte) (3)), ((System.Byte) (55)), ((System.Byte) (145)));
			this.labelCaptionPane3.InactiveTextColor = System.Drawing.Color.White;
			this.labelCaptionPane3.Location = new System.Drawing.Point(236, 63);
			this.labelCaptionPane3.Name = "labelCaptionPane3";
			this.labelCaptionPane3.Size = new System.Drawing.Size(54, 20);
			this.labelCaptionPane3.TabIndex = 67;
			// 
			// labelCaptionPane2
			// 
			this.labelCaptionPane2.Active = false;
			this.labelCaptionPane2.ActiveGradientHighColor = System.Drawing.Color.FromArgb(((System.Byte) (255)), ((System.Byte) (225)), ((System.Byte) (155)));
			this.labelCaptionPane2.ActiveGradientLowColor = System.Drawing.Color.FromArgb(((System.Byte) (255)), ((System.Byte) (165)), ((System.Byte) (78)));
			this.labelCaptionPane2.ActiveTextColor = System.Drawing.Color.Black;
			this.labelCaptionPane2.AllowActive = false;
			this.labelCaptionPane2.AntiAlias = false;
			this.labelCaptionPane2.Caption = "Condition";
			this.labelCaptionPane2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
			this.labelCaptionPane2.InactiveGradientHighColor = System.Drawing.Color.FromArgb(((System.Byte) (90)), ((System.Byte) (135)), ((System.Byte) (215)));
			this.labelCaptionPane2.InactiveGradientLowColor = System.Drawing.Color.FromArgb(((System.Byte) (3)), ((System.Byte) (55)), ((System.Byte) (145)));
			this.labelCaptionPane2.InactiveTextColor = System.Drawing.Color.White;
			this.labelCaptionPane2.Location = new System.Drawing.Point(142, 63);
			this.labelCaptionPane2.Name = "labelCaptionPane2";
			this.labelCaptionPane2.Size = new System.Drawing.Size(98, 20);
			this.labelCaptionPane2.TabIndex = 66;
			// 
			// labelCaptionPane1
			// 
			this.labelCaptionPane1.Active = false;
			this.labelCaptionPane1.ActiveGradientHighColor = System.Drawing.Color.FromArgb(((System.Byte) (255)), ((System.Byte) (225)), ((System.Byte) (155)));
			this.labelCaptionPane1.ActiveGradientLowColor = System.Drawing.Color.FromArgb(((System.Byte) (255)), ((System.Byte) (165)), ((System.Byte) (78)));
			this.labelCaptionPane1.ActiveTextColor = System.Drawing.Color.Black;
			this.labelCaptionPane1.AllowActive = false;
			this.labelCaptionPane1.AntiAlias = false;
			this.labelCaptionPane1.Caption = "Check List";
			this.labelCaptionPane1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
			this.labelCaptionPane1.InactiveGradientHighColor = System.Drawing.Color.FromArgb(((System.Byte) (90)), ((System.Byte) (135)), ((System.Byte) (215)));
			this.labelCaptionPane1.InactiveGradientLowColor = System.Drawing.Color.FromArgb(((System.Byte) (3)), ((System.Byte) (55)), ((System.Byte) (145)));
			this.labelCaptionPane1.InactiveTextColor = System.Drawing.Color.White;
			this.labelCaptionPane1.Location = new System.Drawing.Point(0, 63);
			this.labelCaptionPane1.Name = "labelCaptionPane1";
			this.labelCaptionPane1.Size = new System.Drawing.Size(144, 20);
			this.labelCaptionPane1.TabIndex = 65;
			// 
			// cboStatus
			// 
			this.cboStatus.BackColor = System.Drawing.Color.Beige;
			this.cboStatus.Location = new System.Drawing.Point(262, 36);
			this.cboStatus.Name = "cboStatus";
			this.cboStatus.Size = new System.Drawing.Size(78, 21);
			this.cboStatus.TabIndex = 0;
			this.cboStatus.SelectedIndexChanged += new System.EventHandler(this.cboStatus_SelectedIndexChanged);
			// 
			// label14
			// 
			this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (177)));
			this.label14.Location = new System.Drawing.Point(214, 36);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(48, 24);
			this.label14.TabIndex = 64;
			this.label14.Text = "Status : ";
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (177)));
			this.label13.Location = new System.Drawing.Point(16, 36);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(96, 24);
			this.label13.TabIndex = 63;
			this.label13.Text = "Assembly Line :";
			// 
			// label12
			// 
			this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (177)));
			this.label12.Location = new System.Drawing.Point(16, 12);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(60, 24);
			this.label12.TabIndex = 62;
			this.label12.Text = "Model : ";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (177)));
			this.label4.Location = new System.Drawing.Point(10, 356);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(114, 24);
			this.label4.TabIndex = 61;
			this.label4.Text = "Inverse Flow Check : ";
			// 
			// label11
			// 
			this.label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (177)));
			this.label11.Location = new System.Drawing.Point(10, 332);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(114, 24);
			this.label11.TabIndex = 60;
			this.label11.Text = "N2 Suction Check : ";
			// 
			// cdtInvsFlowChk
			// 
			this.cdtInvsFlowChk.FormActionStatus = DCI.HRMS.FormAction.Save;
			this.cdtInvsFlowChk.Location = new System.Drawing.Point(140, 351);
			this.cdtInvsFlowChk.Maximum = 0;
			this.cdtInvsFlowChk.Minimum = 0;
			this.cdtInvsFlowChk.Name = "cdtInvsFlowChk";
			this.cdtInvsFlowChk.Operator = DCIBizPro.DTO.Common.OperatorType.Equals;
			this.cdtInvsFlowChk.Size = new System.Drawing.Size(204, 24);
			this.cdtInvsFlowChk.TabIndex = 12;
			// 
			// cdtN2SuctionChk
			// 
			this.cdtN2SuctionChk.FormActionStatus = DCI.HRMS.FormAction.Save;
			this.cdtN2SuctionChk.Location = new System.Drawing.Point(140, 327);
			this.cdtN2SuctionChk.Maximum = 0;
			this.cdtN2SuctionChk.Minimum = 0;
			this.cdtN2SuctionChk.Name = "cdtN2SuctionChk";
			this.cdtN2SuctionChk.Operator = DCIBizPro.DTO.Common.OperatorType.Equals;
			this.cdtN2SuctionChk.Size = new System.Drawing.Size(204, 24);
			this.cdtN2SuctionChk.TabIndex = 11;
			// 
			// label10
			// 
			this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (177)));
			this.label10.Location = new System.Drawing.Point(10, 308);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(102, 24);
			this.label10.TabIndex = 59;
			this.label10.Text = "Comp Time Check : ";
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (177)));
			this.label9.Location = new System.Drawing.Point(10, 284);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(84, 24);
			this.label9.TabIndex = 58;
			this.label9.Text = "Power Check : ";
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (177)));
			this.label8.Location = new System.Drawing.Point(10, 260);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(84, 24);
			this.label8.TabIndex = 57;
			this.label8.Text = "Sound Check : ";
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (177)));
			this.label7.Location = new System.Drawing.Point(10, 236);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(84, 24);
			this.label7.TabIndex = 56;
			this.label7.Text = "Leak : ";
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (177)));
			this.label6.Location = new System.Drawing.Point(10, 212);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(84, 24);
			this.label6.TabIndex = 55;
			this.label6.Text = "Vacuum Time : ";
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (177)));
			this.label5.Location = new System.Drawing.Point(10, 188);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(84, 24);
			this.label5.TabIndex = 54;
			this.label5.Text = "Current Amp : ";
			// 
			// lblFinalWg
			// 
			this.lblFinalWg.AutoSize = true;
			this.lblFinalWg.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (177)));
			this.lblFinalWg.Location = new System.Drawing.Point(10, 164);
			this.lblFinalWg.Name = "lblFinalWg";
			this.lblFinalWg.Size = new System.Drawing.Size(77, 17);
			this.lblFinalWg.TabIndex = 53;
			this.lblFinalWg.Text = "Final Weight : ";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (177)));
			this.label3.Location = new System.Drawing.Point(10, 140);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(126, 24);
			this.label3.TabIndex = 52;
			this.label3.Text = "Weight Before Charge :";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (177)));
			this.label2.Location = new System.Drawing.Point(10, 116);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(108, 24);
			this.label2.TabIndex = 51;
			this.label2.Text = "Oil Charge Qty :";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (177)));
			this.label1.Location = new System.Drawing.Point(10, 92);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84, 24);
			this.label1.TabIndex = 50;
			this.label1.Text = "Tempperature :";
			// 
			// cdtCompTimeChk
			// 
			this.cdtCompTimeChk.FormActionStatus = DCI.HRMS.FormAction.Save;
			this.cdtCompTimeChk.Location = new System.Drawing.Point(140, 303);
			this.cdtCompTimeChk.Maximum = 0;
			this.cdtCompTimeChk.Minimum = 0;
			this.cdtCompTimeChk.Name = "cdtCompTimeChk";
			this.cdtCompTimeChk.Operator = DCIBizPro.DTO.Common.OperatorType.Equals;
			this.cdtCompTimeChk.Size = new System.Drawing.Size(204, 24);
			this.cdtCompTimeChk.TabIndex = 10;
			// 
			// cdtPowerChk
			// 
			this.cdtPowerChk.FormActionStatus = DCI.HRMS.FormAction.Save;
			this.cdtPowerChk.Location = new System.Drawing.Point(140, 279);
			this.cdtPowerChk.Maximum = 0;
			this.cdtPowerChk.Minimum = 0;
			this.cdtPowerChk.Name = "cdtPowerChk";
			this.cdtPowerChk.Operator = DCIBizPro.DTO.Common.OperatorType.Equals;
			this.cdtPowerChk.Size = new System.Drawing.Size(204, 24);
			this.cdtPowerChk.TabIndex = 9;
			// 
			// cdtSoundChk
			// 
			this.cdtSoundChk.FormActionStatus = DCI.HRMS.FormAction.Save;
			this.cdtSoundChk.Location = new System.Drawing.Point(140, 255);
			this.cdtSoundChk.Maximum = 0;
			this.cdtSoundChk.Minimum = 0;
			this.cdtSoundChk.Name = "cdtSoundChk";
			this.cdtSoundChk.Operator = DCIBizPro.DTO.Common.OperatorType.Equals;
			this.cdtSoundChk.Size = new System.Drawing.Size(204, 24);
			this.cdtSoundChk.TabIndex = 8;
			// 
			// cdtLeak
			// 
			this.cdtLeak.FormActionStatus = DCI.HRMS.FormAction.Save;
			this.cdtLeak.Location = new System.Drawing.Point(140, 231);
			this.cdtLeak.Maximum = 0;
			this.cdtLeak.Minimum = 0;
			this.cdtLeak.Name = "cdtLeak";
			this.cdtLeak.Operator = DCIBizPro.DTO.Common.OperatorType.Equals;
			this.cdtLeak.Size = new System.Drawing.Size(204, 24);
			this.cdtLeak.TabIndex = 7;
			// 
			// cdtCurrentAmp
			// 
			this.cdtCurrentAmp.FormActionStatus = DCI.HRMS.FormAction.Save;
			this.cdtCurrentAmp.Location = new System.Drawing.Point(140, 183);
			this.cdtCurrentAmp.Maximum = 0;
			this.cdtCurrentAmp.Minimum = 0;
			this.cdtCurrentAmp.Name = "cdtCurrentAmp";
			this.cdtCurrentAmp.Operator = DCIBizPro.DTO.Common.OperatorType.Equals;
			this.cdtCurrentAmp.Size = new System.Drawing.Size(204, 24);
			this.cdtCurrentAmp.TabIndex = 5;
			// 
			// cdtFinalWg
			// 
			this.cdtFinalWg.FormActionStatus = DCI.HRMS.FormAction.Save;
			this.cdtFinalWg.Location = new System.Drawing.Point(140, 159);
			this.cdtFinalWg.Maximum = 0;
			this.cdtFinalWg.Minimum = 0;
			this.cdtFinalWg.Name = "cdtFinalWg";
			this.cdtFinalWg.Operator = DCIBizPro.DTO.Common.OperatorType.Equals;
			this.cdtFinalWg.Size = new System.Drawing.Size(204, 24);
			this.cdtFinalWg.TabIndex = 4;
			// 
			// cdtWgBefore
			// 
			this.cdtWgBefore.FormActionStatus = DCI.HRMS.FormAction.Save;
			this.cdtWgBefore.Location = new System.Drawing.Point(140, 135);
			this.cdtWgBefore.Maximum = 0;
			this.cdtWgBefore.Minimum = 0;
			this.cdtWgBefore.Name = "cdtWgBefore";
			this.cdtWgBefore.Operator = DCIBizPro.DTO.Common.OperatorType.Equals;
			this.cdtWgBefore.Size = new System.Drawing.Size(204, 24);
			this.cdtWgBefore.TabIndex = 3;
			// 
			// cdtOilChrgQty
			// 
			this.cdtOilChrgQty.FormActionStatus = DCI.HRMS.FormAction.Save;
			this.cdtOilChrgQty.Location = new System.Drawing.Point(140, 111);
			this.cdtOilChrgQty.Maximum = 0;
			this.cdtOilChrgQty.Minimum = 0;
			this.cdtOilChrgQty.Name = "cdtOilChrgQty";
			this.cdtOilChrgQty.Operator = DCIBizPro.DTO.Common.OperatorType.Equals;
			this.cdtOilChrgQty.Size = new System.Drawing.Size(204, 24);
			this.cdtOilChrgQty.TabIndex = 2;
			// 
			// cdtTemp
			// 
			this.cdtTemp.FormActionStatus = DCI.HRMS.FormAction.Save;
			this.cdtTemp.Location = new System.Drawing.Point(140, 87);
			this.cdtTemp.Maximum = 0;
			this.cdtTemp.Minimum = 0;
			this.cdtTemp.Name = "cdtTemp";
			this.cdtTemp.Operator = DCIBizPro.DTO.Common.OperatorType.Equals;
			this.cdtTemp.Size = new System.Drawing.Size(204, 24);
			this.cdtTemp.TabIndex = 1;
			// 
			// lblAssemblyLine
			// 
			this.lblAssemblyLine.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte) (177)));
			this.lblAssemblyLine.Location = new System.Drawing.Point(118, 36);
			this.lblAssemblyLine.Name = "lblAssemblyLine";
			this.lblAssemblyLine.Size = new System.Drawing.Size(84, 24);
			this.lblAssemblyLine.TabIndex = 37;
			this.lblAssemblyLine.Text = "-";
			// 
			// lblModelCode
			// 
			this.lblModelCode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte) (177)));
			this.lblModelCode.Location = new System.Drawing.Point(118, 12);
			this.lblModelCode.Name = "lblModelCode";
			this.lblModelCode.Size = new System.Drawing.Size(132, 24);
			this.lblModelCode.TabIndex = 36;
			this.lblModelCode.Text = "-";
			// 
			// ManualQualityPane
			// 
			this.Controls.Add(this.panel1);
			this.Name = "ManualQualityPane";
			this.Size = new System.Drawing.Size(340, 384);
			this.Load += new System.EventHandler(this.ManualQualityPane_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		#region IFormAction Members

		public void New()
		{
			// TODO:  Add ManualQualityPane.New implementation
		}

		public void Save()
		{
			// TODO:  Add ManualQualityPane.Save implementation
		}

		public void Delete()
		{
			// TODO:  Add ManualQualityPane.Delete implementation
		}

		public void Undo()
		{
			// TODO:  Add ManualQualityPane.Undo implementation
		}

		public void Redo()
		{
			// TODO:  Add ManualQualityPane.Redo implementation
		}

		public FormAction FormActionStatus
		{
			set
			{
			}
			get { return FormAction.Save; }
		}

		public void Reset()
		{
			this.lblAssemblyLine.Text = "-";
			this.lblModelCode.Text = "-";

			try
			{
				this.cboStatus.DataBindings.Clear();
				this.cboStatus.DataSource = DocumentControl.findStatus("Status");
				this.cboStatus.DisplayMember = "DefaultDescription";
				this.cboStatus.ValueMember = "DefaultData";
			}
			catch
			{
			}

			this.m_OperatorType = DefaultEngine.findOperatorTypeList();

			this.SetOperatorTypeList(this.cdtTemp, this.m_OperatorType);
			this.SetOperatorTypeList(this.cdtOilChrgQty, this.m_OperatorType);
			this.SetOperatorTypeList(this.cdtWgBefore, this.m_OperatorType);
			this.SetOperatorTypeList(this.cdtFinalWg, this.m_OperatorType);
			this.SetOperatorTypeList(this.cdtLeak, this.m_OperatorType);
			this.SetOperatorTypeList(this.cdtCurrentAmp, this.m_OperatorType);
			this.SetOperatorTypeList(this.cdtVacuumTimeSec, this.m_OperatorType);
			this.SetOperatorTypeList(this.cdtSoundChk, this.m_OperatorType);
			this.SetOperatorTypeList(this.cdtPowerChk, this.m_OperatorType);
			this.SetOperatorTypeList(this.cdtCompTimeChk, this.m_OperatorType);
			this.SetOperatorTypeList(this.cdtN2SuctionChk, this.m_OperatorType);
			this.SetOperatorTypeList(this.cdtInvsFlowChk, this.m_OperatorType);

			this.cdtTemp.Reset();
			this.cdtOilChrgQty.Reset();
			this.cdtWgBefore.Reset();
			this.cdtFinalWg.Reset();
			this.cdtCurrentAmp.Reset();
			this.cdtVacuumTimeSec.Reset();
			this.cdtLeak.Reset();
			this.cdtSoundChk.Reset();
			this.cdtPowerChk.Reset();
			this.cdtCompTimeChk.Reset();
			this.cdtN2SuctionChk.Reset();
			this.cdtInvsFlowChk.Reset();
		}

		private void SetOperatorTypeList(ConditionPane pane, DefaultValueCollection operatorList)
		{
			pane.OperatorTypeList = (DefaultValueCollection) operatorList.Clone();
			//pane.OperatorTypeList = operatorList;
		}

		public void Display()
		{
			try
			{
				this.lblAssemblyLine.Text = this.m_ManualQuality.AssemblyLine.AssemblyLineCode;
				this.lblModelCode.Text = this.m_ManualQuality.Item.Code;
				this.cboStatus.SelectedValue = this.m_ManualQuality.Status;

				this.SetConditionValue(this.m_ManualQuality.Tempperature, this.cdtTemp, false);
				this.SetConditionValue(this.m_ManualQuality.OilChargeQty, this.cdtOilChrgQty, false);
				this.SetConditionValue(this.m_ManualQuality.WeightBeforeCharge, this.cdtWgBefore, false);
				this.SetConditionValue(this.m_ManualQuality.FinalWeight, this.cdtFinalWg, false);
				this.SetConditionValue(this.m_ManualQuality.CurrentAmp, this.cdtCurrentAmp, false);
				this.SetConditionValue(this.m_ManualQuality.VacuumTime, this.cdtVacuumTimeSec, false);
				this.SetConditionValue(this.m_ManualQuality.Leak, this.cdtLeak, false);
				this.SetConditionValue(this.m_ManualQuality.SoundCheck, this.cdtSoundChk, false);
				this.SetConditionValue(this.m_ManualQuality.CompTimeCheck, this.cdtCompTimeChk, false);
				this.SetConditionValue(this.m_ManualQuality.PowerCheck, this.cdtPowerChk, false);
				this.SetConditionValue(this.m_ManualQuality.N2SuctionCheck, this.cdtN2SuctionChk, false);
				this.SetConditionValue(this.m_ManualQuality.InverseFlowCheck, this.cdtInvsFlowChk, false);

				if (this.m_ManualQuality.Item.Type.Equals("SC"))
				{
					this.lblFinalWg.Text = "Weight After Charge : ";
				}
				else
				{
					this.lblFinalWg.Text = "Final Weight : ";
				}
			}
			catch
			{
			}
		}

		/// <summary>
		/// Refresh MQ Data
		/// </summary>
		public void Reload()
		{
			try
			{
				this.m_ManualQuality.AssemblyLine.AssemblyLineCode = this.lblAssemblyLine.Text;
				this.m_ManualQuality.Item.Code = this.lblModelCode.Text;
				this.m_ManualQuality.Status = this.cboStatus.SelectedValue.ToString();

				this.SetConditionValue(this.m_ManualQuality.Tempperature, this.cdtTemp, true);
				this.SetConditionValue(this.m_ManualQuality.OilChargeQty, this.cdtOilChrgQty, true);
				this.SetConditionValue(this.m_ManualQuality.WeightBeforeCharge, this.cdtWgBefore, true);
				this.SetConditionValue(this.m_ManualQuality.FinalWeight, this.cdtFinalWg, true);
				this.SetConditionValue(this.m_ManualQuality.CurrentAmp, this.cdtCurrentAmp, true);
				this.SetConditionValue(this.m_ManualQuality.VacuumTime, this.cdtVacuumTimeSec, true);
				this.SetConditionValue(this.m_ManualQuality.Leak, this.cdtLeak, true);
				this.SetConditionValue(this.m_ManualQuality.SoundCheck, this.cdtSoundChk, true);
				this.SetConditionValue(this.m_ManualQuality.CompTimeCheck, this.cdtCompTimeChk, true);
				this.SetConditionValue(this.m_ManualQuality.PowerCheck, this.cdtPowerChk, true);
				this.SetConditionValue(this.m_ManualQuality.N2SuctionCheck, this.cdtN2SuctionChk, true);
				this.SetConditionValue(this.m_ManualQuality.InverseFlowCheck, this.cdtInvsFlowChk, true);


			}
			catch
			{
			}
		}

/*
		private void SetConditionValue(ConditionValue baseValue , ConditionValue toValue)
		{
			baseValue.Condition = toValue.Condition;
			baseValue.Minimum = toValue.Minimum;
			baseValue.Maximum = toValue.Maximum;
		}
*/

		private void SetConditionValue(ConditionValue condition, ConditionPane pane, bool isSwitchToObject)
		{
			if (isSwitchToObject)
			{
				pane.Reload();
				condition = pane.Condition;
			}
			else
			{
				pane.Condition = condition;
			}

		}

		#endregion

		private void ManualQualityPane_Load(object sender, EventArgs e)
		{
			this.labelCaptionPane1.Text = "Check List";
			this.labelCaptionPane2.Text = "Condition";
			this.labelCaptionPane3.Text = "Min";
			this.labelCaptionPane4.Text = "Max";
		}

		public BorderStyle FrameBorderStyle
		{
			set { this.panel1.BorderStyle = value; }
			get { return this.panel1.BorderStyle; }
		}

		private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				this.m_ManualQuality.Status = this.cboStatus.SelectedValue.ToString();
			}
			catch
			{
			}
		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{
		}
	}
}