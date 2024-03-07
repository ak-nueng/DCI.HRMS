using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Common;
using DCI.HRD.Model;
using System.Collections;
using System.Globalization;
using DCI.HRD.Service;

namespace DCI.HRMS.PSN.DialogBox
{
    public partial class DlgSearchPatientRecord : Form
    {
        private string selectedItem = string.Empty;
        private FirstAidService firstAidService = FirstAidService.Instance();

        public DlgSearchPatientRecord()
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
                string keyword = txtKeyword.Text;

                this.dgResult.Columns.Clear();
                this.dgResult.Rows.Clear();
                this.dgResult.AutoGenerateColumns = false;

                if (keyword == string.Empty)
                    keyword = "%";

                ArrayList patientRecords = firstAidService.FindFirstAidRecords(keyword, dtFrom.Value, dtTo.Value);

                PrepareGridColumn();

                foreach (FirstAidRecordInfo patientRecord in patientRecords)
                {
                    this.dgResult.Rows.Add(new Object[]
                                            {
                                                patientRecord.RecordNo , 
                                                patientRecord.Date.ToString("dd/MM/yy",new CultureInfo("en-US")) ,
                                                patientRecord.Type ,
                                                patientRecord.Patient.Code , 
                                                patientRecord.Patient.NameInThai.ToString() ,
                                                patientRecord.Patient.Division.ToString()
                                            });
                }

                DataGridViewStyleDefault.SetDefault(this.dgResult);
            }
            catch
            {
                dgResult.Rows.Clear();
            }
        }

        private void PrepareGridColumn()
        {
            DataGridViewTextBoxColumn col_RecNo = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col_RecDate = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col_RecType = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col_EmpCode = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col_EmpName = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn col_EmpDept = new DataGridViewTextBoxColumn();

            col_RecNo.Name = "R No.";
            col_RecNo.ReadOnly = true;
            col_RecNo.Width = 95;
            col_RecNo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            col_RecDate.Name = "วันที่";
            col_RecDate.Width = 77;
            col_RecDate.ReadOnly = true;
            col_RecDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            col_RecType.Name = "Type";
            col_RecType.Width = 60;
            col_RecType.ReadOnly = true;
            col_RecType.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            col_EmpCode.Name = "รหัส พนง.";
            col_EmpCode.Width = 80;
            col_EmpCode.ReadOnly = true;
            col_EmpCode.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            col_EmpName.Name = "ชื่อ-นามสกุล";
            col_EmpName.Width = 135;
            col_EmpName.ReadOnly = true;
            col_EmpCode.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            col_EmpDept.Name = "สังกัด";
            col_EmpDept.Width = 200;
            col_EmpDept.ReadOnly = true;
            col_EmpCode.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.dgResult.Columns.Add(col_RecNo);
            this.dgResult.Columns.Add(col_RecDate);
            this.dgResult.Columns.Add(col_RecType);
            this.dgResult.Columns.Add(col_EmpCode);
            this.dgResult.Columns.Add(col_EmpName);
            this.dgResult.Columns.Add(col_EmpDept);
        }

        private void DlgSearchPatientRecord_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            dtFrom.Value = new DateTime(now.Year, now.Month, 1, 0, 0, 0);
            dtTo.Value = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);

            txtKeyword.Text = string.Empty;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.Search();
        }

        private void dgResult_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                DataGridViewStyleDefault.ShowRowNumber((DataGridView)sender, e);
            }
            catch { }
        }

        private void dgResult_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewCell cell = dgResult.Rows[e.RowIndex].Cells[0];
                SelectedItem = Convert.ToString(cell.Value);
                this.Close();
            }
            catch { }
        }
    }
}