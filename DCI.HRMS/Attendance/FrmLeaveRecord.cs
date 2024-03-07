using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Service;
using DCI.HRMS.Model.Attendance;
using System.Collections;
using DCI.HRMS.Base;
using DCI.HRMS.Util;
using DCI.HRMS.Common;
using DCI.Security.Model;
using DCI.HRMS.Model.Personal;
using DCI.HRMS.Model;
using DCI.HRMS.Model.Common;
using DCI.HRMS.Service.SubContract;
using DCI.HRMS.Service.Trainee;
using System.Data.SqlClient;
using System.IO;

namespace DCI.HRMS.Attendance
{
    public partial class FrmLeaveRecord : BaseForm, IFormParent, IFormPermission
    {

        private ObjectInfo inform;
        private ApplicationManager appMgr = ApplicationManager.Instance();
        private EmployeeLeaveService emlvSvr = EmployeeLeaveService.Instance();
        private SubContractLeaveService sublvSvr = SubContractLeaveService.Instance();
        private TraineeLeaveService tnlvSvr = TraineeLeaveService.Instance();
        private TimeCardService tmSvr = TimeCardService.Instance();
        private ArrayList lvData = new ArrayList();
        private ArrayList searchData = new ArrayList();
        private ArrayList gvData = new ArrayList();
        private FormAction formAct = FormAction.New;
        private ShiftService emShft = ShiftService.Instance();

        private SubContractShiftService subShft = SubContractShiftService.Instance();
        private SubContractTimeCardService subtmSvr = SubContractTimeCardService.Instance();

        private TraineeShiftService tnShft = TraineeShiftService.Instance();
        private TraineeTimeCardService tntmSvr = TraineeTimeCardService.Instance();

        private readonly string[] colName = new string[] { "Code", "LeaveDate", "Type", "From", "To", "Hour", "Minute", "Pay", "LeaveNo", "Reason", "AddBy", "AddDate", "UpdateBy", "UpdateDate" };
        private readonly string[] propName = new string[] { "EmpCode", "LvDate", "LvType", "LvFrom", "LvTo", "TotalHour", "TotalMinute", "PayStatus", "LvNo", "Reason", "CreateBy", "CreateDateTime", "LastUpdateBy", "LastUpDateDateTime" };
        private readonly int[] width = new int[] { 50, 80, 50, 50, 50, 50, 50, 50, 80, 220, 100, 120, 100, 120 };
        private ArrayList lvType = new ArrayList();
        

        public FrmLeaveRecord()
        {
            InitializeComponent();
        }

        #region Method
        private void FillDataGrid()
        {

            dgItems.DataSource = gvData;
        }
        private void ClearDataGride()
        {
            gvData = new ArrayList();

            dgItems.DataSource = gvData;
            dgItems.Refresh();

        }

        private WorkingHourInfo GetWorkHour(string empCode, DateTime lvDt)
        {
            if (empCode.StartsWith("I"))
            {
                EmployeeShiftInfo sh = subShft.GetEmShift(empCode, lvDt.ToString("yyyyMM"));
                if (sh != null)
                {
                    string shft = sh.DateShift(lvDt);
                    return subtmSvr.GetWorkingHour(lvDt, shft);
                }
                else
                {
                    return null;
                    // txtCode.Focus();
                } 
            }
            else if (empCode.StartsWith("7"))
            {
                EmployeeShiftInfo sh = tnShft.GetEmShift(empCode, lvDt.ToString("yyyyMM"));
                if (sh != null)
                {
                    string shft = sh.DateShift(lvDt);
                    return tntmSvr.GetWorkingHour(lvDt, shft);
                }
                else
                {
                    return null;
                    // txtCode.Focus();
                } 
            }
            else
            {
                EmployeeShiftInfo sh = emShft.GetEmShift(empCode, lvDt.ToString("yyyyMM"));
                if (sh != null)
                {
                    string shft = sh.DateShift(lvDt);
                    return tmSvr.GetWorkingHour(lvDt, shft);
                }
                else
                {
                    return null;
                    // txtCode.Focus();
                } 
            }
        }
        private bool ValidInput()
        {
            if (formAct == FormAction.New)
            {
                if (txtCode.Text == "")
                {
                    txtCode.Focus();
                    return false;

                }
                if (cmbLeaveType.SelectedValue == null)
                {
                    cmbLeaveType.Focus();

                    return false;

                }
                if (txtFrom.Text == "")
                {
                    txtFrom.Focus();
                    return false;
                }
                if (txtTo.Text == "")
                {
                    txtTo.Focus();
                    return false;
                }
                ucl_ActionControl1.CurrentAction = FormActionType.AddNew;

                return true;
            }
            return false;
        }
        private void FindRecord(string id)
        {
            try
            {
                int found = 0;
                for (int i = 0; i < lvData.Count; i++)
                {
                    found = i;
                    EmployeeLealeRequestInfo item = (EmployeeLealeRequestInfo)lvData[i];
                    if (item.DocId == id)
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
        private void CheckShift()
        {
            EmployeeShiftInfo sh = new EmployeeShiftInfo();
            try
            {
                sh = emShft.GetEmShift(txtCode.Text, dtpLvFrom.Value.ToString("yyyyMM"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (txtCode.Text.StartsWith("I"))
            {
                sh = subShft.GetEmShift(txtCode.Text, dtpLvFrom.Value.ToString("yyyyMM"));
            }
            else if (txtCode.Text.StartsWith("7"))
            {
                sh = tnShft.GetEmShift(txtCode.Text, dtpLvFrom.Value.ToString("yyyyMM"));
            }
                   
            dtpLvTo.Value.DayOfWeek.ToString();
            if (sh != null)
            {

                string shft = sh.DateShift(dtpLvFrom.Value);
                if (shft == "D" || shft == "N")
                {
                    WorkingHourInfo tmp = tmSvr.GetWorkingHour(dtpLvFrom.Value, shft);
                    if (tmp != null)
                    {
                        WorkingHourInfo whr = tmp;

                        txtFrom.Text = whr.FirstStart.ToString("HH:mm");
                        txtTo.Text = whr.SecondEnd.ToString("HH:mm");

                    }
                    else
                    {

                        if (shft == "D")
                        {
                            txtFrom.Text = "08:00";
                            txtTo.Text = "17:45";
                        }
                        else if (shft == "N")
                        {
                            txtFrom.Text = "20:00";
                            txtTo.Text = "05:35";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("พนักงานมีตารางกะวันหยุดกรุณาตรวจสอบวันที่", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // dtpLvFrom.Focus();
                }
            }
            else
            {
                MessageBox.Show("ไม่พบข้อมูลตารางกะ กรุณาป้อนข้อมูลตารางกะก่อน", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // txtCode.Focus();
            }


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
                EmployeeLealeRequestInfo item = new EmployeeLealeRequestInfo();

                item.DocId = lblDocId.Text;
                item.EmpCode = txtCode.Text;
                BasicInfo cmbSel = (BasicInfo)cmbLeaveType.SelectedItem;
                item.LvType = cmbSel.Code;
                item.PayStatus = cmbPaySts.SelectedItem.ToString() == "No" ? "N" : "";
                item.LvDate = dtpLvFrom.Value.Date;
                item.LvFrom = txtFrom.Text;
                item.LvTo = txtTo.Text;

                try
                {
                    /*
                    DateTime tFrom = DateTime.Parse(txtFrom.Text);
                    DateTime tTo = DateTime.Parse(txtTo.Text);
                    if (txtTo.Text == "00:00")
                    {
                        tTo = tTo.AddDays(1);
                    }
                    TimeSpan t = tTo - tFrom;

                    if (tFrom >= DateTime.Parse("08:00") && tFrom <= DateTime.Parse("12:00")
                        && tTo >= DateTime.Parse("13:00") && tTo <= DateTime.Parse("17:45"))
                    {
                        t = tTo.AddHours(-1) - tFrom;
                    }

                    else if (tFrom >= DateTime.Parse("20:00") && tFrom <= DateTime.Parse("00:00").AddDays(1)
                        && tTo >= DateTime.Parse("00:40") && tTo <= DateTime.Parse("05:35"))
                    {

                        t = tTo.AddDays(1).AddMinutes(-50) - tFrom;
                    }
                    */

                    WorkingHourInfo whr = GetWorkHour(txtCode.Text, dtpLvFrom.Value.Date);
                    if (whr == null)
                    {
                        whr = new WorkingHourInfo();
                    }

                    int hour = 0;
                    int min = 0;


                    DateTime tFrom = DateTime.Parse(  item.LvDate.ToString("dd/MM/yyyy ") + txtFrom.Text);
                    DateTime tTo = DateTime.Parse(item.LvDate.ToString("dd/MM/yyyy ") + txtTo.Text);
                   //************************************//
                   //     Check if time of night shift   //
                   //************************************//
                    if (tTo < tFrom)
                    {
                        tTo = tTo.AddDays(1);
                    }
                    else if (tFrom >= DateTime.Parse(item.LvDate.ToString("dd/MM/yyyy ")+"00:00") && tFrom < DateTime.Parse(item.LvDate.ToString("dd/MM/yyyy ")+"08:00"))
                    {
                        tTo = tTo.AddDays(1);
                        tFrom = tFrom.AddDays(1);
                    }

                    //*************************************/
                    if (tFrom == whr.FirstStart && tTo == whr.SecondEnd)
                    {
                        int total = whr.FirstTotal + whr.SecondTotal;
                        hour = (int)total / 60;
                        min = total % 60;
                    }
                    else if ((tFrom >= whr.FirstStart && tTo <= whr.FirstEnd) || (tFrom >= whr.SecondStart && tTo <= whr.SecondEnd))
                    {
                        TimeSpan t = tTo - tFrom;
                        hour = t.Hours;
                        min = t.Minutes;

                    }

                    else if (tFrom >= whr.FirstStart && tFrom <= whr.FirstEnd && tTo >= whr.SecondStart && tTo <= whr.SecondEnd)
                    {


                        TimeSpan tf = whr.FirstEnd - tFrom;
                        TimeSpan ts = tTo - whr.SecondStart;

                        /*if (tFrom >= DateTime.Parse("08:00") && tFrom <= DateTime.Parse("12:00")
                            && tTo >= DateTime.Parse("13:00") && tTo <= DateTime.Parse("17:45"))
                        {


                            t = tTo.AddHours(-1) - tFrom;


                        }

                        else if (tFrom >= DateTime.Parse("20:00") && tFrom <= DateTime.Parse("00:00").AddDays(1)
                            && tTo >= DateTime.Parse("00:40") && tTo <= DateTime.Parse("05:35"))
                        {

                            t = tTo.AddDays(1).AddMinutes(-50) - tFrom;

                        }*/
                        hour = tf.Hours + ts.Hours;
                        min = tf.Minutes + tf.Minutes;
                    }
                    else
                    {

                        /*MessageBox.Show("เวลาไปตรงกับตารางกะ กรุณาป้อนใหม่", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);

                      txtFrom.Clear();
                      txtTo.Clear();

                      txtCode.Focus();*/

                    }


                    item.TotalHour = (hour).ToString("00") + ":" + min.ToString("00");
                    item.TotalMinute = (hour * 60 + min);


                }
                catch
                {
                    item.TotalHour = "08:45";
                    item.TotalMinute = 525;                    
                }

                try
                {
                    item.LvNo = int.Parse(txtLvNo.Text);
                }
                catch { }
                //item.Reason = txtReason.Text;
                item.Reason = EncodeLanguage(txtReason.Text);

                return item;
            }
            set
            {

            }
        }

        public void AddNew()
        {
            ucl_ActionControl1.CurrentAction = FormActionType.AddNew;
        }

        public void Save()
        {
            if (formAct == FormAction.New)
            {
                if (ucl_ActionControl1.CurrentAction == FormActionType.SaveAs)
                {
                    if (ValidInput())
                    {

                        if (dtpLvFrom.Value.Date == dtpLvTo.Value.Date)
                        {
                            EmployeeShiftInfo sh = emShft.GetEmShift(txtCode.Text, dtpLvTo.Value.ToString("yyyyMM"));

                            if (txtCode.Text.StartsWith("I"))
                            {
                                 sh = subShft.GetEmShift(txtCode.Text, dtpLvTo.Value.ToString("yyyyMM"));

                            }
                            else if (txtCode.Text.StartsWith("7"))
                            {
                                 sh = tnShft.GetEmShift(txtCode.Text, dtpLvTo.Value.ToString("yyyyMM"));

                            }
                            

                            if (sh == null)
                            {
                                if (MessageBox.Show("ไม่พบข้อมูลตารงกะ ต้องการดำเนินการต่อหรือไม่", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                                {
                                    this.Clear();
                                    return;
                                }
                            }



                            EmployeeLealeRequestInfo item = (EmployeeLealeRequestInfo)this.Information;
                            try
                            {
                                inform = new ObjectInfo();
                                inform.CreateBy = appMgr.UserAccount.AccountId;
                                item.Inform = inform;

                                /************************************************************/
                                /*                        04/04/2009                        */
                                /*    Check if these is Absent record  confirm to delete it */
                                /***********************************************************/
                                if (item.LvType != "ABSE")
                                {
                                    try
                                    {

                                        ArrayList lvAbs = emlvSvr.GetAllLeave(item.EmpCode, item.LvDate, item.LvDate, "ABSE");
                                        if (item.EmpCode.StartsWith("I"))
                                        {
                                             lvAbs = sublvSvr.GetAllLeave(item.EmpCode, item.LvDate, item.LvDate, "ABSE");

                                        }
                                        else if (item.EmpCode.StartsWith("7"))
                                        {
                                             lvAbs = tnlvSvr.GetAllLeave(item.EmpCode, item.LvDate, item.LvDate, "ABSE");

                                        }
                                        
                                        if (lvAbs != null)
                                        {
                                            foreach (EmployeeLealeRequestInfo abitem in lvAbs)
                                            {
                                                if (MessageBox.Show("มีข้อมูลการขาดงานของพนักงาน รหัส:" + item.EmpCode + "วันที่ :" + abitem.LvDate.ToString("dd/MM/yyyy")
                                                    + "\nต้องการลบก่อนใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                                {

                                                    if (item.EmpCode.StartsWith("I"))
                                                    {
                                                        sublvSvr.DeleteLeaveRequest(abitem);
                                                    }
                                                    else if (item.EmpCode.StartsWith("7"))
                                                    {
                                                        tnlvSvr.DeleteLeaveRequest(abitem);
                                                    }
                                                    else
                                                    {

                                                        emlvSvr.DeleteLeaveRequest(abitem);
                                                    }

                                                    //***** SQL DELETE  *****
                                                    DeleteLVRQ_REQ(abitem);

                                                }
                                            }
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }


                                if (item.EmpCode.StartsWith("I"))
                                {
                                    sublvSvr.SaveLeaveRequest(item);
                                }
                                else if (item.EmpCode.StartsWith("7"))
                                {
                                    tnlvSvr.SaveLeaveRequest(item);
                                }
                                else
                                {

                                    emlvSvr.SaveLeaveRequest(item);
                                }

                                //***** SQL Insert *****
                                InsertLVRQ_REQ(item);

                         
                                this.RefreshData();
                                this.FindRecord(item.DocId);
                                this.Clear();

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }
                        else
                        {
                            EmployeeShiftInfo sh = emShft.GetEmShift(txtCode.Text, dtpLvFrom.Value.ToString("yyyyMM"));
                            if (txtCode.Text.StartsWith("I"))
                            {
                                 sh = subShft.GetEmShift(txtCode.Text, dtpLvFrom.Value.ToString("yyyyMM"));

                            }
                            else if (txtCode.Text.StartsWith("7"))
                            {
                                 sh = tnShft.GetEmShift(txtCode.Text, dtpLvFrom.Value.ToString("yyyyMM"));

                            }
                            if (sh != null)
                            {
                                DateTime rdate = dtpLvFrom.Value.Date;
                                DateTime tDate = dtpLvTo.Value.Date;
                                EmployeeLealeRequestInfo item = (EmployeeLealeRequestInfo)this.Information;
                                EmployeeInfo temp = (EmployeeInfo)empData_Control1.Information;
                                BasicInfo Obj = emlvSvr.GetLeaveTypeInfo(item.LvType);

                                int canLeave = 0;
                                bool checkLeave = false;
                                if (item.LvType != "ANNU")
                                {
                                    try
                                    {
                                        canLeave = int.Parse(Obj.Description) * 525;
                                        checkLeave = true;
                                    }
                                    catch
                                    { }
                                    if (canLeave != 0 && checkLeave)
                                    {
                                        ArrayList lvTotal = (ArrayList)leaveTotal_Control1.Information;
                                        foreach (LeaveTotal var in lvTotal)
                                        {
                                            if (var.Type == item.LvType)
                                                canLeave -= var.LvTotal;
                                        }
                                    }
                                }
                                else
                                {

                                    canLeave = Convert.ToInt32(annualLeave_Control1.Information);
                                }

                                while (rdate <= tDate)
                                {

                                    /***  Check Shift to set leave Time  *******/

                                    if (txtCode.Text.StartsWith("I"))
                                    {
                                        sh = subShft.GetEmShift(txtCode.Text, rdate.ToString("yyyyMM"));

                                    }
                                    else if (txtCode.Text.StartsWith("7"))
                                    {
                                        sh = tnShft.GetEmShift(txtCode.Text, rdate.ToString("yyyyMM"));

                                    }
                                    else
                                    {
                                        sh = emShft.GetEmShift(txtCode.Text, rdate.ToString("yyyyMM"));

                                    }
                                    
                                    if (sh != null)
                                    {
                                        string shft = sh.DateShift(rdate);
                                        if (shft == "D" || shft == "N")
                                        {
                                            WorkingHourInfo tmp = tmSvr.GetWorkingHour(rdate, shft);
                                            if (tmp != null)
                                            {
                                                WorkingHourInfo whr = tmp;

                                                txtFrom.Text = whr.FirstStart.ToString("HH:mm");
                                                txtTo.Text = whr.SecondEnd.ToString("HH:mm");

                                            }
                                            else
                                            {

                                                if (shft == "D")
                                                {
                                                    txtFrom.Text = "08:00";
                                                    txtTo.Text = "17:45";
                                                }
                                                else if (shft == "N")
                                                {
                                                    txtFrom.Text = "20:00";
                                                    txtTo.Text = "05:35";
                                                }
                                            }
                                        }
                                        /*
                                        string shft = sh.DateShift(dtpLvFrom.Value);
                                        if (shft == "D")
                                        {
                                            txtFrom.Text = "08:00";
                                            txtTo.Text = "17:45";
                                        }
                                        else if (shft == "N")
                                        {
                                            txtFrom.Text = "20:00";
                                            txtTo.Text = "05:35";
                                        }*/
                                        else
                                        {
                                            rdate = rdate.AddDays(1);
                                            dtpLvFrom.Value = rdate;
                                            continue;

                                        }

                                        /********************************************/

                                        if (item.LvType != "ANNU")
                                        {
                                            if (canLeave <= 0 && checkLeave)
                                                cmbPaySts.SelectedIndex = 1;
                                            else
                                                cmbPaySts.SelectedIndex = 0;
                                            item = (EmployeeLealeRequestInfo)this.Information;
                                            try
                                            {
                                                inform = new ObjectInfo();
                                                inform.CreateBy = appMgr.UserAccount.AccountId;
                                                item.Inform = inform;


                                                /************************************************************/
                                                /*                        04/04/2009                        */
                                                /*    Check if these is Absent record  confirm to delete it */
                                                /***********************************************************/
                                                if (item.LvType != "ABSE")
                                                {
                                                    try
                                                    {

                                                        ArrayList lvAbs = emlvSvr.GetAllLeave(item.EmpCode, item.LvDate, item.LvDate, "ABSE");
                                                        if (item.EmpCode.StartsWith("I"))
                                                        {
                                                            lvAbs = sublvSvr.GetAllLeave(item.EmpCode, item.LvDate, item.LvDate, "ABSE");

                                                        }
                                                        else if (item.EmpCode.StartsWith("7"))
                                                        {
                                                            lvAbs = tnlvSvr.GetAllLeave(item.EmpCode, item.LvDate, item.LvDate, "ABSE");

                                                        }
                                                        if (lvAbs != null)
                                                        {
                                                            foreach (EmployeeLealeRequestInfo abitem in lvAbs)
                                                            {
                                                                if (MessageBox.Show("มีข้อมูลการขาดงานของพนักงาน รหัส:" + item.EmpCode + "วันที่ :" + abitem.LvDate.ToString("dd/MM/yyyy")
                                                                    + "\nต้องการลบก่อนใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                                                {
                                                                    if (item.EmpCode.StartsWith("I"))
                                                                    {
                                                                        sublvSvr.DeleteLeaveRequest(abitem);
                                                                    }
                                                                    else if (item.EmpCode.StartsWith("7"))
                                                                    {
                                                                        tnlvSvr.DeleteLeaveRequest(abitem);
                                                                    }
                                                                    else
                                                                    {

                                                                        emlvSvr.DeleteLeaveRequest(abitem);
                                                                    }
                                                                    //***** SQL Delete *****
                                                                    DeleteLVRQ_REQ(abitem);

                                                                }
                                                            }
                                                        }
                                                    }
                                                    catch
                                                    {
                                                    }
                                                }


                                                if (item.EmpCode.StartsWith("I"))
                                                {
                                                    sublvSvr.SaveLeaveRequest(item);
                                                }
                                                else if (item.EmpCode.StartsWith("7"))
                                                {
                                                    tnlvSvr.SaveLeaveRequest(item);
                                                }
                                                else
                                                {

                                                    emlvSvr.SaveLeaveRequest(item);
                                                }

                                                //***** SQL Insert *****
                                                InsertLVRQ_REQ(item);



                                                canLeave -= 525;
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                break;
                                            }
                                        }
                                        else
                                        {

                                            item = (EmployeeLealeRequestInfo)this.Information;

                                            annualLeave_Control1.CalTotalAnnual(txtCode.Text, temp.JoinDate, rdate.Date);
                                            canLeave = Convert.ToInt32(annualLeave_Control1.Information);



                                            if ((canLeave - item.TotalMinute) < 0)
                                            {
                                                if (MessageBox.Show("ไม่สามารถลาวันที่ " + rdate.ToString("dd/MM/yyyy") + "ได้\n เนื่องจากไม่มีวันพักร้อนคงเหลือไม่พอ\n ต้องการดำเนินการต่อหรือไม่?", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.No)
                                                {
                                                    txtCode.Focus();
                                                    break;
                                                }

                                            }
                                            else
                                            {
                                                cmbPaySts.SelectedIndex = 0;
                                            }
                                         
                                            try
                                            {
                                                inform = new ObjectInfo();
                                                inform.CreateBy = appMgr.UserAccount.AccountId;
                                                item.Inform = inform;

                                                /************************************************************/
                                                /*                        04/04/2009                        */
                                                /*    Check if these is Absent record  confirm to delete it */
                                                /***********************************************************/
                                                if (item.LvType != "ABSE" )
                                                {
                                                    try
                                                    {

                                                        ArrayList lvAbs = emlvSvr.GetAllLeave(item.EmpCode, item.LvDate, item.LvDate, "ABSE");
                                                        if (lvAbs != null)
                                                        {
                                                            foreach (EmployeeLealeRequestInfo abitem in lvAbs)
                                                            {
                                                                if (MessageBox.Show("มีข้อมูลการขาดงานของพนักงาน รหัส:" + item.EmpCode + "วันที่ :" + abitem.LvDate.ToString("dd/MM/yyyy")
                                                                    + "\nต้องการลบก่อนใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                                                {
                                                                    emlvSvr.DeleteLeaveRequest(abitem);

                                                                    //***** SQL Delete *****
                                                                    DeleteLVRQ_REQ(abitem);
                                                                }
                                                            }
                                                        }
                                                    }
                                                    catch
                                                    {
                                                    }
                                                }


                                                emlvSvr.SaveLeaveRequest(item);
                                                
                                                //***** SQL Insert *****
                                                InsertLVRQ_REQ(item);



                                                canLeave -= 525;
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                break;
                                            }

                                        }
                                    }
                                    rdate = rdate.AddDays(1);
                                    dtpLvFrom.Value = rdate;
                                }
                                this.RefreshData();
                                this.FindRecord(item.DocId);
                                this.Clear();
                            }
                            else
                            {
                            }
                        }
                    }
                }
                else
                {
                    EmployeeLealeRequestInfo item = (EmployeeLealeRequestInfo)leaveRecord_Control1.Infromation;
                    try
                    {/*

                        BasicInfo Obj = emlvSvr.GetLeaveTypeInfo(item.LvType);
                        
                        int canLeave = 0;
                        if (item.LvType != "ANNU")
                        {
                            try
                            {
                                canLeave = int.Parse(Obj.Description) * 525;
                            }
                            catch
                            { }
                            if (canLeave != 0)
                            {
                                ArrayList lvTotal = (ArrayList)leaveTotal_Control1.Information;
                                foreach (LeaveTotal var in lvTotal)
                                {
                                    if (var.Type == item.LvType)
                                        canLeave -= var.LvTotal;
                                }
                            }
                        }
                        else
                        {
                            canLeave = Convert.ToInt32(annualLeave_Control1.Information);
                            canLeave -= item.TotalMinute;
                        }

                        if (canLeave < 0)
                        {
                            if (item.LvType != "ANNU")
                            {
                                item.PayStatus = "N";
                            }
                            else
                            {
                                if (MessageBox.Show("วันพักร้อนของพนักงานเกิน ต้องการบันทึกข้อมูลใช่หรือไม่", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                                {
                                    goto refesh;
                                }

                            }
                        }
                        else
                        {
                            item.PayStatus = "";
                        }

                        */


                        inform = new ObjectInfo();
                        inform.LastUpdateBy = appMgr.UserAccount.AccountId;



               
                        item.Inform = inform;
                     

                        if (item.EmpCode.StartsWith("I"))
                        {
                            sublvSvr.UpdateLeaveRequest(item);
                        }
                        else if (item.EmpCode.StartsWith("7"))
                        {
                            tnlvSvr.UpdateLeaveRequest(item);
                        }
                        else
                        {
                            emlvSvr.UpdateLeaveRequest(item);
                        }

                        //***** SQL Insert *****
                        InsertLVRQ_REQ(item);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
               // refesh:
                    txtCode.Text = item.EmpCode;
                    this.RefreshData();
                    this.FindRecord(item.DocId);
                    this.Clear();


                }
            }
            else if (formAct == FormAction.Save)
            {

                EmployeeLealeRequestInfo item = (EmployeeLealeRequestInfo)leaveRecord_Control3.Infromation;
                try
                {
                    inform = new ObjectInfo();
                    inform.LastUpdateBy = appMgr.UserAccount.AccountId;
                    item.Inform = inform;
                    emlvSvr.UpdateLeaveRequest(item);


                    if (item.EmpCode.StartsWith("I"))
                    {
                        sublvSvr.UpdateLeaveRequest(item);
                    }
                    else if (item.EmpCode.StartsWith("7"))
                    {
                        tnlvSvr.UpdateLeaveRequest(item);
                    }
                    else
                    {
                        emlvSvr.UpdateLeaveRequest(item);
                    }
                    //***** SQL Insert *****
                    InsertLVRQ_REQ(item);



                    this.RefreshData();


                }
                catch (Exception ex)
                {
                    MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }

        }

        public void Delete()
        {
            if (MessageBox.Show("ต้องการลบข้อมูลใช่หรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (formAct == FormAction.Save)
                {
                    try
                    {
                        foreach (DataGridViewRow var in dgItems.SelectedRows)
                        {
                            EmployeeLealeRequestInfo rq = (EmployeeLealeRequestInfo)searchData[var.Index];
                            inform = new ObjectInfo();
                            inform.LastUpdateBy = appMgr.UserAccount.AccountId;
                            rq.Inform = inform;
                            try
                            {

                                if (rq.EmpCode.StartsWith("I"))
                                {
                                    sublvSvr.DeleteLeaveRequest(rq);
                                }
                                else if (rq.EmpCode.StartsWith("7"))
                                {
                                    tnlvSvr.DeleteLeaveRequest(rq);
                                }
                                else
                                {
                                    emlvSvr.DeleteLeaveRequest(rq);
                                }


                                //***** SQL DELETE  *****
                                DeleteLVRQ_REQ(rq);


                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show("ไม่สามารถลบข้อมูล Code =" + rq.EmpCode + "ได้ เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }
                        this.RefreshData();
                    }
                    catch
                    {
                    }
                    /*
                    try
                    {
                        EmployeeLealeRequestInfo item = (EmployeeLealeRequestInfo)leaveRecord_Control3.Infromation;
                        emlvSvr.DeleteLeaveRequest(item);

                        this.RefreshData();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ไม่สามารถลบข้อมูลได้เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    }*/
                }
                else if (formAct == FormAction.New)
                {
                    try
                    {
                        EmployeeLealeRequestInfo item = (EmployeeLealeRequestInfo)leaveRecord_Control1.Infromation;
              

                        if (item.EmpCode.StartsWith("I"))
                        {
                            sublvSvr.DeleteLeaveRequest(item);
                        }
                        else if (item.EmpCode.StartsWith("7"))
                        {
                            tnlvSvr.DeleteLeaveRequest(item);
                        }
                        else
                        {
                            emlvSvr.DeleteLeaveRequest(item);
                        }

                        //***** SQL DELETE  *****
                        DeleteLVRQ_REQ(item);

                        //  textBox1.Text = item.EmpCode;
                        txtCode.Text = item.EmpCode;
                        this.RefreshData();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ไม่สามารถลบข้อมูลได้เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    }
                }

            }
        }

        public void Search()
        {
            try
            {

                if (formAct == FormAction.Save)
                {
                    searchData = emlvSvr.GetAllLeave(txtSCode.Text, dtpSFrom.Value.Date, dtpSTo.Value.Date, cmnSType.SelectedValue.ToString());
                    if (txtSCode.Text.StartsWith("I"))
                    {
                        searchData = sublvSvr.GetAllLeave(txtSCode.Text, dtpSFrom.Value.Date, dtpSTo.Value.Date, cmnSType.SelectedValue.ToString());
                    }
                    else if (txtSCode.Text.StartsWith("7"))
                    {
                        searchData = tnlvSvr.GetAllLeave(txtSCode.Text, dtpSFrom.Value.Date, dtpSTo.Value.Date, cmnSType.SelectedValue.ToString());
                    }

                    gvData = searchData;
                    FillDataGrid();
                }
                else if (formAct == FormAction.New)
                {
                    string code = txtCode.Text;


                    if (code.StartsWith("I"))
                    {
                        EmployeeInfo emp = SubContractService.Instance().Find(code);
                        lvData = sublvSvr.GetAllLeave(code, "%");
                        annualLeave_Control1.Clear();
                        leaveTotal_Control1.CalLeave(emp.Code, DateTime.Today);
                    }
                    else if (code.StartsWith("7"))
                    {
                        EmployeeInfo emp = TraineeService.Instance().Find(code);
                        lvData = tnlvSvr.GetAllLeave(code, "%");
                        annualLeave_Control1.Clear();
                        leaveTotal_Control1.CalLeave(emp.Code, DateTime.Today);
                    }
                    else
                    {
                        EmployeeInfo emp = EmployeeService.Instance().Find(code);
                        lvData = emlvSvr.GetAllLeave(code, "%");
                        annualLeave_Control1.CalTotalAnnual(emp.Code, emp.AnnualcalDate, dtpLvFrom.Value.Date);
                        leaveTotal_Control1.CalLeave(emp.Code, DateTime.Today);
                    }




                    ClearDataGride();
                    gvData = lvData;
                    FillDataGrid();
                    if (dgItems.Rows.Count != 0)
                        dgItems.CurrentCell = dgItems[1, dgItems.Rows.Count - 1];
                }


            }
            catch
            {


            }

        }

        public void Export()
        {
            Reports.FrmRptAttandance rpt = new DCI.HRMS.Attendance.Reports.FrmRptAttandance();
            rpt.Show();
        }

        public void Print()
        {

        }

        public void Open()
        {
            AddGridViewColumns();
            lvType = emlvSvr.GetAllLeaveType();
            cmbLeaveType.DisplayMember = "NameForSearching";
            cmbLeaveType.ValueMember = "Code";
            cmbLeaveType.DataSource = lvType;
            cmbLeaveType.SelectedIndex = -1;
            cmbPaySts.SelectedIndex = 0;
            ArrayList lvTypeAll = (ArrayList)lvType.Clone();
            BasicInfo objAll = new BasicInfo("", "All", "");
            lvTypeAll.Insert(0, objAll);
            cmnSType.DisplayMember = "NameForSearching";
            cmnSType.ValueMember = "Code";
            cmnSType.DataSource = lvTypeAll;
            cmnSType.SelectedIndex = 0;
            AutoCompleteStringCollection lvsn = new AutoCompleteStringCollection();
            ArrayList lsrs = emlvSvr.GetLeaveReasonSuggestion();
            foreach (EmployeeLealeRequestInfo tt in lsrs)
            {
                lvsn.Add(tt.Reason);
            }
            txtReason.AutoCompleteCustomSource = lvsn;


            kryptonHeaderGroup1_Click(kryptonHeaderGroup1, new EventArgs());
        }

        public void Clear()
        {
            ucl_ActionControl1.CurrentAction = FormActionType.None;
            //txtCode.Text = "";
            txtFrom.Text = "";
            txtLvNo.Text = "";
            txtReason.Text = "";
            txtTo.Text = "";
            cmbLeaveType.SelectedIndex = -1;
            cmbPaySts.SelectedIndex = 0;
            dtpLvFrom.Value = DateTime.Today;
            dtpLvTo.Value = DateTime.Today;
            txtCode.Focus();
            txtCode.SelectAll();



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

        #region IFormPermission Members

        public PermissionInfo Permission
        {
            set { ucl_ActionControl1.Permission = value; }
        }

        #endregion

        #region Event

        private void leaveRecord_Control1_Load(object sender, EventArgs e)
        {
            leaveRecord_Control1.emlvSvr = EmployeeLeaveService.Instance();
            leaveRecord_Control1.emShft = ShiftService.Instance();
            leaveRecord_Control1.tmSvr = TimeCardService.Instance();
            leaveRecord_Control1.GetAllType();
        }
        private void leaveRecord_Control3_Load(object sender, EventArgs e)
        {
            leaveRecord_Control3.emlvSvr = EmployeeLeaveService.Instance();
            leaveRecord_Control3.emShft = ShiftService.Instance();
            leaveRecord_Control3.tmSvr = TimeCardService.Instance();
            leaveRecord_Control3.GetAllType();
        }
        private void leaveRecord_Control1_Code_Enter(object sender)
        {


        }
        private void FrmLeaveRecord_Load(object sender, EventArgs e)
        {

            this.Open();
            ucl_ActionControl1.Owner = this;
        }

        private void leaveRecord_Control1_Date_Enter(object sender)
        {

        }

        private void annualLeave_Control1_Load(object sender, EventArgs e)
        {
            annualLeave_Control1.empLvSvr = emlvSvr;
        }
        private void kryptonHeaderGroup1_Click(object sender, EventArgs e)
        {
            if (sender == kryptonHeaderGroup1)
            {
                kryptonHeaderGroup3.Collapsed = true;
                kryptonHeaderGroup1.Collapsed = false;
                ucl_ActionControl1.CurrentAction = FormActionType.None;
                formAct = FormAction.New;
                //dgItems.ClearSelection();
                gvData = lvData;
                FillDataGrid();
                dgItems.MultiSelect = false;
            }
            else
            {

                kryptonHeaderGroup1.Collapsed = true;
                kryptonHeaderGroup3.Collapsed = false;
                formAct = FormAction.Save;
                gvData = searchData;
                FillDataGrid();
                dgItems.MultiSelect = true;
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

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (e.KeyCode == Keys.Enter)
            {
                TextBox tt = (TextBox)sender;
                EmployeeInfo emp = EmployeeService.Instance().Find(txtCode.Text);

                if (txtCode.Text.StartsWith("I"))
                {
                    emp = SubContractService.Instance().Find(txtCode.Text);
                }
                else if (txtCode.Text.StartsWith("7"))
                {
                    emp = TraineeService.Instance().Find(txtCode.Text);
                }
                
                if (emp != null)
                {
                   // this.Clear();

                    empData_Control1.Information = emp;
                    this.Search();

                    lblDocId.Text = emlvSvr.LoadRecordKey();


                    KeyPressManager.Enter(e);
                    ucl_ActionControl1.CurrentAction = FormActionType.AddNew;
                    txtDateTo_Leave(sender, e);

                    if (emp.Resigned && emp.ResignDate < dtpLvTo.Value)
                    {

                        MessageBox.Show("พนักงานลาออกแล้ว ตั้งแต่วันที่ " + emp.ResignDate.ToString("dd/MM/yyyy"), "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtCode.Clear();
                        this.Clear();


                    }


                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูลพนักงาน", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCode.Clear();
                    this.Clear();
                }

            }
            this.Cursor = Cursors.Default;
        }



        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dtpLvTo.Value = dtpLvFrom.Value;
        }

        private void cmbLeaveType_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPressManager.Enter(e);
        }

        private void cmbPaySts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPaySts.SelectedItem.ToString() == "No")
            {
                cmbPaySts.BackColor = Color.Red;

            }
            else
            {
                cmbPaySts.BackColor = Color.White;
            }
        }

        private void txtFrom_Leave(object sender, EventArgs e)
        {
            KeyPressManager.ConvertTextTime(sender);
        }

        private void txtReason_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ucl_ActionControl1.CurrentAction = FormActionType.AddNew;
                this.Save();
            }
        }

        private void dgItems_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyleDefault.ShowRowNumber(dgItems, e);
            if (dgItems["Type", e.RowIndex].Value.ToString() == "ANNU")
            {
                dgItems.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Blue;
            }
            else if (dgItems["Type", e.RowIndex].Value.ToString() == "ABSE")
            {
                dgItems.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;

            }
        }

        private void cmbLeaveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BasicInfo cmbSel = (BasicInfo)cmbLeaveType.SelectedItem;
                if (cmbSel.Code == "ABSE")
                {
                    cmbPaySts.SelectedIndex = 1;
                }
                else if (cmbSel.Code == "LATE")
                {
                    if (dtpLvFrom.Value.Date == dtpLvTo.Value.Date)
                    {
                        DateTime cmpDt = DateTime.Parse("01:00");
                        DateTime chkDt = DateTime.Parse(textBox1.Text);
                        if (chkDt >= cmpDt)
                        {
                            cmbPaySts.SelectedIndex = 1;
                        }
                        else
                        {
                            cmbPaySts.SelectedIndex = 0;
                        }
                    }
                    else
                    {

                    }
                }
                else if (cmbSel.Code == "ANNU")
                {
                    EmployeeInfo temp = (EmployeeInfo)empData_Control1.Information;
                    annualLeave_Control1.CalTotalAnnual(txtCode.Text, temp.JoinDate, dtpLvFrom.Value.Date);
                    double remain = (double)annualLeave_Control1.Information;
                    if (remain < 525 && remain > 0)
                    {
                        if (MessageBox.Show("วันพักร้อนของพนักงานเหลือน้อยกว่า 1 วัน", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.No)
                        {
                            txtCode.Focus();
                        }
                    }
                    else if (remain <= 0)
                    {
                        if (MessageBox.Show("ไม่สามารถลาได้ เนื่องจากไม่มีวันพักร้อนคงเหลือ", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.No)
                        {
                            txtCode.Focus();
                        }
                    }
                    cmbPaySts.SelectedIndex = 0;
                }
                else
                {
                    BasicInfo Obj = emlvSvr.GetLeaveTypeInfo(cmbSel.Code);

                    int canLeave = 0;
                    try
                    {
                        canLeave = int.Parse(Obj.Description) * 525;

                        ArrayList lvTotal = (ArrayList)leaveTotal_Control1.Information;
                        foreach (LeaveTotal var in lvTotal)
                        {
                            if (var.Type == cmbSel.Code)
                            {
                                canLeave -= var.LvTotal;
                            }
                        }

                        if (canLeave <= 0)
                        {
                            cmbPaySts.SelectedIndex = 1;
                        }
                        else
                        {
                            cmbPaySts.SelectedIndex = 0;
                        }
                    }
                    catch
                    {

                        cmbPaySts.SelectedIndex = 0;

                    }
                }
            }
            catch
            {
            }
        }
        private void txtTo_Leave(object sender, EventArgs e)
        {
            if (KeyPressManager.ConvertTextTime(sender))
            {
                try
                {
                    if (dtpLvFrom.Value.Date == dtpLvTo.Value.Date)
                    {

                        WorkingHourInfo whr = GetWorkHour(txtCode.Text, dtpLvFrom.Value.Date);
                        if (whr==null)  
                        {
                            whr = new WorkingHourInfo();
                        }

                          int hour =0;
                          int min = 0; 


                        DateTime tFrom = DateTime.Parse(dtpLvFrom.Value.ToString("dd/MM/yyyy ") +txtFrom.Text);
                        DateTime tTo = DateTime.Parse(dtpLvFrom.Value.ToString("dd/MM/yyyy ") + txtTo.Text);
                      
                        //*******************************//
                        // Check if time of night shift  // 
                        /********************************/
                        if (tTo < tFrom)
                        {
                            tTo = tTo.AddDays(1);
                        }
          
                        else if (tFrom >= DateTime.Parse(dtpLvFrom.Value.ToString("dd/MM/yyyy ")+"00:00") && tFrom < DateTime.Parse(dtpLvFrom.Value.ToString("dd/MM/yyyy ") +"08:00"))
                        {
                            tTo = tTo.AddDays(1);
                            tFrom = tFrom.AddDays(1);
                        }    
                        
                        /***********************************/

                        if (tFrom == whr.FirstStart && tTo == whr.SecondEnd)
                        {
                            int total = whr.FirstTotal + whr.SecondTotal;
                            hour = (int)total / 60;
                            min = total % 60;
                        }
                        else if ((tFrom >= whr.FirstStart && tTo <= whr.FirstEnd )||(tFrom >= whr.SecondStart && tTo <= whr.SecondEnd))
                        {
                            TimeSpan t = tTo - tFrom;
                            hour = t.Hours;
                            min = t.Minutes;

                        }   
                        else if(tFrom >= whr.FirstStart && tFrom<= whr.FirstEnd &&tTo>= whr.SecondStart&& tTo <= whr.SecondEnd)
                        {


                            TimeSpan tf = whr.FirstEnd - tFrom;
                            TimeSpan ts = tTo - whr.SecondStart;
                            /*
                            if (tFrom >= DateTime.Parse("08:00") && tFrom <= DateTime.Parse("12:00")
                                && tTo >= DateTime.Parse("13:00") && tTo <= DateTime.Parse("17:45"))
                            {


                                t = tTo.AddHours(-1) - tFrom;


                            }

                            else if (tFrom >= DateTime.Parse("20:00") && tFrom <= DateTime.Parse("00:00").AddDays(1)
                                && tTo >= DateTime.Parse("00:40") && tTo <= DateTime.Parse("05:35"))
                            {

                                t = tTo.AddDays(1).AddMinutes(-50) - tFrom;

                            }*/
                            hour = tf.Hours + ts.Hours;
                            min = tf.Minutes+ tf.Minutes;
                        }
                        else
                        {
                          
                            MessageBox.Show("เวลาไม่ตรงกับตารางกะ กรุณาป้อนใหม่", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            txtFrom.Clear();
                            txtTo.Clear();

                            txtCode.Focus();
                        }


               

                        textBox1.Text = (hour).ToString("00") + ":" + min.ToString("00");

                        lblTimeUnit.Text = "Hour";


                    }
                    else
                    {

                    }
                }
                catch
                {


                }

            }
        }
        private void txtDateTo_Leave(object sender, EventArgs e)
        {
            // KeyPressManager.ConvertTextdate(sender);
            try
            {
                if (txtCode.Text != "")
                {
                    if (dtpLvTo.Value > dtpLvFrom.Value)
                    {
                        EmployeeShiftInfo sh = emShft.GetEmShift(txtCode.Text, dtpLvFrom.Value.ToString("yyyyMM"));
                        EmployeeShiftInfo esh = emShft.GetEmShift(txtCode.Text, dtpLvTo.Value.ToString("yyyyMM"));

                        if (txtCode.Text.StartsWith("I"))
                        {
                            sh = subShft.GetEmShift(txtCode.Text, dtpLvFrom.Value.ToString("yyyyMM"));
                            esh = subShft.GetEmShift(txtCode.Text, dtpLvTo.Value.ToString("yyyyMM"));
                        }
                        else if (txtCode.Text.StartsWith("7"))
                        {
                            sh = tnShft.GetEmShift(txtCode.Text, dtpLvFrom.Value.ToString("yyyyMM"));
                            esh = tnShft.GetEmShift(txtCode.Text, dtpLvTo.Value.ToString("yyyyMM"));
                        }


                        if (sh == null || esh == null)
                        {
                            MessageBox.Show("ไม่พบข้อมูลตารงกะ กรุณาป้อนข้อมูลตารางกะก่อน", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtCode.Text = "";
                            return;
                        }
                        else
                        {
                            string shft = sh.DateShift(dtpLvFrom.Value);
                            if (shft == "D" || shft == "N")
                            {
                                WorkingHourInfo tmp = tmSvr.GetWorkingHour(dtpLvFrom.Value, shft);
                                if (tmp != null)
                                {
                                    WorkingHourInfo whr = tmp;

                                    txtFrom.Text = whr.FirstStart.ToString("HH:mm");
                                    txtTo.Text = whr.SecondEnd.ToString("HH:mm");
                                }
                                else
                                {
                                    if (shft == "D")
                                    {
                                        txtFrom.Text = "08:00";
                                        txtTo.Text = "17:45";
                                    }
                                    else if (shft == "N")
                                    {
                                        txtFrom.Text = "20:00";
                                        txtTo.Text = "05:35";
                                    }
                                }
                            }
                        }
                        TimeSpan t = dtpLvTo.Value.Date - dtpLvFrom.Value.Date;
                        DateTime rdate = dtpLvFrom.Value.Date;
                        double day = 0;
                        while (rdate <= dtpLvTo.Value.Date)
                        {



                            if (txtCode.Text.StartsWith("I"))
                            {
                                sh = subShft.GetEmShift(txtCode.Text, rdate.ToString("yyyyMM"));
                            }
                            else if (txtCode.Text.StartsWith("7"))
                            {
                                sh = tnShft.GetEmShift(txtCode.Text, rdate.ToString("yyyyMM"));
                            }
                            else
                            {
                                sh = emShft.GetEmShift(txtCode.Text, rdate.ToString("yyyyMM"));
                            }

                            if (sh != null)
                            {
                                string shft = sh.DateShift(rdate);
                                if (shft == "D" || shft == "N")
                                {
                                    day++;
                                }

                            }
                            rdate = rdate.AddDays(1);
                        }




                        textBox1.Text = day.ToString();
                        lblTimeUnit.Text = "Day";
                    }
                    else
                    {
                        CheckShift();

                    }

                    if (dtpLvFrom.Value.Year != DateTime.Today.Year)
                    {
                        leaveTotal_Control1.CalLeave(txtCode.Text, dtpLvFrom.Value.Date);
                    }
                }
            }
            catch
            {


            }
        }


        private void dtpLvTo_ValueChanged(object sender, EventArgs e)
        {
            if (dtpLvTo.Value.Date < dtpLvFrom.Value.Date)
            {
              //  dtpLvTo.Value = dtpLvFrom.Value;
            }

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Search();
        }

        private void txtSCode_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPressManager.Enter(e);
        }

        private void dgItems_SelectionChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (formAct == FormAction.Save)
            {

                try
                {
                    int index = dgItems.CurrentRow.Index;
                    EmployeeLealeRequestInfo item = (EmployeeLealeRequestInfo)searchData[index];
                    leaveRecord_Control3.Infromation = item;
                    ucl_ActionControl1.CurrentAction = FormActionType.Save;

                    EmployeeInfo emp = EmployeeService.Instance().Find(item.EmpCode);
                    if (item.EmpCode.StartsWith("I"))
                    {
                        emp = SubContractService.Instance().Find(item.EmpCode);
                    }
                    else if (item.EmpCode.StartsWith("7"))
                    {
                        emp = TraineeService.Instance().Find(item.EmpCode);
                    }


                    EmployeeInfo temp = (EmployeeInfo)empData_Control1.Information;
                    empData_Control1.Information = emp;
                    if (temp == null)
                    {
                        try
                        {
                            annualLeave_Control1.CalTotalAnnual(emp.Code, emp.JoinDate, item.LvDate);

                        }
                        catch
                        {
                            annualLeave_Control1.Clear();

                        }
                        try
                        {
                            leaveTotal_Control1.CalLeave(emp.Code, item.LvDate);
                        }
                        catch
                        {

                            leaveTotal_Control1.Clear();
                        }
                    }

                    else if (emp != null && emp.Code != temp.Code)
                    {
                        try
                        {
                            annualLeave_Control1.CalTotalAnnual(emp.Code, emp.JoinDate, item.LvDate);

                        }
                        catch
                        {
                            annualLeave_Control1.Clear();

                        }
                        try
                        {
                            leaveTotal_Control1.CalLeave(emp.Code, item.LvDate);
                        }
                        catch
                        {

                            leaveTotal_Control1.Clear();
                        }
                    }

                }
                catch
                {
                    empData_Control1.Information = null;
                    leaveTotal_Control1.Clear();
                    annualLeave_Control1.Clear();

                }
            }
            if (formAct == FormAction.New)
            {
                try
                {
                    int index = dgItems.SelectedRows[0].Index;
                    EmployeeLealeRequestInfo item = (EmployeeLealeRequestInfo)lvData[index];
                    leaveRecord_Control1.Infromation = item;
                    EmployeeInfo temp = (EmployeeInfo)empData_Control1.Information;

                    if (temp == null)
                    {

                        EmployeeInfo emp = EmployeeService.Instance().Find(item.EmpCode);
                        if (item.EmpCode.StartsWith("I"))
                        {
                            emp = SubContractService.Instance().Find(item.EmpCode);
                        }
                        else if (item.EmpCode.StartsWith("7"))
                        {
                            emp = TraineeService.Instance().Find(item.EmpCode);
                        }

                        empData_Control1.Information = emp;
                        annualLeave_Control1.CalTotalAnnual(emp.Code, emp.JoinDate, item.LvDate);
                        leaveTotal_Control1.CalLeave(emp.Code, item.LvDate);
                        //ucl_ActionControl1.CurrentAction = FormActionType.None;

                    }
                    // else
                    ucl_ActionControl1.CurrentAction = FormActionType.Save;
                }
                catch
                {
                    ucl_ActionControl1.CurrentAction = FormActionType.None;
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void dtpSFrom_ValueChanged(object sender, EventArgs e)
        {
            if (dtpSFrom.Value > dtpSTo.Value)
            {
                dtpSTo.Value = dtpSFrom.Value;
            }

        }

        private void dtpSTo_ValueChanged(object sender, EventArgs e)
        {
            if (dtpSFrom.Value > dtpSTo.Value)
            {
                dtpSFrom.Value = dtpSTo.Value;
            }
        }

        private void leaveTotal_Control1_Load(object sender, EventArgs e)
        {
            leaveTotal_Control1.emLvSvr = emlvSvr;
            leaveTotal_Control1.subLvSvr = sublvSvr;
            leaveTotal_Control1.tnLvSvr = tnlvSvr;
        }

        private void FrmLeaveRecord_KeyDown(object sender, KeyEventArgs e)
        {
            ucl_ActionControl1.OnActionKeyDown(sender, e);
        }

        private void leaveRecord_Control1_Enter(object sender, EventArgs e)
        {
            if (dgItems.Rows.Count > 0)
            {
                ucl_ActionControl1.CurrentAction = FormActionType.Save;
            }
        }

        private void leaveRecord_Control1_DataMove()
        {
            this.Save();
        }

        #endregion

        private void cmbPaySts_Enter(object sender, EventArgs e)
        {
            try
            {
                BasicInfo cmbSel = (BasicInfo)cmbLeaveType.SelectedItem;
                if (cmbSel.Code == "ABSE")
                {
                    cmbPaySts.SelectedIndex = 1;
                }
                else if (cmbSel.Code == "LATE")
                {
                    if (dtpLvFrom.Value.Date == dtpLvTo.Value.Date)
                    {
                        DateTime cmpDt = DateTime.Parse("01:00");
                        DateTime chkDt = DateTime.Parse(textBox1.Text);
                        if (chkDt >= cmpDt)
                        {
                            cmbPaySts.SelectedIndex = 1;
                        }
                        else
                        {
                            cmbPaySts.SelectedIndex = 0;
                        }
                    }
                    else
                    {

                    }
                }
            }
            catch
            {

            }

        }


        private string DecodeLanguage(string data)
        {
            StringBuilder output = new StringBuilder();

            if (data != null && data.Length > 0)
            {
                foreach (char c in data)
                {
                    int ascii = (int)c;

                    if (ascii > 160)
                        ascii += 3424;

                    output.Append((char)ascii);
                }
            }

            return output.ToString();
        }
        private string EncodeLanguage(string data)
        {
            StringBuilder output = new StringBuilder();
            if (data != null && data.Length > 0)
            {
                foreach (char c in data)
                {
                    int ascii = (int)c;

                    if (ascii >= 160)
                        ascii -= 3424;

                    output.Append((char)ascii);
                }
            }
            return output.ToString();
        }




        SqlConnectDB oSqlHRM = new SqlConnectDB("dbHRM");
        private void InsertLVRQ_REQ(EmployeeLealeRequestInfo lvReq)
        {

            DataTable dtCheck = new DataTable();
            string strCheck = @"SELECT CODE,REQ_STATUS,ProgBit FROM LVRQ_REQ WHERE CODE=@CODE AND CDATE=@CDATE AND [TYPE]=@TYPE AND LVFROM=@LVFROM AND DOC_ID=@DOC_ID ";
            SqlCommand cmdCheck = new SqlCommand();
            cmdCheck.CommandText = strCheck;
            cmdCheck.Parameters.Add(new SqlParameter("@CODE", lvReq.EmpCode));
            cmdCheck.Parameters.Add(new SqlParameter("@CDATE", lvReq.LvDate.ToString("yyyy-MM-dd")));
            cmdCheck.Parameters.Add(new SqlParameter("@TYPE", lvReq.LvType));
            cmdCheck.Parameters.Add(new SqlParameter("@LVFROM", lvReq.LvFrom));
            cmdCheck.Parameters.Add(new SqlParameter("@DOC_ID", lvReq.DocId));
            dtCheck = oSqlHRM.Query(cmdCheck);

            DateTime updDate = new DateTime(1900, 1, 1);
            if (lvReq.LastUpDateDateTime > new DateTime(2000, 1, 1)) {
                updDate = lvReq.LastUpDateDateTime;
            }

            if (dtCheck.Rows.Count == 0)
            {                
                //***********************************************************
                //                  INSERT OTRQ_REQ
                //***********************************************************

                

                string strInstr = @"INSERT INTO LVRQ_REQ ([CODE],[CDATE],[TYPE],[LVFROM],[DOC_ID],[LVTO],[TIMES],[TOTAL],[SALSTS],[LVNO],[REASON],
                                        [ADD_DATE],[EDIT_BY],[UPD_BY],[UPD_DT],[APPROVE_BY],[APPROVE_DT],[REQ_STATUS],[ProgBit],[Remark]) 
                                    VALUES (@CODE,@CDATE,@TYPE,@LVFROM,@DOC_ID,@LVTO,@TIMES,@TOTAL,@SALSTS,@LVNO,@REASON,
                                            @ADD_DATE,@EDIT_BY,@UPD_BY,@UPD_DT,@APPROVE_BY,@APPROVE_DT,@REQ_STATUS,@ProgBit,@Remark)";
                SqlCommand cmdInstr = new SqlCommand();
                cmdInstr.CommandText = strInstr;
                cmdInstr.Parameters.Add(new SqlParameter("@CODE", lvReq.EmpCode));
                cmdInstr.Parameters.Add(new SqlParameter("@CDATE", lvReq.LvDate.ToString("yyyy-MM-dd")));
                cmdInstr.Parameters.Add(new SqlParameter("@TYPE", lvReq.LvType));                
                cmdInstr.Parameters.Add(new SqlParameter("@LVFROM", lvReq.LvFrom));
                cmdInstr.Parameters.Add(new SqlParameter("@DOC_ID", lvReq.DocId));
                cmdInstr.Parameters.Add(new SqlParameter("@LVTO", lvReq.LvTo));
                cmdInstr.Parameters.Add(new SqlParameter("@TIMES", lvReq.TotalHour));
                cmdInstr.Parameters.Add(new SqlParameter("@TOTAL", lvReq.TotalMinute));
                cmdInstr.Parameters.Add(new SqlParameter("@SALSTS", lvReq.PayStatus));
                cmdInstr.Parameters.Add(new SqlParameter("@LVNO", lvReq.LvNo));
                cmdInstr.Parameters.Add(new SqlParameter("@REASON", lvReq.Reason));
                cmdInstr.Parameters.Add(new SqlParameter("@ADD_DATE", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                cmdInstr.Parameters.Add(new SqlParameter("@EDIT_BY", lvReq.CreateBy));
                cmdInstr.Parameters.Add(new SqlParameter("@UPD_BY", lvReq.LastUpdateBy));
                cmdInstr.Parameters.Add(new SqlParameter("@UPD_DT", updDate.ToString("yyyy-MM-dd HH:mm:ss")));
                cmdInstr.Parameters.Add(new SqlParameter("@APPROVE_BY", lvReq.LastUpdateBy));
                cmdInstr.Parameters.Add(new SqlParameter("@APPROVE_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                cmdInstr.Parameters.Add(new SqlParameter("@REQ_STATUS", "APPROVE"));
                cmdInstr.Parameters.Add(new SqlParameter("@ProgBit", "M"));
                cmdInstr.Parameters.Add(new SqlParameter("@Remark", "HR Manual input"));
                oSqlHRM.ExecuteCommand(cmdInstr);

            }
            else
            {
                
                string strUpd = @"UPDATE LVRQ_REQ SET [TYPE]=@TYPE, [LVFROM]=@LVFROM, [LVTO]=@LVTO, [TIMES]=@TIMES, [TOTAL]=@TOTAL,
                                    [SALSTS]=@SALSTS, [LVNO]=@LVNO, [REASON]=@REASON, [UPD_BY]=@UPD_BY, [UPD_DT]=@UPD_DT, [APPROVE_BY]=@APPROVE_BY, 
                                    [APPROVE_DT]=@APPROVE_DT, [REQ_STATUS]=@REQ_STATUS, [ProgBit]=@ProgBit, [Remark]=@Remark                                     
                                  WHERE CODE=@CODE AND CDATE=@CDATE AND DOC_ID=@DOC_ID  ";
                SqlCommand cmdUpd = new SqlCommand();
                cmdUpd.CommandText = strUpd;
                cmdUpd.Parameters.Add(new SqlParameter("@CODE", lvReq.EmpCode));
                cmdUpd.Parameters.Add(new SqlParameter("@CDATE", lvReq.LvDate.ToString("yyyy-MM-dd")));
                cmdUpd.Parameters.Add(new SqlParameter("@TYPE", lvReq.LvType));
                cmdUpd.Parameters.Add(new SqlParameter("@LVFROM", lvReq.LvFrom));
                cmdUpd.Parameters.Add(new SqlParameter("@DOC_ID", lvReq.DocId));
                cmdUpd.Parameters.Add(new SqlParameter("@LVTO", lvReq.LvTo));
                cmdUpd.Parameters.Add(new SqlParameter("@TIMES", lvReq.TotalHour));
                cmdUpd.Parameters.Add(new SqlParameter("@TOTAL", lvReq.TotalMinute));
                cmdUpd.Parameters.Add(new SqlParameter("@SALSTS", lvReq.PayStatus));
                cmdUpd.Parameters.Add(new SqlParameter("@LVNO", lvReq.LvNo));
                cmdUpd.Parameters.Add(new SqlParameter("@REASON", lvReq.Reason));
                cmdUpd.Parameters.Add(new SqlParameter("@UPD_BY", lvReq.LastUpdateBy));
                cmdUpd.Parameters.Add(new SqlParameter("@UPD_DT", updDate.ToString("yyyy-MM-dd HH:mm:ss")));
                cmdUpd.Parameters.Add(new SqlParameter("@APPROVE_BY", lvReq.LastUpdateBy));
                cmdUpd.Parameters.Add(new SqlParameter("@APPROVE_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                cmdUpd.Parameters.Add(new SqlParameter("@REQ_STATUS", "APPROVE"));
                cmdUpd.Parameters.Add(new SqlParameter("@ProgBit", "M"));
                cmdUpd.Parameters.Add(new SqlParameter("@Remark", "HR Manual input"));
                oSqlHRM.ExecuteCommand(cmdUpd);
            }

            try
            {
                string strDelTemp = @"DELETE FROM LVRQ_ACTION WHERE [CODE]=@CODE AND [CDATE]=@CDATE AND [TYPE]=@TYPE AND [LVFROM]=@LVFROM AND [DOC_ID]=@DOC_ID ";
                SqlCommand cmdDelTemp = new SqlCommand();
                cmdDelTemp.CommandText = strDelTemp;
                cmdDelTemp.Parameters.Add(new SqlParameter("@CODE", lvReq.EmpCode));
                cmdDelTemp.Parameters.Add(new SqlParameter("@CDATE", lvReq.LvDate.ToString("yyyy-MM-dd")));
                cmdDelTemp.Parameters.Add(new SqlParameter("@TYPE", lvReq.LvType));
                cmdDelTemp.Parameters.Add(new SqlParameter("@LVFROM", lvReq.LvFrom));
                cmdDelTemp.Parameters.Add(new SqlParameter("@DOC_ID", lvReq.DocId));
                oSqlHRM.ExecuteCommand(cmdDelTemp);
            }
            catch { }


            //***********************************************************
            //                  INSERT LOG
            //***********************************************************
            Random r = new Random();
            int n = r.Next(1, 999);
            string strLog = @"INSERT INTO LVRQ_LOG (NBR,CDATE,CODE,TYPE,DATA_ACTION,DATA_DATE,DATA_REMARK) VALUES 
                                (@NBR,@CDATE,@CODE,@TYPE,@DATA_ACTION,@DATA_DATE,@DATA_REMARK) ";
            SqlCommand cmdLog = new SqlCommand();
            cmdLog.CommandText = strLog;
            cmdLog.Parameters.Add(new SqlParameter("@NBR", DateTime.Now.ToString("yyyyMMddHHmmss") + n.ToString("D3")));
            cmdLog.Parameters.Add(new SqlParameter("@CDATE", lvReq.LvDate.ToString("yyyy-MM-dd")));
            cmdLog.Parameters.Add(new SqlParameter("@CODE", lvReq.EmpCode));
            cmdLog.Parameters.Add(new SqlParameter("@TYPE", lvReq.LvType));
            cmdLog.Parameters.Add(new SqlParameter("@DATA_ACTION", "LV_ADD_LEAVE"));
            cmdLog.Parameters.Add(new SqlParameter("@DATA_DATE", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            cmdLog.Parameters.Add(new SqlParameter("@DATA_REMARK", "HR INPUT : " + lvReq.CreateBy));
            oSqlHRM.ExecuteCommand(cmdLog);

        }



        private void DeleteLVRQ_REQ(EmployeeLealeRequestInfo lvReq)
        {

            DataTable dtCheck = new DataTable();
            string strCheck = @"SELECT CODE,REQ_STATUS,ProgBit FROM LVRQ_REQ WHERE CODE=@CODE AND CDATE=@CDATE AND [TYPE]=@TYPE AND LVFROM=@LVFROM AND DOC_ID=@DOC_ID ";
            SqlCommand cmdCheck = new SqlCommand();
            cmdCheck.CommandText = strCheck;
            cmdCheck.Parameters.Add(new SqlParameter("@CODE", lvReq.EmpCode));
            cmdCheck.Parameters.Add(new SqlParameter("@CDATE", lvReq.LvDate.ToString("yyyy-MM-dd")));
            cmdCheck.Parameters.Add(new SqlParameter("@TYPE", lvReq.LvType));
            cmdCheck.Parameters.Add(new SqlParameter("@LVFROM", lvReq.LvFrom));
            cmdCheck.Parameters.Add(new SqlParameter("@DOC_ID", lvReq.DocId));
            dtCheck = oSqlHRM.Query(cmdCheck);

            if (dtCheck.Rows.Count > 0)
            {
                string strDel = @"DELETE FROM LVRQ_REQ WHERE CODE=@CODE AND CDATE=@CDATE AND [TYPE]=@TYPE AND LVFROM=@LVFROM AND DOC_ID=@DOC_ID ";
                SqlCommand cmdDel = new SqlCommand();
                cmdDel.CommandText = strDel;
                cmdDel.Parameters.Add(new SqlParameter("@CODE", lvReq.EmpCode));
                cmdDel.Parameters.Add(new SqlParameter("@CDATE", lvReq.LvDate.ToString("yyyy-MM-dd")));
                cmdDel.Parameters.Add(new SqlParameter("@TYPE", lvReq.LvType));
                cmdDel.Parameters.Add(new SqlParameter("@LVFROM", lvReq.LvFrom));
                cmdDel.Parameters.Add(new SqlParameter("@DOC_ID", lvReq.DocId));
                oSqlHRM.ExecuteCommand(cmdDel);

                /*string strUpd = @"UPDATE LVRQ_REQ SET APPROVE_BY=@APPROVE_BY, APPROVE_DT=@APPROVE_DT, REQ_STATUS=@REQ_STATUS, ProgBit=@ProgBit 
                                  WHERE CODE=@CODE AND CDATE=@CDATE AND [TYPE]=@TYPE AND LVFROM=@LVFROM AND DOC_ID=@DOC_ID  ";
                SqlCommand cmdUpd = new SqlCommand();
                cmdUpd.CommandText = strUpd;
                cmdUpd.Parameters.Add(new SqlParameter("@APPROVE_BY", lvReq.LastUpdateBy));
                cmdUpd.Parameters.Add(new SqlParameter("@APPROVE_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                cmdUpd.Parameters.Add(new SqlParameter("@REQ_STATUS", "REJECT"));
                cmdUpd.Parameters.Add(new SqlParameter("@ProgBit", "M"));
                cmdUpd.Parameters.Add(new SqlParameter("@CODE", lvReq.EmpCode));
                cmdUpd.Parameters.Add(new SqlParameter("@CDATE", lvReq.LvDate.ToString("yyyy-MM-dd")));
                cmdUpd.Parameters.Add(new SqlParameter("@TYPE", lvReq.LvType));
                cmdUpd.Parameters.Add(new SqlParameter("@LVFROM", lvReq.LvFrom));
                cmdUpd.Parameters.Add(new SqlParameter("@DOC_ID", lvReq.DocId));
                oSqlHRM.ExecuteCommand(cmdUpd);
                 * */
            }


            try
            {
                string strDelTemp = @"DELETE FROM LVRQ_ACTION WHERE [CODE]=@CODE AND [CDATE]=@CDATE AND [TYPE]=@TYPE AND [LVFROM]=@LVFROM AND [DOC_ID]=@DOC_ID ";
                SqlCommand cmdDelTemp = new SqlCommand();
                cmdDelTemp.CommandText = strDelTemp;
                cmdDelTemp.Parameters.Add(new SqlParameter("@CODE", lvReq.EmpCode));
                cmdDelTemp.Parameters.Add(new SqlParameter("@CDATE", lvReq.LvDate.ToString("yyyy-MM-dd")));
                cmdDelTemp.Parameters.Add(new SqlParameter("@TYPE", lvReq.LvType));
                cmdDelTemp.Parameters.Add(new SqlParameter("@LVFROM", lvReq.LvFrom));
                cmdDelTemp.Parameters.Add(new SqlParameter("@DOC_ID", lvReq.DocId));
                oSqlHRM.ExecuteCommand(cmdDelTemp);
            }
            catch { }


            //***********************************************************
            //                  INSERT LOG
            //***********************************************************
            Random r = new Random();
            int n = r.Next(1, 999);
            string strLog = @"INSERT INTO LVRQ_LOG (NBR,CDATE,CODE,TYPE,DATA_ACTION,DATA_DATE,DATA_REMARK) VALUES 
                                (@NBR,@CDATE,@CODE,@TYPE,@DATA_ACTION,@DATA_DATE,@DATA_REMARK) ";
            SqlCommand cmdLog = new SqlCommand();
            cmdLog.CommandText = strLog;
            cmdLog.Parameters.Add(new SqlParameter("@NBR", DateTime.Now.ToString("yyyyMMddHHmmss") + n.ToString("D3")));
            cmdLog.Parameters.Add(new SqlParameter("@CDATE", lvReq.LvDate.ToString("yyyy-MM-dd")));
            cmdLog.Parameters.Add(new SqlParameter("@CODE", lvReq.EmpCode));
            cmdLog.Parameters.Add(new SqlParameter("@TYPE", lvReq.LvType));
            cmdLog.Parameters.Add(new SqlParameter("@DATA_ACTION", "LV_DELETE"));
            cmdLog.Parameters.Add(new SqlParameter("@DATA_DATE", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            cmdLog.Parameters.Add(new SqlParameter("@DATA_REMARK", "HR INPUT : " + lvReq.LastUpdateBy));
            oSqlHRM.ExecuteCommand(cmdLog);

        }

        private void btnExport_Click(object sender, EventArgs e)
        {

            if(dgItems.Rows.Count > 0)
            {

                saveFileDlg.FileName = "";
                saveFileDlg.RestoreDirectory = true;
                saveFileDlg.Filter = "CSV Files (.csv)|*.csv;";
                DialogResult dlg = saveFileDlg.ShowDialog();
                if (dlg == DialogResult.OK)
                {
                    StreamWriter sw = new StreamWriter(saveFileDlg.FileName, false);
                    int iColCount = dgItems.ColumnCount;
                    

                    for (int i = 0; i < iColCount; i++)
                    {
                        

                        sw.Write(dgItems.Columns[i].HeaderText.ToString());
                        if (i < iColCount - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    sw.Write(sw.NewLine);

                    // Now write all the rows.

                    foreach (DataGridViewRow dr in dgItems.Rows)
                    {
                        for (int i = 0; i < iColCount; i++)
                        {
                            if (!Convert.IsDBNull(dr.Cells[i]))
                            {                                
                                sw.Write(DecodeLanguage(dr.Cells[i].Value.ToString()));
                            }
                            if (i < iColCount - 1)

                            {
                                sw.Write(",");
                            }
                        }
                        sw.Write(sw.NewLine);
                    }
                    sw.Close();


                    MessageBox.Show("Success");

                } // end if

                
            } // end if
            else
            {
                MessageBox.Show("No data export");
            }
        }
    }
}
   