using System;
using System.Collections.Generic;
using System.Text;
using DCI.HRMS.Model.Organize;
using DCI.HRMS.Persistence;
using DCI.HRMS.Model;
using System.Collections;

namespace DCI.HRMS.Service
{
 

   public class PositionService
    { 
       private static readonly PositionService instance = new PositionService();
       private DaoFactory daoFactory = DaoFactory.Instance();
       private IDictionaryDao dict;
       internal PositionService()
        {
            dict = daoFactory.CreateDictionaryDao();
        }
       public static PositionService Instance()
       {
           return instance;
       }
       public ArrayList GelAllPosition()
       {
           ArrayList pls = new ArrayList();

           try
           {

               daoFactory.StartTransaction(true);
               ArrayList bposi = dict.SelectAll("POSI");

               foreach (BasicInfo item in bposi)
               {
                   PositionInfo posi = new PositionInfo();
                   posi.Code = item.Code;
                   posi.NameEng = item.DetailEn;
                   posi.NameThai = item.DetailTh;
                   pls.Add(posi);
               }


               return pls;

           }
           catch
           {

               return null;
           }
           finally
           {
               daoFactory.EndTransaction();
           }


       }
       public PositionInfo GetPosition(string posit)
       {
           PositionInfo posi = new PositionInfo();
           try
           {
               posi.Code = posit;
               daoFactory.StartTransaction(true);
               BasicInfo bposi = dict.Select("POSI", posit);
               posi.NameEng = bposi.DetailEn;
               posi.NameThai = bposi.DetailTh;
               return posi;

           }
           catch
           {

               return posi;
           }
           finally
           {
               daoFactory.EndTransaction();
           }



       }
    }
}
