using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DCI.HRMS.Model;
using DCI.HRMS.Persistence;
using System.Diagnostics;
using DCI.HRMS.Model.Personal;
using DCI.HRMS.Model.Organize;
using DCI.HRMS.Model.Common;
using System.Collections;

namespace DCI.HRMS.Service.SubContract
{
    public class SubContractService
    {   private DaoFactory daoFactory = DaoFactory.Instance();

        private static SubContractService instance = new SubContractService();

        private SubContractDaoFactory subDaoFactory = SubContractDaoFactory.Instance();

        private IDictionaryDao dict;
        private IDictionaryDao rsType;
        private IEmployeeDao employeeDao;
        private PositionService positSvr = PositionService.Instance();
        internal SubContractService()
        {
            employeeDao = subDaoFactory.CreateEmployeeDao();
            rsType = daoFactory.CreateDictionaryDao();

            dict = daoFactory.CreateDictionaryDao();

        }

        public static SubContractService Instance()
        {
            return instance;
        }

        public EmployeeInfo Find(string employeeId)
        {
            try
            {
                subDaoFactory.StartTransaction(true);
                EmployeeInfo emp = employeeDao.Select(employeeId);

                subDaoFactory.EndTransaction();

                DivisionInfo division = DivisionService.Instance().FindRootStructure(emp.Division.Code);
                emp.Division = division;

                return emp;
            }
            catch{
                return null;
            }
        }
        public EmployeeInfo FindBasicInfo(string employeeId)
        {
            try
            {
                subDaoFactory.StartTransaction(true);
                EmployeeInfo emp = employeeDao.Select(employeeId);

                subDaoFactory.EndTransaction();



                return emp;
            }
            catch
            {
                return null;
            }
        }
        public EmployeeDataInfo GetEmployeeData(string empCode)
        {
            try
            {
                subDaoFactory.StartTransaction(true);

                EmployeeDataInfo emp = employeeDao.GetEmployeeDataInfo(empCode);



                subDaoFactory.EndTransaction();

                DivisionInfo division = DivisionService.Instance().FindRootStructure(emp.Division.Code);
                emp.Division = division;
                emp.Position =positSvr.GetPosition(emp.Position.Code);
                try
                {
                    emp.Hospital = GetHospital(emp.Hospital.Code);
                }
                catch
                { }

                try
                {
                    emp.FamilyMember = GetEmployeeFamily(emp.Code);

                }
                catch
                { }

                try
                {
                    emp.WorkHistory = employeeDao.GetEmployeeWorkHistory(emp.Code);
                }
                catch
                { }
                try
                {
                    emp.WorkHistory = employeeDao.GetEmployeeWorkHistory(emp.Code);
                }
                catch
                { }



                return emp;


            }
            catch
            {

                return null;
            }

        }

        public ArrayList GetCurrentEmployees()
        {
            try
            {
                subDaoFactory.StartTransaction(true);
                return employeeDao.SelectCurEmp();
            }
            catch
            {
                return null;
            }
            finally
            {
                subDaoFactory.EndTransaction();
            }
        }
        public ArrayList GetCurrentEmployeesByDVCD(string dvcd, string grpot)
        {
            try
            {
                daoFactory.StartTransaction(true);
                return employeeDao.SelectCurEmpByDVCD(dvcd, grpot);
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

        public ArrayList GetCurrentEmployeesListByDVCD(string dvcd)
        {
            try
            {
                daoFactory.StartTransaction(true);
                return employeeDao.SelectCurEmpListByDVCD(dvcd);
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
        public DataSet FindAllEmp()
        {
            try
            {
                subDaoFactory.StartTransaction(true);
                DataSet emp = employeeDao.SelectAllEmp();

                subDaoFactory.EndTransaction();

            
                return emp;
            }
            catch
            {
                return null;
            }
        }

        public void SaveEmployeeInfo(EmployeeDataInfo empInfo)
        {
            try
            {
                subDaoFactory.StartTransaction(false);
                employeeDao.SaveEmployeeInfo(empInfo);
                subDaoFactory.CommitTransaction();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                subDaoFactory.EndTransaction();
            }
        }
        public void UpdateEmployeeInfo(EmployeeDataInfo empInfo)
        {
            try
            {
                subDaoFactory.StartTransaction(false);
                employeeDao.UpdateEmployeeInfo(empInfo);
                subDaoFactory.CommitTransaction();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                subDaoFactory.EndTransaction();
            }
        }
        public void DeleteEmployeeInfo(string empCode)
        {
            try
            {
                subDaoFactory.StartTransaction(false);
                employeeDao.DeleteEmployeeInfo(empCode);
                subDaoFactory.CommitTransaction();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                subDaoFactory.EndTransaction();
            }
        }
        
        public BasicInfo GetResignType(string _type)
        {
            try
            {
                subDaoFactory.StartTransaction(true);
               BasicInfo rst = rsType.Select("REST", _type);

          

                return rst;
            }
            catch
            {
                return null;
            }
            finally
            {    
                subDaoFactory.EndTransaction();


            }


        }
        public ArrayList GetEmployeeFamily(string empCode)
        {
            try
            {
                subDaoFactory.StartTransaction(true);

                return employeeDao.GetEmployeeFamily(empCode);
            }
            catch
            {
                return null;
            }
            finally
            {
                subDaoFactory.EndTransaction();


            }
        }
        public void SaveEmployeeFamily(FamilyInfo emfm)
        {
            try
            {
                subDaoFactory.StartTransaction(true);

              employeeDao.SaveEmployeeFamily(emfm);
                //daoFactory.CommitTransaction();
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            finally
            {
                subDaoFactory.EndTransaction();


            }
        }
        public void UpdateEmployeeFamily(FamilyInfo emfm)
        {
            try
            {
                subDaoFactory.StartTransaction(true);

               employeeDao.UpdateEmployeeFamily(emfm);
              //  daoFactory.CommitTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                subDaoFactory.EndTransaction();


            }
        }
        public void DeleteEmployeeFamily(FamilyInfo emfm)
        {
            try
            {
                subDaoFactory.StartTransaction(true);

         employeeDao.DeleteloyeeFamily(emfm);
                //daoFactory.CommitTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                subDaoFactory.EndTransaction();


            }
        }
        public ArrayList GetFamilyRelation()
        {
            try
            {
                daoFactory.StartTransaction(true);

                return rsType.SelectAll("RELA");
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
        public HospitalInfo GetHospital(string hosit)
        {
            HospitalInfo hosp = new HospitalInfo();
            try
            {
                hosp.Code = hosit;
                daoFactory.StartTransaction(true);
                BasicInfo bposi = dict.Select("HOSP", hosit);
                hosp.NameEng = bposi.DetailEn;
                hosp.NameThai = bposi.DetailTh;
                return hosp;

            }
            catch
            {

                return hosp;
            }
            finally
            {
                daoFactory.EndTransaction();
            }



        }

        public DataSet GetRecordHistory(string empCode)
        {
            try
            {
                subDaoFactory.StartTransaction(false);
                return employeeDao.GetRecordHistoryDataSet(empCode);

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                subDaoFactory.EndTransaction();


            }
        }

        public void EmployeeResignation(string empCode, DateTime rsDate, string rsType, string rsReason,string rsRemark, string by)
        {
            try
            {
                subDaoFactory.StartTransaction(false);
                employeeDao.EmployeeResignation(empCode, rsDate, rsType, rsReason,rsRemark, by);
                subDaoFactory.CommitTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                subDaoFactory.EndTransaction();


            }
        }
        public DataSet GenEmpployee()
        {
            try
            {
                subDaoFactory.StartTransaction(true);
                DataSet emp = employeeDao.GenerateEmp();

                subDaoFactory.EndTransaction();


                return emp;
            }
            catch
            {
                return null;
            }
        }

        public DataSet GetManpowerForBC(DateTime pdate)
        {
            try
            {
                subDaoFactory.StartTransaction(false);
                return employeeDao.GetManpowerForBC(pdate);

            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
            finally
            {
                subDaoFactory.EndTransaction();


            }
        }


        public DataSet GetManpowerForBC1(DateTime pdate, string pDvcd)
        {
            try
            {
                subDaoFactory.StartTransaction(false);
                return employeeDao.GetManpowerForBC1(pdate, pDvcd);

            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
            finally
            {
                subDaoFactory.EndTransaction();


            }
        }




    }
}
