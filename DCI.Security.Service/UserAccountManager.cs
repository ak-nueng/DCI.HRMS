using System;
using System.Collections.Generic;
using System.Text;
using DCI.Security.Model;
using DCI.Security.Persistence;
using DCIBizPro.Util.Cryptography;
using DCIBizPro.Util.Diagnostics;
using System.Collections;
//using DCIBizPro.DataAccess.Db;
//using DCIBizPro.DataAccess.SM;
//using DCIBizPro.DTO.SM;
//using DCIBizPro.Util.Cryptography;
//using DCIBizPro.Util.Diagnostics;

namespace DCI.Security.Service
{
	/// <summary>
	/// การจัดการบัญชีผู้ใช้งานในระบบ
	/// </summary>
	public class UserAccountManager
	{
        private static UserAccountManager Instanse = new UserAccountManager();
        private DaoFactory factory = DaoFactory.Instance();
        private IUserAccountDao userAccountDao;
        private IUserGroupDao userGroupDao;


        public UserAccountManager()
        {
          
        
            userAccountDao = factory.CreateUserAccountDao();
            userGroupDao = factory.CreateUserGroupDao();
        
        }
		/// <summary>
		/// เพิ่มผู้ใช้งาน
		/// </summary>
		/// <param name="account"></param>
		/// <param name="password"></param>
		/// <param name="updateBy"></param>
		public  void add(UserAccountInfo account, string password, string updateBy)
		{
			
			try
			{
                factory.StartTransaction(true);

				string hashPassword = Encrypt.HashPassword(password);

               // userAccountDao accountDAO = new UserAccountDAO(repository.Session);
                userAccountDao.insert(account, hashPassword, updateBy);

                factory.CommitTransaction();
			}
			catch (Exception ex)
			{
				

				EventLogHelper.logError("Add user account occured error : " + ex.Message);
				throw new Exception(string.Format("ไม่สามารถสร้าง Account ได้ \nDetail\n{0}", ex.Message));
			}
			finally
			{
                factory.EndTransaction();
            
			}
		}

		/// <summary>
		/// บันทึกข้อมูลผู้ใช้งาน
		/// </summary>
		/// <param name="account"></param>
		/// <param name="updateBy"></param>
		public void save(UserAccountInfo account, string updateBy)
		{
			try
			{
                factory.StartTransaction(true);

				//UserAccountDAO accountDAO = new UserAccountDAO(repository.Session);
                userAccountDao.update(account, updateBy);

				factory.CommitTransaction();
			}
			catch (Exception ex)
			{
			     EventLogHelper.logError("Save user account occured error : " + ex.Message);
				throw new Exception(string.Format("ไม่สามารถบันทึกข้อมูลได้ \nDetail\n{0}", ex.Message));
			}
			finally
			{
                factory.EndTransaction();
			}
		}

		/// <summary>
		/// ลบบัญชีผู้ใช้ออกจากระบบ
		/// </summary>
		/// <param name="accountId"></param>
		public  void remove(string accountId)
		{
			
			try
			{
				//repository.beginTransaction();
                factory.StartTransaction(true);
				//UserAccountDAO accountDAO = new UserAccountDAO(repository.Session);
                userAccountDao.delete(accountId);

                factory.CommitTransaction();
			}
			catch (Exception ex)
			{
				//repository.rollbackTransaction();
				EventLogHelper.logError("Remove user account occured error : " + ex.Message);
				throw ex;
			}
			finally
			{
                factory.EndTransaction();
			}
		}
        /// <summary>
        /// เปลี่ยนรหัสผ่าน เมื่อ Logon ครั้งต่อไป
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="password"></param>
        /// <param name="changeAtNextLogon"></param>
        /// <param name="updateBy"></param>
        public void changePassword(string accountId, string password, bool changeAtNextLogon, string updateBy)
        {


            try
            {
                //repository.beginTransaction();
                factory.StartTransaction(true);
                password = Encrypt.HashPassword(password);

                //UserAccountDAO accountDAO = new UserAccountDAO(repository.Session);
                UserAccountInfo accountInfo = userAccountDao.Select(accountId);

                if (accountInfo != null)
                {
                    userAccountDao.updateNewPassword(accountInfo.AccountId, password, updateBy);
                    changePasswordAtNextLogon(changeAtNextLogon, accountInfo, userAccountDao);
                }
                else
                {
                    throw new Exception(string.Format("ไม่พบบัญชีผู้ใช้ {0} ในระบบ", accountId));
                }
                //repository.commitTransaction();
                factory.CommitTransaction();
            }
            catch (Exception ex)
            {
                //repository.rollbackTransaction();
                throw ex;
            }
            finally
            {
                factory.EndTransaction();
            }
        }

		/// <summary>
		/// เปลี่ยนรหัสผ่านใหม่
		/// </summary>
		/// <param name="accountId"></param>
		/// <param name="oldPassword"></param>
		/// <param name="newPassword"></param>
		/// <param name="updateBy"></param>
        public void changePassword(string accountId, string oldPassword, string newPassword, string updateBy)
        {

            try
            {
                factory.StartTransaction(true);

                oldPassword = Encrypt.HashPassword(oldPassword);
                newPassword = Encrypt.HashPassword(newPassword);

                //	IUserAccountDAO accountDAO = new UserAccountDAO(repository.Session);
                UserAccountInfo accountInfo = new UserAccountInfo();

                try
                {
                    accountInfo = userAccountDao.Select(accountId, oldPassword);
                }
                catch (Exception)
                {

                    throw new Exception("ชื่อผู้ใช้ หรือ รหัสผ่าน ไม่ถูกต้อง");
                }
              
                userAccountDao.updateNewPassword(accountId, newPassword, updateBy);
             

                factory.CommitTransaction();
            }
            catch (Exception ex)
            {
                //repository.rollbackTransaction();
                EventLogHelper.logError("Change Password occured error : " + ex.Message);
                throw ex;
            }
            finally
            {
                factory.EndTransaction();
            }
        }

		/// <summary>
		/// ค้นหาบัญชีผู้ใช้
		/// </summary>
		/// <param name="accountId"></param>
		/// <returns></returns>
		public  UserAccountInfo findUserAccount(string accountId)
		{
			try
            {
                factory.StartTransaction(true);
				//UserAccountDAO accountDAO = new UserAccountDAO(repository.Session);
                UserAccountInfo accountInfo = userAccountDao.Select(accountId);
                accountInfo.UserGroup = userGroupDao.Select(accountInfo.UserGroup.ID);
				return accountInfo;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
                factory.EndTransaction();
			}
		}
        public ArrayList findUserAccountByGroup(string groupId)
        {
            try
            {
                //UserAccountDAO accountDAO = new UserAccountDAO(repository.Session);

                factory.StartTransaction(true);
                ArrayList usr = userAccountDao.GetUser(groupId);
          
                foreach (UserAccountInfo item in usr)
                {
                    item.UserGroup = userGroupDao.Select(item.UserGroup.ID);
                }

                return usr;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                factory.EndTransaction();
            }
        }

	
		private  void changePasswordAtNextLogon(bool changeAtNextLogon, UserAccountInfo accountInfo, IUserAccountDao accountDAO)
		{
			if (changeAtNextLogon)
			{
				accountInfo.ChangePasswordAtNextLogon = false;
				accountDAO.update(accountInfo, "SYSTEM");
			}
		}

		/// <summary>
		/// ตั้งรหัสผ่านให้ใหม่
		/// </summary>
		/// <param name="accountId"></param>
		/// <param name="password"></param>
		/// <param name="updateBy"></param>
		public void changePassword(string accountId, string password, string updateBy)
		{
			//Repository repository = new Repository();
			//repository.open();

			try
			{
				//repository.beginTransaction();
                factory.StartTransaction(true);
				password = Encrypt.HashPassword(password);

				//UserAccountDAO accountDAO = new UserAccountDAO(repository.Session);
				userAccountDao.updateNewPassword(accountId, password, updateBy);

                factory.CommitTransaction();
			}
			catch (Exception ex)
			{
				    
				throw ex;
			}
			finally
			{
                factory.EndTransaction();
			}
		}

		/// <summary>
		/// เช็คการหมดอายุของรหัสผ่าน
		/// </summary>
		/// <param name="accountInfo"></param>
		/// <returns></returns>
		public bool passwordExpire(UserAccountInfo accountInfo)
		{
			bool isExpire = false;

			//Repository repository = new Repository();
			//repository.open();

			try
			{
				if (!accountInfo.PasswordNeverExpires)
				{
					//IUserAccountDao accountDAO = new UserAccountDAO(repository.Session);
					isExpire = passwordExpired( accountInfo);
				}

			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
                factory.EndTransaction();
			}
			return isExpire;
		}

        private bool passwordExpired( UserAccountInfo accountInfo)
		{
			bool isExpire = false;
            TimeSpan tm = DateTime.Today - accountInfo.PasswordLastChange;
			int days = tm.Days ;

			if (days >= UserAccountManager.getPasswordExpiryWarningDay())
			{
				isExpire = true;
			}
			return isExpire;
		}

		/// <summary>
		/// หาจำนวนวันที่กำหนดไว้เพื่อเปลี่ยนรหัสผ่าน
		/// </summary>
		/// <returns></returns>
		private static int getPasswordExpiryWarningDay()
		{
			int days;

			try
			{
                days = 60;// Convert.ToInt32(ConfigurationSettings.AppSettings["PasswordExpiryWarning"]);
			}
			catch
			{
				days = 30;
			}

			return days;
		}

       
    }
}