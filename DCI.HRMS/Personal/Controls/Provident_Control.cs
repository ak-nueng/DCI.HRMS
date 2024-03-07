using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Model.Personal;

namespace DCI.HRMS.Personal.Controls
{
    public partial class Provident_Control : UserControl
    {
        public Provident_Control()
        {
            InitializeComponent();
        }
        ProvidenceInfo information = new ProvidenceInfo();
        public void Clear()
        {
            dtpJoin.Value = new DateTime(1900,1,1);
            txtAccNo.Text = "";
            txtPercent.Text = "";
            txtPercentCompany.Text = "";
            dtpResign.Value = new DateTime(1900,1,1);
        }
        public object Information
        {
            set
            {
                try
                {
                    information = (ProvidenceInfo)value;
                    dtpJoin.Value = information.ProvidenceDate;
                    txtAccNo.Text = information.ProvidenceNo;
                    txtPercent.Text = information.ProvidencePercent == 0 ? "" : information.ProvidencePercent.ToString();
                    txtPercentCompany.Text = information.ProvidencePercentCompany == 0 ? "" : information.ProvidencePercentCompany.ToString();
                    dtpResign.Value = information.ProvidenceTerminate;
                }
                catch
                {

                    Clear();
                }

            }
            get
            {

                information.ProvidenceDate = dtpJoin.Value;
                information.ProvidenceNo = txtAccNo.Text;
                try
                {
                    information.ProvidencePercent = decimal.Parse(txtPercent.Text);
                }
                catch
                {
                    information.ProvidencePercent = 0;
                }

                try
                {
                    information.ProvidencePercentCompany = decimal.Parse(txtPercentCompany.Text);
                }
                catch
                {
                    information.ProvidencePercentCompany = 0;
                }

                information.ProvidenceTerminate = dtpResign.Value;
                return information;
            }
        }
    }
}
