namespace DCI.HRMS.PSN
{
    partial class FrmEntryPatientRecord
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRecordNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPosition = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSection = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCitizenId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtHospital = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdoSick = new System.Windows.Forms.RadioButton();
            this.rdoAccident = new System.Windows.Forms.RadioButton();
            this.label16 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnViewProfile = new System.Windows.Forms.Button();
            this.picEmp = new System.Windows.Forms.PictureBox();
            this.rdoOut = new System.Windows.Forms.RadioButton();
            this.rdoIn = new System.Windows.Forms.RadioButton();
            this.label15 = new System.Windows.Forms.Label();
            this.txtLine = new System.Windows.Forms.TextBox();
            this.txtRecordDate = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dgMedicineList = new System.Windows.Forms.DataGridView();
            this.dgDiseaseList = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cboDoctor = new System.Windows.Forms.ComboBox();
            this.txtInputBy = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.lblTotalMedicine = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblDisease = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.uclAction = new DCI.HRMS.Controls.Ucl_ActionControl();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEmp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMedicineList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDiseaseList)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(24, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "รหัสพนักงาน";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtCode.Location = new System.Drawing.Point(156, 108);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(100, 22);
            this.txtCode.TabIndex = 11;
            this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmpCode_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(24, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "Record No.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRecordNo
            // 
            this.txtRecordNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtRecordNo.Location = new System.Drawing.Point(156, 60);
            this.txtRecordNo.Name = "txtRecordNo";
            this.txtRecordNo.ReadOnly = true;
            this.txtRecordNo.Size = new System.Drawing.Size(100, 22);
            this.txtRecordNo.TabIndex = 13;
            this.txtRecordNo.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(24, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 15);
            this.label3.TabIndex = 16;
            this.label3.Text = "ชื่อพนักงาน";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFullName
            // 
            this.txtFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtFullName.Location = new System.Drawing.Point(156, 132);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.ReadOnly = true;
            this.txtFullName.Size = new System.Drawing.Size(292, 22);
            this.txtFullName.TabIndex = 15;
            this.txtFullName.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(24, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 15);
            this.label4.TabIndex = 18;
            this.label4.Text = "ตำแหน่ง";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPosition
            // 
            this.txtPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtPosition.Location = new System.Drawing.Point(156, 156);
            this.txtPosition.Name = "txtPosition";
            this.txtPosition.ReadOnly = true;
            this.txtPosition.Size = new System.Drawing.Size(160, 22);
            this.txtPosition.TabIndex = 17;
            this.txtPosition.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(372, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 15);
            this.label5.TabIndex = 20;
            this.label5.Text = "หน่วยงาน";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSection
            // 
            this.txtSection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtSection.Location = new System.Drawing.Point(492, 156);
            this.txtSection.Name = "txtSection";
            this.txtSection.ReadOnly = true;
            this.txtSection.Size = new System.Drawing.Size(232, 22);
            this.txtSection.TabIndex = 19;
            this.txtSection.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(24, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 15);
            this.label6.TabIndex = 22;
            this.label6.Text = "เลขที่ประกันสังคม/ปชช.";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCitizenId
            // 
            this.txtCitizenId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtCitizenId.Location = new System.Drawing.Point(156, 180);
            this.txtCitizenId.Name = "txtCitizenId";
            this.txtCitizenId.ReadOnly = true;
            this.txtCitizenId.Size = new System.Drawing.Size(160, 22);
            this.txtCitizenId.TabIndex = 21;
            this.txtCitizenId.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(372, 184);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 15);
            this.label7.TabIndex = 24;
            this.label7.Text = "รพ.ประกันสังคม";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtHospital
            // 
            this.txtHospital.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtHospital.Location = new System.Drawing.Point(492, 180);
            this.txtHospital.Name = "txtHospital";
            this.txtHospital.ReadOnly = true;
            this.txtHospital.Size = new System.Drawing.Size(282, 22);
            this.txtHospital.TabIndex = 23;
            this.txtHospital.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flowLayoutPanel2);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.btnViewProfile);
            this.panel1.Controls.Add(this.picEmp);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.txtLine);
            this.panel1.Controls.Add(this.uclAction);
            this.panel1.Controls.Add(this.txtRecordDate);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtCode);
            this.panel1.Controls.Add(this.txtHospital);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtRecordNo);
            this.panel1.Controls.Add(this.txtCitizenId);
            this.panel1.Controls.Add(this.txtFullName);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtSection);
            this.panel1.Controls.Add(this.txtPosition);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(782, 244);
            this.panel1.TabIndex = 25;
            // 
            // rdoSick
            // 
            this.rdoSick.AutoSize = true;
            this.rdoSick.Location = new System.Drawing.Point(73, 3);
            this.rdoSick.Name = "rdoSick";
            this.rdoSick.Size = new System.Drawing.Size(63, 17);
            this.rdoSick.TabIndex = 38;
            this.rdoSick.Text = "เจ็บป่วย";
            this.rdoSick.UseVisualStyleBackColor = true;
            this.rdoSick.Click += new System.EventHandler(this.rdoSick_Click);
            // 
            // rdoAccident
            // 
            this.rdoAccident.AutoSize = true;
            this.rdoAccident.Checked = true;
            this.rdoAccident.Location = new System.Drawing.Point(3, 3);
            this.rdoAccident.Name = "rdoAccident";
            this.rdoAccident.Size = new System.Drawing.Size(64, 17);
            this.rdoAccident.TabIndex = 37;
            this.rdoAccident.TabStop = true;
            this.rdoAccident.Text = "อุบัติเหตุ";
            this.rdoAccident.UseVisualStyleBackColor = true;
            this.rdoAccident.Click += new System.EventHandler(this.rdoAccident_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label16.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label16.Location = new System.Drawing.Point(372, 208);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(62, 15);
            this.label16.TabIndex = 36;
            this.label16.Text = "บาดเจ็บจาก";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSearch
            // 
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSearch.Location = new System.Drawing.Point(256, 60);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(26, 24);
            this.btnSearch.TabIndex = 35;
            this.btnSearch.Text = "S";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnViewProfile
            // 
            this.btnViewProfile.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnViewProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnViewProfile.Location = new System.Drawing.Point(256, 108);
            this.btnViewProfile.Name = "btnViewProfile";
            this.btnViewProfile.Size = new System.Drawing.Size(26, 24);
            this.btnViewProfile.TabIndex = 34;
            this.btnViewProfile.Text = "V";
            this.btnViewProfile.UseVisualStyleBackColor = true;
            this.btnViewProfile.Click += new System.EventHandler(this.btnViewProfile_Click);
            // 
            // picEmp
            // 
            this.picEmp.ImageLocation = "";
            this.picEmp.Location = new System.Drawing.Point(672, 48);
            this.picEmp.Name = "picEmp";
            this.picEmp.Size = new System.Drawing.Size(100, 104);
            this.picEmp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picEmp.TabIndex = 32;
            this.picEmp.TabStop = false;
            // 
            // rdoOut
            // 
            this.rdoOut.AutoSize = true;
            this.rdoOut.Location = new System.Drawing.Point(66, 3);
            this.rdoOut.Name = "rdoOut";
            this.rdoOut.Size = new System.Drawing.Size(64, 17);
            this.rdoOut.TabIndex = 31;
            this.rdoOut.Text = "นอกงาน";
            this.rdoOut.UseVisualStyleBackColor = true;
            this.rdoOut.Click += new System.EventHandler(this.rdoOut_Click);
            // 
            // rdoIn
            // 
            this.rdoIn.AutoSize = true;
            this.rdoIn.Checked = true;
            this.rdoIn.Location = new System.Drawing.Point(3, 3);
            this.rdoIn.Name = "rdoIn";
            this.rdoIn.Size = new System.Drawing.Size(57, 17);
            this.rdoIn.TabIndex = 30;
            this.rdoIn.TabStop = true;
            this.rdoIn.Text = "ในงาน";
            this.rdoIn.UseVisualStyleBackColor = true;
            this.rdoIn.Click += new System.EventHandler(this.rdoIn_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label15.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label15.Location = new System.Drawing.Point(24, 208);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(62, 15);
            this.label15.TabIndex = 29;
            this.label15.Text = "บาดเจ็บจาก";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLine
            // 
            this.txtLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtLine.Location = new System.Drawing.Point(726, 156);
            this.txtLine.Name = "txtLine";
            this.txtLine.ReadOnly = true;
            this.txtLine.Size = new System.Drawing.Size(48, 22);
            this.txtLine.TabIndex = 28;
            this.txtLine.TabStop = false;
            // 
            // txtRecordDate
            // 
            this.txtRecordDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtRecordDate.Location = new System.Drawing.Point(156, 84);
            this.txtRecordDate.Name = "txtRecordDate";
            this.txtRecordDate.Size = new System.Drawing.Size(100, 22);
            this.txtRecordDate.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label8.Location = new System.Drawing.Point(24, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 15);
            this.label8.TabIndex = 26;
            this.label8.Text = "วันที่ใช้บริการ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgMedicineList
            // 
            this.dgMedicineList.BackgroundColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.dgMedicineList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMedicineList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgMedicineList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgMedicineList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgMedicineList.Location = new System.Drawing.Point(0, 32);
            this.dgMedicineList.MultiSelect = false;
            this.dgMedicineList.Name = "dgMedicineList";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMedicineList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgMedicineList.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightBlue;
            this.dgMedicineList.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgMedicineList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgMedicineList.Size = new System.Drawing.Size(519, 208);
            this.dgMedicineList.StandardTab = true;
            this.dgMedicineList.TabIndex = 1;
            this.dgMedicineList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMedicineList_CellClick);
            this.dgMedicineList.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgMedicineList_RowsAdded);
            this.dgMedicineList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMedicineList_CellEndEdit);
            this.dgMedicineList.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.gridView_RowPostPaint);
            this.dgMedicineList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgMedicineList_EditingControlShowing);
            this.dgMedicineList.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgMedicineList_RowsRemoved);
            // 
            // dgDiseaseList
            // 
            this.dgDiseaseList.BackgroundColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.dgDiseaseList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDiseaseList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgDiseaseList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDiseaseList.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgDiseaseList.Location = new System.Drawing.Point(20, 0);
            this.dgDiseaseList.MultiSelect = false;
            this.dgDiseaseList.Name = "dgDiseaseList";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDiseaseList.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgDiseaseList.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightBlue;
            this.dgDiseaseList.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgDiseaseList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDiseaseList.Size = new System.Drawing.Size(230, 336);
            this.dgDiseaseList.StandardTab = true;
            this.dgDiseaseList.TabIndex = 0;
            this.dgDiseaseList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDiseaseList_CellClick);
            this.dgDiseaseList.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgDiseaseList_RowsAdded);
            this.dgDiseaseList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDiseaseList_CellEndEdit);
            this.dgDiseaseList.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.gridView_RowPostPaint);
            this.dgDiseaseList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgDiseaseList_EditingControlShowing);
            this.dgDiseaseList.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgDiseaseList_RowsRemoved);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 248);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgDiseaseList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cboDoctor);
            this.splitContainer1.Panel2.Controls.Add(this.txtInputBy);
            this.splitContainer1.Panel2.Controls.Add(this.label14);
            this.splitContainer1.Panel2.Controls.Add(this.label13);
            this.splitContainer1.Panel2.Controls.Add(this.label12);
            this.splitContainer1.Panel2.Controls.Add(this.label11);
            this.splitContainer1.Panel2.Controls.Add(this.txtNote);
            this.splitContainer1.Panel2.Controls.Add(this.lblTotalMedicine);
            this.splitContainer1.Panel2.Controls.Add(this.label10);
            this.splitContainer1.Panel2.Controls.Add(this.lblDisease);
            this.splitContainer1.Panel2.Controls.Add(this.label9);
            this.splitContainer1.Panel2.Controls.Add(this.dgMedicineList);
            this.splitContainer1.Size = new System.Drawing.Size(773, 348);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            // 
            // cboDoctor
            // 
            this.cboDoctor.FormattingEnabled = true;
            this.cboDoctor.Location = new System.Drawing.Point(320, 264);
            this.cboDoctor.Name = "cboDoctor";
            this.cboDoctor.Size = new System.Drawing.Size(196, 21);
            this.cboDoctor.TabIndex = 37;
            // 
            // txtInputBy
            // 
            this.txtInputBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtInputBy.Location = new System.Drawing.Point(320, 312);
            this.txtInputBy.Name = "txtInputBy";
            this.txtInputBy.ReadOnly = true;
            this.txtInputBy.Size = new System.Drawing.Size(196, 22);
            this.txtInputBy.TabIndex = 36;
            this.txtInputBy.TabStop = false;
            this.txtInputBy.Text = "chatthapol.s";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(320, 292);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 13);
            this.label14.TabIndex = 34;
            this.label14.Text = "ผู้บันทึก";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label13.Location = new System.Drawing.Point(320, 244);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(40, 13);
            this.label13.TabIndex = 33;
            this.label13.Text = "ผู้รักษา";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(472, 4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 13);
            this.label12.TabIndex = 32;
            this.label12.Text = "รายการ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 244);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(52, 13);
            this.label11.TabIndex = 29;
            this.label11.Text = "หมายเหคุ";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(0, 264);
            this.txtNote.MaxLength = 500;
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(308, 72);
            this.txtNote.TabIndex = 31;
            this.txtNote.TabStop = false;
            // 
            // lblTotalMedicine
            // 
            this.lblTotalMedicine.AutoSize = true;
            this.lblTotalMedicine.Location = new System.Drawing.Point(424, 4);
            this.lblTotalMedicine.Name = "lblTotalMedicine";
            this.lblTotalMedicine.Size = new System.Drawing.Size(10, 13);
            this.lblTotalMedicine.TabIndex = 5;
            this.lblTotalMedicine.Text = "-";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(320, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "รายการยาที่ใช้";
            // 
            // lblDisease
            // 
            this.lblDisease.AutoSize = true;
            this.lblDisease.Location = new System.Drawing.Point(56, 4);
            this.lblDisease.Name = "lblDisease";
            this.lblDisease.Size = new System.Drawing.Size(10, 13);
            this.lblDisease.TabIndex = 3;
            this.lblDisease.Text = "-";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "โรค : ";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.rdoAccident);
            this.flowLayoutPanel1.Controls.Add(this.rdoSick);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(490, 204);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(178, 30);
            this.flowLayoutPanel1.TabIndex = 39;
            // 
            // uclAction
            // 
            this.uclAction.Dock = System.Windows.Forms.DockStyle.Top;
            this.uclAction.Location = new System.Drawing.Point(0, 0);
            this.uclAction.Name = "uclAction";
            this.uclAction.Size = new System.Drawing.Size(782, 45);
            this.uclAction.TabIndex = 27;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.rdoIn);
            this.flowLayoutPanel2.Controls.Add(this.rdoOut);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(154, 204);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(176, 26);
            this.flowLayoutPanel2.TabIndex = 40;
            // 
            // FrmEntryPatientRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(782, 616);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "FrmEntryPatientRecord";
            this.Text = "Patient Record No. :";
            this.Load += new System.EventHandler(this.FrmPatientRecord_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEmp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMedicineList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDiseaseList)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRecordNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPosition;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSection;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCitizenId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtHospital;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtRecordDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgDiseaseList;
        private DCI.HRMS.Controls.Ucl_ActionControl uclAction;
        private System.Windows.Forms.TextBox txtLine;
        private System.Windows.Forms.DataGridView dgMedicineList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblTotalMedicine;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblDisease;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtInputBy;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.RadioButton rdoOut;
        private System.Windows.Forms.RadioButton rdoIn;
        private System.Windows.Forms.PictureBox picEmp;
        private System.Windows.Forms.ComboBox cboDoctor;
        private System.Windows.Forms.Button btnViewProfile;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.RadioButton rdoSick;
        private System.Windows.Forms.RadioButton rdoAccident;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
    }
}