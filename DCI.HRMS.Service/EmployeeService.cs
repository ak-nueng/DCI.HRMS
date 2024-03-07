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
using DCI.HRMS.Model.Attendance;
using DCI.HRMS.Model.Allowance;

namespace DCI.HRMS.Service
{
    public class EmployeeService
    {
        private static EmployeeService instance = new EmployeeService();
        private DaoFactory daoFactory = DaoFactory.Instance();
        private IDictionaryDao dict;
        private IEmployeeDao employeeDao;
        private IOTDao otDao;
        private IPenaltyDao penDao;
        private IPropertyBorrowDao propertyDao;
        private ILeaveRequestDao leaveDao;
        private IShiftDao shiftDao;
        private ITimeCardDao timeDao;
        private ISkillAllowanceDao sklDao;

        private SubContractDaoFactory subDaoFactory = SubContractDaoFactory.Instance();
        private IEmployeeDao subContractDao;
        private IOTDao subOtDao;
        private ILeaveRequestDao subLeaveDao;
        private IShiftDao subShiftDao;
        private ITimeCardDao subTimeDao;
        private ISkillAllowanceDao subSklDao;


        internal EmployeeService()
        {

            employeeDao = daoFactory.CreateEmployeeDao();
            dict = daoFactory.CreateDictionaryDao();
            otDao = daoFactory.CreateOtDao();
            leaveDao = daoFactory.CreateLeaveReqDao();
            shiftDao = daoFactory.CreateShiftDao();
            propertyDao = daoFactory.CreatePropertyBorrowDao();
            timeDao = daoFactory.CreateTimeCardDAO();
            sklDao = daoFactory.CreateSkillAllowanceDao();
            penDao = daoFactory.CreatePenaltyDao();
            
            subContractDao = subDaoFactory.CreateEmployeeDao();   
            subOtDao = subDaoFactory.CreateOtDao();
            subLeaveDao = subDaoFactory.CreateLeaveReqDao();
            subShiftDao = subDaoFactory.CreateShiftDao();  
            subTimeDao = subDaoFactory.CreateTimeCardDAO();
            subSklDao = subDaoFactory.CreateSkillAllowanceDao();
     
            
        }

        public static EmployeeService Instance()
        {
            return instance;
        }

        public EmployeeDataInfo GetEmployeeData(string empCode)
        {
            try
            {
                daoFactory.StartTransaction(true);
                subDaoFactory.StartTransaction(false);

                //======= Edited By Aukit 15/06/2015 ========
                //    added get data from sub contract 
                //===========================================
                EmployeeDataInfo emp;
                if (empCode.StartsWith("I"))
                {
                    emp = subContractDao.GetEmployeeDataInfo(empCode);

                }else {
                    emp = employeeDao.GetEmployeeDataInfo(empCode);

                }
                //===========================================

                daoFactory.EndTransaction();

                DivisionInfo division = DivisionService.Instance().FindRootStructure(emp.Division.Code);
                emp.Division = division;
                emp.Position = GetPosition(emp.Position.Code);
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
        public BasicInfo GetDailyRate()
        {
            try
            {

                return dict.Select("DLRA", "0");



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
        public ArrayList GetAllPosition()
        {
            ArrayList posi = new ArrayList();
            try
            {

                daoFactory.StartTransaction(true);
                ArrayList bposi = dict.SelectAll("POSI");
                foreach (BasicInfo var in bposi)
                {
                    PositionInfo po = new PositionInfo();
                    po.Code = var.Code;
                    po.NameEng = var.DetailEn;
                    po.NameThai = var.DetailTh;
                    posi.Add(po);
                }
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
        public ArrayList GetAllHospital()
        {
            ArrayList hosp = new ArrayList();
            try
            {

                daoFactory.StartTransaction(true);
                ArrayList bposi = dict.SelectAll("HOSP");
                foreach (BasicInfo var in bposi)
                {
                    HospitalInfo ho = new HospitalInfo();
                    ho.Code = var.Code;
                    ho.NameEng = var.DetailEn;
                    ho.NameThai = var.DetailTh;
                    hosp.Add(ho);
                }
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

        public EmployeeInfo Find(string employeeId)
        {
            try
            {
                daoFactory.StartTransaction(true);
                EmployeeInfo emp = employeeDao.Select(employeeId);

                daoFactory.EndTransaction();

                DivisionInfo division = DivisionService.Instance().FindRootStructure(emp.Division.Code);
                emp.Division = division;

                return emp;
            }
            catch
            {
                return null;
            }
        }
        public EmployeeInfo FindBasicInfo(string employeeId)
        {
            try
            {
                daoFactory.StartTransaction(true);
                EmployeeInfo emp = employeeDao.Select(employeeId);

                daoFactory.EndTransaction();



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
                daoFactory.StartTransaction(true);
                return employeeDao.SelectCurEmp();
            }
            catch (Exception ex)
            {
                // throw ex;
                return null;
            }
            finally
            {
                daoFactory.EndTransaction();
            }
        }
        public ArrayList GetCurrentEmployeesByPosit(string posit)
        {
            try
            {
                daoFactory.StartTransaction(true);
                return employeeDao.SelectCurEmpByPosition(posit);
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

        public void SaveEmployeeInfo(EmployeeDataInfo empInfo)
        {
            try
            {
                daoFactory.StartTransaction(false);
                employeeDao.SaveEmployeeInfo(empInfo);
                daoFactory.CommitTransaction();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                daoFactory.EndTransaction();
            }
        }
        public void UpdateEmployeeInfo(EmployeeDataInfo empInfo)
        {
            try
            {
                daoFactory.StartTransaction(false);
                employeeDao.UpdateEmployeeInfo(empInfo);
                daoFactory.CommitTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                daoFactory.EndTransaction();
            }
        }
        public void DeleteEmployeeInfo(string empCode)
        {
            try
            {
                daoFactory.StartTransaction(false);
                employeeDao.DeleteEmployeeInfo(empCode);
                daoFactory.CommitTransaction();
            }
            catch (Exception ex)
            {

                throw ex;
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
                daoFactory.StartTransaction(true);
                DataSet emp = employeeDao.SelectAllEmp();

                daoFactory.EndTransaction();


                return emp;
            }
            catch
            {
                return null;
            }
        }
        public DataSet GenEmpployee()
        {
            try
            {
                daoFactory.StartTransaction(true);
                DataSet emp = employeeDao.GenerateEmp();

                daoFactory.EndTransaction();


                return emp;
            }
            catch
            {
                return null;
            }
        }
        public ArrayList GenResignEmployee(DateTime fromDate, DateTime toDate)
        {
            try
            {
                daoFactory.StartTransaction(true);
                ArrayList emp = employeeDao.GetResignEmp(fromDate, toDate);

                daoFactory.EndTransaction();


                return emp;
            }
            catch
            {
                return null;
            }
        }
        public BasicInfo GetResignType(string _type)
        {
            try
            {
                daoFactory.StartTransaction(true);
                BasicInfo rst = dict.Select("REST", _type);



                return rst;
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
        public ArrayList GetAllResignType()
        {
            try
            {
                daoFactory.StartTransaction(true);
                ArrayList rst = dict.SelectAll("REST");



                return rst;
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
        public ArrayList GetEmployeeFamily(string empCode)
        {
            try
            {
                daoFactory.StartTransaction(true);

                ArrayList fmlist = employeeDao.GetEmployeeFamily(empCode);
                ArrayList rtLs = new ArrayList();

                foreach (FamilyInfo item in fmlist)
                {
                    item.RelationType = dict.Select("RELA", item.Relation.Substring(4)).DetailTh;
                    rtLs.Add(item);
                }

                return rtLs;
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
        public void SaveEmployeeFamily(FamilyInfo emfm)
        {
            try
            {
                daoFactory.StartTransaction(true);

                employeeDao.SaveEmployeeFamily(emfm);
                //daoFactory.CommitTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                daoFactory.EndTransaction();


            }
        }
        public void UpdateEmployeeFamily(FamilyInfo emfm)
        {
            try
            {
                daoFactory.StartTransaction(true);

                employeeDao.UpdateEmployeeFamily(emfm);
                //  daoFactory.CommitTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                daoFactory.EndTransaction();


            }
        }
        public void DeleteEmployeeFamily(FamilyInfo emfm)
        {
            try
            {
                daoFactory.StartTransaction(true);

                employeeDao.DeleteloyeeFamily(emfm);
                //daoFactory.CommitTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                daoFactory.EndTransaction();


            }
        }
        public ArrayList GetFamilyRelation()
        {
            try
            {
                daoFactory.StartTransaction(true);

                return dict.SelectAll("RELA");
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
        public ArrayList GetAllDegree()
        {
            try
            {
                daoFactory.StartTransaction(true);

                return dict.SelectAll("DEGR");
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
        public ArrayList GetEmployeeWorkHistory(string _empCode)
        {
            try
            {
                daoFactory.StartTransaction(true);

                return employeeDao.GetEmployeeWorkHistory(_empCode);
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
        public void SaveEmployeeWorkHistory(WorkHistoryInfo _wkh)
        {
            try
            {
                daoFactory.StartTransaction(true);

                employeeDao.SaveEmployeeWorkHistory(_wkh);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                daoFactory.EndTransaction();


            }
        }
        public void UpdateEmployeeWorkHistory(WorkHistoryInfo _wkh)
        {
            try
            {
                daoFactory.StartTransaction(true);

                employeeDao.UpdateEmployeeWorkHistory(_wkh);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                daoFactory.EndTransaction();


            }
        }
        public void DeleteEmployeeWorkHistory(WorkHistoryInfo _wkh)
        {
            try
            {
                daoFactory.StartTransaction(true);

                employeeDao.DeleteloyeeWorkHistory(_wkh);
                //daoFactory.CommitTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                daoFactory.EndTransaction();


            }
        }
        public void EmployeeResignation(string empCode, DateTime rsDate, string rsType, string rsReason, string rsRemark, string by)
        {
            try
            {
                daoFactory.StartTransaction(false);
                employeeDao.EmployeeResignation(empCode, rsDate, rsType, rsReason, rsRemark, by);
                daoFactory.CommitTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                daoFactory.EndTransaction();


            }
        }
        public void EmployeeEmail(string empCode, string email, string extension)
        {
            try
            {
                daoFactory.StartTransaction(false);
                employeeDao.EmployeeSetEmail(empCode, email, extension);
                daoFactory.CommitTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                daoFactory.EndTransaction();


            }
        }
        public ArrayList GetEmployeeCodetransfer(string empCode)
        {
            try
            {
                daoFactory.StartTransaction(true);
                return employeeDao.GetEmployeeCodeTransfer(empCode);

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                daoFactory.EndTransaction();


            }
        }

        public ArrayList GetEmployeeCodetransfer(string empCode, DateTime transDate)
        {
            try
            {
                daoFactory.StartTransaction(true);
                return employeeDao.GetEmployeeCodeTransfer(empCode, transDate, "%");

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                daoFactory.EndTransaction();


            }
        }
        public EmployeeCodeTransferInfo GetEmployeeCodetransfer(string oldCode, string newCode)
        {
            try
            {
                daoFactory.StartTransaction(true);
                return employeeDao.GetUnqEmpCodeTransfer(oldCode, newCode);

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                daoFactory.EndTransaction();


            }
        }
        public void SaveEmployeeCodetransfer(EmployeeCodeTransferInfo emfm)
        {
            try
            {
                daoFactory.StartTransaction(false);

                employeeDao.SaveEmployeeCodeTransfer(emfm);
                daoFactory.CommitTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                daoFactory.EndTransaction();


            }
        }
        public void UpdateEmployeeCodetransfer(EmployeeCodeTransferInfo emfm)
        {
            try
            {
                daoFactory.StartTransaction(false);

                employeeDao.UpdateEmployeeCodeTransfer(emfm);
                daoFactory.CommitTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                daoFactory.EndTransaction();


            }
        }
        public void DeleteEmployeeCodetransfer(EmployeeCodeTransferInfo emfm)
        {
            try
            {
                daoFactory.StartTransaction(false);

                employeeDao.DeleteEmployeeCodeTransfer(emfm.OldCode, emfm.NewCode);
                daoFactory.CommitTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                daoFactory.EndTransaction();
            }
        }
        public bool TransferCode(EmployeeCodeTransferInfo empTr)
        {
            try
            {
                //FindBasicInfo(empTr.NewCode) != null ||
 


                if (empTr.OldCode.StartsWith("I"))
                {

                    //Tranfer from Subcontract

                    daoFactory.StartTransaction(false);
                    subDaoFactory.StartTransaction(false);

                    if (subContractDao.GetEmployeeDataInfo(empTr.OldCode) == null)
                        return false;

                    EmployeeDataInfo oldEmp = subContractDao.GetEmployeeDataInfo(empTr.OldCode);
                    EmployeeDataInfo newEmp = oldEmp;

                    try
                    {
                        subContractDao.EmployeeResignation(empTr.OldCode, empTr.TransferDate, "6", "เริ่มงานที่ DCI", "", empTr.CreateBy);
                    }
                    catch
                    {

                        throw new Exception("resignation Old Code Error");
                    }

                    //--------------- set Annual Date is 01/07/LastYear ------------------
                    int _CurYear = empTr.TransferDate.Year;
                    DateTime _curDt = new DateTime(_CurYear, 7, 1);
                    DateTime _prevAnnDt = new DateTime();
                    _prevAnnDt = _curDt.AddYears(-1);

                    //--------------- set Annual Date is 01/07/LastYear ------------------

                    newEmp.Code = empTr.NewCode;
                    //newEmp.AnnualcalDate = newEmp.JoinDate;
                    newEmp.AnnualcalDate = _prevAnnDt;
                    newEmp.JoinDate = empTr.TransferDate;
                    newEmp.Company = "DCI";


                    try
                    {
                        try
                        {

                            EmployeeInfo emp = employeeDao.Select(newEmp.Code);
                            newEmp.ResignDate = DateTime.MinValue;
                            newEmp.ResignType = "";
                            newEmp.ResignReason = "";
                            employeeDao.UpdateEmployeeInfo(newEmp);

                        }
                        catch
                        {
                            employeeDao.SaveEmployeeInfo(newEmp);
                        }



                    }
                    catch
                    {

                        throw new Exception("Save Employee Data of New Code Error");
                    }
                    ArrayList otDt;
                    try
                    {
                        otDt = subOtDao.GetOTRequest(empTr.OldCode, empTr.TransferDate, DateTime.Today.AddMonths(1), "%", "%", "%", "%", "%");
                    }
                    catch
                    {
                        otDt = null;
                    }
                    ArrayList lvDt;
                    try
                    {
                        lvDt = subLeaveDao.GetLeaveReq(empTr.OldCode, "%");
                    }
                    catch
                    {
                        lvDt = null;
                    }
                    ArrayList shDt;
                    try
                    {
                        shDt = subShiftDao.GetShiftDataByCode(empTr.OldCode, empTr.TransferDate.Year);
                    }
                    catch
                    {
                        shDt = null;
                    }
                    //ArrayList prtDt;
                    //try
                    //{
                    //    prtDt = propertyDao.GetByEmpCode(empTr.OldCode);
                    //}
                    //catch
                    //{
                    //    prtDt = null;
                    //}
                    ArrayList tmDt;
                    try
                    {
                        tmDt = subTimeDao.GetTimeCardByDate(empTr.OldCode, empTr.TransferDate, DateTime.Today.AddMonths(1));
                    }
                    catch
                    {
                        tmDt = null;
                    }

                    ArrayList tmMnDt;
                    try
                    {
                        tmMnDt = subTimeDao.GetTimeCardManual(empTr.OldCode, empTr.TransferDate, DateTime.Today.AddMonths(1), "%");
                    }
                    catch
                    {
                        tmMnDt = null;
                    }
                    //ArrayList penDt;
                    //try
                    //{
                    //    penDt = penDao.GetPenalty(empTr.OldCode);
                    //}
                    //catch
                    //{
                    //    penDt = null;
                    //}




                    ArrayList certDt;
                    try
                    {
                        certDt = subSklDao.GetCertificateByCode(empTr.OldCode);
                    }
                    catch
                    {

                        certDt = null;
                    }
                    ArrayList sklDt;
                    try
                    {
                        sklDt = subSklDao.GetSkillByCode(empTr.OldCode, "%");
                    }
                    catch
                    {

                        sklDt = null;
                    }


                    if (otDt != null)
                    {
                        try
                        {
                            foreach (OtRequestInfo item in otDt)
                            {

                                if (item.OtDate >= empTr.TransferDate)
                                {

                                    item.EmpCode = empTr.NewCode;
                                    try
                                    {
                                        otDao.GetOTRequest(item.EmpCode, item.OtDate, item.OtDate, "%", "%", item.OtFrom, item.OtTo, "%");

                                    }
                                    catch
                                    {
                                        otDao.SaveOtRequest(item);

                                    }

                                }

                            }
                        }
                        catch
                        {

                            throw new Exception("Tranfer OverTime Record Error");
                        }

                    }
                    if (lvDt != null)
                    {
                        // change for use sql statement by akone 20210825

                        /*
                        try
                        {
                            foreach (EmployeeLealeRequestInfo lvItem in lvDt)
                            {

                                if (lvItem.LvDate >= empTr.TransferDate)
                                {
                                    lvItem.EmpCode = empTr.NewCode;

                                    try
                                    {
                                        leaveDao.GetLeaveReq(lvItem.EmpCode, lvItem.LvDate, lvItem.LvType);
                                    }
                                    catch
                                    {
                                        leaveDao.SaveLeaveReq(lvItem);

                                    }
                                }
                            }
                        }
                        catch
                        {

                            throw new Exception("Tranfer Leave Record Error");
                        }
                        */
                    }
                    if (shDt != null)
                    {
                        try
                        {
                            foreach (EmployeeShiftInfo item in shDt)
                            {
                                item.EmpCode = empTr.NewCode;
                                try
                                {
                                    shiftDao.GetShiftData(item.YearMonth, item.EmpCode);
                                }
                                catch
                                {
                                    shiftDao.Insert(item);

                                }
                            }
                        }
                        catch
                        {

                            throw new Exception("Tranfer Employee Shift Record Error");
                        }
                    }
                    //if (prtDt != null)
                    //{
                    //    foreach (PropertyBorrowInfo item in prtDt)
                    //    {
                    //        try
                    //        {
                    //            item.EmpCode = empTr.NewCode;

                    //            propertyDao.UpdateData(item);


                    //        }
                    //        catch
                    //        {

                    //            throw new Exception("Tranfer Property Borrow Record Error");
                    //        }
                    //    }

                    //}
                    if (tmDt != null)
                    {
                        foreach (TimeCardInfo item in tmDt)
                        {
                            if (item.CardDate >= empTr.TransferDate)
                            {
                                try
                                {

                                    item.EmpCode = empTr.NewCode;
                                    try
                                    {
                                        timeDao.GetUniqTimeCard(item.EmpCode, item.CardDate, item.CardTime);
                                    }
                                    catch
                                    {
                                        timeDao.Insert(item);

                                    }

                                }
                                catch
                                {

                                    throw new Exception("Tranfer TimeCard Record Error");
                                }
                            }
                        }

                    }
                    if (tmMnDt != null)
                    {
                        foreach (TimeCardManualInfo item in tmMnDt)
                        {
                            if (item.RqDate >= empTr.TransferDate)
                            {
                                try
                                {
                                    item.EmpCode = empTr.NewCode;
                                    try
                                    {
                                        timeDao.GetTimeCardManual(item.EmpCode, item.RqDate, item.RqType);
                                    }
                                    catch
                                    {
                                        timeDao.SaveTimeCardManual(item);

                                    }
                                }
                                catch
                                {

                                    throw new Exception("Tranfer TimeCard Manual Record Error");
                                }

                            }
                        }
                    }
                    //if (penDt != null)
                    //{
                    //    foreach (PenaltyInfo item in penDt)
                    //    {
                    //        try
                    //        {
                    //            item.EmpCode = empTr.NewCode;
                    //            try
                    //            {
                    //                penDao.GetPenalty(item.EmpCode, item.PenaltyFrom, item.PenaltyTo);
                    //            }
                    //            catch
                    //            {

                    //                penDao.SavePenalty(item);
                    //            }

                    //        }
                    //        catch
                    //        {

                    //            throw new Exception("Tranfer Penalty Record Error");
                    //        }
                    //    }
                    //}
                    if (certDt != null)
                    {
                        foreach (EmpCertInfo item in certDt)
                        {
                            try
                            {
                                item.EmpCode = empTr.NewCode;
                                try
                                {
                                    sklDao.GetCertificateByCode(item.EmpCode, item.CerType, item.Level);
                                }
                                catch
                                {
                                    sklDao.SaveEmpCertificate(item);
                                }

                            }
                            catch
                            {

                                throw new Exception("Tranfer Certificate Record Error");
                            }
                        }
                    }
                    if (sklDt != null)
                    {
                        foreach (EmpSkillAllowanceInfo item in sklDt)
                        {
                            try
                            {
                                item.EmpCode = empTr.NewCode;
                                try
                                {
                                    sklDao.GetSkillByCode(item.EmpCode, item.Month, item.CertType, item.CertLevel);
                                }
                                catch
                                {
                                    sklDao.SaveSkillAllowance(item);
                                }

                            }
                            catch
                            {

                                throw new Exception("Tranfer SkillAllowance Record Error");
                            }
                        }
                    }
                    empTr.TransferStatus = "P";
                    employeeDao.UpdateEmployeeCodeTransfer(empTr);
                    subDaoFactory.CommitTransaction();
                    daoFactory.CommitTransaction();
                    return true;
                }
                else
                {
                    //Tranfer Tempolary Employee


                    daoFactory.StartTransaction(false);

                    if (employeeDao.GetEmployeeDataInfo(empTr.OldCode) == null)
                        return false;

                    EmployeeDataInfo oldEmp = employeeDao.GetEmployeeDataInfo(empTr.OldCode);
                    EmployeeDataInfo newEmp = oldEmp;

                    try
                    {
                        employeeDao.EmployeeResignation(empTr.OldCode, empTr.TransferDate, "6", "เริ่มงานที่ DCI", "", empTr.CreateBy);
                    }
                    catch
                    {

                        throw new Exception("resignation Old Code Error");
                    }
                    newEmp.Code = empTr.NewCode;

                    try
                    {
                        try
                        {

                            EmployeeInfo emp = employeeDao.Select(newEmp.Code);
                            newEmp.ResignDate = DateTime.MinValue;
                            newEmp.ResignType = "";
                            newEmp.ResignReason = "";
                            employeeDao.UpdateEmployeeInfo(newEmp);
                            
                        }
                        catch
                        {
                            employeeDao.SaveEmployeeInfo(newEmp);
                        }



                    }
                    catch
                    {

                        throw new Exception("Save Employee Data of New Code Error");
                    }
                    ArrayList otDt;
                    try
                    {
                        otDt = otDao.GetOTRequest(empTr.OldCode, empTr.TransferDate, DateTime.Today.AddMonths(1), "%", "%", "%", "%", "%");
                    }
                    catch
                    {
                        otDt = null;
                    }
                    ArrayList lvDt;
                    try
                    {
                        lvDt = leaveDao.GetLeaveReq(empTr.OldCode, "%");
                    }
                    catch
                    {
                        lvDt = null;
                    }
                    ArrayList shDt;
                    try
                    {
                        shDt = shiftDao.GetShiftDataByCode(empTr.OldCode, empTr.TransferDate.Year);
                    }
                    catch
                    {
                        shDt = null;
                    }
                    ArrayList prtDt;
                    try
                    {
                        prtDt = propertyDao.GetByEmpCode(empTr.OldCode);
                    }
                    catch
                    {
                        prtDt = null;
                    }
                    ArrayList tmDt;
                    try
                    {
                        tmDt = timeDao.GetTimeCardByDate(empTr.OldCode, empTr.TransferDate, DateTime.Today.AddMonths(1));
                    }
                    catch
                    {
                        tmDt = null;
                    }
                    ArrayList tmMnDt;
                    try
                    {
                        tmMnDt = timeDao.GetTimeCardManual(empTr.OldCode, empTr.TransferDate, DateTime.Today.AddMonths(1), "%");
                    }
                    catch
                    {
                        tmMnDt = null;
                    }
                    ArrayList penDt;
                    try
                    {
                        penDt = penDao.GetPenalty(empTr.OldCode);
                    }
                    catch
                    {
                        penDt = null;
                    }




                    ArrayList certDt;
                    try
                    {
                        certDt = sklDao.GetCertificateByCode(empTr.OldCode);
                    }
                    catch
                    {

                        certDt = null;
                    }
                    ArrayList sklDt;
                    try
                    {
                        sklDt = sklDao.GetSkillByCode(empTr.OldCode, "%");
                    }
                    catch
                    {

                        sklDt = null;
                    }
                    if (otDt != null)
                    {
                        try
                        {
                            foreach (OtRequestInfo item in otDt)
                            {

                                if (item.OtDate >= empTr.TransferDate)
                                {

                                    item.EmpCode = empTr.NewCode;
                                    try
                                    {
                                        otDao.GetOTRequest(item.EmpCode, item.OtDate, item.OtDate, "%", "%", item.OtFrom, item.OtTo, "%");

                                    }
                                    catch
                                    {
                                        otDao.SaveOtRequest(item);

                                    }

                                }

                            }
                        }
                        catch
                        {

                            throw new Exception("Tranfer OverTime Record Error");
                        }

                    }
                    if (lvDt != null)
                    {
                        try
                        {
                            foreach (EmployeeLealeRequestInfo lvItem in lvDt)
                            {

                                lvItem.EmpCode = empTr.NewCode;

                                try
                                {
                                    leaveDao.GetLeaveReq(lvItem.EmpCode, lvItem.LvDate, lvItem.LvType);
                                }
                                catch
                                {
                                    leaveDao.SaveLeaveReq(lvItem);

                                }
                            }
                        }
                        catch
                        {

                            throw new Exception("Tranfer Leave Record Error");
                        }

                    }
                    if (shDt != null)
                    {
                        try
                        {
                            foreach (EmployeeShiftInfo item in shDt)
                            {
                                item.EmpCode = empTr.NewCode;
                                try
                                {
                                    shiftDao.GetShiftData(item.YearMonth, item.EmpCode);
                                }
                                catch
                                {
                                    shiftDao.Insert(item);

                                }
                            }
                        }
                        catch
                        {

                            throw new Exception("Tranfer Employee Shift Record Error");
                        }
                    }
                    if (prtDt != null)
                    {
                        foreach (PropertyBorrowInfo item in prtDt)
                        {
                            try
                            {
                                item.EmpCode = empTr.NewCode;

                                propertyDao.UpdateData(item);


                            }
                            catch
                            {

                                throw new Exception("Tranfer Property Borrow Record Error");
                            }
                        }

                    }
                    if (tmDt != null)
                    {
                        foreach (TimeCardInfo item in tmDt)
                        {
                            if (item.CardDate >= empTr.TransferDate)
                            {
                                try
                                {

                                    item.EmpCode = empTr.NewCode;
                                    try
                                    {
                                        timeDao.GetUniqTimeCard(item.EmpCode, item.CardDate, item.CardTime);
                                    }
                                    catch
                                    {
                                        timeDao.Insert(item);

                                    }

                                }
                                catch
                                {

                                    throw new Exception("Tranfer TimeCard Record Error");
                                }
                            }
                        }

                    }
                    if (tmMnDt != null)
                    {
                        foreach (TimeCardManualInfo item in tmMnDt)
                        {
                            if (item.RqDate >= empTr.TransferDate)
                            {
                                try
                                {
                                    item.EmpCode = empTr.NewCode;
                                    try
                                    {
                                        timeDao.GetTimeCardManual(item.EmpCode, item.RqDate, item.RqType);
                                    }
                                    catch
                                    {
                                        timeDao.SaveTimeCardManual(item);

                                    }
                                }
                                catch
                                {

                                    throw new Exception("Tranfer TimeCard Manual Record Error");
                                }

                            }
                        }
                    }
                    if (penDt != null)
                    {
                        foreach (PenaltyInfo item in penDt)
                        {
                            try
                            {
                                item.EmpCode = empTr.NewCode;
                                try
                                {
                                    penDao.GetPenalty(item.EmpCode, item.PenaltyFrom, item.PenaltyTo);
                                }
                                catch
                                {

                                    penDao.SavePenalty(item);
                                }

                            }
                            catch
                            {

                                throw new Exception("Tranfer Penalty Record Error");
                            }
                        }
                    }
                    if (certDt != null)
                    {
                        foreach (EmpCertInfo item in certDt)
                        {
                            try
                            {
                                item.EmpCode = empTr.NewCode;
                                try
                                {
                                    sklDao.GetCertificateByCode(item.EmpCode, item.CerType, item.Level);
                                }
                                catch
                                {
                                    sklDao.SaveEmpCertificate(item);
                                }

                            }
                            catch
                            {

                                throw new Exception("Tranfer Certificate Record Error");
                            }
                        }
                    }
                    if (sklDt != null)
                    {
                        foreach (EmpSkillAllowanceInfo item in sklDt)
                        {
                            try
                            {
                                item.EmpCode = empTr.NewCode;
                                try
                                {
                                    sklDao.GetSkillByCode(item.EmpCode, item.Month, item.CertType, item.CertLevel);
                                }
                                catch
                                {
                                    sklDao.SaveSkillAllowance(item);
                                }

                            }
                            catch
                            {

                                throw new Exception("Tranfer SkillAllowance Record Error");
                            }
                        }
                    }
                    empTr.TransferStatus = "P";
                    employeeDao.UpdateEmployeeCodeTransfer(empTr);
                    daoFactory.CommitTransaction();
                    return true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
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
                daoFactory.StartTransaction(false);
                return employeeDao.GetRecordHistoryDataSet(empCode);

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                daoFactory.EndTransaction();


            }
        }

        public DataSet GetManpowerByGrpOT(string pdate, string posit, string grpot)
        {
            try
            {
                daoFactory.StartTransaction(false);
                return employeeDao.GetManpowerByGrpot(pdate, posit, grpot);

            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
            finally
            {
                daoFactory.EndTransaction();


            }
        }
        public int GetEmpCardIssue(string empCode)
        {
            try
            {
                int issue = 0;
                daoFactory.StartTransaction(false);
                issue = employeeDao.GetEmpCardIssue(empCode);
                daoFactory.CommitTransaction();
                return issue;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }
        public int GenEmpCardIssue(string empCode)
        {
            try
            {
                int issue = 0;
                daoFactory.StartTransaction(false);
                issue = employeeDao.GenEmpCardIssue(empCode);
                daoFactory.CommitTransaction();
                return issue;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }
        public int GenEmpCardIssue(string empCode, string empName)
        {
            try
            {
                int issue = 0;
                daoFactory.StartTransaction(false);
                issue = employeeDao.GenEmpCardIssue(empCode, empName);
                daoFactory.CommitTransaction();
                return issue;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public DataSet GetManpowerForBC(DateTime pdate)
        {
            try
            {
                daoFactory.StartTransaction(false);
                return employeeDao.GetManpowerForBC(pdate);

            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
            finally
            {
                daoFactory.EndTransaction();


            }
        }


        public DataSet GetManpowerForBC1(DateTime pdate, string pDvcd)
        {
            try
            {
                daoFactory.StartTransaction(false);
                return employeeDao.GetManpowerForBC1(pdate, pDvcd);
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
            finally
            {
                daoFactory.EndTransaction();
            }
        }


    }
}
