using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using PCUOnline.Dao;
using PCUOnline.Dao.Ora;
using    DCI.HRMS.Model.Common;

namespace DCI.HRMS.Persistence.Oracle
{
   
        /// <summary>
        /// Summary description for SqlKeyGeneratorDAO.
        /// </summary>
        public class OraKeyGeneratorDao : DaoBase, IKeyGeneratorDao
        {
            public OraKeyGeneratorDao(DaoManager daoManager)
                : base(daoManager) { }

            public string NextId(string key)
            {
                RunningNumber num = LoadUnique(key);

                OracleCommand cmd = new OracleCommand();
                cmd.CommandText = "pkg_hr_dc.sp_DCRunNbr_NextId";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_Key", OracleDbType.Varchar2).Value = num.Key;
                cmd.Parameters.Add("p_IsReSet", OracleDbType.Varchar2).Value = num.MustReset.ToString();

                OraHelper.ExecuteNonQuery(this.Transaction, cmd);
                return num.ToString(true);
            }

            public RunningNumber LoadUnique(string key)
            {
                OracleCommand cmd = new OracleCommand();
                cmd.CommandText = "pkg_hr_dc.sp_getFormatDocNbrByKey";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_Key",OracleDbType.Varchar2).Value = key;

                return (RunningNumber)OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(RunningNumber));
            }

            public void ObjectToCommandParameters(object obj, IDbCommand cmd)
            {
                throw new NotImplementedException();
            }


            public override object QueryForObject(DataRow row, Type t)
            {
                if (t == typeof(RunningNumber))
                {
                    RunningNumber item = new RunningNumber();

            
                    try
                    {
                        item.Key = Convert.ToString(row["DocKey"]);
                    }
                    catch { }
                    try
                    {
                        item.Prefix = Convert.ToString(row["DocPrefix"]);
                    }
                    catch { }
                    try
                    {
                        item.LenYearPrefix = Convert.ToInt32(row["YearNbrPrefix"]);
                    }
                    catch { }
                    try
                    {
                        item.LenMonthPrefix = Convert.ToInt32(row["MonthNbrPrefix"]);
                    }
                    catch { }
                    try
                    {
                        item.LenDayPrefix = Convert.ToInt32(row["DayNbrPrefix"]);
                    }
                    catch { }
                    try
                    {
                        item.LenRunId = Convert.ToInt32(row["FormatNbr"]);
                    }
                    catch { }
                    try
                    {
                        item.NextId = Convert.ToInt32(row["NextId"]);
                    }
                    catch { }
                    try
                    {
                        item.ActiveDate = Convert.ToDateTime(row["ActiveDate"]);
                    }
                    catch { }
                    try
                    {
                        item.Date = Convert.ToDateTime(row["CurDate"]);
                    }
                    catch { }
                    try
                    {
                        item.Remark = Convert.ToString(row["Remark"]);
                    }
                    catch { }
                    try
                    {
                        item.ResetOption = Convert.ToString(row["ResetOption"]);
                    }
                    catch { }
                    return item;
                }
                else
                {
                    return null;
                }
            }

            public override void AddParameters(IDbCommand cmd, object obj)
            {

            }
        }
    
}
