namespace DCI.HRMS.Personal.Controls
{
    partial class Address_Control
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Address_Control));
            this.kryptonGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.kryptonGroup2 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.txtTelephone = new System.Windows.Forms.MaskedTextBox();
            this.txtPostCode = new System.Windows.Forms.TextBox();
            this.txtProvince = new System.Windows.Forms.TextBox();
            this.txtDistrict = new System.Windows.Forms.TextBox();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtSubDistrict = new System.Windows.Forms.TextBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonHeader1 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1.Panel)).BeginInit();
            this.kryptonGroup1.Panel.SuspendLayout();
            this.kryptonGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup2.Panel)).BeginInit();
            this.kryptonGroup2.Panel.SuspendLayout();
            this.kryptonGroup2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonGroup1
            // 
            this.kryptonGroup1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroup1.Location = new System.Drawing.Point(0, 0);
            this.kryptonGroup1.Name = "kryptonGroup1";
            // 
            // kryptonGroup1.Panel
            // 
            this.kryptonGroup1.Panel.Controls.Add(this.kryptonGroup2);
            this.kryptonGroup1.Panel.Controls.Add(this.kryptonHeader1);
            this.kryptonGroup1.Size = new System.Drawing.Size(409, 146);
            this.kryptonGroup1.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonGroup1.StateCommon.Border.Rounding = 2;
            this.kryptonGroup1.TabIndex = 0;
            // 
            // kryptonGroup2
            // 
            this.kryptonGroup2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroup2.Location = new System.Drawing.Point(0, 25);
            this.kryptonGroup2.Name = "kryptonGroup2";
            // 
            // kryptonGroup2.Panel
            // 
            this.kryptonGroup2.Panel.Controls.Add(this.kryptonPanel1);
            this.kryptonGroup2.Size = new System.Drawing.Size(405, 117);
            this.kryptonGroup2.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonGroup2.StateCommon.Border.Rounding = 2;
            this.kryptonGroup2.TabIndex = 1;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.txtTelephone);
            this.kryptonPanel1.Controls.Add(this.txtPostCode);
            this.kryptonPanel1.Controls.Add(this.txtProvince);
            this.kryptonPanel1.Controls.Add(this.txtDistrict);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel6);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel5);
            this.kryptonPanel1.Controls.Add(this.txtSubDistrict);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel4);
            this.kryptonPanel1.Controls.Add(this.txtAddress);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(401, 113);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // txtTelephone
            // 
            this.txtTelephone.Location = new System.Drawing.Point(222, 88);
            this.txtTelephone.Mask = "000-0000000";
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(78, 20);
            this.txtTelephone.TabIndex = 5;
            this.txtTelephone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress_KeyDown);
            // 
            // txtPostCode
            // 
            this.txtPostCode.Location = new System.Drawing.Point(85, 88);
            this.txtPostCode.MaxLength = 5;
            this.txtPostCode.Name = "txtPostCode";
            this.txtPostCode.Size = new System.Drawing.Size(94, 20);
            this.txtPostCode.TabIndex = 4;
            this.txtPostCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress_KeyDown);
            // 
            // txtProvince
            // 
            this.txtProvince.Location = new System.Drawing.Point(71, 63);
            this.txtProvince.MaxLength = 100;
            this.txtProvince.Name = "txtProvince";
            this.txtProvince.Size = new System.Drawing.Size(325, 20);
            this.txtProvince.TabIndex = 3;
            this.txtProvince.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress_KeyDown);
            // 
            // txtDistrict
            // 
            this.txtDistrict.Location = new System.Drawing.Point(71, 38);
            this.txtDistrict.MaxLength = 100;
            this.txtDistrict.Name = "txtDistrict";
            this.txtDistrict.Size = new System.Drawing.Size(325, 20);
            this.txtDistrict.TabIndex = 2;
            this.txtDistrict.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress_KeyDown);
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(3, 89);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(76, 19);
            this.kryptonLabel6.TabIndex = 0;
            this.kryptonLabel6.Text = "รหัสไปรษณีย์:";
            this.kryptonLabel6.Values.ExtraText = "";
            this.kryptonLabel6.Values.Image = null;
            this.kryptonLabel6.Values.Text = "รหัสไปรษณีย์:";
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(185, 89);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(31, 19);
            this.kryptonLabel5.TabIndex = 0;
            this.kryptonLabel5.Text = "โทร:";
            this.kryptonLabel5.Values.ExtraText = "";
            this.kryptonLabel5.Values.Image = null;
            this.kryptonLabel5.Values.Text = "โทร:";
            // 
            // txtSubDistrict
            // 
            this.txtSubDistrict.Location = new System.Drawing.Point(204, 13);
            this.txtSubDistrict.MaxLength = 100;
            this.txtSubDistrict.Name = "txtSubDistrict";
            this.txtSubDistrict.Size = new System.Drawing.Size(192, 20);
            this.txtSubDistrict.TabIndex = 1;
            this.txtSubDistrict.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress_KeyDown);
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(23, 64);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(45, 19);
            this.kryptonLabel4.TabIndex = 0;
            this.kryptonLabel4.Text = "จังหวัด:";
            this.kryptonLabel4.Values.ExtraText = "";
            this.kryptonLabel4.Values.Image = null;
            this.kryptonLabel4.Values.Text = "จังหวัด:";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(71, 13);
            this.txtAddress.MaxLength = 100;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(83, 20);
            this.txtAddress.TabIndex = 0;
            this.txtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress_KeyDown);
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(23, 39);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(42, 19);
            this.kryptonLabel3.TabIndex = 0;
            this.kryptonLabel3.Text = "อำเภอ:";
            this.kryptonLabel3.Values.ExtraText = "";
            this.kryptonLabel3.Values.Image = null;
            this.kryptonLabel3.Values.Text = "อำเภอ:";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(160, 14);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(38, 19);
            this.kryptonLabel2.TabIndex = 0;
            this.kryptonLabel2.Text = "ตำบล:";
            this.kryptonLabel2.Values.ExtraText = "";
            this.kryptonLabel2.Values.Image = null;
            this.kryptonLabel2.Values.Text = "ตำบล:";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(32, 13);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(33, 19);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Text = "ทีอยู่:";
            this.kryptonLabel1.Values.ExtraText = "";
            this.kryptonLabel1.Values.Image = null;
            this.kryptonLabel1.Values.Text = "ทีอยู่:";
            // 
            // kryptonHeader1
            // 
            this.kryptonHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeader1.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.Form;
            this.kryptonHeader1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeader1.Name = "kryptonHeader1";
            this.kryptonHeader1.Size = new System.Drawing.Size(405, 25);
            this.kryptonHeader1.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonHeader1.StateCommon.Border.Rounding = 2;
            this.kryptonHeader1.TabIndex = 0;
            this.kryptonHeader1.Text = "kryptonHeader1";
            this.kryptonHeader1.Values.Description = "Description";
            this.kryptonHeader1.Values.Heading = "kryptonHeader1";
            this.kryptonHeader1.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonHeader1.Values.Image")));
            // 
            // Address_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonGroup1);
            this.Name = "Address_Control";
            this.Size = new System.Drawing.Size(409, 146);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1.Panel)).EndInit();
            this.kryptonGroup1.Panel.ResumeLayout(false);
            this.kryptonGroup1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1)).EndInit();
            this.kryptonGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup2.Panel)).EndInit();
            this.kryptonGroup2.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup2)).EndInit();
            this.kryptonGroup2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup2;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader kryptonHeader1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private System.Windows.Forms.TextBox txtProvince;
        private System.Windows.Forms.TextBox txtDistrict;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private System.Windows.Forms.TextBox txtSubDistrict;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private System.Windows.Forms.TextBox txtAddress;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private System.Windows.Forms.TextBox txtPostCode;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private System.Windows.Forms.MaskedTextBox txtTelephone;
    }
}
