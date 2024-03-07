namespace DCI.HRMS.Attendance.Controls
{
    partial class TimeCard_Control
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeCard_Control));
            this.kryptonGroup7 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.kryptonGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.dtpCardDate = new System.Windows.Forms.DateTimePicker();
            this.txtTaffId = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonHeader7 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.cmbDuty = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup7.Panel)).BeginInit();
            this.kryptonGroup7.Panel.SuspendLayout();
            this.kryptonGroup7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1.Panel)).BeginInit();
            this.kryptonGroup1.Panel.SuspendLayout();
            this.kryptonGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonGroup7
            // 
            this.kryptonGroup7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroup7.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonGroup7.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonGroup7.Location = new System.Drawing.Point(0, 0);
            this.kryptonGroup7.Name = "kryptonGroup7";
            this.kryptonGroup7.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonGroup7.Panel
            // 
            this.kryptonGroup7.Panel.Controls.Add(this.kryptonGroup1);
            this.kryptonGroup7.Panel.Controls.Add(this.kryptonHeader7);
            this.kryptonGroup7.Size = new System.Drawing.Size(341, 106);
            this.kryptonGroup7.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonGroup7.StateCommon.Border.Rounding = 2;
            this.kryptonGroup7.TabIndex = 20;
            // 
            // kryptonGroup1
            // 
            this.kryptonGroup1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroup1.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonGroup1.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonGroup1.Location = new System.Drawing.Point(0, 23);
            this.kryptonGroup1.Name = "kryptonGroup1";
            this.kryptonGroup1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonGroup1.Panel
            // 
            this.kryptonGroup1.Panel.Controls.Add(this.cmbDuty);
            this.kryptonGroup1.Panel.Controls.Add(this.kryptonLabel8);
            this.kryptonGroup1.Panel.Controls.Add(this.dtpCardDate);
            this.kryptonGroup1.Panel.Controls.Add(this.txtTime);
            this.kryptonGroup1.Panel.Controls.Add(this.txtTaffId);
            this.kryptonGroup1.Panel.Controls.Add(this.txtCode);
            this.kryptonGroup1.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonGroup1.Panel.Controls.Add(this.kryptonLabel2);
            this.kryptonGroup1.Panel.Controls.Add(this.kryptonLabel6);
            this.kryptonGroup1.Panel.Controls.Add(this.kryptonLabel7);
            this.kryptonGroup1.Size = new System.Drawing.Size(337, 79);
            this.kryptonGroup1.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonGroup1.StateCommon.Border.Rounding = 2;
            this.kryptonGroup1.TabIndex = 4;
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel8.Location = new System.Drawing.Point(3, 13);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel8.Size = new System.Drawing.Size(39, 19);
            this.kryptonLabel8.TabIndex = 2;
            this.kryptonLabel8.Text = "Code:";
            this.kryptonLabel8.Values.ExtraText = "";
            this.kryptonLabel8.Values.Image = null;
            this.kryptonLabel8.Values.Text = "Code:";
            // 
            // dtpCardDate
            // 
            this.dtpCardDate.Enabled = false;
            this.dtpCardDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCardDate.Location = new System.Drawing.Point(142, 13);
            this.dtpCardDate.Name = "dtpCardDate";
            this.dtpCardDate.Size = new System.Drawing.Size(95, 20);
            this.dtpCardDate.TabIndex = 3;
            // 
            // txtTaffId
            // 
            this.txtTaffId.Location = new System.Drawing.Point(115, 40);
            this.txtTaffId.Name = "txtTaffId";
            this.txtTaffId.ReadOnly = true;
            this.txtTaffId.Size = new System.Drawing.Size(41, 20);
            this.txtTaffId.TabIndex = 1;
            this.txtTaffId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTime_KeyDown);
            this.txtTaffId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTaffId_KeyPress);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(48, 13);
            this.txtCode.Name = "txtCode";
            this.txtCode.ReadOnly = true;
            this.txtCode.Size = new System.Drawing.Size(61, 20);
            this.txtCode.TabIndex = 1;
            this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTime_KeyDown);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel1.Location = new System.Drawing.Point(162, 41);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel1.Size = new System.Drawing.Size(36, 19);
            this.kryptonLabel1.TabIndex = 2;
            this.kryptonLabel1.Text = "Duty:";
            this.kryptonLabel1.Values.ExtraText = "";
            this.kryptonLabel1.Values.Image = null;
            this.kryptonLabel1.Values.Text = "Duty:";
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel6.Location = new System.Drawing.Point(6, 41);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel6.Size = new System.Drawing.Size(103, 19);
            this.kryptonLabel6.TabIndex = 2;
            this.kryptonLabel6.Text = "TimeCardMachine:";
            this.kryptonLabel6.Values.ExtraText = "";
            this.kryptonLabel6.Values.Image = null;
            this.kryptonLabel6.Values.Text = "TimeCardMachine:";
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel7.Location = new System.Drawing.Point(109, 13);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel7.Size = new System.Drawing.Size(36, 19);
            this.kryptonLabel7.TabIndex = 2;
            this.kryptonLabel7.Text = "Date:";
            this.kryptonLabel7.Values.ExtraText = "";
            this.kryptonLabel7.Values.Image = null;
            this.kryptonLabel7.Values.Text = "Date:";
            // 
            // kryptonHeader7
            // 
            this.kryptonHeader7.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeader7.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.Form;
            this.kryptonHeader7.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeader7.Name = "kryptonHeader7";
            this.kryptonHeader7.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonHeader7.Size = new System.Drawing.Size(337, 23);
            this.kryptonHeader7.TabIndex = 0;
            this.kryptonHeader7.Text = "TimeCard Information";
            this.kryptonHeader7.Values.Description = "";
            this.kryptonHeader7.Values.Heading = "TimeCard Information";
            this.kryptonHeader7.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonHeader7.Values.Image")));
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel2.Location = new System.Drawing.Point(243, 15);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel2.Size = new System.Drawing.Size(37, 19);
            this.kryptonLabel2.TabIndex = 2;
            this.kryptonLabel2.Text = "Time:";
            this.kryptonLabel2.Values.ExtraText = "";
            this.kryptonLabel2.Values.Image = null;
            this.kryptonLabel2.Values.Text = "Time:";
            // 
            // txtTime
            // 
            this.txtTime.Location = new System.Drawing.Point(286, 13);
            this.txtTime.Name = "txtTime";
            this.txtTime.ReadOnly = true;
            this.txtTime.Size = new System.Drawing.Size(44, 20);
            this.txtTime.TabIndex = 1;
            this.txtTime.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTime_KeyDown);
            this.txtTime.Leave += new System.EventHandler(this.txtTime_Leave);
            // 
            // cmbDuty
            // 
            this.cmbDuty.FormattingEnabled = true;
            this.cmbDuty.Items.AddRange(new object[] {
            "I",
            "O"});
            this.cmbDuty.Location = new System.Drawing.Point(204, 39);
            this.cmbDuty.Name = "cmbDuty";
            this.cmbDuty.Size = new System.Drawing.Size(47, 21);
            this.cmbDuty.TabIndex = 4;
            // 
            // TimeCard_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonGroup7);
            this.Name = "TimeCard_Control";
            this.Size = new System.Drawing.Size(341, 106);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup7.Panel)).EndInit();
            this.kryptonGroup7.Panel.ResumeLayout(false);
            this.kryptonGroup7.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup7)).EndInit();
            this.kryptonGroup7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1.Panel)).EndInit();
            this.kryptonGroup1.Panel.ResumeLayout(false);
            this.kryptonGroup1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1)).EndInit();
            this.kryptonGroup1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup7;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private System.Windows.Forms.DateTimePicker dtpCardDate;
        private System.Windows.Forms.TextBox txtCode;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader kryptonHeader7;
        private System.Windows.Forms.TextBox txtTaffId;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private System.Windows.Forms.TextBox txtTime;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private System.Windows.Forms.ComboBox cmbDuty;
    }
}
