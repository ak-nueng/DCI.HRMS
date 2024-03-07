using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DCI.HRMS.Model.Organize
{
    [Serializable]
    public class PositionInfo
    {
        private string code;
        private string ename;
        private string tname;
        private int totalEmp;


        private static ArrayList currentPositions = new ArrayList();

        public PositionInfo() { }

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

        public int TotalEmployee
        {
            get { return totalEmp; }
            set { totalEmp = value; }
        }
        public string DispText
        {
            get { return code + ":" + ename; }
        }

        public static ArrayList CurrentPositions
        {
            get { return currentPositions; }
            set { currentPositions = value; }
        }
    }
}
