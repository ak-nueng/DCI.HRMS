using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS;
using DCI.HRMS.Service;
using DCI.HRMS.Common;
using DCI.HRMS.Model.Personal;
using DCI.Security.Model;
using DCI.Security.Service;
using System.Collections;
using DCI.HRMS.Util;
using DCI.HRMS.Model.Allowance;
using DCI.HRMS.Service.SubContract;
using DCI.HRMS.Service.Trainee;
using System.IO;
using Oracle.ManagedDataAccess.Client;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using OfficeOpenXml.Style.XmlAccess;
using OfficeOpenXml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using DCI.HRMS.Model.Attendance;
using static DCI.HRMS.Model.Attendance.AttendanceInfo;
using static System.Windows.Forms.AxHost;
using OfficeOpenXml.Drawing.Slicer.Style;

namespace DCI.HRMS.Personal.Reports
{
    public partial class Rpt_EmpMstr : Form
    {
        private ApplicationManager appMgr = ApplicationManager.Instance();
        private readonly UserAccountService userAccountService = UserAccountService.Instance();
        private PropertyBorrowService prbSvr = PropertyBorrowService.Instace();
        private SkillAllowanceService sklSvr = SkillAllowanceService.Instance();
        private EmployeeService empSvr = EmployeeService.Instance();
        private StatusManager stsMng = new StatusManager();

        private SubContractService subSvr = SubContractService.Instance();
        private SubContractSkillAllowanceService subSklSvr = SubContractSkillAllowanceService.Instance();





        private TraineeService tnSvr = TraineeService.Instance();

        public Rpt_EmpMstr()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            stsMng.Progress = 0;
            if (rbtEmpMstr.Checked)
            {
                /*Employee datta without salary data*/
                stsMng.Status = "Generating Employee data";
                DataSet emp = empSvr.GenEmpployee();



                Reports.Rpt_EmpDataGen empRpt = new Rpt_EmpDataGen();
                empRpt.SetDataSource(emp);
                saveFileDialog1.FileName = "EmployeeData" + DateTime.Today.ToString("yyyyMMdd") + ".xls";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fname = saveFileDialog1.FileName;
                    empRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, fname);
                    MessageBox.Show("Generate Complete", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else if (rbnSubcontract.Checked)
            {
                /*Employee datta without salary data*/
                stsMng.Status = "Generating Employee data";
                DataSet emp = subSvr.GenEmpployee();




                Reports.Rpt_EmpDataGen empRpt = new Rpt_EmpDataGen();
                empRpt.SetDataSource(emp);
                saveFileDialog1.FileName = "SubcontractData" + DateTime.Today.ToString("yyyyMMdd") + ".xls";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fname = saveFileDialog1.FileName;
                    empRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, fname);
                    MessageBox.Show("Generate Complete", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else if (rbnTraineeMaster.Checked)
            {
                /*Employee datta without salary data*/
                stsMng.Status = "Generating Employee data";
                DataSet emp = tnSvr.GenEmpployee();




                Reports.Rpt_EmpDataGen empRpt = new Rpt_EmpDataGen();
                empRpt.SetDataSource(emp);
                saveFileDialog1.FileName = "TraineeData" + DateTime.Today.ToString("yyyyMMdd") + ".xls";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fname = saveFileDialog1.FileName;
                    empRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, fname);
                    MessageBox.Show("Generate Complete", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else if (rbtEmpSalary.Checked)
            {
                /*Employee datta with salary data*/

                if (txtSalaryPwd.Text.Trim() != "")
                {

                    ApplicationManager appMgr = ApplicationManager.Instance();
                    try
                    {
                        UserAccountInfo slr = userAccountService.Authentication("SlrPerm", txtSalaryPwd.Text);



                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("รหัสผ่านไม่ถูกต้อง", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        this.Cursor = Cursors.Default;
                        txtSalaryPwd.Clear();
                        return;

                    }
                    stsMng.Status = "Generating Employee data";
                    txtSalaryPwd.Clear();

                    DataSet emp = empSvr.GenEmpployee();
                    Reports.Rpt_EmpGen empRpt = new Rpt_EmpGen();
                    empRpt.SetDataSource(emp);
                    saveFileDialog1.FileName = "EmployeeDetail" + DateTime.Today.ToString("yyyyMMdd") + ".xls";

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string fname = saveFileDialog1.FileName;
                        empRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, fname);
                        MessageBox.Show("Generate Complete", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                else
                {
                    MessageBox.Show("กรุณาป้อน Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    txtSalaryPwd.Focus();
                }
            }
            else if (rbtEmpResignPrpt.Checked)
            {

                ArrayList emp = empSvr.GenResignEmployee(dateTimePicker1.Value, dateTimePicker2.Value);

                DataTable tbRest = new Dts_Empm.EMPM_RESIGNDataTable();

                int ct = 0;
                if (emp != null)
                {
                    stsMng.MaxProgress = emp.Count;
                    foreach (EmployeeInfo item in emp)
                    {


                        ArrayList pbr = prbSvr.GetDataByCode(item.Code);
                        string returned = "";
                        string notReturn = "";
                        if (pbr != null)
                        {
                            foreach (PropertyBorrowInfo pbrItem in pbr)
                            {
                                if (pbrItem.ReturnStatus == ReturnSts.คืนแล้ว)
                                {
                                    returned += pbrItem.TypeName + ",";
                                }
                                else if (pbrItem.ReturnStatus == ReturnSts.ยังไม่คืน)
                                {
                                    notReturn += pbrItem.TypeName + ",";
                                }
                            }
                        }
                        tbRest.Rows.Add(item.Code, item.NameInEng.Title, item.NameInEng.Name, item.NameInEng.Surname
                            , item.NameInThai.Title, item.NameInThai.Name, item.NameInThai.Surname, item.ResignDate, item.ResignType
                            , item.ResignReason, item.Position.Code, item.WorkType, item.Position.NameEng,item.Division.Code, item.Division.Name, returned, notReturn);
                        stsMng.Progress++;

                    }
                }
                Reports.Rpt_EmpReturnPrbr empRpt = new Rpt_EmpReturnPrbr();
                empRpt.SetDataSource(tbRest);
                saveFileDialog1.FileName = "EmployeeResign" + DateTime.Today.ToString("yyyyMMdd") + ".xls";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fname = saveFileDialog1.FileName;
                    empRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, fname);
                    MessageBox.Show("Generate Complete", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                stsMng.Progress = 0;
            }
            else if (rbtEmpCert.Checked)
            {
                stsMng.Status = "Get Data";
                ArrayList certList = sklSvr.GetCertificateByCode("%");
                     stsMng.Status = "Generate Report";
                DataTable certTb = new Dts_Empm.VI_EMPM_CERTDataTable();
                if (certList!= null)
                {
                    stsMng.MaxProgress = certList.Count;
                }
                foreach (EmpCertInfo item in certList)
                {
                    certTb.Rows.Add(item.EmpCode, item.CerType, item.Level, item.Remark, item.CerName, item.CertDate, item.ExpireDate);
                    stsMng.Progress++;
                }
                Reports.Rpt_Certifacate certRpt = new Rpt_Certifacate();
                certRpt.SetDataSource(certTb);
                saveFileDialog1.FileName = "EmployeeCert.xls";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fname = saveFileDialog1.FileName;
                    certRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, fname);
                    MessageBox.Show("Generate Complete", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                stsMng.Progress = 0;
            }
            else if (rbtEmpSklAllow.Checked)
            {
                stsMng.Status = "Get data";
                ArrayList certList = sklSvr.GetSkillAllowancwByCode("%",DateTime.Parse("01/" + dtpSkillMonth.Value.ToString("MM/yyyy")));

                ArrayList subCerList = subSklSvr.GetSkillAllowancwByCode("%", DateTime.Parse("01/" + dtpSkillMonth.Value.ToString("MM/yyyy")));

                DataTable certTb = new Dts_Empm.VI_SKILL_ALLOWDataTable();
                stsMng.Status = "Generate Report";

                int cnt = 0;
                if (certList != null)
                {
                   cnt = certList.Count;
                }

                if (subCerList!= null)
                {
                    cnt += subCerList.Count;
                }

                stsMng.MaxProgress = cnt;

                if (certList != null)
                {
                    foreach (EmpSkillAllowanceInfo item in certList)
                    {
                        certTb.Rows.Add(item.EmpCode, item.Month, item.CertType, item.CertLevel, item.Remark, item.CertName, item.CertCost);
                        stsMng.Progress++;
                    }
                }
                if (subCerList != null)
                {
                    foreach (EmpSkillAllowanceInfo item in subCerList)
                    {
                        certTb.Rows.Add(item.EmpCode, item.Month, item.CertType, item.CertLevel, item.Remark, item.CertName, item.CertCost);
                        stsMng.Progress++;
                    }
                }
                Reports.Rpt_SkillAllow certRpt = new  Rpt_SkillAllow();
                certRpt.SetDataSource(certTb);
                saveFileDialog1.FileName = "EmployeeSkillAllow" + dtpSkillMonth.Value.ToString("_MMyyyy") + ".xls";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fname = saveFileDialog1.FileName;
                    certRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, fname);
                    MessageBox.Show("Generate Complete", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                stsMng.Progress = 0;
            }
            else
            {
                MessageBox.Show("Please select a report", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            stsMng.Status = "Ready";
            this.Cursor = Cursors.Default;
        }
        private void kryptonRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            txtSalaryPwd.Clear();
            txtSalaryPwd.Enabled = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
            dtpSkillMonth.Enabled = false;


            if (rbtEmpSalary.Checked)
            {
                txtSalaryPwd.Enabled = true;

            }


            else if (rbtEmpResignPrpt.Checked)
            {

                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
            }
            else if (rbtEmpSklAllow.Checked)
            {
                dtpSkillMonth.Enabled = true;
            }


        }

        private void Rpt_EmpMstr_Load(object sender, EventArgs e)
        {
            DateTime stDate = DateTime.Today;
            DateTime etDate = DateTime.Today;
            if (stDate.Day < 28)
            {
                //stDate = DateTime.Parse("16/" + stDate.AddMonths(-1).ToString("MM/yyyy"));
                //etDate = DateTime.Parse("15/" + etDate.ToString("MM/yyyy"));
                stDate = new DateTime(stDate.AddMonths(-1).Year, stDate.AddMonths(-1).Month, 16);
                etDate = new DateTime(etDate.Year, etDate.Month, 15);
            }
            else
            {
                //stDate = DateTime.Parse("16/" + stDate.ToString("MM/yyyy"));
                //etDate = DateTime.Parse("15/" + etDate.AddMonths(1).ToString("MM/yyyy"));
                stDate = new DateTime(stDate.Year, stDate.Month , 16);
                etDate = new DateTime(etDate.AddMonths(1).Year, etDate.AddMonths(1).Month, 15);
            }
            dateTimePicker1.Value = stDate;
            dateTimePicker2.Value = etDate;
            if (!(appMgr.UserAccount.UserGroup.ID == 9000 || appMgr.UserAccount.UserGroup.ID == 1000 || appMgr.UserAccount.UserGroup.ID == 1100))
            {
                rbtEmpSalary.Visible = false;
            }
        }

        private void txtSalaryPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
             
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            DateTime DateST = new DateTime();
            DateTime DateEN = new DateTime();

            DateST = dkrRptST.Value.Date;
            DateEN = dkrRptEN.Value.Date;

            string strCon = "DCI";
            if (rdRptDCI.Checked) { strCon = "DCI"; }
            else if (rdRptSUB.Checked) { strCon = "DCISUB"; }
            else if (rdRptTRN.Checked) { strCon = "DCITRN"; }
            ClsOraConnectDB oOra = new ClsOraConnectDB(strCon);

            if(!rdRptDCI.Checked && !rdRptSUB.Checked && !rdRptTRN.Checked)
            {
                MessageBox.Show("Please select one in list DCI, SubContract, Trainee", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return ;
            }

            DataTable dtEMP = new DataTable();
            string strEMP = @"SELECT V.CODE, V.PREN || ' ' || V.NAME  || ' ' || V.SURN EMP_NAME, V.JOIN, V.WSTS, V.WTYPE, V.POSI_CD, GRP, SECT, DEPT, GRPOT, GRPL, SHGRP, GB02 BUDGET 
                              FROM VI_EMP_MSTR V 
                              WHERE RESIGN IS NULL or RESIGN = '01/jan/1900' ";
            OracleCommand cmdEMP = new OracleCommand();
            cmdEMP.CommandText = strEMP;
            dtEMP = oOra.Query(cmdEMP);


            DataTable dtOT = new DataTable();
            string strOT = @"SELECT CODE, SUM(DCI.texttohour(O.OT1)) OT1, SUM(DCI.texttohour(O.OT15)) OT15, SUM(DCI.texttohour(O.OT2)) OT2, SUM(DCI.texttohour(O.OT3)) OT3   
                                FROM OTRQ O
                                WHERE ODATE BETWEEN '"+ DateST.ToString("dd/MMM/yyyy") + "' and '"+ DateEN.ToString("dd/MMM/yyyy") + @"'
                                GROUP BY CODE  ";
            OracleCommand cmdOT = new OracleCommand();
            cmdOT.CommandText = strOT;
            dtOT = oOra.Query(cmdOT);


            DataTable dtLV = new DataTable();
            string strLV = @"SELECT CODE, TYPE, round(SUM(L.TOTAL) / 525, 1) TOTALS
                                FROM LVRQ L
                                WHERE CDATE BETWEEN '"+ DateST.ToString("dd/MMM/yyyy") + "' and '"+ DateEN.ToString("dd/MMM/yyyy") + @"'
                                 and TYPE IN ('PERS','SICK','ABSE','LATE','EARL')
                                 GROUP BY CODE, TYPE  ";
            OracleCommand cmdLV = new OracleCommand();
            cmdLV.CommandText = strLV;
            dtLV = oOra.Query(cmdLV);


            DataTable dtPENA = new DataTable();
            string strPENA = @"SELECT P.CODE, SUM(P.W_TOTAL) W_TOTAL, SUM(P.P_TOTAL) P_TOTAL
                                FROM PENA P
                                WHERE PDATE BETWEEN '"+ DateST.ToString("dd/MMM/yyyy") + "' and '"+ DateEN.ToString("dd/MMM/yyyy") + @"'
                                GROUP BY CODE  ";
            OracleCommand cmdPENA = new OracleCommand();
            cmdPENA.CommandText = strPENA;
            dtPENA = oOra.Query(cmdPENA);


            DataTable dtData = new DataTable();
            dtData.Columns.Add("CODE", typeof(string));
            dtData.Columns.Add("EMP_NAME", typeof(string));
            dtData.Columns.Add("JOIN", typeof(string));
            dtData.Columns.Add("POSI_CD", typeof(string));
            dtData.Columns.Add("WSTS", typeof(string));
            dtData.Columns.Add("WTYPE", typeof(string));
            dtData.Columns.Add("GRP", typeof(string));
            dtData.Columns.Add("SECT", typeof(string));
            dtData.Columns.Add("DEPT", typeof(string));
            dtData.Columns.Add("GRPOT", typeof(string));
            dtData.Columns.Add("GRPL", typeof(string));
            dtData.Columns.Add("SHGRP", typeof(string));
            dtData.Columns.Add("BUDGET", typeof(string));
            dtData.Columns.Add("OT1", typeof(string));
            dtData.Columns.Add("OT15", typeof(string));
            dtData.Columns.Add("OT2", typeof(string));
            dtData.Columns.Add("OT3", typeof(string));
            dtData.Columns.Add("PERS", typeof(string));
            dtData.Columns.Add("SICK", typeof(string));
            dtData.Columns.Add("ABSE", typeof(string));
            dtData.Columns.Add("LATE", typeof(string));
            dtData.Columns.Add("EARL", typeof(string));
            dtData.Columns.Add("PENNALTY", typeof(string));
            dtData.Columns.Add("PER_SICK", typeof(string));
            dtData.Columns.Add("HR_NOTE", typeof(string));

            if(dtEMP.Rows.Count > 0)
            {
                foreach (DataRow drData in dtEMP.Rows)
                {
                    DateTime _Join = new DateTime();
                    try {
                        _Join = Convert.ToDateTime(drData["JOIN"].ToString());
                    } catch { }

                    DataRow newData = dtData.NewRow();
                    newData["CODE"] = drData["CODE"].ToString();
                    newData["EMP_NAME"] = drData["EMP_NAME"].ToString();
                    newData["JOIN"] = _Join.ToString("dd/MM/yyyy");
                    newData["POSI_CD"] = drData["POSI_CD"].ToString();
                    newData["WSTS"] = drData["WSTS"].ToString();
                    newData["WTYPE"] = drData["WTYPE"].ToString();
                    newData["GRP"] = drData["GRP"].ToString();
                    newData["SECT"] = drData["SECT"].ToString();
                    newData["DEPT"] = drData["DEPT"].ToString();
                    newData["GRPOT"] = drData["GRPOT"].ToString();
                    newData["GRPL"] = drData["GRPL"].ToString();
                    newData["SHGRP"] = drData["SHGRP"].ToString();
                    newData["BUDGET"] = drData["BUDGET"].ToString();

                    //**********************
                    //        OT 
                    //**********************
                    string _ot1 = "", _ot15 = "", _ot2 = "", _ot3 = "", _hr_note = "";
                    if (dtOT.Rows.Count > 0)
                    {
                        DataRow[] drOT = dtOT.Select(" CODE='" + drData["CODE"].ToString() + "' ");
                        if (drOT.Count() > 0)
                        {
                            _ot1 = drOT[0]["OT1"].ToString();
                            _ot15 = drOT[0]["OT15"].ToString();
                            _ot2 = drOT[0]["OT2"].ToString();
                            _ot3 = drOT[0]["OT3"].ToString();
                        }
                    }
                    newData["OT1"] = _ot1;
                    newData["OT15"] = _ot15;
                    newData["OT2"] = _ot2;
                    newData["OT3"] = _ot3;
                    //**********************
                    //        OT 
                    //**********************


                    //**********************
                    //        PENA 
                    //**********************
                    string _pena = "";
                    if(dtPENA.Rows.Count > 0)
                    {
                        DataRow[] drPENA = dtPENA.Select(" CODE='" + drData["CODE"].ToString() + "' ");
                        if (drPENA.Count() > 0)
                        {
                            _pena = drPENA[0]["P_TOTAL"].ToString();
                        }
                    }                    
                    newData["PENNALTY"] = _pena;
                    //**********************
                    //        PENA 
                    //**********************


                    //**********************
                    //        Leave 
                    //**********************
                    decimal _pers = 0, _sick = 0, _abse = 0, _late = 0, _earl = 0 ;
                    if (dtLV.Rows.Count > 0)
                    {
                        DataRow[] drLVPers = dtLV.Select(" CODE='" + drData["CODE"].ToString() + "' AND TYPE = 'PERS' ");
                        DataRow[] drLVSick = dtLV.Select(" CODE='" + drData["CODE"].ToString() + "' AND TYPE = 'SICK' ");
                        DataRow[] drLVAbse = dtLV.Select(" CODE='" + drData["CODE"].ToString() + "' AND TYPE = 'ABSE' ");
                        DataRow[] drLVLate = dtLV.Select(" CODE='" + drData["CODE"].ToString() + "' AND TYPE = 'LATE' ");
                        DataRow[] drLVEarl = dtLV.Select(" CODE='" + drData["CODE"].ToString() + "' AND TYPE = 'EARL' ");

                        if (drLVPers.Count() > 0) { _pers = Convert.ToDecimal(drLVPers[0]["TOTALS"].ToString()); }
                        if (drLVSick.Count() > 0) { _sick = Convert.ToDecimal(drLVSick[0]["TOTALS"].ToString()); }
                        if (drLVAbse.Count() > 0) { _abse = Convert.ToDecimal(drLVAbse[0]["TOTALS"].ToString()); }
                        if (drLVLate.Count() > 0) { _late = Convert.ToDecimal(drLVLate[0]["TOTALS"].ToString()); }
                        if (drLVEarl.Count() > 0) { _earl = Convert.ToDecimal(drLVEarl[0]["TOTALS"].ToString()); }
                    }
                    
                    if ((_pers + _sick) > 10) {
                        _hr_note = "E";
                    } else if ((_pers + _sick) >= 5 && (_pers + _sick) <= 10) {
                        _hr_note = "D";
                    }
                    else
                    {
                        _hr_note = "";
                    }
                    newData["PERS"] = (_pers > 0) ? _pers.ToString("N1") : "";
                    newData["SICK"] = (_sick > 0) ? _sick.ToString("N1") : "";
                    newData["ABSE"] = (_abse > 0) ? _abse.ToString("N1") : "";
                    newData["LATE"] = (_late > 0) ? _late.ToString("N1") : "";
                    newData["EARL"] = (_earl > 0) ? _earl.ToString("N1") : "";
                    newData["PER_SICK"] = (_pers + _sick) > 0 ? (_pers + _sick).ToString("N1") : "";
                    newData["HR_NOTE"] = _hr_note;
                    //**********************
                    //        Leave 
                    //**********************

                    
                    // ****  Add new Row ****
                    dtData.Rows.Add(newData);
                }
            }



            if (dtData.Rows.Count > 0)
            {
                SaveFileDialog saveFileDlg = new SaveFileDialog();
                saveFileDlg.FileName = strCon+"_Employee_"+DateTime.Now.ToString("yyyyMMdd");
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
                    CreateCSVFile(dtData, saveFileDlg.FileName);
                    MessageBox.Show("Success");
                }
            }
            else
            {
                MessageBox.Show("No data export");
            }
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

        private void btnExpAttPerc_Click(object sender, EventArgs e)
        {

            ClsOraConnectDB oOraDCI = new ClsOraConnectDB("DCI");
            ClsOraConnectDB oOraSUB = new ClsOraConnectDB("DCISUB");
            ClsOraConnectDB oOraTRN = new ClsOraConnectDB("DCITRN");

            DateTime DateST = new DateTime(dkrRptMonth.Value.Year, dkrRptMonth.Value.Month, 1);
            DateTime DateEN = DateST.AddMonths(1).AddDays(-1);

            DataTable dtEMP = new DataTable();
            string strEMP = @"SELECT CODE, EMP_NAME, JOIN_DT, RESIGN,  POSI_CD, DV_CD, GRP, SECT, DEPT, GRPOT, GRPL, SHGRP, BUDGET, COSTCENTER FROM (
                                SELECT V.CODE, V.PREN || ' ' || V.NAME  || ' ' || V.SURN EMP_NAME, V.JOIN JOIN_DT, V.RESIGN, V.POSI_CD, V.DV_CD, GRP, SECT, DEPT, GRPOT, GRPL, SHGRP, GB02 BUDGET, COSTCENTER 
                                FROM DCI.VI_EMP_MSTR V 
                                WHERE V.WSTS = 'E' AND (RESIGN IS NULL or RESIGN = '01/jan/1900' or RESIGN BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"' or RESIGN >= '" + DateEN.ToString("dd/MMM/yyyy") + @"'  ) 

                                UNION ALL

                                SELECT V.CODE, V.PREN || ' ' || V.NAME  || ' ' || V.SURN EMP_NAME, V.JOIN JOIN_DT, V.RESIGN, V.POSI_CD, V.DV_CD, GRP, SECT, DEPT, GRPOT, GRPL, SHGRP, GB02 BUDGET, COSTCENTER 
                                FROM DCITC.VI_EMP_MSTR V 
                                WHERE V.WSTS = 'E' AND (RESIGN IS NULL or RESIGN = '01/jan/1900' or RESIGN BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"' or RESIGN >= '" + DateEN.ToString("dd/MMM/yyyy") + @"'  )  

                                UNION ALL

                                SELECT V.CODE, V.PREN || ' ' || V.NAME  || ' ' || V.SURN EMP_NAME, V.JOIN JOIN_DT, V.RESIGN, V.POSI_CD, V.DV_CD, GRP, SECT, DEPT, GRPOT, GRPL, SHGRP, GB02 BUDGET, COSTCENTER 
                                FROM DEV_OFFICE.VI_EMP_MSTR V 
                                WHERE V.WSTS = 'E' AND (RESIGN IS NULL or RESIGN = '01/jan/1900' or RESIGN BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"' or RESIGN >= '" + DateEN.ToString("dd/MMM/yyyy") + @"'  )  
                            )   ";
            OracleCommand cmdEMP = new OracleCommand();
            cmdEMP.CommandText = strEMP;
            dtEMP = oOraDCI.Query(cmdEMP);



            Console.WriteLine("EMP : " + dtEMP.Rows.Count.ToString() + " -> OK " + DateTime.Now.ToString("HH:mm:ss") );


            DataTable dtOT = new DataTable();
            string strOT = @"SELECT CODE, OT1, OT15, OT2, OT3 FROM(
                                SELECT CODE, SUM(DCI.texttohour(O.OT1)) OT1, SUM(DCI.texttohour(O.OT15)) OT15, SUM(DCI.texttohour(O.OT2)) OT2, SUM(DCI.texttohour(O.OT3)) OT3   
                                FROM DCI.OTRQ O
                                WHERE ODATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"'
                                GROUP BY CODE  

                                UNION ALL

                                SELECT CODE, SUM(DCI.texttohour(O.OT1)) OT1, SUM(DCI.texttohour(O.OT15)) OT15, SUM(DCI.texttohour(O.OT2)) OT2, SUM(DCI.texttohour(O.OT3)) OT3   
                                FROM DCITC.OTRQ O
                                WHERE ODATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"'
                                GROUP BY CODE

                                UNION ALL

                                SELECT CODE, SUM(DCI.texttohour(O.OT1)) OT1, SUM(DCI.texttohour(O.OT15)) OT15, SUM(DCI.texttohour(O.OT2)) OT2, SUM(DCI.texttohour(O.OT3)) OT3   
                                FROM DEV_OFFICE.OTRQ O
                                WHERE ODATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"'
                                GROUP BY CODE
                            )  ";
            OracleCommand cmdOT = new OracleCommand();
            cmdOT.CommandText = strOT;
            dtOT = oOraDCI.Query(cmdOT);

            Console.WriteLine("OT : " + dtOT.Rows.Count.ToString() + " -> OK " + DateTime.Now.ToString("HH:mm:ss"));


            DataTable dtOTDet = new DataTable();
            string strOTDet = @"SELECT CODE, ODATE  FROM(
                                    SELECT CODE, TO_CHAR(ODATE,'YYYYMMDD') ODATE   
                                    FROM DCI.OTRQ O
                                    WHERE ODATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"'
                                    GROUP BY CODE, ODATE  

                                    UNION ALL

                                    SELECT CODE, TO_CHAR(ODATE,'YYYYMMDD') ODATE   
                                    FROM DCITC.OTRQ O
                                    WHERE ODATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"'
                                    GROUP BY CODE, ODATE

                                    UNION ALL

                                    SELECT CODE, TO_CHAR(ODATE,'YYYYMMDD') ODATE   
                                    FROM DEV_OFFICE.OTRQ O
                                    WHERE ODATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"'
                                    GROUP BY CODE, ODATE
                                )  ";
            OracleCommand cmdOTDet = new OracleCommand();
            cmdOTDet.CommandText = strOTDet;
            dtOTDet = oOraDCI.Query(cmdOTDet);

            Console.WriteLine("OT Det : " + dtOTDet.Rows.Count.ToString() + " -> OK " + DateTime.Now.ToString("HH:mm:ss"));

            /*
            DataTable dtLV = new DataTable();
            string strLV = @"SELECT CODE, TYPES, round(SUM(TOTAL) / 525, 1) TOTALS FROM(
                                SELECT CODE,L.TOTAL, 
                                   CASE TYPE WHEN 'PERS' THEN 'PERS' WHEN 'SICK' THEN 'SICK' WHEN 'ABSE' THEN 'ABSE' WHEN 'LATE' THEN 'LATE' WHEN 'EARL' THEN 'EARL' ELSE 'OTHER' END TYPES 
                                FROM DCI.LVRQ L
                                WHERE CDATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"'
    
                                UNION ALL
    
                                SELECT CODE,L.TOTAL, 
                                   CASE TYPE WHEN 'PERS' THEN 'PERS' WHEN 'SICK' THEN 'SICK' WHEN 'ABSE' THEN 'ABSE' WHEN 'LATE' THEN 'LATE' WHEN 'EARL' THEN 'EARL' ELSE 'OTHER' END TYPES 
                                FROM DCITC.LVRQ L
                                WHERE CDATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"'
    
                                UNION ALL
    
                                SELECT CODE,L.TOTAL, 
                                   CASE TYPE WHEN 'PERS' THEN 'PERS' WHEN 'SICK' THEN 'SICK' WHEN 'ABSE' THEN 'ABSE' WHEN 'LATE' THEN 'LATE' WHEN 'EARL' THEN 'EARL' ELSE 'OTHER' END TYPES 
                                FROM DEV_OFFICE.LVRQ L
                                WHERE CDATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"' 
                            )
                            GROUP BY CODE, TYPES  ";
            OracleCommand cmdLV = new OracleCommand();
            cmdLV.CommandText = strLV;
            dtLV = oOraDCI.Query(cmdLV);
            
            Console.WriteLine("LV : " + dtLV.Rows.Count.ToString() + " -> OK " + DateTime.Now.ToString("HH:mm:ss"));
            */


            DataTable dtLVDet = new DataTable();
            string strLVDet = @"SELECT CODE, CDATE, TYPES, ceil(SUM(TOTAL) / 525) TOTALS FROM (
                                    SELECT CODE, TO_CHAR(L.CDATE,'YYYYMMDD') CDATE, L.TOTAL, 
                                       CASE TYPE WHEN 'PERS' THEN 'PERS' WHEN 'SICK' THEN 'SICK' WHEN 'ABSE' THEN 'ABSE' WHEN 'ANNU' THEN 'ANNU' ELSE 'OTHER' END TYPES 
                                    FROM DCI.LVRQ L
                                    WHERE TYPE NOT IN ('LATE','EARL') AND CDATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"' 
    
                                    UNION ALL
    
                                    SELECT CODE, TO_CHAR(L.CDATE,'YYYYMMDD') CDATE, L.TOTAL, 
                                       CASE TYPE WHEN 'PERS' THEN 'PERS' WHEN 'SICK' THEN 'SICK' WHEN 'ABSE' THEN 'ABSE' WHEN 'ANNU' THEN 'ANNU' ELSE 'OTHER' END TYPES 
                                    FROM DCITC.LVRQ L
                                    WHERE TYPE NOT IN ('LATE','EARL') AND CDATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"' 
    
                                    UNION ALL
    
                                    SELECT CODE, TO_CHAR(L.CDATE,'YYYYMMDD') CDATE, L.TOTAL, 
                                       CASE TYPE WHEN 'PERS' THEN 'PERS' WHEN 'SICK' THEN 'SICK' WHEN 'ABSE' THEN 'ABSE' WHEN 'ANNU' THEN 'ANNU' ELSE 'OTHER' END TYPES 
                                    FROM DEV_OFFICE.LVRQ L
                                    WHERE TYPE NOT IN ('LATE','EARL') AND CDATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"'  
                                )
                                GROUP BY CODE, TYPES, CDATE ";
            OracleCommand cmdLVDet = new OracleCommand();
            cmdLVDet.CommandText = strLVDet;
            dtLVDet = oOraDCI.Query(cmdLVDet);

            Console.WriteLine("LV Det : " + dtLVDet.Rows.Count.ToString() + " -> OK " + DateTime.Now.ToString("HH:mm:ss"));



            DataTable dtEMCL = new DataTable();
            string strEMCL = @"SELECT YM, CODE, STSS FROM (
                                SELECT  E.YM, E.CODE, E.STSS 
                                FROM DCI.EMCL E
                                WHERE YM = '" + DateST.ToString("yyyyMM") + @"'

                                UNION ALL

                                SELECT  E.YM, E.CODE, E.STSS 
                                FROM DCITC.EMCL E
                                WHERE YM = '" + DateST.ToString("yyyyMM") + @"'

                                UNION ALL

                                SELECT  E.YM, E.CODE, E.STSS 
                                FROM DEV_OFFICE.EMCL E
                                WHERE YM = '" + DateST.ToString("yyyyMM") + @"'
                            ) ";
            OracleCommand cmdEMCL = new OracleCommand();
            cmdEMCL.CommandText = strEMCL;
            dtEMCL = oOraDCI.Query(cmdEMCL);


            Console.WriteLine("SHIFT : " + dtEMCL.Rows.Count.ToString() + " -> OK " + DateTime.Now.ToString("HH:mm:ss"));


            DataTable dtWTME = new DataTable();
            string strWTME = @"SELECT CODE, CDATE, TIME, WSTN, DUTY FROM (
                                    SELECT W.CODE, TO_CHAR(W.CDATE,'YYYYMMDD') CDATE, W.TIME, W.WSTN, W.DUTY
                                    FROM DCI.WTME W
                                    WHERE DUTY = 'I' AND CDATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' AND '" + DateEN.ToString("dd/MMM/yyyy") + @"' 

                                    UNION ALL

                                    SELECT W.CODE, TO_CHAR(W.CDATE,'YYYYMMDD') CDATE, W.TIME, W.WSTN, W.DUTY
                                    FROM DCITC.WTME W
                                    WHERE DUTY = 'I' AND CDATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' AND '" + DateEN.ToString("dd/MMM/yyyy") + @"' 

                                    UNION ALL

                                    SELECT W.CODE, TO_CHAR(W.CDATE,'YYYYMMDD') CDATE, W.TIME, W.WSTN, W.DUTY
                                    FROM DEV_OFFICE.WTME W
                                    WHERE DUTY = 'I' AND CDATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' AND '" + DateEN.ToString("dd/MMM/yyyy") + @"' 
                                )  ";
            OracleCommand cmdWTME = new OracleCommand();
            cmdWTME.CommandText = strWTME;
            dtWTME = oOraDCI.Query(cmdWTME);


            DataTable dtTNRQ = new DataTable();
            string strTNRQ = @"SELECT CODE, TO_CHAR(FDATE,'YYYYMMDD') FDATE, TO_CHAR(TDATE,'YYYYMMDD') TDATE FROM(
                                  SELECT T.CODE, T.FDATE, T.TDATE
                                  FROM DCI.TNRQ T
                                  WHERE (FDATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "'  AND '" + DateEN.ToString("dd/MMM/yyyy") + @"') OR
                                        (FDATE <= '" + DateST.ToString("dd/MMM/yyyy") + "' AND TDATE >= '" + DateST.ToString("dd/MMM/yyyy") + @"' )

                                  UNION ALL

                                  SELECT T.CODE, T.FDATE, T.TDATE
                                  FROM DCITC.TNRQ T
                                  WHERE FDATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' AND '" + DateEN.ToString("dd/MMM/yyyy") + @"' OR
                                        (FDATE <= '" + DateST.ToString("dd/MMM/yyyy") + "' AND TDATE >= '" + DateST.ToString("dd/MMM/yyyy") + @"' )

                                  UNION ALL

                                  SELECT T.CODE, T.FDATE, T.TDATE
                                  FROM DEV_OFFICE.TNRQ T
                                  WHERE FDATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' AND '" + DateEN.ToString("dd/MMM/yyyy") + @"' OR
                                        (FDATE <= '" + DateST.ToString("dd/MMM/yyyy") + "' AND TDATE >= '" + DateST.ToString("dd/MMM/yyyy") + @"' )

                                ) ";
            OracleCommand cmdTNRQ = new OracleCommand();
            cmdTNRQ.CommandText = strTNRQ;
            dtTNRQ = oOraDCI.Query(cmdTNRQ);



            Console.WriteLine("TAFF : " + dtWTME.Rows.Count.ToString() + " -> OK " + DateTime.Now.ToString("HH:mm:ss"));


            #region Init Data Table 
            DataTable dtData = new DataTable();
            dtData.Columns.Add("SECT", typeof(string));
            dtData.Columns.Add("DVCD", typeof(string));
            dtData.Columns.Add("COSTCENTER", typeof(string));
            dtData.Columns.Add("GRPOT", typeof(string));
            dtData.Columns.Add("GRPL", typeof(string));
            dtData.Columns.Add("SHGRP", typeof(string));
            dtData.Columns.Add("CODE", typeof(string));
            dtData.Columns.Add("FNAME", typeof(string));
            dtData.Columns.Add("POSIT", typeof(string));
            dtData.Columns.Add("BUDGET", typeof(string));
            for (int d = 1; d <= 31; d++)
            {
                dtData.Columns.Add("D" + d.ToString("00") + "WK", typeof(string));
                dtData.Columns.Add("D" + d.ToString("00") + "OT", typeof(string));
            }
            dtData.Columns.Add("REMARK", typeof(string));
            dtData.Columns.Add("ABSE", typeof(string));
            dtData.Columns.Add("ANNU", typeof(string));
            dtData.Columns.Add("PERS", typeof(string));
            dtData.Columns.Add("SICK", typeof(string));
            dtData.Columns.Add("OTHER", typeof(string));            
            dtData.Columns.Add("WORKDAY", typeof(string));
            dtData.Columns.Add("WORKACT", typeof(string));
            dtData.Columns.Add("WORKOT", typeof(string));
            dtData.Columns.Add("HOLIDAYOT", typeof(string));
            dtData.Columns.Add("WORKNO", typeof(string));
            dtData.Columns.Add("WORKNOPER", typeof(string));

            #endregion

            List<AttendanceInfo.mAttInfo> oAryAtt = new List<AttendanceInfo.mAttInfo>();
            List<AttendanceInfo.mAttSummaryInfo> oAryAttSum = new List<AttendanceInfo.mAttSummaryInfo>();



            if (dtEMP.Rows.Count > 0)
            {
                
                foreach (DataRow drEMP in dtEMP.Rows)
                {
                    DateTime _DtJoin = new DateTime(1900,1,1);
                    DateTime _DtResign = new DateTime(1900,1,1);
                    try { _DtJoin = Convert.ToDateTime(drEMP["JOIN_DT"].ToString()); } catch { }
                    try { _DtResign = Convert.ToDateTime(drEMP["RESIGN"].ToString()); } catch { }


                    DataRow newData = dtData.NewRow();
                    newData["SECT"] = drEMP["GRP"].ToString();
                    newData["DVCD"] = drEMP["DV_CD"].ToString();
                    newData["COSTCENTER"] = drEMP["COSTCENTER"].ToString();
                    newData["GRPOT"] = drEMP["GRPOT"].ToString();
                    newData["GRPL"] = drEMP["GRPL"].ToString();
                    newData["SHGRP"] = drEMP["SHGRP"].ToString();
                    newData["CODE"] = drEMP["CODE"].ToString();
                    newData["FNAME"] = drEMP["EMP_NAME"].ToString();
                    newData["POSIT"] = drEMP["POSI_CD"].ToString();
                    newData["BUDGET"] = drEMP["BUDGET"].ToString();

                    int _workDay = 0;
                    int _workAct = 0;
                    int _workOT = 0;
                    int _holidayOT = 0;
                    int _workNO = 0;
                    decimal _workNOPerc = 0;

                    int _abse = 0, _annu = 0, _pers = 0, _sick = 0, _other = 0;

                    #region Loop Day
                    DataRow[] drEMCL = dtEMCL.Select(" CODE='" + drEMP["CODE"].ToString() + "' ");
                    if (drEMCL.Count() > 0)
                    {
                        DateTime LoopDate = DateST;                        
                        while (LoopDate <= DateEN)
                        {
                            string sht = drEMCL[0]["STSS"].ToString().Substring(LoopDate.Day - 1, 1);

                            //*** Check Shift is Day Or Night Mon-Fri ***
                            if (sht == "D" || sht == "N")
                            {
                                // check working day time
                                DataRow[] drWTME = dtWTME.Select(" CODE='" + drEMP["CODE"].ToString() + "' AND CDATE = '" + LoopDate.ToString("yyyyMMdd") + "'  ");
                                if (drWTME.Count() == 0)
                                {
                                    
                                    // check Business Trip
                                    if(dtTNRQ.Rows.Count > 0)
                                    {
                                        DataRow[] drTNRQ = dtTNRQ.Select(" CODE='" + drEMP["CODE"].ToString() + "' AND FDATE <= '" + LoopDate.ToString("yyyyMMdd") + "' AND TDATE >= '" + LoopDate.ToString("yyyyMMdd") + "'  ");
                                        if(drTNRQ.Count() == 0)
                                        {
                                            sht = "X";
                                        }
                                    }
                                    else
                                    {
                                        sht = "X";
                                    }
                                    
                                    
                                    //_workNO++; // not work
                                }
                                else
                                {
                                    //_workAct++; // work act
                                }

                                string _lv = "";
                                DataRow[] drLVDet = dtLVDet.Select(" CODE='" + drEMP["CODE"].ToString() + "' AND CDATE = '" + LoopDate.ToString("yyyyMMdd") + "'  ");
                                if (drLVDet.Count() > 0)
                                {
                                    switch (drLVDet[0]["TYPES"].ToString())
                                    {
                                        case "PERS": _lv = "P"; _pers++; _workNO++; // not work
                                            break;
                                        case "SICK": _lv = "S"; _sick++; _workNO++; // not work
                                            break;
                                        case "ABSE": _lv = "X"; _abse++; 
                                            break;
                                        case "ANNU": _lv = "A"; _annu++; _workNO++; // not work
                                            break;
                                        case "OTHER": _lv = "Z"; _other++; 
                                            break;
                                    }
                                }

                                if (_lv != "")
                                {
                                    sht = _lv;
                                }


                                _workDay++; // working day
                            }
                            //*** Check Shift is Day Or Night Mon-Fri ***



                            //**** Check OT ****
                            string _ot = "";
                            DataRow[] drOTDet = dtOTDet.Select(" CODE='" + drEMP["CODE"].ToString() + "' AND ODATE = '" + LoopDate.ToString("yyyyMMdd") + "'  ");
                            if (drOTDet.Count() > 0)
                            {
                                _ot = "O";

                                if (sht == "D" || sht == "N" || sht == "X")
                                {
                                    _workOT++;
                                }
                                else
                                {
                                    _holidayOT++;
                                }
                            }
                            // end check OT 

                            


                            newData["D" + LoopDate.Day.ToString("00") + "WK"] = sht;
                            newData["D" + LoopDate.Day.ToString("00") + "OT"] = _ot;


                            // Next Day
                            LoopDate = LoopDate.AddDays(1);                        
                        } // end loop Day

                        //**** Not Working Percent *****
                        try { 
                            _workNOPerc = (Convert.ToDecimal(_workNO) / Convert.ToDecimal(_workDay)) * Convert.ToDecimal(100);
                        } catch(Exception ex) { }


                    }
                    //*** No Shift Data ***
                    else
                    {
                        for (int d = 1; d <= 31; d++)
                        {
                            newData["D" + d.ToString("00") + "WK"] = "";
                            newData["D" + d.ToString("00") + "OT"] = "";
                        }
                    }
                    #endregion

                    string _remark = "";
                    if (_DtJoin >= DateST && _DtJoin <= DateEN)
                    {
                        _remark += " J=" + _DtJoin.ToString("dd/MMM/yyyy");
                    }
                    else if (_DtJoin > DateEN)
                    {
                        _remark += " J=" + _DtJoin.ToString("dd/MMM/yyyy");
                    }


                    if (_DtResign >= DateST && _DtResign <= DateEN)
                    {
                        _remark += " R=" + _DtResign.ToString("dd/MMM/yyyy");
                    }
                    newData["REMARK"] = _remark;

                    newData["ABSE"] = _abse.ToString();
                    newData["ANNU"] = _annu.ToString();
                    newData["PERS"] = _pers.ToString();
                    newData["SICK"] = _sick.ToString();
                    newData["OTHER"] = _other.ToString();

                    newData["WORKDAY"] = _workDay.ToString();
                    newData["WORKACT"] = _workAct.ToString();
                    newData["WORKOT"] = _workOT.ToString();
                    newData["HOLIDAYOT"] = _holidayOT.ToString();
                    newData["WORKNO"] = _workNO.ToString();
                    newData["WORKNOPER"] = _workNOPerc.ToString("0.00")+"%";

                    dtData.Rows.Add(newData);



                    this.Invoke((MethodInvoker)delegate {
                        lblExpMonthlyStatus.Text = "ประมวลผลข้อมูล : " + dtData.Rows.Count.ToString() + " / " + dtEMP.Rows.Count.ToString();
                    });


                    Console.WriteLine("PROC : " + dtData.Rows.Count.ToString() + " / "+ dtEMP.Rows.Count.ToString() +" -> " + DateTime.Now.ToString("HH:mm:ss"));
                } // end foreach








                //*************************************
                //*************************************
                //********** Summary Data *************
                //*************************************
                //*************************************
                DataView dView = new DataView(dtData);
                DataTable disTableGrp = dView.ToTable(true, "SECT");
                
                if (disTableGrp.Rows.Count > 0)
                {
                    foreach(DataRow drGrp in disTableGrp.Rows)
                    {
                        mAttSummaryInfo mAttSum = new mAttSummaryInfo();
                        mAttSum.SECT = drGrp["SECT"].ToString();

                        decimal _annu = 0, _pers = 0, _sick = 0, _WorkAll = 0, _NoWorkAll = 0,
                            _WorkFO = 0, _NoWorkFO = 0, _WorkLE = 0,
                            _WorkSU = 0, _WorkEN = 0, _WorkSF = 0, _WorkTE = 0, _WorkTR = 0,
                            _NoWorkLE = 0, _WorkOP = 0, _NoWorkOP = 0,
                            _NoWorkSU = 0, _NoWorkEN = 0, _NoWorkSF = 0, _NoWorkTE = 0, _NoWorkTR = 0,
                            _WorkPerFO = 0, _WorkPerLE = 0, _WorkPerOP = 0,
                            _WorkPerSU = 0, _WorkPerEN = 0, _WorkPerSF = 0, _WorkPerTE = 0, _WorkPerTR = 0,
                            _NoWorkPerFO = 0, _NoWorkPerLE = 0, _NoWorkPerOP = 0,
                            _NoWorkPerSU = 0, _NoWorkPerEN = 0, _NoWorkPerSF = 0, _NoWorkPerTE = 0, _NoWorkPerTR = 0,
                             _dciCnt = 0, _subCnt = 0, _trnCnt = 0;

                        
                        DataRow[] _drByGroup = dtData.Select(" SECT='" + drGrp["SECT"].ToString() + "' ");
                        _WorkAll = getSumData(_drByGroup, "WORKDAY");                       
                        _NoWorkAll = getSumData(_drByGroup, "WORKNO");
                        _annu = getSumData(_drByGroup, "ANNU");
                        _pers = getSumData(_drByGroup, "PERS");
                        _sick = getSumData(_drByGroup, "SICK");



                        if (_drByGroup.Count() > 0)
                        {
                            foreach (DataRow drSubGrp in _drByGroup)
                            {
                                if (drSubGrp["CODE"].ToString().StartsWith("I") )
                                {
                                    _subCnt += Convert.ToDecimal(drSubGrp["ANNU"].ToString());
                                    _subCnt += Convert.ToDecimal(drSubGrp["PERS"].ToString());
                                    _subCnt += Convert.ToDecimal(drSubGrp["SICK"].ToString());
                                }
                                else if (drSubGrp["CODE"].ToString().StartsWith("7"))
                                {
                                    _trnCnt += Convert.ToDecimal(drSubGrp["ANNU"].ToString());
                                    _trnCnt += Convert.ToDecimal(drSubGrp["PERS"].ToString());
                                    _trnCnt += Convert.ToDecimal(drSubGrp["SICK"].ToString());
                                }
                                else
                                {
                                    _dciCnt += Convert.ToDecimal(drSubGrp["ANNU"].ToString());
                                    _dciCnt += Convert.ToDecimal(drSubGrp["PERS"].ToString());
                                    _dciCnt += Convert.ToDecimal(drSubGrp["SICK"].ToString());
                                }
                            }
                        }


                        DataRow[] _drWork_SU = dtData.Select(" SECT='" + drGrp["SECT"].ToString() + "' AND POSIT IN ('SE','SU','SS','ST','SU') ");
                        _WorkSU = getSumData(_drWork_SU, "WORKDAY");
                        _NoWorkSU = getSumData(_drWork_SU, "WORKNO");

                        DataRow[] _drWork_EN = dtData.Select(" SECT='" + drGrp["SECT"].ToString() + "' AND POSIT IN ('EN','EN.S') ");
                        _WorkEN = getSumData(_drWork_EN, "WORKDAY");
                        _NoWorkEN = getSumData(_drWork_EN, "WORKNO");

                        DataRow[] _drWork_SF = dtData.Select(" SECT='" + drGrp["SECT"].ToString() + "' AND POSIT IN ('SF') ");
                        _WorkSF = getSumData(_drWork_SF, "WORKDAY");
                        _NoWorkSF = getSumData(_drWork_SF, "WORKNO");

                        DataRow[] _drWork_TE = dtData.Select(" SECT='" + drGrp["SECT"].ToString() + "' AND POSIT IN ('TE','TE.S') ");
                        _WorkTE = getSumData(_drWork_TE, "WORKDAY");
                        _NoWorkTE = getSumData(_drWork_TE, "WORKNO");

                        DataRow[] _drWork_TR = dtData.Select(" SECT='" + drGrp["SECT"].ToString() + "' AND POSIT IN ('TR') ");
                        _WorkTR = getSumData(_drWork_TR, "WORKDAY");
                        _NoWorkTR = getSumData(_drWork_TR, "WORKNO");


                        DataRow[] _drWork_FO = dtData.Select(" SECT='" + drGrp["SECT"].ToString() + "' AND POSIT IN ('FO','FO.S') "); 
                        _WorkFO = getSumData(_drWork_FO, "WORKDAY");
                        _NoWorkFO = getSumData(_drWork_FO, "WORKNO");


                        DataRow[] _drWork_LE = dtData.Select(" SECT='" + drGrp["SECT"].ToString() + "' AND POSIT IN ('LE','LE.S') ");                        
                        _WorkLE = getSumData(_drWork_LE, "WORKDAY");
                        _NoWorkLE = getSumData(_drWork_LE, "WORKNO");


                        DataRow[] _drWork_OP = dtData.Select(" SECT='" + drGrp["SECT"].ToString() + "' AND POSIT IN ('OP','OP.S') ");                        
                        _WorkOP = getSumData(_drWork_OP, "WORKDAY");
                        _NoWorkOP = getSumData(_drWork_OP, "WORKNO");


                        //**** Not Working Percent *****
                        try
                        {
                            _NoWorkPerSU = (_NoWorkSU / _WorkAll ) * Convert.ToDecimal(100);
                        }
                        catch (Exception ex) { }

                        //**** Not Working Percent *****
                        try
                        {
                            _NoWorkPerEN = (_NoWorkEN / _WorkAll) * Convert.ToDecimal(100);
                        }
                        catch (Exception ex) { }

                        //**** Not Working Percent *****
                        try
                        {
                            _NoWorkPerTR = (_NoWorkTR / _WorkAll) * Convert.ToDecimal(100);
                        }
                        catch (Exception ex) { }

                        //**** Not Working Percent *****
                        try
                        {
                            _NoWorkPerSF = (_NoWorkSF / _WorkAll) * Convert.ToDecimal(100);
                        }
                        catch (Exception ex) { }

                        //**** Not Working Percent *****
                        try
                        {
                            _NoWorkPerTE = (_NoWorkTE / _WorkAll) * Convert.ToDecimal(100);
                        }
                        catch (Exception ex) { }


                        //**** Not Working Percent *****
                        try
                        {
                            _NoWorkPerFO = (_NoWorkFO / _WorkAll) * Convert.ToDecimal(100);
                        }catch (Exception ex) { }

                        //**** Not Working Percent *****
                        try
                        {
                            _NoWorkPerLE = (_NoWorkLE / _WorkAll) * Convert.ToDecimal(100);
                        }
                        catch (Exception ex) { }

                        //**** Not Working Percent *****
                        try
                        {
                            _NoWorkPerOP = (_NoWorkOP / _WorkAll) * Convert.ToDecimal(100);
                        }
                        catch (Exception ex) { }


                        _WorkPerSU = (100 - _NoWorkPerSU);
                        _WorkPerEN = (100 - _NoWorkPerEN);
                        _WorkPerTR = (100 - _NoWorkPerTR);
                        _WorkPerSF = (100 - _NoWorkPerSF);
                        _WorkPerTE = (100 - _NoWorkPerTE);
                        _WorkPerFO = (100 - _NoWorkPerFO);
                        _WorkPerLE = (100 - _NoWorkPerLE);
                        _WorkPerOP = (100 - _NoWorkPerOP);

                        mAttSum.WORKNOPER = ((_NoWorkAll / _WorkAll) * 100).ToString("0.00") + "%";
                        mAttSum.WORKDAYPER = (100 - ((_NoWorkAll / _WorkAll) * 100)).ToString("0.00") + "%";
                        mAttSum.WORKNO = _NoWorkAll.ToString("N0");

                        try
                        {
                            mAttSum.ANNU = ((_annu / _WorkAll) * 100).ToString("0.00") + "%";
                        }
                        catch (Exception ex) { mAttSum.ANNU = "0%"; }
                        try
                        {
                            mAttSum.PERS = ((_pers / _WorkAll) * 100).ToString("0.00") + "%";
                        }
                        catch (Exception ex) { mAttSum.PERS = "0%"; }
                        try
                        {
                            mAttSum.SICK = ((_sick / _WorkAll) * 100).ToString("0.00") + "%";
                        }
                        catch (Exception ex) { mAttSum.SICK = "0%"; }
                        try
                        {
                            mAttSum.DCI = ((_dciCnt / _WorkAll) * 100).ToString("0.00") + "%";
                        }
                        catch (Exception ex) { mAttSum.DCI = "0%"; }
                        try
                        {
                            mAttSum.SUB = ((_subCnt / _WorkAll) * 100).ToString("0.00") + "%";
                        }
                        catch (Exception ex) { mAttSum.SUB = "0%"; }
                        try
                        {
                            mAttSum.TRN = ((_trnCnt / _WorkAll) * 100).ToString("0.00") + "%";
                        }
                        catch (Exception ex) { mAttSum.TRN = "0%"; }     
                        
                        mAttSum.WORKNO_SU = _NoWorkPerSU.ToString("0.00") + "%";
                        mAttSum.WORKNO_EN = _NoWorkPerEN.ToString("0.00") + "%";
                        mAttSum.WORKNO_SF = _NoWorkPerSF.ToString("0.00") + "%";
                        mAttSum.WORKNO_TR = _NoWorkPerTR.ToString("0.00") + "%";
                        mAttSum.WORKNO_TE = _NoWorkPerTE.ToString("0.00") + "%";
                        mAttSum.WORKNO_FO = _NoWorkPerFO.ToString("0.00") + "%";
                        mAttSum.WORKNO_LE = _NoWorkPerLE.ToString("0.00") + "%";
                        mAttSum.WORKNO_OP = _NoWorkPerOP.ToString("0.00") + "%";
                        mAttSum.WORKDAY_SU = _WorkPerSU.ToString("0.00") + "%";
                        mAttSum.WORKDAY_EN = _WorkPerEN.ToString("0.00") + "%";
                        mAttSum.WORKDAY_SF = _WorkPerSF.ToString("0.00") + "%";
                        mAttSum.WORKDAY_TR = _WorkPerTR.ToString("0.00") + "%";
                        mAttSum.WORKDAY_TE = _WorkPerTE.ToString("0.00") + "%";
                        mAttSum.WORKDAY_FO = _WorkPerFO.ToString("0.00") + "%";
                        mAttSum.WORKDAY_LE = _WorkPerLE.ToString("0.00") + "%";
                        mAttSum.WORKDAY_OP = _WorkPerOP.ToString("0.00") + "%";
                        oAryAttSum.Add(mAttSum);

                    }
                }
                //*************************************
                //*************************************
                //****** END  Summary Data      *******
                //*************************************
                //*************************************

                //***** Convert DataTable to Object 
                foreach (DataRow drData in dtData.Rows)
                {
                    AttendanceInfo.mAttInfo mAtt = new AttendanceInfo.mAttInfo();
                    mAtt.SECT = drData["SECT"].ToString();
                    mAtt.DVCD = drData["DVCD"].ToString();
                    mAtt.COSTCENTER = drData["COSTCENTER"].ToString();
                    mAtt.GRPOT = drData["GRPOT"].ToString();
                    mAtt.GRPL = drData["GRPL"].ToString();
                    mAtt.SHGRP = drData["SHGRP"].ToString();
                    mAtt.CODE = drData["CODE"].ToString();
                    mAtt.FNAME = drData["FNAME"].ToString();
                    mAtt.POSIT = drData["POSIT"].ToString();
                    mAtt.BUDGET = drData["BUDGET"].ToString();
                    mAtt.D01WK = drData["D01WK"].ToString();
                    mAtt.D01OT = drData["D01OT"].ToString();
                    mAtt.D02WK = drData["D02WK"].ToString();
                    mAtt.D02OT = drData["D02OT"].ToString();
                    mAtt.D03WK = drData["D03WK"].ToString();
                    mAtt.D03OT = drData["D03OT"].ToString();
                    mAtt.D04WK = drData["D04WK"].ToString();
                    mAtt.D04OT = drData["D04OT"].ToString();
                    mAtt.D05WK = drData["D05WK"].ToString();
                    mAtt.D05OT = drData["D05OT"].ToString();
                    mAtt.D06WK = drData["D06WK"].ToString();
                    mAtt.D06OT = drData["D06OT"].ToString();
                    mAtt.D07WK = drData["D07WK"].ToString();
                    mAtt.D07OT = drData["D07OT"].ToString();
                    mAtt.D08WK = drData["D08WK"].ToString();
                    mAtt.D08OT = drData["D08OT"].ToString();
                    mAtt.D09WK = drData["D09WK"].ToString();
                    mAtt.D09OT = drData["D09OT"].ToString();
                    mAtt.D10WK = drData["D10WK"].ToString();
                    mAtt.D10OT = drData["D10OT"].ToString();
                    mAtt.D11WK = drData["D11WK"].ToString();
                    mAtt.D11OT = drData["D11OT"].ToString();
                    mAtt.D12WK = drData["D12WK"].ToString();
                    mAtt.D12OT = drData["D12OT"].ToString();
                    mAtt.D13WK = drData["D13WK"].ToString();
                    mAtt.D13OT = drData["D13OT"].ToString();
                    mAtt.D14WK = drData["D14WK"].ToString();
                    mAtt.D14OT = drData["D14OT"].ToString();
                    mAtt.D15WK = drData["D15WK"].ToString();
                    mAtt.D15OT = drData["D15OT"].ToString();
                    mAtt.D16WK = drData["D16WK"].ToString();
                    mAtt.D16OT = drData["D16OT"].ToString();
                    mAtt.D17WK = drData["D17WK"].ToString();
                    mAtt.D17OT = drData["D17OT"].ToString();
                    mAtt.D18WK = drData["D18WK"].ToString();
                    mAtt.D18OT = drData["D18OT"].ToString();
                    mAtt.D19WK = drData["D19WK"].ToString();
                    mAtt.D19OT = drData["D19OT"].ToString();
                    mAtt.D20WK = drData["D20WK"].ToString();
                    mAtt.D20OT = drData["D20OT"].ToString();
                    mAtt.D21WK = drData["D21WK"].ToString();
                    mAtt.D21OT = drData["D21OT"].ToString();
                    mAtt.D22WK = drData["D22WK"].ToString();
                    mAtt.D22OT = drData["D22OT"].ToString();
                    mAtt.D23WK = drData["D23WK"].ToString();
                    mAtt.D23OT = drData["D23OT"].ToString();
                    mAtt.D24WK = drData["D24WK"].ToString();
                    mAtt.D24OT = drData["D24OT"].ToString();
                    mAtt.D25WK = drData["D25WK"].ToString();
                    mAtt.D25OT = drData["D25OT"].ToString();
                    mAtt.D26WK = drData["D26WK"].ToString();
                    mAtt.D26OT = drData["D26OT"].ToString();
                    mAtt.D27WK = drData["D27WK"].ToString();
                    mAtt.D27OT = drData["D27OT"].ToString();
                    mAtt.D28WK = drData["D28WK"].ToString();
                    mAtt.D28OT = drData["D28OT"].ToString();
                    mAtt.D29WK = drData["D29WK"].ToString();
                    mAtt.D29OT = drData["D29OT"].ToString();
                    mAtt.D30WK = drData["D30WK"].ToString();
                    mAtt.D30OT = drData["D30OT"].ToString();
                    mAtt.D31WK = drData["D31WK"].ToString();
                    mAtt.D31OT = drData["D31OT"].ToString();
                    mAtt.REMARK = drData["REMARK"].ToString();
                    mAtt.ABSE = drData["ABSE"].ToString();
                    mAtt.ANNU = drData["ANNU"].ToString();
                    mAtt.PERS = drData["PERS"].ToString();
                    mAtt.SICK = drData["SICK"].ToString();
                    mAtt.OTHER = drData["OTHER"].ToString();
                    mAtt.WORKDAY = drData["WORKDAY"].ToString();
                    mAtt.WORKACT = drData["WORKACT"].ToString();
                    mAtt.WORKOT = drData["WORKOT"].ToString();
                    mAtt.HOLIDAYOT = drData["HOLIDAYOT"].ToString();
                    mAtt.WORKNO = drData["WORKNO"].ToString();
                    mAtt.WORKNOPER = drData["WORKNOPER"].ToString();
                    oAryAtt.Add(mAtt);
                }
                

            } // end if



            if (dtData.Rows.Count > 0)
            {
                SaveFileDialog saveFileDlg = new SaveFileDialog();
                saveFileDlg.FileName = "Attendance_Monthly_"+DateST.ToString("MMMyyyy")+"_" + DateTime.Now.ToString("yyyyMMdd");
                saveFileDlg.RestoreDirectory = true;
                saveFileDlg.Filter = "Excel Files (.xlsx)|*.xlsx;";
                DialogResult dlg = saveFileDlg.ShowDialog();
                if (dlg == DialogResult.OK)
                {
                    this.Invoke((MethodInvoker)delegate {
                        lblExpMonthlyStatus.Text = "";
                    });

                    if (File.Exists(saveFileDlg.FileName))
                    {
                        File.Delete(saveFileDlg.FileName);
                    }


                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    string _pathFle = Application.StartupPath + "\\TEMPLATE\\TMP_ATTENDANCE_MONTHLY.xlsx";
                    FileInfo template = new FileInfo(_pathFle);

                    using (var package = new ExcelPackage(template))
                    {
                        ExcelWorksheet wsData = package.Workbook.Worksheets["DATA"];
                        wsData.Cells["A1"].Value = "ข้อมูลประจำเดือน " + DateST.ToString("MMM, yyyy") ;
                        wsData.Cells["A3:CF"+ oAryAtt.Count + 3].LoadFromCollection(oAryAtt);

                        ExcelWorksheet wsSummary= package.Workbook.Worksheets["SUMMARY"];
                        wsSummary.Cells["A1"].Value = "ข้อมูลประจำเดือน " + DateST.ToString("MMM, yyyy");
                        wsSummary.Cells["A3:P" + oAryAttSum.Count + 3].LoadFromCollection(oAryAttSum);

                        package.SaveAs(new FileInfo(saveFileDlg.FileName));
                    }

                    MessageBox.Show("Success in "+ dtData.Rows.Count.ToString() +" records.");
                }
            }
            else
            {
                MessageBox.Show("No data export");
            }

        }

        public decimal getSumData(DataRow[] oDrData, string ColumnName)
        {
            decimal _rowNumber = 0, result = 0;
            foreach (DataRow drData in oDrData)
            {
                try { _rowNumber = Convert.ToDecimal( drData[ColumnName].ToString()); } catch { }
                result += _rowNumber;
            }
            return result;
        }


        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void btnExportMP_Click(object sender, EventArgs e)
        {
            ClsOraConnectDB oOraDCI = new ClsOraConnectDB("DCI");
            ClsOraConnectDB oOraSUB = new ClsOraConnectDB("DCISUB");
            ClsOraConnectDB oOraTRN = new ClsOraConnectDB("DCITRN");

            //DateTime DateST = new DateTime(dkrRptMPST.Value.Year, dkrRptMPST.Value.Month, 1);
            //DateTime DateEN = DateST.AddMonths(1).AddDays(-1);
            DateTime DateST = dkrRptMPST.Value;
            DateTime DateEN = dkrRptMPEN.Value; 

            

            DataTable dtEMP = new DataTable();
            string strEMP = @"SELECT CODE, WTYPE, WSTS, EMP_NAME, JOIN_DT, RESIGN,  POSI_CD, DV_CD, GRP, SECT, DEPT, GRPOT, GRPL, SHGRP, BUDGET, COSTCENTER FROM (
                                SELECT V.CODE, V.WTYPE, V.WSTS, V.PREN || ' ' || V.NAME  || ' ' || V.SURN EMP_NAME, V.JOIN JOIN_DT, V.RESIGN, V.POSI_CD, V.DV_CD, GRP, SECT, DEPT, GRPOT, GRPL, SHGRP, GB02 BUDGET, COSTCENTER 
                                FROM DCI.VI_EMP_MSTR V 
                                WHERE V.JOIN <= '" + DateEN.ToString("dd/MMM/yyyy") + "' AND (RESIGN IS NULL or RESIGN = '01/jan/1900' or RESIGN BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"') 

                                UNION ALL

                                SELECT V.CODE, V.WTYPE, V.WSTS, V.PREN || ' ' || V.NAME  || ' ' || V.SURN EMP_NAME, V.JOIN JOIN_DT, V.RESIGN, V.POSI_CD, V.DV_CD, GRP, SECT, DEPT, GRPOT, GRPL, SHGRP, GB02 BUDGET, COSTCENTER 
                                FROM DCITC.VI_EMP_MSTR V 
                                WHERE V.JOIN <= '" + DateEN.ToString("dd/MMM/yyyy") + "' AND (RESIGN IS NULL or RESIGN = '01/jan/1900' or RESIGN BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"')  

                                UNION ALL

                                SELECT V.CODE, V.WTYPE, V.WSTS, V.PREN || ' ' || V.NAME  || ' ' || V.SURN EMP_NAME, V.JOIN JOIN_DT, V.RESIGN, V.POSI_CD, V.DV_CD, GRP, SECT, DEPT, GRPOT, GRPL, SHGRP, GB02 BUDGET, COSTCENTER 
                                FROM DEV_OFFICE.VI_EMP_MSTR V 
                                WHERE V.JOIN <= '" + DateEN.ToString("dd/MMM/yyyy") + "' AND (RESIGN IS NULL or RESIGN = '01/jan/1900' or RESIGN BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"')  
                            )   ";
            OracleCommand cmdEMP = new OracleCommand();
            cmdEMP.CommandText = strEMP;
            dtEMP = oOraDCI.Query(cmdEMP);


            DataTable dtGRP = new DataTable();
            string strGRP = @"SELECT COSTCENTER FROM (
                                SELECT V.CODE, V.WTYPE, V.WSTS, V.PREN || ' ' || V.NAME  || ' ' || V.SURN EMP_NAME, V.JOIN JOIN_DT, V.RESIGN, V.POSI_CD, V.DV_CD, GRP, SECT, DEPT, GRPOT, GRPL, SHGRP, GB02 BUDGET, COSTCENTER 
                                FROM DCI.VI_EMP_MSTR V 
                                WHERE  (RESIGN IS NULL or RESIGN = '01/jan/1900' or RESIGN BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"') 

                                UNION ALL

                                SELECT V.CODE, V.WTYPE, V.WSTS, V.PREN || ' ' || V.NAME  || ' ' || V.SURN EMP_NAME, V.JOIN JOIN_DT, V.RESIGN, V.POSI_CD, V.DV_CD, GRP, SECT, DEPT, GRPOT, GRPL, SHGRP, GB02 BUDGET, COSTCENTER 
                                FROM DCITC.VI_EMP_MSTR V 
                                WHERE  (RESIGN IS NULL or RESIGN = '01/jan/1900' or RESIGN BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"')  

                                UNION ALL

                                SELECT V.CODE, V.WTYPE, V.WSTS, V.PREN || ' ' || V.NAME  || ' ' || V.SURN EMP_NAME, V.JOIN JOIN_DT, V.RESIGN, V.POSI_CD, V.DV_CD, GRP, SECT, DEPT, GRPOT, GRPL, SHGRP, GB02 BUDGET, COSTCENTER 
                                FROM DEV_OFFICE.VI_EMP_MSTR V 
                                WHERE  (RESIGN IS NULL or RESIGN = '01/jan/1900' or RESIGN BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"')  
                            )                            
                            GROUP BY COSTCENTER
                            ORDER BY COSTCENTER DESC   ";
            OracleCommand cmdGRP = new OracleCommand();
            cmdGRP.CommandText = strGRP;
            dtGRP = oOraDCI.Query(cmdGRP);




            DataTable dtOT = new DataTable();
            string strOT = @"SELECT COSTCENTER, OT1, OT15, OT2, OT3 FROM(
                                SELECT E.COSTCENTER, SUM(DCI.texttohour(O.OT1)) OT1, SUM(DCI.texttohour(O.OT15)) OT15, SUM(DCI.texttohour(O.OT2)) OT2, SUM(DCI.texttohour(O.OT3)) OT3   
                                FROM DCI.OTRQ O
                                LEFT JOIN DCI.EMPM E ON E.CODE = O.CODE 
                                WHERE ODATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"'
                                GROUP BY E.COSTCENTER

                                UNION ALL

                                SELECT E.COSTCENTER, SUM(DCI.texttohour(O.OT1)) OT1, SUM(DCI.texttohour(O.OT15)) OT15, SUM(DCI.texttohour(O.OT2)) OT2, SUM(DCI.texttohour(O.OT3)) OT3   
                                FROM DCITC.OTRQ O
                                LEFT JOIN DCITC.EMPM E ON E.CODE = O.CODE 
                                WHERE ODATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"'
                                GROUP BY E.COSTCENTER

                                UNION ALL

                                SELECT E.COSTCENTER, SUM(DCI.texttohour(O.OT1)) OT1, SUM(DCI.texttohour(O.OT15)) OT15, SUM(DCI.texttohour(O.OT2)) OT2, SUM(DCI.texttohour(O.OT3)) OT3   
                                FROM DEV_OFFICE.OTRQ O
                                LEFT JOIN DEV_OFFICE.EMPM E ON E.CODE = O.CODE 
                                WHERE ODATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"'
                                GROUP BY E.COSTCENTER
                            )   ";
            OracleCommand cmdOT = new OracleCommand();
            cmdOT.CommandText = strOT;
            dtOT = oOraDCI.Query(cmdOT);



            DataTable dtEMCL = new DataTable();
            string strEMCL = @"SELECT YM, CODE, STSS FROM (
                                SELECT  E.YM, E.CODE, E.STSS 
                                FROM DCI.EMCL E
                                WHERE YM = '" + DateST.ToString("yyyyMM") + @"'

                                UNION ALL

                                SELECT  E.YM, E.CODE, E.STSS 
                                FROM DCITC.EMCL E
                                WHERE YM = '" + DateST.ToString("yyyyMM") + @"'

                                UNION ALL

                                SELECT  E.YM, E.CODE, E.STSS 
                                FROM DEV_OFFICE.EMCL E
                                WHERE YM = '" + DateST.ToString("yyyyMM") + @"'
                            ) ";
            OracleCommand cmdEMCL = new OracleCommand();
            cmdEMCL.CommandText = strEMCL;
            dtEMCL = oOraDCI.Query(cmdEMCL);


            Console.WriteLine("SHIFT : " + dtEMCL.Rows.Count.ToString() + " -> OK " + DateTime.Now.ToString("HH:mm:ss"));


            DataTable dtWTME = new DataTable();
            string strWTME = @"SELECT CODE, CDATE, TIME, WSTN, DUTY FROM (
                                    SELECT W.CODE, TO_CHAR(W.CDATE,'YYYYMMDD') CDATE, W.TIME, W.WSTN, W.DUTY
                                    FROM DCI.WTME W
                                    WHERE DUTY = 'I' AND CDATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' AND '" + DateEN.ToString("dd/MMM/yyyy") + @"' 

                                    UNION ALL

                                    SELECT W.CODE, TO_CHAR(W.CDATE,'YYYYMMDD') CDATE, W.TIME, W.WSTN, W.DUTY
                                    FROM DCITC.WTME W
                                    WHERE DUTY = 'I' AND CDATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' AND '" + DateEN.ToString("dd/MMM/yyyy") + @"' 

                                    UNION ALL

                                    SELECT W.CODE, TO_CHAR(W.CDATE,'YYYYMMDD') CDATE, W.TIME, W.WSTN, W.DUTY
                                    FROM DEV_OFFICE.WTME W
                                    WHERE DUTY = 'I' AND CDATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' AND '" + DateEN.ToString("dd/MMM/yyyy") + @"' 
                                )  ";
            OracleCommand cmdWTME = new OracleCommand();
            cmdWTME.CommandText = strWTME;
            dtWTME = oOraDCI.Query(cmdWTME);


            DataTable dtTNRQ = new DataTable();
            string strTNRQ = @"SELECT CODE, TO_CHAR(FDATE,'YYYYMMDD') FDATE, TO_CHAR(TDATE,'YYYYMMDD') TDATE FROM(
                                  SELECT T.CODE, T.FDATE, T.TDATE
                                  FROM DCI.TNRQ T
                                  WHERE (FDATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "'  AND '" + DateEN.ToString("dd/MMM/yyyy") + @"') OR
                                        (FDATE <= '" + DateST.ToString("dd/MMM/yyyy") + "' AND TDATE >= '" + DateST.ToString("dd/MMM/yyyy") + @"' )

                                  UNION ALL

                                  SELECT T.CODE, T.FDATE, T.TDATE
                                  FROM DCITC.TNRQ T
                                  WHERE FDATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' AND '" + DateEN.ToString("dd/MMM/yyyy") + @"' OR
                                        (FDATE <= '" + DateST.ToString("dd/MMM/yyyy") + "' AND TDATE >= '" + DateST.ToString("dd/MMM/yyyy") + @"' )

                                  UNION ALL

                                  SELECT T.CODE, T.FDATE, T.TDATE
                                  FROM DEV_OFFICE.TNRQ T
                                  WHERE FDATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' AND '" + DateEN.ToString("dd/MMM/yyyy") + @"' OR
                                        (FDATE <= '" + DateST.ToString("dd/MMM/yyyy") + "' AND TDATE >= '" + DateST.ToString("dd/MMM/yyyy") + @"' )

                                ) ";
            OracleCommand cmdTNRQ = new OracleCommand();
            cmdTNRQ.CommandText = strTNRQ;
            dtTNRQ = oOraDCI.Query(cmdTNRQ);




            List<mManpowerInfo> oAryMP = new List<mManpowerInfo>();
            List<mOvertimeInfo> oAryOT = new List<mOvertimeInfo>();


            //DataView dView = new DataView(dtEMP);
            //DataTable disTableGrp = dView.ToTable(true, "DEPT", "SECT", "COSTCENTER"  );

            if (dtGRP.Rows.Count > 0)
            {
                int itm = 1;
                foreach (DataRow drGrp in dtGRP.Rows)
                {
                    DataRow[] drALL = dtEMP.Select(" COSTCENTER='"+ drGrp["COSTCENTER"].ToString() + "' ");
                    DataRow[] drALLGRP = dtEMP.Select(" COSTCENTER='" + drGrp["COSTCENTER"].ToString() + "' AND SECT <> '' AND DEPT <> '' ", "DEPT DESC");
                    DataRow[] drPD = dtEMP.Select(" COSTCENTER='"+ drGrp["COSTCENTER"].ToString() + "' AND POSI_CD IN ('PD') ");
                    DataRow[] drDI = dtEMP.Select(" COSTCENTER='"+ drGrp["COSTCENTER"].ToString() + "' AND POSI_CD IN ('DI') ");
                    DataRow[] drSGM = dtEMP.Select(" COSTCENTER='"+ drGrp["COSTCENTER"].ToString() + "' AND POSI_CD IN ('SGM') ");
                    DataRow[] drGM = dtEMP.Select(" COSTCENTER='"+ drGrp["COSTCENTER"].ToString() + "' AND POSI_CD IN ('GM') ");
                    DataRow[] drAGM = dtEMP.Select(" COSTCENTER='"+ drGrp["COSTCENTER"].ToString() + "' AND POSI_CD IN ('AG') ");
                    DataRow[] drSMG = dtEMP.Select(" COSTCENTER='"+ drGrp["COSTCENTER"].ToString() + "' AND POSI_CD IN ('SMG') ");
                    DataRow[] drMG = dtEMP.Select(" COSTCENTER='"+ drGrp["COSTCENTER"].ToString() + "' AND POSI_CD IN ('MG') ");
                    DataRow[] drAM = dtEMP.Select(" COSTCENTER='"+ drGrp["COSTCENTER"].ToString() + "' AND POSI_CD IN ('AM') ");
                    DataRow[] drAV = dtEMP.Select(" COSTCENTER='"+ drGrp["COSTCENTER"].ToString() + "' AND POSI_CD IN ('AV') ");
                    DataRow[] drSU = dtEMP.Select(" COSTCENTER='"+ drGrp["COSTCENTER"].ToString() + "' AND POSI_CD IN ('SU','SE','SS','ST') ");
                    DataRow[] drEN = dtEMP.Select(" COSTCENTER='"+ drGrp["COSTCENTER"].ToString() + "' AND POSI_CD IN ('EN','EN.S') ");
                    DataRow[] drSF = dtEMP.Select(" COSTCENTER='"+ drGrp["COSTCENTER"].ToString() + "' AND POSI_CD IN ('SF') ");
                    DataRow[] drTR = dtEMP.Select(" COSTCENTER='"+ drGrp["COSTCENTER"].ToString() + "'  AND POSI_CD IN ('TR') ");
                    DataRow[] drFO = dtEMP.Select(" COSTCENTER='"+ drGrp["COSTCENTER"].ToString() + "'  AND POSI_CD IN ('FO','FO.S') ");
                    DataRow[] drTE = dtEMP.Select(" COSTCENTER='" + drGrp["COSTCENTER"].ToString() + "'  AND POSI_CD IN ('TE','TE.S') ");
                    DataRow[] drLE = dtEMP.Select(" COSTCENTER='" + drGrp["COSTCENTER"].ToString() + "' AND POSI_CD IN ('LE','LE.S')  ");

                    DataRow[] drOP = dtEMP.Select(" COSTCENTER='" + drGrp["COSTCENTER"].ToString() + "' AND POSI_CD IN ('OP','OP.S') ");
                    
                    DataRow[] drOP_TN = dtEMP.Select(" COSTCENTER='" + drGrp["COSTCENTER"].ToString() + "' AND POSI_CD IN ('TN') ");
                    DataRow[] drDR = dtEMP.Select(" COSTCENTER='" + drGrp["COSTCENTER"].ToString() + "' AND POSI_CD IN ('DR') ");



                    //*********************************
                    //*********************************
                    //          MANPOWER 
                    //*********************************
                    //*********************************
                    #region MANPOWER
                    int _DciMonthCnt = 0, _DciDailyCnt = 0, _DciTempCnt = 0, _SubCnt = 0;
                    if(drOP.Count() > 0)
                    {
                        foreach (DataRow _drOP in drOP)
                        {
                            if (_drOP["CODE"].ToString().StartsWith("I"))
                            {
                                _SubCnt++;
                            }
                            else if (_drOP["CODE"].ToString().StartsWith("6"))
                            {
                                _DciTempCnt++;
                            }
                            else if (_drOP["CODE"].ToString().StartsWith("7"))
                            {
                            }
                            else
                            {
                                if (_drOP["WTYPE"].ToString() == "S") {
                                    _DciMonthCnt++;
                                }
                                else if (_drOP["WTYPE"].ToString() == "O") {
                                    _DciDailyCnt++;
                                }
                            }
                        }
                    }

                    mManpowerInfo mMP = new mManpowerInfo();
                    mMP.Item = itm.ToString();
                    mMP.Dept = drALLGRP[0]["DEPT"].ToString();
                    mMP.Sect = drALLGRP[0]["SECT"].ToString();
                    mMP.CostCenter = drGrp["COSTCENTER"].ToString();
                    mMP.President = (drPD.Count() > 0) ?  drPD.Count().ToString() : "";
                    mMP.Director = (drDI.Count() > 0) ?  drDI.Count().ToString(): "";
                    mMP.SGM = (drSGM.Count() > 0) ?  drSGM.Count().ToString(): "";
                    mMP.GM = (drGM.Count() > 0) ?  drGM.Count().ToString(): "";
                    mMP.AGM = (drAGM.Count() > 0) ?  drAGM.Count().ToString(): "";
                    mMP.SMG = (drSMG.Count() > 0) ? drSMG.Count().ToString(): "";
                    mMP.MGR = (drMG.Count() > 0) ?  drMG.Count().ToString(): "";
                    mMP.AM = (drAM.Count() > 0) ?  drAM.Count().ToString(): "";
                    mMP.AV = (drAV.Count() > 0) ? drAV.Count().ToString(): "";
                    mMP.SU = (drSU.Count() > 0) ?  drSU.Count().ToString(): "";
                    mMP.Engineer = (drEN.Count() > 0) ?  drEN.Count().ToString(): "";
                    mMP.Staff = (drSF.Count() > 0) ?  drSF.Count().ToString(): "";
                    mMP.Translator = (drTR.Count() > 0) ?  drTR.Count().ToString(): "";
                    mMP.Foreman = (drFO.Count() > 0) ?  drFO.Count().ToString(): "";
                    mMP.Technician = (drTE.Count() > 0) ? drTE.Count().ToString(): "";
                    mMP.Leader = (drLE.Count() > 0) ?  drLE.Count().ToString(): "";
                    mMP.PermanentOperator = (_DciMonthCnt > 0) ? _DciMonthCnt.ToString() : "";
                    mMP.DailyPermanentOperator = (_DciDailyCnt > 0 ) ? _DciDailyCnt.ToString() : "";
                    mMP.TemporaryOperator = (_DciTempCnt > 0) ? _DciTempCnt.ToString() : "";
                    mMP.Others = (drDR.Count() > 0) ?  drDR.Count().ToString(): "";
                    mMP.Sub = (_SubCnt > 0) ? _SubCnt.ToString() : "";
                    mMP.Trainee = (drOP_TN.Count() > 0) ?  drOP_TN.Count().ToString(): "";
                    mMP.Total = (drALL.Count() > 0) ?  drALL.Count().ToString(): "";

                    oAryMP.Add(mMP);
                    #endregion
                    //*********************************
                    //*********************************
                    //        END  MANPOWER 
                    //*********************************
                    //*********************************

                    #region OVERTIME

                    #region Loop Day
                    long _workDay = 0;
                    if (drALL.Count() > 0)
                    {
                        foreach (DataRow _drAll in drALL)
                        {
                            DataRow[] drEMCL = dtEMCL.Select(" CODE='" + _drAll["CODE"].ToString() + "' ");
                            if (drEMCL.Count() > 0)
                            {
                                DateTime LoopDate = DateST;
                                while (LoopDate <= DateEN)
                                {
                                    string sht = drEMCL[0]["STSS"].ToString().Substring(LoopDate.Day - 1, 1);

                                    //*** Check Shift is Day Or Night Mon-Fri ***
                                    if (sht == "D" || sht == "N")
                                    {
                                        // check working day time
                                        DataRow[] drWTME = dtWTME.Select(" CODE='" + _drAll["CODE"].ToString() + "' AND CDATE = '" + LoopDate.ToString("yyyyMMdd") + "'  ");
                                        if (drWTME.Count() == 0)
                                        {
                                            // check Business Trip
                                            if (dtTNRQ.Rows.Count > 0)
                                            {
                                                DataRow[] drTNRQ = dtTNRQ.Select(" CODE='" + _drAll["CODE"].ToString() + "' AND FDATE <= '" + LoopDate.ToString("yyyyMMdd") + "' AND TDATE >= '" + LoopDate.ToString("yyyyMMdd") + "'  ");
                                                if (drTNRQ.Count() > 0)
                                                {
                                                    _workDay++; // working day
                                                }
                                            }
                                        }
                                        else
                                        {
                                            _workDay++; // working day
                                        }
                                    }
                                    //*** Check Shift is Day Or Night Mon-Fri ***


                                    // Next Day
                                    LoopDate = LoopDate.AddDays(1);
                                } // end loop Day
                            }// end if
                        }
                    }
                    #endregion




                    DataRow[] drOT = dtOT.Select(" COSTCENTER='" + drGrp["COSTCENTER"].ToString() + "' ");

                    decimal _OT1 = getSumData(drOT, "OT1");
                    decimal _OT15 = getSumData(drOT, "OT15");
                    decimal _OT2 = getSumData(drOT, "OT2");
                    decimal _OT3 = getSumData(drOT, "OT3");

                    mOvertimeInfo mOT = new mOvertimeInfo();
                    mOT.Item = itm.ToString();
                    mOT.Dept = drALLGRP[0]["DEPT"].ToString();
                    mOT.Sect = drALLGRP[0]["SECT"].ToString();
                    mOT.CostCenter = drGrp["COSTCENTER"].ToString();
                    mOT.OT1 = (_OT1 > 0) ? _OT1.ToString("0.00") : "";
                    mOT.OT15 = (_OT15 > 0) ? _OT15.ToString("0.00") : "";
                    mOT.OT2 = (_OT2 > 0) ? _OT2.ToString("0.00") : "";
                    mOT.OT3 = (_OT3 > 0) ? _OT3.ToString("0.00") : "";
                    mOT.OTTotal = ((_OT1 + _OT15 + _OT2 + _OT3) > 0) ? (_OT1 + _OT15 + _OT2 + _OT3).ToString("0.00") : "";
                    mOT.WorkDay = (_workDay > 0 ) ? _workDay.ToString() : "";
                    mOT.WorkHour = (_workDay > 0) ? (_workDay * 8.75).ToString("0.00") : "";
                    oAryOT.Add(mOT);

                    #endregion






                    itm++;
                }// end foreach
            }



            //***** Manpower ******
            if (oAryMP.Count > 0)
            {
                SaveFileDialog saveFileDlg = new SaveFileDialog();
                saveFileDlg.FileName = "Manpower_" + DateST.ToString("ddMMMyyyy") + "_" + DateEN.ToString("ddMMMyyyy")+"_"+DateTime.Now.ToString("yyyyMMdd");
                saveFileDlg.RestoreDirectory = true;
                saveFileDlg.Filter = "Excel Files (.xlsx)|*.xlsx;";
                DialogResult dlg = saveFileDlg.ShowDialog();
                if (dlg == DialogResult.OK)
                {
                    this.Invoke((MethodInvoker)delegate {
                        lblExpMonthlyStatus.Text = "";
                    });

                    if (File.Exists(saveFileDlg.FileName))
                    {
                        File.Delete(saveFileDlg.FileName);
                    }


                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    string _pathFle = Application.StartupPath + "\\TEMPLATE\\TMP_MANPOWER.xlsx";
                    FileInfo template = new FileInfo(_pathFle);

                    using (var package = new ExcelPackage(template))
                    {
                        ExcelWorksheet wsManpower = package.Workbook.Worksheets["MANPOWER"];
                        wsManpower.Cells["C2"].Value = (DateST.Month < 4) ? (DateST.Year - 1).ToString() : DateST.Year.ToString();
                        wsManpower.Cells["C3"].Value = DateST.ToString("dd/MMM/yyyy") + " - " + DateEN.ToString("dd/MMM/yyyy");
                        wsManpower.Cells["B6:AD" + oAryMP.Count + 6].LoadFromCollection(oAryMP);

                        ExcelWorksheet wsOvertime = package.Workbook.Worksheets["OVERTIME"];
                        wsOvertime.Cells["C2"].Value = (DateST.Month < 4) ? (DateST.Year - 1).ToString() : DateST.Year.ToString();
                        wsOvertime.Cells["C3"].Value = DateST.ToString("dd/MMM/yyyy") + " - " + DateEN.ToString("dd/MMM/yyyy");
                        wsOvertime.Cells["B6:L" + oAryOT.Count + 6].LoadFromCollection(oAryOT);

                        package.SaveAs(new FileInfo(saveFileDlg.FileName));
                    }

                    MessageBox.Show("Success in " + oAryMP.Count.ToString() + " records.");
                }
            }
            else
            {
                MessageBox.Show("No data export");
            }



            Console.WriteLine("EMP : " + dtEMP.Rows.Count.ToString() + " -> OK " + DateTime.Now.ToString("HH:mm:ss"));
        }

        private void btnExportAtt_Click(object sender, EventArgs e)
        {
            DateTime DateST = new DateTime();
            DateTime DateEN = new DateTime();

            DateST = dkrRptST.Value.Date;
            DateEN = dkrRptEN.Value.Date;

            string strCon = "DCI";
            if (rdRptDCI.Checked) { strCon = "DCI"; }
            else if (rdRptSUB.Checked) { strCon = "DCISUB"; }
            else if (rdRptTRN.Checked) { strCon = "DCITRN"; }
            ClsOraConnectDB oOra = new ClsOraConnectDB(strCon);

            if (!rdRptDCI.Checked && !rdRptSUB.Checked && !rdRptTRN.Checked)
            {
                MessageBox.Show("Please select one in list DCI, SubContract, Trainee", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable dtEMP = new DataTable();
            string strEMP = @"SELECT V.CODE, V.PREN || ' ' || V.NAME  || ' ' || V.SURN EMP_NAME, V.JOIN, V.WSTS, V.WTYPE, V.POSI_CD, GRP, SECT, DEPT, GRPOT, GRPL, SHGRP, GB02 BUDGET 
                              FROM VI_EMP_MSTR V 
                              WHERE RESIGN IS NULL or RESIGN = '01/jan/1900' ";
            OracleCommand cmdEMP = new OracleCommand();
            cmdEMP.CommandText = strEMP;
            dtEMP = oOra.Query(cmdEMP);


            DataTable dtOT = new DataTable();
            string strOT = @"SELECT CODE, SUM(DCI.texttohour(O.OT1)) OT1, SUM(DCI.texttohour(O.OT15)) OT15, SUM(DCI.texttohour(O.OT2)) OT2, SUM(DCI.texttohour(O.OT3)) OT3   
                                FROM OTRQ O
                                WHERE ODATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"'
                                GROUP BY CODE  ";
            OracleCommand cmdOT = new OracleCommand();
            cmdOT.CommandText = strOT;
            dtOT = oOra.Query(cmdOT);


            DataTable dtLV = new DataTable();
            //string strLV = @"SELECT CODE, TYPE, round(SUM(L.TOTAL) / 525, 1) TOTALS
            //                    FROM LVRQ L
            //                    WHERE CDATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"'
            //                     and TYPE IN ('PERS','SICK','ABSE','LATE','EARL')
            //                     GROUP BY CODE, TYPE  ";

            string strLV = @"SELECT CODE, TYPE, POINTS, COUNT(POINTS) CNT_LV, CASE WHEN TYPE = 'LATE' THEN SUM(texttoMinute(TIMES)) ELSE ROUND(SUM(TOTAL) / 525, 1) END TOTALS FROM(
                                SELECT CODE, TYPE, 
                                    CASE WHEN L.TOTAL/525 < 1 THEN  0.5 ELSE 1 END POINTS, L.TIMES, L.TOTAL    
                                FROM LVRQ L
                                WHERE CDATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"'
                                 and TYPE IN ('PERS','SICK','ABSE','LATE','EARL','MATE','BMAT','MILI','PRIE','MARR','FUNE','TRAI','ANNU')  
                            )GROUP BY CODE, TYPE, POINTS ";
            OracleCommand cmdLV = new OracleCommand();
            cmdLV.CommandText = strLV;
            dtLV = oOra.Query(cmdLV);


            DataTable dtPENA = new DataTable();
            string strPENA = @"SELECT P.CODE, P.TYPE, SUM(P.W_TOTAL) W_TOTAL, SUM(P.P_TOTAL) P_TOTAL
                                FROM PENA P
                                WHERE PDATE BETWEEN '" + DateST.ToString("dd/MMM/yyyy") + "' and '" + DateEN.ToString("dd/MMM/yyyy") + @"'
                                GROUP BY P.CODE, P.TYPE  ";
            OracleCommand cmdPENA = new OracleCommand();
            cmdPENA.CommandText = strPENA;
            dtPENA = oOra.Query(cmdPENA);


            DataTable dtData = new DataTable();
            dtData.Columns.Add("CODE", typeof(string));
            dtData.Columns.Add("EMP_NAME", typeof(string));
            dtData.Columns.Add("JOIN", typeof(string));
            dtData.Columns.Add("POSI_CD", typeof(string));
            dtData.Columns.Add("WSTS", typeof(string));
            dtData.Columns.Add("WTYPE", typeof(string));
            dtData.Columns.Add("GRP", typeof(string));
            dtData.Columns.Add("SECT", typeof(string));
            dtData.Columns.Add("DEPT", typeof(string));
            dtData.Columns.Add("GRPOT", typeof(string));
            dtData.Columns.Add("GRPL", typeof(string));
            dtData.Columns.Add("SHGRP", typeof(string));
            dtData.Columns.Add("BUDGET", typeof(string));
            dtData.Columns.Add("OT1", typeof(string));
            dtData.Columns.Add("OT15", typeof(string));
            dtData.Columns.Add("OT2", typeof(string));
            dtData.Columns.Add("OT3", typeof(string));
            dtData.Columns.Add("LATE_T", typeof(string));
            dtData.Columns.Add("LATE_H", typeof(string));
            dtData.Columns.Add("LATE_M", typeof(string));
            dtData.Columns.Add("EARL", typeof(string));
            dtData.Columns.Add("SICK", typeof(string));
            dtData.Columns.Add("PERS_H", typeof(string));
            dtData.Columns.Add("PERS", typeof(string));
            dtData.Columns.Add("ABSE", typeof(string));
            dtData.Columns.Add("MATE", typeof(string));
            dtData.Columns.Add("MILI", typeof(string));
            dtData.Columns.Add("PRIE", typeof(string));
            dtData.Columns.Add("MARR", typeof(string));
            dtData.Columns.Add("FUNE", typeof(string));
            dtData.Columns.Add("ANNU_H", typeof(string));
            dtData.Columns.Add("ANNU", typeof(string));
            dtData.Columns.Add("PENALTY_V", typeof(string));
            dtData.Columns.Add("PENALTY_L", typeof(string));
            dtData.Columns.Add("PENALTY_S", typeof(string));
            dtData.Columns.Add("PER_SICK", typeof(string));
            dtData.Columns.Add("HR_NOTE", typeof(string));

            if (dtEMP.Rows.Count > 0)
            {
                foreach (DataRow drData in dtEMP.Rows)
                {
                    DateTime _Join = new DateTime();
                    try
                    {
                        _Join = Convert.ToDateTime(drData["JOIN"].ToString());
                    }
                    catch { }

                    DataRow newData = dtData.NewRow();
                    newData["CODE"] = drData["CODE"].ToString();
                    newData["EMP_NAME"] = drData["EMP_NAME"].ToString();
                    newData["JOIN"] = _Join.ToString("dd/MM/yyyy");
                    newData["POSI_CD"] = drData["POSI_CD"].ToString();
                    newData["WSTS"] = drData["WSTS"].ToString();
                    newData["WTYPE"] = drData["WTYPE"].ToString();
                    newData["GRP"] = drData["GRP"].ToString();
                    newData["SECT"] = drData["SECT"].ToString();
                    newData["DEPT"] = drData["DEPT"].ToString();
                    newData["GRPOT"] = drData["GRPOT"].ToString();
                    newData["GRPL"] = drData["GRPL"].ToString();
                    newData["SHGRP"] = drData["SHGRP"].ToString();
                    newData["BUDGET"] = drData["BUDGET"].ToString();

                    //**********************
                    //        OT 
                    //**********************
                    string _ot1 = "", _ot15 = "", _ot2 = "", _ot3 = "", _hr_note = "";
                    if (dtOT.Rows.Count > 0)
                    {
                        DataRow[] drOT = dtOT.Select(" CODE='" + drData["CODE"].ToString() + "' ");
                        if (drOT.Count() > 0)
                        {
                            _ot1 = drOT[0]["OT1"].ToString();
                            _ot15 = drOT[0]["OT15"].ToString();
                            _ot2 = drOT[0]["OT2"].ToString();
                            _ot3 = drOT[0]["OT3"].ToString();
                        }
                    }
                    newData["OT1"] = _ot1;
                    newData["OT15"] = _ot15;
                    newData["OT2"] = _ot2;
                    newData["OT3"] = _ot3;
                    //**********************
                    //        OT 
                    //**********************


                    //**********************
                    //        PENA 
                    //**********************
                    string _pena_v = "", _pena_l = "", _pena_s = "";
                    if (dtPENA.Rows.Count > 0)
                    {
                        DataRow[] drPENA_V = dtPENA.Select(" CODE='" + drData["CODE"].ToString() + "' AND TYPE = 'VERB' ");
                        DataRow[] drPENA_L = dtPENA.Select(" CODE='" + drData["CODE"].ToString() + "' AND TYPE = 'LETT' ");
                        DataRow[] drPENA_S = dtPENA.Select(" CODE='" + drData["CODE"].ToString() + "' AND TYPE = 'SUSP' ");
                        if (drPENA_V.Count() > 0){ _pena_v = drPENA_V[0]["P_TOTAL"].ToString(); }
                        if (drPENA_L.Count() > 0){ _pena_l = drPENA_L[0]["P_TOTAL"].ToString(); }
                        if (drPENA_S.Count() > 0) { _pena_s = drPENA_S[0]["P_TOTAL"].ToString(); }
                    }
                    newData["PENALTY_V"] = _pena_v;
                    newData["PENALTY_L"] = _pena_l;
                    newData["PENALTY_S"] = _pena_s;
                    //**********************
                    //        PENA 
                    //**********************


                    //**********************
                    //        Leave 
                    //**********************
                    decimal _pers = 0, _pers_h = 0, _sick = 0, _abse = 0, _late = 0, _late_cnt =0, _late_h = 0, _late_m = 0, _earl = 0, _annu = 0, _annu_h = 0, _mate = 0, _mili=0, _prie=0, _marr=0, _fune=0;
                    if (dtLV.Rows.Count > 0)
                    {
                        DataRow[] drLVPers = dtLV.Select(" CODE='" + drData["CODE"].ToString() + "' AND TYPE = 'PERS' AND POINTS='1' ");
                        DataRow[] drLVPers_H = dtLV.Select(" CODE='" + drData["CODE"].ToString() + "' AND TYPE = 'PERS' AND POINTS='0.5' ");
                        DataRow[] drLVAnnu = dtLV.Select(" CODE='" + drData["CODE"].ToString() + "' AND TYPE = 'ANNU' AND POINTS='1' ");
                        DataRow[] drLVAnnu_H = dtLV.Select(" CODE='" + drData["CODE"].ToString() + "' AND TYPE = 'ANNU' AND POINTS='0.5' ");


                        DataRow[] drLVSick = dtLV.Select(" CODE='" + drData["CODE"].ToString() + "' AND TYPE = 'SICK' ");
                        DataRow[] drLVAbse = dtLV.Select(" CODE='" + drData["CODE"].ToString() + "' AND TYPE = 'ABSE' ");
                        DataRow[] drLVLate = dtLV.Select(" CODE='" + drData["CODE"].ToString() + "' AND TYPE = 'LATE' ");
                        DataRow[] drLVEarl = dtLV.Select(" CODE='" + drData["CODE"].ToString() + "' AND TYPE = 'EARL' ");

                        DataRow[] drLVMate = dtLV.Select(" CODE='" + drData["CODE"].ToString() + "' AND TYPE = 'MATE' ");
                        DataRow[] drLVMili = dtLV.Select(" CODE='" + drData["CODE"].ToString() + "' AND TYPE = 'MILI' ");
                        DataRow[] drLVPrie = dtLV.Select(" CODE='" + drData["CODE"].ToString() + "' AND TYPE = 'PRIE' ");
                        DataRow[] drLVMarr = dtLV.Select(" CODE='" + drData["CODE"].ToString() + "' AND TYPE = 'MARR' ");
                        DataRow[] drLVFune = dtLV.Select(" CODE='" + drData["CODE"].ToString() + "' AND TYPE = 'FUNE' ");

                        if (drLVPers.Count() > 0) { _pers = Convert.ToDecimal(drLVPers[0]["CNT_LV"].ToString()); }
                        if (drLVPers_H.Count() > 0) { _pers_h = Convert.ToDecimal(drLVPers_H[0]["CNT_LV"].ToString()); }
                        if (drLVAnnu.Count() > 0) { _annu = Convert.ToDecimal(drLVAnnu[0]["CNT_LV"].ToString()); }
                        if (drLVAnnu_H.Count() > 0) { _annu_h = Convert.ToDecimal(drLVAnnu_H[0]["CNT_LV"].ToString()); }



                        _sick = SummaryAttendanceTotal(drLVSick);
                        _abse = SummaryAttendanceTotal(drLVAbse);
                        
                        //***** Late Calculate ******
                        _late_cnt = drLVLate.Count();
                        if(_late_cnt > 0)
                        {
                            _late = SummaryAttendanceTotal(drLVLate);
                            _late_h = Math.Floor(_late / 60);
                            _late_m = _late - (_late_h * 60);
                        }
                        //***** End Late Calculate ******

                        _earl = SummaryAttendanceTotal(drLVEarl);
                        _mate = SummaryAttendanceTotal(drLVMate);
                        _mili = SummaryAttendanceTotal(drLVMili);
                        _prie = SummaryAttendanceTotal(drLVPrie);
                        _marr = SummaryAttendanceTotal(drLVMarr);
                        _fune = SummaryAttendanceTotal(drLVFune);
                        
                        //if (drLVSick.Count() > 0) { _sick = Convert.ToDecimal(drLVSick[0]["TOTALS"].ToString()); }
                        //if (drLVAbse.Count() > 0) { _abse = Convert.ToDecimal(drLVAbse[0]["TOTALS"].ToString()); }
                        //if (drLVLate.Count() > 0) { _late = Convert.ToDecimal(drLVLate[0]["TOTALS"].ToString()); }
                        //if (drLVEarl.Count() > 0) { _earl = Convert.ToDecimal(drLVEarl[0]["TOTALS"].ToString()); }

                        //if (drLVMate.Count() > 0) { _mate = Convert.ToDecimal(drLVMate[0]["TOTALS"].ToString()); }
                        //if (drLVMili.Count() > 0) { _mili = Convert.ToDecimal(drLVMili[0]["TOTALS"].ToString()); }
                        //if (drLVPrie.Count() > 0) { _prie = Convert.ToDecimal(drLVPrie[0]["TOTALS"].ToString()); }
                        //if (drLVMarr.Count() > 0) { _marr = Convert.ToDecimal(drLVMarr[0]["TOTALS"].ToString()); }
                        //if (drLVFune.Count() > 0) { _fune = Convert.ToDecimal(drLVFune[0]["TOTALS"].ToString()); }

                    }

                    if ((_pers + _pers_h + _sick) > 10)
                    {
                        _hr_note = "E";
                    }
                    else if ((_pers + _pers_h + _sick) >= 5 && (_pers + _pers_h + _sick) <= 10)
                    {
                        _hr_note = "D";
                    }
                    else
                    {
                        _hr_note = "";
                    }

                    newData["EARL"] = (_earl > 0) ? _earl.ToString("N1") : "";
                    newData["PERS"] = (_pers > 0) ? _pers.ToString("N1") : "";
                    newData["PERS_H"] = (_pers_h > 0) ? _pers_h.ToString("N1") : "";
                    newData["ANNU"] = (_annu > 0) ? _annu.ToString("N1") : "";
                    newData["ANNU_H"] = (_annu_h > 0) ? _annu_h.ToString("N1") : "";
                    newData["SICK"] = (_sick > 0) ? _sick.ToString("N1") : "";
                    newData["ABSE"] = (_abse > 0) ? _abse.ToString("N1") : "";

                    newData["MATE"] = (_mate > 0) ? _mate.ToString("N1") : "";
                    newData["MILI"] = (_mili > 0) ? _mili.ToString("N1") : "";
                    newData["PRIE"] = (_prie > 0) ? _prie.ToString("N1") : "";
                    newData["MARR"] = (_marr > 0) ? _marr.ToString("N1") : "";
                    newData["FUNE"] = (_fune > 0) ? _fune.ToString("N1") : "";



                    newData["LATE_T"] = (_late_cnt > 0) ? _late_cnt.ToString("N0") : "";
                    newData["LATE_H"] = (_late_h > 0) ? _late_h.ToString("N0") : "";
                    newData["LATE_M"] = (_late_m > 0) ? _late_m.ToString("N0") : "";

                    newData["PER_SICK"] = (_pers + _pers_h + _sick) > 0 ? (_pers + _pers_h + _sick).ToString("N1") : "";
                    newData["HR_NOTE"] = _hr_note;
                    //**********************
                    //        Leave 
                    //**********************


                    // ****  Add new Row ****
                    dtData.Rows.Add(newData);
                }
            }



            if (dtData.Rows.Count > 0)
            {
                SaveFileDialog saveFileDlg = new SaveFileDialog();
                saveFileDlg.FileName = strCon + "_Employee_" + DateTime.Now.ToString("yyyyMMdd");
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
                    CreateCSVFile(dtData, saveFileDlg.FileName);
                    MessageBox.Show("Success");
                }
            }
            else
            {
                MessageBox.Show("No data export");
            }
        }


        private decimal SummaryAttendanceTotal(DataRow[] drData)
        {
            decimal result = 0;
            if (drData.Count() > 0) {
                foreach (DataRow dr in drData)
                {
                    result += Convert.ToDecimal(dr["TOTALS"].ToString());
                }                           
            }
            return result;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }



}
