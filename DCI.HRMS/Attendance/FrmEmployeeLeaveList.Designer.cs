namespace DCI.HRMS.Attendance
{
    partial class FrmEmployeeLeaveList
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEmployeeLeaveList));
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonHeaderGroup2 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.dgvLeaveResult = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColJoin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColGet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColUse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColUseText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColRemain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColRemainHr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kryptonHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnDisplay = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.cbbLeaveType = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2.Panel)).BeginInit();
            this.kryptonHeaderGroup2.Panel.SuspendLayout();
            this.kryptonHeaderGroup2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeaveResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.Panel.SuspendLayout();
            this.kryptonHeaderGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbbLeaveType)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.kryptonHeaderGroup2);
            this.kryptonPanel.Controls.Add(this.kryptonHeaderGroup1);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonPanel.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kryptonPanel.Size = new System.Drawing.Size(901, 489);
            this.kryptonPanel.TabIndex = 0;
            // 
            // kryptonHeaderGroup2
            // 
            this.kryptonHeaderGroup2.CollapseTarget = ComponentFactory.Krypton.Toolkit.HeaderGroupCollapsedTarget.CollapsedToPrimary;
            this.kryptonHeaderGroup2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonHeaderGroup2.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonHeaderGroup2.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonHeaderGroup2.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Primary;
            this.kryptonHeaderGroup2.HeaderStyleSecondary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.kryptonHeaderGroup2.Location = new System.Drawing.Point(0, 85);
            this.kryptonHeaderGroup2.Name = "kryptonHeaderGroup2";
            this.kryptonHeaderGroup2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonHeaderGroup2.Panel
            // 
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.dgvLeaveResult);
            this.kryptonHeaderGroup2.Size = new System.Drawing.Size(901, 404);
            this.kryptonHeaderGroup2.TabIndex = 2;
            this.kryptonHeaderGroup2.Text = "Heading";
            this.kryptonHeaderGroup2.ValuesPrimary.Description = "";
            this.kryptonHeaderGroup2.ValuesPrimary.Heading = "Heading";
            this.kryptonHeaderGroup2.ValuesPrimary.Image = ((System.Drawing.Image)(resources.GetObject("kryptonHeaderGroup2.ValuesPrimary.Image")));
            this.kryptonHeaderGroup2.ValuesSecondary.Description = "";
            this.kryptonHeaderGroup2.ValuesSecondary.Heading = "Description";
            this.kryptonHeaderGroup2.ValuesSecondary.Image = null;
            // 
            // dgvLeaveResult
            // 
            this.dgvLeaveResult.AllowUserToAddRows = false;
            this.dgvLeaveResult.AllowUserToDeleteRows = false;
            this.dgvLeaveResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLeaveResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCode,
            this.ColJoin,
            this.ColGet,
            this.ColUse,
            this.ColUseText,
            this.ColRemain,
            this.ColRemainHr});
            this.dgvLeaveResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLeaveResult.GridStyles.Style = ComponentFactory.Krypton.Toolkit.DataGridViewStyle.List;
            this.dgvLeaveResult.GridStyles.StyleBackground = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundList;
            this.dgvLeaveResult.GridStyles.StyleColumn = ComponentFactory.Krypton.Toolkit.GridStyle.List;
            this.dgvLeaveResult.GridStyles.StyleDataCells = ComponentFactory.Krypton.Toolkit.GridStyle.List;
            this.dgvLeaveResult.GridStyles.StyleRow = ComponentFactory.Krypton.Toolkit.GridStyle.List;
            this.dgvLeaveResult.Location = new System.Drawing.Point(0, 0);
            this.dgvLeaveResult.Name = "dgvLeaveResult";
            this.dgvLeaveResult.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.dgvLeaveResult.ReadOnly = true;
            this.dgvLeaveResult.Size = new System.Drawing.Size(899, 351);
            this.dgvLeaveResult.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundList;
            this.dgvLeaveResult.TabIndex = 0;
            // 
            // colCode
            // 
            this.colCode.HeaderText = "Code";
            this.colCode.Name = "colCode";
            this.colCode.ReadOnly = true;
            // 
            // ColJoin
            // 
            this.ColJoin.HeaderText = "Join";
            this.ColJoin.Name = "ColJoin";
            this.ColJoin.ReadOnly = true;
            // 
            // ColGet
            // 
            this.ColGet.HeaderText = "Get";
            this.ColGet.Name = "ColGet";
            this.ColGet.ReadOnly = true;
            // 
            // ColUse
            // 
            this.ColUse.HeaderText = "Use";
            this.ColUse.Name = "ColUse";
            this.ColUse.ReadOnly = true;
            // 
            // ColUseText
            // 
            this.ColUseText.HeaderText = "UseText";
            this.ColUseText.Name = "ColUseText";
            this.ColUseText.ReadOnly = true;
            // 
            // ColRemain
            // 
            this.ColRemain.HeaderText = "Remain";
            this.ColRemain.Name = "ColRemain";
            this.ColRemain.ReadOnly = true;
            // 
            // ColRemainHr
            // 
            this.ColRemainHr.HeaderText = "RemainHr";
            this.ColRemainHr.Name = "ColRemainHr";
            this.ColRemainHr.ReadOnly = true;
            // 
            // kryptonHeaderGroup1
            // 
            this.kryptonHeaderGroup1.CollapseTarget = ComponentFactory.Krypton.Toolkit.HeaderGroupCollapsedTarget.CollapsedToPrimary;
            this.kryptonHeaderGroup1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeaderGroup1.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonHeaderGroup1.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonHeaderGroup1.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Primary;
            this.kryptonHeaderGroup1.HeaderStyleSecondary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.kryptonHeaderGroup1.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            this.kryptonHeaderGroup1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonHeaderGroup1.Panel
            // 
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.btnDisplay);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.cbbLeaveType);
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(901, 85);
            this.kryptonHeaderGroup1.TabIndex = 2;
            this.kryptonHeaderGroup1.Text = "Heading";
            this.kryptonHeaderGroup1.ValuesPrimary.Description = "";
            this.kryptonHeaderGroup1.ValuesPrimary.Heading = "Heading";
            this.kryptonHeaderGroup1.ValuesPrimary.Image = ((System.Drawing.Image)(resources.GetObject("kryptonHeaderGroup1.ValuesPrimary.Image")));
            this.kryptonHeaderGroup1.ValuesSecondary.Description = "";
            this.kryptonHeaderGroup1.ValuesSecondary.Heading = "Description";
            this.kryptonHeaderGroup1.ValuesSecondary.Image = null;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel1.Location = new System.Drawing.Point(22, 17);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel1.Size = new System.Drawing.Size(77, 20);
            this.kryptonLabel1.TabIndex = 2;
            this.kryptonLabel1.Text = "Leave Type :";
            this.kryptonLabel1.Values.ExtraText = "";
            this.kryptonLabel1.Values.Image = null;
            this.kryptonLabel1.Values.Text = "Leave Type :";
            // 
            // btnDisplay
            // 
            this.btnDisplay.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.btnDisplay.Location = new System.Drawing.Point(244, 12);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.btnDisplay.Size = new System.Drawing.Size(64, 28);
            this.btnDisplay.TabIndex = 1;
            this.btnDisplay.Text = "Display";
            this.btnDisplay.Values.ExtraText = "";
            this.btnDisplay.Values.Image = null;
            this.btnDisplay.Values.ImageStates.ImageCheckedNormal = null;
            this.btnDisplay.Values.ImageStates.ImageCheckedPressed = null;
            this.btnDisplay.Values.ImageStates.ImageCheckedTracking = null;
            this.btnDisplay.Values.Text = "Display";
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
            // 
            // cbbLeaveType
            // 
            this.cbbLeaveType.DropBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.cbbLeaveType.DropButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.InputControl;
            this.cbbLeaveType.DropDownWidth = 121;
            this.cbbLeaveType.InputControlStyle = ComponentFactory.Krypton.Toolkit.InputControlStyle.Standalone;
            this.cbbLeaveType.Items.AddRange(new object[] {
            "ANNU",
            "ABSE",
            "AMAT",
            "BMAT",
            "CARE",
            "EARL",
            "FUNE",
            "JSCK",
            "LATE",
            "MARR",
            "MATE",
            "MILI",
            "OTH",
            "PER",
            "PERS",
            "PRIE",
            "SICK",
            "SPSL",
            "TRAI"});
            this.cbbLeaveType.ItemStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.ListItem;
            this.cbbLeaveType.Location = new System.Drawing.Point(105, 16);
            this.cbbLeaveType.Name = "cbbLeaveType";
            this.cbbLeaveType.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.cbbLeaveType.Size = new System.Drawing.Size(121, 21);
            this.cbbLeaveType.TabIndex = 0;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // FrmEmployeeLeaveList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 489);
            this.Controls.Add(this.kryptonPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmEmployeeLeaveList";
            this.Text = "FrmEmployeeLeaveList";
            this.Load += new System.EventHandler(this.FrmEmployeeLeaveList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2.Panel)).EndInit();
            this.kryptonHeaderGroup2.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2)).EndInit();
            this.kryptonHeaderGroup2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeaveResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            this.kryptonHeaderGroup1.Panel.ResumeLayout(false);
            this.kryptonHeaderGroup1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbbLeaveType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup2;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDisplay;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbbLeaveType;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgvLeaveResult;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColJoin;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColGet;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColUse;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColUseText;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColRemain;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColRemainHr;
    }
}

