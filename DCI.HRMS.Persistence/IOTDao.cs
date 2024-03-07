using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using DCI.HRMS.Model.Attendance;
using System.Data;

namespace DCI.HRMS.Persistence
{
    public interface IOTDao
    {
        ArrayList GetOtRates();
        OtRateInfo GetOtRate(string rate, string wtype);
        void SaveOtRate(OtRateInfo rq);
        void UpdateOtRate(OtRateInfo rq);
        void DeteteOtRatet(OtRateInfo rq);


        ArrayList GetOTRequest(string empcode, DateTime odate, string reqid, string dvcd);
        ArrayList GetOTRequest(string empcode, DateTime odate, string reqid, string dvcd, string calResult);
        ArrayList GetOTRequest(string empcode, DateTime odate, DateTime odateto, string reqid, string dvcd, string otfrom, string otto, string otremark);
        ArrayList GetOTRequest(string empcode,string empType, string posit, DateTime odate, DateTime odateto, string reqid, string dvcd, string otfrom, string otto, string grpl, string grpot, string otremark);
        ArrayList GetOTRequest(string empcode, string empType,  DateTime odate, DateTime odateto, string reqid, string dvcd, string otfrom, string otto, string grpl, string grpot);

        DataSet GetOTRequestDataSet(string empcode, DateTime odate, DateTime odateto, string reqid, string dvcd, string otfrom, string otto, string otremark);

        void SaveOtRequest(OtRequestInfo rq);
        void UpdateOtReqest(OtRequestInfo rq);
        void UpdateOtReqestById(OtRequestInfo rq);
        void DeteteOtRequest(OtRequestInfo rq);

        ArrayList GetOTBus(DateTime odate);
        DataSet GetOTBusDataSet(DateTime odate);
        DataSet GetOTSumaryDataSet(DateTime odate, DateTime odateto);
        DataSet GetOTSumaryEMPDataSet(DateTime odate, DateTime odateto);
        DataSet GetOTSumaryEMPDataSet(string empCode, DateTime odate, DateTime odateto);
        DataSet GetOTSumaryDVCDDataSet(DateTime odate, DateTime odateto);
        DataSet GetOTSumaryForBC(DateTime pdate);
        DataSet GetOTSumaryForBCDVCD(DateTime pdate, string pdvcd);

    }
}
