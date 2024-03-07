using System;
using System.Collections.Generic;
using System.Text;

namespace DCI.HRMS.Model
{
    [Serializable]
    public class AddressInfo
    {
        private string taddress="";
        private string tsubdistrict="";
        private string tdistrict="";
        private string tprovince="";
        private string telephone="";
        public AddressInfo()
        {
        }
        public string Address
        {
            set { taddress = value; }
            get { return taddress; }
        }
        public string Subdistrict
        {
            set { tsubdistrict = value; }
            get { return tsubdistrict; }
        }
        public string District
        {
            set { tdistrict = value; }
            get { return tdistrict; }
        }
        public string Province
        {
            set { tprovince = value; }
            get { return tprovince; }
        }
        public string  Telephone
        {
            set {   telephone = value; }
            get { return telephone; }
        }
        public override string ToString()
        {
            return taddress + " " + tsubdistrict + " " + tdistrict + " " + tprovince;
        }



    }

}
