using System;
using System.Collections.Generic;
using System.Text;
using PCUOnline.Dao;
using System.Data;
using DCI.Security.Model;
using System.Data.SqlClient;
using PCUOnline.Dao.Ora;
using Oracle.ManagedDataAccess.Client;
using System.Collections;

namespace DCI.Security.Persistence.Ora
{
    public class OracleUserAccountDao : DaoBase, IUserAccountDao
    {
        private const string SP_SELECT_BY_PWD = "PKG_SM.sp_UserAuthentication";
        private const string SP_SELECT = "PKG_SM.sp_User_Select";
        private const string SP_SELECT_BY_Group = "PKG_SM.sp_User_Selectbygroup";


        private const string SP_STORE_USER = "PKG_SM.sp_user_store";
        private const string SP_DELETE_USER = "PKG_SM.sp_user_delete";
        private const string SP_UPDATEPWD_USER = "PKG_SM.sp_user_udatepassword";
        private const string PARAM_USER = "Usr";
        private const string PARAM_PASSWORD = "Password";
        private const string PARAM_GROUP = "p_grpid";




        private const string PARAM_ACTION = "p_action";
        private const string PARAM_USERID = "p_userid";   
        private const string PARAM_FULLNAME = "p_fullname";
        private const string PARAM_DESC = "p_userdescription";
        private const string PARAM_EMAIL = "p_email";
        private const string PARAM_GRPID = "p_usergroupid";
        private const string PARAM_ENABLE = "p_enable";
        private const string PARAM_PWDCHANGE = "p_pwdchange";
        private const string PARAM_PWDNOTCHANGE = "p_pwdnotchange";
        private const string PARAM_PWDNOTEXP = "p_pwdnotexpire"; 
        private const string PARAM_USERPWD = "p_userpwd  "; 
        private const string PARAM_BY = "p_by";




        public OracleUserAccountDao(DaoManager daoManager)
            : base(daoManager)
        {
        }

        public override object QueryForObject(DataRow row, Type t)
        {
            if (t == typeof(UserAccountInfo))
            {
                UserAccountInfo item = new UserAccountInfo();
                item.UserGroup = new UserGroupInfo();

                try
                {
                    item.ChangePasswordAtNextLogon = Convert.ToBoolean(this.Parse(row, "PwdChange"));
                }
                catch { }
                try
                {
                    item.PasswordLastChange = Convert.ToDateTime(this.Parse(row, "PwdLastChange"));
                }
                catch { }
                try
                {
                    item.CannotChangePassword = Convert.ToBoolean(this.Parse(row, "PwdNotChange"));
                }
                catch { }
                try
                {
                    item.PasswordNeverExpires = Convert.ToBoolean(this.Parse(row, "PwdNotExpire"));
                }
                catch { }

                try
                {
                    item.AccountId = (string)this.Parse(row, "UserId");
                }
                catch { }

                try
                {
                    item.FullName = (string)this.Parse(row, "FullName");
                }
                catch { }

                try
                {
                    item.Description = (string)this.Parse(row, "userdescription");
                }
                catch { }

                try
                {
                    item.UserGroup.ID = Convert.ToInt32(this.Parse(row, "UserGroupId"));
                }
                catch { }

                try
                {
                    item.Email = (string)this.Parse(row, "Email");
                }
                catch { }

                try
                {
                    item.Enable = Convert.ToBoolean(this.Parse(row, "Enable"));
                }
                catch { }
                try
                {
                    item.PasswordLastChange = Convert.ToDateTime(this.Parse(row, "PWDLASTCHANGE"));
                }
                catch { }
                try
                {
                    item.JoinDate = Convert.ToDateTime(this.Parse(row, "createdatetime"));
                }
                catch { }

                return item;
            }
            return null;
        }

        public override void AddParameters(IDbCommand cmd, object obj)
        {
            OracleCommand oraCmd = (OracleCommand)cmd;
            if (obj is UserAccountInfo)
            {
                UserAccountInfo item = (UserAccountInfo)obj;

                oraCmd.Parameters.Add(PARAM_USERID, OracleDbType.Varchar2).Value = item.AccountId;
                oraCmd.Parameters.Add(PARAM_FULLNAME, OracleDbType.Varchar2).Value = item.FullName;
                oraCmd.Parameters.Add(PARAM_DESC, OracleDbType.Varchar2).Value = item.Description;
                oraCmd.Parameters.Add(PARAM_EMAIL, OracleDbType.Varchar2).Value = item.Email;
                oraCmd.Parameters.Add(PARAM_GRPID, OracleDbType.Varchar2).Value = item.UserGroup.ID;
                oraCmd.Parameters.Add(PARAM_ENABLE, OracleDbType.Varchar2).Value = item.Enable.ToString();
                oraCmd.Parameters.Add(PARAM_PWDCHANGE, OracleDbType.Varchar2).Value = item.ChangePasswordAtNextLogon.ToString();
                oraCmd.Parameters.Add(PARAM_PWDNOTCHANGE, OracleDbType.Varchar2).Value = item.CannotChangePassword.ToString();
                oraCmd.Parameters.Add(PARAM_PWDNOTEXP, OracleDbType.Varchar2).Value = item.PasswordNeverExpires.ToString();
            }
        
        }

        #region IUserAccountDao Members

        public UserAccountInfo Select(string userId)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_USER, OracleDbType.Varchar2).Value = userId;

            return (UserAccountInfo)OraHelper.ExecuteQuery(this, cmd, typeof(UserAccountInfo));
        }
        public UserAccountInfo Select(string userId, string password)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_BY_PWD, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_USER, OracleDbType.Varchar2).Value = userId;
            cmd.Parameters.Add(PARAM_PASSWORD, OracleDbType.Varchar2).Value = password;

            return (UserAccountInfo)OraHelper.ExecuteQuery(this, cmd, typeof(UserAccountInfo));
        }
        public void KeepLog(string user, string moduleId, string fromMachine, string action, string description)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PKG_SM.sp_User_LogFile";

            cmd.Parameters.Add("Datetime", OracleDbType.Date).Value = System.DateTime.Now;
            cmd.Parameters.Add("Usr", OracleDbType.Varchar2).Value = user;
            cmd.Parameters.Add("ModId", OracleDbType.Varchar2).Value = moduleId;
            cmd.Parameters.Add("Computer", OracleDbType.Varchar2).Value = fromMachine;
            cmd.Parameters.Add("Action", OracleDbType.Varchar2).Value = action;
            cmd.Parameters.Add("Descr", OracleDbType.Varchar2).Value = description;

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }


        public void insert(UserAccountInfo account, string hashPassword, string updateBy)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_USER, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_ACTION, OracleDbType.Varchar2).Value = "ADD";
            this.AddParameters(cmd, account);
            cmd.Parameters.Add(PARAM_USERPWD, OracleDbType.Varchar2).Value = hashPassword;
            cmd.Parameters.Add(PARAM_BY, OracleDbType.Varchar2).Value = updateBy;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);

        }

        public void update(UserAccountInfo account, string updateBy)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_USER, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_ACTION, OracleDbType.Varchar2).Value = "UPDATE";
            this.AddParameters(cmd, account);
            cmd.Parameters.Add(PARAM_USERPWD, OracleDbType.Varchar2).Value = "";
            cmd.Parameters.Add(PARAM_BY, OracleDbType.Varchar2).Value = updateBy;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void delete(string accountId)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_DELETE_USER, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_USERID, OracleDbType.Varchar2).Value = accountId;

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void updateNewPassword(string accountId, string newPassword, string updateBy)
        {

            OracleCommand cmd = OraHelper.CreateCommand(SP_UPDATEPWD_USER, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_USERID, OracleDbType.Varchar2).Value = accountId;
            cmd.Parameters.Add(PARAM_USERPWD, OracleDbType.Varchar2).Value = newPassword;
            cmd.Parameters.Add(PARAM_BY, OracleDbType.Varchar2).Value = updateBy;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public int getDaysNotChangePassword(string p)
        {
            throw new Exception("The method or operation is not implemented.");
        }




        public ArrayList GetUser(string groupId)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_BY_Group, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_GROUP, OracleDbType.Varchar2).Value = groupId;

            return OraHelper.ExecuteQueries(this, cmd, typeof(UserAccountInfo));
        }

        #endregion
    }
}
