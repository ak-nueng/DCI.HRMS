using System;
using System.Collections.Generic;
using System.Text;

namespace DCI.HRMS.Model
{
    public class HospitalInfo
    {
        private string code;
        private string ename;
        private string tname;
        private int totalEmp;

        public HospitalInfo() { }

        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }
        public string NameEng
        {
            get { return this.ename; }
            set { this.ename = value; }
        }
        public string NameThai
        {
            get { return this.tname; }
            set { this.tname = value; }
        }
        public string DispText
        {
            get { return code + ":" + ename; }
        }
        public int TotalEmployee
        {
            get { return totalEmp; }
            set { totalEmp = value; }
        }

    }
}
