using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace DCI.HRMS.Persistence
{
    public interface IAttendanceDao
    {
        ArrayList SummaryLeaveRecords(string leaveType, string divisionCode , DateTime fromDate, DateTime toDate);
        ArrayList SelectByCriteria(string keyword, string sectionCode, string leaveTypeCode, string status, DateTime fromDate, DateTime toDate);
        ArrayList SelectEmployeeAbsentAlertRecords(string keyword, string sectionCode, DateTime fromDate, DateTime toDate);
        ArrayList SelectTotalAnnualByType(string employeeCode, DateTime fromDate, DateTime toDate, string leaveTypeCode);
        
    }
}
