namespace DCI.HRMS.Personal
{
    partial class FrmSkillAllowance
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSkillAllowance));
            DCI.HRMS.Model.Allowance.EmpCertInfo empCertInfo1 = new DCI.HRMS.Model.Allowance.EmpCertInfo();
            this.ucl_ActionControl1 = new DCI.HRMS.Controls.Ucl_ActionControl();
            this.empData_Control1 = new DCI.HRMS.PER.Controls.EmpData_Control();
            this.kryptonGroup4 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.kryptonGroup5 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.chkShowExpired = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.btnGen = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.cmbSkillMonth = new System.Windows.Forms.ComboBox();
            this.kryptonLabel48 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.txtBarCode = new System.Windows.Forms.TextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel10 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonHeader2 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.kryptonGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.label3 = new System.Windows.Forms.Label();
            this.cbExptSkillYear = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExptSkillDVCD = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExptSkill = new System.Windows.Forms.Button();
            this.EmpCertificate_Control1 = new DCI.HRMS.Personal.Controls.EmpCertificate_Control();
            this.kryptonGroup2 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.kryptonGroup3 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.kryptonHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.dgItems = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kryptonHeader1 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.pnDisplay = new System.Windows.Forms.Panel();
            this.lblDisplay = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup4.Panel)).BeginInit();
            this.kryptonGroup4.Panel.SuspendLayout();
            this.kryptonGroup4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup5.Panel)).BeginInit();
            this.kryptonGroup5.Panel.SuspendLayout();
            this.kryptonGroup5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1.Panel)).BeginInit();
            this.kryptonGroup1.Panel.SuspendLayout();
            this.kryptonGroup1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup2.Panel)).BeginInit();
            this.kryptonGroup2.Panel.SuspendLayout();
            this.kryptonGroup2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup3.Panel)).BeginInit();
            this.kryptonGroup3.Panel.SuspendLayout();
            this.kryptonGroup3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.Panel.SuspendLayout();
            this.kryptonHeaderGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.pnDisplay.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucl_ActionControl1
            // 
            this.ucl_ActionControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucl_ActionControl1.Location = new System.Drawing.Point(0, 0);
            this.ucl_ActionControl1.Name = "ucl_ActionControl1";
            this.ucl_ActionControl1.Size = new System.Drawing.Size(1182, 45);
            this.ucl_ActionControl1.TabIndex = 3;
            // 
            // empData_Control1
            // 
            this.empData_Control1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.empData_Control1.Information = null;
            this.empData_Control1.Location = new System.Drawing.Point(322, 0);
            this.empData_Control1.Name = "empData_Control1";
            this.empData_Control1.Size = new System.Drawing.Size(858, 149);
            this.empData_Control1.TabIndex = 6;
            this.empData_Control1.Load += new System.EventHandler(this.empData_Control1_Load);
            // 
            // kryptonGroup4
            // 
            this.kryptonGroup4.Dock = System.Windows.Forms.DockStyle.Left;
            this.kryptonGroup4.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonGroup4.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonGroup4.Location = new System.Drawing.Point(0, 0);
            this.kryptonGroup4.Name = "kryptonGroup4";
            this.kryptonGroup4.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonGroup4.Panel
            // 
            this.kryptonGroup4.Panel.Controls.Add(this.kryptonGroup5);
            this.kryptonGroup4.Panel.Controls.Add(this.kryptonHeader2);
            this.kryptonGroup4.Size = new System.Drawing.Size(322, 149);
            this.kryptonGroup4.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonGroup4.StateCommon.Border.Rounding = 2;
            this.kryptonGroup4.TabIndex = 0;
            // 
            // kryptonGroup5
            // 
            this.kryptonGroup5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroup5.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonGroup5.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonGroup5.Location = new System.Drawing.Point(0, 27);
            this.kryptonGroup5.Name = "kryptonGroup5";
            this.kryptonGroup5.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonGroup5.Panel
            // 
            this.kryptonGroup5.Panel.Controls.Add(this.chkShowExpired);
            this.kryptonGroup5.Panel.Controls.Add(this.btnGen);
            this.kryptonGroup5.Panel.Controls.Add(this.cmbSkillMonth);
            this.kryptonGroup5.Panel.Controls.Add(this.kryptonLabel48);
            this.kryptonGroup5.Panel.Controls.Add(this.txtRemark);
            this.kryptonGroup5.Panel.Controls.Add(this.txtBarCode);
            this.kryptonGroup5.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonGroup5.Panel.Controls.Add(this.kryptonLabel10);
            this.kryptonGroup5.Size = new System.Drawing.Size(318, 118);
            this.kryptonGroup5.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonGroup5.StateCommon.Border.Rounding = 2;
            this.kryptonGroup5.TabIndex = 1;
            // 
            // chkShowExpired
            // 
            this.chkShowExpired.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.chkShowExpired.Location = new System.Drawing.Point(172, 69);
            this.chkShowExpired.Name = "chkShowExpired";
            this.chkShowExpired.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.chkShowExpired.Size = new System.Drawing.Size(123, 20);
            this.chkShowExpired.TabIndex = 68;
            this.chkShowExpired.Text = "Show Expired Cert";
            this.chkShowExpired.Values.ExtraText = "";
            this.chkShowExpired.Values.Image = null;
            this.chkShowExpired.Values.Text = "Show Expired Cert";
            this.chkShowExpired.CheckedChanged += new System.EventHandler(this.chkShowExpired_CheckedChanged);
            // 
            // btnGen
            // 
            this.btnGen.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.btnGen.Location = new System.Drawing.Point(172, 14);
            this.btnGen.Name = "btnGen";
            this.btnGen.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.btnGen.Size = new System.Drawing.Size(120, 25);
            this.btnGen.TabIndex = 67;
            this.btnGen.Text = "Gen Skill Allow";
            this.btnGen.Values.ExtraText = "";
            this.btnGen.Values.Image = null;
            this.btnGen.Values.ImageStates.ImageCheckedNormal = null;
            this.btnGen.Values.ImageStates.ImageCheckedPressed = null;
            this.btnGen.Values.ImageStates.ImageCheckedTracking = null;
            this.btnGen.Values.Text = "Gen Skill Allow";
            this.btnGen.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // cmbSkillMonth
            // 
            this.cmbSkillMonth.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbSkillMonth.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSkillMonth.FormattingEnabled = true;
            this.cmbSkillMonth.Location = new System.Drawing.Point(59, 15);
            this.cmbSkillMonth.Name = "cmbSkillMonth";
            this.cmbSkillMonth.Size = new System.Drawing.Size(84, 21);
            this.cmbSkillMonth.TabIndex = 0;
            this.cmbSkillMonth.SelectedIndexChanged += new System.EventHandler(this.cmbSkillMonth_SelectedIndexChanged);
            this.cmbSkillMonth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSkillMonth_KeyDown);
            // 
            // kryptonLabel48
            // 
            this.kryptonLabel48.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel48.Location = new System.Drawing.Point(7, 17);
            this.kryptonLabel48.Name = "kryptonLabel48";
            this.kryptonLabel48.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel48.Size = new System.Drawing.Size(50, 20);
            this.kryptonLabel48.TabIndex = 66;
            this.kryptonLabel48.Text = "Month:";
            this.kryptonLabel48.Values.ExtraText = "";
            this.kryptonLabel48.Values.Image = null;
            this.kryptonLabel48.Values.Text = "Month:";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(59, 42);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(233, 20);
            this.txtRemark.TabIndex = 1;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSkillMonth_KeyDown);
            // 
            // txtBarCode
            // 
            this.txtBarCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtBarCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtBarCode.Location = new System.Drawing.Point(58, 68);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Size = new System.Drawing.Size(108, 20);
            this.txtBarCode.TabIndex = 2;
            this.txtBarCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarCode_KeyDown);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel1.Location = new System.Drawing.Point(3, 42);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel1.Size = new System.Drawing.Size(54, 20);
            this.kryptonLabel1.TabIndex = 12;
            this.kryptonLabel1.Text = "Remark:";
            this.kryptonLabel1.Values.ExtraText = "";
            this.kryptonLabel1.Values.Image = null;
            this.kryptonLabel1.Values.Text = "Remark:";
            // 
            // kryptonLabel10
            // 
            this.kryptonLabel10.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel10.Location = new System.Drawing.Point(0, 67);
            this.kryptonLabel10.Name = "kryptonLabel10";
            this.kryptonLabel10.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel10.Size = new System.Drawing.Size(58, 20);
            this.kryptonLabel10.TabIndex = 12;
            this.kryptonLabel10.Text = "Barcode:";
            this.kryptonLabel10.Values.ExtraText = "";
            this.kryptonLabel10.Values.Image = null;
            this.kryptonLabel10.Values.Text = "Barcode:";
            // 
            // kryptonHeader2
            // 
            this.kryptonHeader2.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeader2.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.Form;
            this.kryptonHeader2.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeader2.Name = "kryptonHeader2";
            this.kryptonHeader2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonHeader2.Size = new System.Drawing.Size(318, 27);
            this.kryptonHeader2.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonHeader2.StateCommon.Border.Rounding = 2;
            this.kryptonHeader2.TabIndex = 1;
            this.kryptonHeader2.Text = "เพิ่มข้อมูล";
            this.kryptonHeader2.Values.Description = "Description";
            this.kryptonHeader2.Values.Heading = "เพิ่มข้อมูล";
            this.kryptonHeader2.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonHeader2.Values.Image")));
            // 
            // kryptonGroup1
            // 
            this.kryptonGroup1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonGroup1.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonGroup1.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonGroup1.Location = new System.Drawing.Point(0, 45);
            this.kryptonGroup1.Name = "kryptonGroup1";
            this.kryptonGroup1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonGroup1.Panel
            // 
            this.kryptonGroup1.Panel.Controls.Add(this.label3);
            this.kryptonGroup1.Panel.Controls.Add(this.cbExptSkillYear);
            this.kryptonGroup1.Panel.Controls.Add(this.panel2);
            this.kryptonGroup1.Panel.Controls.Add(this.panel1);
            this.kryptonGroup1.Panel.Controls.Add(this.empData_Control1);
            this.kryptonGroup1.Panel.Controls.Add(this.kryptonGroup4);
            this.kryptonGroup1.Size = new System.Drawing.Size(1182, 151);
            this.kryptonGroup1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(980, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 53;
            this.label3.Text = "ปี : ";
            // 
            // cbExptSkillYear
            // 
            this.cbExptSkillYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExptSkillYear.FormattingEnabled = true;
            this.cbExptSkillYear.Location = new System.Drawing.Point(1003, 31);
            this.cbExptSkillYear.Name = "cbExptSkillYear";
            this.cbExptSkillYear.Size = new System.Drawing.Size(84, 21);
            this.cbExptSkillYear.TabIndex = 52;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btnExptSkillDVCD);
            this.panel2.Location = new System.Drawing.Point(1023, 55);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(84, 87);
            this.panel2.TabIndex = 51;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 37);
            this.label2.TabIndex = 47;
            this.label2.Text = "แบบที่ 2\r\nเครื่องมือวัด";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExptSkillDVCD
            // 
            this.btnExptSkillDVCD.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExptSkillDVCD.Image = global::DCI.HRMS.Properties.Resources.Microsoft_Excel_2013_icon_32;
            this.btnExptSkillDVCD.Location = new System.Drawing.Point(27, 49);
            this.btnExptSkillDVCD.Name = "btnExptSkillDVCD";
            this.btnExptSkillDVCD.Size = new System.Drawing.Size(33, 30);
            this.btnExptSkillDVCD.TabIndex = 46;
            this.btnExptSkillDVCD.UseVisualStyleBackColor = true;
            this.btnExptSkillDVCD.Click += new System.EventHandler(this.btnExptSkillDVCD_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnExptSkill);
            this.panel1.Location = new System.Drawing.Point(957, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(60, 87);
            this.panel1.TabIndex = 50;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 37);
            this.label1.TabIndex = 47;
            this.label1.Text = "แบบที่ 1 ทั่วไป";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExptSkill
            // 
            this.btnExptSkill.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExptSkill.Image = global::DCI.HRMS.Properties.Resources.Microsoft_Excel_2013_icon_32;
            this.btnExptSkill.Location = new System.Drawing.Point(13, 49);
            this.btnExptSkill.Name = "btnExptSkill";
            this.btnExptSkill.Size = new System.Drawing.Size(33, 30);
            this.btnExptSkill.TabIndex = 45;
            this.btnExptSkill.UseVisualStyleBackColor = true;
            this.btnExptSkill.Click += new System.EventHandler(this.btnExptSkill_Click);
            // 
            // EmpCertificate_Control1
            // 
            this.EmpCertificate_Control1.BackColor = System.Drawing.SystemColors.Window;
            this.EmpCertificate_Control1.Dock = System.Windows.Forms.DockStyle.Top;
            this.EmpCertificate_Control1.EditEnable = false;
            empCertInfo1.CerName = "";
            empCertInfo1.CertDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            empCertInfo1.CerType = "";
            empCertInfo1.CreateBy = "";
            empCertInfo1.CreateDateTime = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            empCertInfo1.EmpCode = "";
            empCertInfo1.ExpireDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            empCertInfo1.LastUpdateBy = "";
            empCertInfo1.LastUpdateDateTime = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            empCertInfo1.Level = 0;
            empCertInfo1.RecordId = "";
            empCertInfo1.Remark = "";
            this.EmpCertificate_Control1.Information = empCertInfo1;
            this.EmpCertificate_Control1.Location = new System.Drawing.Point(0, 196);
            this.EmpCertificate_Control1.Name = "EmpCertificate_Control1";
            this.EmpCertificate_Control1.Size = new System.Drawing.Size(1182, 257);
            this.EmpCertificate_Control1.TabIndex = 7;
            // 
            // kryptonGroup2
            // 
            this.kryptonGroup2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroup2.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonGroup2.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonGroup2.Location = new System.Drawing.Point(0, 453);
            this.kryptonGroup2.Name = "kryptonGroup2";
            this.kryptonGroup2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonGroup2.Panel
            // 
            this.kryptonGroup2.Panel.Controls.Add(this.kryptonGroup3);
            this.kryptonGroup2.Panel.Controls.Add(this.kryptonHeader1);
            this.kryptonGroup2.Size = new System.Drawing.Size(1182, 183);
            this.kryptonGroup2.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonGroup2.StateCommon.Border.Rounding = 2;
            this.kryptonGroup2.TabIndex = 11;
            // 
            // kryptonGroup3
            // 
            this.kryptonGroup3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroup3.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonGroup3.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonGroup3.Location = new System.Drawing.Point(0, 27);
            this.kryptonGroup3.Name = "kryptonGroup3";
            this.kryptonGroup3.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonGroup3.Panel
            // 
            this.kryptonGroup3.Panel.Controls.Add(this.kryptonHeaderGroup1);
            this.kryptonGroup3.Size = new System.Drawing.Size(1178, 152);
            this.kryptonGroup3.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonGroup3.StateCommon.Border.Rounding = 2;
            this.kryptonGroup3.TabIndex = 1;
            // 
            // kryptonHeaderGroup1
            // 
            this.kryptonHeaderGroup1.CollapseTarget = ComponentFactory.Krypton.Toolkit.HeaderGroupCollapsedTarget.CollapsedToPrimary;
            this.kryptonHeaderGroup1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonHeaderGroup1.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonHeaderGroup1.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonHeaderGroup1.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Primary;
            this.kryptonHeaderGroup1.HeaderStyleSecondary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Primary;
            this.kryptonHeaderGroup1.HeaderVisiblePrimary = false;
            this.kryptonHeaderGroup1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            this.kryptonHeaderGroup1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonHeaderGroup1.Panel
            // 
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.dgItems);
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(1174, 148);
            this.kryptonHeaderGroup1.TabIndex = 3;
            this.kryptonHeaderGroup1.Text = "Heading";
            this.kryptonHeaderGroup1.ValuesPrimary.Description = "";
            this.kryptonHeaderGroup1.ValuesPrimary.Heading = "Heading";
            this.kryptonHeaderGroup1.ValuesPrimary.Image = ((System.Drawing.Image)(resources.GetObject("kryptonHeaderGroup1.ValuesPrimary.Image")));
            this.kryptonHeaderGroup1.ValuesSecondary.Description = "";
            this.kryptonHeaderGroup1.ValuesSecondary.Heading = "Total";
            this.kryptonHeaderGroup1.ValuesSecondary.Image = null;
            // 
            // dgItems
            // 
            this.dgItems.AllowUserToAddRows = false;
            this.dgItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgItems.ContextMenuStrip = this.contextMenuStrip1;
            this.dgItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgItems.GridStyles.Style = ComponentFactory.Krypton.Toolkit.DataGridViewStyle.Sheet;
            this.dgItems.GridStyles.StyleBackground = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundSheet;
            this.dgItems.GridStyles.StyleColumn = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
            this.dgItems.GridStyles.StyleDataCells = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
            this.dgItems.GridStyles.StyleRow = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
            this.dgItems.Location = new System.Drawing.Point(0, 0);
            this.dgItems.Name = "dgItems";
            this.dgItems.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.dgItems.ReadOnly = true;
            this.dgItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgItems.Size = new System.Drawing.Size(1172, 116);
            this.dgItems.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundSheet;
            this.dgItems.TabIndex = 7;
            this.dgItems.TabStop = false;
            this.dgItems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgItems_CellContentClick);
            this.dgItems.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgItems_RowPostPaint);
            this.dgItems.SelectionChanged += new System.EventHandler(this.dgItems_SelectionChanged);
            this.dgItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgItems_KeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(108, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::DCI.HRMS.Properties.Resources.delete;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // kryptonHeader1
            // 
            this.kryptonHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeader1.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.Form;
            this.kryptonHeader1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeader1.Name = "kryptonHeader1";
            this.kryptonHeader1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonHeader1.Size = new System.Drawing.Size(1178, 27);
            this.kryptonHeader1.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonHeader1.StateCommon.Border.Rounding = 2;
            this.kryptonHeader1.TabIndex = 0;
            this.kryptonHeader1.Text = "Skill Allowance";
            this.kryptonHeader1.Values.Description = "ค่าความสามารถ";
            this.kryptonHeader1.Values.Heading = "Skill Allowance";
            this.kryptonHeader1.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonHeader1.Values.Image")));
            // 
            // pnDisplay
            // 
            this.pnDisplay.BackColor = System.Drawing.Color.Black;
            this.pnDisplay.Controls.Add(this.lblDisplay);
            this.pnDisplay.ForeColor = System.Drawing.Color.White;
            this.pnDisplay.Location = new System.Drawing.Point(245, 23);
            this.pnDisplay.Name = "pnDisplay";
            this.pnDisplay.Size = new System.Drawing.Size(707, 298);
            this.pnDisplay.TabIndex = 12;
            this.pnDisplay.Visible = false;
            // 
            // lblDisplay
            // 
            this.lblDisplay.AutoSize = true;
            this.lblDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblDisplay.Location = new System.Drawing.Point(49, 109);
            this.lblDisplay.Name = "lblDisplay";
            this.lblDisplay.Size = new System.Drawing.Size(607, 73);
            this.lblDisplay.TabIndex = 0;
            this.lblDisplay.Text = "กำลังประมวลผลข้อมูล..";
            // 
            // FrmSkillAllowance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1182, 636);
            this.Controls.Add(this.pnDisplay);
            this.Controls.Add(this.kryptonGroup2);
            this.Controls.Add(this.EmpCertificate_Control1);
            this.Controls.Add(this.kryptonGroup1);
            this.Controls.Add(this.ucl_ActionControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSkillAllowance";
            this.Text = "FrmSkillAllowance";
            this.Load += new System.EventHandler(this.FrmSkillAllowance_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup4.Panel)).EndInit();
            this.kryptonGroup4.Panel.ResumeLayout(false);
            this.kryptonGroup4.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup4)).EndInit();
            this.kryptonGroup4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup5.Panel)).EndInit();
            this.kryptonGroup5.Panel.ResumeLayout(false);
            this.kryptonGroup5.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup5)).EndInit();
            this.kryptonGroup5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1.Panel)).EndInit();
            this.kryptonGroup1.Panel.ResumeLayout(false);
            this.kryptonGroup1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1)).EndInit();
            this.kryptonGroup1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup2.Panel)).EndInit();
            this.kryptonGroup2.Panel.ResumeLayout(false);
            this.kryptonGroup2.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup2)).EndInit();
            this.kryptonGroup2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup3.Panel)).EndInit();
            this.kryptonGroup3.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup3)).EndInit();
            this.kryptonGroup3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            this.kryptonHeaderGroup1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.pnDisplay.ResumeLayout(false);
            this.pnDisplay.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DCI.HRMS.Controls.Ucl_ActionControl ucl_ActionControl1;
        private DCI.HRMS.PER.Controls.EmpData_Control empData_Control1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup4;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup5;
        private System.Windows.Forms.TextBox txtBarCode;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel10;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader kryptonHeader2;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup2;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup3;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgItems;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader kryptonHeader1;
        private System.Windows.Forms.TextBox txtRemark;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private DCI.HRMS.Personal.Controls.EmpCertificate_Control EmpCertificate_Control1;
        private System.Windows.Forms.ComboBox cmbSkillMonth;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel48;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnGen;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkShowExpired;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExptSkillDVCD;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExptSkill;
        private System.Windows.Forms.ComboBox cbExptSkillYear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnDisplay;
        private System.Windows.Forms.Label lblDisplay;
    }
}