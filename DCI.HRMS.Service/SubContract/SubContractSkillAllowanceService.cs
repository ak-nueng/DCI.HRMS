using System;
using System.Collections.Generic;

using System.Text;
using DCI.HRMS.Persistence;
using System.Collections;
using DCI.HRMS.Model.Allowance;

namespace DCI.HRMS.Service
{
    public class SubContractSkillAllowanceService
    {

        private static SubContractSkillAllowanceService instance = new SubContractSkillAllowanceService();
        private DaoFactory emfactory = DaoFactory.Instance();
        private SubContractDaoFactory factory = SubContractDaoFactory.Instance();
        private ISkillAllowanceDao skwDao;
        private ISkillAllowanceDao masSkwDao;


        //private IEmployeeDao empDao;
        private SubContractSkillAllowanceService()
        {
            skwDao = factory.CreateSkillAllowanceDao();
            masSkwDao = emfactory.CreateSkillAllowanceDao();

        }
        public static SubContractSkillAllowanceService Instance()
        {
            return instance;
        }
        public ArrayList GetSkillAllowancwByCode(string _emoCode , DateTime _month)
        {
            try
            {
                factory.StartTransaction(true);
                return skwDao.GetSkillByCode(_emoCode,_month.ToString("MM/yyyy"));

            }
            catch
            {

                return null;
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public ArrayList GetSkillAllowancwByCode(string _emoCode)
        {
            try
            {
                factory.StartTransaction(true);
                return skwDao.GetSkillByCode(_emoCode, "%");

            }
            catch
            {

                return null;
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public CertificateInfo GetCerType(string type, int level)
        {
            try
            {
                factory.StartTransaction(true);
                return masSkwDao.GetCerType(type, level);

            }
            catch
            {

                return null;
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public ArrayList GetCertLevel(string type)
        {
            try
            {
                factory.StartTransaction(true);
                return masSkwDao.GetCertLevel(type);

            }
            catch
            {

                return null;
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public ArrayList GetAllType()
        {
            try
            {
                factory.StartTransaction(true);
                return masSkwDao.GetAllType();

            }
            catch
            {

                return null;
            }
            finally
            {
                factory.EndTransaction();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="empCode"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public ArrayList GetSkillByCode(string empCode,DateTime month)
        {
            try
            {
                factory.StartTransaction(true);
                return skwDao.GetSkillByCode(empCode,month.ToString("MM/yyyy"));

            }
            catch
            {

                return null;
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public void SaveSkillAllowance(EmpSkillAllowanceInfo empSkl)
        {
            try
            {
                factory.StartTransaction(false);
                skwDao.SaveSkillAllowance(empSkl);
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
        public void DeleteSkillAllow(string rcId)
        {
            try
            {
                factory.StartTransaction(false);
                skwDao.DeleteSkillAllow(rcId);
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
        /// Get Employee Certification.
        /// </summary>
        /// <param name="empCode">Employee Code (% = All)</param>
        /// <returns>ArrayList of EmpCertInfo</returns>
       public ArrayList GetCertificateByCode(string empCode)
        {
            try
            {
                factory.StartTransaction(true);
                return skwDao.GetCertificateByCode(empCode);

            }
            catch
            {

                return null;
            }
            finally
            {
                factory.EndTransaction();
            }
        }

      public  void SaveEmpCertificate(EmpCertInfo empCert)
        {
            try
            {
                factory.StartTransaction(false);
                skwDao.SaveEmpCertificate(empCert);
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
      public void UpdateEmpCertificate(EmpCertInfo empCert)
      {
          try
          {
              factory.StartTransaction(false);
              skwDao.UpdateEmpCertificate(empCert);
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
      public  void DeleteEmpCertificate(string rcId)
        {
            try
            {
                factory.StartTransaction(false);
                skwDao.DeleteEmpCertificate(rcId);
                factory.CommitTransaction();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {

            }

        }
    }
}
