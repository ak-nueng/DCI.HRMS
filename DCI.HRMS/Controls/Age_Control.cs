using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DCI.HRMS.Controls
{
    public partial class Age_Control : UserControl
    {
        private Language lang = Language.Eng;
        public Age_Control()
        {
            InitializeComponent();
        }
        public enum Language
        {
            Thai=1,
            Eng=2
        }
        public Language Lang
        {
            set
            {
                lang = value;
            }
            get
            {
                return lang;
            }

        }
        public string LabelText
        {
            set
            {
                kryptonLabel1.Text = value;
            }
            get
            {
                return kryptonLabel1.Text;
            }
        }
        public DateTime Value
        {
            set
            {     DateTime birthDate = value;
                if (birthDate != new DateTime() && birthDate!= DateTime.Parse("01/01/1900"))
                {
               
                    TimeSpan ts = DateTime.Today - birthDate;
                    int year = 0;
                    int month = 0;
                    year = ts.Days / 365;
                    month = (ts.Days % 365) / 30;
                    if (lang == Language.Thai)
                    {
                        textBox1.Text = year.ToString() + " ปี " + month.ToString() + " เดือน";
                        kryptonLabel2.Text ="("+ ts.Days.ToString() + " วัน)";
                    }
                    else
                    {
                        textBox1.Text = year.ToString() + " Year " + month.ToString() + " Month ";
                        kryptonLabel2.Text = "(" + ts.Days.ToString() + " Days)";
                    } 
                }
                else
                {
                    textBox1.Text="";
                }
            }
        }

        private void kryptonLabel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
