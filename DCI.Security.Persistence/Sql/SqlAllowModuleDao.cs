using System;
using System.Collections.Generic;
using System.Text;
using PCUOnline.Dao;
using System.Data;
using System.Collections;
using DCI.Security.Model;
using System.Data.SqlClient;
using PCUOnline.Dao.Sql;

namespace DCI.Security.Persistence.Sql
{
    public class SqlAllowModuleDao : DaoBase , IAllowModuleDao
    {
        private const string SP_SELECT_MOD = "sp_usrgrp_select_allow_mod";
        private const string SP_SELECT_MODS = "sp_usrgrp_select_allow_mod";

    
        private const string SP_SELECT_PERM = "sp_permission_select";
        private const string SP_STORE_PERM = "sp_permission_store";

        private const string PARAM_MODULE_ID = "@mod_id";
        private const string PARAM_USER_GROUP = "@ugroup_id";
        private const string PARAM_APP_TYPE = "@appType";


        private const string PARAM_ACTION = "@p_action";
        private const string PARAM_USR_GROUP = "@p_ugroup_id";
        private const string PARAM_MOD_ID = "@p_mod_id";
        private const string PARAM_ADD = "@p_can_add";
        private const string PARAM_VIEW = "@p_can_view";
        private const string PARAM_EDIT = "@p_can_edit";
        private const string PARAM_DELETE = "@p_can_delete";
        private const string PARAM_PRINT = "@p_can_print";
        private const string PARAM_EXPORT = "@p_can_export";
        private const string PARAM_CHANGE = "@p_can_changedoc";
        private const string PARAM_BY = "@p_by";

        private SqlModuleDao moduleDao;

        public SqlAllowModuleDao(DaoManager daoManager) : base(daoManager)
        {
            moduleDao = new SqlModuleDao(daoManager);
        }
        public override object QueryForObject(DataRow row, Type t)
        {
            if (t == typeof(ModuleInfo))
            {
                ModuleInfo item = (ModuleInfo)moduleDao.QueryForObject(row,typeof(ModuleInfo));
                item.Permission = new PermissionInfo();
                try
                {
                    item.Permission.AllowAccess = Convert.ToBoolean(this.Parse(row, "can_view"));
                }
                catch { }
                try
                {
                    item.Permission.AllowAddNew = Convert.ToBoolean(this.Parse(row, "can_add"));
                }
                catch { }
                try
                {
                    item.Permission.AllowEdit = Convert.ToBoolean(this.Parse(row, "can_edit"));
                }
                catch { }
                try
                {
                    item.Permission.AllowDelete = Convert.ToBoolean(this.Parse(row, "can_delete"));
                }
                catch { }
                try
                {
                    item.Permission.AllowPrintReport = Convert.ToBoolean(this.Parse(row, "can_print"));
                }
                catch { }
                try
                {
                    item.Permission.AllowExportData = Convert.ToBoolean(this.Parse(row, "can_export"));
                }
                catch { }
                try
                {
                    item.Permission.AllowChangeStatus = Convert.ToBoolean(this.Parse(row, "can_changedoc"));
                }
                catch { }
                return item;
            }
            return null;
        }

        public override void AddParameters(IDbCommand cmd, object obj)
        {

            SqlCommand oraCmd = (SqlCommand)cmd;
            if (obj is UserGroupPermission)
            {
                UserGroupPermission item = (UserGroupPermission)obj;

                oraCmd.Parameters.Add(PARAM_USR_GROUP, SqlDbType.Int).Value = item.GroupInfo.ID;
                oraCmd.Parameters.Add(PARAM_MOD_ID, SqlDbType.VarChar).Value = item.GroupModuleInfo.Id;
                oraCmd.Parameters.Add(PARAM_ADD, SqlDbType.VarChar).Value = item.AddNewEnable;
                oraCmd.Parameters.Add(PARAM_VIEW, SqlDbType.VarChar).Value = item.ViewEnable;
                oraCmd.Parameters.Add(PARAM_EDIT, SqlDbType.VarChar).Value = item.EditEnable;
                oraCmd.Parameters.Add(PARAM_DELETE, SqlDbType.VarChar).Value = item.DeleteEnable;
                oraCmd.Parameters.Add(PARAM_PRINT, SqlDbType.VarChar).Value = item.PrintEnable;
                oraCmd.Parameters.Add(PARAM_EXPORT, SqlDbType.VarChar).Value = item.ExportEnable;
                oraCmd.Parameters.Add(PARAM_CHANGE, SqlDbType.VarChar).Value = item.ChangeDocumentStatusEnable;

            }
        }

        #region IAllowModuleDao Members

        public ModuleInfo SelectAllowModule(string moduleId, int userGroupId)
        {
            SqlCommand cmd = SqlHelper.CreateCommand(SP_SELECT_MOD, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_APP_TYPE, SqlDbType.VarChar).Value = "%";
            cmd.Parameters.Add(PARAM_MODULE_ID, SqlDbType.VarChar).Value = moduleId;
            cmd.Parameters.Add(PARAM_USER_GROUP, SqlDbType.Int).Value = userGroupId;

            return (ModuleInfo)SqlHelper.ExecuteQuery(this,cmd,typeof(ModuleInfo));
        }
        public ArrayList SelectAllowModules(int userGroupId, ApplicationType applicationType)
        {
            SqlCommand cmd = SqlHelper.CreateCommand(SP_SELECT_MODS, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_APP_TYPE, SqlDbType.VarChar).Value = ModuleInfo.ConvertApplicationType(applicationType);
            cmd.Parameters.Add(PARAM_MODULE_ID, SqlDbType.VarChar).Value = "%";
            cmd.Parameters.Add(PARAM_USER_GROUP, SqlDbType.Int).Value = userGroupId;

            return SqlHelper.ExecuteQueries(this, cmd, typeof(ModuleInfo));
        }
     

        public UserGroupPermission SelectUserGroupPermission(string moduleId, int userGroupId)
        {
              SqlCommand cmd = SqlHelper.CreateCommand(SP_SELECT_PERM, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_MODULE_ID, SqlDbType.VarChar).Value = moduleId;
            cmd.Parameters.Add(PARAM_USR_GROUP, SqlDbType.VarChar).Value = userGroupId;


            return (UserGroupPermission)SqlHelper.ExecuteQuery(this, cmd, typeof(UserGroupPermission));
        }

        public void UpdateUserGroupPermission(UserGroupPermission prem)
        {
            SqlCommand cmd = SqlHelper.CreateCommand(SP_STORE_PERM, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_ACTION, SqlDbType.VarChar).Value = "UPDATE";
            this.AddParameters(cmd, prem);
            cmd.Parameters.Add(PARAM_BY, SqlDbType.VarChar).Value = " ";
            SqlHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void AddUserGroupPermission(UserGroupPermission prem)
        {
            SqlCommand cmd = SqlHelper.CreateCommand(SP_STORE_PERM, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_ACTION, SqlDbType.VarChar).Value = "ADD";
            this.AddParameters(cmd, prem);
            cmd.Parameters.Add(PARAM_BY, SqlDbType.VarChar).Value = " ";
            SqlHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void DeleteUserGroupPermission(string moduleId, int userGroupId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
