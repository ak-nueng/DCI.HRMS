using System;
using System.Collections.Generic;
using System.Text;
using DCI.HRMS.Model;
using DCI.HRMS.Model.Attendance;
using System.Collections;

namespace DCI.HRMS.Persistence
{
    public interface IPenaltyDao
    {
        ArrayList GetPenalty(string code);
        ArrayList GetPenalty(string code, DateTime pdate ,DateTime tdate);
        ArrayList SelectPenalty(string code,string pentype, DateTime pdate, DateTime tdate);
        ArrayList GetPenalty(DateTime pdate, DateTime tdate);
        void SavePenalty(PenaltyInfo pen);
        void UpdatePenalty(PenaltyInfo pen);
        void Delete(PenaltyInfo pen);

    }
}
