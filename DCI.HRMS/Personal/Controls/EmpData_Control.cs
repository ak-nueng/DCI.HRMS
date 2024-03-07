using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Model.Personal;

namespace DCI.HRMS.PER.Controls
{
    public partial class EmpData_Control : UserControl
    {
      private  EmployeeInfo empInfo;
        public EmpData_Control()
        {
            InitializeComponent();
        }

        private void kryptonGroup2_Panel_Paint(object sender, PaintEventArgs e)
        {

        }
        public object Information
        {

            set
            {
                try
                {
                  
                   empInfo =(EmployeeInfo) value;
                    cODETextBox.Text = empInfo.Code;
                    pRENTextBox.Text = empInfo.NameInEng.Title;
                    nAMETextBox.Text = empInfo.NameInEng.Name;
                    sURNTextBox.Text = empInfo.NameInEng.Surname;
                 
                    pOSI_ENAMETextBox.Text = empInfo.Position.NameEng;
                    dV_ENAMETextBox.Text = empInfo.Division.Name;
                
                    txtGrpot.Text = empInfo.OtGroupLine;
                    txtLine.Text = empInfo.WorkGroupLine;

                    if (empInfo.WorkType.Trim() == "S")
                        textBox4.Text = "�����͹";
                    else if (empInfo.WorkType.Trim() == "O")
                        textBox4.Text = "����ѹ";
                    jOINTextBox.Text = empInfo.JoinDate.ToShortDateString();
                    txtCompany.Text = empInfo.Company;
                      if (!empInfo.Resigned)                    
                    {
                        lblResign.Text = "���اҹ:";
                        DateTime birthDate = DateTime.Parse(jOINTextBox.Text);
                        TimeSpan ts = DateTime.Today - birthDate;
                        int year = 0;
                        int month = 0;
                        year = ts.Days / 365;
                        month = (ts.Days % 365) / 30;
                        txtWorkAge.Text = year.ToString() + " �� " + month.ToString() + " ��͹";
                        txtWorkAge.BackColor = System.Drawing.Color.White;
                    }
                    else
                    {
                        lblResign.Text = "�ѹ������͡:";
                        txtWorkAge.Text = empInfo.ResignDate.ToShortDateString();
                        txtWorkAge.BackColor = System.Drawing.Color.Red;
                    }
                }

                catch
                {
                  
                    cODETextBox.Clear();
                    pRENTextBox.Clear();
                    nAMETextBox.Clear();
                    sURNTextBox.Clear();
                
                 
                    pOSI_ENAMETextBox.Clear();
                    dV_ENAMETextBox.Clear();
                
                    txtGrpot.Clear();
                    txtLine.Clear();
                 
                    textBox4.Clear();
                    txtWorkAge.Clear();
                    jOINTextBox.Clear();
                    txtCompany.Clear();

                }
            }
            get
            {
                return empInfo;
            }

        }
        public void Open()
        {
            empInfo = new EmployeeInfo();
        }

        private void EmpData_Control_Load(object sender, EventArgs e)
        {
           
        }
    }
}
