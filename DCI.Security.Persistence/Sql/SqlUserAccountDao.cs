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
    public class SqlUserAccountDao : DaoBase , IUserAccountDao
    {
        private const string SP_SELECT_BY_PWD = "sp_UserAuthentication";
        private const string SP_SELECT = "sp_User_Select";
        private const string SP_SELECT_BY_Group = "sp_User_Selectbygroup";

        private const string SP_STORE_USER = "sp_user_store";
        private const string SP_DELETE_USER = "sp_user_delete";
        private const string SP_UPDATEPWD_USER = "sp_user_udatepassword";
        private const string PARAM_USER = "@User";
        private const string PARAM_PASSWORD = "@Password";
        private const string PARAM_GROUP = "@p_grpid";

        private const string PARAM_ACTION = "@p_action";
        private const string PARAM_USERID = "@p_userid";
        private const string PARAM_FULLNAME = "@p_fullname";
        private const string PARAM_DESC = "@p_userdescription";
        private const string PARAM_EMAIL = "@p_email";
        private const string PARAM_GRPID = "@p_usergroupid";
        private const string PARAM_ENABLE = "@p_enable";
        private const string PARAM_PWDCHANGE = "@p_pwdchange";
        private const string PARAM_PWDNOTCHANGE = "@p_pwdnotchange";
        private const string PARAM_PWDNOTEXP = "@p_pwdnotexpire";
        private const string PARAM_USERPWD = "@p_userpwd  ";
        private const string PARAM_BY = "@p_by";



        public SqlUserAccountDao(DaoManager daoManager)
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
                    item.ChangePasswordAtNextLogon = Convert.ToBoolean(this.Parse(row,"PwdChange"));
                }
                catch { }
                try
                {
                    item.PasswordLastChange = Convert.ToDateTime(this.Parse(row,"PwdLastChange"));
                }
                catch{}
                try
                {
                    item.CannotChangePassword = Convert.ToBoolean(this.Parse(row,"PwdNotChange"));
                }catch{}
                try
                {
                    item.PasswordNeverExpires = Convert.ToBoolean(this.Parse(row,"PwdNotExpire"));
                }catch{}

                try
                {
                    item.AccountId = (string)this.Parse(row,"UserId");
                }catch{}
                
                try
                {
                    item.FullName = (string)this.Parse(row,"FullName");
                }catch{}

                try{
                    item.Description = (string)this.Parse(row,"UserDesc");
                }catch{}

                try
                {
                    item.UserGroup.ID = Convert.ToInt32(this.Parse(row,"UserGroupId"));
                }
                catch{}
                
                try{
                    item.Email = (string)this.Parse(row,"Email");
                }
                catch{}
                
                try
                {
                    item.Enable = Convert.ToBoolean(this.Parse(row,"Enable"));
                }
                catch{}

                return item;
            }
            return null;
        }

        public override void AddParameters(IDbCommand cmd, object obj)
        {

            SqlCommand oraCmd = (SqlCommand)cmd;
            if (obj is UserAccountInfo)
            {
                UserAccountInfo item = (UserAccountInfo)obj;

                oraCmd.Parameters.Add(PARAM_USERID, SqlDbType.VarChar).Value = item.AccountId;
                oraCmd.Parameters.Add(PARAM_FULLNAME, SqlDbType.VarChar).Value = item.FullName;
                oraCmd.Parameters.Add(PARAM_DESC, SqlDbType.VarChar).Value = item.Description;
                oraCmd.Parameters.Add(PARAM_EMAIL, SqlDbType.VarChar).Value = item.Email;
                oraCmd.Parameters.Add(PARAM_GRPID, SqlDbType.VarChar).Value = item.UserGroup.ID;
                oraCmd.Parameters.Add(PARAM_ENABLE, SqlDbType.VarChar).Value = item.Enable.ToString();
                oraCmd.Parameters.Add(PARAM_PWDCHANGE, SqlDbType.VarChar).Value = item.ChangePasswordAtNextLogon.ToString();
                oraCmd.Parameters.Add(PARAM_PWDNOTCHANGE, SqlDbType.VarChar).Value = item.CannotChangePassword.ToString();
                oraCmd.Parameters.Add(PARAM_PWDNOTEXP, SqlDbType.VarChar).Value = item.PasswordNeverExpires.ToString();
            }
        }

        #region IUserAccountDao Members

        public UserAccountInfo Select(string userId)
        {
            SqlCommand cmd = SqlHelper.CreateCommand(SP_SELECT, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_USER, SqlDbType.VarChar).Value = userId;
            
            return (UserAccountInfo)SqlHelper.ExecuteQuery(this, cmd, typeof(UserAccountInfo));
        }
        public UserAccountInfo Select(string userId , string password)
        {
            SqlCommand cmd = SqlHelper.CreateCommand(SP_SELECT_BY_PWD, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_USER, SqlDbType.VarChar).Value = userId;
            cmd.Parameters.Add(PARAM_PASSWORD, SqlDbType.VarChar).Value = password;

            return (UserAccountInfo)SqlHelper.ExecuteQuery(this, cmd, typeof(UserAccountInfo));
        }
        public void KeepLog(string user, string moduleId, string fromMachine, string action, string description)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_User_LogFile";

            cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = user;
            cmd.Parameters.Add("@ModId", SqlDbType.VarChar).Value = moduleId;
            cmd.Parameters.Add("@Computer", SqlDbType.VarChar).Value = fromMachine;
            cmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = action;
            cmd.Parameters.Add("@Descr", SqlDbType.VarChar).Value = description;

            SqlHelper.ExecuteNonQuery(this.Transaction, cmd);
        }



        public void insert(UserAccountInfo account, string hashPassword, string updateBy)
        {
            SqlCommand cmd = SqlHelper.CreateCommand(SP_STORE_USER, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_ACTION, SqlDbType.VarChar).Value = "ADD";
            this.AddParameters(cmd, account);
            cmd.Parameters.Add(PARAM_USERPWD, SqlDbType.VarChar).Value = hashPassword;
            cmd.Parameters.Add(PARAM_BY, SqlDbType.VarChar).Value = updateBy;
            SqlHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void update(UserAccountInfo account, string updateBy)
        {

            SqlCommand cmd = SqlHelper.CreateCommand(SP_STORE_USER, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_ACTION, SqlDbType.VarChar).Value = "UPDATE";
            this.AddParameters(cmd, account);
            cmd.Parameters.Add(PARAM_USERPWD, SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add(PARAM_BY, SqlDbType.VarChar).Value = updateBy;
            SqlHelper.ExecuteNonQuery(this.Transaction, cmd);
        }
        
        public void delete(string accountId)
        {
            SqlCommand cmd = SqlHelper.CreateCommand(SP_DELETE_USER, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_USERID, SqlDbType.VarChar).Value = accountId;

            SqlHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void updateNewPassword(string accountId, string newPassword, string updateBy)
        {

            SqlCommand cmd = SqlHelper.CreateCommand(SP_UPDATEPWD_USER, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_USERID, SqlDbType.VarChar).Value = accountId;
            cmd.Parameters.Add(PARAM_USERPWD, SqlDbType.VarChar).Value = newPassword;
            cmd.Parameters.Add(PARAM_BY, SqlDbType.VarChar).Value = updateBy;
            SqlHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public int getDaysNotChangePassword(string p)
        {
            throw new Exception("The method or operation is not implemented.");
        }

       

        public ArrayList GetUser(string groupId)
        {
            SqlCommand cmd = SqlHelper.CreateCommand(SP_SELECT_BY_Group, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_GROUP, SqlDbType.VarChar).Value = groupId;

            return SqlHelper.ExecuteQueries(this, cmd, typeof(UserAccountInfo));
        }

        #endregion
    }
}
