using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using DCI.HRMS.Model;
using DCI.HRMS.Model.Attendance;

namespace DCI.HRMS.Persistence
{
    public interface IShiftDao
    {
        ArrayList GetMonthShiftByGroup(string shgrpfdf,int year);
        MonthShiftInfo GetMonthShift(string ym, string shgrp);
        void Insert(MonthShiftInfo mhinfo);
        void Update(MonthShiftInfo mhinfo);
        void Delete(MonthShiftInfo mhinfo);

        ArrayList GetShiftDataByCode(string empCode,int year);
        EmployeeShiftInfo GetShiftData(string yearmonth, string empcode);
        void Insert(EmployeeShiftInfo empsh);
        void Update(EmployeeShiftInfo empsh);
        void Delete(EmployeeShiftInfo empsh);
        ArrayList GetShiftAllType();

        ArrayList GenerateEmpShiftData(MonthShiftInfo shgroup, string shsts);
        ArrayList GetDVCDGrpOT(string empdvcd);
    }
}
