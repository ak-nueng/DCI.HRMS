using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DCIBizPro.Business.SM;
using DCIBizPro.DTO.SM;
using System.Diagnostics;
using DCI.HRMS.Util;
using DCI.HRMS.Base;

namespace DCI.HRMS
{
    public partial class Frm_MainMenu : Form
    {
        private readonly PermissionController permController = PermissionController.Instance();

        public Frm_MainMenu()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Frm_MainMenu_Load(object sender, EventArgs e)
        {
            lblTitle.Text = "User menu for " + ApplicationContext.Current.Account.AccountId;

            OpenMainMenu();

            try
            {
                lvMenu.Items[0].Selected = true;
                OpenSubMenu();
            }
            catch { }
        }

        private void OpenMainMenu()
        {
            try
            {
                ArrayList mainModules = permController.GetAllowMainModules(ApplicationContext.Current.Account.UserGroup.ID, ApplicationType.Windows);
                
                if (mainModules != null)
                {
                    lvMenu.Items.Clear();
                    foreach (ModuleInfo mainModule in mainModules)
                    {
                        ListViewItem item = new ListViewItem();
                        item.ToolTipText = mainModule.GuID;
                        item.SubItems[0].Text = mainModule.Name;
                        item.ImageIndex = 0;
                        item.StateImageIndex = 0;
                        lvMenu.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void lvMenu_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //OpenSubMenu();
        }

        private void OpenSubMenu()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                string mainModuleId = lvMenu.SelectedItems[0].ToolTipText;
                int menuNo = lvMenu.SelectedItems[0].Index + 1;
                int subMenuNo = 1;

                lvSubMenu.Groups.Clear();
                lvSubMenu.Items.Clear();

                ArrayList modules = permController.GetAllowModules(ApplicationContext.Current.Account.UserGroup.ID, mainModuleId, ApplicationType.Windows);
                
                if (modules.Count > 0)
                {
                    ListViewGroup[] groups = new ListViewGroup[modules.Count];

                    for (int index = 0; index < modules.Count; index++)
                    {
                        ModuleInfo module = (ModuleInfo)modules[index];
                        groups[index] = new ListViewGroup();
                        groups[index].Header = module.Name;
                        groups[index].Name = module.Name;
                    }
                    lvSubMenu.Groups.AddRange(groups);

                    for (int index = 0; index < modules.Count; index++)
                    {
                        ModuleInfo module = (ModuleInfo)modules[index];
                        if (module.ModuleChildren.Count > 0)
                        {
                            int componentNo = 1;
                            foreach (ModuleInfo component in module.ModuleChildren)
                            {
                                ListViewItem subItem = new ListViewItem();
                                subItem.Group = groups[index];
                                subItem.Font = lvSubMenu.Font;
                                
                                if (component.ModuleType == "FRM")
                                {
                                    subItem.Text = string.Format("{0}.{1}.{2}", menuNo, subMenuNo, componentNo);
                                    subItem.ToolTipText = component.GuID;
                                    subItem.SubItems.Add(component.Name);
                                    subItem.SubItems.Add(component.Description);
                                    subItem.StateImageIndex = 0;
                                    
                                    componentNo++;
                                }
                                else if(component.ModuleType == "FRMGRP")
                                {
                                    subItem.ToolTipText = "";
                                    subItem.Text = component.Name;
                                    subItem.StateImageIndex = -1;
                                    subItem.Font = new Font(lvSubMenu.Font, FontStyle.Bold);
                                }

                                lvSubMenu.Items.Add(subItem);
                            }
                            ListViewItem emptyItem = new ListViewItem();
                            emptyItem.ToolTipText = string.Empty;
                            lvSubMenu.Items.Add(emptyItem);
                        }
                        subMenuNo++;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnOpenMenu_Click(object sender, EventArgs e)
        {
            OpenComponentMenu();
        }

        private void OpenComponentMenu()
        {
            /**
            try
            {
                string moduleComponentId = lvSubMenu.SelectedItems[0].ToolTipText;
                if (moduleComponentId != string.Empty 
                    || moduleComponentId != "")
                {
                    this.Cursor = Cursors.WaitCursor;

                    ModuleInfo modInfo = permController.Get(ApplicationContext.Current.Account.UserGroupId, moduleComponentId);

                    object obj = (object)FormUtil.CreateForm(modInfo.NameSpace, modInfo.ClassName);
                    Form oForm = (Form)obj;
                    Frm_LocalHome frmLocal = (Frm_LocalHome)this.MdiParent;

                    frmLocal.OpenFormChild(oForm);
                    if (obj is IFormPermission)
                    {
                        IFormPermission oForm_Perm = (IFormPermission)obj;
                        oForm_Perm.Permission = modInfo.Permission;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
             * **/
        }

        private void lvSubMenu_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenComponentMenu();
        }

        private void lvMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            OpenSubMenu();
        }

        private void lvMenu_MouseClick(object sender, MouseEventArgs e)
        {
            //OpenSubMenu();
        }

        private void lvMenu_SizeChanged(object sender, EventArgs e)
        {
            lvMenu.View = View.LargeIcon;
        }
    }
}