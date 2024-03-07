using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DCI.HRMS.ATT.Controls
{
    public partial class DayShift_Control : UserControl
    {
        public DayShift_Control()
        {
            InitializeComponent();
        }
        public DayShift_Control(string _date,string _sh)
        {

            InitializeComponent();
            this.Date = _date;
            this.Shift = _sh;

        }
        /// <summary>
        /// Set Date Label
        /// </summary>
        public string Date
        {
            get { return lblDate.Text;}
            set { lblDate.Text = value; }
        }
        /// <summary>
        /// <value>Set day Label   </value>
        /// </summary>
        public string Day
        {
            get { return lblDay.Text; }
            set { lblDay.Text = value; }
        }
        /// <summary>
        /// Set Shift
        /// </summary>
        public string Shift
        {
            get { return txtShift.Text; }
            set { txtShift.Text = value; }
        }

        private void txtShift_KeyDown(object sender, KeyEventArgs e)
        {

                        if (e.KeyCode == Keys.Enter || e.KeyValue== 39)
            {
                SendKeys.Send("{TAB}");
            }
            else if (e.KeyValue== 37)
            {

                SendKeys.Send("+{TAB}");
            }

        }

        private void txtShift_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'D' || e.KeyChar == 'N' || e.KeyChar == 'H' || e.KeyChar == 'T')
            {
                //e.Handled = false;
                e.Handled = true;
                txtShift.Text = e.KeyChar.ToString();
                SendKeys.Send("{TAB}");
            }
            else if (e.KeyChar == '\r')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtShift_Enter(object sender, EventArgs e)
        {
            txtShift.SelectAll();
        }

        private void txtShift_TextChanged(object sender, EventArgs e)
        {
            if (txtShift.Text == "T" || txtShift.Text == "H")
            {
                txtShift.BackColor = Color.Red;
            }
            else
            {
                txtShift.BackColor = Color.White;


            }
    
        }
    }
}
