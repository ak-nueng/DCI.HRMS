namespace DCI.HRMS.Personal
{
    partial class FrmEmployee_Family
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEmployee_Family));
            this.empFamily_Control1 = new DCI.HRMS.Personal.Controls.EmpFamily_Control();
            this.ucl_ActionControl1 = new DCI.HRMS.Controls.Ucl_ActionControl();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtSurn = new System.Windows.Forms.TextBox();
            this.txtPren = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtTax = new System.Windows.Forms.TextBox();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.txtIdNo = new System.Windows.Forms.MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.Panel.SuspendLayout();
            this.kryptonHeaderGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // empFamily_Control1
            // 
            this.empFamily_Control1.BackColor = System.Drawing.SystemColors.Window;
            this.empFamily_Control1.DisableSelect = false;
            this.empFamily_Control1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.empFamily_Control1.Information = null;
            this.empFamily_Control1.Location = new System.Drawing.Point(0, 220);
            this.empFamily_Control1.Name = "empFamily_Control1";
            this.empFamily_Control1.ReadyOnly = false;
            this.empFamily_Control1.Size = new System.Drawing.Size(932, 334);
            this.empFamily_Control1.TabIndex = 1;
            this.empFamily_Control1.Family_Slect += new DCI.HRMS.Personal.Controls.EmpFamily_Control.Famili_SelectHandler(this.empFamily_Control1_Family_Slect);
            // 
            // ucl_ActionControl1
            // 
            this.ucl_ActionControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucl_ActionControl1.Location = new System.Drawing.Point(0, 0);
            this.ucl_ActionControl1.Name = "ucl_ActionControl1";
            this.ucl_ActionControl1.Size = new System.Drawing.Size(932, 45);
            this.ucl_ActionControl1.TabIndex = 2;
            this.ucl_ActionControl1.TabStop = false;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel1.Location = new System.Drawing.Point(3, 14);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel1.Size = new System.Drawing.Size(80, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Text = "รหัสพนักงาน:";
            this.kryptonLabel1.Values.ExtraText = "";
            this.kryptonLabel1.Values.Image = null;
            this.kryptonLabel1.Values.Text = "รหัสพนักงาน:";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(77, 13);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(67, 20);
            this.txtCode.TabIndex = 0;
            this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCode_KeyDown);
            // 
            // txtSurn
            // 
            this.txtSurn.Location = new System.Drawing.Point(249, 70);
            this.txtSurn.Name = "txtSurn";
            this.txtSurn.Size = new System.Drawing.Size(121, 20);
            this.txtSurn.TabIndex = 5;
            this.txtSurn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdNo_KeyDown);
            // 
            // txtPren
            // 
            this.txtPren.Location = new System.Drawing.Point(77, 70);
            this.txtPren.Name = "txtPren";
            this.txtPren.Size = new System.Drawing.Size(31, 20);
            this.txtPren.TabIndex = 3;
            this.txtPren.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdNo_KeyDown);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(114, 70);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(122, 20);
            this.txtName.TabIndex = 4;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdNo_KeyDown);
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(249, 37);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdNo_KeyDown);
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel2.Location = new System.Drawing.Point(27, 72);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel2.Size = new System.Drawing.Size(54, 20);
            this.kryptonLabel2.TabIndex = 5;
            this.kryptonLabel2.Text = "ชื่อ-สกุล:";
            this.kryptonLabel2.Values.ExtraText = "";
            this.kryptonLabel2.Values.Image = null;
            this.kryptonLabel2.Values.Text = "ชื่อ-สกุล:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(77, 98);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(92, 20);
            this.dateTimePicker1.TabIndex = 6;
            this.dateTimePicker1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdNo_KeyDown);
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel5.Location = new System.Drawing.Point(33, 99);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel5.Size = new System.Drawing.Size(47, 20);
            this.kryptonLabel5.TabIndex = 4;
            this.kryptonLabel5.Text = "วันเกิด:";
            this.kryptonLabel5.Values.ExtraText = "";
            this.kryptonLabel5.Values.Image = null;
            this.kryptonLabel5.Values.Text = "วันเกิด:";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel3.Location = new System.Drawing.Point(340, 99);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel3.Size = new System.Drawing.Size(32, 20);
            this.kryptonLabel3.TabIndex = 9;
            this.kryptonLabel3.Text = "บาท";
            this.kryptonLabel3.Values.ExtraText = "";
            this.kryptonLabel3.Values.Image = null;
            this.kryptonLabel3.Values.Text = "บาท";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel4.Location = new System.Drawing.Point(175, 39);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel4.Size = new System.Drawing.Size(80, 20);
            this.kryptonLabel4.TabIndex = 6;
            this.kryptonLabel4.Text = "ความสัมพันธ์:";
            this.kryptonLabel4.Values.ExtraText = "";
            this.kryptonLabel4.Values.Image = null;
            this.kryptonLabel4.Values.Text = "ความสัมพันธ์:";
            // 
            // txtTax
            // 
            this.txtTax.Location = new System.Drawing.Point(249, 98);
            this.txtTax.Name = "txtTax";
            this.txtTax.Size = new System.Drawing.Size(91, 20);
            this.txtTax.TabIndex = 7;
            this.txtTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTax_KeyDown);
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel6.Location = new System.Drawing.Point(-2, 39);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel6.Size = new System.Drawing.Size(85, 20);
            this.kryptonLabel6.TabIndex = 10;
            this.kryptonLabel6.Text = "บัตรประชาชน:";
            this.kryptonLabel6.Values.ExtraText = "";
            this.kryptonLabel6.Values.Image = null;
            this.kryptonLabel6.Values.Text = "บัตรประชาชน:";
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel8.Location = new System.Drawing.Point(175, 99);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel8.Size = new System.Drawing.Size(80, 20);
            this.kryptonLabel8.TabIndex = 8;
            this.kryptonLabel8.Text = "ลดหย่อนภาษี:";
            this.kryptonLabel8.Values.ExtraText = "";
            this.kryptonLabel8.Values.Image = null;
            this.kryptonLabel8.Values.Text = "ลดหย่อนภาษี:";
            // 
            // kryptonHeaderGroup1
            // 
            this.kryptonHeaderGroup1.AutoSize = true;
            this.kryptonHeaderGroup1.CollapseTarget = ComponentFactory.Krypton.Toolkit.HeaderGroupCollapsedTarget.CollapsedToPrimary;
            this.kryptonHeaderGroup1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeaderGroup1.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonHeaderGroup1.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonHeaderGroup1.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Primary;
            this.kryptonHeaderGroup1.HeaderStyleSecondary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.kryptonHeaderGroup1.Location = new System.Drawing.Point(0, 45);
            this.kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            this.kryptonHeaderGroup1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonHeaderGroup1.Panel
            // 
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtIdNo);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtSurn);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtPren);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtName);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtCode);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.comboBox1);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel2);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel8);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.dateTimePicker1);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel6);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel5);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.txtTax);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel3);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel4);
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(932, 175);
            this.kryptonHeaderGroup1.TabIndex = 0;
            this.kryptonHeaderGroup1.Text = "เพิ่มข้อมูล";
            this.kryptonHeaderGroup1.ValuesPrimary.Description = "";
            this.kryptonHeaderGroup1.ValuesPrimary.Heading = "เพิ่มข้อมูล";
            this.kryptonHeaderGroup1.ValuesPrimary.Image = global::DCI.HRMS.Properties.Resources.add;
            this.kryptonHeaderGroup1.ValuesSecondary.Description = "";
            this.kryptonHeaderGroup1.ValuesSecondary.Heading = "Description";
            this.kryptonHeaderGroup1.ValuesSecondary.Image = null;
            this.kryptonHeaderGroup1.Click += new System.EventHandler(this.kryptonHeaderGroup1_Click);
            // 
            // txtIdNo
            // 
            this.txtIdNo.Location = new System.Drawing.Point(77, 39);
            this.txtIdNo.Mask = "0-0000-00000-00-0";
            this.txtIdNo.Name = "txtIdNo";
            this.txtIdNo.Size = new System.Drawing.Size(101, 20);
            this.txtIdNo.TabIndex = 1;
            this.txtIdNo.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.txtIdNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdNo_KeyDown);
            this.txtIdNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIdNo_KeyPress);
            // 
            // FrmEmployee_Family
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 554);
            this.Controls.Add(this.empFamily_Control1);
            this.Controls.Add(this.kryptonHeaderGroup1);
            this.Controls.Add(this.ucl_ActionControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmEmployee_Family";
            this.Text = "FrmEmployee_Family";
            this.Load += new System.EventHandler(this.FrmEmployee_Family_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            this.kryptonHeaderGroup1.Panel.ResumeLayout(false);
            this.kryptonHeaderGroup1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DCI.HRMS.Personal.Controls.EmpFamily_Control empFamily_Control1;
        private DCI.HRMS.Controls.Ucl_ActionControl ucl_ActionControl1;
        private System.Windows.Forms.TextBox txtSurn;
        private System.Windows.Forms.TextBox txtPren;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox comboBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private System.Windows.Forms.TextBox txtTax;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private System.Windows.Forms.TextBox txtCode;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup1;
        private System.Windows.Forms.MaskedTextBox txtIdNo;
    }
}