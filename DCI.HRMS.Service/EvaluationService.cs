using System;
using System.Collections.Generic;
using System.Text;
using DCI.HRMS.Persistence;
using System.Collections;
using DCI.HRMS.Model.Evaluation;

namespace DCI.HRMS.Service
{
    public class EvaluationService
    {
        private static EvaluationService instance = new EvaluationService();


        private DaoFactory factory = DaoFactory.Instance();
        private IEvaluationDao evaDao;
        private EvaluationService()
        {
            evaDao = factory.CreateEvaluationDao();
        }
        public static EvaluationService Instance()
        {
            return instance;
        }

        public ArrayList GetEvaBonus(string _code , string  _year)
        {
            try
            {

                factory.StartTransaction(true);
                return evaDao.GetBonusInfo(_code, _year);
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
        public Eva_BonusInfo GetEvaBonusUnq(string _code, string _year)
        {
            try
            {

                factory.StartTransaction(true);
                return evaDao.GetBonusInfoUnq(_code, _year);
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
        public ArrayList GetEvaSalary(string _code, string _year)
        {
            try
            {

                factory.StartTransaction(true);
                return evaDao.GetSalaryInfo(_code, _year);
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
        public Eva_SalaryInfo GetEvaSalaryUnq(string _code, string _year)
        {
            try
            {

                factory.StartTransaction(true);
                return evaDao.GetSalaryInfoUnq(_code, _year);
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
        public void SaveEvaSalaryInfo(Eva_SalaryInfo _evaSalary)
        {
            try
            {
                factory.StartTransaction(false);
                //  rq.DocId = GenRecordKey();
                evaDao.SaveSalaryInfo(_evaSalary);
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
        public void UpdateEvaSalaryInfo(Eva_SalaryInfo _evaSalary)
        {
            try
            {
                factory.StartTransaction(true);
                evaDao.UpdateSalaryInfo(_evaSalary);
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
        public void SaveEvaBonusInfo(Eva_BonusInfo _evaBonus)
        {
            try
            {
                factory.StartTransaction(false);
                //  rq.DocId = GenRecordKey();
                evaDao.SaveBonusInfo(_evaBonus);
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
        public void UpdateEvaBonusInfo(Eva_BonusInfo _evaBonus)
        {
            try
            {
                factory.StartTransaction(true);
                evaDao.UpdateBonusInfo(_evaBonus);
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
    }
}
