using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Base;
using DCI.HRMS.Util;
using DCI.HRMS.Model.Personal;
using DCI.HRMS.Service;
using DCI.HRMS.Model.Attendance;
using DCI.HRMS.Common;
using DCI.HRMS.Model;
using System.Xml.Serialization;
using System.Data.SqlClient;
using DCI.HRMS.Service.SubContract;
using DCI.HRMS.Service.Trainee;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace DCI.HRMS.Attendance
{
    public partial class FrmPenalty : Form, IFormParent, IFormPermission
    {
        #region Field
        private readonly string[] colName = new string[] { "PenaltyId", "EmpCode", "Reason", "Penalty Type","Penalty Issue","Penalty From","Penalty To","Do From","Do To", "AddBy", "AddDate", "UpdateBy", "UpdateDate" };
        private readonly string[] propName = new string[] { "PenaltyId", "EmpCode", "WDescription", "PenaltyType","PenaltyDate" ,"PenaltyFrom","PenaltyTo","WFrom","WTo","CreateBy", "CreateDateTime", "LastUpdateBy", "LastUpDateDateTime" };
        private readonly int[] width = new int[] { 90, 70, 200, 100, 100, 100, 100, 100, 100,100,100, 100, 100 };
        private ArrayList gvData;
        private ArrayList gvDataTemp;
        private ArrayList addData = new ArrayList();
        private ArrayList searchData = new ArrayList();
        private StatusManager stsMng = new StatusManager();
        private FormAction formAct = FormAction.New;
        private PenaltyService penSvr = PenaltyService.Instanse();
        private ApplicationManager appMgr = ApplicationManager.Instance();


        SqlConnectDB oSqlHRM = new SqlConnectDB("dbHRM");

        #endregion
        public FrmPenalty()
        {
            InitializeComponent();
        }

        private void FrmPenalty_Load(object sender, EventArgs e)
        {
            ucl_ActionControl1.Owner = this;
         
            this.Open();
            AddGridViewColumns();
          //  ucl_ActionControl1.CurrentAction = FormActionType.AddNew;
            stsMng.Status = "Ready";
        
        }
        #region IFormPermission Members

        public DCI.Security.Model.PermissionInfo Permission
        {
            set
            {
                ucl_ActionControl1.Permission = value;
            }
        }

        #endregion

        #region IForm Members

        public string GUID
        {
            get { throw new NotImplementedException(); }
        }

        public object Information
        {
            get
            {
                if (formAct == FormAction.New)
                {
                    PenaltyInfo item = new PenaltyInfo();
                    item.PenaltyId = penaltyId.Text;
                    item.EmpCode = txtCode.Text;
                    item.WDescription = txtReason.Text;
                    item.WFrom = dtpWfrom.Value.Date;
                    item.WTo = dtpWto.Value.Date;
                    item.WTotal = int.Parse(txtWtotal.Text);
                    item.PenaltyType = cboPenType.SelectedValue.ToString();
                    item.PenaltyDate = dtpPenalty.Value.Date;
                    item.PenaltyFrom = dtpPenFrom.Value.Date;
                    item.PenaltyTo = dtpPenTo.Value.Date;
                    item.PenaltyTotal = int.Parse(txtPenTotal.Text);

                    return item;
                }
                else
                {
                    return penalty_Control1.Information;
                }

            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void AddNew()
        {
            this.Clear();
            ucl_ActionControl1.CurrentAction = FormActionType.AddNew;
            ClearDataGride();
            kryptonHeaderGroup1_Click(kryptonHeaderGroup1, new EventArgs());
        }

        public void Save()
        {
            if (formAct == FormAction.New)
            {
                if (ucl_ActionControl1.CurrentAction == FormActionType.SaveAs)
                {
                    if (ucl_ActionControl1.Permission.AllowAddNew)
                    {

                        try
                        {
                            PenaltyInfo item = (PenaltyInfo)this.Information;

                            item.CreateBy = appMgr.UserAccount.AccountId;
                            penSvr.SavePenalty(item);
                            this.Search();
                            if (dgItems.Rows.Count != 0)
                            {
                                dgItems.CurrentCell = dgItems.Rows[dgItems.Rows.Count - 1].Cells[0];
                            }
                            this.Clear();
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("ไม่สามารถเพิ่มข้อมูลได้เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }

                    }
                    else
                    {
                        MessageBox.Show("ไม่สามารถเพิ่มข้อมูลได้เนื่องจาก คุณไม่ได้รับสิทธิ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else if (ucl_ActionControl1.CurrentAction == FormActionType.Save)
                {


                    if (ucl_ActionControl1.Permission.AllowEdit)
                    {

                        try
                        {
                            PenaltyInfo item = penalty_Control2.Information;

                            item.LastUpdateBy = appMgr.UserAccount.AccountId;
                            penSvr.UpdatePenalty(item);
                            txtCode.Text = item.EmpCode;
                            this.Search();
                            FindRecord(item.PenaltyId);
                            this.Clear();
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("ไม่สามารถแก้ไขข้อมูลได้เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }


                    }
                    else
                    {

                        MessageBox.Show("ไม่สามารถแก้ไขข้อมูลได้เนื่องจาก คุณไม่ได้รับสิทธิ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    }
                }
            }
            else
            {
                if (ucl_ActionControl1.Permission.AllowEdit)
                {

                    try
                    {
                        PenaltyInfo item = penalty_Control1.Information;

                        item.LastUpdateBy = appMgr.UserAccount.AccountId;
                        penSvr.UpdatePenalty(item);
                        //txtCode.Text = item.EmpCode;
                        this.Search();
                        FindRecord(item.PenaltyId);
                        this.Clear();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("ไม่สามารถแก้ไขข้อมูลได้เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }


                }
                else
                {
                    MessageBox.Show("ไม่สามารถแก้ไขข้อมูลได้เนื่องจาก คุณไม่ได้รับสิทธิ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }

        }

        public void Delete()
        {
            //if (formAct == FormAction.New)
            //{
            if (ucl_ActionControl1.Permission.AllowDelete)
            {
                if (ucl_ActionControl1.CurrentAction == FormActionType.Save)
                {
                    if (MessageBox.Show("คุณต้องการลบข้อมูลใช่หรือไม่?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {

                            foreach (DataGridViewRow delItem in dgItems.SelectedRows)
                            {
                                PenaltyInfo item = delItem.DataBoundItem as PenaltyInfo;

                                item.LastUpdateBy = appMgr.UserAccount.AccountId;
                                penSvr.DelatePenalty(item);
                                txtCode.Text = item.EmpCode;
                            }

                            this.Search();
                            this.Clear();

                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("ไม่สามารถลบข้อมูลได้เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("ไม่สามารถลบข้อมูลได้เนื่องจาก คุณไม่ได้รับสิทธิ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            //}
            //else
            //{

            //}
        }

        public void Search()
        {
            if (formAct == FormAction.New)
            {
                    addData = penSvr.GetPenaltyByCode(txtCode.Text);
                gvData = addData;
                FillDataGrid();
            }
            else
            {
                string code = txtSCode.Text == "" ? "%" : txtSCode.Text;
                string type = cmbType.SelectedValue.ToString();
                    searchData = penSvr.SelectPenalty(code, type, dpkRqDate.Value.Date, dpkDateTo.Value.Date);
                gvData = searchData;
                FillDataGrid();
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
            kryptonHeaderGroup1_Click(kryptonHeaderGroup1, new EventArgs());
            ArrayList tmrqType = penSvr.SelectPenaltyType();



            ArrayList penTypeS = new ArrayList();
            BasicInfo allPen = new BasicInfo();
            allPen.Code = "%";
            allPen.Name = "All";
            penTypeS.Add(allPen);
            foreach (BasicInfo item in tmrqType)
            {
                penTypeS.Add(item);
            }
            cmbType.DisplayMember = "NameForSearching";
            cmbType.ValueMember = "Code";
            cmbType.DataSource = penTypeS;
            cmbType.SelectedIndex = 0;


            cboPenType.DisplayMember = "NameForSearching";
            cboPenType.ValueMember = "Code";
            cboPenType.DataSource = tmrqType;
            cboPenType.SelectedIndex = 0;
            penalty_Control1.Open();
            penalty_Control2.Open();
            this.Clear();
        }

        public void Clear()
        {

            txtCode.Text = "";
            txtReason.Text = "";
            cboPenType.SelectedValue = "VERB";
            txtCode.Focus();
    
        }

        public void RefreshData()
        {
            string id = "";
            if (dgItems.SelectedRows.Count!=0)
            {
                PenaltyInfo item = dgItems.SelectedRows[0].DataBoundItem as PenaltyInfo;
                id = item.PenaltyId;
                if (formAct== FormAction.New)
                {
                    txtCode.Text = item.EmpCode;
                }
            }
            this.Search();
            FindRecord(id);
        }

        public void Exit()
        {
            this.Close();
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
            dgItems.DataSource = gvData;
            // dgItems.CurrentCell = null;
            //dgItems.Refresh();
            // dgItems.Focus();
            this.Update();

        }
        private void FindRecord(string id)
        {
            try
            {
                int found = 0;
                for (int i = 0; i < gvData.Count; i++)
                {
                    found = i;
                    PenaltyInfo item = (PenaltyInfo)gvData[i];
                    if (item.PenaltyId == id)
                    {
                        dgItems.CurrentCell = dgItems[0, found];
                        break;
                    }
                }
            }
            catch
            {
            }

        }
        private void ClearDataGride()
        {
            gvData = new ArrayList();

            dgItems.DataSource = gvData;
    

        }

        private void kryptonHeaderGroup1_Click(object sender, EventArgs e)
        {
            ucl_ActionControl1.CurrentAction = FormActionType.None;
            if (sender == kryptonHeaderGroup1)
            {
                kryptonHeaderGroup3.Collapsed = true;
                kryptonHeaderGroup1.Collapsed = false;
                ucl_ActionControl1.CurrentAction = FormActionType.None;
                formAct = FormAction.New;
                gvData = addData;
                FillDataGrid();

            }
            else
            {
                formAct = FormAction.Save;
                kryptonHeaderGroup1.Collapsed = true;
                kryptonHeaderGroup3.Collapsed = false;
                gvData = searchData;
                FillDataGrid();
                txtSCode.Focus();
            }
        }

        private void dtpWfrom_ValueChanged(object sender, EventArgs e)
        {
            dtpWto.Value = dtpWfrom.Value;
            TimeSpan tc = dtpWto.Value.Date - dtpWfrom.Value.Date;
            txtWtotal.Text = (tc.TotalDays + 1).ToString();

        }

        private void dtpWto_ValueChanged(object sender, EventArgs e)
        {
            
            TimeSpan tc = dtpWto.Value.Date - dtpWfrom.Value.Date;
            txtWtotal.Text = (tc.TotalDays + 1).ToString();
        }

        private void dtpPenFrom_ValueChanged(object sender, EventArgs e)
        {
            dtpPenTo.Value = dtpPenFrom.Value;
            TimeSpan tc = dtpPenTo.Value.Date - dtpPenFrom.Value.Date;
            txtPenTotal.Text = (tc.TotalDays + 1).ToString();
        }

        private void dtpPenTo_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan tc = dtpPenTo.Value.Date - dtpPenFrom.Value.Date;
            txtPenTotal.Text = (tc.TotalDays + 1).ToString();
        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {

            KeyPressManager.Enter(e);
        }

        private void txtPenTotal_KeyDown(object sender, KeyEventArgs e)
        {
            this.Save();
        }

        private void txtCode_KeyDown_1(object sender, KeyEventArgs e)
        {
            txtOTLockEmpCode.Text = txtCode.Text;

            this.Cursor = Cursors.WaitCursor;
            if (e.KeyCode == Keys.Enter)
            {

                if (txtCode.Text != "")
                {
                    EmployeeInfo emp = null;

                    if (txtCode.Text.StartsWith("I"))
                    {
                        emp = SubContractService.Instance().Find(txtCode.Text);
                    } 
                    else if (txtCode.Text.StartsWith("7")) 
                    {
                        emp = TraineeService.Instance().Find(txtCode.Text);                        
                    }
                    else 
                    {
                        emp = EmployeeService.Instance().Find(txtCode.Text);
                    }


                    if (emp != null)
                    {
                        loadLockOT(); // load Locked OT data list


                        empData_Control1.Information = emp;
                        dgItems.ClearSelection();
                        this.Search();

                        penaltyId.Text = penSvr.LoadRecordKey();
                        KeyPressManager.Enter(e);
                        ucl_ActionControl1.CurrentAction = FormActionType.AddNew;




                    }
                    else
                    {
                        MessageBox.Show("ไม่พบข้อมูลพนักงาน", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtCode.Clear();
                        this.Clear();
                        dgvLockOT.Rows.Clear();
                        txtOTLockEmpCode.Clear();
                    }
                }
                else
                {
                    dgvLockOT.Rows.Clear();
                    txtOTLockEmpCode.Clear();
                }

            }
            this.Cursor = Cursors.Default;
        }

        private void dgItems_SelectionChanged(object sender, EventArgs e)
        {
            if (dgItems.SelectedRows.Count!=0)
            {
                if (formAct== FormAction.New)
                {
                    penalty_Control2.Information = dgItems.SelectedRows[0].DataBoundItem as PenaltyInfo;
                    ucl_ActionControl1.CurrentAction = FormActionType.Save;
                }
                else
                {
                    penalty_Control1.Information = dgItems.SelectedRows[0].DataBoundItem as PenaltyInfo;
                    ucl_ActionControl1.CurrentAction = FormActionType.Save;
                     EmployeeInfo emp = EmployeeService.Instance().Find(penalty_Control1.Information.EmpCode);
                     if (emp != null)
                     {


                         empData_Control1.Information = emp;
                     }
                }
            }
            else
            {
                penalty_Control2.Information = new PenaltyInfo();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.Search();
        }

        private void dgItems_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyleDefault.ShowRowNumber(dgItems, e);
        }

        private void btnLockOT_Click(object sender, EventArgs e)
        {
            string strInstr = @"INSERT INTO [dbHRM].[dbo].[HR_LockReqOT] ([Code],[StartDate],[EndDate],[InsertBy],[InsertDate],[UpdateBy],[UpdateDate]) 
                                    VALUES (@Code,@StartDate,@EndDate,@InsertBy,@InsertDate,@UpdateBy,@UpdateDate) ";
            SqlCommand cmdInstr = new SqlCommand();
            cmdInstr.CommandText = strInstr;
            cmdInstr.Parameters.Add(new SqlParameter("@CODE", txtOTLockEmpCode.Text));
            cmdInstr.Parameters.Add(new SqlParameter("@StartDate", dtpLockOTST.Value.Date.ToString("yyyy-MM-dd")));
            cmdInstr.Parameters.Add(new SqlParameter("@EndDate", dtpLockOTEN.Value.Date.ToString("yyyy-MM-dd")));
            cmdInstr.Parameters.Add(new SqlParameter("@InsertBy", appMgr.UserAccount.AccountId));
            cmdInstr.Parameters.Add(new SqlParameter("@InsertDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            cmdInstr.Parameters.Add(new SqlParameter("@UpdateBy", appMgr.UserAccount.AccountId));
            cmdInstr.Parameters.Add(new SqlParameter("@UpdateDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            oSqlHRM.ExecuteCommand(cmdInstr);

            //**** Reload LockOT Data ****
            loadLockOT();
        }


        public void loadLockOT()
        {
            DataTable dtLockOT = new DataTable();
            string strLockOT = @"SELECT * FROM HR_LockReqOT WHERE CODE=@CODE ORDER BY StartDate DESC ";
            SqlCommand cmdLockOT = new SqlCommand();
            cmdLockOT.CommandText = strLockOT;
            cmdLockOT.Parameters.Add(new SqlParameter("@CODE", txtOTLockEmpCode.Text));
            dtLockOT = oSqlHRM.Query(cmdLockOT);

            dgvLockOT.Rows.Clear();
            if(dtLockOT.Rows.Count > 0)
            {
                foreach(DataRow drLockOT in dtLockOT.Rows)
                {
                    DateTime _st = new DateTime(1900, 1, 1);
                    DateTime _en = new DateTime(1900, 1, 1);
                    DateTime _dt = new DateTime(1900, 1, 1);
                    try { _st = Convert.ToDateTime(drLockOT["StartDate"].ToString()); } catch { }
                    try { _en = Convert.ToDateTime(drLockOT["EndDate"].ToString()); } catch { }
                    try { _dt = Convert.ToDateTime(drLockOT["InsertDate"].ToString()); } catch { }
                    
                    dgvLockOT.Rows.Add(drLockOT["Code"].ToString(), _st.ToString("dd/MMM/yyyy"), _en.ToString("dd/MMM/yyyy"), drLockOT["InsertBy"].ToString(), _dt.ToString("dd/MMM/yyyy")); 
                }
            }
        }

        private void btnLockOTDel_Click(object sender, EventArgs e)
        {
            if(dgvLockOT.SelectedRows.Count > 0)
            {
                string _code = dgvLockOT.SelectedRows[0].Cells["ColLOT"].Value.ToString();
                string _StrFrom = dgvLockOT.SelectedRows[0].Cells["ColLOTFROM"].Value.ToString();
                DateTime _from = new DateTime(1900, 1, 1);
                try { _from = Convert.ToDateTime(_StrFrom); } catch { }

                string strLockOTDel = @"DELETE FROM HR_LockReqOT WHERE CODE=@CODE AND StartDate=@StartDate ";
                SqlCommand cmdLockOTDel = new SqlCommand();
                cmdLockOTDel.CommandText = strLockOTDel;
                cmdLockOTDel.Parameters.Add(new SqlParameter("@CODE", _code));
                cmdLockOTDel.Parameters.Add(new SqlParameter("@StartDate", _from.ToString("yyyy-MM-dd")) );
                oSqlHRM.ExecuteCommand(cmdLockOTDel);


                //**** Reload LockOT Data ****
                loadLockOT();

            }
        }
    }
}
