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
using DCI.Security.Model;
using DCI.HRMS.Service;
using DCI.HRMS.Model.Personal;
using System.Collections;
using DCI.HRMS.Model.Allowance;
using DCI.HRMS.Util;
using DCI.HRMS.Service.SubContract;
using System.Web.Management;
using Oracle.ManagedDataAccess.Client;
using DCI.HRMS.Personal.DialogBox;
using CrystalDecisions.CrystalReports.Engine;
using OfficeOpenXml;
using System.IO;
using System.Web.UI.WebControls;
using Excel = Microsoft.Office.Interop.Excel;
using ExcelDataReader;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.qrcode;
using System.Data.SqlClient;

namespace DCI.HRMS.Personal
{
    public partial class FrmSkillAllowance : BaseForm, IFormParent, IFormPermission
    {
        private readonly string[] colNameS = new string[] { "RecordId", "Code", "CerType", "Level", "Cost", "Remark", "AddBy", "AddDate", "UpdateBy", "UpdateDate" };
        private readonly string[] propNameS = new string[] { "RecordId", "EmpCode", "CertName", "CertLevel", "CertCost", "Remark", "CreateBy", "CreateDateTime", "LastUpdateBy", "LastUpDateDateTime" };
        ApplicationManager appMgr = ApplicationManager.Instance();

        private EmployeeService empSvr = EmployeeService.Instance();
        private SubContractService subSvr = SubContractService.Instance();


        private SkillAllowanceService sklSvr = SkillAllowanceService.Instance();
        private SubContractSkillAllowanceService subSklSvr = SubContractSkillAllowanceService.Instance();
        private ArrayList gvData = new ArrayList();
        private CertificateInfo sklInfo = new CertificateInfo();
        private string empCode = "";
        private StatusManager stsMng = new StatusManager();

        ClsOraConnectDB oOraDCI = new ClsOraConnectDB("DCI");
        ClsOraConnectDB oOraSUB = new ClsOraConnectDB("DCISUB");


        public FrmSkillAllowance()
        {
            InitializeComponent();
        }

        private void FrmSkillAllowance_Load(object sender, EventArgs e)
        {
            Open();
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
            if (ucl_ActionControl1.Permission.AllowAddNew)
            {
                if (empCode != "")
                {

                    EmpSkillAllowanceInfo item = new EmpSkillAllowanceInfo();
                    item.EmpCode = empCode;
                    item.Month = DateTime.Parse("01/" + cmbSkillMonth.SelectedItem.ToString());
                    item.CertType = sklInfo.CerType;
                    item.CertLevel = sklInfo.Level;
                    item.Remark = txtRemark.Text;
                    item.CreateBy = appMgr.UserAccount.AccountId;
                    try
                    {
                        if (item.EmpCode.StartsWith("I"))
                        {
                            subSklSvr.SaveSkillAllowance(item);
                        }
                        else
                        {
                            sklSvr.SaveSkillAllowance(item);
                        }
                        // gvData = sklSvr.GetSkillAllowancwByCode(empCode);
                        // FillDataGrid();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }

        }

        public void Delete()
        {
            if (ucl_ActionControl1.Permission.AllowDelete)
            {
                if (MessageBox.Show("คุณต้องการลบข้อมูลใช่หรือไม่", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    foreach (DataGridViewRow item in dgItems.SelectedRows)
                    {
                        string rcid = item.Cells["RecordId"].Value.ToString();
                        string empcd = item.Cells["EmpCode"].Value.ToString();

                        if (empcd.StartsWith("I"))
                        {
                            subSklSvr.DeleteSkillAllow(rcid);
                        }
                        else
                        {
                            sklSvr.DeleteSkillAllow(rcid);
                        }

                    }
                    Search();
                }
            }
            txtBarCode.Focus();

        }

        public void Search()
        {

            if (empCode.StartsWith("I"))
            {
                gvData = subSklSvr.GetSkillAllowancwByCode(empCode, DateTime.Parse("01/" + cmbSkillMonth.SelectedItem.ToString()));
            }
            else
            {
                gvData = sklSvr.GetSkillAllowancwByCode(empCode, DateTime.Parse("01/" + cmbSkillMonth.SelectedItem.ToString()));
            }
            FillDataGrid();

        }

        public void Export()
        {
            DialogExportSkillAllowance dlg = new DialogExportSkillAllowance();
            dlg.ShowDialog();

            if (dlg.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                //MessageBox.Show(dlg._empType);

                DateTime dataDate = DateTime.Parse("01/" + ConvertMonth(cmbSkillMonth.SelectedItem.ToString()));

                DataTable dtCert = new DataTable();
                string strCert = @"SELECT a.rc_id, a.code, a.ctype, a.clevel, a.remark, a.cname, a.cdate, a.cexpire, a.cr_by, a.cr_dt, a.upd_by, a.upd_dt, s.ccost  
                               FROM vi_empm_cert a
                               LEFT JOIN empm e ON e.code = a.code 
                               LEFT JOIN DCI.skill_cert_mstr s ON s.type = a.ctype and s.CLEVEL = a.clevel
                               WHERE a.code LIKE '%' and e.resign IS NULL 
                                    and (cexpire > '" + dataDate.ToString("dd/MMM/yyyy") + "' or cexpire = '01/jan/0001' or cexpire = '01/jan/1900' or cexpire is null)  ";
                OracleCommand cmdCert = new OracleCommand();
                cmdCert.CommandText = strCert;

                if (dlg._empType == "DCI")
                {
                    dtCert = oOraDCI.Query(cmdCert);
                }
                else if (dlg._empType == "SUB")
                {
                    dtCert = oOraSUB.Query(cmdCert);
                }
                else if (dlg._empType == "TRN")
                {

                }


                if (dtCert.Rows.Count > 0)
                {
                    List<EmployeeSkillAllowanceInfo> oArySkill = new List<EmployeeSkillAllowanceInfo>();
                    foreach (DataRow drCert in dtCert.Rows)
                    {
                        EmployeeSkillAllowanceInfo mSkill = new EmployeeSkillAllowanceInfo();
                        mSkill.code = drCert["code"].ToString();
                        mSkill.ctype = drCert["ctype"].ToString();
                        mSkill.clevel = drCert["clevel"].ToString();
                        mSkill.remark = DecodeLanguage(drCert["remark"].ToString());
                        mSkill.cname = DecodeLanguage(drCert["cname"].ToString());
                        mSkill.cdate = drCert["cdate"].ToString();
                        mSkill.cexpire = drCert["cexpire"].ToString();
                        mSkill.ccost = drCert["ccost"].ToString();



                        oArySkill.Add(mSkill);
                    }



                    SaveFileDialog saveFileDlg = new SaveFileDialog();
                    saveFileDlg.FileName = "SkillAllowance_" + dlg._empType + "_" + dataDate.ToString("ddMMMyyyy") + "_" + DateTime.Now.ToString("yyyyMMdd");
                    saveFileDlg.RestoreDirectory = true;
                    saveFileDlg.Filter = "Excel Files (.xlsx)|*.xlsx;";
                    DialogResult dlgSave = saveFileDlg.ShowDialog();
                    if (dlgSave == DialogResult.OK)
                    {

                        if (File.Exists(saveFileDlg.FileName))
                        {
                            File.Delete(saveFileDlg.FileName);
                        }


                        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                        string _pathFle = Application.StartupPath + "\\TEMPLATE\\TMP_SKILL_ALLOWANCE.xlsx";
                        FileInfo template = new FileInfo(_pathFle);

                        using (var package = new ExcelPackage(template))
                        {
                            ExcelWorksheet wsManpower = package.Workbook.Worksheets["DATA"];
                            wsManpower.Cells["A1"].Value = "Skill Allowance : " + dataDate.ToString("dd/MMM/yyyy");
                            wsManpower.Cells["A3:H" + oArySkill.Count + 3].LoadFromCollection(oArySkill);

                            package.SaveAs(new FileInfo(saveFileDlg.FileName));
                        }

                        MessageBox.Show("Success in " + oArySkill.Count.ToString() + " records.");
                    }

                }




            }



        }

        public void Print()
        {

        }

        public void Open()
        {

            ucl_ActionControl1.Owner = this;
            txtBarCode.Focus();
            EmpCertificate_Control1.sklSvr = sklSvr;
            EmpCertificate_Control1.subSklSvr = subSklSvr;
            EmpCertificate_Control1.Open();
            for (DateTime i = DateTime.Today.AddMonths(1); i > DateTime.Today.AddMonths(-12); i = i.AddMonths(-1))
            {
                cmbSkillMonth.Items.Add(i.ToString("MM/yyyy"));
            }
            cmbSkillMonth.SelectedItem = DateTime.Now.ToString("MM/yyyy");


            DataTable dtSource = new DataTable();
            dtSource.Columns.Add("colDis", typeof(string));
            dtSource.Columns.Add("colVal", typeof(string));
            for (int y = 0; y > -10; y--)
            {
                int year = DateTime.Now.AddYears(y).Year;
                dtSource.Rows.Add(year.ToString(), year.ToString());
            }

            cbExptSkillYear.DataSource = dtSource;
            cbExptSkillYear.ValueMember = "colVal";
            cbExptSkillYear.DisplayMember = "colDis";
            cbExptSkillYear.Refresh();

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

        #region IFormPermission Members

        public PermissionInfo Permission
        {
            set { ucl_ActionControl1.Permission = value; }
        }

        #endregion




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
                //  column.Width = widthS[index];

                columns[index] = column;
                dgItems.Columns.Add(columns[index]);
            }
            //dgItems.ClearSelection();
        }

        private void FillDataGrid()
        {
            decimal tt = 0M;

            dgItems.DataBindings.Clear();
            dgItems.DataSource = null;
            dgItems.DataSource = gvData;

            if (gvData != null)
            {
                foreach (EmpSkillAllowanceInfo item in gvData)
                {
                    tt += item.CertCost;
                }
            }
            else
            {
                tt = 0;
            }

            kryptonHeaderGroup1.ValuesSecondary.Heading = "Total =" + tt.ToString();
            this.Update();

        }
        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtBarCode.Text.Trim() != "")
                {

                    if (!txtBarCode.Text.Contains("-"))
                    {
                        /*Employee Code Input */


                        EmployeeDataInfo empData = empSvr.GetEmployeeData(txtBarCode.Text);

                        if (empData == null)
                        {
                            empData = subSvr.GetEmployeeData(txtBarCode.Text);
                        }

                        if (empData != null)
                        {
                            if (empData.Resigned)
                            {
                                MessageBox.Show("พนักงานรหัส: " + empData.Code + " ลาออกไปแล้ว", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }

                            empCode = txtBarCode.Text;
                            empData_Control1.Information = empData;
                            //    EmpCertificate_Control1.SetData(empCode);
                            //EmpCertificate_Control1.SetData(empCode, chkShowExpired.Checked, DateTime.Parse("01/" + cmbSkillMonth.SelectedItem.ToString()));
                            EmpCertificate_Control1.SetData(empCode, chkShowExpired.Checked, DateTime.Parse("01/" + ConvertMonth(cmbSkillMonth.SelectedItem.ToString())));

                            Search();
                        }
                        else
                        {
                            /*Employee Data not found*/
                            MessageBox.Show("ไม่พบข้อมูลพนักงาน รหัส " + txtBarCode.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                    else
                    {

                        /*Skill Data Input*/
                        string code = (txtBarCode.Text.Split('-'))[0];

                        sklInfo = sklSvr.GetCerType(code, 1);
                        if (sklInfo != null)
                        {
                            Save();
                            Search();
                        }
                        else
                        {
                            MessageBox.Show("ไม่พบข้อมูล" + txtBarCode.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                        }
                    }
                }
                txtBarCode.Clear();

            }
        }

        private void dgItems_SelectionChanged(object sender, EventArgs e)
        {
            if (dgItems.SelectedRows.Count > 0)
            {
                ucl_ActionControl1.CurrentAction = FormActionType.None;
            }
            else
            {
                ucl_ActionControl1.CurrentAction = FormActionType.None;
            }
        }

        private void dgItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46)
            {
                Delete();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void dgItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgItems_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyleDefault.ShowRowNumber(dgItems, e);


        }

        private void cmbSkillMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
            if (empCode != "")
            {
                //EmpCertificate_Control1.SetData(empCode, chkShowExpired.Checked, DateTime.Parse("01/" + cmbSkillMonth.SelectedItem.ToString()));
                EmpCertificate_Control1.SetData(empCode, chkShowExpired.Checked, DateTime.Parse("01/" + ConvertMonth(cmbSkillMonth.SelectedItem.ToString())));
            }
        }

        private void cmbSkillMonth_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPressManager.Enter(e);
        }

        public string ConvertMonth(string _MonYear)
        {
            string[] oAry = { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };
            return oAry[Convert.ToInt32(_MonYear.Substring(0, 2)) - 1] + "/" + _MonYear.Substring(3, 4);
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            DateTime dataDate = DateTime.Parse("01/" + ConvertMonth(cmbSkillMonth.SelectedItem.ToString()));


            if (DateTime.Now.AddMonths(-1) < dataDate)
            {
                if (MessageBox.Show("This will generate skill allowance  of month " + cmbSkillMonth.SelectedItem.ToString() + ". \nDo you want to continue?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    stsMng.Status = "Getting Certificate Data";

                    DataTable dtCert = new DataTable();
                    string strCert = @"SELECT a.rc_id, a.code, a.ctype, a.clevel, a.remark, a.cname, a.cdate, a.cexpire, a.cr_by, a.cr_dt, a.upd_by, a.upd_dt 
                               FROM DCI.vi_empm_cert a
                               LEFT JOIN DCI.empm e ON e.code = a.code 
                               WHERE a.code LIKE '%' and e.resign IS NULL 
                                    and (cexpire > '" + dataDate.ToString("dd/MMM/yyyy") + "' or cexpire = '01/jan/0001' or cexpire = '01/jan/1900' or cexpire is null)  ";
                    OracleCommand cmdCert = new OracleCommand();
                    cmdCert.CommandText = strCert;
                    dtCert = oOraDCI.Query(cmdCert);

                    //********************************
                    stsMng.MaxProgress = dtCert.Rows.Count;
                    int count = 0;
                    if (dtCert.Rows.Count > 0)
                    {

                        //***** Clear Old Before Add New *****
                        string strDelCert = @"DELETE FROM DCI.EMPM_SKILL_CERT WHERE month = '" + dataDate.ToString("dd/MMM/yyyy") + "' ";
                        OracleCommand cmdDelCert = new OracleCommand();
                        cmdDelCert.CommandText = strDelCert;
                        oOraDCI.ExecuteCommand(cmdDelCert);
                        //***** Clear Old Before Add New *****



                        foreach (DataRow drCert in dtCert.Rows)
                        {
                            EmpSkillAllowanceInfo sklItem = new EmpSkillAllowanceInfo();
                            sklItem.EmpCode = drCert["code"].ToString();
                            sklItem.Month = DateTime.Parse("01/" + ConvertMonth(cmbSkillMonth.SelectedItem.ToString()));
                            sklItem.CertType = drCert["ctype"].ToString();
                            sklItem.CertLevel = Convert.ToInt32(drCert["clevel"].ToString());
                            sklItem.Remark = txtRemark.Text;
                            sklItem.CreateBy = appMgr.UserAccount.AccountId;
                            try
                            {
                                sklSvr.SaveSkillAllowance(sklItem);
                            }
                            catch (Exception ex)
                            {
                                //  MessageBox.Show("ไม่สามารถบันทึกข้อมูล " + item.EmpCode + " ได้เนื่องจาก" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }



                            //****************
                            stsMng.Progress = count;
                            count++;

                            //****************
                            stsMng.Status = String.Format("Processing {0} / {1} [{2}]", count.ToString(), dtCert.Rows.Count.ToString(), drCert["code"].ToString());

                        } // end foreach

                        gvData = sklSvr.GetSkillAllowancwByCode(empCode);
                        FillDataGrid();
                    }


                    MessageBox.Show("Generate Complete", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    stsMng.Progress = 0;
                    stsMng.Status = "Ready";
                    this.Cursor = Cursors.Default;


                }
            }
            else
            {
                MessageBox.Show("Not allow to edit old data.");
            }



            #region Commnet Old Code
            /*

            ArrayList allEmp = sklSvr.GetCertificateByCode("%");
            ArrayList subEmp = subSklSvr.GetSkillAllowancwByCode("%");
            if (allEmp == null)
            {
                allEmp = new ArrayList();
            }
            if (subEmp != null)
            {
                foreach (EmpCertInfo item in subEmp)
                {
                    allEmp.Add(item);
                }
            }

            stsMng.MaxProgress = allEmp.Count;
            int count = 0;
            foreach (EmpCertInfo item in allEmp)
            {
                stsMng.Status = "Generating Data " + item.EmpCode;
                //ArrayList cerList = sklSvr.GetCertificateByCode(item.);




                EmployeeDataInfo empData = empSvr.GetEmployeeData(item.EmpCode);
                if (empData == null)
                {
                    empData = subSvr.GetEmployeeData(item.EmpCode);
                }
                if (empData != null)
                {
                    if (!empData.Resigned)
                    {

                        // if (empData.Position.Code == "OP" || empData.Position.Code == "LE")//
                        // if (empData.Position.Code == "OP" || empData.Position.Code == "LE"||empData.Position.Code == "OP.S" || empData.Position.Code == "LE.S")
                        {
                            //if (item.ExpireDate == DateTime.MinValue || item.ExpireDate >= DateTime.Parse("01/" + cmbSkillMonth.SelectedItem.ToString()))
                            if (item.ExpireDate == new DateTime(1900,1,1) || item.ExpireDate >= DateTime.Parse("01/" + ConvertMonth(cmbSkillMonth.SelectedItem.ToString())))
                            {
                                EmpSkillAllowanceInfo sklItem = new EmpSkillAllowanceInfo();
                                sklItem.EmpCode = item.EmpCode;
                                sklItem.Month = DateTime.Parse("01/" + ConvertMonth(cmbSkillMonth.SelectedItem.ToString()));
                                sklItem.CertType = item.CerType;
                                sklItem.CertLevel = item.Level;
                                sklItem.Remark = txtRemark.Text;
                                sklItem.CreateBy = appMgr.UserAccount.AccountId;                                    
                                try
                                {
                                    if (sklItem.EmpCode.StartsWith("I"))
                                    {
                                        subSklSvr.SaveSkillAllowance(sklItem);
                                    }
                                    else
                                    {
                                        sklSvr.SaveSkillAllowance(sklItem);
                                    }
                                     gvData = sklSvr.GetSkillAllowancwByCode(empCode);
                                     FillDataGrid();
                                }
                                catch (Exception ex)
                                {

                                    //  MessageBox.Show("ไม่สามารถบันทึกข้อมูล " + item.EmpCode + " ได้เนื่องจาก" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                        }


                    }
                }
                stsMng.Progress = count;
                count++;
            }


            MessageBox.Show("Generate Complete", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

            stsMng.Progress = 0;
            stsMng.Status = "Ready";
            this.Cursor = Cursors.Default;
            */

            #endregion


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

        private void chkShowExpired_CheckedChanged(object sender, EventArgs e)
        {
            if (empCode != "")
            {
                //EmpCertificate_Control1.SetData(empCode, chkShowExpired.Checked, DateTime.Parse("01/" + cmbSkillMonth.SelectedItem.ToString()));
                EmpCertificate_Control1.SetData(empCode, chkShowExpired.Checked, DateTime.Parse("01/" + ConvertMonth(cmbSkillMonth.SelectedItem.ToString())));
            }
        }

        private void btnExptSkill_Click(object sender, EventArgs e)
        {
            string folderSource = "";
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog(this) != DialogResult.Cancel)
            {
                folderSource = dlg.SelectedPath;
                pnDisplay.Visible = true;
            }
            else
            {
                pnDisplay.Visible = false;
                MessageBox.Show("ยกเลิก");
                return;
            }


            string srtDate = $"01/JAN/{cbExptSkillYear.SelectedValue.ToString()}";
            string endDate = $"31/DEC/{cbExptSkillYear.SelectedValue.ToString()}";
            string Year = cbExptSkillYear.SelectedValue.ToString();

            DataTable dtEmpCert = new DataTable();
            string strEmpCert = $@"SELECT a.ctype, a.clevel, a.cname, a.code,   
                                        v.CODE EMPCODE, v.FNAME, v.TFNAME,
                                        v.WSTS, v.WTYPE, v.dvcd, v.DV_DESCR, v.DEPT, v.SECT, v.GRP ,v.POSI_CD, v.resign 
                                    FROM (
                                        SELECT a.ctype, a.clevel, a.cname, a.code, cexpire 
                                        FROM DCI.vi_empm_cert a

                                        UNION ALL

                                        SELECT a.ctype, a.clevel, a.cname, a.code, cexpire
                                        FROM DCITC.vi_empm_cert a
                                    ) a 
                                    LEFT JOIN (

                                        SELECT e.CODE, e.PREN || e.NAME || ' ' || e.SURN FNAME,
                                            e.TPREN || e.TNAME || ' ' || e.TSURN TFNAME,
                                            e.WSTS, e.WTYPE, e.dvcd, e.DV_DESCR, e.DEPT, e.SECT, e.GRP ,e.POSI_CD, e.resign 
                                        FROM DCI.VI_EMP_MSTR e 
                                        WHERE (e.resign IS NULL OR e.resign = '01/jan/1900' OR e.resign >= '{srtDate}' ) 

                                        UNION ALL

                                        SELECT e.CODE, e.PREN || e.NAME || ' ' || e.SURN FNAME,
                                            e.TPREN || e.TNAME || ' ' || e.TSURN TFNAME,
                                            e.WSTS, e.WTYPE, e.dvcd, e.DV_DESCR, e.DEPT, e.SECT, e.GRP ,e.POSI_CD, e.resign
                                        FROM DCITC.VI_EMP_MSTR e 
                                        WHERE (e.resign IS NULL OR e.resign = '01/jan/1900' OR e.resign >= '{srtDate}' ) 

                                        UNION ALL

                                        SELECT e.CODE, e.PREN || e.NAME || ' ' || e.SURN FNAME,
                                            e.TPREN || e.TNAME || ' ' || e.TSURN TFNAME,
                                            e.WSTS, e.WTYPE, e.dvcd, e.DV_DESCR, e.DEPT, e.SECT, e.GRP ,e.POSI_CD, e.resign
                                        FROM DEV_OFFICE.VI_EMP_MSTR e 
                                        WHERE (e.resign IS NULL OR e.resign = '01/jan/1900' OR e.resign >= '{srtDate}' ) 
                                    ) v ON v.code = a.code 
                                    WHERE v.code is not null and (v.resign IS NULL OR v.resign = '01/jan/1900' OR v.resign >= '{srtDate}' ) 
                                            and ((cexpire >= '{srtDate}' and cexpire >= '{endDate}') or cexpire = '01/jan/0001' or cexpire = '01/jan/1900' or cexpire is null) 

                                            AND SUBSTR(ctype,1,1) NOT IN ('A','B','C') 
                                    ORDER BY ctype, dvcd ASC
                                            ";
            OracleCommand cmdEmpCert = new OracleCommand();
            cmdEmpCert.CommandText = strEmpCert;
            dtEmpCert = oOraDCI.Query(cmdEmpCert);


            //****** Distinct Certificate *********
            DataView dvCert = new DataView(dtEmpCert);
            DataTable dtCerts = dvCert.ToTable(true, "ctype", "clevel", "cname");


            if (dtCerts.Rows.Count > 0)
            {
                List<MDSkillAllowanceInfo> oSkills = new List<MDSkillAllowanceInfo>();

                string ctype = "";
                string clevel = "";
                string cname = "";
                int pages = 1;


                //=== Cert ===
                foreach (DataRow drCert in dtCerts.Rows)
                {
                    ctype = drCert["ctype"].ToString();
                    clevel = drCert["clevel"].ToString();
                    cname = DecodeLanguage(drCert["cname"].ToString());
                    pages = 1;


                    //==== Finding By Type ======
                    DataRow[] drEmpCerts = dtEmpCert.Select($"ctype='{ctype}' AND clevel='{clevel}' ");
                    if (drEmpCerts.Length > 0)
                    {

                        // Loop Employee Certs 
                        int _row = 12, _idx = 1, _no = 1;
                        foreach (DataRow drEmpCert in drEmpCerts)
                        {
                            string code = drEmpCert["code"].ToString();
                            string fname = DecodeLanguage(drEmpCert["FNAME"].ToString());
                            string tfname = DecodeLanguage(drEmpCert["TFNAME"].ToString());
                            string posit = drEmpCert["POSI_CD"].ToString();
                            string dvcd = drEmpCert["dvcd"].ToString();

                            string grp_short = drEmpCert["DV_DESCR"].ToString();
                            string grp = drEmpCert["GRP"].ToString();
                            string sect = drEmpCert["DEPT"].ToString();


                            if (_idx == 20)
                            {
                                // export to pdf 
                                generateSkillAllowance(Year, folderSource, false, oSkills);
                                oSkills = new List<MDSkillAllowanceInfo>();

                                pages++;
                                _idx = 0;
                                _row = 12;
                            }



                            MDSkillAllowanceInfo oSkill = new MDSkillAllowanceInfo();
                            oSkill.ctype = ctype;
                            oSkill.cname = cname;
                            oSkill.no = _no;
                            oSkill.code = code;
                            oSkill.fname = tfname;
                            oSkill.grp_short = grp_short;
                            oSkill.grp = grp;
                            oSkill.dept = sect;
                            oSkill.page = pages;

                            oSkills.Add(oSkill);

                            _no++;
                            _idx++;
                            _row++;
                        } // end foreach



                        // ==== Print Lastest ====
                        if (oSkills.Count > 0)
                        {
                            // export to pdf 
                            generateSkillAllowance(Year, folderSource, false, oSkills);
                            oSkills = new List<MDSkillAllowanceInfo>();
                        }

                    } // end if



                }// end foreach




                //******* Combine File PDF *********
                string[] filePaths = Directory.GetFiles($"{folderSource}\\", "*.pdf");

                CombineMultiplePDFs(filePaths, $"{folderSource}\\all_skill_allow_{cbExptSkillYear.SelectedValue.ToString()}.pdf");
                //******* End Combine File PDF *********


            } // end if

            pnDisplay.Visible = false;
            MessageBox.Show("Finish");
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
                    try
                    {
                        //@File.Delete(fileName);
                    }
                    catch
                    {

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


        public bool generateSkillAllowance(string Year, string folderSource, bool isMeasurement, List<MDSkillAllowanceInfo> oSkills)
        {
            bool result = true;
            string _pathFle = System.Windows.Forms.Application.StartupPath + "\\TEMPLATE\\FORM_SKILL_ALLOWANCE.xlsx";
            if (!Directory.Exists(folderSource))
            {
                return false;
            }

            //=========================================
            //         BEGIN EXCEL
            //=========================================
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
            //=========================================
            //         BEGIN EXCEL
            //=========================================


            (range.Cells[2, 8] as Excel.Range).Cells.Value2 = $"{oSkills[0].page}";



            // Cert Title
            (range.Cells[2, 4] as Excel.Range).Cells.Value2 = $"{oSkills[0].ctype}={oSkills[0].cname}";


            (range.Cells[10, 7] as Excel.Range).Cells.Value2 = $"YEAR {Year}";


            string A1 = (isMeasurement) ? "A1=Vernier" : "";
            string A2 = (isMeasurement) ? "A2=Micrometer" : "";
            string A3 = (isMeasurement) ? "A3=Dial Gage" : "";
            string A4 = (isMeasurement) ? "A4=Cylinder Gage" : "";
            string B1 = (isMeasurement) ? "B1=Air Micro" : "";
            string B2 = (isMeasurement) ? "B2=Electric Micro" : "";
            string B3 = (isMeasurement) ? "B3=Limit Gage" : "";
            string B4 = (isMeasurement) ? "B4=Height Gage" : "";
            string C2 = (isMeasurement) ? "C2=Roughness Tester/Contracer" : "";
            string C3 = (isMeasurement) ? "C3=Profile Projector/Microscope" : "";


            (range.Cells[3, 15] as Excel.Range).Cells.Value2 = $"{A1}";
            (range.Cells[3, 17] as Excel.Range).Cells.Value2 = $"{B2}";
            (range.Cells[4, 15] as Excel.Range).Cells.Value2 = $"{A2}";
            (range.Cells[4, 17] as Excel.Range).Cells.Value2 = $"{B3}";
            (range.Cells[5, 15] as Excel.Range).Cells.Value2 = $"{A3}";
            (range.Cells[5, 17] as Excel.Range).Cells.Value2 = $"{B4}";
            (range.Cells[6, 15] as Excel.Range).Cells.Value2 = $"{A4}";
            (range.Cells[6, 17] as Excel.Range).Cells.Value2 = $"{C2}";
            (range.Cells[7, 15] as Excel.Range).Cells.Value2 = $"{B1}";
            (range.Cells[7, 17] as Excel.Range).Cells.Value2 = $"{C3}";


            int _row = 12, _idx = 0;
            for (int rec = 0; rec < 20; rec++)
            {
                string _no = (oSkills.Count > rec) ? oSkills[rec].no.ToString() : "";
                string _code = (oSkills.Count > rec) ? oSkills[rec].code.ToString() : "";
                string _tfname = (oSkills.Count > rec) ? oSkills[rec].fname.ToString() : "";
                string _grp_short = (oSkills.Count > rec) ? oSkills[rec].grp_short.ToString() : "";
                string _grp = (oSkills.Count > rec) ? oSkills[rec].grp.ToString() : "";
                string _sect = (oSkills.Count > rec) ? oSkills[rec].dept.ToString() : "";

                (range.Cells[_row + rec, 1] as Excel.Range).Cells.Value2 = $"{_no}";
                (range.Cells[_row + rec, 2] as Excel.Range).Cells.Value2 = $"{_code}";
                (range.Cells[_row + rec, 3] as Excel.Range).Cells.Value2 = $"{_tfname}";
                (range.Cells[_row + rec, 4] as Excel.Range).Cells.Value2 = $"{_grp_short}";
                (range.Cells[_row + rec, 5] as Excel.Range).Cells.Value2 = $"{_grp}";
                (range.Cells[_row + rec, 6] as Excel.Range).Cells.Value2 = $"{_sect}";
            }





            //=========================================
            //         END EXCEL
            //=========================================
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


            string fldName = $"{folderSource}\\Skill_Allow_{oSkills[0].ctype}_{oSkills[0].page.ToString("00")}.pdf";
            //**** Save to PDF ****
            xlWorkSheet.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, fldName, xlQualityStandard, true, false,
                    Type.Missing, Type.Missing, false, Type.Missing);

            xlWorkBook.Close(false, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

            //=========================================
            //         END EXCEL
            //=========================================

            return result;

        }


        private void btnExptSkillDVCD_Click(object sender, EventArgs e)
        {
            string folderSource = "";
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog(this) != DialogResult.Cancel)
            {
                folderSource = dlg.SelectedPath;

                pnDisplay.Visible = true;
            }
            else
            {
                pnDisplay.Visible = false;
                MessageBox.Show("ยกเลิก");
                return;
            }


            string srtDate = $"01/JAN/{cbExptSkillYear.SelectedValue.ToString()}";
            string endDate = $"31/DEC/{cbExptSkillYear.SelectedValue.ToString()}";
            string Year = cbExptSkillYear.SelectedValue.ToString();

            DataTable dtEmpCert = new DataTable();
            string strEmpCert = $@"SELECT a.ctype, a.clevel, a.cname, a.code,   
                                        v.CODE EMPCODE, v.FNAME, v.TFNAME,
                                        v.WSTS, v.WTYPE, v.dvcd, v.DV_DESCR, v.DEPT, v.SECT, v.GRP ,v.POSI_CD, v.resign 
                                    FROM (
                                        SELECT a.ctype, a.clevel, a.cname, a.code, cexpire 
                                        FROM DCI.vi_empm_cert a

                                        UNION ALL

                                        SELECT a.ctype, a.clevel, a.cname, a.code, cexpire
                                        FROM DCITC.vi_empm_cert a
                                    ) a 
                                    LEFT JOIN (

                                        SELECT e.CODE, e.PREN || e.NAME || ' ' || e.SURN FNAME,
                                            e.TPREN || e.TNAME || ' ' || e.TSURN TFNAME,
                                            e.WSTS, e.WTYPE, e.dvcd, e.DV_DESCR, e.DEPT, e.SECT, e.GRP ,e.POSI_CD, e.resign 
                                        FROM DCI.VI_EMP_MSTR e 
                                        WHERE (e.resign IS NULL OR e.resign = '01/jan/1900' OR e.resign >= '{srtDate}' ) 

                                        UNION ALL

                                        SELECT e.CODE, e.PREN || e.NAME || ' ' || e.SURN FNAME,
                                            e.TPREN || e.TNAME || ' ' || e.TSURN TFNAME,
                                            e.WSTS, e.WTYPE, e.dvcd, e.DV_DESCR, e.DEPT, e.SECT, e.GRP ,e.POSI_CD, e.resign
                                        FROM DCITC.VI_EMP_MSTR e 
                                        WHERE (e.resign IS NULL OR e.resign = '01/jan/1900' OR e.resign >= '{srtDate}' ) 

                                        UNION ALL

                                        SELECT e.CODE, e.PREN || e.NAME || ' ' || e.SURN FNAME,
                                            e.TPREN || e.TNAME || ' ' || e.TSURN TFNAME,
                                            e.WSTS, e.WTYPE, e.dvcd, e.DV_DESCR, e.DEPT, e.SECT, e.GRP ,e.POSI_CD, e.resign
                                        FROM DEV_OFFICE.VI_EMP_MSTR e 
                                        WHERE (e.resign IS NULL OR e.resign = '01/jan/1900' OR e.resign >= '{srtDate}' ) 
                                    ) v ON v.code = a.code 
                                    WHERE v.code is not null and (v.resign IS NULL OR v.resign = '01/jan/1900' OR v.resign >= '{srtDate}' ) 
                                            and ((cexpire >= '{srtDate}' and cexpire >= '{endDate}') or cexpire = '01/jan/0001' or cexpire = '01/jan/1900' or cexpire is null) 

                                            AND SUBSTR(ctype,1,1) IN ('A','B','C')  
                                    ORDER BY ctype, dvcd ASC
                                            ";
            OracleCommand cmdEmpCert = new OracleCommand();
            cmdEmpCert.CommandText = strEmpCert;
            dtEmpCert = oOraDCI.Query(cmdEmpCert);


            //****** Distinct Certificate *********
            DataView dvCert = new DataView(dtEmpCert);
            DataTable dtCerts = dvCert.ToTable(true, "ctype", "clevel", "cname");





            if (dtCerts.Rows.Count > 0)
            {

                List<MDSkillAllowanceInfo> oSkills = new List<MDSkillAllowanceInfo>();





                string _pathFle = System.Windows.Forms.Application.StartupPath + "\\TEMPLATE\\FORM_SKILL_ALLOWANCE.xlsx";
                if (!Directory.Exists(folderSource))
                {
                    pnDisplay.Visible = false;
                    MessageBox.Show("เลือก Folder ที่จัดเก็บก่อน");
                    return;
                }


                string ctype = "";
                string clevel = "";
                string cname = "";
                int pages = 1;
                string dvcd = "";


                //=== Cert ===
                foreach (DataRow drCert in dtCerts.Rows)
                {

                    ctype = drCert["ctype"].ToString();
                    clevel = drCert["clevel"].ToString();
                    cname = DecodeLanguage(drCert["cname"].ToString());
                    pages = 1;
                    dvcd = "";


                    //==== Finding By Type ======
                    DataRow[] drEmpCerts = dtEmpCert.Select($"ctype='{ctype}' AND clevel='{clevel}' ");
                    if (drEmpCerts.Length > 0)
                    {

                        // Loop Employee Certs 
                        int _row = 12, _idx = 1, _no = 1;
                        foreach (DataRow drEmpCert in drEmpCerts)
                        {
                            string code = drEmpCert["code"].ToString();
                            string fname = DecodeLanguage(drEmpCert["FNAME"].ToString());
                            string tfname = DecodeLanguage(drEmpCert["TFNAME"].ToString());
                            string posit = drEmpCert["POSI_CD"].ToString();
                            if (_no == 1) { dvcd = drEmpCert["dvcd"].ToString(); }


                            string grp_short = drEmpCert["DV_DESCR"].ToString();
                            string grp = drEmpCert["GRP"].ToString();
                            string sect = drEmpCert["DEPT"].ToString();


                            if (_idx == 20 || dvcd != drEmpCert["dvcd"].ToString())
                            {

                                // export to pdf 
                                generateSkillAllowance(Year, folderSource, false, oSkills);
                                oSkills = new List<MDSkillAllowanceInfo>();

                                dvcd = drEmpCert["dvcd"].ToString();
                                pages++;
                                _idx = 0;
                                _row = 12;
                            }



                            MDSkillAllowanceInfo oSkill = new MDSkillAllowanceInfo();
                            oSkill.ctype = ctype;
                            oSkill.cname = cname;
                            oSkill.no = _no;
                            oSkill.code = code;
                            oSkill.fname = tfname;
                            oSkill.grp_short = grp_short;
                            oSkill.grp = grp;
                            oSkill.dept = sect;
                            oSkill.page = pages;

                            oSkills.Add(oSkill);

                            _no++;
                            _idx++;
                            _row++;

                        } // end foreach


                        // ==== Print Lastest ====
                        if (oSkills.Count > 0)
                        {
                            // export to pdf 
                            generateSkillAllowance(Year, folderSource, false, oSkills);
                            oSkills = new List<MDSkillAllowanceInfo>();
                        }


                    } // end if


                }// end foreach














                //******* Combine File PDF *********
                string[] filePaths = Directory.GetFiles($"{folderSource}\\", "*.pdf");

                CombineMultiplePDFs(filePaths, $"{folderSource}\\all_skill_allow_measuring_{cbExptSkillYear.SelectedValue.ToString()}.pdf");
                //******* End Combine File PDF *********

            } // end if


            pnDisplay.Visible = false;
            MessageBox.Show("Finish");
        }

        private void empData_Control1_Load(object sender, EventArgs e)
        {

        }

    }


    public class MDSkillAllowanceInfo
    {
        public string ctype { set; get; }
        public string cname { set; get; }
        public int no { set; get; }
        public string code { set; get; }
        public string fname { set; get; }
        public string grp_short { set; get; }
        public string grp { set; get; }
        public string dept { set; get; }
        public int page { set; get; }

    }






}
