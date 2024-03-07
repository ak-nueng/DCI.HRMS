using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Service;
using DCI.HRMS.Base;
using DCI.HRMS.Model.Attendance;
using DCI.HRMS.Model.Personal;
using DCI.HRMS.Model.Organize;
using DCI.HRMS.Common;
using DCI.HRMS.Model.Common;

namespace DCI.HRMS.Attendance
{
    
    public partial class FrmOtInput : Form, IFormParent, IFormPermission
    {
        private enum Mode { Search, ADD };
          ApplicationManager appMgr = ApplicationManager.Instance();
                  
        private   OtService otsvr = OtService.Instance();
        private ShiftService shiftsrv = ShiftService.Instance();
        private EmployeeService empsrv = EmployeeService.Instance();
        private OtRequestInfo otreq = new OtRequestInfo();
        private ArrayList otadd = new ArrayList();
        private ArrayList otsearch = new ArrayList();
        private ArrayList gvData = new ArrayList();
        private ArrayList allDvcd = new ArrayList();
        private Mode formMode = Mode.ADD;
        private int indexSearch = 0;

        private readonly string[] colName = new string[] { "RequestDate", "RequestId", "EmployeeCode", "Job Type", "OT From", "OT To", "OT 1", "OT 1.5", "OT 2", "OT 3" };
        private readonly string[] propName = new string[] { "OtDate", "ReqId", "EmpCode", "JobType", "OTFrom", "OtTo", "Rate1", "Rate15", "Rate2", "Rate3" };
        private readonly int[] width = new int[] { 80, 80,100,80, 100, 100, 100, 100,100,100};

        private readonly string[] colNameS = new string[] {"Result", "RequestDate", "RequestId", "EmployeeCode", "Job Type", "OT From", "OT To", "OT 1", "OT 1.5", "OT 2", "OT 3" ,"N From","N To","N1","N1.5","N2","N3","TimeCard"};
        private readonly string[] propNameS = new string[] {"CalRest", "OtDate", "ReqId", "EmpCode", "JobType", "OTFrom", "OtTo", "Rate1", "Rate15", "Rate2", "Rate3","NFrom","NTo","N1","N15","N2","N3","TimeCard" };
        private readonly int[] widthS = new int[] { 50,80, 80,80, 80,80,80, 80, 80, 80,80,80,80,80,80,80,80,120 };
        public FrmOtInput()
        {
            InitializeComponent();
        }

        private void FrmOtInput_Load(object sender, EventArgs e)
        { 
            ucl_ActionControl1.Owner = this;

            cmbJobType.SelectedItem = "M";

            allDvcd = DivisionService.Instance().FindByType("GRP");
            DivisionInfo all = new DivisionInfo();
            all.Code = "%";
            all.ShortName = "All";
            allDvcd.Insert(0, all);
            cmbDvcd.DisplayMember = "ShortName";
            cmbDvcd.ValueMember = "Code";
            cmbDvcd.DataSource = allDvcd;      

             AddGridViewColumns();
            kryptonHeaderGroup1_Click(kryptonHeaderGroup1, new EventArgs());
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
            get
            {
                return string.Empty;
            }
        }

        public object Information
        {
            get
            {
                return null;
            }
            set
            {
               
            }
        }

        public void AddNew()
        {
           
        }

        public void Save()
        {
            
        }

        public void Delete()
        {
            
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
            
        }

        public void Exit()
        {
            this.Close();
        }

        #endregion
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                empDetail_Control1.Information = textBox1.Text;
                if (empDetail_Control1.Information != null)
                {
                   // empShift_Control1.Information = shiftsrv.GetEmShift(textBox1.Text, dateTimePicker1.Value.ToString("yyyyMM"));
                    if (txtReqId.Text != "")
                    { 
                         EmployeeInfo temp = (EmployeeInfo)empDetail_Control1.Information;
                        otRate_Control1.SetRate(temp.WorkType);
                        OtRateInfo rtemp =(OtRateInfo) otRate_Control1.Information;
                        if (rtemp.OtFrom == "")
                        {
                            MessageBox.Show("กรุณาป้อนเวลาเริ่ม", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            otRate_Control1.SetFocusTxtFrom();
                            return;
                        }
                        if (rtemp.OtTo == "")
                        {
                            MessageBox.Show("กรุณาป้อนเวลาจบ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            otRate_Control1.SetFocusTxtTo();
                            return;

                        }


                        OtRequestInfo otReq = new OtRequestInfo();
                        EmployeeInfo emp = (EmployeeInfo)empDetail_Control1.Information;
                        otReq.OtDate = dateTimePicker1.Value.Date;
                        otReq.EmpCode = emp.Code;
                        otReq.JobType = cmbJobType.SelectedItem.ToString();
                        otReq.ReqId = txtReqId.Text;

                        ObjectInfo info = new ObjectInfo();
                        info.CreateBy=  appMgr.UserAccount.AccountId;
                        info.CreateDateTime=DateTime.Now;                   
                        otreq.Inform = info;

                        OtRateInfo otRate = (OtRateInfo)otRate_Control1.Information;

                        otReq.OtFrom = otRate.OtFrom;
                        otReq.OtTo = otRate.OtTo;
                        otReq.Rate1 = otRate.Rate1;
                        otReq.Rate15 = otRate.Rate15;
                        otReq.Rate2 = otRate.Rate2;
                        otReq.Rate3 = otRate.Rate3;
                        otadd.Insert(0,otReq);

                        ClearDataGride();
                        gvData = otadd;
                        FillDataGrid();
                       
                    }
                    else
                    {
                        MessageBox.Show("กรุณาป้อนหมายเลข OTReq", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtReqId.Focus();
                        return;

                    }
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูลพนักงานรหัส "+ textBox1.Text , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                textBox1.Clear();
                textBox1.Focus();

            }
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
        private void AddGridViewColumnsS()
        {
            this.dgItems.Columns.Clear();
            dgItems.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn[] columns = new DataGridViewTextBoxColumn[colNameS.Length];
            for (int index = 0; index < columns.Length; index++)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();

                column.Name = colNameS[index];
                column.DataPropertyName = propNameS[index];
                column.ReadOnly = true;
                column.Width = widthS[index];

                columns[index] = column;
                dgItems.Columns.Add(columns[index]);
            }
            dgItems.ClearSelection();
        }

        private void FillDataGrid()
        {

            dgItems.DataSource = gvData;
            dgItems.Refresh();
        }
        private void ClearDataGride()
        {
            gvData = otsvr.GetOTRequest("0000", dtpSDate.Value.Date, "0000", cmbDvcd.SelectedValue.ToString());

            dgItems.DataSource = gvData;
            dgItems.Refresh();

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void cmbJobType_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void delectSelectedRowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow delrw in dgItems.SelectedRows)
            {
                //dgItems.Rows.RemoveAt(delrw.Index);
                otadd.RemoveAt(delrw.Index);
            }
            ClearDataGride();
            gvData = otadd;
            FillDataGrid();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
           
            otsearch = otsvr.GetOTRequest(txtSCode.Text, dtpSDate.Value.Date, txtSreq.Text, cmbDvcd.SelectedValue.ToString());
            ClearDataGride();
            gvData = otsearch;
            FillDataGrid();
           
        }



        private void kryptonHeaderGroup1_Click(object sender, EventArgs e)
        {
            if (sender == kryptonHeaderGroup1)
            {
                kryptonHeaderGroup3.Collapsed = true;
                kryptonHeaderGroup1.Collapsed = false;
                ucl_ActionControl1.CurrentAction = FormActionType.None;
                formMode = Mode.ADD;
                dgItems.ClearSelection();
                ClearDataGride();
                gvData = otadd; 
                AddGridViewColumns();
                FillDataGrid();
              
            }
            else
            {
                formMode = Mode.Search;
                kryptonHeaderGroup1.Collapsed = true;
                kryptonHeaderGroup3.Collapsed = false;
                ClearDataGride();
                gvData = otsearch;
                AddGridViewColumnsS();
                FillDataGrid();
               
            }

        }




        private void empDetail_Control1_Load(object sender, EventArgs e)
        {
            empDetail_Control1.empServ = empsrv;
            empDetail_Control1.shFtServ = shiftsrv;
        }

        private void otRate_Control1_Load(object sender, EventArgs e)
        {
            otRate_Control1.otsrv = otsvr;
            otRate_Control1.SetAllRate(otsvr.GetAllRate());
        }

        
        private void dgItems_SelectionChanged(object sender, EventArgs e)
        {
            try
            {

                if (formMode == Mode.Search)
                {
                    OtRequestInfo selrq = (OtRequestInfo)otsearch[dgItems.SelectedRows[0].Index];
                    empDetail_Control1.ShftDate = selrq.OtDate;
                    empDetail_Control1.Information = selrq.EmpCode;
                    //  empShift_Control1.Information = shiftsrv.GetEmShift(selrq.EmpCode, selrq.OtDate.ToString("yyyyMM"));
                    otRequest_Control1.Information = selrq;
                    dgItems.Focus();

                }


            }
            catch
            {


            }
        }

        private void dgItems_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyleDefault.ShowRowNumber(dgItems, e);
            if (dgItems["Result", e.RowIndex].Value.ToString() != "Y")
            {
                dgItems.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightPink;
            }
        }


  
    }
}