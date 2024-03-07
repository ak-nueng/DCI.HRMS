using System;
using System.Collections.Generic;
using System.Text;
using PCUOnline.Dao;
using System.Collections;
using DCI.HRMS.Model.Welfare;
using Oracle.ManagedDataAccess.Client;
using PCUOnline.Dao.Ora;
using System.Data;
namespace DCI.HRMS.Persistence.Oracle
{
    public class OraMedicalDao : DaoBase, IMedicalDao
    {
        private const string SP_SELECT_MEDICAL = "pkg_hr_med.sp_selectrate";
        private const string SP_SELECT_MEDHOSPITAL = "pkg_hr_med.sp_selecthospital";
        private const string SP_SELECT_MEDSYMPTOM = "pkg_hr_med.sp_selectsymptom";
        private const string SP_SELECT_MEDDISTRICT = "pkg_hr_med.sp_selectdistrict";
        private const string SP_SELECT_MEDPROVINCE = "pkg_hr_med.sp_selectprovince";
        private const string SP_SELECT_MEDBYCODE = "pkg_hr_med.sp_selectmedbycode";
        private const string SP_SELECT_MEDBYDOCID = "pkg_hr_med.sp_selectmedbyid";
        private const string SP_STORE_MED = "pkg_hr_med.sp_storemed_1";
        private const string SP_DELETE_MED = "pkg_hr_med.sp_deletemed";


        private const string PARAM_MEDFROM = "p_medatefrom";
        private const string PARAM_MEDTO = "p_medateto";
        private const string PARAM_DOCIDTo = "p_docid2";

        private const string PARAM_Action = " p_action";
        private const string PARAM_DOCID = "p_docid";
        private const string PARAM_MedDate = " p_medate";
        private const string PARAM_EMCODE = "p_code";
        private const string PARAM_RqDate = " p_rqdate";
        private const string PARAM_Relation = "p_relation";
        private const string PARAM_SympTom = "p_symptom";
        private const string PARAM_Amount = "p_amount";
        private const string PARAM_PatientType = "p_pttype";
        private const string PARAM_PatientName = "p_ptname";
        private const string PARAM_Hospital = "p_hospital";
        private const string PARAM_District = "p_district";
        private const string PARAM_province = "p_province";
        private const string PARAM_User = "p_by";

        public OraMedicalDao(DaoManager daoManager)
            : base(daoManager)
        {
        }
        public override object QueryForObject(DataRow row, Type t)
        {
            if (t == typeof(MedicalAllowanceInfo))
            {
                MedicalAllowanceInfo item = new MedicalAllowanceInfo();
                ObjCommon iform = new ObjCommon();
                item.Inform = iform.QueryForObject(row);
                try
                {
                    item.DocNo = Convert.ToString(this.Parse(row, "docid"));
                }
                catch
                { }

                try
                {
                    item.TrDate = Convert.ToDateTime(this.Parse(row, "csdate"));
                }
                catch
                { }

                try
                {
                    item.EmCode = Convert.ToString(this.Parse(row, "code"));
                }
                catch
                { }


                try
                {
                    item.RqDate = Convert.ToDateTime(this.Parse(row, "rqdate"));
                }
                catch
                { }
                try
                {
                    item.Symptom = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "symptom")));
                }
                catch
                { }
                try
                {
                    item.Amount = Convert.ToDouble(this.Parse(row, "amount"));
                }
                catch
                { }
                try
                {
                    item.PatienType = Convert.ToString(this.Parse(row, "PATIENTTYPE"));
                }
                catch
                { }
                try
                {
                    item.RelationType = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "relation")));
                }
                catch
                { }
                try
                {
                    item.PatienName = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "smperson")));
                }
                catch
                { }
                try
                {
                    item.Hospital = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "hospitol")));
                }
                catch
                { }
                try
                {
                    item.District = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "district")));
                }
                catch
                { }
                try
                {
                    item.Province = OraHelper.DecodeLanguage(Convert.ToString(this.Parse(row, "province")));
                }
                catch
                { }
                return item;



            }
            return null;
        }

        public override void AddParameters(IDbCommand cmd, object obj)
        {
            OracleCommand oraCmd = (OracleCommand)cmd;
            if (obj is MedicalAllowanceInfo)
            {
                MedicalAllowanceInfo item = (MedicalAllowanceInfo)obj;

                oraCmd.Parameters.Add(PARAM_DOCID, OracleDbType.Varchar2).Value = item.DocNo;
                oraCmd.Parameters.Add(PARAM_MedDate, OracleDbType.Date).Value = item.TrDate;
                oraCmd.Parameters.Add(PARAM_EMCODE, OracleDbType.Varchar2).Value = item.EmCode;
                oraCmd.Parameters.Add(PARAM_RqDate, OracleDbType.Date).Value = item.RqDate;
                oraCmd.Parameters.Add(PARAM_Relation, OracleDbType.Varchar2).Value = item.RelationType;
                oraCmd.Parameters.Add(PARAM_SympTom, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.Symptom);
                oraCmd.Parameters.Add(PARAM_Amount, item.Amount);
                oraCmd.Parameters.Add(PARAM_PatientType, OracleDbType.Varchar2).Value = item.PatienType;
                oraCmd.Parameters.Add(PARAM_PatientName, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.PatienName);
                oraCmd.Parameters.Add(PARAM_Hospital, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.Hospital);
                oraCmd.Parameters.Add(PARAM_District, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.District);
                oraCmd.Parameters.Add(PARAM_province, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.Province);

            }

        }

        #region IMedicalDao Members

        public ArrayList GetMedical(DateTime datefrom, DateTime dateto)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public ArrayList GetMedical(string code, DateTime from, DateTime to)
        {

            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_MEDBYCODE, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_EMCODE, OracleDbType.Varchar2).Value = code + "%";
            cmd.Parameters.Add(PARAM_MEDFROM, OracleDbType.Date).Value = from;
            cmd.Parameters.Add(PARAM_MEDTO, OracleDbType.Date).Value = to;
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(MedicalAllowanceInfo));
        }

        public ArrayList GetMedical(string docidfrom, string docidto)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_MEDBYDOCID, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_DOCID, OracleDbType.Varchar2).Value = docidfrom;
            cmd.Parameters.Add(PARAM_DOCIDTo, OracleDbType.Varchar2).Value = docidto;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(MedicalAllowanceInfo));

        }

        public ArrayList GetMedical(string code, string docid, DateTime trdatefrom, DateTime trdateto, string patientype)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        public MedicalAllowanceInfo GetMedical(string docid)
        {

            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_MEDBYDOCID, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_DOCID, OracleDbType.Varchar2).Value = docid;
            cmd.Parameters.Add(PARAM_DOCIDTo, OracleDbType.Varchar2).Value = docid;


            return (MedicalAllowanceInfo)OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(MedicalAllowanceInfo));
        }

        public void AddMedical(MedicalAllowanceInfo med)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_MED, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Action, OracleDbType.Varchar2).Value = "ADD";
            this.AddParameters(cmd, med);
            cmd.Parameters.Add(PARAM_User, OracleDbType.Varchar2).Value = med.CreateBy;
            // cmd.Parameters.Add(PARAM_DOCID, OracleDbType.Varchar2).Value = med.DocNo;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);

        }

        public void UpdateMedical(MedicalAllowanceInfo med)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_MED, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Action, OracleDbType.Varchar2).Value = "UPDATE";
            this.AddParameters(cmd, med);
            cmd.Parameters.Add(PARAM_User, OracleDbType.Varchar2).Value = med.CreateBy;
            //  cmd.Parameters.Add(PARAM_DOCID, OracleDbType.Varchar2).Value = med.DocNo;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void DeleteMedical(MedicalAllowanceInfo med)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_DELETE_MED, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_DOCID, OracleDbType.Varchar2).Value = med.DocNo;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }



        public ArrayList GetMedHospital()
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_MEDHOSPITAL, CommandType.StoredProcedure);

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(MedicalAllowanceInfo));
        }

        public ArrayList GetMedSymptom()
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_MEDSYMPTOM, CommandType.StoredProcedure);

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(MedicalAllowanceInfo));

        }
        public ArrayList GetMedDistrict()
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_MEDDISTRICT, CommandType.StoredProcedure);

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(MedicalAllowanceInfo));
        }
        public ArrayList GetMedProvince()
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_MEDPROVINCE, CommandType.StoredProcedure);

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(MedicalAllowanceInfo));

        }


        #endregion
    }
}
