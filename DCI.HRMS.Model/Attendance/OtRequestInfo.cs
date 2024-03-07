using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DCI.HRMS.Model.Common;


namespace DCI.HRMS.Model.Attendance
{
    public class OtRequestInfo
    {
        private string docId;
        private string emCode = string.Empty;
        private string empType = string.Empty;
        private string empName = string.Empty;
        private string dvcd = string.Empty;
        private string grpot = string.Empty;
        private string shift = string.Empty;
        private string bus = string.Empty;


        private DateTime otDate;
        private string reqId = string.Empty;
        private string jobType = string.Empty;
        private string otFrom = string.Empty;
        private string otTo = string.Empty;
        private string rate1 = string.Empty;
        private string rate15 = string.Empty;
        private string rate2 = string.Empty;
        private string rate3 = string.Empty;
        private string calRes = string.Empty;
        private string nfrom = string.Empty;
        private string nto = string.Empty;
        private string n1 = string.Empty;
        private string n15 = string.Empty;
        private string n2 = string.Empty;
        private string n3 = string.Empty;
        private string timeCard = string.Empty;
        private string rate1From = string.Empty;
        private string rate15From = string.Empty;
        private string rate2From = string.Empty;
        private string rate3From = string.Empty;
        private string rate1To = string.Empty;
        private string rate15To = string.Empty;
        private string rate2To = string.Empty;
        private string rate3To = string.Empty;

        private string otRemark = string.Empty;

        

        private ObjectInfo inform = new ObjectInfo();



        public OtRequestInfo()
        {

        }
        public string EmpName
        {
            get { return empName; }
            set { empName = value; }
        }


        public string Dvcd
        {
            get { return dvcd; }
            set { dvcd = value; }
        }


        public string Grpot
        {
            get { return grpot; }
            set { grpot = value; }
        }
        public string Shift
        {
            get { return shift; }
            set { shift = value; }
        }

        public string Bus
        {
            get { return bus; }
            set { bus = value; }
        }


        public string Rate1From
        {
            get { return rate1From; }
            set { rate1From = value; }
        }

        public string Rate15From
        {
            get { return rate15From; }
            set { rate15From = value; }
        }

        public string Rate2From
        {
            get { return rate2From; }
            set { rate2From = value; }
        }

        public string Rate3From
        {
            get { return rate3From; }
            set { rate3From = value; }
        }

        public string Rate1To
        {
            get { return rate1To; }
            set { rate1To = value; }
        }

        public string Rate15To
        {
            get { return rate15To; }
            set { rate15To = value; }
        }

        public string Rate2To
        {
            get { return rate2To; }
            set { rate2To = value; }
        }


        public string Rate3To
        {
            get { return rate3To; }
            set { rate3To = value; }
        }
        public string DocId
        {
            set { docId = value; }
            get { return docId; }
        }
        public string EmpCode
        {
            set { emCode = value; }
            get { return emCode; }
        }
        public string EmpType
        {
            get { return empType; }
            set { empType = value; }
        }
        public DateTime OtDate
        {
            set { otDate = value; }
            get {
                //if (otDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || otDate <= DateTime.MinValue)
                if (otDate <= new DateTime(2000,1,1))
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return otDate;
                }
            }
        }
        public string ReqId
        {
            set { reqId = value; }
            get { return reqId; }
        }
        public string JobType
        {
            set { jobType = value; }
            get { return jobType; }
        }
        public string OtFrom
        {
            set { otFrom = value; }
            get { return otFrom; }
        }
        public string OtTo
        {
            set { otTo = value; }
            get { return otTo; }
        }
        public string Rate1
        {
            set { rate1 = value; }
            get { return rate1; }
        }
        public string Rate15
        {
            set { rate15 = value; }
            get { return rate15; }
        }
        public string Rate2
        {
            set { rate2 = value; }
            get { return rate2; }
        }
        public string Rate3
        {
            set { rate3 = value; }
            get { return rate3; }
        }
        public string CalRest
        {
            set { calRes = value; }
            get { return calRes; }
        }
        public string N1
        {
            set { n1 = value; }
            get { return n1; }
        }
        public string N15
        {
            set { n15 = value; }
            get { return n15; }
        }
        public string N2
        {
            set { n2 = value; }
            get { return n2; }
        }
        public string N3
        {
            set { n3 = value; }
            get { return n3; }
        }
        public string TimeCard
        {
            set { timeCard = value; }
            get { return timeCard; }
        }
        public string NFrom
        {
            set { nfrom = value; }
            get { return nfrom; }
        }
        public string NTo
        {
            set { nto = value; }
            get { return nto; }
        }

        public string CreateBy
        {
            get { return inform.CreateBy; }
            set { inform.CreateBy = value; }
        }
        public DateTime CreateDateTime
        {
            get { 
                //if (inform.CreateDateTime <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || inform.CreateDateTime <= DateTime.MinValue)
                if (otDate <= new DateTime(2000, 1, 1))
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return inform.CreateDateTime;
                }
            }
            set { inform.CreateDateTime = value; }
        }
        public string LastUpdateBy
        {
            get { return inform.LastUpdateBy; }
            set { inform.LastUpdateBy = value; }
        }
        public DateTime LastUpDateDateTime
        {
            get {
                //if (inform.LastUpdateDateTime <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || inform.LastUpdateDateTime <= DateTime.MinValue)
                if (otDate <= new DateTime(2000, 1, 1))
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return inform.LastUpdateDateTime;
                }
            }
            set { inform.LastUpdateDateTime = value; }
        }
        public ObjectInfo Inform
        {

            set { this.inform = value; }
        }

        public string OtRemark
        {
            get { return otRemark; }
            set { otRemark = value; }
        }

    }
}
