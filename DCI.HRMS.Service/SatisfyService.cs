using System;
using System.Collections.Generic;
using System.Text;
using DCI.HRMS.Persistence;
using DCI.HRMS.Model.Satisfy;
using System.Collections;

namespace DCI.HRMS.Service
{
    public class SatisfyService
    {

        private static readonly SatisfyService instance = new SatisfyService();
        private IKeyGeneratorDao keyDao;

        private DaoFactory factory = DaoFactory.Instance();
        private ISatisfyDao stfDao;
 

        internal SatisfyService()
        {
            stfDao = factory.CreareSatisfyDao();
            keyDao = factory.CreateKeyDao();
        }

        public static SatisfyService Instance()
        {
            return instance;
        }
        public string LoadRecordKey()
        {
            try
            {
                factory.StartTransaction(true);
                return keyDao.LoadUnique("STF").ToString(true);
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
        public string LoadmainRecordKey()
        {
            try
            {
                factory.StartTransaction(true);
                return keyDao.LoadUnique("STFM").ToString(true);
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



        public SatisfyMainInfo GetActiveMainSatisfy()
        {
            try
            {
                factory.StartTransaction(true);

                return stfDao.SelectActiveMainMaster();

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
        public SatisfyMainInfo GetSatisfyMainMaster(string stfMainId)
        {
            try
            {
                factory.StartTransaction(true);

                return stfDao.SelestSatisfyMainMaster(stfMainId);

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
        public ArrayList SelectSatisfyMainMaster()
        {
            try
            {
                factory.StartTransaction(true);

                return stfDao.SelestSatisfyMainMaster();

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
        public void SaveSatisfyMainMaster(SatisfyMainInfo stfMain)
        {
            try
            {
                factory.StartTransaction(true);
                stfDao.SaveSatisfyMainMaster(stfMain);
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
        public void UpdateSatifyMainMsater(SatisfyMainInfo stfMain)
        {
            try
            {
                factory.StartTransaction(true);
                stfDao.UpdateSatifyMainMsater(stfMain);
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
        public void DelectSatifyMainMaster(string stfMainid)
        {
            try
            {
                factory.StartTransaction(true);
                stfDao.DelectSatifyMainMaster(stfMainid);
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













        public ArrayList SelectActiveSatisfy(string stfMainId)
        {
            try
            {
                factory.StartTransaction(true);

                return stfDao.SelectActiveSatisfy(stfMainId);

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
        public SatisfyMasterInfo SelestSatisfyMaster(string stfMainId, string stfId)
        {
            try
            {
                factory.StartTransaction(true);

                return stfDao.SelestSatisfyMaster(stfMainId,stfId);

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
        public ArrayList SelectSatisfyMaster(string stfMainId)
        {
            try
            {
                factory.StartTransaction(true);

                return stfDao.SelectSatisfyMaster(stfMainId);

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
        public void SaveSatisfyMaster(SatisfyMasterInfo stf)
        {
            try
            {
                factory.StartTransaction(true);
                stfDao.SaveSatisfyMaster(stf);
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
        public void UpdateSatifyMsater(SatisfyMasterInfo stf)
        {
            try
            {
                factory.StartTransaction(true);
                stfDao.UpdateSatifyMsater(stf);
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
        public void DelectSatifyMaster(string stfid)
        {
            try
            {
                factory.StartTransaction(true);
                stfDao.DelectSatifyMaster(stfid);
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



        public ArrayList SelestSatisfy(string stfId)
        {

            try
            {
                factory.StartTransaction(true);

                return stfDao.SelestSatisfy(stfId);

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
        public SatisfyDataInfo SelestSatisfy(string stfId, string empCode)
        {
           
            try
            {
                factory.StartTransaction(true);

                return stfDao.SelestSatisfy(stfId, empCode);

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
        public ArrayList SelectSatisfy(string stfId, int choice)
        {

            try
            {
                factory.StartTransaction(true);

                return stfDao.SelectSatisfy(stfId, choice);

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
        public void SaveSatisfy(SatisfyDataInfo stf)
        {
            try
            {
                factory.StartTransaction(true);
                stfDao.SaveSatisfy(stf);
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
        public void UpdateSatify(SatisfyDataInfo stf)
        { try
            {
                factory.StartTransaction(true);
                stfDao.UpdateSatify(stf);
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
