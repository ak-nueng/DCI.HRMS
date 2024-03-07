using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DCI.HRMS.Model.Common;

namespace DCI.HRMS.Model.Allowance
{
    public class EmpCertInfo : ObjectInfo
    {
        string recordId = "";

        string empCode = "";


        string cerType = "";
        string cerName = "";
        int level = 0;
        string remark = "";
        DateTime certDate;
        DateTime expireDate;


        public EmpCertInfo()
        {
        }
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
        public string CerType
        {
            get { return cerType; }
            set { cerType = value; }
        }

        public string CerName
        {
            get { return cerName; }
            set { cerName = value; }
        }
        public int Level
        {
            get { return level; }
            set { level = value; }
        }


 
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        public DateTime CertDate
        {
            get {
                if (certDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || certDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return certDate;
                }
            }
            set { certDate = value; }
        }


        public DateTime ExpireDate
        {
            get {
                if (expireDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || expireDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return expireDate;
                }
            }
            set { expireDate = value; }
        }
        public string DispName
        {
            get
            {
                return cerType + ":" + CerName;
            }
        }

    }
}

