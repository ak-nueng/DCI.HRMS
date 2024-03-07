using System;
using System.Collections.Generic;
using System.Text;
using PCUOnline.Dao;
using System.Data;
using DCI.Security.Model;

using Oracle.ManagedDataAccess.Client;
using PCUOnline.Dao.Ora;
using System.Collections;

namespace DCI.Security.Persistence.Ora
{
    public class OracleUserGroupDao : DaoBase, IUserGroupDao
    {
        private const string SP_SELECT = "PKG_SM.sp_ugrp_select";
        private const string SP_SELECTALL = "PKG_SM.sp_allugrp_select";
        private const string SP_STORE = "PKG_SM.sp_ugrp_store";
        private const string SP_DELETE = "PKG_SM.sp_ugrp_Delete";

        private const string PARAM_ID = "ugroup_id";

        private const string PARAM_ACTION = "p_action";
        private const string PARAM_GRPID = "p_usergroupid";
        private const string PARAM_GRPNAME = "p_usergroupname";
        private const string PARAM_GRPDESC = "p_usergroupdesc";
        private const string PARAM_GRPENABLE = "p_enable";
        private const string PARAM_GRPCANDELETE = "p_candelete";
        private const string PARAM_BY = "p_by";


        public OracleUserGroupDao(DaoManager daoManager)
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
                    item.Description = OraHelper.DecodeLanguage((string)this.Parse(row, "USERGROUPDESC"));
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
            OracleCommand oraCmd = (OracleCommand)cmd;
            if (obj is UserGroupInfo)
            {
                UserGroupInfo item = (UserGroupInfo)obj;
                oraCmd.Parameters.Add(PARAM_GRPID, OracleDbType.Int32).Value = item.ID;
                oraCmd.Parameters.Add(PARAM_GRPNAME, OracleDbType.Varchar2).Value = item.Name;
                oraCmd.Parameters.Add(PARAM_GRPDESC, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.Description);
                oraCmd.Parameters.Add(PARAM_GRPENABLE, OracleDbType.Varchar2).Value = item.Enable.ToString();
                oraCmd.Parameters.Add(PARAM_GRPCANDELETE, OracleDbType.Varchar2).Value = item.Permanent.ToString();
            }
        }

        #region IUserGroupDao Members

        public UserGroupInfo Select(int userGroupId)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_ID, OracleDbType.Int16).Value = userGroupId;

            return (UserGroupInfo)OraHelper.ExecuteQuery(this, cmd, typeof(UserGroupInfo));
        }

   


        public ArrayList Select()
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECTALL, CommandType.StoredProcedure);
            return OraHelper.ExecuteQueries(this, cmd, typeof(UserGroupInfo));
        }

        public void Update(UserGroupInfo grp)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_ACTION, OracleDbType.Varchar2).Value = "UPDATE";
            this.AddParameters(cmd, grp);
            cmd.Parameters.Add(PARAM_BY, OracleDbType.Varchar2).Value = " ";
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void Save(UserGroupInfo grp)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_ACTION, OracleDbType.Varchar2).Value = "ADD";
            this.AddParameters(cmd, grp);
            cmd.Parameters.Add(PARAM_BY, OracleDbType.Varchar2).Value = " ";
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void Delete(int grpId)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_DELETE, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_GRPID, OracleDbType.Int32).Value = grpId;

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        #endregion
    }
}
