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
using System.Windows.Media;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Reflection.Emit;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.xmp.impl;
using ICSharpCode.SharpZipLib.Core;
using Rectangle = iTextSharp.text.Rectangle;
using CrystalDecisions.Shared.Json;


namespace DCI.HRMS.PayRoll
{

    public partial class Frm_PayrollCalculation : Form
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
        public Frm_PayrollCalculation()
        {
            InitializeComponent();
        }

        private void dataRepeater1_CurrentItemIndexChanged(object sender, EventArgs e)
        {

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            String strToolPath = "p:\\payroll.exe";



            try
            {
                Process p = Process.Start(strToolPath);

                Thread.Sleep(500); // Allow the process to open it's window

                SetParent(p.MainWindowHandle, this.MdiParent.Handle);
            }
            catch (Exception ex)
            {
                //System.Diagnostics.EventLog.WriteEntry("HRMS", ex.Message, EventLogEntryType.Error, 1111);
            }
            finally
            {

            }

        }

        private void Frm_PayrollCalculation_Load(object sender, EventArgs e)
        {
            pwDiag.ShowDialog(this);
            HideGroupBoxes();
            tmClose.Enabled = true;/*************************************/


            //*************************************************************
            //            Initial Combobox 
            //*************************************************************
            DataTable dtTaxType = new DataTable();
            dtTaxType.Columns.Add("DataDisplay", typeof(string));
            dtTaxType.Columns.Add("DataValue", typeof(string));
            dtTaxType.Rows.Add("ภ.ง.ด. 1", "PGD1");
            dtTaxType.Rows.Add("ภ.ง.ด. 1 ก", "PGD1K");
            dtTaxType.Rows.Add("ภ.ง.ด. 91", "PGD91");
            cbTaxType.DataSource = dtTaxType;
            cbTaxType.DisplayMember = "DataDisplay";
            cbTaxType.ValueMember = "DataValue";
            cbTaxType.Refresh();

             
            DataTable dtCertType = new DataTable();
            dtCertType.Columns.Add("DataDisplay", typeof(string));
            dtCertType.Columns.Add("DataValue", typeof(string));
            dtCertType.Rows.Add("หนังสือรับรองเงินเดือน", "SALARY");
            dtCertType.Rows.Add("หนังสือรับรอง ธอส.", "TOS");
            dtCertType.Rows.Add("หนังสือรับรอง ออมสิน.", "OMSIN");
            cbCertType.DataSource = dtCertType;
            cbCertType.DisplayMember = "DataDisplay";
            cbCertType.ValueMember = "DataValue";
            cbCertType.Refresh();


            
            DataTable dtTaviType = new DataTable();
            dtTaviType.Columns.Add("DataDisplay", typeof(string));
            dtTaviType.Columns.Add("DataValue", typeof(string));
            dtTaviType.Rows.Add("พิมพ์ทั้งหมด (ONLINE) ", "ONLINE");
            dtTaviType.Rows.Add("พิมพ์ทั้งหมด ( M )", "M");
            dtTaviType.Rows.Add("พิมพ์ทั้งหมด ( R )", "R");
            dtTaviType.Rows.Add("พิมพ์ทั้งหมด ( S )", "S");
            dtTaviType.Rows.Add("พิมพ์ทั้งหมด ( O )", "O");
            dtTaviType.Rows.Add("พิมพ์รายบุคคล", "ONE");
            cbTaviType.DataSource = dtTaviType;
            cbTaviType.DisplayMember = "DataDisplay";
            cbTaviType.ValueMember = "DataValue";
            cbTaviType.Refresh();



            DataTable dtPGDType = new DataTable();
            dtPGDType.Columns.Add("DataDisplay", typeof(string));
            dtPGDType.Columns.Add("DataValue", typeof(string));
            dtPGDType.Rows.Add("ภ.ง.ด. 1", "PGD1");
            dtPGDType.Rows.Add("ภ.ง.ด. 1 ก", "PGD1K");
            cbPGDType.DataSource = dtPGDType;
            cbPGDType.DisplayMember = "DataDisplay";
            cbPGDType.ValueMember = "DataValue";
            cbPGDType.Refresh();



            DataTable dtTaviYear = new DataTable();
            dtTaviYear.Columns.Add("DataDisplay", typeof(string));
            dtTaviYear.Columns.Add("DataValue", typeof(string));
            for (int y = 0; y > -20; y--)
            {
                dtTaviYear.Rows.Add(DateTime.Now.AddYears(y).ToString("yyyy"), DateTime.Now.AddYears(y).ToString("yyyy"));
            }
            cbTaviYear.DataSource = dtTaviYear;
            cbTaviYear.DisplayMember = "DataDisplay";
            cbTaviYear.ValueMember = "DataValue";
            cbTaviYear.Refresh();
            //*************************************************************
            //           End Initial Combobox 
            //*************************************************************


        }




        private void tmClose_Tick(object sender, EventArgs e)
        {
            if (pwDiag.dlgResult == "CANCEL")
            {
                this.Close();
            }


            try
            {
                
                if (pwDiag.Password == "48xx5a")
                {

                }else{
                    UserAccountInfo usr = userAccountService.Authentication("PayRollCalPerm", pwDiag.Password);
                    if (usr == null)
                    {
                        this.Close();
                    }
                }
                
            }
            catch (Exception)
            {

                this.Close();


            }
        }
        private void HideGroupBoxes()
        {
            foreach (Control item in flowLayoutPanel1.Controls)
            {
                if (item is ComponentFactory.Krypton.Toolkit.KryptonGroup)
                {
                    item.Visible = false;
                }
            }
        }

        private void btnSalaryUpload_Click(object sender, EventArgs e)
        {
            HideGroupBoxes();
            grbSalaryUpload.Visible = true;


        }



        private void btnSalaryUploadBrw_Click(object sender, EventArgs e)
        {
            OpenFileDialog flDrg = new OpenFileDialog();
            flDrg.Filter = "ExcelFile|*.xls";

            if (flDrg.ShowDialog(this) != DialogResult.Cancel)
            {
                txtSalaryUpload.Text = flDrg.FileName;


            }
        }
        private void kryptonButton3_Click(object sender, EventArgs e)
        {

            //try
            //{
            //    ds = xl.ConvertFile(txtSalaryUpload.Text, "Salary");

            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}


            //Year CODE    POSITION GRADEPOSITION   WTYPE WSTS    NAME JOIN    DEPT.SECTION GROUP   GRADESALARY PercentUp    Salary HOUSING  CostofLiving POSITIONAllawance



            DataTable dataSalary = new DataTable();
            try
            {
                dataSalary = ReadExcelFileSalaryBonus(txtSalaryUpload.Text);
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }


            //if (ds.Tables.Count != 0)
            if (dataSalary.Rows.Count  > 0)
            {

                DataTable dtEmp = new DataTable();
                string strEmp = @"SELECT * FROM (
                                                SELECT CODE,WTYPE,WSTS,Salary,HOUSING FROM DCI.EMPM WHERE RESIGN IS NULL OR RESIGN > :RESIGN
                                                    UNION ALL
                                                SELECT CODE,WTYPE,WSTS,Salary,HOUSING FROM DCITC.EMPM WHERE RESIGN IS NULL  OR RESIGN > :RESIGN
                                                    UNION ALL
                                                SELECT CODE,WTYPE,WSTS,Salary,HOUSING FROM DEV_OFFICE.EMPM WHERE RESIGN IS NULL  OR RESIGN > :RESIGN
                                            ) ";
                OracleCommand cmdEmp = new OracleCommand();
                cmdEmp.CommandText = strEmp;
                cmdEmp.Parameters.Add(new OracleParameter(":RESIGN", DateTime.Now.AddMonths(-3).ToString("dd/MMM/yyyy")));
                dtEmp = oOraDCI.Query(cmdEmp);
                //***********************************************************
                //***********************************************************
                //***********************************************************
                //***********************************************************

                //DataTable salTb = ds.Tables[0];
                DataTable salTb = dataSalary;
                //stsMgr.MaxProgress = salTb.Rows.Count + 1;
                //stsMgr.Status = "";
                //foreach (DataRow item in salTb.Rows)
                //{
                //    string empCode = item["Code"].ToString();
                //    stsMgr.Progress++;
                //    stsMgr.Status = "Checking Code :" + empCode;
                //    if (empCode.Trim() != "")
                //    {



                //        EmployeeInfo empDt = empSvr.FindBasicInfo(empCode);
                //        if (empDt == null)
                //        {
                //            MessageBox.Show("Employee Code Not Found , please check :" + empCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //            stsMgr.Progress = 0;
                //            stsMgr.Status = "";

                //            goto endProcess;

                //        }
                //    }
                //}
                stsMgr.Progress = 0;

                int idx = 1;
                foreach (DataRow item in salTb.Rows)
                {
                    string empCode = item["Code"].ToString();

                    stsMgr.Progress = (stsMgr.Progress < 100) ? stsMgr.Progress + 1 : 100;
                    stsMgr.Status = "Update Employee Data Code:" + empCode + " (" + idx.ToString("N0") + "/" + salTb.Rows.Count.ToString("N0") + ")";
                    if (empCode.Trim() != "")
                    {


                        DataRow[] drEmp = dtEmp.Select("CODE = '" + empCode + "' ");
                        if (drEmp.Count() > 0)
                        {

                            // update data employee salary
                            if (drEmp[0]["WTYPE"].ToString() == "S")
                            {
                                string strEmpUpd = @"UPDATE EMPM SET Salary=:Salary, HOUSING=:HOUSING, UPD_BY=:UPD_BY, UPD_DT=SYSDATE WHERE CODE=:CODE ";
                                OracleCommand cmdEmpUpd = new OracleCommand();
                                cmdEmpUpd.CommandText = strEmpUpd;
                                cmdEmpUpd.Parameters.Add(new OracleParameter(":Salary", Convert.ToDecimal(item["Salary"])));
                                cmdEmpUpd.Parameters.Add(new OracleParameter(":HOUSING", Convert.ToDecimal(item["HOUSING"])));
                                cmdEmpUpd.Parameters.Add(new OracleParameter(":UPD_BY", appMgr.UserAccount.AccountId));
                                cmdEmpUpd.Parameters.Add(new OracleParameter(":CODE", empCode));
                                oOraDCI.ExecuteCommand(cmdEmpUpd);

                            }else {
                                string strEmpUpd = @"UPDATE EMPM SET DLRATE=:Salary, HOUSING=:HOUSING, UPD_BY=:UPD_BY, UPD_DT=SYSDATE WHERE CODE=:CODE ";
                                OracleCommand cmdEmpUpd = new OracleCommand();
                                cmdEmpUpd.CommandText = strEmpUpd;
                                cmdEmpUpd.Parameters.Add(new OracleParameter(":Salary", Convert.ToDecimal(item["Salary"])));
                                cmdEmpUpd.Parameters.Add(new OracleParameter(":HOUSING", Convert.ToDecimal(item["HOUSING"])));                                
                                cmdEmpUpd.Parameters.Add(new OracleParameter(":UPD_BY", appMgr.UserAccount.AccountId));
                                cmdEmpUpd.Parameters.Add(new OracleParameter(":CODE", empCode));
                                oOraDCI.ExecuteCommand(cmdEmpUpd);
                            }



                            // update data salary
                            Eva_SalaryInfo sal = new Eva_SalaryInfo();
                            sal.Code = empCode;
                            sal.Year = Convert.ToString(item["Year"]);
                            if (item["PercentUp"] != DBNull.Value)
                            {
                                sal.PercentUp = Convert.ToDecimal(item["PercentUp"]);
                            }
                            if (drEmp[0]["WTYPE"].ToString() == "S")
                            {
                                if (item["Salary"] != DBNull.Value)
                                {
                                    sal.Salary = Convert.ToDecimal(item["Salary"]);
                                }
                                sal.Wage = 0;
                            }
                            else
                            {
                                sal.Salary = 0;
                                if (item["Salary"] != DBNull.Value)
                                {
                                    sal.Wage = Convert.ToDecimal(item["Salary"]);
                                }
                            }
                            sal.SalaryGrade = Convert.ToString(item["GRADESALARY"]);

                            try
                            {
                                if (evaSvr.GetEvaSalary(sal.Code, sal.Year) == null)
                                {
                                    evaSvr.SaveEvaSalaryInfo(sal);
                                }
                                else
                                {
                                    evaSvr.UpdateEvaSalaryInfo(sal);
                                }
                            }
                            catch (Exception ex)
                            {

                                throw ex;
                            }


                        } // end if
                        else {
                            MessageBox.Show("Employee Code Not Found , please check :" + empCode , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            stsMgr.Progress = 0;
                            stsMgr.Status = "";

                            goto endProcess;
                        } // end if



                        //**** Comment on 2022-02-18  *******
                        #region comment
                        /*
                        EmployeeDataInfo empDt = empSvr.GetEmployeeData(empCode);
                        
                        if (empDt == null)
                        {
                            MessageBox.Show("Employee Code Not Found , please check :" + empCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            stsMgr.Progress = 0;
                            stsMgr.Status = "";

                            goto endProcess;

                        }
                        else
                        {
                            
                            



                            if (item["POSITION"].ToString() != "")
                            {
                                empDt.Position.Code = item["Position"].ToString();
                            }
                            if (item["GRADEPOSITION"].ToString() != "")
                            {
                                empDt.Grad = Convert.ToInt32(item["GRADEPOSITION"]);
                            }
                            else
                            {
                                empDt.Grad = 0;
                            }
                            if (item["WTYPE"].ToString() != "")
                            {
                                empDt.WorkType = Convert.ToString(item["WTYPE"]);
                            }
                            if (item["WSTS"].ToString() != "")
                            {
                                empDt.EmployeeType = Convert.ToString(item["WSTS"]);
                            }
                            if (item["Salary"].ToString() != "")
                            {
                                if (empDt.WorkType == "S")
                                {
                                    empDt.Salary = Convert.ToDecimal(item["Salary"]);
                                    empDt.Wedge = 0;
                                }
                                else
                                {
                                    empDt.Salary = 0;
                                    empDt.Wedge = Convert.ToDecimal(item["Salary"]);
                                }

                            }
                            if (item["HOUSING"].ToString() != "")
                            {
                                empDt.Housing = Convert.ToDecimal(item["HOUSING"]);
                            }
                            if (item["CostofLiving"].ToString() != "")
                            {

                            }
                            if (item["POSITIONAllawance"].ToString() != "")
                            {
                                empDt.PositAllawance = Convert.ToDecimal(item["POSITIONAllawance"]);
                            }
                            try
                            {
                                empDt.LastUpdateBy = appMgr.UserAccount.AccountId;
                                empSvr.UpdateEmployeeInfo(empDt);
                            }
                            catch (Exception ex)
                            {

                                throw ex;
                            }


                            Eva_SalaryInfo sal = new Eva_SalaryInfo();
                            sal.Code = empCode;
                            sal.Year = Convert.ToString(item["Year"]);
                            if (item["PercentUp"]!= DBNull.Value)
                            {
                                sal.PercentUp = Convert.ToDecimal(item["PercentUp"]); 
                            }
                            if (empDt.WorkType == "S")
                            {
                                if (item["Salary"]!=DBNull.Value)
                                {
                                    sal.Salary = Convert.ToDecimal(item["Salary"]);  
                                }
                                sal.Wage = 0;
                            }
                            else
                            {
                                sal.Salary = 0;
                                if (item["Salary"] != DBNull.Value)
                                {
                                    sal.Wage = Convert.ToDecimal(item["Salary"]); 
                                }
                            }
                            sal.SalaryGrade = Convert.ToString(item["GRADESALARY"]);

                            try
                            {
                                if (evaSvr.GetEvaSalary(sal.Code, sal.Year) == null)
                                {
                                    evaSvr.SaveEvaSalaryInfo(sal);
                                }
                                else
                                {
                                    evaSvr.UpdateEvaSalaryInfo(sal);
                                }
                            }
                            catch (Exception ex)
                            {

                                throw ex;
                            }



                        }
                        */
                        #endregion
                    }
                }
                MessageBox.Show("Update Employee Data Complete", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                stsMgr.Progress = 0;
                stsMgr.Status = "";

            endProcess: ;

            }
        }


        private void btnBonusUpload_Click(object sender, EventArgs e)
        {
            HideGroupBoxes();

            grbBonusUpload.Visible = true;
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            OpenFileDialog flDrg = new OpenFileDialog();
            flDrg.Filter = "ExcelFile|*.xls";

            if (flDrg.ShowDialog(this) != DialogResult.Cancel)
            {
                txtBonusUpload.Text = flDrg.FileName;


            }


        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    ds = xl.ConvertFile(txtBonusUpload.Text, "Bonus");

            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}

            DataTable dataBonus = new DataTable();
            try {
                dataBonus = ReadExcelFileSalaryBonus(txtBonusUpload.Text);
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

            //if (ds.Tables.Count != 0)
            if (dataBonus.Rows.Count > 0)
            {

                DataTable dtEmp = new DataTable();
                string strEmp = @"SELECT * FROM (
                                                SELECT CODE,WTYPE,WSTS,Salary,HOUSING FROM DCI.EMPM WHERE RESIGN IS NULL OR RESIGN > :RESIGN
                                                    UNION ALL
                                                SELECT CODE,WTYPE,WSTS,Salary,HOUSING FROM DCITC.EMPM WHERE RESIGN IS NULL  OR RESIGN > :RESIGN
                                                    UNION ALL
                                                SELECT CODE,WTYPE,WSTS,Salary,HOUSING FROM DEV_OFFICE.EMPM WHERE RESIGN IS NULL  OR RESIGN > :RESIGN
                                            ) ";
                OracleCommand cmdEmp = new OracleCommand();
                cmdEmp.CommandText = strEmp;
                cmdEmp.Parameters.Add(new OracleParameter(":RESIGN", DateTime.Now.AddMonths(-3).ToString("dd/MMM/yyyy")));
                dtEmp = oOraDCI.Query(cmdEmp);
                //***********************************************************
                //***********************************************************
                //***********************************************************
                //***********************************************************



                //DataTable bonTb = ds.Tables[0];
                DataTable bonTb = dataBonus;
                stsMgr.MaxProgress = bonTb.Rows.Count + 1;
                stsMgr.Status = "";
                foreach (DataRow item in bonTb.Rows)
                {
                    string empCode = item["Code"].ToString();
                    stsMgr.Progress++;
                    stsMgr.Status = "Checking Code :" + empCode;
                    if (empCode.Trim() != "")
                    {
                        
                        //EmployeeInfo empDt = empSvr.FindBasicInfo(empCode);
                        //Thread.Sleep(50);

                        //if (empDt == null)

                        DataRow[] drEmp = dtEmp.Select(" CODE = '" + empCode + "' ");
                        if (drEmp.Count() == 0)
                        {
                            MessageBox.Show("Employee Code Not Found , please check :" + empCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            stsMgr.Progress = 0;
                            stsMgr.Status = "";
                            goto endProcessBo;
                        }
                        string year = Convert.ToString(item["Year"]);
                        Eva_BonusInfo bnInfo = new Eva_BonusInfo();

                        bnInfo = new Eva_BonusInfo();
                        bnInfo.Code = empCode;
                        bnInfo.Year = year;

                        if (item["LATETIME"] != DBNull.Value)
                        {
                            string t = item["LATETIME"].ToString();
                             bnInfo.LateTime = ConvDecimal(item["LATETIME"].ToString());
                        }
                        if (item["LATE"] != DBNull.Value)
                        {
                            bnInfo.Late = ConvDecimal(item["LATE"].ToString()); 
                        }
                        if (item["SICKTIME"] != DBNull.Value)
                        {
                            bnInfo.SickTime = ConvDecimal(item["SICKTIME"].ToString());
                        }
                        if (item["SICK"] != DBNull.Value)
                        {
                            bnInfo.Sick = ConvDecimal(item["SICK"].ToString()); 
                        }
                        if (item["PERSTIME"] != DBNull.Value)
                        {
                            bnInfo.PersTime = ConvDecimal(item["PERSTIME"].ToString());
                        }
                        if (item["PERS"] != DBNull.Value)
                        {
                            bnInfo.Pers = ConvDecimal(item["PERS"].ToString());
                        }
                        if (item["ABSETIME"] != DBNull.Value)
                        {
                            bnInfo.AbseTime = ConvDecimal(item["ABSETIME"].ToString()); 
                        }
                        if (item["ABSE"] != DBNull.Value)
                        {
                            bnInfo.Abse = ConvDecimal(item["ABSE"].ToString()); 
                        }
                        if (item["MATETIME"] != DBNull.Value)
                        {
                            bnInfo.MateTime = ConvDecimal(item["MATETIME"].ToString()); 
                        }
                        if (item["MATE"] != DBNull.Value)
                        {
                            bnInfo.Mate = ConvDecimal(item["MATE"].ToString()); 
                        }
                        if (item["MILITIME"] != DBNull.Value)
                        {
                            bnInfo.MiliTime = ConvDecimal(item["MILITIME"].ToString()); 
                        }
                        if (item["MILI"] != DBNull.Value)
                        {
                            bnInfo.Mili = ConvDecimal(item["MILI"].ToString()); 
                        }
                        if (item["PRIETIME"] != DBNull.Value)
                        {
                            bnInfo.PrieTime = ConvDecimal(item["PRIETIME"].ToString()); 
                        }
                        if (item["PRIE"] != DBNull.Value)
                        {
                            bnInfo.Prie = ConvDecimal(item["PRIE"].ToString()); 
                        }
                        if (item["MARRTIME"] != DBNull.Value)
                        {
                            bnInfo.MarrTime = ConvDecimal(item["MARRTIME"].ToString()); 
                        }
                        if (item["MARR"] != DBNull.Value)
                        {
                            bnInfo.Marr = ConvDecimal(item["MARR"].ToString()); 
                        }
                        if (item["FUMETIME"] != DBNull.Value)
                        {
                            bnInfo.FuneTime = ConvDecimal(item["FUMETIME"].ToString()); 
                        }
                        if (item["FUNE"] != DBNull.Value)
                        {
                            bnInfo.Fune = ConvDecimal(item["FUNE"].ToString()); 
                        }
                        if (item["TRAINTIME"] != DBNull.Value)
                        {
                            bnInfo.TrainTime = ConvDecimal(item["TRAINTIME"].ToString()); 
                        }
                        if (item["TRAIN"] != DBNull.Value)
                        {
                            bnInfo.Train = ConvDecimal(item["TRAIN"].ToString()); 
                        }
                        if (item["VERTIME"] != DBNull.Value)
                        {
                            bnInfo.VobalTime = ConvDecimal(item["VERTIME"].ToString()); 
                        }
                        if (item["VER"] != DBNull.Value)
                        {
                            bnInfo.Vobal = ConvDecimal(item["VER"].ToString()); 
                        }
                        if (item["LETTIME"] != DBNull.Value)
                        {
                            bnInfo.LetterTime = ConvDecimal(item["LETTIME"].ToString()); 
                        }
                        if (item["LET"] != DBNull.Value)
                        {
                            bnInfo.Letter = ConvDecimal(item["LET"].ToString()); 
                        }
                        if (item["SUSTIME"] != DBNull.Value)
                        {
                            bnInfo.SuspensionTime = ConvDecimal(item["SUSTIME"].ToString()); 
                        }
                        if (item["SUS"] != DBNull.Value)
                        {
                            bnInfo.Suspension = ConvDecimal(item["SUS"].ToString());
                        }

                        if (item["GRADEBONUS"] != DBNull.Value)
                        {
                            bnInfo.Grade = Convert.ToString(item["GRADEBONUS"].ToString());
                        }
                        if (item["FINALBONUS"] != DBNull.Value)
                        {
                            bnInfo.NetBonus = ConvDecimal(item["FINALBONUS"].ToString()); 
                        }
                        if (item["ACTUALBONUS"] != DBNull.Value)
                        {
                            bnInfo.BasicBonus = ConvDecimal(item["ACTUALBONUS"].ToString());
                        }
                        if (item["DEDUCTION"] != DBNull.Value)
                        {
                            bnInfo.NetDeduct = ConvDecimal(item["DEDUCTION"].ToString()); 
                        }
                        if (item["SPECIALMONEY"] != DBNull.Value)
                        {
                            bnInfo.SpecailMoney = ConvDecimal(item["SPECIALMONEY"].ToString()); 
                        }
                        if (item["PRIZE"] != DBNull.Value)
                        {
                            bnInfo.Prize = ConvDecimal(item["PRIZE"].ToString());
                        }
                        if ((bnInfo.Abse == 0 ^ bnInfo.AbseTime == 0))
                        {
                            MessageBox.Show("Bonus data Error Abse, AbseTime , please check :" + empCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            stsMgr.Progress = 0;
                            stsMgr.Status = "";
                            goto endProcessBo;
                        }
                        if ((bnInfo.Fune == 0 ^ bnInfo.FuneTime == 0))
                        {
                            MessageBox.Show("Bonus data Error Fune, FuneTime , please check :" + empCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            stsMgr.Progress = 0;
                            stsMgr.Status = "";
                            goto endProcessBo;
                        }
                        if ((bnInfo.Late == 0 ^ bnInfo.LateTime < 3))
                        {
                            MessageBox.Show("Bonus data Error Late, LateTime  , please check :" + empCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            stsMgr.Progress = 0;
                            stsMgr.Status = "";
                            goto endProcessBo;
                        }
                        if ((bnInfo.Letter == 0 ^ bnInfo.LetterTime == 0))
                        {
                            MessageBox.Show("Bonus data Error Letter, LetterTime, please check :" + empCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            stsMgr.Progress = 0;
                            stsMgr.Status = "";
                            goto endProcessBo;
                        }
                        if ((bnInfo.Marr == 0 ^ bnInfo.MarrTime == 0))
                        {
                            MessageBox.Show("Bonus data Error Marr, MarrTime, please check :" + empCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            stsMgr.Progress = 0;
                            stsMgr.Status = "";
                            goto endProcessBo;
                        }
                        /*
                        if ((bnInfo.Mate == 0 ^ bnInfo.MateTime == 0))
                        {
                            MessageBox.Show("Bonus data Error , please check :" + empCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            stsMgr.Progress = 0;
                            stsMgr.Status = "";
                            goto endProcessBo;
                        }*/
                        if ((bnInfo.Mili == 0 ^ bnInfo.MiliTime == 0))
                        {
                            MessageBox.Show("Bonus data Error Mili,MiliTime, please check :" + empCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            stsMgr.Progress = 0;
                            stsMgr.Status = "";
                            goto endProcessBo;
                        }
                        if ((bnInfo.Pers == 0 ^ bnInfo.PersTime == 0))
                        {
                            MessageBox.Show("Bonus data Error Pers, PersTime, please check :" + empCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            stsMgr.Progress = 0;
                            stsMgr.Status = "";
                            goto endProcessBo;
                        }
                        if ((bnInfo.Prie == 0 ^ bnInfo.PrieTime == 0))
                        {
                            MessageBox.Show("Bonus data Error , please check :" + empCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            stsMgr.Progress = 0;
                            stsMgr.Status = "";
                            goto endProcessBo;
                        }
                        if ((bnInfo.Sick == 0 ^ bnInfo.SickTime == 0))
                        {
                            MessageBox.Show("Bonus data Error Sick,SickTime, please check :" + empCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            stsMgr.Progress = 0;
                            stsMgr.Status = "";
                            goto endProcessBo;
                        } 
                        if ((bnInfo.Train == 0 ^ bnInfo.TrainTime == 0))
                        {
                            MessageBox.Show("Bonus data Error Train, TrainTime, please check :" + empCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            stsMgr.Progress = 0;
                            stsMgr.Status = "";
                            goto endProcessBo;
                        }
                        if ((bnInfo.Suspension == 0 ^ bnInfo.SuspensionTime == 0))
                        {
                            MessageBox.Show("Bonus data Error Suspension, SuspensionTime, please check :" + empCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            stsMgr.Progress = 0;
                            stsMgr.Status = "";
                            goto endProcessBo;
                        }
                        if ((bnInfo.Vobal == 0 ^ bnInfo.VobalTime == 0))
                        {
                            MessageBox.Show("Bonus data Error Vobal, VobalTime, please check :" + empCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            stsMgr.Progress = 0;
                            stsMgr.Status = "";
                            goto endProcessBo;
                        }


                    }
                }


                stsMgr.Progress = 0;
                int idx = 1;
                foreach (DataRow item in bonTb.Rows)
                {
                    string empCode = item["Code"].ToString();

                    stsMgr.Progress++;
                    stsMgr.Status = "Update  Data Code:" + empCode +" ("+idx.ToString("N0") +"/"+bonTb.Rows.Count.ToString("N0")+")";
                    if (empCode.Trim() != "")
                    {
                        //EmployeeDataInfo empDt = empSvr.GetEmployeeData(empCode);
                        //Thread.Sleep(50);
                        //if (empDt == null)

                        DataRow[] drEmp = dtEmp.Select(" CODE = '" + empCode + "' ");
                        if (drEmp.Count() == 0)
                        {
                            MessageBox.Show("Employee Code Not Found , please check :" + empCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            stsMgr.Progress = 0;
                            stsMgr.Status = "";

                            goto endProcessBo;

                        }
                        string year = Convert.ToString(item["Year"]);
                        Eva_BonusInfo bnInfo = evaSvr.GetEvaBonusUnq(empCode, year);
                        if (bnInfo == null)
                        {
                            bnInfo = new Eva_BonusInfo();
                            bnInfo.Code = empCode;
                            bnInfo.Year = year;

                            if (item["LATETIME"] != DBNull.Value)
                            {
                                bnInfo.LateTime = ConvDecimal(item["LATETIME"].ToString());
                            }
                            if (item["LATE"] != DBNull.Value)
                            {
                                bnInfo.Late = ConvDecimal(item["LATE"].ToString()); 
                            }
                            if (item["SICKTIME"] != DBNull.Value)
                            {
                                bnInfo.SickTime = ConvDecimal(item["SICKTIME"].ToString());
                            }
                            if (item["SICK"] != DBNull.Value)
                            {
                                bnInfo.Sick = ConvDecimal(item["SICK"].ToString()); 
                            }
                            if (item["PERSTIME"] != DBNull.Value)
                            {
                                bnInfo.PersTime = ConvDecimal(item["PERSTIME"].ToString()); 
                            }
                            if (item["PERS"] != DBNull.Value)
                            {
                                bnInfo.Pers = ConvDecimal(item["PERS"].ToString()); 
                            }
                            if (item["ABSETIME"] != DBNull.Value)
                            {
                                bnInfo.AbseTime = ConvDecimal(item["ABSETIME"].ToString()); 
                            }
                            if (item["ABSE"] != DBNull.Value)
                            {
                                bnInfo.Abse = ConvDecimal(item["ABSE"].ToString()); 
                            }
                            if (item["MATETIME"] != DBNull.Value)
                            {
                                bnInfo.MateTime = ConvDecimal(item["MATETIME"].ToString()); 
                            }
                            if (item["MATE"] != DBNull.Value)
                            {
                                bnInfo.Mate = ConvDecimal(item["MATE"].ToString()); 
                            }
                            if (item["MILITIME"] != DBNull.Value)
                            {
                                bnInfo.MiliTime = ConvDecimal(item["MILITIME"].ToString()); 
                            }
                            if (item["MILI"] != DBNull.Value)
                            {
                                bnInfo.Mili = ConvDecimal(item["MILI"].ToString());
                            }
                            if (item["PRIETIME"] != DBNull.Value)
                            {
                                bnInfo.PrieTime = ConvDecimal(item["PRIETIME"].ToString()); 
                            }
                            if (item["PRIE"] != DBNull.Value)
                            {
                                bnInfo.Prie = ConvDecimal(item["PRIE"].ToString()); 
                            }
                            if (item["MARRTIME"] != DBNull.Value)
                            {
                                bnInfo.MarrTime = ConvDecimal(item["MARRTIME"].ToString()); 
                            }
                            if (item["MARR"] != DBNull.Value)
                            {
                                bnInfo.Marr = ConvDecimal(item["MARR"].ToString()); 
                            }
                            if (item["FUMETIME"] != DBNull.Value)
                            {
                                bnInfo.FuneTime = ConvDecimal(item["FUMETIME"].ToString()); 
                            }
                            if (item["FUNE"] != DBNull.Value)
                            {
                                bnInfo.Fune = ConvDecimal(item["FUNE"].ToString()); 
                            }
                            if (item["TRAINTIME"] != DBNull.Value)
                            {
                                bnInfo.TrainTime = ConvDecimal(item["TRAINTIME"].ToString()); 
                            }
                            if (item["TRAIN"] != DBNull.Value)
                            {
                                bnInfo.Train = ConvDecimal(item["TRAIN"].ToString()); 
                            }
                            if (item["VERTIME"] != DBNull.Value)
                            {
                                bnInfo.VobalTime = ConvDecimal(item["VERTIME"].ToString());
                            }
                            if (item["VER"] != DBNull.Value)
                            {
                                bnInfo.Vobal = ConvDecimal(item["VER"].ToString()); 
                            }
                            if (item["LETTIME"] != DBNull.Value)
                            {
                                bnInfo.LetterTime = ConvDecimal(item["LETTIME"].ToString()); 
                            }
                            if (item["LET"] != DBNull.Value)
                            {
                                bnInfo.Letter = ConvDecimal(item["LET"].ToString()); 
                            }
                            if (item["SUSTIME"] != DBNull.Value)
                            {
                                bnInfo.SuspensionTime = ConvDecimal(item["SUSTIME"].ToString()); 
                            }
                            if (item["SUS"] != DBNull.Value)
                            {
                                bnInfo.Suspension = ConvDecimal(item["SUS"].ToString()); 
                            }

                            if (item["GRADEBONUS"] != DBNull.Value)
                            {
                                bnInfo.Grade = Convert.ToString(item["GRADEBONUS"].ToString());
                            }
                            if (item["FINALBONUS"] != DBNull.Value)
                            {
                                bnInfo.NetBonus = ConvDecimal(item["FINALBONUS"].ToString()); 
                            }
                            if (item["ACTUALBONUS"] != DBNull.Value)
                            {
                                bnInfo.BasicBonus = ConvDecimal(item["ACTUALBONUS"].ToString()); 
                            }
                            if (item["DEDUCTION"] != DBNull.Value)
                            {
                                bnInfo.NetDeduct = ConvDecimal(item["DEDUCTION"].ToString()); 
                            }
                            if (item["SPECIALMONEY"] != DBNull.Value)
                            {
                                bnInfo.SpecailMoney = ConvDecimal(item["SPECIALMONEY"].ToString());
                            }
                            if (item["PRIZE"] != DBNull.Value)
                            {
                                bnInfo.Prize = ConvDecimal(item["PRIZE"].ToString()); 
                            }
                            if (!( bnInfo.Abse == 0 && bnInfo.AbseTime == 0))
                            {             
                   
                            }

                            try
                            {
                                evaSvr.SaveEvaBonusInfo(bnInfo);
                            }
                            catch (Exception ex)
                            {

                                throw ex;
                            }


                        }
                        else
                        {
                            if (item["LATETIME"] != DBNull.Value)
                            {
                                bnInfo.LateTime = ConvDecimal(item["LATETIME"].ToString());
                            }
                            if (item["LATE"] != DBNull.Value)
                            {
                                bnInfo.Late = ConvDecimal(item["LATE"].ToString());
                            }
                            if (item["SICKTIME"] != DBNull.Value)
                            {
                                bnInfo.SickTime = ConvDecimal(item["SICKTIME"].ToString());
                            }
                            if (item["SICK"] != DBNull.Value)
                            {
                                bnInfo.Sick = ConvDecimal(item["SICK"].ToString());
                            }
                            if (item["PERSTIME"] != DBNull.Value)
                            {
                                bnInfo.PersTime = ConvDecimal(item["PERSTIME"].ToString());
                            }
                            if (item["PERS"] != DBNull.Value)
                            {
                                bnInfo.Pers = ConvDecimal(item["PERS"].ToString());
                            }
                            if (item["ABSETIME"] != DBNull.Value)
                            {
                                bnInfo.AbseTime = ConvDecimal(item["ABSETIME"].ToString());
                            }
                            if (item["ABSE"] != DBNull.Value)
                            {
                                bnInfo.Abse = ConvDecimal(item["ABSE"].ToString());
                            }
                            if (item["MATETIME"] != DBNull.Value)
                            {
                                bnInfo.MateTime = ConvDecimal(item["MATETIME"].ToString());
                            }
                            if (item["MATE"] != DBNull.Value)
                            {
                                bnInfo.Mate = ConvDecimal(item["MATE"].ToString());
                            }
                            if (item["MILITIME"] != DBNull.Value)
                            {
                                bnInfo.MiliTime = ConvDecimal(item["MILITIME"].ToString());
                            }
                            if (item["MILI"] != DBNull.Value)
                            {
                                bnInfo.Mili = ConvDecimal(item["MILI"].ToString());
                            }
                            if (item["PRIETIME"] != DBNull.Value)
                            {
                                bnInfo.PrieTime = ConvDecimal(item["PRIETIME"].ToString());
                            }
                            if (item["PRIE"] != DBNull.Value)
                            {
                                bnInfo.Prie = ConvDecimal(item["PRIE"].ToString());
                            }
                            if (item["MARRTIME"] != DBNull.Value)
                            {
                                bnInfo.MarrTime = ConvDecimal(item["MARRTIME"].ToString());
                            }
                            if (item["MARR"] != null)
                            {
                                bnInfo.Marr = ConvDecimal(item["MARR"].ToString());
                            }
                            if (item["FUMETIME"] != DBNull.Value)
                            {
                                bnInfo.FuneTime = ConvDecimal(item["FUMETIME"].ToString());
                            }
                            if (item["FUNE"] != DBNull.Value)
                            {
                                bnInfo.Fune = ConvDecimal(item["FUNE"].ToString());
                            }
                            if (item["TRAINTIME"] != DBNull.Value)
                            {
                                bnInfo.TrainTime = ConvDecimal(item["TRAINTIME"].ToString());
                            }
                            if (item["TRAIN"] != DBNull.Value)
                            {
                                bnInfo.Train = ConvDecimal(item["TRAIN"].ToString());
                            }
                            if (item["VERTIME"] != DBNull.Value)
                            {
                                bnInfo.VobalTime = ConvDecimal(item["VERTIME"].ToString());
                            }
                            if (item["VER"] != DBNull.Value)
                            {
                                bnInfo.Vobal = ConvDecimal(item["VER"].ToString());
                            }
                            if (item["LETTIME"] != DBNull.Value)
                            {
                                bnInfo.LetterTime = ConvDecimal(item["LETTIME"].ToString());
                            }
                            if (item["LET"] != DBNull.Value)
                            {
                                bnInfo.Letter = ConvDecimal(item["LET"].ToString());
                            }
                            if (item["SUSTIME"] != DBNull.Value)
                            {
                                bnInfo.SuspensionTime = ConvDecimal(item["SUSTIME"].ToString());
                            }
                            if (item["SUS"] != DBNull.Value)
                            {
                                bnInfo.Suspension = ConvDecimal(item["SUS"].ToString());
                            }


                            if (item["GRADEBONUS"] != DBNull.Value)
                            {
                                bnInfo.Grade = Convert.ToString(item["GRADEBONUS"].ToString());
                            }
                            if (item["FINALBONUS"] != DBNull.Value)
                            {
                                bnInfo.NetBonus = ConvDecimal(item["FINALBONUS"].ToString());
                            }
                            if (item["ACTUALBONUS"] != DBNull.Value)
                            {
                                bnInfo.BasicBonus = ConvDecimal(item["ACTUALBONUS"].ToString());
                            }
                            if (item["DEDUCTION"] != DBNull.Value)
                            {
                                bnInfo.NetDeduct = ConvDecimal(item["DEDUCTION"].ToString());
                            }
                            if (item["SPECIALMONEY"] != DBNull.Value)
                            {
                                bnInfo.SpecailMoney = ConvDecimal(item["SPECIALMONEY"].ToString());
                            }

                            if (item["PRIZE"] != DBNull.Value)
                            {
                                bnInfo.Prize = ConvDecimal(item["PRIZE"].ToString());
                            }
                            try
                            {
                                evaSvr.UpdateEvaBonusInfo(bnInfo);
                            }
                            catch (Exception ex)
                            {

                                throw ex;
                            }


                        }

                    }

                    idx++;
                }

                MessageBox.Show("Update Employee Data Complete", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                stsMgr.Progress = 0;
                stsMgr.Status = "";
            endProcessBo: ;
            }
        }

        public decimal ConvDecimal(string strNumber) {
            decimal number = 0;
            try { number = Convert.ToDecimal(strNumber); }
            catch { number = 0; }
            return number;
        }




        // GET DATA FROM EXCEL AND POPULATE COMB0 BOX.
        private DataTable ReadConvertExcel(string sFile, string ShtName)
        {
            DataTable dtData = new DataTable();


            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            object misValue = System.Reflection.Missing.Value;
            string str;
            int rCnt;
            int cCnt;
            int rw = 0;
            int cl = 0;
            List<int> colIdx = new List<int>();

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(sFile, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            //xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets[ShtName];

            //MessageBox.Show(xlWorkSheet.get_Range("A1", "A1").Value2.ToString());


            range = xlWorkSheet.UsedRange;
            rw = range.Rows.Count;
            //MessageBox.Show(range.Rows.Count.ToString());
            if (rw > 3000)
            {
                rw = 3000;
            }
            cl = range.Columns.Count;

            //********** SET Array Columns Index *************
            for (cCnt = 1; cCnt < cl; cCnt++)
            {
                string strCol = "";
                try
                {
                    strCol = (range.Cells[1, cCnt] as Excel.Range).Value2.ToString().Trim();
                }
                catch (Exception ex) { }  //MessageBox.Show("col [" + cCnt + "]: " + strCol + " , Error:" + ex.Message.ToString()); }

                if (strCol != "")
                {
                    colIdx.Add(cCnt);
                }
            }



            //******* Loop Set Columns Header **********
            if (colIdx.Count > 0)
            {
                foreach (int cIdx in colIdx)
                {
                    try
                    {
                        dtData.Columns.Add((range.Cells[1, cIdx] as Excel.Range).Value2.ToString(), typeof(string));
                    }
                    catch (Exception ex) { MessageBox.Show("col : " + cIdx + " , Error:" + ex.Message.ToString()); }
                } // end foreach columns


                for (rCnt = 2; rCnt <= rw; rCnt++)
                {
                    DataRow newRow = dtData.NewRow();
                    foreach (int cIdx in colIdx)
                    {
                        string strCol = (range.Cells[1, cIdx] as Excel.Range).Value2.ToString().Trim();


                        string strVal = "";
                        try { strVal = (range.Cells[rCnt, cIdx] as Excel.Range).Value2.ToString(); }
                        catch (Exception ex) { } //{ MessageBox.Show("row: " + rCnt + ", col: " + cIdx + ", error: " + ex.Message.ToString()); }

                        newRow[strCol] = strVal;

                    } // end foreach columns 


                    dtData.Rows.Add(newRow);
                }


            } // end columns have data 





            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

            return dtData;

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


        private string getSpaceText(string _Data, int _len)
        {
            string result = "";
            result = _Data;
            for (int i = _Data.Length ; i < _len; i++)
            {
                result += " ";
            }
            return result;
        }

        private string getLeadZeroText(string _Data, int _len, int _float)
        {
            string result = "";

            decimal _num = Convert.ToDecimal(_Data);

            //string _format = "";
            //for (int i = 0; i < _len; i++)
            //{
            //    _format += "0";
            //}


            if (_float == 0)
            {
                string _format = "";
                for (int i = 0; i < _len; i++)
                {
                    _format += "0";
                }
                //result = _num.ToString("D" + _len.ToString());
                result = _num.ToString(_format);

            }
            else if(_float == 2)
            {

                string _format = "";
                for (int i = 0; i < _len-1; i++)
                {
                    _format += "0";
                }
                //result = (_num * 100).ToString("D"+_len.ToString());
                result = (_num * 100).ToString(_format)+"0";
            }
            else
            {
                
            }

            return result;
        }



        private void btnSendBank_Click(object sender, EventArgs e)
        {

            saveFileDlg.FileName = "scb_data.txt";
            saveFileDlg.RestoreDirectory = true;
            saveFileDlg.Filter = "Text Files (.txt)|*.txt;";
            DialogResult dlg = saveFileDlg.ShowDialog();
            if (dlg == DialogResult.OK)
            {
                //string fileName = @"E:\Temp\Mahesh.txt";
                string fileName = saveFileDlg.FileName;
                DateTime iDate = new DateTime(dpkrPayroll.Value.Year, dpkrPayroll.Value.Month, 1);
                DateTime bankPayDate = dpkrBankPay.Value;


                DataTable dtSum = new DataTable();
                //string strSum = @"SELECT SUM(net) AS Snet FROM PRDT WHERE pdate='" + iDate.ToString("dd/MMM/yyyy") + "' and bank='SCB' and  bankac is not null ";
                string strSum = @"SELECT SUM(Snet) Snet FROM (
                                        SELECT SUM(net) AS Snet FROM DCI.PRDT WHERE pdate='" + iDate.ToString("dd/MMM/yyyy") + @"' and bank='SCB' and  bankac is not null
                                           UNION ALL
                                        SELECT SUM(net) AS Snet FROM DCITC.PRDT WHERE pdate='" + iDate.ToString("dd/MMM/yyyy") + @"' and bank='SCB' and  bankac is not null
                                           UNION ALL
                                        SELECT SUM(net) AS Snet FROM DEV_OFFICE.PRDT WHERE pdate='" + iDate.ToString("dd/MMM/yyyy") + @"' and bank='SCB' and  bankac is not null
                                    ) ";
                OracleCommand cmdSum = new OracleCommand();
                cmdSum.CommandText = strSum;
                dtSum = oOraDCI.Query(cmdSum);



                DataTable dtPRDT = new DataTable();
                //string strPRDT = @"SELECT P.*, E.NAME, E.SURN 
                //                    FROM PRDT P
                //                    LEFT JOIN EMPM E ON P.code = E.code 
                //                    WHERE pdate='" + iDate.ToString("dd/MMM/yyyy") + @"' 
                //                            AND P.bank='SCB'  AND P.bankac is not null 
                //                    ORDER BY P.code ";


                //UNION ALL

                //                    SELECT P.code, P.net, P.bank, P.bankac, P.wtype, E.NAME, E.SURN
                //                    FROM DCITC.PRDT P
                //                    LEFT JOIN DCITC.EMPM E ON P.code = E.code
                //                    WHERE pdate = '" + iDate.ToString("dd/MMM/yyyy") + @"'
                //                            AND P.bank = 'SCB'  AND P.bankac is not null


                string strPRDT = @"SELECT * FROM (
                                    SELECT P.code, P.net, P.bank, P.bankac, P.wtype, E.NAME, E.SURN 
                                    FROM DCI.PRDT P
                                    LEFT JOIN DCI.EMPM E ON P.code = E.code 
                                    WHERE pdate='" + iDate.ToString("dd/MMM/yyyy") + @"'
                                            AND P.bank='SCB'  AND P.bankac is not null 
    
                                    UNION ALL
    
                                    SELECT P.code, P.net, P.bank, P.bankac, P.wtype, E.NAME, E.SURN
                                    FROM DEV_OFFICE.PRDT P
                                    LEFT JOIN DEV_OFFICE.EMPM E ON P.code = E.code 
                                    WHERE pdate='" + iDate.ToString("dd/MMM/yyyy") + @"'
                                            AND P.bank='SCB'  AND P.bankac is not null 
                                )
                                ORDER BY code  ";
                OracleCommand cmdPRDT = new OracleCommand();
                cmdPRDT.CommandText = strPRDT;
                dtPRDT = oOraDCI.Query(cmdPRDT);


                //*** check have data ****
                if (dtPRDT.Rows.Count > 0) {

                    try
                    {
                        // Check if file already exists. If yes, delete it.     
                        if (File.Exists(fileName))
                        {
                            File.Delete(fileName);
                        }

                        // Create a new file
                        using (StreamWriter sw = File.CreateText(fileName))
                        {
                            //******* Header *********
                            PayrollSendBankInfo.PayrollSendBankHead mPayHead = new PayrollSendBankInfo.PayrollSendBankHead();
                            mPayHead._hd01_type = "001";
                            mPayHead._hd02_comid = getSpaceText("dcin303", 12);
                            mPayHead._hd03_comref = getSpaceText("dcin303001", 32);
                            mPayHead._hd04_msgDT = bankPayDate.ToString("yyyyMMdd");
                            mPayHead._hd05_msgTM = "000000";
                            mPayHead._hd06_chId = "BCM";
                            mPayHead._hd07_batRef = getSpaceText("dcin303001", 32);
                            sw.WriteLine(String.Format("{0}{1}{2}{3}{4}{5}{6}",
                                mPayHead._hd01_type, mPayHead._hd02_comid, mPayHead._hd03_comref, mPayHead._hd04_msgDT, mPayHead._hd05_msgTM, mPayHead._hd06_chId, mPayHead._hd07_batRef));


                            //******* Debit *********
                            PayrollSendBankInfo.PayrollSendBankDebit mPayDebit = new PayrollSendBankInfo.PayrollSendBankDebit();
                            mPayDebit._Db01_type = "002";
                            mPayDebit._Db02_prdCd = "PAY";
                            mPayDebit._Db03_valDT = bankPayDate.ToString("yyyyMMdd");
                            mPayDebit._Db04_AccNo = getSpaceText("8323003303", 25);
                            mPayDebit._Db05_AccType = "03";
                            mPayDebit._Db06_BrchCd = "0000";
                            mPayDebit._Db07_Curr = "THB";
                            mPayDebit._Db08_Amt = getLeadZeroText(dtSum.Rows[0]["Snet"].ToString(), 16, 2); //**** pay amt 16 digit *****
                            mPayDebit._Db09_Ref = "00000001";
                            mPayDebit._Db10_NoCre = getLeadZeroText(dtPRDT.Rows.Count.ToString(), 6, 0);    //***** record 8 digit *****
                            mPayDebit._Db11_FeeAcc = getSpaceText("8323003303", 15);
                            mPayDebit._Db12_Filtr = getSpaceText("", 9);
                            mPayDebit._Db13_Cler = getSpaceText("", 1);
                            mPayDebit._Db14_AccTypeFee = "03";
                            mPayDebit._Db15_BrchCdFee = "0000";
                            sw.WriteLine(String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}",
                                mPayDebit._Db01_type, mPayDebit._Db02_prdCd, mPayDebit._Db03_valDT, mPayDebit._Db04_AccNo, mPayDebit._Db05_AccType, mPayDebit._Db06_BrchCd, mPayDebit._Db07_Curr, mPayDebit._Db08_Amt, mPayDebit._Db09_Ref, mPayDebit._Db10_NoCre, mPayDebit._Db11_FeeAcc, mPayDebit._Db12_Filtr, mPayDebit._Db13_Cler, mPayDebit._Db14_AccTypeFee, mPayDebit._Db15_BrchCdFee));


                            //**** Loop Employee ****
                            int idx = 1;
                            foreach (DataRow drPRDT in dtPRDT.Rows)
                            {
                                //******* Credit *********
                                PayrollSendBankInfo.PayrollSendBankCredit mPayCredit = new PayrollSendBankInfo.PayrollSendBankCredit();
                                mPayCredit._Cr01_type = "003";
                                mPayCredit._Cr02_seq = getLeadZeroText(idx.ToString(), 6, 0);           // ****** Run Nbr ********
                                mPayCredit._Cr03_acc = getSpaceText(drPRDT["BANKAC"].ToString(), 25);   // ****** Bank Acc ********
                                mPayCredit._Cr04_amt = getLeadZeroText(drPRDT["NET"].ToString(), 16, 2);      // ****** Pay Amt ******** 
                                mPayCredit._Cr05_curr = "THB";
                                mPayCredit._Cr06_ref = "00000001";
                                mPayCredit._Cr07_wht = "N";
                                mPayCredit._Cr08_inv_pre = "N";
                                mPayCredit._Cr09_adv_req = "N";
                                mPayCredit._Cr10_del_mode = "S";
                                mPayCredit._Cr11_pick = getSpaceText("", 4);
                                mPayCredit._Cr12_wht_frm = "00";
                                mPayCredit._Cr13_wht_tax = getSpaceText("", 14);
                                mPayCredit._Cr14_wht_att = "000000";
                                mPayCredit._Cr15_wht_no = "00";
                                mPayCredit._Cr16_wht_amt = "0000000000000000";
                                mPayCredit._Cr17_inv_det = "000000";
                                mPayCredit._Cr18_int_amt = "0000000000000000";
                                mPayCredit._Cr19_wht_pay = getSpaceText("", 1);
                                mPayCredit._Cr20_wht_rmk = getSpaceText("", 40);
                                mPayCredit._Cr21_wht_decDT = getSpaceText("", 8);
                                mPayCredit._Cr22_rec_cd = "014";
                                mPayCredit._Cr23_rec_name = getSpaceText("SIAM COMMERCIAL BANK", 35);
                                mPayCredit._Cr24_rec_brn_cd = getLeadZeroText(drPRDT["BANKAC"].ToString().Substring(0, 3), 4, 0);   // ****** Emp Bank Acc Branch ********
                                mPayCredit._Cr25_rec_brn_name = getSpaceText("Bank: 014 Br.: " + getLeadZeroText(drPRDT["BANKAC"].ToString().Substring(0, 3), 4, 0), 35);   // ****** Emp Bank Name Branch ********
                                mPayCredit._Cr26_wht_sign = "B";
                                mPayCredit._Cr27_benefic = "N";
                                mPayCredit._Cr28_cus_ref = getSpaceText("", 20);
                                mPayCredit._Cr29_cheq_ref = getSpaceText("", 1);
                                mPayCredit._Cr30_pay_type_cd = getSpaceText("", 3);
                                mPayCredit._Cr31_serv_type = "04";
                                mPayCredit._Cr32_rmk = getSpaceText("", 50);
                                mPayCredit._Cr33_scb_rmk = getSpaceText("", 18);
                                mPayCredit._Cr34_benefic_chrg = getSpaceText("", 2);
                                sw.WriteLine(String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}",
                                    mPayCredit._Cr01_type, mPayCredit._Cr02_seq, mPayCredit._Cr03_acc, mPayCredit._Cr04_amt, mPayCredit._Cr05_curr, mPayCredit._Cr06_ref, mPayCredit._Cr07_wht, mPayCredit._Cr08_inv_pre, mPayCredit._Cr09_adv_req, mPayCredit._Cr10_del_mode, 
                                    mPayCredit._Cr11_pick, mPayCredit._Cr12_wht_frm, mPayCredit._Cr13_wht_tax, mPayCredit._Cr14_wht_att, mPayCredit._Cr15_wht_no, mPayCredit._Cr16_wht_amt, mPayCredit._Cr17_inv_det, mPayCredit._Cr18_int_amt, mPayCredit._Cr19_wht_pay, 
                                    mPayCredit._Cr20_wht_rmk, mPayCredit._Cr21_wht_decDT, mPayCredit._Cr22_rec_cd, mPayCredit._Cr23_rec_name, mPayCredit._Cr24_rec_brn_cd, mPayCredit._Cr25_rec_brn_name, mPayCredit._Cr26_wht_sign, mPayCredit._Cr27_benefic, 
                                    mPayCredit._Cr28_cus_ref, mPayCredit._Cr29_cheq_ref, mPayCredit._Cr30_pay_type_cd, mPayCredit._Cr31_serv_type, mPayCredit._Cr32_rmk, mPayCredit._Cr33_scb_rmk, mPayCredit._Cr34_benefic_chrg ));


                                //******* Payee *********
                                PayrollSendBankInfo.PayrollSendBankPayee mPayee = new PayrollSendBankInfo.PayrollSendBankPayee();
                                mPayee._Py01_type = "004";
                                mPayee._Py02_ref = "00000001";
                                mPayee._Py03_seq = getLeadZeroText(idx.ToString(), 6, 0);           // ****** Run Nbr ********
                                mPayee._Py04_id_card = "000000000000000";                           // ****** Personal Card ID ********
                                mPayee._Py05_name_th = getSpaceText(drPRDT["NAME"].ToString(), 100);   // ****** Employee Name ********
                                mPayee._Py06_addr1 = getSpaceText("", 70);
                                mPayee._Py07_addr2 = getSpaceText("", 70);
                                mPayee._Py08_addr3 = getSpaceText("", 70);
                                mPayee._Py09_taxId = getSpaceText("", 10);
                                mPayee._Py10_name_en = getSpaceText("", 70);
                                mPayee._Py11_fax_nbr = "0000000000";
                                mPayee._Py12_mobile = "0000000000";
                                mPayee._Py13_email = getSpaceText("", 64);
                                mPayee._Py14_payee2_name = getSpaceText("", 100);
                                mPayee._Py15_payee2_addr1 = getSpaceText("", 70);
                                mPayee._Py16_payee2_addr2 = getSpaceText("", 70);
                                mPayee._Py17_payee2_addr3 = getSpaceText("", 70);
                                sw.WriteLine(String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}",
                                    mPayee._Py01_type, mPayee._Py02_ref, mPayee._Py03_seq, mPayee._Py04_id_card, mPayee._Py05_name_th, mPayee._Py06_addr1, mPayee._Py07_addr2, mPayee._Py08_addr3, 
                                    mPayee._Py09_taxId, mPayee._Py10_name_en, mPayee._Py11_fax_nbr, mPayee._Py12_mobile, mPayee._Py13_email, mPayee._Py14_payee2_name, mPayee._Py15_payee2_addr1, 
                                    mPayee._Py16_payee2_addr2, mPayee._Py17_payee2_addr3  ));






                                idx++;
                            } // end foreach 





                            //******* Trailer *********
                            PayrollSendBankInfo.PayrollSendBankTrailer mPayTailer = new PayrollSendBankInfo.PayrollSendBankTrailer();
                            mPayTailer._Tr01_type = "999";
                            mPayTailer._Tr02_ttl_debit = "000001";
                            mPayTailer._Tr03_ttl_credit = getLeadZeroText(dtPRDT.Rows.Count.ToString(), 6, 0);    //***** record 8 digit *****
                            mPayTailer._Tr04_ttl_amt = getLeadZeroText(dtSum.Rows[0]["Snet"].ToString(), 16, 2); //**** pay amt 16 digit *****
                            sw.WriteLine(String.Format("{0}{1}{2}{3}",
                                mPayTailer._Tr01_type, mPayTailer._Tr02_ttl_debit, mPayTailer._Tr03_ttl_credit, mPayTailer._Tr04_ttl_amt));




                        } // end write file


                        MessageBox.Show("Write Send Bank file OK!");
                    }
                    catch (Exception Ex)
                    {
                        Console.WriteLine(Ex.ToString());
                    }
                } // end if more than one row
                else
                {
                    MessageBox.Show("No payroll data in date : "+ dpkrPayroll.Value.ToString("dd/MMM/yyyy"));
                }

            } // end select dest file
        }

        private void btnSpSendBank_Click(object sender, EventArgs e)
        {
            saveFileDlg.FileName = "scb_special_data.txt";
            saveFileDlg.RestoreDirectory = true;
            saveFileDlg.Filter = "Text Files (.txt)|*.txt;";
            DialogResult dlg = saveFileDlg.ShowDialog();
            if (dlg == DialogResult.OK)
            {
                //string fileName = @"E:\Temp\Mahesh.txt";
                string fileName = saveFileDlg.FileName;
                DateTime iDate = dpkrSpPayroll.Value;
                DateTime bankPayDate = dpkrSpBankPay.Value;


                DataTable dtSum = new DataTable();
                string strSum = @"SELECT SUM(pay) AS Snet FROM prspecial WHERE pdate='" + iDate.ToString("dd/MMM/yyyy") + "' and bank='SCB' and  bankac is not null ";
                OracleCommand cmdSum = new OracleCommand();
                cmdSum.CommandText = strSum;
                dtSum = oOraDCI.Query(cmdSum);



                DataTable dtPRDT = new DataTable();
                string strPRDT = @"SELECT P.*, E.NAME, E.SURN 
                                    FROM prspecial P
                                    LEFT JOIN EMPM E ON E.code = P.code 
                                    WHERE pdate='" + iDate.ToString("dd/MMM/yyyy") + @"' 
                                            AND P.bank='SCB'  AND P.bankac is not null 
                                    ORDER BY P.code ";
                OracleCommand cmdPRDT = new OracleCommand();
                cmdPRDT.CommandText = strPRDT;
                dtPRDT = oOraDCI.Query(cmdPRDT);


                //*** check have data ****
                if (dtPRDT.Rows.Count > 0)
                {

                    try
                    {
                        // Check if file already exists. If yes, delete it.     
                        if (File.Exists(fileName))
                        {
                            File.Delete(fileName);
                        }

                        // Create a new file
                        using (StreamWriter sw = File.CreateText(fileName))
                        {
                            //******* Header *********
                            PayrollSendBankInfo.PayrollSendBankHead mPayHead = new PayrollSendBankInfo.PayrollSendBankHead();
                            mPayHead._hd01_type = "001";
                            mPayHead._hd02_comid = getSpaceText("dcin303", 12);
                            mPayHead._hd03_comref = getSpaceText("dcin303001", 32);
                            mPayHead._hd04_msgDT = bankPayDate.ToString("yyyyMMdd");
                            mPayHead._hd05_msgTM = "000000";
                            mPayHead._hd06_chId = "BCM";
                            mPayHead._hd07_batRef = getSpaceText("dcin303001", 32);
                            sw.WriteLine(String.Format("{0}{1}{2}{3}{4}{5}{6}",
                                mPayHead._hd01_type, mPayHead._hd02_comid, mPayHead._hd03_comref, mPayHead._hd04_msgDT, mPayHead._hd05_msgTM, mPayHead._hd06_chId, mPayHead._hd07_batRef));


                            //******* Debit *********
                            PayrollSendBankInfo.PayrollSendBankDebit mPayDebit = new PayrollSendBankInfo.PayrollSendBankDebit();
                            mPayDebit._Db01_type = "002";
                            mPayDebit._Db02_prdCd = "PAY";
                            mPayDebit._Db03_valDT = bankPayDate.ToString("yyyyMMdd");
                            mPayDebit._Db04_AccNo = getSpaceText("8323003303", 25);
                            mPayDebit._Db05_AccType = "03";
                            mPayDebit._Db06_BrchCd = "0000";
                            mPayDebit._Db07_Curr = "THB";
                            mPayDebit._Db08_Amt = getLeadZeroText(dtSum.Rows[0]["Snet"].ToString(), 16, 2); //**** pay amt 16 digit *****
                            mPayDebit._Db09_Ref = "00000001";
                            mPayDebit._Db10_NoCre = getLeadZeroText(dtPRDT.Rows.Count.ToString(), 6, 0);    //***** record 8 digit *****
                            mPayDebit._Db11_FeeAcc = getSpaceText("8323003303", 15);
                            mPayDebit._Db12_Filtr = getSpaceText("", 9);
                            mPayDebit._Db13_Cler = getSpaceText("", 1);
                            mPayDebit._Db14_AccTypeFee = "03";
                            mPayDebit._Db15_BrchCdFee = "0000";
                            sw.WriteLine(String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}",
                                mPayDebit._Db01_type, mPayDebit._Db02_prdCd, mPayDebit._Db03_valDT, mPayDebit._Db04_AccNo, mPayDebit._Db05_AccType, mPayDebit._Db06_BrchCd, mPayDebit._Db07_Curr, mPayDebit._Db08_Amt, mPayDebit._Db09_Ref, mPayDebit._Db10_NoCre, mPayDebit._Db11_FeeAcc, mPayDebit._Db12_Filtr, mPayDebit._Db13_Cler, mPayDebit._Db14_AccTypeFee, mPayDebit._Db15_BrchCdFee));


                            //**** Loop Employee ****
                            int idx = 1;
                            foreach (DataRow drPRDT in dtPRDT.Rows)
                            {
                                //******* Credit *********
                                PayrollSendBankInfo.PayrollSendBankCredit mPayCredit = new PayrollSendBankInfo.PayrollSendBankCredit();
                                mPayCredit._Cr01_type = "003";
                                mPayCredit._Cr02_seq = getLeadZeroText(idx.ToString(), 6, 0);           // ****** Run Nbr ********
                                mPayCredit._Cr03_acc = getSpaceText(drPRDT["BANKAC"].ToString(), 25);   // ****** Bank Acc ********
                                mPayCredit._Cr04_amt = getLeadZeroText(drPRDT["PAY"].ToString(), 16, 2);      // ****** Pay Amt ******** 
                                mPayCredit._Cr05_curr = "THB";
                                mPayCredit._Cr06_ref = "00000001";
                                mPayCredit._Cr07_wht = "N";
                                mPayCredit._Cr08_inv_pre = "N";
                                mPayCredit._Cr09_adv_req = "N";
                                mPayCredit._Cr10_del_mode = "S";
                                mPayCredit._Cr11_pick = getSpaceText("", 4);
                                mPayCredit._Cr12_wht_frm = "00";
                                mPayCredit._Cr13_wht_tax = getSpaceText("", 14);
                                mPayCredit._Cr14_wht_att = "000000";
                                mPayCredit._Cr15_wht_no = "00";
                                mPayCredit._Cr16_wht_amt = "0000000000000000";
                                mPayCredit._Cr17_inv_det = "000000";
                                mPayCredit._Cr18_int_amt = "0000000000000000";
                                mPayCredit._Cr19_wht_pay = getSpaceText("", 1);
                                mPayCredit._Cr20_wht_rmk = getSpaceText("", 40);
                                mPayCredit._Cr21_wht_decDT = getSpaceText("", 8);
                                mPayCredit._Cr22_rec_cd = "014";
                                mPayCredit._Cr23_rec_name = getSpaceText("SIAM COMMERCIAL BANK", 35);
                                mPayCredit._Cr24_rec_brn_cd = getLeadZeroText(drPRDT["BANKAC"].ToString().Substring(0,3), 4, 0);   // ****** Emp Bank Acc Branch ********
                                mPayCredit._Cr25_rec_brn_name = getSpaceText("Bank: 014 Br.: "+ getLeadZeroText(drPRDT["BANKAC"].ToString().Substring(0, 3), 4, 0), 35);   // ****** Emp Bank Name Branch ********
                                mPayCredit._Cr26_wht_sign = "B";
                                mPayCredit._Cr27_benefic = "N";
                                mPayCredit._Cr28_cus_ref = getSpaceText("", 20);
                                mPayCredit._Cr29_cheq_ref = getSpaceText("", 1);
                                mPayCredit._Cr30_pay_type_cd = getSpaceText("", 3);
                                mPayCredit._Cr31_serv_type = "04";
                                mPayCredit._Cr32_rmk = getSpaceText("", 50);
                                mPayCredit._Cr33_scb_rmk = getSpaceText("", 18);
                                mPayCredit._Cr34_benefic_chrg = getSpaceText("", 2);
                                sw.WriteLine(String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}",
                                    mPayCredit._Cr01_type, mPayCredit._Cr02_seq, mPayCredit._Cr03_acc, mPayCredit._Cr04_amt, mPayCredit._Cr05_curr, mPayCredit._Cr06_ref, mPayCredit._Cr07_wht, mPayCredit._Cr08_inv_pre, mPayCredit._Cr09_adv_req, mPayCredit._Cr10_del_mode,
                                    mPayCredit._Cr11_pick, mPayCredit._Cr12_wht_frm, mPayCredit._Cr13_wht_tax, mPayCredit._Cr14_wht_att, mPayCredit._Cr15_wht_no, mPayCredit._Cr16_wht_amt, mPayCredit._Cr17_inv_det, mPayCredit._Cr18_int_amt, mPayCredit._Cr19_wht_pay,
                                    mPayCredit._Cr20_wht_rmk, mPayCredit._Cr21_wht_decDT, mPayCredit._Cr22_rec_cd, mPayCredit._Cr23_rec_name, mPayCredit._Cr24_rec_brn_cd, mPayCredit._Cr25_rec_brn_name, mPayCredit._Cr26_wht_sign, mPayCredit._Cr27_benefic,
                                    mPayCredit._Cr28_cus_ref, mPayCredit._Cr29_cheq_ref, mPayCredit._Cr30_pay_type_cd, mPayCredit._Cr31_serv_type, mPayCredit._Cr32_rmk, mPayCredit._Cr33_scb_rmk, mPayCredit._Cr34_benefic_chrg));


                                //******* Payee *********
                                PayrollSendBankInfo.PayrollSendBankPayee mPayee = new PayrollSendBankInfo.PayrollSendBankPayee();
                                mPayee._Py01_type = "004";
                                mPayee._Py02_ref = "00000001";
                                mPayee._Py03_seq = getLeadZeroText(idx.ToString(), 6, 0);           // ****** Run Nbr ********
                                mPayee._Py04_id_card = "000000000000000";                           // ****** Personal Card ID ********
                                mPayee._Py05_name_th = getSpaceText(drPRDT["NAME"].ToString(), 100);   // ****** Employee Name ********
                                mPayee._Py06_addr1 = getSpaceText("", 70);
                                mPayee._Py07_addr2 = getSpaceText("", 70);
                                mPayee._Py08_addr3 = getSpaceText("", 70);
                                mPayee._Py09_taxId = getSpaceText("", 10);
                                mPayee._Py10_name_en = getSpaceText("", 70);
                                mPayee._Py11_fax_nbr = "0000000000";
                                mPayee._Py12_mobile = "0000000000";
                                mPayee._Py13_email = getSpaceText("", 64);
                                mPayee._Py14_payee2_name = getSpaceText("", 100);
                                mPayee._Py15_payee2_addr1 = getSpaceText("", 70);
                                mPayee._Py16_payee2_addr2 = getSpaceText("", 70);
                                mPayee._Py17_payee2_addr3 = getSpaceText("", 70);
                                sw.WriteLine(String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}",
                                    mPayee._Py01_type, mPayee._Py02_ref, mPayee._Py03_seq, mPayee._Py04_id_card, mPayee._Py05_name_th, mPayee._Py06_addr1, mPayee._Py07_addr2, mPayee._Py08_addr3,
                                    mPayee._Py09_taxId, mPayee._Py10_name_en, mPayee._Py11_fax_nbr, mPayee._Py12_mobile, mPayee._Py13_email, mPayee._Py14_payee2_name, mPayee._Py15_payee2_addr1,
                                    mPayee._Py16_payee2_addr2, mPayee._Py17_payee2_addr3));






                                idx++;
                            } // end foreach 





                            //******* Trailer *********
                            PayrollSendBankInfo.PayrollSendBankTrailer mPayTailer = new PayrollSendBankInfo.PayrollSendBankTrailer();
                            mPayTailer._Tr01_type = "999";
                            mPayTailer._Tr02_ttl_debit = "000001";
                            mPayTailer._Tr03_ttl_credit = getLeadZeroText(dtPRDT.Rows.Count.ToString(), 6, 0);    //***** record 8 digit *****
                            mPayTailer._Tr04_ttl_amt = getLeadZeroText(dtSum.Rows[0]["Snet"].ToString(), 16, 2); //**** pay amt 16 digit *****
                            sw.WriteLine(String.Format("{0}{1}{2}{3}",
                                mPayTailer._Tr01_type, mPayTailer._Tr02_ttl_debit, mPayTailer._Tr03_ttl_credit, mPayTailer._Tr04_ttl_amt));




                        } // end write file


                        MessageBox.Show("Write Send Bank file OK!");
                    }
                    catch (Exception Ex)
                    {
                        Console.WriteLine(Ex.ToString());
                    }
                } // end if more than one row
                else
                {
                    MessageBox.Show("No payroll data in date : " + dpkrPayroll.Value.ToString("dd/MMM/yyyy"));
                }

            } // end select dest file
        }

        private void btnBrowseSpecial_Click(object sender, EventArgs e)
        {
            OpenFileDialog flDrg = new OpenFileDialog();
            flDrg.Filter = "Excel File|*.xlsx";

            if (flDrg.ShowDialog(this) != DialogResult.Cancel)
            {
                txtBrowseSpecial.Text = flDrg.FileName;
            }
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

        public DataTable ReadExcelFileSalaryBonus(string filePath)
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
                            dt.Columns[col].ColumnName = (dt.Rows[0][col].ToString().Trim() !="") ? dt.Rows[0][col].ToString() : $"COL{col.ToString()}";
                            //dt.Columns[col].ColumnName = $"COL{col.ToString()}";
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



        private void btnUploadSpecial_Click(object sender, EventArgs e)
        {
            

            // path to your excel file
            string path = txtBrowseSpecial.Text;

            DataTable dtData = ReadExcelFile(path);

            DataTable dtDataSpecial = new DataTable();
            dtDataSpecial.Columns.Add("code", typeof(string));
            dtDataSpecial.Columns.Add("pdate", typeof(DateTime));
            dtDataSpecial.Columns.Add("pay", typeof(decimal));



            bool _error = false;
            string _errStr = "";
            if(dtData.Rows.Count > 0 && dtData.Columns.Count == 4)
            {
                // loop through the worksheet rows and columns

                foreach (DataRow dr in dtData.Rows)
                {
                    string _no = ""; 
                    string _code = ""; 
                    string _pdate = ""; 
                    string _pay = "";

                    try { _no = dr[0].ToString(); } catch { }
                    try { _code = dr[1].ToString(); } catch { }
                    try { _pdate = dr[2].ToString(); } catch { }
                    try { _pay = dr[3].ToString(); } catch { }

                    DateTime PDate = new DateTime(1900, 1, 1);
                    decimal Pay = 0;

                    if(_code != "" && _pdate != "" && _pay != "")
                    {
                        //****** Get & Check Employee *******
                        DataTable dtEmp = new DataTable();
                        string strEmp = @"SELECT * FROM Employee WHERE Code=@Code ";
                        SqlCommand cmdEmp = new SqlCommand();
                        cmdEmp.CommandText = strEmp;
                        cmdEmp.Parameters.Add(new SqlParameter("@Code", _code));
                        dtEmp = oConHRM.Query(cmdEmp);


                        //**** Check Employee ****
                        if(dtEmp.Rows.Count > 0)
                        {
                        
                            try
                            {
                                PDate = new DateTime(Convert.ToInt32(_pdate.Substring(0, 4)),
                                                       Convert.ToInt32(_pdate.Substring(4, 2)),
                                                       Convert.ToInt32(_pdate.Substring(6, 2)));
                            }
                            catch
                            {
                                _errStr += _code + " : error date." + System.Environment.NewLine;
                                _error = true;
                            }

                            try
                            {
                                Pay = Convert.ToDecimal(_pay);
                            }
                            catch
                            {
                                _errStr += _code + " : error Pay." + System.Environment.NewLine;
                                _error = true;
                            }

                            if (!_error)
                            {
                                dtDataSpecial.Rows.Add(_code, PDate, Pay);
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
                if (!_error && dtDataSpecial.Rows.Count > 0)
                {
                    //******************************************
                    //******* Group By DataTable & Sum *********
                    
                    DataTable dtGrpData = dtDataSpecial.AsEnumerable().GroupBy(g => new { code = g.Field<string>("code"), pdate = g.Field<DateTime>("pdate") })
                        .Select(s =>
                        {
                            DataRow drNew = dtDataSpecial.NewRow();
                            drNew["code"] = s.Key.code;
                            drNew["pdate"] = s.Key.pdate;
                            drNew["pay"] = s.Sum(d => d.Field<decimal>("pay"));
                            return drNew;
                        }).CopyToDataTable();
                    //******* Group By DataTable & Sum *********
                    //******************************************



                    DateTime HPDate = new DateTime(1900, 1, 1);


                    // loop for
                    foreach (DataRow dr in dtGrpData.Rows)
                    {
                        string _code = "";
                        string _pdate = "";
                        string _pay = "";
                        

                        try { _code = dr["code"].ToString(); } catch { }
                        try { _pdate = dr["pdate"].ToString(); } catch { }
                        try { _pay = dr["pay"].ToString(); } catch { }

                        if (_code != "" && _pdate != "" && _pay != "")
                        {
                            DateTime PDate = new DateTime(1900, 1, 1);
                            decimal Pay = 0;
                            try
                            {
                                PDate = Convert.ToDateTime(_pdate);
                            }
                            catch { }

                            try
                            {
                                Pay = Convert.ToDecimal(_pay);
                            }
                            catch { }


                            //***** Check ******
                            DataTable dtChk = new DataTable();
                            string strChk = @"SELECT * FROM PRSPECIAL WHERE PDATE='" + PDate.ToString("dd/MMM/yyyy") + "' AND CODE='" + _code + "' ";
                            OracleCommand cmdChk = new OracleCommand();
                            cmdChk.CommandText = strChk;
                            dtChk = oOraDCI.Query(cmdChk);

                            if (dtChk.Rows.Count > 0)
                            {
                                string strUpd = @"UPDATE PRSPECIAL SET PAY='" + _pay.ToString() + "' WHERE PDATE='" + PDate.ToString("dd/MMM/yyyy") + "' AND CODE='" + _code + "' ";
                                OracleCommand cmdUpd = new OracleCommand();
                                cmdUpd.CommandText = strUpd;
                                oOraDCI.ExecuteCommand(cmdUpd);

                            }
                            else
                            {
                                DataTable dtEmp = new DataTable();
                                string strEmp = @"SELECT WTYPE, TAXNO, INSUNO, BANK, BANKAC FROM EMPM WHERE CODE='" + _code + "' ";
                                OracleCommand cmdEmp = new OracleCommand();
                                cmdEmp.CommandText = strEmp;
                                dtEmp = oOraDCI.Query(cmdEmp);


                                string strInstr = @"INSERT INTO PRSPECIAL (PDATE, CODE, WTYPE, PAY, TAXNO, INSUNO, BANK, BANKAC) 
                                VALUES ('" + PDate.ToString("dd/MMM/yyyy") + "','" + _code + "','" + dtEmp.Rows[0]["WTYPE"].ToString() + @"', 
                                    '" + _pay.ToString() + "', '" + dtEmp.Rows[0]["TAXNO"].ToString() + "', '" + dtEmp.Rows[0]["INSUNO"].ToString() + @"',
                                    '" + dtEmp.Rows[0]["BANK"].ToString() + "', '" + dtEmp.Rows[0]["BANKAC"].ToString() + "' ) ";
                                OracleCommand cmdInstr = new OracleCommand();
                                cmdInstr.CommandText = strInstr;
                                oOraDCI.ExecuteCommand(cmdInstr);

                            }

                            //**************
                            HPDate = PDate;

                        } // end if check

                        


                    }// end foreach

                    MessageBox.Show("Successful!");


                    //********************************
                    dgvSpecial.Rows.Clear();

                    DataTable dtGet = new DataTable();
                    string strGet = @"SELECT PDATE, CODE, WTYPE, PAY, TAXNO, INSUNO, BANK, BANKAC FROM PRSPECIAL WHERE PDATE='" + HPDate.ToString("dd/MMM/yyyy") + "' ";
                    OracleCommand cmdGet = new OracleCommand();
                    cmdGet.CommandText = strGet;
                    dtGet = oOraDCI.Query(cmdGet);

                    if(dtGet.Rows.Count > 0)
                    {
                        decimal _totalPay = 0;
                        foreach (DataRow dr in dtGet.Rows)
                        {
                            _totalPay += Convert.ToDecimal(dr["PAY"].ToString());
                            dgvSpecial.Rows.Add(dr["PDATE"].ToString(), dr["CODE"].ToString(), dr["WTYPE"].ToString(), dr["BANK"].ToString(), dr["BANKAC"].ToString(), dr["PAY"].ToString() );
                        }

                        dgvSpecial.Rows.Add( "TOTAL", "", "", "", "", _totalPay.ToString() );
                    }
                    
                    //********************************

                } // end check error
                else
                {
                    MessageBox.Show(_errStr, "Error", MessageBoxButtons.OK ,MessageBoxIcon.Asterisk);
                }
                

            }
            
        }

        private void btnSendBankForm_Click(object sender, EventArgs e)
        {
            HideGroupBoxes();
            grbSendBank.Visible = true;
        }

        private void btnSpecialReset_Click(object sender, EventArgs e)
        {
            dgvSpecial.Rows.Clear();
            txtBrowseSpecial.Text = "";
        }

        private void btnSendBankSpecialForm_Click(object sender, EventArgs e)
        {
            HideGroupBoxes();
            grbSendBankSpecialUpload.Visible = true;



        }

        private void btnExcelTemplateSpecial_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                string sourceFilePath = Application.StartupPath + "\\TEMPLATE\\TMP_PAY_SPECIAL.xlsx";

                saveFileDialog.Title = "Save File";
                saveFileDialog.FileName = "Pay_Special_Template.xlsx";
                saveFileDialog.Filter = "Excel Files|*.xlsx"; // Specify the file filters

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

        private void btnTaxForm_Click(object sender, EventArgs e)
        {
            HideGroupBoxes();
            grbTax.Visible = true;

        }

        private void btnTaxGen_Click(object sender, EventArgs e)
        {

            if (cbTaxType.SelectedValue.ToString() == "PGD1")
            {
                #region PDG1
                saveFileDlg.FileName = "tax_pgd1_data.txt";
                saveFileDlg.RestoreDirectory = true;
                saveFileDlg.Filter = "Text Files (.txt)|*.txt;";
                DialogResult dlg = saveFileDlg.ShowDialog();
                if (dlg == DialogResult.OK)
                {
                    string fileName = saveFileDlg.FileName;                    
                    DateTime dateTax = new DateTime(1900, 1, 1);
                    dateTax = dpkrTaxDate.Value.Date;

                    DataTable dtPRDT = new DataTable();
                    string strPRDT = @"SELECT P.code,tpren,tname,tsurn,pren,name,surn,join,E.idno,SONS,SONB,E.taxno,tcaddr1,tcaddr2,tcaddr3,tcaddr4,marry,sons,P.inc,P.tax 
                                    FROM PRDT P 
                                    LEFT JOIN EMPM E ON E.code = P.code  
                                    WHERE PDATE='" + dateTax.ToString("dd/MMM/yyyy") + "' and inc <> '0'  order by P.code  ";
                    OracleCommand cmdPRDT = new OracleCommand();
                    cmdPRDT.CommandText = strPRDT;
                    dtPRDT = oOraDCI.Query(cmdPRDT);


                    try
                    {
                        // Check if file already exists. If yes, delete it.     
                        if (File.Exists(fileName))
                        {
                            File.Delete(fileName);
                        }

                        // Create a new file
                        using (StreamWriter sw = File.CreateText(fileName))
                        {
                            int no = 1;
                            foreach (DataRow drPRDT in dtPRDT.Rows)
                            {
                                PayrollTaxInfo.PayrollTaxPDG1 mTax = new PayrollTaxInfo.PayrollTaxPDG1();

                                decimal _inc = 0, _tax = 0;
                                _inc = Convert.ToDecimal(drPRDT["INC"].ToString());
                                _tax = Convert.ToDecimal(drPRDT["tax"].ToString());
                                mTax._Tax01_code = "401N";
                                mTax._Tax02_compTax = "0105544013305";
                                mTax._Tax03_type = "1";
                                mTax._Tax04_no = no.ToString("00000");
                                mTax._Tax05_idno = drPRDT["taxno"].ToString(); //drPRDT["idno"].ToString();
                                mTax._Tax06_pren = DecodeLanguage(drPRDT["tpren"].ToString());
                                mTax._Tax07_name = DecodeLanguage(drPRDT["tname"].ToString());
                                mTax._Tax08_surn = DecodeLanguage(drPRDT["tsurn"].ToString());
                                mTax._Tax09_date = dateTax.ToString("yyyy-MM-dd");
                                mTax._Tax10_inc = _inc.ToString("0.00");
                                mTax._Tax11_tax = _tax.ToString("0.00");
                                mTax._Tax12_close = "1";
                                sw.WriteLine(String.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}",
                                        mTax._Tax01_code, mTax._Tax02_compTax, mTax._Tax03_type, mTax._Tax04_no,
                                        mTax._Tax05_idno, mTax._Tax06_pren, mTax._Tax07_name, mTax._Tax08_surn,
                                        mTax._Tax09_date, mTax._Tax10_inc, mTax._Tax11_tax, mTax._Tax12_close));

                                no++;
                            } // end foreach

                            MessageBox.Show("Successful !");

                        } // end using
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                #endregion

            }
            else if (cbTaxType.SelectedValue.ToString() == "PGD1K")
            {
                #region PDG1K
                saveFileDlg.FileName = "tax_pgd1K_data.txt";
                saveFileDlg.RestoreDirectory = true;
                saveFileDlg.Filter = "Text Files (.txt)|*.txt;";
                DialogResult dlg = saveFileDlg.ShowDialog();
                if (dlg == DialogResult.OK)
                {
                    string fileName = saveFileDlg.FileName;
                    DateTime dateTax = new DateTime(1900, 1, 1);
                    dateTax = dpkrTaxDate.Value.Date;

                    DateTime dateEND = new DateTime(dateTax.Year, 12, 31);
                    DateTime dateSTART = new DateTime(dateTax.Year-1, 12, 16);




                    DataTable dtEMP = new DataTable();
                    string strEMP = @"SELECT em.code, em.tpren, em.tname, em.tsurn, em.pren, em.name, em.surn, 
                                             em.tpren || ' ' || em.tname || '  ' || em.tsurn as fname, em.sect, 
                                             em.dv_ename, em.grpot, em.currentaddress, em.homeaddress, em.bus,
                                             em.STOP, emm.idno, emm.taxno, emm.marry,
                                            em.TCADDR1, em.TCADDR2, em.TCADDR3, em.TCADDR4, em.EHPART, 
                                             (SELECT COUNT (*) FROM empm_family e
                                             WHERE e.relation = 'RELA4' AND e.taxdeduct <> 0 AND e.code = em.code) AS CHILD,
                                             (SELECT COUNT (*) FROM empm_family e
                                             WHERE e.relation = 'RELA5' AND e.taxdeduct <> 0 AND e.code = em.code) AS learningchild, emm.insur, emm.loan, emm.HANDYCAP , emm.LTF
                            FROM vi_emp_mstr em, empm emm 
                            WHERE  (emm.resign >= '" + dateSTART.ToString("dd/MMM/yyyy") + @"' or emm.resign is null ) AND emm.join < '" + dateEND.ToString("dd/MMM/yyyy") + @"' AND em.code = emm.code  
                                 
                            ORDER BY  em.code   ";
                    OracleCommand cmdEMP = new OracleCommand();
                    cmdEMP.CommandText = strEMP;
                    dtEMP = oOraDCI.Query(cmdEMP);


                    try
                    {
                        // Check if file already exists. If yes, delete it.     
                        if (File.Exists(fileName))
                        {
                            File.Delete(fileName);
                        }

                        // Create a new file
                        using (StreamWriter sw = File.CreateText(fileName))
                        {
                            int no = 1;
                            foreach (DataRow drEMP in dtEMP.Rows)
                            {
                                //**** PRDT *****
                                DataTable dtPRDT = new DataTable();
                                string strPRDT = @"select  nvl(sum(inc),0) as inacc , nvl(sum(tax),0) as taxacc  from prdt where pdate between '01/JAN/" + dateTax.Year.ToString() + "' and '01/DEC/" + dateTax.Year.ToString() + " ' AND CODE='" + drEMP["code"].ToString() + "' order by pdate ";
                                OracleCommand cmdPRDT = new OracleCommand();
                                cmdPRDT.CommandText = strPRDT;
                                dtPRDT = oOraDCI.Query(cmdPRDT);



                                PayrollTaxInfo.PayrollTaxPDG1K mTax = new PayrollTaxInfo.PayrollTaxPDG1K();

                                decimal _inacc = 0, _taxacc = 0;
                                _inacc = Convert.ToDecimal(dtPRDT.Rows[0]["inacc"].ToString());
                                _taxacc = Convert.ToDecimal(dtPRDT.Rows[0]["taxacc"].ToString());



                                int _child = 0, _lernchild = 0;
                                _child = Convert.ToInt32(drEMP["CHILD"].ToString());
                                _lernchild = Convert.ToInt32(drEMP["learningchild"].ToString());

                                
                                //try { _child = Convert.ToInt32(drEMP["CHILD"].ToString()); }catch(Exception ex) { MessageBox.Show(drEMP["code"].ToString()+ " | " + ex.ToString());  }
                                //try
                                //{
                                //    _lernchild = Convert.ToInt32(drEMP["learningchild"].ToString());
                                //}
                                //catch (Exception ex) { MessageBox.Show(drEMP["code"].ToString() + " | " + ex.ToString()); }


                                mTax._Tax01 = "401N";
                                mTax._Tax02 = "0105544013305";
                                mTax._Tax03 = "1";
                                mTax._Tax04 = no.ToString("00000");
                                mTax._Tax05 = drEMP["taxno"].ToString(); //drEMP["idno"].ToString();
                                mTax._Tax06 = DecodeLanguage(drEMP["tpren"].ToString());
                                mTax._Tax07 = DecodeLanguage(drEMP["tname"].ToString());
                                mTax._Tax08 = DecodeLanguage(drEMP["tsurn"].ToString());
                                mTax._Tax09 = dateEND.ToString("yyyy-MM-dd");

                                mTax._Tax10 = _inacc.ToString("0.00");
                                mTax._Tax11 = _taxacc.ToString("0.00");
                                mTax._Tax12 = "1";

                                mTax._Tax13 = drEMP["marry"].ToString();

                                mTax._Tax14 = (_child + _lernchild).ToString();
                                mTax._Tax15 = "เงินเดือน";
                                mTax._Tax16 = dateEND.ToString("dd/MM/yyyy");
                                mTax._Tax17 = "ค";

                                mTax._Tax18 = (drEMP["TCADDR1"].ToString() != "" ) ? DecodeLanguage(drEMP["TCADDR1"].ToString()) : "1";

                                mTax._Tax19 = DecodeLanguage(drEMP["TCADDR2"].ToString());
                                mTax._Tax20 = DecodeLanguage(drEMP["TCADDR3"].ToString());
                                mTax._Tax21 = DecodeLanguage(drEMP["TCADDR4"].ToString());
                                mTax._Tax22 = DecodeLanguage(drEMP["EHPART"].ToString());

                                sw.WriteLine(String.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}|{16}|{17}|{18}|{19}|{20}|{21}",
                                        mTax._Tax01, mTax._Tax02, mTax._Tax03, mTax._Tax04, mTax._Tax05, mTax._Tax06, mTax._Tax07, mTax._Tax08, mTax._Tax09, mTax._Tax10,
                                        mTax._Tax11, mTax._Tax12, mTax._Tax13, mTax._Tax14, mTax._Tax15, mTax._Tax16, mTax._Tax17, mTax._Tax18, mTax._Tax19, mTax._Tax20,
                                        mTax._Tax21, mTax._Tax22 ));

                                no++;
                            } // end foreach

                            MessageBox.Show("Successful !");

                        } // end using
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                #endregion

            }
            else if (cbTaxType.SelectedValue.ToString() == "PGD91")
            {
                #region PDG91
                saveFileDlg.FileName = "tax_pgd91_data.txt";
                saveFileDlg.RestoreDirectory = true;
                saveFileDlg.Filter = "Text Files (.txt)|*.txt;";
                DialogResult dlg = saveFileDlg.ShowDialog();
                if (dlg == DialogResult.OK)
                {
                    string fileName = saveFileDlg.FileName;
                    
                    DateTime dateTax = new DateTime(1900, 1, 1);
                    dateTax = dpkrTaxDate.Value.Date;

                    DataTable dtPRDT = new DataTable();
                    string strPRDT = @"SELECT p.*, e.pren, e.name, e.surn, e.tpren, e.tname, e.tsurn, e.marry, e.insur, e.ltf, e.interest, e.donate 
                                        FROM prdt p 
                                        LEFT JOIN empm e on p.code = e.code  
                                        WHERE e.resign is null and p.code not like '0%' and pdate='" + dateTax.ToString("dd/MMM/yyyy") + "'  and incacc > 50000 and taxacc = 0 ";
                    OracleCommand cmdPRDT = new OracleCommand();
                    cmdPRDT.CommandText = strPRDT;
                    dtPRDT = oOraDCI.Query(cmdPRDT);


                    try
                    {
                        // Check if file already exists. If yes, delete it.     
                        if (File.Exists(fileName))
                        {
                            File.Delete(fileName);
                        }

                        // Create a new file
                        using (StreamWriter sw = File.CreateText(fileName))
                        {
                            int no = 1;
                            foreach (DataRow drPRDT in dtPRDT.Rows)
                            {
                                //**** Parent Family *****
                                DataTable dtFamily = new DataTable();
                                string strFamily = @"SELECT * FROM empm_family  WHERE code ='" + drPRDT["code"].ToString() + "' AND RELATION NOT IN ('RELA4','RELA5') ";
                                OracleCommand cmdFamily = new OracleCommand();
                                cmdFamily.CommandText = strFamily;
                                dtFamily = oOraDCI.Query(cmdFamily);


                                //**** Child 30000 Family *****
                                DataTable dtChild30 = new DataTable();
                                string strChild30 = @"SELECT * FROM empm_family  WHERE code ='" + drPRDT["code"].ToString() + "' AND RELATION IN ('RELA4','RELA5') AND taxdeduct = '30000'  ";
                                OracleCommand cmdChild30 = new OracleCommand();
                                cmdChild30.CommandText = strChild30;
                                dtChild30 = oOraDCI.Query(cmdChild30);


                                //**** Child 60000 Family *****
                                DataTable dtChild60 = new DataTable();
                                string strChild60 = @"SELECT * FROM empm_family  WHERE code ='" + drPRDT["code"].ToString() + "' AND RELATION IN ('RELA4','RELA5') AND taxdeduct = '60000'  ";
                                OracleCommand cmdChild60 = new OracleCommand();
                                cmdChild60.CommandText = strChild60;
                                dtChild60 = oOraDCI.Query(cmdChild60);



                                PayrollTaxInfo.PayrollTaxPDG91 mTax = new PayrollTaxInfo.PayrollTaxPDG91();
                                mTax._Tax01 = drPRDT["code"].ToString();
                                mTax._Tax02 = drPRDT["taxno"].ToString();  //drPRDT["idno"].ToString();
                                mTax._Tax03 = DecodeLanguage(drPRDT["tpren"].ToString());
                                mTax._Tax04 = DecodeLanguage(drPRDT["tname"].ToString());
                                mTax._Tax05 = DecodeLanguage(drPRDT["tsurn"].ToString());
                                if (drPRDT["marry"].ToString() == "S")
                                {
                                    mTax._Tax06 = "0";
                                    mTax._Tax07 = "1";
                                }
                                else if (drPRDT["marry"].ToString() == "M")
                                {
                                    mTax._Tax06 = "1";
                                    mTax._Tax07 = "3";
                                }
                                else if (drPRDT["marry"].ToString() == "H")
                                {
                                    mTax._Tax06 = "2";
                                    mTax._Tax07 = "2";
                                }


                                mTax._Tax08 = "";
                                mTax._Tax09 = "";
                                mTax._Tax10 = "";
                                mTax._Tax11 = "";




                                //***Get Parent Family****
                                #region Parent Family

                                mTax._Tax34 = "0"; //' Father
                                mTax._Tax35 = ""; //' Father
                                mTax._Tax36 = "0"; //' Mother
                                mTax._Tax37 = ""; //' Mother
                                mTax._Tax38 = "0"; //' Wife Father
                                mTax._Tax39 = ""; //' Wife Father
                                mTax._Tax40 = "0"; //' Wife Mother
                                mTax._Tax41 = ""; //' Wife Mother
                                mTax._Tax46 = "0"; //' Tax Discount
                                mTax._Tax47 = "0";


                                if (dtFamily.Rows.Count > 0)
                                {
                                    foreach (DataRow drFamily in dtFamily.Rows)
                                    {
                                        decimal _taxDeduct = 0;
                                        long _idno = 0;

                                        try{
                                            _taxDeduct = Convert.ToDecimal(drFamily["taxdeduct"].ToString());
                                        }catch { }

                                        try{
                                            _idno = Convert.ToInt64(drFamily["id_no"].ToString());
                                        }catch { }



                                        //'******* Father *******
                                        if (drFamily["relation"].ToString() == "RELA1")
                                        {
                                            if(_taxDeduct > 0)
                                            {
                                                mTax._Tax34 = drFamily["taxdeduct"].ToString();
                                                mTax._Tax35 = _idno.ToString("0000000000000");
                                                mTax._Tax46 = (Convert.ToDecimal(mTax._Tax46) + _taxDeduct).ToString("0.00");
                                            }                                            
                                        }

                                        //'******* Mother *******
                                        else if (drFamily["relation"].ToString() == "RELA2")
                                        {
                                            if (_taxDeduct > 0)
                                            {
                                                mTax._Tax36 = drFamily["taxdeduct"].ToString();
                                                mTax._Tax37 = _idno.ToString("0000000000000");
                                                mTax._Tax46 = (Convert.ToDecimal(mTax._Tax46) + _taxDeduct).ToString("0.00");
                                            }
                                        }

                                        //'******* Wife *******
                                        else if (drFamily["relation"].ToString() == "RELA3")
                                        {
                                            if (drPRDT["marry"].ToString() != "S")
                                            {
                                                mTax._Tax08 = _idno.ToString("0000000000000");
                                                mTax._Tax09 = DecodeLanguage(drFamily["PREN"].ToString());
                                                mTax._Tax10 = DecodeLanguage(drFamily["Name"].ToString());
                                                mTax._Tax11 = DecodeLanguage(drFamily["SURN"].ToString());
                                                if (_taxDeduct > 0)
                                                {
                                                    mTax._Tax46 = (Convert.ToDecimal(mTax._Tax46) + _taxDeduct).ToString("0.00");
                                                }                                                
                                            }
                                        }

                                        //'******* Father Wife *******
                                        else if (drFamily["relation"].ToString() == "RELA6")
                                        {
                                            if (_taxDeduct > 0)
                                            {
                                                mTax._Tax38 = drFamily["taxdeduct"].ToString();
                                                mTax._Tax39 = _idno.ToString("0000000000000");
                                                mTax._Tax46 = (Convert.ToDecimal(mTax._Tax46) + _taxDeduct).ToString("0.00");
                                            }
                                        }

                                        //'******* Mother Wife *******
                                        else if (drFamily["relation"].ToString() == "RELA7")
                                        {
                                            if (_taxDeduct > 0)
                                            {
                                                mTax._Tax40 = drFamily["taxdeduct"].ToString();
                                                mTax._Tax41 = _idno.ToString("0000000000000");
                                                mTax._Tax46 = (Convert.ToDecimal(mTax._Tax46) + _taxDeduct).ToString("0.00");
                                            }
                                        }

                                    }// end foreach Get Parent Family
                                }
                                #endregion



                                //***** Income ******
                                decimal _incacc = 0;
                                try { _incacc = Convert.ToDecimal(drPRDT["incacc"].ToString()); } catch { }
                                mTax._Tax12 = _incacc.ToString("0.00");

                                //***** Tax, Pay Tax  *****
                                decimal _taxacc = 0;
                                try { _taxacc = Convert.ToDecimal(drPRDT["taxacc"].ToString()); } catch { }
                                mTax._Tax13 = _taxacc.ToString("0.00");
                                mTax._Tax14 = "0";


                                //****** Provident Fund ****
                                decimal _provacc = 0;
                                try { _provacc = Convert.ToDecimal(drPRDT["provacc"].ToString()); } catch { }
                                if (_provacc == 0)
                                {
                                    mTax._Tax15 = "";
                                    mTax._Tax16 = "";
                                }
                                else if (_provacc > 10000) {
                                    mTax._Tax15 = (_provacc - 10000).ToString("0.00");
                                    mTax._Tax16 = (10000).ToString("0.00");
                                }
                                else if(_provacc <= 10000)
                                {
                                    mTax._Tax15 = "";
                                    mTax._Tax16 = _provacc.ToString("0.00");
                                }
                                mTax._Tax17 = "";



                                //*** Get Child 30000 Family ****
                                #region Child 30000 Family

                                mTax._Tax18 = "0";
                                mTax._Tax19 = "0";
                                mTax._Tax20 = "";
                                mTax._Tax21 = "";
                                mTax._Tax22 = "";
                                mTax._Tax23 = "";
                                mTax._Tax24 = "";
                                mTax._Tax25 = "";
                                mTax._Tax26 = "";

                                int cntChild30 = 0;
                                if (dtChild30.Rows.Count > 0)
                                {
                                    int idx = 1;                                    
                                    foreach (DataRow drChild in dtChild30.Rows)
                                    {
                                        decimal _taxDeduct = 0;
                                        long _idno = 0;

                                        try
                                        {
                                            _taxDeduct = Convert.ToDecimal(drChild["taxdeduct"].ToString());
                                        }
                                        catch { }

                                        try
                                        {
                                            _idno = Convert.ToInt64(drChild["id_no"].ToString());
                                        }
                                        catch { }

                                        if(_taxDeduct > 0)
                                        {
                                            switch (idx)
                                            {
                                                case 1: mTax._Tax20 = _idno.ToString("0000000000000"); break;
                                                case 2: mTax._Tax21 = _idno.ToString("0000000000000"); break;
                                                case 3: mTax._Tax22 = _idno.ToString("0000000000000"); break;
                                                case 4: mTax._Tax23 = _idno.ToString("0000000000000"); break;
                                                case 5: mTax._Tax24 = _idno.ToString("0000000000000"); break;
                                                case 6: mTax._Tax25 = _idno.ToString("0000000000000"); break;
                                                case 7: mTax._Tax26 = _idno.ToString("0000000000000"); break;
                                            }

                                            cntChild30++;
                                        }
                                        idx++;
                                    }// end foreach
                                }

                                mTax._Tax18 = cntChild30.ToString();
                                mTax._Tax19 = (cntChild30 * 30000).ToString("0.00");
                                mTax._Tax46 = (Convert.ToDecimal(mTax._Tax46) + (cntChild30 * 30000)).ToString("0.00");
                                #endregion
                                //end Get Child Family



                                //*** Get Child 60000 Family ****
                                #region Child 60000 Family

                                mTax._Tax27 = "0";
                                mTax._Tax28 = "0";
                                mTax._Tax29 = "";
                                mTax._Tax30 = "";
                                mTax._Tax31 = "";
                                mTax._Tax32 = "";
                                mTax._Tax33 = "";

                                int cntChild60 = 0;
                                if (dtChild60.Rows.Count > 0)
                                {
                                    int idx = 1;
                                    foreach (DataRow drChild in dtChild60.Rows)
                                    {
                                        decimal _taxDeduct = 0;
                                        long _idno = 0;

                                        try
                                        {
                                            _taxDeduct = Convert.ToDecimal(drChild["taxdeduct"].ToString());
                                        }
                                        catch { }

                                        try
                                        {
                                            _idno = Convert.ToInt64(drChild["id_no"].ToString());
                                        }
                                        catch { }

                                        if (_taxDeduct > 0)
                                        {
                                            switch (idx)
                                            {
                                                case 1: mTax._Tax29 = _idno.ToString("0000000000000"); break;
                                                case 2: mTax._Tax30 = _idno.ToString("0000000000000"); break;
                                                case 3: mTax._Tax31 = _idno.ToString("0000000000000"); break;
                                                case 4: mTax._Tax32 = _idno.ToString("0000000000000"); break;
                                                case 5: mTax._Tax33 = _idno.ToString("0000000000000"); break;
                                            }

                                            cntChild60++;
                                        }
                                        idx++;
                                    }// end foreach
                                }

                                mTax._Tax27 = cntChild60.ToString();
                                mTax._Tax28 = (cntChild60 * 60000).ToString("0.00");
                                mTax._Tax46 = (Convert.ToDecimal(mTax._Tax46) + (cntChild60 * 60000)).ToString("0.00");
                                #endregion
                                //end Get Child Family



                                //***** Insurance ******
                                decimal _insur = 0;
                                try { _insur = Convert.ToDecimal(drPRDT["insur"].ToString()); } catch { }

                                if (_insur == 0)
                                {
                                    mTax._Tax42 = _insur.ToString("0.00");
                                }
                                else if (_insur > 100000)
                                {
                                    mTax._Tax42 = (100000).ToString("0.00");
                                    mTax._Tax46 = (Convert.ToDecimal(mTax._Tax46) + 100000).ToString("0.00");
                                }
                                else if(_insur <= 100000)
                                {
                                    mTax._Tax42 = _insur.ToString("0.00");
                                    mTax._Tax46 = (Convert.ToDecimal(mTax._Tax46) + _insur).ToString("0.00");
                                }


                                //***** LTF ******
                                decimal _ltf = 0;
                                try { _ltf = Convert.ToDecimal(drPRDT["LTF"].ToString()); } catch { }

                                if (_ltf == 0)
                                {
                                    mTax._Tax43 = _ltf.ToString("0.00");
                                }
                                else
                                {
                                    mTax._Tax43 = _ltf.ToString("0.00");
                                    mTax._Tax46 = (Convert.ToDecimal(mTax._Tax46) + _ltf).ToString("0.00");
                                }


                                //***** Bank Interest ******
                                decimal _interest = 0;
                                try { _interest = Convert.ToDecimal(drPRDT["interest"].ToString()); } catch { }

                                if (_interest == 0)
                                {
                                    mTax._Tax44 = _interest.ToString("0.00");
                                }
                                else 
                                {
                                    mTax._Tax44 = _interest.ToString("0.00");
                                    mTax._Tax46 = (Convert.ToDecimal(mTax._Tax46) + _interest).ToString("0.00");
                                }


                                //***** Social Fund ******
                                decimal _socacc = 0;
                                try { _socacc = Convert.ToDecimal(drPRDT["socacc"].ToString()); } catch { }

                                if (_socacc == 0)
                                {
                                    mTax._Tax45 = _socacc.ToString("0.00");
                                }
                                else
                                {
                                    mTax._Tax45 = _socacc.ToString("0.00");
                                    mTax._Tax46 = (Convert.ToDecimal(mTax._Tax46) + _socacc).ToString("0.00");
                                }


                                //***** Donate ******
                                decimal _donate = 0;
                                try { _donate = Convert.ToDecimal(drPRDT["donate"].ToString()); } catch { }

                                if (_donate == 0)
                                {
                                    mTax._Tax47 = _donate.ToString("0.00");
                                }
                                else
                                {
                                    mTax._Tax47 = _donate.ToString("0.00");
                                    mTax._Tax46 = (Convert.ToDecimal(mTax._Tax46) + _donate).ToString("0.00");
                                }


                                // ****** Discont 60,000 ******
                                mTax._Tax46 = (Convert.ToDecimal(mTax._Tax46) + 60000).ToString("0.00");




                                sw.WriteLine(String.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}|{16}|{17}|{18}|{19}|{20}|{21}|{22}|{23}|{24}|{25}|{26}|{27}|{28}|{29}|{30}|{31}|{32}|{33}|{34}|{35}|{36}|{37}|{38}|{39}|{40}|{41}|{42}|{43}|{44}|{45}|{46}",
                                        mTax._Tax01, mTax._Tax02, mTax._Tax03, mTax._Tax04, mTax._Tax05, mTax._Tax06, mTax._Tax07, mTax._Tax08, mTax._Tax09, mTax._Tax10, 
                                        mTax._Tax11, mTax._Tax12, mTax._Tax13, mTax._Tax14, mTax._Tax15, mTax._Tax16, mTax._Tax17, mTax._Tax18, mTax._Tax19, mTax._Tax20, 
                                        mTax._Tax21, mTax._Tax22, mTax._Tax23, mTax._Tax24, mTax._Tax25, mTax._Tax26, mTax._Tax27, mTax._Tax28, mTax._Tax29, mTax._Tax30, 
                                        mTax._Tax31, mTax._Tax32, mTax._Tax33, mTax._Tax34, mTax._Tax35, mTax._Tax36, mTax._Tax37, mTax._Tax38, mTax._Tax39, mTax._Tax40, 
                                        mTax._Tax41, mTax._Tax42, mTax._Tax43, mTax._Tax44, mTax._Tax45, mTax._Tax47, mTax._Tax46  ));

                                no++;
                            } // end foreach

                            MessageBox.Show("Successful !");

                        } // end using
                    }
                    catch (Exception ex) {
                        MessageBox.Show(ex.ToString());
                    }
                }
                #endregion

            }


        }

        private void btnCertGen_Click(object sender, EventArgs e)
        {
            DataTable dtEmp = new DataTable();
            string strEmp = @"SELECT * FROM Employee WHERE Code=@Code ";
            SqlCommand cmdEmp = new SqlCommand();
            cmdEmp.CommandText = strEmp;
            cmdEmp.Parameters.Add(new SqlParameter("@Code", txtCertEmp.Text));
            dtEmp = oConHRM.Query(cmdEmp);

            if(dtEmp.Rows.Count > 0)
            {
                if (cbCertType.SelectedValue.ToString() == "SALARY")
                {
                    GenerateCertSalary(txtCertEmp.Text);
                }
                else if (cbCertType.SelectedValue.ToString() == "TOS")
                {
                    GenerateCertTOS(txtCertEmp.Text);
                }
                else if (cbCertType.SelectedValue.ToString() == "OMSIN")
                {
                    GenerateCertOMSIN(txtCertEmp.Text);
                }
            }
            else
            {
                MessageBox.Show("No employee data in " + txtCertEmp.Text);
            }
            
            txtCertEmp.Text = "";

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

        private void btnCertificateForm_Click(object sender, EventArgs e)
        {
            HideGroupBoxes();
            grbCert.Visible = true;
        }
    
    
    


        public void GenerateCertSalary(string EmpCode)
        {
            //***** Employee Data ******
            DataTable dtEM = new DataTable();
            string strEM = @"SELECT * FROM empm e LEFT JOIN dict d ON d.type='POSI' and d.item = e.posit WHERE code=:code ";
            OracleCommand cmdEM = new OracleCommand();
            cmdEM.CommandText = strEM;
            cmdEM.Parameters.Add(new OracleParameter(":code", EmpCode));
            dtEM = oOraDCI.Query(cmdEM);


            DataTable dtDept = new DataTable();
            string strDept = @"select * from vi_dv_mstr where dv_cd = '" + dtEM.Rows[0]["DVCD"].ToString() + "' ";
            OracleCommand cmdDept = new OracleCommand();
            cmdDept.CommandText = strDept;
            dtDept = oOraDCI.Query(cmdDept);

            string _Dept = "", _Sect = "";
            _Dept = dtDept.Rows[0]["DEPT"].ToString();
            _Sect = dtDept.Rows[0]["SECT"].ToString();


            string DocNo = "";

            DateTime dateJoin = new DateTime(1900, 1, 1);
            try { dateJoin = Convert.ToDateTime(dtEM.Rows[0]["Join"].ToString()); }
            catch { }

            DateTime dateResign = new DateTime(1900, 1, 1);
            try { dateResign = Convert.ToDateTime(dtEM.Rows[0]["Resign"].ToString()); }
            catch { }
            //***** End Employee Data ******

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;


            string _pathFle = "";
            if (dateResign < new DateTime(2000, 1, 1))
            {
                _pathFle = Application.StartupPath + "\\TEMPLATE\\FORM_SALARY.xls";
                DocNo = GenRunNbr("HRDOC01");
            }
            else
            {
                _pathFle = Application.StartupPath + "\\TEMPLATE\\FORM_WORK.xls";
                DocNo = GenRunNbr("HRDOC03");
            }


            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(_pathFle, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            //xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets["SLIP"];

            object misValue = System.Reflection.Missing.Value;
            //xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlApp.Visible = false;
            //xlWorkBook = xlApp.Workbooks.Add(misValue);
            range = xlWorkSheet.UsedRange;
            //rw = range.Rows.Count;
            //cl = range.Columns.Count;





            (range.Cells[9, 1] as Excel.Range).Cells.Value2 = DecodeLanguage(dtEM.Rows[0]["tpren"].ToString()) + " " + DecodeLanguage(dtEM.Rows[0]["tname"].ToString()) + " " + DecodeLanguage(dtEM.Rows[0]["tsurn"].ToString());
            (range.Cells[9, 4] as Excel.Range).Cells.Value2 = dtEM.Rows[0]["code"].ToString();
            (range.Cells[10, 2] as Excel.Range).Cells.Value2 = dateJoin.ToString("dd MMMM yyyy");


            if (dateResign < new DateTime(2000, 1, 1))
            {
                (range.Cells[10, 3] as Excel.Range).Cells.Value2 = "จนถึงปัจจุบัน";
                (range.Cells[5, 1] as Excel.Range).Cells.Value2 = " เลขที่  " + DocNo;
            }
            else
            {
                (range.Cells[10, 3] as Excel.Range).Cells.Value2 = dateResign.ToString("dd MMMM yyyy");
            }


            (range.Cells[11, 4] as Excel.Range).Cells.Value2 = dtEM.Rows[0]["DESCR"].ToString();
            (range.Cells[12, 4] as Excel.Range).Cells.Value2 = _Sect;
            (range.Cells[13, 4] as Excel.Range).Cells.Value2 = _Dept;


            if (dtEM.Rows[0]["WTYPE"].ToString() == "O")
            {
                (range.Cells[14, 6] as Excel.Range).Cells.Value2 = "ต่อวัน";
                //(range.Cells[14, 4] as Excel.Range).Cells.Value2 =  "1000";
                (range.Cells[19, 4] as Excel.Range).Cells.Value2 = "750"; //  ------ Transport

                if (dtEM.Rows[0]["CODE"].ToString().StartsWith("6") || dtEM.Rows[0]["CODE"].ToString().StartsWith("7"))
                {
                    (range.Cells[18, 4] as Excel.Range).Cells.Value2 = " ";
                    (range.Cells[19, 4] as Excel.Range).Cells.Value2 = " "; //  ------ Transport
                }
            }
            else
            {
                (range.Cells[14, 6] as Excel.Range).Cells.Value2 = "ต่อเดือน";
                //(range.Cells[14, 4] as Excel.Range).Cells.Value2 =  "1000";
                (range.Cells[19, 4] as Excel.Range).Cells.Value2 = "750"; //  ------ Transport

                if (dtEM.Rows[0]["POSIT"].ToString() == "LE" || dtEM.Rows[0]["POSIT"].ToString() == "OP")
                {
                    (range.Cells[18, 4] as Excel.Range).Cells.Value2 = "1000";
                }
                else
                {
                    (range.Cells[18, 4] as Excel.Range).Cells.Value2 = " ";
                }
            }


            DataTable dtPR = new DataTable();
            string strPR = @"SELECT * FROM prdt WHERE code=:code ORDER BY pdate DESC ";
            OracleCommand cmdPR = new OracleCommand();
            cmdPR.CommandText = strPR;
            cmdPR.Parameters.Add(new OracleParameter(":code", EmpCode));
            dtPR = oOraDCI.Query(cmdPR);


            decimal CSAL = 0, CHOUSE = 0, CALLOW = 0, otsm = 0, shtsm = 0, shtamt = 0, otamt = 0;
            CSAL = ConvDecimal(dtPR.Rows[0]["SALARY"].ToString());
            CHOUSE = ConvDecimal(dtPR.Rows[0]["housing"].ToString());
            CALLOW = ConvDecimal(dtPR.Rows[0]["allow"].ToString());
            otsm = ConvDecimal(dtPR.Rows[0]["otamt"].ToString());
            shtsm = ConvDecimal(dtPR.Rows[0]["shtamt"].ToString());
            shtamt = ConvDecimal(dtPR.Rows[0]["shtamt"].ToString());
            otamt = ConvDecimal(dtPR.Rows[0]["otamt"].ToString());


            (range.Cells[15, 4] as Excel.Range).Cells.Value2 = otsm;    //'---- OT
            (range.Cells[16, 4] as Excel.Range).Cells.Value2 = shtsm;   //'---- SHIFT

            //******************************
            if (dtEM.Rows[0]["WTYPE"].ToString() == "O")
            {
                (range.Cells[14, 4] as Excel.Range).Cells.Value2 = ConvDecimal(dtEM.Rows[0]["dlrate"].ToString()).ToString("N2");
            }
            else
            {
                (range.Cells[14, 4] as Excel.Range).Cells.Value2 = CSAL.ToString("N2");
            }

            (range.Cells[17, 4] as Excel.Range).Cells.Value2 = CHOUSE.ToString("N2"); //'---- HOUSING 


            //if (dtEM.Rows[0]["POSIT"].ToString() == "LE" || dtEM.Rows[0]["POSIT"].ToString() == "OP" || dtEM.Rows[0]["POSIT"].ToString() == "DR")
            //{
            //    (range.Cells[17, 4] as Excel.Range).Cells.Value2 = "1050";
            //}
            //else
            //{
            //    (range.Cells[17, 4] as Excel.Range).Cells.Value2 = CHOUSE.ToString("N2");
            //}

            if (dtEM.Rows[0]["POSIT"].ToString() == "OP" && (dtEM.Rows[0]["CODE"].ToString().StartsWith("6") || dtEM.Rows[0]["CODE"].ToString().StartsWith("7")))
            {
                (range.Cells[20, 4] as Excel.Range).Cells.Value2 = " ";                    // POSITION
            }
            else
            {
                (range.Cells[20, 4] as Excel.Range).Cells.Value2 = CALLOW.ToString("N2");  // POSITION
            }






            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.DisplayAlerts = false;
            xlApp.DisplayAlerts = false;


            const int xlQualityStandard = 0;



            saveFileDlg.FileName = EmpCode+ "_"+DocNo.Replace('/','_')+".xlsx";
            saveFileDlg.RestoreDirectory = true;
            saveFileDlg.Filter = "Excel Files (.xlsx)|*.xlsx;";
            DialogResult dlg = saveFileDlg.ShowDialog();
            if (dlg == DialogResult.OK)
            {
                string fileName = saveFileDlg.FileName;


                //xlWorkSheet.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, fileName , xlQualityStandard, true, false,
                //        Type.Missing, Type.Missing, false, Type.Missing);

                xlWorkBook.SaveAs(fileName, Excel.XlFileFormat.xlOpenXMLWorkbook, Missing.Value,
                    Missing.Value, false, false, Excel.XlSaveAsAccessMode.xlNoChange,
                    Excel.XlSaveConflictResolution.xlUserResolution, true,
                    Missing.Value, Missing.Value, Missing.Value);

            }

            xlWorkBook.Close(false, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);


            //string fleInput = pathFolder + "notpasscode\\" + EmpCode + "_" + DocNo.Replace('/', '-') + ".pdf";
            //@File.Delete(fleInput);

            MessageBox.Show("Generate Certificate for [ "+ EmpCode +" ] Successful!");
            


        }



        public void GenerateCertTOS(string EmpCode)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            string _pathFle = Application.StartupPath + "\\TEMPLATE\\FORM_CERT_TOS.xls";

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(_pathFle, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            //xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets["SLIP"];

            object misValue = System.Reflection.Missing.Value;
            //xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlApp.Visible = false;
            //xlWorkBook = xlApp.Workbooks.Add(misValue);
            range = xlWorkSheet.UsedRange;
            //rw = range.Rows.Count;
            //cl = range.Columns.Count;



            //***** Employee Data ******
            DataTable dtEM = new DataTable();
            string strEM = @"SELECT * FROM empm e LEFT JOIN dict d ON d.type='POSI' and d.item = e.posit WHERE code=:code ";
            OracleCommand cmdEM = new OracleCommand();
            cmdEM.CommandText = strEM;
            cmdEM.Parameters.Add(new OracleParameter(":code", EmpCode));
            dtEM = oOraDCI.Query(cmdEM);


            DataTable dtDept = new DataTable();
            string strDept = @"select * from vi_dv_mstr where dv_cd = '" + dtEM.Rows[0]["DVCD"].ToString() + "' ";
            OracleCommand cmdDept = new OracleCommand();
            cmdDept.CommandText = strDept;
            dtDept = oOraDCI.Query(cmdDept);

            string _Dept = "", _Sect = "";
            _Dept = dtDept.Rows[0]["DEPT"].ToString();
            _Sect = dtDept.Rows[0]["SECT"].ToString();


            //  **** Document Number *******
            string DocNo = GenRunNbr("HRDOC02");



            DateTime dateJoin = new DateTime(1900, 1, 1);
            try { dateJoin = Convert.ToDateTime(dtEM.Rows[0]["Join"].ToString()); }
            catch { }

            DateTime dateResign = new DateTime(1900, 1, 1);
            try { dateResign = Convert.ToDateTime(dtEM.Rows[0]["Resign"].ToString()); }
            catch { }
            //***** End Employee Data ******

            (range.Cells[9, 3] as Excel.Range).Cells.Value2 = DocNo;
            (range.Cells[18, 17] as Excel.Range).Cells.Value2 = DecodeLanguage(dtEM.Rows[0]["tpren"].ToString()) + " " + DecodeLanguage(dtEM.Rows[0]["tname"].ToString());
            (range.Cells[19, 3] as Excel.Range).Cells.Value2 = DecodeLanguage(dtEM.Rows[0]["tsurn"].ToString());
            (range.Cells[23, 10] as Excel.Range).Cells.Value2 = DecodeLanguage(dtEM.Rows[0]["tpren"].ToString()) + " " + DecodeLanguage(dtEM.Rows[0]["tname"].ToString());
            (range.Cells[23, 18] as Excel.Range).Cells.Value2 = DecodeLanguage(dtEM.Rows[0]["tsurn"].ToString());
            (range.Cells[19, 19] as Excel.Range).Cells.Value2 = dtEM.Rows[0]["DESCR"].ToString();
            (range.Cells[20, 3] as Excel.Range).Cells.Value2 = _Sect;
            (range.Cells[20, 9] as Excel.Range).Cells.Value2 = _Dept;
            (range.Cells[20, 18] as Excel.Range).Cells.Value2 = dateJoin.ToString("dd MMMM yyyy");


            DataTable dtPR = new DataTable();
            string strPR = @"SELECT * FROM prdt WHERE code=:code ORDER BY pdate DESC ";
            OracleCommand cmdPR = new OracleCommand();
            cmdPR.CommandText = strPR;
            cmdPR.Parameters.Add(new OracleParameter(":code", EmpCode));
            dtPR = oOraDCI.Query(cmdPR);


            decimal CSAL = 0, CHOUSE = 0, CALLOW = 0, CTRAN = 0, CPHONE = 0, COTHER = 0, CNET = 0, otsm = 0, shtsm = 0, shtamt = 0, otamt = 0, spcamt = 0;
            CSAL = ConvDecimal(dtPR.Rows[0]["SALARY"].ToString());
            CHOUSE = ConvDecimal(dtPR.Rows[0]["housing"].ToString());
            CALLOW = ConvDecimal(dtPR.Rows[0]["allow"].ToString());
            CTRAN = ConvDecimal(dtPR.Rows[0]["Tran"].ToString());
            CPHONE = ConvDecimal(dtPR.Rows[0]["AD_FD"].ToString());
            otsm = ConvDecimal(dtPR.Rows[0]["otamt"].ToString());
            spcamt = ConvDecimal(dtPR.Rows[0]["spcamt"].ToString());
            shtsm = ConvDecimal(dtPR.Rows[0]["shtamt"].ToString());
            CNET = ConvDecimal(dtPR.Rows[0]["NET"].ToString());

            COTHER = CHOUSE + CALLOW + CTRAN + CPHONE + otsm + spcamt + shtsm;

            (range.Cells[21, 3] as Excel.Range).Cells.Value2 = CSAL.ToString("N2");
            (range.Cells[21, 10] as Excel.Range).Cells.Value2 = CNET.ToString("N2");
            (range.Cells[21, 21] as Excel.Range).Cells.Value2 = COTHER.ToString("N2");



            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.DisplayAlerts = false;
            xlApp.DisplayAlerts = false;


            const int xlQualityStandard = 0;



            saveFileDlg.FileName = EmpCode + "_" + DocNo.Replace('/', '_') + ".xlsx";
            saveFileDlg.RestoreDirectory = true;
            saveFileDlg.Filter = "Excel Files (.xlsx)|*.xlsx;";
            DialogResult dlg = saveFileDlg.ShowDialog();
            if (dlg == DialogResult.OK)
            {
                string fileName = saveFileDlg.FileName;


                //xlWorkSheet.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, fileName, xlQualityStandard, true, false,
                //        Type.Missing, Type.Missing, false, Type.Missing);

                xlWorkBook.SaveAs(fileName, Excel.XlFileFormat.xlOpenXMLWorkbook, Missing.Value,
                    Missing.Value, false, false, Excel.XlSaveAsAccessMode.xlNoChange,
                    Excel.XlSaveConflictResolution.xlUserResolution, true,
                    Missing.Value, Missing.Value, Missing.Value);

            }



            xlWorkBook.Close(false, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

            //string fleInput = pathFolder + "notpasscode\\" + EmpCode + "_" + DocNo.Replace('/', '-') + ".pdf";
            //@File.Delete(fleInput);

            MessageBox.Show("Generate Bank for [ " + EmpCode + " ] Successful!");


        }



        public void GenerateCertOMSIN(string EmpCode)
        {


            string[] oAryMonth = { "มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม" };
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            string _pathFle = Application.StartupPath + "\\TEMPLATE\\FORM_CERT_OMSIN.xlsx";

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(_pathFle, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            //xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets["SLIP"];

            object misValue = System.Reflection.Missing.Value;
            //xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlApp.Visible = false;
            //xlWorkBook = xlApp.Workbooks.Add(misValue);
            range = xlWorkSheet.UsedRange;
            //rw = range.Rows.Count;
            //cl = range.Columns.Count;



            //***** Employee Data ******
            DataTable dtEM = new DataTable();
            string strEM = @"SELECT * FROM empm e LEFT JOIN dict d ON d.type='POSI' and d.item = e.posit WHERE code=:code ";
            OracleCommand cmdEM = new OracleCommand();
            cmdEM.CommandText = strEM;
            cmdEM.Parameters.Add(new OracleParameter(":code", EmpCode));
            dtEM = oOraDCI.Query(cmdEM);


            DataTable dtDept = new DataTable();
            string strDept = @"select * from vi_dv_mstr where dv_cd = '" + dtEM.Rows[0]["DVCD"].ToString() + "' ";
            OracleCommand cmdDept = new OracleCommand();
            cmdDept.CommandText = strDept;
            dtDept = oOraDCI.Query(cmdDept);

            string _Dept = "", _Sect = "";
            _Dept = dtDept.Rows[0]["DEPT"].ToString();
            _Sect = dtDept.Rows[0]["SECT"].ToString();


            //  **** Document Number *******
            //string DocNo = GenRunNbr("HRDOC02");



            DateTime dateJoin = new DateTime(1900, 1, 1);
            try { dateJoin = Convert.ToDateTime(dtEM.Rows[0]["Join"].ToString()); }
            catch { }

            DateTime dateResign = new DateTime(1900, 1, 1);
            try { dateResign = Convert.ToDateTime(dtEM.Rows[0]["Resign"].ToString()); }
            catch { }
            //***** End Employee Data ******

            string strDateTH = DateTime.Now.Day + " " + oAryMonth[DateTime.Now.Month - 1] +" "+ ((DateTime.Now.Year < 2500) ? DateTime.Now.Year + 543 : DateTime.Now.Year).ToString();
            string strDateJoinTH = dateJoin.Day + " " + oAryMonth[dateJoin.Month - 1] +" "+ ((dateJoin.Year < 2500) ? dateJoin.Year + 543 : dateJoin.Year).ToString();

            (range.Cells[10, 18] as Excel.Range).Cells.Value2 = strDateTH;
            (range.Cells[14, 6] as Excel.Range).Cells.Value2 = strDateTH;
            (range.Cells[16, 5] as Excel.Range).Cells.Value2 = DecodeLanguage(dtEM.Rows[0]["tpren"].ToString()) + DecodeLanguage(dtEM.Rows[0]["tname"].ToString()) + " " + DecodeLanguage(dtEM.Rows[0]["tsurn"].ToString());
            (range.Cells[16, 14] as Excel.Range).Cells.Value2 = dtEM.Rows[0]["DESCR"].ToString();
            (range.Cells[16, 20] as Excel.Range).Cells.Value2 = _Dept;

            //**********************************
            //**********************************
            //          PAY MONTH
            //**********************************
            //**********************************
            DataTable dtPR = new DataTable();
            string strPR = @"SELECT * FROM prdt WHERE code=:code ORDER BY pdate DESC ";
            OracleCommand cmdPR = new OracleCommand();
            cmdPR.CommandText = strPR;
            cmdPR.Parameters.Add(new OracleParameter(":code", EmpCode));
            dtPR = oOraDCI.Query(cmdPR);


            decimal CSAL = 0, CHOUSE = 0, CALLOW = 0, CTRAN = 0, CPHONE = 0, COTHER = 0, CNET = 0, otsm = 0, shtsm = 0, shtamt = 0, otamt = 0, spcamt = 0;
            CSAL = ConvDecimal(dtPR.Rows[0]["SALARY"].ToString());
            CHOUSE = ConvDecimal(dtPR.Rows[0]["housing"].ToString());
            CALLOW = ConvDecimal(dtPR.Rows[0]["allow"].ToString());
            CTRAN = ConvDecimal(dtPR.Rows[0]["Tran"].ToString());
            CPHONE = ConvDecimal(dtPR.Rows[0]["AD_FD"].ToString());
            otsm = ConvDecimal(dtPR.Rows[0]["otamt"].ToString());
            spcamt = ConvDecimal(dtPR.Rows[0]["spcamt"].ToString());
            shtsm = ConvDecimal(dtPR.Rows[0]["shtamt"].ToString());
            CNET = ConvDecimal(dtPR.Rows[0]["NET"].ToString());

            COTHER = CHOUSE + CALLOW + CTRAN + CPHONE + otsm + spcamt + shtsm;
            //**********************************
            //**********************************
            //          PAY MONTH
            //**********************************
            //**********************************



            (range.Cells[17, 7] as Excel.Range).Cells.Value2 = strDateJoinTH;
            (range.Cells[17, 15] as Excel.Range).Cells.Value2 = CSAL.ToString("N2");
            (range.Cells[18, 7] as Excel.Range).Cells.Value2 = COTHER.ToString("N2");




            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.DisplayAlerts = false;
            xlApp.DisplayAlerts = false;


            const int xlQualityStandard = 0;



            saveFileDlg.FileName = "Cert_Bank_Omsin_" + EmpCode + ".xlsx";
            saveFileDlg.RestoreDirectory = true;
            saveFileDlg.Filter = "Excel Files (.xlsx)|*.xlsx;";
            DialogResult dlg = saveFileDlg.ShowDialog();
            if (dlg == DialogResult.OK)
            {
                string fileName = saveFileDlg.FileName;


                //xlWorkSheet.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, fileName, xlQualityStandard, true, false,
                //        Type.Missing, Type.Missing, false, Type.Missing);

                xlWorkBook.SaveAs(fileName, Excel.XlFileFormat.xlOpenXMLWorkbook, Missing.Value,
                    Missing.Value, false, false, Excel.XlSaveAsAccessMode.xlNoChange,
                    Excel.XlSaveConflictResolution.xlUserResolution, true,
                    Missing.Value, Missing.Value, Missing.Value);

            }



            xlWorkBook.Close(false, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

            //string fleInput = pathFolder + "notpasscode\\" + EmpCode + "_" + DocNo.Replace('/', '-') + ".pdf";
            //@File.Delete(fleInput);

            MessageBox.Show("Generate Bank for [ " + EmpCode + " ] Successful!");


        }





        public string GenRunNbr(string DocKey)
        {
            DataTable dtRunNbr = new DataTable();
            string str = @"SELECT * FROM [dbHRM].[dbo].[DCRunNbr] WHERE DocKey = @DocKey ";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = str;
            cmd.Parameters.Add(new SqlParameter("@DocKey", DocKey));
            dtRunNbr = oConHRM.Query(cmd);

            string nbr = "";

            if (dtRunNbr.Rows.Count > 0)
            {
                int _UseNbr = 1;
                int _Next = 2;
                DateTime _LastDate = Convert.ToDateTime(dtRunNbr.Rows[0]["ActiveDate"].ToString());

                if (dtRunNbr.Rows[0]["ResetOption"].ToString() == "M")
                {
                    if(_LastDate.ToString("yyyyMM") == DateTime.Now.ToString("yyyyMM"))
                    {
                        _UseNbr = Convert.ToInt32(dtRunNbr.Rows[0]["NextID"].ToString());
                        _Next = _UseNbr + 1;
                    }
                }
                else if(dtRunNbr.Rows[0]["ResetOption"].ToString() == "D")
                {
                    if (_LastDate.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
                    {
                        _UseNbr = Convert.ToInt32(dtRunNbr.Rows[0]["NextID"].ToString());
                        _Next = _UseNbr + 1;
                    }
                }

                string _year = "", _month = "", _day = "";
                if(dtRunNbr.Rows[0]["YearNbrPrefix"].ToString() == "4")
                {
                    _year = DateTime.Now.ToString("yyyy");
                }
                else if (dtRunNbr.Rows[0]["YearNbrPrefix"].ToString() == "2")
                {
                    _year = DateTime.Now.ToString("yyyy").Substring(2, 2);
                }

                if (dtRunNbr.Rows[0]["MonthNbrPrefix"].ToString() == "2")
                {
                    _month = DateTime.Now.ToString("MM");
                }

                if (dtRunNbr.Rows[0]["DayNbrPrefix"].ToString() == "2")
                {
                    _day = DateTime.Now.ToString("dd");
                }

                string strUpd = @"UPDATE [dbHRM].[dbo].[DCRunNbr] SET NextID=@NextID, ActiveDate=@ActiveDate WHERE DocKey = @DocKey  ";
                SqlCommand cmdUpd = new SqlCommand();
                cmdUpd.CommandText = strUpd;
                cmdUpd.Parameters.Add(new SqlParameter("@DocKey", DocKey));
                cmdUpd.Parameters.Add(new SqlParameter("@NextID", _Next.ToString()));
                cmdUpd.Parameters.Add(new SqlParameter("@ActiveDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                oConHRM.ExecuteCommand(cmdUpd);


                nbr = dtRunNbr.Rows[0]["DocPrefix"].ToString() + _year + _month+ _day+"/"+ _UseNbr.ToString("000");

            } // end if

            return nbr;
        }
        

        private void btnSPS_Click(object sender, EventArgs e)
        {
            DateTime datePayroll = new DateTime(dpkrSPSDate.Value.Year, dpkrSPSDate.Value.Month, 1);

            DataTable dtData = new DataTable();
            string str = @"SELECT code, wtype, salary, allow, LIVING, SOCIAL, insuno, pren, tname, tsurn, name, surn, resign, IDNO FROM(
                                SELECT P.code, P.wtype, P.salary, P.allow, P.LIVING, P.SOCIAL , E.insuno, E.pren, E.tname, E.tsurn, E.IDNO, E.name, E.surn, E.resign  
                                FROM prdt P 
                                LEFT JOIN empm E ON P.code = E.code 
                                WHERE P.wtype='S' and pdate='" + datePayroll.ToString("dd/MMM/yyyy") + @"' 
                                    and P.code not in (select item from dict where type = 'NOSO') 

                                UNION ALL    
        
                                SELECT P.code, P.wtype, P.salary, P.allow, P.LIVING, P.SOCIAL , E.insuno, E.pren, E.tname, E.tsurn, E.IDNO, E.name, E.surn, E.resign  
                                FROM prdt P 
                                LEFT JOIN empm E ON P.code = E.code 
                                WHERE P.wtype='O' and pdate='" + datePayroll.ToString("dd/MMM/yyyy") + @"'     
                            ) ORDER BY wtype DESC, code ASC     ";
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = str;
            dtData = oOraDCI.Query(cmd);

            saveFileDlg.FileName = "ssosent.dat";
            saveFileDlg.RestoreDirectory = true;
            saveFileDlg.Filter = "dat Files (.dat)|*.dat;";
            DialogResult dlg = saveFileDlg.ShowDialog();
            if (dlg == DialogResult.OK)
            {

                if(dtData.Rows.Count > 0)
                {
                    
                    string fileName = saveFileDlg.FileName;

                    // Check if file already exists. If yes, delete it.     
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }


                    List<string> oAryStr = new List<string>();

                        
                    decimal _wage = 0, _paid = 0;
                    int _person = 0;
                    string _StrError = "";
                    bool _StatusError = true;

                    foreach (DataRow drData in dtData.Rows)
                    {
                        try {
                            string _pf = getSpaceText("", 3);
                            if (drData["pren"].ToString().ToUpper() == "MR.")
                            {
                                _pf = "003";
                            }
                            else if (drData["pren"].ToString().ToUpper() == "MS.")
                            {
                                _pf = "004";
                            }
                            else if (drData["pren"].ToString().ToUpper() == "MRS.")
                            {
                                _pf = "005";
                            }

                            decimal _salary = 0, _allow = 0, _living = 0, _social = 0, _Inc = 0;
                            _salary = Convert.ToDecimal(drData["SALARY"].ToString());
                            _allow = Convert.ToDecimal(drData["allow"].ToString());
                            _living = Convert.ToDecimal(drData["LIVING"].ToString());
                            _social = Convert.ToDecimal(drData["social"].ToString());

                            string _idno = getSpaceText(drData["IDNO"].ToString(), 13);
                            string _name = getSpaceText(DecodeLanguage(drData["name"].ToString()), 30);
                            //string _tname = getSpaceText(DecodeLanguage(drData["tname"].ToString()), 30);
                            string _surn = getSpaceText(DecodeLanguage(drData["surn"].ToString()), 35);
                            //string _tsurn = getSpaceText(DecodeLanguage(drData["tsurn"].ToString()), 35);
                            string _end = getSpaceText("", 27);

                            _Inc = _salary + _allow + _living;
                            _wage += _Inc;
                            _paid += _social;

                            string _StrInc = getLeadZeroText((_Inc * 100).ToString(), 14, 0);
                            string _StrSocial = getLeadZeroText((_social * 100).ToString(), 12, 0);

                            oAryStr.Add(String.Format("2{0}{1}{2}{3}{4}{5}{6}",
                                _idno, _pf, _name, _surn, _StrInc, _StrSocial, _end));

                            //if (drData["code"].ToString().StartsWith("0"))
                            //{
                            //    oAryStr.Add(String.Format("2{0}{1}{2}{3}{4}{5}{6}",
                            //    _idno, _pf, _name, _surn, _StrInc, _StrSocial, _end));
                            //}
                            //else
                            //{
                            //    oAryStr.Add(String.Format("2{0}{1}{2}{3}{4}{5}{6}",
                            //    _idno, _pf, _tname, _tsurn, _StrInc, _StrSocial, _end));
                            //}


                        }
                        catch (Exception ex){
                            _StatusError = false;
                            _StrError += " Err. :" + ex.Message.ToString() +Environment.NewLine;
                        }
                        
                        _person++;
                    } // end foreach



                    //**** Is not Error Add Header, Details Create File *****
                    if (_StatusError)
                    {
                        // Create a new file
                        using (StreamWriter sw = File.CreateText(fileName))
                        {
                            //string _sendDate = (dpkrSPSSendDate.Value.Year < 2500) ? dpkrSPSSendDate.Value.AddYears(543).ToString("ddMMyy") : dpkrSPSSendDate.Value.ToString("ddMMyy");
                            //string _slipDate = (dpkrSPSDate.Value.Year < 2500) ? dpkrSPSDate.Value.AddYears(543).ToString("MMyy") : dpkrSPSDate.Value.ToString("MMyy");
                            //string _comName = "บริษัท  ไดกิ้นคอมเพรสเซอร์ อินดัสทรีส์  จำกัด";
                            //sw.WriteLine(String.Format("12000028438000000{0}{1}{2}0500{3}{4}{5}{6}{7}",
                            //    _sendDate, _slipDate, _comName,
                            //    getLeadZeroText(_person.ToString(), 6, 0),
                            //    getLeadZeroText((_wage * 100).ToString(), 12, 0),
                            //    getLeadZeroText((_paid * 100 * 2).ToString(), 12, 0),
                            //    getLeadZeroText((_paid * 100).ToString(), 10, 0),
                            //    getLeadZeroText((_paid * 100).ToString(), 10, 0)));
                            //foreach (string oStr in oAryStr) { sw.WriteLine(oStr); }

                            //******** Add Header ***********
                            string _sendDate = (dpkrSPSSendDate.Value.Year < 2500) ? dpkrSPSSendDate.Value.AddYears(543).ToString("ddMMyy") : dpkrSPSSendDate.Value.ToString("ddMMyy");
                            string _slipDate = (dpkrSPSDate.Value.Year < 2500) ? dpkrSPSDate.Value.AddYears(543).ToString("MMyy") : dpkrSPSDate.Value.ToString("MMyy");
                            string _comName = "Daikin Compressor Industries Ltd";
                            sw.WriteLine(String.Format("12000028438000000{0}{1}{2}0500{3}{4}{5}{6}{7}",
                                _sendDate, _slipDate, getSpaceText(_comName, 45),
                                getLeadZeroText(_person.ToString(), 6, 0),
                                getLeadZeroText((_wage * 100).ToString(), 15, 0),
                                getLeadZeroText((_paid * 100 * 2).ToString(), 14, 0),
                                getLeadZeroText((_paid * 100).ToString(), 12, 0),
                                getLeadZeroText((_paid * 100).ToString(), 12, 0)));

                            //******** Add Detail ***********
                            foreach (string oStr in oAryStr) { sw.WriteLine(oStr); }


                        } // end using

                        MessageBox.Show("Write Send S.P.S file OK!");
                    }
                    else
                    {
                        MessageBox.Show(_StrError);
                    }
                    //**** Is not Error Add Header, Details Create File *****

                }// end if

            }

        }

        private void btnSPSForm_Click(object sender, EventArgs e)
        {
            HideGroupBoxes();
            grbSPS.Visible = true;
        }

        private void btn50TaviForm_Click(object sender, EventArgs e)
        {
            HideGroupBoxes();
            grb50Tavi.Visible = true;
        }

        private void cbTaviType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbTaviType.Items.Count > 0)
            {
                if(cbTaviType.SelectedValue.ToString() == "ONE")
                {
                    txtTaviEmpCode.Visible = true;
                    lbTaviEmpCode.Visible = true;
                }
                else
                {
                    txtTaviEmpCode.Visible = false;
                    lbTaviEmpCode.Visible = false;
                }
                
            }
        }

        private void btnTaviGenerate_Click(object sender, EventArgs e)
        {
            string Mwtype = cbTaviType.SelectedValue.ToString();
            string resignDate = "", DateX = "", TpDate ="";
            string strEmpData = "";
            string _pathFle = Application.StartupPath + "\\TEMPLATE\\FORM_50TAVI.xls";
            string folderSource = txtTaviFolder.Text;

            string empCode = txtTaviEmpCode.Text.Trim();

            if (!Directory.Exists(folderSource))
            {
                MessageBox.Show("เลือก Folder ที่จัดเก็บก่อน");
                return;
            }


            DateX = $"01/JAN/{cbTaviYear.SelectedValue.ToString()}";
            TpDate = $"31/DEC/{cbTaviYear.SelectedValue.ToString()}";
            resignDate = $"01/JAN/{(Convert.ToInt16(cbTaviYear.SelectedValue) + 1).ToString()}";

            if (Mwtype == "M")
            {
                strEmpData = $@"SELECT * FROM EMPM  WHERE wsts='M' AND join < '{resignDate}' AND (resign IS Null or resign > '{resignDate}') ORDER BY dvcd,code";
            }
            else if (Mwtype == "R")
            {
                strEmpData = $@"SELECT * FROM EMPM WHERE (resign BETWEEN '{DateX}' AND '{TpDate}') AND join < '{resignDate}' ORDER BY dvcd,code ";                
            }
            else if (Mwtype == "S" || Mwtype == "O")
            {
                strEmpData = $@"SELECT * FROM EMPM  WHERE wsts <> 'M' AND wtype='{Mwtype}' AND join < '{resignDate}' AND (resign IS Null or resign > '{resignDate}')   order by dvcd,grpot,code";
            }
            else if (Mwtype == "ONE")
            {
                strEmpData = $@"SELECT * FROM EMPM  WHERE code='{empCode}' ";
            }
            else if (Mwtype == "ONLINE")
            {
                strEmpData = $@"SELECT * FROM EMPM  
                                WHERE join < '{resignDate}' AND (resign IS Null or resign > '{resignDate}')   
                                ORDER BY dvcd,code";
            }



            //strEmpData = $@"SELECT * FROM EMPM 
            //                WHERE join < '{resignDate}' AND (resign IS Null or resign > '{resignDate}') 
            //                    AND code in ('34421','11644','20741')
            //                ORDER BY dvcd,code";



            int _ok = 0, _ng = 0;


            //**** Get Employee Data *****
            DataTable dtEmpData = new DataTable();
            OracleCommand cmdEmpData = new OracleCommand();
            cmdEmpData.CommandText = strEmpData;
            dtEmpData = oOraDCI.Query(cmdEmpData);

            if (dtEmpData.Rows.Count > 0)
            {
                //****** Dictanary Data Dept / Sect *******
                DataTable dtDict = new DataTable();
                string strDict = "select note, item from dict where type='DVCD'  ";
                OracleCommand cmdDict = new OracleCommand();
                cmdDict.CommandText = strDict;
                dtDict = oOraDCI.Query(cmdDict);


                foreach (DataRow drEmp in dtEmpData.Rows)
                {

                    string docno = GenerateNumber("HRDOC05");

                    if (Mwtype == "ONLINE")
                    {
                        DataTable dtCheck = new DataTable();
                        string strCheck = "SELECT Code FROM [dbHRM].[dbo].[HR_DOC_REQ] WHERE DocType = '50TAVI' AND Code=@Code ";
                        SqlCommand cmdCheck = new SqlCommand();
                        cmdCheck.CommandText = strCheck;
                        cmdCheck.Parameters.Add(new SqlParameter("@Code", drEmp["code"].ToString()));
                        dtCheck = oConHRM.Query(cmdCheck);
                        if(dtCheck.Rows.Count > 0)
                        {
                            continue;
                        }


                        string strInstrLog = @"INSERT INTO [dbHRM].[dbo].[HR_DOC_REQ] (DocNo, DocType, Code, ReqDate, 
                                                    ApproveBy, ApproveDate, IssueBy, IssueDate, ExpireDate, DocFile, DocStatus, Passcode, Remark) 
                                                VALUES (@DocNo, @DocType, @Code, @ReqDate, @ApproveBy, @ApproveDate, @IssueBy, 
                                                        @IssueDate, @ExpireDate, @DocFile, @DocStatus, @Passcode, @Remark) ";
                        SqlCommand cmdInstrLog = new SqlCommand();
                        cmdInstrLog.CommandText = strInstrLog;
                        cmdInstrLog.Parameters.Add(new SqlParameter("@DocNo", docno));
                        cmdInstrLog.Parameters.Add(new SqlParameter("@DocType", "50TAVI"));
                        cmdInstrLog.Parameters.Add(new SqlParameter("@Code", drEmp["code"].ToString()));
                        cmdInstrLog.Parameters.Add(new SqlParameter("@ReqDate", Convert.ToDateTime(TpDate).ToString("yyyy-MM-dd")));
                        cmdInstrLog.Parameters.Add(new SqlParameter("@ApproveBy", appMgr.UserAccount.AccountId));
                        cmdInstrLog.Parameters.Add(new SqlParameter("@ApproveDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                        cmdInstrLog.Parameters.Add(new SqlParameter("@IssueBy", appMgr.UserAccount.AccountId));
                        cmdInstrLog.Parameters.Add(new SqlParameter("@IssueDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                        cmdInstrLog.Parameters.Add(new SqlParameter("@ExpireDate", Convert.ToDateTime(TpDate).AddMonths(3).ToString("yyyy-MM-dd")));
                        cmdInstrLog.Parameters.Add(new SqlParameter("@DocFile", ""));
                        cmdInstrLog.Parameters.Add(new SqlParameter("@DocStatus", "ISSUED"));
                        cmdInstrLog.Parameters.Add(new SqlParameter("@Passcode", ""));
                        cmdInstrLog.Parameters.Add(new SqlParameter("@Remark", ""));
                        oConHRM.ExecuteCommand(cmdInstrLog);


                    } // end if ONLINE





                    Excel.Application xlApp;
                    Excel.Workbook xlWorkBook;
                    Excel.Worksheet xlWorkSheet;
                    Excel.Range range;


                    xlApp = new Excel.Application();
                    xlWorkBook = xlApp.Workbooks.Open(_pathFle, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                    object misValue = System.Reflection.Missing.Value;
                    //xlApp = new Microsoft.Office.Interop.Excel.Application();
                    xlApp.Visible = false;
                    //xlWorkBook = xlApp.Workbooks.Add(misValue);
                    range = xlWorkSheet.UsedRange;
                    //rw = range.Rows.Count;
                    //cl = range.Columns.Count;


                    string pCode = drEmp["code"].ToString();
                    //  personal id
                    string citizenId = (drEmp["IDNO"].ToString() == "") ? "-" : drEmp["IDNO"].ToString();



                    string strEmpSum = "";

                    //****** Get Summary Money Data *********
                    if (Mwtype == "R")
                    {
                        strEmpSum = $@"SELECT empm.IDNO ,  SUM(pr.inc) as incacc , SUM(pr.tax) as taxacc ,
                                                SUM(pr.prov) as provacc , SUM(PR.social) As socacc 
                            FROM PRDT pr , empm  
                            WHERE PR.code = empm.code AND empm.idno = '{citizenId}' AND (pr.pdate BETWEEN '{DateX}' AND '{TpDate}') 
                            GROUP BY empm.idno ";
                    }
                    else
                    {
                        if (ckTaviOnlyPayTax.Checked)
                        {
                            strEmpSum = $@"SELECT empm.IDNO ,  SUM(pr.inc) as incacc , SUM(pr.tax) as taxacc ,
                                                SUM(pr.prov) as provacc , SUM(PR.social) As socacc 
                            FROM PRDT pr , empm  
                            WHERE PR.code = empm.code AND empm.idno = '{citizenId}' AND (pr.pdate BETWEEN '{DateX}' AND '{TpDate}') 
                            GROUP BY empm.idno
                            HAVING SUM(pr.tax)  > 0 ";
                        }
                        else
                        {
                            strEmpSum = $@"SELECT empm.IDNO ,  SUM(pr.inc) as incacc , SUM(pr.tax) as taxacc ,
                                                SUM(pr.prov) as provacc , SUM(PR.social) As socacc 
                            FROM PRDT pr , empm  
                            WHERE PR.code = empm.code AND empm.idno = '{citizenId}' AND (pr.pdate BETWEEN '{DateX}' AND '{TpDate}') 
                            GROUP BY empm.idno ";
                        }
                    }

                    //***** Get Summary Money Data ********
                    DataTable dtEmpSum = new DataTable();
                    OracleCommand cmdEmpSum = new OracleCommand();
                    cmdEmpSum.CommandText = strEmpSum;
                    dtEmpSum = oOraDCI.Query(cmdEmpSum);



                    string fldName = $"{folderSource}\\50Tavi_{cbTaviYear.SelectedValue.ToString()}_{pCode}.pdf";
                    // check have sum money
                    if (dtEmpSum.Rows.Count > 0)
                    {
                        string pName = DecodeLanguage(drEmp["tpren"].ToString()) + DecodeLanguage(drEmp["tname"].ToString()) + " " + DecodeLanguage(drEmp["tsurn"].ToString());
                        string pAddr1 = DecodeLanguage(drEmp["TCADDR1"].ToString());
                        string pAddr2 = DecodeLanguage(drEmp["TCADDR2"].ToString());
                        string pAddr3 = DecodeLanguage(drEmp["TCADDR3"].ToString());
                        string pCity = DecodeLanguage(drEmp["TCADDR4"].ToString());
                        string pTaxno = drEmp["taxno"].ToString();

                        string pIncacc = ConvDecimal(dtEmpSum.Rows[0]["incacc"].ToString()).ToString("N2");
                        string ptaxacc = ConvDecimal(dtEmpSum.Rows[0]["taxacc"].ToString()).ToString("N2");
                        string ppvac = ConvDecimal(dtEmpSum.Rows[0]["provacc"].ToString()).ToString("N2");
                        string Psocac = ConvDecimal(dtEmpSum.Rows[0]["socacc"].ToString()).ToString("N2");
                        string pinsu = (drEmp["IDNO"].ToString() == "") ? " " : drEmp["IDNO"].ToString();
                        string pline = " ";


                        string srcDept = (drEmp["DVCD"].ToString().Length == 5) ? drEmp["DVCD"].ToString().Substring(0, 3) : drEmp["DVCD"].ToString().Substring(0, 2);
                        string srcSect = (drEmp["DVCD"].ToString().Length == 5) ? drEmp["DVCD"].ToString().Substring(0, 4) : drEmp["DVCD"].ToString().Substring(0, 3);

                        DataRow[] drDept = dtDict.Select($" item='{srcDept}00' ");
                        DataRow[] drSect = dtDict.Select($" item='{srcSect}0' ");

                        string _Dept = "", _Sect = "";
                        _Dept = (drDept.Count() > 0) ? drDept[0]["note"].ToString() : "";
                        _Sect = (drSect.Count() > 0) ? drSect[0]["note"].ToString() : "";

                        if (drEmp["resign"].ToString() == "")
                        {
                            pline = $"{getSpaceText("", 50)}{TpDate}{getSpaceText("", 50)}REF : {pCode} {_Dept}-{_Sect}";
                        }
                        else
                        {
                            pline = $"{getSpaceText("", 50)}{TpDate}{getSpaceText("", 50)}REF : {pCode}** {_Dept}-{_Sect}";
                        }


                        (range.Cells[4, 10] as Excel.Range).Cells.Value2 = pCode;
                        (range.Cells[7, 8] as Excel.Range).Cells.Value2 = TpDate;
                        (range.Cells[7, 9] as Excel.Range).Cells.Value2 = pIncacc;
                        (range.Cells[7, 10] as Excel.Range).Cells.Value2 = ptaxacc;
                        (range.Cells[18, 8] as Excel.Range).Cells.Value2 = TpDate;
                        (range.Cells[18, 9] as Excel.Range).Cells.Value2 = pIncacc;
                        (range.Cells[18, 10] as Excel.Range).Cells.Value2 = ptaxacc;
                        (range.Cells[18, 1] as Excel.Range).Cells.Value2 = $"{getSpaceText("", 8)}{pName}";
                        (range.Cells[19, 1] as Excel.Range).Cells.Value2 = $"{getSpaceText("", 8)}{pAddr1}"; ;
                        (range.Cells[20, 1] as Excel.Range).Cells.Value2 = $"{getSpaceText("", 8)}{pAddr2}"; ;
                        (range.Cells[21, 1] as Excel.Range).Cells.Value2 = $"{getSpaceText("", 8)}{pAddr3} {pCity}"; ;
                        (range.Cells[23, 1] as Excel.Range).Cells.Value2 = pTaxno;
                        (range.Cells[29, 1] as Excel.Range).Cells.Value2 = pinsu;


                        int plusCol = 12;
                        (range.Cells[4, 10 + plusCol] as Excel.Range).Cells.Value2 = pCode;
                        (range.Cells[7, 8 + plusCol] as Excel.Range).Cells.Value2 = TpDate;
                        (range.Cells[7, 9 + plusCol] as Excel.Range).Cells.Value2 = pIncacc;
                        (range.Cells[7, 10 + plusCol] as Excel.Range).Cells.Value2 = ptaxacc;
                        (range.Cells[18, 8 + plusCol] as Excel.Range).Cells.Value2 = TpDate;
                        (range.Cells[18, 9 + plusCol] as Excel.Range).Cells.Value2 = pIncacc;
                        (range.Cells[18, 10 + plusCol] as Excel.Range).Cells.Value2 = ptaxacc;
                        (range.Cells[18, 1 + plusCol] as Excel.Range).Cells.Value2 = $"{getSpaceText("", 8)}{pName}";
                        (range.Cells[19, 1 + plusCol] as Excel.Range).Cells.Value2 = $"{getSpaceText("", 8)}{pAddr1}"; ;
                        (range.Cells[20, 1 + plusCol] as Excel.Range).Cells.Value2 = $"{getSpaceText("", 8)}{pAddr2}"; ;
                        (range.Cells[21, 1 + plusCol] as Excel.Range).Cells.Value2 = $"{getSpaceText("", 8)}{pAddr3} {pCity}"; ;
                        (range.Cells[23, 1 + plusCol] as Excel.Range).Cells.Value2 = pTaxno;
                        (range.Cells[29, 1 + plusCol] as Excel.Range).Cells.Value2 = pinsu;

                        if (ConvDecimal(ppvac) > 0)
                        {
                            (range.Cells[24, 4] as Excel.Range).Cells.Value2 = "'/";
                            (range.Cells[24, 9] as Excel.Range).Cells.Value2 = ppvac;

                            (range.Cells[24, 4 + plusCol] as Excel.Range).Cells.Value2 = "'/";
                            (range.Cells[24, 9 + plusCol] as Excel.Range).Cells.Value2 = ppvac;
                        }
                        else
                        {
                            (range.Cells[24, 4] as Excel.Range).Cells.Value2 = "";
                            (range.Cells[24, 9] as Excel.Range).Cells.Value2 = "";

                            (range.Cells[24, 4 + plusCol] as Excel.Range).Cells.Value2 = "";
                            (range.Cells[24, 9 + plusCol] as Excel.Range).Cells.Value2 = "";
                        }

                        if (ConvDecimal(Psocac) > 0)
                        {
                            (range.Cells[25, 4] as Excel.Range).Cells.Value2 = "'/";
                            (range.Cells[25, 9] as Excel.Range).Cells.Value2 = Psocac;

                            (range.Cells[25, 4 + plusCol] as Excel.Range).Cells.Value2 = "'/";
                            (range.Cells[25, 9 + plusCol] as Excel.Range).Cells.Value2 = Psocac;
                        }
                        else
                        {
                            (range.Cells[25, 4] as Excel.Range).Cells.Value2 = "";
                            (range.Cells[25, 9] as Excel.Range).Cells.Value2 = "";

                            (range.Cells[25, 4 + plusCol] as Excel.Range).Cells.Value2 = "";
                            (range.Cells[25, 9 + plusCol] as Excel.Range).Cells.Value2 = "";
                        }

                         (range.Cells[30, 5] as Excel.Range).Cells.Value2 = pline;
                        (range.Cells[30, 5 + plusCol] as Excel.Range).Cells.Value2 = pline;



                        Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                        excel.DisplayAlerts = false;
                        xlApp.DisplayAlerts = false;


                        const int xlQualityStandard = 0;



                        //**** Save to Excel ****
                        //saveFileDlg.FileName = "Cert_Bank_Omsin_" + pCode + ".xlsx";
                        //saveFileDlg.RestoreDirectory = true;
                        //saveFileDlg.Filter = "Excel Files (.xlsx)|*.xlsx;";


                        //**** Save to PDF ****




                        //if(!Directory.Exists($"{folderSource}\\{cbTaviYear.SelectedValue.ToString()}\\")){
                        //    Directory.CreateDirectory($"{folderSource}\\{cbTaviYear.SelectedValue.ToString()}\\");
                        //}
                        if (!Directory.Exists($"{folderSource}\\"))
                        {
                            Directory.CreateDirectory($"{folderSource}\\");
                        }


                        //string fldName = $"{folderSource}\\50Tavi_{cbTaviYear.SelectedValue.ToString()}_{pCode}.pdf";
                        //**** Save to PDF ****
                        xlWorkSheet.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, fldName, xlQualityStandard, true, false,
                                Type.Missing, Type.Missing, false, Type.Missing);


                        /*
                        saveFileDlg.FileName = "50Tavi_" + pCode + ".pdf";
                        saveFileDlg.RestoreDirectory = true;
                        saveFileDlg.Filter = "Excel Files (.pdf)|*.pdf;";
                        DialogResult dlg = saveFileDlg.ShowDialog();
                        if (dlg == DialogResult.OK)
                        {
                            string fileName = saveFileDlg.FileName;
                            //**** Save to PDF ****
                            xlWorkSheet.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, fldName, xlQualityStandard, true, false,
                                    Type.Missing, Type.Missing, false, Type.Missing);

                            //**** Save to Excel ****
                            //xlWorkBook.SaveAs(fileName, Excel.XlFileFormat.xlOpenXMLWorkbook, Missing.Value,
                            //    Missing.Value, false, false, Excel.XlSaveAsAccessMode.xlNoChange,
                            //    Excel.XlSaveConflictResolution.xlUserResolution, true,
                            //    Missing.Value, Missing.Value, Missing.Value);

                        }
                        */

                        //**** Print ****
                        //range.PrintOutEx(1, 1, 1, false, false, false, false, false);


                        _ok++;

                    } // end check have data
                    else
                    {
                        _ng++;
                    }

                    xlWorkBook.Close(false, misValue, misValue);
                    xlApp.Quit();

                    releaseObject(xlWorkSheet);
                    releaseObject(xlWorkBook);
                    releaseObject(xlApp);

                    Marshal.ReleaseComObject(xlWorkSheet);
                    Marshal.ReleaseComObject(xlWorkBook);
                    Marshal.ReleaseComObject(xlApp);



                    //**************************************
                    //       ONLINE ADD PASSCODE
                    //**************************************
                    if (Mwtype == "ONLINE")
                    {
                        #region Add Passcode on PDF
                        Random rnd = new Random();
                        int num = rnd.Next(1000000);
                        string createPwd = num.ToString("D6");
                        //if (!Directory.Exists(folderSource+ "\\notpasscode\\"))
                        //{
                        //    Directory.CreateDirectory(folderSource + "\\notpasscode\\");
                        //}

                        if (!Directory.Exists(folderSource + $"\\{cbTaviYear.SelectedValue.ToString()}\\"))
                        {
                            Directory.CreateDirectory(folderSource + $"\\{cbTaviYear.SelectedValue.ToString()}\\");
                        }

                        //string fleInput = folderSource + "\\notpasscode\\" + EmpCode + "_" + DocNo.Replace('/', '-') + ".pdf";
                        string fleInput = fldName;
                        string fleOutput = folderSource + $"\\{cbTaviYear.SelectedValue.ToString()}\\Tavi_" + pCode + "_" + docno.Replace('/', '-') + ".pdf";
                        using (Stream t_Input = new FileStream(fleInput, FileMode.Open, FileAccess.Read, FileShare.None))
                        using (Stream t_Output = new FileStream(fleOutput, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            PdfReader reader = new PdfReader(t_Input);
                            PdfEncryptor.Encrypt(reader, t_Output, true, createPwd, createPwd, PdfWriter.ALLOW_PRINTING);
                        }
                        string strUpd = @"UPDATE HR_DOC_REQ SET DocStatus=@DocStatus, Passcode=@Passcode, DocFile=@DocFile 
                                       WHERE DocType='50TAVI' AND Code=@Code AND DocNo=@DocNo  ";
                        SqlCommand cmdUpd = new SqlCommand();
                        cmdUpd.CommandText = strUpd;
                        cmdUpd.Parameters.Add(new SqlParameter("@DocStatus", "ISSUED"));
                        cmdUpd.Parameters.Add(new SqlParameter("@Passcode", createPwd));
                        cmdUpd.Parameters.Add(new SqlParameter("@DocFile", cbTaviYear.SelectedValue.ToString() + @"\Tavi_" + pCode + "_" + docno.Replace('/', '-') + ".pdf"));
                        cmdUpd.Parameters.Add(new SqlParameter("@Code", pCode));
                        cmdUpd.Parameters.Add(new SqlParameter("@DocNo", docno));
                        oConHRM.ExecuteCommand(cmdUpd);

                        //@File.Delete(fleInput);
                        #endregion
                    }
                    //**************************************
                    //       ONLINE ADD PASSCODE 
                    //**************************************


                } // end foreach Emp


                //**************************************
                //      NOT ONLINE 
                //**************************************
                if (Mwtype != "ONLINE" && Mwtype != "ONE") { 
                    //******* Combine File PDF *********
                    string[] filePaths = Directory.GetFiles($"{folderSource}\\", "*.pdf");

                    CombineMultiplePDFs(filePaths, $"{folderSource}\\all_{cbTaviYear.SelectedValue.ToString()}.pdf");
                    //******* End Combine File PDF *********
                }
                //**************************************
                //      NOT ONLINE 
                //**************************************



                MessageBox.Show($@"50 Tavi Result{Environment.NewLine} - ทั้งหมด{dtEmpData.Rows.Count.ToString()}{Environment.NewLine} - สำเร็จ : {_ok.ToString()}{Environment.NewLine} - ไม่สำเร็จ : {_ng.ToString()}{Environment.NewLine}");

            } // end check Emp


        }


        public void AddWatermarkPdf(string sjfd, string _)
        {
            PdfReader pdfReader = new PdfReader("SimpleArabic.pdf");
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(DateTime.Now.ToFileTime()
           + "Out.pdf", FileMode.Create, FileAccess.Write, FileShare.None));


            string _pathImg = Application.StartupPath + "\\TEMPLATE\\dci_hr_stamp.png";
            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(_pathImg);
            img.SetAbsolutePosition(-200, -50);
            PdfContentByte waterMark;
            for (int pageIndex = 1; pageIndex <= pdfReader.NumberOfPages; pageIndex++)
            {
                Rectangle pagesize = pdfReader.GetCropBox(pageIndex);
                waterMark = pdfStamper.GetUnderContent(pageIndex);
                waterMark.AddImage(img);
            }
            pdfStamper.FormFlattening = true;
            pdfStamper.Close();
        }



        private void CombineMultiplePDFs(string[] sourceDir, string targetPath)
        {
            try
            {
                int idx = 0;
                // step 1: creation of a document-object
                Document document = new Document();
                // step 2: we create a writer that listens to the document
                PdfCopy writer = new PdfCopy(document, new FileStream(targetPath, FileMode.Create));
                if (writer == null)
                {
                    return;
                }
                // step 3: we open the document
                document.Open();
                foreach (string fileName in sourceDir)
                {
                    //first we validate if pdf file is valid

                    string extension = Path.GetExtension(fileName);
                    if (extension == ".pdf")
                    {
                        if (IsValidPdf(fileName) != true)
                        {
                            //txtTaviMsg.Text += "\n" + Path.GetFileName(fileName) + "  - [" + idx.ToString() + "]  NG";
                        }
                        else
                        {
                            // we create a reader for a certain document
                            //PdfReader.unethicalreading = true;
                            //unlockPdf(fileName);
                            PdfReader reader = new PdfReader(fileName);
                            //txtTaviMsg.Text += "\n" + Path.GetFileName(fileName) + "  - ["+ idx.ToString() + "]  Added!";
                            PdfReader.unethicalreading = true;

                            reader.ConsolidateNamedDestinations();
                            // step 4: we add content
                            for (int i = 1; i <= reader.NumberOfPages; i++)
                            {
                                PdfImportedPage page = writer.GetImportedPage(reader, i);
                                writer.AddPage(page);
                            }
                            PRAcroForm form = reader.AcroForm;
                            if (form != null)
                            {
                                writer.AddDocument(reader);
                            }
                            reader.Close();
                        }
                    }

                    idx++;
                } // end foreach
                // step 5: we close the document and writer                
                writer.Close();
                document.Close();

                //txtTaviMsg.Text += "\n" + "\n" + "Merge completed  - " + Path.GetFileName(targetPath) + " file created!";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                foreach (string fileName in sourceDir)
                {
                    try { 
                        //@File.Delete(fileName); 
                    } catch { 
                    
                    }
                }
            }
        }
        private bool IsValidPdf(string filepath)
        {
            bool Ret = true;
            PdfReader reader = null;
            try
            {
                reader = new PdfReader(filepath);
            }
            catch
            {
                Ret = false;
            }
            return Ret;
        }

        private void btnTaviBrw_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if(dlg.ShowDialog(this) != DialogResult.Cancel)
            {
                txtTaviFolder.Text = dlg.SelectedPath;
            }

            
        }

        private void btnPGD1_PGD1KForm_Click(object sender, EventArgs e)
        {
            HideGroupBoxes();
            grbPGD.Visible = true;
        }

        private void btnPDGGen_Click(object sender, EventArgs e)
        {
            if (cbPGDType.SelectedValue.ToString() == "PGD1")
            {
                #region PDG1

                //
                DataTable dtPay = new DataTable();
                string str = @"SELECT * FROM PRDT 
                               WHERE pdate='" + dpkrDGDDate.Value.ToString("dd/MMM/yyyy") + @"' and code<'90099' and code<>'80020' and inc <> '0' 
                               ORDER BY code ";
                OracleCommand cmd = new OracleCommand();
                cmd.CommandText = str;
                dtPay = oOraDCI.Query(cmd);


                if (dtPay.Rows.Count > 0)
                {
                    DataTable dtEmp = new DataTable();
                    string strEmp = @"select code,tpren,tname,tsurn,pren,name,surn,join,idno,SONS,SONB,taxno,tcaddr1,tcaddr2,tcaddr3,tcaddr4,marry,sons 
                        FROM empm  WHERE code IN (SELECT code FROM PRDT WHERE pdate='" + dpkrDGDDate.Value.ToString("dd/MMM/yyyy") + @"' and code<'90099' and code<>'80020' and inc <> '0' )  ";
                    OracleCommand cmdEmp = new OracleCommand();
                    cmdEmp.CommandText = strEmp;
                    dtEmp = oOraDCI.Query(cmdEmp);

                    if(dtEmp.Rows.Count > 0)
                    {

                        //===== Initial Excel =====
                        Excel.Application xlApp;
                        Excel.Workbook xlWorkBook;
                        Excel.Worksheet xlWorkSheet;
                        Excel.Range range;

                        string _pathFle = Application.StartupPath + "\\TEMPLATE\\FORM_PGD1.xls";

                        xlApp = new Excel.Application();
                        xlWorkBook = xlApp.Workbooks.Open(_pathFle, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                        xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                        //xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets["SLIP"];

                        object misValue = System.Reflection.Missing.Value;
                        //xlApp = new Microsoft.Office.Interop.Excel.Application();
                        xlApp.Visible = false;
                        //xlWorkBook = xlApp.Workbooks.Add(misValue);
                        range = xlWorkSheet.UsedRange;
                        //===== Initial Excel =====

                        int row = 8, itm = 1;

                        foreach (DataRow drPay in dtPay.Rows)
                        {
                            DataRow[] drEmp = dtEmp.Select(string.Format(" code = '{0}' ", drPay["code"].ToString()));

                            string _code = "", _name = "", _idno = "", _taxno = "", _payType = "เงินเดือน", _payTime = "1", _marry = "", _son = "", _join = "" , _pdate = "";
                            decimal _sonb = 0, _sons = 0, _inc = 0, _tax = 0;
                            if (drEmp.Count() > 0)
                            {
                                _code = drEmp[0]["code"].ToString();
                                _name = $"{drEmp[0]["pren"].ToString()} {drEmp[0]["name"].ToString()} {drEmp[0]["surn"].ToString()}";
                                _join = Convert.ToDateTime(drEmp[0]["join"].ToString()).ToString("dd/MMM/yyyy");
                                _idno = drEmp[0]["idno"].ToString();
                                _taxno = drEmp[0]["taxno"].ToString();                                
                                _marry = drEmp[0]["marry"].ToString();
                                try { _sons = Convert.ToDecimal(drEmp[0]["SONS"].ToString()); } catch { }
                                try { _sonb = Convert.ToDecimal(drEmp[0]["SONB"].ToString()); } catch { }
                                _son = (_sons + _sonb).ToString();


                                try { _inc = Convert.ToDecimal(drPay["inc"].ToString()); } catch { }
                                try { _tax = Convert.ToDecimal(drPay["tax"].ToString()); } catch { }
                                try { _pdate = Convert.ToDateTime(drPay["pdate"].ToString()).ToString("mm-yy"); } catch { }

                                (range.Cells[row, 1] as Excel.Range).Cells.Value2 = itm.ToString();
                                (range.Cells[row, 2] as Excel.Range).Cells.Value2 = _code;
                                (range.Cells[row, 3] as Excel.Range).Cells.Value2 = _name;
                                (range.Cells[row, 4] as Excel.Range).Cells.Value2 = _join;
                                (range.Cells[row, 5] as Excel.Range).Cells.Value2 = _idno;
                                (range.Cells[row, 6] as Excel.Range).Cells.Value2 = _taxno;
                                (range.Cells[row, 7] as Excel.Range).Cells.Value2 = "";
                                (range.Cells[row, 8] as Excel.Range).Cells.Value2 = _marry;
                                (range.Cells[row, 9] as Excel.Range).Cells.Value2 = _son;
                                (range.Cells[row, 10] as Excel.Range).Cells.Value2 = _payType;

                                (range.Cells[row, 11] as Excel.Range).Cells.Value2 = _inc.ToString("N2");
                                (range.Cells[row, 12] as Excel.Range).Cells.Value2 = _pdate;
                                (range.Cells[row, 13] as Excel.Range).Cells.Value2 = _inc.ToString("N2");
                                (range.Cells[row, 14] as Excel.Range).Cells.Value2 = _tax.ToString("N2");
                                (range.Cells[row, 15] as Excel.Range).Cells.Value2 = _payTime;

                            } // end check have 

                            itm++;
                            row++;
                        }




                        //===== End Excel =====
                        Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                        excel.DisplayAlerts = false;
                        xlApp.DisplayAlerts = false;
                         
                        //const int xlQualityStandard = 0;
                         
                        saveFileDlg.FileName = $"PGD_{dpkrDGDDate.Value.ToString("MMM_yyyy")}.xlsx";
                        saveFileDlg.RestoreDirectory = true;
                        saveFileDlg.Filter = "Excel Files (.xlsx)|*.xlsx;";
                        DialogResult dlg = saveFileDlg.ShowDialog();
                        if (dlg == DialogResult.OK)
                        {
                            string fileName = saveFileDlg.FileName;

                            xlWorkBook.SaveAs(fileName, Excel.XlFileFormat.xlOpenXMLWorkbook, Missing.Value,
                                Missing.Value, false, false, Excel.XlSaveAsAccessMode.xlNoChange,
                                Excel.XlSaveConflictResolution.xlUserResolution, true,
                                Missing.Value, Missing.Value, Missing.Value);
                        }

                        xlWorkBook.Close(false, misValue, misValue);
                        xlApp.Quit();

                        releaseObject(xlWorkSheet);
                        releaseObject(xlWorkBook);
                        releaseObject(xlApp);

                        //===== Initial Excel =====

                        MessageBox.Show("Generate PGD 1 for [ " + dpkrDGDDate.Value.ToString("MMM_yyyy") + " ] >>> Successful!!!");

                    } // end if check
                } // end if check 

                #endregion

            }
            else if (cbPGDType.SelectedValue.ToString() == "PGD1K")
            {

                #region PDG1K



                int dataYear = dpkrDGDDate.Value.Year;
                DateTime endDate = new DateTime(dataYear, 12, 31);
                DateTime lastDate = new DateTime(dataYear - 1, 12, 16);

                //
                DataTable dtPay = new DataTable();
                string str = @"SELECT em.code, em.tpren || ' ' || em.tname || '  ' || em.tsurn as name, em.sect,
                                 em.dv_ename, em.grpot, em.currentaddress, em.homeaddress, em.bus,
                                 em.STOP, emm.idno, emm.marry,
                                  (SELECT COUNT (*)
                                     FROM empm_family e
                                     WHERE e.relation = 'RELA4'
                                        AND e.taxdeduct <> 0
                                        AND e.code = em.code) AS CHILD,
                                 (SELECT COUNT (*)
                                  FROM empm_family e
                                  WHERE e.relation = 'RELA5'
                                        AND e.taxdeduct <> 0
                                        AND e.code = em.code) AS learningchild, emm.insur, emm.loan, emm.HANDYCAP , emm.LTF
                                  FROM vi_emp_mstr em, empm emm 
                                  WHERE  (emm.resign >= '" + lastDate.ToString("dd/MMM/yyyy") + @"' or emm.resign is null ) AND emm.join < '" + endDate.ToString("dd/MMM/yyyy") + @"'  AND em.code = emm.code
                                 order by em.code  ";
                OracleCommand cmd = new OracleCommand();
                cmd.CommandText = str;
                dtPay = oOraDCI.Query(cmd);


                if (dtPay.Rows.Count > 0)
                {
                    DataTable dtPRDT = new DataTable();
                    string strPRDT = @"SELECT code, sum(inc) as inacc ,sum(tax) as taxacc  
                                      FROM prdt WHERE pdate between '01/JAN/" + dataYear.ToString() + "' and '01/DEC/" + dataYear.ToString() + @"'
                                      GROUP BY code ";
                    OracleCommand cmdPRDT = new OracleCommand();
                    cmdPRDT.CommandText = strPRDT;
                    dtPRDT = oOraDCI.Query(cmdPRDT);

                    if (dtPRDT.Rows.Count > 0)
                    {

                        //===== Initial Excel =====
                        Excel.Application xlApp;
                        Excel.Workbook xlWorkBook;
                        Excel.Worksheet xlWorkSheet;
                        Excel.Range range;

                        string _pathFle = Application.StartupPath + "\\TEMPLATE\\FORM_PGD1K.xls";

                        xlApp = new Excel.Application();
                        xlWorkBook = xlApp.Workbooks.Open(_pathFle, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                        xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                        //xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets["SLIP"];

                        object misValue = System.Reflection.Missing.Value;
                        //xlApp = new Microsoft.Office.Interop.Excel.Application();
                        xlApp.Visible = false;
                        //xlWorkBook = xlApp.Workbooks.Add(misValue);
                        range = xlWorkSheet.UsedRange;
                        //===== Initial Excel =====

                        int row = 8, itm = 1;

                        foreach (DataRow drPay in dtPay.Rows)
                        {
                            DataRow[] drPRDT = dtPRDT.Select(string.Format(" code = '{0}' ", drPay["code"].ToString()));

                            string _code = "", _name = "", _idno = "", _taxno = "", _addr = "", _payType = "เงินเดือน", _taxType = "ค", _marry = "", _son = "", _join = "", _pdate = "";
                            decimal _sonb = 0, _sons = 0, _incacc = 0, _taxacc = 0;
                            if (drPRDT.Count() > 0)
                            {
                                _code = drPay["code"].ToString();
                                _name = $"{drPay["pren"].ToString()} {drPay["name"].ToString()} {drPay["surn"].ToString()}";
                                _addr = drPay["homeaddress"].ToString();
                                _idno = drPay["idno"].ToString();
                                _taxno = drPay["taxno"].ToString();
                                _marry = drPay["marry"].ToString();
                                try { _sons = Convert.ToDecimal(drPay["SONS"].ToString()); } catch { }
                                try { _sonb = Convert.ToDecimal(drPay["SONB"].ToString()); } catch { }
                                _son = (_sons + _sonb).ToString();


                                try { _incacc = Convert.ToDecimal(drPRDT[0]["inc"].ToString()); } catch { }
                                try { _taxacc = Convert.ToDecimal(drPRDT[0]["tax"].ToString()); } catch { }
                                try { _pdate = Convert.ToDateTime(drPay["pdate"].ToString()).ToString("dd/mmm/yyyy"); } catch { }

                                (range.Cells[row, 1] as Excel.Range).Cells.Value2 = itm.ToString();
                                (range.Cells[row, 2] as Excel.Range).Cells.Value2 = _code;
                                (range.Cells[row, 3] as Excel.Range).Cells.Value2 = _name;
                                (range.Cells[row, 4] as Excel.Range).Cells.Value2 = _idno;
                                (range.Cells[row, 5] as Excel.Range).Cells.Value2 = _addr;
                                (range.Cells[row, 6] as Excel.Range).Cells.Value2 = _marry;
                                (range.Cells[row, 7] as Excel.Range).Cells.Value2 = _son;
                                (range.Cells[row, 8] as Excel.Range).Cells.Value2 = _payType;
                                (range.Cells[row, 9] as Excel.Range).Cells.Value2 = _incacc.ToString("N2");
                                (range.Cells[row, 10] as Excel.Range).Cells.Value2 = _pdate;
                                (range.Cells[row, 11] as Excel.Range).Cells.Value2 = _incacc.ToString("N2");
                                (range.Cells[row, 12] as Excel.Range).Cells.Value2 = _taxacc.ToString("N2");
                                (range.Cells[row, 13] as Excel.Range).Cells.Value2 = _taxType;                                

                            } // end check have 

                            itm++;
                            row++;
                        }



                        //===== End Excel =====
                        Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                        excel.DisplayAlerts = false;
                        xlApp.DisplayAlerts = false;

                        //const int xlQualityStandard = 0;

                        saveFileDlg.FileName = $"PGDK_{dpkrDGDDate.Value.ToString("dd_MMM_yyyy")}.xlsx";
                        saveFileDlg.RestoreDirectory = true;
                        saveFileDlg.Filter = "Excel Files (.xlsx)|*.xlsx;";
                        DialogResult dlg = saveFileDlg.ShowDialog();
                        if (dlg == DialogResult.OK)
                        {
                            string fileName = saveFileDlg.FileName;

                            xlWorkBook.SaveAs(fileName, Excel.XlFileFormat.xlOpenXMLWorkbook, Missing.Value,
                                Missing.Value, false, false, Excel.XlSaveAsAccessMode.xlNoChange,
                                Excel.XlSaveConflictResolution.xlUserResolution, true,
                                Missing.Value, Missing.Value, Missing.Value);
                        }

                        xlWorkBook.Close(false, misValue, misValue);
                        xlApp.Quit();

                        releaseObject(xlWorkSheet);
                        releaseObject(xlWorkBook);
                        releaseObject(xlApp);

                        //===== Initial Excel =====

                        MessageBox.Show("Generate PGD 1K for [ " + dpkrDGDDate.Value.ToString("dd_MMM_yyyy") + " ] >>> Successful!!!");

                    } // end if check
                } // end if check 

                #endregion


            }
        
        }





        private string GenerateNumber(string _RunCode)
        {
            //CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            //CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CreateSpecificCulture("en-US");

            DataTable _dTable = new DataTable();
            DateTime _dt = DateTime.Now;
            DateTime _dbDate = new DateTime(1900, 1, 1);
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
                _dTable = oConHRM.Query(cmdRunNbr);
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
                oConHRM.ExecuteCommand(cmdUpd);


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
