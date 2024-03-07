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
using DCI.HRMS.Util;
using DCI.Security.Model;
using DCI.HRMS.Model;
using DCI.HRMS.Service.Trainee;
using DCI.HRMS.Service.SubContract;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Reflection;
using Oracle.ManagedDataAccess.Client;

namespace DCI.HRMS.Attendance
{

    public partial class FrmOverTimeInput : Form, IFormParent, IFormPermission
    {

        private FormAction formAct = FormAction.New;
        private OtService otsvr = OtService.Instance();
        private TraineeOtService tnOtSvr = TraineeOtService.Instance();
        private SubContractOtService subOtSvr = SubContractOtService.Instance();
        private ShiftService shiftsrv = ShiftService.Instance();

        private DictionaryService dictSvr = DictionaryService.Instance();
        private EmployeeService empsrv = EmployeeService.Instance();
        private TraineeService tnSvr = TraineeService.Instance();
        private SubContractService subSvr = SubContractService.Instance();
        //private OtRequestInfo otreq = new OtRequestInfo();
        private ShiftService shSvr = ShiftService.Instance();
        private EmployeeLeaveService lvrqSvr = EmployeeLeaveService.Instance();
        private TimeCardService tmSvr = TimeCardService.Instance();
        private frmAlert messageAlert = new frmAlert();
        private ArrayList otsearch;
        private ArrayList otadd;
        private ArrayList gvData = new ArrayList();
        private ArrayList allDvcd = new ArrayList();
        private StatusManager stsMgr = new StatusManager();
        private DataGridViewPrinter MyDataGridViewPrinter;
        ApplicationManager appMgr = ApplicationManager.Instance();
        private ObjectInfo inform;
        private FrmOvertimeCalculate otCal = new FrmOvertimeCalculate();

        private bool isAuto = false;

        ClsOraConnectDB oOraDCI = new ClsOraConnectDB("DCI");


        // private int indexSearch = 0;
        private string rqId;
        private DateTime rqDt;
        private readonly string[] colName = new string[] { "RequestDate", "RequestId", "EmployeeCode", "Job Type", "OT From", "OT To", "OT 1", "OT 1.5", "OT 2", "OT 3" };
        private readonly string[] propName = new string[] { "OtDate", "ReqId", "EmpCode", "JobType", "OTFrom", "OtTo", "Rate1", "Rate15", "Rate2", "Rate3" };
        private readonly int[] width = new int[] { 80, 80, 100, 80, 100, 100, 100, 100, 100, 100 };

        // private readonly string[] colNameS = new string[] { "RequestDate", "RequestId", "EmployeeCode", "Job Type", "OT From", "OT To", "OT 1", "OT 1.5", "OT 2", "OT 3", "Result", "N From", "N To", "N1", "N1.5", "N2", "N3", "TimeCard", "AddBy", "AddDate", "UpdateBy", "UpdateDate" };
        // private readonly string[] propNameS = new string[] { "OtDate", "ReqId", "EmpCode", "JobType", "OTFrom", "OtTo", "Rate1", "Rate15", "Rate2", "Rate3", "CalRest", "NFrom", "NTo", "N1", "N15", "N2", "N3", "TimeCard" ,"CreateBy", "CreateDateTime", "LastUpdateBy", "LastUpDateDateTime" };
        //  private readonly int[] widthS = new int[] { 50, 80, 80, 80, 80, 80, 80, 80, 80, 80, 80, 80, 80, 80, 80, 80, 80, 120  ,100, 120, 100, 120 };
        private readonly string[] colNameS = new string[] { "RqDate", "RqId", "EmpCode", "EmpName", "DVCD", "Grpot", "Type", "JobType", "Bus", "OTFrom", "OTTo", "OT1", "OT1.5", "OT2", "OT3", "Result", "TimeCard", "Shift", "AddBy", "AddDate", "UpdateBy", "UpdateDate", "OTType" };
        private readonly string[] propNameS = new string[] { "OtDate", "ReqId", "EmpCode", "EmpName", "DVCD", "Grpot", "EmpType", "JobType", "Bus", "OTFrom", "OtTo", "Rate1", "Rate15", "Rate2", "Rate3", "CalRest", "TimeCard", "Shift", "CreateBy", "CreateDateTime", "LastUpdateBy", "LastUpDateDateTime", "OTRemark" };


        //   private readonly int[] widthS = new int[] { 50, 40, 30, 30, 30, 30, 30, 30, 30, 40, 40, 40, 40, 40, 40, 40, 120, 100, 120, 100, 120 };
        private Reports.FrmRptOverTime rpt = new DCI.HRMS.Attendance.Reports.FrmRptOverTime();

        public FrmOverTimeInput()
        {
            InitializeComponent();
        }

        # region Event

        private void FrmOtInput_Load(object sender, EventArgs e)
        {
            this.Open();



            Type dgvType = dgItems.GetType();
            System.Reflection.PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgItems, true, null);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // this.Save();


                txtScanCode.Focus();
            }
            else
            {
            }
        }

        private void textBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.AddNew();
            }
        }
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtScanCode.Text.Length == 5)
            {
                this.AddNew();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //  txtRqdate.Text = dateTimePicker1.Value.ToString("dd/MM/yyyy");
        }

        private void cmbJobType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //textBox1.Focus();
        }

        private void delectSelectedRowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Delete();
            /*
            foreach (DataGridViewRow delrw in dgItems.SelectedRows)
            {
                //dgItems.Rows.RemoveAt(delrw.Index);
                otadd.RemoveAt(delrw.Index);
            }
            //ClearDataGride();
            gvData = otadd;
            FillDataGrid();
        */
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Search();
        }

        private void kryptonHeaderGroup1_Click(object sender, EventArgs e)
        {
            if (sender == kryptonHeaderGroup1)
            {
                kryptonHeaderGroup3.Collapsed = true;
                kryptonHeaderGroup1.Collapsed = false;
                ucl_ActionControl1.CurrentAction = FormActionType.None;
                // formMode = Mode.ADD;
                formAct = FormAction.New;
                //ClearDataGride();
                gvData = otadd;
                FillDataGrid();
                txtScanCode.Focus();

            }
            else
            {
                // formMode = Mode.Search;
                formAct = FormAction.Save;
                kryptonHeaderGroup1.Collapsed = true;
                kryptonHeaderGroup3.Collapsed = false;
                // ClearDataGride();
                otRequest_Control1.EnableEditDate = false;
                gvData = otsearch;
                FillDataGrid();
                txtSCode.Focus();
            }

        }

        private void empDetail_Control1_Load(object sender, EventArgs e)
        {
            // empDetail_Control1.empServ = empsrv;
            //empDetail_Control1.shFtServ = shiftsrv;
        }

        private void otRate_Control1_Load(object sender, EventArgs e)
        {
            otRate_Control1.otsrv = otsvr;
            otRate_Control1.SetAllRate(otsvr.GetAllRate());
        }

        private void dgItems_SelectionChanged_1(object sender, EventArgs e)
        {
            try
            {
                otRequest_Control2.Information = new OtRequestInfo();
                otRequest_Control1.Information = new OtRequestInfo();

                int select = dgItems.SelectedRows[0].Index;
                ucl_ActionControl1.CurrentAction = FormActionType.Save;
                if (formAct == FormAction.Save)
                {
                    OtRequestInfo selrq = (OtRequestInfo)gvData[select];
                    // empDetail_Control1.ShftDate = selrq.OtDate;
                    // empDetail_Control1.Information = selrq.EmpCode;
                    if (selrq.EmpCode.StartsWith("7"))
                    {
                        EmployeeInfo tn = tnSvr.Find(selrq.EmpCode);
                        empData_Control3.Information = tn;

                    }
                    else if (selrq.EmpCode.StartsWith("I"))
                    {
                        EmployeeInfo sub = subSvr.Find(selrq.EmpCode);
                        empData_Control3.Information = sub;

                    }
                    else
                    {
                        EmployeeInfo em = empsrv.Find(selrq.EmpCode);
                        empData_Control3.Information = em;

                    }
                    otRequest_Control1.Information = selrq;
                    //  otRequest_Control1.Focus();

                }
                else if (formAct == FormAction.New)
                {
                    OtRequestInfo selrq = (OtRequestInfo)gvData[select];

                    //  empDetail_Control1.ShftDate = selrq.OtDate;
                    // empDetail_Control1.Information = selrq.EmpCode;
                    if (selrq.EmpCode.StartsWith("7"))
                    {
                        EmployeeInfo tn = tnSvr.Find(selrq.EmpCode);
                        empData_Control3.Information = tn;

                    }
                    if (selrq.EmpCode.StartsWith("I"))
                    {
                        EmployeeInfo sub = subSvr.Find(selrq.EmpCode);
                        empData_Control3.Information = sub;

                    }
                    else
                    {
                        EmployeeInfo em = empsrv.Find(selrq.EmpCode);
                        empData_Control3.Information = em;

                    }
                    otRequest_Control2.Information = selrq;
                }

            }
            catch
            {
            }
        }

        private void txtSCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSCode.SelectAll();
                this.Search();
            }
        }

        private void dgItems_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyleDefault.ShowRowNumber(dgItems, e);
            //if (dgItems["Result", e.RowIndex].Value.ToString() != "Y" && dgItems["Result", e.RowIndex].Value.ToString().Trim() != "")
            //{
            //    dgItems.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;

            //}
        }

        private void FrmOverTimeInput_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        private void FrmOverTimeInput_KeyDown(object sender, KeyEventArgs e)
        {
            ucl_ActionControl1.OnActionKeyDown(sender, e);
        }
        private void otRequest_Control1_Save_Data()
        {
            this.Save();
            if (dgItems.CurrentRow.Index < dgItems.Rows.Count - 1)
            {
                dgItems.CurrentCell = dgItems[0, dgItems.CurrentCell.RowIndex + 1];

            }
            else
            {
                txtSCode.SelectAll();
                txtSCode.Focus();
            }
        }

        private void otRequest_Control1_Move_Data(KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down && dgItems.CurrentRow.Index < dgItems.Rows.Count - 1)
                {
                    dgItems.CurrentCell = dgItems[0, dgItems.CurrentCell.RowIndex + 1];
                }
                else if (e.KeyCode == Keys.Up && dgItems.CurrentRow.Index > 0)
                {
                    dgItems.CurrentCell = dgItems[0, dgItems.CurrentCell.RowIndex - 1];
                }
            }
            catch
            { }

        }
        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPressManager.Enter(e);
        }
        private void txtReqId_Enter(object sender, EventArgs e)
        {
            KeyPressManager.SelectAllTextBox(sender);
            rqId = txtReqId.Text;
        }
        private void dgItems_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (formAct == FormAction.New)
                {
                    if (dgItems.CurrentRow.Index == 0)
                    {
                        dgItems.CurrentCell = dgItems[0, dgItems.Rows.Count - 1];
                    }
                    string st = e.ListChangedType.ToString();

                }
            }
            catch
            {
            }
        }
        private void txtRqdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                KeyPressManager.Enter(e);

                if (txtRqdate.Value.Date != rqDt)
                {
                    otadd = new ArrayList();
                    gvData = new ArrayList();
                    FillDataGrid();
                }

            }
        }
        private void txtRqdate_Enter(object sender, EventArgs e)
        {

        }


        private void txtReqId_Leave(object sender, EventArgs e)
        {
            if (txtReqId.Text != rqId)
            {
                otadd = new ArrayList();
                gvData = new ArrayList();
                FillDataGrid();
            }
        }

        private void txtReqId_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressManager.EnterNumericOnly(e);
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            bool more = MyDataGridViewPrinter.DrawDataGridView(e.Graphics);
            if (more == true)
                e.HasMorePages = true;

        }

        private void txtSFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressManager.EnterNumericOnly(e);
        }
        private void txtSFrom_Leave(object sender, EventArgs e)
        {
            KeyPressManager.ConvertTextTime(sender);
        }

        private void txtSFrom_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPressManager.Enter(e);
        }


        private void dtpSDate_ValueChanged(object sender, EventArgs e)
        {
            if (sender == dtpSDate)
            {
                if (dtpSDate.Value > dtpTDate.Value)
                    dtpTDate.Value = dtpSDate.Value;
            }
            else
            {
                if (dtpSDate.Value > dtpTDate.Value)
                    dtpSDate.Value = dtpTDate.Value;
            }
            txtSCode.Focus();
        }
        private void editSelectedRowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formAct == FormAction.Save)
            {
                if (MessageBox.Show("ต้องการแก้ไขข้อมูลทั้งหมดที่เลื่อกใช่หรือไม่?", "Conferm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    this.Save();
                }
            }
            else
            {
                MessageBox.Show("ไม่สามารถแก้ไขได้เนื่องจากไม่ได้อยู่ใน Search Mode ", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }


        private void txtRqdate_DateChange()
        {
            cmbJobType.Focus();
        }
        #endregion
        #region IFormPermission Members

        public PermissionInfo Permission
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

                if (formAct == FormAction.New)
                {
                    return otRequest_Control2.Information;
                }
                else if (formAct == FormAction.Save)
                {
                    return otRequest_Control1.Information;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (formAct == FormAction.New)
                {
                    otRequest_Control2.Information = value;
                }
                else if (formAct == FormAction.Save)
                {
                    otRequest_Control1.Information = value;
                }

            }
        }

        public void AddNew()
        {
            if (formAct == FormAction.New)
            {
                if (txtScanCode.Text.Trim().Length == 5)
                {
                    EmployeeInfo emp;
                    if (txtScanCode.Text.StartsWith("7"))
                    {
                        emp = tnSvr.Find(txtScanCode.Text);
                    }
                    else if (txtScanCode.Text.StartsWith("I"))
                    {
                        emp = subSvr.Find(txtScanCode.Text);
                    }
                    else
                    {
                        emp = empsrv.Find(txtScanCode.Text);
                    }

                    if (emp != null)
                    {
                        bool dupl = false;



                        //  bool resign = false;
                        // ucl_ActionControl1.CurrentAction = FormActionType.SaveAs;
                        // EmployeeInfo emp = (EmployeeInfo)empDetail_Control1.Information;
                        // empShift_Control1.Information = shiftsrv.GetEmShift(textBox1.Text, dateTimePicker1.Value.ToString("yyyyMM"));

                        if (txtReqId.Text.Trim() != "")
                        {
                            otRate_Control1.SetRate(emp.WorkType);
                            OtRateInfo rtemp = (OtRateInfo)otRate_Control1.Information;
                            
                            

                            
                            if (rtemp.OtFrom != "")
                            {
                                if (rtemp.OtTo != "")
                                {
                                    foreach (OtRequestInfo test in otadd)
                                    {
                                        if (test.EmpCode == txtScanCode.Text && test.ReqId == txtReqId.Text)
                                        {
                                            dupl = true;
                                            break;
                                        }
                                    }
                                    if (!dupl)
                                    {
                                        if (emp.ResignDate == DateTime.Parse("01/01/1900 12:00:00 AM") || emp.ResignDate >= txtRqdate.Value.Date)
                                        {
                                            OtRequestInfo otReq = new OtRequestInfo();

                                            otReq.EmpName = emp.NameInEng.ToString();
                                            try
                                            {
                                                otReq.Dvcd = emp.Division.ShortName;
                                            }
                                            catch
                                            { }
                                            otReq.Grpot = emp.OtGroupLine;
                                            otReq.OtDate = txtRqdate.Value.Date;
                                           otReq.EmpCode = emp.Code;
                                            //otReq.JobType = cmbJobType.SelectedItem.ToString();
                                            otReq.JobType = cmbJobType.SelectedValue.ToString();
                                            otReq.ReqId = txtReqId.Text;
                                            otReq.EmpType = emp.WorkType;
                                            otReq.OtRemark = cmbOTType.SelectedValue.ToString();
                                            OtRateInfo otRate = (OtRateInfo)otRate_Control1.Information;
                                            


                                            if (otRate.OtFrom == "" || otRate.OtTo == "")
                                            {
                                                MessageBox.Show("กรุณาป้อนเวลาการทำ OT ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                return;
                                            }
                                            /* inform = new ObjectInfo();
                                         inform.CreateBy = appMgr.UserAccount.AccountId;
                                         
                                         otReq.Inform = inform;   */
                                            otReq.OtFrom = otRate.OtFrom;
                                            otReq.OtTo = otRate.OtTo;
                                            otReq.Rate1 = otRate.Rate1;
                                            otReq.Rate15 = otRate.Rate15;
                                            otReq.Rate2 = otRate.Rate2;
                                            otReq.Rate3 = otRate.Rate3;
                                            otReq.Rate1From = otRate.Rate1From;
                                            otReq.Rate15From = otRate.Rate15From;
                                            otReq.Rate2From = otRate.Rate2From;
                                            otReq.Rate3From = otRate.Rate3From;
                                            otReq.Rate1To = otRate.Rate1To;
                                            otReq.Rate15To = otRate.Rate15To;
                                            otReq.Rate2To = otRate.Rate2To;
                                            otReq.Rate3To = otRate.Rate3To;

                                            string msg = "";

                                            if (otReq.EmpCode.StartsWith("7"))
                                            {
                                                if (!CheckOtExitTrainee(otReq.EmpCode, otReq.OtDate, otReq.ReqId, otReq.OtFrom, otReq.OtTo, ref msg))
                                                {
                                                    //MessageBox.Show("มีข้อมูลพนักงานรหัส " + textBox1.Text + " ReqId " + txtReqId.Text + " ใน Database แล้ว", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                                    try
                                                    {
                                                        otReq.DocId = otsvr.LoadRecordKey();
                                                        this.Information = otReq;
                                                        this.Save();

                                                    }
                                                    catch (Exception ex)
                                                    {
                                                    }
                                                    try
                                                    {
                                                        dgItems.CurrentCell = dgItems[0, dgItems.RowCount - 1];
                                                    }
                                                    catch
                                                    { }
                                                    ucl_ActionControl1.CurrentAction = FormActionType.Save;
                                                }
                                                else
                                                {
                                                    messageAlert.ShowMessage(this, msg, frmAlert.MsgType.Error);
                                                    // MessageBox.Show("มีข้อมูลพนักงานรหัส " + textBox1.Text + " ReqId " + txtReqId.Text + " ใน Database แล้ว", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                            }
                                            else if (otReq.EmpCode.StartsWith("I"))
                                            {
                                                if (!CheckOtExitSubcontract(otReq.EmpCode, otReq.OtDate, otReq.ReqId, otReq.OtFrom, otReq.OtTo, ref msg))
                                                {
                                                    //MessageBox.Show("มีข้อมูลพนักงานรหัส " + textBox1.Text + " ReqId " + txtReqId.Text + " ใน Database แล้ว", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                                    try
                                                    {
                                                        otReq.DocId = otsvr.LoadRecordKey();
                                                        this.Information = otReq;
                                                        this.Save();

                                                    }
                                                    catch (Exception ex)
                                                    {
                                                    }
                                                    try
                                                    {
                                                        dgItems.CurrentCell = dgItems[0, dgItems.RowCount - 1];
                                                    }
                                                    catch
                                                    { }
                                                    ucl_ActionControl1.CurrentAction = FormActionType.Save;
                                                }
                                                else
                                                {
                                                    messageAlert.ShowMessage(this, msg, frmAlert.MsgType.Error);
                                                    // MessageBox.Show("มีข้อมูลพนักงานรหัส " + textBox1.Text + " ReqId " + txtReqId.Text + " ใน Database แล้ว", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                            }
                                            else
                                            {
                                                if (!CheckOtExit(otReq.EmpCode, otReq.OtDate, otReq.ReqId, otReq.OtFrom, otReq.OtTo, ref msg))
                                                {
                                                    //MessageBox.Show("มีข้อมูลพนักงานรหัส " + textBox1.Text + " ReqId " + txtReqId.Text + " ใน Database แล้ว", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                                    try
                                                    {
                                                        otReq.DocId = otsvr.LoadRecordKey();
                                                        this.Information = otReq;
                                                        this.Save();

                                                    }
                                                    catch (Exception ex)
                                                    {
                                                    }
                                                    try
                                                    {
                                                        dgItems.CurrentCell = dgItems[0, dgItems.RowCount - 1];
                                                    }
                                                    catch
                                                    { }
                                                    ucl_ActionControl1.CurrentAction = FormActionType.Save;
                                                }
                                                else
                                                {
                                                    messageAlert.ShowMessage(this, msg, frmAlert.MsgType.Error);
                                                    // MessageBox.Show("มีข้อมูลพนักงานรหัส " + textBox1.Text + " ReqId " + txtReqId.Text + " ใน Database แล้ว", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                            }

                                        }
                                        else
                                        {
                                            messageAlert.ShowMessage(this, "ไม่สามารถบันทึกข้อมูลพนักงานรหัส " + txtScanCode.Text + " วันที่ " + txtRqdate.Value.Date.ToString("dd/MM/yyyy") + "\nเนื่องจากพนักงานลาออกไปเมื่อวันที่ " + emp.ResignDate.ToString("dd/MM/yyyy")
                                                                        , frmAlert.MsgType.Error);
                                        }
                                    }
                                    else
                                    {
                                        //  MessageBox.Show("มีข้อมูลพนักงานรหัส " + textBox1.Text + "ReqId " + txtReqId.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                                        
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("กรุณาป้อนเวลาจบ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    otRate_Control1.SetFocusTxtTo();
                                }
                            }
                            else
                            {
                                MessageBox.Show("กรุณาป้อนเวลาเริ่ม", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                otRate_Control1.SetFocusTxtFrom();
                            }
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
                        messageAlert.ShowMessage(this, "ไม่พบข้อมูลพนักงานรหัส " + txtScanCode.Text, frmAlert.MsgType.Error);

                        //MessageBox.Show("ไม่พบข้อมูลพนักงานรหัส " + textBox1.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            txtScanCode.Clear();
            txtScanCode.Focus();

        }

        public void Save()
        {


            this.Cursor = Cursors.WaitCursor;
            OtRequestInfo rq;
            if (formAct == FormAction.New)
            {
                if (ucl_ActionControl1.Permission.AllowAddNew)
                {
                    rq = (OtRequestInfo)this.Information;


                    if (rq.OtFrom == "" || rq.OtTo == "")
                    {
                        MessageBox.Show("กรุณาป้อนเวลาการทำ OT ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    try
                    {
                        inform = new ObjectInfo();
                        inform.CreateBy = appMgr.UserAccount.AccountId;
                        rq.Inform = inform;
                        if (rq.EmpCode.StartsWith("7"))
                        {
                            tnOtSvr.SaveOtRequest(rq);
                        }
                        else if (rq.EmpCode.StartsWith("I"))
                        {
                            subOtSvr.SaveOtRequest(rq);
                        }
                        else
                        {
                            otsvr.SaveOtRequest(rq);
                        }
                        otadd.Add(rq);
                        // ClearDataGride();
                        gvData = otadd;
                        if (!isAuto)
                        {
                            FillDataGrid();
                        }

                        //---- Insert SQL ----
                        InsertOTRQ_REQ(rq);
                    }
                    catch (Exception ex)
                    {
                        messageAlert.ShowMessage(this, "ไม่สามารถบันทึกข้อมูลพนักงานรหัส " + txtScanCode.Text + " วันที่ "
                             + rq.OtDate.ToString("dd/MM/yyyy") + " เวลา " + rq.OtFrom + " - " + rq.OtTo + " เนื่องจาก "
                             + ex.Message, frmAlert.MsgType.Error);
                    }
                }
                else
                {
                    MessageBox.Show("ไม่สามารถบันทึกข้อมูล ได้เนื่องจาก คุณไม่มีสิทธิเข้าถึง", "Access Denie", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {

                try
                {
                    if (ucl_ActionControl1.Permission.AllowEdit)
                    {


                        OtRequestInfo rqedit = (OtRequestInfo)this.Information;

                        if (rqedit.OtFrom == "" || rqedit.OtTo == "")
                        {
                            MessageBox.Show("กรุณาป้อนเวลาการทำ OT ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        foreach (DataGridViewRow var in dgItems.SelectedRows)
                        {

                            rq = (OtRequestInfo)var.DataBoundItem;
                            inform = new ObjectInfo();
                            inform.CreateBy = rq.CreateBy;
                            inform.CreateDateTime = rq.CreateDateTime;
                            inform.LastUpdateBy = appMgr.UserAccount.AccountId;
                            rq.Inform = inform;
                            bool dtChange = false;
                            /* Check If User want to change date of records*/
                            if (otRequest_Control1.EnableEditDate)
                            {
                                dtChange = rq.OtDate != rqedit.OtDate;
                                rq.OtDate = rqedit.OtDate;
                            }
                            rq.ReqId = rqedit.ReqId;
                            rq.OtFrom = rqedit.OtFrom;
                            rq.OtTo = rqedit.OtTo;
                            rq.JobType = rqedit.JobType;
                            rq.Rate1 = rqedit.Rate1;
                            rq.Rate15 = rqedit.Rate15;
                            rq.Rate2 = rqedit.Rate2;
                            rq.Rate3 = rqedit.Rate3;

                            rq.Rate1From = rqedit.Rate1From;
                            rq.Rate1To = rqedit.Rate1To;
                            rq.Rate15From = rqedit.Rate15From;
                            rq.Rate15To = rqedit.Rate15To;
                            rq.Rate2From = rqedit.Rate2From;
                            rq.Rate2To = rqedit.Rate2To;
                            rq.Rate3From = rqedit.Rate3From;
                            rq.Rate3To = rqedit.Rate3To;

                            string msg = "";
                            if (!dtChange)
                            {
                                try
                                {
                                    if (rq.EmpCode.StartsWith("7"))
                                    {
                                        if (rq.DocId.Trim() == "")
                                        {
                                            tnOtSvr.UpdateOtRequest(rq);
                                        }
                                        else
                                        {
                                            tnOtSvr.UpdateOtRequestById(rq);
                                        }
                                    }
                                    else
                                        if (rq.EmpCode.StartsWith("I"))
                                        {
                                            if (rq.DocId.Trim() == "")
                                            {
                                                subOtSvr.UpdateOtRequest(rq);
                                            }
                                            else
                                            {
                                                subOtSvr.UpdateOtRequestById(rq);
                                            }
                                        }
                                    else
                                    {
                                        if (rq.DocId.Trim() == "")
                                        {
                                            otsvr.UpdateOtRequest(rq);
                                        }
                                        else
                                        {
                                            otsvr.UpdateOtRequestById(rq);
                                        }
                                    }


                                    //---- Insert SQL ----
                                    InsertOTRQ_REQ(rq);
                                }
                                catch (Exception ex)
                                {

                                    MessageBox.Show("ไม่สามารถบันทึกข้อมูล Code =" + rq.EmpCode + "ได้ เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }
                            }
                            else
                            {


                                if (rq.EmpCode.StartsWith("7"))
                                {
                                    if (!CheckOtExitTrainee(rq.EmpCode, rq.OtDate, rq.ReqId, rq.OtFrom, rq.OtTo, ref msg))
                                    {
                                        try
                                        {

                                            if (rq.DocId.Trim() == "")
                                            {
                                                tnOtSvr.UpdateOtRequest(rq);
                                            }
                                            else
                                            {
                                                tnOtSvr.UpdateOtRequestById(rq);
                                            }

                                            //---- Insert SQL ----
                                            InsertOTRQ_REQ(rq);
                                        }
                                        catch (Exception ex)
                                        {

                                            MessageBox.Show("ไม่สามารถบันทึกข้อมูล Code =" + rq.EmpCode + "ได้ เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("ไม่สามารถบันทึกข้อมูล Code =" + rq.EmpCode + "ได้ เนื่องจาก  " + msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    }
                                }
                                else if (rq.EmpCode.StartsWith("I"))
                                {
                                    if (!CheckOtExitSubcontract(rq.EmpCode, rq.OtDate, rq.ReqId, rq.OtFrom, rq.OtTo, ref msg))
                                    {
                                        try
                                        {

                                            if (rq.DocId.Trim() == "")
                                            {
                                                subOtSvr.UpdateOtRequest(rq);
                                            }
                                            else
                                            {
                                                subOtSvr.UpdateOtRequestById(rq);
                                            }

                                            //---- Insert SQL ----
                                            InsertOTRQ_REQ(rq);
                                        }
                                        catch (Exception ex)
                                        {

                                            MessageBox.Show("ไม่สามารถบันทึกข้อมูล Code =" + rq.EmpCode + "ได้ เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("ไม่สามารถบันทึกข้อมูล Code =" + rq.EmpCode + "ได้ เนื่องจาก  " + msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    }
                                }
                                else
                                {
                                    if (!CheckOtExit(rq.EmpCode, rq.OtDate, rq.ReqId, rq.OtFrom, rq.OtTo, ref msg))
                                    {
                                        try
                                        {
                                            if (rq.DocId.Trim() == "")
                                            {
                                                otsvr.UpdateOtRequest(rq);
                                            }
                                            else
                                            {
                                                otsvr.UpdateOtRequestById(rq);
                                            }

                                            //---- Insert SQL ----
                                            InsertOTRQ_REQ(rq);
                                        }
                                        catch (Exception ex)
                                        {

                                            MessageBox.Show("ไม่สามารถบันทึกข้อมูล Code =" + rq.EmpCode + "ได้ เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("ไม่สามารถบันทึกข้อมูล Code =" + rq.EmpCode + "ได้ เนื่องจาก  " + msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    }
                                }
                            }
                        }


                        // this.RefreshData();
                    }
                    else
                    {
                        MessageBox.Show("ไม่สามารถบันทึกข้อมูล ได้เนื่องจาก คุณไม่มีสิทธิเข้าถึง", "Access Denie", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            this.Cursor = Cursors.Default;
        }



        public void Delete()
        {
            this.Cursor = Cursors.WaitCursor;
            if (ucl_ActionControl1.Permission.AllowDelete)
            {
                if (formAct == FormAction.New)
                {
                    try
                    {
                        if (MessageBox.Show("ต้องการลบข้อมูลใช่หรือไม่?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {

                            foreach (DataGridViewRow var in dgItems.SelectedRows)
                            {
                                OtRequestInfo rq = (OtRequestInfo)otadd[var.Index];

                                otadd.Remove(rq);
                                inform = new ObjectInfo();
                                inform.LastUpdateBy = appMgr.UserAccount.AccountId;
                                rq.Inform = inform;
                                try
                                {
                                    if (rq.EmpCode.StartsWith("7"))
                                    {
                                        tnOtSvr.DeleteOtRequest(rq);
                                    }
                                    else if (rq.EmpCode.StartsWith("I"))
                                    {
                                        subOtSvr.DeleteOtRequest(rq);
                                    }
                                    else
                                    {
                                        otsvr.DeleteOtRequest(rq);
                                    }


                                    //----- Delete SQL -----
                                    DeleteOTRQ_REQ(rq);
                                }
                                catch (Exception ex)
                                {

                                    MessageBox.Show("ไม่สามารถลบข้อมูล Code =" + rq.EmpCode + "ได้ เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }


                            }
                        }

                    }
                    catch
                    { }

                    // ClearDataGride();

                    gvData = otadd;
                    FillDataGrid();
                }
                else
                {
                    try
                    {

                        if (MessageBox.Show("ต้องการลบข้อมูลใช่หรือไม่?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            foreach (DataGridViewRow var in dgItems.SelectedRows)
                            {
                                OtRequestInfo rq = (OtRequestInfo)gvData[var.Index];
                                inform = new ObjectInfo();
                                inform.LastUpdateBy = appMgr.UserAccount.AccountId;
                                rq.Inform = inform;
                                try
                                {
                                    if (rq.EmpCode.StartsWith("7"))
                                    {
                                        tnOtSvr.DeleteOtRequest(rq);
                                    }
                                    else if (rq.EmpCode.StartsWith("I"))
                                    {
                                        subOtSvr.DeleteOtRequest(rq);
                                    }
                                    else
                                    {
                                        otsvr.DeleteOtRequest(rq);
                                    }

                                    //----- Delete SQL -----
                                    DeleteOTRQ_REQ(rq);
                                }
                                catch (Exception ex)
                                {

                                    MessageBox.Show("ไม่สามารถลบข้อมูล Code =" + rq.EmpCode + "ได้ เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                }
                            }
                            //OtRequestInfo rq = (OtRequestInfo)otRequest_Control1.Information;
                            RefreshData();
                            if (dgItems.Rows.Count == 0)
                            {
                                txtSCode.Focus();
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("ไม่สามารถลบข้อมูลได้ เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("ไม่สามารถลบข้อมูล ได้เนื่องจาก คุณไม่มีสิทธิเข้าถึง", "Access Denie", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            this.Cursor = Cursors.Default;

        }

        public void Search()
        {
            
            this.Cursor = Cursors.WaitCursor;
            if (formAct == FormAction.Save)
            {
                ucl_ActionControl1.CurrentAction = FormActionType.Search;
                otsearch = otsvr.GetOTRequest(txtSCode.Text, cboWType.SelectedItem.ToString(), cboPosition.SelectedValue.ToString(), dtpSDate.Value.Date, dtpTDate.Value.Date, txtSreq.Text, cmbDvcd.SelectedValue.ToString(), txtSFrom.Text, txtSTo.Text, cmbGrpl.Text, cmbGrpot.Text, cmbSrcOTType.SelectedValue.ToString());

                ArrayList tnLs = tnOtSvr.GetOTRequest(txtSCode.Text, cboWType.SelectedItem.ToString(), cboPosition.SelectedValue.ToString(), dtpSDate.Value.Date, dtpTDate.Value.Date, txtSreq.Text, cmbDvcd.SelectedValue.ToString(), txtSFrom.Text, txtSTo.Text, cmbGrpl.Text, cmbGrpot.Text, cmbSrcOTType.SelectedValue.ToString());

                if (tnLs != null)
                {
                    if (otsearch == null)
                    {
                        otsearch = new ArrayList();
                    }
                    foreach (OtRequestInfo item in tnLs)
                    {
                        otsearch.Add(item);
                    }

                }

                ArrayList subLs = subOtSvr.GetOTRequest(txtSCode.Text, cboWType.SelectedItem.ToString(), cboPosition.SelectedValue.ToString(), dtpSDate.Value.Date, dtpTDate.Value.Date, txtSreq.Text, cmbDvcd.SelectedValue.ToString(), txtSFrom.Text, txtSTo.Text, cmbGrpl.Text, cmbGrpot.Text, cmbSrcOTType.SelectedValue.ToString());

                if (subLs != null)
                {
                    if (otsearch == null)
                    {
                        otsearch = new ArrayList();
                    }
                    foreach (OtRequestInfo item in subLs)
                    {
                        otsearch.Add(item);
                    }

                }

                gvData = new ArrayList();
                if (chkAll.Checked)
                {
                    gvData = otsearch;

                }
                else
                {
                    foreach (OtRequestInfo item in otsearch)
                    {
                        if (item.CalRest == "Y" && chkY.Checked)
                        {
                            gvData.Add(item);

                        }
                        else if (item.CalRest == "T" && ChkT.Checked)
                        {
                            gvData.Add(item);
                        }
                        else if (item.CalRest == "O" && chkO.Checked)
                        {
                            gvData.Add(item);
                        }
                        else if (item.CalRest == "R" && chkR.Checked)
                        {
                            gvData.Add(item);
                        }
                    }

                }


                FillDataGrid();
                if (dgItems.Rows.Count > 0)
                {
                    otRequest_Control1.Focus();
                }
            }
            else if (formAct == FormAction.New)
            {
                if (txtReqId.Text != "")
                {
                    otadd = otsvr.GetOTRequest("", txtRqdate.Value.Date, txtReqId.Text, "%");

                    ArrayList tnLs = tnOtSvr.GetOTRequest("", txtRqdate.Value.Date, txtReqId.Text, "%");

                    if (tnLs != null)
                    {
                        if (otadd == null)
                        {
                            otadd = new ArrayList();
                        }
                        foreach (OtRequestInfo item in tnLs)
                        {
                            otadd.Add(item);
                        }

                    }
                    ArrayList subLs = subOtSvr.GetOTRequest("", txtRqdate.Value.Date, txtReqId.Text, "%");

                    if (subLs != null)
                    {
                        if (otadd == null)
                        {
                            otadd = new ArrayList();
                        }
                        foreach (OtRequestInfo item in subLs)
                        {
                            otadd.Add(item);
                        }

                    }

                    //ClearDataGride();
                    gvData = otadd;
                    FillDataGrid();
                    txtScanCode.Focus();

                    if (dgItems.Rows.Count > 0)
                    {
                        try
                        {
                            dgItems.CurrentCell = dgItems[0, dgItems.Rows.Count - 1];
                        }
                        catch
                        { }
                    }
                }

            }
            this.Cursor = Cursors.Default;
            

            //*******************************
            /*
            DataTable dtOT = new DataTable();
            string empcode = "", empType = "", posit = "", reqid = "", dvcd = "", otfrom = "", otto = "", grpl = "", grpot = "", otremark = "";
            DateTime odate = new DateTime();
            DateTime odateto = new DateTime();

            empcode = txtSCode.Text.Trim() == "" ? "%" : txtSCode.Text.Trim();
            empType = cboWType.SelectedItem.ToString().Trim() == "" ? "%" : cboWType.SelectedItem.ToString().Trim();
            posit = cboPosition.SelectedValue.ToString().Trim() == "" ? "%" : cboPosition.SelectedValue.ToString().Trim();

            odate = dtpSDate.Value.Date;
            odateto = dtpTDate.Value.Date;

            reqid = txtSreq.Text.Trim() == "" ? "%" : txtSreq.Text.Trim();
            dvcd = cmbDvcd.SelectedValue.ToString().Trim() == "" ? "%" : cmbDvcd.SelectedValue.ToString().Trim();
            otfrom = txtSFrom.Text.Trim() == "" ? "%" : txtSFrom.Text.Trim();
            otto = txtSTo.Text.Trim() == "" ? "%" : txtSTo.Text.Trim();
            grpl = cmbGrpl.Text.Trim() == "" ? "%" : cmbGrpl.Text.Trim();
            grpot = cmbGrpot.Text.Trim() == "" ? "%" : cmbGrpot.Text.Trim();
            otremark = cmbSrcOTType.SelectedValue.ToString().Trim() == "" ? "%" : cmbSrcOTType.SelectedValue.ToString().Trim();

            string strOT = @"SELECT * FROM (
                                SELECT ODATE, RQ, r.CODE, e.NAME||' '||e.SURN EmpName, r.DVCD, e.GrpOT, e.WTYPE, r.JOB, r.BUS, r.STOP, 
                                       r.OTFROM, r.OTTO, r.OT1, r.OT15, r.OT2, r.OT3, STS Result, Res TimeCard, r.CR_DT AddBy, 
                                       r.CR_DT AddDate, r.UPD_BY UpdateBy, r.UPD_DT UpdateDate, Remark OTType 
                                FROM otrq r
                                LEFT JOIN empm e ON e.code = r.code
                                WHERE r.code LIKE '" + empcode + @"'
                                    AND r.odate BETWEEN '" + odate.ToString("dd/MMM/yyyy") + "' AND '" + odateto.ToString("dd/MMM/yyyy") + @"'
                                    AND NVL (r.rq, '') LIKE '" + reqid + @"'
                                    AND r.sect LIKE '" + dvcd + @"'
                                    AND NVL (r.otfrom, '') LIKE '" + otfrom + @"'
                                    AND NVL (r.otto, '') LIKE '" + otto + @"'
                                    AND NVL (r.remark, ' ') LIKE '" + otremark + @"' 
       
                                   UNION ALL
       
                                SELECT ODATE, RQ, r.CODE, e.NAME||' '||e.SURN EmpName, r.DVCD, e.GrpOT, e.WTYPE, r.JOB, r.BUS, r.STOP, 
                                       r.OTFROM, r.OTTO, r.OT1, r.OT15, r.OT2, r.OT3, STS Result, Res TimeCard, r.CR_DT AddBy, 
                                       r.CR_DT AddDate, r.UPD_BY UpdateBy, r.UPD_DT UpdateDate, Remark OTType 
                                FROM DCITC.otrq r
                                LEFT JOIN DCITC.empm e ON e.code = r.code
                                WHERE r.code LIKE '" + empcode + @"'
                                    AND r.odate BETWEEN '" + odate.ToString("dd/MMM/yyyy") + "' AND '" + odateto.ToString("dd/MMM/yyyy") + @"'
                                    AND NVL (r.rq, '') LIKE '" + reqid + @"'
                                    AND r.sect LIKE '" + dvcd + @"'
                                    AND NVL (r.otfrom, '') LIKE '" + otfrom + @"'
                                    AND NVL (r.otto, '') LIKE '" + otto + @"'
                                    AND NVL (r.remark, ' ') LIKE '" + otremark + @"' 
       
                                   UNION ALL
          
                                SELECT ODATE, RQ, r.CODE, e.NAME||' '||e.SURN EmpName, r.DVCD, e.GrpOT, e.WTYPE, r.JOB, r.BUS, r.STOP, 
                                       r.OTFROM, r.OTTO, r.OT1, r.OT15, r.OT2, r.OT3, STS Result, Res TimeCard, r.CR_DT AddBy, 
                                       r.CR_DT AddDate, r.UPD_BY UpdateBy, r.UPD_DT UpdateDate, Remark OTType 
                                FROM DEV_OFFICE.otrq r
                                LEFT JOIN DEV_OFFICE.empm e ON e.code = r.code
                                WHERE r.code LIKE '" + empcode + @"'
                                    AND r.odate BETWEEN '" + odate.ToString("dd/MMM/yyyy") + "' AND '" + odateto.ToString("dd/MMM/yyyy") + @"'
                                    AND NVL (r.rq, '') LIKE '" + reqid + @"'
                                    AND r.sect LIKE '" + dvcd + @"'
                                    AND NVL (r.otfrom, '') LIKE '" + otfrom + @"'
                                    AND NVL (r.otto, '') LIKE '" + otto + @"'
                                    AND NVL (r.remark, ' ') LIKE '" + otremark + @"'        
                            )   ";
            OracleCommand cmdOT = new OracleCommand();
            cmdOT.CommandText = strOT;
            dtOT = oOraDCI.Query(cmdOT);

            dgItems.DataBindings.Clear();
            dgItems.DataSource = null;
            dgItems.DataSource = dtOT;
            */
            //*******************************

        }

        public void Export()
        {
            if (rpt.IsDisposed)
            {
                rpt = new DCI.HRMS.Attendance.Reports.FrmRptOverTime();
            }
            else
            {
                // rpt = new DCI.HRMS.Attendance.Reports.FrmRptOverTime();
            }

            rpt.ShowDialog();
            rpt.Activate();
        }

        public void Print()
        {
            // if( pageSetupDialog1.ShowDialog()== DialogResult.OK)
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {

                FontFamily fm = new FontFamily("Microsoft Sans Serif");
                Font ft = new Font(fm, 8.0f);
                string header;
                if (formAct == FormAction.Save)
                {
                    header = "OT request Report " + txtRqdate.Value.Date.ToString("dd/MM/yyyy");
                }
                else
                {
                    header = "OT request Report " + txtRqdate.Value.Date.ToString("dd/MM/yyyy");
                }
                foreach (DataGridViewRow item in dgItems.Rows)
                {
                    OtRequestInfo ot = (OtRequestInfo)item.DataBoundItem;
                    string sh = "";
                    for (DateTime rd = ot.OtDate.AddDays(-2); rd <= ot.OtDate.AddDays(3); rd = rd.AddDays(1))
                    {
                        try
                        {
                            sh += shiftsrv.GetEmShift(ot.EmpCode, rd);
                        }
                        catch
                        {
                            continue;
                        }

                    }
                    ot.Shift = sh;

                }



                dgItems.Columns["Shift"].Visible = true;
                dgItems.Columns["UpdateBy"].Visible = false;
                dgItems.Columns["UpdateDate"].Visible = false;
                dgItems.Columns["AddBy"].Visible = false;
                dgItems.Columns["AddDate"].Visible = false;
                printDocument1.DefaultPageSettings.Landscape = true;
                printDocument1.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(0, 40, 0, 50);
                MyDataGridViewPrinter = new DataGridViewPrinter(dgItems, printDocument1, true, false, header, ft, Color.Black, true);

                printDocument1.Print();
                dgItems.Columns["AddBy"].Visible = true;
                dgItems.Columns["AddDate"].Visible = true;
                dgItems.Columns["UpdateBy"].Visible = true;
                dgItems.Columns["UpdateDate"].Visible = true;
                dgItems.Columns["Shift"].Visible = false;

            }
        }

        public void Open()
        {

            txtRqdate.Value = DateTime.Today;
            ucl_ActionControl1.Owner = this;

            //cmbJobType.SelectedItem = "A";            
            DataTable dtJobType = new DataTable();
            dtJobType.Columns.Add("dataValue", typeof(string));
            dtJobType.Columns.Add("dataDisplay", typeof(string));
            dtJobType.Rows.Add("A", "A : งานเอกสาร");
            dtJobType.Rows.Add("B", "B : กิจกรรมตามแผน");
            dtJobType.Rows.Add("C", "C : กิจกรรมที่ไม่ได้ตามแผน");
            dtJobType.Rows.Add("D", "D : Rework , Sorting");
            dtJobType.Rows.Add("E", "E : Support Production");
            dtJobType.Rows.Add("F", "F : Kaizen");
            dtJobType.Rows.Add("G", "G : ซ่อม,สร้าง");
            dtJobType.Rows.Add("H", "H : วิศวกรรม, Engineering");
            dtJobType.Rows.Add("I", "I : ");
            dtJobType.Rows.Add("J", "J : ");
            dtJobType.Rows.Add("K", "K : ");
            dtJobType.Rows.Add("L", "L : ");
            dtJobType.Rows.Add("M", "M : ");
            cmbJobType.DataSource = dtJobType;
            cmbJobType.ValueMember = "dataValue";
            cmbJobType.DisplayMember = "dataDisplay";
            cmbJobType.SelectedValue = "A";
            

            allDvcd = DivisionService.Instance().FindByType("GRP");
            DivisionInfo all = new DivisionInfo();
            all.Code = "%";
            all.Name = "All";
            allDvcd.Insert(0, all);
            cmbDvcd.DisplayMember = "Name";
            cmbDvcd.ValueMember = "Code";
            cmbDvcd.DataSource = allDvcd;


            ShiftType allsh = new ShiftType();
            allsh.GrpOt = "%";
            ArrayList sh = shiftsrv.GetShiftType();
            sh.Insert(0, allsh);
            cmbGrpot.DisplayMember = "GrpOt";
            cmbGrpot.ValueMember = "GrpOt";
            cmbGrpot.DataSource = sh;
            //cmbGrpot.SelectedValue = "D";
            //   kryptonButton1_Click(sender, e);

            PositionInfo allPosit = new PositionInfo();
            allPosit.Code = "%";
            allPosit.NameEng = "All";
            ArrayList pos = empsrv.GetAllPosition();
            pos.Insert(0, allPosit);
            cboPosition.DisplayMember = "DispText";
            cboPosition.ValueMember = "Code";
            cboPosition.DataSource = pos;
            cboPosition.SelectedIndex = 0;



            ArrayList line = new ArrayList();
            line = dictSvr.SelectAll("LNGR");
            BasicInfo allLine = new BasicInfo();
            allLine.Code = "%";
            allLine.Name = "All";
            line.Insert(0, allLine);
            cmbGrpl.DisplayMember = "Code";
            cmbGrpl.ValueMember = "Code";
            cmbGrpl.DataSource = line;

            DataTable dtOTType = new DataTable();
            dtOTType.Columns.Add("dataValue", typeof(string));
            dtOTType.Columns.Add("dataDisplay", typeof(string));
            dtOTType.Rows.Add("00:OT Normal", "00:OT Normal");
            dtOTType.Rows.Add("11:Retroactively", "11:Retroactively");
            dtOTType.Rows.Add("22:Change Work Day", "22:Change Work Day");
            dtOTType.Rows.Add("33:OT On Leave Day", "33:OT On Leave Day");
            dtOTType.Rows.Add("44:Manual OT", "44:Manual OT");
            dtOTType.Rows.Add("55:Business Trip", "55:Business Trip");
            dtOTType.Rows.Add("66:Abnormal Time", "66:Abnormal Time");
            dtOTType.Rows.Add("77:Over Normal Time", "77:Over Normal Time");
            dtOTType.Rows.Add("99:System Error", "99:System Error");

            cmbOTType.DataSource = dtOTType;
            cmbOTType.ValueMember = "dataValue";
            cmbOTType.DisplayMember = "dataDisplay";


            DataTable dtScrOTType = new DataTable();
            dtScrOTType.Columns.Add("dataValue", typeof(string));
            dtScrOTType.Columns.Add("dataDisplay", typeof(string));
            dtScrOTType.Rows.Add("", " === All Data === ");
            dtScrOTType.Rows.Add("00:OT Normal", "00:OT Normal");
            dtScrOTType.Rows.Add("11:Retroactively", "11:Retroactively");
            dtScrOTType.Rows.Add("22:Change Work Day", "22:Change Work Day");
            dtScrOTType.Rows.Add("33:OT On Leave Day", "33:OT On Leave Day");
            dtScrOTType.Rows.Add("44:Manual OT", "44:Manual OT");
            dtScrOTType.Rows.Add("55:Business Trip", "55:Business Trip");
            dtScrOTType.Rows.Add("66:Abnormal Time", "66:Abnormal Time");
            dtScrOTType.Rows.Add("77:Over Normal Time", "77:Over Normal Time");
            dtScrOTType.Rows.Add("99:System Error", "99:System Error");

            cmbSrcOTType.DataSource = dtScrOTType;
            cmbSrcOTType.ValueMember = "dataValue";
            cmbSrcOTType.DisplayMember = "dataDisplay";
            try { cmbSrcOTType.SelectedValue = ""; }catch { }

            
            


            txtRqdate.Value = DateTime.Today;
            otsearch = new ArrayList();
            otadd = new ArrayList();
            // otadd.Add(new OtRequestInfo());
            // gvData = otadd;
            AddGridViewColumnsS();
            // FillDataGrid();
            //  kryptonHeaderGroup3.Collapsed = true;
            //otadd.Clear();
            // kryptonHeaderGroup1_Click(kryptonHeaderGroup1, new EventArgs());
            cboWType.SelectedIndex = 0;

            kryptonHeaderGroup3.Collapsed = true;
            kryptonHeaderGroup1.Collapsed = false;
            ucl_ActionControl1.CurrentAction = FormActionType.None;
            formAct = FormAction.New;
            txtScanCode.Focus();
        }

        public void Clear()
        {

        }

        public void RefreshData()
        {
            int selrow = 0;
            try
            {
                selrow = dgItems.SelectedRows[0].Index;
            }
            catch
            { }
            this.Search();
            try
            {                // dgItems.ClearSelection();
                dgItems.CurrentCell = dgItems[0, selrow];
            }
            catch
            { }

        }

        public void Exit()
        {
            this.Close();
        }
        #endregion
        #region Method
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
            // this.dgItems.Columns.Clear();
            dgItems.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn[] columns = new DataGridViewTextBoxColumn[colNameS.Length];
            for (int index = 0; index < columns.Length; index++)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();

                column.Name = colNameS[index];
                column.DataPropertyName = propNameS[index];
                column.ReadOnly = true;
                //column.Width = widthS[index];

                columns[index] = column;
                dgItems.Columns.Add(columns[index]);
            }
            dgItems.Columns["Shift"].Visible = false;
            //dgItems.ClearSelection();
        }

        private void FillDataGrid()
        {
            dgItems.DataBindings.Clear();
            dgItems.DataSource = null;
            dgItems.DataSource = gvData;
            // dgItems.CurrentCell = null;
            //dgItems.Refresh();

            this.Update();

        }
        private void ClearDataGride()
        {
            gvData = new ArrayList();

            dgItems.DataSource = gvData;
            dgItems.Refresh();
            this.Update();
        }

        private bool CheckOtExit(string code, DateTime odate, string rq, string otfrom, string otto, ref string msg)
        {
            ArrayList req = otsvr.GetOTRequest(code, odate, "", "%");
            
            bool Result = false;
            if (req == null || req.Count == 0)
            {
                Result = false;
            }
            else
            {
                if(req.Count > 0){
                    foreach (OtRequestInfo mReq in req)
                    {
                        if (code == mReq.EmpCode && rq == mReq.ReqId &&
                            odate.ToString("yyyy-MM-dd") == mReq.OtDate.ToString("yyyy-MM-dd")) {
                            
                            Result = true;
                            break;
                        }
                    }
                }
                 

                /*
                //  EmployeeShiftInfo sh = shiftsrv.GetEmShift(code, odate.ToString("yyyyMM")); 
                string dsh = shiftsrv.GetEmShift(code, odate);
                if (dsh != null)
                {
                    if ((dsh == "H" || dsh == "T"))
                    {
                        string lsh = shiftsrv.GetEmShift(code, odate.AddDays(-1));
                        if (lsh != null)
                        {
                            if (lsh == "N")
                            {

                                if (!otsvr.OtReqCheckExit(code, odate, otfrom, otto))
                                {
                                    return false;
                                }
                                else
                                {

                                    msg = "มีข้อมูลพนักงานรหัส " + code + " วันที่ " + odate.ToString("dd/MM/yyyy") + " เวลา " + otfrom + " - " + otto + " ใน Database แล้ว";
                                    return true;
                                }
                            }
                            else
                            {
                                msg = "ไม่สามรารถเพิ่มข้อมูลได้ เนื่องจาก พนักงานรหัส " + code + "ไม่ได้มีตารางกะ Night ในวันที่ " + odate.AddDays(-1).ToString("dd/MM/yyyy");
                                return true;
                            }
                        }
                        else
                        {
                            msg = "ไม่สามรารถเพิ่มข้อมูลได้ เนื่องจาก พนักงานรหัส " + code + "ไม่ได้มีตารางกะ Night ในวันที่ " + odate.AddDays(-1).ToString("dd/MM/yyyy");
                            return true;
                        }
                    }
                    else
                    {

                        msg = "ไม่สามรารถเพิ่มข้อมูลได้ เนื่องจากมีข้อมูล พนักงานรหัส " + code + " วันที่ " + odate.ToString("dd/MM/yyyy") + " ใน Database แล้ว";

                        return true;
                    }
                }
                else
                {
                    msg = "ไม่สามารถเพิ่มข้อมูลได้เนื่องจากไม่พบข้อมูลตารางกะพนักงาน";

                    return true;
                }*/
            }

            if (Result)
            {
                msg = "ไม่สามรารถเพิ่มข้อมูลได้ เนื่องจากมีข้อมูล พนักงานรหัส " + code + " วันที่ " + odate.ToString("dd/MM/yyyy") + " ใน Database แล้ว";
            }

            return Result;
        }
        private bool CheckOtExitTrainee(string code, DateTime odate, string rq, string otfrom, string otto, ref string msg)
        {
            ArrayList req = TraineeOtService.Instance().GetOTRequest(code, odate, "", "%");

            bool Result = false;

            if (req == null || req.Count == 0)
            {
                Result = false;

            }
            else
            {
                if (req.Count > 0)
                {
                    foreach (OtRequestInfo mReq in req)
                    {
                        if (code == mReq.EmpCode && rq == mReq.ReqId &&
                            odate.ToString("yyyy-MM-dd") == mReq.OtDate.ToString("yyyy-MM-dd"))
                        {

                            Result = true;
                            break;
                        }
                    }
                }
                /*
                //  EmployeeShiftInfo sh = shiftsrv.GetEmShift(code, odate.ToString("yyyyMM")); 
                string dsh = TraineeShiftService.Instance().GetEmShift(code, odate);
                if (dsh != null)
                {
                    if ((dsh == "H" || dsh == "T"))
                    {
                        string lsh = TraineeShiftService.Instance().GetEmShift(code, odate.AddDays(-1));
                        if (lsh != null)
                        {
                            if (lsh == "N")
                            {

                                if (!TraineeOtService.Instance().OtReqCheckExit(code, odate, otfrom, otto))
                                {
                                    return false;
                                }
                                else
                                {

                                    msg = "มีข้อมูลพนักงานรหัส " + code + " วันที่ " + odate.ToString("dd/MM/yyyy") + " เวลา " + otfrom + " - " + otto + " ใน Database แล้ว";
                                    return true;
                                }
                            }
                            else
                            {
                                msg = "ไม่สามรารถเพิ่มข้อมูลได้ เนื่องจาก พนักงานรหัส " + code + "ไม่ได้มีตารางกะ Night ในวันที่ " + odate.AddDays(-1).ToString("dd/MM/yyyy");
                                return true;
                            }
                        }
                        else
                        {
                            msg = "ไม่สามรารถเพิ่มข้อมูลได้ เนื่องจาก พนักงานรหัส " + code + "ไม่ได้มีตารางกะ Night ในวันที่ " + odate.AddDays(-1).ToString("dd/MM/yyyy");
                            return true;
                        }
                    }
                    else
                    {

                        msg = "ไม่สามรารถเพิ่มข้อมูลได้ เนื่องจากมีข้อมูล พนักงานรหัส " + code + " วันที่ " + odate.ToString("dd/MM/yyyy") + " ใน Database แล้ว";

                        return true;
                    }
                }
                else
                {
                    msg = "ไม่สามารถเพิ่มข้อมูลได้เนื่องจากไม่พบข้อมูลตารางกะพนักงาน";

                    return true;
                }*/


            }

            if (Result)
            {
                msg = "ไม่สามรารถเพิ่มข้อมูลได้ เนื่องจากมีข้อมูล พนักงานรหัส " + code + " วันที่ " + odate.ToString("dd/MM/yyyy") + " ใน Database แล้ว";
            }

            return Result;
        }
        private bool CheckOtExitSubcontract(string code, DateTime odate, string rq, string otfrom, string otto, ref string msg)
        {
            ArrayList req = SubContractOtService.Instance().GetOTRequest(code, odate, "", "%");

            bool Result = false;
            if (req == null || req.Count == 0)
            {
                Result = false;

            }
            else
            {

                if (req.Count > 0)
                {
                    foreach (OtRequestInfo mReq in req)
                    {
                        if (code == mReq.EmpCode && rq == mReq.ReqId &&
                            odate.ToString("yyyy-MM-dd") == mReq.OtDate.ToString("yyyy-MM-dd"))
                        {

                            Result = true;
                            break;
                        }
                    }
                }
                /*
                //  EmployeeShiftInfo sh = shiftsrv.GetEmShift(code, odate.ToString("yyyyMM")); 
                string dsh = SubContractShiftService.Instance().GetEmShift(code, odate);
                if (dsh != null)
                {
                    if ((dsh == "H" || dsh == "T"))
                    {
                        string lsh = SubContractShiftService.Instance().GetEmShift(code, odate.AddDays(-1));
                        if (lsh != null)
                        {
                            if (lsh == "N")
                            {

                                if (!SubContractOtService.Instance().OtReqCheckExit(code, odate, otfrom, otto))
                                {
                                    return false;
                                }
                                else
                                {

                                    msg = "มีข้อมูลพนักงานรหัส " + code + " วันที่ " + odate.ToString("dd/MM/yyyy") + " เวลา " + otfrom + " - " + otto + " ใน Database แล้ว";
                                    return true;
                                }
                            }
                            else
                            {
                                msg = "ไม่สามรารถเพิ่มข้อมูลได้ เนื่องจาก พนักงานรหัส " + code + "ไม่ได้มีตารางกะ Night ในวันที่ " + odate.AddDays(-1).ToString("dd/MM/yyyy");
                                return true;
                            }
                        }
                        else
                        {
                            msg = "ไม่สามรารถเพิ่มข้อมูลได้ เนื่องจาก พนักงานรหัส " + code + "ไม่ได้มีตารางกะ Night ในวันที่ " + odate.AddDays(-1).ToString("dd/MM/yyyy");
                            return true;
                        }
                    }
                    else
                    {

                        msg = "ไม่สามรารถเพิ่มข้อมูลได้ เนื่องจากมีข้อมูล พนักงานรหัส " + code + " วันที่ " + odate.ToString("dd/MM/yyyy") + " ใน Database แล้ว";

                        return true;
                    }
                }
                else
                {
                    msg = "ไม่สามารถเพิ่มข้อมูลได้เนื่องจากไม่พบข้อมูลตารางกะพนักงาน";

                    return true;
                }
                */
            }

            if (Result)
            {
                msg = "ไม่สามรารถเพิ่มข้อมูลได้ เนื่องจากมีข้อมูล พนักงานรหัส " + code + " วันที่ " + odate.ToString("dd/MM/yyyy") + " ใน Database แล้ว";
            }

            return Result;
        }
        #endregion


        private void kryptonButton2_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("This will generate Overtime of " + txtRqdate.Value.Date.ToString() + ".\nDo you want to continue?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Cursor = Cursors.WaitCursor;
                MonthShiftInfo mSh = shiftsrv.GetShift("D1", txtRqdate.Value.Date.ToString("yyyyMM"));
                //Check if Shift Master  is exits.

                if (mSh == null)
                {
                    MessageBox.Show("ไม่พบข้อมูลตารางกะ  Master", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                isAuto = true;
                string daySh = mSh.ShiftData.Substring(txtRqdate.Value.Date.Day - 1, 1);
                string msg = "";
                string errMsg = "";
                stsMgr.Progress = 0;
                //Check if Shift Master is day Shift.
                if (daySh == "D")
                {
                    if (rbnEmp.Checked)
                    {
                        ArrayList allEmp = empsrv.GetCurrentEmployees();
                        // EmployeeInfo inf = empsrv.Find("24974");
                        // allEmp.Add(inf);
                        stsMgr.MaxProgress = allEmp.Count;
                        foreach (EmployeeInfo emp in allEmp)
                        {
                            stsMgr.Status = "Calculate:" + emp.Code;
                            stsMgr.Progress++;
                            if (emp.EmployeeType == "E")
                            {                      //Check if leave.
                                ArrayList lvLs = lvrqSvr.GetAllLeave(emp.Code, txtRqdate.Value.Date, txtRqdate.Value, "%");
                                bool found = false;
                                if (lvLs != null)
                                {
                                    foreach (EmployeeLealeRequestInfo item in lvLs)
                                    {
                                        DateTime lvFrom = DateTime.Parse(item.LvFrom);
                                        DateTime lvTo = DateTime.Parse(item.LvTo);
                                        TimeSpan tt = lvTo - lvFrom;
                                        if (tt.TotalHours > 4)
                                        {
                                            found = true;
                                            break;
                                        }

                                    }
                                }
                                if (!found)
                                //  if (lvrqSvr.GetAllLeave(emp.Code, txtRqdate.Value.Date, txtRqdate.Value.Date, "%") == null)
                                {
                                    string sh = shSvr.GetEmShift(emp.Code, txtRqdate.Value.Date);
                                    OtRateInfo otRate = new OtRateInfo();
                                    OtRequestInfo otReq = new OtRequestInfo();
                                    if (sh == "D" || sh == "N")
                                    {
                                        if (sh == "D" && chkOtDay.Checked)
                                        {
                                            //Check if there is timecard in.
                                            EmployeeWorkTimeInfo empWk = tmSvr.GetEmployeeWorkingHour(emp.Code, txtRqdate.Value.Date);

                                            if (empWk.WorkFrom != DateTime.MinValue)
                                            {
                                                //Add Overtime Rate A (18:15-20:00) .
                                                otRate = otsvr.GetRate("A", emp.WorkType);
                                            }
                                            else
                                            {
                                                continue;
                                            }
                                            otReq.OtDate = txtRqdate.Value.Date;
                                        }
                                        else if (sh == "N" && chkOtNight.Checked)
                                        {
                                            //Add Overtime Rate D (06:05-07:50).

                                            otRate = otsvr.GetRate("D", emp.WorkType);
                                            otReq.OtDate = txtRqdate.Value.Date.AddDays(1);
                                        }
                                        else
                                        {
                                            continue;
                                        }


                                        otReq.EmpCode = emp.Code;
                                        //otReq.JobType = cmbJobType.SelectedItem.ToString();
                                        otReq.JobType = cmbJobType.SelectedValue.ToString();
                                        otReq.ReqId = txtReqId.Text;
                                        otReq.EmpType = emp.WorkType;
                                        //inform = new ObjectInfo();
                                        //inform.CreateBy = appMgr.UserAccount.AccountId;
                                        otReq.OtFrom = otRate.OtFrom;
                                        otReq.OtTo = otRate.OtTo;
                                        otReq.Rate1 = otRate.Rate1;
                                        otReq.Rate15 = otRate.Rate15;
                                        otReq.Rate2 = otRate.Rate2;
                                        otReq.Rate3 = otRate.Rate3;
                                        otReq.Rate1From = otRate.Rate1From;
                                        otReq.Rate15From = otRate.Rate15From;
                                        otReq.Rate2From = otRate.Rate2From;
                                        otReq.Rate3From = otRate.Rate3From;
                                        otReq.Rate1To = otRate.Rate1To;
                                        otReq.Rate15To = otRate.Rate15To;
                                        otReq.Rate2To = otRate.Rate2To;
                                        otReq.Rate3To = otRate.Rate3To;

                                        if (!CheckOtExit(otReq.EmpCode, otReq.OtDate, otReq.ReqId, otReq.OtFrom, otReq.OtTo, ref msg))
                                        {
                                            //MessageBox.Show("มีข้อมูลพนักงานรหัส " + textBox1.Text + " ReqId " + txtReqId.Text + " ใน Database แล้ว", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                            try
                                            {
                                                otReq.DocId = otsvr.LoadRecordKey();
                                                this.Information = otReq;
                                                this.Save();
                                            }
                                            catch (Exception ex)
                                            {
                                                errMsg += otReq.EmpCode + ":" + ex.Message + "\n";
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        stsMgr.Progress = 0;
                    }
                    if (rbnTrainee.Checked)
                    {
                        ArrayList tnLs = TraineeService.Instance().GetCurrentEmployees();
                        if (tnLs != null)
                        {
                            stsMgr.MaxProgress = tnLs.Count;
                            foreach (EmployeeInfo tnItem in tnLs)
                            {
                                stsMgr.Status = "Calculate:" + tnItem.Code;
                                stsMgr.Progress++;
                                // if (tnItem.EmployeeType == "E")
                                {
                                    //Check if leave.
                                    ArrayList lvLs = TraineeLeaveService.Instance().GetAllLeave(tnItem.Code, txtRqdate.Value.Date, txtRqdate.Value, "%");
                                    bool found = false;
                                    if (lvLs != null)
                                    {
                                        foreach (EmployeeLealeRequestInfo item in lvLs)
                                        {
                                            DateTime lvFrom = DateTime.Parse(item.LvFrom);
                                            DateTime lvTo = DateTime.Parse(item.LvTo);
                                            TimeSpan tt = lvTo - lvFrom;
                                            if (tt.TotalHours > 4)
                                            {
                                                found = true;
                                                break;
                                            }

                                        }
                                    }
                                    if (!found)
                                    {
                                        string sh = TraineeShiftService.Instance().GetEmShift(tnItem.Code, txtRqdate.Value);
                                        OtRateInfo otRate = new OtRateInfo();
                                        OtRequestInfo otReq = new OtRequestInfo();
                                        if (sh == "D" || sh == "N")
                                        {
                                            if (sh == "D" && chkOtDay.Checked)
                                            {
                                                //Check if there is timecard in.
                                                EmployeeWorkTimeInfo empWk = TraineeTimeCardService.Instance().GetEmployeeWorkingHour(tnItem.Code, txtRqdate.Value);

                                                if (empWk.WorkFrom != DateTime.MinValue)
                                                {
                                                    //Add Overtime Rate A (18:15-20:00) .
                                                    otRate = otsvr.GetRate("A", tnItem.WorkType);
                                                }
                                                else
                                                {
                                                    continue;
                                                }
                                                otReq.OtDate = txtRqdate.Value;
                                            }
                                            else if (sh == "N" && chkOtNight.Checked)
                                            {
                                                //Add Overtime Rate D (06:05-07:50).

                                                otRate = otsvr.GetRate("D", tnItem.WorkType);
                                                otReq.OtDate = txtRqdate.Value.AddDays(1);
                                            }
                                            else
                                            {
                                                continue;
                                            }

                                            otReq.EmpCode = tnItem.Code;
                                            //otReq.JobType = cmbJobType.SelectedItem.ToString(); 
                                            otReq.JobType = cmbJobType.SelectedValue.ToString();
                                            otReq.ReqId = txtReqId.Text;
                                            otReq.EmpType = tnItem.WorkType;
                                            //inform = new ObjectInfo();
                                            //inform.CreateBy = appMgr.UserAccount.AccountId;
                                            otReq.OtFrom = otRate.OtFrom;
                                            otReq.OtTo = otRate.OtTo;
                                            otReq.Rate1 = otRate.Rate1;
                                            otReq.Rate15 = otRate.Rate15;
                                            otReq.Rate2 = otRate.Rate2;
                                            otReq.Rate3 = otRate.Rate3;
                                            otReq.Rate1From = otRate.Rate1From;
                                            otReq.Rate15From = otRate.Rate15From;
                                            otReq.Rate2From = otRate.Rate2From;
                                            otReq.Rate3From = otRate.Rate3From;
                                            otReq.Rate1To = otRate.Rate1To;
                                            otReq.Rate15To = otRate.Rate15To;
                                            otReq.Rate2To = otRate.Rate2To;
                                            otReq.Rate3To = otRate.Rate3To;

                                            if (!CheckOtExitTrainee(otReq.EmpCode, otReq.OtDate, otReq.ReqId, otReq.OtFrom, otReq.OtTo, ref msg))
                                            {
                                                //MessageBox.Show("มีข้อมูลพนักงานรหัส " + textBox1.Text + " ReqId " + txtReqId.Text + " ใน Database แล้ว", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                                try
                                                {
                                                    // otReq.DocId = otsvr.LoadRecordKey();
                                                    this.Information = otReq;
                                                    this.Save();


                                                }
                                                catch (Exception ex)
                                                {
                                                    errMsg += otReq.EmpCode + ":" + ex.Message + "\n";
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }



                        stsMgr.Progress = 0;
                    }
                    if (rbnSub.Checked)
                    {
                        ArrayList scLs = SubContractService.Instance().GetCurrentEmployees();
                        if (scLs != null)
                        {
                            stsMgr.MaxProgress = scLs.Count;
                            foreach (EmployeeInfo tnItem in scLs)
                            {
                                stsMgr.Status = "Calculate:" + tnItem.Code;
                                stsMgr.Progress++;
                                // if (tnItem.EmployeeType == "E")
                                {
                                    //Check if leave.
                                    ArrayList lvLs = SubContractLeaveService.Instance().GetAllLeave(tnItem.Code, txtRqdate.Value.Date, txtRqdate.Value, "%");
                                    bool found = false;
                                    if (lvLs != null)
                                    {
                                        foreach (EmployeeLealeRequestInfo item in lvLs)
                                        {
                                            DateTime lvFrom = DateTime.Parse(item.LvFrom);
                                            DateTime lvTo = DateTime.Parse(item.LvTo);
                                            TimeSpan tt = lvTo - lvFrom;
                                            if (tt.TotalHours > 4)
                                            {
                                                found = true;
                                                break;
                                            }

                                        }
                                    }
                                    if (!found)
                                    {
                                        string sh = SubContractShiftService.Instance().GetEmShift(tnItem.Code, txtRqdate.Value);
                                        OtRateInfo otRate = new OtRateInfo();
                                        OtRequestInfo otReq = new OtRequestInfo();
                                        if (sh == "D" || sh == "N")
                                        {
                                            if (sh == "D" && chkOtDay.Checked)
                                            {
                                                //Check if there is timecard in.
                                                EmployeeWorkTimeInfo empWk = SubContractTimeCardService.Instance().GetEmployeeWorkingHour(tnItem.Code, txtRqdate.Value);

                                                if (empWk.WorkFrom != DateTime.MinValue)
                                                {
                                                    //Add Overtime Rate A (18:15-20:00) .
                                                    otRate = otsvr.GetRate("A", tnItem.WorkType);
                                                }
                                                else
                                                {
                                                    continue;
                                                }
                                                otReq.OtDate = txtRqdate.Value;
                                            }
                                            else if (sh == "N" && chkOtNight.Checked)
                                            {
                                                //Add Overtime Rate D (06:05-07:50).

                                                otRate = otsvr.GetRate("D", tnItem.WorkType);
                                                otReq.OtDate = txtRqdate.Value.AddDays(1);
                                            }
                                            else
                                            {
                                                continue;
                                            }

                                            otReq.EmpCode = tnItem.Code;
                                            //otReq.JobType = cmbJobType.SelectedItem.ToString(); }
                                            otReq.JobType = cmbJobType.SelectedValue.ToString(); }
                                            otReq.ReqId = txtReqId.Text;
                                            otReq.EmpType = tnItem.WorkType;
                                            //inform = new ObjectInfo();
                                            //inform.CreateBy = appMgr.UserAccount.AccountId;
                                            otReq.OtFrom = otRate.OtFrom;
                                            otReq.OtTo = otRate.OtTo;
                                            otReq.Rate1 = otRate.Rate1;
                                            otReq.Rate15 = otRate.Rate15;
                                            otReq.Rate2 = otRate.Rate2;
                                            otReq.Rate3 = otRate.Rate3;
                                            otReq.Rate1From = otRate.Rate1From;
                                            otReq.Rate15From = otRate.Rate15From;
                                            otReq.Rate2From = otRate.Rate2From;
                                            otReq.Rate3From = otRate.Rate3From;
                                            otReq.Rate1To = otRate.Rate1To;
                                            otReq.Rate15To = otRate.Rate15To;
                                            otReq.Rate2To = otRate.Rate2To;
                                            otReq.Rate3To = otRate.Rate3To;

                                            if (!CheckOtExitSubcontract(otReq.EmpCode, otReq.OtDate, otReq.ReqId, otReq.OtFrom, otReq.OtTo, ref msg))
                                            {
                                                //MessageBox.Show("มีข้อมูลพนักงานรหัส " + textBox1.Text + " ReqId " + txtReqId.Text + " ใน Database แล้ว", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                                try
                                                {
                                                    // otReq.DocId = otsvr.LoadRecordKey();
                                                    this.Information = otReq;
                                                    this.Save();


                                                }
                                                catch (Exception ex)
                                                {
                                                    errMsg += otReq.EmpCode + ":" + ex.Message + "\n";
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }


                    if (errMsg != "")
                    {
                        MessageBox.Show(errMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                }
                else
                {
                    MessageBox.Show("You can not generate Overtime of " + txtRqdate.Value.ToString() + " because it is holiday", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                isAuto = false;
                FillDataGrid();
                this.Cursor = Cursors.Default;
                stsMgr.Status = "Ready";
                stsMgr.Progress = 0;
            }


        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            if (otCal.IsDisposed)
            {
                otCal = new FrmOvertimeCalculate();
            }
            otCal.MdiParent = this.MdiParent;
            otCal.Show();
        }


        SqlConnectDB oSqlHRM = new SqlConnectDB("dbHRM");
        SqlConnectDB oSqlDCI = new SqlConnectDB("dbDCI");
        private void InsertOTRQ_REQ(OtRequestInfo otReq)
        { 
            DataTable dtEmp = new DataTable();
            string strEmp = @"SELECT * FROM Employee WHERE CODE=@CODE";
            SqlCommand cmdEmp = new SqlCommand();
            cmdEmp.CommandText = strEmp;
            cmdEmp.Parameters.Add(new SqlParameter("@CODE", otReq.EmpCode));
            dtEmp = oSqlDCI.Query(cmdEmp);

            string _DVCD = "", _BUS = "", _STOP = "", _WTYPE = "";
            if(dtEmp.Rows.Count > 0){
                _DVCD = dtEmp.Rows[0]["DVCD"].ToString();
                _BUS = dtEmp.Rows[0]["BUS"].ToString(); 
                _STOP = dtEmp.Rows[0]["STOP"].ToString(); 
                _WTYPE = dtEmp.Rows[0]["WTYPE"].ToString();
            }


            DataTable dtCheck = new DataTable();
            string strCheck = @"SELECT CODE,REQ_STATUS,ProgBit FROM OTRQ_REQ WHERE CODE=@CODE AND ODATE=@ODATE AND RQ=@RQ ";
            SqlCommand cmdCheck = new SqlCommand();
            cmdCheck.CommandText = strCheck;
            cmdCheck.Parameters.Add(new SqlParameter("@CODE", otReq.EmpCode));
            cmdCheck.Parameters.Add(new SqlParameter("@ODATE", otReq.OtDate.ToString("yyyy-MM-dd")));
            cmdCheck.Parameters.Add(new SqlParameter("@RQ", otReq.ReqId));
            dtCheck = oSqlHRM.Query(cmdCheck);

            if (dtCheck.Rows.Count == 0)
            {
                /*
                    A = DAY     18:15 - 20:00 
                    D = NIGHT   06:05 - 07:50
                    E = DAY     08:00 - 17:45 (Holiday)
                    F = DAY     08:00 - 20:00 (Holiday)
                    J = NIGHT   20:00 - 05:35 (Holiday) 
                    K = NIGHT   20:00 - 07:50 (Holiday)
                */
                string _Shift = "";
                if (otReq.OtFrom == "18:15" && otReq.OtTo == "20:00")
                {
                    _Shift = "D";
                }
                else if (otReq.OtFrom == "06:05" && otReq.OtTo == "07:50")
                {
                    _Shift = "N";
                }
                else if (otReq.OtFrom == "08:00")
                {
                    _Shift = "HD";
                }
                else if (otReq.OtFrom == "20:00")
                {
                    _Shift = "HN";
                }

                //***********************************************************
                //                  INSERT OTRQ_REQ
                //***********************************************************
                string strInstr = @"INSERT INTO [OTRQ_REQ] ([ODATE],[RQ],[CODE],[BUS],[STOP],[OTFROM],[OTTO],[OT15],[OT1],[OT2],[OT3],
                                    [DVCD],[FBUS],[JOB],[SECT],[N15],[N1],[N2],[N3],[RES],[NFROM],[NTO],[STS],[CR_BY],[CR_DT],
                                    [UPD_BY],[UPD_DT],[DOC_ID],[OT1FROM],[OT1TO],[OT15FROM],[OT2FROM],[OT2TO],[OT3FROM],[OT3TO],
                                    [OT15TO],[WTYPE],[APPROVE_BY],[APPROVE_DT],[REQ_STATUS],[SHIFT],ProgBit,Remark) VALUES (@ODATE,@RQ,@CODE,@BUS,
                                @STOP,@OTFROM,@OTTO,@OT15,@OT1,@OT2,@OT3,@DVCD,@FBUS,@JOB,@SECT,@N15,@N1,@N2,@N3,@RES,@NFROM,@NTO,@STS,
                                @CR_BY,@CR_DT,@UPD_BY,@UPD_DT,@DOC_ID,@OT1FROM,@OT1TO,@OT15FROM,@OT2FROM,@OT2TO,@OT3FROM,@OT3TO,@OT15TO,
                                @WTYPE,@APPROVE_BY,@APPROVE_DT,@REQ_STATUS,@SHIFT,@ProgBit,@Remark)";
                SqlCommand cmdInstr = new SqlCommand();
                cmdInstr.CommandText = strInstr;
                cmdInstr.Parameters.Add(new SqlParameter("@ODATE", otReq.OtDate.ToString("yyyy-MM-dd")));
                cmdInstr.Parameters.Add(new SqlParameter("@RQ", otReq.ReqId));
                cmdInstr.Parameters.Add(new SqlParameter("@CODE", otReq.EmpCode));
                cmdInstr.Parameters.Add(new SqlParameter("@BUS", _BUS));
                cmdInstr.Parameters.Add(new SqlParameter("@STOP", _STOP));
                cmdInstr.Parameters.Add(new SqlParameter("@OTFROM", otReq.OtFrom));
                cmdInstr.Parameters.Add(new SqlParameter("@OTTO", otReq.OtTo));
                cmdInstr.Parameters.Add(new SqlParameter("@OT15", otReq.Rate15));
                cmdInstr.Parameters.Add(new SqlParameter("@OT1", otReq.Rate1));
                cmdInstr.Parameters.Add(new SqlParameter("@OT2", otReq.Rate2));
                cmdInstr.Parameters.Add(new SqlParameter("@OT3", otReq.Rate3));
                cmdInstr.Parameters.Add(new SqlParameter("@DVCD", otReq.Dvcd));
                cmdInstr.Parameters.Add(new SqlParameter("@FBUS", "OT_ADDITION"));
                cmdInstr.Parameters.Add(new SqlParameter("@JOB", otReq.JobType));
                cmdInstr.Parameters.Add(new SqlParameter("@SECT", _DVCD));
                cmdInstr.Parameters.Add(new SqlParameter("@N15", otReq.N15));
                cmdInstr.Parameters.Add(new SqlParameter("@N1", otReq.N1));
                cmdInstr.Parameters.Add(new SqlParameter("@N2", otReq.N2));
                cmdInstr.Parameters.Add(new SqlParameter("@N3", otReq.N3));
                cmdInstr.Parameters.Add(new SqlParameter("@RES", otReq.TimeCard));
                cmdInstr.Parameters.Add(new SqlParameter("@NFROM", otReq.NFrom));
                cmdInstr.Parameters.Add(new SqlParameter("@NTO", otReq.NTo));
                cmdInstr.Parameters.Add(new SqlParameter("@STS", otReq.CalRest));
                cmdInstr.Parameters.Add(new SqlParameter("@CR_BY", otReq.LastUpdateBy));
                cmdInstr.Parameters.Add(new SqlParameter("@CR_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                cmdInstr.Parameters.Add(new SqlParameter("@UPD_BY", otReq.LastUpdateBy));
                cmdInstr.Parameters.Add(new SqlParameter("@UPD_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                cmdInstr.Parameters.Add(new SqlParameter("@DOC_ID", otReq.DocId));
                cmdInstr.Parameters.Add(new SqlParameter("@OT1FROM", otReq.Rate1From));
                cmdInstr.Parameters.Add(new SqlParameter("@OT1TO", otReq.Rate1To));
                cmdInstr.Parameters.Add(new SqlParameter("@OT15FROM", otReq.Rate15From));
                cmdInstr.Parameters.Add(new SqlParameter("@OT2FROM", otReq.Rate2From));
                cmdInstr.Parameters.Add(new SqlParameter("@OT2TO", otReq.Rate2To));
                cmdInstr.Parameters.Add(new SqlParameter("@OT3FROM", otReq.Rate3From));
                cmdInstr.Parameters.Add(new SqlParameter("@OT3TO", otReq.Rate3To));
                cmdInstr.Parameters.Add(new SqlParameter("@OT15TO", otReq.Rate15To));
                cmdInstr.Parameters.Add(new SqlParameter("@WTYPE", _WTYPE));
                cmdInstr.Parameters.Add(new SqlParameter("@APPROVE_BY", otReq.LastUpdateBy));
                cmdInstr.Parameters.Add(new SqlParameter("@APPROVE_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                cmdInstr.Parameters.Add(new SqlParameter("@REQ_STATUS", "APPROVE"));
                cmdInstr.Parameters.Add(new SqlParameter("@SHIFT", _Shift));
                cmdInstr.Parameters.Add(new SqlParameter("@ProgBit", "M"));
                cmdInstr.Parameters.Add(new SqlParameter("@Remark", otReq.OtRemark));
                oSqlHRM.ExecuteCommand(cmdInstr);

            }
            else {
                string FBus = "";
                if ((dtCheck.Rows[0]["REQ_STATUS"].ToString() == "REJECT" || dtCheck.Rows[0]["REQ_STATUS"].ToString() == "REQUEST") &&
                    (dtCheck.Rows[0]["ProgBit"].ToString() == "U" || dtCheck.Rows[0]["ProgBit"].ToString() == "F"))
                {
                    FBus = "OT_ADDITION";
                }
                string strUpd = @"UPDATE OTRQ_REQ SET FBUS=@FBUS, APPROVE_BY=@APPROVE_BY, APPROVE_DT=@APPROVE_DT, 
                                    OTFROM=@OTFROM,OTTO=@OTTO,OT15=@OT15,OT1=@OT1,OT2=@OT2, OT3=@OT3, N15=@N15, N1=@N1, 
                                    N2=@N2, N3=@N3, NFROM=@NFROM, NTO=@NTO, OT1FROM=@OT1FROM, OT1TO=@OT1TO, OT15FROM=@OT15FROM, 
                                    OT2FROM=@OT2FROM, OT2TO=@OT2TO, OT3FROM=@OT3FROM, OT3TO=@OT3TO, OT15TO=@OT15TO, 
                                    REQ_STATUS=@REQ_STATUS, ProgBit=@ProgBit, Remark=@Remark 
                                 WHERE CODE=@CODE AND ODATE=@ODATE AND RQ=@RQ ";
                SqlCommand cmdUpd = new SqlCommand();
                cmdUpd.CommandText = strUpd;
                cmdUpd.Parameters.Add(new SqlParameter("@FBUS", FBus));
                cmdUpd.Parameters.Add(new SqlParameter("@APPROVE_BY", otReq.CreateBy));
                cmdUpd.Parameters.Add(new SqlParameter("@APPROVE_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));

                cmdUpd.Parameters.Add(new SqlParameter("@OTFROM", otReq.OtFrom));
                cmdUpd.Parameters.Add(new SqlParameter("@OTTO", otReq.OtTo));
                cmdUpd.Parameters.Add(new SqlParameter("@OT15", otReq.Rate15));
                cmdUpd.Parameters.Add(new SqlParameter("@OT1", otReq.Rate1));
                cmdUpd.Parameters.Add(new SqlParameter("@OT2", otReq.Rate2));
                cmdUpd.Parameters.Add(new SqlParameter("@OT3", otReq.Rate3));
                cmdUpd.Parameters.Add(new SqlParameter("@N15", otReq.N15));
                cmdUpd.Parameters.Add(new SqlParameter("@N1", otReq.N1));
                cmdUpd.Parameters.Add(new SqlParameter("@N2", otReq.N2));
                cmdUpd.Parameters.Add(new SqlParameter("@N3", otReq.N3));
                cmdUpd.Parameters.Add(new SqlParameter("@NFROM", otReq.NFrom));
                cmdUpd.Parameters.Add(new SqlParameter("@NTO", otReq.NTo));
                cmdUpd.Parameters.Add(new SqlParameter("@OT1FROM", otReq.Rate1From));
                cmdUpd.Parameters.Add(new SqlParameter("@OT1TO", otReq.Rate1To));
                cmdUpd.Parameters.Add(new SqlParameter("@OT15FROM", otReq.Rate15From));
                cmdUpd.Parameters.Add(new SqlParameter("@OT2FROM", otReq.Rate2From));
                cmdUpd.Parameters.Add(new SqlParameter("@OT2TO", otReq.Rate2To));
                cmdUpd.Parameters.Add(new SqlParameter("@OT3FROM", otReq.Rate3From));
                cmdUpd.Parameters.Add(new SqlParameter("@OT3TO", otReq.Rate3To));
                cmdUpd.Parameters.Add(new SqlParameter("@OT15TO", otReq.Rate15To));
                cmdUpd.Parameters.Add(new SqlParameter("@Remark", otReq.OtRemark));

                cmdUpd.Parameters.Add(new SqlParameter("@REQ_STATUS", "APPROVE"));
                cmdUpd.Parameters.Add(new SqlParameter("@ProgBit", "M"));
                cmdUpd.Parameters.Add(new SqlParameter("@CODE", otReq.EmpCode));
                cmdUpd.Parameters.Add(new SqlParameter("@ODATE", otReq.OtDate.ToString("yyyy-MM-dd")));
                cmdUpd.Parameters.Add(new SqlParameter("@RQ", otReq.ReqId));
                oSqlHRM.ExecuteCommand(cmdUpd);
            }

            //***********************************************************
            //                  INSERT LOG
            //***********************************************************
            Random r = new Random();
            int n = r.Next(1,999);               
            string strLog = @"INSERT INTO OTRQ_LOG (NBR,ODATE,CODE,RQ,DATA_ACTION,DATA_DATE,DATA_REMARK) VALUES (@NBR,@ODATE,@CODE,@RQ,@DATA_ACTION,@DATA_DATE,@DATA_REMARK) ";
            SqlCommand cmdLog = new SqlCommand();
            cmdLog.CommandText = strLog;
            cmdLog.Parameters.Add(new SqlParameter("@NBR", DateTime.Now.ToString("yyyyMMddHHmmss") + n.ToString("D3")));
            cmdLog.Parameters.Add(new SqlParameter("@ODATE", otReq.OtDate.ToString("yyyy-MM-dd")));
            cmdLog.Parameters.Add(new SqlParameter("@CODE", otReq.EmpCode));
            cmdLog.Parameters.Add(new SqlParameter("@RQ", otReq.ReqId));
            cmdLog.Parameters.Add(new SqlParameter("@DATA_ACTION", "OT_APPROVE"));
            cmdLog.Parameters.Add(new SqlParameter("@DATA_DATE", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            cmdLog.Parameters.Add(new SqlParameter("@DATA_REMARK", "HR INPUT : "+otReq.CreateBy));
            oSqlHRM.ExecuteCommand(cmdLog);
        
        }



        private void DeleteOTRQ_REQ(OtRequestInfo otReq)
        {
            DataTable dtCheck = new DataTable();
            string strCheck = @"SELECT CODE,REQ_STATUS,ProgBit FROM OTRQ_REQ WHERE CODE=@CODE AND ODATE=@ODATE AND RQ=@RQ ";
            SqlCommand cmdCheck = new SqlCommand();
            cmdCheck.CommandText = strCheck;
            cmdCheck.Parameters.Add(new SqlParameter("@CODE", otReq.EmpCode));
            cmdCheck.Parameters.Add(new SqlParameter("@ODATE", otReq.OtDate.ToString("yyyy-MM-dd")));
            cmdCheck.Parameters.Add(new SqlParameter("@RQ", otReq.ReqId));
            dtCheck = oSqlHRM.Query(cmdCheck);

            if (dtCheck.Rows.Count > 0)
            {
                string FBus = "";
                if ((dtCheck.Rows[0]["REQ_STATUS"].ToString() == "APPROVE") &&
                    (dtCheck.Rows[0]["ProgBit"].ToString() == "F"))
                {
                    FBus = "OT_ABSENT";
                }

                string strUpd = @"UPDATE OTRQ_REQ SET FBUS=@FBUS, APPROVE_BY=@APPROVE_BY, APPROVE_DT=@APPROVE_DT, REQ_STATUS=@REQ_STATUS, ProgBit=@ProgBit WHERE CODE=@CODE AND ODATE=@ODATE AND RQ=@RQ ";
                SqlCommand cmdUpd = new SqlCommand();
                cmdUpd.CommandText = strUpd;
                cmdUpd.Parameters.Add(new SqlParameter("@FBUS", FBus));
                cmdUpd.Parameters.Add(new SqlParameter("@APPROVE_BY", otReq.LastUpdateBy));
                cmdUpd.Parameters.Add(new SqlParameter("@APPROVE_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                cmdUpd.Parameters.Add(new SqlParameter("@REQ_STATUS", "REJECT"));
                cmdUpd.Parameters.Add(new SqlParameter("@ProgBit", "M"));
                cmdUpd.Parameters.Add(new SqlParameter("@CODE", otReq.EmpCode));
                cmdUpd.Parameters.Add(new SqlParameter("@ODATE", otReq.OtDate.ToString("yyyy-MM-dd")));
                cmdUpd.Parameters.Add(new SqlParameter("@RQ", otReq.ReqId));
                oSqlHRM.ExecuteCommand(cmdUpd);
            }
           

            //***********************************************************
            //                  INSERT LOG
            //***********************************************************
            Random r = new Random();
            int n = r.Next(1, 999);
            string strLog = @"INSERT INTO OTRQ_LOG (NBR,ODATE,CODE,RQ,DATA_ACTION,DATA_DATE,DATA_REMARK) VALUES (@NBR,@ODATE,@CODE,@RQ,@DATA_ACTION,@DATA_DATE,@DATA_REMARK) ";
            SqlCommand cmdLog = new SqlCommand();
            cmdLog.CommandText = strLog;
            cmdLog.Parameters.Add(new SqlParameter("@NBR", DateTime.Now.ToString("yyyyMMddHHmmss") + n.ToString("D3")));
            cmdLog.Parameters.Add(new SqlParameter("@ODATE", otReq.OtDate.ToString("yyyy-MM-dd")));
            cmdLog.Parameters.Add(new SqlParameter("@CODE", otReq.EmpCode));
            cmdLog.Parameters.Add(new SqlParameter("@RQ", otReq.ReqId));
            cmdLog.Parameters.Add(new SqlParameter("@DATA_ACTION", "OT_DELETE"));
            cmdLog.Parameters.Add(new SqlParameter("@DATA_DATE", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            cmdLog.Parameters.Add(new SqlParameter("@DATA_REMARK", "HR INPUT : " + otReq.LastUpdateBy));
            oSqlHRM.ExecuteCommand(cmdLog);

        }



        private void btnExport_Click(object sender, EventArgs e)
        {

            DataTable dtOT = new DataTable();
            string empcode = "", empType = "", posit = "", reqid = "", dvcd = "", otfrom = "", otto = "", grpl = "", grpot = "", otremark = "";
            DateTime odate = new DateTime();
            DateTime odateto = new DateTime();

            empcode = txtSCode.Text.Trim() == "" ? "%" : txtSCode.Text.Trim();
            empType = cboWType.SelectedItem.ToString().Trim() == "" ? "%" : cboWType.SelectedItem.ToString().Trim();
            posit = cboPosition.SelectedValue.ToString().Trim() == "" ? "%" : cboPosition.SelectedValue.ToString().Trim();

            odate = dtpSDate.Value.Date;
            odateto = dtpTDate.Value.Date;

            reqid = txtSreq.Text.Trim() == "" ? "%" : txtSreq.Text.Trim();
            dvcd = cmbDvcd.SelectedValue.ToString().Trim() == "" ? "%" : cmbDvcd.SelectedValue.ToString().Trim();
            otfrom = txtSFrom.Text.Trim() == "" ? "%" : txtSFrom.Text.Trim();
            otto = txtSTo.Text.Trim() == "" ? "%" : txtSTo.Text.Trim();
            grpl = cmbGrpl.Text.Trim() == "" ? "%" : cmbGrpl.Text.Trim();
            grpot = cmbGrpot.Text.Trim() == "" ? "%" : cmbGrpot.Text.Trim();
            otremark = cmbSrcOTType.SelectedValue.ToString().Trim() == "" ? "%" : cmbSrcOTType.SelectedValue.ToString().Trim();

            string strOT = @"SELECT * FROM (
                                SELECT ODATE, RQ, r.CODE, e.NAME||' '||e.SURN EmpName, r.DVCD, e.GrpOT, e.WTYPE, r.JOB, r.BUS, r.STOP, 
                                       r.OTFROM, r.OTTO, r.OT1, r.OT15, r.OT2, r.OT3, STS Result, Res TimeCard, r.CR_DT AddBy, 
                                       r.CR_DT AddDate, r.UPD_BY UpdateBy, r.UPD_DT UpdateDate, Remark OTType 
                                FROM otrq r
                                LEFT JOIN empm e ON e.code = r.code
                                WHERE r.code LIKE '"+ empcode + @"'
                                    AND r.odate BETWEEN '"+ odate.ToString("dd/MMM/yyyy") + "' AND '"+ odateto.ToString("dd/MMM/yyyy") + @"'
                                    AND NVL (r.rq, '') LIKE '"+ reqid + @"'
                                    AND r.sect LIKE '"+ dvcd + @"'
                                    AND NVL (r.otfrom, '') LIKE '"+ otfrom + @"'
                                    AND NVL (r.otto, '') LIKE '"+ otto + @"'
                                    AND NVL (r.remark, ' ') LIKE '"+ otremark + @"' 
       
                                   UNION ALL
       
                                SELECT ODATE, RQ, r.CODE, e.NAME||' '||e.SURN EmpName, r.DVCD, e.GrpOT, e.WTYPE, r.JOB, r.BUS, r.STOP, 
                                       r.OTFROM, r.OTTO, r.OT1, r.OT15, r.OT2, r.OT3, STS Result, Res TimeCard, r.CR_DT AddBy, 
                                       r.CR_DT AddDate, r.UPD_BY UpdateBy, r.UPD_DT UpdateDate, Remark OTType 
                                FROM DCITC.otrq r
                                LEFT JOIN DCITC.empm e ON e.code = r.code
                                WHERE r.code LIKE '" + empcode + @"'
                                    AND r.odate BETWEEN '"+ odate.ToString("dd/MMM/yyyy") + "' AND '"+ odateto.ToString("dd/MMM/yyyy") + @"'
                                    AND NVL (r.rq, '') LIKE '"+ reqid + @"'
                                    AND r.sect LIKE '"+ dvcd + @"'
                                    AND NVL (r.otfrom, '') LIKE '"+ otfrom + @"'
                                    AND NVL (r.otto, '') LIKE '"+ otto + @"'
                                    AND NVL (r.remark, ' ') LIKE '"+ otremark + @"' 
       
                                   UNION ALL
          
                                SELECT ODATE, RQ, r.CODE, e.NAME||' '||e.SURN EmpName, r.DVCD, e.GrpOT, e.WTYPE, r.JOB, r.BUS, r.STOP, 
                                       r.OTFROM, r.OTTO, r.OT1, r.OT15, r.OT2, r.OT3, STS Result, Res TimeCard, r.CR_DT AddBy, 
                                       r.CR_DT AddDate, r.UPD_BY UpdateBy, r.UPD_DT UpdateDate, Remark OTType 
                                FROM DEV_OFFICE.otrq r
                                LEFT JOIN DEV_OFFICE.empm e ON e.code = r.code
                                WHERE r.code LIKE '" + empcode + @"'
                                    AND r.odate BETWEEN '"+ odate.ToString("dd/MMM/yyyy") + "' AND '"+ odateto.ToString("dd/MMM/yyyy") + @"'
                                    AND NVL (r.rq, '') LIKE '"+ reqid + @"'
                                    AND r.sect LIKE '"+ dvcd + @"'
                                    AND NVL (r.otfrom, '') LIKE '"+ otfrom + @"'
                                    AND NVL (r.otto, '') LIKE '"+ otto + @"'
                                    AND NVL (r.remark, ' ') LIKE '"+ otremark + @"'        
                            )   ";
            OracleCommand cmdOT = new OracleCommand();
            cmdOT.CommandText = strOT;
            dtOT = oOraDCI.Query(cmdOT);


            if (dtOT.Rows.Count > 0)
            {

                saveFileDlg.FileName = "";
                saveFileDlg.RestoreDirectory = true;
                //saveFileDlg.Filter = "Excel Files (.xlsx)|*.xlsx;";
                saveFileDlg.Filter = "CSV Files (.csv)|*.csv;";
                DialogResult dlg = saveFileDlg.ShowDialog();
                if (dlg == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    if (File.Exists(saveFileDlg.FileName))
                    {
                        File.Delete(saveFileDlg.FileName);
                    }

                    FileInfo fileInfo = new FileInfo(saveFileDlg.FileName);

                    //MessageBox.Show(saveFileDlg.FileName);
                    CreateCSVFile(dtOT, saveFileDlg.FileName);
                    MessageBox.Show("Success");
                }
            }
            else
            {
                MessageBox.Show("No data export");
            }

            /*
            if (dgItems.Rows.Count > 0)
            {

                saveFileDlg.FileName = "";
                saveFileDlg.RestoreDirectory = true;
                saveFileDlg.Filter = "Excel Files (.xlsx)|*.xlsx;";
                DialogResult dlg = saveFileDlg.ShowDialog();
                if (dlg == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    if (File.Exists(saveFileDlg.FileName))
                    {
                        File.Delete(saveFileDlg.FileName);
                    }

                    FileInfo fileInfo = new FileInfo(saveFileDlg.FileName);

                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    ExcelPackage excel = new ExcelPackage(fileInfo);
                    var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
                    workSheet.TabColor = System.Drawing.Color.Red;
                    workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    workSheet.Row(1).Style.Font.Bold = true;

                    int wi = 1;
                    foreach (DataGridViewColumn dcolumn in dgItems.Columns)
                    {
                        workSheet.Cells[1, wi].Value = dcolumn.HeaderText;
                        wi++;
                    }

                    for (int i = 0; i < dgItems.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgItems.ColumnCount; j++)
                        {
                            workSheet.Cells[i + 2, j + 1].Value = dgItems.Rows[i].Cells[j].Value.ToString();
                        }
                    }

                    for (int i = 1; i <= dgItems.Rows.Count; i++)
                    {
                        workSheet.Column(i).AutoFit();

                    }

                    excel.Save();

                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Export data complete.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBox.Show("No data to export!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);                
            }
            */

        }


        public void CreateCSVFile(DataTable dtDataTablesList, string strFilePath)
        {
            // Create the CSV file to which grid data will be exported.

            StreamWriter sw = new StreamWriter(strFilePath, false);

            //First we will write the headers.

            int iColCount = dtDataTablesList.Columns.Count;

            for (int i = 0; i < iColCount; i++)
            {
                sw.Write(dtDataTablesList.Columns[i]);
                if (i < iColCount - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);

            // Now write all the rows.

            foreach (DataRow dr in dtDataTablesList.Rows)
            {
                for (int i = 0; i < iColCount; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        sw.Write(dr[i].ToString());
                    }
                    if (i < iColCount - 1)

                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }


    }


}
