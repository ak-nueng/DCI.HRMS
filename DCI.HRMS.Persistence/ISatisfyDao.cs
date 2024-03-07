using System;
using System.Collections.Generic;
using System.Text;
using DCI.HRMS.Model.Satisfy;
using System.Collections;

namespace DCI.HRMS.Persistence
{
    public interface ISatisfyDao
    {

        SatisfyMainInfo SelectActiveMainMaster();
        SatisfyMainInfo SelestSatisfyMainMaster(string stMainfId);
        ArrayList SelestSatisfyMainMaster();
        void SaveSatisfyMainMaster(SatisfyMainInfo stfMain);
        void UpdateSatifyMainMsater(SatisfyMainInfo stfMain);
        void DelectSatifyMainMaster(string stMainfId);

        ArrayList SelectActiveSatisfy(string stfMainId);
        SatisfyMasterInfo SelestSatisfyMaster(string stfMainId,string stfId);
        ArrayList SelectSatisfyMaster(string stfMainId);
        void SaveSatisfyMaster(SatisfyMasterInfo stf);
        void UpdateSatifyMsater(SatisfyMasterInfo stf);
        void DelectSatifyMaster(string stfid);



        ArrayList SelestSatisfy(string stfId);
        SatisfyDataInfo SelestSatisfy(string stfId, string empCode);
        ArrayList SelectSatisfy(string stfId, int choice);
        void SaveSatisfy(SatisfyDataInfo stf);
        void UpdateSatify(SatisfyDataInfo stf);




    }
}
