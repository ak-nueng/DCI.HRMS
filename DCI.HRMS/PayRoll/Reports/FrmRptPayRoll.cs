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
using DCI.HRMS.Base;
using DCI.HRMS.Util;
using System.Data.SqlClient;
using DCI.HRMS.Common;
using System.Globalization;
using Oracle.ManagedDataAccess.Client;

namespace DCI.HRMS.PayRoll.Reports
{
    public partial class FrmRptPayRoll : Form, IFormPermission
    {
        public FrmRptPayRoll()
        {
            InitializeComponent();
        }
        ApplicationManager appMgr = ApplicationManager.Instance();
        private TimeCardService emTmc = TimeCardService.Instance();
        private EmployeeLeaveService emLvSvr = EmployeeLeaveService.Instance();
        private PenaltyService penSvr = PenaltyService.Instanse();
        private DivisionService devSvr = DivisionService.Instance();
        private OtService otsvr = OtService.Instance();
        private ShiftService shiftsrv = ShiftService.Instance();
        private EmployeeService empsrv = EmployeeService.Instance();
        private OtRequestInfo otreq = new OtRequestInfo();
        private ArrayList otsearch = new ArrayList();
       private PayRollDataSet dt = new  PayRollDataSet();
       private StatusManager stsMng = new StatusManager();
       private string calCode = "";

       


        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (m_AsyncWorker.IsBusy)
            {
                btnGenerate.Enabled = false;
            
                stsMng.Status = "Cancelling...";

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
            
                stsMng.Status = "Running...";
                // Kickoff the worker thread to begin it's DoWork function.
                this.Cursor = Cursors.WaitCursor;
                dt = new PayRollDataSet();
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
        private void FrmRptPayRoll_Load(object sender, EventArgs e)
        {
            int xx = appMgr.UserAccount.UserGroup.ID;

            pnDocumentHeader.Visible = true;
            pnDocumentDetail.Visible = true;



            DateTime caldate = DateTime.Today;
           // dtpSDate.Value = DateTime.Parse("01/02/2010");
          //  dtpTDate.Value = DateTime.Parse("28/02/2010");

            DataTable dtDocType = new DataTable();
            dtDocType.Columns.Add("dataValue", typeof(string));
            dtDocType.Columns.Add("dataDisplay", typeof(string));
            dtDocType.Rows.Add("ALL", "=== ทั้งหมด ===");
            dtDocType.Rows.Add("Slip", "สลิปเงินเดือน");
            dtDocType.Rows.Add("GSB", "ใบรับรอง ธอส.");
            dtDocType.Rows.Add("Salary", "หนังสือรับรองเงินเดือน");

            cbEDocType.DataSource = dtDocType;
            cbEDocType.DisplayMember = "dataDisplay";
            cbEDocType.ValueMember = "dataValue";
            cbEDocType.SelectedValue = "ALL";


            DataTable dtDocStatus = new DataTable();
            dtDocStatus.Columns.Add("dataValue", typeof(string));
            dtDocStatus.Columns.Add("dataDisplay", typeof(string));
            dtDocStatus.Rows.Add("ALL", "=== สถานะ ทั้งหมด ===");
            dtDocStatus.Rows.Add("REQUEST", "ร้องขอ");
            dtDocStatus.Rows.Add("APPROVE", "รอสร้างเอกสาร");
            dtDocStatus.Rows.Add("ISSUED", "เอกสารสมบูรณ์");
            cbEDocStatus.DataSource = dtDocStatus;
            cbEDocStatus.DisplayMember = "dataDisplay";
            cbEDocStatus.ValueMember = "dataValue";
            cbEDocStatus.SelectedValue = "REQUEST";


            //dtpSDate.Value= DateTime.Parse("16/" + caldate.AddMonths(-2).Month.ToString() + "/" + caldate.AddMonths(-2).Year.ToString());
            //dtpTDate.Value = DateTime.Parse("15/" + caldate.AddMonths(2).Month.ToString() + "/" + caldate.AddMonths(2).Year.ToString());
            dtpSDate.Value = new DateTime(caldate.AddMonths(-2).Year, caldate.AddMonths(-2).Month, 16);
            dtpTDate.Value = new DateTime(caldate.AddMonths(2).Year, caldate.AddMonths(2).Month, 15);
        }

        
     

        private void m_AsyncWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            stsMng.Status = "Calcutating: " + calCode;
            stsMng.Progress=e.ProgressPercentage;
        }

        private void m_AsyncWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Cursor = Cursors.Default;
       
            stsMng.Progress = 0;
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
           
                stsMng.Status = "Cancelled...";
            }
            else
            {

           
                stsMng.Status = "Completed...";
            }

            if (rbnCalculateResult.Checked)
            {
                Rpt_ConfermSlip rptCnSl = new Rpt_ConfermSlip();
                //Copy_of_Rpt_ConfermSlip rptCnSl = new Copy_of_Rpt_ConfermSlip();
                rptCnSl.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rptCnSl;
                ParameterDiscreteValue pdv = new ParameterDiscreteValue();
                ParameterDiscreteValue pdv1 = new ParameterDiscreteValue();
                ParameterField pf = new ParameterField();
                ParameterField pf1 = new ParameterField();
                ParameterFields pfs = new ParameterFields();

                pf.Name = "sDate";
                pf1.Name = "tDate";
                pdv.Value = dtpSDate.Value.Date;
                pdv1.Value = dtpTDate.Value.Date;
                pf.CurrentValues.Add(pdv);
                pf1.CurrentValues.Add(pdv1);
                pfs.Add(pf);
                pfs.Add(pf1);
                crystalReportViewer1.ParameterFieldInfo = pfs;
            /*

            }
            else if (rbnOtSumary.Checked)
            {
                Rpt_OverTimeSumary rptOt = new Rpt_OverTimeSumary();
                rptOt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rptOt;

            }
            else if (kryptonRadioButton1.Checked)
            {
                Rpt_NoOtTimeCard rpt = new Rpt_NoOtTimeCard();
                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;
            }
*/

            }
            stsMng.Progress=0;
        }

        private void m_AsyncWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bwAsync = sender as BackgroundWorker;

            if (rbnCalculateResult.Checked)
            {
            
                DateTime sDate = dtpSDate.Value.Date;
                DateTime tDate = dtpTDate.Value.Date;

         
              //  stsMng.Status="Calculating. Please wait.";
               ArrayList curEmp = empsrv.GetCurrentEmployees();

            
                if (curEmp != null)
                {
                    //toolStripProgressBar1.Maximum = otsearch.Count;
                    int i = 0;

                    foreach (EmployeeInfo var in curEmp)
                    {

                        calCode = var.Code;
                        if (var.EmployeeType!="M")
                        {
                           
                            PayRollDataSet.ConfermSlipTableRow dr = dt.ConfermSlipTable.NewConfermSlipTableRow();


                            ArrayList allVeave = emLvSvr.GetLeaveTotal(var.Code, sDate, tDate,true);
                            ArrayList allPen = penSvr.GetPenalty(var.Code, sDate, tDate);
                            ArrayList allannu = emLvSvr.GetAnnualTotal(var.Code, tDate, false);

                            var.Division = devSvr.FindRootStructure(var.Division.Code);
                            dr.Code = var.Code;
                            dr.Name = var.NameInThai.ToString();
                            dr.Dep = var.Division.Code;
                            dr.Group=var.Division.ToString();
                            dr.OtGroup = var.OtGroupLine;
                            dr.Join = var.JoinDate;

                            if (allVeave!= null && allVeave.Count>0)
                            {   
                                foreach (LeaveTotal lvvar in allVeave)
                                {
                                    if (lvvar.Type == "ANNU" || lvvar.Type == "PERS")
                                    {
                                        dr[lvvar.Type] = lvvar.Total;
                                    }
                                    else if (lvvar.Type == "LATE" || lvvar.Type=="EARL")
                                    {
                                        dr.LateTime = lvvar.Time;
                                        dr["LATE"] = lvvar.Total;
                                    }
                                    else
                                    {
                                        if (lvvar.Type == "AMAT" || lvvar.Type=="BMAT")
                                        {
                                            dr["MATE"] = lvvar.LvTotal / 525;
                                        }
                                        else if (lvvar.Type == "SICK" || lvvar.Type == "SPSL")
                                        {
                                            dr["SICK"] = lvvar.LvTotal / 525;
                                        }
                                        else
                                        {
                                            dr[lvvar.Type] = lvvar.LvTotal / 525;
                                        }
                                    }
                                }
                            }
                            if (allannu!=null && allannu.Count>0)
                            {
                                AnnualTotal anu = (AnnualTotal)allannu[allannu.Count - 1];
                                dr.AnnualRemain = anu.RemainHr;

                            }
                            if (allPen!= null && allPen.Count>0)
                            {
                                foreach (PenaltyInfo pvar in allPen)
                                {
                                    if (dr[pvar.PenaltyType].ToString() == "")
                                    {
                                        dr[pvar.PenaltyType] = 0;
                                    }
                                    dr[pvar.PenaltyType]= int.Parse(dr[pvar.PenaltyType].ToString())+1;
                                }
                            }
        


                            dt.ConfermSlipTable.Rows.Add(dr); 
                        }

                        i++;

                        // toolStripProgressBar1.Value++;
                        bwAsync.ReportProgress(Convert.ToInt32(i * (100.0 / curEmp.Count)));

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

        #region IFormPermission Members

        public DCI.Security.Model.PermissionInfo Permission
        {
            set {      Util.FormUtil.SetReportPermission(value,crystalReportViewer1) ; }
        }

        #endregion






        SqlConnectDB oSqlHRM = new SqlConnectDB("dbHRM");
        ClsOraConnectDB oOraDCI = new ClsOraConnectDB("DCI");

        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnConfirm.Visible = false;
            string strCondition = "";
            string strConditionStatus = "";
            if (cbEDocType.SelectedValue.ToString().ToUpper() == "ALL") {

            }
            else if (cbEDocType.SelectedValue.ToString().ToUpper() == "SLIP")
            {
                strCondition = " AND DocType  = 'Slip' ";
            }
            else if (cbEDocType.SelectedValue.ToString().ToUpper() == "SALARY")
            {
                strCondition = " AND DocType  = 'Salary' ";
            }
            else if (cbEDocType.SelectedValue.ToString().ToUpper() == "GSB")
            {
                strCondition = " AND DocType  = 'GSB' ";
            }


            if (cbEDocStatus.SelectedValue.ToString().ToUpper() == "ALL")
            {
                strConditionStatus = " AND DocStatus  IN ('REQUEST','APPROVE','ISSUED') ";
            }
            else if (cbEDocStatus.SelectedValue.ToString().ToUpper() == "REQUEST")
            {
                strConditionStatus = " AND DocStatus  IN ('REQUEST') ";
            }
            else if (cbEDocStatus.SelectedValue.ToString().ToUpper() == "APPROVE")
            {
                strConditionStatus = " AND DocStatus  IN ('APPROVE') ";
            }
            else if (cbEDocStatus.SelectedValue.ToString().ToUpper() == "ISSUED")
            {
                strConditionStatus = " AND DocStatus  IN ('ISSUED') ";
            }


            DataTable dtFilter = new DataTable();
            string strFilter = @"SELECT [DocNo],[DocType],D.Code, CONCAT(E.Name,'.',SUBSTRING(E.SURN,1,1)) ReqName ,[ReqDate],[ApproveBy],[ApproveDate],
                                    [IssueBy],[IssueDate],[ExpireDate],[DocFile],[DocStatus],[Passcode],[Remark]
                                FROM [dbHRM].[dbo].[HR_DOC_REQ] D
                                LEFT JOIN [dbHRM].[dbo].[Employee] E ON E.Code = D.Code 
                                WHERE ReqDate BETWEEN @STDATE AND @ENDATE  " + strCondition + strConditionStatus;
            SqlCommand cmdFilter = new SqlCommand();
            cmdFilter.CommandText = strFilter;
            cmdFilter.Parameters.Add(new SqlParameter("@STDATE", dateST.Value.ToString("yyyy-MM-dd")));
            cmdFilter.Parameters.Add(new SqlParameter("@ENDATE", dateEN.Value.ToString("yyyy-MM-dd")));
            dtFilter = oSqlHRM.Query(cmdFilter);


            if (dtFilter.Rows.Count > 0) {
                
                foreach(DataRow drFilter in dtFilter.Rows){
                    bool _sts = false;
                    if (drFilter["DocStatus"].ToString() == "REQUEST") { 
                        _sts = true;
                        btnConfirm.Visible = true;
                    }
                    dgvEDocList.Rows.Add(_sts, 
                            drFilter["DocNo"].ToString(), 
                            drFilter["DocType"].ToString(),
                            drFilter["ReqName"].ToString(),
                            drFilter["ReqDate"].ToString(),
                            drFilter["DocStatus"].ToString(),
                            drFilter["ApproveBy"].ToString(),
                            drFilter["ApproveDate"].ToString()
                    );
                } // end foreach
            }

        }



        private void btnGeneratePayrollSlip_Click(object sender, EventArgs e)
        {
            if (dpkrSlipYM.Value.ToString("yyyyMM") == dpkrSlipIssue.Value.ToString("yyyyMM"))
            {
                DataTable dtSlip = new DataTable();
                string strSlip = @"SELECT [DocNo], [DocType],[Code],[ReqDate],[ApproveBy],[ApproveDate],
                                    [IssueBy],[IssueDate],[ExpireDate],[DocFile],[DocStatus],[Passcode],[Remark]
                               FROM [dbHRM].[dbo].[HR_DOC_REQ]
                               WHERE DocType=@DocType AND YEAR(ReqDate)=@YEAR AND MONTH(ReqDate)=@MONTH ";
                SqlCommand cmdSlip = new SqlCommand();
                cmdSlip.CommandText = strSlip;
                cmdSlip.Parameters.Add(new SqlParameter("@DocType", "Slip"));
                cmdSlip.Parameters.Add(new SqlParameter("@YEAR", dpkrSlipYM.Value.Year.ToString()));
                cmdSlip.Parameters.Add(new SqlParameter("@MONTH", dpkrSlipYM.Value.Month.ToString()));
                dtSlip = oSqlHRM.Query(cmdSlip);

                if (dtSlip.Rows.Count == 0)
                {
                    DataTable dtPRDT = new DataTable();
                    //string strPRDT = @"SELECT * FROM PRDT WHERE PDATE='01/" + dpkrSlipYM.Value.ToString("MMM/yyyy") + "'  ";
                    string strPRDT = @"SELECT code FROM (
                                SELECT code FROM DCI.PRDT WHERE PDATE='01/" + dpkrSlipYM.Value.ToString("MMM/yyyy") + @"' and code not like '0%'
                                    UNION ALL
                                SELECT code FROM DEV_OFFICE.PRDT WHERE PDATE='01/" + dpkrSlipYM.Value.ToString("MMM/yyyy") + @"' and code not like '0%'
                            ) ";
                    //OracleCommand cmdPRDT = new OracleCommand();
                    //cmdPRDT.CommandText = strPRDT;
                    dtPRDT = oOraDCI.Query(strPRDT);

                    if (dtPRDT.Rows.Count > 0)
                    {
                        foreach (DataRow drPRDT in dtPRDT.Rows)
                        {
                            string docno = GenerateNumber("HRDOC04");
                            DateTime reqDate = new DateTime(dpkrSlipYM.Value.Year, dpkrSlipYM.Value.Month, 1);
                            DateTime apprDate = new DateTime(dpkrSlipYM.Value.Year, dpkrSlipYM.Value.Month, DateTime.DaysInMonth(dpkrSlipYM.Value.Year, dpkrSlipYM.Value.Month));
                            string strInstr = @"INSERT INTO HR_DOC_REQ (DocNo,DocType,Code,ReqDate,ApproveBy,ApproveDate,
                                    IssueBy,IssueDate,ExpireDate,DocFile,DocStatus,Passcode,Remark) VALUES (@DocNo,
                                    @DocType,@Code,@ReqDate,@ApproveBy,@ApproveDate,@IssueBy,@IssueDate,@ExpireDate,
                                    @DocFile,@DocStatus,@Passcode,@Remark) ";
                            SqlCommand cmdInstr = new SqlCommand();
                            cmdInstr.CommandText = strInstr;
                            cmdInstr.Parameters.Add(new SqlParameter("@DocNo", docno));
                            cmdInstr.Parameters.Add(new SqlParameter("@DocType", "Slip"));
                            cmdInstr.Parameters.Add(new SqlParameter("@Code", drPRDT["Code"].ToString()));
                            cmdInstr.Parameters.Add(new SqlParameter("@ReqDate", reqDate.ToString("yyyy-MM-dd HH:mm:ss")));
                            cmdInstr.Parameters.Add(new SqlParameter("@ApproveBy", appMgr.UserAccount.AccountId));
                            cmdInstr.Parameters.Add(new SqlParameter("@ApproveDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                            cmdInstr.Parameters.Add(new SqlParameter("@IssueBy", appMgr.UserAccount.AccountId));
                            cmdInstr.Parameters.Add(new SqlParameter("@IssueDate", dpkrSlipIssue.Value.ToString("yyyy-MM-dd HH:mm:ss")));
                            cmdInstr.Parameters.Add(new SqlParameter("@ExpireDate", DBNull.Value));
                            cmdInstr.Parameters.Add(new SqlParameter("@DocFile", ""));
                            cmdInstr.Parameters.Add(new SqlParameter("@DocStatus", "APPROVE"));
                            cmdInstr.Parameters.Add(new SqlParameter("@Passcode", ""));
                            cmdInstr.Parameters.Add(new SqlParameter("@Remark", dpkrSlipYM.Value.ToString("MMMM yyyy")));
                            oSqlHRM.ExecuteCommand(cmdInstr);

                        } // end foreach

                        
                        
                        //********** Msg Inform ***********
                        MessageBox.Show("สำเร็จ!, จัดเตรียมข้อมูล เพื่อทำสลิปเงินเดือน " + dpkrSlipYM.Value.ToString("MMMM yyyy") + " จำนวน " + dtPRDT.Rows.Count.ToString("N0") + " ข้อมูล");

                    } // end if

                    else
                    {
                        MessageBox.Show("ไม่พบข้อมูลการทำเงินเดือน ของเดื่อน " + dpkrSlipYM.Value.ToString("MMMM yyyy") + " ในระบบ");
                    }

                }
                else
                {
                    MessageBox.Show("มีข้อมูลการทำ Slip ของเดือน " + dpkrSlipYM.Value.ToString("MMMM yyyy") + " อยู่แล้วในระบบ");
                }
            }
            else
            {
                MessageBox.Show("ข้อมูลเดือนที่ทำ (" + dpkrSlipYM.Value.ToString("MMMM yyyy") + ") และ เดือนที่ Issue (" + dpkrSlipIssue.Value.ToString("MMMM yyyy") + ") ไม่ตรงกัน!!!");
            }


            

        }



        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (dgvEDocList.Rows.Count > 0) { 
                foreach(DataGridViewRow drDoc in dgvEDocList.Rows){
                    if (Convert.ToBoolean(drDoc.Cells["ColCheck"].Value))
                    {
                        string strReq = @"UPDATE HR_DOC_REQ SET DocStatus=@DocStatus,ApproveDate=@ApproveDate, ApproveBy=@ApproveBy WHERE DocNo=@DocNo AND Code=@Code ";
                        SqlCommand cmdReq = new SqlCommand();
                        cmdReq.CommandText = strReq;
                        cmdReq.Parameters.Add(new SqlParameter("@DocStatus", "APPROVE"));
                        cmdReq.Parameters.Add(new SqlParameter("@ApproveDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                        cmdReq.Parameters.Add(new SqlParameter("@ApproveBy", appMgr.UserAccount.AccountId));
                        cmdReq.Parameters.Add(new SqlParameter("@DocNo", drDoc.Cells["ColDocNo"].Value.ToString()));
                        cmdReq.Parameters.Add(new SqlParameter("@Code", drDoc.Cells["ColReqBy"].Value.ToString()));
                        oSqlHRM.ExecuteCommand(cmdReq);
                    }
                    
                } // end foreach

                //***** Re-Load *****
                btnSearch_Click(sender, e);


            } // end if
        }



        private string GenerateNumber(string _RunCode)
        {
            //CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            //CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CreateSpecificCulture("en-US");

            DataTable _dTable = new DataTable();
            DateTime _dt = DateTime.Now;
            DateTime _dbDate = new DateTime(1900,1,1);
            int _dbYear = 0;
            int _dbMonth = 0;
            int _dbDay = 0;
            int _dbNumber = 0;
            string _Prefix = "";
            int _NextNumber = 0;
            string _RunningNumber = "";
            string _FormatYear = "";
            string _FormatMonth = "";
            string _FormatDay = "";
            string _FormatNumber = "";
            string _RunningNumberReset = "";

            string StrYear = "";
            string StrMonth = "";
            string StrDay = "";
            string StrRunNbr = "";


            try
            {
                string strRunNbr = @"SELECT TOP 1 [FormatId],[DocKey],[DocPrefix]
                        ,[Remark],[YearNbrPrefix],[MonthNbrPrefix]
                        ,[DayNbrPrefix],[FormatNbr],[ResetOption],[NextID]
                        ,[ActiveDate],CURRENT_TIMESTAMP sysdate 
                    FROM DCRunNbr WHERE DocKey=@DocKey ";
                SqlCommand cmdRunNbr = new SqlCommand();
                cmdRunNbr.CommandText = strRunNbr;
                cmdRunNbr.Parameters.Add(new SqlParameter("DocKey", _RunCode));
                _dTable = oSqlHRM.Query(cmdRunNbr);
            }
            catch { }

            try
            {
                _dt = Convert.ToDateTime(_dTable.Rows[0]["sysdate"]);
            }
            catch
            {
                _dt = DateTime.Now;
            }

            try
            {
                _Prefix = _dTable.Rows[0]["DocPrefix"].ToString();
            }
            catch { }

            try
            {
                _dbDate = Convert.ToDateTime(_dTable.Rows[0]["ActiveDate"]);
            }
            catch
            {
                _dbDate = DateTime.MinValue;
            }

            try
            {
                _NextNumber = int.Parse(_dTable.Rows[0]["NextID"].ToString());
            }
            catch { }

            try
            {
                _dbYear = Convert.ToInt16(_dTable.Rows[0]["YearNbrPrefix"]);
            }
            catch { }

            try
            {
                _dbMonth = Convert.ToInt16(_dTable.Rows[0]["MonthNbrPrefix"]);
            }
            catch { }

            try
            {
                _dbDay = Convert.ToInt16(_dTable.Rows[0]["DayNbrPrefix"]);
            }
            catch { }

            try
            {
                _dbNumber = Convert.ToInt16(_dTable.Rows[0]["FormatNbr"]);
            }
            catch { }

            try
            {
                _RunningNumberReset = _dTable.Rows[0]["ResetOption"].ToString();
            }
            catch { }

            try
            {
                // ====== Reset Year =======
                if (_RunningNumberReset == "Y")
                {
                    // ====== not reset running number =======
                    if (_dbDate.ToString("yyyy") != _dt.ToString("yyyy"))
                    {
                        _NextNumber = 1;
                    }
                }
                // ====== Reset MOnth =======
                else if (_RunningNumberReset == "M")
                {
                    // ====== not reset running number =======
                    if (_dbDate.ToString("yyyyMM") != _dt.ToString("yyyyMM"))
                    {
                        _NextNumber = 1;
                    }
                }
                // ====== Reset Day =======
                else
                {
                    // ====== not reset running number =======
                    if (_dbDate.ToString("yyyyMMdd") != _dt.ToString("yyyyMMdd"))
                    {
                        _NextNumber = 1;
                    }
                }
            }
            catch { }


            try
            {
                if (_dbYear > 0)
                {
                    if (_dbYear == 2)
                    {
                        _FormatYear = GetFormatLength(_dbYear);
                        StrYear = _dt.Year.ToString(_FormatYear).Substring(2, 2);
                    }
                    else
                    {
                        _FormatYear = GetFormatLength(_dbYear);
                        StrYear = _dt.Year.ToString(_FormatYear);
                    }
                }


                if (_dbMonth > 0)
                {
                    if (_dbMonth == 1)
                    {
                        if (_dt.Month == 10)
                        {
                            StrMonth = "X";
                        }
                        else if (_dt.Month == 11)
                        {
                            StrMonth = "Y";
                        }
                        else if (_dt.Month == 12)
                        {
                            StrMonth = "Z";
                        }
                        else
                        {
                            StrMonth = _dt.Month.ToString();
                        }
                    }
                    else
                    {
                        _FormatMonth = GetFormatLength(_dbMonth);
                        StrMonth = _dt.Month.ToString(_FormatMonth);
                    }
                }


                if (_dbDay > 0)
                {
                    _FormatDay = GetFormatLength(_dbDay);
                    StrDay = _dt.Day.ToString(_FormatDay);
                }

                if (_dbNumber > 0)
                {
                    _FormatNumber = GetFormatLength(_dbNumber);
                    StrRunNbr = _NextNumber.ToString(_FormatNumber);
                }
                else
                {
                    StrRunNbr = _NextNumber.ToString();
                }

                // ---- Normal Running Number ----
                _RunningNumber = _Prefix + StrYear + StrMonth + StrDay + "/" + StrRunNbr;


            }
            catch { }

            try
            {
                // ----------- Update Running Number ------------
                int _dNextID = _NextNumber + 1;
                string strUpd = @"UPDATE DCRunNbr SET NextID=@NextID, ActiveDate=@ActiveDate WHERE DocKey=@DocKey ";
                SqlCommand cmdUpd = new SqlCommand();
                cmdUpd.CommandText = strUpd;
                cmdUpd.Parameters.Add(new SqlParameter("@NextID", _dNextID));
                cmdUpd.Parameters.Add(new SqlParameter("@ActiveDate", _dt));
                cmdUpd.Parameters.Add(new SqlParameter("@DocKey", _RunCode));
                oSqlHRM.ExecuteCommand(cmdUpd);


            }
            catch { }


            return _RunningNumber;
        }

        private string GetFormatLength(int _Format)
        {
            string _ReturnFormat = "";

            for (int a = 0; a < _Format; a++)
            {
                _ReturnFormat += "0";
            }

            return _ReturnFormat;
        }





    }
}