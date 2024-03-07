using DCI.HRMS.Attendance.Controls;
namespace DCI.HRMS.Attendance
{
    partial class FrmShiftMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmShiftMaster));
            this.kryptonHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.month_Shift_Control1 = new DCI.HRMS.Attendance.Controls.MonthShift_Control();
            this.kryptonHeaderGroup2 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.dgItems = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.ucl_ActionControl1 = new DCI.HRMS.Controls.Ucl_ActionControl();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.Panel.SuspendLayout();
            this.kryptonHeaderGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2.Panel)).BeginInit();
            this.kryptonHeaderGroup2.Panel.SuspendLayout();
            this.kryptonHeaderGroup2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonHeaderGroup1
            // 
            this.kryptonHeaderGroup1.AutoSize = true;
            this.kryptonHeaderGroup1.CollapseTarget = ComponentFactory.Krypton.Toolkit.HeaderGroupCollapsedTarget.CollapsedToPrimary;
            this.kryptonHeaderGroup1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeaderGroup1.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonHeaderGroup1.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ButtonStandalone;
            this.kryptonHeaderGroup1.HeaderPositionSecondary = ComponentFactory.Krypton.Toolkit.VisualOrientation.Top;
            this.kryptonHeaderGroup1.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Primary;
            this.kryptonHeaderGroup1.HeaderStyleSecondary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Form;
            this.kryptonHeaderGroup1.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup1.Location = new System.Drawing.Point(0, 45);
            this.kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            this.kryptonHeaderGroup1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonHeaderGroup1.Panel
            // 
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.month_Shift_Control1);
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(831, 175);
            this.kryptonHeaderGroup1.TabIndex = 1;
            this.kryptonHeaderGroup1.Text = "Month Calendar";
            this.kryptonHeaderGroup1.ValuesPrimary.Description = "";
            this.kryptonHeaderGroup1.ValuesPrimary.Heading = "Month Calendar";
            this.kryptonHeaderGroup1.ValuesPrimary.Image = global::DCI.HRMS.Properties.Resources.date;
            this.kryptonHeaderGroup1.ValuesPrimary.ImageTransparentColor = System.Drawing.Color.WhiteSmoke;
            this.kryptonHeaderGroup1.ValuesSecondary.Description = "";
            this.kryptonHeaderGroup1.ValuesSecondary.Heading = "Description";
            this.kryptonHeaderGroup1.ValuesSecondary.Image = null;
            // 
            // month_Shift_Control1
            // 
            this.month_Shift_Control1.BackColor = System.Drawing.SystemColors.Window;
            this.month_Shift_Control1.Cursor = System.Windows.Forms.Cursors.Default;
            this.month_Shift_Control1.Dock = System.Windows.Forms.DockStyle.Top;
            this.month_Shift_Control1.Information = ((object)(resources.GetObject("month_Shift_Control1.Information")));
            this.month_Shift_Control1.Location = new System.Drawing.Point(0, 0);
            this.month_Shift_Control1.Name = "month_Shift_Control1";
            this.month_Shift_Control1.Size = new System.Drawing.Size(827, 139);
            this.month_Shift_Control1.TabIndex = 0;
            this.month_Shift_Control1.month_Changed += new DCI.HRMS.Attendance.Controls.MonthShift_Control.month_ChangeHandler(this.month_Shift_Control1_year_Changed);
            this.month_Shift_Control1.year_Changed += new DCI.HRMS.Attendance.Controls.MonthShift_Control.year_ChangeHandler(this.month_Shift_Control1_year_Changed);
            this.month_Shift_Control1.grp_Changed += new DCI.HRMS.Attendance.Controls.MonthShift_Control.grp_ChangeHandler(this.month_Shift_Control1_year_Changed);
            // 
            // kryptonHeaderGroup2
            // 
            this.kryptonHeaderGroup2.CollapseTarget = ComponentFactory.Krypton.Toolkit.HeaderGroupCollapsedTarget.CollapsedToPrimary;
            this.kryptonHeaderGroup2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonHeaderGroup2.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonHeaderGroup2.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ButtonStandalone;
            this.kryptonHeaderGroup2.HeaderPositionSecondary = ComponentFactory.Krypton.Toolkit.VisualOrientation.Top;
            this.kryptonHeaderGroup2.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Primary;
            this.kryptonHeaderGroup2.HeaderStyleSecondary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.kryptonHeaderGroup2.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup2.Location = new System.Drawing.Point(0, 220);
            this.kryptonHeaderGroup2.Name = "kryptonHeaderGroup2";
            this.kryptonHeaderGroup2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            // 
            // kryptonHeaderGroup2.Panel
            // 
            this.kryptonHeaderGroup2.Panel.Controls.Add(this.dgItems);
            this.kryptonHeaderGroup2.Size = new System.Drawing.Size(831, 365);
            this.kryptonHeaderGroup2.TabIndex = 1;
            this.kryptonHeaderGroup2.Text = "Shift Data";
            this.kryptonHeaderGroup2.ValuesPrimary.Description = "";
            this.kryptonHeaderGroup2.ValuesPrimary.Heading = "Shift Data";
            this.kryptonHeaderGroup2.ValuesPrimary.Image = global::DCI.HRMS.Properties.Resources.Calendar;
            this.kryptonHeaderGroup2.ValuesPrimary.ImageTransparentColor = System.Drawing.Color.White;
            this.kryptonHeaderGroup2.ValuesSecondary.Description = "";
            this.kryptonHeaderGroup2.ValuesSecondary.Heading = "Description";
            this.kryptonHeaderGroup2.ValuesSecondary.Image = null;
            // 
            // dgItems
            // 
            this.dgItems.AllowUserToAddRows = false;
            this.dgItems.AllowUserToDeleteRows = false;
            this.dgItems.AllowUserToResizeColumns = false;
            this.dgItems.AllowUserToResizeRows = false;
            this.dgItems.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
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
            this.dgItems.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgItems.Size = new System.Drawing.Size(827, 327);
            this.dgItems.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundSheet;
            this.dgItems.TabIndex = 0;
            this.dgItems.VirtualMode = true;
            this.dgItems.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgItems_CellDoubleClick);
            this.dgItems.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgItems_RowPostPaint);
            this.dgItems.SelectionChanged += new System.EventHandler(this.dgItems_SelectionChanged);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 220);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(831, 2);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // ucl_ActionControl1
            // 
            this.ucl_ActionControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucl_ActionControl1.Location = new System.Drawing.Point(0, 0);
            this.ucl_ActionControl1.Name = "ucl_ActionControl1";
            this.ucl_ActionControl1.Size = new System.Drawing.Size(831, 45);
            this.ucl_ActionControl1.TabIndex = 0;
            // 
            // FrmShiftMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(831, 585);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.kryptonHeaderGroup2);
            this.Controls.Add(this.kryptonHeaderGroup1);
            this.Controls.Add(this.ucl_ActionControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmShiftMaster";
            this.Text = "FrmCalendar";
            this.Load += new System.EventHandler(this.FrmCalendar_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCalendar_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            this.kryptonHeaderGroup1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2.Panel)).EndInit();
            this.kryptonHeaderGroup2.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup2)).EndInit();
            this.kryptonHeaderGroup2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DCI.HRMS.Attendance.Controls.MonthShift_Control month_Shift_Control1;
        private DCI.HRMS.Controls.Ucl_ActionControl ucl_ActionControl1;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup2;
        private System.Windows.Forms.Splitter splitter1;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgItems;
    }
}