using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using DCI.HRMS.Util;
using DCI.HRMS.Base;
using DCI.HRMS.Model.Attendance;
using DCI.HRMS.Service;
using DCI.HRMS.Service.Trainee;
using DCI.HRMS.Service.SubContract;
using DCI.HRMS.Model.Personal;
using DCI.HRMS.Common;
using DCI.HRMS.Model.Common;
using System.Reflection;
using System.Data;

namespace DCI.HRMS.Attendance
{
    public partial class FrmTimeCardTranfer : Form, IFormParent, IFormPermission
    {
        private EmployeeService empSvr = EmployeeService.Instance();  
        private ArrayList gvData = new ArrayList();
        private FormAction formAct = FormAction.New;
        private StatusManager stsMng = new StatusManager();
        private ArrayList empTimeCards = new ArrayList();
        private ArrayList searchData = new ArrayList();
        private ArrayList trnTimeCards = new ArrayList();
        private TimeCardService tmCardServ = TimeCardService.Instance();       
        private EmployeeLeaveService lvrqSvr = EmployeeLeaveService.Instance();
        private TimeCardService tmrqSvr = TimeCardService.Instance();
        private ShiftService shSvr = ShiftService.Instance();
        private BusinessTripService bussSvr = BusinessTripService.Instance();
        ApplicationManager appMgr = ApplicationManager.Instance();
        private StatusManager stsMgr = new StatusManager();
        private DataGridViewPrinter MyDataGridViewPrinter;

        private TraineeService tnSvr = TraineeService.Instance();
        private TraineeShiftService traineeShSvr = TraineeShiftService.Instance();
        private TraineeTimeCardService traineeTmCardSvr = TraineeTimeCardService.Instance();
        private TraineeLeaveService traineeLvrqSvr = TraineeLeaveService.Instance();

        private SubContractService subSvr = SubContractService.Instance();
        private SubContractTimeCardService subContractTmCardSvr = SubContractTimeCardService.Instance();
        private SubContractShiftService subContractShSvr = SubContractShiftService.Instance();
        private SubContractLeaveService subContractLvrqSvr = SubContractLeaveService.Instance();

        public FrmTimeCardTranfer()
        {
            InitializeComponent();

        }

        private void FrmTimeCardTranfer_Load(object sender, EventArgs e)
        {
            ucl_ActionControl1.Owner = this;
            this.Open();


            Type dgvEmpLeaveType = dgvEmpLeave.GetType();
            Type dgvTaffType = dgvTaff.GetType();
            Type dgvEmpTimeType = dgvEmpTime.GetType();
            System.Reflection.PropertyInfo piLeave = dgvEmpLeaveType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            System.Reflection.PropertyInfo piTaff = dgvTaffType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            System.Reflection.PropertyInfo piTime = dgvEmpTimeType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            piLeave.SetValue(dgvEmpLeave, true, null);
            piTaff.SetValue(dgvTaff, true, null);
            piTime.SetValue(dgvEmpTime, true, null);
        }


        /// <summary>
        /// Read Time Card data from file.
        /// </summary>
        private void read_file()
        {

            DateTime day = dateTimePicker1.Value;
            try
            {
                empTimeCards = new ArrayList();
                stsMng.Status = "Openning log file" + "L:/" + day.ToString("yyyyMMdd") + ".LOG";
                StreamReader logReader;


                ListViewItem timeData = new ListViewItem();

                // toolStripProgressBar1.Value = 0;

                string lineData;

                string CardDate;
                string CardTime;
                string[] data = new string[8];

                dgvEmpTime.Rows.Clear();

                string logline;
                string logtime;
                string logstatus;
                string cardcount;
                string taffid;
                dgvTaff.Rows.Clear();

                /*
                try
                {
                    logReader = new StreamReader("L:/" + day.ToString("yyyyMMdd") + ".LOG"); //Set Path and filename.


                    do
                    {
                        logline = logReader.ReadLine();
                        if (logline.Substring(10, 2) == "--")
                        {
                            logtime = logline.Substring(0, 8);
                            char[] split = new char[2];
                            split[0] = '-';
                            split[1] = '!';
                            logstatus = logline.Split(split)[2];
                            split[0] = ':';
                            split[1] = '<';
                            cardcount = logline.Split(split)[3];
                            split[0] = '<';
                            split[1] = '>';
                            taffid = logline.Split(split)[1];
                            string[] logdata = { logtime, taffid, logstatus, cardcount };
                            dataGridView5.Rows.Add(logdata);
                            if (logstatus != " TRANSFER COMPLETED")
                            {
                                dataGridView5.Rows[dataGridView5.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
                                toolStripStatusLabel5.Text = "Taff error please check records";

                                // MessageBox.Show(taffid +" error","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                            }
                            else
                            {
                                toolStripStatusLabel5.Text = "Taff tranfer complete";
                            }


                        }

                    }
                    while (logReader.Peek() != -1);
                }
                catch
                {

                }
                */
                stsMng.Status = "Openning file" + "T:/" + day.ToString("ddMMMyy").ToUpper() + ".TAF";
                StreamReader reader = new StreamReader("T:/" + day.ToString("ddMMMyy").ToUpper() + ".TAF"); //Set Path and filename.
                int line = 1;
                do
                {
                    //Split Line data.
                    try
                    {
                        lineData = reader.ReadLine();
                        stsMng.Status = "Reading data: " + lineData;
                        //data = lineData.Split(' ');
                        //  listBox1.Items.Add(lineData);
                        //data = lineData.Split(' ');
                        data[0] = lineData.Substring(0, 5);
                        data[1] = lineData.Substring(9, 6);
                        data[2] = lineData.Substring(16, 4);
                        data[3] = lineData.Substring(21, 2);
                        data[4] = lineData.Substring(7, 1);
                        data[5] = lineData.Substring(6, 1);
                       // CardDate = data[3].Substring(4, 2) + "/" + data[3].Substring(2, 2) + "/" + data[3].Substring(0, 2);
                        //CardTime = data[4].Substring(0, 2) + ":" + data[4].Substring(2, 2);
                        CardDate = data[1].Substring(4, 2) + "/" + data[1].Substring(2, 2) + "/" + data[1].Substring(0, 2);
                        CardTime = data[2].Substring(0, 2) + ":" + data[2].Substring(2, 2);
                         string[] TimeData = { data[0], data[1], CardDate, CardTime, data[5].Trim(), data[2] };

                        // if (data[0].StartsWith("7"))
                        {
                            //  dataGridView2.Rows.Add(TimeData);
                        }
                        // else
                        {
                            //dataGridView3.Rows.Add(TimeData);
                            TimeCardInfo emtc = new TimeCardInfo();
                            emtc.EmpCode = data[0];
                            try
                            {
                                emtc.Issue = int.Parse(data[5]);
                            }
                            catch
                            {
                                emtc.Issue = 0;
                            }
                            emtc.CardDate = DateTime.Parse(CardDate);
                            emtc.CardTime = CardTime;
                            emtc.CardMachId = int.Parse(data[3]);
                            emtc.Duty = data[4];
                            empTimeCards.Add(emtc);

                            line++;
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Line: "+line.ToString()+" " +ex.ToString());

                    }


                }
                while (reader.Peek() != -1);
                stsMng.Status = "Ready";
                reader.Close();
                ucl_ActionControl1.CurrentAction = FormActionType.SaveAs;
                gvData = empTimeCards;
                FillDataGrid();
                logReader = null;


            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());

            }



        }


        private void btnTransfer_Click(object sender, EventArgs e)
        {
            read_file();
        }

        private void btnTransfer_Click_1(object sender, EventArgs e)
        {
            read_file();
        }

        #region IForm Members

        public string GUID
        {
            get { return null; }
        }

        public object Information
        {
            get
            {
                return null;
            }
            set
            {

            }
        }

        public void AddNew()
        {

        }

        public void Save()
        {
            if (formAct == FormAction.New)
            {
                //  toolStripStatusLabel1.Text = "";
                stsMng.Status = "Insert Employee TimeCard";
                int count = 0;
                stsMng.MaxProgress = dgvEmpTime.Rows.Count;
                stsMng.Progress = 0;
                foreach (DataGridViewRow gvRow in dgvEmpTime.Rows)
                {
                    stsMng.Status = "Inserting data: " + gvRow.Cells[0].Value.ToString() + " " + gvRow.Cells[1].Value.ToString() + " " + gvRow.Cells[2].Value.ToString() + " " + gvRow.Cells[3].Value.ToString() + " " + gvRow.Cells[4].Value.ToString();

                    // Subcontract TimeCard.
                    if (gvRow.Cells[0].Value.ToString().Trim().StartsWith("I"))
                    {
                        if (!subContractTmCardSvr.CheckDupTimeCard(gvRow.Cells[0].Value.ToString(), DateTime.Parse(gvRow.Cells[1].Value.ToString()), gvRow.Cells[2].Value.ToString()))
                        {
                            TimeCardInfo tc = new TimeCardInfo(gvRow.Cells[0].Value.ToString(), DateTime.Parse(gvRow.Cells[1].Value.ToString()), gvRow.Cells[2].Value.ToString(), int.Parse(gvRow.Cells[3].Value.ToString()), gvRow.Cells[4].Value.ToString(), 1);
                            subContractTmCardSvr.StoreTimeCard(tc);
                            count++;
                        }
                        else
                        {
                            // gvRow.DefaultCellStyle.BackColor = Color.Red;
                            // statusText(gvRow.Cells[0].Value.ToString() + " " + gvRow.Cells[1].Value.ToString() + " " + gvRow.Cells[1].Value.ToString() + " exited ");

                        }


                    }
                    else if (!gvRow.Cells[0].Value.ToString().Trim().StartsWith("7") && gvRow.Cells[0].Value.ToString().Trim() != "90004")
                    {

                        if (!tmCardServ.CheckDupTimeCard(gvRow.Cells[0].Value.ToString(), DateTime.Parse(gvRow.Cells[1].Value.ToString()), gvRow.Cells[2].Value.ToString()))
                        {
                            TimeCardInfo tc = new TimeCardInfo(gvRow.Cells[0].Value.ToString(), DateTime.Parse(gvRow.Cells[1].Value.ToString()), gvRow.Cells[2].Value.ToString(), int.Parse(gvRow.Cells[3].Value.ToString()), gvRow.Cells[4].Value.ToString(), 1);
                            tmCardServ.StoreTimeCard(tc);
                            count++;
                        }
                        else
                        {
                            // gvRow.DefaultCellStyle.BackColor = Color.Red;
                            // statusText(gvRow.Cells[0].Value.ToString() + " " + gvRow.Cells[1].Value.ToString() + " " + gvRow.Cells[1].Value.ToString() + " exited ");

                        }
                    }
                    else
                    {
                        if (!traineeTmCardSvr.CheckDupTimeCard(gvRow.Cells[0].Value.ToString(), DateTime.Parse(gvRow.Cells[1].Value.ToString()), gvRow.Cells[2].Value.ToString()))
                        {
                            TimeCardInfo tc = new TimeCardInfo(gvRow.Cells[0].Value.ToString(), DateTime.Parse(gvRow.Cells[1].Value.ToString()), gvRow.Cells[2].Value.ToString(), int.Parse(gvRow.Cells[3].Value.ToString()), gvRow.Cells[4].Value.ToString(), 1);
                            traineeTmCardSvr.StoreTimeCard(tc);
                            count++;
                        }
                        else
                        {
                            // gvRow.DefaultCellStyle.BackColor = Color.Red;
                            // statusText(gvRow.Cells[0].Value.ToString() + " " + gvRow.Cells[1].Value.ToString() + " " + gvRow.Cells[1].Value.ToString() + " exited ");

                        }
                    }
                    stsMng.Progress++;
                }
                stsMng.Status = "Inserted " + count.ToString() + " of " + dgvEmpTime.Rows.Count.ToString();
                MessageBox.Show("Transfer Completed.", "Complete", MessageBoxButtons.OK);
                stsMng.Progress = 0;
                stsMng.Status = "Ready";
            }
            else if (formAct == FormAction.Save)
            {
                if (MessageBox.Show("คุณต้องการแก้ไขข้อมูลใช่หรือไม่?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    TimeCardInfo tmItem = timeCard_Control1.Information;

                    foreach (DataGridViewRow item in dgvEmpTime.SelectedRows)
                    {
                        if (item.Cells[0].Value.ToString().Trim().StartsWith("I"))
                        {
                            try
                            {
                                TimeCardInfo tmInfo = subContractTmCardSvr.GetTimeCard(item.Cells[0].Value.ToString(), DateTime.Parse(item.Cells[1].Value.ToString()), item.Cells[2].Value.ToString());
                                tmInfo.Duty = tmItem.Duty;

                                subContractTmCardSvr.UpdateTimeCard(tmInfo);
                            }
                            catch
                            {
                                continue;
                            }
                        }
                        else if (item.Cells[0].Value.ToString().Trim().StartsWith("7"))
                        {
                            try
                            {
                                TimeCardInfo tmInfo = traineeTmCardSvr.GetTimeCard(item.Cells[0].Value.ToString(), DateTime.Parse(item.Cells[1].Value.ToString()), item.Cells[2].Value.ToString());
                                tmInfo.Duty = tmItem.Duty;

                                traineeTmCardSvr.UpdateTimeCard(tmInfo);
                            }
                            catch
                            {
                                continue;
                            }

                        }
                        else
                        {
                            try
                            {
                                TimeCardInfo tmInfo = tmCardServ.GetTimeCard(item.Cells[0].Value.ToString(), DateTime.Parse(item.Cells[1].Value.ToString()), item.Cells[2].Value.ToString());
                                tmInfo.Duty = tmItem.Duty;

                                tmCardServ.UpdateTimeCard(tmInfo);
                            }
                            catch
                            {
                                continue;
                            }
                        }


                    }
                    this.Search();
                }


            }

        }

        public void Delete()
        {
            if (formAct == FormAction.Save)
            {
                if (MessageBox.Show("คุณต้องการลบข้อมูลใช่หรือไม่", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    TimeCardInfo tmItem = timeCard_Control1.Information;

                    foreach (DataGridViewRow item in dgvEmpTime.SelectedRows)
                    {
                        try
                        {
                            if (item.Cells[0].Value.ToString().StartsWith("I"))
                            {
                                TimeCardInfo tmInfo = subContractTmCardSvr.GetTimeCard(item.Cells[0].Value.ToString(), DateTime.Parse(item.Cells[1].Value.ToString()), item.Cells[2].Value.ToString());

                                subContractTmCardSvr.DeleteTimeCard(tmInfo); 
                            }
                            else if (item.Cells[0].Value.ToString().StartsWith("7"))
                            {
                                TimeCardInfo tmInfo = traineeTmCardSvr.GetTimeCard(item.Cells[0].Value.ToString(), DateTime.Parse(item.Cells[1].Value.ToString()), item.Cells[2].Value.ToString());

                                traineeTmCardSvr.DeleteTimeCard(tmInfo);
                            }
                            else
                            {
                                TimeCardInfo tmInfo = tmCardServ.GetTimeCard(item.Cells[0].Value.ToString(), DateTime.Parse(item.Cells[1].Value.ToString()), item.Cells[2].Value.ToString());

                                tmCardServ.DeleteTimeCard(tmInfo);
                            }
                        }
                        catch
                        {
                            continue;
                        }

                    }
                    this.Search();
                }


            }
        }

        public void Search()
        {
            try
            {
                if (textBox1.Text.Trim() == "")
                {
                    ArrayList ArryTrainee = new ArrayList();
                    ArrayList ArrySubContract = new ArrayList();
                    ArrayList ArryEmployee = new ArrayList();

                    ArryTrainee = traineeTmCardSvr.GetTimeCardCodesDates(textBox1.Text, dateTimePicker2.Value.Date, dateTimePicker3.Value.Date);

                    ArrySubContract = subContractTmCardSvr.GetTimeCardCodesDates(textBox1.Text, dateTimePicker2.Value.Date, dateTimePicker3.Value.Date);

                    ArryEmployee = tmCardServ.GetTimeCardCodesDates(textBox1.Text, dateTimePicker2.Value.Date, dateTimePicker3.Value.Date, txtTaffId.Text);

                    
                    if (ArryTrainee != null)
                    {
                        foreach (TimeCardInfo var in ArryTrainee)
                        {
                            searchData.Add(var);
                        }
                    }

                    if (ArrySubContract != null)
                    {
                        foreach (TimeCardInfo var in ArrySubContract)
                        {
                            searchData.Add(var);
                        }
                    }

                    if (ArryEmployee != null)
                    {
                        foreach (TimeCardInfo var in ArryEmployee)
                        {
                            searchData.Add(var);
                        }
                    }
                }
                else
                {
                    if (textBox1.Text.StartsWith("7"))
                    {
                        searchData = traineeTmCardSvr.GetTimeCardCodesDates(textBox1.Text, dateTimePicker2.Value.Date, dateTimePicker3.Value.Date);
                    }
                    else if (textBox1.Text.StartsWith("I"))
                    {
                        searchData = subContractTmCardSvr.GetTimeCardCodesDates(textBox1.Text, dateTimePicker2.Value.Date, dateTimePicker3.Value.Date);
                    }
                    else
                    {
                        searchData = tmCardServ.GetTimeCardCodesDates(textBox1.Text, dateTimePicker2.Value.Date, dateTimePicker3.Value.Date, txtTaffId.Text);
                    }
                }

                if (searchData != null)
                {
                    gvData = searchData;
                    FillDataGrid();
                    ucl_ActionControl1.CurrentAction = FormActionType.Save;
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูล", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ucl_ActionControl1.CurrentAction = FormActionType.None;
                    dgvEmpTime.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                ucl_ActionControl1.CurrentAction = FormActionType.None;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            kryptonHeaderGroup3_Click(kryptonHeaderGroup1, new EventArgs());
            dateTimePicker4.Value = DateTime.Today.AddDays(-1);
        }

        public void Clear()
        {

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

        public DCI.Security.Model.PermissionInfo Permission
        {
            set
            {
                ucl_ActionControl1.Permission = value;
            }
        }

        #endregion

        private void btnCheck_Click(object sender, EventArgs e)
        {
            this.Search();

        }
        private void FillDataGrid()
        {
            dgvEmpTime.Rows.Clear();
            try
            {
                foreach (TimeCardInfo var in gvData)
                {
                  int issue=  empSvr.GetEmpCardIssue(var.EmpCode);
                    dgvEmpTime.Rows.Add(var.EmpCode, var.CardDate, var.CardTime, var.CardMachId, var.Duty,var.Issue,issue);
                }
                toolStripStatusLabel1.Text = "Total " + dgvEmpTime.Rows.Count.ToString() + " records";
            }
            catch
            {
            }
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCheck_Click(sender, new EventArgs());

            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker3.Value < dateTimePicker2.Value)
            {
                dateTimePicker3.Value = dateTimePicker2.Value;
            }
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEmpTime.SelectedRows.Count != 0)
            {


                   if (textBox1.Text.StartsWith("7"))
                {
                 
                    TimeCardInfo tmInfo = traineeTmCardSvr.GetTimeCard(dgvEmpTime.SelectedRows[0].Cells[0].Value.ToString(), DateTime.Parse(dgvEmpTime.SelectedRows[0].Cells[1].Value.ToString()), dgvEmpTime.SelectedRows[0].Cells[2].Value.ToString());
                    timeCard_Control1.Information = tmInfo;

                }
                   else if (textBox1.Text.StartsWith("I"))
                   {
                      
                       TimeCardInfo tmInfo = subContractTmCardSvr.GetTimeCard(dgvEmpTime.SelectedRows[0].Cells[0].Value.ToString(), DateTime.Parse(dgvEmpTime.SelectedRows[0].Cells[1].Value.ToString()), dgvEmpTime.SelectedRows[0].Cells[2].Value.ToString());
                       timeCard_Control1.Information = tmInfo;
                   }
                   else
                   {

                       TimeCardInfo tmInfo = tmCardServ.GetTimeCard(dgvEmpTime.SelectedRows[0].Cells[0].Value.ToString(), DateTime.Parse(dgvEmpTime.SelectedRows[0].Cells[1].Value.ToString()), dgvEmpTime.SelectedRows[0].Cells[2].Value.ToString());
                       timeCard_Control1.Information = tmInfo;
                   }
            }
            else
            {
                timeCard_Control1.Information = new TimeCardInfo();
            }
        }

        private void kryptonHeaderGroup3_Click(object sender, EventArgs e)
        {
            ucl_ActionControl1.CurrentAction = FormActionType.None;
            if (sender == kryptonHeaderGroup3)
            {
                formAct = FormAction.Save;
                kryptonHeaderGroup1.Collapsed = true;
                kryptonHeaderGroup3.Collapsed = false;
                ucl_ActionControl1.CurrentAction = FormActionType.None;
                gvData = searchData;
                FillDataGrid();

            }
            else
            {

                kryptonHeaderGroup1.Collapsed = false;
                kryptonHeaderGroup3.Collapsed = true;
                formAct = FormAction.New;
                gvData = empTimeCards;
                FillDataGrid();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;
            dgvEmpLeave.Rows.Clear();
            if (rbnEmp.Checked)
            {
                ArrayList allEmp = empSvr.GetCurrentEmployees();
                // allEmp.Add(empSvr.Find("36508"));
                stsMgr.MaxProgress = allEmp.Count;

                foreach (EmployeeInfo item in allEmp)
                {

                    
                    stsMgr.Progress++;
                    stsMgr.Status = "Calculating:" + item.Code;
                    if (item.EmployeeType == "E" && item.Position.Code != "DR" && item.JoinDate <= dateTimePicker4.Value.Date)
                    {
                        EmployeeWorkTimeInfo wktm = tmCardServ.GetEmployeeWorkingHour(item.Code, dateTimePicker4.Value.Date);

                        if (wktm.Remark.Contains("ABSE"))
                        {
                            BusinesstripInfo bussLs = bussSvr.GetBusinessTripInfo(item.Code, dateTimePicker4.Value.Date);
                            if (bussLs != null)
                            {
                                wktm.Remark = bussLs.Note;
                            }
                        }
                        dgvEmpLeave.Rows.Add(wktm.EmpCode, 
                                               wktm.WorkDate.ToString("dd/MM/yyyy"), 
                                               wktm.WorkFrom <= new DateTime(2000,1,1) ? "" : wktm.WorkFrom.ToString("dd/MM/yyyy HH:mm"), 
                                               wktm.WorkTo <= new DateTime(2000, 1, 1) ? "" : wktm.WorkTo.ToString("dd/MM/yyyy HH:mm"), 
                                               wktm.TimeOk, wktm.Shift, wktm.Remark);

                        //}   
                    }
                }
                stsMgr.Status = "Raedy";
                stsMgr.Progress = 0; 
            }
                

            //Trianee

            if (rbnTrainee.Checked)
            {
                ArrayList allTn = tnSvr.GetCurrentEmployees();
                // allEmp.Add(empSvr.Find("36508"));
                stsMgr.MaxProgress = allTn.Count;

                foreach (EmployeeInfo item in allTn)
                {
                    stsMgr.Progress++;
                    stsMgr.Status = "Calculating:" + item.Code;
                    if (item.JoinDate <= dateTimePicker4.Value.Date)
                    {
                        EmployeeWorkTimeInfo wktm = traineeTmCardSvr.GetEmployeeWorkingHour(item.Code, dateTimePicker4.Value.Date);

                        if (wktm.Remark.Contains("ABSE"))
                        {
                            // BusinesstripInfo bussLs = traineeTmCardSvr.GetBusinessTripInfo(item.Code, dateTimePicker4.Value.Date);
                            // if (bussLs != null)
                            //{
                            //  wktm.Remark = bussLs.Note;
                            // }
                        }
                        dgvEmpLeave.Rows.Add(wktm.EmpCode, wktm.WorkDate.ToString("dd/MM/yyyy"), 
                                                wktm.WorkFrom <= new DateTime(2000, 1, 1) ? "" : wktm.WorkFrom.ToString("dd/MM/yyyy HH:mm"), 
                                                wktm.WorkTo <= new DateTime(2000, 1, 1) ? "" : wktm.WorkTo.ToString("dd/MM/yyyy HH:mm"), 
                                                wktm.TimeOk, wktm.Shift, wktm.Remark);

                        //}   
                    }
                }
                stsMgr.Status = "Raedy";
                stsMgr.Progress = 0; 
            }

            //Subcontract
            if (rbnSub.Checked)
            {

                ArrayList allSub = subSvr.GetCurrentEmployees();
                // allEmp.Add(empSvr.Find("36508"));
                stsMgr.MaxProgress = allSub.Count;


                WorkingHourInfo dtWork =tmCardServ.GetWorkingHour(dateTimePicker4.Value.Date, "D");
                WorkingHourInfo ntWork =tmCardServ. GetWorkingHour(dateTimePicker4.Value.Date, "N");
                WorkingHourInfo dtWorkNt = tmCardServ.GetWorkingHour(dateTimePicker4.Value.Date.AddDays(1), "D");
                WorkingHourInfo ntWorkNt = tmCardServ.GetWorkingHour(dateTimePicker4.Value.Date.AddDays(1), "N");

                foreach (EmployeeInfo item in allSub)
                {

                    
                    stsMgr.Progress++;
                    stsMgr.Status = "Calculating:" + item.Code;
                    if (item.JoinDate <= dateTimePicker4.Value.Date)
                    {
                        EmployeeWorkTimeInfo wktm = subContractTmCardSvr.GetEmployeeWorkingHour(item.Code, dateTimePicker4.Value.Date,dtWork,ntWork,dtWorkNt ,ntWorkNt);

                        if (wktm.Remark.Contains("ABSE"))
                        {
                            // BusinesstripInfo bussLs = traineeTmCardSvr.GetBusinessTripInfo(item.Code, dateTimePicker4.Value.Date);
                            // if (bussLs != null)
                            //{
                            //  wktm.Remark = bussLs.Note;
                            // }
                        }
                        dgvEmpLeave.Rows.Add(wktm.EmpCode, wktm.WorkDate.ToString("dd/MM/yyyy"), 
                                                wktm.WorkFrom <= new DateTime(2000, 1, 1) ? "" : wktm.WorkFrom.ToString("dd/MM/yyyy HH:mm"), 
                                                wktm.WorkTo <= new DateTime(2000, 1, 1) ? "" : wktm.WorkTo.ToString("dd/MM/yyyy HH:mm"), 
                                                wktm.TimeOk, wktm.Shift, wktm.Remark);

                        //}   
                    }
                }
                stsMgr.Status = "Raedy";
                stsMgr.Progress = 0; 
            }


            this.Cursor = Cursors.Default;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณต้องการคำนวณ ABSENT วันที่ " + dateTimePicker4.Value.ToString("dd/MM/yyyy") + " ใช่หรือไม่?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                this.Cursor = Cursors.WaitCursor;
                dgvEmpLeave.Rows.Clear();

                if (rbnEmp.Checked) CalEmpAbsn();
                if (rbnTrainee.Checked) CalTraineeAbsn();
                if (rbnSub.Checked) CalSubContractAbsn();

                this.Cursor = Cursors.Default;

            }
        }

        private void CalEmpAbsn()
        {
            DateTime calDate = dateTimePicker4.Value.Date;
            WorkingHourInfo wkD = tmCardServ.GetWorkingHour(calDate, "D");

            WorkingHourInfo wkN = tmCardServ.GetWorkingHour(calDate, "N");
            ArrayList allEmp = empSvr.GetCurrentEmployees();
            // EmployeeInfo    emp  =  empSvr.Find("11440");
            // allEmp.Add(emp);
            stsMgr.MaxProgress = allEmp.Count;
            foreach (EmployeeInfo item in allEmp)
            {
                stsMgr.Progress++;
                stsMgr.Status = "Calculating:" + item.Code;
                if (item.EmployeeType == "E" && item.Position.Code != "DR" && item.JoinDate <= calDate)
                {
                    EmployeeWorkTimeInfo wktm = tmCardServ.GetEmployeeWorkingHour(item.Code, calDate);
                    string shInf = shSvr.GetEmShift(item.Code, calDate);
                    if (wktm.Shift == "")
                    {
                        wktm.Shift = shInf;
                    }
                    BusinesstripInfo bussLs = bussSvr.GetBusinessTripInfo(item.Code, calDate);

                    //Check if goto Bussiness Trip.

                    if (bussLs == null && (shInf == "D" || shInf == "N"))
                    {
                        if (!wktm.TimeOk)
                        {
                            EmployeeLealeRequestInfo lvRq = new EmployeeLealeRequestInfo();
                            lvRq.EmpCode = wktm.EmpCode;
                            lvRq.LvDate = wktm.WorkDate;
                            ObjectInfo info = new ObjectInfo();
                            info.CreateBy = appMgr.UserAccount.AccountId;
                            lvRq.Inform = info;
                            lvRq.LvNo = 99;
                            lvRq.Reason = "Server";

                            if (wktm.Remark.Contains("(NotWork") || wktm.Remark.Contains("(ABSE") || wktm.Remark.Contains("(NO IN)") || wktm.Remark.Contains("(NO OUT)"))
                            {
                                ArrayList lvLs = lvrqSvr.GetAllLeave(wktm.EmpCode, wktm.WorkDate, wktm.WorkDate, "%");
                                DateTime rdDate = calDate;
                                int conAbs = 1;
                                while (true)
                                {
                                    rdDate = rdDate.AddDays(-1);
                                    string sh = shSvr.GetEmShift(item.Code, rdDate);
                                    if (sh == "D" || sh == "N")
                                    {
                                        ArrayList lv = lvrqSvr.GetAllLeave(item.Code, rdDate, rdDate, "ABSE");
                                        if (lv == null)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            conAbs++;
                                        }
                                    }
                                }

                                if (wktm.Shift == "D")
                                {
                                    bool found = false;
                                    if (lvLs != null)
                                    {
                                        foreach (EmployeeLealeRequestInfo lvItem in lvLs)
                                        {
                                            if (lvItem.LvType == "ABSE")
                                            {
                                                found = true;
                                                lvRq = lvItem;
                                            }
                                        }
                                    }


                                    if (!found)
                                    {          //Insert ABSE day.  
                                        lvRq.LvFrom = wkD.FirstStart.ToString("HH:mm");
                                        lvRq.LvTo = wkD.SecondEnd.ToString("HH:mm");
                                        lvRq.LvType = "ABSE";
                                        lvRq.PayStatus = "N";
                                        lvRq.TotalMinute = 525;
                                        lvRq.TotalHour = "08:45";

                                        try
                                        {
                                            lvrqSvr.SaveLeaveRequest(lvRq);
                                        }
                                        catch (Exception ex)
                                        {
                                            if (MessageBox.Show("Error:Code= " + wktm.EmpCode + " " + ex.Message + "\n Do you want to continue?", "Etrror", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }

                                }
                                else if (wktm.Shift == "N")
                                {
                                    bool found = false;
                                    if (lvLs != null)
                                    {
                                        foreach (EmployeeLealeRequestInfo lvItem in lvLs)
                                        {
                                            if (lvItem.LvType == "ABSE")
                                            {
                                                found = true;
                                                lvRq = lvItem;
                                            }
                                        }
                                    }


                                    if (!found)
                                    {
                                        //Insert ABSE night.  
                                        lvRq.LvFrom = wkN.FirstStart.ToString("HH:mm");
                                        lvRq.LvTo = wkN.SecondEnd.ToString("HH:mm");
                                        lvRq.LvType = "ABSE";
                                        lvRq.PayStatus = "N";
                                        lvRq.TotalMinute = 525;
                                        lvRq.TotalHour = "08:45";

                                        try
                                        {
                                            lvrqSvr.SaveLeaveRequest(lvRq);
                                        }
                                        catch (Exception ex)
                                        {
                                            if (MessageBox.Show("Error:Code= " + wktm.EmpCode + " " + ex.Message + "\n Do you want to continue?", "Etrror", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                                dgvEmpLeave.Rows.Add(wktm.EmpCode, wktm.WorkDate.ToString("dd/MM/yyyy"), 
                                    wktm.WorkFrom <= new DateTime(2000, 1, 1) ? "" : wktm.WorkFrom.ToString("dd/MM/yyyy HH:mm"), 
                                    wktm.WorkTo <= new DateTime(2000, 1, 1) ? "" : wktm.WorkTo.ToString("dd/MM/yyyy HH:mm"), 
                                    wktm.TimeOk, wktm.Shift, lvRq.LvType + " " + lvRq.LvFrom + "-" + lvRq.LvTo, conAbs);

                            }
                            else if (wktm.Remark.Contains("(LATE"))
                            {
                                if (wktm.Shift == "D")
                                {
                                    if (wktm.Remark.Contains("(LATE)"))
                                    {
                                        if (wktm.WorkFrom > wkD.FirstStart)
                                        {
                                            if (wktm.WorkFrom < wkD.FirstEnd)
                                            {
                                                lvRq.LvFrom = wkD.FirstStart.ToString("HH:mm");
                                                lvRq.LvTo = wktm.WorkFrom.ToString("HH:mm");
                                                wktm.Remark = "(LATE " + wkD.FirstStart.ToString("HH:mm") + "-" + wktm.WorkFrom.ToString("HH:mm") + ")";
                                                lvRq.LvType = "LATE";
                                            }
                                        }
                                        if (wktm.WorkTo < wkD.SecondEnd)
                                        {
                                            if (wktm.WorkTo > wkD.SecondStart)
                                            {
                                                lvRq.LvFrom = wktm.WorkTo.ToString("HH:mm");
                                                lvRq.LvTo = wkD.SecondEnd.ToString("HH:mm");
                                                wktm.Remark = "(EARL " + wktm.WorkTo.ToString("HH:mm") + "-" + wkD.SecondEnd.ToString("HH:mm") + ")";
                                                lvRq.LvType = "EARL";
                                            }
                                        }
                                        //Insert LATE .  



                                        DateTime lvTo = DateTime.Parse(lvRq.LvTo);
                                        DateTime lvFrom = DateTime.Parse(lvRq.LvFrom);
                                        TimeSpan tt = lvTo - lvFrom;

                                        if (tt.TotalHours >= 1)
                                        {/*
                                                lvRq.LvFrom = wkD.FirstStart.ToString("HH:mm");
                                                lvRq.LvTo = wkD.SecondEnd.ToString("HH:mm");
                                                lvRq.LvType = "ABSE";
                                                lvRq.PayStatus = "N";
                                                lvRq.TotalMinute = 525;
                                                lvRq.TotalHour = "08:45";

                                                */
                                            int hr = (int)tt.TotalHours;
                                            int mn = (int)tt.TotalMinutes % 60;
                                            lvRq.TotalHour = hr.ToString("00") + ":" + mn.ToString("00");
                                            lvRq.TotalMinute = (int)tt.TotalMinutes;
                                            lvRq.PayStatus = "Y";
                                        }
                                        else
                                        {
                                            int hr = (int)tt.TotalHours;
                                            int mn = (int)tt.TotalMinutes % 60;
                                            lvRq.TotalHour = hr.ToString("00") + ":" + mn.ToString("00");
                                            lvRq.TotalMinute = (int)tt.TotalMinutes;
                                            lvRq.PayStatus = "Y";
                                        }

                                        try
                                        {
                                            lvrqSvr.SaveLeaveRequest(lvRq);
                                        }
                                        catch (Exception ex)
                                        {
                                            if (MessageBox.Show("Error:Code= " + wktm.EmpCode + " " + ex.Message + "\n Do you want to continue?", "Etrror", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                                else if (wktm.Shift == "N")
                                {
                                    if (wktm.Remark.Contains("(LATE)"))
                                    {
                                        if (wktm.WorkFrom > wkN.FirstStart)
                                        {
                                            if (wktm.WorkFrom < wkN.FirstEnd)
                                            {
                                                lvRq.LvFrom = wkN.FirstStart.ToString("HH:mm");
                                                lvRq.LvTo = wktm.WorkFrom.ToString("HH:mm");
                                                wktm.Remark = "(LATE " + wkN.FirstStart.ToString("HH:mm") + "-" + wktm.WorkFrom.ToString("HH:mm") + ")";
                                                lvRq.LvType = "LATE";
                                            }
                                        }
                                        if (wktm.WorkTo < wkN.SecondEnd)
                                        {
                                            if (wktm.WorkTo > wkN.SecondStart)
                                            {
                                                lvRq.LvFrom = wktm.WorkTo.ToString("HH:mm");
                                                lvRq.LvTo = wkN.SecondEnd.ToString("HH:mm");
                                                wktm.Remark = "(EARL " + wktm.WorkTo.ToString("HH:mm") + "-" + wkN.SecondEnd.ToString("HH:mm") + ")";
                                                lvRq.LvType = "EARL";
                                            }
                                        }

                                        //Insert LATE .  


                                        DateTime lvTo = DateTime.Parse(lvRq.LvTo);
                                        DateTime lvFrom = DateTime.Parse(lvRq.LvFrom);
                                        TimeSpan tt = lvTo - lvFrom;
                                        if (tt.TotalHours >= 1)
                                        {/*
                                                lvRq.LvFrom = wkN.FirstStart.ToString("HH:mm");
                                                lvRq.LvTo = wkN.SecondEnd.ToString("HH:mm");
                                                lvRq.LvType = "ABSE";
                                                lvRq.PayStatus = "N";
                                                lvRq.TotalMinute = 525;
                                                lvRq.TotalHour = "08:45";*/
                                            int hr = (int)tt.TotalHours;
                                            int mn = (int)tt.TotalMinutes % 60;
                                            lvRq.TotalHour = hr.ToString("00") + ":" + mn.ToString("00");
                                            lvRq.TotalMinute = (int)tt.TotalMinutes;
                                            lvRq.PayStatus = "Y";
                                        }
                                        else
                                        {
                                            int hr = (int)tt.TotalHours;
                                            int mn = (int)tt.TotalMinutes % 60;
                                            lvRq.TotalHour = hr.ToString("00") + ":" + mn.ToString("00");
                                            lvRq.TotalMinute = (int)tt.TotalMinutes;
                                            lvRq.PayStatus = "Y";
                                        }

                                        try
                                        {
                                            lvrqSvr.SaveLeaveRequest(lvRq);
                                        }
                                        catch (Exception ex)
                                        {
                                            if (MessageBox.Show("Error:Code= " + wktm.EmpCode + " " + ex.Message + "\n Do you want to continue?", "Etrror", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                                dgvEmpLeave.Rows.Add(wktm.EmpCode, wktm.WorkDate.ToString("dd/MM/yyyy HH:mm"), 
                                                      wktm.WorkFrom <= new DateTime(2000, 1, 1) ? "" : wktm.WorkFrom.ToString("dd/MM/yyyy HH:mm"), 
                                                      wktm.WorkTo <= new DateTime(2000, 1, 1) ? "" : wktm.WorkTo.ToString("dd/MM/yyyy HH:mm"), 
                                                      wktm.TimeOk, wktm.Shift, wktm.Remark, 0);

                            }

                        }
                    }
                }
            }
            
            stsMgr.Status = "Raedy";
            stsMgr.Progress = 0;
        }

        private void CalTraineeAbsn()
        {
            DateTime calDate = dateTimePicker4.Value.Date;
            WorkingHourInfo wkD = tmCardServ.GetWorkingHour(calDate, "D");

            WorkingHourInfo wkN = tmCardServ.GetWorkingHour(calDate, "N");
            ArrayList allEmp = tnSvr.GetCurrentEmployees();
            // EmployeeInfo    emp  =  empSvr.Find("11440");
            // allEmp.Add(emp);
            stsMgr.MaxProgress = allEmp.Count;
            foreach (EmployeeInfo item in allEmp)
            {
                stsMgr.Progress++;
                stsMgr.Status = "Calculating:" + item.Code;
                if (item.EmployeeType == "E" && item.Position.Code != "DR" && item.JoinDate <= calDate)
                {
                    EmployeeWorkTimeInfo wktm = traineeTmCardSvr.GetEmployeeWorkingHour(item.Code, calDate);
                    string shInf = traineeShSvr.GetEmShift(item.Code, calDate);
                    if (wktm.Shift == "")
                    {
                        wktm.Shift = shInf;
                    }
                   // BusinesstripInfo bussLs = bussSvr.GetBusinessTripInfo(item.Code, calDate);

                    //Check if goto Bussiness Trip.

                    if  (shInf == "D" || shInf == "N")
                    {
                        if (!wktm.TimeOk)
                        {
                            EmployeeLealeRequestInfo lvRq = new EmployeeLealeRequestInfo();
                            lvRq.EmpCode = wktm.EmpCode;
                            lvRq.LvDate = wktm.WorkDate;
                            ObjectInfo info = new ObjectInfo();
                            info.CreateBy = appMgr.UserAccount.AccountId;
                            lvRq.Inform = info;
                            lvRq.LvNo = 99;
                            lvRq.Reason = "Server";

                            if (wktm.Remark.Contains("(NotWork") || wktm.Remark.Contains("(ABSE") || wktm.Remark.Contains("(NO IN)") || wktm.Remark.Contains("(NO OUT)"))
                            {
                                ArrayList lvLs = traineeLvrqSvr.GetAllLeave(wktm.EmpCode, wktm.WorkDate, wktm.WorkDate, "%");
                                DateTime rdDate = calDate;
                                int conAbs = 1;
                                while (true)
                                {
                                    rdDate = rdDate.AddDays(-1);
                                    string sh = traineeShSvr.GetEmShift(item.Code, rdDate);
                                    if (sh== null)
                                    {
                                        break;
                                    }
                                    if (sh == "D" || sh == "N")
                                    {
                                        ArrayList lv = traineeLvrqSvr.GetAllLeave(item.Code, rdDate, rdDate, "ABSE");
                                        if (lv == null)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            conAbs++;
                                        }
                                    }
                                }

                                if (wktm.Shift == "D")
                                {
                                    bool found = false;
                                    if (lvLs != null)
                                    {
                                        foreach (EmployeeLealeRequestInfo lvItem in lvLs)
                                        {
                                            if (lvItem.LvType == "ABSE")
                                            {
                                                found = true;
                                                lvRq = lvItem;
                                            }
                                        }
                                    }


                                    if (!found)
                                    {          //Insert ABSE day.  
                                        lvRq.LvFrom = wkD.FirstStart.ToString("HH:mm");
                                        lvRq.LvTo = wkD.SecondEnd.ToString("HH:mm");
                                        lvRq.LvType = "ABSE";
                                        lvRq.PayStatus = "N";
                                        lvRq.TotalMinute = 525;
                                        lvRq.TotalHour = "08:45";

                                        try
                                        {
                                            traineeLvrqSvr.SaveLeaveRequest(lvRq);
                                        }
                                        catch (Exception ex)
                                        {
                                            if (MessageBox.Show("Error:Code= " + wktm.EmpCode + " " + ex.Message + "\n Do you want to continue?", "Etrror", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }

                                }
                                else if (wktm.Shift == "N")
                                {
                                    bool found = false;
                                    if (lvLs != null)
                                    {
                                        foreach (EmployeeLealeRequestInfo lvItem in lvLs)
                                        {
                                            if (lvItem.LvType == "ABSE")
                                            {
                                                found = true;
                                                lvRq = lvItem;
                                            }
                                        }
                                    }


                                    if (!found)
                                    {
                                        //Insert ABSE night.  
                                        lvRq.LvFrom = wkN.FirstStart.ToString("HH:mm");
                                        lvRq.LvTo = wkN.SecondEnd.ToString("HH:mm");
                                        lvRq.LvType = "ABSE";
                                        lvRq.PayStatus = "N";
                                        lvRq.TotalMinute = 525;
                                        lvRq.TotalHour = "08:45";

                                        try
                                        {
                                            traineeLvrqSvr.SaveLeaveRequest(lvRq);
                                        }
                                        catch (Exception ex)
                                        {
                                            if (MessageBox.Show("Error:Code= " + wktm.EmpCode + " " + ex.Message + "\n Do you want to continue?", "Etrror", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                                dgvEmpLeave.Rows.Add(wktm.EmpCode, wktm.WorkDate.ToString("dd/MM/yyyy"), 
                                    wktm.WorkFrom <= new DateTime(2000, 1, 1) ? "" : wktm.WorkFrom.ToString("dd/MM/yyyy HH:mm"), 
                                    wktm.WorkTo <= new DateTime(2000, 1, 1) ? "" : wktm.WorkTo.ToString("dd/MM/yyyy HH:mm"), 
                                    wktm.TimeOk, wktm.Shift, lvRq.LvType + " " + lvRq.LvFrom + "-" + lvRq.LvTo, conAbs);

                            }
                            else if (wktm.Remark.Contains("(LATE"))
                            {
                                if (wktm.Shift == "D")
                                {
                                    if (wktm.Remark.Contains("(LATE)"))
                                    {
                                        if (wktm.WorkFrom > wkD.FirstStart)
                                        {
                                            if (wktm.WorkFrom < wkD.FirstEnd)
                                            {
                                                lvRq.LvFrom = wkD.FirstStart.ToString("HH:mm");
                                                lvRq.LvTo = wktm.WorkFrom.ToString("HH:mm");
                                                wktm.Remark = "(LATE " + wkD.FirstStart.ToString("HH:mm") + "-" + wktm.WorkFrom.ToString("HH:mm") + ")";
                                                lvRq.LvType = "LATE";
                                            }
                                        }
                                        if (wktm.WorkTo < wkD.SecondEnd)
                                        {
                                            if (wktm.WorkTo > wkD.SecondStart)
                                            {
                                                lvRq.LvFrom = wktm.WorkTo.ToString("HH:mm");
                                                lvRq.LvTo = wkD.SecondEnd.ToString("HH:mm");
                                                wktm.Remark = "(EARL " + wktm.WorkTo.ToString("HH:mm") + "-" + wkD.SecondEnd.ToString("HH:mm") + ")";
                                                lvRq.LvType = "EARL";
                                            }
                                        }
                                        //Insert LATE .  



                                        DateTime lvTo = DateTime.Parse(lvRq.LvTo);
                                        DateTime lvFrom = DateTime.Parse(lvRq.LvFrom);
                                        TimeSpan tt = lvTo - lvFrom;

                                        if (tt.TotalHours >= 1)
                                        {/*
                                                lvRq.LvFrom = wkD.FirstStart.ToString("HH:mm");
                                                lvRq.LvTo = wkD.SecondEnd.ToString("HH:mm");
                                                lvRq.LvType = "ABSE";
                                                lvRq.PayStatus = "N";
                                                lvRq.TotalMinute = 525;
                                                lvRq.TotalHour = "08:45";

                                                */
                                            int hr = (int)tt.TotalHours;
                                            int mn = (int)tt.TotalMinutes % 60;
                                            lvRq.TotalHour = hr.ToString("00") + ":" + mn.ToString("00");
                                            lvRq.TotalMinute = (int)tt.TotalMinutes;
                                            lvRq.PayStatus = "Y";
                                        }
                                        else
                                        {
                                            int hr = (int)tt.TotalHours;
                                            int mn = (int)tt.TotalMinutes % 60;
                                            lvRq.TotalHour = hr.ToString("00") + ":" + mn.ToString("00");
                                            lvRq.TotalMinute = (int)tt.TotalMinutes;
                                            lvRq.PayStatus = "Y";
                                        }

                                        try
                                        {
                                            traineeLvrqSvr.SaveLeaveRequest(lvRq);
                                        }
                                        catch (Exception ex)
                                        {
                                            if (MessageBox.Show("Error:Code= " + wktm.EmpCode + " " + ex.Message + "\n Do you want to continue?", "Etrror", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                                else if (wktm.Shift == "N")
                                {
                                    if (wktm.Remark.Contains("(LATE)"))
                                    {
                                        if (wktm.WorkFrom > wkN.FirstStart)
                                        {
                                            if (wktm.WorkFrom < wkN.FirstEnd)
                                            {
                                                lvRq.LvFrom = wkN.FirstStart.ToString("HH:mm");
                                                lvRq.LvTo = wktm.WorkFrom.ToString("HH:mm");
                                                wktm.Remark = "(LATE " + wkN.FirstStart.ToString("HH:mm") + "-" + wktm.WorkFrom.ToString("HH:mm") + ")";
                                                lvRq.LvType = "LATE";
                                            }
                                        }
                                        if (wktm.WorkTo < wkN.SecondEnd)
                                        {
                                            if (wktm.WorkTo > wkN.SecondStart)
                                            {
                                                lvRq.LvFrom = wktm.WorkTo.ToString("HH:mm");
                                                lvRq.LvTo = wkN.SecondEnd.ToString("HH:mm");
                                                wktm.Remark = "(EARL " + wktm.WorkTo.ToString("HH:mm") + "-" + wkN.SecondEnd.ToString("HH:mm") + ")";
                                                lvRq.LvType = "EARL";
                                            }
                                        }

                                        //Insert LATE .  


                                        DateTime lvTo = DateTime.Parse(lvRq.LvTo);
                                        DateTime lvFrom = DateTime.Parse(lvRq.LvFrom);
                                        TimeSpan tt = lvTo - lvFrom;
                                        if (tt.TotalHours >= 1)
                                        {/*
                                                lvRq.LvFrom = wkN.FirstStart.ToString("HH:mm");
                                                lvRq.LvTo = wkN.SecondEnd.ToString("HH:mm");
                                                lvRq.LvType = "ABSE";
                                                lvRq.PayStatus = "N";
                                                lvRq.TotalMinute = 525;
                                                lvRq.TotalHour = "08:45";*/
                                            int hr = (int)tt.TotalHours;
                                            int mn = (int)tt.TotalMinutes % 60;
                                            lvRq.TotalHour = hr.ToString("00") + ":" + mn.ToString("00");
                                            lvRq.TotalMinute = (int)tt.TotalMinutes;
                                            lvRq.PayStatus = "Y";
                                        }
                                        else
                                        {
                                            int hr = (int)tt.TotalHours;
                                            int mn = (int)tt.TotalMinutes % 60;
                                            lvRq.TotalHour = hr.ToString("00") + ":" + mn.ToString("00");
                                            lvRq.TotalMinute = (int)tt.TotalMinutes;
                                            lvRq.PayStatus = "Y";
                                        }

                                        try
                                        {
                                            traineeLvrqSvr.SaveLeaveRequest(lvRq);
                                        }
                                        catch (Exception ex)
                                        {
                                            if (MessageBox.Show("Error:Code= " + wktm.EmpCode + " " + ex.Message + "\n Do you want to continue?", "Etrror", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                                dgvEmpLeave.Rows.Add(wktm.EmpCode, wktm.WorkDate.ToString("dd/MM/yyyy HH:mm"), 
                                    wktm.WorkFrom <= new DateTime(2000, 1, 1) ? "" : wktm.WorkFrom.ToString("dd/MM/yyyy HH:mm"), 
                                    wktm.WorkTo <= new DateTime(2000, 1, 1) ? "" : wktm.WorkTo.ToString("dd/MM/yyyy HH:mm"), 
                                    wktm.TimeOk, wktm.Shift, wktm.Remark, 0);

                            }

                        }
                    }
                }
            }

            stsMgr.Status = "Raedy";
            stsMgr.Progress = 0;
        }

        private void CalSubContractAbsn()
        {
            DateTime calDate = dateTimePicker4.Value.Date;
            WorkingHourInfo wkD = tmCardServ.GetWorkingHour(calDate, "D");

            WorkingHourInfo wkN = tmCardServ.GetWorkingHour(calDate, "N");
            ArrayList allEmp = subSvr.GetCurrentEmployees();
            // EmployeeInfo    emp  =  empSvr.Find("11440");
            // allEmp.Add(emp);
            stsMgr.MaxProgress = allEmp.Count;

            WorkingHourInfo dtWork = tmCardServ.GetWorkingHour(calDate, "D");
            WorkingHourInfo ntWork = tmCardServ.GetWorkingHour(calDate, "N");
            WorkingHourInfo dtWorkNt = tmCardServ.GetWorkingHour(calDate.AddDays(1), "D");
            WorkingHourInfo ntWorkNt = tmCardServ.GetWorkingHour(calDate.AddDays(1), "N");


            foreach (EmployeeInfo item in allEmp)
            {
                

                stsMgr.Progress++;
                stsMgr.Status = "Calculating:" + item.Code;
                if ( item.JoinDate <= calDate)
                {
                    EmployeeWorkTimeInfo wktm = subContractTmCardSvr.GetEmployeeWorkingHour(item.Code, calDate,dtWork,ntWork,dtWorkNt,ntWorkNt);
                    string shInf = subContractShSvr.GetEmShift(item.Code, calDate);
                    if (wktm.Shift == "")
                    {
                        wktm.Shift = shInf;
                    }
                    // BusinesstripInfo bussLs = bussSvr.GetBusinessTripInfo(item.Code, calDate);

                    //Check if goto Bussiness Trip.

                    if (shInf == "D" || shInf == "N")
                    {
                        if (!wktm.TimeOk)
                        {
                            EmployeeLealeRequestInfo lvRq = new EmployeeLealeRequestInfo();
                            lvRq.EmpCode = wktm.EmpCode;
                            lvRq.LvDate = wktm.WorkDate;
                            ObjectInfo info = new ObjectInfo();
                            info.CreateBy = appMgr.UserAccount.AccountId;
                            lvRq.Inform = info;
                            lvRq.LvNo = 99;
                            lvRq.Reason = "Server";

                            
                            if (wktm.Remark.Contains("(NotWork") || wktm.Remark.Contains("(ABSE") || wktm.Remark.Contains("(NO IN)") || wktm.Remark.Contains("(NO OUT)"))
                            {
                                ArrayList lvLs = subContractLvrqSvr.GetAllLeave(wktm.EmpCode, wktm.WorkDate, wktm.WorkDate, "%");
                                DateTime rdDate = calDate;
                                int conAbs = 1;
                                while (true)
                                {
                                    rdDate = rdDate.AddDays(-1);
                                    string sh = subContractShSvr.GetEmShift(item.Code, rdDate);
                                    if (sh== null)
                                    {
                                        break;
                                    }
                                    if (sh == "D" || sh == "N")
                                    {
                                        ArrayList lv = subContractLvrqSvr.GetAllLeave(item.Code, rdDate, rdDate, "ABSE");
                                        if (lv == null)
                                        {
                                            break;
                                        }
                                        else if (rdDate<= item.JoinDate)
                                        {
                                            break;
                                        }

                                        else
                                        {
                                            conAbs++;
                                        }
                                    }
                                    
                                }

                                if (wktm.Shift == "D")
                                {
                                    bool found = false;
                                    if (lvLs != null)
                                    {
                                        foreach (EmployeeLealeRequestInfo lvItem in lvLs)
                                        {
                                            if (lvItem.LvType == "ABSE")
                                            {
                                                found = true;
                                                lvRq = lvItem;
                                            }
                                        }
                                    }


                                    if (!found)
                                    {          //Insert ABSE day.  
                                        lvRq.LvFrom = wkD.FirstStart.ToString("HH:mm");
                                        lvRq.LvTo = wkD.SecondEnd.ToString("HH:mm");
                                        lvRq.LvType = "ABSE";
                                        lvRq.PayStatus = "N";
                                        lvRq.TotalMinute = 525;
                                        lvRq.TotalHour = "08:45";

                                        try
                                        {
                                            subContractLvrqSvr.SaveLeaveRequest(lvRq);
                                        }
                                        catch (Exception ex)
                                        {
                                            if (MessageBox.Show("Error:Code= " + wktm.EmpCode + " " + ex.Message + "\n Do you want to continue?", "Etrror", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }

                                }
                                else if (wktm.Shift == "N")
                                {
                                    bool found = false;
                                    if (lvLs != null)
                                    {
                                        foreach (EmployeeLealeRequestInfo lvItem in lvLs)
                                        {
                                            if (lvItem.LvType == "ABSE")
                                            {
                                                found = true;
                                                lvRq = lvItem;
                                            }
                                        }
                                    }


                                    if (!found)
                                    {
                                        //Insert ABSE night.  
                                        lvRq.LvFrom = wkN.FirstStart.ToString("HH:mm");
                                        lvRq.LvTo = wkN.SecondEnd.ToString("HH:mm");
                                        lvRq.LvType = "ABSE";
                                        lvRq.PayStatus = "N";
                                        lvRq.TotalMinute = 525;
                                        lvRq.TotalHour = "08:45";

                                        try
                                        {
                                            subContractLvrqSvr.SaveLeaveRequest(lvRq);
                                        }
                                        catch (Exception ex)
                                        {
                                            if (MessageBox.Show("Error:Code= " + wktm.EmpCode + " " + ex.Message + "\n Do you want to continue?", "Etrror", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                                dgvEmpLeave.Rows.Add(wktm.EmpCode, wktm.WorkDate.ToString("dd/MM/yyyy"), 
                                    wktm.WorkFrom <= new DateTime(2000, 1, 1) ? "" : wktm.WorkFrom.ToString("dd/MM/yyyy HH:mm"), 
                                    wktm.WorkTo <= new DateTime(2000, 1, 1) ? "" : wktm.WorkTo.ToString("dd/MM/yyyy HH:mm"), 
                                    wktm.TimeOk, wktm.Shift, lvRq.LvType + " " + lvRq.LvFrom + "-" + lvRq.LvTo, conAbs);

                            }
                            else if (wktm.Remark.Contains("(LATE"))
                            {
                                if (wktm.Shift == "D")
                                {
                                    if (wktm.Remark.Contains("(LATE)"))
                                    {
                                        if (wktm.WorkFrom > wkD.FirstStart)
                                        {
                                            if (wktm.WorkFrom < wkD.FirstEnd)
                                            {
                                                lvRq.LvFrom = wkD.FirstStart.ToString("HH:mm");
                                                lvRq.LvTo = wktm.WorkFrom.ToString("HH:mm");
                                                wktm.Remark = "(LATE " + wkD.FirstStart.ToString("HH:mm") + "-" + wktm.WorkFrom.ToString("HH:mm") + ")";
                                                lvRq.LvType = "LATE";
                                            }
                                        }
                                        if (wktm.WorkTo < wkD.SecondEnd)
                                        {
                                            if (wktm.WorkTo > wkD.SecondStart)
                                            {
                                                lvRq.LvFrom = wktm.WorkTo.ToString("HH:mm");
                                                lvRq.LvTo = wkD.SecondEnd.ToString("HH:mm");
                                                wktm.Remark = "(EARL " + wktm.WorkTo.ToString("HH:mm") + "-" + wkD.SecondEnd.ToString("HH:mm") + ")";
                                                lvRq.LvType = "EARL";
                                            }
                                        }
                                        //Insert LATE .  



                                        DateTime lvTo = DateTime.Parse(lvRq.LvTo);
                                        DateTime lvFrom = DateTime.Parse(lvRq.LvFrom);
                                        TimeSpan tt = lvTo - lvFrom;

                                        if (tt.TotalHours >= 1)
                                        {/*
                                                lvRq.LvFrom = wkD.FirstStart.ToString("HH:mm");
                                                lvRq.LvTo = wkD.SecondEnd.ToString("HH:mm");
                                                lvRq.LvType = "ABSE";
                                                lvRq.PayStatus = "N";
                                                lvRq.TotalMinute = 525;
                                                lvRq.TotalHour = "08:45";

                                                */
                                            int hr = (int)tt.TotalHours;
                                            int mn = (int)tt.TotalMinutes % 60;
                                            lvRq.TotalHour = hr.ToString("00") + ":" + mn.ToString("00");
                                            lvRq.TotalMinute = (int)tt.TotalMinutes;
                                            lvRq.PayStatus = "Y";
                                        }
                                        else
                                        {
                                            int hr = (int)tt.TotalHours;
                                            int mn = (int)tt.TotalMinutes % 60;
                                            lvRq.TotalHour = hr.ToString("00") + ":" + mn.ToString("00");
                                            lvRq.TotalMinute = (int)tt.TotalMinutes;
                                            lvRq.PayStatus = "Y";
                                        }

                                        try
                                        {
                                            subContractLvrqSvr.SaveLeaveRequest(lvRq);
                                        }
                                        catch (Exception ex)
                                        {
                                            if (MessageBox.Show("Error:Code= " + wktm.EmpCode + " " + ex.Message + "\n Do you want to continue?", "Etrror", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                                else if (wktm.Shift == "N")
                                {
                                    if (wktm.Remark.Contains("(LATE)"))
                                    {
                                        if (wktm.WorkFrom > wkN.FirstStart)
                                        {
                                            if (wktm.WorkFrom < wkN.FirstEnd)
                                            {
                                                lvRq.LvFrom = wkN.FirstStart.ToString("HH:mm");
                                                lvRq.LvTo = wktm.WorkFrom.ToString("HH:mm");
                                                wktm.Remark = "(LATE " + wkN.FirstStart.ToString("HH:mm") + "-" + wktm.WorkFrom.ToString("HH:mm") + ")";
                                                lvRq.LvType = "LATE";
                                            }
                                        }
                                        if (wktm.WorkTo < wkN.SecondEnd)
                                        {
                                            if (wktm.WorkTo > wkN.SecondStart)
                                            {
                                                lvRq.LvFrom = wktm.WorkTo.ToString("HH:mm");
                                                lvRq.LvTo = wkN.SecondEnd.ToString("HH:mm");
                                                wktm.Remark = "(EARL " + wktm.WorkTo.ToString("HH:mm") + "-" + wkN.SecondEnd.ToString("HH:mm") + ")";
                                                lvRq.LvType = "EARL";
                                            }
                                        }

                                        //Insert LATE .  


                                        DateTime lvTo = DateTime.Parse(lvRq.LvTo);
                                        DateTime lvFrom = DateTime.Parse(lvRq.LvFrom);
                                        TimeSpan tt = lvTo - lvFrom;
                                        if (tt.TotalHours >= 1)
                                        {/*
                                                lvRq.LvFrom = wkN.FirstStart.ToString("HH:mm");
                                                lvRq.LvTo = wkN.SecondEnd.ToString("HH:mm");
                                                lvRq.LvType = "ABSE";
                                                lvRq.PayStatus = "N";
                                                lvRq.TotalMinute = 525;
                                                lvRq.TotalHour = "08:45";*/
                                            int hr = (int)tt.TotalHours;
                                            int mn = (int)tt.TotalMinutes % 60;
                                            lvRq.TotalHour = hr.ToString("00") + ":" + mn.ToString("00");
                                            lvRq.TotalMinute = (int)tt.TotalMinutes;
                                            lvRq.PayStatus = "Y";
                                        }
                                        else
                                        {
                                            int hr = (int)tt.TotalHours;
                                            int mn = (int)tt.TotalMinutes % 60;
                                            lvRq.TotalHour = hr.ToString("00") + ":" + mn.ToString("00");
                                            lvRq.TotalMinute = (int)tt.TotalMinutes;
                                            lvRq.PayStatus = "Y";
                                        }

                                        try
                                        {
                                            subContractLvrqSvr.SaveLeaveRequest(lvRq);
                                        }
                                        catch (Exception ex)
                                        {
                                            if (MessageBox.Show("Error:Code= " + wktm.EmpCode + " " + ex.Message + "\n Do you want to continue?", "Etrror", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                                dgvEmpLeave.Rows.Add(wktm.EmpCode, wktm.WorkDate.ToString("dd/MM/yyyy HH:mm"), 
                                    wktm.WorkFrom <= new DateTime(2000, 1, 1) ? "" : wktm.WorkFrom.ToString("dd/MM/yyyy HH:mm"), 
                                    wktm.WorkTo <= new DateTime(2000, 1, 1) ? "" : wktm.WorkTo.ToString("dd/MM/yyyy HH:mm"), 
                                    wktm.TimeOk, wktm.Shift, wktm.Remark, 0);

                            }

                        }
                    }
                }
            }

            stsMgr.Status = "Raedy";
            stsMgr.Progress = 0;
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {

                FontFamily fm = new FontFamily("Microsoft Sans Serif");
                Font ft = new Font(fm, 8.0f);
                string header;

                header = "Employee Leave Report " + dateTimePicker4.Value.Date.ToString("dd/MM/yyyy");
                printDocument1.DefaultPageSettings.Landscape = false;
                printDocument1.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 20);
                MyDataGridViewPrinter = new DataGridViewPrinter(dgvEmpLeave, printDocument1, true, true, header, ft, Color.Black, true);

                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            bool more = MyDataGridViewPrinter.DrawDataGridView(e.Graphics);
            if (more == true)
                e.HasMorePages = true;
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.RowIndex >= 0 )
            {
                kryptonHeaderGroup3_Click(kryptonHeaderGroup3, new EventArgs());
                textBox1.Text = dgvEmpLeave.Rows[e.RowIndex].Cells[0].Value.ToString();
                this.Search();
            }
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker3.Value < dateTimePicker2.Value)
            {
                dateTimePicker2.Value = dateTimePicker3.Value;
            }
        }
    }
}
