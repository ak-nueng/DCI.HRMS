using DCI.HRMS.Attendance.Controls;
using DCI.HRMS.Model;
using System.Windows.Forms;
using DCI.HRMS.Model.Attendance;
using System;
namespace DCI.HRMS.Attendance
{
    partial class FrmShiftInput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmShiftInput));
            DCI.HRMS.Model.Attendance.EmployeeShiftInfo employeeShiftInfo1 = new DCI.HRMS.Model.Attendance.EmployeeShiftInfo();
            this.btnGenerate = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.kryptonGroup3 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.chkTr = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chkSub = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chkEmp = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.kryptonGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txbCodeManual = new System.Windows.Forms.TextBox();
            this.monthShift_Control1 = new DCI.HRMS.Attendance.Controls.MonthShift_Control();
            this.dgItems = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.delectSelectedRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kryptonHeaderGroup3 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.grpOt = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonTextBox1 = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.shGrp = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.shsts = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonHeaderGroup2 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.empShift_Control1 = new DCI.HRMS.Attendance.Controls.EmpShift_Control();
            this.kryptonGroup2 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.empData_Control1 = new DCI.HRMS.PER.Controls.EmpData_Control();
            this.ucl_ActionControl1 = new DCI.HRMS.Controls.Ucl_ActionControl();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.Panel.SuspendLayout();
            this.kryptonHeaderGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup3.Panel)).BeginInit();
            this.kryptonGroup3.Panel.SuspendLayout();
            this.kryptonGroup3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1.Panel)).BeginInit();
            this.kryptonGroup1.Panel.SuspendLayout();
            this.kryptonGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup3.Panel)).BeginInit();
            this.kryptonHeaderGroup3.Panel.SuspendLayout();
            this.kryptonHeaderGroup3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2.Panel)).BeginInit();
            this.kryptonHeaderGroup2.Panel.SuspendLayout();
            this.kryptonHeaderGroup2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup2.Panel)).BeginInit();
            this.kryptonGroup2.Panel.SuspendLayout();
            this.kryptonGroup2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.AutoSize = true;
            this.btnGenerate.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.btnGenerate.Location = new System.Drawing.Point(313, 3);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.btnGenerate.Size = new System.Drawing.Size(85, 30);
            this.btnGenerate.TabIndex = 5;
            this.btnGenerate.TabStop = false;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.Values.ExtraText = "";
            this.btnGenerate.Values.Image = global::DCI.HRMS.Properties.Resources.sync2;
            this.btnGenerate.Values.ImageStates.ImageCheckedNormal = null;
            this.btnGenerate.Values.ImageStates.ImageCheckedPressed = null;
            this.btnGenerate.Values.ImageStates.ImageCheckedTracking = null;
            this.btnGenerate.Values.Text = "Generate";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
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
            this.kryptonHeaderGroup1.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup1.Location = new System.Drawing.Point(0, 45);
            this.kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            this.kryptonHeaderGroup1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonHeaderGroup1.Panel
            // 
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonGroup3);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.kryptonGroup1);
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.monthShift_Control1);
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(860, 238);
            this.kryptonHeaderGroup1.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonHeaderGroup1.StateCommon.Border.Rounding = 2;
            this.kryptonHeaderGroup1.TabIndex = 0;
            this.kryptonHeaderGroup1.Text = "Master Shift Data";
            this.kryptonHeaderGroup1.ValuesPrimary.Description = "";
            this.kryptonHeaderGroup1.ValuesPrimary.Heading = "Master Shift Data";
            this.kryptonHeaderGroup1.ValuesPrimary.Image = global::DCI.HRMS.Properties.Resources.sync2;
            this.kryptonHeaderGroup1.ValuesSecondary.Description = "";
            this.kryptonHeaderGroup1.ValuesSecondary.Heading = "Description";
            this.kryptonHeaderGroup1.ValuesSecondary.Image = null;
            this.kryptonHeaderGroup1.Click += new System.EventHandler(this.kryptonHeaderGroup1_Click);
            // 
            // kryptonGroup3
            // 
            this.kryptonGroup3.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonGroup3.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonGroup3.Location = new System.Drawing.Point(316, 161);
            this.kryptonGroup3.Name = "kryptonGroup3";
            this.kryptonGroup3.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonGroup3.Panel
            // 
            this.kryptonGroup3.Panel.Controls.Add(this.chkTr);
            this.kryptonGroup3.Panel.Controls.Add(this.chkSub);
            this.kryptonGroup3.Panel.Controls.Add(this.chkEmp);
            this.kryptonGroup3.Panel.Controls.Add(this.btnGenerate);
            this.kryptonGroup3.Size = new System.Drawing.Size(405, 43);
            this.kryptonGroup3.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonGroup3.StateCommon.Border.Rounding = 2;
            this.kryptonGroup3.TabIndex = 8;
            // 
            // chkTr
            // 
            this.chkTr.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.chkTr.Location = new System.Drawing.Point(211, 9);
            this.chkTr.Name = "chkTr";
            this.chkTr.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.chkTr.Size = new System.Drawing.Size(63, 20);
            this.chkTr.TabIndex = 6;
            this.chkTr.Text = "Trainee";
            this.chkTr.Values.ExtraText = "";
            this.chkTr.Values.Image = null;
            this.chkTr.Values.Text = "Trainee";
            // 
            // chkSub
            // 
            this.chkSub.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.chkSub.Location = new System.Drawing.Point(109, 9);
            this.chkSub.Name = "chkSub";
            this.chkSub.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.chkSub.Size = new System.Drawing.Size(89, 20);
            this.chkSub.TabIndex = 6;
            this.chkSub.Text = "Subcontract";
            this.chkSub.Values.ExtraText = "";
            this.chkSub.Values.Image = null;
            this.chkSub.Values.Text = "Subcontract";
            // 
            // chkEmp
            // 
            this.chkEmp.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.chkEmp.Location = new System.Drawing.Point(19, 9);
            this.chkEmp.Name = "chkEmp";
            this.chkEmp.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.chkEmp.Size = new System.Drawing.Size(77, 20);
            this.chkEmp.TabIndex = 6;
            this.chkEmp.Text = "Employee";
            this.chkEmp.Values.ExtraText = "";
            this.chkEmp.Values.Image = null;
            this.chkEmp.Values.Text = "Employee";
            // 
            // kryptonGroup1
            // 
            this.kryptonGroup1.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonGroup1.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonGroup1.Location = new System.Drawing.Point(7, 160);
            this.kryptonGroup1.Name = "kryptonGroup1";
            this.kryptonGroup1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonGroup1.Panel
            // 
            this.kryptonGroup1.Panel.Controls.Add(this.kryptonButton1);
            this.kryptonGroup1.Panel.Controls.Add(this.kryptonLabel7);
            this.kryptonGroup1.Panel.Controls.Add(this.txbCodeManual);
            this.kryptonGroup1.Size = new System.Drawing.Size(303, 43);
            this.kryptonGroup1.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonGroup1.StateCommon.Border.Rounding = 2;
            this.kryptonGroup1.TabIndex = 7;
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.AutoSize = true;
            this.kryptonButton1.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.kryptonButton1.Location = new System.Drawing.Point(198, 3);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonButton1.Size = new System.Drawing.Size(90, 30);
            this.kryptonButton1.TabIndex = 1;
            this.kryptonButton1.Text = "Clear List";
            this.kryptonButton1.Values.ExtraText = "";
            this.kryptonButton1.Values.Image = global::DCI.HRMS.Properties.Resources.remove_file;
            this.kryptonButton1.Values.ImageStates.ImageCheckedNormal = null;
            this.kryptonButton1.Values.ImageStates.ImageCheckedPressed = null;
            this.kryptonButton1.Values.ImageStates.ImageCheckedTracking = null;
            this.kryptonButton1.Values.Text = "Clear List";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel7.Location = new System.Drawing.Point(3, 10);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel7.Size = new System.Drawing.Size(42, 20);
            this.kryptonLabel7.TabIndex = 4;
            this.kryptonLabel7.Text = "Code:";
            this.kryptonLabel7.Values.ExtraText = "";
            this.kryptonLabel7.Values.Image = null;
            this.kryptonLabel7.Values.Text = "Code:";
            // 
            // txbCodeManual
            // 
            this.txbCodeManual.Location = new System.Drawing.Point(49, 9);
            this.txbCodeManual.Name = "txbCodeManual";
            this.txbCodeManual.Size = new System.Drawing.Size(100, 20);
            this.txbCodeManual.TabIndex = 0;
            this.txbCodeManual.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbCodeManual_KeyDown);
            // 
            // monthShift_Control1
            // 
            this.monthShift_Control1.Cursor = System.Windows.Forms.Cursors.Default;
            this.monthShift_Control1.Information = ((object)(resources.GetObject("monthShift_Control1.Information")));
            this.monthShift_Control1.Location = new System.Drawing.Point(7, 5);
            this.monthShift_Control1.Name = "monthShift_Control1";
            this.monthShift_Control1.Size = new System.Drawing.Size(711, 150);
            this.monthShift_Control1.TabIndex = 4;
            this.monthShift_Control1.TabStop = false;
            this.monthShift_Control1.month_Changed += new DCI.HRMS.Attendance.Controls.MonthShift_Control.month_ChangeHandler(this.monthShift_Control1_grp_Changed);
            this.monthShift_Control1.year_Changed += new DCI.HRMS.Attendance.Controls.MonthShift_Control.year_ChangeHandler(this.monthShift_Control1_grp_Changed);
            this.monthShift_Control1.grp_Changed += new DCI.HRMS.Attendance.Controls.MonthShift_Control.grp_ChangeHandler(this.monthShift_Control1_grp_Changed);
            // 
            // dgItems
            // 
            this.dgItems.AllowUserToAddRows = false;
            this.dgItems.ContextMenuStrip = this.contextMenuStrip1;
            this.dgItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgItems.GridStyles.Style = ComponentFactory.Krypton.Toolkit.DataGridViewStyle.Sheet;
            this.dgItems.GridStyles.StyleBackground = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundSheet;
            this.dgItems.GridStyles.StyleColumn = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
            this.dgItems.GridStyles.StyleDataCells = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
            this.dgItems.GridStyles.StyleRow = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
            this.dgItems.Location = new System.Drawing.Point(0, 0);
            this.dgItems.MultiSelect = false;
            this.dgItems.Name = "dgItems";
            this.dgItems.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.dgItems.ReadOnly = true;
            this.dgItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgItems.Size = new System.Drawing.Size(856, 68);
            this.dgItems.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundSheet;
            this.dgItems.TabIndex = 4;
            this.dgItems.TabStop = false;
            this.dgItems.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgItems_DataBindingComplete);
            this.dgItems.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgItems_RowPostPaint);
            this.dgItems.SelectionChanged += new System.EventHandler(this.dgItems_SelectionChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.delectSelectedRowsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 26);
            // 
            // delectSelectedRowsToolStripMenuItem
            // 
            this.delectSelectedRowsToolStripMenuItem.Image = global::DCI.HRMS.Properties.Resources.remove_file;
            this.delectSelectedRowsToolStripMenuItem.Name = "delectSelectedRowsToolStripMenuItem";
            this.delectSelectedRowsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.delectSelectedRowsToolStripMenuItem.Text = "DelectSelected Rows";
            this.delectSelectedRowsToolStripMenuItem.Click += new System.EventHandler(this.delectSelectedRowsToolStripMenuItem_Click);
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
            this.kryptonHeaderGroup3.Location = new System.Drawing.Point(0, 636);
            this.kryptonHeaderGroup3.Name = "kryptonHeaderGroup3";
            this.kryptonHeaderGroup3.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonHeaderGroup3.Panel
            // 
            this.kryptonHeaderGroup3.Panel.Controls.Add(this.dgItems);
            this.kryptonHeaderGroup3.Size = new System.Drawing.Size(860, 106);
            this.kryptonHeaderGroup3.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonHeaderGroup3.StateCommon.Border.Rounding = 2;
            this.kryptonHeaderGroup3.TabIndex = 3;
            this.kryptonHeaderGroup3.Text = "Shift Data";
            this.kryptonHeaderGroup3.ValuesPrimary.Description = "";
            this.kryptonHeaderGroup3.ValuesPrimary.Heading = "Shift Data";
            this.kryptonHeaderGroup3.ValuesPrimary.Image = global::DCI.HRMS.Properties.Resources.Calendar;
            this.kryptonHeaderGroup3.ValuesPrimary.ImageTransparentColor = System.Drawing.Color.White;
            this.kryptonHeaderGroup3.ValuesSecondary.Description = "";
            this.kryptonHeaderGroup3.ValuesSecondary.Heading = "Description";
            this.kryptonHeaderGroup3.ValuesSecondary.Image = null;
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel5.Location = new System.Drawing.Point(80, 51);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel5.Size = new System.Drawing.Size(39, 19);
            this.kryptonLabel5.TabIndex = 0;
            this.kryptonLabel5.Text = "Grpot";
            this.kryptonLabel5.Values.ExtraText = "";
            this.kryptonLabel5.Values.Image = null;
            this.kryptonLabel5.Values.Text = "Grpot";
            // 
            // grpOt
            // 
            this.grpOt.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.grpOt.Location = new System.Drawing.Point(80, 51);
            this.grpOt.Name = "grpOt";
            this.grpOt.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.grpOt.Size = new System.Drawing.Size(39, 19);
            this.grpOt.TabIndex = 0;
            this.grpOt.Text = "Grpot";
            this.grpOt.Values.ExtraText = "";
            this.grpOt.Values.Image = null;
            this.grpOt.Values.Text = "Grpot";
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = true;
            this.btnSave.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.btnSave.Location = new System.Drawing.Point(456, 128);
            this.btnSave.Name = "btnSave";
            this.btnSave.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.btnSave.Size = new System.Drawing.Size(85, 30);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.Values.ExtraText = "";
            this.btnSave.Values.Image = global::DCI.HRMS.Properties.Resources.remove;
            this.btnSave.Values.ImageStates.ImageCheckedNormal = null;
            this.btnSave.Values.ImageStates.ImageCheckedPressed = null;
            this.btnSave.Values.ImageStates.ImageCheckedTracking = null;
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // kryptonTextBox1
            // 
            this.kryptonTextBox1.InputControlStyle = ComponentFactory.Krypton.Toolkit.InputControlStyle.Standalone;
            this.kryptonTextBox1.Location = new System.Drawing.Point(74, 134);
            this.kryptonTextBox1.Name = "kryptonTextBox1";
            this.kryptonTextBox1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonTextBox1.Size = new System.Drawing.Size(100, 20);
            this.kryptonTextBox1.TabIndex = 3;
            // 
            // shGrp
            // 
            this.shGrp.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.shGrp.Location = new System.Drawing.Point(193, 51);
            this.shGrp.Name = "shGrp";
            this.shGrp.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.shGrp.Size = new System.Drawing.Size(39, 19);
            this.shGrp.TabIndex = 0;
            this.shGrp.Text = "Grpot";
            this.shGrp.Values.ExtraText = "";
            this.shGrp.Values.Image = null;
            this.shGrp.Values.Text = "Grpot";
            // 
            // shsts
            // 
            this.shsts.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.shsts.Location = new System.Drawing.Point(306, 51);
            this.shsts.Name = "shsts";
            this.shsts.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.shsts.Size = new System.Drawing.Size(39, 19);
            this.shsts.TabIndex = 0;
            this.shsts.Text = "Grpot";
            this.shsts.Values.ExtraText = "";
            this.shsts.Values.Image = null;
            this.shsts.Values.Text = "Grpot";
            // 
            // kryptonHeaderGroup2
            // 
            this.kryptonHeaderGroup2.AutoSize = true;
            this.kryptonHeaderGroup2.CollapseTarget = ComponentFactory.Krypton.Toolkit.HeaderGroupCollapsedTarget.CollapsedToPrimary;
            this.kryptonHeaderGroup2.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeaderGroup2.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonHeaderGroup2.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonHeaderGroup2.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Primary;
            this.kryptonHeaderGroup2.HeaderStyleSecondary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.kryptonHeaderGroup2.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup2.Location = new System.Drawing.Point(0, 283);
            this.kryptonHeaderGroup2.Name = "kryptonHeaderGroup2";
            this.kryptonHeaderGroup2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonHeaderGroup2.Panel
            // 
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.empShift_Control1);
            this.kryptonHeaderGroup2.Size = new System.Drawing.Size(860, 204);
            this.kryptonHeaderGroup2.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonHeaderGroup2.StateCommon.Border.Rounding = 2;
            this.kryptonHeaderGroup2.TabIndex = 2;
            this.kryptonHeaderGroup2.Text = "Modify Shift Data";
            this.kryptonHeaderGroup2.ValuesPrimary.Description = "";
            this.kryptonHeaderGroup2.ValuesPrimary.Heading = "Modify Shift Data";
            this.kryptonHeaderGroup2.ValuesPrimary.Image = global::DCI.HRMS.Properties.Resources.addd;
            this.kryptonHeaderGroup2.ValuesSecondary.Description = "";
            this.kryptonHeaderGroup2.ValuesSecondary.Heading = "Description";
            this.kryptonHeaderGroup2.ValuesSecondary.Image = null;
            this.kryptonHeaderGroup2.Click += new System.EventHandler(this.kryptonHeaderGroup1_Click);
            // 
            // empShift_Control1
            // 
            this.empShift_Control1.Cursor = System.Windows.Forms.Cursors.Default;
            employeeShiftInfo1.EmpCode = "";
            employeeShiftInfo1.ShiftData = "DDDDHHDDDDDHHDDDDDHHDDDDDHHDDDD";
            employeeShiftInfo1.ShiftO = "1111111111111111111111111111111";
            employeeShiftInfo1.YearMonth = "201401";
            this.empShift_Control1.Information = employeeShiftInfo1;
            this.empShift_Control1.Location = new System.Drawing.Point(11, 4);
            this.empShift_Control1.Name = "empShift_Control1";
            this.empShift_Control1.Size = new System.Drawing.Size(710, 166);
            this.empShift_Control1.TabIndex = 2;
            this.empShift_Control1.month_Changed += new DCI.HRMS.Attendance.Controls.EmpShift_Control.month_ChangeHandler(this.empShift_Control1_year_Changed);
            this.empShift_Control1.year_Changed += new DCI.HRMS.Attendance.Controls.EmpShift_Control.year_ChangeHandler(this.empShift_Control1_year_Changed);
            this.empShift_Control1.txtCode_Enter += new DCI.HRMS.Attendance.Controls.EmpShift_Control.txtCode_EnterHandler(this.empShift_Control1_txtCode_Enter);
            // 
            // kryptonGroup2
            // 
            this.kryptonGroup2.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonGroup2.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonGroup2.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonGroup2.Location = new System.Drawing.Point(0, 487);
            this.kryptonGroup2.Name = "kryptonGroup2";
            this.kryptonGroup2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonGroup2.Panel
            // 
            this.kryptonGroup2.Panel.Controls.Add(this.textBox1);
            this.kryptonGroup2.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonGroup2.Panel.Controls.Add(this.empData_Control1);
            this.kryptonGroup2.Size = new System.Drawing.Size(860, 149);
            this.kryptonGroup2.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Window;
            this.textBox1.Location = new System.Drawing.Point(587, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(43, 20);
            this.textBox1.TabIndex = 52;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel1.Location = new System.Drawing.Point(540, 35);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonLabel1.Size = new System.Drawing.Size(47, 20);
            this.kryptonLabel1.TabIndex = 51;
            this.kryptonLabel1.Text = "Group:";
            this.kryptonLabel1.Values.ExtraText = "";
            this.kryptonLabel1.Values.Image = null;
            this.kryptonLabel1.Values.Text = "Group:";
            // 
            // empData_Control1
            // 
            this.empData_Control1.Information = null;
            this.empData_Control1.Location = new System.Drawing.Point(3, 5);
            this.empData_Control1.Name = "empData_Control1";
            this.empData_Control1.Size = new System.Drawing.Size(719, 137);
            this.empData_Control1.TabIndex = 4;
            // 
            // ucl_ActionControl1
            // 
            this.ucl_ActionControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucl_ActionControl1.Location = new System.Drawing.Point(0, 0);
            this.ucl_ActionControl1.Name = "ucl_ActionControl1";
            this.ucl_ActionControl1.Size = new System.Drawing.Size(860, 45);
            this.ucl_ActionControl1.TabIndex = 0;
            this.ucl_ActionControl1.TabStop = false;
            // 
            // FrmShiftInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(860, 742);
            this.Controls.Add(this.kryptonHeaderGroup3);
            this.Controls.Add(this.kryptonGroup2);
            this.Controls.Add(this.kryptonHeaderGroup2);
            this.Controls.Add(this.kryptonHeaderGroup1);
            this.Controls.Add(this.ucl_ActionControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmShiftInput";
            this.Text = "FrmShiftInput";
            this.Load += new System.EventHandler(this.FrmShiftInput_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmShiftInput_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            this.kryptonHeaderGroup1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup3.Panel)).EndInit();
            this.kryptonGroup3.Panel.ResumeLayout(false);
            this.kryptonGroup3.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup3)).EndInit();
            this.kryptonGroup3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1.Panel)).EndInit();
            this.kryptonGroup1.Panel.ResumeLayout(false);
            this.kryptonGroup1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1)).EndInit();
            this.kryptonGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup3.Panel)).EndInit();
            this.kryptonHeaderGroup3.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup3)).EndInit();
            this.kryptonHeaderGroup3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2.Panel)).EndInit();
            this.kryptonHeaderGroup2.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2)).EndInit();
            this.kryptonHeaderGroup2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup2.Panel)).EndInit();
            this.kryptonGroup2.Panel.ResumeLayout(false);
            this.kryptonGroup2.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup2)).EndInit();
            this.kryptonGroup2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DCI.HRMS.Controls.Ucl_ActionControl ucl_ActionControl1;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnGenerate;
        private MonthShift_Control monthShift_Control1;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgItems;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup3;
        private EmpShift_Control empShift_Control1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel grpOt;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private TextBox txbCodeManual;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox kryptonTextBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel shGrp;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel shsts;
        private ToolStripMenuItem delectSelectedRowsToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup2;
        private DCI.HRMS.PER.Controls.EmpData_Control empData_Control1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup2;
        private TextBox textBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup3;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkTr;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkSub;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkEmp;
    


       
    }
}