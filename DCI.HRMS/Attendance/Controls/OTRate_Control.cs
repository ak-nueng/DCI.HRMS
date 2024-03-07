using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Model.Attendance;
using DCI.HRMS.Service;
using DCI.HRMS.Util;
using DCI.HRMS.Common;

namespace DCI.HRMS.Attendance.Controls
{
    public partial class OTRate_Control : UserControl
    {
        private OtRateInfo otRate = new OtRateInfo();
        private ArrayList allRate = new ArrayList();
        public OtService otsrv;
      
        public OTRate_Control()
        {
            InitializeComponent();
        }

        /*
        public OtRateInfo getInformation() {
            OtRateInfo resOTRate = new OtRateInfo();
            try
            {
                resOTRate.RateId = cmbRateId.SelectedValue.ToString();
            }
            catch
            {

            }

            try
            {
                if (txtFrom.Text.Trim() != ":")
                {
                    resOTRate.OtFrom = txtFrom.Text.Trim();
                }
                else
                {
                    resOTRate.OtFrom = "";
                    // throw new Exception("กรุณาป้อนเวลาเริ่ม") ;
                }
            }
            catch { }


            try
            {
                if (txtTo.Text.Trim() != ":")
                {
                    resOTRate.OtTo = txtTo.Text.Trim();
                }
                else
                {
                    // throw new Exception("กรุณาป้อนเวลาจบ");
                    resOTRate.OtTo = "";
                }
            }
            catch { }

            try
            {
                if (txt1.Text.Trim() != ":")
                {
                    resOTRate.Rate1 = txt1.Text.Trim();
                }
                else
                {
                    resOTRate.Rate1 = string.Empty;
                }
            }
            catch { }

            try
            {
                if (txt15.Text.Trim() != ":")
                {
                    resOTRate.Rate15 = txt15.Text.Trim();
                }
                else
                {
                    resOTRate.Rate15 = string.Empty;
                }
            }
            catch { }

            try
            {
                if (txt2.Text.Trim() != ":")
                {
                    resOTRate.Rate2 = txt2.Text.Trim();
                }
                else
                {
                    resOTRate.Rate2 = string.Empty;
                }
            }
            catch { }

            try
            {
                if (txt3.Text.Trim() != ":")
                {
                    resOTRate.Rate3 = txt3.Text.Trim();
                }
                else
                {
                    resOTRate.Rate3 = string.Empty;
                }
            }
            catch { }


            try
            {
                if (txtRate1From.Text.Trim() != ":")
                {
                    resOTRate.Rate1From = txtRate1From.Text.Trim();
                }
                else
                {
                    resOTRate.Rate1From = string.Empty;
                }
            }
            catch { }


            try
            {
                if (txtRate15From.Text.Trim() != ":")
                {
                    resOTRate.Rate15From = txtRate15From.Text.Trim();
                }
                else
                {
                    resOTRate.Rate15From = string.Empty;
                }
            }
            catch { }

            try
            {
                if (txtRate2From.Text.Trim() != ":")
                {
                    resOTRate.Rate2From = txtRate2From.Text.Trim();
                }
                else
                {
                    resOTRate.Rate2From = string.Empty;
                }
            }
            catch { }

            try
            {
                if (txtRate3From.Text.Trim() != ":")
                {
                    resOTRate.Rate3From = txtRate3From.Text.Trim();
                }
                else
                {
                    resOTRate.Rate3From = string.Empty;
                }
            }
            catch { }


            try
            {
                if (txtRate1To.Text.Trim() != ":")
                {
                    resOTRate.Rate1To = txtRate1To.Text.Trim();
                }
                else
                {
                    resOTRate.Rate1To = string.Empty;
                }
            }
            catch { }

            try
            {
                if (txtRate15To.Text.Trim() != ":")
                {
                    resOTRate.Rate15To = txtRate15To.Text.Trim();
                }
                else
                {
                    resOTRate.Rate15To = string.Empty;
                }
            }
            catch { }

            try
            {
                if (txtRate2To.Text.Trim() != ":")
                {
                    resOTRate.Rate2To = txtRate2To.Text.Trim();
                }
                else
                {
                    resOTRate.Rate2To = string.Empty;
                }
            }
            catch { }


            try
            {
                if (txtRate3To.Text.Trim() != ":") { resOTRate.Rate3To = txtRate3To.Text.Trim(); }
                else
                {
                    resOTRate.Rate3To = string.Empty;
                }
            }
            catch { }


            try
            {
                resOTRate.Shift = txtShift.Text.Trim();
            }
            catch
            {
                //resOTRate.Shift = "";
            }
            try
            {
                resOTRate.WorkType = txtWtype.Text.Trim();
            }
            catch
            {

                //resOTRate.WorkType = string.Empty;
            }


            return resOTRate;
        }
        */


        public object Information
        
        {

            get
            {
                try
                {
                    otRate.RateId = cmbRateId.SelectedValue.ToString();
                }
                catch
                {

                }

                try {
                    if (txtFrom.Text.Trim() != ":")
                    {
                        otRate.OtFrom = txtFrom.Text.Trim();
                    }
                    else
                    {
                        otRate.OtFrom = "";
                        // throw new Exception("กรุณาป้อนเวลาเริ่ม") ;
                    }
                }
                catch { }


                try
                {
                    if (txtTo.Text.Trim() != ":")
                    {
                        otRate.OtTo = txtTo.Text.Trim();
                    }
                    else
                    {
                        // throw new Exception("กรุณาป้อนเวลาจบ");
                        otRate.OtTo = "";
                    }
                }
                catch { }

                try
                {
                    if (txt1.Text.Trim() != ":")
                    {
                        otRate.Rate1 = txt1.Text.Trim();
                    }
                    else
                    {
                        otRate.Rate1 = string.Empty;
                    }
                }
                catch { }

                try
                {
                    if (txt15.Text.Trim() != ":")
                    {
                        otRate.Rate15 = txt15.Text.Trim();
                    }
                    else
                    {
                        otRate.Rate15 = string.Empty;
                    }
                }
                catch { }

                try
                {
                    if (txt2.Text.Trim() != ":")
                    {
                        otRate.Rate2 = txt2.Text.Trim();
                    }
                    else
                    {
                        otRate.Rate2 = string.Empty;
                    }
                }
                catch { }

                try
                {
                    if (txt3.Text.Trim() != ":")
                    {
                        otRate.Rate3 = txt3.Text.Trim();
                    }
                    else
                    {
                        otRate.Rate3 = string.Empty;
                    }
                }
                catch { }


                try
                {
                    if (txtRate1From.Text.Trim() != ":")
                    {
                        otRate.Rate1From = txtRate1From.Text.Trim();
                    }
                    else
                    {
                        otRate.Rate1From = string.Empty;
                    }
                }
                catch { }


                try
                {
                    if (txtRate15From.Text.Trim() != ":")
                    {
                        otRate.Rate15From = txtRate15From.Text.Trim();
                    }
                    else
                    {
                        otRate.Rate15From = string.Empty;
                    }
                }
                catch { }

                try
                {
                    if (txtRate2From.Text.Trim() != ":")
                    {
                        otRate.Rate2From = txtRate2From.Text.Trim();
                    }
                    else
                    {
                        otRate.Rate2From = string.Empty;
                    }
                }
                catch { }

                try
                {
                    if (txtRate3From.Text.Trim() != ":")
                    {
                        otRate.Rate3From = txtRate3From.Text.Trim();
                    }
                    else
                    {
                        otRate.Rate3From = string.Empty;
                    }
                }
                catch { }


                try
                {
                    if (txtRate1To.Text.Trim() != ":")
                    {
                        otRate.Rate1To = txtRate1To.Text.Trim();
                    }
                    else
                    {
                        otRate.Rate1To = string.Empty;
                    }
                }
                catch { }

                try
                {
                    if (txtRate15To.Text.Trim() != ":")
                    {
                        otRate.Rate15To = txtRate15To.Text.Trim();
                    }
                    else
                    {
                        otRate.Rate15To = string.Empty;
                    }
                }
                catch { }

                try
                {
                    if (txtRate2To.Text.Trim() != ":")
                    {
                        otRate.Rate2To = txtRate2To.Text.Trim();
                    }
                    else
                    {
                        otRate.Rate2To = string.Empty;
                    }
                }
                catch { }


                try
                {
                    if (txtRate3To.Text.Trim() != ":") { otRate.Rate3To = txtRate3To.Text.Trim(); }
                    else
                    {
                        otRate.Rate3To = string.Empty;
                    }
                }
                catch { }


                try
                {
                    otRate.Shift = txtShift.Text.Trim();
                }
                catch
                {
                    //otRate.Shift = "";
                }
                try
                {
                    otRate.WorkType = txtWtype.Text.Trim();
                }
                catch 
                {

                    //otRate.WorkType = string.Empty;
                }



                return otRate;
            }
            set
            {
                try
                {
                    otRate = (OtRateInfo)value;


                    try
                    {
                        txtFrom.Text = otRate.OtFrom;
                    }
                    catch{}
                    try
                    {
                        txtTo.Text = otRate.OtTo;
                    }
                    catch{}
                    try
                    {
                        txt1.Text = otRate.Rate1;
                    }
                    catch
                    { 
                        txt1.Text = ""; 
                    }
                    try
                    {
                        txt15.Text = otRate.Rate15;
                    }
                    catch
                    { 
                        txt15.Text = ""; 
                    }
                    try
                    {
                        txt2.Text = otRate.Rate2;
                    }
                    catch
                    { 
                        txt2.Text = ""; 
                    }
                    try
                    {
                        txt3.Text = otRate.Rate3;
                    }
                    catch
                    { 
                        txt3.Text = ""; 
                    }

                    try
                    {
                        txtRate1From.Text = otRate.Rate1From;
                    }
                    catch
                    {
                        txtRate1From.Text = "";
                    }
                    try
                    {
                        txtRate15From.Text = otRate.Rate15From;
                    }
                    catch
                    {
                        txtRate15From.Text = "";
                    }
                    try
                    {
                        txtRate2From.Text = otRate.Rate2From;
                    }
                    catch
                    {
                        txtRate2From.Text = "";
                    }
                    try
                    {
                        txtRate3From.Text = otRate.Rate3From;
                    }
                    catch
                    {
                        txtRate3From.Text = "";
                    }
                    try
                    {
                        txtRate1To.Text = otRate.Rate1To;
                    }
                    catch
                    {
                        txtRate1To.Text = "";
                    }
                    try
                    {
                        txtRate15To.Text = otRate.Rate15To;
                    }
                    catch
                    {
                        txtRate15To.Text = "";
                    }
                    try
                    {
                        txtRate2To.Text = otRate.Rate2To;
                    }
                    catch
                    {
                        txtRate2To.Text = "";
                    }
                    try
                    {
                        txtRate3To.Text = otRate.Rate3To;
                    }
                    catch
                    {
                        txtRate3To.Text = "";
                    }


                    try
                    {
                        txtShift.Text = otRate.Shift;
                    }
                    catch
                    { 
                        txtShift.Text = "";
                    }
                    try
                    {
                        txtWtype.Text = otRate.WorkType;
                    }
                    catch
                    {
                        txtWtype.Text = "";
                    }
                }
                catch
                {
                }

            }
        }
        public bool CanEdit
        {
            set
            {
                txtWtype.ReadOnly=!value;
                txtShift.ReadOnly = !value;
            }
            get
            {
                return !txtWtype.ReadOnly;
            }
        }

        public void SetRate(string wtype)
        {
            if (cmbRateId.SelectedValue.ToString()!="Manual")
            {
                this.Information= otsrv.GetRate(cmbRateId.SelectedValue.ToString(), wtype);
            }
           

        }
        private void cmbRateId_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Information =(OtRateInfo) allRate[cmbRateId.SelectedIndex];
            if (cmbRateId.SelectedIndex==0)
            {
               // txtFrom.Focus(); 
            }
           

        }

        private void RateOT_Control_Load(object sender, EventArgs e)
        {


        }
        public void SetAllRate(ArrayList _allrate)
        {
            allRate = _allrate;
            OtRateInfo manua = new OtRateInfo();
            manua.RateId = "Manual";
            allRate.Insert(0, manua);
            cmbRateId.DisplayMember = "DisplayText";
            cmbRateId.ValueMember = "RateId";   
            cmbRateId.DataSource = allRate;
            cmbRateId.SelectedIndex = 0;
        }

        private void txtFrom_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPressManager.Enter(e);
        }

        private void txtFrom_Enter(object sender, EventArgs e)
        {
            MaskedTextBox tx = (MaskedTextBox)sender;
            tx.SelectAll();
        }

        private void kryptonHeader1_Paint(object sender, PaintEventArgs e)
        {

        }
        public void SetFocusTxtFrom()
        {
            txtFrom.Focus();
        }
        public void SetFocusTxtTo()
        {
            txtTo.Focus();
        }

  

        private void txtFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressManager.EnterNumericOnly(e);
        }

        private void txtFrom_Leave(object sender, EventArgs e)
        {
            KeyPressManager.ConvertTextTime(sender);

        }

        

        

        

    }
}
