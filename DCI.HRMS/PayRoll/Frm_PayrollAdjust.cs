using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using DCI.Security.Model;
using DCI.Security.Service;
using DCIBizPro.Util.Excel;
using DCI.HRMS.Service;
using DCI.HRMS.Model.Personal;
using DCI.HRMS.Util;
using DCI.HRMS.Common;
using System.Collections;
using DCI.HRMS.Model.Evaluation;
using DCIBizPro.Util.Data;


using Excel = Microsoft.Office.Interop.Excel;
using Oracle.ManagedDataAccess.Client;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
//using System.Data.OracleClient;

using DCI.HRMS.Model.Payroll;
using ExcelDataReader;
using System.Data.SqlClient;
using System.Xml;
using OfficeOpenXml;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.VisualBasic;

namespace DCI.HRMS.PayRoll
{
    public partial class Frm_PayrollAdjust : Form
    {

        ClsOraConnectDB oOraDCI = new ClsOraConnectDB("DCI");
        ClsOraConnectDB oOraSUB = new ClsOraConnectDB("DCISUB");
        ClsOraConnectDB oOraTRN = new ClsOraConnectDB("DCITRN");
        SqlConnectDB oConHRM = new SqlConnectDB("dbHRM");

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        private DialogBox.Dlg_Password pwDiag = new DCI.HRMS.DialogBox.Dlg_Password();
        private readonly UserAccountService userAccountService = UserAccountService.Instance();
        private EmployeeService empSvr = EmployeeService.Instance();
        private DataSet ds = new DataSet();
        private StatusManager stsMgr = new StatusManager();
        private ApplicationManager appMgr = ApplicationManager.Instance();
        private EvaluationService evaSvr = EvaluationService.Instance();
        private ExcelFile xl = new ExcelFile();



        public Frm_PayrollAdjust()
        {
            InitializeComponent();
        }


        public DataTable ReadExcelFile(string filePath)
        {
            DataTable dt = new DataTable();
            // Create an empty DataSet to store the data from the Excel file
            var dataSet = new DataSet();

            // Create an instance of ExcelDataReader and open the Excel file
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                // Configure the reader options
                var readerOptions = new ExcelReaderConfiguration
                {
                    // Set the following option if you want to read .xlsx files
                    //FileType = ExcelFileType.OpenXml
                };

                // Read the data from the Excel file into the DataSet
                dataSet = reader.AsDataSet();
            }

            // Access the data in the DataSet
            if (dataSet.Tables.Count > 0)
            {
                dt = dataSet.Tables[0];

                // Set Column Name
                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Count > 0)
                    {
                        for (int col = 0; col < dt.Columns.Count; col++)
                        {
                            dt.Columns[col].ColumnName = dt.Rows[0][col].ToString();
                        }
                    }
                }

                // Skip the header row
                if (dt.Rows.Count > 0)
                {
                    dt.Rows.RemoveAt(0);
                }
            }

            return dt;
        }

        public string DecodeLanguage(string data)
        {
            StringBuilder output = new StringBuilder();

            if (data != null && data.Length > 0)
            {
                foreach (char c in data)
                {
                    int ascii = (int)c;

                    if (ascii > 160)
                    {
                        ascii += 3424;
                    }
                    output.Append((char)ascii);
                }
            }

            return output.ToString();
        }
        public string EncodeLanguage(string data)
        {
            StringBuilder output = new StringBuilder();
            if (data != null && data.Length > 0)
            {
                foreach (char c in data)
                {
                    int ascii = (int)c;

                    if (ascii >= 160)
                    {
                        ascii -= 3424;
                    }

                    output.Append((char)ascii);
                }
            }
            return output.ToString();
        }

        public DateTime ConvertDate(string _date) {
            
            DateTime result = new DateTime(1900, 1, 1);
            try
            {
                result = new DateTime(Convert.ToInt32(_date.Substring(0, 4)),
                    Convert.ToInt32(_date.Substring(4, 2)),
                    Convert.ToInt32(_date.Substring(6, 2)));
            }
            catch
            {
            }

            return result;
        }


        private void Frm_PayrollAdjust_Load(object sender, EventArgs e)
        {

        }



        #region TISSUE
        private void btnTissueBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog flDrg = new OpenFileDialog();
            flDrg.Filter = "Excel File|*.xlsx";

            if (flDrg.ShowDialog(this) != DialogResult.Cancel)
            {
                txtTissue.Text = flDrg.FileName;
            }
        }

        private void btnTissueSave_Click(object sender, EventArgs e)
        {
            // path to your excel file
            string path = txtTissue.Text;

            DateTime pDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 16);


            //******* Update All ********
            if (rdTissueDCI.Checked)
            {
                string strUpdF = @"UPDATE DCI.EMPM SET atbn01='35' WHERE SEX = 'F' AND (resign is null or resign >= '" + pDate.ToString("dd/MMM/yyyy") + "' ) ";
                OracleCommand cmdUpdF = new OracleCommand();
                cmdUpdF.CommandText = strUpdF;
                oOraDCI.ExecuteCommand(cmdUpdF);

                string strUpdM = @"UPDATE DCI.EMPM SET atbn01='30' WHERE SEX = 'M' AND (resign is null or resign >= '" + pDate.ToString("dd/MMM/yyyy") + "' ')  ";
                OracleCommand cmdUpdM = new OracleCommand();
                cmdUpdM.CommandText = strUpdM;
                oOraDCI.ExecuteCommand(cmdUpdM);
            }
            else if (rdTissueTRN.Checked)
            {                
                string strUpdF = @"UPDATE DEV_OFFICE.EMPM SET atbn01='35' WHERE SEX = 'F' AND (resign is null or resign >= '" + pDate.ToString("dd/MMM/yyyy") + "' ) ";
                OracleCommand cmdUpdF = new OracleCommand();
                cmdUpdF.CommandText = strUpdF;
                oOraTRN.ExecuteCommand(cmdUpdF);

                string strUpdM = @"UPDATE DEV_OFFICE.EMPM SET atbn01='30' WHERE SEX = 'M' AND (resign is null or resign >= '" + pDate.ToString("dd/MMM/yyyy") + "' ')  ";
                OracleCommand cmdUpdM = new OracleCommand();
                cmdUpdM.CommandText = strUpdM;
                oOraTRN.ExecuteCommand(cmdUpdM);
            }
            //******* Update All ********



            DataTable dtData = ReadExcelFile(path);

            DataTable dtDataTissue = new DataTable();
            dtDataTissue.Columns.Add("code", typeof(string));
            dtDataTissue.Columns.Add("fname", typeof(string));
            dtDataTissue.Columns.Add("posit", typeof(string));
            dtDataTissue.Columns.Add("tissue", typeof(decimal));


            dgvTissue.Rows.Clear();
            bool _error = false;
            string _errStr = "";
            if (dtData.Rows.Count > 0)
            {
                // loop through the worksheet rows and columns

                foreach (DataRow dr in dtData.Rows)
                {
                    string _code = "";
                    string _tissue = "";

                    try { _code = dr[1].ToString(); } catch { }
                    try { _tissue = dr[6].ToString(); } catch { }


                    decimal Tissue = 0;

                    if (_code != "" && _tissue != "")
                    {
                        if (_code.Length == 2)
                        {
                            _code = "000" + _code;
                        }
                        //****** Get & Check Employee *******
                        DataTable dtEmp = new DataTable();
                        string strEmp = @"SELECT [CODE],CONCAT([PREN],[NAME],' ',[SURN]) Fname, POSIT FROM Employee WHERE Code=@Code ";
                        SqlCommand cmdEmp = new SqlCommand();
                        cmdEmp.CommandText = strEmp;
                        cmdEmp.Parameters.Add(new SqlParameter("@Code", _code));
                        dtEmp = oConHRM.Query(cmdEmp);


                        //**** Check Employee ****
                        if (dtEmp.Rows.Count > 0)
                        {
                            try
                            {
                                Tissue = Math.Round(Convert.ToDecimal(_tissue),2);
                            }
                            catch
                            {
                                _errStr += _code + " : error Tissue." + System.Environment.NewLine;
                                _error = true;
                            }

                            if (!_error)
                            {
                                dtDataTissue.Rows.Add(_code, dtEmp.Rows[0]["Fname"].ToString(), dtEmp.Rows[0]["POSIT"].ToString(), Tissue);
                            }
                        }
                        else
                        {
                            _errStr += _code + " : error no Emp Code." + System.Environment.NewLine;
                            _error = true;
                        }//*** end Check Employee ***


                    } // check have data

                } // end loop for


                //*** Error ***
                if (!_error && dtDataTissue.Rows.Count > 0)
                {
                    // loop for
                    foreach (DataRow dr in dtDataTissue.Rows)
                    {
                        string _code = "";
                        string _tissue = "";


                        try { _code = dr["code"].ToString(); } catch { }
                        try { _tissue = dr["tissue"].ToString(); } catch { }

                        if (_code != "" && _tissue != "")
                        {
                            decimal Pay = 0;

                            try
                            {
                                Pay = Math.Round(Convert.ToDecimal(_tissue),2);
                            }
                            catch { }

                            if (_code.StartsWith("7"))
                            {
                                string strUpd = @"UPDATE DEV_OFFICE.EMPM SET atbn01='" + Pay.ToString() + "' WHERE  CODE='" + _code + "' ";
                                OracleCommand cmdUpd = new OracleCommand();
                                cmdUpd.CommandText = strUpd;
                                oOraTRN.ExecuteCommand(cmdUpd);
                            }
                            else if (_code.StartsWith("I"))
                            {

                            }
                            else
                            {
                                string strUpd = @"UPDATE EMPM SET atbn01='" + Pay.ToString() + "' WHERE  CODE='" + _code + "' ";
                                OracleCommand cmdUpd = new OracleCommand();
                                cmdUpd.CommandText = strUpd;
                                oOraDCI.ExecuteCommand(cmdUpd);
                            }


                            dgvTissue.Rows.Add(_code, dr["fname"].ToString(), dr["posit"].ToString(), Pay.ToString());


                        } // end if check




                    }// end foreach

                    MessageBox.Show("Successful!");


                } // end check error
                else
                {
                    MessageBox.Show(_errStr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }


            }

            lblTissue.Text = "จำนวน "+dgvTissue.Rows.Count.ToString();
        }

        private void btnTissueRenew_Click(object sender, EventArgs e)
        {
            txtTissue.Text = "";
            dgvTissue.Rows.Clear();
        }


        private void btnExcelTemplateTissue_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                string sourceFilePath = Application.StartupPath + "\\TEMPLATE\\TMP_TISSUE.xlsx";

                saveFileDialog.Title = "Save File";
                saveFileDialog.FileName = "Tissue_Template.xlsx";
                saveFileDialog.Filter = "Excel Files|*.xlsx"; // Tissue the file filters

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string destinationFilePath = saveFileDialog.FileName;

                    try
                    {
                        // Copy the file from the source to the destination
                        File.Copy(sourceFilePath, destinationFilePath);

                        MessageBox.Show("Download successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while copying the file: " + ex.Message);
                    }
                }
            }
        }

        #endregion



        #region STUDENT LOAN
        private void btnStdLoanTemplate_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                string sourceFilePath = Application.StartupPath + "\\TEMPLATE\\TMP_STUDENTLOAN.xlsx";

                saveFileDialog.Title = "Save File";
                saveFileDialog.FileName = "StudentLoan_Template.xlsx";
                saveFileDialog.Filter = "Excel Files|*.xlsx"; // Tissue the file filters

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string destinationFilePath = saveFileDialog.FileName;

                    try
                    {
                        // Copy the file from the source to the destination
                        File.Copy(sourceFilePath, destinationFilePath);

                        MessageBox.Show("Download successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while copying the file: " + ex.Message);
                    }
                }
            }
        }

        private void btnStdLoanReNew_Click(object sender, EventArgs e)
        {
            txtStdLoan.Text = "";
            dgvStudLoan.Rows.Clear();
        }

        private void btnStdLoanBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog flDrg = new OpenFileDialog();
            flDrg.Filter = "Excel File|*.xlsx";

            if (flDrg.ShowDialog(this) != DialogResult.Cancel)
            {
                txtStdLoan.Text = flDrg.FileName;
            }
        }

        private void btnStdLoanSave_Click(object sender, EventArgs e)
        {
            // path to your excel file
            string path = txtStdLoan.Text;

            DataTable dtData = ReadExcelFile(path);

            DataTable dtDataStdLoan = new DataTable();
            dtDataStdLoan.Columns.Add("pdate", typeof(DateTime));
            dtDataStdLoan.Columns.Add("code", typeof(string));
            dtDataStdLoan.Columns.Add("fname", typeof(string));
            dtDataStdLoan.Columns.Add("money", typeof(decimal));
            dtDataStdLoan.Columns.Add("remark", typeof(string));


            dgvStudLoan.Rows.Clear();
            bool _error = false;
            string _errStr = "", _pdateRcd = "";
            if (dtData.Rows.Count > 0)
            {
                // loop through the worksheet rows and columns
                int i = 0;
                foreach (DataRow dr in dtData.Rows)
                {
                    string _code = "";
                    string _pdate = "";
                    string _fname = "";
                    string _money = "";
                    string _remark = "";

                    try { _pdate = dr[0].ToString(); } catch { }
                    try { _code = dr[1].ToString(); } catch { }
                    try { _fname = dr[2].ToString(); } catch { }
                    try { _money = dr[3].ToString(); } catch { }
                    try { _remark = dr[4].ToString(); } catch { }


                    //**** Check Pdate ****
                    if (i == 0)
                    {
                        _pdateRcd = _pdate;
                    }
                    if(_pdate != _pdateRcd && i > 0)
                    {
                        _errStr += _code + " : error no PDate not match." + System.Environment.NewLine;
                        _error = true;
                    }




                    decimal Money = 0;
                    DateTime PDate = new DateTime(1900,1,1);

                    if (_code != "" && _pdate != "" && _money != "")
                    {
                        if (_code.Length == 2)
                        {
                            _code = "000" + _code;
                        }
                        //****** Get & Check Employee *******
                        DataTable dtEmp = new DataTable();
                        string strEmp = @"SELECT [CODE],CONCAT([PREN],[NAME],' ',[SURN]) Fname, POSIT FROM Employee WHERE Code=@Code ";
                        SqlCommand cmdEmp = new SqlCommand();
                        cmdEmp.CommandText = strEmp;
                        cmdEmp.Parameters.Add(new SqlParameter("@Code", _code));
                        dtEmp = oConHRM.Query(cmdEmp);


                        //**** Check Employee ****
                        if (dtEmp.Rows.Count > 0)
                        {
                            try
                            {
                                Money = Math.Round(Convert.ToDecimal(_money),2);
                            }
                            catch
                            {
                                _errStr += _code + " : error Money." + System.Environment.NewLine;
                                _error = true;
                            }


                            PDate = ConvertDate(_pdate);
                            if(PDate.Year < 2000)
                            {
                                _errStr += _code + " : error no PDate wrong format." + System.Environment.NewLine;
                                _error = true;
                            }

                            if (!_error)
                            {
                                dtDataStdLoan.Rows.Add(PDate, _code, _fname, Money, _remark);
                            }
                        }
                        else
                        {
                            _errStr += _code + " : error no Emp Code." + System.Environment.NewLine;
                            _error = true;
                        }//*** end Check Employee ***


                    } // check have data
                    
                    
                    i++;
                } // end loop for




                //*** Error ***
                if (!_error && dtDataStdLoan.Rows.Count > 0)
                {
                    DateTime PDate = new DateTime(1900,1,1);
                    PDate = Convert.ToDateTime(dtDataStdLoan.Rows[0]["pdate"].ToString());
                    //****** Delete *******
                    string strDel = "DELETE  FROM PR_STUDENTLOAN WHERE PDATE = '" + PDate.ToString("dd/MMM/yyyy") + "' ";
                    OracleCommand cmdDel = new OracleCommand();
                    cmdDel.CommandText = strDel;
                    oOraDCI.ExecuteCommand(cmdDel);
                    



                    // loop for
                    foreach (DataRow dr in dtDataStdLoan.Rows)
                    {
                        DateTime _PDate = new DateTime(1900, 1, 1);
                        string _code = "";
                        string _fname = "";
                        decimal _money = 0;
                        string _remark = "";


                        try { _PDate = Convert.ToDateTime(dr["pdate"].ToString()); } catch { }
                        try { _code = dr["code"].ToString(); } catch { }
                        try { _fname = dr["fname"].ToString(); } catch { }
                        try { _money = Math.Round(Convert.ToDecimal(dr["money"].ToString()),2); } catch { }
                        try { _remark = dr["remark"].ToString(); } catch { }

                        if (_code != "" && _money > 0)
                        {


                            
                            if (_code.StartsWith("7"))
                            {
                                
                            }
                            else if (_code.StartsWith("I"))
                            {

                            }
                            else
                            {
                                string strInstr = @"INSERT INTO PR_STUDENTLOAN (PDATE, CODE, MONEY, REMARK) 
                                                        VALUES ( '" + _PDate.ToString("dd/MMM/yyyy") + @"' ,   
                                                        '" + _code + "', '" + _money.ToString() + "', '" + _remark + "')  ";
                                OracleCommand cmdInstr = new OracleCommand();
                                cmdInstr.CommandText = strInstr;                                
                                oOraDCI.ExecuteCommand(cmdInstr);
                            }

                        } // end if check




                    }// end foreach


                    DataTable dtSel = new DataTable();
                    string strSel = @"SELECT PDATE, CODE, MONEY, REMARK FROM PR_STUDENTLOAN WHERE PDATE = '" + PDate.ToString("dd/MMM/yyyy") + @"' ";
                    OracleCommand cmdSel = new OracleCommand();
                    cmdSel.CommandText = strSel;
                    dtSel = oOraDCI.Query(cmdSel);

                    dgvStudLoan.Rows.Clear();
                    if(dtSel.Rows.Count > 0)
                    {
                        decimal _money = 0;
                        foreach(DataRow drSel in dtSel.Rows)
                        {
                            _money += Math.Round(Convert.ToDecimal(drSel["MONEY"].ToString()),2);
                            dgvStudLoan.Rows.Add(drSel["PDATE"].ToString(), drSel["CODE"].ToString(), drSel["MONEY"].ToString(), drSel["REMARK"].ToString() );
                        }

                        //**** Total ****
                        dgvStudLoan.Rows.Add("TOTAL", "", _money.ToString(), "");
                    }



                    

                    MessageBox.Show("Successful!");


                } // end check error
                else
                {
                    MessageBox.Show(_errStr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }


            }

            lblStdLoan.Text = "จำนวน " + dgvStudLoan.Rows.Count.ToString();
        }


        #endregion




        #region Cremation Fund
        private void btnCremFundTemplate_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                string sourceFilePath = Application.StartupPath + "\\TEMPLATE\\TMP_CREMATION_FUND.xlsx";

                saveFileDialog.Title = "Save File";
                saveFileDialog.FileName = "CremationFund_Template.xlsx";
                saveFileDialog.Filter = "Excel Files|*.xlsx"; // Tissue the file filters

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string destinationFilePath = saveFileDialog.FileName;

                    try
                    {
                        // Copy the file from the source to the destination
                        File.Copy(sourceFilePath, destinationFilePath);

                        MessageBox.Show("Download successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while copying the file: " + ex.Message);
                    }
                }
            }
        }

        private void btnCremFundReNew_Click(object sender, EventArgs e)
        {
            txtCremFund.Text = "";
            dgvCremFund.Rows.Clear();
        }

        private void btnCremFundBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog flDrg = new OpenFileDialog();
            flDrg.Filter = "Excel File|*.xlsx";

            if (flDrg.ShowDialog(this) != DialogResult.Cancel)
            {
                txtCremFund.Text = flDrg.FileName;
            }
        }

        private void btnCremFundSave_Click(object sender, EventArgs e)
        {
            // path to your excel file
            string path = txtCremFund.Text;

            DataTable dtData = ReadExcelFile(path);

            DataTable dtDataCremFund = new DataTable();
            dtDataCremFund.Columns.Add("pdate", typeof(DateTime));
            dtDataCremFund.Columns.Add("code", typeof(string));
            dtDataCremFund.Columns.Add("fname", typeof(string));
            dtDataCremFund.Columns.Add("money", typeof(decimal));


            dgvCremFund.Rows.Clear();
            bool _error = false;
            string _errStr = "", _pdateRcd = "";
            if (dtData.Rows.Count > 0)
            {
                // loop through the worksheet rows and columns
                int i = 0;
                foreach (DataRow dr in dtData.Rows)
                {
                    string _code = "";
                    string _pdate = "";
                    string _fname = "";
                    string _money = "";

                    try { _pdate = dr[0].ToString(); } catch { }
                    try { _code = dr[1].ToString(); } catch { }
                    try { _fname = dr[2].ToString(); } catch { }
                    try { _money = dr[3].ToString(); } catch { }


                    //**** Check Pdate ****
                    if (i == 0)
                    {
                        _pdateRcd = _pdate;
                    }
                    if (_pdate != _pdateRcd && i > 0)
                    {
                        _errStr += _code + " : error no PDate not match." + System.Environment.NewLine;
                        _error = true;
                    }




                    decimal Money = 0;
                    DateTime PDate = new DateTime(1900, 1, 1);

                    if (_code != "" && _pdate != "" && _money != "")
                    {
                        if (_code.Length == 2)
                        {
                            _code = "000" + _code;
                        }
                        //****** Get & Check Employee *******
                        DataTable dtEmp = new DataTable();
                        string strEmp = @"SELECT [CODE],CONCAT([PREN],[NAME],' ',[SURN]) Fname, POSIT FROM Employee WHERE Code=@Code ";
                        SqlCommand cmdEmp = new SqlCommand();
                        cmdEmp.CommandText = strEmp;
                        cmdEmp.Parameters.Add(new SqlParameter("@Code", _code));
                        dtEmp = oConHRM.Query(cmdEmp);


                        //**** Check Employee ****
                        if (dtEmp.Rows.Count > 0)
                        {
                            try
                            {
                                Money = Math.Round(Convert.ToDecimal(_money),2);
                            }
                            catch
                            {
                                _errStr += _code + " : error Money." + System.Environment.NewLine;
                                _error = true;
                            }


                            PDate = ConvertDate(_pdate);
                            if (PDate.Year < 2000)
                            {
                                _errStr += _code + " : error no PDate wrong format." + System.Environment.NewLine;
                                _error = true;
                            }

                            if (!_error)
                            {
                                dtDataCremFund.Rows.Add(PDate, _code, _fname, Money);
                            }
                        }
                        else
                        {
                            _errStr += _code + " : error no Emp Code." + System.Environment.NewLine;
                            _error = true;
                        }//*** end Check Employee ***


                    } // check have data


                    i++;
                } // end loop for




                //*** Error ***
                if (!_error && dtDataCremFund.Rows.Count > 0)
                {

                    //dtDataCremFund
                    //***************************************
                    //********* Group By DataTable **********
                    DataTable dtGrpData = dtDataCremFund.AsEnumerable()
                        .GroupBy(r => new { pdate = r.Field<DateTime>("pdate"), code = r.Field<string>("code"), fname = r.Field<string>("fname") })
                        .Select(s => {
                            DataRow drNew = dtDataCremFund.NewRow();
                            drNew["pdate"] = s.Key.pdate;
                            drNew["code"] = s.Key.code;
                            drNew["fname"] = s.Key.fname;
                            drNew["money"] = s.Sum(d => d.Field<decimal>("money"));

                            return drNew;
                        }).CopyToDataTable();
                    //******* End Group By DataTable ********
                    //***************************************


                    DateTime PDate = new DateTime(1900, 1, 1);
                    PDate = Convert.ToDateTime(dtGrpData.Rows[0]["pdate"].ToString());
                    //****** Delete *******
                    string strDel = "DELETE  FROM EMPM_CREMATION_FUND WHERE PDATE = '" + PDate.ToString("dd/MMM/yyyy") + "' ";
                    OracleCommand cmdDel = new OracleCommand();
                    cmdDel.CommandText = strDel;
                    oOraDCI.ExecuteCommand(cmdDel);




                    // loop for
                    foreach (DataRow dr in dtGrpData.Rows)
                    {
                        DateTime _PDate = new DateTime(1900, 1, 1);
                        string _code = "";
                        string _fname = "";
                        decimal _money = 0;


                        try { _PDate = Convert.ToDateTime(dr["pdate"].ToString()); } catch { }
                        try { _code = dr["code"].ToString(); } catch { }
                        try { _fname = dr["fname"].ToString(); } catch { }
                        try { _money = Math.Round(Convert.ToDecimal(dr["money"].ToString()),2); } catch { }                        

                        if (_code != "" && _money > 0)
                        {


                            if (_code.StartsWith("7"))
                            {

                            }
                            else if (_code.StartsWith("I"))
                            {

                            }
                            else
                            {
                                string strInstr = @"INSERT INTO EMPM_CREMATION_FUND (CODE, PDATE, AMOUNT ) 
                                                        VALUES ('" + _code + "', '" + _PDate.ToString("dd/MMM/yyyy") + @"' ,   
                                                         '" + _money.ToString() + "' )  ";
                                OracleCommand cmdInstr = new OracleCommand();
                                cmdInstr.CommandText = strInstr;
                                oOraDCI.ExecuteCommand(cmdInstr);
                            }

                        } // end if check




                    }// end foreach


                    DataTable dtSel = new DataTable();
                    string strSel = @"SELECT CODE, PDATE, AMOUNT FROM EMPM_CREMATION_FUND WHERE PDATE = '" + PDate.ToString("dd/MMM/yyyy") + @"' ";
                    OracleCommand cmdSel = new OracleCommand();
                    cmdSel.CommandText = strSel;
                    dtSel = oOraDCI.Query(cmdSel);

                    dgvCremFund.Rows.Clear();
                    if (dtSel.Rows.Count > 0)
                    {
                        decimal _money = 0;
                        foreach (DataRow drSel in dtSel.Rows)
                        {
                            _money += Math.Round(Convert.ToDecimal(drSel["AMOUNT"].ToString()),2);
                            dgvCremFund.Rows.Add(drSel["PDATE"].ToString(), drSel["CODE"].ToString(), drSel["AMOUNT"].ToString() );
                        }

                        //**** Total ****
                        dgvCremFund.Rows.Add("TOTAL", "", _money.ToString() );
                    }





                    MessageBox.Show("Successful!");


                } // end check error
                else
                {
                    MessageBox.Show(_errStr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }


            }

            lblCremFund.Text = "จำนวน " + dgvCremFund.Rows.Count.ToString();
        }
        #endregion


        #region Coop Loan
        private void btnCoopTemplate_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                string sourceFilePath = Application.StartupPath + "\\TEMPLATE\\TMP_COOPLOAN.xlsx";

                saveFileDialog.Title = "Save File";
                saveFileDialog.FileName = "CoopLoan_Template.xlsx";
                saveFileDialog.Filter = "Excel Files|*.xlsx"; // Tissue the file filters

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string destinationFilePath = saveFileDialog.FileName;

                    try
                    {
                        // Copy the file from the source to the destination
                        File.Copy(sourceFilePath, destinationFilePath);

                        MessageBox.Show("Download successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while copying the file: " + ex.Message);
                    }
                }
            }
        }

        private void btnCoopReNew_Click(object sender, EventArgs e)
        {
            txtCoop.Text = "";
            dgvCoop.Rows.Clear();
        }

        private void btnCoopBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog flDrg = new OpenFileDialog();
            flDrg.Filter = "Excel File|*.xlsx";

            if (flDrg.ShowDialog(this) != DialogResult.Cancel)
            {
                txtCoop.Text = flDrg.FileName;
            }
        }

        private void btnCoopSave_Click(object sender, EventArgs e)
        {
            // path to your excel file
            string path = txtCoop.Text;

            DataTable dtData = ReadExcelFile(path);

            DataTable dtDataCoop = new DataTable();            
            dtDataCoop.Columns.Add("code", typeof(string));
            dtDataCoop.Columns.Add("fname", typeof(string));
            dtDataCoop.Columns.Add("regis_date", typeof(DateTime));
            dtDataCoop.Columns.Add("coop_amount", typeof(decimal));
            dtDataCoop.Columns.Add("coop_deduct", typeof(decimal));
            dtDataCoop.Columns.Add("coop_loan", typeof(decimal));
            dtDataCoop.Columns.Add("resign_date", typeof(DateTime));
            dtDataCoop.Columns.Add("coop_status", typeof(string));


            dgvCoop.Rows.Clear();
            bool _error = false;
            string _errStr = "";
            if (dtData.Rows.Count > 0)
            {
                // loop through the worksheet rows and columns
                int i = 0;
                foreach (DataRow dr in dtData.Rows)
                {
                    string _code = "";                   
                    string _fname = "";
                    string _regis_date = "";
                    string _coop_amount = "";
                    string _coop_deduct = "";
                    string _coop_loan = "";
                    string _resign_date = "";
                    string _coop_status = "";

                    
                    try { _code = dr[0].ToString(); } catch { }
                    try { _fname = dr[1].ToString(); } catch { }
                    try { _regis_date = dr[2].ToString(); } catch { }
                    try { _coop_amount = dr[3].ToString(); } catch { }
                    try { _coop_deduct = dr[4].ToString(); } catch { }
                    try { _coop_loan = dr[5].ToString(); } catch { }
                    try { _resign_date = dr[6].ToString(); } catch { }
                    try { _coop_status = dr[7].ToString(); } catch { }


                    decimal CoopAmount = 0;
                    decimal CoopDeduct = 0;
                    decimal CoopLoan = 0;
                    DateTime RegisDate = new DateTime(1900, 1, 1);
                    DateTime ResignDate = new DateTime(1900, 1, 1);

                    if (_code != "" && (_coop_status == "NEW" || _coop_status == "UPDATE" || _coop_status == "RESIGN" || _coop_status == "LOAN"))
                    {
                        if (_code.Length == 2)
                        {
                            _code = "000" + _code;
                        }
                        //****** Get & Check Employee *******
                        DataTable dtEmp = new DataTable();
                        string strEmp = @"SELECT [CODE],CONCAT([PREN],[NAME],' ',[SURN]) Fname, POSIT FROM Employee WHERE Code=@Code ";
                        SqlCommand cmdEmp = new SqlCommand();
                        cmdEmp.CommandText = strEmp;
                        cmdEmp.Parameters.Add(new SqlParameter("@Code", _code));
                        dtEmp = oConHRM.Query(cmdEmp);


                        //**** Check Employee ****
                        if (dtEmp.Rows.Count > 0)
                        {

                            if(_coop_status == "NEW")
                            {
                                RegisDate = ConvertDate(_regis_date);
                                if (RegisDate.Year < 2000)
                                {
                                    _errStr += _code + " : error วันที่สมัคร รูปแบบไม่ถูกต้อง." + System.Environment.NewLine;
                                    _error = true;
                                }

                                try
                                {
                                    CoopAmount = Math.Round(Convert.ToDecimal(_coop_amount),2);
                                }
                                catch
                                {
                                    _errStr += _code + " : error จำนวนหุ้น" + System.Environment.NewLine;
                                    _error = true;
                                }

                                try
                                {
                                    CoopDeduct = Math.Round(Convert.ToDecimal(_coop_deduct),2);
                                }
                                catch
                                {
                                    _errStr += _code + " : error จำนวนเงิน." + System.Environment.NewLine;
                                    _error = true;
                                }

                                try
                                {
                                    if ((CoopAmount * 10) != CoopDeduct)
                                    {
                                        _errStr += _code + " : error จำนวนหุ้น และ จำนวนเงิน ไม่ถูกต้อง" + System.Environment.NewLine;
                                        _error = true;
                                    }
                                }
                                catch {
                                    _errStr += _code + " : errors จำนวนหุ้น และ จำนวนเงิน ไม่ถูกต้อง" + System.Environment.NewLine;
                                    _error = true;
                                }
                                

                            }
                            else if(_coop_status == "UPDATE")
                            {
                                try
                                {
                                    CoopAmount = Math.Round(Convert.ToDecimal(_coop_amount),2);
                                }
                                catch
                                {
                                    _errStr += _code + " : error จำนวนหุ้น" + System.Environment.NewLine;
                                    _error = true;
                                }

                                try
                                {
                                    CoopDeduct = Math.Round(Convert.ToDecimal(_coop_deduct),2);
                                }
                                catch
                                {
                                    _errStr += _code + " : error จำนวนเงิน." + System.Environment.NewLine;
                                    _error = true;
                                }

                                try
                                {
                                    if ((CoopAmount * 10) != CoopDeduct)
                                    {
                                        _errStr += _code + " : error จำนวนหุ้น และ จำนวนเงิน ไม่ถูกต้อง" + System.Environment.NewLine;
                                        _error = true;
                                    }
                                }
                                catch
                                {
                                    _errStr += _code + " : errors จำนวนหุ้น และ จำนวนเงิน ไม่ถูกต้อง" + System.Environment.NewLine;
                                    _error = true;
                                }

                            }
                            else if (_coop_status == "LOAN")
                            {
                                try
                                {
                                    CoopLoan = Math.Round(Convert.ToDecimal(_coop_loan),2);
                                }
                                catch
                                {
                                    _errStr += _code + " : error เงินกู้." + System.Environment.NewLine;
                                    _error = true;
                                }


                            }
                            else if (_coop_status == "RESIGN")
                            {
                                ResignDate = ConvertDate(_resign_date);
                                if (ResignDate.Year < 2000)
                                {
                                    _errStr += _code + " : error วันที่ลาออก รูปแบบไม่ถูกต้อง." + System.Environment.NewLine;
                                    _error = true;
                                }


                            }


                            if (!_error)
                            {
                                dtDataCoop.Rows.Add( _code, _fname, RegisDate, CoopAmount, CoopDeduct, CoopLoan, ResignDate, _coop_status);
                            }
                        }
                        else
                        {
                            _errStr += _code + " : error no Emp Code." + System.Environment.NewLine;
                            _error = true;
                        }//*** end Check Employee ***


                    } // check have data


                    i++;
                } // end loop for




                //*** Error ***
                if (!_error && dtDataCoop.Rows.Count > 0)
                {
                    List<string> oEmp = new List<string>();




                    //****** ReSet Loan To Zero *******
                    DataRow[] drLoanCnt =  dtDataCoop.Select(" coop_status='LOAN' ");

                    if(drLoanCnt.Length > 0)
                    {
                        string sEmpLoan = (rdCoopDCI.Checked) ? "DCI" : "SubContact";
                        DialogResult dlg = MessageBox.Show("ตรวจพบข้อมูล Loan ("+ sEmpLoan + "), กรุณายืนยันการเคลียร์ข้อมูลเดิม?", "ยืนยันการเคลียร์ข้อมูล Loan", MessageBoxButtons.YesNo);
                        if(dlg == DialogResult.Yes)
                        {
                            //****** ReSet Loan To Zero *******
                            if (rdCoopDCI.Checked)
                            {
                                
                                string strUpdZero = @"UPDATE EMPM SET COOPLOAN='0' WHERE RESIGN IS NULL  ";
                                OracleCommand cmdUpdZero = new OracleCommand();
                                cmdUpdZero.CommandText = strUpdZero;
                                oOraDCI.ExecuteCommand(cmdUpdZero);
                            }
                            else if (rdCoopSUB.Checked)
                            {
                                string strUpdZero = @"UPDATE EMPM SET COOPLOAN='0' WHERE RESIGN IS NULL  ";
                                OracleCommand cmdUpdZero = new OracleCommand();
                                cmdUpdZero.CommandText = strUpdZero;
                                oOraSUB.ExecuteCommand(cmdUpdZero);

                            }
                        }
                    }
                    //****** ReSet Loan To Zero *******





                    // loop for
                    foreach (DataRow dr in dtDataCoop.Rows)
                    {
                        DateTime _RegisDate = new DateTime(1900, 1, 1);
                        DateTime _ResignDate = new DateTime(1900, 1, 1);
                        string _code = "";
                        string _fname = "";
                        decimal _CoopAmount = 0;
                        decimal _CoopDeduct = 0;
                        decimal _CoopLoan = 0;
                        string _coop_status = "";


                        try { _RegisDate = Convert.ToDateTime(dr["regis_date"].ToString()); } catch { }
                        try { _ResignDate = Convert.ToDateTime(dr["resign_date"].ToString()); } catch { }
                        try { _code = dr["code"].ToString(); } catch { }
                        try { _fname = dr["fname"].ToString(); } catch { }
                        try { _CoopAmount = Math.Round(Convert.ToDecimal(dr["coop_amount"].ToString()),2); } catch { }
                        try { _CoopDeduct = Math.Round(Convert.ToDecimal(dr["coop_deduct"].ToString()),2); } catch { }
                        try { _CoopLoan = Math.Round(Convert.ToDecimal(dr["coop_loan"].ToString()),2); } catch { }
                        try { _coop_status = dr["coop_status"].ToString(); } catch { }

                        string strUpd = "";
                        if (_coop_status== "NEW")
                        {
                            strUpd = @"UPDATE EMPM SET  COOP_DATE = '" + _RegisDate.ToString("dd/MMM/yyyy") + "', COOP_AMOUNT = '" + _CoopAmount.ToString() + "', COOP_DEDUCT = '" + _CoopDeduct.ToString() + "', COOP_TERM = '' WHERE CODE = '" + _code + "'  ";                        
                        }
                        else if (_coop_status == "UPDATE")
                        {
                            strUpd = @"UPDATE EMPM SET  COOP_AMOUNT = '" + _CoopAmount.ToString() + "', COOP_DEDUCT = '" + _CoopDeduct.ToString() + "', COOP_TERM = '' WHERE CODE = '" + _code + "'  ";
                        }
                        else if (_coop_status == "RESIGN")
                        {
                            strUpd = @"UPDATE EMPM SET COOP_TERM = '" + _ResignDate.ToString("dd/MMM/yyyy") + "'  WHERE CODE = '" + _code + "'  ";
                        }
                        else if (_coop_status == "LOAN")
                        {                           
                            strUpd = @"UPDATE EMPM SET COOPLOAN='" + _CoopLoan.ToString() + "'  WHERE CODE = '" + _code + "'  ";
                        }


                        if (strUpd != "")
                        {
                            oEmp.Add(_code);

                            OracleCommand cmdUpd = new OracleCommand();
                            cmdUpd.CommandText = strUpd;

                            if (_code.StartsWith("I"))
                            {
                                oOraSUB.ExecuteCommand(cmdUpd);
                            }
                            else if (_code.StartsWith("7"))
                            {

                            }
                            else
                            {                                
                                oOraDCI.ExecuteCommand(cmdUpd);
                            }
                            
                        }
                        
                    }// end foreach


                    DataTable dtSel = new DataTable();
                    string strSel = @"SELECT CODE, COOP_DATE, COOP_AMOUNT, COOP_DEDUCT, COOPLOAN,COOP_TERM  FROM (
                                            SELECT CODE, COOP_DATE, COOP_AMOUNT, COOP_DEDUCT, COOPLOAN,COOP_TERM  FROM DCI.EMPM 
                                                UNION ALL
                                            SELECT CODE, COOP_DATE, COOP_AMOUNT, COOP_DEDUCT, COOPLOAN,COOP_TERM  FROM DCITC.EMPM
                                        )  ";
                    OracleCommand cmdSel = new OracleCommand();
                    cmdSel.CommandText = strSel;
                    dtSel = oOraDCI.Query(cmdSel);

                    dgvCoop.Rows.Clear();
                    if (dtSel.Rows.Count > 0)
                    {
                        decimal _stock = 0, _money = 0, _loan = 0;
                        foreach (DataRow drSel in dtSel.Rows)
                        {
                            if(oEmp.IndexOf(drSel["CODE"].ToString()) > -1)
                            {
                                try { _stock += Math.Round(Convert.ToDecimal(drSel["COOP_AMOUNT"].ToString()),2); } catch { }
                                try { _money += Math.Round(Convert.ToDecimal(drSel["COOP_DEDUCT"].ToString()),2); } catch { }
                                try { _loan += Math.Round(Convert.ToDecimal(drSel["COOPLOAN"].ToString()),2); } catch { }
                                dgvCoop.Rows.Add(drSel["CODE"].ToString(), drSel["COOP_DATE"].ToString(), drSel["COOP_AMOUNT"].ToString(), drSel["COOP_DEDUCT"].ToString(), drSel["COOPLOAN"].ToString(), drSel["COOP_TERM"].ToString());
                            }
                            
                        }


                        dgvCoop.Rows.Add("TOTAL", "", _stock.ToString(), _money.ToString(), _loan.ToString(), "");
                    }


                    MessageBox.Show("Successful!");

                } // end check error
                else
                {
                    MessageBox.Show(_errStr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }


            }

            lblCoop.Text = "จำนวน " + dgvCoop.Rows.Count.ToString();
        }

        #endregion



        #region A400
        private void btnA400Template_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                string sourceFilePath = Application.StartupPath + "\\TEMPLATE\\TMP_A400.xlsx";

                saveFileDialog.Title = "Save File";
                saveFileDialog.FileName = "A400_Template.xlsx";
                saveFileDialog.Filter = "Excel Files|*.xlsx"; // Tissue the file filters

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string destinationFilePath = saveFileDialog.FileName;

                    try
                    {
                        // Copy the file from the source to the destination
                        File.Copy(sourceFilePath, destinationFilePath);

                        MessageBox.Show("Download successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while copying the file: " + ex.Message);
                    }
                }
            }
        }

        private void btnA400ReNew_Click(object sender, EventArgs e)
        {
            txtA400.Text = "";
            dgvA400.Rows.Clear();
        }

        private void btnA400Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog flDrg = new OpenFileDialog();
            flDrg.Filter = "Excel File|*.xlsx";

            if (flDrg.ShowDialog(this) != DialogResult.Cancel)
            {
                txtA400.Text = flDrg.FileName;
            }
        }

        private void btnA400Save_Click(object sender, EventArgs e)
        {
            // path to your excel file
            string path = txtA400.Text;

            DataTable dtData = ReadExcelFile(path);

            DataTable dtDataA400 = new DataTable();
            dtDataA400.Columns.Add("pdate", typeof(DateTime));
            dtDataA400.Columns.Add("code", typeof(string));
            dtDataA400.Columns.Add("fname", typeof(string));
            dtDataA400.Columns.Add("money", typeof(decimal));


            dgvA400.Rows.Clear();
            bool _error = false;
            string _errStr = "", _pdateRcd = "";
            if (dtData.Rows.Count > 0)
            {
                // loop through the worksheet rows and columns
                int i = 0;
                foreach (DataRow dr in dtData.Rows)
                {
                    string _code = "";
                    string _pdate = "";
                    string _fname = "";
                    string _money = "";

                    try { _pdate = dr[0].ToString(); } catch { }
                    try { _code = dr[1].ToString(); } catch { }
                    try { _fname = dr[2].ToString(); } catch { }
                    try { _money = dr[3].ToString(); } catch { }


                    //**** Check Pdate ****
                    if (i == 0)
                    {
                        _pdateRcd = _pdate;
                    }
                    if (_pdate != _pdateRcd && i > 0)
                    {
                        _errStr += _code + " : error no PDate not match." + System.Environment.NewLine;
                        _error = true;
                    }




                    decimal Money = 0;
                    DateTime PDate = new DateTime(1900, 1, 1);

                    if (_code != "" && _pdate != "" && _money != "")
                    {
                        if (_code.Length == 2)
                        {
                            _code = "000" + _code;
                        }
                        //****** Get & Check Employee *******
                        DataTable dtEmp = new DataTable();
                        string strEmp = @"SELECT [CODE],CONCAT([PREN],[NAME],' ',[SURN]) Fname, POSIT FROM Employee WHERE Code=@Code ";
                        SqlCommand cmdEmp = new SqlCommand();
                        cmdEmp.CommandText = strEmp;
                        cmdEmp.Parameters.Add(new SqlParameter("@Code", _code));
                        dtEmp = oConHRM.Query(cmdEmp);


                        //**** Check Employee ****
                        if (dtEmp.Rows.Count > 0)
                        {
                            try
                            {
                                Money = Math.Round(Convert.ToDecimal(_money),2);
                            }
                            catch
                            {
                                _errStr += _code + " : error Money." + System.Environment.NewLine;
                                _error = true;
                            }


                            PDate = ConvertDate(_pdate);
                            if (PDate.Year < 2000)
                            {
                                _errStr += _code + " : error no PDate wrong format." + System.Environment.NewLine;
                                _error = true;
                            }

                            if (!_error)
                            {
                                dtDataA400.Rows.Add(PDate, _code, _fname, Money);
                            }
                        }
                        else
                        {
                            _errStr += _code + " : error no Emp Code." + System.Environment.NewLine;
                            _error = true;
                        }//*** end Check Employee ***


                    } // check have data


                    i++;
                } // end loop for




                //*** Error ***
                if (!_error && dtDataA400.Rows.Count > 0)
                {
                    DateTime PDate = new DateTime(1900, 1, 1);
                    PDate = Convert.ToDateTime(dtDataA400.Rows[0]["pdate"].ToString());

                    List<string> oEmp = new List<string>();


                    // loop for
                    foreach (DataRow dr in dtDataA400.Rows)
                    {
                        DateTime _PDate = new DateTime(1900, 1, 1);
                        string _code = "";
                        string _fname = "";
                        decimal _money = 0;


                        try { _PDate = Convert.ToDateTime(dr["pdate"].ToString()); } catch { }
                        try { _code = dr["code"].ToString(); } catch { }
                        try { _fname = dr["fname"].ToString(); } catch { }
                        try { _money = Math.Round(Convert.ToDecimal(dr["money"].ToString()),2); } catch { }

                        if (_code != "" && _money > 0)
                        {
                            oEmp.Add(_code);

                            string strUpd = @"UPDATE PRAJ SET A400 = '" + _money.ToString() + @"' 
                                                    WHERE CODE = '" + _code + @"' AND ADJ = '0' 
                                            AND  PDATE = '" + _PDate.ToString("dd/MMM/yyyy") + "'  ";
                            OracleCommand cmdUpd = new OracleCommand();
                            cmdUpd.CommandText = strUpd;


                            if (_code.StartsWith("7"))
                            {
                                oOraTRN.ExecuteCommand(cmdUpd);
                            }
                            else if (_code.StartsWith("I"))
                            {
                                oOraSUB.ExecuteCommand(cmdUpd);
                            }
                            else
                            {
                                oOraDCI.ExecuteCommand(cmdUpd);
                            }

                        } // end if check




                    }// end foreach



                    DataTable dtSel = new DataTable();
                    string strSel = @"SELECT CODE, PDATE, A400 FROM(
                                        SELECT CODE, PDATE, A400 FROM DCI.PRAJ WHERE PDATE = '" + PDate.ToString("dd/MMM/yyyy") + @"' AND ADJ='0'
                                            UNION ALL
                                        SELECT CODE, PDATE, A400 FROM DCITC.PRAJ WHERE PDATE = '" + PDate.ToString("dd/MMM/yyyy") + @"' AND ADJ='0'
                                            UNION ALL
                                        SELECT CODE, PDATE, A400 FROM DEV_OFFICE.PRAJ WHERE PDATE = '" + PDate.ToString("dd/MMM/yyyy") + @"' AND ADJ='0'  
                                    )
                                       ";
                    OracleCommand cmdSel = new OracleCommand();
                    cmdSel.CommandText = strSel;
                    dtSel = oOraDCI.Query(cmdSel);

                    dgvA400.Rows.Clear();
                    if (dtSel.Rows.Count > 0)
                    {
                        decimal _money = 0;
                        foreach (DataRow drSel in dtSel.Rows)
                        {
                            if (oEmp.IndexOf(drSel["CODE"].ToString()) > -1)
                            {
                                _money += Math.Round(Convert.ToDecimal(drSel["A400"].ToString()),2);
                                dgvA400.Rows.Add(drSel["PDATE"].ToString(), drSel["CODE"].ToString(), drSel["A400"].ToString());
                            }
                                
                        }

                        //**** Total ****
                        dgvA400.Rows.Add("TOTAL", "", _money.ToString());
                    }





                    MessageBox.Show("Successful!");


                } // end check error
                else
                {
                    MessageBox.Show(_errStr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }


            }

            lblA400.Text = "จำนวน " + dgvA400.Rows.Count.ToString();
        }


        #endregion



        #region Salary
        private void btnSalaryTemplate_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                string sourceFilePath = Application.StartupPath + "\\TEMPLATE\\TMP_PRSAL.xlsx";

                saveFileDialog.Title = "Save File";
                saveFileDialog.FileName = "Salary_Template.xlsx";
                saveFileDialog.Filter = "Excel Files|*.xlsx"; // Tissue the file filters

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string destinationFilePath = saveFileDialog.FileName;

                    try
                    {
                        // Copy the file from the source to the destination
                        File.Copy(sourceFilePath, destinationFilePath);

                        MessageBox.Show("Download successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while copying the file: " + ex.Message);
                    }
                }
            }
        }

        private void btnSalaryReNew_Click(object sender, EventArgs e)
        {
            txtSalaryAtt.Text = "";
            dgvSalary.Rows.Clear();
        }

        private void btnSalaryBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog flDrg = new OpenFileDialog();
            flDrg.Filter = "Excel File|*.xlsx";

            if (flDrg.ShowDialog(this) != DialogResult.Cancel)
            {
                txtSalaryAtt.Text = flDrg.FileName;
            }
        }

        private void btnSalarySave_Click(object sender, EventArgs e)
        {
            // path to your excel file
            string path = txtSalaryAtt.Text;

            DataTable dtData = ReadExcelFile(path);

            DataTable dtDataSalary = new DataTable();
            dtDataSalary.Columns.Add("pdate", typeof(DateTime));
            dtDataSalary.Columns.Add("code", typeof(string));
            dtDataSalary.Columns.Add("wtype", typeof(string));
            dtDataSalary.Columns.Add("fname", typeof(string));
            dtDataSalary.Columns.Add("salary1", typeof(decimal));
            dtDataSalary.Columns.Add("salary2", typeof(decimal));
            dtDataSalary.Columns.Add("housing1", typeof(decimal));
            dtDataSalary.Columns.Add("housing2", typeof(decimal));
            dtDataSalary.Columns.Add("calot1", typeof(decimal));
            dtDataSalary.Columns.Add("calot2", typeof(decimal));


            dgvSalary.Rows.Clear();
            bool _error = false;
            string _errStr = "", _pdateRcd = "";
            if (dtData.Rows.Count > 0)
            {
                // loop through the worksheet rows and columns
                int i = 0;
                foreach (DataRow dr in dtData.Rows)
                {
                    string _pdate = "";
                    string _code = "";
                    string _wtype = "";
                    string _fname = "";
                    string _salary1 = "";
                    string _salary2 = "";
                    string _housing1 = "";
                    string _housing2 = "";
                    string _calot1 = "";
                    string _calot2 = "";

                    try { _pdate = dr[0].ToString(); } catch { }
                    try { _code = dr[1].ToString(); } catch { }
                    try { _wtype = dr[2].ToString(); } catch { }
                    try { _fname = dr[3].ToString(); } catch { }
                    try { _salary1 = dr[4].ToString(); } catch { }
                    try { _salary2 = dr[5].ToString(); } catch { }
                    try { _housing1 = dr[6].ToString(); } catch { }
                    try { _housing2 = dr[7].ToString(); } catch { }
                    try { _calot1 = dr[8].ToString(); } catch { }
                    try { _calot2 = dr[9].ToString(); } catch { }


                    //**** Check Pdate ****
                    if (i == 0)
                    {
                        _pdateRcd = _pdate;
                    }
                    if (_pdate != _pdateRcd && i > 0)
                    {
                        _errStr += _code + " : error no PDate not match." + System.Environment.NewLine;
                        _error = true;
                    }


                    decimal Salary1 = 0, Salary2 =0, Calot1=0, Calot2=0, Housing1 =0, Housing2 =0;
                    DateTime PDate = new DateTime(1900, 1, 1);

                    if (_code != "" && _wtype != "" && _salary1 != "" && _salary2 != "" && _calot1 != "" && _calot2 != "")
                    {
                        if (_code.Length == 2)
                        {
                            _code = "000" + _code;
                        }
                        //****** Get & Check Employee *******
                        DataTable dtEmp = new DataTable();
                        string strEmp = @"SELECT [CODE],CONCAT([PREN],[NAME],' ',[SURN]) Fname, POSIT FROM Employee WHERE Code=@Code ";
                        SqlCommand cmdEmp = new SqlCommand();
                        cmdEmp.CommandText = strEmp;
                        cmdEmp.Parameters.Add(new SqlParameter("@Code", _code));
                        dtEmp = oConHRM.Query(cmdEmp);


                        //**** Check Employee ****
                        if (dtEmp.Rows.Count > 0)
                        {
                            PDate = ConvertDate(_pdate);
                            if (PDate.Year < 2000)
                            {
                                _errStr += _code + " : error no PDate wrong format." + System.Environment.NewLine;
                                _error = true;
                            }


                            if (_wtype=="O" || _wtype == "S")
                            {

                            }
                            else
                            {
                                _errStr += _code + " : error WTYPE." + System.Environment.NewLine;
                                _error = true;
                            }


                            try
                            {
                                Salary1 = Math.Round(Convert.ToDecimal(_salary1),2);
                            }
                            catch
                            {
                                _errStr += _code + " : error Salary 1." + System.Environment.NewLine;
                                _error = true;
                            }


                            try
                            {
                                Salary2 = Math.Round(Convert.ToDecimal(_salary2),2);
                            }
                            catch
                            {
                                _errStr += _code + " : error Salary 2." + System.Environment.NewLine;
                                _error = true;
                            }

                            try
                            {
                                Calot1 = Math.Round(Convert.ToDecimal(_calot1),2);
                            }
                            catch
                            {
                                _errStr += _code + " : error Cal OT 1." + System.Environment.NewLine;
                                _error = true;
                            }

                            try
                            {
                                Calot2 = Math.Round(Convert.ToDecimal(_calot2),2);
                            }
                            catch
                            {
                                _errStr += _code + " : error Cal OT 2." + System.Environment.NewLine;
                                _error = true;
                            }

                            try { Housing1 = Math.Round(Convert.ToDecimal(_housing1),2); } catch { }
                            try { Housing2 = Math.Round(Convert.ToDecimal(_housing2),2); } catch { }



                            if (!_error)
                            {
                                dtDataSalary.Rows.Add(PDate, _code, _wtype, _fname, Salary1, Salary2, Housing1, Housing2, Calot1, Calot2);
                            }
                        }
                        else
                        {
                            _errStr += _code + " : error no Emp Code." + System.Environment.NewLine;
                            _error = true;
                        }//*** end Check Employee ***


                    } // check have data


                    i++;
                } // end loop for


                //*** Error ***
                if (!_error && dtDataSalary.Rows.Count > 0)
                {
                    List<string> oEmp = new List<string>();

                    DateTime PDate = new DateTime(1900, 1, 1);
                    PDate = Convert.ToDateTime(dtDataSalary.Rows[0]["pdate"].ToString());

                    // loop for
                    foreach (DataRow dr in dtDataSalary.Rows)
                    {
                        DateTime _PDate = new DateTime(1900, 1, 1);
                        string _code = "";
                        string _wtype = "";
                        string _fname = "";
                        decimal _salary1 = 0;
                        decimal _salary2 = 0;
                        decimal _housing1 = 0;
                        decimal _housing2 = 0;
                        decimal _calot1 = 0;
                        decimal _calot2 = 0;


                        try { _PDate = Convert.ToDateTime(dr["pdate"].ToString()); } catch { }
                        try { _code = dr["code"].ToString(); } catch { }
                        try { _wtype = dr["wtype"].ToString(); } catch { }
                        try { _fname = dr["fname"].ToString(); } catch { }
                        try { _salary1 = Math.Round(Convert.ToDecimal(dr["salary1"].ToString()),2); } catch { }
                        try { _salary2 = Math.Round(Convert.ToDecimal(dr["salary2"].ToString()), 2); } catch { }
                        try { _housing1 = Math.Round(Convert.ToDecimal(dr["housing1"].ToString()), 2); } catch { }
                        try { _housing2 = Math.Round(Convert.ToDecimal(dr["housing2"].ToString()), 2); } catch { }
                        try { _calot1 = Math.Round(Convert.ToDecimal(dr["calot1"].ToString()), 2); } catch { }
                        try { _calot2 = Math.Round(Convert.ToDecimal(dr["calot2"].ToString()), 2); } catch { }

                        if (_code != "" && _wtype != "" && _salary1 > 0 && _salary2 > 0 && _calot1 > 0 && _calot2 > 0)
                        {
                            oEmp.Add(_code);

                            string _house1 = (_housing1 > 0) ? _housing1.ToString() : "";
                            string _house2 = (_housing2 > 0) ? _housing2.ToString() : "";

                            string strInstr = @"INSERT INTO PRSAL (PDATE, CODE, WTYPE, SALARY1, SALARY2, HOUSING1, HOUSING2, CALOT1, CALOT2) 
                                    VALUES ('"+ _PDate.ToString("dd/MMM/yyyy") + "', '"+ _code + "', '"+ _wtype + @"', 
                                        '"+ _salary1.ToString() + "', '"+ _salary2.ToString() + "', '"+ _house1 + @"', 
                                        '"+ _house2 + @"', '"+ _calot1.ToString() + @"', '"+ _calot2.ToString() + @"' )   ";
                            OracleCommand cmdInstr = new OracleCommand();
                            cmdInstr.CommandText = strInstr;

                            string strDel = @"DELETE FROM PRSAL WHERE PDATE='" + _PDate.ToString("dd/MMM/yyyy") + "' AND CODE='" + _code + "'  ";
                            OracleCommand cmdDel = new OracleCommand();
                            cmdDel.CommandText = strDel;

                            if (_code.StartsWith("7"))
                            {
                                oOraTRN.ExecuteCommand(cmdDel);
                                oOraTRN.ExecuteCommand(cmdInstr);
                            }
                            else if (_code.StartsWith("I"))
                            {
                                oOraSUB.ExecuteCommand(cmdDel);
                                oOraSUB.ExecuteCommand(cmdInstr);
                            }
                            else
                            {
                                oOraDCI.ExecuteCommand(cmdDel);
                                oOraDCI.ExecuteCommand(cmdInstr);
                            }

                        } // end if check




                    }// end foreach



                    DataTable dtSel = new DataTable();
                    string strSel = @"SELECT PDATE, CODE, WTYPE, SALARY1, SALARY2, HOUSING1, HOUSING2, CALOT1, CALOT2 FROM (
                                        SELECT  PDATE, CODE, WTYPE, SALARY1, SALARY2, HOUSING1, HOUSING2, CALOT1, CALOT2 
                                        FROM DCI.PRSAL WHERE PDATE = '"+ PDate.ToString("dd/MMM/yyyy") + @"'
                                            UNION ALL
                                        SELECT  PDATE, CODE, WTYPE, SALARY1, SALARY2, HOUSING1, HOUSING2, CALOT1, CALOT2 
                                        FROM DCITC.PRSAL WHERE PDATE = '"+ PDate.ToString("dd/MMM/yyyy") + @"'
                                            UNION ALL 
                                        SELECT  PDATE, CODE, WTYPE, SALARY1, SALARY2, HOUSING1, HOUSING2, CALOT1, CALOT2 
                                        FROM DEV_OFFICE.PRSAL WHERE PDATE = '"+ PDate.ToString("dd/MMM/yyyy") + @"'
                                    )   ";
                    OracleCommand cmdSel = new OracleCommand();
                    cmdSel.CommandText = strSel;
                    dtSel = oOraDCI.Query(cmdSel);

                    dgvSalary.Rows.Clear();
                    if (dtSel.Rows.Count > 0)
                    {
                        decimal sal1 = 0, sal2 = 0, hou1 = 0, hou2 = 0, cal1 = 0, cal2 = 0;
                        foreach (DataRow drSel in dtSel.Rows)
                        {
                            if (oEmp.IndexOf(drSel["CODE"].ToString()) > -1) { 

                                try { sal1 += Math.Round(Convert.ToDecimal(drSel["SALARY1"].ToString()),2); } catch { }
                                try { sal2 += Math.Round(Convert.ToDecimal(drSel["SALARY2"].ToString()), 2); } catch { }
                                try { hou1 += Math.Round(Convert.ToDecimal(drSel["HOUSING2"].ToString()), 2); } catch { }
                                try { hou2 += Math.Round(Convert.ToDecimal(drSel["HOUSING2"].ToString()), 2); } catch { }
                                try { cal1 += Math.Round(Convert.ToDecimal(drSel["CALOT1"].ToString()), 2); } catch { }
                                try { cal2 += Math.Round(Convert.ToDecimal(drSel["CALOT2"].ToString()), 2); } catch { }
                                dgvSalary.Rows.Add(drSel["PDATE"].ToString(), drSel["CODE"].ToString(), drSel["WTYPE"].ToString(),
                                    drSel["SALARY1"].ToString(), drSel["SALARY2"].ToString(), drSel["HOUSING1"].ToString(),
                                    drSel["HOUSING2"].ToString(), drSel["CALOT1"].ToString(), drSel["CALOT2"].ToString());
                            }
                        }

                        //**** Total ****
                        dgvSalary.Rows.Add("Total", "", "", sal1.ToString(), sal2.ToString(), hou1.ToString(), hou2.ToString(), cal1.ToString(), cal2.ToString() );
                    }





                    MessageBox.Show("Successful!");


                } // end check error
                else
                {
                    MessageBox.Show(_errStr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }


            }

            lblSalary.Text = "จำนวน " + dgvSalary.Rows.Count.ToString();
        }

        #endregion


        #region Adjust
        private void btnAdjustTemplate_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                string sourceFilePath = Application.StartupPath + "\\TEMPLATE\\TMP_ADJUST.xlsx";

                saveFileDialog.Title = "Save File";
                saveFileDialog.FileName = "Adjust_Template.xlsx";
                saveFileDialog.Filter = "Excel Files|*.xlsx"; // Tissue the file filters

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string destinationFilePath = saveFileDialog.FileName;

                    try
                    {
                        // Copy the file from the source to the destination
                        File.Copy(sourceFilePath, destinationFilePath);

                        MessageBox.Show("Download successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while copying the file: " + ex.Message);
                    }
                }
            }
        }

        private void btnAdjustReNew_Click(object sender, EventArgs e)
        {
            txtAdjust.Text = "";
            dgvAdjust.Rows.Clear();
        }

        private void btnAdjustBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog flDrg = new OpenFileDialog();
            flDrg.Filter = "Excel File|*.xlsx";

            if (flDrg.ShowDialog(this) != DialogResult.Cancel)
            {
                txtAdjust.Text = flDrg.FileName;
            }
        }

        private void btnAdjustSave_Click(object sender, EventArgs e)
        {
            // path to your excel file
            string path = txtAdjust.Text;

            DataTable dtData = ReadExcelFile(path);

            DataTable dtDataAdjust = new DataTable();
            dtDataAdjust.Columns.Add("pdate", typeof(DateTime));
            dtDataAdjust.Columns.Add("code", typeof(string));
            dtDataAdjust.Columns.Add("wtype", typeof(string));
            dtDataAdjust.Columns.Add("fname", typeof(string));
            dtDataAdjust.Columns.Add("ot", typeof(decimal));
            dtDataAdjust.Columns.Add("all", typeof(decimal));
            dtDataAdjust.Columns.Add("tran", typeof(decimal));
            dtDataAdjust.Columns.Add("shift", typeof(decimal));
            dtDataAdjust.Columns.Add("sal_all", typeof(decimal));
            dtDataAdjust.Columns.Add("teian", typeof(decimal));
            dtDataAdjust.Columns.Add("otfood", typeof(decimal));
            dtDataAdjust.Columns.Add("special_money", typeof(decimal));

            pnGrpAdjust.ValuesSecondary.Heading = "Processing Adjust : 0/" + dtData.Rows.Count.ToString();

            dgvAdjust.Rows.Clear();
            bool _error = false;
            string _errStr = "", _pdateRcd = "";
            if (dtData.Rows.Count > 0)
            {
                // loop through the worksheet rows and columns
                int i = 0;
                foreach (DataRow dr in dtData.Rows)
                {
                    string _pdate = "";
                    string _code = "";
                    string _wtype = "";
                    string _fname = "";
                    string _ot = "";
                    string _all = "";
                    string _tran = "";
                    string _shift = "";
                    string _sal_all = "";
                    string _teian = "";
                    string _otfood = "";
                    string _special_money = "";

                    try { _pdate = dr[0].ToString(); } catch { }
                    try { _code = dr[1].ToString(); } catch { }
                    try { _wtype = dr[2].ToString(); } catch { }
                    try { _fname = dr[3].ToString(); } catch { }
                    try { _ot = dr[4].ToString(); } catch { }
                    try { _all = dr[5].ToString(); } catch { }
                    try { _tran = dr[6].ToString(); } catch { }
                    try { _shift = dr[7].ToString(); } catch { }
                    try { _sal_all = dr[8].ToString(); } catch { }
                    try { _teian = dr[9].ToString(); } catch { }
                    try { _otfood = dr[10].ToString(); } catch { }
                    try { _special_money = dr[11].ToString(); } catch { }


                    //**** Check Pdate ****
                    if (i == 0)
                    {
                        _pdateRcd = _pdate;
                    }
                    if (_pdate != _pdateRcd && i > 0)
                    {
                        _errStr += _code + " : error no PDate not match." + System.Environment.NewLine;
                        _error = true;
                    }


                    decimal OT = 0, All = 0, Tran = 0, Shift = 0, SalAll = 0, Teian = 0, OTFood = 0, SpecialMoney = 0;
                    DateTime PDate = new DateTime(1900, 1, 1);

                    if (_code != "" && _wtype != "" )
                    {
                        if (_code.Length == 2)
                        {
                            _code = "000" + _code;
                        }
                        //****** Get & Check Employee *******
                        DataTable dtEmp = new DataTable();
                        string strEmp = @"SELECT [CODE],CONCAT([PREN],[NAME],' ',[SURN]) Fname, POSIT FROM Employee WHERE Code=@Code ";
                        SqlCommand cmdEmp = new SqlCommand();
                        cmdEmp.CommandText = strEmp;
                        cmdEmp.Parameters.Add(new SqlParameter("@Code", _code));
                        dtEmp = oConHRM.Query(cmdEmp);


                        //**** Check Employee ****
                        if (dtEmp.Rows.Count > 0)
                        {
                            PDate = ConvertDate(_pdate);
                            if (PDate.Year < 2000)
                            {
                                _errStr += _code + " : error no PDate wrong format." + System.Environment.NewLine;
                                _error = true;
                            }


                            if (_wtype == "O" || _wtype == "S")
                            {

                            }
                            else
                            {
                                _errStr += _code + " : error WTYPE." + System.Environment.NewLine;
                                _error = true;
                            }


                            try { OT = Math.Round(Convert.ToDecimal(_ot),2); } catch { }
                            try { All = Math.Round(Convert.ToDecimal(_all), 2); } catch { }
                            try { Tran = Math.Round(Convert.ToDecimal(_tran), 2); } catch { }
                            try { Shift = Math.Round(Convert.ToDecimal(_shift), 2); } catch { }
                            try { SalAll = Math.Round(Convert.ToDecimal(_sal_all), 2); } catch { }
                            try { Teian = Math.Round(Convert.ToDecimal(_teian), 2); } catch { }
                            try { OTFood = Math.Round(Convert.ToDecimal(_otfood), 2); } catch { }
                            try { SpecialMoney = Math.Round(Convert.ToDecimal(_special_money), 2); } catch { }

                            if (!_error)
                            {
                                dtDataAdjust.Rows.Add(PDate, _code, _wtype, _fname, OT, All, Tran, Shift, SalAll, Teian, OTFood, SpecialMoney);
                            }
                        }
                        else
                        {
                            _errStr += _code + " : error no Emp Code." + System.Environment.NewLine;
                            _error = true;
                        }//*** end Check Employee ***


                    } // check have data


                    i++;
                } // end loop for


                //*** Error ***
                if (!_error && dtDataAdjust.Rows.Count > 0)
                {


                    //*********************************************
                    //********   Group By Data & Sum  *************
                    DataTable dtGrpData = dtDataAdjust.AsEnumerable()
                        .GroupBy(g => new { pdate = g.Field<DateTime>("pdate"), code = g.Field<string>("code"), wtype = g.Field<string>("wtype"), fname = g.Field<string>("fname") })
                        .Select(s =>
                        {
                            DataRow drNew = dtDataAdjust.NewRow();
                            drNew["pdate"] = s.Key.pdate;
                            drNew["code"] = s.Key.code;
                            drNew["wtype"] = s.Key.wtype;
                            drNew["fname"] = s.Key.fname;
                            drNew["ot"] = s.Sum( d => d.Field<decimal>("ot"));
                            drNew["all"] = s.Sum(d => d.Field<decimal>("all"));
                            drNew["tran"] = s.Sum( d => d.Field<decimal>("tran"));
                            drNew["shift"] = s.Sum(d => d.Field<decimal>("shift"));
                            drNew["sal_all"] = s.Sum( d => d.Field<decimal>("sal_all"));
                            drNew["teian"] = s.Sum( d => d.Field<decimal>("teian"));
                            drNew["otfood"] = s.Sum( d => d.Field<decimal>("otfood"));
                            drNew["special_money"] = s.Sum( d=> d.Field<decimal>("special_money"));

                            return drNew;
                        }).CopyToDataTable();
                    //********   Group By Data & Sum  *************
                    //*********************************************

                    List<string> oEmp = new List<string>();

                    DateTime PDate = new DateTime(1900, 1, 1);
                    PDate = Convert.ToDateTime(dtGrpData.Rows[0]["pdate"].ToString());

                    int idx = 0;
                    // loop for
                    foreach (DataRow dr in dtGrpData.Rows)
                    {
                        DateTime _PDate = new DateTime(1900, 1, 1);
                        string _code = "";
                        string _wtype = "";
                        string _fname = "";
                        decimal _ot = 0;
                        decimal _all = 0;
                        decimal _tran = 0;
                        decimal _shift = 0;
                        decimal _sal_all = 0;
                        decimal _teian = 0;
                        decimal _otfood = 0;
                        decimal _special_money = 0;


                        try { _PDate = Convert.ToDateTime(dr["pdate"].ToString()); } catch { }
                        try { _code = dr["code"].ToString(); } catch { }
                        try { _wtype = dr["wtype"].ToString(); } catch { }
                        try { _fname = dr["fname"].ToString(); } catch { }
                        try { _ot = Math.Round(Convert.ToDecimal(dr["ot"].ToString()), 2); } catch { }
                        try { _all = Math.Round(Convert.ToDecimal(dr["all"].ToString()), 2); } catch { }
                        try { _tran = Math.Round(Convert.ToDecimal(dr["tran"].ToString()), 2); } catch { }
                        try { _shift = Math.Round(Convert.ToDecimal(dr["shift"].ToString()), 2); } catch { }
                        try { _sal_all = Math.Round(Convert.ToDecimal(dr["sal_all"].ToString()), 2); } catch { }
                        try { _teian = Math.Round(Convert.ToDecimal(dr["teian"].ToString()), 2); } catch { }
                        try { _otfood = Math.Round(Convert.ToDecimal(dr["otfood"].ToString()),2); } catch { }
                        try { _special_money = Math.Round(Convert.ToDecimal(dr["special_money"].ToString()), 2); } catch { }

                        if (_code != "" && _wtype != "" )
                        {
                            oEmp.Add(_code);

                            //*** Check Have Data ****
                            DataTable dtChk = new DataTable();
                            string strChk = @"SELECT * FROM PRAJ WHERE ADJ='1' AND CODE='"+ _code + "' AND PDATE = '"+ _PDate.ToString("dd/MMM/yyyy") + "' ";
                            OracleCommand cmdChk = new OracleCommand();
                            cmdChk.CommandText = strChk;
                            
                            if (_code.StartsWith("7"))
                            {
                                dtChk = oOraTRN.Query(cmdChk);
                            }
                            else if (_code.StartsWith("I"))
                            {
                                dtChk = oOraSUB.Query(cmdChk);
                            }
                            else
                            {
                                dtChk = oOraDCI.Query(cmdChk);
                            }



                            string strSQL = "";
                            //*** No Data => Insert ***
                            if(dtChk.Rows.Count == 0)
                            {
                                strSQL = @"INSERT INTO PRAJ (PDATE, WTYPE, CODE,  ADJ, ABDAY, WORK1,  WORK2, LOT1, LOT15,  LOT2, LOT3, COT1,  
                                                COT15, COT2, COT3,  OT, SALARY, ALLOW,  OTHINN, OTHDED, A400,  SHIFT, FOODSHT, FULL,  SUPER, 
                                                DVCD, POSIT,  ADBN, AD_FD, AD_FO,  AD_UN, AD_TP, AD_PZ, TRAN, A200, A400LEVEL) VALUES 
                                            ('"+ _PDate.ToString("dd/MMM/yyyy") + "', '"+ _wtype + "', '"+ _code + @"',  '1', '0', '0',  '0', 
                                                '0', '0', '0', '0', '0', '0', '0', '0', '"+_ot.ToString()+"', '"+_sal_all.ToString()+ @"', '0',
                                                '0', '0', '0', '"+_shift.ToString()+"', '"+ _otfood.ToString()+ @"', '0', '0', '0', '0',  '0', 
                                                '0', '0', '"+_teian.ToString()+"', '0', '"+_special_money.ToString()+"', '"+_tran.ToString()+@"', '0', '0'  )";

                            }
                            //*** Have Data => Update ***
                            else
                            {
                                strSQL = @"UPDATE PRAJ SET  OT='"+_ot.ToString()+"', SALARY='"+_sal_all.ToString()+"', SHIFT='"+_shift.ToString()+@"', 
                                                FOODSHT='"+ _otfood.ToString()+"', AD_UN='"+_teian.ToString()+"', AD_PZ='"+_special_money.ToString()+"', TRAN='"+_tran.ToString()+@"' 
                                           WHERE ADJ='1' AND CODE='"+ _code + "' AND PDATE = '"+ _PDate.ToString("dd/MMM/yyyy") + "'   ";
                            }
                            OracleCommand cmdSQL = new OracleCommand();
                            cmdSQL.CommandText = strSQL;

                            if (_code.StartsWith("7"))
                            {
                                oOraTRN.Query(cmdSQL);
                            }
                            else if (_code.StartsWith("I"))
                            {
                                oOraSUB.Query(cmdSQL);
                            }
                            else
                            {
                                oOraDCI.Query(cmdSQL);
                            }

                        } // end if check

                        idx++;
                        pnGrpAdjust.ValuesSecondary.Heading = "Processing : "+ idx.ToString() + "/" + dtData.Rows.Count.ToString();

                    }// end foreach



                    DataTable dtSel = new DataTable();
                    string strSel = @"SELECT PDATE, CODE, WTYPE, OT, SALARY, SHIFT, FOODSHT, AD_UN, AD_PZ, TRAN FROM (
                                        SELECT  PDATE, CODE, WTYPE, OT, SALARY, SHIFT, FOODSHT, AD_UN, AD_PZ, TRAN 
                                        FROM DCI.PRAJ  WHERE  ADJ='1' AND PDATE = '" + PDate.ToString("dd/MMM/yyyy") + @"'
                                            UNION ALL
                                        SELECT  PDATE, CODE, WTYPE, OT, SALARY, SHIFT, FOODSHT, AD_UN, AD_PZ, TRAN 
                                        FROM DCITC.PRAJ  WHERE ADJ='1' AND PDATE = '" + PDate.ToString("dd/MMM/yyyy") + @"'
                                            UNION ALL 
                                        SELECT  PDATE, CODE, WTYPE, OT, SALARY, SHIFT, FOODSHT, AD_UN, AD_PZ, TRAN 
                                        FROM DEV_OFFICE.PRAJ  WHERE ADJ='1' AND PDATE = '" + PDate.ToString("dd/MMM/yyyy") + @"'
                                    )     ";
                    OracleCommand cmdSel = new OracleCommand();
                    cmdSel.CommandText = strSel;
                    dtSel = oOraDCI.Query(cmdSel);

                    dgvAdjust.Rows.Clear();
                    if (dtSel.Rows.Count > 0)
                    {
                        decimal ot = 0, sal = 0, shf = 0, food = 0, ad_un = 0, ad_pz = 0, tran = 0;
                        foreach (DataRow drSel in dtSel.Rows)
                        {
                            if(oEmp.IndexOf(drSel["CODE"].ToString()) > -1)
                            {
                                try { ot += Math.Round(Convert.ToDecimal(drSel["OT"].ToString()),2); } catch { }
                                try { sal += Math.Round(Convert.ToDecimal(drSel["SALARY"].ToString()), 2); } catch { }
                                try { shf += Math.Round(Convert.ToDecimal(drSel["SHIFT"].ToString()), 2); } catch { }
                                try { food += Math.Round(Convert.ToDecimal(drSel["FOODSHT"].ToString()), 2); } catch { }
                                try { ad_un += Math.Round(Convert.ToDecimal(drSel["AD_UN"].ToString()), 2); } catch { }
                                try { ad_pz += Math.Round(Convert.ToDecimal(drSel["AD_PZ"].ToString()), 2); } catch { }
                                try { tran += Math.Round(Convert.ToDecimal(drSel["TRAN"].ToString()), 2); } catch { }
                                dgvAdjust.Rows.Add(drSel["PDATE"].ToString(), drSel["CODE"].ToString(), drSel["WTYPE"].ToString(),
                                    drSel["OT"].ToString(), drSel["SALARY"].ToString(), drSel["SHIFT"].ToString(),
                                    drSel["FOODSHT"].ToString(), drSel["AD_UN"].ToString(), drSel["AD_PZ"].ToString(), drSel["TRAN"].ToString());
                            }                            
                        }

                        //**** Total ****
                        dgvAdjust.Rows.Add("Total", "", "", ot.ToString(), sal.ToString(), shf.ToString(), food.ToString(), ad_un.ToString(), ad_pz.ToString(), tran.ToString());
                    }



                    pnGrpAdjust.ValuesSecondary.Heading = "Description";

                    MessageBox.Show("Successful!");


                } // end check error
                else
                {
                    MessageBox.Show(_errStr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }


            }

            lblAdjust.Text = "จำนวน " + dgvAdjust.Rows.Count.ToString();
        }




        #endregion


        #region Air
        private void btnAirTemplate_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                string sourceFilePath = Application.StartupPath + "\\TEMPLATE\\TMP_AIR.xlsx";

                saveFileDialog.Title = "Save File";
                saveFileDialog.FileName = "Air_Template.xlsx";
                saveFileDialog.Filter = "Excel Files|*.xlsx"; // Tissue the file filters

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string destinationFilePath = saveFileDialog.FileName;

                    try
                    {
                        // Copy the file from the source to the destination
                        File.Copy(sourceFilePath, destinationFilePath);

                        MessageBox.Show("Download successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while copying the file: " + ex.Message);
                    }
                }
            }
        }

        private void btnAirReNew_Click(object sender, EventArgs e)
        {
            txtAir.Text = "";
            dgvAir.Rows.Clear();
        }

        private void btnAirBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog flDrg = new OpenFileDialog();
            flDrg.Filter = "Excel File|*.xlsx";

            if (flDrg.ShowDialog(this) != DialogResult.Cancel)
            {
                txtAir.Text = flDrg.FileName;
            }
        }

        private void btnAirSave_Click(object sender, EventArgs e)
        {
            // path to your excel file
            string path = txtAir.Text;

            DataTable dtData = ReadExcelFile(path);

            DataTable dtDataAir = new DataTable();
            dtDataAir.Columns.Add("pdate", typeof(DateTime));
            dtDataAir.Columns.Add("code", typeof(string));
            dtDataAir.Columns.Add("wtype", typeof(string));
            dtDataAir.Columns.Add("fname", typeof(string));
            dtDataAir.Columns.Add("air", typeof(decimal));

            dgvAir.Rows.Clear();
            bool _error = false;
            string _errStr = "", _pdateRcd = "";
            if (dtData.Rows.Count > 0)
            {
                // loop through the worksheet rows and columns
                int i = 0;
                foreach (DataRow dr in dtData.Rows)
                {
                    string _pdate = "";
                    string _code = "";
                    string _wtype = "";
                    string _fname = "";
                    string _air = "";

                    try { _pdate = dr[0].ToString(); } catch { }
                    try { _code = dr[1].ToString(); } catch { }
                    try { _wtype = dr[2].ToString(); } catch { }
                    try { _fname = dr[3].ToString(); } catch { }
                    try { _air = dr[4].ToString(); } catch { }


                    //**** Check Pdate ****
                    if (i == 0)
                    {
                        _pdateRcd = _pdate;
                    }
                    if (_pdate != _pdateRcd && i > 0)
                    {
                        _errStr += _code + " : error no PDate not match." + System.Environment.NewLine;
                        _error = true;
                    }


                    decimal AIR = 0;
                    DateTime PDate = new DateTime(1900, 1, 1);

                    if (_code != "" && _wtype != "")
                    {
                        if (_code.Length == 2)
                        {
                            _code = "000" + _code;
                        }
                        //****** Get & Check Employee *******
                        DataTable dtEmp = new DataTable();
                        string strEmp = @"SELECT [CODE],CONCAT([PREN],[NAME],' ',[SURN]) Fname, POSIT FROM Employee WHERE Code=@Code ";
                        SqlCommand cmdEmp = new SqlCommand();
                        cmdEmp.CommandText = strEmp;
                        cmdEmp.Parameters.Add(new SqlParameter("@Code", _code));
                        dtEmp = oConHRM.Query(cmdEmp);


                        //**** Check Employee ****
                        if (dtEmp.Rows.Count > 0)
                        {
                            PDate = ConvertDate(_pdate);
                            if (PDate.Year < 2000)
                            {
                                _errStr += _code + " : error no PDate wrong format." + System.Environment.NewLine;
                                _error = true;
                            }


                            if (_wtype == "O" || _wtype == "S")
                            {

                            }
                            else
                            {
                                _errStr += _code + " : error WTYPE." + System.Environment.NewLine;
                                _error = true;
                            }


                            try { AIR = Math.Round(Convert.ToDecimal(_air),2); } catch { }

                            if (!_error)
                            {
                                dtDataAir.Rows.Add(PDate, _code, _wtype, _fname, AIR);
                            }
                        }
                        else
                        {
                            _errStr += _code + " : error no Emp Code." + System.Environment.NewLine;
                            _error = true;
                        }//*** end Check Employee ***


                    } // check have data


                    i++;
                } // end loop for


                //*** Error ***
                if (!_error && dtDataAir.Rows.Count > 0)
                {

                    //***************************************
                    //********* Group By DataTable **********
                    DataTable dtGrpData = dtDataAir.AsEnumerable()
                    .GroupBy(r => new { pdate = r.Field<DateTime>("pdate"), code = r.Field<string>("code"), wtype = r.Field<string>("wtype"), fname = r.Field<string>("fname") })
                    .Select(g =>
                    {
                        DataRow row = dtDataAir.NewRow();
                        row["pdate"] = g.Key.pdate;
                        row["code"] = g.Key.code;
                        row["wtype"] = g.Key.wtype;
                        row["fname"] = g.Key.fname;
                        row["air"] = g.Sum(r => r.Field<decimal>("air"));
                        return row;
                    }).CopyToDataTable();
                    //******* End Group By DataTable ********
                    //***************************************




                    List<string> oEmp = new List<string>();

                    DateTime PDate = new DateTime(1900, 1, 1);
                    PDate = Convert.ToDateTime(dtGrpData.Rows[0]["pdate"].ToString());

                    // loop for
                    foreach (DataRow dr in dtGrpData.Rows)
                    {
                        DateTime _PDate = new DateTime(1900, 1, 1);
                        string _code = "";
                        string _wtype = "";
                        string _fname = "";
                        decimal _air = 0;


                        try { _PDate = Convert.ToDateTime(dr["pdate"].ToString()); } catch { }
                        try { _code = dr["code"].ToString(); } catch { }
                        try { _wtype = dr["wtype"].ToString(); } catch { }
                        try { _fname = dr["fname"].ToString(); } catch { }
                        try { _air = Math.Round(Convert.ToDecimal(dr["air"].ToString()),2); } catch { }
                        
                        if (_code != "" && _wtype != "")
                        {
                            oEmp.Add(_code);

                            //*** Check Have Data ****
                            DataTable dtChk = new DataTable();
                            string strChk = @"SELECT * FROM PRAJ WHERE ADJ='4' AND CODE='" + _code + "' AND PDATE = '" + _PDate.ToString("dd/MMM/yyyy") + "' ";
                            OracleCommand cmdChk = new OracleCommand();
                            cmdChk.CommandText = strChk;

                            if (_code.StartsWith("7"))
                            {
                                dtChk = oOraTRN.Query(cmdChk);
                            }
                            else if (_code.StartsWith("I"))
                            {
                                dtChk = oOraSUB.Query(cmdChk);
                            }
                            else
                            {
                                dtChk = oOraDCI.Query(cmdChk);
                            }



                            string strSQL = "";
                            //*** No Data => Insert ***
                            if (dtChk.Rows.Count == 0)
                            {
                                strSQL = @"INSERT INTO PRAJ (PDATE, WTYPE, CODE,  ADJ, ABDAY, WORK1,  WORK2, LOT1, LOT15,  LOT2, LOT3, COT1,  
                                                COT15, COT2, COT3,  OT, SALARY, ALLOW,  OTHINN, OTHDED, A400,  SHIFT, FOODSHT, FULL,  SUPER, 
                                                DVCD, POSIT,  ADBN, AD_FD, AD_FO,  AD_UN, AD_TP, AD_PZ, TRAN, A200, A400LEVEL) VALUES 
                                            ('" + _PDate.ToString("dd/MMM/yyyy") + "', '" + _wtype + "', '" + _code + @"',  '4', '0', '0',  '0', 
                                                '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0',
                                                '0', '" + _air.ToString() + @"', '0', '0', '0', '0', '0', '0', '0',  '0', 
                                                '0', '0', '0', '0', '0', '0', '0', '0'  )";

                            }
                            //*** Have Data => Update ***
                            else
                            {
                                strSQL = @"UPDATE PRAJ SET  OTHDED='" + _air.ToString() + @"'  
                                           WHERE ADJ='4' AND CODE='" + _code + "' AND PDATE = '" + _PDate.ToString("dd/MMM/yyyy") + "'   ";
                            }
                            OracleCommand cmdSQL = new OracleCommand();
                            cmdSQL.CommandText = strSQL;

                            if (_code.StartsWith("7"))
                            {
                                oOraTRN.Query(cmdSQL);
                            }
                            else if (_code.StartsWith("I"))
                            {
                                oOraSUB.Query(cmdSQL);
                            }
                            else
                            {
                                oOraDCI.Query(cmdSQL);
                            }

                        } // end if check




                    }// end foreach


                    DataTable dtSel = new DataTable();
                    string strSel = @"SELECT PDATE, CODE, WTYPE, OTHDED FROM (
                                        SELECT  PDATE, CODE, WTYPE, OTHDED 
                                        FROM DCI.PRAJ WHERE ADJ='4' AND PDATE = '" + PDate.ToString("dd/MMM/yyyy") + @"'
                                            UNION ALL
                                        SELECT  PDATE, CODE, WTYPE, OTHDED 
                                        FROM DCITC.PRAJ WHERE ADJ='4' AND PDATE = '" + PDate.ToString("dd/MMM/yyyy") + @"'
                                            UNION ALL 
                                        SELECT  PDATE, CODE, WTYPE, OTHDED 
                                        FROM DEV_OFFICE.PRAJ WHERE ADJ='4' AND PDATE = '" + PDate.ToString("dd/MMM/yyyy") + @"'
                                    )    ";
                    OracleCommand cmdSel = new OracleCommand();
                    cmdSel.CommandText = strSel;
                    dtSel = oOraDCI.Query(cmdSel);

                    dgvAir.Rows.Clear();
                    if (dtSel.Rows.Count > 0)
                    {
                        decimal air = 0;
                        foreach (DataRow drSel in dtSel.Rows)
                        {
                            if(oEmp.IndexOf(drSel["CODE"].ToString()) > -1)
                            {
                                try { air += Math.Round(Convert.ToDecimal(drSel["OTHDED"].ToString()),2); } catch { }

                                dgvAir.Rows.Add(drSel["PDATE"].ToString(), drSel["CODE"].ToString(), drSel["WTYPE"].ToString(),
                                    drSel["OTHDED"].ToString());
                            }                            
                        }

                        //**** Total ****
                        dgvAir.Rows.Add("Total", "", "", air.ToString());
                    }





                    MessageBox.Show("Successful!");


                } // end check error
                else
                {
                    MessageBox.Show(_errStr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }


            }

            lblAir.Text = "จำนวน " + dgvAir.Rows.Count.ToString();
        }

        #endregion


        #region Legal
        private void btnLegalTemplate_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                string sourceFilePath = Application.StartupPath + "\\TEMPLATE\\TMP_LEGAL.xlsx";

                saveFileDialog.Title = "Save File";
                saveFileDialog.FileName = "Legal_Template.xlsx";
                saveFileDialog.Filter = "Excel Files|*.xlsx"; // Tissue the file filters

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string destinationFilePath = saveFileDialog.FileName;

                    try
                    {
                        // Copy the file from the source to the destination
                        File.Copy(sourceFilePath, destinationFilePath);

                        MessageBox.Show("Download successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while copying the file: " + ex.Message);
                    }
                }
            }
        }

        private void btnLegalReNew_Click(object sender, EventArgs e)
        {
            txtLegal.Text = "";
            dgvLegal.Rows.Clear();
        }

        private void btnLegalBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog flDrg = new OpenFileDialog();
            flDrg.Filter = "Excel File|*.xlsx";

            if (flDrg.ShowDialog(this) != DialogResult.Cancel)
            {
                txtLegal.Text = flDrg.FileName;
            }
        }

        private void btnLegalSave_Click(object sender, EventArgs e)
        {
            // path to your excel file
            string path = txtLegal.Text;

            DataTable dtData = ReadExcelFile(path);

            DataTable dtDataLegal = new DataTable();
            dtDataLegal.Columns.Add("pdate", typeof(DateTime));
            dtDataLegal.Columns.Add("code", typeof(string));
            dtDataLegal.Columns.Add("wtype", typeof(string));
            dtDataLegal.Columns.Add("fname", typeof(string));
            dtDataLegal.Columns.Add("legal", typeof(decimal));

            dgvLegal.Rows.Clear();
            bool _error = false;
            string _errStr = "", _pdateRcd = "";
            if (dtData.Rows.Count > 0)
            {
                // loop through the worksheet rows and columns
                int i = 0;
                foreach (DataRow dr in dtData.Rows)
                {
                    string _pdate = "";
                    string _code = "";
                    string _wtype = "";
                    string _fname = "";
                    string _legal = "";

                    try { _pdate = dr[0].ToString(); } catch { }
                    try { _code = dr[1].ToString(); } catch { }
                    try { _wtype = dr[2].ToString(); } catch { }
                    try { _fname = dr[3].ToString(); } catch { }
                    try { _legal = dr[4].ToString(); } catch { }


                    //**** Check Pdate ****
                    if (i == 0)
                    {
                        _pdateRcd = _pdate;
                    }
                    if (_pdate != _pdateRcd && i > 0)
                    {
                        _errStr += _code + " : error no PDate not match." + System.Environment.NewLine;
                        _error = true;
                    }


                    decimal LEGAL = 0;
                    DateTime PDate = new DateTime(1900, 1, 1);

                    if (_code != "" && _wtype != "")
                    {
                        if (_code.Length == 2)
                        {
                            _code = "000" + _code;
                        }
                        //****** Get & Check Employee *******
                        DataTable dtEmp = new DataTable();
                        string strEmp = @"SELECT [CODE],CONCAT([PREN],[NAME],' ',[SURN]) Fname, POSIT FROM Employee WHERE Code=@Code ";
                        SqlCommand cmdEmp = new SqlCommand();
                        cmdEmp.CommandText = strEmp;
                        cmdEmp.Parameters.Add(new SqlParameter("@Code", _code));
                        dtEmp = oConHRM.Query(cmdEmp);


                        //**** Check Employee ****
                        if (dtEmp.Rows.Count > 0)
                        {
                            PDate = ConvertDate(_pdate);
                            if (PDate.Year < 2000)
                            {
                                _errStr += _code + " : error no PDate wrong format." + System.Environment.NewLine;
                                _error = true;
                            }


                            if (_wtype == "O" || _wtype == "S")
                            {

                            }
                            else
                            {
                                _errStr += _code + " : error WTYPE." + System.Environment.NewLine;
                                _error = true;
                            }


                            try { LEGAL = Math.Round(Convert.ToDecimal(_legal),2); } catch { }

                            if (!_error)
                            {
                                dtDataLegal.Rows.Add(PDate, _code, _wtype, _fname, LEGAL);
                            }
                        }
                        else
                        {
                            _errStr += _code + " : error no Emp Code." + System.Environment.NewLine;
                            _error = true;
                        }//*** end Check Employee ***


                    } // check have data


                    i++;
                } // end loop for


                //*** Error ***
                if (!_error && dtDataLegal.Rows.Count > 0)
                {
                    //************************************************
                    //************** GROUP BY DATA & SUM *************
                    DataTable dtGrpData = dtDataLegal.AsEnumerable()
                        .GroupBy(g => new { pdate = g.Field<DateTime>("pdate"), code = g.Field<string>("code"), wtype = g.Field<string>("wtype"), fname = g.Field<string>("fname") })
                        .Select(s =>
                        {
                            DataRow drNew = dtDataLegal.NewRow();
                            drNew["pdate"] = s.Key.pdate;
                            drNew["code"] = s.Key.code;
                            drNew["wtype"] = s.Key.wtype;
                            drNew["fname"] = s.Key.fname;
                            drNew["legal"] = s.Sum(d => d.Field<decimal>("legal"));

                            return drNew;
                        }).CopyToDataTable();
                    //************** GROUP BY DATA & SUM *************
                    //************************************************



                    List<string> oEmp = new List<string>();

                    DateTime PDate = new DateTime(1900, 1, 1);
                    PDate = Convert.ToDateTime(dtGrpData.Rows[0]["pdate"].ToString());

                    // loop for
                    foreach (DataRow dr in dtGrpData.Rows)
                    {
                        DateTime _PDate = new DateTime(1900, 1, 1);
                        string _code = "";
                        string _wtype = "";
                        string _fname = "";
                        decimal _legal = 0;


                        try { _PDate = Convert.ToDateTime(dr["pdate"].ToString()); } catch { }
                        try { _code = dr["code"].ToString(); } catch { }
                        try { _wtype = dr["wtype"].ToString(); } catch { }
                        try { _fname = dr["fname"].ToString(); } catch { }
                        try { _legal = Math.Round(Convert.ToDecimal(dr["legal"].ToString()),2); } catch { }

                        if (_code != "" && _wtype != "")
                        {
                            oEmp.Add(_code);

                            //*** Check Have Data ****
                            DataTable dtChk = new DataTable();
                            string strChk = @"SELECT * FROM PRAJ WHERE ADJ='6' AND CODE='" + _code + "' AND PDATE = '" + _PDate.ToString("dd/MMM/yyyy") + "' ";
                            OracleCommand cmdChk = new OracleCommand();
                            cmdChk.CommandText = strChk;

                            if (_code.StartsWith("7"))
                            {
                                dtChk = oOraTRN.Query(cmdChk);
                            }
                            else if (_code.StartsWith("I"))
                            {
                                dtChk = oOraSUB.Query(cmdChk);
                            }
                            else
                            {
                                dtChk = oOraDCI.Query(cmdChk);
                            }



                            string strSQL = "";
                            //*** No Data => Insert ***
                            if (dtChk.Rows.Count == 0)
                            {
                                strSQL = @"INSERT INTO PRAJ (PDATE, WTYPE, CODE,  ADJ, ABDAY, WORK1,  WORK2, LOT1, LOT15,  LOT2, LOT3, COT1,  
                                                COT15, COT2, COT3,  OT, SALARY, ALLOW,  OTHINN, OTHDED, A400,  SHIFT, FOODSHT, FULL,  SUPER, 
                                                DVCD, POSIT,  ADBN, AD_FD, AD_FO,  AD_UN, AD_TP, AD_PZ, TRAN, A200, A400LEVEL) VALUES 
                                            ('" + _PDate.ToString("dd/MMM/yyyy") + "', '" + _wtype + "', '" + _code + @"',  '6', '0', '0',  '0', 
                                                '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0',
                                                '0', '" + _legal.ToString() + @"', '0', '0', '0', '0', '0', '0', '0',  '0', 
                                                '0', '0', '0', '0', '0', '0', '0', '0'  )";

                            }
                            //*** Have Data => Update ***
                            else
                            {
                                strSQL = @"UPDATE PRAJ SET  OTHDED='" + _legal.ToString() + @"'  
                                           WHERE ADJ='6' AND CODE='" + _code + "' AND PDATE = '" + _PDate.ToString("dd/MMM/yyyy") + "'   ";
                            }
                            OracleCommand cmdSQL = new OracleCommand();
                            cmdSQL.CommandText = strSQL;

                            if (_code.StartsWith("7"))
                            {
                                oOraTRN.Query(cmdSQL);
                            }
                            else if (_code.StartsWith("I"))
                            {
                                oOraSUB.Query(cmdSQL);
                            }
                            else
                            {
                                oOraDCI.Query(cmdSQL);
                            }

                        } // end if check




                    }// end foreach



                    DataTable dtSel = new DataTable();
                    string strSel = @"SELECT PDATE, CODE, WTYPE, OTHDED FROM (
                                        SELECT  PDATE, CODE, WTYPE, OTHDED 
                                        FROM DCI.PRAJ WHERE ADJ='6' AND PDATE = '" + PDate.ToString("dd/MMM/yyyy") + @"'
                                            UNION ALL
                                        SELECT  PDATE, CODE, WTYPE, OTHDED 
                                        FROM DCITC.PRAJ WHERE ADJ='6' AND PDATE = '" + PDate.ToString("dd/MMM/yyyy") + @"'
                                            UNION ALL 
                                        SELECT  PDATE, CODE, WTYPE, OTHDED 
                                        FROM DEV_OFFICE.PRAJ WHERE ADJ='6' AND PDATE = '" + PDate.ToString("dd/MMM/yyyy") + @"'
                                    )   ";
                    OracleCommand cmdSel = new OracleCommand();
                    cmdSel.CommandText = strSel;
                    dtSel = oOraDCI.Query(cmdSel);

                    dgvLegal.Rows.Clear();
                    if (dtSel.Rows.Count > 0)
                    {
                        decimal legel = 0;
                        foreach (DataRow drSel in dtSel.Rows)
                        {
                            if(oEmp.IndexOf(drSel["CODE"].ToString()) > -1)
                            {
                                try { legel += Math.Round(Convert.ToDecimal(drSel["OTHDED"].ToString()),2); } catch { }

                                dgvLegal.Rows.Add(drSel["PDATE"].ToString(), drSel["CODE"].ToString(), drSel["WTYPE"].ToString(),
                                    drSel["OTHDED"].ToString());
                            }
                            
                        }

                        //**** Total ****
                        dgvLegal.Rows.Add("Total", "", "", legel.ToString());
                    }





                    MessageBox.Show("Successful!");


                } // end check error
                else
                {
                    MessageBox.Show(_errStr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }


            }

            lblLegal.Text = "จำนวน " + dgvLegal.Rows.Count.ToString();
        }


        #endregion

        private void btnFrmSrc_Click(object sender, EventArgs e)
        {
            DateTime datePDate = new DateTime(1900, 1, 1);
            datePDate = new DateTime(dptPDATE.Value.Year, dptPDATE.Value.Month, 1);

            DataTable dtSrc = new DataTable();
            string strSrc = @"SELECT A.*, E.pren, E.name, E.surn, E.join joinDT, E.resign 
                              FROM PRAJ A
                              LEFT JOIN EMPM E ON E.CODE = A.CODE 
                              WHERE pdate='" + datePDate.ToString("dd/MMM/yyyy") + @"' 
                                    AND adj = '"+ txtAdj.Text +@"' AND A.CODE = '"+ txtEmpCode.Text +@"'
                              ORDER BY pdate,wtype,CODE,adj ";
            OracleCommand cmdSrc = new OracleCommand();
            cmdSrc.CommandText = strSrc;
            dtSrc = oOraDCI.Query(cmdSrc);

            if(dtSrc.Rows.Count > 0)
            {
                DataRow drPRAJ = dtSrc.Rows[0];

                decimal _ot = Math.Round(Convert.ToDecimal(drPRAJ["OT"].ToString()),2);
                decimal _salary = Math.Round(Convert.ToDecimal(drPRAJ["SALARY"].ToString()),2);
                decimal _allow = Math.Round(Convert.ToDecimal(drPRAJ["ALLOW"].ToString()),2);


                txtFrmAbday.Text = drPRAJ["abday"].ToString();
                txtFrmWork1.Text = drPRAJ["work1"].ToString();
                txtFrmWork2.Text = drPRAJ["work2"].ToString();
                txtFrmLOT1.Text = drPRAJ["lot1"].ToString();
                txtFrmLOT15.Text = drPRAJ["lot15"].ToString();
                txtFrmLOT2.Text = drPRAJ["lot2"].ToString();
                txtFrmLOT3.Text = drPRAJ["lot3"].ToString();
                txtFrmCOT1.Text = drPRAJ["cot1"].ToString();
                txtFrmCOT15.Text = drPRAJ["cot15"].ToString();
                txtFrmCOT2.Text = drPRAJ["cot2"].ToString();
                txtFrmCOT3.Text = drPRAJ["cot3"].ToString();
                txtFrmOT.Text = drPRAJ["OT"].ToString();
                txtFrmSalary.Text = drPRAJ["SALARY"].ToString();
                txtFrmAllow.Text = drPRAJ["ALLOW"].ToString();
                txtFrmOTHINT.Text = (_ot + _salary + _allow).ToString();


                decimal _full = Math.Round(Convert.ToDecimal(drPRAJ["Full"].ToString()),2);
                decimal _super = Math.Round(Convert.ToDecimal(drPRAJ["super"].ToString()),2);
                txtFrmFull.Text = drPRAJ["Full"].ToString();
                txtFrmSuper.Text = drPRAJ["super"].ToString();
                txtFrmPrize.Text = (_full + _super).ToString();




                txtFrmOTHINN.Text = drPRAJ["othinn"].ToString();
                txtFrmOTHDED.Text = drPRAJ["othded"].ToString();
                txtFrmTran.Text = drPRAJ["TRAN"].ToString();
                txtFrmShift.Text = drPRAJ["Shift"].ToString();
                txtFrmFoodSht.Text = drPRAJ["foodsht"].ToString();

                decimal _ad_fd = Math.Round(Convert.ToDecimal(drPRAJ["ad_fd"].ToString()),2);
                decimal _ad_fo = Math.Round(Convert.ToDecimal(drPRAJ["ad_fo"].ToString()),2);
                decimal _ad_un = Math.Round(Convert.ToDecimal(drPRAJ["ad_un"].ToString()),2);
                decimal _ad_tp = Math.Round(Convert.ToDecimal(drPRAJ["ad_tp"].ToString()),2);
                decimal _ad_pz = Math.Round(Convert.ToDecimal(drPRAJ["ad_pz"].ToString()),2);

                txtFrmAD_FD.Text = drPRAJ["ad_fd"].ToString();
                txtFrmAD_FO.Text = drPRAJ["ad_fo"].ToString();
                txtFrmAD_UN.Text = drPRAJ["ad_un"].ToString();
                txtFrmAD_TP.Text = drPRAJ["ad_tp"].ToString();
                txtFrmAD_PZ.Text = drPRAJ["ad_pz"].ToString();
                txtFrmBenefit.Text = (_ad_fd + _ad_fo + _ad_un + _ad_tp + _ad_pz).ToString();

            } // end if have data

            
            


        }

        private void btnFrmAdjustRpt_Click(object sender, EventArgs e)
        {

        }

        private void btnFrmAdjustRptSpecial_Click(object sender, EventArgs e)
        {

        }
    }
}
