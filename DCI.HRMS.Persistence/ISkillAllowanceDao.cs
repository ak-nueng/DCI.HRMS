using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using DCI.HRMS.Model.Allowance;

namespace DCI.HRMS.Persistence
{
    public interface ISkillAllowanceDao
    {


        CertificateInfo GetCerType(string type, int level);
        ArrayList GetAllType();
        ArrayList GetCertLevel(string type);
        void SaveCerType(CertificateInfo sklMstr);
        void UpdateCerType(CertificateInfo sklMstr);
        void DeleteCerType(string type, int level);


        EmpSkillAllowanceInfo GetSkillByCode(string empCode, DateTime month, string cerType, int cerLevel);
        ArrayList GetSkillByCode(string empCode,string  month);
        void SaveSkillAllowance(EmpSkillAllowanceInfo empSkl);
        void DeleteSkillAllow(string rcId);

        EmpCertInfo GetCertificateByCode(string empCode, string cerType, int cerLevel);
        ArrayList GetCertificateByCode(string empCode);
        void SaveEmpCertificate(EmpCertInfo empCert);
        void UpdateEmpCertificate(EmpCertInfo empCert);
        void DeleteEmpCertificate(string rcId);
    }
}
    