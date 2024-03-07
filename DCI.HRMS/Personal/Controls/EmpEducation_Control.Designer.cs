namespace DCI.HRMS.Personal.Controls
{
    partial class EmpEducation_Control
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmpEducation_Control));
            this.kryptonGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.kryptonGroup2 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.cmbDegreeType = new System.Windows.Forms.ComboBox();
            this.txtEnInst = new System.Windows.Forms.TextBox();
            this.txtDegree = new System.Windows.Forms.TextBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.txtEnMajor = new System.Windows.Forms.TextBox();
            this.txtThInst = new System.Windows.Forms.TextBox();
            this.txtThMajor = new System.Windows.Forms.TextBox();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonHeader1 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1.Panel)).BeginInit();
            this.kryptonGroup1.Panel.SuspendLayout();
            this.kryptonGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup2.Panel)).BeginInit();
            this.kryptonGroup2.Panel.SuspendLayout();
            this.kryptonGroup2.SuspendLayout();
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
            this.kryptonGroup1.Size = new System.Drawing.Size(594, 128);
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
            this.kryptonGroup2.Panel.Controls.Add(this.cmbDegreeType);
            this.kryptonGroup2.Panel.Controls.Add(this.txtEnInst);
            this.kryptonGroup2.Panel.Controls.Add(this.txtDegree);
            this.kryptonGroup2.Panel.Controls.Add(this.txtYear);
            this.kryptonGroup2.Panel.Controls.Add(this.txtEnMajor);
            this.kryptonGroup2.Panel.Controls.Add(this.txtThInst);
            this.kryptonGroup2.Panel.Controls.Add(this.txtThMajor);
            this.kryptonGroup2.Panel.Controls.Add(this.kryptonLabel6);
            this.kryptonGroup2.Panel.Controls.Add(this.kryptonLabel5);
            this.kryptonGroup2.Panel.Controls.Add(this.kryptonLabel7);
            this.kryptonGroup2.Panel.Controls.Add(this.kryptonLabel4);
            this.kryptonGroup2.Panel.Controls.Add(this.kryptonLabel3);
            this.kryptonGroup2.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonGroup2.Panel.Controls.Add(this.kryptonLabel2);
            this.kryptonGroup2.Size = new System.Drawing.Size(590, 99);
            this.kryptonGroup2.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonGroup2.StateCommon.Border.Rounding = 2;
            this.kryptonGroup2.TabIndex = 1;
            // 
            // cmbDegreeType
            // 
            this.cmbDegreeType.FormattingEnabled = true;
            this.cmbDegreeType.Location = new System.Drawing.Point(119, 9);
            this.cmbDegreeType.Name = "cmbDegreeType";
            this.cmbDegreeType.Size = new System.Drawing.Size(144, 21);
            this.cmbDegreeType.TabIndex = 0;
            this.cmbDegreeType.SelectedValueChanged += new System.EventHandler(this.cmbDegreeType_SelectedValueChanged);
            // 
            // txtEnInst
            // 
            this.txtEnInst.Location = new System.Drawing.Point(390, 60);
            this.txtEnInst.MaxLength = 100;
            this.txtEnInst.Name = "txtEnInst";
            this.txtEnInst.Size = new System.Drawing.Size(191, 20);
            this.txtEnInst.TabIndex = 5;
            // 
            // txtDegree
            // 
            this.txtDegree.Location = new System.Drawing.Point(340, 10);
            this.txtDegree.Name = "txtDegree";
            this.txtDegree.Size = new System.Drawing.Size(148, 20);
            this.txtDegree.TabIndex = 1;
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(531, 10);
            this.txtYear.MaxLength = 4;
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(51, 20);
            this.txtYear.TabIndex = 1;
            // 
            // txtEnMajor
            // 
            this.txtEnMajor.Location = new System.Drawing.Point(390, 36);
            this.txtEnMajor.MaxLength = 100;
            this.txtEnMajor.Name = "txtEnMajor";
            this.txtEnMajor.Size = new System.Drawing.Size(191, 20);
            this.txtEnMajor.TabIndex = 3;
            // 
            // txtThInst
            // 
            this.txtThInst.Location = new System.Drawing.Point(119, 62);
            this.txtThInst.MaxLength = 100;
            this.txtThInst.Name = "txtThInst";
            this.txtThInst.Size = new System.Drawing.Size(185, 20);
            this.txtThInst.TabIndex = 4;
            // 
            // txtThMajor
            // 
            this.txtThMajor.Location = new System.Drawing.Point(119, 36);
            this.txtThMajor.MaxLength = 100;
            this.txtThMajor.Name = "txtThMajor";
            this.txtThMajor.Size = new System.Drawing.Size(185, 20);
            this.txtThMajor.TabIndex = 2;
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(310, 36);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(74, 19);
            this.kryptonLabel6.TabIndex = 0;
            this.kryptonLabel6.Text = "สาขาวิชา(En):";
            this.kryptonLabel6.Values.ExtraText = "";
            this.kryptonLabel6.Values.Image = null;
            this.kryptonLabel6.Values.Text = "สาขาวิชา(En):";
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(32, 36);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(81, 19);
            this.kryptonLabel5.TabIndex = 0;
            this.kryptonLabel5.Text = "สาขาวิชา(ไทย):";
            this.kryptonLabel5.Values.ExtraText = "";
            this.kryptonLabel5.Values.Image = null;
            this.kryptonLabel5.Values.Text = "สาขาวิชา(ไทย):";
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(319, 63);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(65, 19);
            this.kryptonLabel7.TabIndex = 0;
            this.kryptonLabel7.Text = "สถาบัน(En):";
            this.kryptonLabel7.Values.ExtraText = "";
            this.kryptonLabel7.Values.Image = null;
            this.kryptonLabel7.Values.Text = "สถาบัน(En):";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(41, 61);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(72, 19);
            this.kryptonLabel4.TabIndex = 0;
            this.kryptonLabel4.Text = "สถาบัน(ไทย):";
            this.kryptonLabel4.Values.ExtraText = "";
            this.kryptonLabel4.Values.Image = null;
            this.kryptonLabel4.Values.Text = "สถาบัน(ไทย):";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(494, 11);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(40, 19);
            this.kryptonLabel3.TabIndex = 0;
            this.kryptonLabel3.Text = "ปีที่จบ:";
            this.kryptonLabel3.Values.ExtraText = "";
            this.kryptonLabel3.Values.Image = null;
            this.kryptonLabel3.Values.Text = "ปีที่จบ:";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(266, 11);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(73, 19);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Text = "วุฒิการศึกษา:";
            this.kryptonLabel1.Values.ExtraText = "";
            this.kryptonLabel1.Values.Image = null;
            this.kryptonLabel1.Values.Text = "วุฒิการศึกษา:";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(3, 11);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(110, 19);
            this.kryptonLabel2.TabIndex = 0;
            this.kryptonLabel2.Text = "ประเภทวุฒิการศึกษา:";
            this.kryptonLabel2.Values.ExtraText = "";
            this.kryptonLabel2.Values.Image = null;
            this.kryptonLabel2.Values.Text = "ประเภทวุฒิการศึกษา:";
            // 
            // kryptonHeader1
            // 
            this.kryptonHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeader1.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.Form;
            this.kryptonHeader1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeader1.Name = "kryptonHeader1";
            this.kryptonHeader1.Size = new System.Drawing.Size(590, 25);
            this.kryptonHeader1.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonHeader1.StateCommon.Border.Rounding = 2;
            this.kryptonHeader1.TabIndex = 0;
            this.kryptonHeader1.Text = "Education Information";
            this.kryptonHeader1.Values.Description = "ประวัติการศึกษา";
            this.kryptonHeader1.Values.Heading = "Education Information";
            this.kryptonHeader1.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonHeader1.Values.Image")));
            // 
            // EmpEducation_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonGroup1);
            this.Name = "EmpEducation_Control";
            this.Size = new System.Drawing.Size(594, 128);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1.Panel)).EndInit();
            this.kryptonGroup1.Panel.ResumeLayout(false);
            this.kryptonGroup1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1)).EndInit();
            this.kryptonGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup2.Panel)).EndInit();
            this.kryptonGroup2.Panel.ResumeLayout(false);
            this.kryptonGroup2.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup2)).EndInit();
            this.kryptonGroup2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup2;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader kryptonHeader1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private System.Windows.Forms.ComboBox cmbDegreeType;
        private System.Windows.Forms.TextBox txtEnInst;
        private System.Windows.Forms.TextBox txtDegree;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.TextBox txtEnMajor;
        private System.Windows.Forms.TextBox txtThInst;
        private System.Windows.Forms.TextBox txtThMajor;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
    }
}
