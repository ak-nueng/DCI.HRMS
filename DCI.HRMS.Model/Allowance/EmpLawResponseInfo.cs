using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DCI.HRMS.Model.Allowance
{
    public class EmpLawResponseInfo
    {
        public EmpLawResponseInfo()
        {
        }
        private string lawRespId = "";
        private string empCode = "";
        private string licenseNo = "";
        private DateTime licenseDate = DateTime.MinValue;
        private DateTime licenseExp = DateTime.MinValue;
        private int spare;
        private string remark = "";
        public string LawRespId
        {
            get { return lawRespId; }
            set { lawRespId = value; }
        }

        public string EmpCode
        {
            get { return empCode; }
            set { empCode = value; }
        }

        public string LicenseNo
        {
            get { return licenseNo; }
            set { licenseNo = value; }
        }

        public DateTime LicenseDate
        {
            get
            {
                if (licenseDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || licenseDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return licenseDate;
                }
            }
            set { licenseDate = value; }
        }

        public DateTime LicenseExp
        {
            get
            {
                if (licenseExp <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || licenseExp <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return licenseExp;
                }
            }
            set { licenseExp = value; }
        }

        public int Spare
        {
            get { return spare; }
            set { spare = value; }
        }


        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

    }
}
