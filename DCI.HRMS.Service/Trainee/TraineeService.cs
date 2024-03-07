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

namespace DCI.HRMS.Service.Trainee
{
    public class TraineeService
    {
        private DaoFactory daoFactory = DaoFactory.Instance();

        private static TraineeService instance = new TraineeService();
        private TraineeDaoFactory tnDaoFactory = TraineeDaoFactory.Instance();
        private IDictionaryDao dict;
        private IDictionaryDao rsType;
        private IEmployeeDao employeeDao;
        private PositionService positSvr = PositionService.Instance();

        internal TraineeService()
        {
            employeeDao = tnDaoFactory.CreateEmployeeDao();
            rsType = daoFactory.CreateDictionaryDao();
            dict = daoFactory.CreateDictionaryDao();
        }

        public static TraineeService Instance()
        {
            return instance;
        }

        public EmployeeInfo Find(string employeeId)
        {
            try
            {
                tnDaoFactory.StartTransaction(true);
                EmployeeInfo emp = employeeDao.Select(employeeId);

                tnDaoFactory.EndTransaction();

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
                tnDaoFactory.StartTransaction(true);
                EmployeeInfo emp = employeeDao.Select(employeeId);

                tnDaoFactory.EndTransaction();



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
                tnDaoFactory.StartTransaction(true);

                EmployeeDataInfo emp = employeeDao.GetEmployeeDataInfo(empCode);



                tnDaoFactory.EndTransaction();

                DivisionInfo division = DivisionService.Instance().FindRootStructure(emp.Division.Code);
                emp.Division = division;
                emp.Position = positSvr.GetPosition(emp.Position.Code);
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
                tnDaoFactory.StartTransaction(true);
                return employeeDao.SelectCurEmp();
            }
            catch
            {
                return null;
            }
            finally
            {
                tnDaoFactory.EndTransaction();
            }
        }
        public DataSet FindAllEmp()
        {
            try
            {
                tnDaoFactory.StartTransaction(true);
                DataSet emp = employeeDao.SelectAllEmp();

                tnDaoFactory.EndTransaction();

            
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
                tnDaoFactory.StartTransaction(false);
                employeeDao.SaveEmployeeInfo(empInfo);
                tnDaoFactory.CommitTransaction();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
               tnDaoFactory.EndTransaction();
            }
        }
        public void UpdateEmployeeInfo(EmployeeDataInfo empInfo)
        {
            try
            {
                tnDaoFactory.StartTransaction(false);
                employeeDao.UpdateEmployeeInfo(empInfo);
                tnDaoFactory.CommitTransaction();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                tnDaoFactory.EndTransaction();
            }
        }
        public void DeleteEmployeeInfo(string empCode)
        {
            try
            {
                tnDaoFactory.StartTransaction(false);
                employeeDao.DeleteEmployeeInfo(empCode);
                tnDaoFactory.CommitTransaction();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                tnDaoFactory.EndTransaction();
            }
        }

        public BasicInfo GetResignType(string _type)
        {
            try
            {
                tnDaoFactory.StartTransaction(true);
               BasicInfo rst = rsType.Select("REST", _type);

          

                return rst;
            }
            catch
            {
                return null;
            }
            finally
            {    
                tnDaoFactory.EndTransaction();


            }


        }
        public ArrayList GetEmployeeFamily(string empCode)
        {
            try
            {
                tnDaoFactory.StartTransaction(true);

                return employeeDao.GetEmployeeFamily(empCode);
            }
            catch
            {
                return null;
            }
            finally
            {
                tnDaoFactory.EndTransaction();


            }
        }
        public void SaveEmployeeFamily(FamilyInfo emfm)
        {
            try
            {
                tnDaoFactory.StartTransaction(true);

              employeeDao.SaveEmployeeFamily(emfm);
                //daoFactory.CommitTransaction();
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            finally
            {
                tnDaoFactory.EndTransaction();


            }
        }
        public void UpdateEmployeeFamily(FamilyInfo emfm)
        {
            try
            {
                tnDaoFactory.StartTransaction(true);

               employeeDao.UpdateEmployeeFamily(emfm);
              //  daoFactory.CommitTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                tnDaoFactory.EndTransaction();


            }
        }
        public void DeleteEmployeeFamily(FamilyInfo emfm)
        {
            try
            {
                tnDaoFactory.StartTransaction(true);

         employeeDao.DeleteloyeeFamily(emfm);
                //daoFactory.CommitTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                tnDaoFactory.EndTransaction();


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
                tnDaoFactory.StartTransaction(false);
                return employeeDao.GetRecordHistoryDataSet(empCode);

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                tnDaoFactory.EndTransaction();


            }
        }
        public void EmployeeResignation(string empCode, DateTime rsDate, string rsType, string rsReason, string rsRemark, string by)
        {
            try
            {
                tnDaoFactory.StartTransaction(false);
                employeeDao.EmployeeResignation(empCode, rsDate, rsType, rsReason,rsRemark, by);
                tnDaoFactory.CommitTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                tnDaoFactory.EndTransaction();


            }
        }
        public DataSet GenEmpployee()
        {
            try
            {
                tnDaoFactory.StartTransaction(true);
                DataSet emp = employeeDao.GenerateEmp();

                tnDaoFactory.EndTransaction();


                return emp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet GetManpowerForBC(DateTime pdate)
        {
            try
            {
                tnDaoFactory.StartTransaction(false);
                return employeeDao.GetManpowerForBC(pdate);

            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
            finally
            {
                tnDaoFactory.EndTransaction();


            }
        }


        public DataSet GetManpowerForBC1(DateTime pdate, string pDvcd)
        {
            try
            {
                tnDaoFactory.StartTransaction(false);
                return employeeDao.GetManpowerForBC1(pdate, pDvcd);

            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
            finally
            {
                tnDaoFactory.EndTransaction();


            }
        }


    }
}
