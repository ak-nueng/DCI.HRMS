using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using DCI.HRMS.Model;
using PCUOnline.Dao;
using PCUOnline.Dao.Ora;
using DCI.HRMS.Model.Organize;

namespace DCI.HRMS.Persistence.Oracle
{
    public class OraPositionDao : DaoBase , IPositionDao
    {
        private const string SP_SELECT_ALL = "pkg_mstr_data.sp_get_position";

        public OraPositionDao(DaoManager daoManager) : base(daoManager)
        {
        }
        public override void AddParameters(IDbCommand cmd, object obj)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override object QueryForObject(DataRow row, Type t)
        {
            if (t == typeof(PositionInfo))
            {
                PositionInfo item = new PositionInfo();
                try
                {
                    item.Code = OraHelper.DecodeLanguage((string)this.Parse(row,"posi_cd"));
                }
                catch { }
                try
                {
                    if(item.Code=="")
                    item.Code = OraHelper.DecodeLanguage((string)this.Parse(row, "posit"));
                }
                catch { }
                try
                {
                    item.NameEng = OraHelper.DecodeLanguage((string)this.Parse(row,"posi_ename"));
                }
                catch { }
                try
                {
                    item.NameThai = OraHelper.DecodeLanguage((string)this.Parse(row,"posi_tname"));
                }
                catch { }
                return item;
            }
            else
            {
                return null;
            }
        }

        #region IPositionDao Members

        public ArrayList SelectAll()
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_ALL, CommandType.StoredProcedure);
            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(PositionInfo));
        }

        #endregion
    }
}
