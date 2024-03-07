using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DCI.HRMS.Common;
using System.Collections;
using DCI.Security.Model;
using DCI.Security.Service;
using DCI.HRMS.Base;

namespace DCI.HRMS.Security
{
    public partial class Frm_GroupPermision : Form, IFormPermission
    {
        private PermissionInfo perm = new PermissionInfo();
        private UserGroupService userGroup = UserGroupService.Instance();
        private UserGroupInfo usrGrp = new UserGroupInfo();
        private UserAccountManager usrMgr = new UserAccountManager();
        private UserGroupService grpSvr = UserGroupService.Instance();
        private UserGroupPermission information = new UserGroupPermission();
        public Frm_GroupPermision(UserGroupInfo grp)
        {
            InitializeComponent();
            usrGrp = grp;
            this.Text = "Permission of " + usrGrp.Name;
        }
        private readonly string[] colName = new string[] { "AccountId", "FullName", "Description", "Email", "Enable" };
        private readonly string[] propName = new string[] { "AccountId", "FullName", "Description", "Email", "Enable" };
        private readonly int[] width = new int[] { 80, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 };
        private ArrayList gvData = new ArrayList();


        private void LoadMenu()
        {
            try
            {


                ApplicationManager appMgr = ApplicationManager.Instance();
                int userGroupId = appMgr.UserAccount.UserGroup.ID;
                ArrayList allowMenuList = userGroup.GetAllowModules(userGroupId, ApplicationType.WINDOWS);

                TreeNode mainNode = new TreeNode("Roles for " + appMgr.UserAccount.UserGroup.Name);
                mainNode.SelectedImageKey = "Menu";
                mainNode.StateImageKey = "Menu";

                if (allowMenuList != null && allowMenuList.Count > 0)
                {
                    foreach (ModuleInfo menu in allowMenuList)
                    {
                        // trvMenu.Nodes.Add(AddSubMenuToTreeNode(menu));
                        //AddSubMenuToTreeNode(menu, mainNode);
                        int temp = FormCount(menu);
                        if (temp == 0)
                        {
                            menu.visible = false;

                        }
                        else
                        {
                            TreeNode chNode = AddSubMenuToTreeNode(menu);
                            if (chNode.Name != "" && menu.visible)
                            {
                                chNode.Expand();
                                mainNode.Nodes.Add(chNode);
                            }
                        }
                    }
                }

                trvMenu.Nodes.Add(mainNode);
                mainNode.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int FormCount(ModuleInfo menu)
        {
            int formCount = 0;
            if (menu.Type != ModuleType.Menu)
            {
                return 1;
            }
            else
            {
                if (menu.SubModules != null)
                {
                    foreach (ModuleInfo subMod in menu.SubModules)
                    {
                        int temp = FormCount(subMod);
                        if (temp == 0)
                        {
                            subMod.visible = false;
                        }
                        else
                        {
                            formCount += temp;
                        }
                    }
                }
                return formCount;
            }
        }

        private TreeNode AddSubMenuToTreeNode(ModuleInfo menu)
        {

            TreeNode childNode = new TreeNode(menu.Name);

            if (menu.SubModules != null && menu.SubModules.Count > 0 && menu.visible)
            {


                foreach (ModuleInfo subMenu in menu.SubModules)
                {
                    TreeNode chNode = AddSubMenuToTreeNode(subMenu);
                    if (chNode.Name != "")
                        childNode.Nodes.Add(chNode);
                }
            }
            if (menu.Type == ModuleType.Menu)
            {

            }
            childNode.Name = menu.Id;
            childNode.ToolTipText = menu.Description;
            childNode.ImageKey = menu.Icon;
            childNode.SelectedImageKey = menu.Icon;
            childNode.StateImageKey = menu.Icon;
            childNode.Tag = menu.Type;
            return childNode;

        }

        private void AddGridViewColumnsUsr()
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
        private void FillDataGridUsr()
        {
            dgItems.DataBindings.Clear();
            dgItems.DataSource = null;

            dgItems.DataSource = gvData;
            this.Update();
        }
        private void Frm_GroupPermision_Load(object sender, EventArgs e)
        {


            trvMenu.Nodes.Clear();

            /*****  Add Menu Pictures List *********/
            this.menuImages.TransparentColor = System.Drawing.Color.Transparent;
            this.menuImages.Images.Add("HRMS", global::DCI.HRMS.Properties.Resources.alert);

            this.menuImages.Images.Add("access", global::DCI.HRMS.Properties.Resources.access);
            this.menuImages.Images.Add("aim", global::DCI.HRMS.Properties.Resources.aim);
            this.menuImages.Images.Add("aimpo", global::DCI.HRMS.Properties.Resources.aim_protocol);
            this.menuImages.Images.Add("administration", global::DCI.HRMS.Properties.Resources.administration);
            this.menuImages.Images.Add("antivirus", global::DCI.HRMS.Properties.Resources.antivirus);
            this.menuImages.Images.Add("Business", global::DCI.HRMS.Properties.Resources.gnome_keyboard_layout);
            this.menuImages.Images.Add("blog", global::DCI.HRMS.Properties.Resources.Blog);
            this.menuImages.Images.Add("cardtime", global::DCI.HRMS.Properties.Resources.card_machine);
            this.menuImages.Images.Add("calendar", global::DCI.HRMS.Properties.Resources.Calendar);
            this.menuImages.Images.Add("date", global::DCI.HRMS.Properties.Resources.date);
            this.menuImages.Images.Add("discussion", global::DCI.HRMS.Properties.Resources.discussion);
            this.menuImages.Images.Add("dms", global::DCI.HRMS.Properties.Resources.dms);
            this.menuImages.Images.Add("family", global::DCI.HRMS.Properties.Resources.generic);
            this.menuImages.Images.Add("family2", global::DCI.HRMS.Properties.Resources.Generic2);
            this.menuImages.Images.Add("dollar", global::DCI.HRMS.Properties.Resources.dollar_sign);
            this.menuImages.Images.Add("globe", global::DCI.HRMS.Properties.Resources.globe);
            this.menuImages.Images.Add("group_add", global::DCI.HRMS.Properties.Resources.group_add);
            this.menuImages.Images.Add("helmet", global::DCI.HRMS.Properties.Resources.helmet);
            this.menuImages.Images.Add("home", global::DCI.HRMS.Properties.Resources.home);
            this.menuImages.Images.Add("icq", global::DCI.HRMS.Properties.Resources.icq_protocol);
            this.menuImages.Images.Add("identity", global::DCI.HRMS.Properties.Resources.identity);
            this.menuImages.Images.Add("kedit", global::DCI.HRMS.Properties.Resources.kedit);
            this.menuImages.Images.Add("kmenuedit", global::DCI.HRMS.Properties.Resources.kmenuedit);
            this.menuImages.Images.Add("kalarm", global::DCI.HRMS.Properties.Resources.karm);
            this.menuImages.Images.Add("keyboard", global::DCI.HRMS.Properties.Resources.keyboard);
            this.menuImages.Images.Add("Leave", global::DCI.HRMS.Properties.Resources.yast_YaST);
            this.menuImages.Images.Add("Menu", global::DCI.HRMS.Properties.Resources.Menu);
            this.menuImages.Images.Add("Magnet", global::DCI.HRMS.Properties.Resources.magmet);
            this.menuImages.Images.Add("Ot", global::DCI.HRMS.Properties.Resources.yast_timezone);
            this.menuImages.Images.Add("OtRate", global::DCI.HRMS.Properties.Resources.yast_timezone2);
            this.menuImages.Images.Add("scale", global::DCI.HRMS.Properties.Resources.Scale);
            this.menuImages.Images.Add("starthear", global::DCI.HRMS.Properties.Resources.starthere);
            this.menuImages.Images.Add("timecard", global::DCI.HRMS.Properties.Resources.clock_settings);
            this.menuImages.Images.Add("todo", global::DCI.HRMS.Properties.Resources.todo);
            this.menuImages.Images.Add("transfer", global::DCI.HRMS.Properties.Resources.My_Recieved_Filesr2);
            this.menuImages.Images.Add("up", global::DCI.HRMS.Properties.Resources.up);
            this.menuImages.Images.Add("protect", global::DCI.HRMS.Properties.Resources.protect_blue);
            this.menuImages.Images.Add("tools", global::DCI.HRMS.Properties.Resources.tools);

            /******************************************/
            LoadMenu();

            txtGrpId.Text = usrGrp.ID.ToString();
            txtGrpName.Text = usrGrp.Name;
            txtGrpDesc.Text = usrGrp.Description;
            chkGrpEnable.Checked = usrGrp.Enable;
            usrGrp.Users = usrMgr.findUserAccountByGroup(usrGrp.ID.ToString());

            AddGridViewColumnsUsr();
            gvData = usrGrp.Users;
            FillDataGridUsr();


        }

        #region IFormPermission Members

        public PermissionInfo Permission
        {
            set { perm = value; }
        }

        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (information == null)
            {
                try
                {
                    information = new UserGroupPermission();
                    information.GroupInfo = usrGrp;
                    information.GroupModuleInfo = new ModuleInfo();
                    information.GroupModuleInfo.Id = lblNodeName.Text;
                    GetDataInfo();
                    grpSvr.SaveGroupPermission(information);
                    MessageBox.Show("บันทึกข้อมูลเรียบร้อยแล้ว", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    GetDataInfo();
                    grpSvr.UpdateGroupPermission(information);
                    MessageBox.Show("บันทึกข้อมูลเรียบร้อยแล้ว", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้เนื่องจาก\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void trvMenu_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
                ClearSts();
                try
                {
                    if ((ModuleType)e.Node.Tag != ModuleType.Menu)
                    {
                        btnSave.Enabled = true;
                        information = grpSvr.GetGroupPermission(usrGrp.ID, e.Node.Name);
                        lblNodeName.Text = e.Node.Name;
                        kryptonHeaderGroup4.ValuesPrimary.Heading = "Permission Information: " + e.Node.Text;
                        if (information != null)
                        {
                            fillDataInfo();
                        }


                    }   
                    else
                    {
                        information = null;
                        lblNodeName.Text = "";
                        kryptonHeaderGroup4.ValuesPrimary.Heading = "Permission Information:";
                        btnSave.Enabled = false;
                    }
                }
                catch 
                {
                    information = null;
                    lblNodeName.Text = "";
                    kryptonHeaderGroup4.ValuesPrimary.Heading = "Permission Information:";
                    btnSave.Enabled = false;
                }
        }
        private void fillDataInfo()
        {
            chkView.Checked = information.ViewEnable;
            chkPrint.Checked = information.PrintEnable;
            chkNew.Checked = information.AddNewEnable;
            chkExport.Checked = information.ExportEnable;
            chkEdit.Checked = information.EditEnable;
            chkDelete.Checked = information.DeleteEnable;
            chkChangeDoc.Checked = information.ChangeDocumentStatusEnable;
        }
        private void GetDataInfo()
        {
            information.ViewEnable = chkView.Checked;
            information.PrintEnable = chkPrint.Checked;
            information.AddNewEnable = chkNew.Checked;
            information.ExportEnable = chkExport.Checked;
            information.EditEnable = chkEdit.Checked;
            information.DeleteEnable = chkDelete.Checked;
            information.ChangeDocumentStatusEnable = chkChangeDoc.Checked;
        }

        private void ClearSts()
        {
            foreach (Control item in kryptonPanel1.Controls)
            {
                if (item is CheckBox)
                {
                    CheckBox chk = (CheckBox)item;
                    chk.Checked = false;
                }
                
            }
        }
    }
}
