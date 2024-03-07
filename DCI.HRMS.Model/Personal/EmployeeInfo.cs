using System;
using System.Collections.Generic;
using System.Text;
using DCI.HRMS.Model.Organize;
using System.Collections;
using System.Globalization;

namespace DCI.HRMS.Model.Personal
{
    [Serializable]
    public class EmployeeInfo : PersonInfo
    {
        private string workGroup = string.Empty;
        private string otGroup = string.Empty;
        private string otType = string.Empty;
        private PositionInfo position;
        private string trPosit;

        private DivisionInfo division;
        private HospitalInfo hospital;
        private DateTime joinDate;
        private DateTime resignDate;
        private DateTime probationDate;
        private string rsType;
        private string rsReason;
        private string rsRemark;
        private string company = "";

        private string workType;
        private string employeeType;

        private string bus;
        private string busstop;
        private string busWay;
        private string busStopName;


        private string bank;
        private string bankaccount;
        private string extensionno;
        private string email;
        private string costcenter;


        //===== Nueng 05-06-2015 ======
        private string tposiname;
        private DateTime tposijoin;
        private DateTime annualcalDate;
        //===== Nueng 05-06-2015 ======


        private ArrayList family;
        private ArrayList workHistory;

        private string workcenter;
        private string budgetType;
        private string lineno;
        private string mcno;

        private DateTime contractExpDT;




        public EmployeeInfo()
        {
        }
        public string Company
        {
            get { return company; }
            set { company = value; }
        }
        public PositionInfo Position
        {
            get { return position; }
            set { position = value; }
        }
        public DivisionInfo Division
        {
            get { return division; }
            set { division = value; }
        }
        public HospitalInfo Hospital
        {
            get { return hospital; }
            set { hospital = value; }
        }
        public DateTime JoinDate
        {
            get
            {
                if (joinDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || joinDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return joinDate;
                }
            }
            set { joinDate = value; }
        }
        public DateTime ResignDate
        {
            get
            {
                if (resignDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || resignDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return resignDate;
                }
            }
            set { resignDate = value; }
        }
        public DateTime ProbationDate
        {
            get
            {
                if (probationDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || probationDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return probationDate;
                }
            }
            set { probationDate = value; }
        }
        public string ResignType
        {
            get { return rsType; }
            set { rsType = value; }
        }
        public string ResignReason
        {
            get { return rsReason; }
            set { rsReason = value; }
        }
        public bool Resigned
        {
            get
            {
                if (resignDate == DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || resignDate == DateTime.MinValue)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
        }

        public string RsRemark
        {
            get { return rsRemark; }
            set { rsRemark = value; }
        }
        public string WorkGroupLine
        {
            get { return workGroup; }
            set { workGroup = value; }
        }
        public string OtType
        {
            get { return otType; }
            set { otType = value; }
        }
        public string OtGroupLine
        {
            get { return otGroup; }
            set { otGroup = value; }
        }
        public string WorkType
        {
            get { return workType; }
            set { workType = value; }
        }
        public string EmployeeType
        {
            get { return employeeType; }
            set { employeeType = value; }

        }
        public string Bus
        {
            get { return bus; }
            set { bus = value; }

        }
        public string BusStop
        {
            get { return busstop; }
            set { busstop = value; }

        }
        public string BusWay
        {
            get { return busWay; }
            set { busWay = value; }
        }


        public string BusStopName
        {
            get { return busStopName; }
            set { busStopName = value; }
        }
        public string Bank
        {
            get { return bank; }
            set { bank = value; }

        }
        public string BankAccount
        {
            get { return bankaccount; }
            set { bankaccount = value; }

        }
        public string ExtensionNumber
        {
            get { return extensionno; }
            set { extensionno = value; }

        }
        public string Email
        {
            get { return email; }
            set { email = value; }


        }
        public ArrayList FamilyMember
        {
            set { family = value; }
            get { return family; }
        }
        public ArrayList WorkHistory
        {
            set { workHistory = value; }
            get { return workHistory; }
        }
        public string Costcenter
        {
            get { return costcenter; }
            set { costcenter = value; }
        }

        //===== Nueng 05-06-2015 ======
        public DateTime Tposijoin
        {
            get
            {
                if (tposijoin <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || tposijoin <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return tposijoin;
                }
            }
            set { tposijoin = value; }
        }


        public DateTime AnnualcalDate
        {
            get
            {
                if (annualcalDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || annualcalDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return annualcalDate;
                }
            }
            set { annualcalDate = value; }
        }

        public string Tposiname
        {
            get { return tposiname; }
            set { tposiname = value; }
        }




        //===== Nueng 18-06-2019 ======
        public string BudgetType
        {
            get { return budgetType; }
            set { budgetType = value; }
        }

        public string Workcenter
        {
            get { return workcenter; }
            set { workcenter = value; }
        }

        public string Lineno
        {
            get { return lineno; }
            set { lineno = value; }
        }

        public string Mcno
        {
            get { return mcno; }
            set { mcno = value; }
        }

        public DateTime ContractExpDT
        {
            get
            {
                if (contractExpDT <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || contractExpDT <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return contractExpDT;
                }
            }
            set { contractExpDT = value; }
        }

    }
}
