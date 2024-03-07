using System;
using System.Collections.Generic;
using System.Text;
using PCUOnline.Dao;
using System.Data;
using DCI.Security.Model;
using System.Data.SqlClient;
using PCUOnline.Dao.Sql;
using System.Collections;

namespace DCI.Security.Persistence.Sql
{
    public class SqlUserGroupDao : DaoBase , IUserGroupDao
    {
        private const string SP_SELECT = "sp_ugrp_select";
        private const string PARAM_ID = "@ugroup_id";

   
        private const string SP_SELECTALL = "sp_allugrp_select";
        private const string SP_STORE = "sp_ugrp_store";
        private const string SP_DELETE = "sp_ugrp_Delete";

    

        private const string PARAM_ACTION = "@p_action";
        private const string PARAM_GRPID = "@p_usergroupid";
        private const string PARAM_GRPNAME = "@p_usergroupname";
        private const string PARAM_GRPDESC = "@p_usergroupdesc";
        private const string PARAM_GRPENABLE = "@p_enable";
        private const string PARAM_GRPCANDELETE = "@p_candelete";
        private const string PARAM_BY = "@p_by";


        public SqlUserGroupDao(DaoManager daoManager)
            : base(daoManager)
        {
        }

        public override object QueryForObject(DataRow row, Type t)
        {
            if (t == typeof(UserGroupInfo))
            {
                UserGroupInfo item = new UserGroupInfo();
                try
                {
                    item.ID = Convert.ToInt32(this.Parse(row, "UserGroupId"));
                }
                catch { }
                try
                {
                    item.Name = (string)this.Parse(row, "UserGroupName");
                }
                catch { }
                try
                {
                    item.Description = (string)this.Parse(row, "USERGROUPDESC");
                }
                catch { }
                try
                {
                    item.Enable = Convert.ToBoolean(this.Parse(row, "enable"));
                }
                catch { }
                try
                {
                    item.Permanent = Convert.ToBoolean(this.Parse(row, "canDelete"));
                }
                catch { }
                return item;
            }
            return null;
        }

        public override void AddParameters(IDbCommand cmd, object obj)
        {
            SqlCommand oraCmd = (SqlCommand)cmd;
            if (obj is UserGroupInfo)
            {
                UserGroupInfo item = (UserGroupInfo)obj;
                oraCmd.Parameters.Add(PARAM_GRPID, SqlDbType.Int).Value = item.ID;
                oraCmd.Parameters.Add(PARAM_GRPNAME, SqlDbType.VarChar).Value = item.Name;
                oraCmd.Parameters.Add(PARAM_GRPDESC, SqlDbType.VarChar).Value = item.Description;
                oraCmd.Parameters.Add(PARAM_GRPENABLE, SqlDbType.VarChar).Value = item.Enable.ToString();
                oraCmd.Parameters.Add(PARAM_GRPCANDELETE, SqlDbType.VarChar).Value = item.Permanent.ToString();
            }
        }

        #region IUserGroupDao Members

        public UserGroupInfo Select(int userGroupId)
        {
            SqlCommand cmd = SqlHelper.CreateCommand(SP_SELECT, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_ID, SqlDbType.Int).Value = userGroupId;

            return (UserGroupInfo)SqlHelper.ExecuteQuery(this, cmd, typeof(UserGroupInfo));
        }

        #endregion

        #region IUserGroupDao Members


        public ArrayList Select()
        {
            SqlCommand cmd = SqlHelper.CreateCommand(SP_SELECTALL, CommandType.StoredProcedure);
            return SqlHelper.ExecuteQueries(this, cmd, typeof(UserGroupInfo));
        }

        public void Update(UserGroupInfo grp)
        {
            SqlCommand cmd = SqlHelper.CreateCommand(SP_STORE, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_ACTION, SqlDbType.VarChar).Value = "UPDATE";
            this.AddParameters(cmd, grp);
            cmd.Parameters.Add(PARAM_BY, SqlDbType.VarChar).Value = " ";
            SqlHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void Save(UserGroupInfo grp)
        {
            SqlCommand cmd = SqlHelper.CreateCommand(SP_STORE, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_ACTION, SqlDbType.VarChar).Value = "ADD";
            this.AddParameters(cmd, grp);
            cmd.Parameters.Add(PARAM_BY, SqlDbType.VarChar).Value = " ";
            SqlHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void Delete(int grpId)
        {
            SqlCommand cmd = SqlHelper.CreateCommand(SP_DELETE, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_GRPID, SqlDbType.Int).Value = grpId;

            SqlHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        #endregion
    }
}
