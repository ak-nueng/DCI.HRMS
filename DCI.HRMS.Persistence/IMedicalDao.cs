using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using DCI.HRMS.Model.Welfare;

namespace DCI.HRMS.Persistence
{
   public interface IMedicalDao
    {
       ArrayList GetMedHospital();
       ArrayList GetMedSymptom();
        ArrayList GetMedDistrict();
        ArrayList GetMedProvince();
       ArrayList GetMedical(DateTime datefrom,DateTime dateto);
       ArrayList GetMedical(string code, DateTime dateFrom,DateTime dateTo  );
       ArrayList GetMedical(string docidfrom, string docidto);
       ArrayList GetMedical(string code, string docid, DateTime trdatefrom, DateTime trdateto, string patientype);
       MedicalAllowanceInfo GetMedical(string docid);
       void AddMedical(MedicalAllowanceInfo med);
       void UpdateMedical(MedicalAllowanceInfo med);
       void DeleteMedical(MedicalAllowanceInfo med);
   }
}
