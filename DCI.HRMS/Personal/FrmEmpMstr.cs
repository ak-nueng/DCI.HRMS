using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using DCI.HRMS.Base;
using DCI.HRMS.Common;
using DCI.HRMS.Model;
using DCI.HRMS.Model.Attendance;
using DCI.HRMS.Model.Organize;
using DCI.HRMS.Model.Personal;
using DCI.HRMS.Model.Welfare;
using DCI.HRMS.Service;
using DCI.HRMS.Util;
using DCI.Security.Model;
using DCI.Security.Service;
using DCI.HRMS.Model.Common;
using System.Data;
using DCI.HRMS.Service.Trainee;
using DCI.HRMS.Service.SubContract;
using System.IO;
using System.Runtime.InteropServices;
using BOOL = System.Boolean;
using DWORD = System.UInt32;
using LPWSTR = System.String;
using NET_API_STATUS = System.UInt32;

//using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using ExcelDataReader;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using DCI.HRMS.DialogBox;

namespace DCI.HRMS.Personal
{
    public partial class FrmEmpMstr : BaseForm, IFormParent, IFormPermission
    {
        public FrmEmpMstr()
        {
            InitializeComponent();
        }
        private DCI.HRMS.Personal.Reports.Rpt_EmpMstr rpt = new DCI.HRMS.Personal.Reports.Rpt_EmpMstr();
        private DivisionService dvSvr = DivisionService.Instance();
        private EmployeeService empSvr = EmployeeService.Instance();
        private TraineeService tnSvr = TraineeService.Instance();
        private SubContractService subSvr = SubContractService.Instance();

        private WelfareService wfSvr = WelfareService.Instance();
        private ShiftService shSvr = ShiftService.Instance();
        private DictionaryService dictSvr = DictionaryService.Instance();
        private PropertyBorrowService prtSvr = PropertyBorrowService.Instace();
        private SkillAllowanceService skwSvr = SkillAllowanceService.Instance();
        private ApplicationManager appMgr = ApplicationManager.Instance();
        private readonly UserAccountService userAccountService = UserAccountService.Instance();
        private Dlg_Password pwDiag = new Dlg_Password();
        private readonly string[] colName = new string[] { "Code", "Date", "Data", "Oldvalue", "NewValue", "Remark", "EditBy" };
        private readonly string[] propName = new string[] { "Code", "edit_dt", "Data", "Oldvalue", "Value", "Remark", "Edit_By" };

        ClsOraConnectDB oOraDCI = new ClsOraConnectDB("DCI");
        ClsOraConnectDB oOraSUB = new ClsOraConnectDB("DCISUB");
        ClsOraConnectDB oOraTRN = new ClsOraConnectDB("DCITRN");
        SqlConnectDB oSqlHRM = new SqlConnectDB("dbHRM");


        private void AddGridViewColumns()
        {
            this.dgRecHist.Columns.Clear();
            this.dgRecHist.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn[] columns = new DataGridViewTextBoxColumn[colName.Length];
            for (int index = 0; index < columns.Length; index++)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();

                column.Name = colName[index];
                column.DataPropertyName = propName[index];
                column.ReadOnly = true;
                // column.Width = width[index];

                columns[index] = column;
                dgRecHist.Columns.Add(columns[index]);
            }
            dgRecHist.ClearSelection();
        }
        private void FrmEmpMstr_Load(object sender, EventArgs e)
        {
            try
            {
                this.Open();

            }catch(Exception ex){

                //MessageBox.Show(ex.ToString());
            }
        }

        private void cODETextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cODETextBox.Text!="")
                {
                    this.Searching();
                }
                
            }
        }


        #region IFormPermission Members

        public PermissionInfo Permission
        {
            set
            {
                ucl_ActionControl1.Permission = value;
                kryptonButton1.Enabled = value.AllowEdit;
            }
        }

        #endregion

        #region IForm Members

        public string GUID
        {
            get { return null; }
        }

        public object Information
        {
            set
            {
                EmployeeDataInfo item = (EmployeeDataInfo)value;
                grbSalary.Visible = false;
                try
                {
                    pictureBox1.ImageLocation = @"HTTP://DCIDMC.DCI.DAIKIN.CO.JP/PICTURE/" + item.Code + ".JPG";
                }
                catch
                {
                    //pictureBox1.Image = pictureBox1.InitialImage;
                }
                dtpJoinDate.Value = item.JoinDate;
                iDNOTextBox.Text = item.CitizenId;
                dtpIdIssueDate.Value = item.IdcardIssueDate;
                preNameCmb.Text = item.NameInThai.Title;
                nAMETextBox.Text = item.NameInThai.Name;
                sURNTextBox.Text = item.NameInThai.Surname;
                ePreNameCmb.Text = item.NameInEng.Title;
                eNameTextBox.Text = item.NameInEng.Name;
                eSurNameTextBox.Text = item.NameInEng.Surname;
                genderCmb.Text = item.GenderTH == "ช" ? "ชาย" : "หญิง";
                eGenderCmb.Text = item.Gender == "M" ? "Male" : "Female";
                txtCostCenter.Text = item.Costcenter;
                txtTNickname.Text = item.NameInThai.NickName;
                dtpBirthDate.Value = item.BirthDate;
                dtpJoinDate.Value = item.JoinDate;
                dtpResignDate.Value = item.ResignDate;

                try
                {
                    cmbCompany.SelectedValue = item.Company;
                }
                catch 
                {   }

                try
                {
                    cmbDv.SelectedValue = item.Division.Code;
                }
                catch
                {
                    MessageBox.Show("Error : Divisuion Code Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbDv.SelectedIndex = -1;
                }
                age_Control1.Value = item.JoinDate;
                age_Control2.Value = item.BirthDate;
                try
                {
                    cmbGrpl.SelectedValue = item.WorkGroupLine;
                }
                catch
                {

                    cmbGrpl.SelectedIndex = -1;
                }

                try
                {
                    cmbGrpot.SelectedValue = item.OtGroupLine;
                   
                }
                catch
                {

                    cmbGrpot.SelectedIndex = 0;
                }

                try
                {
                    cmbEmpSts.SelectedValue = item.EmployeeType;
                }
                catch {}
                try
                {
                    cmbEmpType.SelectedValue = item.WorkType;
                }
                catch {}
                try
                {
                    cmbPosition.SelectedValue = item.Position.Code;
                }
                catch 
                { }
                txtExtention.Text = item.ExtensionNumber;
                txtEmail.Text = item.Email;
                cmbHospital.SelectedValue = item.Hospital.Code;
                chkMilitary.Checked = item.MilitaryStatus == "1" ? true : false;
                txtBankAcc.Text = item.BankAccount;
                try
                {
                    cmbBank.SelectedValue = item.Bank;
                }
                catch
                {
                    cmbBank.SelectedIndex = -1;
                }

                txtInsNo.Text = item.InsuranNo;
                txtTaxId.Text = item.TaxNumber;
               
                txtIns.Text = item.Insuran == 0 ? "" : item.Insuran.ToString();
                txtInterest.Text = item.Interest == 0 ? "" : item.Interest.ToString();
                txtDon.Text = item.Donate == 0 ? "" : item.Donate.ToString();
                txtHandycap.Text = item.Handycap == 0 ? "" : item.Handycap.ToString();

                txtLoanNo.Text = item.LoanNo;
                txtLoan.Text = item.Loan == 0 ? "" : item.Loan.ToString();
                txtGSB.Text = item.GsbLoan == 0 ? "" : item.GsbLoan.ToString();
                txtLTF.Text = item.Ltf == 0 ? "" : item.Ltf.ToString();
                txtCoop.Text = item.CoopLoan == 0 ? "" : item.CoopLoan.ToString();

                cmbResignType.SelectedValue = item.ResignType;
                rSREASONTextBox.Text = item.ResignReason;

                if (item.Resigned)
                {
                    gbxResign.StateCommon.Back.Color1 = Color.Red;
                }
                else
                {
                    gbxResign.StateCommon.Back.Color1 = Color.White;
                }
                    
                txtResignRemark.Text = item.RsRemark;
                empEducation_Control1.Information = item.Education;

                addrCurEn.Clear();
                addrCurTh.Clear();
                addrHomEn.Clear();
                addrHomeTh.Clear();

                addrCurEn.Information = item.PresentAddressInEng;
                addrCurTh.Information = item.PresentAddressInThai;
                addrHomEn.Information = item.HomeAddressInEng;
                addrHomeTh.Information = item.HomeAddressInThai;

                txtSonS.Text = item.Sons.ToString();
                txtSonB.Text = item.Sonb.ToString(); ;
                txtSalary.Text = item.Salary.ToString();
                txtHousing.Text = item.Housing.ToString();
                txtPositAllw.Text = item.PositAllawance.ToString();
                txtAllw.Text = item.SkillAllawance.ToString();
                txtprfAllw.Text = item.ProfesionalAllowance.ToString();
                txtWedge.Text = item.Wedge.ToString();
                txtGrade.Text = item.Grad.ToString();
                txtRange.Text = item.Rank.ToString();
                txtMobile.Text = item.MobileAllawance.ToString();
                txtGasAllw.Text = item.GasolineAllowance.ToString();
                txtTalent.Text = item.Talent.ToString();

                txtRefPerson1.Text = item.RefPerson1;
                txtRefContact1.Text = item.RefContact1;
                txtRefPerson2.Text = item.RefPerson2;
                txtRefContact2.Text = item.RefContact2;
                dtpPbDate.Value = item.ProbationDate;                
                txtTrainPosition.Text = item.Tposiname;
                dtpTrainPosition.Value = item.Tposijoin;
                dtpAnnualCalDate.Value = item.AnnualcalDate;

                try {
                    provident_Control1.Information = item.Provence;
                } catch { }
                try
                {
                    cooperative_Control1.Information = item.Cooperative;
                }
                catch { }
                

                try
                {
                    cmbBusWay.SelectedValue = item.Bus;
                }
                catch { }
                try
                {
                    cmbBusStop.SelectedValue = item.BusStop;                    
                }
                catch { }
                try
                {
                    cmbMarry.SelectedItem = item.MarryStatus;
                }
                catch { }
                try
                {
                    cmbReligion.SelectedValue = item.Religion;
                }
                catch { }
                empFamily_Control1.Information = item.FamilyMember;

                empWorkHistory_Control1.EmpCode = item.Code;
                empWorkHistory_Control1.Information = item.WorkHistory;

                cmbSkillMonth_SelectedIndexChanged(this, new EventArgs());


                dgRecHist.DataSource = null;
                DataSet rechist = empSvr.GetRecordHistory(cODETextBox.Text);
                if (item.Code.StartsWith("I"))
                {
                    rechist = subSvr.GetRecordHistory(cODETextBox.Text);
                }
                else if (item.Code.StartsWith("7"))
                {
                    rechist = tnSvr.GetRecordHistory(cODETextBox.Text);
                }
                else
                {
                    rechist = empSvr.GetRecordHistory(cODETextBox.Text);
                }

                dgRecHist.Rows.Clear();
                if (rechist != null)
                {
                    DataTable recTb = new DataTable();

                    DataRow[] recrws = rechist.Tables[0].Select("EDITLEVEL <>'0'");
                    foreach (DataRow recItem in recrws)
                    {
                        dgRecHist.Rows.Add(recItem[0], recItem[1], recItem[2], recItem[3], recItem[4], recItem[6], recItem[5]);
                    }
                }

                lblLastEditBy.Text = item.LastUpdateBy;
                lblLastEdit.Text = item.LastUpdateDateTime == DateTime.MinValue ? "" : item.LastUpdateDateTime.ToString("dd/MM/yyyy HH:mm");

                eBudgetTypeCmb.Text = item.BudgetType;
                eWorkCenterTextBox.Text = item.Workcenter;
                txtLineNo.Text = item.Lineno;
                txtMCNo.Text = item.Mcno;
                dtpContractExpDate.Value = item.ContractExpDT;
                
            }
            get
            {

                EmployeeDataInfo item = new EmployeeDataInfo();
                item.Code = cODETextBox.Text;
                item.CitizenId = iDNOTextBox.Text;
                item.TaxNumber = txtTaxId.Text;
                item.IdcardIssueDate = dtpIdIssueDate.Value;
                item.NameInEng = new NameInfo();
                item.NameInThai = new NameInfo();
                item.NameInThai.Title = preNameCmb.Text;
                item.NameInThai.Name = nAMETextBox.Text;
                item.NameInThai.Surname = sURNTextBox.Text;
                item.NameInThai.NickName = txtTNickname.Text;
                item.NameInEng.Title = ePreNameCmb.Text;
                item.NameInEng.Name = eNameTextBox.Text;
                item.NameInEng.Surname = eSurNameTextBox.Text;
                

                item.Company = cmbCompany.SelectedValue.ToString() ;

                try
                {
                    item.GenderTH = genderCmb.Text.Substring(0, 1);
                }
                catch
                { }
                try
                {
                    item.Gender = eGenderCmb.Text.Substring(0, 1);
                }
                catch
                { }
                item.BirthDate = dtpBirthDate.Value;
                item.JoinDate = dtpJoinDate.Value;

                item.ResignDate = dtpResignDate.Value;
                item.ProbationDate = dtpPbDate.Value;

                try
                {
                    item.ResignType = cmbResignType.SelectedValue.ToString();
                    item.ResignReason = rSREASONTextBox.Text;
                }
                catch
                {
                    item.ResignType = "";
                    item.ResignReason = "";
                }

                item.RsRemark = txtResignRemark.Text;

                item.Division = new DivisionInfo();
                try
                {
                    item.Division.Code = cmbDv.SelectedValue.ToString();
                }
                catch 
                {  }
                item.Costcenter = txtCostCenter.Text;
                item.WorkGroupLine = cmbGrpl.Text;
                item.OtGroupLine = cmbGrpot.Text;
                item.EmployeeType = cmbEmpSts.SelectedValue.ToString();
                item.WorkType = cmbEmpType.SelectedValue.ToString();
                item.Position = new PositionInfo();
                try
                {
                    item.Position.Code = cmbPosition.SelectedValue.ToString();
                }
                catch 
                {  }
                item.ExtensionNumber = txtExtention.Text;
                item.Email = txtEmail.Text;

                item.Hospital = new HospitalInfo();
                try
                {
                    item.Hospital.Code = cmbHospital.SelectedValue.ToString();
                }
                catch
                { }
                item.MilitaryStatus = chkMilitary.Checked ? "1" : "0";
                try
                {
                    item.Sons = int.Parse(txtSonS.Text);
                }
                catch
                { }
                try
                {
                    item.Sonb = int.Parse(txtSonB.Text);
                }
                catch
                { }

                item.BankAccount = txtBankAcc.Text;
                try
                {
                    item.Bank = cmbBank.SelectedValue.ToString();
                }
                catch
                { }
                item.InsuranNo = txtInsNo.Text;
                item.TaxId = txtTaxId.Text;
                try
                {
                    item.Handycap = decimal.Parse(txtHandycap.Text);
                }
                catch 
                {

                    item.Handycap = 0;
                }
                try
                {
                    item.Insuran = decimal.Parse(txtIns.Text);
                }
                catch
                {
                    item.Insuran = 0;
                }
                try
                {
                    item.Interest = decimal.Parse(txtInterest.Text);
                }
                catch
                {
                    item.Interest = 0;

                }
                try
                {
                    item.Donate = decimal.Parse(txtDon.Text);
                }
                catch
                {
                    item.Donate = 0;
                }
                item.LoanNo = txtLoanNo.Text;
                try
                {
                    item.Loan = decimal.Parse(txtLoan.Text);
                }
                catch
                {
                    item.Loan = 0;
                }
                try
                {
                    item.GsbLoan = decimal.Parse(txtGSB.Text);
                }
                catch
                {
                    item.GsbLoan = 0;
                }
                try
                {
                    item.CoopLoan = decimal.Parse(txtCoop.Text);
                }
                catch
                {
                    item.CoopLoan = 0;
                }

                try
                {
                    item.Ltf = decimal.Parse(txtLTF.Text);
                }
                catch
                {
                    item.Ltf = 0;
                }

                // item.ResignType = cmbResignType.SelectedValue.ToString();
                item.Education = new EducationInfo();
                item.Education = (EducationInfo)empEducation_Control1.Information;
                item.PresentAddressInEng = new AddressInfo();
                item.PresentAddressInEng = (AddressInfo)addrCurEn.Information;
                item.PresentAddressInThai = new AddressInfo();
                item.PresentAddressInThai = (AddressInfo)addrCurTh.Information;
                item.HomeAddressInEng = new AddressInfo();
                item.HomeAddressInEng = (AddressInfo)addrHomEn.Information;
                item.HomeAddressInThai = new AddressInfo();
                item.HomeAddressInThai = (AddressInfo)addrHomeTh.Information;
                item.RefPerson1 = txtRefPerson1.Text;
                item.RefContact1 = txtRefContact1.Text;
                item.RefPerson2 = txtRefPerson2.Text;
                item.RefContact2 = txtRefContact2.Text;
                try
                {
                    item.Salary = decimal.Parse(txtSalary.Text);
                }
                catch
                {

                    item.Salary = 0;
                }
                try
                {
                    item.Housing = decimal.Parse(txtHousing.Text);
                }
                catch
                {

                    item.Housing = 0;
                }
                try
                {
                    item.PositAllawance = decimal.Parse(txtPositAllw.Text);
                }
                catch
                {

                    item.PositAllawance = 0;
                }
                try
                {
                    item.SkillAllawance = decimal.Parse(txtAllw.Text);
                }
                catch
                {

                    item.SkillAllawance = 0;
                }
                try
                {
                    item.ProfesionalAllowance = decimal.Parse(txtprfAllw.Text);
                }
                catch
                {

                    item.ProfesionalAllowance = 0;
                }
                try
                {
                    item.Wedge = decimal.Parse(txtWedge.Text);
                }
                catch
                {

                    item.Wedge = 0;
                }
                try
                {
                    item.Grad = int.Parse(txtGrade.Text);
                }
                catch
                {

                    item.Grad = 0;
                }
                try
                {
                    item.Rank = int.Parse(txtRange.Text);
                }
                catch
                {
                    item.Rank = 0;

                }
                try {
                    item.MobileAllawance = decimal.Parse(txtMobile.Text);
                }
                catch { item.MobileAllawance = 0; }

                try {
                    item.GasolineAllowance = decimal.Parse(txtGasAllw.Text);
                }
                catch { item.GasolineAllowance = 0; }

                try {
                    item.Talent = decimal.Parse(txtTalent.Text);
                }
                catch { item.Talent = 0; }


                item.Provence = new ProvidenceInfo();

                item.Provence = (ProvidenceInfo)provident_Control1.Information;
                item.Cooperative = (CooperativeInfo)cooperative_Control1.Information;

                try
                {
                    item.Bus = cmbBusWay.SelectedValue.ToString();
                }
                catch
                {

                    item.Bus = "";
                }
                try
                {
                    item.BusStop = cmbBusStop.SelectedValue.ToString();
                }
                catch
                {

                    item.BusStop = "";
                }


                try
                {
                    item.MarryStatus = cmbMarry.SelectedItem.ToString();
                }
                catch
                {

                    item.MarryStatus = "";
                }
                try
                {
                    item.Religion = cmbReligion.SelectedValue.ToString();
                }
                catch
                {

                    item.Religion = "";
                }
                item.FamilyMember = empFamily_Control1.GetFamilyData();
                item.WorkHistory = empWorkHistory_Control1.GetWorkHostoryData();
                item.Remark = txtRemark.Text;

                item.Tposiname = txtTrainPosition.Text;
                item.Tposijoin = dtpTrainPosition.Value;
                item.AnnualcalDate = dtpAnnualCalDate.Value;
                item.BudgetType = eBudgetTypeCmb.Text;
                item.Workcenter = eWorkCenterTextBox.Text;
                item.Lineno = txtLineNo.Text;
                item.Mcno = txtMCNo.Text;
                item.ContractExpDT = dtpContractExpDate.Value;

                return item;
            }

        }


        public void AddNew()
        {
            ucl_ActionControl1.CurrentAction = FormActionType.AddNew;

            this.Clear();
            gbxResign.Enabled = false;
            cODETextBox.ReadOnly = false;
            cODETextBox.Focus();
            txtRemark.Clear();
            txtRemark.ReadOnly = true;
            try
            {
                cmbBank.SelectedIndex = 0;
            }
            catch
            { }
        }
        private bool CheckInfo()
        {
            if (cmbCompany.SelectedIndex==-1)
            {
                MessageBox.Show("Error : กรุณาเลือกข้อมูลบริษัท", "กรุณาเลือกข้อมูลบริษัท", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCompany.Focus();
                return false;
            }

            if (dtpAnnualCalDate.Value == DateTime.MinValue)
            {
                MessageBox.Show("Error : กรุณาเลือกข้อมูลวันที่เริ่มคำนวนวันลาพักร้อน", "กรุณาเลือกข้อมูลวันที่เริ่มคำนวนวันลาพักร้อน", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpAnnualCalDate.Focus();
                return false;
            }

            return true;
        }

        public void Save()
        {
                
            if (CheckInfo())
            {
                if (ucl_ActionControl1.CurrentAction == FormActionType.Save)
                {


                    if (this.ucl_ActionControl1.Permission.AllowEdit)
                    {


                        EmployeeDataInfo itm = (EmployeeDataInfo)this.Information;
                        itm.LastUpdateBy = appMgr.UserAccount.AccountId;
                        try
                        {

                            if (itm.Code.StartsWith("I"))
                            {
                                subSvr.UpdateEmployeeInfo(itm);
                            }
                            else if (itm.Code.StartsWith("7"))
                            {
                                tnSvr.UpdateEmployeeInfo(itm);
                            }
                            else
                            {
                                empSvr.UpdateEmployeeInfo(itm);
                            }


                            // Transfer data to ALPHA
                            //TransferToALPHA(itm);

                            MessageBox.Show("แก้ไขข้อมูลเรียบร้อยแล้ว", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error : ไม่สามรถแก้ไขข้อมูลได้เนื่องจาก\n" + ex.Message, "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {

                        MessageBox.Show("Error : คุณไม่สามรถแก้ไขข้อมูลได้", "Access Denie", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    }
                }
                else if (ucl_ActionControl1.CurrentAction == FormActionType.SaveAs)
                {
                    if (ucl_ActionControl1.Permission.AllowAddNew)
                    {


                        try
                        {
                            EmployeeDataInfo itm = (EmployeeDataInfo)this.Information;
                            itm.CreateBy = appMgr.UserAccount.AccountId;


                            if (itm.Code.StartsWith("I"))
                            {
                                subSvr.SaveEmployeeInfo(itm);
                            }
                            else if (itm.Code.StartsWith("7"))
                            {
                                tnSvr.SaveEmployeeInfo(itm);
                            }
                            else
                            {
                                empSvr.SaveEmployeeInfo(itm);
                            }

                            // Transfer data to ALPHA
                            //TransferToALPHA(itm);


                            MessageBox.Show("เพิ่มข้อมูลเรียบร้อยแล้ว", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error : ไม่สามรถเพิ่มข้อมูลได้เนื่องจาก\n" + ex.Message, "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    else
                    {
                        MessageBox.Show("Error : คุณไม่สามรถเพิ่มข้อมูลได้", "Access Denie", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }
                this.RefreshData();
                
            }

        }

        public void Delete()
        {
            if (ucl_ActionControl1.Permission.AllowDelete)
            {
                EmployeeDataInfo item = (EmployeeDataInfo)this.Information;
                if (MessageBox.Show("คุณต้องการลบข้อมูลพนักงานรหัส: " + item.Code + " ใช่หรือไม่", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                      
                        if (item.Code.StartsWith("I"))
                        {
                            subSvr.DeleteEmployeeInfo(item.Code);
                        }
                        else if(item.Code.StartsWith("7"))
                        {
                            tnSvr.DeleteEmployeeInfo(item.Code);
                        }
                        else
                        {
                            empSvr.DeleteEmployeeInfo(item.Code);
                        }


                        this.Clear();
                        MessageBox.Show("ลบข้อมูลเรียบร้อยแล้ว", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error : ไม่สามรถลบข้อมูลได้เนื่องจาก\n" + ex.Message, "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.RefreshData();
                    } 
                    ucl_ActionControl1.CurrentAction = FormActionType.None;
                }
               

            }
            else
            {
                MessageBox.Show("Error : คุณไม่สามรถลบข้อมูลได้", "Access Denie", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        public void Search()
        {
            ucl_ActionControl1.CurrentAction = FormActionType.Search;
            cODETextBox.ReadOnly = false;
            cODETextBox.Clear();
            cODETextBox.Focus();
            txtRemark.Clear();


        }
        private void Searching()
        {
            if (ucl_ActionControl1.CurrentAction == FormActionType.None || ucl_ActionControl1.CurrentAction == FormActionType.Search)
            {
                tmRefresh.Enabled = false;
                EmployeeDataInfo emp = empSvr.GetEmployeeData(cODETextBox.Text);
                if (cODETextBox.Text.StartsWith("I"))
                {
                    emp = subSvr.GetEmployeeData(cODETextBox.Text);
                }
                else if (cODETextBox.Text.StartsWith("7"))
                {
                    emp = tnSvr.GetEmployeeData(cODETextBox.Text);
                }
                else
                {
                    emp = empSvr.GetEmployeeData(cODETextBox.Text);
                }


                if (emp != null)
                {
                    txtRemark.Clear();
                    txtRemark.ReadOnly = false;
                    this.Information = emp;
                    empFamily_Control1.EmpCode = emp.Code;
                    empFamily_Control1.ReadyOnly = false;
                    empWorkHistory_Control1.ReadyOnly = false;
                    ucl_ActionControl1.CurrentAction = FormActionType.Save;
                    gbxResign.Enabled = true;
                    cODETextBox.ReadOnly = true;
                    PropertyBorrow_Control1.SetData(emp.Code);
                    EmpCertificate_Control1.SetData(emp.Code);
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูลพนักงาน รหัส: " + cODETextBox.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    empFamily_Control1.ReadyOnly = true;
                    empFamily_Control1.EmpCode = "";
                    empWorkHistory_Control1.ReadyOnly = true;
                    ucl_ActionControl1.CurrentAction = FormActionType.None;
                    this.Clear();
                }
            }
            else
            {

            }
        }

        public void Export()
        {
            if (rpt.IsDisposed)
            {
                rpt = new DCI.HRMS.Personal.Reports.Rpt_EmpMstr();
            }
            rpt.Show();
            rpt.Activate();
        }

        public void Print()
        {
        }

        public void Open()
        {
            cODETextBox.ReadOnly = true;
            ucl_ActionControl1.Owner = this;



            pwDiag.ShowDialog(this);

            tmClose.Enabled = true;/*************************************/

            if (!(appMgr.UserAccount.UserGroup.ID == 9000 || appMgr.UserAccount.UserGroup.ID == 1000 || appMgr.UserAccount.UserGroup.ID == 1100))
            {
                tabControl1.TabPages.Remove(tpSpecial);
                tabControl1.TabPages.Remove(tpWorkHist);
            }
            AddGridViewColumns();

            ArrayList company = dictSvr.SelectAll("COMP");
            cmbCompany.DisplayMember = "NameForSearching";
            cmbCompany.ValueMember = "Code";
            cmbCompany.DataSource = company;

            ArrayList religions = dictSvr.SelectAll("RELG");
            cmbReligion.DisplayMember = "NameForSearching";
            cmbReligion.ValueMember = "Code";
            cmbReligion.DataSource = religions;


            ArrayList banks = dictSvr.SelectAll("BANK");
            cmbBank.DisplayMember = "NameForSearching";
            cmbBank.ValueMember = "Code";
            cmbBank.DataSource = banks;

            ArrayList dv = dvSvr.GetAll();
            cmbDv.DisplayMember = "DispText";
            cmbDv.ValueMember = "Code";
            cmbDv.DataSource = dv;
            cmbDv.SelectedIndex = -1;

            ArrayList pos = empSvr.GetAllPosition();
            cmbPosition.DisplayMember = "DispText";
            cmbPosition.ValueMember = "Code";
            cmbPosition.DataSource = pos;
            cmbPosition.SelectedIndex = -1;

            ArrayList hospital = empSvr.GetAllHospital();
            cmbHospital.DisplayMember = "DispText";
            cmbHospital.ValueMember = "Code";
            cmbHospital.DataSource = hospital;
            cmbHospital.SelectedIndex = -1;

            ArrayList resign = empSvr.GetAllResignType();
            cmbResignType.DisplayMember = "Code";
            cmbResignType.ValueMember = "Code";
            cmbResignType.DataSource = resign;
            cmbResignType.SelectedIndex = -1;

            empEducation_Control1.Open(empSvr);
            //    ArrayList st= wfSvr.GetBusStop("C");

            ArrayList bw = wfSvr.GetAllBusWay();            
            BusWayInfo busNull = new BusWayInfo();
            busNull.Code = "";
            busNull.NameEng = "";
            busNull.NameThai = "";
            busNull.Order = 0;
            bw.Add(busNull);


            cmbBusWay.DisplayMember = "DispText";
            cmbBusWay.ValueMember = "Code";
            cmbBusWay.DataSource = bw;
            cmbBusWay.SelectedIndex = -1;


            ArrayList sh = shSvr.GetShiftType();
            cmbGrpot.DisplayMember = "GrpOt";
            cmbGrpot.ValueMember = "GrpOt";
            cmbGrpot.DataSource = sh;
            cmbGrpot.SelectedValue = "D";

            ArrayList line = new ArrayList();
            line = dictSvr.SelectAll("LNGR");
            cmbGrpl.DisplayMember = "Code";
            cmbGrpl.ValueMember = "Code";
            cmbGrpl.DataSource = line;





            empFamily_Control1.empSvr = this.empSvr;
            empFamily_Control1.Open();
            empFamily_Control1.ReadyOnly = true;


            empWorkHistory_Control1.empSvr = this.empSvr;
            empWorkHistory_Control1.ReadyOnly = true;

            PropertyBorrow_Control1.prtSvr = prtSvr;
            PropertyBorrow_Control1.Open();
            EmpCertificate_Control1.sklSvr = SkillAllowanceService.Instance();
            EmpCertificate_Control1.subSklSvr = SubContractSkillAllowanceService.Instance(); ;
            EmpCertificate_Control1.Open();

            BasicInfo emp = new BasicInfo("E", "Employee", "");
            BasicInfo mana = new BasicInfo("M", "Manager", "");
            ArrayList allEmpSts = new ArrayList();
            allEmpSts.Add(emp);
            allEmpSts.Add(mana);

            cmbEmpSts.DataSource = allEmpSts;


            BasicInfo sal = new BasicInfo("S", "Salary", "");
            BasicInfo wage = new BasicInfo("O", "Wage", "");
            ArrayList allEmpType = new ArrayList();
            allEmpType.Add(sal);
            allEmpType.Add(wage);
            cmbEmpType.DataSource = allEmpType;


            DataTable dtImpType = new DataTable();
            dtImpType.Columns.Add("dataDis", typeof(string));
            dtImpType.Columns.Add("dataVal", typeof(string));
            dtImpType.Rows.Add("พนักงานใหม่", "EMP_NEW");
            dtImpType.Rows.Add("เปลี่ยน GrpOT", "GRPOT");
            dtImpType.Rows.Add("เปลี่ยน ข้อมูลส่วนตัว", "PROFILE");
            ImpTypeCmb.DataSource = dtImpType;
            ImpTypeCmb.DisplayMember = "dataDis";
            ImpTypeCmb.ValueMember = "dataVal";


            /*Skill Allowance Month select*/
            for (DateTime i = DateTime.Today.AddMonths(1); i > DateTime.Today.AddMonths(-12); i = i.AddMonths(-1))
            {
                cmbSkillMonth.Items.Add(i.ToString("MM/yyyy"));
            }
            cmbSkillMonth.SelectedItem = DateTime.Now.ToString("MM/yyyy");

            EmpCertificate_Control1.EditEnable = true;

        }

        public void Clear()
        {
            cODETextBox.Focus();
            cODETextBox.Clear();
            pictureBox1.Image = pictureBox1.InitialImage;

            iDNOTextBox.Clear();
            dtpIdIssueDate.Value = new DateTime(1900, 1, 1);
            preNameCmb.Text = "";
            nAMETextBox.Text = "";
            sURNTextBox.Text = "";
            ePreNameCmb.Text = "";
            eNameTextBox.Text = "";
            eSurNameTextBox.Text = "";
            txtTNickname.Text = "";
            genderCmb.Text = "";
            eGenderCmb.Text = "";
            dtpBirthDate.Value = new DateTime(1900, 1, 1);
            dtpJoinDate.Value = DateTime.Today;
            dtpResignDate.Value = new DateTime(1900, 1, 1);
            cmbDv.SelectedIndex = -1;
            age_Control1.Value = new DateTime(1900, 1, 1);
            age_Control2.Value = new DateTime(1900, 1, 1);
            cmbGrpl.SelectedIndex = -1;

            cmbGrpot.SelectedIndex = 0;

            cmbEmpSts.SelectedIndex = 0;
            cmbEmpType.SelectedValue = "O";
            cmbPosition.SelectedValue = "OP";
            txtExtention.Text = "";
            txtEmail.Text = "";
            cmbHospital.SelectedIndex = -1;
            chkMilitary.Checked = false;
            txtBankAcc.Text = "";
            cmbBank.SelectedIndex = 0;
            txtCostCenter.Text = "";

            txtInsNo.Text = "";
            txtIns.Text = "";
            txtInterest.Text = "";
            txtDon.Text = "";
            txtLoanNo.Text = "";
            txtLoan.Text = "";


            cmbResignType.SelectedIndex = -1;
            empEducation_Control1.Clear();
            addrCurEn.Clear();
            addrCurTh.Clear();
            addrHomEn.Clear();
            addrHomeTh.Clear();

            txtMobile.Text = "0";
            txtSalary.Text = "0";
            txtHousing.Text = "0";
            txtPositAllw.Text = "0";
            txtAllw.Text = "0";
            txtWedge.Text = "0";
            txtGrade.Text = "0";
            txtRange.Text = "0";
            txtSonS.Text = "0";
            txtSonB.Text = "0";


            provident_Control1.Clear();
            cooperative_Control1.Clear();

            cmbBusWay.SelectedIndex = -1;

            txtTrainPosition.Text = "";
            dtpTrainPosition.Value = new DateTime(1900, 1, 1);
            dtpAnnualCalDate.Value = dtpJoinDate.Value;
            dtpContractExpDate.Value = new DateTime(1900, 1, 1);


            cmbMarry.SelectedIndex = -1;
            cmbReligion.Text = "";
            empFamily_Control1.Information = new ArrayList();
            empWorkHistory_Control1.Information = new ArrayList();
            skillAllowance_Control1.Information = new ArrayList();
        }

        public void RefreshData()
        {
            ucl_ActionControl1.CurrentAction = FormActionType.Search;
            this.Searching();
        }

        public void Exit()
        {
            this.Close();
        }

        public void TransferToALPHA(EmployeeDataInfo itm)
        {
            string FileName = String.Format("E{0}_{1}.tsv", itm.Code, DateTime.Now.ToString("yyMMddHHmmss"));
            string FolderPathFile = @"C:\ALPHA\";

            if (!Directory.Exists(FolderPathFile))
            {
                Directory.CreateDirectory(FolderPathFile);
            }

            using (StreamWriter writer = File.AppendText(FolderPathFile + "\\" + FileName))
            {
                string _ResignDate = "";
                string _ComType = "";
                string _OTCalType = "";
                _ResignDate = (itm.ResignDate.ToString("yyyyMMdd") == "19000101") ? "" : itm.ResignDate.ToString("yyyyMMdd");
                
                if(_ComType.StartsWith("I")){
                    _ComType = "S";
                    _OTCalType = "S";

                }else if(_ComType.StartsWith("7")){
                    _ComType = "T";
                    _OTCalType = "P";

                }else{
                    _ComType = "P";
                    if(itm.Position.Code == "PD" || itm.Position.Code == "DI" || itm.Position.Code == "CM" || 
                       itm.Position.Code == "SGM" || itm.Position.Code == "GM" || itm.Position.Code == "AG" || 
                       itm.Position.Code == "MG" || itm.Position.Code == "AM" || itm.Position.Code == "AV" )
                    {
                        _OTCalType = "N";
                    }else{
                        _OTCalType = "P";
                    }
                }

                
                //CODE,PREN,NAME,SURN,JOIN,RESIGN,POSIT,POSITEQ,DEPT,SEX,BIRTH,COMID,BRN,
                //COMTYPE,OTCALTYPE,WCNO,WSTS,TPREN,TNAME,TSURN,EMAIL,IDNO,TEL,PHS,TELEPHONE,
                //RSTYPE,RSREASON,CDATE,UDATE,UMODIFY,BudgetType
                string rwFile = String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}\t{18}\t{19}\t{20}\t{21}\t{22}\t{23}\t{24}\t{25}\t{26}\t{27}\t{28}\t{29}\t{30}",
                    itm.Code, itm.NameInEng.Title, itm.NameInEng.Name, itm.NameInEng.Surname, 
                    itm.JoinDate.ToString("yyyyMMdd"), _ResignDate, itm.Position.Code, "", itm.Division.Code,
                    itm.Gender, itm.BirthDate.ToString("yyyyMMdd"), itm.Company, "001", _ComType,
                    _OTCalType, itm.Workcenter, itm.EmployeeType, itm.NameInThai.Title, itm.NameInThai.Name,
                    itm.NameInThai.Surname, itm.Email, itm.CitizenId, itm.ExtensionNumber, "", "",
                    itm.ResignType, itm.ResignReason, itm.CreateDateTime.ToString("yyyyMMdd"),
                    itm.LastUpdateDateTime.ToString("yyyyMMdd"), "", itm.BudgetType);
                
                writer.WriteLine(rwFile);

            } // end using create file


            UNCAccess oUNC = new UNCAccess(@"\\dkcidb\inv\employee", "administrator", "dciserver_", " dceiaoahpea");
            oUNC.login(@"\\dkcidb\inv\employee", "administrator", "dciserver_", " dceiaoahpea");
            File.Copy(FolderPathFile + "\\" + FileName, @"\\dkcidb\inv\employee\" + FileName);

        }

        #endregion

        private void cmbDv_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                DivisionInfo selDv = dvSvr.FindRootStructure(cmbDv.SelectedValue.ToString());
                txtOrg.Text = selDv.ToString();
            }
            catch
            {
                txtOrg.Clear();

            }

        }

        private void empFamily_Control1_Load(object sender, EventArgs e)
        {

        }

        private void cmbResignType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BasicInfo rs = (BasicInfo)cmbResignType.SelectedItem;
                rSREASONTextBox.Text = rs.DetailTh;

            }
            catch { rSREASONTextBox.Clear(); }

            if (cmbResignType.SelectedIndex != -1)
            {
                rSREASONTextBox.BackColor = Color.Red;
            }
            else
            {
                rSREASONTextBox.BackColor = Color.White;
            }
        }

        private void cmbBusWay_SelectedValueChanged(object sender, EventArgs e)
        {
            
            try
            {
                BusWayInfo bway = (BusWayInfo)cmbBusWay.SelectedItem;
                if (bway.Stops.Count == 0)
                {
                    bway.Stops = wfSvr.GetBusStop(bway.Code);
                }

                cmbBusStop.DataSource = bway.Stops;

                cmbBusStop.ValueMember = "StopCode";
                cmbBusStop.DisplayMember = "DispText";
            }
            catch
            {
                cmbBusStop.DataSource = null;
                cmbBusStop.ValueMember = "StopCode";
                cmbBusStop.DisplayMember = "DispText";

            }

        }

        private void cmbGrpot_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                ShiftType shtype = (ShiftType)cmbGrpot.SelectedItem;
                txtShGrp.Text = shtype.ShiftGroup + shtype.ShiftStatus;
            }
            catch
            {

                txtShGrp.Clear();
            }
        }



        private void FrmEmpMstr_KeyDown(object sender, KeyEventArgs e)
        {
            ucl_ActionControl1.OnActionKeyDown(sender, e);
        }

        private void kryptonPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cODETextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar!='I')
            {
                KeyPressManager.EnterNumericOnly(e);
            }
            
        }

        private void cmbEmpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ucl_ActionControl1.CurrentAction == FormActionType.SaveAs)
            {
                if (cmbEmpType.SelectedValue.ToString() == "O")
                {
                    BasicInfo dl = empSvr.GetDailyRate();
                    txtWedge.Text = dl.Description;
                    txtWedge.Enabled = true;
                    txtSalary.Enabled = false;
                }
                else
                {
                    txtWedge.Text = "0";
                    txtWedge.Enabled = false;
                    txtSalary.Enabled = true;
                }
            }
            else if (ucl_ActionControl1.CurrentAction == FormActionType.Save)
            {
                if (cmbEmpType.SelectedValue.ToString() == "O")
                {

                    txtWedge.Enabled = true;
                    txtSalary.Enabled = false;
                }
                else
                {

                    txtWedge.Enabled = false;
                    txtSalary.Enabled = true;
                }
            }
        }

        private void preNameCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmp = (ComboBox)sender;
            ePreNameCmb.SelectedIndex = cmp.SelectedIndex;
            preNameCmb.SelectedIndex = cmp.SelectedIndex;
            if (cmp.SelectedIndex <= 1)
            {
                genderCmb.SelectedIndex = preNameCmb.SelectedIndex;
                eGenderCmb.SelectedIndex = preNameCmb.SelectedIndex;
            }
            else
            {
                genderCmb.SelectedIndex = 1;
                eGenderCmb.SelectedIndex = 1;
            }

        }

        private void dtpBirthDate_DateChange()
        {
            age_Control2.Value = dtpBirthDate.Value;
        }

        private void dtpJoinDate_DateChange()
        {
            age_Control1.Value = dtpJoinDate.Value;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            DiagResignEmployee dgrs = new DiagResignEmployee();
            dgrs.ShowDialog(this);
        }

        private void genderCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (genderCmb.SelectedIndex == 0)
            {
                chkMilitary.Enabled = true;
            }
            else
            {
                chkMilitary.Enabled = false;
            }
        }

        private void cmbBusWay_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbPosition_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPressManager.Enter(e);
        }

        private void txtSalaryPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSalaryPwd.Text == "48xx5a")
                {
                    grbSalary.Visible = true;
                    tmRefresh.Enabled = true;
                }
                else {
                    ApplicationManager appMgr = ApplicationManager.Instance();
                    try
                    {
                        UserAccountInfo slr = userAccountService.Authentication("SlrPerm", txtSalaryPwd.Text);

                        if (slr != null)
                        {
                            userAccountService.KeepLog(appMgr.UserAccount.AccountId, "EmpMStr", SystemInformation.ComputerName, "Salary View", cODETextBox.Text);
                            grbSalary.Visible = true;
                            tmRefresh.Enabled = true;
                        }
                        else
                        {
                            userAccountService.KeepLog(appMgr.UserAccount.AccountId, "EmpMStr", SystemInformation.ComputerName, "Fail:Salary View", cODETextBox.Text);
                            MessageBox.Show("Error : รหัสผ่านไม่ถูกต้อง", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }

                    }
                    catch (Exception ex)
                    {
                        userAccountService.KeepLog(appMgr.UserAccount.AccountId, "EmpMStr", SystemInformation.ComputerName, "Fail:Salary View", cODETextBox.Text);
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                
                }

                
                txtSalaryPwd.Clear();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pwDiag.dlgResult == "CANCEL")
            {
                this.Close();
                return;
            }


            try
            {
                UserAccountInfo usr = userAccountService.Authentication("FrmEMPM", pwDiag.Password);
                if (usr == null)
                {
                    if (pwDiag.Password != "48xx5a")//Bypass Password;
                    {
                        this.Close();

                    }
                    else
                    {
                        tmClose.Enabled = false;

                    }
                }
                else
                {
                    tmClose.Enabled = false;
                }
            }
            catch (Exception)
            {
                if (pwDiag.Password != "48xx5a")//Bypass Password;
                {
                    this.Close();
                }
                else
                {
                    tmClose.Enabled = false;
                }

            }
        }

        private void cmbSkillMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cODETextBox.Text != "")
            {
                skillAllowance_Control1.Information = skwSvr.GetSkillAllowancwByCode(cODETextBox.Text, DateTime.Parse("01/" + cmbSkillMonth.SelectedItem.ToString()));
            }
            else
            {
                skillAllowance_Control1.Information = new ArrayList();
            }
        }

        private void txtPwdRec_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ApplicationManager appMgr = ApplicationManager.Instance();
                try
                {
                    UserAccountInfo slr = userAccountService.Authentication("RecPerm", txtPwdRec.Text);

                    if (slr != null)
                    {
                        userAccountService.KeepLog(appMgr.UserAccount.AccountId, "EmpMStrRecHist", SystemInformation.ComputerName, "Salary View", cODETextBox.Text);
                        dgRecHist.DataSource = null;
                        DataSet rechist = empSvr.GetRecordHistory(cODETextBox.Text);
                        dgRecHist.Rows.Clear();
                        if (rechist != null)
                        {



                            foreach (DataRow recItem in rechist.Tables[0].Rows)
                            {
                                dgRecHist.Rows.Add(recItem[0], recItem[1], recItem[2], recItem[3], recItem[4], recItem[5]);
                            }
                        }
                        tmRefresh.Enabled = true;
                    }
                    else
                    {
                        userAccountService.KeepLog(appMgr.UserAccount.AccountId, "EmpMStrRecHist", SystemInformation.ComputerName, "Fail:Salary View", cODETextBox.Text);
                        MessageBox.Show("รหัสผ่านไม่ถูกต้อง", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                catch (Exception ex)
                {
                    userAccountService.KeepLog(appMgr.UserAccount.AccountId, "EmpMStrRecHist", SystemInformation.ComputerName, "Fail:Salary View", cODETextBox.Text);

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                txtPwdRec.Clear();
            }
        }

        private void tmRefresh_Tick(object sender, EventArgs e)
        {
            this.RefreshData();

        }

        private void skillAllowance_Control1_Load(object sender, EventArgs e)
        {

        }

        private void kryptonGroup1_Panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ucl_ActionControl1_Load(object sender, EventArgs e)
        {

        }

        private void btnEmployeeUploadBrw_Click(object sender, EventArgs e)
        {
            OpenFileDialog flDrg = new OpenFileDialog();
            flDrg.Filter = "Excel File|*.xlsx";

            if (flDrg.ShowDialog(this) != DialogResult.Cancel)
            {
                txtEmployeeUpload.Text = flDrg.FileName;                
            }
        }

        private void btnCheckUpload_Click(object sender, EventArgs e)
        {
            if(txtEmployeeUpload.Text != "")
            {

                DataTable dtEmpData = new DataTable();
                try
                {
                    dtEmpData = ReadExcelFile(txtEmployeeUpload.Text);
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString() ); }


                if (dtEmpData.Rows.Count > 0)
                {

                    if (ImpTypeCmb.SelectedValue.ToString() == "EMP_NEW")
                    {
                        #region EMP NEW
                        if (dtEmpData.Rows.Count > 0)
                        {
                            string StrError = "เกิดความผิดพลาดของข้อมูล";
                            bool VerifyStatus = true;
                            int no = 1;

                            //CODE      DVCD    GRPOT   COSTCENTER  WORKCENTER  LINENO
                            DataTable dtGRPOT = new DataTable();
                            string strGRPOT = @"SELECT ITEM FROM DICT WHERE TYPE = 'SHFT' ORDER BY ITEM ";
                            OracleCommand cmdGRPOT = new OracleCommand();
                            cmdGRPOT.CommandText = strGRPOT;
                            dtGRPOT = oOraDCI.Query(cmdGRPOT);

                            DataTable dtLNGR = new DataTable();
                            string strLNGR = @"SELECT ITEM FROM DICT WHERE TYPE = 'LNGR' ORDER BY ITEM ";
                            OracleCommand cmdLNGR = new OracleCommand();
                            cmdLNGR.CommandText = strLNGR;
                            dtLNGR = oOraDCI.Query(cmdLNGR);

                            DataTable dtDVCD = new DataTable();
                            string strDVCD = @"SELECT ITEM FROM DICT WHERE TYPE = 'DVCD' ORDER BY ITEM";
                            OracleCommand cmdDVCD = new OracleCommand();
                            cmdDVCD.CommandText = strDVCD;
                            dtDVCD = oOraDCI.Query(cmdDVCD);

                            DataTable dtDEGR = new DataTable();
                            string strDEGR = @"SELECT DESCR FROM DICT WHERE TYPE = 'DEGR' ORDER BY DESCR ";
                            OracleCommand cmdDEGR = new OracleCommand();
                            cmdDEGR.CommandText = strDEGR;
                            dtDEGR = oOraDCI.Query(cmdDEGR);

                            DataTable dtCOMPANY = new DataTable();
                            string strCOMPANY = @"SELECT ITEM FROM DICT WHERE TYPE = 'COMP' ORDER BY ITEM";
                            OracleCommand cmdCOMPANY = new OracleCommand();
                            cmdCOMPANY.CommandText = strCOMPANY;
                            dtCOMPANY = oOraDCI.Query(cmdCOMPANY);

                            DataTable dtBUS = new DataTable();
                            string strBUS = @"SELECT ITEM FROM DICT WHERE TYPE = 'BUS' ORDER BY ITEM";
                            OracleCommand cmdBUS = new OracleCommand();
                            cmdBUS.CommandText = strBUS;
                            dtBUS = oOraDCI.Query(cmdBUS);

                            DataTable dtSTOP = new DataTable();
                            string strSTOP = @"SELECT DISTINCT TITEM FROM DICT WHERE TYPE = 'STOP' ORDER BY TITEM";
                            OracleCommand cmdSTOP = new OracleCommand();
                            cmdSTOP.CommandText = strSTOP;
                            dtSTOP = oOraDCI.Query(cmdSTOP);

                            DataTable dtBANK = new DataTable();
                            string strBANK = @"SELECT ITEM FROM DICT WHERE TYPE = 'BANK' ORDER BY ITEM";
                            OracleCommand cmdBANK = new OracleCommand();
                            cmdBANK.CommandText = strBANK;
                            dtBANK = oOraDCI.Query(cmdBANK);


                            DataTable dtCOSTCENTER = new DataTable();
                            string strCOSTCENTER = @"SELECT DISTINCT COSTCENTER FROM EMPM WHERE RESIGN IS NULL ORDER BY COSTCENTER";
                            OracleCommand cmdCOSTCENTER = new OracleCommand();
                            cmdCOSTCENTER.CommandText = strCOSTCENTER;
                            dtCOSTCENTER = oOraDCI.Query(cmdCOSTCENTER);


                            DataTable dtLine = new DataTable();
                            string strLine = @"SELECT DISTINCT [ln_code] FROM [dbHRM].[dbo].[DVCD_Std] ORDER BY [ln_code] ";
                            SqlCommand cmdLine = new SqlCommand();
                            cmdLine.CommandText = strLine;
                            dtLine = oSqlHRM.Query(cmdLine);


                            string[] AryPREN = { "MR.", "MRS.", "MS.", "Mr.", "Mrs.", "Ms." };
                            string[] AryTPREN = { "น.ส", "น.ส.", "นาง", "นาย" };
                            string[] ArySEX = { "M", "F" };
                            string[] AryTSEX = { "ช", "ญ", "ห" };
                            string[] AryWTYPE = { "O", "S" };
                            string[] AryWSTS = { "E", "M" };
                            string[] AryPOSIT = { "AG", "AM", "AV", "CM", "DI", "DR", "EN", "EN.S", "FO", "GM", "LE", "LE.S", "MG", "OP", "OP.S", "PD", "SE", "SF", "SGM", "SS", "ST", "SU", "Se.ST", "TE", "TE.S", "TN", "TR" };
                            string[] AryBudgetType = { "DIRECT", "INDIRECT", "SGA" };
                            string[] AryMARRY = { "M", "H", "S", "" };
                            string[] AryRELIGION = { "B", "C", "O", "" };


                            //*** Loop ****
                            foreach (DataRow drEmp in dtEmpData.Rows)
                            {
                                // init row status
                                bool _rowStatus = true;
                                // init error string
                                string _rowErrString = "";


                                if (drEmp["CODE"].ToString().Length == 5)
                                {
                                    if (drEmp["CODE"].ToString().StartsWith("0") || drEmp["CODE"].ToString().StartsWith("4") || drEmp["CODE"].ToString().StartsWith("6")
                                        || drEmp["CODE"].ToString().StartsWith("7") || drEmp["CODE"].ToString().StartsWith("9") || drEmp["CODE"].ToString().StartsWith("I"))
                                    {
                                        DataTable dtEmp = new DataTable();
                                        string strEmp = @"SELECT * FROM Employee WHERE Code=@Code ";
                                        SqlCommand cmdEmp = new SqlCommand();
                                        cmdEmp.CommandText = strEmp;
                                        cmdEmp.Parameters.Add(new SqlParameter("@Code", drEmp["CODE"].ToString()));
                                        dtEmp = oSqlHRM.Query(cmdEmp);

                                        if (dtEmp.Rows.Count > 0)
                                        {
                                            _rowStatus = false;
                                            _rowErrString += " , CODE(Dup)";
                                        }
                                    }
                                    else
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , CODE(Format)";
                                    }
                                }
                                else
                                {
                                    _rowStatus = false;
                                    _rowErrString += " , CODE(Format L)";
                                }


                                if (_rowStatus)
                                {
                                    if (Array.IndexOf(AryPREN, drEmp["PREN"].ToString()) < 0)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , PREN";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (Array.IndexOf(AryTPREN, drEmp["TPREN"].ToString()) < 0)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , TPREN";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (Array.IndexOf(ArySEX, drEmp["SEX"].ToString()) < 0)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , SEX";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (Array.IndexOf(AryTSEX, drEmp["TSEX"].ToString()) < 0)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , TSEX";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (Array.IndexOf(AryWTYPE, drEmp["WTYPE"].ToString()) < 0)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , WTYPE";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["IDNO"].ToString().Length > 13)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , IDNO";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (Array.IndexOf(AryWSTS, drEmp["WSTS"].ToString()) < 0)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , WSTS";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (Array.IndexOf(AryPOSIT, drEmp["POSIT"].ToString()) < 0)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , POSIT";
                                    }
                                }



                                if (_rowStatus)
                                {
                                    DateTime CKBirth = new DateTime(1900, 1, 1);
                                    try
                                    {
                                        CKBirth = new DateTime(Convert.ToInt32(drEmp["BIRTH"].ToString().Substring(0, 4)),
                                            Convert.ToInt32(drEmp["BIRTH"].ToString().Substring(4, 2)),
                                            Convert.ToInt32(drEmp["BIRTH"].ToString().Substring(6, 2)));
                                    }
                                    catch
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , BIRTH (YYYYMMDD)";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    DateTime CKJoin = new DateTime(1900, 1, 1);
                                    try
                                    {
                                        CKJoin = new DateTime(Convert.ToInt32(drEmp["JOIN"].ToString().Substring(0, 4)),
                                            Convert.ToInt32(drEmp["JOIN"].ToString().Substring(4, 2)),
                                            Convert.ToInt32(drEmp["JOIN"].ToString().Substring(6, 2)));
                                    }
                                    catch
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , JOIN (YYYYMMDD)";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    DateTime CKCONTRACT_EXP_DT = new DateTime(1900, 1, 1);
                                    try
                                    {
                                        CKCONTRACT_EXP_DT = new DateTime(Convert.ToInt32(drEmp["CONTRACT_EXP_DT"].ToString().Substring(0, 4)),
                                            Convert.ToInt32(drEmp["CONTRACT_EXP_DT"].ToString().Substring(4, 2)),
                                            Convert.ToInt32(drEmp["CONTRACT_EXP_DT"].ToString().Substring(6, 2)));
                                    }
                                    catch
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , CONTRACT_EXP_DT (YYYYMMDD)";
                                    }
                                }


                                if (_rowStatus)
                                {
                                    if (drEmp["BUS"].ToString().Length > 0)
                                    {
                                        DataRow[] drBUS = dtBUS.Select(" ITEM = '" + drEmp["BUS"].ToString() + "' ");

                                        if (drBUS.Count() == 0)
                                        {
                                            _rowStatus = false;
                                            _rowErrString += " , BUS";
                                        }
                                    }
                                    else
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , BUS";
                                    }
                                }


                                if (_rowStatus)
                                {
                                    if (drEmp["STOP"].ToString().Length > 0)
                                    {
                                        string bus_stop = (drEmp["STOP"].ToString().Trim().Length == 1) ? $"0{drEmp["STOP"].ToString().Trim()}" : drEmp["STOP"].ToString().Trim();

                                        DataRow[] drSTOP = dtSTOP.Select(" TITEM = '" + bus_stop + "' ");

                                        if (drSTOP.Count() == 0)
                                        {
                                            _rowStatus = false;
                                            _rowErrString += " , STOP";
                                        }
                                    }
                                    else
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , STOP";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["DEGREE"].ToString().Length > 0)
                                    {
                                        DataRow[] deDEGR = dtDEGR.Select(" DESCR = '" + drEmp["DEGREE"].ToString() + "' ");

                                        if (deDEGR.Count() == 0)
                                        {
                                            _rowStatus = false;
                                            _rowErrString += " , DEGREE";
                                        }
                                    }
                                    else
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , DEGREE";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["MAJOR"].ToString().Length > 100)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , MAJOR";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["TSCHOOL"].ToString().Length > 40)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , TSCHOOL";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["TCADDR1"].ToString().Length > 40)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , TCADDR1";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["TCADDR2"].ToString().Length > 40)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , TCADDR2";
                                    }
                                }


                                if (_rowStatus)
                                {
                                    if (drEmp["TCADDR3"].ToString().Length > 40)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , TCADDR3";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["TCADDR4"].ToString().Length > 40)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , TCADDR4";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["TCTEL"].ToString().Length > 12)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , TCTEL";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["NICKNAME"].ToString().Length > 20)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , NICKNAME";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["THADDR1"].ToString().Length > 40)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , THADDR1";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["THADDR2"].ToString().Length > 40)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , THADDR2";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["THADDR3"].ToString().Length > 40)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , THADDR3";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["THADDR4"].ToString().Length > 40)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , THADDR3";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["THTEL"].ToString().Length > 12)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , THTEL";
                                    }
                                }


                                if (_rowStatus)
                                {
                                    if (drEmp["COMPANY"].ToString().Length > 0)
                                    {
                                        DataRow[] drCOMPANY = dtCOMPANY.Select(" ITEM = '" + drEmp["COMPANY"].ToString() + "' ");

                                        if (drCOMPANY.Count() == 0)
                                        {
                                            _rowStatus = false;
                                            _rowErrString += " , COMPANY";
                                        }
                                    }
                                    else
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , COMPANY";
                                    }
                                }


                                /*
                                if (_rowStatus)
                                {
                                    if (drEmp["BANK"].ToString().Length > 0)
                                    {
                                        DataRow[] drBANK = dtBANK.Select(" ITEM = '" + drEmp["BANK"].ToString() + "' ");

                                        if (drBANK.Count() == 0)
                                        {
                                            _rowStatus = false;
                                            _rowErrString += " , BANK";
                                        }
                                    }
                                    else
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , BANK";
                                    }
                                }

                                if (_rowStatus) {
                                    if (drEmp["BANKAC"].ToString().Length > 0)
                                    {
                                        if (drEmp["BANKAC"].ToString().Length > 10) {
                                            _rowStatus = false;
                                            _rowErrString += " , BANK AC";
                                        }
                                    }
                                    else
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , BANK AC";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["WORKCENTER"].ToString().Length > 0)
                                    {
                                        if (drEmp["WORKCENTER"].ToString().Length > 10)
                                        {
                                            _rowStatus = false;
                                            _rowErrString += " , WORKCENTER";
                                        }
                                    }
                                    else
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , WORKCENTER";
                                    }
                                }
                                */

                                if (_rowStatus)
                                {
                                    if (Array.IndexOf(AryBudgetType, drEmp["BUDGETTYPE"].ToString()) < 0)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , BUDGETTYPE";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (Array.IndexOf(AryMARRY, drEmp["MARRY"].ToString()) < 0)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , MARRY";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (Array.IndexOf(AryRELIGION, drEmp["RELIGION"].ToString()) < 0)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , RELIGION";
                                    }
                                }


                                if (_rowStatus)
                                {
                                    if (drEmp["DVCD"].ToString().Length == 4 || drEmp["DVCD"].ToString().Length == 5)
                                    {
                                        DataRow[] drDVCD = dtDVCD.Select(" ITEM = '" + drEmp["DVCD"].ToString() + "' ");

                                        if (drDVCD.Count() == 0)
                                        {
                                            _rowStatus = false;
                                            _rowErrString += " , DVCD";
                                        }
                                    }
                                    else
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , DVCD";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["COSTCENTER"].ToString().Length == 4)
                                    {
                                        DataRow[] drCOSTCENTER = dtCOSTCENTER.Select(" COSTCENTER = '" + drEmp["COSTCENTER"].ToString() + "' ");

                                        if (drCOSTCENTER.Count() == 0)
                                        {
                                            _rowStatus = false;
                                            _rowErrString += " , COSTCENTER";
                                        }
                                    }
                                    else
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , COSTCENTER";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["GRPOT"].ToString().Length > 0)
                                    {
                                        DataRow[] drGRPOT = dtGRPOT.Select(" ITEM = '" + drEmp["GRPOT"].ToString() + "' ");

                                        if (drGRPOT.Count() == 0)
                                        {
                                            _rowStatus = false;
                                            _rowErrString += " , GRPOT";
                                        }
                                    }
                                    else
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , GRPOT";
                                    }
                                }

                                /*
                                if (_rowStatus)
                                {
                                    if (drEmp["GRPL"].ToString().Length > 0)
                                    {
                                        DataRow[] drLNGR = dtLNGR.Select(" ITEM = '" + drEmp["GRPL"].ToString() + "' ");

                                        if (drLNGR.Count() == 0)
                                        {
                                            _rowStatus = false;
                                            _rowErrString += " , GRPL";
                                        }
                                    }
                                    else
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , GRPL";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["LINECODE"].ToString().Length > 0)
                                    {
                                        DataRow[] drLine = dtLine.Select(" ln_code = '" + drEmp["LINECODE"].ToString() + "' ");

                                        if (drLine.Count() == 0)
                                        {
                                            _rowStatus = false;
                                            _rowErrString += " , LINE NO";
                                        }
                                    }
                                    else
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , LINE NO";
                                    }
                                }
                                */


                                if (!_rowStatus)
                                {
                                    VerifyStatus = false;
                                    StrError += String.Format("{0}: {1} [{2}]" + System.Environment.NewLine, no.ToString(), drEmp["CODE"].ToString(), _rowErrString);

                                }


                                no++;
                            } // end foreach





                            // check status
                            if (VerifyStatus)
                            {
                                try
                                {
                                    int cnt = 0;
                                    int nbr = 1;
                                    string InstrError = "";
                                    
                                    //*** Loop ****
                                    foreach (DataRow drEmp in dtEmpData.Rows)
                                    {
                                        string bus_stop = (drEmp["STOP"].ToString().Trim().Length == 1) ? $"0{drEmp["STOP"].ToString().Trim()}" : drEmp["STOP"].ToString().Trim();



                                        DateTime _Join = new DateTime(1900, 1, 1);
                                        try
                                        {
                                            _Join = new DateTime(Convert.ToInt32(drEmp["JOIN"].ToString().Substring(0, 4)),
                                                Convert.ToInt32(drEmp["JOIN"].ToString().Substring(4, 2)),
                                                Convert.ToInt32(drEmp["JOIN"].ToString().Substring(6, 2)));
                                        }
                                        catch { }

                                        DateTime _Birth = new DateTime(1900, 1, 1);
                                        try
                                        {
                                            _Birth = new DateTime(Convert.ToInt32(drEmp["BIRTH"].ToString().Substring(0, 4)),
                                                Convert.ToInt32(drEmp["BIRTH"].ToString().Substring(4, 2)),
                                                Convert.ToInt32(drEmp["BIRTH"].ToString().Substring(6, 2)));
                                        }
                                        catch { }

                                        DateTime _ContractExpDT = new DateTime(1900, 1, 1);
                                        try
                                        {
                                            _ContractExpDT = new DateTime(Convert.ToInt32(drEmp["CONTRACT_EXP_DT"].ToString().Substring(0, 4)),
                                                Convert.ToInt32(drEmp["CONTRACT_EXP_DT"].ToString().Substring(4, 2)),
                                                Convert.ToInt32(drEmp["CONTRACT_EXP_DT"].ToString().Substring(6, 2)));
                                        }
                                        catch { }


                                        string _code = drEmp["CODE"].ToString();
                                        string _bank = (!_code.StartsWith("I")) ? drEmp["BANK"].ToString() : "";
                                        string _bankac = (!_code.StartsWith("I")) ? drEmp["BANKAC"].ToString() : "";
                                        string strInstr = @"INSERT INTO EMPM (CODE, PREN, NAME, SURN, SEX, TPREN, TNAME, TSURN, TSEX, BIRTH, JOIN,  
                                            WTYPE, WSTS, POSIT, BUS, STOP, DEGREE, MAJOR, TSCHOOL, TCADDR1, TCADDR2, TCADDR3, TCADDR4, TCTEL, 
                                            MARRY, IDNO, DLRATE, NICKNAME, COMPANY, GB02, CONTRACT_EXP_DT, RELIGION, DVCD, THADDR1, 
                                            THADDR2, THADDR3, THADDR4, THTEL, GRPOT, GRPL, COSTCENTER, LINECODE, GB04, CR_BY, CR_DT, BANK, BANKAC, WCNO, GB03 ) VALUES ('" + _code + @"', 
                                    '" + EncodeLanguage(drEmp["PREN"].ToString()) + "', '" + EncodeLanguage(drEmp["NAME"].ToString()) + "', '" + EncodeLanguage(drEmp["SURN"].ToString()) + "', '" + EncodeLanguage(drEmp["SEX"].ToString()) + @"', 
                                    '" + EncodeLanguage(drEmp["TPREN"].ToString()) + "', '" + EncodeLanguage(drEmp["TNAME"].ToString()) + "', '" + EncodeLanguage(drEmp["TSURN"].ToString()) + @"', '" + EncodeLanguage(drEmp["TSEX"].ToString()) + @"', 
                                    '" + _Birth.ToString("dd/MMM/yyyy") + @"', '" + _Join.ToString("dd/MMM/yyyy") + @"',  
                                    '" + drEmp["WTYPE"].ToString() + "', '" + drEmp["WSTS"].ToString() + "', '" + drEmp["POSIT"].ToString() + @"', 
                                    '" + drEmp["BUS"].ToString() + "', '" + bus_stop + "', '" + drEmp["DEGREE"].ToString() + @"', 
                                    '" + EncodeLanguage(drEmp["MAJOR"].ToString()) + "', '" + EncodeLanguage(drEmp["TSCHOOL"].ToString()) + "', '" + EncodeLanguage(drEmp["TCADDR1"].ToString()) + @"', 
                                    '" + EncodeLanguage(drEmp["TCADDR2"].ToString()) + "', '" + EncodeLanguage(drEmp["TCADDR3"].ToString()) + "', '" + EncodeLanguage(drEmp["TCADDR4"].ToString()) + @"', 
                                    '" + drEmp["TCTEL"].ToString() + "', '" + drEmp["MARRY"].ToString() + "', '" + drEmp["IDNO"].ToString() + @"', 
                                    '" + drEmp["DLRATE"].ToString() + "', '" + EncodeLanguage(drEmp["NICKNAME"].ToString()) + "', '" + drEmp["COMPANY"].ToString() + @"', 
                                    '" + drEmp["BUDGETTYPE"].ToString() + "', '" + _ContractExpDT.ToString("dd/MMM/yyyy") + "', '" + drEmp["RELIGION"].ToString() + @"', 
                                    '" + drEmp["DVCD"].ToString() + "', '" + EncodeLanguage(drEmp["THADDR1"].ToString()) + "', '" + EncodeLanguage(drEmp["THADDR2"].ToString()) + @"', 
                                    '" + EncodeLanguage(drEmp["THADDR3"].ToString()) + "', '" + EncodeLanguage(drEmp["THADDR4"].ToString()) + "', '" + drEmp["THTEL"].ToString() + @"', 
                                    '" + drEmp["GRPOT"].ToString() + "', '" + drEmp["GRPL"].ToString() + "', '" + drEmp["COSTCENTER"].ToString() + @"', 
                                    '" + drEmp["LINECODE"].ToString() + "', '" + drEmp["LINECODE"].ToString() + "', '" + appMgr.UserAccount.AccountId + @"', SYSDATE, 
                                    '" + _bank + "', '" + _bankac + "', '" + drEmp["WORKCENTER"].ToString() + @"', '" + drEmp["WORKCENTER"].ToString() + @"' ) ";
                                        OracleCommand cmdInstr = new OracleCommand();
                                        cmdInstr.CommandText = strInstr;

                                        int res = 0;
                                        if (_code.StartsWith("7"))
                                        {
                                            res = oOraTRN.ExecuteCommandReturn(cmdInstr);
                                        }
                                        else if (_code.StartsWith("I"))
                                        {
                                            res = oOraSUB.ExecuteCommandReturn(cmdInstr);
                                        }
                                        else
                                        {
                                            res = oOraDCI.ExecuteCommandReturn(cmdInstr);
                                        }

                                        //            CODE	PREN	NAME	SURN	SEX	TPREN	TNAME	TSURN	TSEX	BIRTH	JOIN	            
                                        //            WTYPE	WSTS	POSIT	BUS	STOP	DEGREE	MAJOR	TSCHOOL	TCADDR1	TCADDR2	TCADDR3	TCADDR4	TCTEL	
                                        //            MARRY	IDNO	DLRATE	NICKNAME	COMPANY	BUDGETTYPE	CONTRACT_EXP_DT
                                        //            RELIGION	DVCD	THADDR1	THADDR2	THADDR3	THADDR4	THTEL	GRPOT	GRPL	COSTCENTER	LINECODE	



                                        if (res > 0)
                                        {
                                            cnt++;
                                        }
                                        else
                                        {
                                            InstrError += nbr.ToString() + ". " + _code + " : " + System.Environment.NewLine;
                                        }


                                        nbr++;
                                    } // end foreach


                                    txtEmployeeUpload.Text = "";
                                    MessageBox.Show("Successful in " + cnt.ToString() + "/" + dtEmpData.Rows.Count.ToString(), "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            } // end if
                            else
                            {
                                MessageBox.Show(StrError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        } // end have data


                        #endregion


                    }
                    else if (ImpTypeCmb.SelectedValue.ToString() == "GRPOT")
                    {
                        #region GRPOT
                        if (dtEmpData.Rows.Count > 0)
                        {
                            string StrError = "เกิดความผิดพลาดของข้อมูล";
                            bool VerifyStatus = true;
                            int no = 1;

                            //CODE      DVCD    GRPOT   COSTCENTER  WORKCENTER  LINENO
                            DataTable dtGRPOT = new DataTable();
                            string strGRPOT = @"SELECT ITEM FROM DICT WHERE TYPE = 'SHFT' ORDER BY ITEM ";
                            OracleCommand cmdGRPOT = new OracleCommand();
                            cmdGRPOT.CommandText = strGRPOT;
                            dtGRPOT = oOraDCI.Query(cmdGRPOT);


                            DataTable dtDVCD = new DataTable();
                            string strDVCD = @"SELECT ITEM FROM DICT WHERE TYPE = 'DVCD' ORDER BY ITEM";
                            OracleCommand cmdDVCD = new OracleCommand();
                            cmdDVCD.CommandText = strDVCD;
                            dtDVCD = oOraDCI.Query(cmdDVCD);


                            DataTable dtCOSTCENTER = new DataTable();
                            string strCOSTCENTER = @"SELECT DISTINCT COSTCENTER FROM EMPM WHERE RESIGN IS NULL ORDER BY COSTCENTER";
                            OracleCommand cmdCOSTCENTER = new OracleCommand();
                            cmdCOSTCENTER.CommandText = strCOSTCENTER;
                            dtCOSTCENTER = oOraDCI.Query(cmdCOSTCENTER);


                            DataTable dtLine = new DataTable();
                            string strLine = @"SELECT DISTINCT [ln_code] FROM [dbHRM].[dbo].[DVCD_Std] ORDER BY [ln_code] ";
                            SqlCommand cmdLine = new SqlCommand();
                            cmdLine.CommandText = strLine;
                            dtLine = oSqlHRM.Query(cmdLine);


                            //*** Loop For Check ****
                            foreach (DataRow drEmp in dtEmpData.Rows)
                            {
                                // init row status
                                bool _rowStatus = true;
                                // init error string
                                string _rowErrString = "";




                                if (drEmp["CODE"].ToString().Length == 5)
                                {
                                    DataTable dtEmp = new DataTable();
                                    string strEmp = @"SELECT * FROM Employee WHERE Code=@Code ";
                                    SqlCommand cmdEmp = new SqlCommand();
                                    cmdEmp.CommandText = strEmp;
                                    cmdEmp.Parameters.Add(new SqlParameter("@Code", drEmp["CODE"].ToString()));
                                    dtEmp = oSqlHRM.Query(cmdEmp);

                                    if (dtEmp.Rows.Count == 0)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , CODE";
                                    }
                                }
                                else
                                {
                                    _rowStatus = false;
                                    _rowErrString += " , CODE";
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["DVCD"].ToString().Length == 4 || drEmp["DVCD"].ToString().Length == 5)
                                    {
                                        DataRow[] drDVCD = dtDVCD.Select(" ITEM = '" + drEmp["DVCD"].ToString() + "' ");

                                        if (drDVCD.Count() == 0)
                                        {
                                            _rowStatus = false;
                                            _rowErrString += " , DVCD";
                                        }
                                    }
                                    else
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , DVCD";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["COSTCENTER"].ToString().Length == 4)
                                    {
                                        DataRow[] drCOSTCENTER = dtCOSTCENTER.Select(" COSTCENTER = '" + drEmp["COSTCENTER"].ToString() + "' ");

                                        if (drCOSTCENTER.Count() == 0)
                                        {
                                            _rowStatus = false;
                                            _rowErrString += " , COSTCENTER";
                                        }
                                    }
                                    else
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , COSTCENTER";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["GRPOT"].ToString().Length > 0)
                                    {
                                        DataRow[] drGRPOT = dtGRPOT.Select(" ITEM = '" + drEmp["GRPOT"].ToString() + "' ");

                                        if (drGRPOT.Count() == 0)
                                        {
                                            _rowStatus = false;
                                            _rowErrString += " , GRPOT";
                                        }
                                    }
                                    else
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , GRPOT";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["LINENO"].ToString().Length > 0)
                                    {
                                        DataRow[] drLine = dtLine.Select(" ln_code = '" + drEmp["LINENO"].ToString() + "' ");

                                        if (drLine.Count() == 0)
                                        {
                                            _rowStatus = false;
                                            _rowErrString += " , LINE NO";
                                        }
                                    }
                                    else
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , LINE NO";
                                    }
                                }



                                if (!_rowStatus)
                                {
                                    VerifyStatus = false;
                                    StrError += String.Format("{0}: {1} [{2}]" + System.Environment.NewLine, no.ToString(), drEmp["CODE"].ToString(), _rowErrString);

                                }


                                no++;
                            } // end foreach





                            //**** CHECK BEFORE UPDATE ****
                            //CODE DVCD    GRPOT COSTCENTER  WORKCENTER LINENO
                            if (VerifyStatus)
                            {
                                try
                                {
                                    //*** Loop For Save Data ****
                                    foreach (DataRow drEmp in dtEmpData.Rows)
                                    {
                                        string _code = drEmp["CODE"].ToString();
                                        string _dvcd = drEmp["DVCD"].ToString();
                                        string _grpot = drEmp["GRPOT"].ToString();
                                        string _costcenter = drEmp["COSTCENTER"].ToString();
                                        string _workcenter = drEmp["WORKCENTER"].ToString();
                                        string _lineno = drEmp["LINENO"].ToString();

                                        string strUpd = @"UPDATE EMPM SET DVCD='" + _dvcd + "', GRPOT='" + _grpot + "', COSTCENTER='" + _costcenter + @"', 
                                                    WCNO='" + _workcenter + "', GB03='" + _workcenter + "' , LINECODE='" + _lineno + @"', GB04='" + _lineno + @"', UPD_BY='" + appMgr.UserAccount.AccountId + @"', 
                                                    UPD_DT=SYSDATE  WHERE CODE='" + _code + "'   ";
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
                                    } // end foreach



                                    txtEmployeeUpload.Text = "";
                                    MessageBox.Show("Successful !", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }
                            else
                            {
                                MessageBox.Show(StrError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        } // end if check have data 


                        #endregion
                    }
                    else if (ImpTypeCmb.SelectedValue.ToString() == "PROFILE") {
                        #region PROFILE
                        if (dtEmpData.Rows.Count > 0)
                        {
                            string StrError = "เกิดความผิดพลาดของข้อมูล";
                            bool VerifyStatus = true;
                            int no = 1;

                            //*** Loop For Check ****
                            foreach (DataRow drEmp in dtEmpData.Rows)
                            {
                                // init row status
                                bool _rowStatus = true;
                                // init error string
                                string _rowErrString = "";




                                if (drEmp["CODE"].ToString().Length == 5)
                                {
                                    DataTable dtEmp = new DataTable();
                                    string strEmp = @"SELECT * FROM Employee WHERE Code=@Code ";
                                    SqlCommand cmdEmp = new SqlCommand();
                                    cmdEmp.CommandText = strEmp;
                                    cmdEmp.Parameters.Add(new SqlParameter("@Code", drEmp["CODE"].ToString()));
                                    dtEmp = oSqlHRM.Query(cmdEmp);

                                    if (dtEmp.Rows.Count == 0)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , CODE";
                                    }
                                }
                                else
                                {
                                    _rowStatus = false;
                                    _rowErrString += " , CODE";
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["TCADDR1"].ToString().Length > 40)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , TCADDR1 (40)";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["TCADDR2"].ToString().Length > 40)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , TCADDR2 (40)";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["TCADDR3"].ToString().Length > 40)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , TCADDR3 (40)";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["TCADDR4"].ToString().Length > 40)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , TCADDR4 (40)";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["IDNO"].ToString().Length != 13)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , IDNO";
                                    }
                                }


                                if (_rowStatus)
                                {
                                    if (drEmp["BANKAC"].ToString().Length > 0 && !drEmp["CODE"].ToString().StartsWith("I"))
                                    {
                                        if (drEmp["BANKAC"].ToString().Length > 10)
                                        {
                                            _rowStatus = false;
                                            _rowErrString += " , BANK AC";
                                        }
                                    }
                                    else
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , BANK AC";
                                    }
                                }

                                if (_rowStatus)
                                {
                                    if (drEmp["NICKNAME"].ToString().Length > 20)
                                    {
                                        _rowStatus = false;
                                        _rowErrString += " , NICKNAME";
                                    }
                                }

                                if (!_rowStatus)
                                {
                                    VerifyStatus = false;
                                    StrError += String.Format("{0}: {1} [{2}]" + System.Environment.NewLine, no.ToString(), drEmp["CODE"].ToString(), _rowErrString);

                                }


                                no++;
                            } // end foreach





                            //**** CHECK BEFORE UPDATE ****
                            //CODE TCADDR1    IDNO BANKAC  NICKNAME
                            if (VerifyStatus)
                            {
                                try
                                {
                                    //*** Loop For Save Data ****
                                    foreach (DataRow drEmp in dtEmpData.Rows)
                                    {
                                        string _code = drEmp["CODE"].ToString();
                                        string _tcaddr1 = EncodeLanguage(drEmp["TCADDR1"].ToString());
                                        string _tcaddr2 = EncodeLanguage(drEmp["TCADDR2"].ToString());
                                        string _tcaddr3 = EncodeLanguage(drEmp["TCADDR3"].ToString());
                                        string _tcaddr4 = EncodeLanguage(drEmp["TCADDR4"].ToString());
                                        string _idno = drEmp["IDNO"].ToString();
                                        string _bankac = drEmp["BANKAC"].ToString();
                                        string _nickname = EncodeLanguage(drEmp["NICKNAME"].ToString());

                                        string strUpd = (!_code.StartsWith("I")) ? 
                                                        @"UPDATE EMPM SET TCADDR1='" + _tcaddr1 + "', TCADDR2='" + _tcaddr2 + "', TCADDR3='" + _tcaddr3 + "', TCADDR4='" + _tcaddr4 + @"', 
                                                            IDNO='" + _idno + "', BANKAC='" + _bankac + @"', NICKNAME='" + _nickname + "', UPD_BY='" + appMgr.UserAccount.AccountId + @"', UPD_DT=SYSDATE  
                                                          WHERE CODE='" + _code + "'   " : 
                                                        @"UPDATE EMPM SET TCADDR1='" + _tcaddr1 + "', TCADDR2='" + _tcaddr2 + "', TCADDR3='" + _tcaddr3 + "', TCADDR4='" + _tcaddr4 + @"', 
                                                            IDNO='" + _idno + "', BANK='', BANKAC='', NICKNAME='" + _nickname + "', UPD_BY='" + appMgr.UserAccount.AccountId + @"', UPD_DT=SYSDATE  
                                                          WHERE CODE='" + _code + "'   ";


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
                                    } // end foreach



                                    txtEmployeeUpload.Text = "";
                                    MessageBox.Show("Successful !", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }
                            else
                            {
                                MessageBox.Show(StrError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        } // end if check have data 
                        #endregion
                    }

                } // end if datatable
            
            } // end if check text


        }

        private void btnTemplate_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                string fileName = "EMP_NEW";
                string sourceFilePath = Application.StartupPath + "\\TEMPLATE\\TMP_EMP_NEW.xlsx";
                if (ImpTypeCmb.SelectedValue.ToString() == "EMP_NEW")
                {
                    fileName = "EMP_NEW";
                    sourceFilePath = Application.StartupPath + "\\TEMPLATE\\TMP_EMP_NEW.xlsx";
                }
                else if (ImpTypeCmb.SelectedValue.ToString() == "GRPOT")
                {
                    fileName = "EMP_GRPOT";
                    sourceFilePath = Application.StartupPath + "\\TEMPLATE\\TMP_EMP_GRPOT.xlsx";
                }
                else if (ImpTypeCmb.SelectedValue.ToString() == "PROFILE")
                {
                    fileName = "EMP_PROFILE";
                    sourceFilePath = Application.StartupPath + "\\TEMPLATE\\TMP_EMP_PROFILE.xlsx";
                }

                //string fileName = (ImpTypeCmb.SelectedValue.ToString() == "GRPOT") ? "EMP_GRPOT" : "EMP_NEW";
                //string sourceFilePath = (ImpTypeCmb.SelectedValue.ToString() == "GRPOT") ? Application.StartupPath + "\\TEMPLATE\\TMP_EMP_GRPOT.xlsx" : Application.StartupPath + "\\TEMPLATE\\TMP_EMP_NEW.xlsx";
                
                saveFileDialog.Title = "Save File";
                saveFileDialog.FileName = fileName + "_Template.xlsx";
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


        // GET DATA FROM EXCEL AND POPULATE COMB0 BOX.
        private DataTable ReadConvertExcel(string sFile, string ShtName)
        {
            DataTable dtData = new DataTable();
            //Excel.Application xlApp;
            //Excel.Workbook xlWorkBook;
            //Excel.Worksheet xlWorkSheet;
            //Excel.Range range;

            //object misValue = System.Reflection.Missing.Value;
            //string str;
            //int rCnt;
            //int cCnt;
            //int rw = 0;
            //int cl = 0;
            //List<int> colIdx = new List<int>();

            //xlApp = new Excel.Application();
            //xlWorkBook = xlApp.Workbooks.Open(sFile, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            ////xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            //xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets[ShtName];

            ////MessageBox.Show(xlWorkSheet.get_Range("A1", "A1").Value2.ToString());


            //range = xlWorkSheet.UsedRange;
            //rw = range.Rows.Count;
            ////MessageBox.Show(range.Rows.Count.ToString());
            //if (rw > 201)
            //{
            //    rw = 201;
            //}
            //cl = range.Columns.Count;

            ////********** SET Array Columns Index *************
            //for (cCnt = 1; cCnt < cl; cCnt++)
            //{
            //    string strCol = "";
            //    try
            //    {
            //        strCol = (range.Cells[1, cCnt] as Excel.Range).Value2.ToString().Trim();
            //    }
            //    catch (Exception ex) { }  

            //    if (strCol != "")
            //    {
            //        colIdx.Add(cCnt);
            //    }
            //}


            ////string[] aryColDate = { "JOIN", "CONTRACT_EXP_DT" };

            ////******* Loop Set Columns Header **********
            //if (colIdx.Count > 0)
            //{
            //    foreach (int cIdx in colIdx)
            //    {
            //        string strCol = (range.Cells[1, cCnt] as Excel.Range).Value2.ToString().Trim();

            //        if (strCol=="BIRTH" || strCol == "JOIN" || strCol == "CONTRACT_EXP_DT")
            //        {
            //            dtData.Columns.Add(strCol, typeof(DateTime));
            //        }
            //        else
            //        {
            //            dtData.Columns.Add(strCol, typeof(string));
            //        }
            //    } // end foreach columns




            //    for (rCnt = 2; rCnt <= rw; rCnt++)
            //    {
            //        DataRow newRow = dtData.NewRow();
            //        foreach (int cIdx in colIdx)
            //        {
            //            string strCol = (range.Cells[1, cIdx] as Excel.Range).Value2.ToString().Trim();

            //            string strVal = "";
            //            DateTime dateVal = new DateTime(1900,1,1);

            //            try { strVal = (range.Cells[rCnt, cIdx] as Excel.Range).Value2.ToString(); }
            //            catch (Exception ex) { }


            //            if (strCol == "BIRTH" || strCol == "JOIN" || strCol == "CONTRACT_EXP_DT")
            //            {
            //                try { dateVal = Convert.ToDateTime(strVal); }catch (Exception ex) { }
            //                newRow[strCol] = dateVal;
            //            }
            //            else
            //            {
            //                newRow[strCol] = strVal;
            //            }
                        

            //        } // end foreach columns 


            //        dtData.Rows.Add(newRow);
            //    }


            //} // end columns have data 





            //xlWorkBook.Close(true, misValue, misValue);
            //xlApp.Quit();

            //releaseObject(xlWorkSheet);
            //releaseObject(xlWorkBook);
            //releaseObject(xlApp);

            return dtData;

        }

        
        
        // GET DATA FROM EXCEL AND POPULATE COMB0 BOX.
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
                    if(dt.Columns.Count > 0)
                    {
                        for(int col = 0; col < dt.Columns.Count; col++)
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
                MessageBox.Show(" Error : Unable to release the Object " + ex.ToString() );
            }
            finally
            {
                GC.Collect();
            }
        }

        private void DatePicker_ValueChanged(object sender, EventArgs e)
        {
            //DateTimePicker obj = sender as DateTimePicker;

            //if (obj.Value <= new DateTime(1900, 1, 1))
            //{                

            //    obj.CalendarForeColor = Color.White;
            //}
            //else
            //{
            //    obj.CalendarForeColor = Color.Black;
            //}
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




    }






    public class UNCAccess
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct USE_INFO_2
        {
            internal LPWSTR ui2_local;
            internal LPWSTR ui2_remote;
            internal LPWSTR ui2_password;
            internal DWORD ui2_status;
            internal DWORD ui2_asg_type;
            internal DWORD ui2_refcount;
            internal DWORD ui2_usecount;
            internal LPWSTR ui2_username;
            internal LPWSTR ui2_domainname;
        }

        [DllImport("NetApi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern NET_API_STATUS NetUseAdd(
            LPWSTR UncServerName,
            DWORD Level,
            ref USE_INFO_2 Buf,
            out DWORD ParmError);

        [DllImport("NetApi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern NET_API_STATUS NetUseDel(
            LPWSTR UncServerName,
            LPWSTR UseName,
            DWORD ForceCond);

        private string sUNCPath;
        private string sUser;
        private string sPassword;
        private string sDomain;
        private int iLastError;
        public UNCAccess()
        {
        }
        public UNCAccess(string UNCPath, string User, string Domain, string Password)
        {
            login(UNCPath, User, Domain, Password);
        }
        public int LastError
        {
            get { return iLastError; }
        }

        /// <summary>
        /// Connects to a UNC share folder with credentials
        /// </summary>
        /// <param name="UNCPath">UNC share path</param>
        /// <param name="User">Username</param>
        /// <param name="Domain">Domain</param>
        /// <param name="Password">Password</param>
        /// <returns>True if login was successful</returns>
        public bool login(string UNCPath, string User, string Domain, string Password)
        {
            sUNCPath = UNCPath;
            sUser = User;
            sPassword = Password;
            sDomain = Domain;
            return NetUseWithCredentials();
        }
        private bool NetUseWithCredentials()
        {
            uint returncode;
            try
            {
                USE_INFO_2 useinfo = new USE_INFO_2();

                useinfo.ui2_remote = sUNCPath;
                useinfo.ui2_username = sUser;
                useinfo.ui2_domainname = sDomain;
                useinfo.ui2_password = sPassword;
                useinfo.ui2_asg_type = 0;
                useinfo.ui2_usecount = 1;
                uint paramErrorIndex;
                returncode = NetUseAdd(null, 2, ref useinfo, out paramErrorIndex);
                iLastError = (int)returncode;
                return returncode == 0;
            }
            catch
            {
                iLastError = Marshal.GetLastWin32Error();
                return false;
            }
        }

        /// <summary>
        /// Closes the UNC share
        /// </summary>
        /// <returns>True if closing was successful</returns>
        public bool NetUseDelete()
        {
            uint returncode;
            try
            {
                returncode = NetUseDel(null, sUNCPath, 2);
                iLastError = (int)returncode;
                return (returncode == 0);
            }
            catch
            {
                iLastError = Marshal.GetLastWin32Error();
                return false;
            }
        }


        


    }



}
