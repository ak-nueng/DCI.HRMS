using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Common;
using DCI.HRMS.Base;
using DCI.HRMS.Service;
using System.Collections;
using DCI.HRMS.Model.Personal;
using DCI.HRMS.Util;
using Oracle.ManagedDataAccess.Client;

namespace DCI.HRMS.Personal
{
    public partial class FrmEmpCodeTransfer : BaseForm, IFormParent, IFormPermission
    {
        private EmployeeService empSvr = EmployeeService.Instance();
        private OtService otSvr = OtService.Instance();
        private EmployeeLeaveService lvSvr = EmployeeLeaveService.Instance();
        private PropertyBorrowService prtSvr = PropertyBorrowService.Instace();
        private ShiftService shSvr = ShiftService.Instance();
        private TimeCardService tmSvr = TimeCardService.Instance();


        private readonly string[] colName = new string[] { "OldCode", "NewCode", "TransferDate", "TransferStatus", "AddBy", "AddDate", "UpdateBy", "UpdateDate" };
        private readonly string[] propName = new string[] { "OldCode", "NewCode", "TransferDate", "TransferStatus", "CreateBy", "CreateDateTime", "LastUpdateBy", "LastUpDateDateTime" };
        private readonly int[] width = new int[] { 100, 100, 100, 100, 100, 120, 100, 120 };
        private ArrayList addData = new ArrayList();
        private ArrayList searchData = new ArrayList();
        private ArrayList gvData = new ArrayList();
        private StatusManager stMgr = new StatusManager();
        DataTable dtData = new DataTable();

        ClsOraConnectDB oOraDCI = new ClsOraConnectDB("DCI");
        ClsOraConnectDB oOraSUB = new ClsOraConnectDB("DCISUB");
        ClsOraConnectDB oOraTRN = new ClsOraConnectDB("DCITRN");

        public FrmEmpCodeTransfer()
        {
            InitializeComponent();
        }

        // convert ArrayList to DataTable
        private void pushData(ArrayList arry) {
            
            try
            {
                DataTable dtTemp = new DataTable();
                dtTemp.Columns.Add("TransferDate", typeof(DateTime));
                dtTemp.Columns.Add("OldCode", typeof(string));
                dtTemp.Columns.Add("NewCode", typeof(string));
                dtTemp.Columns.Add("TransferStatus", typeof(string));
                dtTemp.Columns.Add("CreateBy", typeof(string));
                dtTemp.Columns.Add("CreateDateTime", typeof(DateTime));
                dtTemp.Columns.Add("LastUpdateBy", typeof(string));
                dtTemp.Columns.Add("LastUpDateDateTime", typeof(DateTime));
                
                if (arry.Count > 0)
                {
                    foreach (EmployeeCodeTransferInfo item in arry)
                    {
                        DataRow dr = dtTemp.NewRow();
                        dr["TransferDate"] = item.TransferDate;
                        dr["OldCode"] = item.OldCode;
                        dr["NewCode"] = item.NewCode;
                        dr["TransferStatus"] = item.TransferStatus;
                        dr["CreateBy"] = item.CreateBy;
                        dr["CreateDateTime"] = item.CreateDateTime;
                        dr["LastUpdateBy"] = item.LastUpdateBy;
                        dr["LastUpDateDateTime"] = item.LastUpdateDateTime;
                        dtTemp.Rows.Add(dr);
                    }

                }
                else
                {
                    DataRow dr = dtTemp.NewRow();
                    dr["TransferDate"] = "";
                    dr["OldCode"] = "";
                    dr["NewCode"] = "";
                    dr["TransferStatus"] = "";
                    dr["CreateBy"] = "";
                    dr["CreateDateTime"] = "";
                    dr["LastUpdateBy"] = "";
                    dr["LastUpDateDateTime"] = "";
                    dtTemp.Rows.Add(dr);
                }

                dtData.Clear();
                dtData = dtTemp;


            }catch{}
        
        }

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
            pushData(gvData);

            
            dgItems.DataBindings.Clear();
            dgItems.DataSource = null;
            dgItems.DataSource = dtData;

            //dgItems.CurrentCell = null;
            //dgItems.Refresh();
            

            this.Update();

        }
        private void TransferCode(EmployeeCodeTransferInfo item)
        {
            EmployeeDataInfo oldInfo = empSvr.GetEmployeeData(item.OldCode);
            EmployeeDataInfo newInfo = oldInfo;

            oldInfo.ResignDate = item.TransferDate;
            newInfo.Code = item.NewCode;
            empSvr.UpdateEmployeeInfo(oldInfo);
            empSvr.SaveEmployeeInfo(newInfo);

            ArrayList otLst = otSvr.GetOTRequestSinceDate(item.OldCode, item.TransferDate);
            ArrayList lvLs = lvSvr.GetAllLeave(item.OldCode, oldInfo.JoinDate, DateTime.MaxValue, "%");
            ArrayList tcLs = tmSvr.GetTimeCardCodesDates(item.OldCode, item.TransferDate, DateTime.MaxValue);
            ArrayList tmLs = tmSvr.GetTimeManual(item.OldCode, item.TransferDate, DateTime.MaxValue, "%");



        }


        private void FrmEmpCodeTransfer_Load(object sender, EventArgs e)
        {

            ucl_ActionControl1.Owner = this;
            this.Open();
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
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void AddNew()
        {

        }

        public void Save()
        {
            if (ucl_ActionControl1.CurrentAction == FormActionType.SaveAs)
            {
                if (ucl_ActionControl1.Permission.AllowAddNew)
                {
                    try
                    {
                        EmployeeCodeTransferInfo item = new EmployeeCodeTransferInfo();
                        item.OldCode = txtOldCode.Text;
                        item.NewCode = txtNewCode.Text;
                        item.TransferDate = dtpTranDate.Value.Date;
                        item.CreateBy = ApplicationManager.Instance().UserAccount.AccountId;
                        item.CreateDateTime = DateTime.Now;
                        empSvr.SaveEmployeeCodetransfer(item);

                        addData.Add(item);                        
                        gvData = addData;
                        FillDataGrid();
                        txtNewCode.Clear();
                        txtOldCode.Clear();

                    }
                    catch (Exception EX)
                    {

                        throw EX;
                    }
                }
                else
                {

                }
            }
            else if (ucl_ActionControl1.CurrentAction == FormActionType.Save)
            {
                if (ucl_ActionControl1.Permission.AllowEdit)
                {

                    try
                    {
                        EmployeeCodeTransferInfo item = empTransfer_Control1.Information;

                        item.LastUpdateBy = ApplicationManager.Instance().UserAccount.AccountId;

                        empSvr.UpdateEmployeeCodetransfer(item);

                    }
                    catch (Exception EX)
                    {

                        throw EX;
                    }
                }
                else
                {

                }
            }
        }

        public void Delete()
        {
            if (ucl_ActionControl1.CurrentAction == FormActionType.Save)
            {
                if (ucl_ActionControl1.Permission.AllowDelete)
                {

                    EmployeeCodeTransferInfo item = empTransfer_Control1.Information;
                    empSvr.DeleteEmployeeCodetransfer(item);
                    this.Search();
                }
                else
                {

                }
            }
            else if (ucl_ActionControl1.CurrentAction == FormActionType.SaveAs)
            {


            }

        }

        public void Search()
        {
            string empCd;
            if (txtCode.Text == "")
            {
                empCd = "%";
            }
            else
            {
                empCd = txtCode.Text;
            }

            if (dtpTDate.Value != DateTime.MinValue)
            {
                searchData = empSvr.GetEmployeeCodetransfer(empCd, dtpTDate.Value.Date);
            }
            else
            {
                searchData = empSvr.GetEmployeeCodetransfer(empCd);
            }
            gvData = searchData;
            FillDataGrid();
        }

        public void Export()
        {

        }

        public void Print()
        {

        }

        public void Open()
        {
            AddGridViewColumns();
            gvData = addData;

            kryptonHeaderGroup1_Click(kryptonHeaderGroup1, new EventArgs());

            
        }

        public void Clear()
        {
            gvData = null;
        }

        public void RefreshData()
        {

        }

        public void Exit()
        {
            this.Close();
        }

        #endregion

        #region IFormPermission Members

        public DCI.Security.Model.PermissionInfo Permission
        {
            set { ucl_ActionControl1.Permission = value; }
        }

        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.Search();
        }

        private void kryptonHeaderGroup1_Click(object sender, EventArgs e)
        {
            if (sender == kryptonHeaderGroup1)
            {
                kryptonHeaderGroup3.Collapsed = true;
                kryptonHeaderGroup1.Collapsed = false;
                ucl_ActionControl1.CurrentAction = FormActionType.AddNew;
                // formMode = Mode.ADD;
                //    formAct = FormAction.New;
                //ClearDataGride();

                gvData = addData;
                FillDataGrid();
                txtOldCode.Focus();

            }
            else
            {
                // formMode = Mode.Search;
                // formAct = FormAction.Save;
                kryptonHeaderGroup1.Collapsed = true;
                kryptonHeaderGroup3.Collapsed = false;
                // ClearDataGride();
                //  otRequest_Control1.EnableEditDate = false;
                ucl_ActionControl1.CurrentAction = FormActionType.Save;

                //-------
                if (empData_Control1.cODETextBox.Text != "") {
                    searchData = empSvr.GetEmployeeCodetransfer(empData_Control1.cODETextBox.Text);
                }

                gvData = searchData;
                FillDataGrid();
                txtCode.Focus();
            }

        }

        private void txtOldCode_KeyDown(object sender, KeyEventArgs e)
         {
            if (e.KeyCode == Keys.Enter)
            {
                txtNewCode.Focus();

            }
        }

        private void txtNewCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (txtOldCode.Text == "")
                {
                    return;
                }
                else
                {
                    EmployeeInfo em = new EmployeeInfo();
                    em = empSvr.GetEmployeeData(txtOldCode.Text);
                    if (em == null)
                    {
                        return;
                    }

                }

                if (txtNewCode.Text == "")
                {

                }
                else
                {
                    EmployeeInfo em = new EmployeeInfo();
                    em = empSvr.GetEmployeeData(txtNewCode.Text);
                    if (em != null)
                    {
                      //  return;
                    }
                }
                if (empSvr.GetEmployeeCodetransfer(txtOldCode.Text) != null || empSvr.GetEmployeeCodetransfer(txtNewCode.Text) != null)
                {
                    return;
                }

                this.Save();

                txtOldCode.Focus();
            }
        }

        private void dgItems_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyleDefault.ShowRowNumber(dgItems, e);
        }

        private void dgItems_SelectionChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (dgItems.SelectedRows.Count > 0)
                {
                    EmployeeInfo em = new EmployeeInfo();
                    EmployeeCodeTransferInfo inf = (EmployeeCodeTransferInfo)gvData[dgItems.SelectedRows[0].Index];
                    if (inf.TransferStatus == "")
                    {
                        em = empSvr.GetEmployeeData(inf.OldCode);
                    }
                    else
                    {
                        em = empSvr.GetEmployeeData(inf.NewCode);
                    }
                    if (em != null)
                    {
                        if (em.Code != "")
                        {
                            empData_Control1.Information = em;
                        }
                    }
                    empTransfer_Control1.Information = inf;
                }
            }
            catch { }
            this.Cursor = Cursors.Default;
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (ucl_ActionControl1.CurrentAction == FormActionType.Save)
            {
                if (MessageBox.Show("This will tranfer employee, do you want to continue?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    stMgr.MaxProgress = dgItems.SelectedRows.Count;
                    foreach (DataGridViewRow item in dgItems.SelectedRows)
                    {
                        try
                        {
                            EmployeeCodeTransferInfo emtr = (EmployeeCodeTransferInfo)gvData[item.Index];
                            stMgr.Status = "Tranfingring Code:" + emtr.OldCode + "To Code:" + emtr.NewCode;
                            stMgr.Progress++;
                            if (emtr.TransferStatus != "P")
                            {
                                empSvr.TransferCode(emtr);

                                //*************************************
                                //    add leave old code -> new code
                                //*************************************
                                if (emtr.OldCode.StartsWith("I")) {
                                    string str = @"INSERT INTO DCI.LVRQ (CODE, CDATE, TYPE, LVFROM, LVTO, TIMES, TOTAL, SALSTS, LVNO, REASON, ADD_DATE, EDIT_BY, UPD_BY, UPD_DT, DOC_ID)
                                               SELECT '" + emtr.NewCode + @"', CDATE, TYPE, LVFROM, LVTO, TIMES, TOTAL, SALSTS, LVNO, REASON, CR_DT, CR_BY, UPD_BY, UPD_DT, DOC_ID FROM DCITC.LVRQ L WHERE CODE = '" + emtr.OldCode + @"' ";
                                    OracleCommand cmd = new OracleCommand();
                                    cmd.CommandText = str;
                                    oOraDCI.ExecuteCommand(cmd);
                                }
                                

                            }
                            else
                            {
                                DialogResult res = MessageBox.Show("พนักงานรหัส " + emtr.OldCode + " ถูก Tranfer แล้วคุณต้องการ Tranfer ใหม่หรือไม่?", "คำเตือน", MessageBoxButtons.YesNoCancel);
                                if (res == DialogResult.Yes)
                                {
                                    empSvr.TransferCode(emtr);

                                    //*************************************
                                    //    add leave old code -> new code
                                    //*************************************
                                    if (emtr.OldCode.StartsWith("I"))
                                    {
                                        string str = @"INSERT INTO DCI.LVRQ (CODE, CDATE, TYPE, LVFROM, LVTO, TIMES, TOTAL, SALSTS, LVNO, REASON, ADD_DATE, EDIT_BY, UPD_BY, UPD_DT, DOC_ID)
                                               SELECT '" + emtr.NewCode + @"', CDATE, TYPE, LVFROM, LVTO, TIMES, TOTAL, SALSTS, LVNO, REASON, CR_DT, CR_BY, UPD_BY, UPD_DT, DOC_ID FROM DCITC.LVRQ L WHERE CODE = '" + emtr.OldCode + @"' ";
                                        OracleCommand cmd = new OracleCommand();
                                        cmd.CommandText = str;
                                        oOraDCI.ExecuteCommand(cmd);
                                    }


                                }
                                else if (res == DialogResult.No)
                                {
                                    continue;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("ไม่สามารถ Tranfer พนักงานรหัส " + item.Cells[0].Value.ToString() + "ได้เนื่องจาก " + ex.Message, "Error",
                             MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    this.Search();
                }
            }
        }

        private void dgItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex > 0)
                {
                    //txtOldCode.Text = dgItems.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    //txtNewCode.Text = dgItems.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                    DataGridViewRow item = dgItems.Rows[e.RowIndex];
                    EmployeeCodeTransferInfo emtr = (EmployeeCodeTransferInfo)gvData[item.Index];

                    empTransfer_Control1.txtOldCode.Text = emtr.OldCode;
                    empTransfer_Control1.txtNewCode.Text = emtr.NewCode;
                    empTransfer_Control1.dtpTrans.Value = emtr.TransferDate;

                }
            }
            catch { }
        }

        
    }
}
