using System;
using System.Collections.Generic;
using System.Text;
using DCI.HRMS.Persistence;
using System.Collections;
using DCI.HRMS.Model;

namespace DCI.HRMS.Service
{
    public class DictionaryService
    {
        private static readonly DictionaryService instance = new DictionaryService();
        private DaoFactory factory = DaoFactory.Instance();
        private IDictionaryDao dictionaryDao;

        internal DictionaryService()
        {
            dictionaryDao = factory.CreateDictionaryDao();

        }

        public static DictionaryService Instance()
        {
            return instance;
        }
        public ArrayList SelectAll(string key)
        {
            try
            {
                factory.StartTransaction(true);
                return dictionaryDao.SelectAll(key);
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
        public BasicInfo Select(string type, string code)
        {
            try
            {
                factory.StartTransaction(true);
                return dictionaryDao.Select(type, code);
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
                return dictionaryDao.SelectAllType();
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

        public void InsertData(BasicInfo _data)
        {
            try
            {
                factory.StartTransaction(true);
                dictionaryDao.SaveDictData(_data);
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
        public void UpdateData(BasicInfo _data)
        {
            try
            {
                factory.StartTransaction(true);
                dictionaryDao.UpdateDictData(_data);
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
        public void DeleteData(string type, string code)
        {
            try
            {
                factory.StartTransaction(true);
                dictionaryDao.DeleteDictData(type, code);
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
