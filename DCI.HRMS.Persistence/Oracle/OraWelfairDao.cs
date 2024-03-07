using System;
using System.Collections.Generic;
using System.Text;
using PCUOnline.Dao;
using DCI.HRMS.Model.Welfare;
using System.Data;
using PCUOnline.Dao.Ora;
using Oracle.ManagedDataAccess.Client;
using System.Collections;

namespace DCI.HRMS.Persistence.Oracle
{
   public class OraWelfairDao: DaoBase, IWelfairDao
    {
       private const string SP_SELECT_BUSWAY = "pkg_mstr_data.sp_getbusway";
       private const string SP_SELECT_BUSSTOPBYBUSWAY = "pkg_mstr_data.sp_getbusstopbybusway";
       private const string SP_SELECT_BUSSTOP = "pkg_mstr_data.sp_getbusstop";

       public OraWelfairDao(DaoManager daoManager)
            : base(daoManager)
        {
        }

        public override object QueryForObject(DataRow row, Type t)
        {
            if (t== typeof(BusStopInfo))
            {
                BusStopInfo item = new BusStopInfo();
                try
                {
                    item.StopCode = OraHelper.DecodeLanguage((string)this.Parse(row, "code")).Substring(1);
                }
                catch 
                {}
                try
                {
                    item.Code = OraHelper.DecodeLanguage((string)this.Parse(row, "code"));
                }
                catch
                { }
                try
                {
                    item.StopName = OraHelper.DecodeLanguage((string)this.Parse(row, "stopname"));
                }
                catch
                { }
                try
                {
                    item.Busway = OraHelper.DecodeLanguage((string)this.Parse(row, "busway"));
                }
                catch
                { }
                try
                {
                    item.NumEmp = int.Parse(this.Parse(row, "emp").ToString());
                }
                catch
                { }
                try
                {
                    item.Order = int.Parse(this.Parse(row, "sort_order").ToString()) ;
                }
                catch
                { }

                try
                {
                    item.TimeDay = OraHelper.DecodeLanguage((string)this.Parse(row, "stoptime")).Split(',')[0];
                }
                catch
                {
                    item.TimeDay = "";
                }
                try
                {
                    item.TimeNight = OraHelper.DecodeLanguage((string)this.Parse(row, "stoptime")).Split(',')[1];
                }
                catch
                {
                    item.TimeNight = "";
                }
                return item;
                
            }
            else if (t == typeof(BusWayInfo))
            {
                BusWayInfo item = new BusWayInfo();
           
                try
                {
                    item.Code = OraHelper.DecodeLanguage((string)this.Parse(row, "code"));
                }
                catch
                { }
                try
                {
                    item.NameEng = OraHelper.DecodeLanguage((string)this.Parse(row, "ename"));
                }
                catch
                { }
                try
                {
                    item.NameThai = OraHelper.DecodeLanguage((string)this.Parse(row, "tname"));
                }
                catch
                { }
                try
                {
                    item.NumEmp = int.Parse(this.Parse(row, "emp").ToString());
                }
                catch
                { }
                try
                {
                    item.Order = int.Parse(this.Parse(row, "sort_order").ToString());
                }
                catch
                { }
                return item;

            }
            return null;
        }

        public override void AddParameters(IDbCommand cmd, object obj)
        {
            throw new NotImplementedException();
        }

        #region IWelfairDao Members

        public BusStopInfo GetBusStop(string busway, string stopCode)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_BUSSTOP, CommandType.StoredProcedure);

            cmd.Parameters.Add("p_busway", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(busway);
            cmd.Parameters.Add("p_busstop", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(stopCode);

            return (BusStopInfo)OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(BusStopInfo));
        }

        public BusWayInfo GetBusWay(string busWay)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_BUSWAY, CommandType.StoredProcedure);

            cmd.Parameters.Add("p_busway", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(busWay);

            return (BusWayInfo)OraHelper.ExecuteQuery(this,this.Transaction, cmd, typeof(BusWayInfo));
        }

        public ArrayList GetBusStop(string busway)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_BUSSTOPBYBUSWAY, CommandType.StoredProcedure);

            cmd.Parameters.Add("p_busway", OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(busway);

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(BusStopInfo));
        }

        public ArrayList GetAllBusWay()
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_BUSWAY, CommandType.StoredProcedure);

            cmd.Parameters.Add("p_busway", OracleDbType.Varchar2).Value ="%";

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(BusWayInfo));
        }

        #endregion
    }
}
