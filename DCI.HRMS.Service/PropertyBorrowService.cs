using System;
using System.Collections.Generic;

using System.Text;
using DCI.HRMS.Persistence;
using DCI.HRMS.Model.Welfare;
using System.Collections;
using DCI.HRMS.Model.Personal;
using System.Data;

namespace DCI.HRMS.Service
{
    public class PropertyBorrowService
    {
        private static readonly PropertyBorrowService instance = new PropertyBorrowService();
        private DaoFactory factory = DaoFactory.Instance();
        private IPropertyBorrowDao prBrDao;
        private IKeyGeneratorDao keyDao;
        private IDictionaryDao dictionaryDao;
        internal PropertyBorrowService()
        {
            prBrDao = factory.CreatePropertyBorrowDao();
            keyDao = factory.CreateKeyDao();
            dictionaryDao = factory.CreateDictionaryDao();
        }
        public static PropertyBorrowService Instace()
        {
            return instance;
        }
        public string LoadRecordKey()
        {
            try
            {
                factory.StartTransaction(true);
                return keyDao.LoadUnique("PRBR").ToString(true);
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
        private string GenRecordKey()
        {
            try
            {
                return keyDao.NextId("PRBR");
            }
            catch
            {
                return null;
            }
        }
        /*
        public ArrayList GetAllProperty()
        {
            try
            {


                factory.StartTransaction(true);
                return dictionaryDao.SelectAll("PRPT");
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
        */
        public PropertyBorrowInfo GetDataById(string _brId)
        {
            try
            {


                factory.StartTransaction(true);
                return prBrDao.GetByID(_brId);
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
        public ArrayList GetData(string _empCode, string _type ,string _returnSts) 
        {
            try
            {


                factory.StartTransaction(true);
                return prBrDao.GetData(_empCode,_type,_returnSts);
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
        public ArrayList GetDataByCode(string _empCode)
        {
            try
            {


                factory.StartTransaction(true);
                return prBrDao.GetByEmpCode(_empCode);
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
        public void UpdateData(PropertyBorrowInfo _prBr)
        {
            try
            {


                factory.StartTransaction(false);
                 prBrDao.UpdateData(_prBr);
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
        public void SaveData(PropertyBorrowInfo _prBr)
        {
            try
            {


                factory.StartTransaction(false);
              //  _prBr.BorrowId = LoadRecordKey();
                 prBrDao.Savedata(_prBr);
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
        public void Deletedata(string _prId)
        {
            try
            {


                factory.StartTransaction(false);
                 prBrDao.DeleteData(_prId);
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
        public ArrayList GetAllPropertiesType()
        {
            try
            {


                factory.StartTransaction(true);
                return prBrDao.GetProperty();
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
        public void UpdateMaster(PropertyInfo _pr)
        {
            try
            {


                factory.StartTransaction(false);
                prBrDao.UpdateProperty(_pr);
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
        public void SaveMaster(PropertyInfo _pr)
        {
            try
            {


                factory.StartTransaction(false);
                prBrDao.SaveProperty(_pr);
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
        public ArrayList GetLockerMaster(string _lockerId,string _keycode,string _ecmcode)
        {
            try
            {


                factory.StartTransaction(true);
                return prBrDao.GetLockerMaster( _lockerId, _keycode, _ecmcode);
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
        public DataSet GetLockerMasterDataSet(string _lockerId, string _keycode, string _ecmcode)
        {
            try
            {


                factory.StartTransaction(true);
                return prBrDao.GetLockerMasterDataSet(_lockerId, _keycode, _ecmcode);
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
        public LockerInfo GetLockerMaster(string _lockerId)
        {
            try
            {


                factory.StartTransaction(true);
                return prBrDao.GetLockerMaster(_lockerId);
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
        public ArrayList GetLockerBorrowList(string _lockerId)
        {
            try
            {


                factory.StartTransaction(true);
                return prBrDao.GetLockerborrowData(_lockerId);
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
        public void UpdateLockerMaster(LockerInfo _locker)
        {
            try
            {


                factory.StartTransaction(false);
                prBrDao.UpdateLockerMaster(_locker);
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
        public void SaveLockerMaster(LockerInfo _locker )
        {
            try
            {


                factory.StartTransaction(false);
                prBrDao.SaveLockerMaster(_locker);
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
        public void DeleteLockerMaster(string  _lockerId)
        {
            try
            {


                factory.StartTransaction(false);
                prBrDao.DeleteLockerMaster(_lockerId);
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
