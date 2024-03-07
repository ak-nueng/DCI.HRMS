using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Service;
using DCI.HRMS.Model.Welfare;
using System.Collections;
using DCI.HRMS.Util;

namespace DCI.HRMS.Welfare.Controls
{
    public partial class Medical_Control : UserControl
    {
        private ArrayList medList = new ArrayList();
        private MedicalAllowanceInfo information;
        private bool readyOnly = false;
        public MedicalAllowanceService medSvr;

        public delegate void txtCode_EnterHandler(string em_code,DateTime rq_Date );

        [Category("Action")]
        [Description("Fires when the MonthComboBox change.")]
        public event txtCode_EnterHandler txtCode_Enter;
        protected virtual void OnTxtCode_Enter(string em_code ,DateTime rq_date)
        {
            if (txtCode_Enter != null)
            {
                txtCode_Enter(em_code,rq_date);

            }

        }
      
        public Medical_Control()
        {
            InitializeComponent();
        }
        public object Information
        {
            get
            {
                try
                {
                    information.EmCode = txtCode.Text;
                }
                catch { }
                try
                {
                    information.DocNo = txtDocId.Text;
                }
                catch
                { }
                try
                {
                    information.Hospital = txtHospital.Text;
                }
                catch
                { }
                try
                {
                    information.District = txtDistrict.Text;
                }
                catch
                { }
                try
                {
                    information.Province = txtProvince.Text; ;
                }
                catch
                { }
                try
                {
                    information.PatienName = txtPatiener.Text;
                }
                catch
                { }
                try
                {
                    information.RelationType = TxtRelation.Text;
                }
                catch
                { }
                try
                {
                    information.TrDate = DptTrDate.Value;
                }
                catch
                { }
                try
                {
                    information.RqDate = dptRqDate.Value;
                }
                catch
                { }
                try
                {
                    information.Symptom = txtSymptom.Text;
                }
                catch
                { }
                try
                {
                    information.Amount = int.Parse(txtAmount.Text);
                }
                catch
                { }
                try
                {
                    information.PatienType = kryptonRadioButton1.Checked ? "I" : "O";
                }
                catch 
                {  }
                return information;
            }
            set
            {
                try
                {
                    try
                    {
                        information = (MedicalAllowanceInfo)value;
                    }
                    catch 
                    { }

                    try
                    {
                        txtCode.Text = information.EmCode;
                    }
                    catch 
                    {  }
                    try
                    {
                        txtDocId.Text = information.DocNo;
                    }
                    catch 
                    { }
                    try
                    {
                        txtHospital.Text = information.Hospital;
                    }
                    catch 
                    {  }

                    try
                    {
                        txtProvince.Text = information.Province;
                    }
                    catch
                    { }
                    try
                    {
                        txtDistrict.Text = information.District;
                    }
                    catch
                    { }
                    try
                    {
                        txtPatiener.Text = information.PatienName;
                    }
                    catch 
                    { }
                    try
                    {
                        TxtRelation.Text = information.RelationType;
                    }
                    catch
                    { }
                 
                    try
                    {
                        DptTrDate.Value = information.TrDate;
                    }
                    catch 
                    { }
                    try
                    {
                        dptRqDate.Value = information.RqDate;
                    }
                    catch 
                    { }
                    try
                    {
                        txtSymptom.Text = information.Symptom;
                    }
                    catch 
                    {  }
                    try
                    {
                        txtAmount.Text = information.Amount.ToString();
                    }
                    catch 
                    {}
                    if (information.PatienType == "I")
                    {
                        kryptonRadioButton1.Checked = true;
                    }
                    else
                    {
                        kryptonRadioButton2.Checked = true;
                    }
                }
                catch
                {


                }

            }
            
    
        }
        public bool ReadOnly
        {
            set
            {
                readyOnly = value;
                foreach (Control var in kryptonGroup2.Panel.Controls)
                {
                    if (var is TextBox && var != txtPatiener)
                    {
                        TextBox t = (TextBox)var;
                        t.ReadOnly = readyOnly;
                    }
                    else if (var is ComponentFactory.Krypton.Toolkit.KryptonGroup)
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonGroup g = (ComponentFactory.Krypton.Toolkit.KryptonGroup)var;
                        g.Enabled = false;// !readyOnly;
                    }
                    else if (var is DateTimePicker)
                    {
                        DateTimePicker dt = (DateTimePicker)var;
                        dt.Enabled = !readyOnly;
                    }
                }

            }
            get
            {
                return readyOnly;
            }
        }
        public string PatienName
        {
            set  {txtPatiener.Text = value;  }
        }
        public string Relation
        {
            set { TxtRelation.Text = value; }
        }

     

        private void Medical_Control_Load(object sender, EventArgs e)
        {

        }
        public void Open()
        {
            new MedicalAllowanceInfo();
            AutoCompleteStringCollection hosp = new AutoCompleteStringCollection();
            ArrayList temp = medSvr.GetAutoCompHospital();
            foreach (MedicalAllowanceInfo var in temp)
            {
                hosp.Add(var.Hospital);
            }
            txtHospital.AutoCompleteCustomSource = hosp;
            AutoCompleteStringCollection symptom = new AutoCompleteStringCollection();
            temp = medSvr.GetAutoCompSymptom();
            foreach (MedicalAllowanceInfo var in temp)
            {
                symptom.Add(var.Hospital);
            }
            txtSymptom.AutoCompleteCustomSource = symptom;

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //medList = medSvr.GetMedical(txtCode.Text, dateTimePicker1.Value.Date);
                OnTxtCode_Enter(txtCode.Text,DptTrDate.Value.Date);
 

                SendKeys.Send("{TAB}");
            }
        }

        private void txtHospital_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPressManager.Enter(e);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime rdate = DateTime.Today;
            while (DayOfWeek.Friday != rdate.DayOfWeek)
            {
                rdate = rdate.AddDays(1);
            }
            dptRqDate.Value = rdate.Date;
            if (txtCode.Text.Trim()!="")
            {
               // OnTxtCode_Enter(txtCode.Text, dateTimePicker1.Value.Date); 
            }
           
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {

        }
    }
}
