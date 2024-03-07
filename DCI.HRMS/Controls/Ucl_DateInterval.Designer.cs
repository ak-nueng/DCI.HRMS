namespace DCI.HRMS.Controls
{
    partial class Ucl_DateInterval
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
            this.DateText_Control1 = new System.Windows.Forms.DateTimePicker();
            this.dateText_Control2 = new System.Windows.Forms.DateTimePicker();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DateText_Control1
            // 
            this.DateText_Control1.BackColor = System.Drawing.SystemColors.Window;
            this.DateText_Control1.CustomFormat = "dd/MM/yyyy";
            this.DateText_Control1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateText_Control1.Location = new System.Drawing.Point(48, 3);
            this.DateText_Control1.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.DateText_Control1.Name = "DateText_Control1";
            this.DateText_Control1.Size = new System.Drawing.Size(87, 20);
            this.DateText_Control1.TabIndex = 0;
            this.DateText_Control1.ValueChanged += new System.EventHandler(this.DateText_Control1_ValueChanged);
            // 
            // dateText_Control2
            // 
            this.dateText_Control2.BackColor = System.Drawing.SystemColors.Window;
            this.dateText_Control2.CustomFormat = "dd/MM/yyyy";
            this.dateText_Control2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateText_Control2.Location = new System.Drawing.Point(162, 3);
            this.dateText_Control2.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dateText_Control2.Name = "dateText_Control2";
            this.dateText_Control2.Size = new System.Drawing.Size(87, 20);
            this.dateText_Control2.TabIndex = 1;
            this.dateText_Control2.ValueChanged += new System.EventHandler(this.dateText_Control2_ValueChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Window;
            this.flowLayoutPanel1.Controls.Add(this.kryptonLabel2);
            this.flowLayoutPanel1.Controls.Add(this.DateText_Control1);
            this.flowLayoutPanel1.Controls.Add(this.kryptonLabel1);
            this.flowLayoutPanel1.Controls.Add(this.dateText_Control2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(312, 29);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel2.Location = new System.Drawing.Point(3, 3);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel2.Size = new System.Drawing.Size(39, 20);
            this.kryptonLabel2.TabIndex = 3;
            this.kryptonLabel2.Text = "Date:";
            this.kryptonLabel2.Values.ExtraText = "";
            this.kryptonLabel2.Values.Image = null;
            this.kryptonLabel2.Values.Text = "Date:";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel1.Location = new System.Drawing.Point(141, 3);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel1.Size = new System.Drawing.Size(15, 20);
            this.kryptonLabel1.TabIndex = 2;
            this.kryptonLabel1.Text = "-";
            this.kryptonLabel1.Values.ExtraText = "";
            this.kryptonLabel1.Values.Image = null;
            this.kryptonLabel1.Values.Text = "-";
            // 
            // Ucl_DateInterval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "Ucl_DateInterval";
            this.Size = new System.Drawing.Size(312, 29);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker DateText_Control1;
        private System.Windows.Forms.DateTimePicker dateText_Control2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
    }
}
