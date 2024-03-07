using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using DCI.HRMS.Model.Allowance;

namespace DCI.HRMS.Persistence
{
   public interface ILawResponseDao
    {
       ArrayList SelectAllGroupMaster();
       LawResponseGroupInfo GetGroupMasterUnq(string id);
       ArrayList SelectGroupMaster(string type);
       void SaveGroupMaster(LawResponseGroupInfo grp);
       void UpdateGroupMaster(LawResponseGroupInfo grp);
       void DeleteGroupMaster(string id );

       ArrayList SelectAllMaster(string groupId);
       ArrayList SelectAllMaster();
       LawResponseInfo GetMaster( string resId);
       void SaveMaster(LawResponseInfo master);
       void UpdateMaster(LawResponseInfo master);
       void DeleteMaster(string id);

       ArrayList SelectEmpResponse(string respId, string code );
       void SaveEmpResponse(EmpLawResponseInfo master);
       void UpdateEmpResponse(EmpLawResponseInfo master);
       void DeleteEmpResponse(string respId,string code);
    }
}
