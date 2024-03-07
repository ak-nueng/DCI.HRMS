using System;
using System.Collections.Generic;
using System.Text;
using PCUOnline.Dao;
using System.Data;
using DCI.HRMS.Model;
using PCUOnline.Dao.Ora;
using Oracle.ManagedDataAccess.Client;
using DCI.HRMS.Model.Personal;
using DCI.HRMS.Model.Organize;
using System.Collections;
using System.Globalization;
using System.Reflection.Emit;

namespace DCI.HRMS.Persistence.Oracle
{
    public class OraEmployeeDao : DaoBase, IEmployeeDao
    {
        //private const string SP_STOREEmployee = "PKG_HR_EMP.sp_storeemp_2";
        private const string SP_STOREEmployee = "PKG_HR_EMP.sp_storeemp_3";
        private const string SP_UPDATEEmployeeBusway = "PKG_HR_EMP.sp_updateempbusway";
        private const string SP_SELECTEmployeeData = "PKG_HR_EMP.sp_selectdata";
        private const string SP_GEN_EmployeeData = "PKG_HR_EMP.sp_gen_emp_mstr";
        private const string SP_GEN_EmployeeResignData = "PKG_HR_EMP.sp_gen_emp_resign";
        private const string SP_DELETEEmployee = "PKG_HR_EMP.sp_delete";
        private const string SP_SELECT = "PKG_HR_EMP.SP_SELECT";
        private const string SP_SELECTALL = "PKG_HR_EMP.sp_get_all_emp";
        private const string SP_SELECTCurrentEmp = "PKG_HR_EMP.sp_get_cur_emp_all";
        private const string SP_SELECTCurrentEmpByPosit = "PKG_HR_EMP.sp_get_cur_emp_byposit";
        private const string SP_SELECTCurrentEmpByDVCD = "PKG_HR_EMP.sp_get_cur_emp_bydvcd";
        private const string SP_SELECTCurrentEmpListByDVCD = "PKG_HR_EMP.sp_get_cur_emp_list_bydvcd";
        private const string SP_SELECTCurrentEmpByBusWay = "PKG_HR_EMP.sp_get_cur_emp_bybusway";
        private const string SP_SELECTFamily = "PKG_HR_EMP.sp_selectempfamily";
        private const string SP_STOREFamily = "PKG_HR_EMP.sp_storeempfamily";
        private const string SP_DELETEFamily = "PKG_HR_EMP.sp_deleteempfamily";

        private const string SP_SELECTWorkHistory = "PKG_HR_EMP.sp_selectempworkhist";
        private const string SP_STOREWorkHistory = "PKG_HR_EMP.sp_storeempworkhist";
        private const string SP_DELETEWorkHistory = "PKG_HR_EMP.sp_deleteempworkhist";
        private const string SP_EmployeeResigning = "PKG_HR_EMP.sp_empresigning_2";
        private const string SP_EmployeeSetEmail = "PKG_HR_EMP.sp_empsetemail";

        private const string SP_STOREEmpCodeTransfer = "PKG_HR_EMP. sp_storeempcodetransfer";
        private const string SP_DELETEEmpCodeTransfer = "PKG_HR_EMP.sp_deleteemptransfer";
        private const string SP_SELECTEmpCodeTransfer = "PKG_HR_EMP.sp_selecteempcodetransfer";
        private const string SP_SELECTEmpCodeTransferByCode = "PKG_HR_EMP.sp_selecttransferbycode";
        private const string SP_SELECTUnqEmpTransfer = "PKG_HR_EMP.sp_selectunqeemptransfer";

        private const string SP_GETManpowerByGrpot = "PKG_HR_EMP.sp_getmanpower_otgrp";

        private const string SP_GETManpowerForBC = "PKG_HR_EMP.sp_getmanpowerforbc";
        private const string SP_GETManpowerForBC1 = "PKG_HR_EMP.sp_getmanpowerforbc_1";


        private const string SP_SELECTRecordHistory = "PKG_HR_EMP.sp_selectrechist";

        private const string SP_GENEmpCardIssue = "PKG_HR_EMP.sp_gencardissue";
        private const string SP_GENEmpCardIssue1 = "PKG_HR_EMP.sp_gencardissue_1";
        private const string SP_GETEmpCardIssue = "PKG_HR_EMP.sp_getcardissue";

        private const string PARAM_ACTION = "p_action";
        private const string PARAM_DATE = "p_date";
        private const string PARAM_CODE = "p_code";
        private const string PARAM_RELATION = "p_relation";
        private const string PARAM_PRENAME = "p_pren";
        private const string PARAM_NAME = "p_name";
        private const string PARAM_SURN = "p_surn";
        private const string PARAM_BIRTH = "p_birth";
        private const string PARAM_ID = "p_citizid";
        private const string PARAM_TAX = "p_taxded";
        private const string PARAM_USER = "p_by";

        private const string PARAM_COMPANY = "p_company";
        private const string PARAM_ADDRESS = "p_address";
        private const string PARAM_TCOMPANY = "p_tcompany";
        private const string PARAM_TADDRESS = "p_taddress";
        private const string PARAM_WORKFROM = "p_wfrom";
        private const string PARAM_WORKTO = "p_wto";
        private const string PARAM_LASTPOSIT = "p_lastposit";
        private const string PARAM_REASON = "p_reason";

        private const string PARAM_TransferDate = "p_tdate";
        private const string PARAM_TransferStatus = "p_tsts";
        private const string PARAM_OldCode = "p_ocode";
        private const string PARAM_NewCode = "p_ncode";
        private const string PARAM_DateFrom = "p_datefrom";
        private const string PARAM_DateTo = "p_dateto";
        private OraDivisionDao divisionDao;

        public OraEmployeeDao(DaoManager daoManager)
            : base(daoManager)
        {
            divisionDao = new OraDivisionDao(daoManager);
        }

        public override void AddParameters(IDbCommand cmd, object obj)
        {
            OracleCommand oraCmd = (OracleCommand)cmd;

            if (obj is FamilyInfo)
            {
                FamilyInfo item = (FamilyInfo)obj;
                oraCmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = item.EmpCode;
                oraCmd.Parameters.Add(PARAM_RELATION, OracleDbType.Varchar2).Value = item.Relation;
                oraCmd.Parameters.Add(PARAM_PRENAME, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.NameInThai.Title);
                oraCmd.Parameters.Add(PARAM_NAME, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.NameInThai.Name);
                oraCmd.Parameters.Add(PARAM_SURN, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.NameInThai.Surname);
                oraCmd.Parameters.Add(PARAM_BIRTH, OracleDbType.Date).Value = item.Birth;
                oraCmd.Parameters.Add(PARAM_ID, OracleDbType.Varchar2).Value = item.IdNo;
                oraCmd.Parameters.Add(PARAM_TAX, OracleDbType.Double).Value = item.TaxDed;
            }
            else if (obj is WorkHistoryInfo)
            {
                WorkHistoryInfo item = (WorkHistoryInfo)obj;

                oraCmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = item.EmpCode;
                oraCmd.Parameters.Add(PARAM_COMPANY, OracleDbType.Varchar2).Value = item.CompanyName;
                oraCmd.Parameters.Add(PARAM_ADDRESS, OracleDbType.Varchar2).Value = item.Address;
                oraCmd.Parameters.Add(PARAM_TCOMPANY, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.CompanyNameInThai);
                oraCmd.Parameters.Add(PARAM_TADDRESS, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.AddressInThai);
                oraCmd.Parameters.Add(PARAM_WORKFROM, OracleDbType.Date).Value = item.WorkFrom;
                oraCmd.Parameters.Add(PARAM_WORKTO, OracleDbType.Date).Value = item.WorkTo;
                oraCmd.Parameters.Add(PARAM_LASTPOSIT, OracleDbType.Date).Value = item.LastPosition;
                oraCmd.Parameters.Add(PARAM_REASON, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.ResignReason);

            }
                    else if (obj is CooperativeInfo)
            {
                CooperativeInfo item = (CooperativeInfo)obj;
          
                oraCmd.Parameters.Add("p_coop_date", OracleDbType.Varchar2).Value = item.CooDate > new DateTime(1900, 1, 1) ? item.CooDate.ToString("dd/MMM/yyyy", new CultureInfo("EN-US")) : "";
                oraCmd.Parameters.Add("p_coop_term", OracleDbType.Varchar2).Value = item.CooTerm > new DateTime(1900, 1, 1) ? item.CooTerm.ToString("dd/MMM/yyyy", new CultureInfo("EN-US")) : "";
                oraCmd.Parameters.Add("p_coop_amount", OracleDbType.Decimal).Value = item.Amount;
                 oraCmd.Parameters.Add("p_coop_deduct", OracleDbType.Decimal).Value = item.Deduct; 
                    }
            else if (obj is ProvidenceInfo)
            {
                ProvidenceInfo item = (ProvidenceInfo)obj;
                oraCmd.Parameters.Add("p_pvno", OracleDbType.Varchar2).Value = item.ProvidenceNo;
                oraCmd.Parameters.Add("p_pvdate", OracleDbType.Varchar2).Value = item.ProvidenceDate > new DateTime(1900, 1, 1) ? item.ProvidenceDate.ToString("dd/MMM/yyyy", new CultureInfo("EN-US")) : "";
                oraCmd.Parameters.Add("p_pvterm", OracleDbType.Varchar2).Value = item.ProvidenceTerminate > new DateTime(1900, 1, 1) ? item.ProvidenceTerminate.ToString("dd/MMM/yyyy", new CultureInfo("EN-US")) : "";
                oraCmd.Parameters.Add("p_pvperc", OracleDbType.Decimal).Value = item.ProvidencePercent;
                oraCmd.Parameters.Add("p_pvperccompany", OracleDbType.Decimal).Value = item.ProvidencePercentCompany;
            }
            else if (obj is EducationInfo)
            {
                EducationInfo item = (EducationInfo)obj;
                oraCmd.Parameters.Add("p_degree", OracleDbType.Varchar2).Value = item.Degree;
                oraCmd.Parameters.Add("p_dtype", OracleDbType.Varchar2).Value = item.DegreeType;
                oraCmd.Parameters.Add("p_major", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.MajorInEng);
                oraCmd.Parameters.Add("p_tmajor", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.MajorInThai);
                oraCmd.Parameters.Add("p_schly", OracleDbType.Varchar2).Value = item.GraduateYear;
                oraCmd.Parameters.Add("p_school", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.SchoolInEng);
                oraCmd.Parameters.Add("p_tschool", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.SchoolInThai);

            }
            else if (obj is EmployeeDataInfo)
            {

                EmployeeDataInfo item = (EmployeeDataInfo)obj;
                /******************************************************************************/
                /***************************************************************************/
                oraCmd.Parameters.Add("p_code", OracleDbType.Varchar2).Value = item.Code;
                oraCmd.Parameters.Add("p_pren", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.NameInEng.Title);
                oraCmd.Parameters.Add("p_name", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.NameInEng.Name);
                oraCmd.Parameters.Add("p_surn", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.NameInEng.Surname);
                oraCmd.Parameters.Add("p_sex", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.Gender);
                oraCmd.Parameters.Add("p_tpren", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.NameInThai.Title);
                oraCmd.Parameters.Add("p_tname", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.NameInThai.Name);
                oraCmd.Parameters.Add("p_tsurn", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.NameInThai.Surname);
                oraCmd.Parameters.Add("p_nickname", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.NameInThai.NickName);
                oraCmd.Parameters.Add("p_tsex", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.GenderTH);
                oraCmd.Parameters.Add("p_birth", OracleDbType.Date).Value = item.BirthDate;
                oraCmd.Parameters.Add("p_religion", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.Religion);
                AddParameters(oraCmd, item.Education);
                oraCmd.Parameters.Add("p_ecaddr1", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.PresentAddressInEng.Address);
                oraCmd.Parameters.Add("p_ecaddr2", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.PresentAddressInEng.Subdistrict);
                oraCmd.Parameters.Add("p_ecaddr3", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.PresentAddressInEng.District);
                oraCmd.Parameters.Add("p_ecaddr4", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.PresentAddressInEng.Province);
                oraCmd.Parameters.Add("p_ectel", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.PresentAddressInEng.Telephone);
                oraCmd.Parameters.Add("p_tcaddr1", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.PresentAddressInThai.Address);
                oraCmd.Parameters.Add("p_tcaddr2", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.PresentAddressInThai.Subdistrict);
                oraCmd.Parameters.Add("p_tcaddr3", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.PresentAddressInThai.District);
                oraCmd.Parameters.Add("p_tcaddr4", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.PresentAddressInThai.Province);
                oraCmd.Parameters.Add("p_tctel", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.PresentAddressInThai.Telephone);
                oraCmd.Parameters.Add("p_ehaddr1", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.HomeAddressInEng.Address);
                oraCmd.Parameters.Add("p_ehaddr2", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.HomeAddressInEng.Subdistrict);
                oraCmd.Parameters.Add("p_ehaddr3", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.HomeAddressInEng.District);
                oraCmd.Parameters.Add("p_ehaddr4", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.HomeAddressInEng.Province);
                oraCmd.Parameters.Add("p_ehtel", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.HomeAddressInEng.Telephone);
                oraCmd.Parameters.Add("p_thaddr1", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.HomeAddressInThai.Address);
                oraCmd.Parameters.Add("p_thaddr2", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.HomeAddressInThai.Subdistrict);
                oraCmd.Parameters.Add("p_thaddr3", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.HomeAddressInThai.District);
                oraCmd.Parameters.Add("p_thaddr4", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.HomeAddressInThai.Province);
                oraCmd.Parameters.Add("p_thtel", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.HomeAddressInThai.Telephone);
                oraCmd.Parameters.Add("p_marry", OracleDbType.Varchar2).Value = item.MarryStatus;
                oraCmd.Parameters.Add("p_sons", OracleDbType.Int16).Value = item.Sons;
                oraCmd.Parameters.Add("p_sonb", OracleDbType.Int16).Value = item.Sonb;
                oraCmd.Parameters.Add("p_idno", OracleDbType.Varchar2).Value = item.CitizenId;
                oraCmd.Parameters.Add("p_iddate", OracleDbType.Varchar2).Value = item.IdcardIssueDate > new DateTime(1900, 1, 1) ? item.IdcardIssueDate.ToString("dd/MMM/yyyy", new CultureInfo("EN-US")) : ""; ;
                oraCmd.Parameters.Add("p_taxno", OracleDbType.Varchar2).Value = item.TaxNumber;
                oraCmd.Parameters.Add("p_military", OracleDbType.Varchar2).Value = item.MilitaryStatus;
                oraCmd.Parameters.Add("p_refperson1", OracleDbType.Varchar2).Value =OraHelper.EncodeLanguage( item.RefPerson1);
                oraCmd.Parameters.Add("p_refContact1", OracleDbType.Varchar2).Value =OraHelper.EncodeLanguage( item.RefContact1);
                oraCmd.Parameters.Add("p_refperson2", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.RefPerson2);
                oraCmd.Parameters.Add("p_refContact2", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.RefContact2);

                /****************************************************************************************/
                oraCmd.Parameters.Add("p_join", OracleDbType.Date).Value = item.JoinDate;
                oraCmd.Parameters.Add("p_resign", OracleDbType.Varchar2).Value = item.ResignDate > new DateTime(1900, 1, 1) ? item.ResignDate.ToString("dd/MMM/yyyy", new CultureInfo("EN-US")) : "";
                oraCmd.Parameters.Add("p_rstype", OracleDbType.Varchar2).Value = item.ResignType;
                oraCmd.Parameters.Add("p_rsreason", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.ResignReason);

                oraCmd.Parameters.Add("p_dvcd", OracleDbType.Varchar2).Value = item.Division.Code;
                oraCmd.Parameters.Add("p_costcenter", OracleDbType.Varchar2).Value = item.Costcenter;
                oraCmd.Parameters.Add("p_wtype", OracleDbType.Varchar2).Value = item.WorkType;
                oraCmd.Parameters.Add("p_wsts", OracleDbType.Varchar2).Value = item.EmployeeType;
                oraCmd.Parameters.Add("p_posit", OracleDbType.Varchar2).Value = item.Position.Code;
                oraCmd.Parameters.Add("p_bus", OracleDbType.Varchar2).Value = item.Bus;
                oraCmd.Parameters.Add("p_stop", OracleDbType.Varchar2).Value = item.BusStop;
                oraCmd.Parameters.Add("p_bank", OracleDbType.Varchar2).Value = item.Bank;
                oraCmd.Parameters.Add("p_bankac", OracleDbType.Varchar2).Value = item.BankAccount;
                oraCmd.Parameters.Add("p_hospital", OracleDbType.Varchar2).Value = item.Hospital.Code;
                oraCmd.Parameters.Add("p_telephone", OracleDbType.Varchar2).Value = item.ExtensionNumber;
                oraCmd.Parameters.Add("p_mail", OracleDbType.Varchar2).Value = item.Email;
                oraCmd.Parameters.Add("p_grpl", OracleDbType.Varchar2).Value = item.WorkGroupLine;
                oraCmd.Parameters.Add("p_grpot", OracleDbType.Varchar2).Value = item.OtGroupLine;
                oraCmd.Parameters.Add("p_otype", OracleDbType.Varchar2).Value = item.OtType;
                oraCmd.Parameters.Add("p_pbdate", OracleDbType.Varchar2).Value = item.ProbationDate > new DateTime(1900,1,1) ? item.ProbationDate.ToString("dd/MMM/yyyy", new CultureInfo("EN-US")) : "";
            
                /****************************************************************************************/
                oraCmd.Parameters.Add("p_insur", OracleDbType.Decimal).Value = item.Insuran;
              
                oraCmd.Parameters.Add("p_donate", OracleDbType.Decimal).Value = item.Donate;
                oraCmd.Parameters.Add("p_interest", OracleDbType.Decimal).Value = item.Interest;
                oraCmd.Parameters.Add("p_insuno", OracleDbType.Varchar2).Value = item.InsuranNo;
                oraCmd.Parameters.Add("p_loanno", OracleDbType.Varchar2).Value = item.LoanNo;
                AddParameters(oraCmd, item.Provence);
                oraCmd.Parameters.Add("p_housing", OracleDbType.Decimal).Value = item.Housing;
                oraCmd.Parameters.Add("p_houseded", OracleDbType.Decimal).Value = item.Houseded;
                oraCmd.Parameters.Add("p_loan", OracleDbType.Decimal).Value = item.Loan;
                oraCmd.Parameters.Add("p_salary", OracleDbType.Decimal).Value = item.Salary;
                oraCmd.Parameters.Add("p_allow", OracleDbType.Decimal).Value = item.SkillAllawance;
                oraCmd.Parameters.Add("p_positallow", OracleDbType.Decimal).Value = item.PositAllawance;
                oraCmd.Parameters.Add("p_prfallow", OracleDbType.Decimal).Value = item.ProfesionalAllowance;
                oraCmd.Parameters.Add("p_dlrate", OracleDbType.Decimal).Value = item.Wedge;
                oraCmd.Parameters.Add("p_ltf", OracleDbType.Decimal).Value = item.Ltf;
                oraCmd.Parameters.Add("p_gsbloan", OracleDbType.Decimal).Value = item.GsbLoan;
                oraCmd.Parameters.Add("p_cooploan", OracleDbType.Decimal).Value = item.CoopLoan;
                AddParameters(oraCmd, item.Cooperative);
                oraCmd.Parameters.Add("p_p_rank", OracleDbType.Int16).Value = item.Rank;
                oraCmd.Parameters.Add("p_p_grade", OracleDbType.Int16).Value = item.Grad;  
                oraCmd.Parameters.Add("p_remark", OracleDbType.Varchar2).Value = item.Remark;
                oraCmd.Parameters.Add("p_handycap", OracleDbType.Decimal).Value = item.Handycap;
                oraCmd.Parameters.Add("p_company", OracleDbType.Varchar2).Value = item.Company;
                oraCmd.Parameters.Add("p_rs_remark", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.RsRemark);

                /**************************************************************************************/
                oraCmd.Parameters.Add("p_tposiname", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.Tposiname);
                oraCmd.Parameters.Add("p_tposi_dt", OracleDbType.Date).Value = item.Tposijoin;
                oraCmd.Parameters.Add("p_annualcal_dt", OracleDbType.Date).Value = item.AnnualcalDate;
                oraCmd.Parameters.Add("p_mobileallow", OracleDbType.Decimal).Value = item.MobileAllawance;
                oraCmd.Parameters.Add("p_gasolineallow", OracleDbType.Decimal).Value = item.GasolineAllowance;
                oraCmd.Parameters.Add("p_budgettype", OracleDbType.Varchar2).Value = item.BudgetType;
                oraCmd.Parameters.Add("p_workcenter", OracleDbType.Varchar2).Value = item.Workcenter;

                oraCmd.Parameters.Add("p_lineno", OracleDbType.Varchar2).Value = item.Lineno;
                oraCmd.Parameters.Add("p_mcno", OracleDbType.Varchar2).Value = item.Mcno;
                if (item.ContractExpDT > new DateTime(1900, 1, 1))
                {
                    oraCmd.Parameters.Add("p_contract_exp_dt", OracleDbType.Date).Value = item.ContractExpDT;
                }
                else
                {
                    oraCmd.Parameters.Add("p_contract_exp_dt", OracleDbType.Date).Value = new DateTime(1900, 1, 1);
                }
                
                oraCmd.Parameters.Add("p_talent", OracleDbType.Varchar2).Value = item.Talent;
                oraCmd.Parameters.Add("p_wcno", OracleDbType.Varchar2).Value = item.Workcenter;
                oraCmd.Parameters.Add("p_linecode", OracleDbType.Varchar2).Value = item.Lineno;

            }
            else if (obj is EmployeeInfo)
            {



            }
            else if (obj is PersonInfo)
            {

            }

        }

        public override object QueryForObject(DataRow row, Type t)
        {
            if (t == typeof(PersonInfo))
            {
                EmployeeInfo item = new EmployeeInfo();
                this.FillObject(row, item);
                return item;
            }

            else if (t == typeof(EmployeeInfo))
            {
                EmployeeInfo item = new EmployeeInfo();
                this.FillObject(row, item);
                return item;
            }
            else if (t == typeof(EmployeeDataInfo))
            {
                EmployeeDataInfo item = new EmployeeDataInfo();
                this.FillObject(row, item);
                return item;
            }
            else if (t == typeof(ProvidenceInfo))
            {
                ProvidenceInfo item = new ProvidenceInfo();
                try
                {
                    item.ProvidenceDate = Convert.ToDateTime(this.Parse(row, "pvdate"));
                }
                catch { }
                try
                {
                    item.ProvidenceNo = Convert.ToString(this.Parse(row, "pvno"));
                }
                catch { }
                try
                {
                    item.ProvidencePercent = Convert.ToDecimal(this.Parse(row, "pvperc"));
                }
                catch { }
                try
                {
                    item.ProvidencePercentCompany = Convert.ToDecimal(this.Parse(row, "pvperccompany"));
                }
                catch { }
                try
                {
                    item.ProvidenceTerminate = Convert.ToDateTime(this.Parse(row, "pvterm"));
                }
                catch { }
                return item;
            }

            else if (t== typeof(CooperativeInfo))
            {
                CooperativeInfo item = new CooperativeInfo();
                try
                {
                    item.CooDate = Convert.ToDateTime(this.Parse(row, "coop_date"));
                }
                catch { }
                try
                {
                    item.Amount = Convert.ToDecimal(this.Parse(row, "coop_amount"));
                }
                catch { }
                try
                {
                    item.Deduct = Convert.ToDecimal(this.Parse(row, "coop_deduct"));
                }
                catch { }
                try
                {
                    item.CooTerm = Convert.ToDateTime(this.Parse(row, "coop_term"));
                }
                catch { }
                return item;
            }
            else if (t == typeof(PositionInfo))
            {
                PositionInfo item = new PositionInfo();
                try
                {
                    item.Code = OraHelper.DecodeLanguage((string)this.Parse(row, "posi_cd"));
                }
                catch { }

                try
                {
                    if (item.Code == "")
                    {
                        item.Code = OraHelper.DecodeLanguage((string)this.Parse(row, "posit"));
                    }
                }
                catch { }

                try
                {
                    item.NameEng = OraHelper.DecodeLanguage((string)this.Parse(row, "posi_ename"));
                }
                catch { }
                try
                {
                    item.NameThai = OraHelper.DecodeLanguage((string)this.Parse(row, "posi_tname"));
                }
                catch { }
                return item;
            }
            else if (t == typeof(HospitalInfo))
            {
                HospitalInfo item = new HospitalInfo();
                try
                {
                    item.Code = OraHelper.DecodeLanguage((string)this.Parse(row, "hosp_cd"));
                }
                catch { }
                if (item.Code == "")
                {
                    try
                    {
                        item.Code = OraHelper.DecodeLanguage((string)this.Parse(row, "hospital"));
                    }
                    catch { }
                }

                try
                {
                    item.NameEng = OraHelper.DecodeLanguage((string)this.Parse(row, "hosp_ename"));
                }
                catch { }
                try
                {
                    item.NameThai = OraHelper.DecodeLanguage((string)this.Parse(row, "hosp_tname"));
                }
                catch { }
                return item;
            }
            else if (t == typeof(FamilyInfo))
            {
                FamilyInfo item = new FamilyInfo();
                item.NameInThai = new NameInfo();



                try
                {
                    item.EmpCode = Convert.ToString(this.Parse(row, "code"));
                }
                catch
                { }
                try
                {
                    item.IdNo = Convert.ToString(this.Parse(row, "id_no"));
                }
                catch
                { }
                try
                {
                    item.NameInThai.Title = OraHelper.DecodeLanguage((string)this.Parse(row, "pren"));
                }
                catch
                { }
                try
                {
                    item.NameInThai.Name = OraHelper.DecodeLanguage((string)this.Parse(row, "name"));
                }
                catch
                { }
                try
                {
                    item.NameInThai.Surname = OraHelper.DecodeLanguage((string)this.Parse(row, "surn"));
                }
                catch
                { }

                try
                {
                    item.Relation = Convert.ToString(this.Parse(row, "relation"));
                }
                catch
                { }
                try
                {
                    item.Birth = Convert.ToDateTime(this.Parse(row, "birth"));
                }
                catch
                { }
                try
                {
                    item.TaxDed = Convert.ToInt32(this.Parse(row, "taxdeduct"));
                }
                catch
                { }
                try
                {
                    item.CreateBy = Convert.ToString(this.Parse(row, "cr_by"));
                }
                catch { }
                try
                {
                    item.CreateDateTime = Convert.ToDateTime(this.Parse(row, "cr_dt"));
                }
                catch { }
                try
                {
                    item.LastUpdateBy = Convert.ToString(this.Parse(row, "upd_by"));
                }
                catch { }
                try
                {
                    item.LastUpdateDateTime = Convert.ToDateTime(this.Parse(row, "upd_dt"));
                }
                catch { }
                return item;


            }
            else if (t == typeof(WorkHistoryInfo))
            {
                WorkHistoryInfo item = new WorkHistoryInfo();
                try
                {
                    item.Address = OraHelper.DecodeLanguage((string)this.Parse(row, "address"));
                }
                catch
                { }
                try
                {
                    item.AddressInThai = OraHelper.DecodeLanguage((string)this.Parse(row, "taddress"));
                }
                catch
                { }
                try
                {
                    item.CompanyName = OraHelper.DecodeLanguage((string)this.Parse(row, "compname"));
                }
                catch
                { }
                try
                {
                    item.CompanyNameInThai = OraHelper.DecodeLanguage((string)this.Parse(row, "tcompname"));
                }
                catch
                { }
                try
                {
                    item.EmpCode = OraHelper.DecodeLanguage((string)this.Parse(row, "code"));
                }
                catch
                { }
                try
                {
                    item.ResignReason = OraHelper.DecodeLanguage((string)this.Parse(row, "reason"));
                }
                catch
                { }
                try
                {
                    item.LastPosition = OraHelper.DecodeLanguage((string)this.Parse(row, "lastposition"));
                }
                catch
                { }
                try
                {
                    item.WorkFrom = Convert.ToDateTime(this.Parse(row, "wfrom"));
                }
                catch
                { }
                try
                {
                    item.WorkTo = Convert.ToDateTime(this.Parse(row, "wto"));
                }
                catch
                { }
                return item;

            }
            else if (t == typeof(EmployeeCodeTransferInfo))
            {
                EmployeeCodeTransferInfo item = new EmployeeCodeTransferInfo();
                try
                {
                    item.NewCode = Convert.ToString(this.Parse(row, "n_code"));
                }
                catch
                { }
                try
                {
                    item.OldCode = Convert.ToString(this.Parse(row, "o_code"));
                }
                catch
                { }
                try
                {
                    item.TransferStatus = Convert.ToString(this.Parse(row, "status"));
                }
                catch
                { }
                try
                {
                    item.TransferDate = Convert.ToDateTime(this.Parse(row, "t_date"));
                }
                catch
                { }
                try
                {
                    item.CreateBy = Convert.ToString(this.Parse(row, "cr_by"));
                }
                catch { }
                try
                {
                    item.CreateDateTime = Convert.ToDateTime(this.Parse(row, "cr_dt"));
                }
                catch { }
                try
                {
                    item.LastUpdateBy = Convert.ToString(this.Parse(row, "upd_by"));
                }
                catch { }
                try
                {
                    item.LastUpdateDateTime = Convert.ToDateTime(this.Parse(row, "upd_dt"));
                }
                catch { }
                return item;

            }
            else if (t == typeof(int))
            {
                int item = 0;
                try
                {
                  item =  Convert.ToInt16(this.Parse(row, "issue"));
                }
                catch
                {}
                return item;
            }
            else
            {
                return null;
            }

        }

        #region IEmployeeDao Members

        public void FillObject(DataRow row, PersonInfo employee)
        {
            if (employee != null)
            {
                try
                {
                    employee.Code = OraHelper.DecodeLanguage((string)this.Parse(row, "code"));
                }
                catch { }
                try
                {
                    employee.NameInEng = new NameInfo();
                    employee.NameInEng.Title = OraHelper.DecodeLanguage((string)this.Parse(row, "pren"));
                    employee.NameInEng.Name = OraHelper.DecodeLanguage((string)this.Parse(row, "name"));
                    employee.NameInEng.Surname = OraHelper.DecodeLanguage((string)this.Parse(row, "surn"));
                   // employee.NameInEng.NickName = OraHelper.DecodeLanguage((string)this.Parse(row, "nickname"));
                }
                catch { }
                try
                {
                    employee.NameInThai = new NameInfo();
                    employee.NameInThai.Title = OraHelper.DecodeLanguage((string)this.Parse(row, "tpren"));
                    employee.NameInThai.Name = OraHelper.DecodeLanguage((string)this.Parse(row, "tname"));
                    employee.NameInThai.Surname = OraHelper.DecodeLanguage((string)this.Parse(row, "tsurn"));
                    employee.NameInThai.NickName = OraHelper.DecodeLanguage((string)this.Parse(row, "nickname"));

                }
                catch { }
                try
                {
                    employee.Gender = OraHelper.DecodeLanguage((string)this.Parse(row, "sex"));
                }
                catch { }
                try
                {
                    employee.GenderTH = OraHelper.DecodeLanguage((string)this.Parse(row, "tsex"));
                }
                catch { }
                try
                {
                    employee.BirthDate = Convert.ToDateTime(this.Parse(row, "birth"));
                }
                catch { employee.BirthDate = new DateTime(1900, 1, 1); }

                try
                {
                    employee.CitizenId = OraHelper.DecodeLanguage((string)this.Parse(row, "idno"));
                }
                catch { }
                try
                {
                    employee.TaxNumber = OraHelper.DecodeLanguage((string)this.Parse(row, "taxno"));

                }
                catch { }
                try
                {
                    employee.IdcardIssueDate = Convert.ToDateTime(this.Parse(row, "iddate"));

                }
                catch { }

                try
                {
                    EducationInfo edu = new EducationInfo();
                    try
                    {
                        edu.Degree = OraHelper.DecodeLanguage((string)this.Parse(row, "degree"));
                    }
                    catch { }
                    try
                    {
                        edu.DegreeType = OraHelper.DecodeLanguage((string)this.Parse(row, "dtype"));
                    }
                    catch { }
                    try
                    {
                        edu.MajorInEng = OraHelper.DecodeLanguage((string)this.Parse(row, "major"));
                    }
                    catch { }
                    try
                    {
                        edu.MajorInThai = OraHelper.DecodeLanguage((string)this.Parse(row, "tmajor"));
                    }
                    catch { }
                    try
                    {
                        edu.GraduateYear = OraHelper.DecodeLanguage((string)this.Parse(row, "schly"));
                    }
                    catch { }
                    try
                    {
                        edu.SchoolInEng = OraHelper.DecodeLanguage((string)this.Parse(row, "school"));
                    }
                    catch { }
                    try
                    {
                        edu.SchoolInThai = OraHelper.DecodeLanguage((string)this.Parse(row, "tschool"));
                    }
                    catch { }
                    employee.Education = new EducationInfo();
                    employee.Education = edu;
                }
                catch { }
                try
                {
                    AddressInfo adr = new AddressInfo();
                    try
                    {
                        adr.Address = OraHelper.DecodeLanguage((string)this.Parse(row, "ecaddr1"));

                    }
                    catch { }
                    try
                    {
                        adr.Subdistrict = OraHelper.DecodeLanguage((string)this.Parse(row, "ecaddr2"));
                    }
                    catch { }
                    try
                    {
                        adr.District = OraHelper.DecodeLanguage((string)this.Parse(row, "ecaddr3"));
                    }
                    catch { }
                    try
                    {
                        adr.Province = OraHelper.DecodeLanguage((string)this.Parse(row, "ecaddr4"));
                    }
                    catch { }
                    try
                    {
                        adr.Telephone = OraHelper.DecodeLanguage((string)this.Parse(row, "ectel"));
                    }
                    catch { }
                    employee.PresentAddressInEng = new AddressInfo();
                    employee.PresentAddressInEng = adr;

                }
                catch { }
                try
                {
                    AddressInfo adr = new AddressInfo();
                    try
                    {
                        adr.Address = OraHelper.DecodeLanguage((string)this.Parse(row, "tcaddr1"));

                    }
                    catch { }
                    try
                    {
                        adr.Subdistrict = OraHelper.DecodeLanguage((string)this.Parse(row, "tcaddr2"));
                    }
                    catch { }
                    try
                    {
                        adr.District = OraHelper.DecodeLanguage((string)this.Parse(row, "tcaddr3"));
                    }
                    catch { }
                    try
                    {
                        adr.Province = OraHelper.DecodeLanguage((string)this.Parse(row, "tcaddr4"));
                    }
                    catch { }
                    try
                    {
                        adr.Telephone = OraHelper.DecodeLanguage((string)this.Parse(row, "tctel"));
                    }
                    catch { }
                    employee.PresentAddressInThai = new AddressInfo();
                    employee.PresentAddressInThai = adr;

                }
                catch { }
                try
                {
                    AddressInfo adr = new AddressInfo();
                    try
                    {
                        adr.Address = OraHelper.DecodeLanguage((string)this.Parse(row, "ehaddr1"));

                    }
                    catch { }
                    try
                    {
                        adr.Subdistrict = OraHelper.DecodeLanguage((string)this.Parse(row, "ehaddr2"));
                    }
                    catch { }
                    try
                    {
                        adr.District = OraHelper.DecodeLanguage((string)this.Parse(row, "ehaddr3"));
                    }
                    catch { }
                    try
                    {
                        adr.Province = OraHelper.DecodeLanguage((string)this.Parse(row, "ehaddr4"));
                    }
                    catch { }
                    try
                    {
                        adr.Telephone = OraHelper.DecodeLanguage((string)this.Parse(row, "ehtel"));
                    }
                    catch { }
                    employee.HomeAddressInEng = new AddressInfo();
                    employee.HomeAddressInEng = adr;

                }
                catch { }
                try
                {
                    AddressInfo adr = new AddressInfo();
                    try
                    {
                        adr.Address = OraHelper.DecodeLanguage((string)this.Parse(row, "thaddr1"));

                    }
                    catch { }
                    try
                    {
                        adr.Subdistrict = OraHelper.DecodeLanguage((string)this.Parse(row, "thaddr2"));
                    }
                    catch { }
                    try
                    {
                        adr.District = OraHelper.DecodeLanguage((string)this.Parse(row, "thaddr3"));
                    }
                    catch { }
                    try
                    {
                        adr.Province = OraHelper.DecodeLanguage((string)this.Parse(row, "thaddr4"));
                    }
                    catch { }
                    try
                    {
                        adr.Telephone = OraHelper.DecodeLanguage((string)this.Parse(row, "thtel"));
                    }
                    catch { }
                    employee.HomeAddressInThai = new AddressInfo();
                    employee.HomeAddressInThai = adr;

                }
                catch { }
                try
                {
                    employee.MarryStatus = OraHelper.DecodeLanguage((string)this.Parse(row, "marry"));

                }
                catch { }
                try
                {
                    employee.MilitaryStatus = OraHelper.DecodeLanguage((string)this.Parse(row, "military"));

                }
                catch { }
                try
                {
                    employee.Religion = OraHelper.DecodeLanguage((string)this.Parse(row, "religion"));

                }
                catch { }
                try
                {
                    employee.Sonb = Convert.ToInt32(this.Parse(row, "sonb"));

                }
                catch { }
                try
                {
                    employee.Sons = Convert.ToInt32(this.Parse(row, "sons"));

                }
                catch { }
                try
                {
                    employee.RefPerson1 = OraHelper.DecodeLanguage((string)this.Parse(row, "refperson1"));

                }
                catch { }
                try
                {
                    employee.RefContact1 = OraHelper.DecodeLanguage((string)this.Parse(row, "refcontact1"));

                }
                catch { }
                try
                {
                    employee.RefPerson2 = OraHelper.DecodeLanguage((string)this.Parse(row, "refperson2"));

                }
                catch { }
                try
                {
                    employee.RefContact2 = OraHelper.DecodeLanguage((string)this.Parse(row, "refcontact2"));

                }
                catch { }
                try
                {
                    employee.CreateBy = Convert.ToString(this.Parse(row, "cr_by"));
                }
                catch { }
                try
                {
                    employee.CreateDateTime = Convert.ToDateTime(this.Parse(row, "cr_dt"));
                }
                catch { }
                try
                {
                    employee.LastUpdateBy = Convert.ToString(this.Parse(row, "upd_by"));
                }
                catch { }
                try
                {
                    employee.LastUpdateDateTime = Convert.ToDateTime(this.Parse(row, "upd_dt"));
                }
                catch { }


            }

        }

        public void FillObject(DataRow row, EmployeeDataInfo employee)
        {
            if (employee != null)
            {
                FillObject(row, (EmployeeInfo)employee);

                try
                {
                    employee.PositAllawance = Convert.ToDecimal(this.Parse(row, "positallow"));

                }
                catch { }
                try
                {
                    employee.SkillAllawance = Convert.ToDecimal(this.Parse(row, "allow"));

                }
                catch { }
                try
                {
                    employee.ProfesionalAllowance = Convert.ToDecimal(this.Parse(row, "PRFALLOW"));

                }
                catch { }
                try
                {
                    employee.Donate = Convert.ToDecimal(this.Parse(row, "donate"));

                }
                catch { }
                try
                {
                    employee.Grad = Convert.ToInt32(this.Parse(row, "p_grade"));

                }
                catch { }
                try
                {
                    employee.Houseded = Convert.ToDecimal(this.Parse(row, "houseded"));

                }
                catch { }
                try
                {
                    employee.Housing = Convert.ToDecimal(this.Parse(row, "housing"));

                }
                catch { }
                try
                {
                    employee.Insuran = Convert.ToDecimal(this.Parse(row, "insur"));

                }
                catch { }
                try
                {
                    employee.InsuranNo = Convert.ToString(this.Parse(row, "insuno"));

                }
                catch { }
                try
                {
                    employee.Interest = Convert.ToDecimal(this.Parse(row, "interest"));

                }
                catch { }

                try
                {
                    employee.Handycap = Convert.ToDecimal(this.Parse(row, "handycap"));

                }
                catch { }
                try
                {
                    employee.Loan = Convert.ToDecimal(this.Parse(row, "loan"));

                }
                catch { }
                try
                {
                    employee.LoanNo = Convert.ToString(this.Parse(row, "loanno"));

                }
                catch { }
                try
                {
                    employee.Ltf = Convert.ToDecimal(this.Parse(row, "ltf"));

                }
                catch { }
                try
                {
                    employee.GsbLoan = Convert.ToDecimal(this.Parse(row, "gsbloan"));

                }
                catch { }
                try
                {
                    employee.CoopLoan = Convert.ToDecimal(this.Parse(row, "cooploan"));

                }
                catch { }
                try
                {
                    employee.Rank = Convert.ToInt32(this.Parse(row, "p_rank"));

                }
                catch { }
                try
                {
                    employee.Salary = Convert.ToDecimal(this.Parse(row, "salary"));

                }
                catch { }
                try
                {
                    employee.Wedge = Convert.ToDecimal(this.Parse(row, "DLRATE"));

                }
                catch { }
                try
                {
                    employee.MobileAllawance = Convert.ToDecimal(this.Parse(row, "ATBN03"));

                }
                catch { }

                try
                {
                    employee.GasolineAllowance = Convert.ToDecimal(this.Parse(row, "ATBN04"));

                }
                catch { }

                employee.Provence = (ProvidenceInfo)QueryForObject(row, typeof(ProvidenceInfo));
                employee.Cooperative = (CooperativeInfo)QueryForObject(row, typeof(CooperativeInfo));

                try
                {
                    employee.Talent = Convert.ToDecimal(this.Parse(row, "talent"));
                }
                catch { }

                
            }
        }
        
        public void FillObject(DataRow row, EmployeeInfo employee)
        {
            if (employee != null)
            {
                FillObject(row, (PersonInfo)employee);

                /*
                try
                {
                    employee.Code = OraHelper.DecodeLanguage((string)this.Parse(row, "code"));
                }
                catch { }
                try
                {
                    employee.NameInEng = new NameInfo();
                    employee.NameInEng.Title = OraHelper.DecodeLanguage((string)this.Parse(row, "pren"));
                    employee.NameInEng.Name = OraHelper.DecodeLanguage((string)this.Parse(row, "name"));
                    employee.NameInEng.Surname = OraHelper.DecodeLanguage((string)this.Parse(row, "surn"));
                }
                catch { }
                try
                {
                    employee.NameInThai = new NameInfo();
                    employee.NameInThai.Title = OraHelper.DecodeLanguage((string)this.Parse(row, "tpren"));
                    employee.NameInThai.Name = OraHelper.DecodeLanguage((string)this.Parse(row, "tname"));
                    employee.NameInThai.Surname = OraHelper.DecodeLanguage((string)this.Parse(row, "tsurn"));
                }
                catch { }
                try
                {
                    employee.Gender = OraHelper.DecodeLanguage((string)this.Parse(row, "sex"));
                }
                catch { }
                try
                {
                    employee.BirthDate = Convert.ToDateTime(this.Parse(row, "birth"));
                }
                catch { employee.BirthDate = new DateTime(1900, 1, 1); }
                              try
                {
                    employee.CitizenId = OraHelper.DecodeLanguage((string)this.Parse(row, "idno"));
                }
                catch { }*/
                try
                {
                    employee.JoinDate = Convert.ToDateTime(this.Parse(row, "join"));
                }
                catch { }
                try
                {
                    employee.ResignDate = Convert.ToDateTime(this.Parse(row, "resign"));
                }
                catch { }
                try
                {
                    employee.ResignReason = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "rsreason")));
                }
                catch
                {


                }
                try
                {
                    employee.ResignType = Convert.ToString(this.Parse(row, "rstype"));
                }
                catch { }
                try
                {
                    employee.RsRemark =OraHelper.DecodeLanguage( Convert.ToString(this.Parse(row, "rs_remark")));
                }
                catch { }


                try
                {
                    employee.Division = (DivisionInfo)divisionDao.QueryForObject(row, typeof(DivisionInfo));
                }
                catch { }
                try
                {
                    employee.Position = (PositionInfo)this.QueryForObject(row, typeof(PositionInfo));
                }
                catch { }
                try
                {
                    employee.Hospital = (HospitalInfo)this.QueryForObject(row, typeof(HospitalInfo));
                }
                catch { }
                try
                {
                    employee.Company = OraHelper.DecodeLanguage((string)this.Parse(row, "company"));
                }
                catch { }
                try
                {
                    employee.WorkGroupLine = OraHelper.DecodeLanguage((string)this.Parse(row, "grpl"));
                }
                catch { }
                try
                {
                    employee.OtGroupLine = OraHelper.DecodeLanguage((string)this.Parse(row, "grpot"));
                }
                catch { }
                try
                {
                    employee.Costcenter = OraHelper.DecodeLanguage((string)this.Parse(row, "costcenter"));
                }
                catch { }
                try
                {
                    employee.OtType = OraHelper.DecodeLanguage((string)this.Parse(row, "ottype"));
                }
                catch { }
                try
                {
                    employee.WorkType = OraHelper.DecodeLanguage((string)this.Parse(row, "wtype"));
                }
                catch { }
                try
                {
                    employee.EmployeeType = OraHelper.DecodeLanguage((string)this.Parse(row, "wsts"));
                }
                catch { }
                try
                {
                    employee.Bank = OraHelper.DecodeLanguage((string)this.Parse(row, "bank"));
                }
                catch { }
                try
                {
                    employee.BankAccount = OraHelper.DecodeLanguage((string)this.Parse(row, "bankac"));
                }
                catch { }
                try
                {
                    employee.Bus = OraHelper.DecodeLanguage((string)this.Parse(row, "bus"));
                }
                catch { }
                try
                {
                    employee.BusWay = OraHelper.DecodeLanguage((string)this.Parse(row, "busway"));
                }
                catch { }
                try
                {
                    employee.BusStop = OraHelper.DecodeLanguage((string)this.Parse(row, "stop"));
                }
                catch { }
                try
                {
                    employee.BusStopName = OraHelper.DecodeLanguage((string)this.Parse(row, "busstop"));
                }
                catch { }
                try
                {
                    employee.Email = OraHelper.DecodeLanguage((string)this.Parse(row, "mail"));
                }
                catch { }
                try
                {
                    employee.ExtensionNumber = OraHelper.DecodeLanguage((string)this.Parse(row, "telephone"));
                }
                catch { }
                try
                {
                    employee.ProbationDate = Convert.ToDateTime(this.Parse(row, "pbdate"));
                }
                catch { }


                //================================================
                //      Edited By Nueng 08/06/2015
                //================================================
                try
                {
                    employee.Tposiname = OraHelper.DecodeLanguage((string)this.Parse(row, "tposiname"));
                }
                catch { }

                try
                {
                    employee.Tposijoin = Convert.ToDateTime(this.Parse(row, "tposijoin_dt"));
                }
                catch { }

                try
                {
                    employee.AnnualcalDate = Convert.ToDateTime(this.Parse(row, "annualcal_dt"));
                }
                catch { }

                try
                {
                    employee.BudgetType = OraHelper.DecodeLanguage((string)this.Parse(row, "gb02"));
                }
                catch { }

                try
                {
                    employee.Workcenter = OraHelper.DecodeLanguage((string)this.Parse(row, "gb03"));
                }
                catch { }

                try {
                    employee.Lineno = OraHelper.DecodeLanguage((string)this.Parse(row, "gb04"));
                }
                catch { }

                try {
                    employee.Mcno = OraHelper.DecodeLanguage((string)this.Parse(row, "gb05"));
                }
                catch { }

                //================================================
                //      Edited By Nueng 17/11/2021
                //================================================
                try
                {
                    employee.ContractExpDT = Convert.ToDateTime(this.Parse(row, "contract_exp_dt"));
                }
                catch { employee.ContractExpDT = new DateTime(1900, 1, 1); }
            }

        }
        
        private void DecodeDataSet(ref DataSet _dts, string _dttable)
        {
            DataTable dt = _dts.Tables[_dttable];
            for (int j = 0; j < dt.Rows.Count; j++)
            {


                for (int i = 0; i < dt.Rows[j].ItemArray.Length; i++)
                {
                    try
                    {
                        if (_dts.Tables[_dttable].Columns[i].DataType == typeof(string))
                        {
                            string temp;
                            temp = OraHelper.DecodeLanguage(dt.Rows[j][i].ToString());
                            _dts.Tables[_dttable].Rows[j][i] = temp;
                        }
                    }
                    catch
                    {


                    }
                }
            }

        }

        public EmployeeInfo Select(string employeeCode)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = employeeCode;

            return (EmployeeInfo)OraHelper.ExecuteQuery(this, cmd, typeof(EmployeeInfo));
        }
        
        public DataSet SelectAllEmp()
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECTALL, CommandType.StoredProcedure);
            DataSet emp = OraHelper.GetDataSet(this.Transaction, cmd, "EMPM");
            DecodeDataSet(ref emp, "EMPM");
            return emp;
        }
        
        public ArrayList SelectCurEmp()
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECTCurrentEmp, CommandType.StoredProcedure);

            // cmd.Parameters.Add(PARAM_DATE, OracleDbType.Date).Value = curDate;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(EmployeeInfo));
        }

        public ArrayList SelectCurEmpByPosition(string posit)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECTCurrentEmpByPosit, CommandType.StoredProcedure);

            cmd.Parameters.Add("p_posits", OracleDbType.Varchar2).Value = posit;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(EmployeeInfo));
        }

        public ArrayList SelectCurEmpByDVCD(string dvcd,string grpot)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECTCurrentEmpByDVCD, CommandType.StoredProcedure);

            cmd.Parameters.Add("p_dvcd", OracleDbType.Varchar2).Value = dvcd;
            cmd.Parameters.Add("p_grpot", OracleDbType.Varchar2).Value = grpot;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(EmployeeInfo));
        }

        public ArrayList SelectCurEmpListByDVCD(string dvcd)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECTCurrentEmpListByDVCD, CommandType.StoredProcedure);

            cmd.Parameters.Add("p_dvcd", OracleDbType.Varchar2).Value = dvcd;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(EmployeeInfo));
        }
        
        public ArrayList SelectCurEmpByBusWay(string busway, string stop)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECTCurrentEmpByBusWay , CommandType.StoredProcedure);

            cmd.Parameters.Add("p_bus", OracleDbType.Varchar2).Value = busway;
            cmd.Parameters.Add("p_stop", OracleDbType.Varchar2).Value = stop;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(EmployeeInfo));
        }
        
        public DataSet SelectCurEmpByBusWayDataset(string busway, string stop)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECTCurrentEmpByBusWay, CommandType.StoredProcedure);

            cmd.Parameters.Add("p_bus", OracleDbType.Varchar2).Value = busway;
            cmd.Parameters.Add("p_stop", OracleDbType.Varchar2).Value = stop;
            DataSet emp = OraHelper.GetDataSet(this.Transaction, cmd, "EMPM");
            DecodeDataSet(ref emp, "EMPM");

            return emp;
        }

        public ArrayList GetEmployeeFamily(string empCode)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECTFamily, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = empCode;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(FamilyInfo));
        }



        public void SaveEmployeeFamily(FamilyInfo emfm)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STOREFamily, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_ACTION, OracleDbType.Varchar2).Value = "ADD";
            this.AddParameters(cmd, emfm);
            cmd.Parameters.Add(PARAM_USER, OracleDbType.Varchar2).Value = emfm.CreateBy;

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void UpdateEmployeeFamily(FamilyInfo emfm)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STOREFamily, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_ACTION, OracleDbType.Varchar2).Value = "UPDATE";
            this.AddParameters(cmd, emfm);
            cmd.Parameters.Add(PARAM_USER, OracleDbType.Varchar2).Value = emfm.LastUpdateBy;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void DeleteloyeeFamily(FamilyInfo emfm)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_DELETEFamily, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = emfm.EmpCode;
            cmd.Parameters.Add(PARAM_ID, OracleDbType.Varchar2).Value = emfm.IdNo;
            cmd.Parameters.Add(PARAM_RELATION, OracleDbType.Varchar2).Value = emfm.Relation;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }


        public void UpdateEmployeeBusWay(string code, string bus, string stop)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_UPDATEEmployeeBusway, CommandType.StoredProcedure);
            cmd.Parameters.Add("p_code", OracleDbType.Varchar2).Value = code;
            cmd.Parameters.Add("p_bus", OracleDbType.Varchar2).Value = bus;
            cmd.Parameters.Add("p_stop", OracleDbType.Varchar2).Value = stop;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }



        public EmployeeDataInfo GetEmployeeDataInfo(string empCode)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECTEmployeeData, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = empCode;

            return (EmployeeDataInfo)OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(EmployeeDataInfo));
        }

        public void SaveEmployeeInfo(EmployeeDataInfo empInfo)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STOREEmployee, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_ACTION, OracleDbType.Varchar2).Value = "ADD";
            this.AddParameters(cmd, empInfo);
            cmd.Parameters.Add(PARAM_USER, OracleDbType.Varchar2).Value = empInfo.CreateBy;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void UpdateEmployeeInfo(EmployeeDataInfo empInfo)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STOREEmployee, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_ACTION, OracleDbType.Varchar2).Value = "UPDATE";
            this.AddParameters(cmd, empInfo);
            cmd.Parameters.Add(PARAM_USER, OracleDbType.Varchar2).Value = empInfo.LastUpdateBy;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void DeleteEmployeeInfo(string empId)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_DELETEEmployee, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = empId;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }




        public ArrayList GetEmployeeWorkHistory(string empCode)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECTWorkHistory, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = empCode;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(WorkHistoryInfo));
        }

        public void SaveEmployeeWorkHistory(WorkHistoryInfo emfm)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STOREWorkHistory, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_ACTION, OracleDbType.Varchar2).Value = "ADD";
            this.AddParameters(cmd, emfm);
            cmd.Parameters.Add(PARAM_USER, OracleDbType.Varchar2).Value = "";

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void UpdateEmployeeWorkHistory(WorkHistoryInfo emfm)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STOREWorkHistory, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_ACTION, OracleDbType.Varchar2).Value = "UPDATE";
            this.AddParameters(cmd, emfm);
            cmd.Parameters.Add(PARAM_USER, OracleDbType.Varchar2).Value = "";
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void DeleteloyeeWorkHistory(WorkHistoryInfo emfm)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_DELETEWorkHistory, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = emfm.EmpCode;
            cmd.Parameters.Add(PARAM_WORKFROM, OracleDbType.Date).Value = emfm.WorkFrom;
            cmd.Parameters.Add(PARAM_WORKTO, OracleDbType.Date).Value = emfm.WorkTo;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }



        public void EmployeeResignation(string empCode, DateTime rsDate, string rsType, string rsReason,string rsRemark,string by)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_EmployeeResigning, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = empCode;
            cmd.Parameters.Add("p_resign", OracleDbType.Varchar2).Value = rsDate != DateTime.MinValue ? rsDate.ToString("dd/MMM/yyyy", new CultureInfo("EN-US")) : "";
            cmd.Parameters.Add("p_rsType", OracleDbType.Varchar2).Value = rsType;
            cmd.Parameters.Add("p_rsreason", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(rsReason);
            cmd.Parameters.Add("p_rs_remark", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(rsRemark);
            cmd.Parameters.Add(PARAM_USER, OracleDbType.Varchar2).Value = by;
  
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }



        public void EmployeeSetEmail(string code, string email, string extension)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_EmployeeSetEmail, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = code;
            cmd.Parameters.Add("p_telephone", OracleDbType.Varchar2).Value = extension;
            cmd.Parameters.Add("p_mail", OracleDbType.Varchar2).Value = email;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }




        public ArrayList GetEmployeeCodeTransfer(string empCode)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECTEmpCodeTransferByCode, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = empCode;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(EmployeeCodeTransferInfo));

        }

        public ArrayList GetEmployeeCodeTransfer(string empCode, DateTime transDate, string transStatus)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECTEmpCodeTransfer, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = empCode;
            cmd.Parameters.Add(PARAM_TransferDate, OracleDbType.Date).Value = transDate;
            cmd.Parameters.Add(PARAM_TransferStatus, OracleDbType.Varchar2).Value = transStatus;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(EmployeeCodeTransferInfo));
        }




        public EmployeeCodeTransferInfo GetUnqEmpCodeTransfer(string oCode, string newCode)
        {

            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECTUnqEmpTransfer, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_OldCode, OracleDbType.Varchar2).Value = oCode;
            cmd.Parameters.Add(PARAM_NewCode, OracleDbType.Date).Value = newCode;


            return (EmployeeCodeTransferInfo)OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(EmployeeCodeTransferInfo));
        }


        public void SaveEmployeeCodeTransfer(EmployeeCodeTransferInfo empInfo)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STOREEmpCodeTransfer, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_ACTION, OracleDbType.Varchar2).Value = "ADD";
            cmd.Parameters.Add(PARAM_OldCode, OracleDbType.Varchar2).Value = empInfo.OldCode;
            cmd.Parameters.Add(PARAM_NewCode, OracleDbType.Varchar2).Value = empInfo.NewCode;
            cmd.Parameters.Add(PARAM_TransferDate, OracleDbType.Date).Value = empInfo.TransferDate;
            cmd.Parameters.Add(PARAM_TransferStatus, OracleDbType.Varchar2).Value = empInfo.TransferStatus;
            cmd.Parameters.Add(PARAM_USER, OracleDbType.Varchar2).Value = empInfo.CreateBy;

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void UpdateEmployeeCodeTransfer(EmployeeCodeTransferInfo empInfo)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STOREEmpCodeTransfer, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_ACTION, OracleDbType.Varchar2).Value = "UPDATE";
            cmd.Parameters.Add(PARAM_OldCode, OracleDbType.Varchar2).Value = empInfo.OldCode;
            cmd.Parameters.Add(PARAM_NewCode, OracleDbType.Varchar2).Value = empInfo.NewCode;
            cmd.Parameters.Add(PARAM_TransferDate, OracleDbType.Date).Value = empInfo.TransferDate;
            cmd.Parameters.Add(PARAM_TransferStatus, OracleDbType.Varchar2).Value = empInfo.TransferStatus;
            cmd.Parameters.Add(PARAM_USER, OracleDbType.Varchar2).Value = empInfo.LastUpdateBy;

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void DeleteEmployeeCodeTransfer(string oldCode, string newCode)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_DELETEEmpCodeTransfer, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_OldCode, OracleDbType.Varchar2).Value = oldCode;
            cmd.Parameters.Add(PARAM_NewCode, OracleDbType.Varchar2).Value = newCode;


            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }



        public DataSet GetRecordHistoryDataSet(string empCode)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECTRecordHistory, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = empCode;

            DataSet emp = OraHelper.GetDataSet(this.Transaction, cmd, "RecHist");
            DecodeDataSet(ref emp, "RecHist");
            return emp;

        }



        public DataSet GenerateEmp()
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_GEN_EmployeeData, CommandType.StoredProcedure);
            DataSet emp = OraHelper.GetDataSet(this.Transaction, cmd, "EMPM");
            DecodeDataSet(ref emp, "EMPM");
            return emp;
        }


        public DataSet GetManpowerByGrpot(string pdate,string posit,string grpot )
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_GETManpowerByGrpot, CommandType.StoredProcedure);
            cmd.Parameters.Add("p_pdate", OracleDbType.Varchar2).Value = pdate;
            cmd.Parameters.Add("p_posit", OracleDbType.Varchar2).Value = posit;
            cmd.Parameters.Add("p_grpot", OracleDbType.Varchar2).Value = grpot;

            DataSet emp = OraHelper.GetDataSet(this.Transaction, cmd, "EMPM");
           
            return emp;
        }


        public ArrayList GetResignEmp(DateTime from, DateTime to)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_GEN_EmployeeResignData, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_DateFrom, OracleDbType.Date).Value = from;
            cmd.Parameters.Add(PARAM_DateTo, OracleDbType.Date).Value = to;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(EmployeeInfo));
        }

        public int GetEmpCardIssue(string empCode)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_GETEmpCardIssue  , CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = empCode;    

            return (int) OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(int));
        }
        public int GenEmpCardIssue(string empCode)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_GENEmpCardIssue, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = empCode;

            return (int)OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(int));
        }

        public int GenEmpCardIssue(string empCode,string name)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_GENEmpCardIssue1, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = empCode;
            cmd.Parameters.Add(PARAM_NAME, OracleDbType.Varchar2).Value = name;

            return (int)OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(int));
        }



        public DataSet GetManpowerForBC(DateTime pdate)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_GETManpowerForBC, CommandType.StoredProcedure);
            cmd.Parameters.Add("p_pdate", OracleDbType.Date).Value = pdate;
            DataSet emp = OraHelper.GetDataSet(this.Transaction, cmd, "EMPM");
            //DecodeDataSet(ref emp, "EMPM");
            return emp;
        }

        public DataSet GetManpowerForBC1(DateTime pdate, string pDvcd)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_GETManpowerForBC1, CommandType.StoredProcedure);
            cmd.Parameters.Add("p_pdate", OracleDbType.Date).Value = pdate;
            cmd.Parameters.Add("p_dvcd", OracleDbType.Varchar2).Value = pDvcd;
            DataSet emp = OraHelper.GetDataSet(this.Transaction, cmd, "EMPM");
            //DecodeDataSet(ref emp, "EMPM");
            return emp;
        }

        #endregion
    }
}

