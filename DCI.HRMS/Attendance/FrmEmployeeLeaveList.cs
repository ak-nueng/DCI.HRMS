using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using DCI.HRMS.Service;
using System.Collections;
using DCI.HRMS.Model.Personal;

namespace DCI.HRMS.Attendance
{
    public partial class FrmEmployeeLeaveList : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public FrmEmployeeLeaveList()
        {
            InitializeComponent();
        }

        

        private void FrmEmployeeLeaveList_Load(object sender, EventArgs e)
        {

        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            
            ArrayList allCurEmp = EmployeeService.Instance().GetCurrentEmployees();

            foreach (EmployeeInfo emp in allCurEmp)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    dgvLeaveResult.Rows.Add(
                        emp.Code, emp.JoinDate.ToString("dd/MM/yyyy"), "", "", "", "", ""
                    );
                });
            }



            if (!backgroundWorker1.IsBusy) {
                backgroundWorker1.RunWorkerAsync();
            }

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach(DataGridViewRow drEmp in dgvLeaveResult.Rows){

                if(drEmp.Cells["colCode"].Value.ToString() != ""){
                    ArrayList annu = EmployeeLeaveService.Instance().GetAnnualTotal(drEmp.Cells["colCode"].Value.ToString(), DateTime.Today, true);
                    if (annu.Count != 0)
                    {

                        DataTable antb = ServiceUtility.ToDataTable(annu);
                        foreach(DataRow drAnn in antb.Rows){
                            if(drAnn["Year"].ToString() == "2016"){
                                drEmp.Cells["ColGet"].Value = drAnn["Get"].ToString();
                                drEmp.Cells["ColUse"].Value = drAnn["Use"].ToString();
                                drEmp.Cells["ColUseText"].Value = drAnn["UseText"].ToString();
                                drEmp.Cells["ColRemain"].Value = drAnn["Remain"].ToString();
                                drEmp.Cells["ColRemainHr"].Value = drAnn["RemainHr"].ToString();
                            }
                        }
                        
                    }
                }

                
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("COMPLETE!");
        }
    }
}