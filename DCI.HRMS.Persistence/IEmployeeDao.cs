using System;
using System.Collections.Generic;
using System.Text;
using DCI.HRMS.Model;
using System.Data;
using DCI.HRMS.Model.Personal;
using System.Collections;

namespace DCI.HRMS.Persistence
{
    public interface IEmployeeDao
    {
        void FillObject(DataRow row, EmployeeInfo employee);
        void FillObject(DataRow row, PersonInfo employee);
        void FillObject(DataRow row, EmployeeDataInfo employee);

        EmployeeInfo Select(string employeeCode);
        DataSet SelectAllEmp();
        DataSet GenerateEmp();
        ArrayList SelectCurEmp();
        ArrayList SelectCurEmpByPosition(string posit);
        ArrayList GetResignEmp(DateTime from, DateTime to);
        ArrayList SelectCurEmpByDVCD(string dvcd,string grpot);
        ArrayList SelectCurEmpListByDVCD(string dvcd);
        ArrayList SelectCurEmpByBusWay(string busway, string stop);
        DataSet SelectCurEmpByBusWayDataset(string busway, string stop);

        EmployeeDataInfo GetEmployeeDataInfo(string empCode);
        void SaveEmployeeInfo(EmployeeDataInfo empInfo);
        void UpdateEmployeeInfo(EmployeeDataInfo empInfo);
        void DeleteEmployeeInfo(string empId);

        ArrayList GetEmployeeFamily(string empCode);
        void SaveEmployeeFamily(FamilyInfo emfm);
        void UpdateEmployeeFamily(FamilyInfo emfm);
        void DeleteloyeeFamily(FamilyInfo emfm);

         void UpdateEmployeeBusWay(string code, string bus, string stop);

        ArrayList GetEmployeeWorkHistory(string empCode);
        void SaveEmployeeWorkHistory(WorkHistoryInfo emfm);
        void UpdateEmployeeWorkHistory(WorkHistoryInfo emfm);
        void DeleteloyeeWorkHistory(WorkHistoryInfo emfm);

        void EmployeeResignation(string empCode, DateTime rsDate, string rsType, string rsReason,string rsRemark, string by);

        void EmployeeSetEmail(string code, string email, string extension);

        ArrayList GetEmployeeCodeTransfer(string empCode);
        ArrayList GetEmployeeCodeTransfer(string empCode, DateTime transDate, string transStatus);
        EmployeeCodeTransferInfo GetUnqEmpCodeTransfer(string oCode, string newCode);
        void SaveEmployeeCodeTransfer(EmployeeCodeTransferInfo empInfo);
        void UpdateEmployeeCodeTransfer(EmployeeCodeTransferInfo empInfo);
        void DeleteEmployeeCodeTransfer(string oldCode, string newCode);
        DataSet GetRecordHistoryDataSet(string empCode);


        DataSet GetManpowerByGrpot(string pdate, string posit, string grpot);
        int GetEmpCardIssue(string empCode);
        int GenEmpCardIssue(string empCode);
         int GenEmpCardIssue(string empCode, string name);
        DataSet GetManpowerForBC(DateTime pdate);
        DataSet GetManpowerForBC1(DateTime pdate, string pDvcd);

    }
}
