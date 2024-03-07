using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using DCI.HRMS.Model;
using DCI.HRMS.Persistence;
using DCI.HRMS.Model.Organize;

namespace DCI.HRMS.Service
{
    public class DivisionService
    {
        private static readonly DivisionService instance = new DivisionService();
        private DaoFactory  factory = DaoFactory.Instance();
        private IDivisionDao divisionDao;
        private IDictionaryDao dictDao;

        internal DivisionService()
        {
            divisionDao = factory.CreateDivisionDao();
            dictDao = factory.CreateDictionaryDao();
        }

        public static DivisionService Instance()
        {
            return instance;
        }
        public ArrayList GetAll()
        {

            try
            {
                factory.StartTransaction(true);
                ArrayList dv= divisionDao.SelectAll();/*
                ArrayList rdv = new ArrayList();
                foreach (DivisionInfo var in dv)
                {
                
                 
                   rdv.Add( FindRootStructure(var.Code));
                }*/
                return dv;



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



        public DivisionInfo Find(string divisionCode , bool includeDivisionChilds)
        {
            try
            {
                factory.StartTransaction(true);
                DivisionInfo division = divisionDao.Select(divisionCode);
                try
                {
                    if (includeDivisionChilds)
                        division.DivisionChild = divisionDao.SelectByOwner(divisionCode);
                }
                catch { }
                return division;
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

        public DivisionInfo FindRootStructure(string divisionCode)
        {
            DivisionInfo curDiv = Find(divisionCode, false);
            DivisionInfo dept = new DivisionInfo();
            DivisionInfo sect = new DivisionInfo();
            DivisionInfo grp = new DivisionInfo();

            if (curDiv != null)
            {
                if (curDiv.Type == DivisionType.Group)
                {
                    grp = curDiv;
                    sect = Find(grp.DivisionOwner.Code, false);
                    dept = Find(sect.DivisionOwner.Code, false);
                    grp.DivisionOwner = sect;
                    grp.DivisionOwner.DivisionOwner = dept;

                    return grp;
                }
                else if (curDiv.Type == DivisionType.Section)
                {
                    sect = curDiv;
                    dept = Find(sect.DivisionOwner.Code, false);
                    sect.DivisionOwner = dept;
                    return sect;
                }
                else if (curDiv.Type == DivisionType.Department)
                {
                    dept = curDiv;
                    return dept;
                }


            }
            return null;
        }
        public ArrayList FindByType(string divisionTypeCode)
        {
            try{
                factory.StartTransaction(true);
                return divisionDao.SelectByType(divisionTypeCode);
            }catch{
                throw;
            }finally{
                factory.EndTransaction();
            }
        }
        public ArrayList FindByOwner(string ownerId)
        {
            try
            {
                factory.StartTransaction(true);
                return divisionDao.SelectByOwner(ownerId);
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

        public void Update(DivisionInfo item)
        {
            throw new NotImplementedException();
        }

        public void Save(DivisionInfo item)
        {
            throw new NotImplementedException();
        }

        public void Delete(string p)
        {
            throw new NotImplementedException();
        }

        public ArrayList GetAllType()
        {
            try
            {
                factory.StartTransaction(true);
                return dictDao.SelectAll("ORGT");
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


    }
}
