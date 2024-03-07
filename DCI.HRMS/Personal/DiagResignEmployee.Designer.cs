namespace DCI.HRMS.Personal
{
    partial class DiagResignEmployee
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiagResignEmployee));
            this.kryptonHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.cmbResignType = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtRsRemark = new System.Windows.Forms.TextBox();
            this.txtRsReason = new System.Windows.Forms.TextBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.empDetail_Control1 = new DCI.HRMS.Controls.EmpDetail_Control();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.Panel.SuspendLayout();
            this.kryptonHeaderGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonHeaderGroup1
            // 
            this.kryptonHeaderGroup1.CollapseTarget = ComponentFactory.Krypton.Toolkit.HeaderGroupCollapsedTarget.CollapsedToPrimary;
            this.kryptonHeaderGroup1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeaderGroup1.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonHeaderGroup1.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonHeaderGroup1.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Form;
            this.kryptonHeaderGroup1.HeaderStyleSecondary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.kryptonHeaderGroup1.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            this.kryptonHeaderGroup1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonHeaderGroup1.Panel
            // 
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.cmbResignType);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.dateTimePicker1);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtCode);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.btnSave);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel2);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel8);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel5);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtRsRemark);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtRsReason);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel4);
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(589, 125);
            this.kryptonHeaderGroup1.TabIndex = 0;
            this.kryptonHeaderGroup1.Text = "ป้อนข้อมูล";
            this.kryptonHeaderGroup1.ValuesPrimary.Description = "";
            this.kryptonHeaderGroup1.ValuesPrimary.Heading = "ป้อนข้อมูล";
            this.kryptonHeaderGroup1.ValuesPrimary.Image = global::DCI.HRMS.Properties.Resources.add;
            this.kryptonHeaderGroup1.ValuesSecondary.Description = "";
            this.kryptonHeaderGroup1.ValuesSecondary.Heading = "Description";
            this.kryptonHeaderGroup1.ValuesSecondary.Image = null;
            // 
            // cmbResignType
            // 
            this.cmbResignType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbResignType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbResignType.FormattingEnabled = true;
            this.cmbResignType.Location = new System.Drawing.Point(289, 32);
            this.cmbResignType.Name = "cmbResignType";
            this.cmbResignType.Size = new System.Drawing.Size(71, 21);
            this.cmbResignType.TabIndex = 2;
            this.cmbResignType.SelectedIndexChanged += new System.EventHandler(this.cmbResignType_SelectedIndexChanged);
            this.cmbResignType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker1_KeyDown);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.BackColor = System.Drawing.SystemColors.Window;
            this.dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(96, 32);
            this.dateTimePicker1.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(87, 20);
            this.dateTimePicker1.TabIndex = 1;
            this.dateTimePicker1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker1_KeyDown);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(97, 9);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(102, 20);
            this.txtCode.TabIndex = 0;
            this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCode_KeyDown);
            // 
            // btnSave
            // 
            this.btnSave.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(491, 59);
            this.btnSave.Name = "btnSave";
            this.btnSave.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.btnSave.Size = new System.Drawing.Size(90, 25);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.Values.ExtraText = "";
            this.btnSave.Values.Image = global::DCI.HRMS.Properties.Resources.save;
            this.btnSave.Values.ImageStates.ImageCheckedNormal = null;
            this.btnSave.Values.ImageStates.ImageCheckedPressed = null;
            this.btnSave.Values.ImageStates.ImageCheckedTracking = null;
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel1.Location = new System.Drawing.Point(11, 9);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel1.Size = new System.Drawing.Size(80, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Text = "รหัสพนักงาน:";
            this.kryptonLabel1.Values.ExtraText = "";
            this.kryptonLabel1.Values.Image = null;
            this.kryptonLabel1.Values.Text = "รหัสพนักงาน:";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel2.Location = new System.Drawing.Point(28, 59);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel2.Size = new System.Drawing.Size(62, 20);
            this.kryptonLabel2.TabIndex = 8;
            this.kryptonLabel2.Text = "หมายเหตุ:";
            this.kryptonLabel2.Values.ExtraText = "";
            this.kryptonLabel2.Values.Image = null;
            this.kryptonLabel2.Values.Text = "หมายเหตุ:";
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel8.Location = new System.Drawing.Point(366, 33);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel8.Size = new System.Drawing.Size(47, 20);
            this.kryptonLabel8.TabIndex = 8;
            this.kryptonLabel8.Text = "เหตุผล:";
            this.kryptonLabel8.Values.ExtraText = "";
            this.kryptonLabel8.Values.Image = null;
            this.kryptonLabel8.Values.Text = "เหตุผล:";
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel5.Location = new System.Drawing.Point(20, 33);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel5.Size = new System.Drawing.Size(70, 20);
            this.kryptonLabel5.TabIndex = 4;
            this.kryptonLabel5.Text = "วันที่ลาออก:";
            this.kryptonLabel5.Values.ExtraText = "";
            this.kryptonLabel5.Values.Image = null;
            this.kryptonLabel5.Values.Text = "วันที่ลาออก:";
            // 
            // txtRsRemark
            // 
            this.txtRsRemark.Location = new System.Drawing.Point(97, 59);
            this.txtRsRemark.Multiline = true;
            this.txtRsRemark.Name = "txtRsRemark";
            this.txtRsRemark.Size = new System.Drawing.Size(373, 38);
            this.txtRsRemark.TabIndex = 3;
            this.txtRsRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCode_KeyDown);
            // 
            // txtRsReason
            // 
            this.txtRsReason.Location = new System.Drawing.Point(416, 33);
            this.txtRsReason.Name = "txtRsReason";
            this.txtRsReason.Size = new System.Drawing.Size(165, 20);
            this.txtRsReason.TabIndex = 3;
            this.txtRsReason.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCode_KeyDown);
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel4.Location = new System.Drawing.Point(184, 33);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel4.Size = new System.Drawing.Size(107, 20);
            this.kryptonLabel4.TabIndex = 6;
            this.kryptonLabel4.Text = "ประเภทการลาออก:";
            this.kryptonLabel4.Values.ExtraText = "";
            this.kryptonLabel4.Values.Image = null;
            this.kryptonLabel4.Values.Text = "ประเภทการลาออก:";
            // 
            // empDetail_Control1
            // 
            this.empDetail_Control1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.empDetail_Control1.Information = null;
            this.empDetail_Control1.Location = new System.Drawing.Point(0, 125);
            this.empDetail_Control1.Name = "empDetail_Control1";
            this.empDetail_Control1.Size = new System.Drawing.Size(589, 230);
            this.empDetail_Control1.TabIndex = 1;
            this.empDetail_Control1.TabStop = false;
            // 
            // DiagResignEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 355);
            this.Controls.Add(this.empDetail_Control1);
            this.Controls.Add(this.kryptonHeaderGroup1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DiagResignEmployee";
            this.Text = "Employee Resignation";
            this.Load += new System.EventHandler(this.FrmResignEmployee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            this.kryptonHeaderGroup1.Panel.ResumeLayout(false);
            this.kryptonHeaderGroup1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.ComboBox cmbResignType;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private System.Windows.Forms.TextBox txtRsReason;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private DCI.HRMS.Controls.EmpDetail_Control empDetail_Control1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private System.Windows.Forms.TextBox txtRsRemark;
    }
}