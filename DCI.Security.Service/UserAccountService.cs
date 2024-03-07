using System;
using System.Collections.Generic;
using System.Text;
using DCI.Security.Model;
using DCI.Security.Persistence;
using DCIBizPro.Util.Cryptography;

namespace DCI.Security.Service
{
    public class UserAccountService
    {
        private readonly static UserAccountService instance = new UserAccountService();
        private DaoFactory factory = DaoFactory.Instance();
        private IUserAccountDao userAccountDao;
        private IUserGroupDao userGroupDao;

        private UserAccountService()
        {
            userAccountDao = factory.CreateUserAccountDao();
            userGroupDao = factory.CreateUserGroupDao();
        }
        public static UserAccountService Instance()
        {
            return instance;
        }
        public UserAccountInfo Authentication(string userId, string password)
        {
            try
            {
                factory.StartTransaction(true);

                UserAccountInfo user = userAccountDao.Select(userId , Encrypt.HashPassword(password));
                user.UserGroup = userGroupDao.Select(user.UserGroup.ID);

                CheckUserAvailable(user);
                return user;
            }
            catch {   throw;

               // return null;
            
            }
            finally
            {
                factory.EndTransaction();
            }
        }

        public void CheckUserAvailable(UserAccountInfo user)
        {
            if(!user.Enable)
                throw new Exception("Your account is not available now. please , contact your administrator.");

            UserGroupService.Instance().CheckUserGroupAvailable(user.UserGroup);
        }
        public void KeepLogInLog(string user, string fromMachine)
        {
            try
            {
                factory.StartTransaction(true);

                userAccountDao.KeepLog(user,"USR_LOGON", fromMachine,"Login", "Success");

                factory.CommitTransaction();
            }
            catch
            {
                factory.EndTransaction();
                //throw;
            }



        }
        public void KeepLogInFail(string user, string fromMachine)
        {
            try
            {
                factory.StartTransaction(true);

                userAccountDao.KeepLog(user, "USR_LOGON", fromMachine, "Login", "Fail");

                factory.CommitTransaction();
            }
            catch
            {
                factory.EndTransaction();
                //throw;
            }



        }
        public void KeepLog(string user,string module,  string fromMachine,string action,string desc)
        {
            try
            {
                factory.StartTransaction(true);

                userAccountDao.KeepLog(user, module, fromMachine, action, desc);

                factory.CommitTransaction();
            }
            catch
            {
                factory.EndTransaction();
                //throw;
            }



        }
    }
}
