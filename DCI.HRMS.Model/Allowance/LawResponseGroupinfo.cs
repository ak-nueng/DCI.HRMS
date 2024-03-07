using System;
using System.Collections.Generic;
using System.Text;

namespace DCI.HRMS.Model.Allowance
{
    public class LawResponseGroupInfo
    {
        public LawResponseGroupInfo()
        {


        }

        private string resId = "";

        private string type = "";

   

        private string resName = "";
        private string reaDetail = "";
        private string remark = "";
        public string ResId
        {
            get { return resId; }
            set { resId = value; }
        }
     public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public string ResName
        {
            get { return resName; }
            set { resName = value; }
        }

        public string ReaDetail
        {
            get { return reaDetail; }
            set { reaDetail = value; }
        }


        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

    }
}

