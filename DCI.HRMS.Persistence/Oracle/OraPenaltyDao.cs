using System;
using System.Collections.Generic;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using DCI.HRMS.Model;
using PCUOnline.Dao;
using PCUOnline.Dao.Ora;
using DCI.HRMS.Model.Attendance;
using System.Collections;
using System.Data;
namespace DCI.HRMS.Persistence.Oracle
{
    public class OraPenaltyDao : DaoBase, IPenaltyDao
    {
        private const string SP_SELECT_Penalty = "pkg_hr_pen.sp_get";
        private const string SP_SELECT_PENALTY = "pkg_hr_pen.sp_select";
        private const string SP_SELECT_PenaltyByCode = "pkg_hr_pen.sp_getPenbycode";
        private const string SP_STORE_Penalty = "pkg_hr_pen.sp_store";
        private const string SP_DELETE_Penalty = "pkg_hr_pen.sp_delete";

        private const string PARAM_Action = "p_action";
        private const string PARAM_EMPCODE = "p_code";

        private const string PARAM_By = "p_by";
        private const string PARAM_PenId = "p_PEN_ID";
        private const string PARAM_WDesc = "p_W_DESC";
        private const string PARAM_WFrom = "p_W_FROM";
        private const string PARAM_WTo = "p_W_TO";
        private const string PARAM_WTotal = "p_W_TOTAL";
        private const string PARAM_PType = "p_P_TYPE";
        private const string PARAM_PDate = "p_P_DATE";
        private const string PARAM_PFrom = "p_P_FROM";
        private const string PARAM_PTo = "p_P_TO";
        private const string PARAM_PTotal = "p_P_TOTAL";

        private const string PARAM_TYPE = "p_type";
        private const string PARAM_Pdate = "p_pdate";
        private const string PARAM_Tdate = "p_tdate";
        private const string PARAM_Note = "p_note";

        public OraPenaltyDao(DaoManager daoManager)
            : base(daoManager)
        {

        }



        public override object QueryForObject(System.Data.DataRow row, Type t)
        {
            if (t == typeof(PenaltyInfo))
            {
                 
   
   
                PenaltyInfo item = new PenaltyInfo();
                    try
                {
                    item.PenaltyId = Convert.ToString(this.Parse(row, "PEN_ID"));

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
                    item.PenaltyType = Convert.ToString(this.Parse(row, "P_TYPE"));

                }
                catch
                { }
                            try
                {
                    item.PenaltyDate = Convert.ToDateTime(this.Parse(row, "P_DATE"));

                }
                catch
                { }
                try
                {
                    item.PenaltyFrom = Convert.ToDateTime(this.Parse(row, "P_FROM"));

                }
                catch
                { }
                try
                {
                    item.PenaltyTo = Convert.ToDateTime(this.Parse(row, "P_TO"));

                }
                catch
                { }
                         try
                {
                    item.PenaltyTotal = Convert.ToInt16(this.Parse(row, "P_TOTAL"));

                }
                catch
                { }
                try
                {
                    item.WDescription = OraHelper.DecodeLanguage( Convert.ToString(this.Parse(row, "W_DESC")));

                }
                catch
                { }
                                try
                {
                    item.WFrom = Convert.ToDateTime(this.Parse(row, "W_FROM"));

                }
                catch
                { }
                                                try
                {
                    item.WTo = Convert.ToDateTime(this.Parse(row, "W_TO"));

                }
                catch
                { }
                                                           try
                {
                    item.WTotal = Convert.ToInt16(this.Parse(row, "W_TOTAL"));

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

        }

        #region IPenaltyDao Members

        public ArrayList GetPenalty(string code)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_PenaltyByCode, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_EMPCODE, OracleDbType.Varchar2).Value = code;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(PenaltyInfo));
        }

        public ArrayList GetPenalty(string code, DateTime pdate, DateTime tdate)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_Penalty, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_EMPCODE, OracleDbType.Varchar2).Value = code;
            cmd.Parameters.Add(PARAM_TYPE, OracleDbType.Varchar2).Value = "%";
            cmd.Parameters.Add(PARAM_Pdate, OracleDbType.Date).Value = pdate;
            cmd.Parameters.Add(PARAM_Tdate, OracleDbType.Date).Value = tdate;
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(PenaltyInfo));
        }
        public ArrayList SelectPenalty(string code, string pentype, DateTime pdate, DateTime tdate)
        {


            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_PENALTY, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_EMPCODE, OracleDbType.Varchar2).Value = code;
            cmd.Parameters.Add(PARAM_TYPE, OracleDbType.Varchar2).Value =  pentype;
            cmd.Parameters.Add(PARAM_Pdate, OracleDbType.Date).Value = pdate;
            cmd.Parameters.Add(PARAM_Tdate, OracleDbType.Date).Value = tdate;
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(PenaltyInfo));


        }

        public ArrayList GetPenalty(DateTime pdate, DateTime tdate)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_Penalty, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_EMPCODE, OracleDbType.Varchar2).Value = "%";
            cmd.Parameters.Add(PARAM_TYPE, OracleDbType.Varchar2).Value = "%";
            cmd.Parameters.Add(PARAM_Pdate, OracleDbType.Date).Value = pdate;
            cmd.Parameters.Add(PARAM_Tdate, OracleDbType.Date).Value = tdate;
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(PenaltyInfo));
        }



        public void SavePenalty(PenaltyInfo pen)
        {  
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_Penalty, CommandType.StoredProcedure);
            cmd.Parameters.Add("p_action", OracleDbType.Varchar2).Value = "ADD";
            cmd.Parameters.Add("p_pen_id", OracleDbType.Varchar2).Value = pen.PenaltyId;
            cmd.Parameters.Add("p_code", OracleDbType.Varchar2).Value = pen.EmpCode;
            cmd.Parameters.Add("p_w_desc", OracleDbType.Varchar2).Value =OraHelper.EncodeLanguage( pen.WDescription);
            cmd.Parameters.Add("p_w_from", OracleDbType.Date).Value = pen.WFrom;
            cmd.Parameters.Add("p_w_to", OracleDbType.Date).Value = pen.WTo;
            cmd.Parameters.Add("p_w_total", OracleDbType.Int32).Value = pen.WTotal;
            cmd.Parameters.Add("p_p_type", OracleDbType.Varchar2).Value = pen.PenaltyType;
            cmd.Parameters.Add("p_p_date", OracleDbType.Date).Value = pen.PenaltyDate;
            cmd.Parameters.Add("p_p_from", OracleDbType.Date).Value = pen.PenaltyFrom;
            cmd.Parameters.Add("p_p_to", OracleDbType.Date).Value = pen.PenaltyTo;
            cmd.Parameters.Add("p_p_total", OracleDbType.Int32).Value = pen.PenaltyTotal;
            cmd.Parameters.Add("p_by", OracleDbType.Varchar2).Value = pen.CreateBy;

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void UpdatePenalty(PenaltyInfo pen)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_Penalty, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Action, OracleDbType.Varchar2).Value = "UPDATE";
            cmd.Parameters.Add(PARAM_PenId, OracleDbType.Varchar2).Value = pen.PenaltyId;
            cmd.Parameters.Add(PARAM_EMPCODE, OracleDbType.Varchar2).Value = pen.EmpCode;
            cmd.Parameters.Add(PARAM_WDesc, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage( pen.WDescription);
            cmd.Parameters.Add(PARAM_WFrom, OracleDbType.Date).Value = pen.WFrom;
            cmd.Parameters.Add(PARAM_WTo, OracleDbType.Date).Value = pen.WTo;
            cmd.Parameters.Add(PARAM_WTotal, OracleDbType.Int16).Value = pen.WTotal;
            cmd.Parameters.Add(PARAM_PType, OracleDbType.Varchar2).Value = pen.PenaltyType;
            cmd.Parameters.Add(PARAM_PDate, OracleDbType.Date).Value = pen.PenaltyDate;
            cmd.Parameters.Add(PARAM_PFrom, OracleDbType.Date).Value = pen.PenaltyFrom;
            cmd.Parameters.Add(PARAM_PTo, OracleDbType.Date).Value = pen.PenaltyTo;
            cmd.Parameters.Add(PARAM_PTotal, OracleDbType.Int16).Value = pen.PenaltyTotal;
            cmd.Parameters.Add(PARAM_By, OracleDbType.Varchar2).Value = pen.LastUpdateBy;


            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void Delete(PenaltyInfo pen)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_DELETE_Penalty, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_PenId, OracleDbType.Varchar2).Value = pen.PenaltyId;
  

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        #endregion
    }
}
