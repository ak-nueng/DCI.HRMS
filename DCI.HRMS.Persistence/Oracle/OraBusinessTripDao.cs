using System;
using System.Collections.Generic;
using System.Text;
using PCUOnline.Dao;
using System.Collections;
using DCI.HRMS.Model.Attendance;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using PCUOnline.Dao.Ora;

namespace DCI.HRMS.Persistence.Oracle
{
    public class OraBusinessTripDao : DaoBase, IBusinessTripDao
    {
        private const string SP_SELECT_Bussiness = "pkg_hr_TNRQ.sp_select";
        private const string SP_SELECT_BussinessByDate = "pkg_hr_TNRQ.sp_selectbydate";
        private const string SP_DELETE_Bussiness = "pkg_hr_tnrq.sp_delete";
        private const string SP_STORE_Bussiness = "pkg_hr_tnrq.sp_store";

        private const string PARA_Action = "p_action";
        private const string PARA_EMPCODE = "p_code";
        private const string PARA_Fdate = "p_fdate";
        private const string PARA_Tdate = "p_tdate";
        private const string PARA_Ftime = "p_ftime";
        private const string PARA_tTime = "p_ttime";
        private const string PARA_Note = "p_note";
        private const string PARA_User = "p_by";


        public OraBusinessTripDao(DaoManager daoManager)
            : base(daoManager)
        {
        }


        public override object QueryForObject(DataRow row, Type t)
        {
            if (t == typeof(BusinesstripInfo))
            {

                BusinesstripInfo item = new BusinesstripInfo();
                try
                {
                    item.EmpCode = Convert.ToString(this.Parse(row, "code"));
                }
                catch
                {
                }
                try
                {
                    item.FDate = Convert.ToDateTime(this.Parse(row, "fdate"));
                }
                catch
                {
                }
                try
                {
                    item.TDate = Convert.ToDateTime(this.Parse(row, "tdate"));
                }
                catch
                {
                }
                try
                {
                    item.Note = Convert.ToString(this.Parse(row, "note"));
                }
                catch
                {
                }
                try
                {
                    item.TFrom = Convert.ToString(this.Parse(row, "tfrom"));
                }
                catch
                {
                }
                try
                {
                    item.TTo = Convert.ToString(this.Parse(row, "tto"));
                }
                catch
                {
                }
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

        public override void AddParameters(IDbCommand cmd, object obj)
        {

        }



        #region IBusinessTripDao Members

        public ArrayList GetBusinessTrip(string empCode, DateTime fDate, DateTime tDate)
        {

            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_Bussiness, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_EMPCODE, OracleDbType.Varchar2).Value = empCode;
            cmd.Parameters.Add(PARA_Fdate, OracleDbType.Date).Value = fDate;
            cmd.Parameters.Add(PARA_Tdate, OracleDbType.Date).Value = tDate;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(BusinesstripInfo));
        }

        public void SaveBusinessTrip(BusinesstripInfo rq)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_Bussiness, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_Action, OracleDbType.Varchar2).Value = "ADD";
            cmd.Parameters.Add(PARA_EMPCODE, OracleDbType.Varchar2).Value = rq.EmpCode;
            cmd.Parameters.Add(PARA_Fdate, OracleDbType.Date).Value = rq.FDate;
            cmd.Parameters.Add(PARA_Tdate, OracleDbType.Date).Value = rq.TDate;
            cmd.Parameters.Add(PARA_Ftime, OracleDbType.Varchar2).Value = rq.TFrom;
            cmd.Parameters.Add(PARA_tTime, OracleDbType.Varchar2).Value = rq.TTo;
            cmd.Parameters.Add(PARA_Note, OracleDbType.Varchar2).Value = rq.Note;
            cmd.Parameters.Add(PARA_User, OracleDbType.Varchar2).Value = rq.CreateBy;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void UpdateBusinessTrip(BusinesstripInfo rq)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_Bussiness, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_Action, OracleDbType.Varchar2).Value = "UPDATE";
            cmd.Parameters.Add(PARA_EMPCODE, OracleDbType.Varchar2).Value = rq.EmpCode;
            cmd.Parameters.Add(PARA_Fdate, OracleDbType.Date).Value = rq.FDate;
            cmd.Parameters.Add(PARA_Tdate, OracleDbType.Date).Value = rq.TDate;
            cmd.Parameters.Add(PARA_Ftime, OracleDbType.Varchar2).Value = rq.TFrom;
            cmd.Parameters.Add(PARA_tTime, OracleDbType.Varchar2).Value = rq.TTo;
            cmd.Parameters.Add(PARA_Note, OracleDbType.Varchar2).Value = rq.Note;
            cmd.Parameters.Add(PARA_User, OracleDbType.Varchar2).Value = rq.LastUpdateBy;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void DeteteBusinessTrip(BusinesstripInfo rq)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_DELETE_Bussiness, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARA_EMPCODE, OracleDbType.Varchar2).Value = rq.EmpCode;
            cmd.Parameters.Add(PARA_Fdate, OracleDbType.Date).Value = rq.FDate;
            cmd.Parameters.Add(PARA_Tdate, OracleDbType.Date).Value = rq.TDate;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }




        public BusinesstripInfo GetBusinessTrip(string empCode, DateTime fDate)
        {

            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_BussinessByDate, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_EMPCODE, OracleDbType.Varchar2).Value = empCode;
            cmd.Parameters.Add(PARA_Fdate, OracleDbType.Date).Value = fDate;


            return (BusinesstripInfo)OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(BusinesstripInfo));
        }

        #endregion
    }
}
