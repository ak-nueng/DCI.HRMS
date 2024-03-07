using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using DCI.HRMS.Model;
using DCI.HRMS.Persistence;
using System.Diagnostics;
using DCI.HRMS.Model.Organize;

namespace DCI.HRMS.Service
{
    public class AttendanceService
    {
        private static readonly AttendanceService instance = new AttendanceService();

        private const string LEAVE_TYPE = "LVRQ";

        private DaoFactory factory = DaoFactory.Instance();
        private IAttendanceDao empLeaveDao;
        private IDictionaryDao dictionaryDao;

        internal AttendanceService()
        {
            empLeaveDao = factory.CreateEmployeeLeaveDao();
            dictionaryDao = factory.CreateDictionaryDao();
        }

        public static AttendanceService Instance()
        {
            return instance;
        }

        # region Common data

        public ArrayList GetLeaveTypes()
        {
            try
            {
                factory.StartTransaction(true);
                return dictionaryDao.SelectAll(LEAVE_TYPE);
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

        # endregion

        public ArrayList FindLeaveRecords(string keyword, string sectionCode, string leaveTypeCode, string status, DateTime fromDate, DateTime toDate)
        {
            try
            {
                factory.StartTransaction(true);
                return empLeaveDao.SelectByCriteria(keyword, sectionCode, leaveTypeCode, status, fromDate, toDate);
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

        public ArrayList FindLeaveRecords(string employeeCode, string leaveTypeCode, DateTime fromDate, DateTime toDate)
        {
            return null;
        }

        # region Summary Data

        public ArrayList SummaryLeaveRecordGroupBySection(string leaveType , DateTime fromDate , DateTime toDate)
        {
            ArrayList section_TmpList = DivisionService.Instance().FindByType("SEC");
            ArrayList section_List = new ArrayList();

            try
            {
                factory.StartTransaction(true);
                foreach (DivisionInfo section in section_TmpList)
                {
                    section.Items = empLeaveDao.SummaryLeaveRecords(leaveType, section.Code, fromDate, toDate);
                    if (section.Items != null && section.Items.Count > 0)
                    {
                        section_List.Add(section);
                    }
                }
                return section_List;
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

        public ArrayList FindEmployeeAbsentAlert(string keyword , string sectionCode , DateTime fromDate, DateTime toDate)
        {
            ArrayList section_List = new ArrayList();

            try
            {
                if (sectionCode == "%")
                {
                    ArrayList section_TmpList = DivisionService.Instance().FindByType("SECT");
                    factory.StartTransaction(true);

                    foreach (DivisionInfo section in section_TmpList)
                    {
                        AddEmployeeAbsentAlertRecord(keyword, fromDate, toDate, section_List, section);
                    }
                }
                else
                {
                    DivisionInfo section = DivisionService.Instance().Find(sectionCode, false);
                    factory.StartTransaction(true);

                    AddEmployeeAbsentAlertRecord(keyword, fromDate, toDate, section_List, section);
                }
                return section_List;
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

        private void AddEmployeeAbsentAlertRecord(string keyword, DateTime fromDate, DateTime toDate, ArrayList section_List, DivisionInfo section)
        {
            try
            {
                section.Items = empLeaveDao.SelectEmployeeAbsentAlertRecords(keyword, section.Code, fromDate, toDate);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SEARCH DEPT: " + section.Name + " HAVE ERR: " + ex.Message);
            }
            if (section.Items != null && section.Items.Count > 0)
            {
                section_List.Add(section);
            }
        }
        # endregion

    }
}
