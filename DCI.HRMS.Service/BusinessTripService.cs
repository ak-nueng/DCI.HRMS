using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using DCI.HRMS.Persistence;
using DCI.HRMS.Model.Attendance;

namespace DCI.HRMS.Service
{
    public class BusinessTripService
    {
        private static readonly BusinessTripService instance = new BusinessTripService();
        private DaoFactory factory = DaoFactory.Instance();
        private IBusinessTripDao busTripDao;
        private IDictionaryDao tmrqType;
        internal BusinessTripService()
        {
            busTripDao = factory.CreateBusinessTripDao();
            tmrqType = factory.CreateDictionaryDao();
        }
        public static BusinessTripService Instance()
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
       

    }
}
