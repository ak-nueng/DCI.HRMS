using System;
using System.ComponentModel;
using System.Windows.Forms;
using DCIBizPro.DTO.Production;

namespace DCI.HRMS.Panes
{
	/// <summary>
	/// Summary description for ManualQualityPane.
	/// </summary>
	public class MQPane : UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		private Panel panel1;
		private LabelCaptionPane labelCaptionPane4;
		private LabelCaptionPane labelCaptionPane3;
		private LabelCaptionPane labelCaptionPane2;
		private LabelCaptionPane labelCaptionPane1;
		private ComboBox cboStatus;
		private Label label14;
		private Label label13;
		private Label label12;
		private Label lblAssemblyLine;
		private MQConditionPane mqTemp;
		private MQConditionPane mqOilCharge;
		private MQConditionPane mqWgBefore;
		private MQConditionPane mqFinalWg;
		private MQConditionPane mqCurAmp;
		private MQConditionPane mqVacuumTime;
		private MQConditionPane mqLeak;
		private MQConditionPane mqSoundChk;
		private MQConditionPane mqPowerChk;
		private MQConditionPane mqComTimeChk;
		private MQConditionPane mqN2SuctionChk;
		private MQConditionPane mqInvFlowChk;
		private MQConditionPane mqN2Charge;
		private MQConditionPane mqLowVoltage;
		private Label lblModelCode;
		private ProductMQInfo mq;
        private MQConditionPane mqResistance;
		private bool readOnly = false;

		public MQPane()
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
			}
			get { return null; }
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.panel1 = new System.Windows.Forms.Panel();
            this.mqResistance = new DCI.HRMS.Panes.MQConditionPane();
            this.mqLowVoltage = new DCI.HRMS.Panes.MQConditionPane();
            this.mqN2Charge = new DCI.HRMS.Panes.MQConditionPane();
            this.mqInvFlowChk = new DCI.HRMS.Panes.MQConditionPane();
            this.mqN2SuctionChk = new DCI.HRMS.Panes.MQConditionPane();
            this.mqComTimeChk = new DCI.HRMS.Panes.MQConditionPane();
            this.mqPowerChk = new DCI.HRMS.Panes.MQConditionPane();
            this.mqSoundChk = new DCI.HRMS.Panes.MQConditionPane();
            this.mqLeak = new DCI.HRMS.Panes.MQConditionPane();
            this.mqVacuumTime = new DCI.HRMS.Panes.MQConditionPane();
            this.mqCurAmp = new DCI.HRMS.Panes.MQConditionPane();
            this.mqFinalWg = new DCI.HRMS.Panes.MQConditionPane();
            this.mqWgBefore = new DCI.HRMS.Panes.MQConditionPane();
            this.mqOilCharge = new DCI.HRMS.Panes.MQConditionPane();
            this.mqTemp = new DCI.HRMS.Panes.MQConditionPane();
            this.labelCaptionPane4 = new DCI.HRMS.Panes.LabelCaptionPane();
            this.labelCaptionPane3 = new DCI.HRMS.Panes.LabelCaptionPane();
            this.labelCaptionPane2 = new DCI.HRMS.Panes.LabelCaptionPane();
            this.labelCaptionPane1 = new DCI.HRMS.Panes.LabelCaptionPane();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblAssemblyLine = new System.Windows.Forms.Label();
            this.lblModelCode = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.mqResistance);
            this.panel1.Controls.Add(this.mqLowVoltage);
            this.panel1.Controls.Add(this.mqN2Charge);
            this.panel1.Controls.Add(this.mqInvFlowChk);
            this.panel1.Controls.Add(this.mqN2SuctionChk);
            this.panel1.Controls.Add(this.mqComTimeChk);
            this.panel1.Controls.Add(this.mqPowerChk);
            this.panel1.Controls.Add(this.mqSoundChk);
            this.panel1.Controls.Add(this.mqLeak);
            this.panel1.Controls.Add(this.mqVacuumTime);
            this.panel1.Controls.Add(this.mqCurAmp);
            this.panel1.Controls.Add(this.mqFinalWg);
            this.panel1.Controls.Add(this.mqWgBefore);
            this.panel1.Controls.Add(this.mqOilCharge);
            this.panel1.Controls.Add(this.mqTemp);
            this.panel1.Controls.Add(this.labelCaptionPane4);
            this.panel1.Controls.Add(this.labelCaptionPane3);
            this.panel1.Controls.Add(this.labelCaptionPane2);
            this.panel1.Controls.Add(this.labelCaptionPane1);
            this.panel1.Controls.Add(this.cboStatus);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.lblAssemblyLine);
            this.panel1.Controls.Add(this.lblModelCode);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(340, 450);
            this.panel1.TabIndex = 0;
            // 
            // mqResistance
            // 
            this.mqResistance.Location = new System.Drawing.Point(4, 420);
            this.mqResistance.Max = 0;
            this.mqResistance.Min = 0;
            this.mqResistance.Name = "mqResistance";
            this.mqResistance.Size = new System.Drawing.Size(332, 24);
            this.mqResistance.TabIndex = 83;
            this.mqResistance.Topic = "Resistance:";
            // 
            // mqLowVoltage
            // 
            this.mqLowVoltage.Location = new System.Drawing.Point(4, 396);
            this.mqLowVoltage.Max = 0;
            this.mqLowVoltage.Min = 0;
            this.mqLowVoltage.Name = "mqLowVoltage";
            this.mqLowVoltage.Size = new System.Drawing.Size(332, 24);
            this.mqLowVoltage.TabIndex = 82;
            this.mqLowVoltage.Topic = "Low Voltage Test";
            // 
            // mqN2Charge
            // 
            this.mqN2Charge.Location = new System.Drawing.Point(4, 372);
            this.mqN2Charge.Max = 0;
            this.mqN2Charge.Min = 0;
            this.mqN2Charge.Name = "mqN2Charge";
            this.mqN2Charge.Size = new System.Drawing.Size(332, 24);
            this.mqN2Charge.TabIndex = 81;
            this.mqN2Charge.Topic = "N2 Charge";
            // 
            // mqInvFlowChk
            // 
            this.mqInvFlowChk.Location = new System.Drawing.Point(4, 348);
            this.mqInvFlowChk.Max = 0;
            this.mqInvFlowChk.Min = 0;
            this.mqInvFlowChk.Name = "mqInvFlowChk";
            this.mqInvFlowChk.Size = new System.Drawing.Size(332, 24);
            this.mqInvFlowChk.TabIndex = 80;
            this.mqInvFlowChk.Topic = "Inverse Flow Check";
            // 
            // mqN2SuctionChk
            // 
            this.mqN2SuctionChk.Location = new System.Drawing.Point(4, 324);
            this.mqN2SuctionChk.Max = 0;
            this.mqN2SuctionChk.Min = 0;
            this.mqN2SuctionChk.Name = "mqN2SuctionChk";
            this.mqN2SuctionChk.Size = new System.Drawing.Size(332, 24);
            this.mqN2SuctionChk.TabIndex = 79;
            this.mqN2SuctionChk.Topic = "N2 Suction Check";
            // 
            // mqComTimeChk
            // 
            this.mqComTimeChk.Location = new System.Drawing.Point(4, 300);
            this.mqComTimeChk.Max = 0;
            this.mqComTimeChk.Min = 0;
            this.mqComTimeChk.Name = "mqComTimeChk";
            this.mqComTimeChk.Size = new System.Drawing.Size(332, 24);
            this.mqComTimeChk.TabIndex = 78;
            this.mqComTimeChk.Topic = "Comp Time Check";
            // 
            // mqPowerChk
            // 
            this.mqPowerChk.Location = new System.Drawing.Point(4, 276);
            this.mqPowerChk.Max = 0;
            this.mqPowerChk.Min = 0;
            this.mqPowerChk.Name = "mqPowerChk";
            this.mqPowerChk.Size = new System.Drawing.Size(332, 24);
            this.mqPowerChk.TabIndex = 77;
            this.mqPowerChk.Topic = "Power Check";
            // 
            // mqSoundChk
            // 
            this.mqSoundChk.Location = new System.Drawing.Point(4, 252);
            this.mqSoundChk.Max = 0;
            this.mqSoundChk.Min = 0;
            this.mqSoundChk.Name = "mqSoundChk";
            this.mqSoundChk.Size = new System.Drawing.Size(332, 24);
            this.mqSoundChk.TabIndex = 76;
            this.mqSoundChk.Topic = "Sound Check";
            // 
            // mqLeak
            // 
            this.mqLeak.Location = new System.Drawing.Point(4, 228);
            this.mqLeak.Max = 0;
            this.mqLeak.Min = 0;
            this.mqLeak.Name = "mqLeak";
            this.mqLeak.Size = new System.Drawing.Size(332, 24);
            this.mqLeak.TabIndex = 75;
            this.mqLeak.Topic = "Leak";
            // 
            // mqVacuumTime
            // 
            this.mqVacuumTime.Location = new System.Drawing.Point(4, 204);
            this.mqVacuumTime.Max = 0;
            this.mqVacuumTime.Min = 0;
            this.mqVacuumTime.Name = "mqVacuumTime";
            this.mqVacuumTime.Size = new System.Drawing.Size(332, 24);
            this.mqVacuumTime.TabIndex = 74;
            this.mqVacuumTime.Topic = "Vacuum Time";
            // 
            // mqCurAmp
            // 
            this.mqCurAmp.Location = new System.Drawing.Point(4, 180);
            this.mqCurAmp.Max = 0;
            this.mqCurAmp.Min = 0;
            this.mqCurAmp.Name = "mqCurAmp";
            this.mqCurAmp.Size = new System.Drawing.Size(332, 24);
            this.mqCurAmp.TabIndex = 73;
            this.mqCurAmp.Topic = "Current Amp";
            // 
            // mqFinalWg
            // 
            this.mqFinalWg.Location = new System.Drawing.Point(4, 156);
            this.mqFinalWg.Max = 0;
            this.mqFinalWg.Min = 0;
            this.mqFinalWg.Name = "mqFinalWg";
            this.mqFinalWg.Size = new System.Drawing.Size(332, 24);
            this.mqFinalWg.TabIndex = 72;
            this.mqFinalWg.Topic = "Final weight";
            // 
            // mqWgBefore
            // 
            this.mqWgBefore.Location = new System.Drawing.Point(4, 132);
            this.mqWgBefore.Max = 0;
            this.mqWgBefore.Min = 0;
            this.mqWgBefore.Name = "mqWgBefore";
            this.mqWgBefore.Size = new System.Drawing.Size(332, 24);
            this.mqWgBefore.TabIndex = 71;
            this.mqWgBefore.Topic = "Weight before charge";
            // 
            // mqOilCharge
            // 
            this.mqOilCharge.Location = new System.Drawing.Point(4, 108);
            this.mqOilCharge.Max = 0;
            this.mqOilCharge.Min = 0;
            this.mqOilCharge.Name = "mqOilCharge";
            this.mqOilCharge.Size = new System.Drawing.Size(332, 24);
            this.mqOilCharge.TabIndex = 70;
            this.mqOilCharge.Topic = "Oil charge qty";
            // 
            // mqTemp
            // 
            this.mqTemp.Location = new System.Drawing.Point(4, 84);
            this.mqTemp.Max = 0;
            this.mqTemp.Min = 0;
            this.mqTemp.Name = "mqTemp";
            this.mqTemp.Size = new System.Drawing.Size(332, 24);
            this.mqTemp.TabIndex = 69;
            this.mqTemp.Topic = "Temperature";
            // 
            // labelCaptionPane4
            // 
            this.labelCaptionPane4.Active = false;
            this.labelCaptionPane4.ActiveGradientHighColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(225)))), ((int)(((byte)(155)))));
            this.labelCaptionPane4.ActiveGradientLowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(78)))));
            this.labelCaptionPane4.ActiveTextColor = System.Drawing.Color.Black;
            this.labelCaptionPane4.AllowActive = false;
            this.labelCaptionPane4.AntiAlias = false;
            this.labelCaptionPane4.Caption = "Max";
            this.labelCaptionPane4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.labelCaptionPane4.InactiveGradientHighColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(135)))), ((int)(((byte)(215)))));
            this.labelCaptionPane4.InactiveGradientLowColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(55)))), ((int)(((byte)(145)))));
            this.labelCaptionPane4.InactiveTextColor = System.Drawing.Color.White;
            this.labelCaptionPane4.Location = new System.Drawing.Point(288, 63);
            this.labelCaptionPane4.Name = "labelCaptionPane4";
            this.labelCaptionPane4.Size = new System.Drawing.Size(52, 20);
            this.labelCaptionPane4.TabIndex = 68;
            // 
            // labelCaptionPane3
            // 
            this.labelCaptionPane3.Active = false;
            this.labelCaptionPane3.ActiveGradientHighColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(225)))), ((int)(((byte)(155)))));
            this.labelCaptionPane3.ActiveGradientLowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(78)))));
            this.labelCaptionPane3.ActiveTextColor = System.Drawing.Color.Black;
            this.labelCaptionPane3.AllowActive = false;
            this.labelCaptionPane3.AntiAlias = false;
            this.labelCaptionPane3.Caption = "Min";
            this.labelCaptionPane3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.labelCaptionPane3.InactiveGradientHighColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(135)))), ((int)(((byte)(215)))));
            this.labelCaptionPane3.InactiveGradientLowColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(55)))), ((int)(((byte)(145)))));
            this.labelCaptionPane3.InactiveTextColor = System.Drawing.Color.White;
            this.labelCaptionPane3.Location = new System.Drawing.Point(236, 63);
            this.labelCaptionPane3.Name = "labelCaptionPane3";
            this.labelCaptionPane3.Size = new System.Drawing.Size(54, 20);
            this.labelCaptionPane3.TabIndex = 67;
            // 
            // labelCaptionPane2
            // 
            this.labelCaptionPane2.Active = false;
            this.labelCaptionPane2.ActiveGradientHighColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(225)))), ((int)(((byte)(155)))));
            this.labelCaptionPane2.ActiveGradientLowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(78)))));
            this.labelCaptionPane2.ActiveTextColor = System.Drawing.Color.Black;
            this.labelCaptionPane2.AllowActive = false;
            this.labelCaptionPane2.AntiAlias = false;
            this.labelCaptionPane2.Caption = "Condition";
            this.labelCaptionPane2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.labelCaptionPane2.InactiveGradientHighColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(135)))), ((int)(((byte)(215)))));
            this.labelCaptionPane2.InactiveGradientLowColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(55)))), ((int)(((byte)(145)))));
            this.labelCaptionPane2.InactiveTextColor = System.Drawing.Color.White;
            this.labelCaptionPane2.Location = new System.Drawing.Point(142, 63);
            this.labelCaptionPane2.Name = "labelCaptionPane2";
            this.labelCaptionPane2.Size = new System.Drawing.Size(98, 20);
            this.labelCaptionPane2.TabIndex = 66;
            // 
            // labelCaptionPane1
            // 
            this.labelCaptionPane1.Active = false;
            this.labelCaptionPane1.ActiveGradientHighColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(225)))), ((int)(((byte)(155)))));
            this.labelCaptionPane1.ActiveGradientLowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(78)))));
            this.labelCaptionPane1.ActiveTextColor = System.Drawing.Color.Black;
            this.labelCaptionPane1.AllowActive = false;
            this.labelCaptionPane1.AntiAlias = false;
            this.labelCaptionPane1.Caption = "Check List";
            this.labelCaptionPane1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.labelCaptionPane1.InactiveGradientHighColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(135)))), ((int)(((byte)(215)))));
            this.labelCaptionPane1.InactiveGradientLowColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(55)))), ((int)(((byte)(145)))));
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
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label14.Location = new System.Drawing.Point(214, 36);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 24);
            this.label14.TabIndex = 64;
            this.label14.Text = "Status : ";
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label13.Location = new System.Drawing.Point(16, 36);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(96, 24);
            this.label13.TabIndex = 63;
            this.label13.Text = "Assembly Line :";
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label12.Location = new System.Drawing.Point(16, 12);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 24);
            this.label12.TabIndex = 62;
            this.label12.Text = "Model : ";
            // 
            // lblAssemblyLine
            // 
            this.lblAssemblyLine.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblAssemblyLine.Location = new System.Drawing.Point(118, 36);
            this.lblAssemblyLine.Name = "lblAssemblyLine";
            this.lblAssemblyLine.Size = new System.Drawing.Size(84, 24);
            this.lblAssemblyLine.TabIndex = 37;
            this.lblAssemblyLine.Text = "-";
            // 
            // lblModelCode
            // 
            this.lblModelCode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblModelCode.Location = new System.Drawing.Point(118, 12);
            this.lblModelCode.Name = "lblModelCode";
            this.lblModelCode.Size = new System.Drawing.Size(132, 24);
            this.lblModelCode.TabIndex = 36;
            this.lblModelCode.Text = "-";
            // 
            // MQPane
            // 
            this.Controls.Add(this.panel1);
            this.Name = "MQPane";
            this.Size = new System.Drawing.Size(340, 450);
            this.Load += new System.EventHandler(this.ManualQualityPane_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

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
		public bool ReadOnly
		{
			get{ return readOnly; }
			set
			{
				readOnly = value;

				foreach(Control c in this.panel1.Controls)
				{
					if(c is MQConditionPane)
					{
						MQConditionPane mqCondition = (MQConditionPane)c;
						mqCondition.ReadOnly = readOnly;
					}
				}
			}
		}

		private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		public void SetMQ(ProductMQInfo mq)
		{
			this.mq = mq;
			this.lblModelCode.Text = mq.Code + ":" + mq.Name;
			this.lblAssemblyLine.Text = mq.AssemblyLine.Name;
			
			this.mqComTimeChk.SetCondition(mq.CompTimeCheck);
			this.mqCurAmp.SetCondition(mq.CurrentAmp);
			this.mqFinalWg.SetCondition(mq.FinalWeight);
			this.mqInvFlowChk.SetCondition(mq.InverseFlowCheck);
			this.mqLeak.SetCondition(mq.Leak);
			this.mqLowVoltage.SetCondition(mq.LowVoltageTest);
			this.mqN2Charge.SetCondition(mq.N2Charge);
			this.mqN2SuctionChk.SetCondition(mq.N2SuctionCheck);
			this.mqOilCharge.SetCondition(mq.OilChargeQty);
			this.mqPowerChk.SetCondition(mq.PowerCheck);
			this.mqSoundChk.SetCondition(mq.SoundCheck);
			this.mqTemp.SetCondition(mq.Temperature);
			this.mqVacuumTime.SetCondition(mq.VacuumTime);
			this.mqWgBefore.SetCondition(mq.WeightBeforeCharge);
            this.mqN2Charge.SetCondition(mq.N2Charge);
            this.mqLowVoltage.SetCondition(mq.LowVoltageTest);
            this.mqResistance.SetCondition(mq.Resistance);
		}
		public ProductMQInfo GetMQ()
		{
			mq.CompTimeCheck = this.mqComTimeChk.GetCondition();
			mq.CurrentAmp = this.mqCurAmp.GetCondition();
			mq.FinalWeight = this.mqFinalWg.GetCondition();
			mq.InverseFlowCheck = this.mqInvFlowChk.GetCondition();
			mq.Leak = this.mqLeak.GetCondition();
			mq.LowVoltageTest = this.mqLowVoltage.GetCondition();
			mq.N2Charge = this.mqN2Charge.GetCondition();
			mq.N2SuctionCheck = this.mqN2SuctionChk.GetCondition();
			mq.OilChargeQty = this.mqOilCharge.GetCondition();
			mq.PowerCheck = this.mqPowerChk.GetCondition();
			mq.SoundCheck = this.mqSoundChk.GetCondition();
			mq.Temperature = this.mqTemp.GetCondition();
			mq.VacuumTime = this.mqVacuumTime.GetCondition();
			mq.WeightBeforeCharge = this.mqWgBefore.GetCondition();
            mq.N2Charge = this.mqN2Charge.GetCondition();
            mq.LowVoltageTest = this.mqLowVoltage.GetCondition();
            mq.Resistance = this.mqResistance.GetCondition();
            mq.LastModifiedBy = "TESTER";
			return mq;
		}
		public void Clear()
		{
			this.lblAssemblyLine.Text = string.Empty;
			this.lblModelCode.Text = string.Empty;

			foreach(Control c in this.panel1.Controls)
			{
				if(c is MQConditionPane)
				{
					MQConditionPane mqCondition = (MQConditionPane)c;
					mqCondition.Clear();
				}
			}
		}
	}
}