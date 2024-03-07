using System;
using System.Collections.Generic;
using System.Text;
using DCI.HRMS.Model.Personal;
using DCI.HRMS.Model.Organize;
using System.Globalization;

namespace DCI.HRMS.Model.Attendance
{
    [Serializable]
    public class EmployeeLeaveInfo : EmployeeInfo
    {
        private DivisionInfo line;
        private DateTime leaveDate = DateTime.Now;
        private DateTime leaveFromTime = new DateTime(1900,1,1,8,0,0);
        private DateTime leaveToTime = new DateTime(1900,1,1,17,45,0);
        private string leaveType;

        public EmployeeLeaveInfo()
        {
        }

        public DivisionInfo Line
        {
            get { return line; }
            set { line = value; }
        }
        public DateTime LeaveDate
        {
            get {
                if (leaveDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || leaveDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return leaveDate;
                }

            }
            set { leaveDate = value; }
        }
        public DateTime LeaveFromTime
        {
            get { 
                if (leaveFromTime <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || leaveFromTime <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return leaveFromTime;
                }
            }
            set { leaveFromTime = value; }
        }
        public DateTime LeaveToTime
        {
            get { 
                if (leaveToTime <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || leaveToTime <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return leaveToTime;
                }
            }
            set { leaveToTime = value; }
        }
        public string LeaveType
        {
            get { return leaveType; }
            set { leaveType = value; }
        }
        public double TotalLeaveTime
        {
            get
            {
                return 0.0;
            }
        }
    }
}
