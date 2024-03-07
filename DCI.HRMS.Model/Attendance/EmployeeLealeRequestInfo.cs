using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DCI.HRMS.Model.Common;

namespace DCI.HRMS.Model.Attendance
{
    public class EmployeeLealeRequestInfo
    {
        private string emCode;
        private string docId;
        private DateTime lvDate;
        private string lvType;
        private string lvFrom;
        private string lvTo;
        private string totalHour;
        private int totalMinute;
        private string payStatus;
        private int lvNo;
        private string reason;
        private ObjectInfo inform = new ObjectInfo();

        public EmployeeLealeRequestInfo()
        {

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
        public DateTime LvDate
        {
            set
            {
                lvDate = value;
            }
            get {
                if (lvDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || lvDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return lvDate;
                }
            }
        }
        public string LvType
        {
            set { lvType = value; }
            get { return lvType; }
        }
        public string LvFrom
        {
            set { lvFrom = value; }
            get { return lvFrom; }
        }

        public string LvTo
        {
            set { lvTo = value; }
            get { return lvTo; }
        }
        public string TotalHour
        {
            set { totalHour = value; }
            get { return totalHour; }
        }
        public int TotalMinute
        {
            set { totalMinute = value; }
            get { return totalMinute; }
        }
        public string PayStatus
        {
            set { payStatus = value; }
            get { return payStatus; }
        }
        public int LvNo
        {
            set { lvNo = value; }
            get { return lvNo; }
        }
        public string Reason
        {
            set { reason = value; }
            get { return reason; }
        }

        public string CreateBy
        {
            get { return inform.CreateBy; }
        }
        public DateTime CreateDateTime
        {
            get { 
                if (inform.CreateDateTime <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || inform.CreateDateTime <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return inform.CreateDateTime;
                }

            }
        }
        public string LastUpdateBy
        {
            get { return inform.LastUpdateBy; }
        }
        public DateTime LastUpDateDateTime
        {
            get { 
                if (inform.LastUpdateDateTime <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || inform.LastUpdateDateTime <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return inform.LastUpdateDateTime;
                }
            }
        }
        public ObjectInfo Inform
        {

            set { this.inform = value; }
        }

    }
}
