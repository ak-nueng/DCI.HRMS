using System;
using System.Collections.Generic;
using System.Text;
using DCI.HRMS.Persistence;
using System.Collections;
using DCI.HRMS.Model.Common;
using DCI.HRMS.Model;
using DCI.HRMS.Model.Welfare;
using DCI.HRMS.Model.Allowance;

namespace DCI.HRMS.Service
{

    public class LawResponseService
    {
        private static readonly LawResponseService instance = new LawResponseService();
        private DaoFactory factory = DaoFactory.Instance();
        private ILawResponseDao lawRespDao;
        private LawResponseService()
        {
           lawRespDao = factory.CreateLawResponseDao();
        }
        public static LawResponseService Instance()
        {
            return instance;
        }

        public ArrayList GetLawResponseMaster()
        {
            try
            {
                factory.StartTransaction(true);
                return lawRespDao.SelectAllMaster();

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
        public ArrayList GetLawResponseGroupMaster()
        {
            try
            {
                factory.StartTransaction(true);
                return lawRespDao.SelectAllGroupMaster();

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
        public ArrayList SelectLawResponseGroupMaster(string grpType)
        {
            try
            {
                factory.StartTransaction(true);
                return lawRespDao.SelectGroupMaster(grpType);

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
        public LawResponseGroupInfo GetLawResponseGroupMaster(string id)
        {
            try
            {
                factory.StartTransaction(true);
                return lawRespDao.GetGroupMasterUnq(id);

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
        public void SaveLawResponseGroupMaster(LawResponseGroupInfo item)
        {
            try
            {
                factory.StartTransaction(false);
                lawRespDao.SaveGroupMaster(item);
                factory.CommitTransaction();
            }
            catch( Exception ex )
            {
                throw ex;
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public void UpdateLawResponseGroupMaster(LawResponseGroupInfo item)
        {
            try
            {
                factory.StartTransaction(false);
                lawRespDao.UpdateGroupMaster(item);
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
        public void DeleteLawResponseGroupMaster(string id)
        {
            try
            {
                factory.StartTransaction(false);
                lawRespDao.DeleteGroupMaster(id);
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
        public ArrayList SelectLawResponseMaster(string groupid)
        {
            try
            {
                factory.StartTransaction(true);
                return lawRespDao.SelectAllMaster(groupid);

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
        public LawResponseInfo GetLawResponseMasterUnq(string resId)
        {
            try
            {
                factory.StartTransaction(true);
                return lawRespDao.GetMaster(resId);

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
       
        public void SaveLawResponseMaster(LawResponseInfo item)
        {
            try
            {
                factory.StartTransaction(false);
                lawRespDao.SaveMaster(item);
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
        public void UpdateLawResponseMaster(LawResponseInfo item)
        {
            try
            {
                factory.StartTransaction(false);
                lawRespDao.UpdateMaster(item);
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
        public void DeleteLawResponseMaster(string id)
        {
            try
            {
                factory.StartTransaction(false);
                lawRespDao.DeleteMaster(id);
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
        public EmpLawResponseInfo GetEmpLawResponse(string resId, string empCode)
        {
            try
            {
                factory.StartTransaction(true);

                return lawRespDao.SelectEmpResponse(resId, empCode)[0] as EmpLawResponseInfo;

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
        public ArrayList SelectEmpLawResponse(string resId)
        {
            try
            {
                factory.StartTransaction(true);
                return lawRespDao.SelectEmpResponse(resId, "%");
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
        public ArrayList SelectEmpLawResponseByCode(string code )
        {
            try
            {
                factory.StartTransaction(true);
                return lawRespDao.SelectEmpResponse("%", code);
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
        public void SaveEmpLawResponse(EmpLawResponseInfo item)
        {
            try
            {
                factory.StartTransaction(false);
                lawRespDao.SaveEmpResponse(item);
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
        public void UpdateEmpLawResponse(EmpLawResponseInfo item)
        {
            try
            {
                factory.StartTransaction(false);
                lawRespDao.UpdateEmpResponse(item);
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
        public void DeleteEmpLawResponse(string resId,string empCode)
        {
            try
            {
                factory.StartTransaction(false);
                lawRespDao.DeleteEmpResponse(resId,empCode);
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
