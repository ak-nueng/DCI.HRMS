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


namespace DCI.HRMS
{
	/// <summary>
	/// Summary description for FrmMenu.
	/// </summary>
	public class Frm_LocalHome :ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private StatusStrip sBar;
        private MenuStrip mBar;
        private ToolStripMenuItem mitemFile;
        private ToolStripMenuItem mSubItem_MainMenu;
        private ToolStripMenuItem mSubItem_Exit;
        private ToolStripMenuItem mItem_Help;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem mSubItem_Help;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem mSubItem_About;
        private Frm_AboutProgram1 About;
        private int menuWd =260;

        private UserGroupService userGroup = UserGroupService.Instance();
        private bool hideMenu = false;
       // private ImageList imgTreeMenu;
        private Splitter splitter1;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup1;
        private TreeView trvMenu;
        private IContainer components;
        private ToolStrip toolStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolMenuToolStripMenuItem;
        private ToolStripMenuItem mainMenuToolStripMenuItem;
        private ToolStripMenuItem skinToolStripMenuItem;
        private ToolStripButton toolStripButton1;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem cutToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem selectAllToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem9;
        private ToolStripMenuItem toolStripMenuItem11;
        private ToolStripMenuItem toolStripMenuItem12;
        private ToolStripMenuItem toolStripMenuItem13;
        private ToolStripMenuItem toolStripMenuItem14;
        private ToolStripMenuItem toolStripMenuItem15;
        private ToolStripMenuItem toolStripMenuItem10;
        private ToolStripMenuItem toolStripMenuItem16;
        private ImageList menuImages;
       // private ImageList menuImage ;

		public Frm_LocalHome()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_LocalHome));
            this.sBar = new System.Windows.Forms.StatusStrip();
            this.mBar = new System.Windows.Forms.MenuStrip();
            this.mitemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mSubItem_MainMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mSubItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem16 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripMenuItem();
            this.mItem_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mSubItem_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mSubItem_About = new System.Windows.Forms.ToolStripMenuItem();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.menuImages = new System.Windows.Forms.ImageList(this.components);
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.kryptonHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.trvMenu = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.mBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.Panel.SuspendLayout();
            this.kryptonHeaderGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sBar
            // 
            this.sBar.Location = new System.Drawing.Point(0, 670);
            this.sBar.Name = "sBar";
            this.sBar.Size = new System.Drawing.Size(840, 22);
            this.sBar.TabIndex = 1;
            this.sBar.Text = "statusStrip1";
            // 
            // mBar
            // 
            this.mBar.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.mBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.mBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mitemFile,
            this.editToolStripMenuItem,
            this.toolStripMenuItem1,
            this.toolStripMenuItem10,
            this.toolStripMenuItem9,
            this.mItem_Help});
            this.mBar.Location = new System.Drawing.Point(0, 0);
            this.mBar.Name = "mBar";
            this.mBar.Size = new System.Drawing.Size(840, 24);
            this.mBar.TabIndex = 2;
            this.mBar.Text = "menuStrip1";
            // 
            // mitemFile
            // 
            this.mitemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mSubItem_MainMenu,
            this.toolStripSeparator1,
            this.mSubItem_Exit});
            this.mitemFile.Name = "mitemFile";
            this.mitemFile.Size = new System.Drawing.Size(37, 20);
            this.mitemFile.Text = "File";
            // 
            // mSubItem_MainMenu
            // 
            this.mSubItem_MainMenu.Enabled = false;
            this.mSubItem_MainMenu.Name = "mSubItem_MainMenu";
            this.mSubItem_MainMenu.ShortcutKeyDisplayString = "";
            this.mSubItem_MainMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.mSubItem_MainMenu.Size = new System.Drawing.Size(175, 22);
            this.mSubItem_MainMenu.Text = "Main menu";
            this.mSubItem_MainMenu.Click += new System.EventHandler(this.mSubItem_MainMenu_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(172, 6);
            // 
            // mSubItem_Exit
            // 
            this.mSubItem_Exit.Name = "mSubItem_Exit";
            this.mSubItem_Exit.Size = new System.Drawing.Size(175, 22);
            this.mSubItem_Exit.Text = "Exit";
            this.mSubItem_Exit.Click += new System.EventHandler(this.mSubItem_Exit_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator5,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator6,
            this.selectAllToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.undoToolStripMenuItem.Text = "&Undo";
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.redoToolStripMenuItem.Text = "&Redo";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(139, 6);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
            this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.cutToolStripMenuItem.Text = "Cu&t";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
            this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.pasteToolStripMenuItem.Text = "&Paste";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(139, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.selectAllToolStripMenuItem.Text = "Select &All";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolMenuToolStripMenuItem,
            this.mainMenuToolStripMenuItem,
            this.skinToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.toolStripMenuItem1.Text = "&View";
            // 
            // toolMenuToolStripMenuItem
            // 
            this.toolMenuToolStripMenuItem.CheckOnClick = true;
            this.toolMenuToolStripMenuItem.Name = "toolMenuToolStripMenuItem";
            this.toolMenuToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.toolMenuToolStripMenuItem.Text = "Tool Menu";
            this.toolMenuToolStripMenuItem.Click += new System.EventHandler(this.toolMenuToolStripMenuItem_Click);
            // 
            // mainMenuToolStripMenuItem
            // 
            this.mainMenuToolStripMenuItem.Checked = true;
            this.mainMenuToolStripMenuItem.CheckOnClick = true;
            this.mainMenuToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mainMenuToolStripMenuItem.Name = "mainMenuToolStripMenuItem";
            this.mainMenuToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.mainMenuToolStripMenuItem.Text = "Main Menu";
            this.mainMenuToolStripMenuItem.Click += new System.EventHandler(this.mainMenuToolStripMenuItem_Click);
            // 
            // skinToolStripMenuItem
            // 
            this.skinToolStripMenuItem.Name = "skinToolStripMenuItem";
            this.skinToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.skinToolStripMenuItem.Text = "Skin";
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem16});
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(46, 20);
            this.toolStripMenuItem10.Text = "&Tools";
            // 
            // toolStripMenuItem16
            // 
            this.toolStripMenuItem16.Name = "toolStripMenuItem16";
            this.toolStripMenuItem16.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItem16.Text = "&Options";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem11,
            this.toolStripMenuItem12,
            this.toolStripMenuItem13,
            this.toolStripMenuItem14,
            this.toolStripMenuItem15});
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(68, 20);
            this.toolStripMenuItem9.Text = "&Windows";
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem11.Text = "&Cascade";
            this.toolStripMenuItem11.Click += new System.EventHandler(this.CascadeToolStripMenuItem_Click);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem12.Text = "Tile &Vertical";
            this.toolStripMenuItem12.Click += new System.EventHandler(this.TileVerticleToolStripMenuItem_Click);
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem13.Text = "Tile &Horizontal";
            this.toolStripMenuItem13.Click += new System.EventHandler(this.TileHorizontalToolStripMenuItem_Click);
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem14.Text = "C&lose All";
            this.toolStripMenuItem14.Click += new System.EventHandler(this.CloseAllToolStripMenuItem_Click);
            // 
            // toolStripMenuItem15
            // 
            this.toolStripMenuItem15.Name = "toolStripMenuItem15";
            this.toolStripMenuItem15.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem15.Text = "&Arrange Icons";
            // 
            // mItem_Help
            // 
            this.mItem_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.mSubItem_Help,
            this.toolStripSeparator2,
            this.mSubItem_About});
            this.mItem_Help.Name = "mItem_Help";
            this.mItem_Help.Size = new System.Drawing.Size(43, 20);
            this.mItem_Help.Text = "Help";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(161, 22);
            this.toolStripMenuItem2.Text = "&Contents";
            // 
            // mSubItem_Help
            // 
            this.mSubItem_Help.Name = "mSubItem_Help";
            this.mSubItem_Help.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.mSubItem_Help.Size = new System.Drawing.Size(161, 22);
            this.mSubItem_Help.Text = "HRMS Help";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(158, 6);
            // 
            // mSubItem_About
            // 
            this.mSubItem_About.Name = "mSubItem_About";
            this.mSubItem_About.Size = new System.Drawing.Size(161, 22);
            this.mSubItem_About.Text = "About DCI HRMS";
            this.mSubItem_About.Click += new System.EventHandler(this.mSubItem_About_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(29, 24);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 646);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            this.splitter1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitter1_SplitterMoved);
            // 
            // menuImages
            // 
            this.menuImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.menuImages.ImageSize = new System.Drawing.Size(20, 20);
            this.menuImages.TransparentColor = System.Drawing.Color.Empty;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::DCI.HRMS.Properties.Resources.access;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 23);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // kryptonHeaderGroup1
            // 
            this.kryptonHeaderGroup1.AllowButtonSpecToolTips = true;
            this.kryptonHeaderGroup1.Collapsed = true;
            this.kryptonHeaderGroup1.DataBindings.Add(new System.Windows.Forms.Binding("Visible", global::DCI.HRMS.Properties.Settings.Default, "mainMenuVisual", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.kryptonHeaderGroup1.Dock = System.Windows.Forms.DockStyle.Left;
            this.kryptonHeaderGroup1.HeaderPositionPrimary = ComponentFactory.Krypton.Toolkit.VisualOrientation.Right;
            this.kryptonHeaderGroup1.HeaderPositionSecondary = ComponentFactory.Krypton.Toolkit.VisualOrientation.Right;
            this.kryptonHeaderGroup1.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup1.Location = new System.Drawing.Point(0, 24);
            this.kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            // 
            // kryptonHeaderGroup1.Panel
            // 
            this.kryptonHeaderGroup1.Panel.Controls.Add(this.trvMenu);
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(29, 646);
            this.kryptonHeaderGroup1.TabIndex = 10;
            this.kryptonHeaderGroup1.Text = "Heading";
            this.kryptonHeaderGroup1.ValuesPrimary.Description = "";
            this.kryptonHeaderGroup1.ValuesPrimary.Heading = "Heading";
            this.kryptonHeaderGroup1.ValuesPrimary.Image = ((System.Drawing.Image)(resources.GetObject("kryptonHeaderGroup1.ValuesPrimary.Image")));
            this.kryptonHeaderGroup1.ValuesPrimary.ImageTransparentColor = System.Drawing.Color.White;
            this.kryptonHeaderGroup1.ValuesSecondary.Description = "";
            this.kryptonHeaderGroup1.ValuesSecondary.Heading = "Description";
            this.kryptonHeaderGroup1.ValuesSecondary.Image = null;
            this.kryptonHeaderGroup1.Visible = global::DCI.HRMS.Properties.Settings.Default.mainMenuVisual;
            this.kryptonHeaderGroup1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.kryptonHeaderGroup1_MouseEnter);
            // 
            // trvMenu
            // 
            this.trvMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvMenu.ImageKey = "Menu";
            this.trvMenu.ImageList = this.menuImages;
            this.trvMenu.Location = new System.Drawing.Point(0, 0);
            this.trvMenu.Name = "trvMenu";
            this.trvMenu.SelectedImageKey = "administration";
            this.trvMenu.ShowNodeToolTips = true;
            this.trvMenu.Size = new System.Drawing.Size(0, 0);
            this.trvMenu.TabIndex = 0;
            this.trvMenu.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvMenu_NodeMouseDoubleClick);
            this.trvMenu.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvMenu_NodeMouseClick);
            this.trvMenu.MouseLeave += new System.EventHandler(this.trvMenu_MouseLeave);
            // 
            // toolStrip1
            // 
            this.toolStrip1.DataBindings.Add(new System.Windows.Forms.Binding("Visible", global::DCI.HRMS.Properties.Settings.Default, "toolMenuVisible", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(840, 25);
            this.toolStrip1.TabIndex = 12;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Visible = global::DCI.HRMS.Properties.Settings.Default.toolMenuVisible;
            // 
            // Frm_LocalHome
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(840, 692);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.kryptonHeaderGroup1);
            this.Controls.Add(this.sBar);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.mBar);
            this.Icon = global::DCI.HRMS.Properties.Resources.alert;
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.mBar;
            this.Name = "Frm_LocalHome";
            this.Text = "DCI Human Resource Management System ";
            this.TextExtra = "V1.0";
            this.TransparencyKey = System.Drawing.Color.White;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMenu_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Frm_LocalHome_FormClosed);
            this.mBar.ResumeLayout(false);
            this.mBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            this.kryptonHeaderGroup1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
                this.kryptonHeaderGroup1.ValuesPrimary.Image =menuImages.Images["kmenuedit"];

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
               
               // trvMenu.SelectedImageIndex = 1;

                mainNode.Expand();
               // mainNode.ExpandAll();
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
                            // menu.SubModules.Remove(subMod.);
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
        
        /*
        private void AddSubMenuToTreeNode(ModuleInfo menu, TreeNode node)
        {
            if (menu.SubModules != null && menu.SubModules.Count > 0 )
            {
                TreeNode childNode = new TreeNode(menu.Name);
                childNode.Name = menu.Id;
                childNode.StateImageIndex = 0;
                //childNode.SelectedImageIndex = 1;
               
                foreach (ModuleInfo subMenu in menu.SubModules)
                {
                    TreeNode chNode = AddSubMenuToTreeNode(subMenu);
                    if (chNode.Name != "")
                      childNode.Nodes.Add(chNode);
                }
                node.Nodes.Add(childNode);
            }
            else   if (menu.Type == ModuleType.Menu)
            {
        
            }
            if (menu.Type != ModuleType.Menu)
            {
                TreeNode childNode = new TreeNode(menu.Name);
                childNode.Name = menu.Id;

                if (menu.Type == ModuleType.Form)
                {
                    childNode.ImageIndex = 2;
                    childNode.StateImageIndex = 2;
                    childNode.SelectedImageIndex = 2;
                }
                else
                {
                    childNode.ImageIndex = 3;
                    childNode.StateImageIndex = 3;
                    childNode.SelectedImageIndex = 3;
                }
                node.Nodes.Add(childNode);
            }
       
        }*/
        private TreeNode AddSubMenuToTreeNode(ModuleInfo menu)
        {

            TreeNode childNode = new TreeNode(menu.Name);
          
            if (menu.SubModules != null && menu.SubModules.Count > 0 && menu.visible)
            {             
               
                foreach (ModuleInfo subMenu in menu.SubModules)
                {
                    TreeNode chNode = AddSubMenuToTreeNode(subMenu);
                    if(chNode.Name!="")
                        childNode.Nodes.Add( chNode);
                }            
            }
            if (menu.Type != ModuleType.Menu)
            {
                ToolStripButton menuButt = new ToolStripButton(menu.Name, menuImages.Images[menu.Icon],new System.EventHandler(this.toolMenu_Click));
                menuButt.Name = menu.Id;
                menuButt.ToolTipText = menu.Description;
                menuButt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft; ;
                toolStrip1.Items.Add(menuButt);


            }
          
            childNode.Name = menu.Id;
            childNode.ToolTipText = menu.Description;
            childNode.ImageKey = menu.Icon;
            childNode.SelectedImageKey = menu.Icon;
            childNode.StateImageKey = menu.Icon;
           
          //  if (menu.visible)
                return childNode;
           // else

              //  return new TreeNode();

        }
        private void FrmMenu_Load(object sender, EventArgs e)
        {
            kryptonHeaderGroup1.ValuesPrimary.Heading = "User menu for " + ApplicationManager.Instance().UserAccount.AccountId;
            trvMenu.Nodes.Clear();

            /*****  Add Menu Pictures List *********/   
            this.menuImages.TransparentColor = System.Drawing.Color.Transparent;
            this.menuImages.Images.Add("HRMS", global::DCI.HRMS.Properties.Resources.alert);

            this.menuImages.Images.Add("access", global::DCI.HRMS.Properties.Resources.access);
            this.menuImages.Images.Add("aim", global::DCI.HRMS.Properties.Resources.aim);
            this.menuImages.Images.Add("aimpo", global::DCI.HRMS.Properties.Resources.aim_protocol);    
            this.menuImages.Images.Add("administration", global::DCI.HRMS.Properties.Resources.administration);
            this.menuImages.Images.Add("antivirus", global::DCI.HRMS.Properties.Resources.antivirus);
            this.menuImages.Images.Add("blog", global::DCI.HRMS.Properties.Resources.Blog);
            this.menuImages.Images.Add("calendar", global::DCI.HRMS.Properties.Resources.Calendar);
            this.menuImages.Images.Add("date", global::DCI.HRMS.Properties.Resources.date);
            this.menuImages.Images.Add("discussion", global::DCI.HRMS.Properties.Resources.discussion);
            this.menuImages.Images.Add("dms", global::DCI.HRMS.Properties.Resources.dms);
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
            this.menuImages.Images.Add("Menu", global::DCI.HRMS.Properties.Resources.Menu);
            this.menuImages.Images.Add("Magnet", global::DCI.HRMS.Properties.Resources.magmet);
            this.menuImages.Images.Add("scale", global::DCI.HRMS.Properties.Resources.Scale);
            this.menuImages.Images.Add("starthear", global::DCI.HRMS.Properties.Resources.starthere);
            this.menuImages.Images.Add("up", global::DCI.HRMS.Properties.Resources.up);
            /******************************************/

            this.LoadMenu();
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
            set { 
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
                    }

                    this.Cursor = Cursors.Default;
                  
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
            }

        }

        private void mSubItem_About_Click(object sender, EventArgs e)
        {
            
                About = new Frm_AboutProgram1();
                About.ShowDialog(this);
            
        }
 

        private void kryptonHeaderGroup1_MouseEnter(object sender, MouseEventArgs e)
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
            if(!kryptonHeaderGroup1.Collapsed)
            menuWd =kryptonHeaderGroup1.Width ;
            
              
        }

        private void toolMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            toolStrip1.Visible = toolMenuToolStripMenuItem.Checked;
        }

        private void mainMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kryptonHeaderGroup1.Visible = mainMenuToolStripMenuItem.Checked;
        }
        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
          //  toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //statusStrip.Visible = statusBarToolStripMenuItem.Checked;
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

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }


	}
}
