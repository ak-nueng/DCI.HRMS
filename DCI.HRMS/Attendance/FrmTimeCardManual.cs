using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Service;
using DCI.HRMS.Model.Attendance;
using DCI.HRMS.Model.Common;
using DCI.HRMS.Common;
using DCI.HRMS.Base;
using DCI.HRMS.Model;
using DCI.HRMS.Service.SubContract;
using DCI.HRMS.Service.Trainee;
using DCI.HRMS.Model.Personal;

namespace DCI.HRMS.Attendance
{
    public partial class FrmTimeCardManual   : BaseForm, IFormParent, IFormPermission
    {
        private FormAction formAct = FormAction.New;
        private readonly string[] colName = new string[] { "Code", "Date", "From", "To", "Type", "CreateBy", "CreateDate" ,"LastUpdateBy","LastUpdateDateTime"};
        private readonly string[] propName = new string[] { "EmpCode", "RqDate", "TimeFrom", "TimeTo", "RqType", "CreateBy", "CreateDateTime", "LastUpdateBy", "LastUpdateDateTime" };
        private readonly int[] width = new int[] { 80, 100, 100, 100, 100,100,100 ,100,100,100};

        ApplicationManager appMgr = ApplicationManager.Instance();
        private TimeCardManualInfo tmrq = new TimeCardManualInfo();
        private ArrayList addData; 
        private ArrayList searchData ;    
        private ArrayList gvData = new ArrayList();
        private EmployeeService empsrv = EmployeeService.Instance();
        private SubContractService subsrv = SubContractService.Instance();
        private TraineeService tnsrv = TraineeService.Instance();


        private TimeCardService tmSvr = TimeCardService.Instance();

        private SubContractTimeCardService subTmSvr = SubContractTimeCardService.Instance();

        private TraineeTimeCardService tnTmSvr = TraineeTimeCardService.Instance();

        private DataGridViewPrinter MyDataGridViewPrinter;

        private bool listMode = false;
        public FrmTimeCardManual()
        {
            InitializeComponent();
        }

        private void FrmTimeCardManual_Load(object sender, EventArgs e)
        {
            Open();
            ucl_ActionControl1.Owner = this;

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
            /*gvData = new ArrayList();
            if (formAct == FormAction.New)
            {
                gvData = addData;
               // dgItems.CurrentCell = dgItems.FirstDisplayedCell;
            }
            else if (formAct == FormAction.Save)
            {
                gvData = searchData;
                dgItems.CurrentCell = dgItems.FirstDisplayedCell;
            }*/
            dgItems.DataSource = gvData;       
            this.Update();
        }

        #region IForm Members

        public string GUID
        {
            get {return string.Empty; }
        }

        public object Information
        {
            get
            {
                return tmrq;
            }
            set
            {
                tmrq = (TimeCardManualInfo)value;
            }
        }

        public void AddNew()
        {
            TimeCardManualInfo item = timeCardManual_Control1.Information;

            if (item != null)
            {
                EmployeeInfo emp = empsrv.Find(item.EmpCode);

                if (item.EmpCode.StartsWith("I"))
                {
                    emp = subsrv.Find(item.EmpCode);
                }
                else if (item.EmpCode.StartsWith("7"))
                {
                    emp = tnsrv.Find(item.EmpCode);
                }
              

                if (emp != null)
                {
                    bool dupl = false;
                    foreach (TimeCardManualInfo test in addData)
                    {
                        if (test.EmpCode == item.EmpCode && test.RqDate == item.RqDate && test.RqType == item.RqType)
                        {
                            dupl = true;
                            break;
                        }
                    }
                    if (!dupl)
                    {
                      
                        if (!tmSvr.CheckTimeManual(item))
                        {
                            try
                            {
                                this.Information = item;                             
                                this.Save();                        
                            }
                            catch
                            {
                                //*Save Data Error*/


                            }
                            gvData = addData;
                            FillDataGrid();

                            if (dgItems.Rows.Count > 0)
                            {
                                try
                                {

                                    dgItems.CurrentCell = dgItems.Rows[dgItems.Rows.Count - 1].Cells[2];
                                }
                                catch
                                {
                                }
                            }


                        }
                        else
                        {
                            //*TimeManual Exited in Database*/
                        }


                    }
                    else
                    {
                        // /*TimeManual Exited inList*/

                    }

                }
                else
                {
                    //*Employee not found*/
                }

            }
        }

        public void Save()
        {
            this.Cursor = Cursors.WaitCursor;
            TimeCardManualInfo rq;
            ObjectInfo inform;
            if (formAct == FormAction.New)
            {
                if (ucl_ActionControl1.Permission.AllowAddNew)
                {

                    try
                    {
                        rq = (TimeCardManualInfo)this.Information;
                        inform = new ObjectInfo();
                        inform.CreateBy = appMgr.UserAccount.AccountId;
                        rq.Inform = inform;
                        if (rq.EmpCode.StartsWith("I"))
                        {
                            subTmSvr.SaveTimeCardManual(rq);
                        }
                        else if (rq.EmpCode.StartsWith("7"))
                        {
                            tnTmSvr.SaveTimeCardManual(rq);
                        }
                        else
                        {
                            tmSvr.SaveTimeCardManual(rq);
                        }





                        addData.Add(rq);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ไม่สามารถบันทึกข้อมูล ได้เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("ไม่สามารถบันทึกข้อมูล ได้เนื่องจาก คุณไม่มีสิทธิเข้าถึง", "Access Denie", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else if (formAct== FormAction.Save)
            {
                try
                {
                    if (ucl_ActionControl1.Permission.AllowEdit)
                    {
                        foreach (DataGridViewRow var in dgItems.SelectedRows)
                        {
                            TimeCardManualInfo rqedit = (TimeCardManualInfo)this.Information;
                            rq = (TimeCardManualInfo)searchData[var.Index];
                            inform = new ObjectInfo();
                            inform.LastUpdateBy = appMgr.UserAccount.AccountId;
                            rq.Inform = inform;
                            TimeCardManualInfo editdata  = (TimeCardManualInfo) timeCardManual_Control2.Information;

                            rq.RqType = editdata.RqType;
                            rq.TimeFrom = editdata.TimeFrom;
                            rq.TimeTo = editdata.TimeTo;
                            try
                            {

                                if (rq.EmpCode.StartsWith("I"))
                                {
                                    subTmSvr.UpdateTimeCardManual(rq);
                                }
                                else if (rq.EmpCode.StartsWith("7"))
                                {
                                    tnTmSvr.UpdateTimeCardManual(rq);
                                }
                                else
                                {
                                    tmSvr.UpdateTimeCardManual(rq);
                                }

                               
                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show("ไม่สามารถบันทึกข้อมูล Code =" + rq.EmpCode + "ได้ เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                continue;
                            }
                        }


                        this.RefreshData();
                    }
                    else
                    {
                        MessageBox.Show("ไม่สามารถบันทึกข้อมูล ได้เนื่องจาก คุณไม่มีสิทธิเข้าถึง", "Access Denie", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                }

            }
            this.Cursor = Cursors.Default;
           
        }

        public void Delete()
        {

            this.Cursor = Cursors.WaitCursor;
            ObjectInfo inform;
            if (ucl_ActionControl1.Permission.AllowDelete)
            {
                if (formAct == FormAction.New)
                {
                    try
                    {
                        if (MessageBox.Show("ต้องการลบข้อมูลใช่หรือไม่?", "Conferm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {

                            foreach (DataGridViewRow var in dgItems.SelectedRows)
                            {
                                TimeCardManualInfo rq = (TimeCardManualInfo)addData[var.Index];

                                addData.Remove(rq);
                                inform = new ObjectInfo();
                                inform.LastUpdateBy = appMgr.UserAccount.AccountId;
                                rq.Inform = inform;
                                try
                                {


                                    if (rq.EmpCode.StartsWith("I"))
                                    {
                                        subTmSvr.DeleteTimeCardManual(rq);
                                    }
                                    else if (rq.EmpCode.StartsWith("7"))
                                    {
                                        tnTmSvr.DeleteTimeCardManual(rq);
                                    }
                                    else
                                    {
                                        tmSvr.DeleteTimeCardManual(rq);
                                    }
                                   
                                }
                                catch (Exception ex)
                                {

                                    MessageBox.Show("ไม่สามารถลบข้อมูล Code =" + rq.EmpCode + "ได้ เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }


                            }
                        }

                    }
                    catch
                    { }
                    gvData = addData;
                    FillDataGrid();
                }
                else
                {
                    try
                    {

                        if (MessageBox.Show("ต้องการลบข้อมูลใช่หรือไม่?", "Conferm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            foreach (DataGridViewRow var in dgItems.SelectedRows)
                            {
                                TimeCardManualInfo rq = (TimeCardManualInfo)searchData[var.Index];
                                inform = new ObjectInfo();
                                inform.LastUpdateBy = appMgr.UserAccount.AccountId;
                                rq.Inform = inform;
                                try
                                {
                                    if (rq.EmpCode.StartsWith("I"))
                                    {
                                        subTmSvr.DeleteTimeCardManual(rq);
                                    }
                                    else if (rq.EmpCode.StartsWith("7"))
                                    {
                                        tnTmSvr.DeleteTimeCardManual(rq);
                                    }
                                    else
                                    {
                                        tmSvr.DeleteTimeCardManual(rq);
                                    }
                                }
                                catch (Exception ex)
                                {

                                    MessageBox.Show("ไม่สามารถลบข้อมูล Code =" + rq.EmpCode + "ได้ เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                }
                            }
                            
                            RefreshData();
             
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("ไม่สามารถลบข้อมูลได้ เนื่องจาก " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("ไม่สามารถลบข้อมูล ได้เนื่องจาก คุณไม่มีสิทธิเข้าถึง", "Access Denie", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            this.Cursor = Cursors.Default;
            
        }

        public void Search()
        {
            this.Cursor = Cursors.WaitCursor;
            if (formAct == FormAction.Save)
            {
                ucl_ActionControl1.CurrentAction = FormActionType.Search;

                if (kryptonTextBox1.Text.StartsWith("I"))
                {
                    searchData = subTmSvr.GetTimeManual(kryptonTextBox1.Text, dpkRqDate.Value.Date, dpkDateTo.Value.Date, cmbType.SelectedValue.ToString());

                }
                else if (kryptonTextBox1.Text.StartsWith("7"))
                {
                    searchData = tnTmSvr.GetTimeManual(kryptonTextBox1.Text, dpkRqDate.Value.Date, dpkDateTo.Value.Date, cmbType.SelectedValue.ToString());

                }
                else
                {
                    searchData = tmSvr.GetTimeManual(kryptonTextBox1.Text, dpkRqDate.Value.Date, dpkDateTo.Value.Date, cmbType.SelectedValue.ToString());

                }
                gvData = searchData;
                FillDataGrid();
            }
            else if (formAct == FormAction.New)
            {

            }
            
            this.Cursor = Cursors.Default;
           
        }

        public void Export()
        {
        
        }

        public void Print()
        {
       

          
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {

                FontFamily fm = new FontFamily("Microsoft Sans Serif");
                Font ft = new Font(fm, 15.0f);
                string header;
                if (formAct == FormAction.Save)
                {
                    header = "Time Manual Report " + dpkRqDate.Value.ToString("dd/MM/yyyy") + "-" + dpkDateTo.Value.ToString("dd/MM/yyyy");


                    MyDataGridViewPrinter = new DataGridViewPrinter(dgItems, printDocument1, true, true, header, ft
                            , Color.Black, true);
                    printDocument1.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 1);
                    printDocument1.Print();
                }
                else
                {
                //    header = "Time Manual Report" + txtRqdate.Text;
                }
            }
        }

        public void Open()
        {
            timeCardManual_Control1.Open();
            timeCardManual_Control2.Open();


            searchData = new ArrayList();
            addData = new ArrayList();
            addData.Add(new TimeCardManualInfo());
            gvData = addData;
            FillDataGrid();
            AddGridViewColumns();
            addData.Clear();

            kryptonHeaderGroup1_Click(kryptonHeaderGroup1, new EventArgs());

            ArrayList tmrqType = tmSvr.GetTimeCardManualType();
            BasicInfo objAll = new BasicInfo("%", "All", "");
            tmrqType.Insert(0, objAll);
            cmbType.DisplayMember = "NameForSearching";
            cmbType.ValueMember = "Code";
            cmbType.DataSource = tmrqType;
            cmbType.SelectedIndex = 0;

        }

        public void Clear()
        {
      
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
                dgItems.CurrentCell = dgItems[1, selrow];
            }
            catch
            { }
        }

        public void Exit()
        {

            this.Close();
        }

        #endregion

        #region IFormPermission Members

        public DCI.Security.Model.PermissionInfo Permission
        {
            set { ucl_ActionControl1.Permission=value; }
        }

        #endregion

        private void timeCardManual_Control1_Load(object sender, EventArgs e)
        {
            timeCardManual_Control1.tmcSrv = TimeCardService.Instance();
            timeCardManual_Control2.tmcSrv = TimeCardService.Instance();
        }

        private void timeCardManual_Control1_enterData()
        {

        }




        private void timeCardManual_Control1_enterCode()
        {
            this.AddNew();

        }

       


        private void kryptonHeaderGroup1_Click(object sender, EventArgs e)
        {
            ucl_ActionControl1.CurrentAction = FormActionType.None;
            if (sender == kryptonHeaderGroup1)
            {
                kryptonHeaderGroup3.Collapsed = true;
                kryptonHeaderGroup1.Collapsed = false;
                ucl_ActionControl1.CurrentAction = FormActionType.None;          
                formAct = FormAction.New;
                gvData = addData;
                FillDataGrid();
          
            }
            else
            {                
                formAct = FormAction.Save;
                kryptonHeaderGroup1.Collapsed = true;
                kryptonHeaderGroup3.Collapsed = false;
                gvData = searchData;
                FillDataGrid();           
            }

        }

        private void timeCardManual_Control2_enterData()
        {
            this.Save();
        }

        private void dgItems_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            try
            {
                if (formAct == FormAction.New)
                {
                    if (dgItems.CurrentRow.Index == 0 && dgItems.Rows.Count > 0)
                    {
                        dgItems.CurrentCell = dgItems[1, dgItems.Rows.Count - 1];
                    }
 
                }
            }
            catch
            {
            }
        }

        private void dgItems_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyleDefault.ShowRowNumber(dgItems, e);
        }

        private void dgItems_SelectionChanged_1(object sender, EventArgs e)
        {
            try
            {
                int select = dgItems.SelectedRows[0].Index;
   

                ucl_ActionControl1.CurrentAction = FormActionType.Save;
                if (formAct == FormAction.Save)
                {
                    TimeCardManualInfo item = (TimeCardManualInfo)searchData[select];
                    if (item.EmpCode.StartsWith("I"))
                    {
                        empData_Control1.Information = subsrv.Find(item.EmpCode);       
                    }
                    else if (item.EmpCode.StartsWith("7"))
                    {
                        empData_Control1.Information = tnsrv.Find(item.EmpCode);   
                    }
                    else
                    {
                        empData_Control1.Information = empsrv.Find(item.EmpCode);   
                    }
                    timeCardManual_Control2.Information = item;
                    this.Information = item;

                }
                else if (formAct == FormAction.New)
                {
                    TimeCardManualInfo item = (TimeCardManualInfo)addData[select];
                    if (item.EmpCode.StartsWith("I"))
                    {
                        empData_Control1.Information = subsrv.Find(item.EmpCode);
                    }
                    else if (item.EmpCode.StartsWith("7"))
                    {
                        empData_Control1.Information = tnsrv.Find(item.EmpCode);
                    }
                    else
                    {
                        empData_Control1.Information = empsrv.Find(item.EmpCode);
                    }
                    this.Information = item;
                }

            }
            catch
            {
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.Search();
        }

        private void dpkRqDate_ValueChanged(object sender, EventArgs e)
        {
            if (sender == dpkRqDate)
            {
                if (dpkRqDate.Value > dpkDateTo.Value)
                    dpkDateTo.Value = dpkRqDate.Value;
            }
            else
            {
                if (dpkRqDate.Value > dpkDateTo.Value)
                    dpkRqDate.Value = dpkDateTo.Value;
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            bool more = MyDataGridViewPrinter.DrawDataGridView(e.Graphics);
            if (more == true)
                e.HasMorePages = true;

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            addData.Clear();
            gvData = addData;
            FillDataGrid();
        }
    }
}