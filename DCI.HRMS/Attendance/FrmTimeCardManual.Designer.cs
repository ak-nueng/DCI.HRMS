namespace DCI.HRMS.Attendance
{
    partial class FrmTimeCardManual
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTimeCardManual));
            this.ucl_ActionControl1 = new DCI.HRMS.Controls.Ucl_ActionControl();
            this.timeCardManual_Control1 = new DCI.HRMS.Attendance.Controls.TimeCardManual_Control();
            this.kryptonHeaderGroup2 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.dgItems = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.kryptonHeaderGroup3 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.timeCardManual_Control2 = new DCI.HRMS.Attendance.Controls.TimeCardManual_Control();
            this.kryptonGroup5 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.kryptonGroup6 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.btnSearch = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cmbType = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.dpkDateTo = new System.Windows.Forms.DateTimePicker();
            this.dpkRqDate = new System.Windows.Forms.DateTimePicker();
            this.kryptonTextBox1 = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonHeader3 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.kryptonHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.empData_Control1 = new DCI.HRMS.PER.Controls.EmpData_Control();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2.Panel)).BeginInit();
            this.kryptonHeaderGroup2.Panel.SuspendLayout();
            this.kryptonHeaderGroup2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup3.Panel)).BeginInit();
            this.kryptonHeaderGroup3.Panel.SuspendLayout();
            this.kryptonHeaderGroup3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup5.Panel)).BeginInit();
            this.kryptonGroup5.Panel.SuspendLayout();
            this.kryptonGroup5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup6.Panel)).BeginInit();
            this.kryptonGroup6.Panel.SuspendLayout();
            this.kryptonGroup6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.Panel.SuspendLayout();
            this.kryptonHeaderGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucl_ActionControl1
            // 
            this.ucl_ActionControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucl_ActionControl1.Location = new System.Drawing.Point(0, 0);
            this.ucl_ActionControl1.Name = "ucl_ActionControl1";
            this.ucl_ActionControl1.Size = new System.Drawing.Size(1085, 45);
            this.ucl_ActionControl1.TabIndex = 0;
            this.ucl_ActionControl1.TabStop = false;
            // 
            // timeCardManual_Control1
            // 
            this.timeCardManual_Control1.CodeEnable = true;
            this.timeCardManual_Control1.Information = null;
            this.timeCardManual_Control1.Location = new System.Drawing.Point(12, 6);
            this.timeCardManual_Control1.Name = "timeCardManual_Control1";
            this.timeCardManual_Control1.Size = new System.Drawing.Size(620, 106);
            this.timeCardManual_Control1.TabIndex = 3;
            this.timeCardManual_Control1.enterCode += new DCI.HRMS.Attendance.Controls.TimeCardManual_Control.EnterCode(this.timeCardManual_Control1_enterCode);
            this.timeCardManual_Control1.Load += new System.EventHandler(this.timeCardManual_Control1_Load);
            // 
            // kryptonHeaderGroup2
            // 
            this.kryptonHeaderGroup2.CollapseTarget = ComponentFactory.Krypton.Toolkit.HeaderGroupCollapsedTarget.CollapsedToPrimary;
            this.kryptonHeaderGroup2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonHeaderGroup2.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonHeaderGroup2.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonHeaderGroup2.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Primary;
            this.kryptonHeaderGroup2.HeaderStyleSecondary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.kryptonHeaderGroup2.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup2.Location = new System.Drawing.Point(0, 562);
            this.kryptonHeaderGroup2.Name = "kryptonHeaderGroup2";
            this.kryptonHeaderGroup2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonHeaderGroup2.Panel
            // 
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.dgItems);
            this.kryptonHeaderGroup2.Size = new System.Drawing.Size(1085, 180);
            this.kryptonHeaderGroup2.TabIndex = 7;
            this.kryptonHeaderGroup2.Text = "ข้อมูลทั้งหมด";
            this.kryptonHeaderGroup2.ValuesPrimary.Description = "";
            this.kryptonHeaderGroup2.ValuesPrimary.Heading = "ข้อมูลทั้งหมด";
            this.kryptonHeaderGroup2.ValuesPrimary.Image = global::DCI.HRMS.Properties.Resources.script;
            this.kryptonHeaderGroup2.ValuesSecondary.Description = "";
            this.kryptonHeaderGroup2.ValuesSecondary.Heading = "Description";
            this.kryptonHeaderGroup2.ValuesSecondary.Image = null;
            // 
            // dgItems
            // 
            this.dgItems.AllowUserToAddRows = false;
            this.dgItems.AllowUserToDeleteRows = false;
            this.dgItems.AllowUserToResizeRows = false;
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
            this.dgItems.Size = new System.Drawing.Size(1083, 148);
            this.dgItems.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundSheet;
            this.dgItems.TabIndex = 0;
            this.dgItems.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgItems_RowPostPaint);
            this.dgItems.SelectionChanged += new System.EventHandler(this.dgItems_SelectionChanged_1);
            // 
            // kryptonHeaderGroup3
            // 
            this.kryptonHeaderGroup3.AutoSize = true;
            this.kryptonHeaderGroup3.CollapseTarget = ComponentFactory.Krypton.Toolkit.HeaderGroupCollapsedTarget.CollapsedToPrimary;
            this.kryptonHeaderGroup3.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeaderGroup3.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonHeaderGroup3.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonHeaderGroup3.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Primary;
            this.kryptonHeaderGroup3.HeaderStyleSecondary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.kryptonHeaderGroup3.Location = new System.Drawing.Point(0, 213);
            this.kryptonHeaderGroup3.Name = "kryptonHeaderGroup3";
            this.kryptonHeaderGroup3.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonHeaderGroup3.Panel
            // 
            this.kryptonHeaderGroup3.Panel.AutoScroll = true;
            this.kryptonHeaderGroup3.Panel.Controls.Add(this.timeCardManual_Control2);
            this.kryptonHeaderGroup3.Panel.Controls.Add(this.kryptonGroup5);
            this.kryptonHeaderGroup3.Size = new System.Drawing.Size(1085, 213);
            this.kryptonHeaderGroup3.TabIndex = 8;
            this.kryptonHeaderGroup3.Text = "ค้นหาและแก้ไข";
            this.kryptonHeaderGroup3.ValuesPrimary.Description = "";
            this.kryptonHeaderGroup3.ValuesPrimary.Heading = "ค้นหาและแก้ไข";
            this.kryptonHeaderGroup3.ValuesPrimary.Image = global::DCI.HRMS.Properties.Resources.checkmark;
            this.kryptonHeaderGroup3.ValuesSecondary.Description = "";
            this.kryptonHeaderGroup3.ValuesSecondary.Heading = "Description";
            this.kryptonHeaderGroup3.ValuesSecondary.Image = null;
            this.kryptonHeaderGroup3.Click += new System.EventHandler(this.kryptonHeaderGroup1_Click);
            // 
            // timeCardManual_Control2
            // 
            this.timeCardManual_Control2.CodeEnable = false;
            this.timeCardManual_Control2.Information = null;
            this.timeCardManual_Control2.Location = new System.Drawing.Point(299, 6);
            this.timeCardManual_Control2.Name = "timeCardManual_Control2";
            this.timeCardManual_Control2.Size = new System.Drawing.Size(640, 151);
            this.timeCardManual_Control2.TabIndex = 2;
            this.timeCardManual_Control2.enterData += new DCI.HRMS.Attendance.Controls.TimeCardManual_Control.Enter_data(this.timeCardManual_Control2_enterData);
            this.timeCardManual_Control2.Load += new System.EventHandler(this.timeCardManual_Control1_Load);
            // 
            // kryptonGroup5
            // 
            this.kryptonGroup5.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonGroup5.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonGroup5.Location = new System.Drawing.Point(9, 7);
            this.kryptonGroup5.Name = "kryptonGroup5";
            this.kryptonGroup5.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonGroup5.Panel
            // 
            this.kryptonGroup5.Panel.Controls.Add(this.kryptonGroup6);
            this.kryptonGroup5.Panel.Controls.Add(this.kryptonHeader3);
            this.kryptonGroup5.Size = new System.Drawing.Size(284, 150);
            this.kryptonGroup5.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonGroup5.StateCommon.Border.Rounding = 2;
            this.kryptonGroup5.TabIndex = 1;
            // 
            // kryptonGroup6
            // 
            this.kryptonGroup6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroup6.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonGroup6.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonGroup6.Location = new System.Drawing.Point(0, 27);
            this.kryptonGroup6.Name = "kryptonGroup6";
            this.kryptonGroup6.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonGroup6.Panel
            // 
            this.kryptonGroup6.Panel.Controls.Add(this.btnSearch);
            this.kryptonGroup6.Panel.Controls.Add(this.kryptonLabel3);
            this.kryptonGroup6.Panel.Controls.Add(this.cmbType);
            this.kryptonGroup6.Panel.Controls.Add(this.dpkDateTo);
            this.kryptonGroup6.Panel.Controls.Add(this.dpkRqDate);
            this.kryptonGroup6.Panel.Controls.Add(this.kryptonTextBox1);
            this.kryptonGroup6.Panel.Controls.Add(this.kryptonLabel2);
            this.kryptonGroup6.Panel.Controls.Add(this.kryptonLabel5);
            this.kryptonGroup6.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonGroup6.Size = new System.Drawing.Size(280, 119);
            this.kryptonGroup6.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonGroup6.StateCommon.Border.Rounding = 2;
            this.kryptonGroup6.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.AutoSize = true;
            this.btnSearch.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.btnSearch.Location = new System.Drawing.Point(167, 81);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.btnSearch.Size = new System.Drawing.Size(90, 30);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "Search";
            this.btnSearch.Values.ExtraText = "";
            this.btnSearch.Values.Image = global::DCI.HRMS.Properties.Resources.find;
            this.btnSearch.Values.ImageStates.ImageCheckedNormal = null;
            this.btnSearch.Values.ImageStates.ImageCheckedPressed = null;
            this.btnSearch.Values.ImageStates.ImageCheckedTracking = null;
            this.btnSearch.Values.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel3.Location = new System.Drawing.Point(3, 54);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel3.Size = new System.Drawing.Size(91, 20);
            this.kryptonLabel3.TabIndex = 13;
            this.kryptonLabel3.Text = "TimeCardType:";
            this.kryptonLabel3.Values.ExtraText = "";
            this.kryptonLabel3.Values.Image = null;
            this.kryptonLabel3.Values.Text = "TimeCardType:";
            // 
            // cmbType
            // 
            this.cmbType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbType.DropBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.cmbType.DropButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.InputControl;
            this.cmbType.DropDownWidth = 251;
            this.cmbType.InputControlStyle = ComponentFactory.Krypton.Toolkit.InputControlStyle.Standalone;
            this.cmbType.ItemStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.ListItem;
            this.cmbType.Location = new System.Drawing.Point(93, 54);
            this.cmbType.Name = "cmbType";
            this.cmbType.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.cmbType.Size = new System.Drawing.Size(164, 21);
            this.cmbType.TabIndex = 11;
            // 
            // dpkDateTo
            // 
            this.dpkDateTo.CustomFormat = "dd/MM/yyyy";
            this.dpkDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpkDateTo.Location = new System.Drawing.Point(171, 28);
            this.dpkDateTo.Name = "dpkDateTo";
            this.dpkDateTo.Size = new System.Drawing.Size(86, 20);
            this.dpkDateTo.TabIndex = 10;
            this.dpkDateTo.ValueChanged += new System.EventHandler(this.dpkRqDate_ValueChanged);
            // 
            // dpkRqDate
            // 
            this.dpkRqDate.CustomFormat = "dd/MM/yyyy";
            this.dpkRqDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpkRqDate.Location = new System.Drawing.Point(41, 28);
            this.dpkRqDate.Name = "dpkRqDate";
            this.dpkRqDate.Size = new System.Drawing.Size(84, 20);
            this.dpkRqDate.TabIndex = 10;
            this.dpkRqDate.ValueChanged += new System.EventHandler(this.dpkRqDate_ValueChanged);
            // 
            // kryptonTextBox1
            // 
            this.kryptonTextBox1.InputControlStyle = ComponentFactory.Krypton.Toolkit.InputControlStyle.Standalone;
            this.kryptonTextBox1.Location = new System.Drawing.Point(41, 3);
            this.kryptonTextBox1.MaxLength = 5;
            this.kryptonTextBox1.Name = "kryptonTextBox1";
            this.kryptonTextBox1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonTextBox1.Size = new System.Drawing.Size(56, 23);
            this.kryptonTextBox1.TabIndex = 5;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel2.Location = new System.Drawing.Point(129, 29);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel2.Size = new System.Drawing.Size(39, 20);
            this.kryptonLabel2.TabIndex = 12;
            this.kryptonLabel2.Text = "Date:";
            this.kryptonLabel2.Values.ExtraText = "";
            this.kryptonLabel2.Values.Image = null;
            this.kryptonLabel2.Values.Text = "Date:";
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel5.Location = new System.Drawing.Point(1, 29);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel5.Size = new System.Drawing.Size(39, 20);
            this.kryptonLabel5.TabIndex = 12;
            this.kryptonLabel5.Text = "Date:";
            this.kryptonLabel5.Values.ExtraText = "";
            this.kryptonLabel5.Values.Image = null;
            this.kryptonLabel5.Values.Text = "Date:";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel1.Location = new System.Drawing.Point(1, 6);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel1.Size = new System.Drawing.Size(42, 20);
            this.kryptonLabel1.TabIndex = 4;
            this.kryptonLabel1.Text = "Code:";
            this.kryptonLabel1.Values.ExtraText = "";
            this.kryptonLabel1.Values.Image = null;
            this.kryptonLabel1.Values.Text = "Code:";
            // 
            // kryptonHeader3
            // 
            this.kryptonHeader3.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeader3.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.Form;
            this.kryptonHeader3.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeader3.Name = "kryptonHeader3";
            this.kryptonHeader3.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonHeader3.Size = new System.Drawing.Size(280, 27);
            this.kryptonHeader3.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonHeader3.StateCommon.Border.Rounding = 2;
            this.kryptonHeader3.TabIndex = 0;
            this.kryptonHeader3.Text = "Search";
            this.kryptonHeader3.Values.Description = "Description";
            this.kryptonHeader3.Values.Heading = "Search";
            this.kryptonHeader3.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonHeader3.Values.Image")));
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
            this.kryptonHeaderGroup1.Panel.AutoScroll = true;
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonButton1);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.timeCardManual_Control1);
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(1085, 168);
            this.kryptonHeaderGroup1.TabIndex = 6;
            this.kryptonHeaderGroup1.Text = "เพิ่มข้อมูล";
            this.kryptonHeaderGroup1.ValuesPrimary.Description = "";
            this.kryptonHeaderGroup1.ValuesPrimary.Heading = "เพิ่มข้อมูล";
            this.kryptonHeaderGroup1.ValuesPrimary.Image = global::DCI.HRMS.Properties.Resources.add;
            this.kryptonHeaderGroup1.ValuesSecondary.Description = "";
            this.kryptonHeaderGroup1.ValuesSecondary.Heading = "Description";
            this.kryptonHeaderGroup1.ValuesSecondary.Image = null;
            this.kryptonHeaderGroup1.Click += new System.EventHandler(this.kryptonHeaderGroup1_Click);
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.AutoSize = true;
            this.kryptonButton1.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.kryptonButton1.Location = new System.Drawing.Point(638, 74);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonButton1.Size = new System.Drawing.Size(90, 30);
            this.kryptonButton1.TabIndex = 7;
            this.kryptonButton1.Text = "Clear List";
            this.kryptonButton1.Values.ExtraText = "";
            this.kryptonButton1.Values.Image = global::DCI.HRMS.Properties.Resources.remove_file;
            this.kryptonButton1.Values.ImageStates.ImageCheckedNormal = null;
            this.kryptonButton1.Values.ImageStates.ImageCheckedPressed = null;
            this.kryptonButton1.Values.ImageStates.ImageCheckedTracking = null;
            this.kryptonButton1.Values.Text = "Clear List";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // empData_Control1
            // 
            this.empData_Control1.Dock = System.Windows.Forms.DockStyle.Top;
            this.empData_Control1.Information = null;
            this.empData_Control1.Location = new System.Drawing.Point(0, 426);
            this.empData_Control1.Name = "empData_Control1";
            this.empData_Control1.Size = new System.Drawing.Size(1085, 136);
            this.empData_Control1.TabIndex = 9;
            this.empData_Control1.TabStop = false;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printDialog1
            // 
            this.printDialog1.Document = this.printDocument1;
            this.printDialog1.UseEXDialog = true;
            // 
            // FrmTimeCardManual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1085, 742);
            this.Controls.Add(this.kryptonHeaderGroup2);
            this.Controls.Add(this.empData_Control1);
            this.Controls.Add(this.kryptonHeaderGroup3);
            this.Controls.Add(this.kryptonHeaderGroup1);
            this.Controls.Add(this.ucl_ActionControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmTimeCardManual";
            this.Text = "FrmTimeCardManual";
            this.Load += new System.EventHandler(this.FrmTimeCardManual_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2.Panel)).EndInit();
            this.kryptonHeaderGroup2.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2)).EndInit();
            this.kryptonHeaderGroup2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup3.Panel)).EndInit();
            this.kryptonHeaderGroup3.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup3)).EndInit();
            this.kryptonHeaderGroup3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup5.Panel)).EndInit();
            this.kryptonGroup5.Panel.ResumeLayout(false);
            this.kryptonGroup5.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup5)).EndInit();
            this.kryptonGroup5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup6.Panel)).EndInit();
            this.kryptonGroup6.Panel.ResumeLayout(false);
            this.kryptonGroup6.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup6)).EndInit();
            this.kryptonGroup6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            this.kryptonHeaderGroup1.Panel.ResumeLayout(false);
            this.kryptonHeaderGroup1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DCI.HRMS.Controls.Ucl_ActionControl ucl_ActionControl1;
        private DCI.HRMS.Attendance.Controls.TimeCardManual_Control timeCardManual_Control1;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup2;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup3;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup1;
        private DCI.HRMS.Attendance.Controls.TimeCardManual_Control timeCardManual_Control2;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup5;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup6;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader kryptonHeader3;
        private DCI.HRMS.PER.Controls.EmpData_Control empData_Control1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox kryptonTextBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cmbType;
        private System.Windows.Forms.DateTimePicker dpkDateTo;
        private System.Windows.Forms.DateTimePicker dpkRqDate;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSearch;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgItems;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
    }
}