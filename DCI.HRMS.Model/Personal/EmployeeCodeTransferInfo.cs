using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DCI.HRMS.Model.Common;

namespace DCI.HRMS.Model.Personal
{
    public class EmployeeCodeTransferInfo : ObjectInfo
    {

        private DateTime transferDate;
        private string oldCode;
        private string newCode;
        private string transferStatus="";
        public DateTime TransferDate
        {
            get { 
                if (transferDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || transferDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return transferDate;
                }

            }
            set { transferDate = value; }
        }

        public string OldCode
        {
            get { return oldCode; }
            set { oldCode = value; }
        }

        public string NewCode
        {
            get { return newCode; }
            set { newCode = value; }
        }


        public string TransferStatus
        {
            get { return transferStatus; }
            set { transferStatus = value; }
        }

    }
}
