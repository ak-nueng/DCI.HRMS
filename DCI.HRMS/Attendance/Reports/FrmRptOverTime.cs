using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Service;
using DCI.HRMS.Model.Attendance;
using DCI.HRMS.Model.Personal;
using System.Collections;
using CrystalDecisions.Shared;
using System.Threading;
using DCI.HRMS.Model.Organize;
using DCI.HRMS.Service.SubContract;
using DCI.HRMS.Service.Trainee;

namespace DCI.HRMS.Attendance.Reports
{
    public partial class FrmRptOverTime : Form
    {
        public FrmRptOverTime()
        {
            InitializeComponent();
        }
        private DivisionService dvSvr = DivisionService.Instance();
        private TimeCardService emTmc = TimeCardService.Instance();
        private EmployeeLeaveService emLvSvr = EmployeeLeaveService.Instance();
        private OtService otsvr = OtService.Instance();
        private ShiftService shiftsrv = ShiftService.Instance();
        private EmployeeService empsrv = EmployeeService.Instance();

        private SubContractService subsrv = SubContractService.Instance();
        private SubContractShiftService subShSvr = SubContractShiftService.Instance();
        private SubContractTimeCardService subTmSvr = SubContractTimeCardService.Instance();
        private SubContractOtService subOtSvr = SubContractOtService.Instance();

        private TraineeService tnSvr = TraineeService.Instance();

        private OtRequestInfo otreq = new OtRequestInfo();
        private ArrayList otsearch = new ArrayList();
       private AttendanceDataSet dt = new AttendanceDataSet();

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (m_AsyncWorker.IsBusy)
            {
                btnGenerate.Enabled = false;
                toolStripStatusLabel1.Text = "Cancelling...";

                // Notify the worker thread that a cancel has been requested.

                // The cancel will not actually happen until the thread in the

                // DoWork checks the bwAsync.CancellationPending flag, for this

                // reason we set the label to "Cancelling...", because we haven't

                // actually cancelled yet.

                m_AsyncWorker.CancelAsync();
            }
            else
            {
                btnGenerate.Text = "Cancel";
                toolStripStatusLabel1.Text = "Running...";

                // Kickoff the worker thread to begin it's DoWork function.
                this.Cursor = Cursors.WaitCursor;
                dt = new AttendanceDataSet();
                m_AsyncWorker.RunWorkerAsync();
            }
        
        }
        private void GenerateReport()
        {
          
        }
        private decimal TextToHour(string time)
        {
            try
            {
                decimal res = 0.0M;

                string hr = time.Substring(0, 2);
                string mn = time.Substring(3, 2);

                res = decimal.Parse(hr) + decimal.Parse(mn) / 60M;
                return res;
            }
            catch 
            {

                return 0M;
            }
        }

        private void FrmRptOverTime_Load(object sender, EventArgs e)
        {

        }

        
     

        private void m_AsyncWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
        }

        private void m_AsyncWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Cursor = Cursors.Default;
            toolStripProgressBar1.Value = 0;
            btnGenerate.Text = "Generate";
            btnGenerate.Enabled = true;
            this.Activate();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }

            // Check to see if the background process was cancelled.
         
            if (e.Cancelled)
            {
                toolStripStatusLabel1.Text = "Cancelled...";
            }
            else
            {

                toolStripStatusLabel1.Text = "Completed...";
            }

            if (rbnCalculateResult.Checked)
            {

                Rpt_OverTimeCalculate rptOt = new Rpt_OverTimeCalculate();
                rptOt.SetDataSource(dt);
                ParameterDiscreteValue pdv = new ParameterDiscreteValue();
                ParameterDiscreteValue pdv1 = new ParameterDiscreteValue();
                ParameterField pf = new ParameterField();
                ParameterField pf1 = new ParameterField();
                ParameterFields pfs = new ParameterFields();

                pf.Name = "OtDate";
                pf1.Name = "OtDatet";
                pdv.Value = dtpSDate.Value.Date;
                pdv1.Value = dtpTDate.Value.Date;
                pf.CurrentValues.Add(pdv);
                pf1.CurrentValues.Add(pdv1);
                pfs.Add(pf);
                pfs.Add(pf1);
                crystalReportViewer1.ParameterFieldInfo = pfs;
                crystalReportViewer1.ReportSource = rptOt;
          

            }
            else if (rbnOtSumary.Checked)
            {
                Rpt_OverTimeSumary rptOt = new Rpt_OverTimeSumary();
                rptOt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rptOt;

            }

            else if (kryptonRadioButton1.Checked || rbnSubNoOt.Checked)
            {
                Rpt_NoOtTimeCard rpt = new Rpt_NoOtTimeCard();
                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;
            }
            else if (rbnOt36Hr.Checked || rbnSubOt36Hr.Checked)
            {
                ParameterDiscreteValue pdv = new ParameterDiscreteValue();
                ParameterDiscreteValue pdv1 = new ParameterDiscreteValue();
                ParameterField pf = new ParameterField();
                ParameterField pf1 = new ParameterField();
                ParameterFields pfs = new ParameterFields();

                pf.Name = "OtDate";
                pf1.Name = "OtDatet";
                pdv.Value = dtpSDate.Value.Date;
                pdv1.Value = dtpTDate.Value.Date;
                pf.CurrentValues.Add(pdv);
                pf1.CurrentValues.Add(pdv1);
                pfs.Add(pf);
                pfs.Add(pf1);
                crystalReportViewer1.ParameterFieldInfo = pfs;
                Rpt_OverTimeOver36 rptOt = new Rpt_OverTimeOver36();
                rptOt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rptOt;

            }
            else if (rbnTotalOtbyEmp.Checked)
            {
                ParameterDiscreteValue pdv = new ParameterDiscreteValue();
                ParameterDiscreteValue pdv1 = new ParameterDiscreteValue();
                ParameterField pf = new ParameterField();
                ParameterField pf1 = new ParameterField();
                ParameterFields pfs = new ParameterFields();

                pf.Name = "OtDate";
                pf1.Name = "OtDatet";
                pdv.Value = dtpSDate.Value.Date;
                pdv1.Value = dtpTDate.Value.Date;
                pf.CurrentValues.Add(pdv);
                pf1.CurrentValues.Add(pdv1);
                pfs.Add(pf);
                pfs.Add(pf1);
                crystalReportViewer1.ParameterFieldInfo = pfs;
                Rpt_OvertimeTotal rptOt = new Rpt_OvertimeTotal();
                rptOt.SetDataSource(e.Result);
                crystalReportViewer1.ReportSource = rptOt;
            }
            else if (rbnOtSignForm.Checked)
            {
                Rpt_OtSignForm rpt = new Rpt_OtSignForm();
                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;
            }
 
            if (kryptonCheckBox1.Checked && !e.Cancelled)
            {
                crystalReportViewer1.PrintReport();

            }


        }

        private void m_AsyncWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bwAsync = sender as BackgroundWorker;

            if (rbnCalculateResult.Checked)
            {
                toolStripStatusLabel1.Text = "Calculating. Please wait.";
                //this.Update();
                otsearch = otsvr.GetOTRequest("", dtpSDate.Value.Date, dtpTDate.Value.Date, txtRqCode.Text, "%", txtSFrom.Text, txtSTo.Text, "");
                DataSet empm = empsrv.FindAllEmp();
                DataTable empTb = empm.Tables["EMPM"];

                if (otsearch != null)
                {
                    //toolStripProgressBar1.Maximum = otsearch.Count;
                    int i = 0;

                    foreach (OtRequestInfo var in otsearch)
                    {
                        toolStripStatusLabel1.Text = "Calcutating: " + var.EmpCode;
                        DataRow dr = dt.OTCalResult.NewOTCalResultRow();
                        DataRow[] emdr = empTb.Select("Code = '" + var.EmpCode + "'");

                        DateTime lDate = var.OtDate.AddDays(-1);
                        DateTime llDate = var.OtDate.AddDays(-2);
                        DateTime lllDate = var.OtDate.AddDays(-3);
                        DateTime nDate = var.OtDate.AddDays(1);
                        string llsh = "X";
                        string lsh = "X";
                        string nsh = "X";
                        string csh = "X";
                        string scsh = "";
                        EmployeeShiftInfo sh = new EmployeeShiftInfo();

                        try
                        {
                            sh = shiftsrv.GetEmShift(var.EmpCode, var.OtDate.ToString("yyyyMM"));
                            csh = sh.DateShift(var.OtDate);
                            scsh = sh.ShiftO.Substring(var.OtDate.Day - 1, 1);
                        }
                        catch
                        { }
                        try
                        {
                            sh = shiftsrv.GetEmShift(var.EmpCode, lDate.ToString("yyyyMM"));
                            lsh = sh.DateShift(lDate);
                        }
                        catch
                        { }
                        try
                        {
                            sh = shiftsrv.GetEmShift(var.EmpCode, lllDate.ToString("yyyyMM"));
                            llsh = sh.DateShift(lllDate);
                        }
                        catch
                        { }
                        try
                        {
                            sh = shiftsrv.GetEmShift(var.EmpCode, llDate.ToString("yyyyMM"));
                            llsh += sh.DateShift(llDate);
                        }
                        catch
                        { }
                        try
                        {
                            sh = shiftsrv.GetEmShift(var.EmpCode, nDate.ToString("yyyyMM"));
                            nsh = sh.DateShift(nDate);
                        }
                        catch
                        { }


                        dr["Odate"] = var.OtDate.Date;
                        dr["RQ"] = var.ReqId;
                        dr["Code-Name"] = var.EmpCode + " " + emdr[0]["TNAME"].ToString();
                        dr["OtFrom"] = var.OtFrom;
                        dr["OtTo"] = var.OtTo;
                        dr["Ot1"] = var.Rate1;
                        dr["Ot15"] = var.Rate15;
                        dr["Ot2"] = var.Rate2;
                        dr["Ot3"] = var.Rate3;
                        dr["NOt1"] = var.N1;
                        dr["NOt15"] = var.N15;
                        dr["NOt2"] = var.N2;
                        dr["NOt3"] = var.N3;
                        dr["STS"] = var.CalRest;
                        dr["PPD"] = llsh;
                        dr["PD"] = lsh;
                        dr["CD"] = csh;
                        dr["ND"] = nsh;
                        dr["RES"] = var.TimeCard;
                        dr["SO"] = scsh;
                        dr["Type"] = emdr[0]["WTYPE"].ToString();

                        dt.OTCalResult.Rows.Add(dr);

                        i++;

                        // toolStripProgressBar1.Value++;
                        bwAsync.ReportProgress(Convert.ToInt32(i * (100.0 / otsearch.Count)));

                        if (bwAsync.CancellationPending)
                        {
                            // Pause for a bit to demonstrate that there is time between

                            // "Cancelling..." and "Cancel ed".

                            Thread.Sleep(1200);

                            // Set the e.Cancel flag so that the WorkerCompleted event

                            // knows that the process was cancelled.

                            e.Cancel = true;
                            return;
                        }




                    }
                }

            }
            else if (rbnOtSumary.Checked)
            {
                //  toolStripStatusLabel1.Text = "Calculating. Please Wait.";
                //this.Update();
                //  otsearch = otsvr.GetOTRequest("", dtpSDate.Value.Date, dtpTDate.Value.Date, txtRqCode.Text, "%", txtSFrom.Text, txtSTo.Text);
                DataSet dts = otsvr.GetOtSumaryDataSet(dtpSDate.Value.Date, dtpTDate.Value.Date);
                // AttendanceDataSet dt = new AttendanceDataSet();
                if (dts != null)
                {
                    DataTable dtb = dts.Tables["OTSumary"];
                    // toolStripProgressBar1.Maximum = dtb.Rows.Count;
                    int i = 0;

                    foreach (DataRow var in dtb.Rows)
                    {
                        //  toolStripStatusLabel1.Text = "Calcutating: ";//+ var[";
                        DataRow dr = dt.OTCalResult.NewOTCalResultRow();
                        // EmployeeInfo emp = empsrv.Find(var.EmpCode);
                        // EmployeeShiftInfo sh = new EmployeeShiftInfo();
                        //  string scsh = "";
                        //  try
                        //   {
                        //      sh = shiftsrv.GetEmShift(var.EmpCode, var.OtDate.ToString("yyyyMM"));
                        //       scsh = sh.ShiftO.Substring(var.OtDate.Day - 1, 1);
                        //   }
                        //   catch 
                        //   { }


                        dr["Odate"] = var["ODate"];
                        dr["DVCD"] = var["DVCD"];
                        dr["Position"] = var["Posit"];
                        //dr["Code-Name"] = var.EmpCode + " " + emp.NameInEng.Name;
                        // dr["OtFrom"] = var.OtFrom;
                        // dr["OtTo"] = var.OtTo;
                        // dr["Ot1"] = var.Rate1;
                        // dr["Ot15"] = var.Rate15;
                        // dr["Ot2"] = var.Rate2;
                        // dr["Ot3"] = var.Rate3;
                        dr["AOt1"] = var["Aot1"];
                        dr["AOt15"] = var["Aot15"];
                        dr["AOt2"] = var["Aot2"]; ;
                        dr["AOt3"] = var["Aot3"];
                        //  dr["STS"] = var.CalRest;
                        //dr["PPD"] = llsh;
                        //dr["PD"] = lsh;
                        dr["CD"] = var["SH"];
                        //dr["ND"] = nsh;
                        //  dr["RES"] = var.TimeCard;
                        dr["SO"] = var["SO"];
                        dr["Type"] = var["wtype"];

                        dt.OTCalResult.Rows.Add(dr);


                        i++;
                        //  toolStripProgressBar1.Value++;

                        bwAsync.ReportProgress(Convert.ToInt32(i * (100.0 / dtb.Rows.Count)));
                        if (bwAsync.CancellationPending)
                        {
                            // Pause for a bit to demonstrate that there is time between

                            // "Cancelling..." and "Cancel ed".

                            Thread.Sleep(1200);

                            // Set the e.Cancel flag so that the WorkerCompleted event

                            // knows that the process was cancelled.

                            e.Cancel = true;
                            return;
                        }
                        //this.Update();

                    }
                }


            }
            else if (kryptonRadioButton1.Checked)
            {
                ArrayList empLs = empsrv.GetCurrentEmployees();
                dt.NoOtTimeCard.Rows.Clear();
                int i = 0;
                DateTime rdate = dtpSDate.Value.Date;
                while (rdate <= dtpTDate.Value.Date)
                {
                    foreach (EmployeeInfo var in empLs)
                    {

                        if (var.EmployeeType != "M")
                        {

                            try
                            {

                                string shift = shiftsrv.GetEmShift(var.Code, rdate.ToString("yyyyMM")).DateShift(rdate);

                                ArrayList tmc = emTmc.GetTimeCardCodeDate(var.Code, rdate);
                                if (shift == "D")
                                {

                                    if (tmc != null)
                                    {
                                        bool found = false;
                                        string timeCard = "{ ";
                                        foreach (TimeCardInfo tcvar in tmc)
                                        {
                                            timeCard += tcvar.CardTime + " ";
                                            if (tcvar.CardTime.CompareTo("19:00") >= 0)
                                            {

                                                found = true;

                                            }
                                        }
                                        timeCard += "}{";
                                        tmc = emTmc.GetTimeCardCodeDate(var.Code, rdate.AddDays(1));
                                        if (tmc != null)
                                        {

                                            foreach (TimeCardInfo tcvar in tmc)
                                            {
                                                timeCard += tcvar.CardTime + " ";

                                            }
                                        }
                                        timeCard += "}";

                                        if (found)
                                        {
                                            ArrayList otSl = otsvr.GetOTRequest(var.Code, rdate, "", "%");
                                            if (otSl == null || otSl.Count == 0)
                                            {
                                                AttendanceDataSet.NoOtTimeCardRow dr = dt.NoOtTimeCard.NewNoOtTimeCardRow();
                                                dr.Code = var.Code;
                                                dr.DVCD = var.Division.Code;
                                                dr.DVName = var.Division.Name;
                                                dr.TimeCard = timeCard;
                                                dr.Line = var.OtGroupLine;
                                                dr.Name = var.NameInEng.ToString();
                                                dr.OtDate = rdate;
                                                dr.Shift = shift;
                                                dt.NoOtTimeCard.Rows.Add(dr);

                                            }

                                        }
                                    }

                                }
                                else if (shift == "N")
                                {


                                    if (tmc != null)
                                    {
                                        bool found = false;
                                        string timeCard = "{ ";
                                        foreach (TimeCardInfo tcvar in tmc)
                                        {
                                            timeCard += tcvar.CardTime + " ";

                                        }
                                        timeCard += "}{";
                                        tmc = emTmc.GetTimeCardCodeDate(var.Code, rdate.AddDays(1));
                                        if (tmc != null)
                                        {

                                            foreach (TimeCardInfo tcvar in tmc)
                                            {
                                                timeCard += tcvar.CardTime + " ";
                                                if (tcvar.CardTime.CompareTo("07:00") >= 0 && tcvar.CardTime.CompareTo("12:00") <= 0)
                                                {

                                                    found = true;

                                                }

                                            }
                                        }
                                        timeCard += "}";
                                        if (found)
                                        {
                                            ArrayList otSl = otsvr.GetOTRequest(var.Code, rdate.AddDays(1), "", "%");
                                            if (otSl == null || otSl.Count == 0)
                                            {
                                                bool check = false;
                                                foreach (OtRequestInfo rqvar in otSl)
                                                {
                                                    if (rqvar.OtFrom == "06:05")
                                                    {
                                                        check = true;
                                                    }

                                                }
                                                if (!check)
                                                {
                                                    AttendanceDataSet.NoOtTimeCardRow dr = dt.NoOtTimeCard.NewNoOtTimeCardRow();
                                                    dr.Code = var.Code;
                                                    dr.DVCD = var.Division.Code;
                                                    dr.DVName = var.Division.Name;
                                                    dr.TimeCard = timeCard;
                                                    dr.Line = var.OtGroupLine;
                                                    dr.Name = var.NameInEng.ToString();
                                                    dr.OtDate = rdate;
                                                    dr.Shift = shift;
                                                    dt.NoOtTimeCard.Rows.Add(dr);
                                                }

                                            }

                                        }
                                    }

                                }
                                else
                                {

                                    if (tmc != null)
                                    {
                                        bool found = false;

                                        string timeCard = "{ ";
                                        foreach (TimeCardInfo tcvar in tmc)
                                        {
                                            timeCard += tcvar.CardTime + " ";

                                        }
                                        timeCard += "}{ ";
                                        ArrayList tmc1 = emTmc.GetTimeCardCodeDate(var.Code, rdate.AddDays(1));
                                        if (tmc1 != null)
                                        {

                                            foreach (TimeCardInfo tcvar in tmc1)
                                            {
                                                timeCard += tcvar.CardTime + " ";

                                            }
                                        }
                                        if (tmc.Count >= 2)
                                        {
                                            found = true;
                                        }
                                        else
                                        {
                                            if (tmc.Count == 1)
                                            {
                                                TimeCardInfo tmif = (TimeCardInfo)tmc[0];
                                                if (tmif.CardTime.CompareTo("12:00") > 0 && tmif.CardTime.CompareTo("20:00") <= 0)
                                                {
                                                    if (tmc != null)
                                                    {

                                                        foreach (TimeCardInfo tcvar in tmc1)
                                                        {
                                                            if (tcvar.CardTime.CompareTo("08:00") <= 0)
                                                            {
                                                                found = true;
                                                            }

                                                        }
                                                    }

                                                }
                                            }

                                        }

                                        timeCard += "}";
                                        if (found)
                                        {
                                            bool check = false;
                                            ArrayList otSl = otsvr.GetOTRequest(var.Code, rdate, "", "%");
                                            if (otSl == null || otSl.Count == 0)
                                            {
                                            }
                                            else if (otSl.Count == 1)
                                            {
                                                OtRequestInfo rqvar = (OtRequestInfo)otSl[0];
                                                if (rqvar.OtFrom != "06:05")
                                                {
                                                    check = true;
                                                }

                                            }
                                            else
                                            {
                                                check = true;
                                            }

                                            if (!check)
                                            {
                                                AttendanceDataSet.NoOtTimeCardRow dr = dt.NoOtTimeCard.NewNoOtTimeCardRow();
                                                dr.Code = var.Code;
                                                dr.DVCD = var.Division.Code;
                                                dr.DVName = var.Division.Name;
                                                dr.TimeCard = timeCard;
                                                dr.Line = var.OtGroupLine;
                                                dr.Name = var.NameInEng.ToString();
                                                dr.OtDate = rdate;
                                                dr.Shift = shift;
                                                dt.NoOtTimeCard.Rows.Add(dr);
                                            }



                                        }
                                    }
                                }

                            }
                            catch
                            {

                            }



                        }
                        i++;



                        //  toolStripProgressBar1.Value++;

                        bwAsync.ReportProgress(Convert.ToInt32(i * (100.0 / (empLs.Count * (((TimeSpan)(dtpTDate.Value - dtpSDate.Value)).Days + 1)))));
                        if (bwAsync.CancellationPending)
                        {
                            // Pause for a bit to demonstrate that there is time between

                            // "Cancelling..." and "Cancel ed".

                            Thread.Sleep(1200);

                            // Set the e.Cancel flag so that the WorkerCompleted event

                            // knows that the process was cancelled.

                            e.Cancel = true;
                            return;
                        }
                    }
                    rdate = rdate.AddDays(1);
                }

            }

            else if (rbnOt36Hr.Checked)
            {



                //  toolStripStatusLabel1.Text = "Calculating. Please Wait.";
                //this.Update();
                //  otsearch = otsvr.GetOTRequest("", dtpSDate.Value.Date, dtpTDate.Value.Date, txtRqCode.Text, "%", txtSFrom.Text, txtSTo.Text);
                DataSet dts = otsvr.GetOtSumaryEMPDataSet(dtpSDate.Value.Date, dtpTDate.Value.Date);
                // AttendanceDataSet dt = new AttendanceDataSet();
                if (dts != null)
                {
                    DataTable dtb = dts.Tables["OTSumary"];
                    // toolStripProgressBar1.Maximum = dtb.Rows.Count;
                    int i = 0;

                    foreach (DataRow var in dtb.Rows)
                    {
                        //  toolStripStatusLabel1.Text = "Calcutating: ";//+ var[";
                        DataRow dr = dt.OTCalResult.NewOTCalResultRow();

                        // EmployeeShiftInfo sh = new EmployeeShiftInfo();
                        //  string scsh = "";
                        //  try
                        //   {
                        //      sh = shiftsrv.GetEmShift(var.EmpCode, var.OtDate.ToString("yyyyMM"));
                        //       scsh = sh.ShiftO.Substring(var.OtDate.Day - 1, 1);
                        //   }
                        //   catch 
                        //   { }


                        //  dr["Odate"] = var["ODate"];
                        dr["DVCD"] = var["DV_ename"];
                        dr["Position"] = var["Posi_cd"];

                        // dr["OtFrom"] = var.OtFrom;
                        // dr["OtTo"] = var.OtTo;
                        // dr["Ot1"] = var.Rate1;
                        // dr["Ot15"] = var.Rate15;
                        // dr["Ot2"] = var.Rate2;
                        // dr["Ot3"] = var.Rate3;
                        dr["AOt1"] = var["Aot1"];
                        dr["AOt15"] = var["Aot15"];
                        dr["AOt2"] = var["Aot2"]; ;
                        dr["AOt3"] = var["Aot3"];
                        //  dr["STS"] = var.CalRest;
                        //dr["PPD"] = llsh;
                        //dr["PD"] = lsh;
                        //  dr["CD"] = var["SH"];
                        //dr["ND"] = nsh;
                        //  dr["RES"] = var.TimeCard;
                        //dr["SO"] = var["SO"];
                        dr["Type"] = var["wtype"];
                        dr["ATOTAL"] = var["total"];
                        dr["GRPNAME"] = var["grpname"];

                        try
                        {
                            if (decimal.Parse(var["total"].ToString()) > 36m)
                            {
                                EmployeeInfo emp = empsrv.FindBasicInfo(var["code"].ToString());
                                dr["Code-Name"] = var["code"].ToString();
                                dr["NAME"] = emp.NameInThai.ToString();
                                dt.OTCalResult.Rows.Add(dr);
                            }
                        }
                        catch
                        { }



                        i++;
                        //  toolStripProgressBar1.Value++;

                        bwAsync.ReportProgress(Convert.ToInt32(i * (100.0 / dtb.Rows.Count)));
                        if (bwAsync.CancellationPending)
                        {
                            // Pause for a bit to demonstrate that there is time between

                            // "Cancelling..." and "Cancel ed".

                            Thread.Sleep(1200);

                            // Set the e.Cancel flag so that the WorkerCompleted event

                            // knows that the process was cancelled.

                            e.Cancel = true;
                            return;
                        }
                        //this.Update();

                    }
                }


            }
            else if (rbnTotalOtbyEmp.Checked)
            {
                DataSet otTlDs = otsvr.GetOtSumaryEMPDataSet(dtpSDate.Value.Date, dtpTDate.Value.Date);
                if (otTlDs != null)
                {

                    DataTable tbl = otTlDs.Tables["OTSumary"];
                    int i = 0;
                    foreach (DataRow item in tbl.Rows)
                    {


                        DataRow dr = dt.OTSummary.NewOTSummaryRow();

                        dr["CODE"] = item["CODE"].ToString();
                        dr["NAME"] = item["Name"].ToString();
                        dr["AOT1"] = Convert.ToDecimal(item["AOT1"]);
                        dr["AOT15"] = Convert.ToDecimal(item["AOT15"]);
                        dr["AOT2"] = Convert.ToDecimal(item["AOT2"]);
                        dr["AOT3"] = Convert.ToDecimal(item["AOT3"]);
                        dr["TOTAL"] = Convert.ToDecimal(item["TOTAL"]);
                        dr["DV_CD"] = item["DV_CD"].ToString();
                        dr["DV_ENAME"] = item["DV_ENAME"].ToString();
                        dr["GRPNAME"] = item["GRPNAME"].ToString();
                        dr["GRPOT"] = item["GRPOT"].ToString();
                        dr["POSI_CD"] = item["POSI_CD"].ToString();
                        dr["SECT"] = item["SECT"].ToString();
                        dr["WTYPE"] = item["WTYPE"].ToString();


                        i++;
                        //  toolStripProgressBar1.Value++;

                        bwAsync.ReportProgress(Convert.ToInt32(i * (100.0 / tbl.Rows.Count)));
                        if (bwAsync.CancellationPending)
                        {
                            // Pause for a bit to demonstrate that there is time between

                            // "Cancelling..." and "Cancel ed".

                            Thread.Sleep(1200);

                            // Set the e.Cancel flag so that the WorkerCompleted event

                            // knows that the process was cancelled.

                            e.Cancel = true;
                            return;
                        }
                    }
                    e.Result = tbl;
                }


            }
            else if (rbnOtSignForm.Checked)
            {
                ArrayList emps = empsrv.GetCurrentEmployees();

                ArrayList subs = subsrv.GetCurrentEmployees();
                if (subs!= null)
                {
                    emps.AddRange(subs);
                }

                ArrayList trs = tnSvr.GetCurrentEmployees();
                if (trs != null)
                {
                    emps.AddRange(trs);
                }

    


                if (emps != null)
                {

                    int i = 0;
                    foreach (EmployeeInfo item in emps)
                    {


                        DataRow dr = dt.EMP.NewEMPRow();

                        if (item.EmployeeType == "E")
                        {
                            dr["CODE"] = item.Code;
                            dr["NAME"] = item.NameInEng.Name;
                            dr["SURN"] = item.NameInEng.Surname;
                            dr["BUS"] = item.Bus + "-" + item.BusStop;
                            dr["DVCD"] = item.Division.Code;
                            dr["GRPOT"] = item.OtGroupLine;



                            DivisionInfo selDv = dvSvr.FindRootStructure(item.Division.Code);



                            dr["SECT"] = selDv.DivisionOwner.ShortName;
                            dr["GRP"] = item.Division.ShortName;

                            if (txtDVCD.Text == "" || txtDVCD.Text.Contains(item.Division.Code))
                            {

                                dt.EMP.Rows.Add(dr);
                            }
                            
                           
                        }

                        i++;
                        //  toolStripProgressBar1.Value++;

                        bwAsync.ReportProgress(Convert.ToInt32(i * (100.0 / emps.Count)));
                        if (bwAsync.CancellationPending)
                        {
                            // Pause for a bit to demonstrate that there is time between

                            // "Cancelling..." and "Cancel ed".

                            Thread.Sleep(1200);

                            // Set the e.Cancel flag so that the WorkerCompleted event

                            // knows that the process was cancelled.

                            e.Cancel = true;
                            return;
                        }
                    }

                }


            }



            else if (rbnSubNoOt.Checked)
            {
                ArrayList empLs = subsrv.GetCurrentEmployees();
                dt.NoOtTimeCard.Rows.Clear();
                int i = 0;
                DateTime rdate = dtpSDate.Value.Date;
                while (rdate <= dtpTDate.Value.Date)
                {
                    foreach (EmployeeInfo var in empLs)
                    {

                        if (var.EmployeeType != "M")
                        {

                            try
                            {

                                string shift =subShSvr.GetEmShift(var.Code, rdate.ToString("yyyyMM")).DateShift(rdate);

                                ArrayList tmc =subTmSvr.GetTimeCardCodeDate(var.Code, rdate);
                                if (shift == "D")
                                {

                                    if (tmc != null)
                                    {
                                        bool found = false;
                                        string timeCard = "{ ";
                                        foreach (TimeCardInfo tcvar in tmc)
                                        {
                                            timeCard += tcvar.CardTime + " ";
                                            if (tcvar.CardTime.CompareTo("19:00") >= 0)
                                            {

                                                found = true;

                                            }
                                        }
                                        timeCard += "}{";
                                        tmc = emTmc.GetTimeCardCodeDate(var.Code, rdate.AddDays(1));
                                        if (tmc != null)
                                        {

                                            foreach (TimeCardInfo tcvar in tmc)
                                            {
                                                timeCard += tcvar.CardTime + " ";

                                            }
                                        }
                                        timeCard += "}";

                                        if (found)
                                        {
                                            ArrayList otSl = subOtSvr.GetOTRequest(var.Code, rdate, "", "%");
                                            if (otSl == null || otSl.Count == 0)
                                            {
                                                AttendanceDataSet.NoOtTimeCardRow dr = dt.NoOtTimeCard.NewNoOtTimeCardRow();
                                                dr.Code = var.Code;
                                                dr.DVCD = var.Division.Code;
                                                dr.DVName = var.Division.Name;
                                                dr.TimeCard = timeCard;
                                                dr.Line = var.OtGroupLine;
                                                dr.Name = var.NameInEng.ToString();
                                                dr.OtDate = rdate;
                                                dr.Shift = shift;
                                                dt.NoOtTimeCard.Rows.Add(dr);

                                            }

                                        }
                                    }

                                }
                                else if (shift == "N")
                                {


                                    if (tmc != null)
                                    {
                                        bool found = false;
                                        string timeCard = "{ ";
                                        foreach (TimeCardInfo tcvar in tmc)
                                        {
                                            timeCard += tcvar.CardTime + " ";

                                        }
                                        timeCard += "}{";
                                        tmc =subTmSvr.GetTimeCardCodeDate(var.Code, rdate.AddDays(1));
                                        if (tmc != null)
                                        {

                                            foreach (TimeCardInfo tcvar in tmc)
                                            {
                                                timeCard += tcvar.CardTime + " ";
                                                if (tcvar.CardTime.CompareTo("07:00") >= 0 && tcvar.CardTime.CompareTo("12:00") <= 0)
                                                {

                                                    found = true;

                                                }

                                            }
                                        }
                                        timeCard += "}";
                                        if (found)
                                        {
                                            ArrayList otSl = subOtSvr.GetOTRequest(var.Code, rdate.AddDays(1), "", "%");
                                            if (otSl == null || otSl.Count == 0)
                                            {
                                                bool check = false;
                                                foreach (OtRequestInfo rqvar in otSl)
                                                {
                                                    if (rqvar.OtFrom == "06:05")
                                                    {
                                                        check = true;
                                                    }

                                                }
                                                if (!check)
                                                {
                                                    AttendanceDataSet.NoOtTimeCardRow dr = dt.NoOtTimeCard.NewNoOtTimeCardRow();
                                                    dr.Code = var.Code;
                                                    dr.DVCD = var.Division.Code;
                                                    dr.DVName = var.Division.Name;
                                                    dr.TimeCard = timeCard;
                                                    dr.Line = var.OtGroupLine;
                                                    dr.Name = var.NameInEng.ToString();
                                                    dr.OtDate = rdate;
                                                    dr.Shift = shift;
                                                    dt.NoOtTimeCard.Rows.Add(dr);
                                                }

                                            }

                                        }
                                    }

                                }
                                else
                                {

                                    if (tmc != null)
                                    {
                                        bool found = false;

                                        string timeCard = "{ ";
                                        foreach (TimeCardInfo tcvar in tmc)
                                        {
                                            timeCard += tcvar.CardTime + " ";

                                        }
                                        timeCard += "}{ ";
                                        ArrayList tmc1 = subTmSvr.GetTimeCardCodeDate(var.Code, rdate.AddDays(1));
                                        if (tmc1 != null)
                                        {

                                            foreach (TimeCardInfo tcvar in tmc1)
                                            {
                                                timeCard += tcvar.CardTime + " ";

                                            }
                                        }
                                        if (tmc.Count >= 2)
                                        {
                                            found = true;
                                        }
                                        else
                                        {
                                            if (tmc.Count == 1)
                                            {
                                                TimeCardInfo tmif = (TimeCardInfo)tmc[0];
                                                if (tmif.CardTime.CompareTo("12:00") > 0 && tmif.CardTime.CompareTo("20:00") <= 0)
                                                {
                                                    if (tmc != null)
                                                    {

                                                        foreach (TimeCardInfo tcvar in tmc1)
                                                        {
                                                            if (tcvar.CardTime.CompareTo("08:00") <= 0)
                                                            {
                                                                found = true;
                                                            }

                                                        }
                                                    }

                                                }
                                            }

                                        }

                                        timeCard += "}";
                                        if (found)
                                        {
                                            bool check = false;
                                            ArrayList otSl =subOtSvr.GetOTRequest(var.Code, rdate, "", "%");
                                            if (otSl == null || otSl.Count == 0)
                                            {
                                            }
                                            else if (otSl.Count == 1)
                                            {
                                                OtRequestInfo rqvar = (OtRequestInfo)otSl[0];
                                                if (rqvar.OtFrom != "06:05")
                                                {
                                                    check = true;
                                                }

                                            }
                                            else
                                            {
                                                check = true;
                                            }

                                            if (!check)
                                            {
                                                AttendanceDataSet.NoOtTimeCardRow dr = dt.NoOtTimeCard.NewNoOtTimeCardRow();
                                                dr.Code = var.Code;
                                                dr.DVCD = var.Division.Code;
                                                dr.DVName = var.Division.Name;
                                                dr.TimeCard = timeCard;
                                                dr.Line = var.OtGroupLine;
                                                dr.Name = var.NameInEng.ToString();
                                                dr.OtDate = rdate;
                                                dr.Shift = shift;
                                                dt.NoOtTimeCard.Rows.Add(dr);
                                            }



                                        }
                                    }
                                }

                            }
                            catch
                            {

                            }



                        }
                        i++;



                        //  toolStripProgressBar1.Value++;

                        bwAsync.ReportProgress(Convert.ToInt32(i * (100.0 / (empLs.Count * (((TimeSpan)(dtpTDate.Value - dtpSDate.Value)).Days + 1)))));
                        if (bwAsync.CancellationPending)
                        {
                            // Pause for a bit to demonstrate that there is time between

                            // "Cancelling..." and "Cancel ed".

                            Thread.Sleep(1200);

                            // Set the e.Cancel flag so that the WorkerCompleted event

                            // knows that the process was cancelled.

                            e.Cancel = true;
                            return;
                        }
                    }
                    rdate = rdate.AddDays(1);
                }

            }

            else if (rbnSubOt36Hr.Checked)
            {



                //  toolStripStatusLabel1.Text = "Calculating. Please Wait.";
                //this.Update();
                //  otsearch = otsvr.GetOTRequest("", dtpSDate.Value.Date, dtpTDate.Value.Date, txtRqCode.Text, "%", txtSFrom.Text, txtSTo.Text);
                DataSet dts =subOtSvr.GetOtSumaryEMPDataSet(dtpSDate.Value.Date, dtpTDate.Value.Date);
                // AttendanceDataSet dt = new AttendanceDataSet();
                if (dts != null)
                {
                    DataTable dtb = dts.Tables["OTSumary"];
                    // toolStripProgressBar1.Maximum = dtb.Rows.Count;
                    int i = 0;

                    foreach (DataRow var in dtb.Rows)
                    {
                        //  toolStripStatusLabel1.Text = "Calcutating: ";//+ var[";
                        DataRow dr = dt.OTCalResult.NewOTCalResultRow();

                        // EmployeeShiftInfo sh = new EmployeeShiftInfo();
                        //  string scsh = "";
                        //  try
                        //   {
                        //      sh = shiftsrv.GetEmShift(var.EmpCode, var.OtDate.ToString("yyyyMM"));
                        //       scsh = sh.ShiftO.Substring(var.OtDate.Day - 1, 1);
                        //   }
                        //   catch 
                        //   { }


                        //  dr["Odate"] = var["ODate"];
                        dr["DVCD"] = var["DV_ename"];
                        dr["Position"] = var["Posi_cd"];

                        // dr["OtFrom"] = var.OtFrom;
                        // dr["OtTo"] = var.OtTo;
                        // dr["Ot1"] = var.Rate1;
                        // dr["Ot15"] = var.Rate15;
                        // dr["Ot2"] = var.Rate2;
                        // dr["Ot3"] = var.Rate3;
                        dr["AOt1"] = var["Aot1"];
                        dr["AOt15"] = var["Aot15"];
                        dr["AOt2"] = var["Aot2"]; ;
                        dr["AOt3"] = var["Aot3"];
                        //  dr["STS"] = var.CalRest;
                        //dr["PPD"] = llsh;
                        //dr["PD"] = lsh;
                        //  dr["CD"] = var["SH"];
                        //dr["ND"] = nsh;
                        //  dr["RES"] = var.TimeCard;
                        //dr["SO"] = var["SO"];
                        dr["Type"] = var["wtype"];
                        dr["ATOTAL"] = var["total"];
                        dr["GRPNAME"] = var["grpname"];

                        try
                        {
                            if (decimal.Parse(var["total"].ToString()) > 36m)
                            {
                                EmployeeInfo emp = subsrv.FindBasicInfo(var["code"].ToString());
                                dr["Code-Name"] = var["code"].ToString();
                                dr["NAME"] = emp.NameInThai.ToString();
                                dt.OTCalResult.Rows.Add(dr);
                            }
                        }
                        catch
                        { }



                        i++;
                        //  toolStripProgressBar1.Value++;

                        bwAsync.ReportProgress(Convert.ToInt32(i * (100.0 / dtb.Rows.Count)));
                        if (bwAsync.CancellationPending)
                        {
                            // Pause for a bit to demonstrate that there is time between

                            // "Cancelling..." and "Cancel ed".

                            Thread.Sleep(1200);

                            // Set the e.Cancel flag so that the WorkerCompleted event

                            // knows that the process was cancelled.

                            e.Cancel = true;
                            return;
                        }
                        //this.Update();

                    }
                }


            }





            // this.Cursor = Cursors.Default;


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
        }
    }
}