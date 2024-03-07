using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Common;
using DCI.HRMS.Base;
using DCI.Security.Model;
using DCI.HRMS.Model.Attendance;
using System.Collections;
using DCI.HRMS.Util;
using DCI.HRMS.Service;
using DCI.HRMS.Model.Personal;
using DCI.HRMS.Service.SubContract;

namespace DCI.HRMS.Attendance
{
    public partial class FrmBusinessTrip : BaseForm, IFormParent, IFormPermission
    {
        private BusinesstripInfo tnrq = new BusinesstripInfo();
        private ArrayList gvData = new ArrayList();
        private ArrayList addData;
        private ArrayList searchData;    
        private FormAction formAct = FormAction.New;
        private readonly string[] colName = new string[] { "Code", "From", "To", "TimeFrom", "TimeTo","Note", "CreateBy", "CreateDate", "LastUpdateBy", "LastUpdateDateTime" };
        private readonly string[] propName = new string[] { "EmpCode", "FDate", "TDate", "TFrom", "TTo","Note", "CreateBy", "CreateDateTime", "LastUpdateBy", "LastUpdateDateTime" };
        private readonly int[] width = new int[] { 80, 80, 80, 80, 80, 150, 100, 100, 100, 100 };
        
        private BusinessTripService busTripSvr = BusinessTripService.Instance();
        private SubContractBusinessTripService busTripSubSvr = SubContractBusinessTripService.Instance();
        private EmployeeService empSvr = EmployeeService.Instance();
        private SubContractService empSubSvr = SubContractService.Instance();
        
        


        ApplicationManager appMgr = ApplicationManager.Instance();
        public FrmBusinessTrip()
        {
            InitializeComponent();
        }

        private void FrmBusinessTrip_Load(object sender, EventArgs e)
        {
            Open();
            ucl_ActionControl1.Owner = this;
        }

        #region IForm Members
        public string GUID
        {
            get { return string.Empty; }
        }

        public object Information
        {
            get
            {
               




                return tnrq;
            }
            set
            {
                tnrq = (BusinesstripInfo)value;
            }
        }

        public void AddNew()
        {
           
        }

        public void Save()
        {



            if (ucl_ActionControl1.CurrentAction == FormActionType.Save)
            {


                if (formAct == FormAction.Save)
                {
                    BusinesstripInfo item = (BusinesstripInfo)BusinessTrip_Control1.Information;
                    if (item != null)
                    {
                        if(item.EmpCode.StartsWith("I") ){
                            item.LastUpdateBy = appMgr.UserAccount.AccountId;
                            try
                            {                                
                                busTripSubSvr.UpdateBusinesstripInfo(item);
                                RefreshData();
                                txtCode.Clear();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("ไม่สามารถบันทึกข้อมูล ได้เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }                        
                        }else{
                            item.LastUpdateBy = appMgr.UserAccount.AccountId;
                            try
                            {

                                busTripSvr.UpdateBusinesstripInfo(item);
                                RefreshData();
                                txtCode.Clear();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("ไม่สามารถบันทึกข้อมูล ได้เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        
                        }
                        
                    }

                }

            }
            else if (ucl_ActionControl1.CurrentAction == FormActionType.SaveAs)
            {
                if (formAct == FormAction.New)
                {


                    if (txtNote.Text == "")
                    {
                        MessageBox.Show("กรุณาป้อนข้อมูล", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtNote.Focus();
                        return;
                    }
                    else if (txtCode.Text == "")
                    {
                        MessageBox.Show("กรุณาป้อนรหัสพนักงาน", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCode.Focus();
                        return;

                    }
                    else
                    {
                        if (txtCode.Text.StartsWith("I"))
                        {
                            if (empSubSvr.Find(txtCode.Text) == null){
                            MessageBox.Show("ไม่พบข้อมูลพนักงาน รหัส: " + txtCode.Text, "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtCode.Clear();
                            txtCode.Focus();
                            return;
                            }
                        }else{
                            if (empSvr.Find(txtCode.Text) == null){
                            MessageBox.Show("ไม่พบข้อมูลพนักงาน รหัส: " + txtCode.Text, "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtCode.Clear();
                            txtCode.Focus();
                            return;
                            }   
                        }
                        
                    }

                    BusinesstripInfo item = new BusinesstripInfo();
                    item.EmpCode = txtCode.Text;
                    item.FDate = dpkRqDate.Value.Date;
                    item.TDate = dpkRqDateTo.Value.Date;
                    item.TFrom = txtFrom.Text;
                    item.TTo = txtTo.Text;
                    item.Note = txtNote.Text;


                    item.CreateBy = appMgr.UserAccount.AccountId;


                    if (item.EmpCode.StartsWith("I"))
                    {
                        if (busTripSubSvr.GetBusinessTripInfo(item.EmpCode, item.TDate, item.TDate) != null)
                        {
                            MessageBox.Show("ไม่สามารถบันทึกข้อมูล ได้เนื่องจาก มีข้อมูลอยู่แล้ว", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtCode.Clear();
                            txtCode.Focus();
                            return;
                        }
                        try
                        {
                            busTripSubSvr.SaveBusinesstripInfo(item);
                            addData.Add(item);
                            gvData = addData;
                            FillDataGrid();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ไม่สามารถบันทึกข้อมูล ได้เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }


                    }else{
                        if (busTripSvr.GetBusinessTripInfo(item.EmpCode, item.TDate, item.TDate) != null)
                        {
                            MessageBox.Show("ไม่สามารถบันทึกข้อมูล ได้เนื่องจาก มีข้อมูลอยู่แล้ว", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtCode.Clear();
                            txtCode.Focus();
                            return;
                        }
                        try
                        {
                            busTripSvr.SaveBusinesstripInfo(item);
                            addData.Add(item);
                            gvData = addData;
                            FillDataGrid();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ไม่สามารถบันทึกข้อมูล ได้เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    
                    txtCode.Clear();
                }
            }

        }

        public void Delete()
        {

            if (formAct == FormAction.Save)
            {
                try
                {
                    if (MessageBox.Show("ต้องการลบข้อมูลใช่หรือไม่?", "Conferm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow GVRW in dgItems.SelectedRows)
                        {
                            BusinesstripInfo item = (BusinesstripInfo)gvData[GVRW.Index];
                            if (item.EmpCode.StartsWith("I"))
                            {
                                busTripSubSvr.DeleteBusinesstripInfo(item);
                            }
                            else {
                                busTripSvr.DeleteBusinesstripInfo(item);
                            }
                            

                        }
                        RefreshData();
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("ไม่สามารถบันทึกข้อมูล ได้เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        public void Search()
        {

            try
            {
                if (txtCodeSerarch.Text.StartsWith("I"))
                {
                    searchData = busTripSubSvr.GetBusinessTripInfo(txtCodeSerarch.Text, dtpDatesarchFrom.Value.Date, dtpDatesarchTo.Value.Date);
                }
                else {
                    searchData = busTripSvr.GetBusinessTripInfo(txtCodeSerarch.Text, dtpDatesarchFrom.Value.Date, dtpDatesarchTo.Value.Date);
                }
                
                gvData = searchData;
                FillDataGrid();
            }
            catch 
            {
                
                throw;
            }
            
        }

        public void Export()
        {
          
        }

        public void Print()
        {
           
        }

        public void Open()
        {
            searchData = new ArrayList();
            addData = new ArrayList();
            addData.Add(new BusinesstripInfo());
            gvData = addData;
            FillDataGrid();
            AddGridViewColumns();
            addData.Clear();

            kryptonHeaderGroup1_Click(kryptonHeaderGroup2, new EventArgs());
            dpkRqDate.Focus();
        }

        public void Clear()
        {
         
        }

        public void RefreshData()
        {
            try
            {
                BusinesstripInfo tmp = tnrq;
                this.Search();
                int i = 0;
                foreach (BusinesstripInfo item in gvData)
                {
                    if (item.EmpCode == tmp.EmpCode && item.FDate == tmp.FDate && item.TDate == item.TDate)
                    {

                        dgItems.CurrentCell = dgItems.Rows[i].Cells[0];
                        break;
                    }
                    i++;
                }

            }
            catch 
            { }

        }

        public void Exit()
        {
            this.Close();
        }

        #endregion

        #region IFormPermission Members

        public PermissionInfo Permission
        {

            set { ucl_ActionControl1.Permission = value; }
        }

        #endregion

        private void AddGridViewColumns()
        {
            this.dgItems.Columns.Clear();
            dgItems.AutoGenerateColumns = false;
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
            dgItems.DataBindings.Clear();
            dgItems.DataSource = null;
            /*gvData = new ArrayList();
            if (formAct == FormAction.New)
            {
                gvData = addData;
               // dgItems.CurrentCell = dgItems.FirstDisplayedCell;
            }
            else if (formAct == FormAction.Save)
            {
                gvData = searchData;
                dgItems.CurrentCell = dgItems.FirstDisplayedCell;
            }*/
            dgItems.DataSource = gvData;
            this.Update();
        }
        private void dgItems_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyleDefault.ShowRowNumber(dgItems, e);
        }
        private void dpkRqDate_ValueChanged(object sender, EventArgs e)
        {
            if (sender == dpkRqDate)
            {
                if (dpkRqDate.Value > dpkRqDateTo.Value)
                    dpkRqDateTo.Value = dpkRqDate.Value.Date;
            }
            else
            {
                if (dpkRqDate.Value > dpkRqDateTo.Value)
                    dpkRqDate.Value = dpkRqDateTo.Value.Date;
            }


            if (dpkRqDateTo.Value.Date == dpkRqDate.Value.Date)

            {
             txtTo.ReadOnly = false;
                txtFrom.ReadOnly = false;
                
            }
            else
            {     txtFrom.Text = "";
                txtTo.Text = "";
                txtTo.ReadOnly = true;
                txtFrom.ReadOnly = true;
              

            }
        }
        private void kryptonHeaderGroup1_Click(object sender, EventArgs e)
        {
            ucl_ActionControl1.CurrentAction = FormActionType.None;
            if (sender == kryptonHeaderGroup1)
            {
                kryptonHeaderGroup2.Collapsed = true;
                kryptonHeaderGroup1.Collapsed = false;
                ucl_ActionControl1.CurrentAction = FormActionType.None;
              formAct = FormAction.Save;
                gvData = searchData;
                FillDataGrid();

            }
            else
            {
                ucl_ActionControl1.CurrentAction = FormActionType.AddNew;
           
                kryptonHeaderGroup1.Collapsed = true;
                kryptonHeaderGroup2.Collapsed = false;
                gvData = addData;    
                formAct = FormAction.New;
                FillDataGrid();
            }

        }

        private void dpkRqDate_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPressManager.Enter(e);
        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode  == Keys.Enter)
            {
                this.Save();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.Search();
        }

        private void dgItems_SelectionChanged(object sender, EventArgs e)
        {
            if (formAct== FormAction.Save)
            {
                if (dgItems.SelectedRows.Count != 0)
                {
                    tnrq = (BusinesstripInfo)gvData[dgItems.SelectedRows[0].Index];
                    BusinessTrip_Control1.Information = tnrq;

                    if(tnrq.EmpCode.StartsWith("I")){
                        empData_Control1.Information = empSubSvr.Find(tnrq.EmpCode);
                    }else{
                        empData_Control1.Information = empSvr.Find(tnrq.EmpCode);
                    }
                    
                    ucl_ActionControl1.CurrentAction = FormActionType.Save;
                }
                else
                {
                    ucl_ActionControl1.CurrentAction = FormActionType.None;
                } 
            }
            else
            {
                if (dgItems.SelectedRows.Count != 0)
                {
                    if (tnrq != null)
                    {
                        tnrq = (BusinesstripInfo)gvData[dgItems.SelectedRows[0].Index];

                        if (tnrq.EmpCode != null)
                        {
                            if (tnrq.EmpCode.StartsWith("I"))
                            {
                                empData_Control1.Information = empSubSvr.Find(tnrq.EmpCode);
                            }
                            else
                            {
                                empData_Control1.Information = empSvr.Find(tnrq.EmpCode);
                            }
                        }
                    }
                }
            }
        }

        private void BusinessTrip_Control1_enterCode()
        {
            this.Save();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            addData.Clear();
            gvData = addData;
            FillDataGrid();
        }

    }

}