using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using DCI.HRMS.Model.Attendance;
using System.Data;

namespace DCI.HRMS.Persistence
{
    public interface ITimeCardDao
    {
        ArrayList GetTimeCardByDate(String code, DateTime stdt, DateTime endt);
        DataSet GetTimeCardDataSetByDate(String code, DateTime stdt, DateTime endt);
        ArrayList GetTimeCardByDate(String code, DateTime stdt, DateTime endt, string taffId);
        TimeCardInfo GetUniqTimeCard(string empCode, DateTime tmDate, string tmtime);
        void Insert(TimeCardInfo tcInfo);
        void Delete(TimeCardInfo tcInfo);
        void Update(TimeCardInfo tcInfo);
        void UpdateTimeCardManual(TimeCardManualInfo tmrq);
        void SaveTimeCardManual(TimeCardManualInfo tmrq);
        void DeleteTimeCardManual(TimeCardManualInfo tmrq);
        ArrayList GetTimeCardManual(string code, DateTime cdate, DateTime cdateto, string type);
        DataSet GetTimeCardManualDataSet(string code, DateTime cdate, DateTime cdateto, string type);
        TimeCardManualInfo GetTimeCardManual(string code, DateTime cdate, string type);
        ArrayList GetWorkingHour(string _day, string _shift);
        ArrayList GetWorkingHour(DateTime _date, string _shift);
    }
}
