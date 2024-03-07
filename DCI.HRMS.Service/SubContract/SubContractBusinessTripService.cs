using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using DCI.HRMS.Model;
using DCI.HRMS.Persistence;
using System.Diagnostics;
using DCI.HRMS.Model.Attendance;


namespace DCI.HRMS.Service.SubContract
{
    public class SubContractBusinessTripService
    {
        /*
        private static readonly SubContractBusinessTripService instance = new SubContractBusinessTripService();

        private const string SHIFT_TYPE = "SHFT";

        private SubContractDaoFactory factory = SubContractDaoFactory.Instance();
        private IShiftDao shiftDao;
        private IDictionaryDao dictionaryDao;

        internal SubContractBusinessTripService()
        {
            shiftDao = factory.CreateShiftDao();
            dictionaryDao = factory.CreateDictionaryDao();
        }

        public static SubContractBusinessTripService Instance()
        {
            return instance;
        }

        */

        //***************

        private static readonly SubContractBusinessTripService instance = new SubContractBusinessTripService();
        private SubContractDaoFactory factory = SubContractDaoFactory.Instance();
        private IBusinessTripDao busTripDao;
        private IDictionaryDao dictionaryDao;
        internal SubContractBusinessTripService()
        {
            busTripDao = factory.CreateBusinessTripDao();
            dictionaryDao = factory.CreateDictionaryDao();
        }
        public static SubContractBusinessTripService Instance()
        {
            return instance;
        }

        public ArrayList GetBusinessTripInfo(string empcode, DateTime fDate,DateTime tDate)
        {
            try
            {
                factory.StartTransaction(true);
                return busTripDao.GetBusinessTrip(empcode, fDate,tDate);

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
        public BusinesstripInfo GetBusinessTripInfo(string empcode, DateTime fDate)
        {
            try
            {
                factory.StartTransaction(true);
                return busTripDao.GetBusinessTrip(empcode, fDate);

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





        public void SaveBusinesstripInfo(BusinesstripInfo tmrq)
        {

            try
            {
                factory.StartTransaction(false);
                busTripDao.SaveBusinessTrip(tmrq);
                factory.CommitTransaction();
            }
            catch
            {
                throw;
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public void UpdateBusinesstripInfo(BusinesstripInfo tmrq)
        {

            try
            {
                factory.StartTransaction(false);
                busTripDao.UpdateBusinessTrip(tmrq);
                factory.CommitTransaction();
            }
            catch
            {
                throw;
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public void DeleteBusinesstripInfo(BusinesstripInfo tmrq)
        {

            try
            {
                factory.StartTransaction(false);
                busTripDao.DeteteBusinessTrip(tmrq);
                factory.CommitTransaction();
            }
            catch
            {
                throw;
            }
            finally
            {
                factory.EndTransaction();
            }
        }

        //***************





    }
}
