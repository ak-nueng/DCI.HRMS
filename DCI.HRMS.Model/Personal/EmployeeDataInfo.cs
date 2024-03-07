using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DCI.HRMS.Model.Personal
{
    [Serializable]
    public class EmployeeDataInfo: EmployeeInfo
    {
        private int grad;
        private int rank;
        private decimal salary;
        private decimal wedge;
        private decimal housing;
        private decimal positAllawance;
        private decimal skillAllawance;
        private decimal insuran;
        private string insuranno;
        private decimal donate;
        private decimal interest;
        private decimal loan;
        private string loanno;
        private decimal houseded;
        private decimal ltf;
        private decimal profesionalAllowance;
        private decimal handycap;
        private decimal mobileAllawance;
        private decimal gasolineAllowance;
        private decimal talent;
               
        private decimal gsbLoan;    
        private decimal coopLoan;

        private ProvidenceInfo pv;

        private CooperativeInfo coo;

   


        public EmployeeDataInfo()
        {

        }

        public int Grad
        {
            get { return this.grad; }
            set { this.grad = value; }
        }
        public int Rank
        {
            get { return this.rank; }
            set { this.rank = value; }
        }
        public decimal Salary
        {
            set { salary = value; }
            get { return salary; }

        }
        public decimal Wedge
        {
            set { wedge = value; }
            get { return wedge; }

        }
        public decimal Housing
        {
            set { housing = value; }
            get { return housing; }

        }
        public decimal PositAllawance
        {
            set { positAllawance = value; }
            get { return positAllawance; }

        }
        public decimal SkillAllawance
        {
            set { skillAllawance = value; }
            get { return skillAllawance; }
        }
        public decimal Insuran
        {
            set { insuran = value; }
            get { return insuran; }

        }
        public string InsuranNo
        {
            set { insuranno = value; }
            get { return insuranno; }

        }
        public decimal Donate
        {
            set { donate = value; }
            get { return donate; }

        }
        public decimal Interest
        {
            set { interest = value; }
            get { return interest; }

        }
        public decimal Handycap
        {
            get { return handycap; }
            set { handycap = value; }
        }

        public decimal Loan
        {
            set { loan = value; }
            get { return loan; }

        }
        public string LoanNo
        {
            set { loanno = value; }
            get { return loanno; }

        }
        public decimal Houseded
        {
            set { houseded = value; }
            get { return houseded; }
        }
        public ProvidenceInfo Provence
        {
            set { pv = value; }
            get { return pv; }
        }

        public decimal CoopLoan
        {
            get { return coopLoan; }
            set { coopLoan = value; }
        }
        public decimal GsbLoan
        {
            get { return gsbLoan; }
            set { gsbLoan = value; }
        }
        public decimal Ltf
        {
            get { return ltf; }
            set { ltf = value; }
        }
        public CooperativeInfo Cooperative
        {
            get { return coo; }
            set { coo = value; }
        }
        public decimal ProfesionalAllowance
        {
            get { return profesionalAllowance; }
            set { profesionalAllowance = value; }
        }
        public decimal MobileAllawance
        {
            get { return mobileAllawance; }
            set { mobileAllawance = value; }
        }

        public decimal GasolineAllowance
        {
            get { return gasolineAllowance; }
            set { gasolineAllowance = value; }
        }

        public decimal Talent
        {
            get { return talent; }
            set { talent = value; }
        }

        


     
    }
}
