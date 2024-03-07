using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Model.Personal;

namespace DCI.HRMS.Personal.Controls
{
    public partial class Cooperative_Control : UserControl
    {
        public Cooperative_Control()
        {
            InitializeComponent();
        }

        CooperativeInfo information = new CooperativeInfo();
        public void Clear()
        {
            dtpJoin.Value = new DateTime(1900, 1, 1);
            txtAmount.Text = "";
            txtDeduct.Text = "";
            dtpResign.Value = new DateTime(1900, 1, 1);
        }
        public object Information
        {
            set
            {
                try
                {
                    information = (CooperativeInfo)value;
                    dtpJoin.Value = information.CooDate.Date;
                    txtAmount.Text = information.Amount == 0 ? "" : information.Amount.ToString();
                    txtDeduct.Text = information.Deduct == 0 ? "" : information.Deduct.ToString();
                    dtpResign.Value = information.CooTerm;
                }
                catch
                {

                    Clear();
                }

            }
            get
            {
                information = new CooperativeInfo();
                information.CooDate = dtpJoin.Value;
                information.CooTerm = dtpResign.Value;
                try
                {
                    information.Deduct = decimal.Parse(txtDeduct.Text);
                }
                catch
                {
                    information.Deduct = 0;
                }
                try
                {
                    information.Amount = decimal.Parse(txtAmount.Text);
                }
                catch
                {

                    information.Amount = 0;
                }

                return information;
            }
        }

    }
}
