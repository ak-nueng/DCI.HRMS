using System;
using System.Collections.Generic;

using System.Text;
using DCI.HRMS.Persistence;
using System.Collections;
using DCI.HRMS.Model.Allowance;

namespace DCI.HRMS.Service
{
    public class SkillAllowanceService
    {

        private static SkillAllowanceService instance = new SkillAllowanceService();
        private DaoFactory factory = DaoFactory.Instance();
        private ISkillAllowanceDao skwDao;


        //private IEmployeeDao empDao;
        private SkillAllowanceService()
        {
            skwDao = factory.CreateSkillAllowanceDao();

        }
        public static SkillAllowanceService Instance()
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
                return skwDao.GetCerType(type, level);

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
                return skwDao.GetCertLevel(type);

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
                return skwDao.GetAllType();

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
        public void SaveCerType(CertificateInfo sklMstr)
        {
            try
            {
                factory.StartTransaction(false);
                skwDao.SaveCerType(sklMstr);
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
        public void UpdateCerType(CertificateInfo sklMstr)
        {
            try
            {
                factory.StartTransaction(false);
                skwDao.UpdateCerType(sklMstr);
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
        public void DeleteCerType(string type, int level)
        {
            try
            {
                factory.StartTransaction(false);
                skwDao.DeleteCerType(type, level);
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
