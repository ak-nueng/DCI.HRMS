namespace DCI.HRMS.Attendance.Controls
{
    partial class AnnualLeave_Control
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnnualLeave_Control));
            this.kryptonGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.kryptonGroup2 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.kryptonDataGridView1 = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.Year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Get = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Used2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remain2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kryptonGroup3 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.txtRemain = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtCode = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtJoin = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
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
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup3.Panel)).BeginInit();
            this.kryptonGroup3.Panel.SuspendLayout();
            this.kryptonGroup3.SuspendLayout();
            this.SuspendLayout();
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
            this.kryptonGroup1.Size = new System.Drawing.Size(399, 206);
            this.kryptonGroup1.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonGroup1.StateCommon.Border.Rounding = 2;
            this.kryptonGroup1.TabIndex = 0;
            // 
            // kryptonGroup2
            // 
            this.kryptonGroup2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroup2.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonGroup2.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonGroup2.Location = new System.Drawing.Point(0, 27);
            this.kryptonGroup2.Name = "kryptonGroup2";
            this.kryptonGroup2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonGroup2.Panel
            // 
            this.kryptonGroup2.Panel.Controls.Add(this.kryptonDataGridView1);
            this.kryptonGroup2.Panel.Controls.Add(this.kryptonGroup3);
            this.kryptonGroup2.Size = new System.Drawing.Size(395, 175);
            this.kryptonGroup2.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonGroup2.StateCommon.Border.Rounding = 2;
            this.kryptonGroup2.TabIndex = 1;
            // 
            // kryptonDataGridView1
            // 
            this.kryptonDataGridView1.AllowUserToAddRows = false;
            this.kryptonDataGridView1.AllowUserToDeleteRows = false;
            this.kryptonDataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.kryptonDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Year,
            this.Get,
            this.Total,
            this.Used2,
            this.Remain2});
            this.kryptonDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonDataGridView1.GridStyles.Style = ComponentFactory.Krypton.Toolkit.DataGridViewStyle.Sheet;
            this.kryptonDataGridView1.GridStyles.StyleBackground = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundSheet;
            this.kryptonDataGridView1.GridStyles.StyleColumn = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
            this.kryptonDataGridView1.GridStyles.StyleDataCells = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
            this.kryptonDataGridView1.GridStyles.StyleRow = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
            this.kryptonDataGridView1.Location = new System.Drawing.Point(0, 34);
            this.kryptonDataGridView1.Name = "kryptonDataGridView1";
            this.kryptonDataGridView1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonDataGridView1.ReadOnly = true;
            this.kryptonDataGridView1.RowHeadersWidth = 20;
            this.kryptonDataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.kryptonDataGridView1.Size = new System.Drawing.Size(391, 137);
            this.kryptonDataGridView1.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundSheet;
            this.kryptonDataGridView1.TabIndex = 3;
            // 
            // Year
            // 
            this.Year.DataPropertyName = "Year";
            this.Year.HeaderText = "Year";
            this.Year.Name = "Year";
            this.Year.ReadOnly = true;
            this.Year.Width = 59;
            // 
            // Get
            // 
            this.Get.DataPropertyName = "Get";
            this.Get.HeaderText = "Get";
            this.Get.Name = "Get";
            this.Get.ReadOnly = true;
            this.Get.Width = 54;
            // 
            // Total
            // 
            this.Total.DataPropertyName = "TotalText";
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            this.Total.Width = 63;
            // 
            // Used2
            // 
            this.Used2.DataPropertyName = "UseText";
            this.Used2.HeaderText = "Use";
            this.Used2.Name = "Used2";
            this.Used2.ReadOnly = true;
            this.Used2.Width = 55;
            // 
            // Remain2
            // 
            this.Remain2.DataPropertyName = "RemainHr";
            this.Remain2.HeaderText = "Remain";
            this.Remain2.Name = "Remain2";
            this.Remain2.ReadOnly = true;
            this.Remain2.Width = 76;
            // 
            // kryptonGroup3
            // 
            this.kryptonGroup3.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonGroup3.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonGroup3.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonGroup3.Location = new System.Drawing.Point(0, 0);
            this.kryptonGroup3.Name = "kryptonGroup3";
            this.kryptonGroup3.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonGroup3.Panel
            // 
            this.kryptonGroup3.Panel.Controls.Add(this.txtRemain);
            this.kryptonGroup3.Panel.Controls.Add(this.txtCode);
            this.kryptonGroup3.Panel.Controls.Add(this.txtJoin);
            this.kryptonGroup3.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonGroup3.Panel.Controls.Add(this.kryptonLabel3);
            this.kryptonGroup3.Panel.Controls.Add(this.kryptonLabel2);
            this.kryptonGroup3.Size = new System.Drawing.Size(391, 34);
            this.kryptonGroup3.TabIndex = 4;
            // 
            // txtRemain
            // 
            this.txtRemain.InputControlStyle = ComponentFactory.Krypton.Toolkit.InputControlStyle.Standalone;
            this.txtRemain.Location = new System.Drawing.Point(284, 4);
            this.txtRemain.Name = "txtRemain";
            this.txtRemain.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.txtRemain.Size = new System.Drawing.Size(69, 17);
            this.txtRemain.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtRemain.StateCommon.Content.Color1 = System.Drawing.Color.Blue;
            this.txtRemain.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtRemain.TabIndex = 2;
            // 
            // txtCode
            // 
            this.txtCode.InputControlStyle = ComponentFactory.Krypton.Toolkit.InputControlStyle.Standalone;
            this.txtCode.Location = new System.Drawing.Point(42, 4);
            this.txtCode.Name = "txtCode";
            this.txtCode.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.txtCode.Size = new System.Drawing.Size(58, 20);
            this.txtCode.TabIndex = 2;
            // 
            // txtJoin
            // 
            this.txtJoin.InputControlStyle = ComponentFactory.Krypton.Toolkit.InputControlStyle.Standalone;
            this.txtJoin.Location = new System.Drawing.Point(159, 4);
            this.txtJoin.Name = "txtJoin";
            this.txtJoin.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.txtJoin.Size = new System.Drawing.Size(75, 20);
            this.txtJoin.TabIndex = 2;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel1.Location = new System.Drawing.Point(3, 7);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel1.Size = new System.Drawing.Size(42, 20);
            this.kryptonLabel1.TabIndex = 1;
            this.kryptonLabel1.Target = this.txtCode;
            this.kryptonLabel1.Text = "Code:";
            this.kryptonLabel1.Values.ExtraText = "";
            this.kryptonLabel1.Values.Image = null;
            this.kryptonLabel1.Values.Text = "Code:";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel3.Location = new System.Drawing.Point(234, 7);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel3.Size = new System.Drawing.Size(54, 20);
            this.kryptonLabel3.TabIndex = 1;
            this.kryptonLabel3.Target = this.txtRemain;
            this.kryptonLabel3.Text = "Remain:";
            this.kryptonLabel3.Values.ExtraText = "";
            this.kryptonLabel3.Values.Image = null;
            this.kryptonLabel3.Values.Text = "Remain:";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel2.Location = new System.Drawing.Point(100, 7);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel2.Size = new System.Drawing.Size(64, 20);
            this.kryptonLabel2.TabIndex = 1;
            this.kryptonLabel2.Target = this.txtJoin;
            this.kryptonLabel2.Text = "Join Date:";
            this.kryptonLabel2.Values.ExtraText = "";
            this.kryptonLabel2.Values.Image = null;
            this.kryptonLabel2.Values.Text = "Join Date:";
            // 
            // kryptonHeader1
            // 
            this.kryptonHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeader1.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.Form;
            this.kryptonHeader1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeader1.Name = "kryptonHeader1";
            this.kryptonHeader1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonHeader1.Size = new System.Drawing.Size(395, 27);
            this.kryptonHeader1.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonHeader1.StateCommon.Border.Rounding = 2;
            this.kryptonHeader1.TabIndex = 0;
            this.kryptonHeader1.Text = "Total Annual";
            this.kryptonHeader1.Values.Description = "พักร้อนรวม";
            this.kryptonHeader1.Values.Heading = "Total Annual";
            this.kryptonHeader1.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonHeader1.Values.Image")));
            // 
            // AnnualLeave_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonGroup1);
            this.Name = "AnnualLeave_Control";
            this.Size = new System.Drawing.Size(399, 206);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1.Panel)).EndInit();
            this.kryptonGroup1.Panel.ResumeLayout(false);
            this.kryptonGroup1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1)).EndInit();
            this.kryptonGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup2.Panel)).EndInit();
            this.kryptonGroup2.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup2)).EndInit();
            this.kryptonGroup2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup3.Panel)).EndInit();
            this.kryptonGroup3.Panel.ResumeLayout(false);
            this.kryptonGroup3.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup3)).EndInit();
            this.kryptonGroup3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup2;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtJoin;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader kryptonHeader1;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView kryptonDataGridView1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtCode;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtRemain;
        private System.Windows.Forms.DataGridViewTextBoxColumn Year;
        private System.Windows.Forms.DataGridViewTextBoxColumn Get;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn Used2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remain2;
    }
}
