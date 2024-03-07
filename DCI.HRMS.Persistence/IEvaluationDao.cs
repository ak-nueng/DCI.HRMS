using System;
using System.Collections.Generic;
using System.Text;
using DCI.HRMS.Model.Evaluation;
using System.Collections;

namespace DCI.HRMS.Persistence
{
  public  interface IEvaluationDao
    {
       Eva_SalaryInfo GetSalaryInfoUnq(string code, string year);
       ArrayList GetSalaryInfo(string code, string year);
       Eva_BonusInfo GetBonusInfoUnq(string code, string year);
       ArrayList GetBonusInfo(string code, string year);
       void SaveSalaryInfo(Eva_SalaryInfo salaryInfo);
       void UpdateSalaryInfo(Eva_SalaryInfo salaryInfo);
       void SaveBonusInfo(Eva_BonusInfo bonusInfo);
       void UpdateBonusInfo(Eva_BonusInfo bonusInfo);
    }
}
