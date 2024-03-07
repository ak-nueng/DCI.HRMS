using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using DCI.HRMS.Model.Common;

namespace DCI.HRMS.Model.Attendance
{

    public class OtRateInfo : ObjectInfo
    {

        private string rateId;
        private string otFrom;
        private string otTo;
        private string rate1;
        private string rate15;
        private string rate2;
        private string rate3;
        private string shift;
        private string workType;
        private string rate1From;
        private string rate15From;
        private string rate2From;
        private string rate3From;
        private string rate1To;
        private string rate15To;
        private string rate2To;
        private string rate3To;

        
        private ObjectInfo inform = new ObjectInfo();
        public OtRateInfo()
        {
            //rateId = "";
            //otFrom = "";
            //otTo = "";
            //rate1 = "";
            //rate15 = "";
            //rate2 = "";
            //rate3 = "";
            //shift = "";
            //workType = "";
            //rate1From = "";
            //rate15From = "";
            //rate2From = "";
            //rate3From = "";
            //rate1To = "";
            //rate15To = "";
            //rate2To = "";
            //rate3To = "";
        }


        public string Rate1From
        {
            get { return rate1From; }
            set { rate1From = value; }
        }

        public string Rate15From
        {
            get { return rate15From; }
            set { rate15From = value; }
        }

        public string Rate2From
        {
            get { return rate2From; }
            set { rate2From = value; }
        }

        public string Rate3From
        {
            get { return rate3From; }
            set { rate3From = value; }
        }

        public string Rate1To
        {
            get { return rate1To; }
            set { rate1To = value; }
        }

        public string Rate15To
        {
            get { return rate15To; }
            set { rate15To = value; }
        }

        public string Rate2To
        {
            get { return rate2To; }
            set { rate2To = value; }
        }


        public string Rate3To
        {
            get { return rate3To; }
            set { rate3To = value; }
        }
        
        public string RateId
        {
            set { rateId = value; }
            get { return rateId; }
        }
        public string OtFrom
        {
            set { otFrom = value; }
            get { return otFrom; }
        }
        public string OtTo
        {
            set { otTo = value; }
            get { return otTo; }
        }
        public string Rate1
        {
            set { rate1 = value; }
            get { return rate1; }
        }
        public string Rate15
        {
            set { rate15 = value; }
            get { return rate15; }
        }
        public string Rate2
        {
            set { rate2 = value; }
            get { return rate2; }
        }
        public string Rate3
        {
            set { rate3 = value; }
            get { return rate3; }
        }
        public string Shift
        {
            set { shift = value; }
            get { return shift; }
        }
        public string WorkType
        {
            set { workType = value; }
            get { return workType; }
        }



        public ArrayList GetUniqueRate(ArrayList shtype)
        {
            ArrayList shre = new ArrayList();
            foreach (OtRateInfo test in shtype)
            {
                foreach (OtRateInfo temp in shre)
                    if (temp.RateId == test.RateId)
                        goto next;
                shre.Add(test);
            next: ;
            }

            return shre;

        }
        public string DisplayText
        {
            get { return RateId + " :" + OtFrom + "-" + otTo; }
        }

    }
}
