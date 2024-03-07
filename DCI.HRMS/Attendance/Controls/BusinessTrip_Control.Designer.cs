namespace DCI.HRMS.Attendance.Controls
{
    partial class BusinessTrip_Control
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BusinessTrip_Control));
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.kryptonGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.kryptonGroup2 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.dpkRqDateTo = new System.Windows.Forms.DateTimePicker();
            this.dpkRqDate = new System.Windows.Forms.DateTimePicker();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtNote = new System.Windows.Forms.TextBox();
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
            // kryptonLabel1
            // 
            this.kryptonLabel1.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel1.Location = new System.Drawing.Point(-5, 65);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel1.Size = new System.Drawing.Size(42, 20);
            this.kryptonLabel1.TabIndex = 5;
            this.kryptonLabel1.Text = "Code:";
            this.kryptonLabel1.Values.ExtraText = "";
            this.kryptonLabel1.Values.Image = null;
            this.kryptonLabel1.Values.Text = "Code:";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(39, 64);
            this.txtCode.Name = "txtCode";
            this.txtCode.ReadOnly = true;
            this.txtCode.Size = new System.Drawing.Size(100, 20);
            this.txtCode.TabIndex = 5;
            this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // kryptonGroup1
            // 
            this.kryptonGroup1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroup1.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonGroup1.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonGroup1.Location = new System.Drawing.Point(0, 0);
            this.kryptonGroup1.Name = "kryptonGroup1";
            this.kryptonGroup1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonGroup1.Panel
            // 
            this.kryptonGroup1.Panel.Controls.Add(this.kryptonGroup2);
            this.kryptonGroup1.Panel.Controls.Add(this.kryptonHeader1);
            this.kryptonGroup1.Size = new System.Drawing.Size(613, 125);
            this.kryptonGroup1.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonGroup1.StateCommon.Border.Rounding = 2;
            this.kryptonGroup1.TabIndex = 2;
            // 
            // kryptonGroup2
            // 
            this.kryptonGroup2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroup2.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonGroup2.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonGroup2.Location = new System.Drawing.Point(0, 25);
            this.kryptonGroup2.Name = "kryptonGroup2";
            this.kryptonGroup2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonGroup2.Panel
            // 
            this.kryptonGroup2.Panel.Controls.Add(this.txtCode);
            this.kryptonGroup2.Panel.Controls.Add(this.txtNote);
            this.kryptonGroup2.Panel.Controls.Add(this.txtTo);
            this.kryptonGroup2.Panel.Controls.Add(this.txtFrom);
            this.kryptonGroup2.Panel.Controls.Add(this.dpkRqDateTo);
            this.kryptonGroup2.Panel.Controls.Add(this.dpkRqDate);
            this.kryptonGroup2.Panel.Controls.Add(this.kryptonLabel6);
            this.kryptonGroup2.Panel.Controls.Add(this.kryptonLabel2);
            this.kryptonGroup2.Panel.Controls.Add(this.kryptonLabel4);
            this.kryptonGroup2.Panel.Controls.Add(this.kryptonLabel3);
            this.kryptonGroup2.Panel.Controls.Add(this.kryptonLabel5);
            this.kryptonGroup2.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonGroup2.Size = new System.Drawing.Size(609, 96);
            this.kryptonGroup2.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonGroup2.StateCommon.Border.Rounding = 2;
            this.kryptonGroup2.TabIndex = 0;
            // 
            // dpkRqDateTo
            // 
            this.dpkRqDateTo.CustomFormat = "dd/MM/yyyy";
            this.dpkRqDateTo.Enabled = false;
            this.dpkRqDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpkRqDateTo.Location = new System.Drawing.Point(154, 14);
            this.dpkRqDateTo.Name = "dpkRqDateTo";
            this.dpkRqDateTo.Size = new System.Drawing.Size(100, 20);
            this.dpkRqDateTo.TabIndex = 1;
            this.dpkRqDateTo.ValueChanged += new System.EventHandler(this.dpkRqDate_ValueChanged);
            this.dpkRqDateTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // dpkRqDate
            // 
            this.dpkRqDate.CustomFormat = "dd/MM/yyyy";
            this.dpkRqDate.Enabled = false;
            this.dpkRqDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpkRqDate.Location = new System.Drawing.Point(39, 14);
            this.dpkRqDate.Name = "dpkRqDate";
            this.dpkRqDate.Size = new System.Drawing.Size(100, 20);
            this.dpkRqDate.TabIndex = 0;
            this.dpkRqDate.ValueChanged += new System.EventHandler(this.dpkRqDate_ValueChanged);
            this.dpkRqDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel6.Location = new System.Drawing.Point(139, 15);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel6.Size = new System.Drawing.Size(15, 20);
            this.kryptonLabel6.TabIndex = 9;
            this.kryptonLabel6.Text = "-";
            this.kryptonLabel6.Values.ExtraText = "";
            this.kryptonLabel6.Values.Image = null;
            this.kryptonLabel6.Values.Text = "-";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel2.Location = new System.Drawing.Point(-4, 40);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel2.Size = new System.Drawing.Size(40, 20);
            this.kryptonLabel2.TabIndex = 9;
            this.kryptonLabel2.Text = "Note:";
            this.kryptonLabel2.Values.ExtraText = "";
            this.kryptonLabel2.Values.Image = null;
            this.kryptonLabel2.Values.Text = "Note:";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel4.Location = new System.Drawing.Point(365, 14);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel4.Size = new System.Drawing.Size(27, 20);
            this.kryptonLabel4.TabIndex = 8;
            this.kryptonLabel4.Text = "To:";
            this.kryptonLabel4.Values.ExtraText = "";
            this.kryptonLabel4.Values.Image = null;
            this.kryptonLabel4.Values.Text = "To:";
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(390, 13);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(55, 20);
            this.txtTo.TabIndex = 3;
            this.txtTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.txtTo.Leave += new System.EventHandler(this.textBox2_Leave);
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel3.Location = new System.Drawing.Point(273, 14);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel3.Size = new System.Drawing.Size(41, 20);
            this.kryptonLabel3.TabIndex = 7;
            this.kryptonLabel3.Text = "From:";
            this.kryptonLabel3.Values.ExtraText = "";
            this.kryptonLabel3.Values.Image = null;
            this.kryptonLabel3.Values.Text = "From:";
            // 
            // txtFrom
            // 
            this.txtFrom.Location = new System.Drawing.Point(311, 13);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(54, 20);
            this.txtFrom.TabIndex = 2;
            this.txtFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.txtFrom.Leave += new System.EventHandler(this.textBox2_Leave);
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel5.Location = new System.Drawing.Point(-2, 15);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel5.Size = new System.Drawing.Size(39, 20);
            this.kryptonLabel5.TabIndex = 6;
            this.kryptonLabel5.Text = "Date:";
            this.kryptonLabel5.Values.ExtraText = "";
            this.kryptonLabel5.Values.Image = null;
            this.kryptonLabel5.Values.Text = "Date:";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(39, 40);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(195, 20);
            this.txtNote.TabIndex = 4;
            this.txtNote.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // kryptonHeader1
            // 
            this.kryptonHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeader1.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.Form;
            this.kryptonHeader1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeader1.Name = "kryptonHeader1";
            this.kryptonHeader1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonHeader1.Size = new System.Drawing.Size(609, 25);
            this.kryptonHeader1.TabIndex = 1;
            this.kryptonHeader1.Text = "BusinessTrip Infomation";
            this.kryptonHeader1.Values.Description = "Description";
            this.kryptonHeader1.Values.Heading = "BusinessTrip Infomation";
            this.kryptonHeader1.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonHeader1.Values.Image")));
            // 
            // BusinessTrip_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonGroup1);
            this.Name = "BusinessTrip_Control";
            this.Size = new System.Drawing.Size(613, 125);
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
        private System.Windows.Forms.DateTimePicker dpkRqDateTo;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private System.Windows.Forms.TextBox txtNote;
    }
}
