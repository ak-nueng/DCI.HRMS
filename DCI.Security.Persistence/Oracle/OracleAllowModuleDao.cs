using System;
using System.Collections.Generic;
using System.Text;
using PCUOnline.Dao;
using System.Data;
using System.Collections;
using DCI.Security.Model;
using System.Data.SqlClient;
using PCUOnline.Dao.Ora;
using Oracle.ManagedDataAccess.Client;

namespace DCI.Security.Persistence.Ora
{
    public class OracleAllowModuleDao : DaoBase, IAllowModuleDao
    {
        private const string SP_SELECT_MOD = "PKG_SM.sp_usrgrp_select_allow_mod";
        private const string SP_SELECT_MODS = "PKG_SM.sp_usrgrp_select_allow_mods";
        private const string SP_SELECT_PERM = "PKG_SM.sp_permission_select";
        private const string SP_STORE_PERM = "PKG_SM.sp_permission_store";

        private const string PARAM_MODUL_ID = "p_modul_id";
     
        private const string PARAM_APP_TYPE = "p_apptype";






        private const string PARAM_ACTION = "p_action";
     private const string PARAM_USR_GROUP = "p_ugroup_id";
        private const string PARAM_MOD_ID = "p_mod_id";
        private const string PARAM_ADD = "p_can_add";
        private const string PARAM_VIEW = "p_can_view";
        private const string PARAM_EDIT = "p_can_edit";
        private const string PARAM_DELETE = "p_can_delete";
        private const string PARAM_PRINT = "p_can_print";
        private const string PARAM_EXPORT = "p_can_export";
        private const string PARAM_CHANGE = "p_can_changedoc";
        private const string PARAM_BY = "p_by";

        private OracleModuleDao moduleDao;

        public OracleAllowModuleDao(DaoManager daoManager)
            : base(daoManager)
        {
            moduleDao = new OracleModuleDao(daoManager);
        }
        public override object QueryForObject(DataRow row, Type t)
        {
            if (t == typeof(ModuleInfo))
            {
                ModuleInfo item = (ModuleInfo)moduleDao.QueryForObject(row, typeof(ModuleInfo));
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
            else if (t == typeof(UserGroupPermission))
            {
                UserGroupPermission item = new UserGroupPermission();
                item.GroupInfo = new UserGroupInfo();
                item.GroupModuleInfo = new ModuleInfo();
                try
                {
                    item.GroupInfo.ID = Convert.ToInt32(this.Parse(row, "ugroup_id"));
                }
                catch { }
                try
                {
                    item.GroupModuleInfo.Id = Convert.ToString(this.Parse(row, "MOD_ID"));
                }
                catch { }
                try
                {
                    item.ViewEnable = Convert.ToBoolean(this.Parse(row, "can_view"));
                }
                catch { }
                try
                {
                    item.AddNewEnable = Convert.ToBoolean(this.Parse(row, "can_add"));
                }
                catch { }
                try
                {
                    item.EditEnable = Convert.ToBoolean(this.Parse(row, "can_edit"));
                }
                catch { }
                try
                {
                    item.DeleteEnable = Convert.ToBoolean(this.Parse(row, "can_delete"));
                }
                catch { }
                try
                {
                    item.PrintEnable = Convert.ToBoolean(this.Parse(row, "can_print"));
                }
                catch { }
                try
                {
                    item.ExportEnable = Convert.ToBoolean(this.Parse(row, "can_export"));
                }
                catch { }
                try
                {
                    item.ChangeDocumentStatusEnable = Convert.ToBoolean(this.Parse(row, "can_changedoc"));
                }
                catch { }
                return item;

            }
            return null;
        }

        public override void AddParameters(IDbCommand cmd, object obj)
        {

            OracleCommand oraCmd = (OracleCommand)cmd;
            if (obj is UserGroupPermission)
            {
                UserGroupPermission item = (UserGroupPermission)obj;

                oraCmd.Parameters.Add(PARAM_USR_GROUP, OracleDbType.Int32).Value = item.GroupInfo.ID;
                oraCmd.Parameters.Add(PARAM_MOD_ID, OracleDbType.Varchar2).Value = item.GroupModuleInfo.Id;
                oraCmd.Parameters.Add(PARAM_ADD, OracleDbType.Varchar2).Value = item.AddNewEnable;
                oraCmd.Parameters.Add(PARAM_VIEW, OracleDbType.Varchar2).Value = item.ViewEnable;
                oraCmd.Parameters.Add(PARAM_EDIT, OracleDbType.Varchar2).Value = item.EditEnable;
                oraCmd.Parameters.Add(PARAM_DELETE, OracleDbType.Varchar2).Value = item.DeleteEnable;
                oraCmd.Parameters.Add(PARAM_PRINT, OracleDbType.Varchar2).Value = item.PrintEnable;
                oraCmd.Parameters.Add(PARAM_EXPORT, OracleDbType.Varchar2).Value = item.ExportEnable;
                oraCmd.Parameters.Add(PARAM_CHANGE, OracleDbType.Varchar2).Value = item.ChangeDocumentStatusEnable;

            }
        }

        #region IAllowModuleDao Members

        public ModuleInfo SelectAllowModule(string moduleId, int userGroupId)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_MOD, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_MODUL_ID, OracleDbType.Varchar2).Value = moduleId;
            cmd.Parameters.Add(PARAM_USR_GROUP, OracleDbType.Varchar2).Value = userGroupId;

            return (ModuleInfo)OraHelper.ExecuteQuery(this, cmd, typeof(ModuleInfo));
        }
        public ArrayList SelectAllowModules(int userGroupId, ApplicationType applicationType)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_MODS, CommandType.StoredProcedure);
            string appT = ModuleInfo.ConvertApplicationType(applicationType);
            cmd.Parameters.Add(PARAM_APP_TYPE, OracleDbType.Varchar2).Value = appT;
            cmd.Parameters.Add(PARAM_USR_GROUP, OracleDbType.Varchar2).Value = userGroupId;

            return OraHelper.ExecuteQueries(this, cmd, typeof(ModuleInfo));
        }
     


        public UserGroupPermission SelectUserGroupPermission(string moduleId, int userGroupId)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_PERM, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_MODUL_ID, OracleDbType.Varchar2).Value = moduleId;
            cmd.Parameters.Add(PARAM_USR_GROUP, OracleDbType.Varchar2).Value = userGroupId;
        

            return (UserGroupPermission)OraHelper.ExecuteQuery(this, cmd, typeof(UserGroupPermission));
        }

        public void UpdateUserGroupPermission(UserGroupPermission prem)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_PERM, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_ACTION, OracleDbType.Varchar2).Value = "UPDATE";
            this.AddParameters(cmd, prem);
            cmd.Parameters.Add(PARAM_BY, OracleDbType.Varchar2).Value = " ";
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void AddUserGroupPermission(UserGroupPermission prem)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_PERM, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_ACTION, OracleDbType.Varchar2).Value = "ADD";
            this.AddParameters(cmd, prem);
            cmd.Parameters.Add(PARAM_BY, OracleDbType.Varchar2).Value = " ";
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void DeleteUserGroupPermission(string moduleId, int userGroupId)
        {
           
        }

        #endregion
    }
}
