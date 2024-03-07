using System;
using System.Collections.Generic;

using System.Text;
using DCI.HRMS.Model.Common;

namespace DCI.HRMS.Model.Personal
{
    public class PropertyInfo : ObjectInfo
    {
        string propertyId;
        string propertyName;
        string unit;
        string data;
        string detail;
        decimal price1;
        decimal price2;
        string remark;
        string usedBy;

        public PropertyInfo()
        {
        }

        public string PropertyId
        {
            get { return propertyId; }
            set { propertyId = value; }
        }

        public string PropertyName
        {
            get { return propertyName; }
            set { propertyName = value; }
        }

        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        public string Data
        {
            get { return data; }
            set { data = value; }
        }

        public string Detail
        {
            get { return detail; }
            set { detail = value; }
        }

        public decimal Price1
        {
            get { return price1; }
            set { price1 = value; }
        }

        public decimal Price2
        {
            get { return price2; }
            set { price2 = value; }
        }


        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        public string UsedBy
        {
            get { return usedBy; }
            set { usedBy = value; }
        }

    }
}
