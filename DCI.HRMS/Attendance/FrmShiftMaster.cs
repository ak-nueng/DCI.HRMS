using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Base;
using DCI.HRMS.Util;
using DCI.HRMS.Service;
using DCI.HRMS.Controls;
using DCI.HRMS.Model;
using DCI.Security.Service;
using DCI.HRMS.Model.Attendance;
using DCI.HRMS.Common;


namespace DCI.HRMS.Attendance
{
    public partial class FrmShiftMaster : Form, IFormParent, IFormPermission
    {
        #region Field
        private MonthShiftInfo monthSh = new MonthShiftInfo();
        private ShiftService shserv = ShiftService.Instance();
        private ArrayList yearSh;
        private StatusManager stsMng = new StatusManager();

        private readonly string[] colName = new string[] { "YearMonth", "ShiftGroup", "ShiftData", "AddBy", "AddDate" };
        private readonly string[] propName = new string[] { "YearMonth", "GroupStatus", "ShiftData", "CreateBy", "CreateDateTime" };

        private readonly int[] width = new int[] { 80, 80, 300, 100, 120 };
        # endregion
        # region Contructor
        public FrmShiftMaster()
        {
            stsMng.Status="Loading  FrmShiftMaster";
            InitializeComponent();
            this.dgItems.AutoGenerateColumns = false;
            AddGridViewColumns();
        }

        #endregion
        # region Form Event
        private void FrmCalendar_Load(object sender, EventArgs e)
        {
            ucl_ActionControl1.Owner = this;
            ucl_ActionControl1.CurrentAction = FormActionType.AddNew;

            month_Shift_Control1.ShtType = ShiftService.Instance().GetShiftType();
            monthSh = (MonthShiftInfo) month_Shift_Control1.Information;

            FillDataGrid();
           stsMng.Status="Ready";


        }

        private void month_Shift_Control1_year_Changed()
        {
            this.AddNew();
            monthSh =(MonthShiftInfo) month_Shift_Control1.Information;
            RefreshData();
        }

   


        private void FrmCalendar_KeyDown(object sender, KeyEventArgs e)
        {
            ucl_ActionControl1.OnActionKeyDown(sender, e);
        }
        private void dgItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgItems_SelectionChanged(object sender, EventArgs e)
        {
            if (ucl_ActionControl1.CurrentAction != FormActionType.Search)
            {
                try
                {

                    MonthShiftInfo msh = (MonthShiftInfo)yearSh[dgItems.SelectedRows[0].Index];
                    month_Shift_Control1.Information = msh;

                    ucl_ActionControl1.CurrentAction = FormActionType.Save;

                }
                catch
                {
                }
            }
            else
            {
                dgItems.ClearSelection();
                ucl_ActionControl1.CurrentAction = FormActionType.None;
            }
        }
        private void dgItems_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyleDefault.ShowRowNumber(dgItems, e);
        }
        #endregion
        #region IForm Members

        public string GUID
        {
            get { return string.Empty; }
        }

        public object Information
        {
            get
            {

                monthSh = (MonthShiftInfo)month_Shift_Control1.Information;

                return monthSh;
            }

            set
            {
                monthSh = (MonthShiftInfo)value;
                month_Shift_Control1.Information = monthSh;

            }
        }

        public void AddNew()
        {
            dgItems.ClearSelection();
            ucl_ActionControl1.CurrentAction = FormActionType.AddNew;
            month_Shift_Control1.Focus();
        }

        public void Save()
        {
            monthSh = (MonthShiftInfo)month_Shift_Control1.Information;

            try
            {
                string msg = string.Empty;
                if (!CheckData())
                {
                    if (ucl_ActionControl1.CurrentAction == FormActionType.SaveAs)
                    {

                        shserv.New(monthSh);
                        msg = "เพิ่มข้อมูล ตารางกะ  เรียบร้อย";
                        RefreshData();
                        MessageBox.Show(this, msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }

                }
                else if (ucl_ActionControl1.CurrentAction == FormActionType.Save)
                {
                    shserv.Save(monthSh);
                    msg = "บันทึกข้อมูล ตารางกะ เรียบร้อย";
                    RefreshData();
                    MessageBox.Show(this, msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
                else
                {
                    msg = string.Format("มีข้อมูลข้อมูลตารางกะของเดือน:{0} กลุ่ม:{1} อยู่แล้ว ต้องการ update หรือไม่?", monthSh.YearMonth, monthSh.GroupStatus);
                    DialogResult result = MessageBox.Show(this, msg, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        shserv.Save(monthSh);
                        msg = "บันทึกข้อมูล ตารางกะ เรียบร้อย";
                        RefreshData();
                        MessageBox.Show(this, msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public void Delete()
        {

            monthSh = (MonthShiftInfo)month_Shift_Control1.Information;
            string msg = string.Format("คุณต้องการลบข้อมูล ตารางกะ เดือน: {0} กลุ่ม: {1} ใช่หรือไม่?", monthSh.YearMonth, monthSh.GroupStatus);
            DialogResult result = MessageBox.Show(this, msg, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    shserv.Delete(monthSh);

                    ucl_ActionControl1.CurrentAction = FormActionType.AddNew;
                    RefreshData();
                    MessageBox.Show(this, string.Format("ลบข้อมูล ตารางกะ เดือน: {0} กลุ่ม: {1} เรียบร้อยแล้ว", monthSh.YearMonth, monthSh.GroupStatus), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void Search()
        {
        }

        public void Export()
        {
        }

        public void Print()
        {
        }

        public void Open()
        {
        }

        public void Clear()
        {
        }

        public void RefreshData()
        {

            FillDataGrid();
        }

        public void Exit()
        {
            this.Close();
        }

        #endregion
        #region IFormPermission Members

        public DCI.Security.Model.PermissionInfo Permission
        {
            set
            {
                ucl_ActionControl1.Permission = value;
            }
        }

        #endregion
        #region Method

        private void AddGridViewColumns()
        {
            this.dgItems.Columns.Clear();
            DataGridViewTextBoxColumn[] columns = new DataGridViewTextBoxColumn[colName.Length];
            for (int index = 0; index < columns.Length; index++)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();

                column.Name = colName[index];
                column.DataPropertyName = propName[index];
                column.ReadOnly = true;
                column.Width = width[index];

                columns[index] = column;
                dgItems.Columns.Add(columns[index]);
            }
            dgItems.ClearSelection();
        }
        private void FillDataGrid()
        {
            
            ucl_ActionControl1.CurrentAction = FormActionType.Search;
            yearSh = shserv.GetShiftByGrp(monthSh.GroupStatus,int.Parse( monthSh.YearMonth.Substring(0,4)));
            dgItems.DataSource = yearSh;
            ucl_ActionControl1.CurrentAction = FormActionType.AddNew;

        }

        private bool CheckData()
        {
           
            return shserv.CheckExited(monthSh);
        }
        #endregion


    }
}