using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Service;
using System.Collections;
using DCI.HRMS.Model.Attendance;
using DCI.HRMS.Model.Personal;
using System.Threading;
using DCI.HRMS.Util;
using CrystalDecisions.Shared;

namespace DCI.HRMS.Attendance.Reports
{
    public partial class FrmRptAttandance : Form
    {
           private     EmployeeService emSvr = EmployeeService.Instance();
      private  EmployeeLeaveService emLvSvr = EmployeeLeaveService.Instance();
        private AttendanceDataSet dt = new AttendanceDataSet();
        private StatusManager stsMgr = new StatusManager();
        public FrmRptAttandance()
        {
            InitializeComponent();
        }

        private void FrmRptAttandance_Load(object sender, EventArgs e)
        {
            dtpAnnu.Value = DateTime.Today;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (m_AsyncWorker.IsBusy)
            {
                btnGenerate.Enabled = false;
              stsMgr.Status = "Cancelling...";
                m_AsyncWorker.CancelAsync();
            }
            else
            {
                btnGenerate.Text = "Cancel";
                stsMgr.Status = "Running...";

                // Kickoff the worker thread to begin it's DoWork function.
                this.Cursor = Cursors.WaitCursor;
                dt = new AttendanceDataSet();
                m_AsyncWorker.RunWorkerAsync();
            }
        }
        

        private string MnToText(double mn)
        {

            int dayrm = mn >= 0 ? (int)Math.Floor(mn / 525d) : (int)Math.Ceiling(mn / 525d);
            int md = (int)mn % 525;
            int hr = md >= 0 ? (int)Math.Floor(md / 60d) : (int)Math.Ceiling(md / 60d);
            md = md % 60;

            return dayrm.ToString("0") + ":" + hr.ToString("00") + ":" + md.ToString("00");

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bwAsync = sender as BackgroundWorker;
            stsMgr.MaxProgress = 100;
            if (radioButton1.Checked)
            {
               // toolStripProgressBar1.Value = 0;
                ArrayList emp = emSvr.GetCurrentEmployees();
              
                DataTable dtb = dt.AnnualTable;
             //   toolStripProgressBar1.Maximum = emp.Count;
                int i = 0;
                foreach (EmployeeInfo var in emp)
                {
                    // toolStripStatusLabel1.Text = "Calculating :" + var.Code;
                    if (var.EmployeeType != "M")
                    {
                        ArrayList annu = emLvSvr.GetAnnualTotal(var.Code,dtpAnnu.Value,false);
                        if (annu.Count > 0)
                        {
                            DataRow dr = dt.AnnualTable.NewRow();
                            //  DataGridViewRow dr = new DataGridViewRow();
                            //dr.CreateCells(dataGridView1);
                            // DataRow dr = dt.AnnualTable.NewAnnualTableRow();
                            dr["DVCD"] = var.Division.Code;
                            dr["DV_Name"] = var.Division.Name;
                            //  dr.ItemArray[0] = var.Division.Name;
                            dr["Line"] = var.WorkGroupLine;
                            dr["OTLine"] = var.OtGroupLine;
                            dr["Code"] = var.Code;
                            dr["Name"] = var.NameInThai.ToString();
                            dr["Position"] = var.Position.Code;
                            dr["JoinDate"] = var.JoinDate.ToShortDateString();

                            if (annu.Count == 1)
                            {
                                dr["RemainLastYear"] = "0:00:00";
                                AnnualTotal antt = (AnnualTotal)annu[0];
                                dr["Remain_Day"] = 0;
                                dr["GetThisYear"] = antt.Get.ToString();
                                dr["Total_Day"] = antt.Total / 525d;
                                dr["Total"] = antt.TotalText;
                                dr["Exceed"] = MnToText(0d);
                                dr["Exceed_Day"] = 0;
                            }
                            else
                            {

                                AnnualTotal antL = (AnnualTotal)annu[annu.Count - 2];
                                dr["RemainLastYear"] = antL.RemainHr;
                                dr["Remain_Day"] = antL.Remain / 525d;
                                AnnualTotal antt = (AnnualTotal)annu[annu.Count - 1];
                                dr["GetThisYear"] = antt.Get.ToString();
                                dr["Total_Day"] = antt.Total / 525d;
                                dr["Total"] = antt.TotalText;
                                dr["Exceed"] = MnToText((antt.FullTotal - antt.Total) < 0 ? 0 : (antt.FullTotal - antt.Total));
                                dr["Exceed_Day"] = (antt.FullTotal - antt.Total) < 0 ? 0 : (antt.FullTotal - antt.Total) / 525d;

                            }
                            dt.AnnualTable.Rows.Add(dr);

                        }
                    }
                    //    toolStripProgressBar1.Value++;
                    //   this.Update();

                    i++;
                    bwAsync.ReportProgress(Convert.ToInt32(i * (100.0 / emp.Count)));

                    if (bwAsync.CancellationPending)
                    {
                        // Pause for a bit to demonstrate that there is time between

                        // "Cancelling..." and "Cancel ed".

                        Thread.Sleep(1200);

                        // Set the e.Cancel flag so that the WorkerCompleted event

                        // knows that the process was cancelled.

                        e.Cancel = true;
                        return;
                    }
                }
              //  toolStripStatusLabel1.Text = "";
               // toolStripProgressBar1.Value = 0;

            }
            else  if (radioButton5.Checked)
            {
                // toolStripProgressBar1.Value = 0;
                ArrayList emp = emSvr.GetCurrentEmployees();

                DataTable dtb = dt.AnnualTable;
                //   toolStripProgressBar1.Maximum = emp.Count;
                int i = 0;
                foreach (EmployeeInfo var in emp)
                {
                    // toolStripStatusLabel1.Text = "Calculating :" + var.Code;
                    if (var.EmployeeType == "M")
                    {
                        ArrayList annu = emLvSvr.GetAnnualTotal(var.Code, dtpAnnu.Value, false);
                        if (annu.Count > 0)
                        {
                            DataRow dr = dt.AnnualTable.NewRow();
                            //  DataGridViewRow dr = new DataGridViewRow();
                            //dr.CreateCells(dataGridView1);
                            // DataRow dr = dt.AnnualTable.NewAnnualTableRow();
                            dr["DVCD"] = var.Division.Code;
                            dr["DV_Name"] = var.Division.Name;
                            //  dr.ItemArray[0] = var.Division.Name;
                            dr["Line"] = var.WorkGroupLine;
                            dr["OTLine"] = var.OtGroupLine;
                            dr["Code"] = var.Code;
                            dr["Name"] = var.NameInThai.ToString();
                            dr["Position"] = var.Position.Code;
                            dr["JoinDate"] = var.JoinDate.ToShortDateString();

                            if (annu.Count == 1)
                            {
                                dr["RemainLastYear"] = "0:00:00";
                                AnnualTotal antt = (AnnualTotal)annu[0];
                                dr["Remain_Day"] = 0;
                                dr["GetThisYear"] = antt.Get.ToString();
                                dr["Total_Day"] = antt.Total / 525d;
                                dr["Total"] = antt.TotalText;
                                dr["Exceed"] = MnToText(0d);
                                dr["Exceed_Day"] = 0;
                            }
                            else
                            {

                                AnnualTotal antL = (AnnualTotal)annu[annu.Count - 2];
                                dr["RemainLastYear"] = antL.RemainHr;
                                dr["Remain_Day"] = antL.Remain / 525d;
                                AnnualTotal antt = (AnnualTotal)annu[annu.Count - 1];
                                dr["GetThisYear"] = antt.Get.ToString();
                                dr["Total_Day"] = antt.Total / 525d;
                                dr["Total"] = antt.TotalText;
                                dr["Exceed"] = MnToText((antt.FullTotal - antt.Total) < 0 ? 0 : (antt.FullTotal - antt.Total));
                                dr["Exceed_Day"] = (antt.FullTotal - antt.Total) < 0 ? 0 : (antt.FullTotal - antt.Total) / 525d;

                            }
                            dt.AnnualTable.Rows.Add(dr);

                        }
                    }
                    //    toolStripProgressBar1.Value++;
                    //   this.Update();

                    i++;
                    bwAsync.ReportProgress(Convert.ToInt32(i * (100.0 / emp.Count)));

                    if (bwAsync.CancellationPending)
                    {
                        // Pause for a bit to demonstrate that there is time between

                        // "Cancelling..." and "Cancel ed".

                        Thread.Sleep(1200);

                        // Set the e.Cancel flag so that the WorkerCompleted event

                        // knows that the process was cancelled.

                        e.Cancel = true;
                        return;
                    }
                }
                //  toolStripStatusLabel1.Text = "";
                // toolStripProgressBar1.Value = 0;

            }
            else if (radioButton2.Checked || radioButton3.Checked)
            {
              //  toolStripProgressBar1.Value = 0;
                ArrayList emp = emSvr.GetCurrentEmployees();
          
                DataTable dtb = dt.AnnualTable;
                int i = 0;
            //    toolStripProgressBar1.Maximum = emp.Count;
                foreach (EmployeeInfo var in emp)
                {
                   // toolStripStatusLabel1.Text = "Calculating :" + var.Code;
                    if (var.EmployeeType != "M")
                    {
                        ArrayList annu = emLvSvr.GetAnnualTotal(var.Code, DateTime.Today,false);
                        if (annu.Count > 0)
                        {
                            DataRow dr = dt.AnnualTable.NewRow();
                            //  DataGridViewRow dr = new DataGridViewRow();
                            //dr.CreateCells(dataGridView1);
                            // DataRow dr = dt.AnnualTable.NewAnnualTableRow();
                            dr["DVCD"] = var.Division.Code;
                            dr["DV_Name"] = var.Division.Name;
                            //  dr.ItemArray[0] = var.Division.Name;
                            dr["Line"] = var.WorkGroupLine;
                            dr["OTLine"] = var.OtGroupLine;
                            dr["Code"] = var.Code;
                            dr["Name"] = var.NameInEng.ToString();
                             dr["Position"] = var.Position.Code;
                            //  dr["JoinDate"] = var.JoinDate.ToShortDateString();


                            //  AnnualTotal antL = (AnnualTotal)annu[annu.Count - 2];
                            //  dr["RemainLastYear"] = antL.RemainHr;

                            AnnualTotal antt = (AnnualTotal)annu[annu.Count - 1];
                            // dr["GetThisYear"] = antt.Get.ToString();
                            dr["Total_Day"] = antt.Total / 525d;
                            dr["Remain_Day"] = antt.Remain / 525d;
                            //  dr["Total"] = antt.TotalText;
                            // dr["Exceed"] = MnToText(((antt.Get * 525d + antL.Remain) - 12d * 525d) < 0 ? 0 : ((antt.Get * 525d + antL.Remain) - 12d * 525d));
                            dr["Used_Day"] = antt.Use / 525d;

                            dt.AnnualTable.Rows.Add(dr);

                        }
                    }
                    i++;
                    bwAsync.ReportProgress(Convert.ToInt32(i * (100.0 / emp.Count)));

                    if (bwAsync.CancellationPending)
                    {
                        // Pause for a bit to demonstrate that there is time between

                        // "Cancelling..." and "Cancel ed".

                        Thread.Sleep(1200);

                        // Set the e.Cancel flag so that the WorkerCompleted event

                        // knows that the process was cancelled.

                        e.Cancel = true;
                        return;
                    }
                   // toolStripProgressBar1.Value++;
                  //  this.Update();
                }
               // toolStripStatusLabel1.Text = "";
               // toolStripProgressBar1.Value = 0;

            }
            else if (radioButton4.Checked)
            {
                
            }
        }

        private void m_AsyncWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
          stsMgr.Progress = e.ProgressPercentage;
        }


        private void m_AsyncWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Cursor = Cursors.Default;
         stsMgr.Progress = 0;
            btnGenerate.Text = "Generate";
            btnGenerate.Enabled = true;
            this.Activate();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }

            // Check to see if the background process was cancelled.

            if (e.Cancelled)
            {
                stsMgr.Status = "Cancelled...";
            }
            else
            {

                stsMgr.Status = "Completed...";
            }

            if (radioButton1.Checked || radioButton5.Checked)
            {
                Reports.Rpt_AnnualSumary annuSum = new Rpt_AnnualSumary();

                annuSum.SetDataSource(dt);

                ParameterDiscreteValue pdv = new ParameterDiscreteValue();
          
                ParameterField pf = new ParameterField();
       
                ParameterFields pfs = new ParameterFields();

                pf.Name = "printdate";
         
                pdv.Value =  dtpAnnu.Value.Date;
          
                pf.CurrentValues.Add(pdv);
 
                pfs.Add(pf);
 
                crystalReportViewer1.ParameterFieldInfo = pfs;
                crystalReportViewer1.ReportSource = annuSum;


            }
            else if (radioButton2.Checked) 
            {
            
                Reports.Rpt_AnnualSumaryOtLine annuSum = new Rpt_AnnualSumaryOtLine();
                annuSum.SetDataSource(dt);
                crystalReportViewer1.ReportSource = annuSum;

            }
            else if (radioButton3.Checked)
            {   
                 Reports.Rpt_CurrentAnnual annuSum = new  Rpt_CurrentAnnual();
                annuSum.SetDataSource(dt);
                crystalReportViewer1.ReportSource = annuSum;
            }
            stsMgr.Status = "Ready";


        }

        private void txtAnnuYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressManager.EnterNumericOnly(e);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
            dtpAnnu.Enabled = false;
            if (radioButton1.Checked|| radioButton5.Checked)
            {
                dtpAnnu.Enabled = true;
            }
            else if(radioButton4.Checked)
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
            }
        }
    }
}
