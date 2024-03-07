namespace DCI.HRMS.Master
{
    partial class FrmOrganize
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOrganize));
            this.kryptonHeaderGroup3 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.dgItems = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.kryptonHeaderGroup2 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboOwner = new System.Windows.Forms.ComboBox();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtShortName = new System.Windows.Forms.TextBox();
            this.kryptonLabel9 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.ucl_ActionControl1 = new DCI.HRMS.Controls.Ucl_ActionControl();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup3.Panel)).BeginInit();
            this.kryptonHeaderGroup3.Panel.SuspendLayout();
            this.kryptonHeaderGroup3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2.Panel)).BeginInit();
            this.kryptonHeaderGroup2.Panel.SuspendLayout();
            this.kryptonHeaderGroup2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonHeaderGroup3
            // 
            this.kryptonHeaderGroup3.CollapseTarget = ComponentFactory.Krypton.Toolkit.HeaderGroupCollapsedTarget.CollapsedToPrimary;
            this.kryptonHeaderGroup3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonHeaderGroup3.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonHeaderGroup3.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonHeaderGroup3.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Primary;
            this.kryptonHeaderGroup3.HeaderStyleSecondary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.kryptonHeaderGroup3.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup3.Location = new System.Drawing.Point(0, 45);
            this.kryptonHeaderGroup3.Name = "kryptonHeaderGroup3";
            this.kryptonHeaderGroup3.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonHeaderGroup3.Panel
            // 
            this.kryptonHeaderGroup3.Panel.Controls.Add(this.dgItems);
            this.kryptonHeaderGroup3.Size = new System.Drawing.Size(851, 451);
            this.kryptonHeaderGroup3.TabIndex = 19;
            this.kryptonHeaderGroup3.Text = "ข้อมูลทั้งหมด";
            this.kryptonHeaderGroup3.ValuesPrimary.Description = "";
            this.kryptonHeaderGroup3.ValuesPrimary.Heading = "ข้อมูลทั้งหมด";
            this.kryptonHeaderGroup3.ValuesPrimary.Image = global::DCI.HRMS.Properties.Resources.script;
            this.kryptonHeaderGroup3.ValuesSecondary.Description = "";
            this.kryptonHeaderGroup3.ValuesSecondary.Heading = "Description";
            this.kryptonHeaderGroup3.ValuesSecondary.Image = null;
            // 
            // dgItems
            // 
            this.dgItems.AllowUserToAddRows = false;
            this.dgItems.AllowUserToDeleteRows = false;
            this.dgItems.AllowUserToResizeRows = false;
            this.dgItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgItems.GridStyles.Style = ComponentFactory.Krypton.Toolkit.DataGridViewStyle.Sheet;
            this.dgItems.GridStyles.StyleBackground = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundSheet;
            this.dgItems.GridStyles.StyleColumn = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
            this.dgItems.GridStyles.StyleDataCells = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
            this.dgItems.GridStyles.StyleRow = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
            this.dgItems.Location = new System.Drawing.Point(0, 0);
            this.dgItems.Name = "dgItems";
            this.dgItems.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.dgItems.ReadOnly = true;
            this.dgItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgItems.Size = new System.Drawing.Size(849, 419);
            this.dgItems.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundSheet;
            this.dgItems.TabIndex = 0;
            this.dgItems.SelectionChanged += new System.EventHandler(this.dgItems_SelectionChanged);
            // 
            // kryptonHeaderGroup2
            // 
            this.kryptonHeaderGroup2.AutoSize = true;
            this.kryptonHeaderGroup2.CollapseTarget = ComponentFactory.Krypton.Toolkit.HeaderGroupCollapsedTarget.CollapsedToPrimary;
            this.kryptonHeaderGroup2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonHeaderGroup2.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonHeaderGroup2.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonHeaderGroup2.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Primary;
            this.kryptonHeaderGroup2.HeaderStyleSecondary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.kryptonHeaderGroup2.Location = new System.Drawing.Point(0, 496);
            this.kryptonHeaderGroup2.Name = "kryptonHeaderGroup2";
            this.kryptonHeaderGroup2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonHeaderGroup2.Panel
            // 
            this.kryptonHeaderGroup2.Panel.AutoScroll = true;
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.panel1);
            this.kryptonHeaderGroup2.Size = new System.Drawing.Size(851, 182);
            this.kryptonHeaderGroup2.TabIndex = 18;
            this.kryptonHeaderGroup2.Text = "เพิ่มและแก้ไขข้อมูล";
            this.kryptonHeaderGroup2.ValuesPrimary.Description = "";
            this.kryptonHeaderGroup2.ValuesPrimary.Heading = "เพิ่มและแก้ไขข้อมูล";
            this.kryptonHeaderGroup2.ValuesPrimary.Image = global::DCI.HRMS.Properties.Resources.add;
            this.kryptonHeaderGroup2.ValuesSecondary.Description = "";
            this.kryptonHeaderGroup2.ValuesSecondary.Heading = "Description";
            this.kryptonHeaderGroup2.ValuesSecondary.Image = null;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.cboOwner);
            this.panel1.Controls.Add(this.cboType);
            this.panel1.Controls.Add(this.kryptonLabel4);
            this.panel1.Controls.Add(this.kryptonLabel2);
            this.panel1.Controls.Add(this.kryptonLabel1);
            this.panel1.Controls.Add(this.txtCode);
            this.panel1.Controls.Add(this.kryptonLabel3);
            this.panel1.Controls.Add(this.kryptonLabel7);
            this.panel1.Controls.Add(this.txtShortName);
            this.panel1.Controls.Add(this.kryptonLabel9);
            this.panel1.Controls.Add(this.txtRemark);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(617, 123);
            this.panel1.TabIndex = 0;
            // 
            // cboOwner
            // 
            this.cboOwner.FormattingEnabled = true;
            this.cboOwner.Location = new System.Drawing.Point(105, 60);
            this.cboOwner.Name = "cboOwner";
            this.cboOwner.Size = new System.Drawing.Size(491, 21);
            this.cboOwner.TabIndex = 23;
            // 
            // cboType
            // 
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(402, 34);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(194, 21);
            this.cboType.TabIndex = 22;
            this.cboType.SelectedIndexChanged += new System.EventHandler(this.cboType_SelectedIndexChanged);
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel4.Location = new System.Drawing.Point(50, 61);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel4.Size = new System.Drawing.Size(49, 20);
            this.kryptonLabel4.TabIndex = 21;
            this.kryptonLabel4.Text = "Owner:";
            this.kryptonLabel4.Values.ExtraText = "";
            this.kryptonLabel4.Values.Image = null;
            this.kryptonLabel4.Values.Text = "Owner:";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel2.Location = new System.Drawing.Point(45, 87);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel2.Size = new System.Drawing.Size(54, 20);
            this.kryptonLabel2.TabIndex = 21;
            this.kryptonLabel2.Text = "Remark:";
            this.kryptonLabel2.Values.ExtraText = "";
            this.kryptonLabel2.Values.Image = null;
            this.kryptonLabel2.Values.Text = "Remark:";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel1.Location = new System.Drawing.Point(57, 11);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel1.Size = new System.Drawing.Size(42, 20);
            this.kryptonLabel1.TabIndex = 3;
            this.kryptonLabel1.Text = "Code:";
            this.kryptonLabel1.Values.ExtraText = "";
            this.kryptonLabel1.Values.Image = null;
            this.kryptonLabel1.Values.Text = "Code:";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(105, 10);
            this.txtCode.MaxLength = 4;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(66, 20);
            this.txtCode.TabIndex = 0;
            this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtType_KeyDown);
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel3.Location = new System.Drawing.Point(356, 35);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel3.Size = new System.Drawing.Size(39, 20);
            this.kryptonLabel3.TabIndex = 3;
            this.kryptonLabel3.Text = "Type:";
            this.kryptonLabel3.Values.ExtraText = "";
            this.kryptonLabel3.Values.Image = null;
            this.kryptonLabel3.Values.Text = "Type:";
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel7.Location = new System.Drawing.Point(24, 36);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel7.Size = new System.Drawing.Size(75, 20);
            this.kryptonLabel7.TabIndex = 18;
            this.kryptonLabel7.Text = "ShortName:";
            this.kryptonLabel7.Values.ExtraText = "";
            this.kryptonLabel7.Values.Image = null;
            this.kryptonLabel7.Values.Text = "ShortName:";
            // 
            // txtShortName
            // 
            this.txtShortName.Location = new System.Drawing.Point(105, 35);
            this.txtShortName.Name = "txtShortName";
            this.txtShortName.Size = new System.Drawing.Size(195, 20);
            this.txtShortName.TabIndex = 2;
            this.txtShortName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtType_KeyDown);
            // 
            // kryptonLabel9
            // 
            this.kryptonLabel9.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel9.Location = new System.Drawing.Point(177, 11);
            this.kryptonLabel9.Name = "kryptonLabel9";
            this.kryptonLabel9.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel9.Size = new System.Drawing.Size(46, 20);
            this.kryptonLabel9.TabIndex = 15;
            this.kryptonLabel9.Text = "Name:";
            this.kryptonLabel9.Values.ExtraText = "";
            this.kryptonLabel9.Values.Image = null;
            this.kryptonLabel9.Values.Text = "Name:";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(105, 87);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(491, 20);
            this.txtRemark.TabIndex = 5;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtThDetail_KeyDown);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(229, 10);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(367, 20);
            this.txtName.TabIndex = 1;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtType_KeyDown);
            // 
            // ucl_ActionControl1
            // 
            this.ucl_ActionControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucl_ActionControl1.Location = new System.Drawing.Point(0, 0);
            this.ucl_ActionControl1.Name = "ucl_ActionControl1";
            this.ucl_ActionControl1.Size = new System.Drawing.Size(851, 45);
            this.ucl_ActionControl1.TabIndex = 21;
            // 
            // FrmOrganize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 678);
            this.Controls.Add(this.kryptonHeaderGroup3);
            this.Controls.Add(this.kryptonHeaderGroup2);
            this.Controls.Add(this.ucl_ActionControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmOrganize";
            this.Text = "FrmOrganize";
            this.Load += new System.EventHandler(this.FrmOrganize_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDictionaryData_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup3.Panel)).EndInit();
            this.kryptonHeaderGroup3.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup3)).EndInit();
            this.kryptonHeaderGroup3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2.Panel)).EndInit();
            this.kryptonHeaderGroup2.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2)).EndInit();
            this.kryptonHeaderGroup2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup3;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgItems;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup2;
        private System.Windows.Forms.Panel panel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private System.Windows.Forms.TextBox txtCode;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private System.Windows.Forms.TextBox txtShortName;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel9;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.TextBox txtName;
        private DCI.HRMS.Controls.Ucl_ActionControl ucl_ActionControl1;
        private System.Windows.Forms.ComboBox cboOwner;
        private System.Windows.Forms.ComboBox cboType;

    }
}