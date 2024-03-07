using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DCI.HRMS.Model.Attendance
{
    public class EmployeeWorkTimeInfo
    {
        private string empCode = string.Empty;
        private DateTime workDate = DateTime.MinValue;
        private DateTime workFrom =  DateTime.MinValue;
        private DateTime workTo = DateTime.MinValue;
        private string shift = string.Empty;
        private bool timeOk = true;
        private string remark = string.Empty;
        public EmployeeWorkTimeInfo()
        {

        }
        public string EmpCode
        {
            get { return empCode; }
            set { empCode = value; }
        }

        public DateTime WorkDate
        {
            get { 
                if (workDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || workDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return workDate;
                }

            }
            set { workDate = value; }
        }

        public DateTime WorkFrom
        {
            get { 
                if (workFrom <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || workFrom <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return workFrom;
                }
            }
            set { workFrom = value; }
        }

        public DateTime WorkTo
        {
            get { 
                if (workTo <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || workTo <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return workTo;
                }
            }
            set { workTo = value; }
        }

        public string Shift
        {
            get { return shift; }
            set { shift = value; }
        }

        public bool TimeOk
        {
            get { return timeOk; }
            set { timeOk = value; }
        }


        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }




    }
}
