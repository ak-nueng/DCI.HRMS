using System;
using System.ComponentModel;
using System.Windows.Forms;
using DCI.HRMS.Panes;
using DCI.Security.Service;
using DCI.HRMS.Common;
using DCI.Security.Model;
using System.Collections;
using System.Diagnostics;
using DCI.HRMS.Base;
using DCI.HRMS.Util;
using System.Data;
using DCI.HRMS.Service;
using DCI.HRMS.Properties;
using ComponentFactory.Krypton.Toolkit;
using DCI.HRMS.Service.SubContract;
using DCI.HRMS.Service.Trainee;
using Oracle.ManagedDataAccess;


namespace DCI.HRMS
{
    public partial class Frm_MainForm :KryptonForm
    {
  
        private int childFormNumber = 0;
        private StatusManager stMng ;
        private readonly string[] colName = new string[] { "รหัส", "ชื่อ-สกุล", "ตำแหน่ง", "หน่วยงาน" ,"โทร"};
        private readonly string[] propName = new string[] { "Code", "Tname", "POSI_TNAME", "DV_TNAME" ,"Telephone"};
        private DataTable emTb = new DataTable();

        private readonly int[] width = new int[] { 80, 80, 300, 100,50 };

        //private string oradb = "Data Source=(DESCRIPTION="
        //     + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=DCIDMC)(PORT=1521)))"
        //     + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL)));"
        //     + "User Id=DCI;Password=dcipsn;";

        //private string oradbSUB = "Data Source=(DESCRIPTION="
        //         + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=DCIDMC)(PORT=1521)))"
        //         + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL)));"
        //         + "User Id=DCITC;Password=dcisub;";

        //private string oradbTRN = "Data Source=(DESCRIPTION="
        //         + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=DCIDMC)(PORT=1521)))"
        //         + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL)));"
        //         + "User Id=DEV_OFFICE;Password=developo;";

        public Frm_MainForm()
        {
            InitializeComponent();
        }


        #region Menu Strip Events
        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
                // TODO: Add code here to open the file.
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
                // TODO: Add code here to save the current contents of the form to a file.
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Use System.Windows.Forms.Clipboard to insert the selected text or images into the clipboard
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Use System.Windows.Forms.Clipboard to insert the selected text or images into the clipboard
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Use System.Windows.Forms.Clipboard.GetText() or System.Windows.Forms.GetData to retrieve information from the clipboard.
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip1.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sBar.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
        private void Frm_MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kryptonHeaderGroup1.Visible = menuToolStripMenuItem.Checked;
        }
        #endregion


        public void OpenFormChild(Form form)
        {
            bool exist = false;

            foreach (Form frm in this.MdiChildren)
            {
                if (frm.Name.ToLower() == form.Name.ToLower())
                {
                    exist = true;
                    form = frm;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Activate();
                    break;
                }
            }
            if (!exist)
            {
                form.WindowState = FormWindowState.Maximized;
                form.MdiParent = this;
                form.Show();
            }
        }
        private bool CheckExistingForm(string formName)
        {
            bool flag = false;

            foreach (Form f in this.MdiChildren)
            {
                if (f.Name.ToUpper() == formName.ToUpper())
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        private void LoadMenu()
        {            
            try
            {
                this.kryptonHeaderGroup1.ValuesPrimary.Image = menuImages.Images["kmenuedit"];

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
                mainNode.Expand();
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
                ToolStripSeparator menuSpliter = new ToolStripSeparator();
                toolStrip1.Items.Add(menuSpliter);
                foreach (ModuleInfo subMenu in menu.SubModules)
                {
                    TreeNode chNode = AddSubMenuToTreeNode(subMenu);
                    if (chNode.Name != "")
                        childNode.Nodes.Add(chNode);
                }
            }
            if (menu.Type != ModuleType.Menu)
            {
                ToolStripButton menuButt = new ToolStripButton(menu.Name, menuImages.Images[menu.Icon], new System.EventHandler(this.toolMenu_Click));
                menuButt.Name = menu.Id;
                menuButt.ToolTipText = menu.Description;
                menuButt.ImageScaling = ToolStripItemImageScaling.SizeToFit;
                menuButt.ImageTransparentColor = System.Drawing.Color.White;
                menuButt.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter; 
                menuButt.TextImageRelation = TextImageRelation.ImageAboveText;
                toolStrip1.Items.Add(menuButt);
            }

            childNode.Name = menu.Id;
            childNode.ToolTipText = menu.Description;
            childNode.ImageKey = menu.Icon;
            childNode.SelectedImageKey = menu.Icon;
            childNode.StateImageKey = menu.Icon;
            //if (menu.visible)
           // {
                return childNode;
          //  }
            // else

           //   return new TreeNode();

        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            stMng = new StatusManager(this);
            this.Cursor = Cursors.WaitCursor;
            kryptonHeaderGroup1.ValuesPrimary.Heading = "User menu for " + ApplicationManager.Instance().UserAccount.AccountId;
            trvMenu.Nodes.Clear();

            /*****  Add Menu Pictures List *********/
            this.menuImages.TransparentColor = System.Drawing.Color.Transparent;
            this.menuImages.Images.Add("HRMS", global::DCI.HRMS.Properties.Resources.alert);

            this.menuImages.Images.Add("medical", global::DCI.HRMS.Properties.Resources.medical_suitecase_icon32);
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
            this.menuImages.Images.Add("Generic2", global::DCI.HRMS.Properties.Resources.Generic2);
            this.menuImages.Images.Add("Penalty", global::DCI.HRMS.Properties.Resources.Penalty);
            /******************************************/


            PaletteModeManager pl = DCI.HRMS.Properties.Settings.Default.Skin;
            foreach (ToolStripMenuItem var in skinToolStripMenuItem.DropDownItems)
            {
                if (var.Tag.ToString().CompareTo(pl.ToString()) == 0)
                {
                    var.Checked = true;

                }
                else
                {
                    var.Checked = false;
                }
            }
            this.Update();
            FrmLogin frmLgn = (FrmLogin)this.Owner;
            
            frmLgn.FormStatus("Loading Menu");
            stMng.Status = "Loading Menu";
            this.LoadMenu();
            frmLgn.FormStatus("Loading All Employee  Data");
            stMng.Status = "Loading All Employee  Data";
            
            frmLgn.FormStatus("Ready");
            stMng.Status = "Ready";
            this.Cursor = Cursors.Default;

            frmLgn.HideForm();

            this.LoadEmployeeData();
        }
        private void LoadEmployeeData()
        {
            
            AddGridViewColumns();
            DataSet emp = new DataSet();
            try {
                emp = EmployeeService.Instance().FindAllEmp();
                if(emp != null){
                    emTb = emp.Tables["EMPM"];
                }
            }
            catch (Exception ex) { MessageBox.Show("Error(DCI): "+ex.ToString()); }


            DataSet sub = new DataSet();

            try
            {
                sub = SubContractService.Instance().FindAllEmp();

                DataTable subTb = new DataTable();
                if (sub != null)
                {
                    subTb = sub.Tables["EMPM"];
                    if (subTb.Rows.Count > 0)
                    {
                        foreach (DataRow item in subTb.Rows)
                        {
                            emTb.Rows.Add(item.ItemArray);
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error(SUB): " + ex.ToString()); }


            DataSet trn = new DataSet();
            try
            {
                trn = TraineeService.Instance().FindAllEmp();

                DataTable trnTb = new DataTable();
                if(trn != null)
                {
                    trnTb = trn.Tables["EMPM"];
                    if(trnTb.Rows.Count > 0)
                    {
                        foreach (DataRow drTrn in trnTb.Rows)
                        {
                            emTb.Rows.Add(drTrn.ItemArray);
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error(TRN): " + ex.ToString()); }
        

            
            

            //  dgItems.DataSource = em;
        }
        private void AddGridViewColumns()
        {
            this.dgItems.Columns.Clear();
            this.dgItems.AutoGenerateColumns = false;
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

        private void mSubItem_MainMenu_Click(object sender, EventArgs e)
        {
            HideMenu();
        }

        private void HideMenu()
        {
            if (hideMenu == false)
            {
                // panel1.Visible = false;
                hideMenu = true;
            }
            else
            {
                // panel1.Visible = true;
                hideMenu = false;
            }
        }

        public bool HiddenMenu
        {
            get { return hideMenu; }
            set
            {
                hideMenu = value;
                HideMenu();
            }
        }
        private void mSubItem_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Frm_LocalHome_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void trvMenu_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                if (e.Node.IsExpanded)
                {
                    // e.Node.StateImageIndex = 1;
                }
                else
                {
                    //  e.Node.StateImageIndex = 0;
                }
            }
        }

        private void trvMenu_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }



        private void trvMenu_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Nodes.Count == 0)
            {
                generattForm(e.Node.Name);
                kryptonHeaderGroup1_MouseEnter(sender, new MouseEventArgs(e.Button, e.Clicks, e.X, e.X, e.Delta));

            }
        }
        private void toolMenu_Click(object sender, EventArgs e)
        {
            ToolStripButton tb = (ToolStripButton)sender;
            generattForm(tb.Name);

        }

        private void generattForm(string modname)
        {
            try
            {

                this.Cursor = Cursors.WaitCursor;

                UserGroupInfo userGroup = ApplicationManager.Instance().UserAccount.UserGroup;
                ModuleInfo module = UserGroupService.Instance().GetAllowModule(userGroup.ID, modname);

                object obj = (object)FormUtil.CreateForm(module.NameSpace, module.ClassName);
                Form oForm = (Form)obj;

                this.OpenFormChild(oForm);
                if (obj is IFormPermission)
                {
                    IFormPermission oForm_Perm = (IFormPermission)obj;
                    oForm_Perm.Permission = module.Permission;
                    UserAccountService.Instance().KeepLog(ApplicationManager.Instance().UserAccount.AccountId, module.ClassName, SystemInformation.ComputerName,
                        "OpenForm", "Success");
                }

                this.Cursor = Cursors.Default;


            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
            }

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About = new Frm_AboutProgram1();
            About.ShowDialog(this);
        }
        public void kryptonHeaderGroup1_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!kryptonHeaderGroup1.Collapsed)
            {
                kryptonHeaderGroup1.Collapsed = true;
               kryptonHeaderGroup1.Width = kryptonHeaderGroup1.PreferredSize.Width;
            }
            else
            {
               kryptonHeaderGroup1.Width = menuWd;
                kryptonHeaderGroup1.Collapsed = false;
            }
        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (!kryptonHeaderGroup1.Collapsed)
                menuWd = kryptonHeaderGroup1.Width;


        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form  fm= this.ActiveMdiChild;
        }

        private void kryptonHeaderGroup2_MouseDown(object sender, MouseEventArgs e)
        {
            if (!kryptonHeaderGroup2.Collapsed)
            {
                kryptonHeaderGroup2.Collapsed = true;
                kryptonHeaderGroup2.Width = 22;// kryptonHeaderGroup2.PreferredSize.Width;
               // kryptonHeaderGroup2.HeaderPositionPrimary = ComponentFactory.Krypton.Toolkit.VisualOrientation.Left;
            }
            else
            {
                kryptonHeaderGroup2.Width = 575;
                kryptonHeaderGroup2.Collapsed = false;
                txtSearch.Focus();
                // kryptonHeaderGroup2.HeaderPositionPrimary = ComponentFactory.Krypton.Toolkit.VisualOrientation.Top;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
        private void FillGrid()
        {
            this.Cursor = Cursors.WaitCursor;
            if (txtSearch.TextLength >= 3)
            {
                try
                {
                    dgItems.Rows.Clear();


                    string filterSt = "(tname like '%" + txtSearch.Text + "%' or code like '" + txtSearch.Text + "%' or ename like '%" + txtSearch.Text + "%' or idno  like '"+txtSearch.Text+"%' or nickname like '%"+txtSearch.Text+"%')";
                    if (rdbnResigned.Checked)
                    {
                        filterSt += " and resign <> '01/01/1900 00:00:00'";
                    }
                    if (rdbcNResign.Checked)
                    {
                        filterSt += " and resign = '01/01/1900 00:00:00'";
                    }
                    string orderSt = "tname";


                    if (emTb.Rows.Count > 0) {
                        DataRow[] empRw = emTb.Select(filterSt, orderSt);
                        string resigndate = "";
                        DateTime compareDate = DateTime.Parse("01/01/1900");

                        if (empRw.Length > 0)
                        {
                            foreach (DataRow empCheck in empRw)
                            {
                                DateTime resTime = DateTime.Parse(empCheck[3].ToString());

                                // if (compareDate != resTime)
                                //    resigndate = resTime.ToString("dd/MM/yy");
                                // else
                                //    resigndate = "";
                                dgItems.Rows.Add(empCheck[0], empCheck[1].ToString(), empCheck[5].ToString(), empCheck[6].ToString(), empCheck["Telephone"].ToString());
                                if (compareDate != resTime)
                                {
                                    dgItems.Rows[dgItems.Rows.Count - 1].DefaultCellStyle.ForeColor = System.Drawing.Color.Silver;
                                }
                            } // end foreach
                        } // end check lenght
                    } // end check have data
 
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    this.Cursor = Cursors.Default;
                }

            }
            this.Cursor = Cursors.Default;
        }

        private void rdbnAll_CheckedChanged(object sender, EventArgs e)
        {
            ComponentFactory.Krypton.Toolkit.KryptonRadioButton bt = (ComponentFactory.Krypton.Toolkit.KryptonRadioButton)sender;
            if (bt.Checked)
            {
                FillGrid();
            }
        }


        private void empDetailCtrl_Load(object sender, EventArgs e)
        {
            empDetailCtrl.empServ = EmployeeService.Instance();
            empDetailCtrl.subSvr = SubContractService.Instance();
            empDetailCtrl.trSvr = TraineeService.Instance();

            empDetailCtrl.shFtServ = ShiftService.Instance();
            empDetailCtrl.subShSvr = SubContractShiftService.Instance();
            empDetailCtrl.trShSvr = TraineeShiftService.Instance();

        }

        private void dgItems_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            
            try
            {
                empDetailCtrl.Information =  dgItems.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            catch 
            {             
           
            }

        }

        private void kryptonHeaderGroup2_Leave(object sender, EventArgs e)
        {
            kryptonHeaderGroup2.Collapsed = true;
            kryptonHeaderGroup2.Width = 22;
        }

        private void professionalSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
                      
        }

        private void professionalSystemToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
     
        }

        private void sparklePurpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem selectSkin = (ToolStripMenuItem)sender;
            string tt = PaletteModeManager.Office2007Black.ToString();
            foreach (ToolStripMenuItem var in skinToolStripMenuItem.DropDownItems)
            {
                FrmLogin frml = (FrmLogin)this.Owner;

            
                if (selectSkin == var)
                {

                    var.Checked = true;
                    if (var.Tag.ToString().CompareTo(PaletteModeManager.Office2007Black.ToString()) == 0)
                    {
                         DCI.HRMS.Properties.Settings.Default.Skin = PaletteModeManager.Office2007Black;
                         frml.kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2007Black;

                    }
                    else if (var.Tag.ToString().CompareTo(PaletteModeManager.Office2007Blue.ToString()) == 0)
                    {
                        DCI.HRMS.Properties.Settings.Default.Skin = PaletteModeManager.Office2007Blue;
                        frml.kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2007Blue;

                    }
                    else if (var.Tag.ToString().CompareTo(PaletteModeManager.Office2007Silver.ToString()) == 0)
                    {
                        DCI.HRMS.Properties.Settings.Default.Skin = PaletteModeManager.Office2007Silver;
                        frml.kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2007Silver;

                    }
                    else if (var.Tag.ToString().CompareTo(PaletteModeManager.ProfessionalOffice2003.ToString()) == 0)
                    {
                        DCI.HRMS.Properties.Settings.Default.Skin = PaletteModeManager.ProfessionalOffice2003;
                        frml.kryptonManager1.GlobalPaletteMode = PaletteModeManager.ProfessionalOffice2003;

                    }
                    else if (var.Tag.ToString().CompareTo(PaletteModeManager.ProfessionalSystem.ToString()) == 0)
                    {
                        DCI.HRMS.Properties.Settings.Default.Skin = PaletteModeManager.ProfessionalSystem;
                        frml.kryptonManager1.GlobalPaletteMode = PaletteModeManager.ProfessionalSystem;

                    }
                    else if (var.Tag.ToString().CompareTo(PaletteModeManager.SparkleBlue.ToString()) == 0)
                    {
                        DCI.HRMS.Properties.Settings.Default.Skin = PaletteModeManager.SparkleBlue;
                        frml.kryptonManager1.GlobalPaletteMode = PaletteModeManager.SparkleBlue;

                    }
                    else if (var.Tag.ToString().CompareTo(PaletteModeManager.SparkleOrange.ToString()) == 0)
                    {
                        DCI.HRMS.Properties.Settings.Default.Skin = PaletteModeManager.SparkleOrange;
                        frml.kryptonManager1.GlobalPaletteMode = PaletteModeManager.SparkleOrange;

                    }
                    else if (var.Tag.ToString().CompareTo(PaletteModeManager.SparklePurple.ToString()) == 0)
                    {
                        DCI.HRMS.Properties.Settings.Default.Skin = PaletteModeManager.SparklePurple;
                        frml.kryptonManager1.GlobalPaletteMode = PaletteModeManager.SparklePurple;

                    }




                    DCI.HRMS.Properties.Settings.Default.Save();

                }
                else
                {
                    var.Checked = false;
                }
            }

        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserGroupInfo userGroup = ApplicationManager.Instance().UserAccount.UserGroup;
            Attendance.FrmEmployeeLeaveList oForm = new Attendance.FrmEmployeeLeaveList();

            this.OpenFormChild(oForm);
            
        }





      
    }
}
