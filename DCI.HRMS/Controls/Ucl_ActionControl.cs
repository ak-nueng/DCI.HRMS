using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Base;
using DCI.Security.Model;
using DCI.HRMS.Util;

namespace DCI.HRMS.Controls
{
   
    public partial class Ucl_ActionControl : UserControl
    {
        private FormActionType curAct = FormActionType.None;
        private PermissionInfo permission = null;
        private IForm owner = null;
        private StatusManager stsMng = new StatusManager();

        public Ucl_ActionControl()
        {
            InitializeComponent();
            CurrentAction = FormActionType.None;
        }

        internal PermissionInfo Permission
        {
            get { return permission; }
            set 
            { 
                permission = value;
                EnableAction();
            }
        }
        internal FormActionType CurrentAction
        {
            set
            {
                curAct = value;
                EnableAction();
            }
            get{ return curAct; }
        }

        private void EnableAction()
        {
            if (permission == null)
            {
                EnableAction_WithOut_Permission();
            }
            else
            {
                EnableAction_By_Permission();
            }
        }

        private void EnableAction_By_Permission()
        {
            switch (CurrentAction)
            {
                case FormActionType.AddNew:
                    CurrentAction = FormActionType.SaveAs;
                    break;
                case FormActionType.SaveAs:
                    btnAddNew.Enabled = true;
                    btnSave.Enabled = true;
                    btnDelete.Enabled = false;
                    btnRefresh.Enabled = false;
                    btnPrint.Enabled = false;
                    btnExport.Visible = true;

                    if (!permission.AllowAddNew)
                    {
                        btnAddNew.Enabled = false;
                        btnSave.Enabled = false;
                    }
                    if (!permission.AllowExportData && !(owner is IFormChild))
                        btnExport.Visible = false;

                    break;
                case FormActionType.Save:
                    btnAddNew.Enabled = true;
                    btnSave.Enabled = true;
                    btnDelete.Enabled = true;
                    btnRefresh.Enabled = true;
                    btnPrint.Enabled = true;
                    btnExport.Visible = true;

                    if (!permission.AllowAddNew)
                        btnAddNew.Enabled = false;
                    if (!permission.AllowEdit)
                        btnSave.Enabled = false;
                    if (!permission.AllowDelete)
                        btnDelete.Enabled = false;
                    if (!permission.AllowExportData && !(owner is IFormChild))
                        btnExport.Visible = false;
                    break;
                case FormActionType.Delete:
                    btnAddNew.Enabled = false;
                    btnSave.Enabled = false;
                    btnDelete.Enabled = false;
                    btnRefresh.Enabled = false;
                    btnPrint.Enabled = false;
                    btnExport.Visible = false;
                    break;
                case FormActionType.None:
                    Form_Lazy();
                    break;
                case FormActionType.Search:
                    Form_Lazy();
                    break;
                default:
                    break;
            }

            if (owner is IFormChild)
                btnExport.Visible = false;
        }

        private void Form_Lazy()
        {
            btnAddNew.Enabled = true;
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
            btnRefresh.Enabled = false;
            btnPrint.Enabled = false;
            btnExport.Visible = true;

            if (!permission.AllowAddNew)
                btnAddNew.Enabled = false;
            if (!permission.AllowExportData && !(owner is IFormChild))
                btnExport.Visible = false;
        }

        private void EnableAction_WithOut_Permission()
        {
            switch (CurrentAction)
            {
                case FormActionType.AddNew:
                    CurrentAction = FormActionType.SaveAs;
                    break;
                case FormActionType.SaveAs:
                    btnAddNew.Enabled = true;
                    btnSave.Enabled = true;
                    btnDelete.Enabled = false;
                    btnRefresh.Enabled = false;
                    btnPrint.Enabled = false;
                    btnExport.Visible = true;
                    break;
                case FormActionType.Save:
                    btnAddNew.Enabled = true;
                    btnSave.Enabled = true;
                    btnDelete.Enabled = true;
                    btnRefresh.Enabled = true;
                    btnPrint.Enabled = true;
                    btnExport.Visible = true;
                    break;
                case FormActionType.Delete:
                    btnAddNew.Enabled = false;
                    btnSave.Enabled = false;
                    btnDelete.Enabled = false;
                    btnRefresh.Enabled = false;
                    btnPrint.Enabled = false;
                    btnExport.Visible = true;
                    break;
                case FormActionType.None:
                    btnAddNew.Enabled = true;
                    btnSave.Enabled = false;
                    btnDelete.Enabled = false;
                    btnRefresh.Enabled = false;
                    btnPrint.Enabled = false;
                    btnExport.Visible = true;
                    break;
                default:
                    break;
            }
        }

        internal IForm Owner
        {
            set 
            { 
                owner = value;
                if (owner is IFormChild)
                {
                    btnGoToHome.Visible = false;
                    btnPrint.Visible = false;
                    btnClose.Visible = false;
                    btnExport.Visible = false; 
                    tss1.Visible = false;
                    tss2.Visible = false;
                    tss3.Visible = false;
                }
                else
                {
                    btnGoToHome.Visible = true;
                    btnPrint.Visible = true;
                    btnClose.Visible = true;
                    tss1.Visible = true;
                    tss2.Visible = true;
                    tss3.Visible = true;
                }
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                stsMng.Status="Add New Record";
                owner.AddNew();
                //CurrentAction = FormActionType.SaveAs;
                stsMng.Status="Ready";
            }
            catch { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            stsMng.Status="Saving";
            
            owner.Save();
            
            stsMng.Status="Ready";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
             stsMng.Status="Deleting";
            owner.Delete();
            stsMng.Status="Ready";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            stsMng.Status="Refresh";
            owner.RefreshData();
            stsMng.Status="Ready";
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            stsMng.Status="Printing";
            owner.Print();
            stsMng.Status="Ready";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            owner.Exit();
        }

        internal void OnActionKeyDown(object sender, KeyEventArgs e)
        {
            //Save , Save As
            if (e.KeyCode == Keys.F2)
            {
                if (btnSave.Enabled && btnSave.Visible)
                {
                    stsMng.Status="Saving";
                    owner.Save();
               
                }
            }
            else if (e.KeyCode == Keys.F3)
            {
                if (btnAddNew.Enabled && btnAddNew.Visible)
                {
                    stsMng.Status="Add New Record";
                    owner.AddNew();
                    CurrentAction = FormActionType.SaveAs;
              
                }
            }
        
            else if (e.KeyCode == Keys.F4)
            {
               stsMng.Status="Searching";
                CurrentAction = FormActionType.Search;
                owner.Search();
             
            }
            else if (e.KeyCode == Keys.F5)
            {
                if (btnRefresh.Enabled && btnRefresh.Visible)
                {
                    stsMng.Status="Refresh";
                    owner.RefreshData();
                   
                }
            }
            else if (e.KeyCode == Keys.F6)
            {
                if (btnPrint.Enabled && btnPrint.Visible)
                {
                     stsMng.Status="Printing";
                    owner.Print();
                    
                }
            }
            else if (e.KeyCode == Keys.F9)
            {
                if (btnDelete.Enabled && btnDelete.Visible)
                {
                     stsMng.Status="Deleting Record";
                    owner.Delete();
                    CurrentAction = FormActionType.SaveAs;
                  
                }
            }
            else if (e.KeyCode == Keys.F10)
            {
                if (btnClose.Enabled && btnClose.Visible)
                {
                    try
                    {
                        this.ParentForm.Close();
                    }
                    catch { }
                }
            }
            else if (e.KeyCode == Keys.F11)
            {
                if (btnGoToHome.Visible)
                    GoHome();
            }    
            stsMng.Status="Ready";
        }

        private void btnGoToHome_Click(object sender, EventArgs e)
        {
            GoHome();
        }

        private void GoHome()
        {
            try
            {
                Form frm = this.ParentForm;
                Frm_MainForm mdi = (Frm_MainForm )frm.MdiParent;
                mdi.kryptonHeaderGroup1_MouseEnter(this, new MouseEventArgs(MouseButtons.Left,1,MousePosition.X,MousePosition.Y,1));
            }
            catch { }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            owner.Export();
        }
    }
}
