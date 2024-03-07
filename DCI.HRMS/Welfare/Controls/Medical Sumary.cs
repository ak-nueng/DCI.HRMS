using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Service;
using System.Collections;
using DCI.HRMS.Model.Welfare;
using DCI.HRMS.Model.Common;
using DCI.HRMS.Model;

namespace DCI.HRMS.Welfare.Controls
{
    public partial class Medical_Sumary : UserControl
    {

        public MedicalAllowanceService medSvr;
        private MedicalAllowanceInfo med;
        private double opd;
        private double opdPrr;
        private double opdPregnant;
        private double ipd;
        private double total;
        private string code;
        private DateTime year;
        private double[] inform = new double[5];
        public Medical_Sumary()
        {
            InitializeComponent();
        }
        public void OpenFileDialog()
        {
            med = new MedicalAllowanceInfo();
            BasicInfo temp = medSvr.GetMedicalAmount("OPD"); // get OPD father, mother and Pregnant midwife
            opd = double.Parse(temp.Description);
            opdPrr = double.Parse(temp.DescriptionTh);
            opdPregnant = double.Parse(temp.DescriptionTh);
            temp = medSvr.GetMedicalAmount("IPD");
            ipd = double.Parse(temp.Description);
            temp = medSvr.GetMedicalAmount("TOTL");
            total = double.Parse(temp.Description);
            lblIPD.Text = "ผู้ป่วยใน     " + ipd.ToString() + " บาท";
            lblOPD.Text = "ผู้ป่วยนอกรวม  " + opd.ToString() + " บาท";
            lblOpdPrr.Text = "พ่อ-แม่  " + opdPrr.ToString() + " บาท";
            lbltotal.Text = "วงเงินรวม  " + total.ToString() + " บาท";
            lblOpdPregnant.Text = "ผดุงครรภ์ " + opdPregnant.ToString() + " บาท";

        }
        public void SetInfo(string _code, DateTime _year)
        {
            code = _code;
            year = _year;

            //---- Select Old Data lessthan 2015 ------
            if (_year <= new DateTime(2015, 12, 31))
            {
                opd = 10000;
                opdPrr = 5000;
                opdPregnant = 0;
                ipd = 18000;
                total = 26000;

                lblIPD.Text = "ผู้ป่วยใน     " + ipd.ToString() + " บาท";
                lblOPD.Text = "ผู้ป่วยนอกรวม  " + opd.ToString() + " บาท";
                lblOpdPrr.Text = "พ่อ-แม่  " + opdPrr.ToString() + " บาท";
                lbltotal.Text = "วงเงินรวม  " + total.ToString() + " บาท";
                lblOpdPregnant.Text = "ผดุงครรภ์ " + opdPregnant.ToString() + " บาท";
            }
            else {
                OpenFileDialog();
            
            }
            //---- Select Old Data lessthan 2015 ------

            ArrayList item = medSvr.GetMedical(code, year);
            double opde = 0;
            double opdPrre = 0;
            double opdPregnante = 0;
            double ipde = 0;
            if (item != null)
            {
                foreach (MedicalAllowanceInfo var in item)
                {
                    if (var.PatienType == "I")
                    {
                        ipde += var.Amount;
                    }
                    else
                    {

                        //------ Medical Father & Mother ------
                        if (var.RelationType == "RELA1" || var.RelationType == "RELA2")
                        {
                            opdPrre += var.Amount;
                        
                        //------- Medical Pregnant  midwife  --------
                        }else if(var.RelationType == "RELA8"){
                            opdPregnante += var.Amount;
                        }


                        opde += var.Amount;

                    }
                }
            }
            txtIpdUse.Text = ipde.ToString();
            txtOpdUse.Text = opde.ToString();
            txtOpdPrrUse.Text = opdPrre.ToString(); 
            txtTotalUse.Text = (ipde + opde ).ToString();
            txtIpdRem.Text = (ipd - ipde).ToString();
            txtOpdRem.Text = (opd - opde).ToString();
            txtOpdPrrRem.Text = (opdPrr - opdPrre).ToString();
            txtTotalRem.Text = (total - ipde - opde).ToString();


            //---- Select Old Data lessthan 2015 ------
            if (_year <= new DateTime(2015, 12, 31))
            {
                txtOpdPregnant.Text = "";   //------- Medical Pregnant  midwife  --------
                txtOpdPregnantRem.Text = "";  //------- Medical Pregnant  midwife  --------
            }else {
                txtOpdPregnant.Text = opdPregnante.ToString();   //------- Medical Pregnant  midwife  --------
                txtOpdPregnantRem.Text = (opdPregnant - opdPregnante).ToString();  //------- Medical Pregnant  midwife  --------
            }
            //---- Select Old Data lessthan 2015 ------
        
        }
        public object Information
        {
            get
            {
                inform[0] = 0;
                inform[1] = 0;

                try{
                    inform[0] = double.Parse(txtIpdRem.Text);
                }catch{}

                try {
                    inform[1] = double.Parse(txtOpdRem.Text);
                }catch { }
                
                try{
                    inform[2] = double.Parse(txtOpdPrrRem.Text);
                }catch{}

                try{
                    inform[3] = double.Parse(txtOpdPregnantRem.Text);
                }catch{}

                try {
                    inform[4] = double.Parse(txtTotalRem.Text);
                }catch { }

                return inform;

            }
            set
            {


            }
        }

        



    }
}
