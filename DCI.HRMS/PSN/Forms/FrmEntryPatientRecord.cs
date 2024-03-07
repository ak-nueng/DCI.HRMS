using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Base;
//using DCIBizPro.DTO.SM;
using DCI.HRD.Service;
using DCI.HRD.Model;
using System.Collections;
using System.Globalization;
using DCI.HRMS.Common;
using System.Diagnostics;
using DCI.HRMS.PSN.DialogBox;
using DCI.Security.Model;

namespace DCI.HRMS.PSN
{
    public partial class FrmEntryPatientRecord : Form , IFormParent , IFormPermission
    {
        private EmployeeService employeeService = EmployeeService.Instance();
        private FirstAidService firstAidService = FirstAidService.Instance();
        private FirstAidRecordInfo patientRecord = new FirstAidRecordInfo();
        
        private string lastDiseaseItem = string.Empty;

        private int curRowIndex_MedicineList = 0;
        private int curRowIndex_DiseaseList = 0;

        private int curColIndex_MedicineList = 0;
        private int curColIndex_DiseaseList = 0;

        private string searchItem = string.Empty;

        public FrmEntryPatientRecord()
        {
            InitializeComponent();
        }

        #region IForm Members

        public string GUID
        {
            get { return string.Empty; }
        }

        public object Information
        {
            get
            {
                patientRecord.RecordNo = txtRecordNo.Text;
                patientRecord.Date = Convert.ToDateTime(txtRecordDate.Text);
                patientRecord.TreatmentBy = (PersonInfo)cboDoctor.SelectedItem;
                patientRecord.RecordBy = txtInputBy.Text;
                patientRecord.Note = txtNote.Text;

                if (rdoIn.Checked)
                {
                    patientRecord.Type = "IN";
                }
                else
                {
                    patientRecord.Type = "OUT";
                }
                if (rdoAccident.Checked)
                {
                    patientRecord.InjuredType = "ACC";
                }
                else
                {
                    patientRecord.InjuredType = "SICK";
                }
                return patientRecord;
            }
            set
            {
                patientRecord = (FirstAidRecordInfo)value;

                txtRecordNo.Text = patientRecord.RecordNo;
                txtRecordDate.Text = patientRecord.Date.ToString("dd/MM/yyyy", new CultureInfo("en-US"));
                cboDoctor.SelectedValue = patientRecord.TreatmentBy.Code;
                txtInputBy.Text = patientRecord.RecordBy;
                txtNote.Text = patientRecord.Note;

                if (patientRecord.Type == "IN")
                {
                    rdoIn.Checked = true;
                    rdoOut.Checked = false;
                }
                else
                {
                    rdoIn.Checked = false;
                    rdoOut.Checked = true;
                }
                if (patientRecord.Type == "ACC")
                {
                    rdoAccident.Checked = true;
                    rdoSick.Checked = false;
                }
                else
                {
                    rdoAccident.Checked = false;
                    rdoSick.Checked = true;
                }
                ShowEmployeeInfo(patientRecord.Patient);
                ShowDiseaseInfo(patientRecord.Diseases);
            }
        }

        public void AddNew()
        {
            uclAction.CurrentAction = FormActionType.AddNew;
            Clear();

            this.patientRecord = new FirstAidRecordInfo();
            this.txtRecordNo.Text = firstAidService.NewPatientRecordNo();
            this.Text = "Patient Record No. : " + this.txtRecordNo.Text;
            txtCode.Focus();
        }

        public void Save()
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                string msg = string.Empty;
                
                if (uclAction.CurrentAction == FormActionType.SaveAs)
                {
                    firstAidService.AddNewPatientRecord((FirstAidRecordInfo)this.Information);
                    msg = "เพิ่มข้อมูลเรียบร้อย";
                }
                else if (uclAction.CurrentAction == FormActionType.Save)
                {
                    firstAidService.SavePatientRecord((FirstAidRecordInfo)this.Information);
                    msg = "บันทึกข้อมูลเรียบร้อย";
                }

                Text = "Patient Record No. : " + patientRecord.RecordNo;
                uclAction.CurrentAction = FormActionType.Save;

                MessageBox.Show(this, msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Cursor = Cursors.Default;
        }

        public void Delete()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                string msg = string.Format("คุณต้องการลบข้อมูล  {0} ใช่หรือไม่?", Text);
                DialogResult result = MessageBox.Show(this, msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (txtRecordNo.Text != string.Empty)
                    {
                        firstAidService.RemovePatientRecord(txtRecordNo.Text);

                        Clear();
                        uclAction.CurrentAction = FormActionType.None;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Cursor = Cursors.Default;
        }

        public void Search()
        {
            try
            {
                Clear();
                patientRecord = firstAidService.FindFirstAidRecord(searchItem);
                if (patientRecord != null)
                {
                    this.Information = patientRecord;

                    if (patientRecord.Diseases != null
                        && patientRecord.Diseases.Count > 0)
                    {
                        DiseaseInfo disease = (DiseaseInfo)patientRecord.Diseases[0];
                        if (disease.Medicines != null 
                            && disease.Medicines.Count > 0)
                        {
                            ShowMedicineInfo(disease.Medicines);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Export()
        {
            
        }

        public void Print()
        {
            
        }

        public void Open()
        {
            this.patientRecord = new FirstAidRecordInfo();
            this.uclAction.Owner = this;

            try
            {
                ArrayList doctors = firstAidService.FindAllDoctor();

                cboDoctor.DisplayMember = "NameInThai";
                cboDoctor.ValueMember = "Code";
                cboDoctor.DataSource = doctors;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
            Clear();
        }

        public void Clear()
        {
            this.Text = "Patient Record No. : ";
            txtCitizenId.Text = string.Empty;
            txtCode.Text = string.Empty;
            txtFullName.Text = string.Empty;
            txtHospital.Text = string.Empty;
            txtPosition.Text = string.Empty;
            txtRecordNo.Text = string.Empty;
            txtSection.Text = string.Empty;
            txtInputBy.Text = "chatthapol.s";
            txtRecordDate.Text = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-US"));

            rdoIn.Checked = true;
            rdoOut.Checked = false;

            picEmp.ImageLocation = "";

            try
            {
                ShowDiseaseInfo(new ArrayList());
            }
            catch { }

            try
            {
                ShowMedicineInfo(new ArrayList());
            }
            catch { }
        }

        public void RefreshData()
        {
            
        }

        public void Exit()
        {
            this.Close();
        }

        #endregion

        #region IFormPermission Members

        public PermissionInfo Permission
        {
            set { this.uclAction.Permission = value; }
        }

        #endregion

        private bool ValidateInput()
        {
            StringBuilder sb = new StringBuilder();
            if (this.txtRecordDate.Text == string.Empty)
                sb.Append("- วันที่ใช้บริการ\n"); txtRecordDate.Focus();
            if (this.txtCode.Text == string.Empty)
                sb.Append("- รหัสพนักงาน\n"); txtCode.Focus();
            //if (this.txtTreatmentBy.Text == string.Empty)
              //  sb.Append("- ผู้รักษา\n"); txtTreatmentBy.Focus();

            string err = sb.ToString();
            if (err.Length > 0)
            {
                sb.Insert(0, "กรุณาระบุข้อมูลต่อไปนี้ให้เรียบร้อย\n");
                MessageBox.Show(this, sb.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }
            else
            {
                return true;
            }
        }
        private void ShowEmployeeInfo(EmployeeInfo employee)
        {
            patientRecord.Patient = employee;

            txtCode.Text = employee.Code;
            txtFullName.Text = employee.NameInThai.ToString();
            txtPosition.Text = employee.Position.NameEng.ToString();
            txtSection.Text = employee.Division.ToString();
            txtCitizenId.Text = employee.CitizenId;
            picEmp.ImageLocation = "http://dciweb.dci.daikin.co.jp/PICTURE/" + employee.Code + ".jpg";

            try
            {
                txtHospital.Text = employee.Hospital.NameThai;
            }
            catch { }
        }
        private void ShowDiseaseInfo(ArrayList diseaseRecords)
        {
            ArrayList disease_List = new ArrayList();
            
            try
            {
                disease_List = firstAidService.FindAllDisease();
            }
            catch { }

            if (disease_List == null)
                disease_List = new ArrayList();

            dgDiseaseList.Columns.Clear();
            dgDiseaseList.Rows.Clear();

            DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn();
            
            column.Name = "โรค";
            column.DataPropertyName = "Code";
            column.ReadOnly = false;
            column.Width = 150;
            column.DataSource = disease_List;
            column.ValueMember = "Code";
            column.DisplayMember = "NameForSearching";

            dgDiseaseList.Columns.Add(column);

            this.dgDiseaseList.RowsAdded -= new DataGridViewRowsAddedEventHandler(this.dgDiseaseList_RowsAdded);

            foreach (DiseaseInfo disease in diseaseRecords)
            {
                this.dgDiseaseList.Rows.Add(disease.Code);
            }

            this.dgDiseaseList.RowsAdded += new DataGridViewRowsAddedEventHandler(this.dgDiseaseList_RowsAdded);
            DataGridViewStyleDefault.SetDefault(this.dgDiseaseList);
        }
        private void ShowMedicineInfo(ArrayList medicineRecords)
        {
            this.dgMedicineList.RowsAdded -= new DataGridViewRowsAddedEventHandler(this.dgMedicineList_RowsAdded);
            this.dgMedicineList.RowsRemoved -= new DataGridViewRowsRemovedEventHandler(this.dgMedicineList_RowsRemoved);

            ArrayList medicines_List = new ArrayList();
            
            try
            {
                medicines_List = firstAidService.FindAllMedicine();
            }
            catch { }

            if (medicines_List == null)
                medicines_List = new ArrayList();

            if (medicineRecords == null)
                medicineRecords = new ArrayList();

            this.dgMedicineList.Columns.Clear();
            this.dgMedicineList.Rows.Clear();

            DataGridViewComboBoxColumn colMedicine_Code = new DataGridViewComboBoxColumn();
            DataGridViewTextBoxColumn colMedicine_Unit = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn colMedicine_Qty = new DataGridViewTextBoxColumn();

            colMedicine_Code.Name = "ยา";
            colMedicine_Code.DataPropertyName = "Code";
            colMedicine_Code.ReadOnly = false;
            colMedicine_Code.Width = 200;
            colMedicine_Code.DataSource = medicines_List;
            colMedicine_Code.ValueMember = "Code";
            colMedicine_Code.DisplayMember = "NameForSearching";

            colMedicine_Qty.Name = "จำนวน";
            colMedicine_Qty.DataPropertyName = "Quantity";
            colMedicine_Qty.Width = 60;
            colMedicine_Qty.ReadOnly = false;
            colMedicine_Qty.MaxInputLength = 4;

            //colMedicine_Qty.MinimumWidth = 1;

            colMedicine_Unit.Name = "หน่วย";
            colMedicine_Unit.DataPropertyName = "Unit";
            colMedicine_Unit.Width = 60;
            colMedicine_Unit.ReadOnly = true;

            this.dgMedicineList.Columns.Add(colMedicine_Code);
            this.dgMedicineList.Columns.Add(colMedicine_Qty);
            this.dgMedicineList.Columns.Add(colMedicine_Unit);

            foreach (MedicineInfo medicine in medicineRecords)
            {
                this.dgMedicineList.Rows.Add(new Object[]
                                            {
                                                medicine.Code , 
                                                medicine.Quantity.ToString() , 
                                                medicine.Unit
                                            });
            }

            this.dgMedicineList.RowsAdded += new DataGridViewRowsAddedEventHandler(this.dgMedicineList_RowsAdded);
            this.dgMedicineList.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.dgMedicineList_RowsRemoved);

            DataGridViewStyleDefault.SetDefault(this.dgMedicineList); 
        }
        private void ShowData()
        {
            try
            {
                if (patientRecord.Patient != null)
                    Debug.WriteLine("Patient : " + patientRecord.Patient.NameInEng.ToString());

                if (patientRecord.Diseases != null)
                {
                    Debug.WriteLine("จำนวนโรค (" + this.curRowIndex_DiseaseList.ToString() + " ) : " + patientRecord.Diseases.Count.ToString());
                    foreach (DiseaseInfo d in patientRecord.Diseases)
                    {
                        Debug.WriteLine("-- " + d.Code);
                        if (d.Medicines != null)
                        {
                            Debug.WriteLine("--- จำนวนยา : " + d.Medicines.Count.ToString());
                            foreach (MedicineInfo m in d.Medicines)
                            {
                                Debug.WriteLine("---- " + m.NameForSearching);
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private DiseaseInfo SelectedDiseaseItem(int row)
        {
            DiseaseInfo disease = new DiseaseInfo();
            DataGridViewCell cell = dgDiseaseList.Rows[row].Cells[0];
            DataGridViewComboBoxCell cbCell = dgDiseaseList.Rows[row].Cells[0] as DataGridViewComboBoxCell;

            foreach (DiseaseInfo selectedItem in cbCell.Items)
            {
                if (selectedItem.Code == (string)cell.Value)
                {
                    disease.Code = selectedItem.Code;
                    disease.Name = selectedItem.Name;
                    disease.Description = selectedItem.Description;
                    disease.Medicines = new ArrayList();
                    break;
                }
            }
            return disease;
        }
        private MedicineInfo SelectedMedicineItem(int row)
        {
            MedicineInfo medicine = new MedicineInfo();

            DataGridViewCell cell = this.dgMedicineList.Rows[row].Cells[0];
            DataGridViewComboBoxCell cbCell = this.dgMedicineList.Rows[row].Cells[0] as DataGridViewComboBoxCell;
            
            foreach (MedicineInfo selectedItem in cbCell.Items)
            {
                if (selectedItem.Code == (string)cell.Value)
                {
                    medicine.Code = selectedItem.Code;
                    medicine.Name = selectedItem.Name;
                    medicine.Unit = selectedItem.Unit;
                    medicine.Quantity = 0;
                    break;
                }
            }
            return medicine;
        }
        
        private void FrmPatientRecord_Load(object sender, EventArgs e)
        {
            this.Open();
        }
        private void rdoIn_Click(object sender, EventArgs e)
        {
            rdoIn.Checked = true;
            rdoOut.Checked = false;
        }
        private void rdoOut_Click(object sender, EventArgs e)
        {
            rdoOut.Checked = true;
            rdoIn.Checked = false;
        }
        private void rdoAccident_Click(object sender, EventArgs e)
        {
            rdoAccident.Checked = true;
            rdoSick.Checked = false;
        }
        private void rdoSick_Click(object sender, EventArgs e)
        {
            rdoAccident.Checked = false;
            rdoSick.Checked = true;
        }
        private void txtEmpCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string searchEmpCode = txtCode.Text;
                if (searchEmpCode.Length > 0)
                {
                    EmployeeInfo employee = employeeService.Find(searchEmpCode);
                    if (employee != null)
                    {
                        ShowEmployeeInfo(employee);
                    }
                    else
                    {
                        MessageBox.Show(this, "ไม่พบข้อมูลพนักงานที่ท่านต้องการค้นหา", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        private void gridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                DataGridViewStyleDefault.ShowRowNumber((DataGridView)sender, e);
            }
            catch { }
        }
        
        private void cmbDisease_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cb = sender as ComboBox;
                DataGridViewCell cell = dgDiseaseList.Rows[curRowIndex_DiseaseList].Cells[curColIndex_DiseaseList];
                
                cell.Value = (string)cb.SelectedValue;

            }
            catch { }
        }
        private void dgDiseaseList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                curRowIndex_DiseaseList = e.RowIndex;
                curColIndex_DiseaseList = e.ColumnIndex;

                DiseaseInfo selectedItem = (DiseaseInfo)patientRecord.Diseases[e.RowIndex];
                
                lblDisease.Text = selectedItem.NameForSearching;
                ShowMedicineInfo(selectedItem.Medicines);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private void dgDiseaseList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DiseaseInfo disease = (DiseaseInfo)patientRecord.Diseases[e.RowIndex];
                DataGridViewCell cell = dgDiseaseList.Rows[e.RowIndex].Cells[e.ColumnIndex];
                DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)dgDiseaseList.Rows[e.RowIndex].Cells[e.ColumnIndex];

                foreach (DiseaseInfo selectedItem in cbCell.Items)
                {
                    if ((string)cell.Value == selectedItem.Code)
                    {
                        disease.Code = selectedItem.Code;
                        disease.Name = selectedItem.Name;
                        disease.Description = selectedItem.Description;
                        break;
                    }
                }
            }
            catch { }
        }
        private void dgDiseaseList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is ComboBox)
            {
                ComboBox cb = e.Control as ComboBox;

                cb.SelectedIndexChanged -= new EventHandler(cmbDisease_SelectedIndexChanged);
                cb.SelectedIndexChanged += new EventHandler(cmbDisease_SelectedIndexChanged);
            }
        }
        private void dgDiseaseList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (patientRecord.Diseases == null)
                patientRecord.Diseases = new ArrayList();

            int row = e.RowIndex-1;
            if (row < 0)
                row = 0;

            DiseaseInfo disease = SelectedDiseaseItem(row);

            if(disease.Code != null 
                && disease.Code != string.Empty)
            {
                patientRecord.Diseases.Add(disease);
                ShowMedicineInfo(disease.Medicines);
                ShowData();
            }
        }
        private void dgDiseaseList_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            
            if (patientRecord.Diseases != null
                && patientRecord.Diseases.Count >= e.RowIndex)
            {
                if (patientRecord.Diseases.Count > 0)
                {
                    try
                    {
                        if(patientRecord.Diseases.Count-1 >= e.RowIndex)
                            patientRecord.Diseases.RemoveAt(e.RowIndex);

                        dgDiseaseList.Rows[0].Selected = true;

                        if(patientRecord.Diseases.Count > 0)
                            ShowMedicineInfo(((DiseaseInfo)patientRecord.Diseases[0]).Medicines);

                        ShowData();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
            }
        }

        private void cmbMedicine_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;

            try
            {
                this.dgMedicineList.Rows[curRowIndex_MedicineList].Cells[curColIndex_MedicineList].Value = cb.SelectedValue;
            }
            catch { }

            try
            {
                MedicineInfo medicine = cb.SelectedItem as MedicineInfo;
                if (medicine != null)
                    dgMedicineList.Rows[curRowIndex_MedicineList].Cells[2].Value = medicine.Unit;
            }
            catch { }

        }
        private void dgMedicineList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                curRowIndex_MedicineList = e.RowIndex;
                curColIndex_MedicineList = e.ColumnIndex;
            }
            catch { }
        }
        private void dgMedicineList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DiseaseInfo disease = (DiseaseInfo)patientRecord.Diseases[curRowIndex_DiseaseList];
                MedicineInfo medicine = (MedicineInfo)disease.Medicines[e.RowIndex];

                //DataGridViewCell cell = (DataGridViewCell)dgMedicineList.Rows[e.RowIndex].Cells[0];
                DataGridViewCell cellQty = (DataGridViewCell)dgMedicineList.Rows[e.RowIndex].Cells[1];
                DataGridViewComboBoxCell cbxCell = (DataGridViewComboBoxCell)dgMedicineList.Rows[e.RowIndex].Cells[0];

                foreach (MedicineInfo selectedItem in cbxCell.Items)
                {
                    if ((string)medicine.Code == selectedItem.Code)
                    {
                        medicine.Code = selectedItem.Code;
                        medicine.Name = selectedItem.Name;
                        medicine.Unit = selectedItem.Unit;
                        try
                        {
                            medicine.Quantity = Convert.ToInt32((string)cellQty.Value);
                        }
                        catch { }
                        break;
                    }
                }
            }
            catch { }
        }
        private void dgMedicineList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is ComboBox)
            {
                ComboBox cb = e.Control as ComboBox;

                cb.SelectedIndexChanged -= new EventHandler(cmbMedicine_SelectedIndexChanged);
                cb.SelectedIndexChanged += new EventHandler(cmbMedicine_SelectedIndexChanged);
            }
        }
        private void dgMedicineList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                DiseaseInfo disease = (DiseaseInfo)this.patientRecord.Diseases[curRowIndex_DiseaseList];
                if (disease.Medicines == null)
                    disease.Medicines = new ArrayList();

                int row = e.RowIndex - 1;
                if (row < 0)
                    row = 0;

                MedicineInfo medicine = SelectedMedicineItem(row);

                if (medicine.Code != null && medicine.Code != string.Empty)
                    disease.Medicines.Add(medicine);
            }
            catch { }
            ShowData();
        }
        private void dgMedicineList_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (patientRecord.Diseases != null)
            {
                DataGridViewSelectedRowCollection selectedRows =  dgDiseaseList.SelectedRows;
                if (selectedRows.Count > 0)
                {
                    try
                    {
                        DataGridViewRow row = selectedRows[0];
                        DiseaseInfo disease = (DiseaseInfo)patientRecord.Diseases[row.Index];
                        if (disease.Medicines != null && disease.Medicines.Count > 0)
                        {
                            if (disease.Medicines.Count - 1 >= e.RowIndex)
                                disease.Medicines.RemoveAt(e.RowIndex);

                            ShowData();
                        }
                    }
                    catch { }
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DlgSearchPatientRecord dlgSearch = new DlgSearchPatientRecord();
                dlgSearch.ShowDialog(this);

                this.searchItem = dlgSearch.SelectedItem;
                dlgSearch.Dispose();
            }
            catch { }

            this.Search();
        }

        private void btnViewProfile_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text != string.Empty
                    || txtCode.Text.Length < 5)
                {
                    FrmViewPatientRecord frm = new FrmViewPatientRecord();
                    frm.SelectedItem = txtCode.Text.Trim();
                    //frm.Search();
                    frm.ShowDialog(this);
                }
                else
                {
                    MessageBox.Show(this, "Warning", "รหัสพนักงานไม่ถูกต้อง กรุณาระบุใหม่.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCode.Focus();
                }
            }
            catch { }
        }
    }
}