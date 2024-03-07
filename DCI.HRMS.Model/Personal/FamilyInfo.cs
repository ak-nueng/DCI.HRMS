using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DCI.HRMS.Model.Common;

namespace DCI.HRMS.Model.Personal
{
    public class FamilyInfo:ObjectInfo
    {
        private string empCode;
        private string relation;
        private string relationType;

        private NameInfo nameTH = new NameInfo();
        private DateTime birth;
        private string idNo;
        private int taxDed;


        public FamilyInfo()
        {
        }

        public string EmpCode
        {
            set { empCode = value; }
            get { return empCode; }
        }
        public string Relation
        {
            set { relation = value; }
            get { return relation; }
        }
        public string RelationType
        {
            get { return relationType; }
            set { relationType = value; }
        }
        public NameInfo NameInThai
        {
            get { return nameTH; }
            set { nameTH = value; }
        }
        public DateTime Birth
        {
            set { birth = value; }
            get { 
                if (birth <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || birth <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return birth;
                }
            }
        }
        public string IdNo
        {
            set { idNo = value; }
            get { return idNo; }
        }
        public int TaxDed
        {
            set { taxDed = value; }
            get { return taxDed; }
        }
    }
}
