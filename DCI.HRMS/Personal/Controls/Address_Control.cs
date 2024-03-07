using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Model;
using DCI.HRMS.Util;

namespace DCI.HRMS.Personal.Controls
{
    public partial class Address_Control : UserControl
    {
        public Address_Control()
        {
            InitializeComponent();
        }
        AddressInfo information = new AddressInfo();
        public object Information
        {
            set
            {
                try
                {
                    information = (AddressInfo)value;
                    txtAddress.Text = information.Address;
                    txtSubDistrict.Text = information.Subdistrict;
                    txtDistrict.Text = information.District;
                    txtTelephone.Text = information.Telephone;
                    char[] spch = new char[1];
                    spch[0] = ' ';

                    string[] pv = information.Province.Split(spch, StringSplitOptions.RemoveEmptyEntries);
                    if (pv.Length >= 1)
                    {


                        txtProvince.Text = pv[0];
                        if (pv.Length > 1)
                        {
                            txtPostCode.Text = pv[1];
                        }
                        else
                        {
                            txtPostCode.Clear();
                        }
                    }


                }
                catch
                {
                    Clear();

                }
            }
            get
            {

                information.Address = txtAddress.Text;
                information.Subdistrict = txtSubDistrict.Text;
                information.District = txtDistrict.Text;
                information.Province = txtProvince.Text +" " + txtPostCode.Text; 
                information.Telephone = txtTelephone.Text;
                return information;
            }
        }
        public void Clear()
        {

            txtAddress.Text = "";
            txtSubDistrict.Text = "";
            txtDistrict.Text = "";
            txtProvince.Text = "";
            txtTelephone.Text = "";
            txtPostCode.Text = "";
        }
        public string HeaderText
        {
            set { kryptonHeader1.Text = value; }
            get { return kryptonHeader1.Text; }
        }
        public string DescriptionText
        {
            set { kryptonHeader1.Values.Description = value; }
            get { return kryptonHeader1.Values.Description; }

        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPressManager.Enter(e);
        }
    }
}
