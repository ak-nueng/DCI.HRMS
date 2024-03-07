using System;
using System.Collections.Generic;
using System.Text;

namespace DCI.HRMS.Model.Attendance
{
    public class TimeCardInfo
    {
        DateTime cardDate;
        private string empCode;
        private string cardTime;
        private int cardMachId;
        private string duty;
        private int issue;




        public TimeCardInfo()
        {
        }
        public TimeCardInfo(string _empcode, DateTime _carddate, string _cardtime, int _cardmachid, string _duty, int _issue)
        {
            empCode = _empcode;
            cardDate = _carddate;
            cardTime = _cardtime;
            cardMachId = _cardmachid;
            duty = _duty;
            issue = _issue;

        }
        public DateTime CardDate
        {
            get { return cardDate; }
            set { cardDate = value; }

        }
        public string EmpCode
        {
            get { return empCode; }
            set { empCode = value; }
        }
        public string CardTime
        {
            get { return cardTime; }
            set { cardTime = value; }
        }
        public int CardMachId
        {
            get { return cardMachId; }
            set { cardMachId = value; }
        }
        public string Duty
        {
            get { return duty; }
            set { duty = value; }
        }
        public int Issue
        {
            get { return issue; }
            set { issue = value; }
        }

    }
}
