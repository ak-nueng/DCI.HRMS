using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using DCI.HRMS.Model;
using PCUOnline.Dao;
using PCUOnline.Dao.Ora;
using DCI.HRMS.Model.Attendance;
using DCI.HRMS.Model.Organize;
using Oracle.ManagedDataAccess.Client;


namespace DCI.HRMS.Persistence.Oracle
{
    public class OraAttendanceDao : DaoBase , IAttendanceDao
    {
        private const string SP_SUM_LEAVE_RECORD = "";
        private const string SP_SELECT_BY_CRITERIA = "pkg_hr_empattd.sp_get_lvrec_bycriteria";
        private const string SP_SELECT_BY_ABSENT_ALERT_RECORDS = "pkg_hr_empattd.sp_get_alrtabsrec";

        private const string PARAM_LEAVE_TYPE = "p_lv_type";
        private const string PARAM_DIV_CD = "p_dv_cd";
        private const string PARAM_FROM_DT = "p_from_dt";
        private const string PARAM_TO_DT = "p_to_dt";
        private const string PARAM_STATUS = "p_rs_type";

        private OraEmployeeDao employeeDao;

        public OraAttendanceDao(DaoManager daoManager)
            : base(daoManager)
        {
            employeeDao = new OraEmployeeDao(daoManager);
        }

        public override void AddParameters(IDbCommand cmd, object obj)
        {
            
        }

        public override object QueryForObject(DataRow row, Type t)
        {
            if(t == typeof(EmployeeAbsentAlertInfo))
            {
                EmployeeAbsentAlertInfo item = new EmployeeAbsentAlertInfo();
                employeeDao.FillObject(row, item);

                try
                {
                    item.TotalAbsent = Convert.ToInt32(this.Parse(row,"total_abs"));
                }
                catch { }
                try
                {
                    item.LastAbsentDate = Convert.ToDateTime(this.Parse(row,"last_abs_dt"));
                }
                catch {
                    item.LastAbsentDate = new DateTime(1900, 1, 1);
                }
                try
                {
                    item.TotalPenaltyByVerbal = Convert.ToInt32(this.Parse(row,"total_verb"));
                }
                catch { }
                try
                {
                    item.LastPenaltyByVerbalDate = Convert.ToDateTime(this.Parse(row,"last_verb_dt"));
                }
                catch {
                    item.LastPenaltyByVerbalDate = new DateTime(1900, 1, 1);
                }
                try
                {
                    item.TotalPenaltyByLetter = Convert.ToInt32(this.Parse(row,"total_lett"));
                }
                catch { }
                try
                {
                    item.LastPenaltyByLetterDate = Convert.ToDateTime(this.Parse(row,"last_lett_dt"));
                }
                catch { item.LastPenaltyByLetterDate = new DateTime(1900, 1, 1); }
                try
                {
                    item.TotalPenaltyByBan = Convert.ToInt32(this.Parse(row,"total_susp"));
                }
                catch { }
                try
                {
                    item.LastPenaltyByBanDate = Convert.ToDateTime(this.Parse(row,"last_susp_dt"));
                }
                catch { item.LastPenaltyByBanDate = new DateTime(1900, 1, 1); }
                return item;
            }else if (t == typeof(EmployeeLeaveInfo))
            {
                EmployeeLeaveInfo item = new EmployeeLeaveInfo();
                employeeDao.FillObject(row, item);

                try
                {
                    item.LeaveDate = Convert.ToDateTime(this.Parse(row,"cdate"));
                }
                catch { }
                try
                {
                    item.LeaveType = (string)this.Parse(row,"type");
                }
                catch { }
                try
                {
                    item.LeaveFromTime = Convert.ToDateTime(this.Parse(row,"lvfrom"));
                }
                catch { }
                try
                {
                    item.LeaveToTime = Convert.ToDateTime(this.Parse(row, "lvto"));
                }
                catch { }
                return item;
            }
            else if (t == typeof(ManpowerInfo))
            {
                ManpowerInfo item = new ManpowerInfo();
                try
                {
                    item.Code = OraHelper.DecodeLanguage((string)this.Parse(row,"dv_cd"));
                }
                catch { }
                try
                {
                    item.Name = OraHelper.DecodeLanguage((string)this.Parse(row,"dv_ename"));
                }
                catch { }
                try
                {
                    item.ShortName = OraHelper.DecodeLanguage((string)this.Parse(row,"dv_descr"));
                }
                catch { }
                try
                {
                    foreach (PositionInfo p in item.Positions)
                    {
                        try
                        {
                            p.TotalEmployee = Convert.ToInt32(row[p.Code]);
                        }
                        catch { }
                    }
                }
                catch { }
                try
                {
                    item.TotalMale = (int)this.Parse(row,"Male");
                }
                catch { }
                try
                {
                    item.TotalFemale = (int)this.Parse(row,"Female");
                }
                catch { }
                return item;
            }
            else
            {
                return null;
            }
        }

        #region IAbsenceDao Members

        public ArrayList SelectByCriteria(string keyword, string sectionCode, string leaveTypeCode, string status, DateTime fromDate, DateTime toDate)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_BY_CRITERIA, CommandType.StoredProcedure);

            cmd.Parameters.Add("p_keyword", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(keyword);
            cmd.Parameters.Add(PARAM_DIV_CD, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(sectionCode);
            cmd.Parameters.Add(PARAM_LEAVE_TYPE, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(leaveTypeCode);
            cmd.Parameters.Add(PARAM_STATUS, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(status);
            cmd.Parameters.Add(PARAM_FROM_DT, OracleDbType.Date).Value = fromDate;
            cmd.Parameters.Add(PARAM_TO_DT, OracleDbType.Date).Value = toDate;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(EmployeeLeaveInfo));
        }

        public ArrayList SummaryLeaveRecords(string leaveType, string divisionCode, DateTime fromDate, DateTime toDate)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SUM_LEAVE_RECORD, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_DIV_CD, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(divisionCode);
            cmd.Parameters.Add(PARAM_LEAVE_TYPE, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(leaveType);
            cmd.Parameters.Add(PARAM_FROM_DT, OracleDbType.Date).Value = fromDate;
            cmd.Parameters.Add(PARAM_TO_DT, OracleDbType.Date).Value = toDate;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(EmployeeLeaveInfo));
        }

        public ArrayList SelectEmployeeAbsentAlertRecords(string keyword , string sectionCode , DateTime fromDate , DateTime toDate)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_BY_ABSENT_ALERT_RECORDS, CommandType.StoredProcedure);

            cmd.Parameters.Add("p_keyword", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(keyword);
            cmd.Parameters.Add(PARAM_DIV_CD, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(sectionCode);
            //cmd.Parameters.Add(PARAM_FROM_DT, OracleDbType.Date).Value = fromDate.ToString("dd-MMM-yy", new CultureInfo("en-US"));
            //cmd.Parameters.Add(PARAM_TO_DT, OracleDbType.Date).Value = toDate.ToString("dd-MMM-yy", new CultureInfo("en-US"));

            cmd.Parameters.Add(PARAM_FROM_DT, OracleDbType.Date).Value = new DateTime(fromDate.Year,fromDate.Month,fromDate.Day,0,0,0);
            cmd.Parameters.Add(PARAM_TO_DT, OracleDbType.Date).Value = new DateTime(toDate.Year,toDate.Month,toDate.Day,0,0,0);

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(EmployeeAbsentAlertInfo));
        }
        #endregion

        #region IAttendanceDao Members


        public ArrayList SelectTotalAnnualByType(string employeeCode, DateTime fromDate, DateTime toDate, string leaveTypeCode)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
