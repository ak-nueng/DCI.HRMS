using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DCI.HRMS.Model.Common;

namespace DCI.HRMS.Model.Welfare
{
    public class LockerInfo
    {
        public LockerInfo()
        {
        }
        private string lockerId = "";

        public string LockerId
        {
            get { return lockerId; }
            set { lockerId = value; }
        }
        private string keyCode = "";

        public string KeyCode
        {
            get { return keyCode; }
            set { keyCode = value; }
        }
        private string empCode = "";

        public string EmpCode
        {
            get { return empCode; }
            set { empCode = value; }
        }
        private string eName = "";

        public string EName
        {
            get { return eName; }
            set { eName = value; }
        }
        private string tName = "";

        public string TName
        {
            get { return tName; }
            set { tName = value; }
        }
        private DateTime resign = DateTime.Parse("01/01/1900  00:00:00");

        public DateTime Resign 
        {
            get { 
                if (resign <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || resign <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return resign;
                }

            }
            set { resign = value; }
        }
        private string dv_ename = "";

        public string Dv_ename
        {
            get { return dv_ename; }
            set { dv_ename = value; }
        }
        private string remark = "";

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }


    }
}
