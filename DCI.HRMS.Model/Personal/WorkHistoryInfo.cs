using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DCI.HRMS.Model.Common;

namespace DCI.HRMS.Model.Personal
{
   public class WorkHistoryInfo:ObjectInfo
    {
        private string empcode;

        private int rank;
        private string companyname;
        private string address;
        private string tcompanyname;
        private string taddress;
        private DateTime workfrom;
        private DateTime workto;
        private string resignreason;
        private string lastPosition;

      
        public WorkHistoryInfo()
        {
        }

        public string EmpCode
        {
            set { empcode = value; }
            get { return empcode; }
        }
 
        public string CompanyName
        {
            set { companyname = value; }
            get { return companyname; }
        }
        public string Address
        {
            set { address = value; }
            get { return address; }
        }
        public string CompanyNameInThai
        {
            set { tcompanyname = value; }
            get { return tcompanyname; }
        }
        public string AddressInThai
        {
            set { taddress = value; }
            get { return taddress; }
        }
        public DateTime WorkFrom
        {
            set { workfrom = value; }
            get { 
                if (workfrom <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || workfrom <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return workfrom;
                }
            }
        }
        public DateTime WorkTo
        {
            set { workto = value; }
            get { 
                if (workto <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || workto <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return workto;
                }
            }
        }
        public string LastPosition
        {
            get { return lastPosition; }
            set { lastPosition = value; }
        }
        public string ResignReason
        {
            set { resignreason = value; }
            get { return resignreason; }
        }
   



    }
}
