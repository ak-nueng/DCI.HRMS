using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Service;
using DCI.HRMS.Model.Attendance;
using System.Collections;
using DCI.HRMS.Model.Personal;

namespace DCI.HRMS.Attendance.Controls
{
    public partial class AnnualLeave_Control : UserControl
    {
        private ArrayList anulv = new ArrayList();
        private DateTime joinDt;
        private ArrayList annutotal = new ArrayList();
        private string emcode;
        private EmployeeInfo empInfo;
        public EmployeeLeaveService empLvSvr;
        private double remainTotal = 0;




        public AnnualLeave_Control()
        {
            InitializeComponent();
            kryptonDataGridView1.AutoGenerateColumns = false;
        }
        public object Information
        {
            get
            {
               return remainTotal;
            }
        }
        public void Open()
        {
       


        }
        public void CalTotalAnnual(string _empcode, DateTime _joidate, DateTime _caldate )
        {
            Clear();
            emcode = _empcode;  
         
            joinDt = _joidate;
            txtCode.Text = emcode;
            txtJoin.Text = _joidate.ToShortDateString();
    
            annutotal = empLvSvr.GetAnnualTotal(_empcode,_caldate,true);
            AnnualTotal AnTotal = new AnnualTotal();
            try
            {
                AnTotal = (AnnualTotal)annutotal[annutotal.Count - 1];
            }
            catch 
            { }
            remainTotal = AnTotal.Remain;
            if (remainTotal < 0)
            {
                txtRemain.BackColor = Color.Red;
            }
            else
            {
                txtRemain.BackColor = Color.White;
            }
            txtRemain.Text = AnTotal.RemainHr;
            kryptonDataGridView1.DataSource = null;
            annutotal.Sort(new AnnualTotalDesc());
            kryptonDataGridView1.DataSource = annutotal;
        }





        private void CalAnnual()
        {

            annutotal = new ArrayList();
            DataTable lvTb = new DataTable();
            if (anulv != null)
            {
                lvTb = ServiceUtility.ToDataTable(anulv);
            }
            int calyear = joinDt.Year;
            if (joinDt >= DateTime.Parse("01/07/" + calyear.ToString()))
            {
                calyear++;
            }
            txtRemain.Clear();
            remainTotal = 0;


            #region comment
            /*
            if (emcode.StartsWith("6"))
            {
                if (calyear < 2008)
                {
                    calyear = 2008;
                }


                for (; calyear <= DateTime.Today.Year; calyear++)
                {
                    DateTime startAnnu = DateTime.Parse("01/07/" + calyear.ToString());
                    DateTime endAnnu = DateTime.Parse("30/06/" + (calyear + 1).ToString());
                    TimeSpan calintv = startAnnu - joinDt;
                    if (startAnnu <= DateTime.Today)
                    {
                        AnnualTotal AnTotal = new AnnualTotal();
                        AnTotal.Year = calyear;
                        if (calintv.Days >= 365)
                        {

                            AnTotal.Get = 6;
                            AnnualTotal temp = new AnnualTotal();
                            try
                            {
                                temp = (AnnualTotal)annutotal[annutotal.Count - 1];
                            }
                            catch { }
                            AnTotal.Total = AnTotal.Get * 525 + temp.Remain;
                            //if (lvTb.Rows.Count != 0)
                            //{
                            //    string filter = "LvType='ANNU' and   LvDate >= '" + startAnnu.Date.ToShortDateString() + "' and LvDate <='" + endAnnu.Date.ToShortDateString() + "'";

                            //    DataRow[] yearannu = lvTb.Select(filter);
                            //    foreach (DataRow var in yearannu)
                            //    {

                            //        AnTotal.Use += (double.Parse(var["TotalMinute"].ToString()));
                            //    }
                            //}
                            //AnTotal.Remain = AnTotal.Total - AnTotal.Use;
                            //remainTotal = AnTotal.Remain;
                            //txtRemain.Text = AnTotal.RemainHr;
                            //annutotal.Add(AnTotal);
                        }
                        else {
                            double antt = calintv.Days / 365.25 * 6;
                            int anday = (int)Math.Round(antt, 0);
                            AnTotal.Get = anday;
                            AnTotal.Total = AnTotal.Get * 525;
                        }


                        //***************************
                        if (lvTb.Rows.Count != 0)
                        {
                            string filter = "LvType='ANNU' and   LvDate >= '" + startAnnu.Date.ToShortDateString() + "' and LvDate <='" + endAnnu.Date.ToShortDateString() + "'";

                            DataRow[] yearannu = lvTb.Select(filter);
                            foreach (DataRow var in yearannu)
                            {

                                AnTotal.Use += (double.Parse(var["TotalMinute"].ToString()));
                            }
                        }
                        AnTotal.Remain = AnTotal.Total - AnTotal.Use;
                        remainTotal = AnTotal.Remain;
                        txtRemain.Text = AnTotal.RemainHr;
                        annutotal.Add(AnTotal);

                    }

                }

            }

            else
            {
             */

            #endregion


            for (; calyear <= DateTime.Today.Year; calyear++)
                {

                    DateTime startAnnu = DateTime.Parse("01/07/" + calyear.ToString());
                    DateTime endAnnu = DateTime.Parse("30/06/" + (calyear + 1).ToString());
                    TimeSpan calintv = startAnnu - joinDt;
                    if (startAnnu <= DateTime.Today)
                    {
                        AnnualTotal AnTotal = new AnnualTotal();
                        AnTotal.Year = calyear;
                        if (calintv.Days < 365)
                        {
                            double antt = calintv.Days / 365.25 * 6;
                            int anday = (int)Math.Round(antt, 0);
                            AnTotal.Get = anday;
                            AnTotal.Total = AnTotal.Get * 525;
                        }
                        else
                        {
                            double antt = calintv.Days / 365.25 + 5;
                            int anday = (int)antt;
                            AnTotal.Get = anday;



                            //********************************************************
                            //  Edit by AKONE on 2022-01-28
                            //  In case Sub-Contract change to DCI  
                            //   year in sub-contract = 6 after to DCI is calculate year annual
                            //********************************************************
                            if (joinDt.Year >= calyear)
                            {
                                AnTotal.Get = 6;
                            }
                            //********************************************************
                            //  Edit by AKONE on 2019-11-19
                            //  Add condition temp get annual per 6 day every year
                            //********************************************************
                            if (emcode.StartsWith("6")){
                                AnTotal.Get = 6;
                            }
                            //********************************************************
                            //  End Edit by AKONE on 2019-11-19
                            //  Add condition temp get annual per 6 day every year
                            //********************************************************

                            


                            AnnualTotal temp = (AnnualTotal)annutotal[annutotal.Count - 1];
                            AnTotal.Total = AnTotal.Get * 525 + temp.Remain;
                            if (calyear < 2008 && AnTotal.Total > 12 * 525)
                            {
                                AnTotal.Total = 12 * 525;
                            }
                        }

                        string filter = "LvType='ANNU' and LvDate >= '" + startAnnu.Date.ToShortDateString() + "' and LvDate <='" + endAnnu.Date.ToShortDateString() + "'";

                        DataRow[] yearannu = lvTb.Select(filter);
                        foreach (DataRow var in yearannu)
                        {

                            AnTotal.Use += (double.Parse(var["TotalMinute"].ToString()));
                        }
                        AnTotal.Remain = AnTotal.Total - AnTotal.Use;
                        remainTotal = AnTotal.Remain;
                        txtRemain.Text = AnTotal.RemainHr;
                        annutotal.Add(AnTotal);

                    }


                }
            
            
            //} // end if employee temp

            kryptonDataGridView1.DataSource = null;
            annutotal.Sort(new AnnualTotalDesc());
            kryptonDataGridView1.DataSource = annutotal;


            //  DateTime enddate = DateTime.Parse("30/06/" + (calyear+1).ToString());


        }
        public void Clear()
        {
            txtCode.Clear();
            txtJoin.Clear();
            txtRemain.Clear();
            annutotal = new ArrayList();
            kryptonDataGridView1.DataSource = null;
            kryptonDataGridView1.DataSource = annutotal;
        }

    }

   
}
