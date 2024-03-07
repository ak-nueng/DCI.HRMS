using System;
using System.Collections.Generic;
using System.Text;
using PCUOnline.Dao;
using DCI.HRMS.Model.Attendance;
using System.Collections;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using PCUOnline.Dao.Ora;

namespace DCI.HRMS.Persistence.Oracle
{
    public class OraLeaveReqDao : DaoBase, ILeaveRequestDao
    {
        private const string SP_SELECT_Levae = "pkg_hr_emplv.sp_getleave";
        private const string SP_SELECT_LeaveByDate = "pkg_hr_emplv.sp_getleavebydate";
        private const string SP_STORE_LeaveRequest = "pkg_hr_emplv.sp_storeleaverequest";
        private const string SP_DELETE_LeaveRequest = "pkg_hr_emplv.sp_deleteleaverequest";
        private const string SP_SELECT_LeaveReason = "pkg_hr_emplv.sp_selectreason";

        private const string PARAM_LeaveType = "p_lv_type";
        private const string PARAM_LeaveFrom = "p_lv_date_from";
        private const string PARAM_LaveTo = "p_lv_date_to";



        private const string PARAM_Action = "p_action";
        private const string PARAM_DocId = "p_docid";
        private const string PARAM_EmpCode = "p_code";
        private const string PARAM_CDate = "p_cdate";
        private const string PARAM_Type = "p_type";
        private const string PARAM_From = "p_lvfrom";
        private const string PARAM_To = "p_lvto";
        private const string PARAM_Time = "p_times";
        private const string PARAM_Total = "p_total";
        private const string PARAM_Status = "p_salsts";
        private const string PARAM_LeaveNo = "p_lvno";
        private const string PARAM_Reason = "p_reason";
        private const string PARAM_User = "p_by";


        public OraLeaveReqDao(DaoManager daoManager)
            : base(daoManager)
        {
        }

        public override object QueryForObject(System.Data.DataRow row, Type t)
        {
            if (t == typeof(EmployeeLealeRequestInfo))
            {
                EmployeeLealeRequestInfo item = new EmployeeLealeRequestInfo();
                ObjCommon iform = new ObjCommon();
                item.Inform = iform.QueryForObject(row);
                try
                {
                    item.DocId = Convert.ToString(this.Parse(row, "doc_id"));
                }
                catch { }
                try
                {
                    item.EmpCode = Convert.ToString(this.Parse(row, "code"));
                }
                catch { }

                try
                {
                    item.LvDate = Convert.ToDateTime(this.Parse(row, "cdate"));
                }
                catch { }

                try
                {
                    item.LvType = Convert.ToString(this.Parse(row, "type"));
                }
                catch { }

                try
                {
                    item.LvFrom = Convert.ToString(this.Parse(row, "lvfrom"));
                }
                catch { }

                try
                {
                    item.LvTo = Convert.ToString(this.Parse(row, "lvto"));
                }
                catch { }

                try
                {
                    item.TotalHour = Convert.ToString(this.Parse(row, "times"));
                }
                catch { }

                try
                {
                    item.TotalMinute = Convert.ToInt32(this.Parse(row, "total"));
                }
                catch { }

                try
                {
                    item.PayStatus = Convert.ToString(this.Parse(row, "salsts"));
                }
                catch { }

                try
                {
                    //item.Reason =  Convert.ToString(this.Parse(row, "reason"));
                    item.Reason = DecodeLanguage(Convert.ToString(this.Parse(row, "reason")));
                    
                    
                }
                catch { }

                try
                {
                    item.LvNo = Convert.ToInt32(this.Parse(row, "lvno"));
                }
                catch { }

                return item;
            }


            return null;
        }

        public static string DecodeLanguage(string data)
        {
            StringBuilder output = new StringBuilder();

            if (data != null && data.Length > 0)
            {
                foreach (char c in data)
                {
                    int ascii = (int)c;

                    if (ascii > 160)
                        ascii += 3424;

                    output.Append((char)ascii);
                }
            }

            return output.ToString();
        }
        public static string EncodeLanguage(string data)
        {
            StringBuilder output = new StringBuilder();
            if (data != null && data.Length > 0)
            {
                foreach (char c in data)
                {
                    int ascii = (int)c;

                    if (ascii >= 160)
                        ascii -= 3424;

                    output.Append((char)ascii);
                }
            }
            return output.ToString();
        }

        public override void AddParameters(System.Data.IDbCommand cmd, object obj)
        {
            OracleCommand oraCmd = (OracleCommand)cmd;
            if (obj is EmployeeLealeRequestInfo)
            {
                EmployeeLealeRequestInfo item = (EmployeeLealeRequestInfo)obj;
                oraCmd.Parameters.Add(PARAM_DocId, OracleDbType.Varchar2).Value = item.DocId;
                oraCmd.Parameters.Add(PARAM_EmpCode, OracleDbType.Varchar2).Value = item.EmpCode;
                oraCmd.Parameters.Add(PARAM_CDate, OracleDbType.Date).Value = item.LvDate;
                oraCmd.Parameters.Add(PARAM_Type, OracleDbType.Varchar2).Value = item.LvType;
                oraCmd.Parameters.Add(PARAM_From, OracleDbType.Varchar2).Value = item.LvFrom;

                oraCmd.Parameters.Add(PARAM_To, OracleDbType.Varchar2).Value = item.LvTo;
                oraCmd.Parameters.Add(PARAM_Time, OracleDbType.Varchar2).Value = item.TotalHour;
                oraCmd.Parameters.Add(PARAM_Total, OracleDbType.Int32).Value = item.TotalMinute;
                oraCmd.Parameters.Add(PARAM_Status, OracleDbType.Varchar2).Value = item.PayStatus;
                oraCmd.Parameters.Add(PARAM_LeaveNo, OracleDbType.Varchar2).Value = item.LvNo;
                oraCmd.Parameters.Add(PARAM_Reason, OracleDbType.Varchar2).Value = item.Reason;
            }
        }
        #region ILeaveRequestDao Members


        public ArrayList GetLeaveReq(string empCode, string lvType)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_Levae, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_EmpCode, OracleDbType.Varchar2).Value = empCode;
            cmd.Parameters.Add(PARAM_LeaveType, OracleDbType.Varchar2).Value = lvType;
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(EmployeeLealeRequestInfo));
        }


        public DataSet GetLeaveReq(string empCode)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_Levae, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_EmpCode, OracleDbType.Varchar2).Value = empCode;
            cmd.Parameters.Add(PARAM_LeaveType, OracleDbType.Varchar2).Value = "%";
            return OraHelper.GetDataSet(this.Transaction, cmd, "LeaveRecord");
        }


        public ArrayList GetLeaveReq(string empCode, DateTime lvDateFrom, DateTime lvDateTo, string lvType)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_LeaveByDate, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_EmpCode, OracleDbType.Varchar2).Value = empCode + "%";
            cmd.Parameters.Add(PARAM_LeaveType, OracleDbType.Varchar2).Value = lvType + "%";
            cmd.Parameters.Add(PARAM_LeaveFrom, OracleDbType.Date).Value = lvDateFrom;
            cmd.Parameters.Add(PARAM_LaveTo, OracleDbType.Date).Value = lvDateTo;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(EmployeeLealeRequestInfo));
        }
        public DataSet GetLeaveReqDataSet(string empCode, DateTime lvDateFrom, DateTime lvDateTo, string lvType)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_LeaveByDate, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_EmpCode, OracleDbType.Varchar2).Value = empCode + "%";
            cmd.Parameters.Add(PARAM_LeaveType, OracleDbType.Varchar2).Value = lvType + "%";
            cmd.Parameters.Add(PARAM_LeaveFrom, OracleDbType.Date).Value = lvDateFrom;
            cmd.Parameters.Add(PARAM_LaveTo, OracleDbType.Date).Value = lvDateTo;

            return OraHelper.GetDataSet( this.Transaction, cmd, "Leave");
       
        }
        public EmployeeLealeRequestInfo GetLeaveReq(string code, DateTime rqDate, string lvType)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_LeaveByDate, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_EmpCode, OracleDbType.Varchar2).Value = code;
            cmd.Parameters.Add(PARAM_LeaveType, OracleDbType.Varchar2).Value = lvType;
            cmd.Parameters.Add(PARAM_LeaveFrom, OracleDbType.Date).Value = rqDate;
            cmd.Parameters.Add(PARAM_LaveTo, OracleDbType.Date).Value = rqDate;

            return (EmployeeLealeRequestInfo)OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(EmployeeLealeRequestInfo));

        }

        public void SaveLeaveReq(EmployeeLealeRequestInfo lvrq)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_LeaveRequest, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Action, OracleDbType.Varchar2).Value = "ADD";
            AddParameters(cmd, lvrq);
            cmd.Parameters.Add(PARAM_User, OracleDbType.Varchar2).Value = lvrq.CreateBy;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);

        }

        public void UpdateLeaveReq(EmployeeLealeRequestInfo lvrq)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_LeaveRequest, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Action, OracleDbType.Varchar2).Value = "UPDATE";
            AddParameters(cmd, lvrq);
            cmd.Parameters.Add(PARAM_User, OracleDbType.Varchar2).Value = lvrq.LastUpdateBy;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void DeleteLeaveReq(EmployeeLealeRequestInfo lvrq)
        {

            OracleCommand cmd = OraHelper.CreateCommand(SP_DELETE_LeaveRequest, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_DocId, OracleDbType.Varchar2).Value = lvrq.DocId;
            cmd.Parameters.Add(PARAM_EmpCode, OracleDbType.Varchar2).Value = lvrq.EmpCode;
            cmd.Parameters.Add(PARAM_CDate, OracleDbType.Date).Value = lvrq.LvDate;
            cmd.Parameters.Add(PARAM_LeaveType, OracleDbType.Varchar2).Value = lvrq.LvType;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);

        }


        public ArrayList GetLeavReason()
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_LeaveReason, CommandType.StoredProcedure);

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(EmployeeLealeRequestInfo));
        }
  




        #endregion
    }
}
