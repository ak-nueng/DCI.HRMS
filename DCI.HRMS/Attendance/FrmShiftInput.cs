using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Base;
using DCI.HRMS.Model;
using DCI.HRMS.Service;
using DCI.HRMS.Util;
using DCI.HRMS.Model.Common;

using DCI.HRMS.Common;
using DCI.HRMS.Model.Attendance;
using DCI.HRMS.Model.Personal;
using DCI.HRMS.Service.SubContract;
using DCI.HRMS.Service.Trainee;
using System.Data.SqlClient;


namespace DCI.HRMS.Attendance
{
    public partial class FrmShiftInput : Form, IFormParent, IFormPermission
    {
        #region Field
        private readonly string[] colName = new string[] { "YearMonth", "Employee Code", "ShiftData", "ShiftStatus", "AddBy", "AddDate", "UpdateBy", "UpdateDate" };
        private readonly string[] propName = new string[] { "YearMonth", "EmpCode", "ShiftData", "ShiftO", "CreateBy", "CreateDateTime", "LastUpdateBy", "LastUpDateDateTime" };
        private readonly int[] width = new int[] { 80, 120, 300, 200, 100, 120, 100, 120 };

        private ShiftService shSvr = ShiftService.Instance();

        private SubContractShiftService subShSvr = SubContractShiftService.Instance();
        private TraineeShiftService tnShSvr = TraineeShiftService.Instance();

        private EmployeeShiftInfo shIfo = new EmployeeShiftInfo();
        private EmployeeShiftInfo searchInfo = new EmployeeShiftInfo();
        private EmployeeInfo empInfo = new EmployeeInfo();
        private bool manualAdd = false;

        private ArrayList shiftTypeData;
        private ArrayList gvData;
        private ArrayList gvDataTemp;
        private ArrayList manualData = new ArrayList();
        private ArrayList searchData = new ArrayList();
        private StatusManager stsMng = new StatusManager();
        private FormAction formAct = FormAction.New;

        #endregion
        #region Contructor
        public FrmShiftInput()
        {
            stsMng.Status="Loading  FrmShiftInput";
            InitializeComponent();
        }
        #endregion
        #region Event
        private void FrmShiftInput_Load(object sender, EventArgs e)
        {

            ucl_ActionControl1.Owner = this;
            ucl_ActionControl1.CurrentAction = FormActionType.AddNew;
            AddGridViewColumns();
            shiftTypeData = ShiftService.Instance().GetShiftType();
            monthShift_Control1.ShtType = shiftTypeData;
            kryptonHeaderGroup1_Click(kryptonHeaderGroup1, new EventArgs());
           stsMng.Status="Ready";
          //  cmbShStatus.SelectedIndex = 0;


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            stsMng.Status="Searching";
            this.Search();
           stsMng.Status="Ready";

        }
        private void FrmShiftInput_KeyDown(object sender, KeyEventArgs e)
        {


            ucl_ActionControl1.OnActionKeyDown(sender, e);
        }

        private void dgItems_SelectionChanged(object sender, EventArgs e)
        {
            if (formAct== FormAction.New)
            {
                try
                {
                    EmployeeShiftInfo item = (EmployeeShiftInfo)manualData[dgItems.SelectedRows[0].Index];
          
                    ucl_ActionControl1.CurrentAction = FormActionType.Save;
               //     kryptonHeaderGroup1_Click(kryptonHeaderGroup2, new EventArgs());
                    SetEmpData(item.EmpCode);
                    this.Information = item;

                }
                catch
                {

                }
            }
            else if (formAct== FormAction.Save)
            {
                EmployeeShiftInfo item = new EmployeeShiftInfo();
                try
                {
                    item = (EmployeeShiftInfo)searchData[dgItems.SelectedRows[0].Index];
                    ucl_ActionControl1.CurrentAction = FormActionType.Save;
      
                    SetEmpData(item.EmpCode);
                    this.Information = item;
                
                }
                catch
                {

                }
            }
        }


        private void empShift_Control1_year_Changed()
        {
            dgItems.ClearSelection();
            ucl_ActionControl1.CurrentAction = FormActionType.AddNew;

        }

        private void monthShift_Control1_grp_Changed()
        {
            fillMonthData();

            
        }
        private void kryptonHeaderGroup1_Click(object sender, EventArgs e)
        {
            if (sender == kryptonHeaderGroup1)
            {
                kryptonHeaderGroup2.Collapsed = true;
                kryptonHeaderGroup1.Collapsed = false;
                ucl_ActionControl1.CurrentAction = FormActionType.None;
                formAct = FormAction.New;
                if (manualData.Count!=0)
                {
                    gvData = manualData;
                    FillDataGrid();
                }
                else
                {
                    gvData = new ArrayList();
                    FillDataGrid();
                }

             
            }
            else
            {
        
                kryptonHeaderGroup1.Collapsed = true;
                kryptonHeaderGroup2.Collapsed = false;
                formAct = FormAction.Save;
                gvData = searchData;
                FillDataGrid();
            }

        }
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            MonthShiftInfo item = (MonthShiftInfo)monthShift_Control1.Information;
            DialogResult res;
            if (item.GroupStatus != "D1")
            {
                res = MessageBox.Show(string.Format("This will generate Shift data of Group:{0} Month:{1} . \n Do you want to continue?", item.GroupStatus, item.YearMonth), "Conferm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            }
            else
            {
                res = MessageBox.Show("This will generate Normal Shift data for all employees. \n Do you want to continue?", "Conferm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            }
            if (res == DialogResult.Yes)
            {
                this.Cursor = Cursors.WaitCursor;
                stsMng.Status = string.Format("Generating Shift Data of Month:{0} Group: {1}", item.YearMonth, item.GroupStatus);
                manualData = new ArrayList();
                StringBuilder shto = new StringBuilder(item.GroupStatus.Substring(1, 1));
                shto.Append(Convert.ToChar(item.GroupStatus.Substring(1, 1)), item.ShiftData.Length - 1);
                if (chkEmp.Checked)
                {
                         manualData = shSvr.GenerateEmpShiftData(item, shto.ToString());
                }
                
                if (chkSub.Checked)
                {
                    ArrayList subgenSh = subShSvr.GenerateEmpShiftData(item, shto.ToString());
                    if (subgenSh != null)
                    {
                        if (manualData == null)
                        {
                            manualData = new ArrayList();
                        }
                        manualData.AddRange(subgenSh);
                    } 
                }

                if (chkTr.Checked)
                {
                    ArrayList tngensh = tnShSvr.GenerateEmpShiftData(item, shto.ToString());
                    if (tngensh != null)
                    {
                        if (manualData == null)
                        {
                            manualData = new ArrayList();
                        }
                        manualData.AddRange(tngensh);
                    } 
                }
                gvData = manualData;
                FillDataGrid();
                stsMng.Status = "Ready";
                this.Cursor = Cursors.Default;


            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

            manualAdd = false;
            ClearDataGride();

            manualData = gvData;
            dgItems.AllowUserToDeleteRows = false;
            dgItems.MultiSelect = false;
            delectSelectedRowsToolStripMenuItem.Enabled = false;
            ucl_ActionControl1.CurrentAction = FormActionType.None;

        }
        private void delectSelectedRowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Delete();   
        }
        private void empShift_Control1_txtCode_Enter()
        {
            btnSearch_Click(this.empShift_Control1, new EventArgs());

        }

        private void txbCodeManual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txbCodeManual.Text!="")
            {
                this.Save();
                txbCodeManual.Clear();
                txbCodeManual.Focus();
                
            }

        }
        private void empDetail_Control1_Load(object sender, EventArgs e)
        {
            //empDetail_Control1.empServ = EmployeeService.Instance();
        }
        private void dgItems_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyleDefault.ShowRowNumber((DataGridView) sender, e);
        }
        #endregion
        #region IForm Members

        public string GUID
        {
            get { return string.Empty; }
        }

        public object Information
        {
            get
            {
                return empShift_Control1.Information;


            }
            set
            {
                shIfo = (EmployeeShiftInfo)value;
                empShift_Control1.Information = shIfo;
            }
        }

        public void AddNew()
        {
            ucl_ActionControl1.CurrentAction = FormActionType.AddNew;


        }

        public void Save()
        {
            if (formAct == FormAction.New)
            {
                string code = txbCodeManual.Text;
                EmployeeInfo emit = getEmployeeData(code);



                ObjectInfo objinfo = new ObjectInfo();

                if (emit != null)
                {


                    ShiftType emshty = new ShiftType();
                    emshty = emshty.GetShiftTypeByOt(emit.OtGroupLine, shiftTypeData);
                    try
                    {
                        // cmbShStatus.SelectedItem = emshty.ShiftStatus;
                    }
                    catch
                    {
                        // MessageBox.Show(string.Format("ไม่พบข้อมูล OT Group ของ {0}", emit.Code), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }

                    EmployeeShiftInfo shma = new EmployeeShiftInfo();
                    MonthShiftInfo mshma = new MonthShiftInfo();

                    mshma = (MonthShiftInfo)monthShift_Control1.Information;

                    shma.EmpCode = code;
                    shma.ShiftData = mshma.ShiftData;
                    shma.YearMonth = mshma.YearMonth;
                    string grp = mshma.GroupStatus.Substring(1, 1);
                    StringBuilder shto = new StringBuilder(grp);
                    shto.Append(Convert.ToChar(grp), shma.ShiftData.Length - 1);
                    shma.ShiftO = shto.ToString();


                    try
                    {



                        if (code.StartsWith("I"))
                        {
                            if (!subShSvr.CheckExited(shma))
                            {
                                objinfo.CreateBy = ApplicationManager.Instance().UserAccount.AccountId;
                                objinfo.CreateDateTime = DateTime.Now;
                                shma.Inform = objinfo;
                                subShSvr.New(shma);
                            }
                            else
                            {
                                string msg = string.Format("มีข้อมูลข้อมูลตารางกะของเดือน:{0} รหัส:{1} อยู่แล้ว ต้องการ update หรือไม่?", shIfo.YearMonth, shIfo.EmpCode);
                                DialogResult result = MessageBox.Show(this, msg, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                                if (result == DialogResult.Yes)
                                {
                                    objinfo.LastUpdateBy = ApplicationManager.Instance().UserAccount.AccountId;
                                    objinfo.LastUpdateDateTime = DateTime.Now;
                                    shma.Inform = objinfo;
                                    subShSvr.Save(shma);
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                        else if (code.StartsWith("7"))
                        {
                            if (!tnShSvr.CheckExited(shma))
                            {
                                objinfo.CreateBy = ApplicationManager.Instance().UserAccount.AccountId;
                                objinfo.CreateDateTime = DateTime.Now;
                                shma.Inform = objinfo;
                                tnShSvr.New(shma);
                            }
                            else
                            {
                                string msg = string.Format("มีข้อมูลข้อมูลตารางกะของเดือน:{0} รหัส:{1} อยู่แล้ว ต้องการ update หรือไม่?", shIfo.YearMonth, shIfo.EmpCode);
                                DialogResult result = MessageBox.Show(this, msg, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                                if (result == DialogResult.Yes)
                                {
                                    objinfo.LastUpdateBy = ApplicationManager.Instance().UserAccount.AccountId;
                                    objinfo.LastUpdateDateTime = DateTime.Now;
                                    shma.Inform = objinfo;
                                    tnShSvr.Save(shma);
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (!shSvr.CheckExited(shma))
                            {
                                objinfo.CreateBy = ApplicationManager.Instance().UserAccount.AccountId;
                                objinfo.CreateDateTime = DateTime.Now;
                                shma.Inform = objinfo;
                                shSvr.New(shma);
                            }
                            else
                            {
                                string msg = string.Format("มีข้อมูลข้อมูลตารางกะของเดือน:{0} รหัส:{1} อยู่แล้ว ต้องการ update หรือไม่?", shIfo.YearMonth, shIfo.EmpCode);
                                DialogResult result = MessageBox.Show(this, msg, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                                if (result == DialogResult.Yes)
                                {
                                    objinfo.LastUpdateBy = ApplicationManager.Instance().UserAccount.AccountId;
                                    objinfo.LastUpdateDateTime = DateTime.Now;
                                    shma.Inform = objinfo;
                                    shSvr.Save(shma);
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }


                        //---- Shift SQL ----
                        InsertEMCL(shma);


                        manualData.Add(shma);
                        gvData = manualData;
                        FillDataGrid();
                        if (dgItems.Rows.Count > 0)
                        {
                            dgItems.CurrentCell = dgItems[0, dgItems.Rows.Count - 1];
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else
                {
                    MessageBox.Show(string.Format("ไม่พบข้อมูลพนักงาน รหัส:{0}", code), "No Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (formAct == FormAction.Save)
            {
                shIfo = (EmployeeShiftInfo)Information;
                ObjectInfo objinfo = new ObjectInfo();
                try
                {
                    objinfo.LastUpdateBy = ApplicationManager.Instance().UserAccount.AccountId;
                    objinfo.LastUpdateDateTime = DateTime.Now;
                    shIfo.Inform = objinfo;

                    if (shIfo.EmpCode.StartsWith("I"))
                    {
                        subShSvr.Save(shIfo);

                    }
                    else if (shIfo.EmpCode.StartsWith("7"))
                    {
                        tnShSvr.Save(shIfo);
                    }
                    else
                    {
                        shSvr.Save(shIfo);
                    }


                    //---- Shift SQL ----
                    InsertEMCL(shIfo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                RefreshData();
                empShift_Control1.Focus();
            }
        }

        public void Delete()
        {
            if (dgItems.SelectedRows.Count > 0)
            {
                shIfo = (EmployeeShiftInfo)Information;
                string msg = string.Format("คุณต้องการลบข้อมูล ตารางกะ เดือน: {0} รหัส: {1} ใช่หรือไม่?", shIfo.YearMonth, shIfo.EmpCode);
                DialogResult result = MessageBox.Show(this, msg, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        if (shIfo.EmpCode.StartsWith("I"))
                        {
                            subShSvr.Delete(shIfo);

                        }
                        else if (shIfo.EmpCode.StartsWith("7"))
                        {
                            tnShSvr.Delete(shIfo);
                        }
                        else
                        {
                            shSvr.Delete(shIfo);
                        }

                        //---- Delete Shfit ----
                        DeleteEMCL(shIfo);
                       

                        ucl_ActionControl1.CurrentAction = FormActionType.AddNew;
                        if (formAct == FormAction.New)
                        {
                            int i = 0;

                            foreach (EmployeeShiftInfo var in manualData)
                            {

                                if (var.EmpCode == shIfo.EmpCode && var.YearMonth == shIfo.YearMonth)
                                {
                                    break;

                                }
                                i++;

                            }
                            manualData.RemoveAt(i);

                        }

                        RefreshData();
                        //  MessageBox.Show(this, string.Format("ลบข้อมูล ตารางกะ เดือน: {0} รหัส: {1} เรียบร้อยแล้ว", shIfo.YearMonth, shIfo.EmpCode), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void Search()
        {
            this.Cursor = Cursors.WaitCursor;
            ucl_ActionControl1.CurrentAction = FormActionType.None;

            if (formAct == FormAction.Save)
            {
                searchInfo = (EmployeeShiftInfo)Information;
                if (searchInfo.EmpCode != "")
                {
                    if (searchInfo.EmpCode.StartsWith("I"))
                    {
                        searchData = subShSvr.GetShiftByCode(searchInfo.EmpCode, int.Parse(searchInfo.YearMonth.Substring(0, 4)));

                    }
                    else if (searchInfo.EmpCode.StartsWith("7"))
                    {
                        searchData = tnShSvr.GetShiftByCode(searchInfo.EmpCode, int.Parse(searchInfo.YearMonth.Substring(0, 4)));


                    }
                    else
                    {
                        searchData = shSvr.GetShiftByCode(searchInfo.EmpCode, int.Parse(searchInfo.YearMonth.Substring(0, 4)));

                    }
                    gvData = searchData;
                    FillDataGrid();
                }
                else
                {
                    MessageBox.Show("กรุณาป้อนรหัสพนักงาน", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }





            }
            else if (formAct == FormAction.New)
            {
                gvData = manualData;
                FillDataGrid();
            }
            this.Cursor = Cursors.Default;
        }

        public void Export()
        {
        }

        public void Print()
        {
        }

        public void Open()
        {
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
        #region IFormPermission Members

        public DCI.Security.Model.PermissionInfo Permission
        {
            set
            {
                ucl_ActionControl1.Permission = value;
            }
        }




        #endregion
        #region Medthod

        private EmployeeInfo getEmployeeData(string code)
        {



            if (code.StartsWith("I"))
            {
                EmployeeInfo emitem = SubContractService.Instance().Find(code);
                return emitem;
            }
            else if (code.StartsWith("7"))
            {
                EmployeeInfo emitem = TraineeService.Instance().Find(code);
                return emitem;
            }
            else
            {
                EmployeeInfo emitem = EmployeeService.Instance().Find(code);
                return emitem;
            }
        }
        private void SetEmpData(string item)
        {/*
            empInfo = getEmployeeData(item);
            this.empCode.Text = this.empInfo.Code;
            this.empName.Text = string.Format("{0} {1}  {2}", this.empInfo.NameInEng.Title, this.empInfo.NameInEng.Name, this.empInfo.NameInEng.Surname);
            this.empPosit.Text = this.empInfo.Position.NameEng;
            this.empDivision.Text = this.empInfo.Division.Name;
            this.empGrpOt.Text = this.empInfo.OtGroupLine;
            ShiftType shitem = new ShiftType();



            shitem = shitem.GetShiftTypeByOt(empInfo.OtGroupLine, shiftTypeData);

            try
            {
                this.empShGrp.Text = shitem.ShiftGroup;
                this.empShsts.Text = shitem.ShiftStatus;
            }
            catch
            {
                this.empShGrp.Text = "";
                this.empShsts.Text = "";

            }*/
            empInfo = getEmployeeData(item);
            empData_Control1.Information = empInfo;
            try
            {
                ShiftType shitem = new ShiftType();
                shitem = shitem.GetShiftTypeByOt(empInfo.OtGroupLine, shiftTypeData);
                textBox1.Text = shitem.ShiftGroup + shitem.ShiftStatus;
            }
            catch
            {
                textBox1.Text = "";
            }

        }
        private void fillMonthData()
        {
            MonthShiftInfo msh = (MonthShiftInfo)monthShift_Control1.Information;
            MonthShiftInfo sdt = shSvr.GetShift(msh.GroupStatus, msh.YearMonth);
            if (sdt != null)
            {
                monthShift_Control1.Information = sdt;
                btnGenerate.Enabled = true;
            }
            else
            {
                btnGenerate.Enabled = false;
            }

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
            // dgItems.Focus();
            this.Update();

        }
        private void ClearDataGride()
        {
            gvData = shSvr.GetShiftByCode("0", 1000);

            dgItems.DataSource = gvData;
            dgItems.Refresh();

        }

        #endregion

        private void dgItems_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (formAct == FormAction.New)
                {
                   // if (dgItems.CurrentRow == null)
                //   {
                  //      dgItems.Focus();
                   // }
                    if (dgItems.CurrentRow.Index==0 && dgItems.Rows.Count>0)
                    {
                        dgItems.CurrentCell = dgItems[0, dgItems.Rows.Count - 1];
                    }
          
                }
            }
            catch
            {
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            manualData.Clear();
            gvData = manualData;
            FillDataGrid();
        }



        SqlConnectDB oSqlHRM = new SqlConnectDB("dbHRM");
        SqlConnectDB oSqlDCI = new SqlConnectDB("dbDCI");
        private void InsertEMCL(EmployeeShiftInfo EmpShift)
        {
            DataTable dtCheck = new DataTable();
            string strCheck = @"SELECT CODE FROM EMCL WHERE CODE=@CODE AND YM=@YM ";
            SqlCommand cmdCheck = new SqlCommand();
            cmdCheck.CommandText = strCheck;
            cmdCheck.Parameters.Add(new SqlParameter("@CODE", EmpShift.EmpCode));
            cmdCheck.Parameters.Add(new SqlParameter("@YM", EmpShift.YearMonth));            
            dtCheck = oSqlHRM.Query(cmdCheck);

            if (dtCheck.Rows.Count == 0)
            {

                //***********************************************************
                //                  INSERT EMCL
                //***********************************************************
                string strInstr = @"INSERT INTO (YM,CODE,STSS,STSO,CR_BY,CR_DT,UPD_BY,UPD_DT) VALUES (@YM,@CODE,@STSS,@STSO,@CR_BY,@CR_DT,@UPD_BY,@UPD_DT)";
                SqlCommand cmdInstr = new SqlCommand();
                cmdInstr.CommandText = strInstr;
                cmdInstr.Parameters.Add(new SqlParameter("@YM", EmpShift.YearMonth));
                cmdInstr.Parameters.Add(new SqlParameter("@CODE", EmpShift.EmpCode));
                cmdInstr.Parameters.Add(new SqlParameter("@STSS", EmpShift.ShiftData));
                cmdInstr.Parameters.Add(new SqlParameter("@STSO", EmpShift.ShiftO));
                cmdInstr.Parameters.Add(new SqlParameter("@CR_BY", EmpShift.LastUpdateBy));
                cmdInstr.Parameters.Add(new SqlParameter("@CR_DT", DateTime.Now.ToString("yyyy-MM-dd")));
                cmdInstr.Parameters.Add(new SqlParameter("@UPD_BY", EmpShift.LastUpdateBy));
                cmdInstr.Parameters.Add(new SqlParameter("@UPD_DT", DateTime.Now.ToString("yyyy-MM-dd")));
                oSqlHRM.ExecuteCommand(cmdInstr);

            }
            else
            {

                string strUpd = @"UPDATE EMCL SET STSS=@STSS, STSO=@STSO, UPD_BY=@UPD_BY, UPD_DT=@UPD_DT WHERE YM=@YM AND CODE=@CODE ";
                SqlCommand cmdUpd = new SqlCommand();
                cmdUpd.CommandText = strUpd;
                cmdUpd.Parameters.Add(new SqlParameter("@STSS", EmpShift.ShiftData));
                cmdUpd.Parameters.Add(new SqlParameter("@STSO", EmpShift.ShiftO));
                cmdUpd.Parameters.Add(new SqlParameter("@UPD_BY", EmpShift.LastUpdateBy));
                cmdUpd.Parameters.Add(new SqlParameter("@UPD_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                cmdUpd.Parameters.Add(new SqlParameter("@YM", EmpShift.YearMonth));
                cmdUpd.Parameters.Add(new SqlParameter("@CODE", EmpShift.EmpCode));
                oSqlHRM.ExecuteCommand(cmdUpd);
            }


            //**************************************
            //         Check & Change Shift
            //**************************************
            #region Check & Change Shift
            int _Year = Convert.ToInt16(EmpShift.YearMonth.Substring(0, 4));
            int _Month = Convert.ToInt16(EmpShift.YearMonth.Substring(4, 2));
            int _DayInMonth = DateTime.DaysInMonth(_Year, _Month);
            DateTime _STDate = new DateTime(_Year, _Month, 1);
            DateTime _ENDate = new DateTime(_Year, _Month, _DayInMonth);
            string RQ = "";
            DateTime loopDate = _STDate;
            while (loopDate <= _ENDate)
            {
                RQ += "'" + loopDate.Day.ToString() + "','" + loopDate.Day.ToString() + "0',";
                loopDate = loopDate.AddDays(1);
            }
            RQ = " AND RQ IN (" + RQ.Substring(0,RQ.Length-1) + ")";

            DataTable dtOT = new DataTable();
            string strOT = @"SELECT * FROM OTRQ_REQ 
                              WHERE CODE=@CODE AND ODATE BETWEEN @STDATE AND @ENDATE 
                                AND ProgBit='U' " + RQ;
            SqlCommand cmdOT = new SqlCommand();
            cmdOT.CommandText = strOT;
            cmdOT.Parameters.Add(new SqlParameter("@CODE", EmpShift.EmpCode));
            cmdOT.Parameters.Add(new SqlParameter("@STDATE", _STDate.ToString("yyyy-MM-dd")));
            cmdOT.Parameters.Add(new SqlParameter("@ENDATE", _ENDate.AddDays(1).ToString("yyyy-MM-dd")));
            dtOT = oSqlHRM.Query(cmdOT);

            if (dtOT.Rows.Count > 0)
            {
                DataTable dtEmpShift = new DataTable();
                string strEmpShift = @"SELECT CODE, YM, STSS AS shdata FROM EMCL WHERE CODE=@CODE AND (YM=@YM OR YM=@YM2)  ";
                SqlCommand cmdEmpShift = new SqlCommand();
                cmdEmpShift.CommandText = strEmpShift;
                cmdEmpShift.Parameters.Add(new SqlParameter("@CODE", EmpShift.EmpCode));
                cmdEmpShift.Parameters.Add(new SqlParameter("@YM", _STDate.ToString("yyyyMM")));
                cmdEmpShift.Parameters.Add(new SqlParameter("@YM2", _STDate.AddMonths(-1).ToString("yyyyMM")));
                dtEmpShift = oSqlHRM.Query(cmdEmpShift);

                foreach (DataRow drOT in dtOT.Rows)
                {
                    DateTime OTDate = new DateTime(1900,1,1);
                    DateTime ODate = new DateTime(1900,1,1);
                    if (drOT["OTFROM"].ToString() == "06:05" && drOT["OTTO"].ToString() == "07:50")
                    {
                        try
                        {
                            ODate = Convert.ToDateTime(drOT["ODATE"].ToString());
                            OTDate = Convert.ToDateTime(drOT["ODATE"].ToString());
                            OTDate = OTDate.AddDays(-1);
                        }
                        catch { }
                    }
                    else {
                        try
                        {
                            ODate = Convert.ToDateTime(drOT["ODATE"].ToString());
                            OTDate = Convert.ToDateTime(drOT["ODATE"].ToString());
                        }
                        catch { }
                    }

                    string GetShift = GetEmployeeShiftData(dtEmpShift, EmpShift.EmpCode, OTDate);
                    if (GetShift == "D" || GetShift == "N")
                    {
                        if (GetShift != drOT["SHIFT"].ToString())
                        {
                            string _otfrom = "", _otto="", _ot1from="", _ot1to="",_ot15from="", _ot15to="",_ot2from="", _ot2to="",_ot3from="", _ot3to="";
                            if (GetShift == "N")
                            {
                                _otfrom = "06:05";
                                _otto="07:50";
                                _ot1from="";
                                _ot1to="";
                                _ot15from="06:05";
                                _ot15to="07:50";
                                _ot2from="";
                                _ot2to="";
                                _ot3from=""; 
                                _ot3to="";
                            }else { 
                                _otfrom = "18:15";
                                _otto="20:00";
                                _ot1from="";
                                _ot1to="";
                                _ot15from="18:15";
                                _ot15to="20:00";
                                _ot2from="";
                                _ot2to="";
                                _ot3from=""; 
                                _ot3to="";
                            }

                            string strUpd = @"UPDATE OTRQ_REQ SET ODATE=@ODATEN, RQ=@RQN, OTFROM=@OTFROM, OTTO=@OTTO, OT1FROM=@OT1FROM, OT1TO=@OT1TO, 
		                                    OT15FROM=@OT15FROM, OT15TO=@OT15TO, OT2FROM=@OT2FROM, OT2TO=@OT2TO, 
		                                    OT3FROM=@OT3FROM, OT3TO=@OT3TO, SHIFT=@SHIFTN
                                    WHERE ODATE=@ODATE AND RQ=@RQ AND CODE=@CODE AND  SHIFT=@SHIFT ";
                            SqlCommand cmdUpd = new SqlCommand();
                            cmdUpd.CommandText = strUpd;
                            cmdUpd.Parameters.Add(new SqlParameter("@ODATEN", (GetShift=="N")? OTDate.AddDays(1).ToString("yyyy-MM-dd"):OTDate.ToString("yyyy-MM-dd") ));
                            cmdUpd.Parameters.Add(new SqlParameter("@RQN", (GetShift=="N")? OTDate.Day.ToString()+"0": OTDate.Day.ToString()));
                            cmdUpd.Parameters.Add(new SqlParameter("@OTFROM", _otfrom));
                            cmdUpd.Parameters.Add(new SqlParameter("@OTTO", _otto));
                            cmdUpd.Parameters.Add(new SqlParameter("@OT1FROM", _ot1from));
                            cmdUpd.Parameters.Add(new SqlParameter("@OT1TO", _ot1to));
                            cmdUpd.Parameters.Add(new SqlParameter("@OT15FROM", _ot15from));
                            cmdUpd.Parameters.Add(new SqlParameter("@OT15TO", _ot15to));
                            cmdUpd.Parameters.Add(new SqlParameter("@OT2FROM", _ot2from));
                            cmdUpd.Parameters.Add(new SqlParameter("@OT2TO", _ot2to));
                            cmdUpd.Parameters.Add(new SqlParameter("@OT3FROM", _ot3from));
                            cmdUpd.Parameters.Add(new SqlParameter("@OT3TO", _ot3to));
                            cmdUpd.Parameters.Add(new SqlParameter("@SHIFTN", GetShift));
                            cmdUpd.Parameters.Add(new SqlParameter("@ODATE", ODate.ToString("yyyy-MM-dd")));
                            cmdUpd.Parameters.Add(new SqlParameter("@RQ", drOT["RQ"].ToString()));
                            cmdUpd.Parameters.Add(new SqlParameter("@CODE", drOT["CODE"].ToString()));
                            cmdUpd.Parameters.Add(new SqlParameter("@SHIFT", drOT["SHIFT"].ToString()));
                            oSqlHRM.ExecuteCommand(cmdUpd);
                        }
                    }else {

                        string HolidayShift = GetEmployeeHolidayShiftData(dtEmpShift, EmpShift.EmpCode, OTDate);
                        if ("H" + HolidayShift != drOT["SHIFT"].ToString())
                        {
                            string _otfrom = "", _otto = "", _ot1from = "", _ot1to = "", _ot15from = "", _ot15to = "", _ot2from = "", _ot2to = "", _ot3from = "", _ot3to = "";
                            if (HolidayShift == "N")
                            {
                                _otfrom = "20:00";
                                _otto = "05:35";
                                if (drOT["WTYPE"].ToString() == "S"){
                                    _ot1from = "20:00";
                                    _ot1to = "05:35";
                                    _ot2from = "";
                                    _ot2to = "";
                                }else {
                                    _ot1from = "";
                                    _ot1to = "";
                                    _ot2from = "20:00";
                                    _ot2to = "05:35";
                                }
                                _ot15from = "";
                                _ot15to = "";
                                _ot3from = "";
                                _ot3to = "";
                            }
                            else
                            {
                                _otfrom = "08:00";
                                _otto = "17:45";
                                if (drOT["WTYPE"].ToString() == "S")
                                {
                                    _ot1from = "08:00";
                                    _ot1to = "17:45";
                                    _ot2from = "";
                                    _ot2to = "";
                                }
                                else
                                {
                                    _ot1from = "";
                                    _ot1to = "";
                                    _ot2from = "08:00";
                                    _ot2to = "17:45";
                                }
                                _ot15from = "";
                                _ot15to = "";
                                _ot3from = "";
                                _ot3to = "";
                            }

                            string strUpd = @"UPDATE OTRQ_REQ SET ODATE=@ODATEN, RQ=@RQN, OTFROM=@OTFROM, OTTO=@OTTO, OT1FROM=@OT1FROM, OT1TO=@OT1TO, 
		                                    OT15FROM=@OT15FROM, OT15TO=@OT15TO, OT2FROM=@OT2FROM, OT2TO=@OT2TO, 
		                                    OT3FROM=@OT3FROM, OT3TO=@OT3TO, SHIFT=@SHIFTN
                                    WHERE ODATE=@ODATE AND RQ=@RQ AND CODE=@CODE AND  SHIFT=@SHIFT ";
                            SqlCommand cmdUpd = new SqlCommand();
                            cmdUpd.CommandText = strUpd;
                            cmdUpd.Parameters.Add(new SqlParameter("@ODATEN", OTDate.ToString("yyyy-MM-dd")));
                            cmdUpd.Parameters.Add(new SqlParameter("@RQN", (HolidayShift == "N") ? OTDate.Day.ToString() + "0" : OTDate.Day.ToString()));
                            cmdUpd.Parameters.Add(new SqlParameter("@OTFROM", _otfrom));
                            cmdUpd.Parameters.Add(new SqlParameter("@OTTO", _otto));
                            cmdUpd.Parameters.Add(new SqlParameter("@OT1FROM", _ot1from));
                            cmdUpd.Parameters.Add(new SqlParameter("@OT1TO", _ot1to));
                            cmdUpd.Parameters.Add(new SqlParameter("@OT15FROM", _ot15from));
                            cmdUpd.Parameters.Add(new SqlParameter("@OT15TO", _ot15to));
                            cmdUpd.Parameters.Add(new SqlParameter("@OT2FROM", _ot2from));
                            cmdUpd.Parameters.Add(new SqlParameter("@OT2TO", _ot2to));
                            cmdUpd.Parameters.Add(new SqlParameter("@OT3FROM", _ot3from));
                            cmdUpd.Parameters.Add(new SqlParameter("@OT3TO", _ot3to));
                            cmdUpd.Parameters.Add(new SqlParameter("@SHIFTN", "H" + HolidayShift));
                            cmdUpd.Parameters.Add(new SqlParameter("@ODATE", ODate.ToString("yyyy-MM-dd")));
                            cmdUpd.Parameters.Add(new SqlParameter("@RQ", drOT["RQ"].ToString()));
                            cmdUpd.Parameters.Add(new SqlParameter("@CODE", drOT["CODE"].ToString()));
                            cmdUpd.Parameters.Add(new SqlParameter("@SHIFT", drOT["SHIFT"].ToString()));
                            oSqlHRM.ExecuteCommand(cmdUpd);
                        }
                    }

                } // end foreach
            } // end if
            #endregion


        }



        private void DeleteEMCL(EmployeeShiftInfo EmpShift)
        {
            DataTable dtCheck = new DataTable();
            string strCheck = @"SELECT CODE FROM EMCL WHERE CODE=@CODE AND YM=@YM ";
            SqlCommand cmdCheck = new SqlCommand();
            cmdCheck.CommandText = strCheck;
            cmdCheck.Parameters.Add(new SqlParameter("@YM", EmpShift.YearMonth));
            cmdCheck.Parameters.Add(new SqlParameter("@CODE", EmpShift.EmpCode));
            dtCheck = oSqlHRM.Query(cmdCheck);

            if (dtCheck.Rows.Count > 0)
            {
                string strUpd = @"UPDATE EMCL SET STSS=@STSS, STSO=@STSO, UPD_BY=@UPD_BY, UPD_DT=@UPD_DT WHERE YM=@YM AND CODE=@CODE ";
                SqlCommand cmdUpd = new SqlCommand();
                cmdUpd.CommandText = strUpd;
                cmdUpd.Parameters.Add(new SqlParameter("@STSS", EmpShift.ShiftData));
                cmdUpd.Parameters.Add(new SqlParameter("@STSO", EmpShift.ShiftO));
                cmdUpd.Parameters.Add(new SqlParameter("@UPD_BY", EmpShift.LastUpdateBy));
                cmdUpd.Parameters.Add(new SqlParameter("@UPD_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                cmdUpd.Parameters.Add(new SqlParameter("@YM", EmpShift.YearMonth));
                cmdUpd.Parameters.Add(new SqlParameter("@CODE", EmpShift.EmpCode));
                oSqlHRM.ExecuteCommand(cmdUpd);
            }

        }


        private string GetEmployeeShiftData(DataTable dtShift, string EmpCode, DateTime ReqDate)
        {
            string strResult = "";

            // check have shift data
            if (dtShift.Rows.Count > 0)
            {

                DataRow[] drShift = dtShift.Select("code = '" + EmpCode + "' AND ym='" + ReqDate.ToString("yyyyMM") + "' ");

                if (drShift.Length > 0)
                {
                    foreach (DataRow drData in drShift)
                    {
                        try
                        {
                            strResult = drData["shdata"].ToString().Substring(ReqDate.Day - 1, 1);
                        }
                        catch { strResult = ""; }
                        break;
                    }
                }
            }// end check

            return strResult;
        }
        

        private string GetEmployeeHolidayShiftData(DataTable dtShift, string EmpCode, DateTime ReqDate)
        {
            string strResult = "";
            // check have shift data
            if (dtShift.Rows.Count > 0)
            {
                try
                {
                    for (int i = 0; i >= -20; i--)
                    {
                        string res = GetEmployeeShiftData(dtShift, EmpCode, ReqDate.AddDays(i));
                        if (res == "D" || res == "N")
                        {
                            strResult = res;
                            break;
                        }
                    } // end for

                }
                catch (Exception ex)
                {
                    strResult = ex.ToString();
                }

            }// end check

            return strResult;
        }



    }
}