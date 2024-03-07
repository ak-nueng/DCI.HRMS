using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DCI.HRMS.Model.Welfare
{
    public class BusWayInfo
    {
        private string code;

        private string ename;
        private string tname;


        private ArrayList stops = new ArrayList();

        private int order = 0;
        private int emp = 0;



        public BusWayInfo()
        {
        }
        public string Code
        {
            get { return code; }
            set { code = value; }
        }


        public string NameEng
        {
            get { return ename; }
            set { ename = value; }
        }
        public string NameThai
        {
            get { return tname; }
            set { tname = value; }
        }
        public string DispText
        {
            get { return code + ":" + tname; }
        }


        public ArrayList Stops
        {
            get { return stops; }
            set { stops = value; }
        }
        public BusStopInfo GetStop(string _code)
        {
            foreach (BusStopInfo var in stops)
            {
                if (var.Code == _code)
                {
                    return var;

                }
            }
            return null;
        }

        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        public int NumEmp
        {
            get { return emp; }
            set { emp = value; }
        }


    }
}
