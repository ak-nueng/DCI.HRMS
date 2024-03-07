using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Model.Personal;
using DCI.HRMS.Service;
using System.Collections;
using DCI.HRMS.Model;

namespace DCI.HRMS.Personal.Controls
{
    public partial class EmpEducation_Control : UserControl
    {
        EmployeeService empsvr;
        public EmpEducation_Control()
        {
            InitializeComponent();
        }
        private EducationInfo information = new EducationInfo();
        public void Clear()
        {

            cmbDegreeType.SelectedIndex = -1;
            txtDegree.Text = "";
            txtEnMajor.Text = "";
            txtThMajor.Text = "";
            txtEnInst.Text = "";
            txtThInst.Text = "";
            txtYear.Text = "";

        }
        public object Information
        {
            set
            {
                try
                {
                    information = (EducationInfo)value;
                    cmbDegreeType.SelectedValue = information.DegreeType;
                    txtDegree.Text = information.Degree;
                    txtEnMajor.Text = information.MajorInEng;
                    txtThMajor.Text = information.MajorInThai;
                    txtEnInst.Text = information.SchoolInEng;
                    txtThInst.Text = information.SchoolInThai;
                    txtYear.Text = information.GraduateYear;
                }
                catch 
                {
                    Clear();
                  
                }

             
            }
            get
            {

                try
                {
                    information.DegreeType = cmbDegreeType.SelectedValue.ToString();
                }
                catch 
                {  }
                information.Degree = txtDegree.Text;
                information.MajorInEng = txtEnMajor.Text;
                information.MajorInThai = txtThMajor.Text;
                information.SchoolInEng = txtEnInst.Text;
                information.SchoolInThai = txtThInst.Text;
                information.GraduateYear = txtYear.Text;
                return information;
            }

        }
        public void Open(EmployeeService emp )
        {
            empsvr = emp;
            ArrayList dgr = empsvr.GetAllDegree();
            cmbDegreeType.DisplayMember = "NameForSearching";
            cmbDegreeType.ValueMember = "Code";
            cmbDegreeType.DataSource = dgr;
            cmbDegreeType.SelectedIndex = -1;
        }

        private void cmbDegreeType_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                BasicInfo item = (BasicInfo)cmbDegreeType.SelectedItem;
                txtDegree.Text = item.Name;
            }
            catch      
            {
                txtDegree.Clear();
               
            }

        }
        
    }
}
