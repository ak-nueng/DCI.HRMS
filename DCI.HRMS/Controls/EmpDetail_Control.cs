using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Model;
using DCI.HRMS.Model.Common;
using DCI.HRMS.Model.Attendance;
using DCI.HRMS.Model.Personal;
using DCI.HRMS.Service;
using DCI.HRMS.Service.SubContract;
using DCI.HRMS.Service.Trainee;

namespace DCI.HRMS.Controls
{
    public partial class EmpDetail_Control : UserControl
    {
        public EmployeeService empServ;
        public SubContractService subSvr;
        public TraineeService trSvr;
        public ShiftService shFtServ;
        public TraineeShiftService trShSvr;
        public SubContractShiftService subShSvr;
        private string empCode;
        private DateTime shDate = new DateTime(1900,1,1);
        EmployeeInfo empInfo;
        private EmployeeShiftInfo shinf;
        public EmpDetail_Control()
        {
            InitializeComponent();
        }

        private void EmpData_Control_Load(object sender, EventArgs e)
        {
            shDate = DateTime.Today;

        }
        public DateTime ShftDate
        {
            set { 
                shDate = value;

            }

        }
        public void FillShift()
        {
            try
            {
              
                dayShift_Control1.Shift = shinf.ShiftData.Substring(shDate.Day - 1, 1);
                dayShift_Control1.Day = shDate.DayOfWeek.ToString().Substring(0, 2);
                dayShift_Control1.Date = shDate.Day.ToString("00");
                kryptonGroup4.Visible = true;
            }
            catch
            {
                kryptonGroup4.Visible = false;


            }

        }
        public object Information
        {

            set
            {
                try
                {
                    empCode = (string) value;
                    pictureBox1.Image = null;
                    try
                    {

                        if (empCode.StartsWith("I"))
                        {
                            empInfo = subSvr.Find(empCode);
                        }
                        else if (empCode.StartsWith("7"))
                        {
                            empInfo = trSvr.Find(empCode);
                        }
                        else
                        {
                            empInfo = empServ.Find(empCode);
                        }

                    }
                    catch (Exception ex) { 
                        //MessageBox.Show(ex.ToString()); 
                    }


                    cODETextBox.Text = empInfo.Code;
                    pRENTextBox.Text = empInfo.NameInEng.Title;
                    nAMETextBox.Text = empInfo.NameInEng.Name;
                    sURNTextBox.Text = empInfo.NameInEng.Surname;
                    iDNOTextBox.Text = empInfo.CitizenId;
                    txtNickName.Text = empInfo.NameInThai.NickName;
                    pOSI_ENAMETextBox.Text = empInfo.Position.NameEng;
                    try
                    {
                        dV_ENAMETextBox.Text = empInfo.Division.Name;
                    }
                    catch
                    {
                        dV_ENAMETextBox.Text = "";
                    }
                    bIRTHTextBox.Text = empInfo.BirthDate.ToString("dd/MM/yyyy");
                    jOINTextBox.Text = empInfo.JoinDate.ToString("dd/MM/yyyy");
                    txtGrpot.Text = empInfo.OtGroupLine;
                    txtLine.Text = empInfo.WorkGroupLine;

                    if (empInfo.WorkType.Trim() == "S")
                        textBox4.Text = "รายเดือน";
                    else if (empInfo.WorkType.Trim() == "O")
                        textBox4.Text = "รายวัน";
                    txtCompany.Text = empInfo.Company;   
                    rESIGNTextBox.Clear();
                    kryptonGroup3.Enabled = false;
                    rSTYPETextBox.Clear();
                    rSREASONTextBox.Clear();
                    kryptonGroup3.BackColor = Color.White;
                    if (empInfo.Resigned)
                    {
                        rESIGNTextBox.Text = empInfo.ResignDate.ToString("dd/MM/yyyy");
                        rSTYPETextBox.Text = empInfo.ResignType;
                        BasicInfo rsty = EmployeeService.Instance().GetResignType(empInfo.ResignType);
                        rSREASONTextBox.Text = empInfo.ResignReason;
                        kryptonGroup3.Enabled = true;
                        kryptonGroup3.BackColor = Color.Red;
                    }


                    try
                    {
                        textBox2.Clear();
                        if (empInfo.BirthDate.Year > 1900 )
                        {
                            //DateTime birthDate = Convert.ToDateTime(bIRTHTextBox.Text);
                            TimeSpan ts = DateTime.Now - empInfo.BirthDate;
                            int year = 0;
                            int month = 0;
                            year = ts.Days / 365;
                            month = (ts.Days % 365) / 30;
                            textBox2.Text = year.ToString() + " ปี " + month.ToString() + " เดือน";

                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.ToString()); }

                    try
                    {
                        textBox3.Clear();
                        if (empInfo.JoinDate.Year > 1900 && empInfo.ResignDate.Year <= 1900 )
                        {
                            //DateTime joinDate = Convert.ToDateTime(jOINTextBox.Text);
                            TimeSpan ts = DateTime.Now - empInfo.JoinDate;
                            int year = 0;
                            int month = 0;
                            year = ts.Days / 365;
                            month = (ts.Days % 365) / 30;
                            textBox3.Text = year.ToString() + " ปี " + month.ToString() + " เดือน";

                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.ToString()); }


                    try
                    {
                        pictureBox1.ImageLocation = "http://dcidmc.dci.daikin.co.jp/PICTURE/" + empCode + ".JPG";
                    }
                    catch
                    {


                    }
                    try
                    {



                        if (empCode.StartsWith("I"))
                        {
                            shinf = subShSvr.GetEmShift(empInfo.Code, shDate.ToString("yyyyMM"));
                        }
                        else if (empCode.StartsWith("7"))
                        {
                            shinf = trShSvr.GetEmShift(empInfo.Code, shDate.ToString("yyyyMM"));
                        }
                        else
                        {
                            shinf = shFtServ.GetEmShift(empInfo.Code, shDate.ToString("yyyyMM"));
                        }
                        dayShift_Control1.Shift = shinf.ShiftData.Substring(shDate.Day - 1, 1);
                        dayShift_Control1.Day = shDate.DayOfWeek.ToString().Substring(0, 2);
                        dayShift_Control1.Date = shDate.Day.ToString("00");
                        kryptonGroup4.Visible = true;
                    }
                    catch
                    {
                        kryptonGroup4.Visible = false;


                    }

                }

                catch
                {
                    empCode = null;

                    cODETextBox.Clear();
                    pRENTextBox.Clear();
                    nAMETextBox.Clear();
                    sURNTextBox.Clear();
                    iDNOTextBox.Clear();
                    pOSI_ENAMETextBox.Clear();
                    txtNickName.Clear();
                    dV_ENAMETextBox.Clear();
                    bIRTHTextBox.Clear();
                    jOINTextBox.Clear();
                    txtGrpot.Clear();
                    txtLine.Clear();
                    rESIGNTextBox.Clear();
                    kryptonGroup3.Enabled = false;
                    rSTYPETextBox.Clear();
                    rSREASONTextBox.Clear();
                    textBox4.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    kryptonGroup4.Visible = false;
                    txtCompany.Clear();

                }
            }
            get
            {
                return empInfo;
            }

        }



    }
}
