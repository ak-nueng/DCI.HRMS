using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DCI.HRMS.Model.Common;

namespace DCI.HRMS.Model.Satisfy
{
    public class SatisfyMainInfo : ObjectInfo
    {


        private string satisfyMainId = "";
        private string satisfyMainName = "";
        private bool active = false;
        private DateTime startVote = DateTime.Parse("1900-01-01");
        private DateTime endVote = DateTime.Parse("1900-01-01");
    



        public SatisfyMainInfo()
        {
        }
        public string SatisfyMainId
        {
            get { return satisfyMainId; }
            set { satisfyMainId = value; }
        }
        public string SatisfyMainName
        {
            get { return satisfyMainName; }
            set { satisfyMainName = value; }
        }




        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        public DateTime StartVote
        {
            get { 
                if (startVote <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || startVote <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return startVote;
                }
            }
            set { startVote = value; }
        }


        public DateTime EndVote
        {
            get { 
                if (endVote <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || endVote <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return endVote;
                }
            }
            set { endVote = value; }
        }

    }
}
