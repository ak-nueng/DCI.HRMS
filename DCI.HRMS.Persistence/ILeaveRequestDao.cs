using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using DCI.HRMS.Model.Attendance;
using System.Data;

namespace DCI.HRMS.Persistence
{
    public interface ILeaveRequestDao
    {
        ArrayList GetLeaveReq(string empCode, DateTime lvDateFrom,DateTime lvDateTo,string lvType);
        DataSet GetLeaveReqDataSet(string empCode, DateTime lvDateFrom, DateTime lvDateTo, string lvType);
        DataSet  GetLeaveReq(string empCode);
        ArrayList GetLeaveReq(string empCode,string lvType );
        EmployeeLealeRequestInfo GetLeaveReq(string code, DateTime rqDate, string lvType);
        void SaveLeaveReq(EmployeeLealeRequestInfo lvrq);
        void UpdateLeaveReq(EmployeeLealeRequestInfo lvrq);
        void DeleteLeaveReq(EmployeeLealeRequestInfo lvrq);
        ArrayList GetLeavReason();

    }
}
