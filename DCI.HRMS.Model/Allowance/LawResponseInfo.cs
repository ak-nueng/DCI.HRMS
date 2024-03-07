using System;
using System.Collections.Generic;
using System.Text;

namespace DCI.HRMS.Model.Allowance
{
    public class LawResponseInfo
    {
        public LawResponseInfo()
        {


        }

        private string resId="";
        private string groupResId = "";


        private string resName="";
        private string reaDetail="";
        private string remark="";
        public string ResId
        {
            get { return resId; }
            set { resId = value; }
        }
        public string GroupResId
        {
            get { return groupResId; }
            set { groupResId = value; }
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
