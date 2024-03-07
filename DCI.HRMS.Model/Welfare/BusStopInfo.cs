using System;
using System.Collections.Generic;
using System.Text;

namespace DCI.HRMS.Model.Welfare
{
    public class BusStopInfo
    {
        private string code;
        private string ename;
        private string tname;
        private string stopCode;
        private string busway;

       
        private string timeDay;
        private string timeNight;
        private int order;
        private int numEmp = 0;

 

        public BusStopInfo()
        {
        }
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        public string StopCode
        {
            get { return stopCode; }
            set { stopCode = value; }
        }
        public string StopName
        {
            //get { return ename; }
            get { return code + ":" + ename; }
            set { ename = value; }
        }
        public string DispText
        {
            get { return code + ":" + ename; }
           // set { tname = value; }
        }
        public string TimeDay
        {
            get { return timeDay; }
            set { timeDay = value; }
        }


        public string TimeNight
        {
            get { return timeNight; }
            set { timeNight = value; }
        }

        public int Order
        {
            get { return order; }
            set { order = value; }
        }
        public int NumEmp
        {
            get { return numEmp; }
            set { numEmp = value; }
        }

        public string Busway
        {
            get { return busway; }
            set { busway = value; }
        }
    }
}
