using System;
using System.Collections.Generic;
using System.Text;
using DCI.HRMS.Persistence;
using System.Collections;
using DCI.HRMS.Model.Attendance;

namespace DCI.HRMS.Service
{
   public class PenaltyService
    {
       private static readonly PenaltyService instanse = new PenaltyService();
       private DaoFactory factory = DaoFactory.Instance();
       private IPenaltyDao penDao;
       private IKeyGeneratorDao keyDao;
       private IDictionaryDao dictDao;

       internal PenaltyService()
       {
           penDao = factory.CreatePenaltyDao();
           keyDao = factory.CreateKeyDao();
           dictDao = factory.CreateDictionaryDao();
       }
       public static PenaltyService Instanse()
       {
           return instanse;
       }
       public ArrayList SelectPenaltyType()
       {
           try
           {
               factory.StartTransaction();
               return dictDao.SelectAll("PENA");
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
       public string LoadRecordKey()
       {
           try
           {
               factory.StartTransaction(true);
               return keyDao.LoadUnique("PENA").ToString(true);
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
       public ArrayList GetPenalty(string empCode, DateTime pDate, DateTime tDate)
       {
           try
           {
               factory.StartTransaction(true);
               return penDao.GetPenalty(empCode, pDate, tDate);
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
       public ArrayList SelectPenalty(string empCode,string penType, DateTime pDate, DateTime tDate)
       {
           try
           {
               factory.StartTransaction(true);
               return penDao.SelectPenalty(empCode,penType, pDate, tDate);
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
       public ArrayList GetPenaltyByCode(string empCode)
       {
           try
           {
               factory.StartTransaction(true);
               return penDao.GetPenalty(empCode);
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
       public void SavePenalty(PenaltyInfo pen)
       {
           try
           {
               factory.StartTransaction(false);
               penDao.SavePenalty(pen);
               factory.CommitTransaction();
           }
           catch( Exception ex)
           {

               throw ex;
           }
           finally
           {
               factory.EndTransaction();
           }
       }
       public void UpdatePenalty(PenaltyInfo pen)
       {

           try
           {
               factory.StartTransaction(false);
               penDao.UpdatePenalty(pen);
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
       public void DelatePenalty(PenaltyInfo pen)
       {

           try
           {
               factory.StartTransaction(false);
               penDao.Delete(pen);
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
