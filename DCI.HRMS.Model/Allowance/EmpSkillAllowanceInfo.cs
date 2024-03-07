using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DCI.HRMS.Model.Common;

namespace DCI.HRMS.Model.Allowance
{
    public class EmpSkillAllowanceInfo : ObjectInfo
    {
        private string recordId;
        private string empCode;
        private DateTime month;


        private string certType;
        private string certName;
        private int certLevel;
        private decimal certCost;
        private DateTime nextTest;
        private string remark;

        public string RecordId
        {
            get { return recordId; }
            set { recordId = value; }
        }

        public string EmpCode
        {
            get { return empCode; }
            set { empCode = value; }
        }
        public DateTime Month
        {
            get { 
                if (month <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || month <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return month;
                }
            }
            set { month = value; }
        } 
        public string CertType
        {
            get { return certType; }
            set { certType = value; }
        }

        public string CertName
        {
            get { return certName; }
            set { certName = value; }
        }

        public int CertLevel
        {
            get { return certLevel; }
            set { certLevel = value; }
        }

        public decimal CertCost
        {
            get { return certCost; }
            set { certCost = value; }
        }

        public DateTime NextTest
        {
            get { 
                if (nextTest <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || nextTest <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return nextTest;
                }
            }
            set { nextTest = value; }
        }


        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
    }



    public class EmployeeSkillAllowanceInfo
    {
        public string code { get; set; }
        public string ctype { get; set; }
        public string clevel { get; set; }
        public string remark { get; set; }
        public string cname { get; set; }
        public string cdate { get; set; }
        public string cexpire { get; set; }
        public string ccost { get; set; }

        public EmployeeSkillAllowanceInfo()
        {

        }
    }

}
