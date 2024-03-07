using System;
using System.Collections.Generic;
using DCI.HRMS.Model.Allowance;
using System.Text;
using PCUOnline.Dao;
using System.Collections;
using Oracle.ManagedDataAccess.Client;
using PCUOnline.Dao.Ora;
using System.Data;

namespace DCI.HRMS.Persistence.Oracle
{
    public class OraSkillAllowanceDao : DaoBase, ISkillAllowanceDao
    {
        private const string SP_SelectSkillAllow = "pkg_hr_skillallow.sp_selectbycode";
        private const string SP_SelectSkillAllowUnq = "pkg_hr_skillallow.sp_selectbycodeUnq";
        private const string SP_SelectMasterByType = "pkg_hr_skillallow.sp_selectmasterbytype";
        private const string SP_SelectMaster = "pkg_hr_skillallow.sp_selectmaster ";
        private const string SP_SelectMasterLevel = "pkg_hr_skillallow.sp_selectmasterlevel";
        private const string SP_StoreSkillAllow = "pkg_hr_skillallow.sp_store";
        private const string SP_DeleteSkillAllow = "pkg_hr_skillallow.sp_delete";
          private const string SP_SelectCertByCode = "pkg_hr_skillallow.sp_selectcertbycode";
          private const string SP_SelectCertByCodeUnq = "pkg_hr_skillallow.sp_selectcertbycodeUnq";

            private const string SP_StoreCert = "pkg_hr_skillallow.sp_storecert";
            private const string SP_DeleteCert = "pkg_hr_skillallow.sp_deletecert";
        private const string PARAM_ACT = "p_action";
        private const string PARAM_RacId = "p_rcid";
        private const string PARAM_CODE = "p_code";
        private const string PARAM_Month = "p_month";
        private const string PARAM_Type = "p_ctype";
        private const string PARAM_Level = "p_clevel";
        private const string PARAM_Remark = "p_remark";
        private const string PARAM_User = "p_by";

 
       private const string PARAM_CertDate ="p_cdate";
       private const string PARAM_CertExpire = "p_cexpire";
  




        public OraSkillAllowanceDao(DaoManager daoManager)
            : base(daoManager)
        {

        }



        public override object QueryForObject(System.Data.DataRow row, Type t)
        {
            if (t == typeof(EmpSkillAllowanceInfo))
            {
                EmpSkillAllowanceInfo item = new EmpSkillAllowanceInfo();
                try
                {
                    item.RecordId = Convert.ToString(this.Parse(row, "rc_id"));
                }
                catch
                { }
                try
                {
                    item.EmpCode = Convert.ToString(this.Parse(row, "code"));
                }
                catch
                { }
                try
                {
                    item.Month = Convert.ToDateTime(this.Parse(row, "month"));
                }
                catch
                { }
                try
                {
                    item.CertType = Convert.ToString(this.Parse(row, "ctype"));
                }
                catch
                { }
                try
                {
                    item.CertLevel = Convert.ToInt32(this.Parse(row, "clevel"));
                }
                catch
                { }
                try
                {
                    item.Remark = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "remark")));
                }
                catch
                { }
                try
                {
                    item.CertName = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "cname")));
                }
                catch
                { }
                try
                {
                    item.CertCost = Convert.ToDecimal(this.Parse(row, "ccost"));
                }
                catch
                { }
                try
                {
                    item.NextTest = Convert.ToDateTime(this.Parse(row, "nexttest"));
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
            else if (t == typeof(CertificateInfo))
            {
                CertificateInfo item = new CertificateInfo();
          
                try
                {
                    item.CerType = Convert.ToString(this.Parse(row, "TYPE"));
                }
                catch
                { }
                try
                {
                    item.CerName =OraHelper.DecodeLanguage( Convert.ToString(this.Parse(row, "CNAME")));
                }
                catch
                { }


                try
                {
                    item.CerCost = Convert.ToDecimal(this.Parse(row, "ccost"));
                }
                catch
                { }
                try
                {
                    item.Level = Convert.ToInt32(this.Parse(row, "clevel"));
                }
                catch
                { }
                try
                {
                    item.Remark = OraHelper.DecodeLanguage( Convert.ToString(this.Parse(row, "cremark")));
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
            else if (t == typeof(EmpCertInfo)) 
            {
                EmpCertInfo item = new EmpCertInfo();
                try
                {
                    item.RecordId = Convert.ToString(this.Parse(row, "rc_id"));
                }
                catch
                { }
                try
                {
                    item.EmpCode = Convert.ToString(this.Parse(row, "code"));
                }
                catch
                { }

                try
                {
                    item.CerType = Convert.ToString(this.Parse(row, "CTYPE"));
                }
                catch
                { }
                try
                {
                    item.CerName =OraHelper.DecodeLanguage(  Convert.ToString(this.Parse(row, "CNAME")));
                }
                catch
                { }

                try
                {
                    item.CertDate = Convert.ToDateTime(this.Parse(row, "cdate"));
                }
                catch
                { }
                try
                {
                    item.ExpireDate = Convert.ToDateTime(this.Parse(row, "cexpire"));
                }
                catch
                { }
                try
                {
                    item.Level = Convert.ToInt32(this.Parse(row, "clevel"));
                }
                catch
                { }
                try
                {
                    item.Remark =OraHelper.DecodeLanguage(  Convert.ToString(this.Parse(row, "remark")));
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
            return null;
        }

        public override void AddParameters(System.Data.IDbCommand cmd, object obj)
        {
            throw new NotImplementedException();
        }

        #region ISkillAllowanceDao Members

        public ArrayList GetSkillByCode(string empCode, string month)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SelectSkillAllow, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = empCode;
            cmd.Parameters.Add(PARAM_Month, OracleDbType.Varchar2).Value = month;
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(EmpSkillAllowanceInfo));

        }
        

        public EmpSkillAllowanceInfo GetSkillByCode(string empCode, DateTime month, string cerType, int cerLevel)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SelectSkillAllowUnq, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = empCode;
            cmd.Parameters.Add(PARAM_Month, OracleDbType.Date).Value = month;
            cmd.Parameters.Add(PARAM_Type, OracleDbType.Varchar2).Value =cerType;
            cmd.Parameters.Add(PARAM_Level, OracleDbType.Int16).Value = cerLevel;
            return (EmpSkillAllowanceInfo)OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(EmpSkillAllowanceInfo));
        }
        public void SaveSkillAllowance(EmpSkillAllowanceInfo empSkl)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_StoreSkillAllow, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_ACT, OracleDbType.Varchar2).Value = "ADD";
            cmd.Parameters.Add(PARAM_RacId, OracleDbType.Varchar2).Value = empSkl.RecordId;
            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = empSkl.EmpCode;
            cmd.Parameters.Add(PARAM_Month, OracleDbType.Date).Value = empSkl.Month;
            cmd.Parameters.Add(PARAM_Type, OracleDbType.Varchar2).Value = empSkl.CertType;
            cmd.Parameters.Add(PARAM_Level, OracleDbType.Int16).Value = empSkl.CertLevel;
            cmd.Parameters.Add(PARAM_Remark, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage( empSkl.Remark);
            cmd.Parameters.Add(PARAM_User, OracleDbType.Varchar2).Value = empSkl.CreateBy;

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);

        }

        public void DeleteSkillAllow(string rcId)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_DeleteSkillAllow, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_RacId, OracleDbType.Varchar2).Value = rcId;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }




        public CertificateInfo GetCerType(string type,int level)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SelectMasterByType, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = type;
            cmd.Parameters.Add(PARAM_Level, OracleDbType.Int16).Value =level;
            return (CertificateInfo)OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(CertificateInfo));

        }


        public ArrayList GetCertLevel(string type)
        {
     
            OracleCommand cmd = OraHelper.CreateCommand(SP_SelectMasterLevel, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = type;
  
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(CertificateInfo));

        
        }


        public ArrayList GetAllType()
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SelectMaster, CommandType.StoredProcedure);
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(CertificateInfo));

        }




        public void SaveCerType(CertificateInfo sklMstr)
        {
            throw new NotImplementedException();
        }

        public void UpdateCerType(CertificateInfo sklMstr)
        {
            throw new NotImplementedException();
        }

        public void DeleteCerType(string type, int level)
        {
            throw new NotImplementedException();
        }




        public ArrayList GetCertificateByCode(string empCode)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SelectCertByCode, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = empCode;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(EmpCertInfo));
        }

        public void SaveEmpCertificate(EmpCertInfo empCert)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_StoreCert, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_ACT, OracleDbType.Varchar2).Value = "ADD";
            cmd.Parameters.Add(PARAM_RacId, OracleDbType.Varchar2).Value = empCert.RecordId;
            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = empCert.EmpCode;
            cmd.Parameters.Add(PARAM_Type, OracleDbType.Varchar2).Value = empCert.CerType;
            cmd.Parameters.Add(PARAM_Level, OracleDbType.Int16).Value = empCert.Level;
            cmd.Parameters.Add(PARAM_Remark, OracleDbType.Varchar2).Value =OraHelper.EncodeLanguage( empCert.Remark);
            cmd.Parameters.Add(PARAM_CertDate, OracleDbType.Date).Value = empCert.CertDate;
            cmd.Parameters.Add(PARAM_CertExpire, OracleDbType.Date).Value = empCert.ExpireDate;
            cmd.Parameters.Add(PARAM_User, OracleDbType.Varchar2).Value = empCert.CreateBy;

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);

        }
        public void UpdateEmpCertificate(EmpCertInfo empCert)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_StoreCert, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_ACT, OracleDbType.Varchar2).Value = "UPDATE";
            cmd.Parameters.Add(PARAM_RacId, OracleDbType.Varchar2).Value = empCert.RecordId;
            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = empCert.EmpCode;
            cmd.Parameters.Add(PARAM_Type, OracleDbType.Varchar2).Value = empCert.CerType;
            cmd.Parameters.Add(PARAM_Level, OracleDbType.Int16).Value = empCert.Level;
            cmd.Parameters.Add(PARAM_Remark, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage( empCert.Remark);
            cmd.Parameters.Add(PARAM_CertDate, OracleDbType.Date).Value = empCert.CertDate;
            cmd.Parameters.Add(PARAM_CertExpire, OracleDbType.Date).Value = empCert.ExpireDate;
            cmd.Parameters.Add(PARAM_User, OracleDbType.Varchar2).Value = empCert.LastUpdateBy;

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);

        }

        public void DeleteEmpCertificate(string rcId)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_DeleteCert, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_RacId, OracleDbType.Varchar2).Value = rcId;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }


        public EmpCertInfo GetCertificateByCode(string empCode, string cerType, int cerLevel)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SelectCertByCodeUnq, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_CODE, OracleDbType.Varchar2).Value = empCode;
            cmd.Parameters.Add(PARAM_Type, OracleDbType.Varchar2).Value = cerType;
            cmd.Parameters.Add(PARAM_Level, OracleDbType.Int16).Value = cerLevel;
            return (EmpCertInfo)OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(EmpCertInfo));
        }

     


        #endregion
    }

}
