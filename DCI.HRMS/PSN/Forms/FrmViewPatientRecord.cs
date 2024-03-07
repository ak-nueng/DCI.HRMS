using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCI.HRD.Model;
using DCI.HRD.Service;
using System.Globalization;
using System.Collections;
using DCI.HRMS.Common;

namespace DCI.HRMS.PSN
{
    public partial class FrmViewPatientRecord : Form
    {
        private FirstAidService firstAidService = FirstAidService.Instance();
        private EmployeeService employeeService = EmployeeService.Instance();

        private string selectedItem;

        public FrmViewPatientRecord()
        {
            InitializeComponent();
        }

        public string SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; }
        }

        public void Search()
        {
            try
            {
                Clear();
                EmployeeInfo emp = employeeService.Find(selectedItem);

                if (emp != null)
                {
                    this.Text = "ประวัติการเจ็บป่วย พนง.: " + emp.Code + " " + emp.NameInThai.ToString();
                    txtCode.Text = emp.Code;
                    txtFullName.Text = emp.NameInThai.ToString();
                    txtCitizenId.Text = emp.CitizenId;
                    txtHospital.Text = emp.Hospital.NameThai;
                    txtJoinDate.Text = emp.JoinDate.ToString("dd MMM yy", new CultureInfo("th-TH"));
                    txtPosition.Text = emp.Position.NameEng;
                    txtSection.Text = emp.Division.ToString();
                    txtLine.Text = string.Empty;
                    picEmp.ImageLocation = "http://dciweb.dci.daikin.co.jp/PICTURE/" + emp.Code + ".jpg";

                    ArrayList patientRecords = PatientRecordService.Instance().FindPatientRecords(emp.Code);

                    ShowPatientRecords(patientRecords);
                }
            }
            catch { }
        }

        private void ShowPatientRecords(ArrayList patientRecords)
        {
            try
            {
                dgPatientRecord.AutoGenerateColumns = false;
                dgPatientRecord.Columns.Clear();
                dgPatientRecord.Rows.Clear();

                DataGridViewTextBoxColumn col_Date = new DataGridViewTextBoxColumn();
                DataGridViewTextBoxColumn col_Disease = new DataGridViewTextBoxColumn();

                col_Date.Name = "วันที่";
                col_Date.ReadOnly = true;
                col_Date.Width = 100;
                col_Date.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                col_Disease.Name = "โรค";
                col_Disease.ReadOnly = true;
                col_Disease.Width = 250;
                col_Disease.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                dgPatientRecord.Columns.Add(col_Date);
                dgPatientRecord.Columns.Add(col_Disease);

                foreach (PatientRecordInfo patientRecord in patientRecords)
                {
                    dgPatientRecord.Rows.Add(new Object[] { 
                                                    patientRecord.Date.ToString("yyyy-MM-dd",new CultureInfo("en-US")) , 
                                                    patientRecord.Disease.Name
                                                });
                }

                DataGridViewStyleDefault.SetDefault(dgPatientRecord);
            }
            catch { }
        }

        public void Clear()
        {
            txtCitizenId.Text = string.Empty;
            txtCode.Text = string.Empty;
            txtFullName.Text = string.Empty;
            txtHospital.Text = string.Empty;
            txtJoinDate.Text = string.Empty;
            txtLine.Text = string.Empty;
            txtPosition.Text = string.Empty;
            txtSection.Text = string.Empty;
            picEmp.ImageLocation = string.Empty;

            dgPatientRecord.Rows.Clear();
        }

        private void FrmViewPatientRecord_Load(object sender, EventArgs e)
        {
            this.Clear();
            if (this.selectedItem != string.Empty)
            {
                Search();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.selectedItem = txtCode.Text.Trim();
            Search();
        }

        private void dgPatientRecord_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                DataGridViewStyleDefault.ShowRowNumber((DataGridView)sender, e);
            }
            catch { }
        }
    }
}