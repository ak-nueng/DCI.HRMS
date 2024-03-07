using System;
using System.Collections.Generic;
using System.Text;
using DCI.HRMS.Model.Common;

namespace DCI.HRMS.Model.Attendance
{
    public class PenaltyInfo : ObjectInfo
    {
        private string penaltyId;
        private string empCode;
        private DateTime penaltyDate = new DateTime(1900,1,1);
        private DateTime penaltyFrom = new DateTime(1900, 1, 1);
        private DateTime penaltyTo = new DateTime(1900, 1, 1);
        private int penaltyTotal = 0;
        private string penaltyType;
        private string wDescription;
        private DateTime wFrom = new DateTime(1900, 1, 1);
        private DateTime wTo = new DateTime(1900, 1, 1);
        private int wTotal = 0;
        public PenaltyInfo()
        {
        }
        public DateTime PenaltyDate
        {
            get { return penaltyDate; }
            set { penaltyDate = value; }
        }

        public int PenaltyTotal
        {
            get { return penaltyTotal; }
            set { penaltyTotal = value; }
        }

        public string WDescription
        {
            get { return wDescription; }
            set { wDescription = value; }
        }

        public DateTime WFrom
        {
            get { return wFrom; }
            set { wFrom = value; }
        }

        public DateTime WTo
        {
            get { return wTo; }
            set { wTo = value; }
        }


        public int WTotal
        {
            get { return wTotal; }
            set { wTotal = value; }
        }

   
        public string PenaltyId
        {
            get { return penaltyId; }
            set { penaltyId = value; }
        }
        public string EmpCode
        {
            get { return empCode; }
            set { empCode = value; }
        }
        public DateTime PenaltyFrom
        {
            get { return penaltyFrom; }
            set { penaltyFrom = value; }
        }
        public DateTime PenaltyTo
        {
            get { return penaltyTo; }
            set { penaltyTo = value; }
        }
        public string PenaltyType
        {
            get { return penaltyType; }
            set { penaltyType = value; }
        }
     

    }
}
