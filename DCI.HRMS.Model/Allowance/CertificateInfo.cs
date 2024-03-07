using System;
using System.Collections.Generic;
using System.Text;
using DCI.HRMS.Model.Common;

namespace DCI.HRMS.Model.Allowance
{
    public class CertificateInfo : ObjectInfo
    {
        string cerType = "";
        string cerName = "";


        int level = 0;
        decimal cerCost = 0M;
        string remark = "";


        public CertificateInfo()
        {
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


        public decimal CerCost
        {
            get { return cerCost; }
            set { cerCost = value; }
        }
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        public string DispName
        {
            get
            {
                return cerType + ":" + cerName;
            }
        }
    }
}
