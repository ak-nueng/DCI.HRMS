namespace DCI.HRMS.Attendance.Controls
{
    partial class TimeCardManual_Control
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeCardManual_Control));
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.kryptonGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.kryptonGroup2 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.dpkRqDate = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonHeader1 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
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
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(3, 39);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(39, 19);
            this.kryptonLabel1.TabIndex = 5;
            this.kryptonLabel1.Text = "Code:";
            this.kryptonLabel1.Values.ExtraText = "";
            this.kryptonLabel1.Values.Image = null;
            this.kryptonLabel1.Values.Text = "Code:";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(47, 38);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(100, 20);
            this.txtCode.TabIndex = 4;
            this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
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
            this.kryptonGroup1.Size = new System.Drawing.Size(613, 107);
            this.kryptonGroup1.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonGroup1.StateCommon.Border.Rounding = 2;
            this.kryptonGroup1.TabIndex = 2;
            // 
            // kryptonGroup2
            // 
            this.kryptonGroup2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroup2.Location = new System.Drawing.Point(0, 23);
            this.kryptonGroup2.Name = "kryptonGroup2";
            // 
            // kryptonGroup2.Panel
            // 
            this.kryptonGroup2.Panel.Controls.Add(this.comboBox1);
            this.kryptonGroup2.Panel.Controls.Add(this.kryptonButton1);
            this.kryptonGroup2.Panel.Controls.Add(this.dpkRqDate);
            this.kryptonGroup2.Panel.Controls.Add(this.kryptonLabel2);
            this.kryptonGroup2.Panel.Controls.Add(this.kryptonLabel4);
            this.kryptonGroup2.Panel.Controls.Add(this.txtTo);
            this.kryptonGroup2.Panel.Controls.Add(this.kryptonLabel3);
            this.kryptonGroup2.Panel.Controls.Add(this.txtFrom);
            this.kryptonGroup2.Panel.Controls.Add(this.kryptonLabel5);
            this.kryptonGroup2.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonGroup2.Panel.Controls.Add(this.txtCode);
            this.kryptonGroup2.Size = new System.Drawing.Size(609, 80);
            this.kryptonGroup2.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonGroup2.StateCommon.Border.Rounding = 2;
            this.kryptonGroup2.TabIndex = 0;
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.AutoSize = true;
            this.kryptonButton1.Location = new System.Drawing.Point(502, 39);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(90, 30);
            this.kryptonButton1.TabIndex = 5;
            this.kryptonButton1.Text = "Save";
            this.kryptonButton1.Values.ExtraText = "";
            this.kryptonButton1.Values.Image = global::DCI.HRMS.Properties.Resources.save1;
            this.kryptonButton1.Values.ImageStates.ImageCheckedNormal = null;
            this.kryptonButton1.Values.ImageStates.ImageCheckedPressed = null;
            this.kryptonButton1.Values.ImageStates.ImageCheckedTracking = null;
            this.kryptonButton1.Values.Text = "Save";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click_1);
            // 
            // dpkRqDate
            // 
            this.dpkRqDate.CustomFormat = "dd/MM/yyyy";
            this.dpkRqDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpkRqDate.Location = new System.Drawing.Point(47, 13);
            this.dpkRqDate.Name = "dpkRqDate";
            this.dpkRqDate.Size = new System.Drawing.Size(100, 20);
            this.dpkRqDate.TabIndex = 0;
            this.dpkRqDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(359, 14);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(84, 19);
            this.kryptonLabel2.TabIndex = 9;
            this.kryptonLabel2.Text = "TimeCardType:";
            this.kryptonLabel2.Values.ExtraText = "";
            this.kryptonLabel2.Values.Image = null;
            this.kryptonLabel2.Values.Text = "TimeCardType:";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(263, 14);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(25, 19);
            this.kryptonLabel4.TabIndex = 8;
            this.kryptonLabel4.Text = "To:";
            this.kryptonLabel4.Values.ExtraText = "";
            this.kryptonLabel4.Values.Image = null;
            this.kryptonLabel4.Values.Text = "To:";
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(296, 13);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(55, 20);
            this.txtTo.TabIndex = 2;
            this.txtTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.txtTo.Leave += new System.EventHandler(this.textBox2_Leave);
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(155, 14);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(38, 19);
            this.kryptonLabel3.TabIndex = 7;
            this.kryptonLabel3.Text = "From:";
            this.kryptonLabel3.Values.ExtraText = "";
            this.kryptonLabel3.Values.Image = null;
            this.kryptonLabel3.Values.Text = "From:";
            // 
            // txtFrom
            // 
            this.txtFrom.Location = new System.Drawing.Point(201, 13);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(54, 20);
            this.txtFrom.TabIndex = 1;
            this.txtFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.txtFrom.Leave += new System.EventHandler(this.textBox2_Leave);
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(3, 14);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(36, 19);
            this.kryptonLabel5.TabIndex = 6;
            this.kryptonLabel5.Text = "Date:";
            this.kryptonLabel5.Values.ExtraText = "";
            this.kryptonLabel5.Values.Image = null;
            this.kryptonLabel5.Values.Text = "Date:";
            // 
            // kryptonHeader1
            // 
            this.kryptonHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeader1.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.Form;
            this.kryptonHeader1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeader1.Name = "kryptonHeader1";
            this.kryptonHeader1.Size = new System.Drawing.Size(609, 23);
            this.kryptonHeader1.TabIndex = 1;
            this.kryptonHeader1.Text = "TimeCard Manual";
            this.kryptonHeader1.Values.Description = "Description";
            this.kryptonHeader1.Values.Heading = "TimeCard Manual";
            this.kryptonHeader1.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonHeader1.Values.Image")));
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(449, 14);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(143, 21);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // TimeCardManual_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonGroup1);
            this.Name = "TimeCardManual_Control";
            this.Size = new System.Drawing.Size(613, 107);
            this.Load += new System.EventHandler(this.TimeCardManual_Control_Load);
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

        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private System.Windows.Forms.TextBox txtCode;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup2;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader kryptonHeader1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.DateTimePicker dpkRqDate;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private System.Windows.Forms.TextBox txtTo;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}
