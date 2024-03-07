using System;
using System.Collections.Generic;
using System.Text;
using PCUOnline.Dao;
using System.Data;
using System.Collections;
using DCI.Security.Model;
using PCUOnline.Dao.Sql;
using System.Data.SqlClient;

namespace DCI.Security.Persistence.Sql
{
    public class SqlModuleDao : DaoBase , IModuleDao
    {
        private const string SP_SELECT_BY_TYPE = "sp_mod_select_by_type";

        private const string PARAM_MOD_TYPE = "@modType";
        private const string PARAM_APP_TYPE = "@appType";

        public SqlModuleDao(DaoManager daoManager) : base(daoManager)
        {
        }

        public override object QueryForObject(DataRow row, Type t)
        {
            if (t == typeof(ModuleInfo))
            {
                ModuleInfo item = new ModuleInfo();
                item.Owner = new ModuleInfo();

                try
                {
                    item.Id = (string)this.Parse(row,"mod_id");
                }
                catch { }
                try
                {
                    item.Name = (string)this.Parse(row, "mod_name");
                }
                catch { }
                try
                {
                    item.Description = (string)this.Parse(row, "descr");
                }
                catch { }
                try
                {
                    item.Owner.Id = (string)this.Parse(row, "parent_id");
                }
                catch { }
                try
                {
                    item.SortingNo = Convert.ToInt32(this.Parse(row, "rank_no"));
                }
                catch { }
                try
                {
                    item.Enable = Convert.ToBoolean(this.Parse(row, "visible"));
                }
                catch { }
                try
                {
                    if (this.Parse(row, "namespace") != null)
                        item.NameSpace = (string)(this.Parse(row, "namespace"));
                }
                catch { }
                try
                {
                    if(this.Parse(row, "class") != null)
                        item.ClassName = (string)(this.Parse(row, "class"));
                }
                catch { }
                try
                {
                    if(this.Parse(row, "icon") != null)
                        item.Icon = (string)(this.Parse(row, "icon"));
                }
                catch { }
                try
                {
                    item.ApplicationType = ModuleInfo.ConvertApplicationType((string)this.Parse(row, "app_type"));
                }
                catch { }
                try
                {
                    item.Type = ModuleInfo.ConvertModuleType((string)this.Parse(row, "mod_type"));
                }
                catch { }
                return item;
            }
            return null;
        }

        public override void AddParameters(IDbCommand cmd, object obj)
        {
            
        }

        #region IModuleDao Members

        public ArrayList SelectByModuleType(ModuleType type)
        {
            SqlCommand cmd = SqlHelper.CreateCommand(SP_SELECT_BY_TYPE, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_MOD_TYPE, SqlDbType.VarChar).Value = ModuleInfo.ConvertModuleType(type);
            cmd.Parameters.Add(PARAM_APP_TYPE, SqlDbType.VarChar).Value = "%";

            return SqlHelper.ExecuteQueries(this,cmd,typeof(ModuleInfo));
        }

        public ArrayList SelectByModuleType(ModuleType type, ApplicationType applicationType)
        {
            SqlCommand cmd = SqlHelper.CreateCommand(SP_SELECT_BY_TYPE, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_MOD_TYPE, SqlDbType.VarChar).Value = ModuleInfo.ConvertModuleType(type);
            cmd.Parameters.Add(PARAM_APP_TYPE, SqlDbType.VarChar).Value = ModuleInfo.ConvertApplicationType(applicationType);

            return SqlHelper.ExecuteQueries(this, cmd, typeof(ModuleInfo));
        }

        #endregion
    }
}
