using System;
using PCUOnline.Dao;
using System.Collections;
using DCI.HRMS.Model.Attendance;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using PCUOnline.Dao.Ora;

namespace DCI.HRMS.Persistence.Oracle
{
    public class OraTimeCardDao : DaoBase, ITimeCardDao
    {
        private const string SP_SELECT_TimeCard = "pkg_hr_timecard.sp_select";
        private const string SP_SELECT_TimeCardTaff = "pkg_hr_timecard.sp_selectbytaff";
        private const string SP_UNIQSELECT_TimeCard = "pkg_hr_timecard.sp_uqselect";
        private const string SP_DELETE_TimeCard = "pkg_hr_timecard.sp_delete";
        private const string SP_STORE_TimeCard = "pkg_hr_timecard.sp_store";
        private const string SP_UPDATE_TimeCard = "pkg_hr_timecard.sp_update";
        private const string SP_STORE_TimeManual = "pkg_hr_timecard.sp_storetimemanual";
        private const string SP_SELECT_TimeManual = "pkg_hr_timecard.sp_selecttimemanual";
        private const string SP_DELETE_TimeManual = "pkg_hr_timecard.sp_deletetimemanual";


        private const string SP_SELECT_WorkingTime = "pkg_hr_timecard.sp_selectworktime";
        private const string SP_SELECT_SpecialWorkingTime = "pkg_hr_timecard.sp_selectspecialworktime";

        private const string PARA_EMPCODE = "p_code";
        private const string PARA_DATE = "p_cdate";
        private const string PARA_DATETO = "p_cdateto";
        private const string PARA_TYPE = "p_type";
        private const string PARA_FROMTIME = "p_fromtime";
        private const string PARA_TOTIME = "p_totime";
        private const string PARA_ACTION = "p_action";
        private const string PARA_USER = "p_by";
        private const string PARA_TIME = "p_time";
         private const string PARA_TIMEID = "p_machineid";
        private const string PARA_STARTDATE = "p_stdate"; 
        private const string PARA_ENDDATE = "p_endate";
        private const string PARA_DUTY = "p_duty";

        private const string PARA_Day = "p_day";
        private const string PARA_Date = "p_date";
        private const string PARA_Shift = "p_shift";


        public OraTimeCardDao(DaoManager daoManager)
            : base(daoManager)
        {
        }


        public override object QueryForObject(DataRow row, Type t)
        {
            if (t == typeof(TimeCardInfo))
            {

                TimeCardInfo item = new TimeCardInfo();
                try
                {
                    item.EmpCode = Convert.ToString(this.Parse(row, "code"));
                }
                catch
                {
                }
                try
                {
                    item.CardDate = Convert.ToDateTime(this.Parse(row, "cdate"));
                }
                catch
                {
                }
                try
                {
                    item.CardTime = Convert.ToString(this.Parse(row, "time"));
                }
                catch
                {
                }
                try
                {
                    item.CardMachId = Convert.ToInt32(this.Parse(row, "wstn"));
                }
                catch
                {
                }
                try
                {
                    item.Duty = Convert.ToString(this.Parse(row, "duty"));
                }
                catch
                {
                }
                return item;
            }
            if (t == typeof(TimeCardManualInfo))
            {
                TimeCardManualInfo item = new TimeCardManualInfo();
                ObjCommon iform = new ObjCommon();
                item.Inform = iform.QueryForObject(row);

                try
                {
                    item.EmpCode = Convert.ToString(this.Parse(row, "code"));
                }
                catch { }
                try
                {
                    item.RqDate = Convert.ToDateTime(this.Parse(row, "cdate"));
                }
                catch { }
                try
                {
                    item.RqType = Convert.ToString(this.Parse(row, "type"));
                }
                catch { }
                try
                {
                    item.TimeFrom = Convert.ToString(this.Parse(row, "ftime"));

                }
                catch { }
                try
                {
                    item.TimeTo = Convert.ToString(this.Parse(row, "ttime"));
                }
                catch { }
                return item;
            }
            if (t == typeof(WorkingHourInfo))
            {
                WorkingHourInfo item = new WorkingHourInfo();

                try
                {
                    item.DayOfWeek = Convert.ToString(this.Parse(row, "day"));
                }
                catch { }
                try
                {
                    item.Shift = Convert.ToString(this.Parse(row, "shift"));
                }
                catch { }
                try
                {
                    item.FirstStart = DateTime.Parse(Convert.ToString(this.Parse(row, "startfh")));
                }
                catch { }
                try
                {
                    item.FirstEnd = DateTime.Parse(Convert.ToString(this.Parse(row, "endfh")));
                }
                catch { }
                try
                {
                    item.SecondStart = DateTime.Parse(Convert.ToString(this.Parse(row, "startsh")));
                }
                catch { }
                try
                {
                    item.SecondEnd = DateTime.Parse(Convert.ToString(this.Parse(row, "endsh")));
                }
                catch { }
                try
                {
                    item.FirstTotal = Convert.ToInt32(this.Parse(row, "totalfh"));
                }
                catch { }
                try
                {
                    item.SecondTotal = Convert.ToInt32(this.Parse(row, "totalsh"));
                }
                catch { }


                try
                {
                    if (item.FirstEnd < item.FirstStart)
                    {
                        item.FirstEnd = item.FirstEnd.AddDays(1);
                        item.SecondStart = item.SecondStart.AddDays(1);
                        item.SecondEnd = item.SecondEnd.AddDays(1);
                    }
                }
                catch { }
                return item;


            }
            return null;

        }

        public override void AddParameters(IDbCommand cmd, object obj)
        {

        }

        #region ITimeCardDao Members

        public ArrayList GetTimeCardByDate(string code, DateTime stdt, DateTime endt)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_TimeCard, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_EMPCODE, OracleDbType.Varchar2).Value = code + "%";
            cmd.Parameters.Add(PARA_STARTDATE, OracleDbType.Date).Value = stdt;
            cmd.Parameters.Add(PARA_ENDDATE, OracleDbType.Date).Value = endt;
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(TimeCardInfo));
        }


        public DataSet GetTimeCardDataSetByDate(string code, DateTime stdt, DateTime endt)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_TimeCard, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_EMPCODE, OracleDbType.Varchar2).Value = code + "%";
            cmd.Parameters.Add(PARA_STARTDATE, OracleDbType.Date).Value = stdt;
            cmd.Parameters.Add(PARA_ENDDATE, OracleDbType.Date).Value = endt;
            return OraHelper.GetDataSet(this.Transaction, cmd,"TimeCard");
        }
        public ArrayList GetTimeCardByDate(string code, DateTime stdt, DateTime endt, string taffId)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_TimeCardTaff, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_EMPCODE, OracleDbType.Varchar2).Value = code + "%";
            cmd.Parameters.Add(PARA_STARTDATE, OracleDbType.Date).Value = stdt;
            cmd.Parameters.Add(PARA_ENDDATE, OracleDbType.Date).Value = endt;
            cmd.Parameters.Add(PARA_TIMEID, OracleDbType.Varchar2).Value = taffId + "%";
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(TimeCardInfo));
        }

        public void Insert(TimeCardInfo tcInfo)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_TimeCard, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_EMPCODE, OracleDbType.Varchar2).Value = tcInfo.EmpCode;
            cmd.Parameters.Add(PARA_DATE, OracleDbType.Date).Value = tcInfo.CardDate;
            cmd.Parameters.Add(PARA_TIME, OracleDbType.Varchar2).Value = tcInfo.CardTime;
            cmd.Parameters.Add(PARA_TIMEID, OracleDbType.Varchar2).Value = tcInfo.CardMachId.ToString("00");
            cmd.Parameters.Add(PARA_DUTY, OracleDbType.Varchar2).Value = tcInfo.Duty;

            OraHelper.ExecuteNonQuery( this.Transaction, cmd);
            
        }


        public void Update(TimeCardInfo tcInfo)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_UPDATE_TimeCard, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_EMPCODE, OracleDbType.Varchar2).Value = tcInfo.EmpCode;
            cmd.Parameters.Add(PARA_DATE, OracleDbType.Date).Value = tcInfo.CardDate.Date;
            cmd.Parameters.Add(PARA_TIME, OracleDbType.Varchar2).Value = tcInfo.CardTime;
            cmd.Parameters.Add(PARA_TIMEID, OracleDbType.Varchar2).Value = tcInfo.CardMachId.ToString();
            cmd.Parameters.Add(PARA_DUTY, OracleDbType.Varchar2).Value = tcInfo.Duty;

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void Delete(TimeCardInfo tcInfo)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_DELETE_TimeCard, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_EMPCODE, OracleDbType.Varchar2).Value = tcInfo.EmpCode;
            cmd.Parameters.Add(PARA_DATE, OracleDbType.Date).Value = tcInfo.CardDate;
            cmd.Parameters.Add(PARA_TIME, OracleDbType.Varchar2).Value = tcInfo.CardTime;
            cmd.Parameters.Add(PARA_TIMEID, OracleDbType.Varchar2).Value = tcInfo.CardMachId.ToString("00");
             OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public TimeCardInfo GetUniqTimeCard(string empCode, DateTime tmDate, string tmtime)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_UNIQSELECT_TimeCard, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_EMPCODE, OracleDbType.Varchar2).Value = empCode;
            cmd.Parameters.Add(PARA_DATE, OracleDbType.Date).Value=  tmDate;
            cmd.Parameters.Add(PARA_TIME, OracleDbType.Varchar2).Value = tmtime;
            return (TimeCardInfo) OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(TimeCardInfo));
        }
        public ArrayList GetTimeCardManual(string code, DateTime cdate ,DateTime cdateto, string type)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_TimeManual, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_EMPCODE, OracleDbType.Varchar2).Value = code + "%";
            cmd.Parameters.Add(PARA_DATE, OracleDbType.Date).Value = cdate;
            cmd.Parameters.Add(PARA_DATETO, OracleDbType.Date).Value = cdateto;
            cmd.Parameters.Add(PARA_TYPE, OracleDbType.Varchar2).Value = type ;
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(TimeCardManualInfo));
        }


        public DataSet GetTimeCardManualDataSet(string code, DateTime cdate, DateTime cdateto, string type)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_TimeManual, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_EMPCODE, OracleDbType.Varchar2).Value = code + "%";
            cmd.Parameters.Add(PARA_DATE, OracleDbType.Date).Value = cdate;
            cmd.Parameters.Add(PARA_DATETO, OracleDbType.Date).Value = cdateto;
            cmd.Parameters.Add(PARA_TYPE, OracleDbType.Varchar2).Value = type;
            return OraHelper.GetDataSet( this.Transaction, cmd,"TMRQ");
        }
        public TimeCardManualInfo GetTimeCardManual(string code, DateTime cdate, string type)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_TimeCard, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_EMPCODE, OracleDbType.Varchar2).Value = code;
            cmd.Parameters.Add(PARA_DATE, OracleDbType.Date).Value = cdate;
            cmd.Parameters.Add(PARA_DATETO, OracleDbType.Date).Value = cdate;
            cmd.Parameters.Add(PARA_TYPE, OracleDbType.Date).Value = type;
            return (TimeCardManualInfo)OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(TimeCardManualInfo));
        }
        
        public void UpdateTimeCardManual(TimeCardManualInfo tmrq)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_TimeManual, CommandType.StoredProcedure); 
            cmd.Parameters.Add(PARA_ACTION  , OracleDbType.Varchar2).Value = "UPDATE";     
            cmd.Parameters.Add(PARA_EMPCODE, OracleDbType.Varchar2).Value = tmrq.EmpCode;
            cmd.Parameters.Add(PARA_DATE , OracleDbType.Date).Value = tmrq.RqDate;
            cmd.Parameters.Add(PARA_TYPE , OracleDbType.Varchar2).Value = tmrq.RqType;
            cmd.Parameters.Add(PARA_FROMTIME , OracleDbType.Varchar2).Value = tmrq.TimeFrom;
            cmd.Parameters.Add(PARA_TOTIME , OracleDbType.Varchar2).Value = tmrq.TimeTo;
                
            cmd.Parameters.Add(PARA_USER, OracleDbType.Varchar2).Value = tmrq.LastUpdateBy;
           OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void SaveTimeCardManual(TimeCardManualInfo tmrq)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_TimeManual, CommandType.StoredProcedure); 
            cmd.Parameters.Add(PARA_ACTION, OracleDbType.Varchar2).Value = "ADD";
            cmd.Parameters.Add(PARA_EMPCODE, OracleDbType.Varchar2).Value = tmrq.EmpCode;
            cmd.Parameters.Add(PARA_DATE, OracleDbType.Date).Value = tmrq.RqDate;
            cmd.Parameters.Add(PARA_TYPE, OracleDbType.Varchar2).Value = tmrq.RqType;
            cmd.Parameters.Add(PARA_FROMTIME, OracleDbType.Varchar2).Value = tmrq.TimeFrom;
            cmd.Parameters.Add(PARA_TOTIME, OracleDbType.Varchar2).Value = tmrq.TimeTo;           
            cmd.Parameters.Add(PARA_USER, OracleDbType.Varchar2).Value = tmrq.CreateBy;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void DeleteTimeCardManual(TimeCardManualInfo tmrq)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_DELETE_TimeManual, CommandType.StoredProcedure);          
            cmd.Parameters.Add(PARA_EMPCODE, OracleDbType.Varchar2).Value = tmrq.EmpCode;
            cmd.Parameters.Add(PARA_DATE, OracleDbType.Date).Value = tmrq.RqDate;
            cmd.Parameters.Add(PARA_TYPE, OracleDbType.Varchar2).Value = tmrq.RqType;       
            cmd.Parameters.Add(PARA_USER, OracleDbType.Varchar2).Value = tmrq.LastUpdateBy;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }




        public ArrayList GetWorkingHour(string _day, string _shift)
        {
          
      
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_WorkingTime, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_Day, OracleDbType.Varchar2).Value = _day;
            cmd.Parameters.Add(PARA_Shift, OracleDbType.Varchar2).Value = _shift;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(WorkingHourInfo));
      

        }


        public ArrayList GetWorkingHour(DateTime _date, string _shift)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_SpecialWorkingTime, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARA_Date, OracleDbType.Date).Value = _date;
            cmd.Parameters.Add(PARA_Shift, OracleDbType.Varchar2).Value = _shift;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(WorkingHourInfo));
      
        }




        #endregion
    }
}
