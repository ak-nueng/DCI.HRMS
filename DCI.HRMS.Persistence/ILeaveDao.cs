using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DCI.HRMS.Persistence
{
    public interface ILeaveDao
    {
        ArrayList SelectTotalAnnualByType(string employeeCode , DateTime fromDate, DateTime toDate, string annualType);
        
    }
}
