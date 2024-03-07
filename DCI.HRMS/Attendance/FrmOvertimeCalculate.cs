using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Service;
using DCI.HRMS.Service.Trainee;
using DCI.HRMS.Model.Attendance;
using DCI.HRMS.Common;
using System.Collections;
using DCI.HRMS.Util;
using DCI.HRMS.Model.Personal;
using DCI.HRMS.Model.Organize;
using DCI.HRMS.Model;
using DCI.HRMS.Model.Common;

namespace DCI.HRMS.Attendance
{
    public partial class FrmOvertimeCalculate : Form
    {
 
        private OtService otsvr = OtService.Instance();
        private TraineeOtService tnOtSvr = TraineeOtService.Instance();
        private ShiftService shiftsrv = ShiftService.Instance();

        private DictionaryService dictSvr = DictionaryService.Instance();
        private EmployeeService empsrv = EmployeeService.Instance();
        private TraineeService tnSvr = TraineeService.Instance();
        private OtRequestInfo otreq = new OtRequestInfo();
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
        private readonly string[] colNameS = new string[] { "RqDate", "RqId", "EmpCode", "EmpName", "DVCD", "Grpot", "Type", "OTFrom", "OTTo", "OT1", "OT1.5", "OT2", "OT3", "Result", "TimeCard", "Shift", "AddBy", "AddDate", "UpdateBy", "UpdateDate" };
        private readonly string[] propNameS = new string[] { "OtDate", "ReqId", "EmpCode", "EmpName", "DVCD", "Grpot", "EmpType", "OTFrom", "OtTo", "Rate1", "Rate15", "Rate2", "Rate3", "CalRest", "TimeCard", "Shift", "CreateBy", "CreateDateTime", "LastUpdateBy", "LastUpDateDateTime" };

        public FrmOvertimeCalculate()
        {
            InitializeComponent();
        }

        private void FrmOvertimeCalculate_Load(object sender, EventArgs e)
        {









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

         


       

            otsearch = new ArrayList();
            otadd = new ArrayList();

            AddGridViewColumnsS();
            FillDataGrid();
            cboWType.SelectedIndex = 0;



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
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            DateTime calDate = dateTimePicker2.Value.Date;
            if (MessageBox.Show("คุณต้องการคำณวน OT วันที่ " + calDate.ToString("dd/MM/yyyy") + " ใช่หรือไม่", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                otsearch = new ArrayList();
                this.Cursor = Cursors.WaitCursor;
                stsMgr.Status = "Collecting OT data";

                otsearch = new ArrayList();
                chkY.Checked = true;
                chkT.Checked = true;
                chkO.Checked = true;
                chkR.Checked = true;

                MonthShiftInfo mtSh = shSvr.GetShift("D1", calDate.ToString("yyyyMM"));
                string dtSh = mtSh.DateShift(calDate);
                WorkingHourInfo wkD = tmSvr.GetWorkingHour(calDate, "D");
                WorkingHourInfo wkN = tmSvr.GetWorkingHour(calDate, "N");

                ArrayList otLs = otsvr.GetOTRequest("%", "%", "%", calDate, calDate.AddDays(1), "%", "%", "%", "%", "%", "%", "%");
                stsMgr.MaxProgress = otLs.Count;
                stsMgr.Progress = 0;
                foreach (OtRequestInfo item in otLs)
                {
                    OtRequestInfo bkItem = item;
                    stsMgr.Status = "Calculating " + item.EmpCode;
                    stsMgr.Progress++;
                    item.NFrom = item.OtFrom;
                    item.NTo = item.OtTo;
                    item.N1 = item.Rate1;
                    item.N15 = item.Rate15;
                    item.N2 = item.Rate2;
                    item.N3 = item.Rate3;
                  
                    item.LastUpdateBy = appMgr.UserAccount.AccountId;

                    DateTime otfrom = DateTime.Parse(item.OtDate.ToString("dd/MM/yyyy ") + item.OtFrom);
                    DateTime otTo = DateTime.Parse(item.OtDate.ToString("dd/MM/yyyy ") + item.OtTo);


                    DateTime rate1from = new DateTime(1900,1,1);
                    DateTime rate1To = new DateTime(1900,1,1);
                    DateTime rate15from = new DateTime(1900,1,1);
                    DateTime rate15To = new DateTime(1900,1,1);
                    DateTime rate2from = new DateTime(1900,1,1);
                    DateTime rate2To = new DateTime(1900,1,1);
                    DateTime rate3from = new DateTime(1900,1,1);
                    DateTime rate3To = new DateTime(1900,1,1);

                    if (otfrom > otTo)
                    {
                        otTo = otTo.AddDays(1);
                    }
                    if (item.OtDate == calDate && otfrom >= wkD.FirstStart || item.OtDate == calDate.AddDays(1) && otfrom > wkN.SecondEnd && otfrom < wkD.FirstStart.AddDays(1))
                    {

                        EmployeeInfo emp = empsrv.Find(item.EmpCode);
                        EmployeeWorkTimeInfo emWk = new EmployeeWorkTimeInfo();

                        emWk = tmSvr.GetEmployeeWorkingHour(item.EmpCode, calDate);
                        //Check Emp Type.
                        if (emp.WorkType != item.EmpType)
                        {
                            item.CalRest = "O";
                            if (emWk.WorkFrom != DateTime.MinValue)
                            {
                                item.TimeCard = emWk.WorkFrom.ToString("HH:mm") + "-";
                            }
                            else
                            {
                                item.TimeCard = "NO IN" + "-";
                            }
                            if (emWk.WorkTo != DateTime.MinValue)
                            {
                                item.TimeCard += emWk.WorkTo.ToString("HH:mm");
                            }
                            else
                            {
                                item.TimeCard += "NO OUT";
                            }
                            if (emWk.WorkFrom == DateTime.MinValue && emWk.WorkTo == DateTime.MinValue)
                            {
                                item.TimeCard = emWk.Remark;

                            }
                            item.TimeCard += "(WTypeWrong)";


                            otsearch.Add(item);
                            if (chkAutoCut.Checked)
                            {
                                otsvr.UpdateOtRequestById(item);
                            }

                        }
                        //   if (!emWk.TimeOk  && ! emWk.Remark.Contains("LATE"))
                        else if (emWk.WorkFrom == DateTime.MinValue || emWk.WorkTo == DateTime.MinValue || emWk.Remark.Contains("(ABSE ") || emWk.Remark.Contains("(SICK "))
                        {
                            //Can not calculate.

                            item.CalRest = "O";
                            item.TimeCard = emWk.Remark;
                            if (emWk.Remark.Contains("ABSE") || emWk.Remark.Contains("SICK"))
                            {
                                item.CalRest = "R";

                                if (chkAutoCut.Checked)
                                {
                                    otsvr.DeleteOtRequest(item);
                                }
                            }
                            else if (emWk.Remark.Contains("ANNU") || emWk.Remark.Contains("PERS"))
                            {
                                ArrayList lvLs = lvrqSvr.GetAllLeave(item.EmpCode, calDate, calDate, "%");
                                if (lvLs != null)
                                {
                                    foreach (EmployeeLealeRequestInfo lvItem in lvLs)
                                    {
                                        DateTime lvFrom = DateTime.Parse(lvItem.LvFrom);
                                        DateTime lvTo = DateTime.Parse(lvItem.LvTo);
                                        if (lvTo < lvFrom)
                                        {
                                            lvTo = lvTo.AddDays(1);
                                        }
                                        TimeSpan tm = lvTo - lvFrom;
                                        if (tm.TotalHours > 6)
                                        {
                                            item.CalRest = "R";

                                            if (chkAutoCut.Checked)
                                            {
                                                otsvr.DeleteOtRequest(item);
                                            }

                                        }

                                    }

                                }
                            }
                            else if (emWk.Remark == "(NO OUT)")
                            {
                                item.TimeCard = "(" + emWk.WorkFrom.ToString("HH:MM") + "-NO OUT)";
                            }
                            else if (emWk.Remark == "(NO IN)")
                            {
                                item.TimeCard = "(NO IN-" + emWk.WorkTo.ToString("HH:mm") + ")";
                            }
                            otsearch.Add(item);
                            if (chkAutoCut.Checked)
                            {
                                otsvr.UpdateOtRequestById(item);
                            }
                        }
                        else
                        {

                            bool found = false;
                            bool rateOk = false;
                            bool otOk = true;
                            if (emWk.WorkFrom <= otfrom && emWk.WorkTo >= otTo)
                            {
                                //OK.
                                rateOk = true;
                                found = true;

                            }

                            else
                            {

                                if (item.Rate1 != "")
                                {
                                    if (item.Rate1From != "" && item.Rate1To != "")
                                    {
                                        rate1from = DateTime.Parse(item.OtDate.ToString("dd/MM/yyyy ") + item.Rate1From);
                                        rate1To = DateTime.Parse(item.OtDate.ToString("dd/MM/yyyy ") + item.Rate1To);
                                        found = true;
                                        if (rate1To < rate1from)
                                        {
                                            rate1To = rate1To.AddDays(1);
                                        }

                                        if (emWk.WorkFrom <= rate1from && emWk.WorkTo >= rate1To)
                                        {
                                            //OK.
                                            rateOk = true;
                                            item.CalRest = "Y";

                                        }
                                        else
                                        {
                                            if (emWk.WorkFrom <= rate1from && emWk.WorkTo <= rate1To && emWk.WorkTo >= rate1from.AddMinutes(30))
                                            {

                                                otOk = false;
                                                DateTime calMnTo = new DateTime(1900,1,1);
                                                bool toOk = false;
                                                for (calMnTo = rate1from.AddMinutes(30); calMnTo <= emWk.WorkTo; calMnTo = calMnTo.AddMinutes(15))
                                                {
                                                    toOk = true;
                                                }
                                                if (toOk)
                                                {
                                                    rateOk = true;
                                                    rate1To = calMnTo.AddMinutes(-15);
                                                }
                                                else
                                                {
                                                    //Cancel.
                                                    item.Rate1From = "";
                                                    item.Rate1To = "";
                                                    item.Rate1 = "";
                                                    item.CalRest = "T";
                                                }
                                            }
                                            else if (emWk.WorkFrom >= rate1from && emWk.WorkFrom.AddMinutes(30) <= rate1To && emWk.WorkTo >= rate1To)
                                            {
                                                otOk = false;
                                                bool ok = false;
                                                DateTime calMinute = new DateTime(1900,1,1);
                                                for (calMinute = rate1from; calMinute <= rate1To.AddMinutes(-30); calMinute = calMinute.AddMinutes(15))
                                                {
                                                    if (calMinute >= emWk.WorkFrom)
                                                    {
                                                        ok = true;
                                                        break;
                                                    }
                                                }

                                                if (ok)
                                                {
                                                    rateOk = true;
                                                    rate1from = calMinute;
                                                }
                                                else
                                                {
                                                    //Cancel.
                                                    item.Rate1From = "";
                                                    item.Rate1To = "";
                                                    item.Rate1 = "";
                                                    item.CalRest = "T";

                                                }
                                            }
                                            else if (emWk.WorkFrom >= rate1from && emWk.WorkFrom < rate1To && emWk.WorkTo <= rate1To)
                                            {
                                                otOk = false;
                                                bool ok = false;
                                                DateTime calMinute = new DateTime(1900,1,1);
                                                DateTime calMnTo = new DateTime(1900,1,1);
                                                for (calMinute = rate1from; calMinute <= emWk.WorkTo; calMinute = calMinute.AddMinutes(15))
                                                {
                                                    if (calMinute >= emWk.WorkFrom)
                                                    {
                                                        ok = true;
                                                        break;
                                                    }
                                                }

                                                if (ok)
                                                {
                                                    bool toOk = false;
                                                    for (calMnTo = calMinute.AddMinutes(30); calMnTo <= emWk.WorkTo; calMnTo = calMnTo.AddMinutes(15))
                                                    {
                                                        toOk = true;
                                                    }
                                                    if (toOk)
                                                    {
                                                        rateOk = true;
                                                        rate1from = calMinute;
                                                        rate1To = calMnTo.AddMinutes(-15);
                                                    }
                                                    else
                                                    {
                                                        //Cancel.
                                                        otOk = false;
                                                        item.Rate1From = "";
                                                        item.Rate1To = "";
                                                        item.Rate1 = "";
                                                        item.CalRest = "T";
                                                    }
                                                }
                                                else
                                                {
                                                    //Cancel.
                                                    otOk = false;
                                                    item.Rate1From = "";
                                                    item.Rate1To = "";
                                                    item.Rate1 = "";
                                                    item.CalRest = "T";
                                                }
                                            }
                                            else
                                            {
                                                //Cancel.
                                                otOk = false;
                                                item.Rate1From = "";
                                                item.Rate1To = "";
                                                item.Rate1 = "";
                                                item.CalRest = "T";
                                            }
                                            //Recalculate OT total.
                                            if (item.Rate1 != "")
                                            {

                                                // Check if TimeCard between break time.
                                                if (rate1from > wkD.FirstEnd && rate1from < wkD.SecondStart)
                                                {
                                                    if (rate1To >= wkD.SecondStart)
                                                    {
                                                        rate1from = wkD.SecondStart;
                                                    }
                                                    else
                                                    {
                                                        // In-Out during break time.
                                                        //Cancel.
                                                        otOk = false;
                                                        item.Rate1From = "";
                                                        item.Rate1To = "";
                                                        item.Rate1 = "";
                                                        item.CalRest = "T";
                                                    }
                                                }
                                                else if (rate1To > wkD.FirstEnd && rate1To < wkD.SecondStart)
                                                {
                                                    if (rate1from <= wkD.FirstEnd)
                                                    {
                                                        rate1To = wkD.FirstEnd;
                                                    }
                                                    else
                                                    {
                                                        //In-Out during break time.
                                                        //Cancel.
                                                        otOk = false;
                                                        item.Rate1From = "";
                                                        item.Rate1To = "";
                                                        item.Rate1 = "";
                                                        item.CalRest = "T";
                                                    }
                                                }
                                                else if (rate1from > wkN.FirstEnd && rate1from < wkN.SecondStart)
                                                {
                                                    if (rate1To >= wkN.SecondStart)
                                                    {
                                                        rate1from = wkN.SecondStart;
                                                    }
                                                    else
                                                    {
                                                        // In-Out during break time.
                                                        //Cancel.
                                                        otOk = false;
                                                        item.Rate1From = "";
                                                        item.Rate1To = "";
                                                        item.Rate1 = "";
                                                        item.CalRest = "T";
                                                    }
                                                }
                                                else if (rate1To > wkN.FirstEnd && rate1To < wkN.SecondStart)
                                                {
                                                    if (rate1from <= wkN.FirstEnd)
                                                    {
                                                        rate1To = wkN.FirstEnd;
                                                    }
                                                    else
                                                    {
                                                        //In-Out during break time.
                                                        //Cancel.
                                                        otOk = false;
                                                        item.Rate1From = "";
                                                        item.Rate1To = "";
                                                        item.Rate1 = "";
                                                        item.CalRest = "T";
                                                    }
                                                }
                                                if (item.Rate1 != "")
                                                {
                                                    TimeSpan tt = rate1To - rate1from;

                                                    if (tt.TotalMinutes < 30)
                                                    {
                                                        //Cancel.
                                                        otOk = false;
                                                        item.Rate1From = "";
                                                        item.Rate1To = "";
                                                        item.Rate1 = "";
                                                        item.CalRest = "T";
                                                    }
                                                    else
                                                    {
                                                        //Deduct break time from total.
                                                        if (wkD.FirstEnd >= rate1from && wkD.SecondStart <= rate1To)
                                                        {
                                                            tt = tt - (wkD.SecondStart - wkD.FirstEnd);
                                                        }
                                                        if (wkN.FirstEnd >= rate1from && wkN.SecondStart <= rate1To)
                                                        {
                                                            tt = tt - (wkN.SecondStart - wkN.FirstEnd);
                                                        }

                                                        item.Rate1From = rate1from.ToString("HH:mm");
                                                        item.Rate1To = rate1To.ToString("HH:mm");
                                                        item.Rate1 = tt.Hours.ToString("00") + ":" + tt.Minutes.ToString("00");

                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //Can not calculate.
                                        otOk = false;
                                    }
                                }
                                if (item.Rate15 != "")
                                {
                                    if (item.Rate15From != "" && item.Rate15To != "")
                                    {
                                        rate15from = DateTime.Parse(item.OtDate.ToString("dd/MM/yyyy ") + item.Rate15From);
                                        rate15To = DateTime.Parse(item.OtDate.ToString("dd/MM/yyyy ") + item.Rate15To);
                                        found = true;
                                        if (rate15To < rate15from)
                                        {
                                            rate15To = rate15To.AddDays(1);
                                        }

                                        if (emWk.WorkFrom <= rate15from && emWk.WorkTo >= rate15To)
                                        {
                                            //OK.
                                            rateOk = true;

                                            item.CalRest = "Y";

                                        }
                                        else
                                        {
                                            if (emWk.WorkFrom <= rate15from && emWk.WorkTo <= rate15To && emWk.WorkTo >= rate15from.AddMinutes(30))
                                            {

                                                otOk = false;
                                                DateTime calMnTo = new DateTime(1900,1,1);
                                                bool toOk = false;
                                                for (calMnTo = rate15from.AddMinutes(30); calMnTo <= emWk.WorkTo; calMnTo = calMnTo.AddMinutes(15))
                                                {
                                                    toOk = true;
                                                }
                                                if (toOk)
                                                {
                                                    rateOk = true;
                                                    rate15To = calMnTo.AddMinutes(-15);
                                                }
                                                else
                                                {
                                                    //Cancel.
                                                    item.Rate15From = "";
                                                    item.Rate15To = "";
                                                    item.Rate15 = "";
                                                    item.CalRest = "T";
                                                }
                                            }
                                            else if (emWk.WorkFrom >= rate15from && emWk.WorkFrom.AddMinutes(30) <= rate15To && emWk.WorkTo >= rate15To)
                                            {
                                                otOk = false;
                                                bool ok = false;
                                                DateTime calMinute = new DateTime(1900,1,1);
                                                for (calMinute = rate15from; calMinute <= rate15To.AddMinutes(-30); calMinute = calMinute.AddMinutes(15))
                                                {
                                                    if (calMinute >= emWk.WorkFrom)
                                                    {
                                                        ok = true;
                                                        break;
                                                    }
                                                }

                                                if (ok)
                                                {
                                                    rateOk = true;
                                                    rate15from = calMinute;
                                                }
                                                else
                                                {
                                                    //Cancel.
                                                    item.Rate15From = "";
                                                    item.Rate15To = "";
                                                    item.Rate15 = "";
                                                    item.CalRest = "T";

                                                }
                                            }
                                            else if (emWk.WorkFrom >= rate15from && emWk.WorkFrom < rate15To && emWk.WorkTo <= rate15To)
                                            {
                                                otOk = false;
                                                bool ok = false;
                                                DateTime calMinute = new DateTime(1900,1,1);
                                                DateTime calMnTo = new DateTime(1900,1,1);
                                                for (calMinute = rate15from; calMinute <= emWk.WorkTo; calMinute = calMinute.AddMinutes(15))
                                                {
                                                    if (calMinute >= emWk.WorkFrom)
                                                    {
                                                        ok = true;
                                                        break;
                                                    }
                                                }

                                                if (ok)
                                                {
                                                    bool toOk = false;
                                                    for (calMnTo = calMinute.AddMinutes(30); calMnTo <= emWk.WorkTo; calMnTo = calMnTo.AddMinutes(15))
                                                    {
                                                        toOk = true;
                                                    }
                                                    if (toOk)
                                                    {
                                                        rateOk = true;
                                                        rate15from = calMinute;
                                                        rate15To = calMnTo.AddMinutes(-15);
                                                    }
                                                    else
                                                    {
                                                        //Cancel.
                                                        otOk = false;
                                                        item.Rate15From = "";
                                                        item.Rate15To = "";
                                                        item.Rate15 = "";
                                                        item.CalRest = "T";
                                                    }
                                                }
                                                else
                                                {
                                                    //Cancel.
                                                    otOk = false;
                                                    item.Rate15From = "";
                                                    item.Rate15To = "";
                                                    item.Rate15 = "";
                                                    item.CalRest = "T";
                                                }
                                            }
                                            else
                                            {
                                                //Cancel.
                                                otOk = false;
                                                item.Rate15From = "";
                                                item.Rate15To = "";
                                                item.Rate15 = "";
                                                item.CalRest = "T";
                                            }
                                            //Recalculate OT total.
                                            if (item.Rate15 != "")
                                            {

                                                // Check if TimeCard between break time.
                                                if (rate15from > wkD.FirstEnd && rate15from < wkD.SecondStart)
                                                {
                                                    if (rate15To >= wkD.SecondStart)
                                                    {
                                                        rate15from = wkD.SecondStart;
                                                    }
                                                    else
                                                    {
                                                        // In-Out during break time.
                                                        //Cancel.
                                                        otOk = false;
                                                        item.Rate15From = "";
                                                        item.Rate15To = "";
                                                        item.Rate15 = "";
                                                        item.CalRest = "T";
                                                    }
                                                }
                                                else if (rate15To > wkD.FirstEnd && rate15To < wkD.SecondStart)
                                                {
                                                    if (rate15from <= wkD.FirstEnd)
                                                    {
                                                        rate15To = wkD.FirstEnd;
                                                    }
                                                    else
                                                    {
                                                        //In-Out during break time.
                                                        //Cancel.
                                                        otOk = false;
                                                        item.Rate15From = "";
                                                        item.Rate15To = "";
                                                        item.Rate15 = "";
                                                        item.CalRest = "T";
                                                    }
                                                }
                                                else if (rate15from > wkN.FirstEnd && rate15from < wkN.SecondStart)
                                                {
                                                    if (rate15To >= wkN.SecondStart)
                                                    {
                                                        rate15from = wkN.SecondStart;
                                                    }
                                                    else
                                                    {
                                                        // In-Out during break time.
                                                        //Cancel.
                                                        otOk = false;
                                                        item.Rate15From = "";
                                                        item.Rate15To = "";
                                                        item.Rate15 = "";
                                                        item.CalRest = "T";
                                                    }
                                                }
                                                else if (rate15To > wkN.FirstEnd && rate15To < wkN.SecondStart)
                                                {
                                                    if (rate15from <= wkN.FirstEnd)
                                                    {
                                                        rate15To = wkN.FirstEnd;
                                                    }
                                                    else
                                                    {
                                                        //In-Out during break time.
                                                        //Cancel.
                                                        otOk = false;
                                                        item.Rate15From = "";
                                                        item.Rate15To = "";
                                                        item.Rate15 = "";
                                                        item.CalRest = "T";
                                                    }
                                                }
                                                if (item.Rate15 != "")
                                                {
                                                    TimeSpan tt = rate15To - rate15from;

                                                    if (tt.TotalMinutes < 30)
                                                    {
                                                        //Cancel.
                                                        otOk = false;
                                                        item.Rate15From = "";
                                                        item.Rate15To = "";
                                                        item.Rate15 = "";
                                                        item.CalRest = "T";
                                                    }
                                                    else
                                                    {
                                                        //Deduct break time from total.
                                                        if (wkD.FirstEnd >= rate15from && wkD.SecondStart <= rate15To)
                                                        {
                                                            tt = tt - (wkD.SecondStart - wkD.FirstEnd);
                                                        }
                                                        if (wkN.FirstEnd >= rate15from && wkN.SecondStart <= rate15To)
                                                        {
                                                            tt = tt - (wkN.SecondStart - wkN.FirstEnd);
                                                        }

                                                        item.Rate15From = rate15from.ToString("HH:mm");
                                                        item.Rate15To = rate15To.ToString("HH:mm");
                                                        item.Rate15 = tt.Hours.ToString("00") + ":" + tt.Minutes.ToString("00");

                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //Can not calculate.
                                        otOk = false;
                                    }
                                }
                                if (item.Rate2 != "")
                                {
                                    if (item.Rate2From != "" && item.Rate2To != "")
                                    {
                                        rate2from = DateTime.Parse(item.OtDate.ToString("dd/MM/yyyy ") + item.Rate2From);
                                        rate2To = DateTime.Parse(item.OtDate.ToString("dd/MM/yyyy ") + item.Rate2To);
                                        found = true;
                                        if (rate2To < rate2from)
                                        {
                                            rate2To = rate2To.AddDays(1);
                                        }

                                        if (emWk.WorkFrom <= rate2from && emWk.WorkTo >= rate2To)
                                        {
                                            //OK.
                                            rateOk = true;

                                            item.CalRest = "Y";

                                        }
                                        else
                                        {
                                            if (emWk.WorkFrom <= rate2from && emWk.WorkTo <= rate2To && emWk.WorkTo >= rate2from.AddMinutes(30))
                                            {

                                                otOk = false;
                                                DateTime calMnTo = new DateTime(1900,1,1);
                                                bool toOk = false;
                                                for (calMnTo = rate2from.AddMinutes(30); calMnTo <= emWk.WorkTo; calMnTo = calMnTo.AddMinutes(15))
                                                {
                                                    toOk = true;
                                                }
                                                if (toOk)
                                                {
                                                    rateOk = true;
                                                    rate2To = calMnTo.AddMinutes(-15);
                                                }
                                                else
                                                {
                                                    //Cancel.
                                                    item.Rate2From = "";
                                                    item.Rate2To = "";
                                                    item.Rate2 = "";
                                                    item.CalRest = "T";
                                                }
                                            }
                                            else if (emWk.WorkFrom >= rate2from && emWk.WorkFrom.AddMinutes(30) <= rate2To && emWk.WorkTo >= rate2To)
                                            {
                                                otOk = false;
                                                bool ok = false;
                                                DateTime calMinute = new DateTime(1900,1,1);
                                                for (calMinute = rate2from; calMinute <= rate2To.AddMinutes(-30); calMinute = calMinute.AddMinutes(15))
                                                {
                                                    if (calMinute >= emWk.WorkFrom)
                                                    {
                                                        ok = true;
                                                        break;
                                                    }
                                                }

                                                if (ok)
                                                {
                                                    rateOk = true;
                                                    rate2from = calMinute;
                                                }
                                                else
                                                {
                                                    //Cancel.
                                                    item.Rate2From = "";
                                                    item.Rate2To = "";
                                                    item.Rate2 = "";
                                                    item.CalRest = "T";

                                                }
                                            }
                                            else if (emWk.WorkFrom >= rate2from && emWk.WorkFrom < rate2To && emWk.WorkTo <= rate2To)
                                            {
                                                otOk = false;
                                                bool ok = false;
                                                DateTime calMinute = new DateTime(1900,1,1);
                                                DateTime calMnTo = new DateTime(1900,1,1);
                                                for (calMinute = rate2from; calMinute <= emWk.WorkTo; calMinute = calMinute.AddMinutes(15))
                                                {
                                                    if (calMinute >= emWk.WorkFrom)
                                                    {
                                                        ok = true;
                                                        break;
                                                    }
                                                }

                                                if (ok)
                                                {
                                                    bool toOk = false;
                                                    for (calMnTo = calMinute.AddMinutes(30); calMnTo <= emWk.WorkTo; calMnTo = calMnTo.AddMinutes(15))
                                                    {
                                                        toOk = true;
                                                    }
                                                    if (toOk)
                                                    {
                                                        rateOk = true;
                                                        rate2from = calMinute;
                                                        rate2To = calMnTo.AddMinutes(-15);
                                                    }
                                                    else
                                                    {
                                                        //Cancel.
                                                        otOk = false;
                                                        item.Rate2From = "";
                                                        item.Rate2To = "";
                                                        item.Rate2 = "";
                                                        item.CalRest = "T";
                                                    }
                                                }
                                                else
                                                {
                                                    //Cancel.
                                                    otOk = false;
                                                    item.Rate2From = "";
                                                    item.Rate2To = "";
                                                    item.Rate2 = "";
                                                    item.CalRest = "T";
                                                }
                                            }
                                            else
                                            {
                                                //Cancel.
                                                otOk = false;
                                                item.Rate2From = "";
                                                item.Rate2To = "";
                                                item.Rate2 = "";
                                                item.CalRest = "T";
                                            }
                                            //Recalculate OT total.
                                            if (item.Rate2 != "")
                                            {

                                                // Check if TimeCard between break time.
                                                if (rate2from > wkD.FirstEnd && rate2from < wkD.SecondStart)
                                                {
                                                    if (rate2To >= wkD.SecondStart)
                                                    {
                                                        rate2from = wkD.SecondStart;
                                                    }
                                                    else
                                                    {
                                                        // In-Out during break time.
                                                        //Cancel.
                                                        otOk = false;
                                                        item.Rate2From = "";
                                                        item.Rate2To = "";
                                                        item.Rate2 = "";
                                                        item.CalRest = "T";
                                                    }
                                                }
                                                else if (rate2To > wkD.FirstEnd && rate2To < wkD.SecondStart)
                                                {
                                                    if (rate2from <= wkD.FirstEnd)
                                                    {
                                                        rate2To = wkD.FirstEnd;
                                                    }
                                                    else
                                                    {
                                                        //In-Out during break time.
                                                        //Cancel.
                                                        otOk = false;
                                                        item.Rate2From = "";
                                                        item.Rate2To = "";
                                                        item.Rate2 = "";
                                                        item.CalRest = "T";
                                                    }
                                                }
                                                else if (rate2from > wkN.FirstEnd && rate2from < wkN.SecondStart)
                                                {
                                                    if (rate2To >= wkN.SecondStart)
                                                    {
                                                        rate2from = wkN.SecondStart;
                                                    }
                                                    else
                                                    {
                                                        // In-Out during break time.
                                                        //Cancel.
                                                        otOk = false;
                                                        item.Rate2From = "";
                                                        item.Rate2To = "";
                                                        item.Rate2 = "";
                                                        item.CalRest = "T";
                                                    }
                                                }
                                                else if (rate2To > wkN.FirstEnd && rate2To < wkN.SecondStart)
                                                {
                                                    if (rate2from <= wkN.FirstEnd)
                                                    {
                                                        rate2To = wkN.FirstEnd;
                                                    }
                                                    else
                                                    {
                                                        //In-Out during break time.
                                                        //Cancel.
                                                        otOk = false;
                                                        item.Rate2From = "";
                                                        item.Rate2To = "";
                                                        item.Rate2 = "";
                                                        item.CalRest = "T";
                                                    }
                                                }
                                                if (item.Rate2 != "")
                                                {
                                                    TimeSpan tt = rate2To - rate2from;

                                                    if (tt.TotalMinutes < 30)
                                                    {
                                                        //Cancel.
                                                        otOk = false;
                                                        item.Rate2From = "";
                                                        item.Rate2To = "";
                                                        item.Rate2 = "";
                                                        item.CalRest = "T";
                                                    }
                                                    else
                                                    {
                                                        //Deduct break time from total.
                                                        if (wkD.FirstEnd >= rate2from && wkD.SecondStart <= rate2To)
                                                        {
                                                            tt = tt - (wkD.SecondStart - wkD.FirstEnd);
                                                        }
                                                        if (wkN.FirstEnd >= rate2from && wkN.SecondStart <= rate2To)
                                                        {
                                                            tt = tt - (wkN.SecondStart - wkN.FirstEnd);
                                                        }

                                                        item.Rate2From = rate2from.ToString("HH:mm");
                                                        item.Rate2To = rate2To.ToString("HH:mm");
                                                        item.Rate2 = tt.Hours.ToString("00") + ":" + tt.Minutes.ToString("00");

                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //Can not calculate.
                                        otOk = false;
                                    }

                                }
                                if (item.Rate3 != "")
                                {

                                    if (item.Rate3From != "" && item.Rate3To != "")
                                    {
                                        rate3from = DateTime.Parse(item.OtDate.ToString("dd/MM/yyyy ") + item.Rate3From);
                                        rate3To = DateTime.Parse(item.OtDate.ToString("dd/MM/yyyy ") + item.Rate3To);
                                        found = true;
                                        if (rate3To < rate3from)
                                        {
                                            rate3To = rate3To.AddDays(1);
                                        }

                                        if (emWk.WorkFrom <= rate3from && emWk.WorkTo >= rate3To)
                                        {
                                            //OK.
                                            rateOk = true;

                                            item.CalRest = "Y";

                                        }
                                        else
                                        {
                                            if (emWk.WorkFrom <= rate3from && emWk.WorkTo <= rate3To && emWk.WorkTo >= rate3from.AddMinutes(30))
                                            {

                                                otOk = false;
                                                DateTime calMnTo = new DateTime(1900,1,1);
                                                bool toOk = false;
                                                for (calMnTo = rate3from.AddMinutes(30); calMnTo <= emWk.WorkTo; calMnTo = calMnTo.AddMinutes(15))
                                                {
                                                    toOk = true;
                                                }
                                                if (toOk)
                                                {
                                                    rateOk = true;
                                                    rate3To = calMnTo.AddMinutes(-15);
                                                }
                                                else
                                                {
                                                    //Cancel.
                                                    item.Rate3From = "";
                                                    item.Rate3To = "";
                                                    item.Rate3 = "";
                                                    item.CalRest = "T";
                                                }
                                            }
                                            else if (emWk.WorkFrom >= rate3from && emWk.WorkFrom.AddMinutes(30) <= rate3To && emWk.WorkTo >= rate3To)
                                            {
                                                otOk = false;
                                                bool ok = false;
                                                DateTime calMinute = new DateTime(1900,1,1);
                                                for (calMinute = rate3from; calMinute <= rate3To.AddMinutes(-30); calMinute = calMinute.AddMinutes(15))
                                                {
                                                    if (calMinute >= emWk.WorkFrom)
                                                    {
                                                        ok = true;
                                                        break;
                                                    }
                                                }

                                                if (ok)
                                                {
                                                    rateOk = true;
                                                    rate3from = calMinute;
                                                }
                                                else
                                                {
                                                    //Cancel.
                                                    item.Rate3From = "";
                                                    item.Rate3To = "";
                                                    item.Rate3 = "";
                                                    item.CalRest = "T";

                                                }
                                            }
                                            else if (emWk.WorkFrom >= rate3from && emWk.WorkFrom < rate3To && emWk.WorkTo <= rate3To)
                                            {
                                                otOk = false;
                                                bool ok = false;
                                                DateTime calMinute = new DateTime(1900,1,1);
                                                DateTime calMnTo = new DateTime(1900,1,1);
                                                for (calMinute = rate3from; calMinute <= emWk.WorkTo; calMinute = calMinute.AddMinutes(15))
                                                {
                                                    if (calMinute >= emWk.WorkFrom)
                                                    {
                                                        ok = true;
                                                        break;
                                                    }
                                                }

                                                if (ok)
                                                {
                                                    bool toOk = false;
                                                    for (calMnTo = calMinute.AddMinutes(30); calMnTo <= emWk.WorkTo; calMnTo = calMnTo.AddMinutes(15))
                                                    {
                                                        toOk = true;
                                                    }
                                                    if (toOk)
                                                    {
                                                        rateOk = true;
                                                        rate3from = calMinute;
                                                        rate3To = calMnTo.AddMinutes(-15);
                                                    }
                                                    else
                                                    {
                                                        //Cancel.
                                                        otOk = false;
                                                        item.Rate3From = "";
                                                        item.Rate3To = "";
                                                        item.Rate3 = "";
                                                        item.CalRest = "T";
                                                    }
                                                }
                                                else
                                                {
                                                    //Cancel.
                                                    otOk = false;
                                                    item.Rate3From = "";
                                                    item.Rate3To = "";
                                                    item.Rate3 = "";
                                                    item.CalRest = "T";
                                                }
                                            }
                                            else
                                            {
                                                //Cancel.
                                                otOk = false;
                                                item.Rate3From = "";
                                                item.Rate3To = "";
                                                item.Rate3 = "";
                                                item.CalRest = "T";
                                            }
                                            //Recalculate OT total.
                                            if (item.Rate3 != "")
                                            {

                                                // Check if TimeCard between break time.
                                                if (rate3from > wkD.FirstEnd && rate3from < wkD.SecondStart)
                                                {
                                                    if (rate3To >= wkD.SecondStart)
                                                    {
                                                        rate3from = wkD.SecondStart;
                                                    }
                                                    else
                                                    {
                                                        // In-Out during break time.
                                                        //Cancel.
                                                        otOk = false;
                                                        item.Rate3From = "";
                                                        item.Rate3To = "";
                                                        item.Rate3 = "";
                                                        item.CalRest = "T";
                                                    }
                                                }
                                                else if (rate3To > wkD.FirstEnd && rate3To < wkD.SecondStart)
                                                {
                                                    if (rate3from <= wkD.FirstEnd)
                                                    {
                                                        rate3To = wkD.FirstEnd;
                                                    }
                                                    else
                                                    {
                                                        //In-Out during break time.
                                                        //Cancel.
                                                        otOk = false;
                                                        item.Rate3From = "";
                                                        item.Rate3To = "";
                                                        item.Rate3 = "";
                                                        item.CalRest = "T";
                                                    }
                                                }
                                                else if (rate3from > wkN.FirstEnd && rate3from < wkN.SecondStart)
                                                {
                                                    if (rate3To >= wkN.SecondStart)
                                                    {
                                                        rate3from = wkN.SecondStart;
                                                    }
                                                    else
                                                    {
                                                        // In-Out during break time.
                                                        //Cancel.
                                                        otOk = false;
                                                        item.Rate3From = "";
                                                        item.Rate3To = "";
                                                        item.Rate3 = "";
                                                        item.CalRest = "T";
                                                    }
                                                }
                                                else if (rate3To > wkN.FirstEnd && rate3To < wkN.SecondStart)
                                                {
                                                    if (rate3from <= wkN.FirstEnd)
                                                    {
                                                        rate3To = wkN.FirstEnd;
                                                    }
                                                    else
                                                    {
                                                        //In-Out during break time.
                                                        //Cancel.
                                                        otOk = false;
                                                        item.Rate3From = "";
                                                        item.Rate3To = "";
                                                        item.Rate3 = "";
                                                        item.CalRest = "T";
                                                    }
                                                }
                                                if (item.Rate3 != "")
                                                {
                                                    TimeSpan tt = rate3To - rate3from;

                                                    if (tt.TotalMinutes < 30)
                                                    {
                                                        //Cancel.
                                                        otOk = false;
                                                        item.Rate3From = "";
                                                        item.Rate3To = "";
                                                        item.Rate3 = "";
                                                        item.CalRest = "T";
                                                    }
                                                    else
                                                    {
                                                        //Deduct break time from total.
                                                        if (wkD.FirstEnd >= rate3from && wkD.SecondStart <= rate3To)
                                                        {
                                                            tt = tt - (wkD.SecondStart - wkD.FirstEnd);
                                                        }
                                                        if (wkN.FirstEnd >= rate3from && wkN.SecondStart <= rate3To)
                                                        {
                                                            tt = tt - (wkN.SecondStart - wkN.FirstEnd);
                                                        }

                                                        item.Rate3From = rate3from.ToString("HH:mm");
                                                        item.Rate3To = rate3To.ToString("HH:mm");
                                                        item.Rate3 = tt.Hours.ToString("00") + ":" + tt.Minutes.ToString("00");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //Can not calculate.
                                        otOk = false;
                                    }
                                }
                            }

                            if (!found)
                            {
                                item.CalRest = "O";
                                item.TimeCard = emWk.WorkFrom.ToString("HH:mm") + "-" + emWk.WorkTo.ToString("HH:mm");

                                otsearch.Add(item);
                                //Update OT.
                                if (chkAutoCut.Checked)
                                {
                                    otsvr.UpdateOtRequestById(item);
                                }

                            }
                            else
                            {
                                if (rateOk)
                                {

                                    if (otOk)
                                    {

                                        item.CalRest = "Y";
                                        item.TimeCard = emWk.WorkFrom.ToString("HH:mm") + "-" + emWk.WorkTo.ToString("HH:mm");


                                    }
                                    else
                                    {
                                        DateTime otSt = DateTime.MaxValue;
                                        DateTime otEn = DateTime.MinValue;
                                        if (item.Rate1 != "")
                                        {
                                            otSt = rate1from;
                                        }
                                        if (item.Rate15 != "" && rate15from < otSt)
                                        {
                                            otSt = rate15from;
                                        }
                                        if (item.Rate2 != "" && rate2from < otSt)
                                        {
                                            otSt = rate2from;
                                        }
                                        if (item.Rate3 != "" && rate3from < otSt)
                                        {
                                            otSt = rate3from;
                                        }
                                        if (item.Rate1 != "")
                                        {
                                            otEn = rate1To;
                                        }
                                        if (item.Rate15 != "" && rate15To > otEn)
                                        {
                                            otEn = rate15To;
                                        }
                                        if (item.Rate2 != "" && rate2To > otEn)
                                        {
                                            otEn = rate2To;
                                        }
                                        if (item.Rate3 != "" && rate3To > otEn)
                                        {
                                            otEn = rate3To;
                                        }

                                        item.OtFrom = otSt.ToString("HH:mm");
                                        item.OtTo = otEn.ToString("HH:mm");
                                        item.CalRest = "T";
                                        item.TimeCard = emWk.WorkFrom.ToString("HH:mm") + "-" + emWk.WorkTo.ToString("HH:mm");

                                    }

                                    otsearch.Add(item);
                                    //Update OT.
                                    if (chkAutoCut.Checked)
                                    {
                                        otsvr.UpdateOtRequestById(item);
                                    }

                                }
                                else
                                {

                                    //Update OT.
                                    // item = bkItem;

                                    item.CalRest = "R";
                                    item.TimeCard = emWk.WorkFrom.ToString("HH:mm") + "-" + emWk.WorkTo.ToString("HH:mm");
                                    otsearch.Add(item);


                                    if (chkAutoCut.Checked)
                                    {  //Delete OT..
                                        otsvr.DeleteOtRequest(item);
                                    }

                                }
                            }

                        }
                    }

                }

                stsMgr.Progress = 0;
                stsMgr.Status = "Ready";
                this.Cursor = Cursors.Default;
                gvData = otsearch;
                FillDataGrid();

            }



        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {/*
            if (otsearch.Count != 0)
            {
                gvData = new ArrayList();
                foreach (OtRequestInfo item in otsearch)
                {
                    if (item.CalRest == "Y" && chkY.Checked)
                    {
                        gvData.Add(item);
                    }
                    if (item.CalRest == "T" && chkT.Checked)
                    {
                        gvData.Add(item);
                    }
                    if (item.CalRest == "O" && chkO.Checked)
                    {
                        gvData.Add(item);
                    }
                    if (item.CalRest == "R" && chkR.Checked)
                    {
                        gvData.Add(item);
                    }
                }
                FillDataGrid();

            }*/
        }

        private void dgItems_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyleDefault.ShowRowNumber(dgItems, e);
            if (dgItems["Result", e.RowIndex].Value.ToString() != "Y" && dgItems["Result", e.RowIndex].Value.ToString().Trim() != "")
            {
                dgItems.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;

            }
        }
        private void dgItems_SelectionChanged_1(object sender, EventArgs e)
        {
            try
            {
                otRequest_Control1.Information = new OtRequestInfo();

                int select = dgItems.SelectedRows[0].Index;
                OtRequestInfo selrq = (OtRequestInfo)dgItems.CurrentRow.DataBoundItem;
                // empDetail_Control1.ShftDate = selrq.OtDate;
                // empDetail_Control1.Information = selrq.EmpCode;
                if (selrq.EmpCode.StartsWith("7"))
                {
                    empData_Control3.Information = tnSvr.Find(selrq.EmpCode);
                }
                else
                {
                    empData_Control3.Information = empsrv.Find(selrq.EmpCode);
                }
                otRequest_Control1.Information = selrq;
                //  otRequest_Control1.Focus();
                
            }
            catch
            {
            }
        }

        private void otRequest_Control1_Save_Data()
        {
            // this.Save();

            OtRequestInfo selrq = (OtRequestInfo)dgItems.CurrentRow.DataBoundItem;
        
            ObjectInfo inform = new  ObjectInfo();
            string msg = "";
            inform.LastUpdateBy = appMgr.UserAccount.AccountId;
            inform.CreateBy = selrq.CreateBy;
            inform.CreateDateTime = selrq.CreateDateTime;
            selrq.Inform = inform;
            bool dtChange = false;
            /* Check If User want to change date of records*/
            if (otRequest_Control1.EnableEditDate)
            {
                dtChange = selrq.OtDate != ((OtRequestInfo)otRequest_Control1.Information).OtDate;
             
            }
             selrq = (OtRequestInfo)otRequest_Control1.Information;


             if (!dtChange)
             {
                 try
                 {

                     if (selrq.DocId.Trim() == "")
                     {
                         otsvr.UpdateOtRequest(selrq);
                     }
                     else
                     {
                         otsvr.UpdateOtRequestById(selrq);
                     }

                 }
                 catch (Exception ex)
                 {

                     MessageBox.Show("ไม่สามารถบันทึกข้อมูล Code =" + selrq.EmpCode + "ได้ เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                 }
             }
             else
             {




                 if (!CheckOtExit(selrq.EmpCode, selrq.OtDate, selrq.OtFrom, selrq.OtTo, ref msg))
                 {
                     try
                     {
                         if (selrq.DocId.Trim() == "")
                         {
                             otsvr.UpdateOtRequest(selrq);
                         }
                         else
                         {
                             otsvr.UpdateOtRequestById(selrq);
                         }
                     }
                     catch (Exception ex)
                     {

                         MessageBox.Show("ไม่สามารถบันทึกข้อมูล Code =" + selrq.EmpCode + "ได้ เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                         ;
                     }
                 }
                 else
                 {
                     MessageBox.Show("ไม่สามารถบันทึกข้อมูล Code =" + selrq.EmpCode + "ได้ เนื่องจาก  " + msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                 }
             }





            if (dgItems.CurrentRow.Index < dgItems.Rows.Count - 1)
            {
                dgItems.CurrentCell = dgItems[0, dgItems.CurrentCell.RowIndex + 1];
                dgItems_SelectionChanged_1(otRequest_Control1, new EventArgs());
            }
            else
            {
                // txtSCode.SelectAll();
                // txtSCode.Focus();
            }
        }
        private bool CheckOtExit(string code, DateTime odate, string otfrom, string otto, ref string msg)
        {
            ArrayList req = otsvr.GetOTRequest(code, odate, "", "%");

            if (req == null || req.Count == 0)
            {
                return false;

            }
            else
            {
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
                }
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
                dgItems_SelectionChanged_1(otRequest_Control1, e);
            }
            catch
            { }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
           
            gvData = new ArrayList();
            foreach (OtRequestInfo item in otsearch)
            { bool add = true;
                if (cboWType.SelectedItem.ToString() != "%" && item.EmpType != cboWType.SelectedItem.ToString())
                {
                    add = false;
                }

                if (cmbGrpot.Text != "%" && cmbGrpot.Text != item.Grpot)
                {
                    add = false;
                }
                if (cmbDvcd.SelectedValue.ToString() != "%")
                {
                    DivisionInfo dv = (DivisionInfo)cmbDvcd.SelectedItem;

                    if (dv.ShortName != item.Dvcd)
                    {

                        add = false;
                    }
                }
               

                if (item.CalRest == "Y" && !chkY.Checked)
                {

                    add = false;
                }
              
                else if (item.CalRest == "T" && !chkT.Checked)
                {
                    add = false;
                }
                else if (item.CalRest == "O" && !chkO.Checked)
                {
                    add = false;
                }
                else if (item.CalRest == "R" && !chkR.Checked)
                {
                    add = false;
                }

                if (add)
                {
                      gvData.Add(item);
                }

            

            }
                 FillDataGrid();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {

                FontFamily fm = new FontFamily("Microsoft Sans Serif");
                Font ft = new Font(fm, 8.0f);
                string header =" ";
              
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
                            sh += " ";
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

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            bool more = MyDataGridViewPrinter.DrawDataGridView(e.Graphics);
            if (more == true)
                e.HasMorePages = true;
        }
    }
}
