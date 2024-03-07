using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Service;
using System.Collections;
using DCI.HRMS.Base;
using DCI.Security.Model;
using DCI.HRMS.Model.Welfare;
using DCI.HRMS.Model.Personal;
using DCI.HRMS.Util;
using DCI.HRMS.Common;
using DCI.HRMS.Model.Common;
using DCI.HRMS.Model;

namespace DCI.HRMS.Welfare
{
    public partial class FrmMedical : Form, IFormParent, IFormPermission
    {
        private FormAction formAct = FormAction.New;
        private MedicalAllowanceInfo infromation = new MedicalAllowanceInfo();
        private ApplicationManager appMgr = ApplicationManager.Instance();
        private ObjectInfo inform = new ObjectInfo();
        private ArrayList addData;
        private ArrayList searchData;
        private ArrayList gvData = new ArrayList();
        private readonly string[] colName = new string[] { "DocumentNo", "EmployeeCode", "TreateDate", "PayDate", "Symptom", "PatenType", "Relation","Patiener", "Hospital","Distict","Province", "Amount", "CreateBy", "CreateTime", "LastUpdateBy", "LastUpdateTime" };

        private readonly string[] propName = new string[] { "DocNo", "EmCode", "TrDate", "RqDate", "Symptom", "PatienType", "Relation", "PatienName", "Hospital", "District", "Province", "Amount", "CreateBy", "CreateDateTime", "LastUpdateBy", "LastUpDateDateTime" };
        private readonly int[] width = new int[] { 80, 80, 100, 80, 100, 100, 100, 100, 100,100,100,100, 100, 100, 120, 100, 120 };

        private MedicalAllowanceService medSvr = MedicalAllowanceService.Instance();
        private EmployeeService empSvr = EmployeeService.Instance();
        public FrmMedical()
        {
            InitializeComponent();
        }

        private void AddGridViewColumns()
        {
            this.dgItems.Columns.Clear();
            dgItems.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn[] columns = new DataGridViewTextBoxColumn[colName.Length];
            for (int index = 0; index < columns.Length; index++)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();

                column.Name = colName[index];
                column.DataPropertyName = propName[index];
                column.ReadOnly = true;
                column.Width = width[index];

                columns[index] = column;
                dgItems.Columns.Add(columns[index]);
            }
            dgItems.ClearSelection();
        }
        private void FillDataGrid()
        {
            dgItems.DataBindings.Clear();
            dgItems.DataSource = null;
            dgItems.DataSource = gvData;
            // dgItems.CurrentCell = null;
            //dgItems.Refresh();

            this.Update();

        }
        private void FindRecord(string id)
        {
            try
            {
                int found = 0;
                for (int i = 0; i < gvData.Count; i++)
                {
                    found = i;
                    MedicalAllowanceInfo item = (MedicalAllowanceInfo)gvData[i];
                    if (item.DocNo == id)
                    {
                        dgItems.CurrentCell = dgItems[0, found];
                        break;
                    }
                }
            }
            catch
            {
            }

        }



        #region IFormPermission Members

        public PermissionInfo Permission
        {
            set { this.ucl_ActionControl1.Permission = value; }
        }

        #endregion

        #region IForm Members

        public string GUID
        {
            get { return null; }
        }

        public object Information
        {

            get
            {
                if (formAct == FormAction.New)
                {
                    try
                    {
                        infromation.DocNo = txtDocId.Text;
                        infromation.EmCode = txtCode.Text;
                        infromation.TrDate = dateTimePicker1.Value.Date;
                        infromation.RqDate = dateTimePicker2.Value.Date;
                        infromation.PatienType = rbnIpd.Checked ? "I" : "O";
                        infromation.Hospital = txtHospital.Text;
                        infromation.Symptom = txtSymptom.Text;
                        infromation.RelationType = txtRelation.Text;
                        infromation.PatienName = txtPatientName.Text;
                        infromation.District = txtDistict.Text;
                        infromation.Province = txtProvince.Text;
                        infromation.Amount = int.Parse(txtAmount.Text);

                        //------- Medical -------
                        if (dateTimePicker1.Value.Date > new DateTime(2015,12,31))
                        {
                            if (rbnMidwife.Checked == true) {
                                infromation.RelationType = "RELA8";
                            }
                        }
                        //------- Medical -------


                        inform.CreateBy = appMgr.UserAccount.AccountId;
                        infromation.Inform = inform;
                        return infromation;

                    }
                    catch
                    {

                        return null;
                    }



                }
                else if (formAct == FormAction.Save)
                {
                    MedicalAllowanceInfo med = (MedicalAllowanceInfo)medical_Control2.Information;

                    inform.CreateBy = med.CreateBy;
                    inform.CreateDateTime = med.CreateDateTime;
                    inform.LastUpdateBy = appMgr.UserAccount.AccountId;
                    med.Inform = inform;
                    return medical_Control2.Information;

                }
                return null;
            }
            set
            {
                if (formAct == FormAction.Save)
                {
                    infromation = (MedicalAllowanceInfo)value;
                    medical_Control2.Information = infromation;
                }
            }
        }
        private bool IsValidInput()
        {

            if (txtCode.Text == "")
            {
                MessageBox.Show("กรุณาป้อนรหัสพนักงาน", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCode.Focus();
                return false;
            }

            if (txtHospital.Text == "")
            {

                MessageBox.Show("กรุณาป้อนชื่อโรงพยาบาล", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHospital.Focus();
                return false;
            }
            if (txtSymptom.Text == "")
            {

                MessageBox.Show("กรุณาป้อนรายละเอียดโรค", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSymptom.Focus();
                return false;
            }


            try
            {
                int t = int.Parse(txtAmount.Text);
            }
            catch
            {

                MessageBox.Show("กรุณาป้อนจำนวนเงินให้ถูกต้อง", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAmount.Clear();
                return false;
            }
            return true;
        }

        public void AddNew()
        {
            kryptonHeaderGroup1_Click(kryptonHeaderGroup1, new EventArgs());


        }

        public void Save() 
        {
            if (formAct == FormAction.New)
            {
                if (IsValidInput())
                {
                    try
                    {

                        //------- Medical SetDataInfo --------
                        medical_Sumary1.SetInfo(txtCode.Text, dateTimePicker1.Value.Date);


                        MedicalAllowanceInfo item = (MedicalAllowanceInfo)Information;
                     
                        double[] pd = (double[])medical_Sumary1.Information;
                        double remain=0;
                        bool check = true;
                        double request = item.Amount;
                  
                        if (rbnOpd.Checked)
                        {
                            if (pd[1] < request)
                            {
                                check = false;
                                remain = pd[1];
                            }
                            if (item.RelationType== "RELA1"||item.RelationType=="RELA2" )
                            {
                                if (pd[2]<request)
                                {
                                    check = false;
                                    remain = pd[2];
                                }

                            
                            }
                            //------- Medical Pregnant  midwife  --------
                            if (item.RelationType == "RELA8")
                            {
                                if (pd[3] < request)
                                {
                                    check = false;
                                    remain = pd[3];
                                }
                            }
                            //------- Medical Pregnant  midwife  --------
                           
                        }
                        else
                        {
                            if (pd[0] < request)
                            {
                                check = false;
                                remain = pd[0];
                            }
                        }

                        if (pd[4]<request)
                        {
                            check = false;
                            remain = pd[4];
                        }
                      
                        if (!check)
                        {
                            if (MessageBox.Show("ยอดขอเบิกมากกว่ายอดคงเหลือ\nคุณต้องการบันทึกใช่หรือไม่?", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            {
                                txtAmount.Text = remain.ToString("00");
                                txtAmount.Focus();
                                return;
                            }
                        }


                        if (rbnByEmployee.Checked){
                            medSvr.SaveMedical(item);
                        }else if(rbnByHospital.Checked){
                            medSvr.SaveMedical2(item);
                        }
                        this.RefreshData();
                        // this.AddNew();
                        try
                        {
                           // dgItems.CurrentCell = dgItems[0, dgItems.Rows.Count - 1];
                            FindRecord(item.DocNo);
                        }
                        catch
                        { }
                        this.Clear();
                        txtCode.Focus();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }

            }
            else if (formAct == FormAction.Save)
            {
                try
                {
                    double tempAmount = 0d;
                
                    try
                    {
                        MedicalAllowanceInfo temp = (MedicalAllowanceInfo)searchData[dgItems.SelectedRows[0].Index];
                        tempAmount = temp.Amount;
                    }
                    catch 
                    {                       
                    }
                    MedicalAllowanceInfo usrMedical = (MedicalAllowanceInfo)medical_Control2.Information;

                    

                    //if (txtCode.Text.Trim().Length == 5) { 
                    if (usrMedical != null && usrMedical.EmCode.Length == 5)
                    { 
                        //------- Medical SetDataInfo --------
                        //medical_Sumary1.SetInfo(txtCode.Text, dateTimePicker1.Value.Date);
                        medical_Sumary1.SetInfo(usrMedical.EmCode, usrMedical.TrDate );

                        MedicalAllowanceInfo item = (MedicalAllowanceInfo)Information;
                        double[] pd = (double[])medical_Sumary1.Information;
                        double remain = 0;
                        bool check = true;
                        double request = item.Amount;

                        if (item.PatienType=="O")
                        {
                            if (pd[1] + tempAmount < request)
                            {
                                check = false;
                                remain = pd[1] + tempAmount;
                            }
                            if (item.RelationType == "RELA1" || item.RelationType == "RELA2")
                            {
                                if (pd[2] + tempAmount < request)
                                {
                                    check = false;
                                    remain = pd[2] + tempAmount;
                                }
                            }
                            if (item.RelationType == "RELA8")
                            {
                                if (pd[3] + tempAmount < request)
                                {
                                    check = false;
                                    remain = pd[3] + tempAmount;
                                }
                            }

                        }
                        else
                        {
                            if (pd[0] + tempAmount < request)
                            {
                                check = false;
                                remain = pd[0] + tempAmount;
                            }
                        }

                        if (pd[4] + tempAmount < request)
                        {
                            check = false;
                            remain = pd[4] + tempAmount;
                        }

                        if (!check)
                        {
                            if (MessageBox.Show("ยอดขอเบิกมากกว่ายอดคงเหลือ\nคุณต้องการบันทึกใช่หรือไม่?", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            {
                                txtAmount.Text = remain.ToString("00");
                                txtAmount.Focus();
                                return;
                            }
                        }

                        medSvr.UpdateMedical(item);
                        this.RefreshData();
                        // this.AddNew();

                    } // end if check employee code

                }
                catch (Exception ex)
                {
                    MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }


        }

        public void Delete()
        {
            if (formAct == FormAction.Save)
            {
                try
                {
                    medSvr.DeleteMedical((MedicalAllowanceInfo)Information);
                    this.RefreshData();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("ไม่สามารถลบข้อมูลได้เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        public void Search()
        {
            if (formAct == FormAction.New)
            {
                addData = medSvr.GetMedical(txtCode.Text, dateTimePicker1.Value.Date);
                gvData = addData;

                FillDataGrid();
                medical_Sumary1.SetInfo(txtCode.Text, dateTimePicker1.Value.Date);
            }
            else if (formAct == FormAction.Save)
            {
                if (!kryptonGroup9.Enabled)
                {
                    searchData = medSvr.GetMedical(txtsDocId1.Text, txtsDocId2.Text);
                    gvData = searchData;
                    FillDataGrid();
                }
                else
                {
                    searchData = medSvr.GetMedical(txtSCode.Text, dtpSFrom.Value.Date, dtpSTo.Value.Date);
                    gvData = searchData;
                    FillDataGrid();

                }


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
            ucl_ActionControl1.Owner = this;
            AddGridViewColumns();
            addData = new ArrayList();
            searchData = new ArrayList();
            empFamily_Control1.empSvr = empSvr;
            empFamily_Control1.Open();
            // empData_Control1.empServ = empSvr;
            medical_Sumary1.medSvr = medSvr;
            medical_Sumary1.OpenFileDialog();
            kryptonHeaderGroup1_Click(kryptonHeaderGroup1, new EventArgs());



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
                symptom.Add(var.Symptom);
            }
            txtSymptom.AutoCompleteCustomSource = symptom;


            AutoCompleteStringCollection dist = new AutoCompleteStringCollection();
            temp = medSvr.GetAutoCompDistrict();
            foreach (MedicalAllowanceInfo var in temp)
            {
                dist.Add(var.District);
            }
            txtDistict.AutoCompleteCustomSource = dist;

            AutoCompleteStringCollection prov = new AutoCompleteStringCollection();
            temp = medSvr.GetAutoCompProvince();
            foreach (MedicalAllowanceInfo var in temp)
            {
                prov.Add(var.Province);
            }
            txtProvince.AutoCompleteCustomSource = prov;

        }

        public void Clear()
        {
            txtCode.Clear();
            txtHospital.Clear();
            txtPatientName.Clear();
            txtSymptom.Clear();
            txtAmount.Clear();
            txtDocId.Clear();
            txtRelation.Clear();
            txtDistict.Clear();
            txtProvince.Clear();
            kryptonRadioButton4.Checked = true;
            rbnOpd.Checked = true;
            empFamily_Control1.DisableSelect = true;
        }

        public void RefreshData()
        {
            int selrow = 0;
            try
            {
                selrow = dgItems.SelectedRows[0].Index;
            }
            catch
            { }
            this.Search();
            try
            {                // dgItems.ClearSelection();
                dgItems.CurrentCell = dgItems[0, selrow];
            }
            catch
            { }
        }

        public void Exit()
        {
            this.Close();
        }

        #endregion


        private void FrmMedical_Load(object sender, EventArgs e)
        {
            this.Open();


        }

        private void medical_Control1_Load(object sender, EventArgs e)
        {
            medical_Control1.medSvr = medSvr;
            medical_Control1.Open();
        }

        private void medical_Control1_txtCode_Enter(string em_code, DateTime rq_date)
        {



        }

        private void empFamily_Control1_Load(object sender, EventArgs e)
        {

        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                string em_code = txtCode.Text;
            EmployeeDataInfo emp = empSvr.GetEmployeeData(em_code);
             
                if (emp != null)
                {
                    empData_Control1.Information = emp;
                    
                    if(rbnByEmployee.Checked){
                        txtDocId.Text = medSvr.LoadRecordKey();
                    }else if(rbnByHospital.Checked){
                        txtDocId.Text = medSvr.LoadRecordKey2();
                    }

                    this.Search();

                    empFamily_Control1.SetFamilyData(em_code);
                    kryptonRadioButton4_CheckedChanged(sender, e);
                    KeyPressManager.Enter(e);
                    ucl_ActionControl1.CurrentAction = FormActionType.AddNew;

                }
                else
                {
                    this.Clear();
                }
            }
        }

        private void dgItems_SelectionChanged(object sender, EventArgs e)
        {
            medical_Control1.Information = new MedicalAllowanceInfo();
            medical_Control2.Information = new MedicalAllowanceInfo();
            if (formAct == FormAction.New)
            {

                try
                {
                    MedicalAllowanceInfo med = (MedicalAllowanceInfo)addData[dgItems.SelectedRows[0].Index];
                    medical_Control1.Information = med;
                   // empFamily_Control1.SelectRelation ( med.Relation);
                    medical_Sumary1.SetInfo(med.EmCode, med.TrDate);
                    ucl_ActionControl1.CurrentAction = FormActionType.AddNew;

                    //  empDetail_Control1.Information = med.EmCode;
                    // empFamily_Control1.SetFamilyData(med.EmCode);
                }
                catch
                {

                }
            }
            else if (formAct == FormAction.Save)
            {

                try
                {

                    MedicalAllowanceInfo med = (MedicalAllowanceInfo)searchData[dgItems.SelectedRows[0].Index];
                    ucl_ActionControl1.CurrentAction = FormActionType.Save;
                    medical_Control2.Information = med;
                    EmployeeDataInfo emp = empSvr.GetEmployeeData(med.EmCode);
                   
                    empData_Control1.Information =emp;
                    empFamily_Control1.SetFamilyData(med.EmCode);
                    medical_Sumary1.SetInfo(med.EmCode, med.TrDate);
                    empFamily_Control1.SelectRelation(med.RelationType);

                }
                catch
                {

                }
            }
        }

        private void kryptonHeaderGroup1_Click(object sender, EventArgs e)
        {
            if (sender == kryptonHeaderGroup1)
            {
                kryptonHeaderGroup3.Collapsed = true;
                kryptonHeaderGroup1.Collapsed = false;

                formAct = FormAction.New;
                gvData = addData;
                FillDataGrid();
                ucl_ActionControl1.CurrentAction = FormActionType.None;
                this.Clear();

            }
            else
            {
                formAct = FormAction.Save;
                kryptonHeaderGroup1.Collapsed = true;
                kryptonHeaderGroup3.Collapsed = false;
                gvData = searchData;
                FillDataGrid();
                ucl_ActionControl1.CurrentAction = FormActionType.None;
                empFamily_Control1.DisableSelect = false;

            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Search();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            txtsDocId2.Text = txtsDocId1.Text;
            if (txtsDocId1.Text != "")
            {
                kryptonGroup9.Enabled = false;
            }
            else
            {
                kryptonGroup9.Enabled = true;
            }
        }

        private void empFamily_Control1_Family_Slect()
        {
            FamilyInfo fm = (FamilyInfo)empFamily_Control1.Information;
            if (formAct == FormAction.New)
            {
                if (kryptonRadioButton3.Checked)
                {
                    if (fm.Relation == "RELA4" || fm.Relation == "RELA5")
                    {
                        Age ag = new Age(fm.Birth,dateTimePicker1.Value);
                        if (ag.Years > 18 || ag.Years ==18 && (ag.Days>0 || ag.Months>0))
                        {
                            MessageBox.Show("ไม่สามารถเบิกค่ารักษาพยาบาล บุตรอายุเกิน 18 ปีบริบูรณ์ได้", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtPatientName.Text = "";
                            txtRelation.Text = "";
                            return;
                        }                       

                    }
                    txtPatientName.Text = fm.NameInThai.ToString();
                    txtRelation.Text = fm.Relation;
                }

            }
            else if (formAct == FormAction.Save)
            {
               // medical_Control2.PatienName = fm.NameInThai.ToString();
              //  medical_Control2.Relation = fm.Relation;

            }
        }
        private void empDetail_Control1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (formAct == FormAction.Save)
            {
                EmployeeInfo emp = (EmployeeInfo)empData_Control1.Information;
                medical_Control2.PatienName = emp.NameInThai.ToString();
                medical_Control2.Relation = "";

            }
        }


        private void kryptonRadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (formAct == FormAction.New)
            {
                if (kryptonRadioButton4.Checked)
                {
                    empFamily_Control1.DisableSelect = true;
                    try
                    {
                        EmployeeInfo emp = (EmployeeInfo)empData_Control1.Information;
                        txtPatientName.Text = emp.NameInThai.ToString();
                    }
                    catch
                    {

                        txtPatientName.Text = "";
                    }
                    txtRelation.Text = "";

                }
                else if (rbnMidwife.Checked)
                {
                    empFamily_Control1.DisableSelect = true;
                    try
                    {
                        EmployeeInfo emp = (EmployeeInfo)empData_Control1.Information;
                        txtPatientName.Text = emp.NameInThai.ToString();
                    }
                    catch
                    {

                        txtPatientName.Text = "";
                    }
                    txtRelation.Text = "";

                }
                else
                {
                    empFamily_Control1.DisableSelect = false;
                    try
                    {
                        FamilyInfo fm = (FamilyInfo)empFamily_Control1.Information;
                        if (fm != null)
                        {
                            txtPatientName.Text = fm.NameInThai.ToString();
                            txtRelation.Text = fm.Relation;
                        }
                        else
                        {
                            MessageBox.Show("ไม่พบข้อมูลสมาชิกครอบครัว", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            kryptonRadioButton4.Checked = true;
                        }
                    }
                    catch
                    {
                        txtRelation.Text = "";
                        txtPatientName.Text = "";
                    }

                }

            }
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Save();

            }
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressManager.EnterNumericOnly(e);
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPressManager.Enter(e);
        }

        private void dgItems_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyleDefault.ShowRowNumber(dgItems, e);
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            
        }

    }
}