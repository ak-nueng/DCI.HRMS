using System;
using System.ComponentModel;
using System.Windows.Forms;
using DCI.HRMS.Production;

namespace DCI.HRMS
{
	/// <summary>
	/// Summary description for FrmMainMenu.
	/// </summary>
	public class FrmMain : Form
	{
		private MainMenu menuBar;
		private MenuItem mnuFile;
		private MenuItem mnuEdit;
		private MenuItem mnuWindow;
		private MenuItem mnuHelp;
		private MenuItem mnuTools;
		private StatusBar statusBar;
		private ToolBar toolBar;
		private StatusBarPanel sItmFormAction;
		private StatusBarPanel sItmUser;
		private StatusBarPanel sItmDateTime;
		private ToolBarButton btnNew;
		private ToolBarButton btnSave;
		private ToolBarButton btnDelete;
		private Panel pnlMainMenu;
		private Button btnHideMenu;
		private ImageList imgFolderList;
		public System.Windows.Forms.ImageList imgToolBar;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private IContainer components;

		public FrmMain()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmMain));
			this.menuBar = new System.Windows.Forms.MainMenu();
			this.mnuFile = new System.Windows.Forms.MenuItem();
			this.mnuEdit = new System.Windows.Forms.MenuItem();
			this.mnuTools = new System.Windows.Forms.MenuItem();
			this.mnuWindow = new System.Windows.Forms.MenuItem();
			this.mnuHelp = new System.Windows.Forms.MenuItem();
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.sItmFormAction = new System.Windows.Forms.StatusBarPanel();
			this.sItmUser = new System.Windows.Forms.StatusBarPanel();
			this.sItmDateTime = new System.Windows.Forms.StatusBarPanel();
			this.toolBar = new System.Windows.Forms.ToolBar();
			this.btnNew = new System.Windows.Forms.ToolBarButton();
			this.btnSave = new System.Windows.Forms.ToolBarButton();
			this.btnDelete = new System.Windows.Forms.ToolBarButton();
			this.imgToolBar = new System.Windows.Forms.ImageList(this.components);
			this.pnlMainMenu = new System.Windows.Forms.Panel();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.btnHideMenu = new System.Windows.Forms.Button();
			this.imgFolderList = new System.Windows.Forms.ImageList(this.components);
			((System.ComponentModel.ISupportInitialize)(this.sItmFormAction)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sItmUser)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sItmDateTime)).BeginInit();
			this.pnlMainMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuBar
			// 
			this.menuBar.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mnuFile,
																					this.mnuEdit,
																					this.mnuTools,
																					this.mnuWindow,
																					this.mnuHelp});
			// 
			// mnuFile
			// 
			this.mnuFile.Index = 0;
			this.mnuFile.Text = "File";
			// 
			// mnuEdit
			// 
			this.mnuEdit.Index = 1;
			this.mnuEdit.Text = "Edit";
			// 
			// mnuTools
			// 
			this.mnuTools.Index = 2;
			this.mnuTools.Text = "Tools";
			// 
			// mnuWindow
			// 
			this.mnuWindow.Index = 3;
			this.mnuWindow.Text = "Window";
			// 
			// mnuHelp
			// 
			this.mnuHelp.Index = 4;
			this.mnuHelp.Text = "Help";
			// 
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(0, 328);
			this.statusBar.Name = "statusBar";
			this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						 this.sItmFormAction,
																						 this.sItmUser,
																						 this.sItmDateTime});
			this.statusBar.ShowPanels = true;
			this.statusBar.Size = new System.Drawing.Size(736, 22);
			this.statusBar.TabIndex = 1;
			// 
			// sItmFormAction
			// 
			this.sItmFormAction.Text = "DCIBizPro : Main Menu";
			this.sItmFormAction.Width = 400;
			// 
			// sItmUser
			// 
			this.sItmUser.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
			this.sItmUser.Text = "Current User";
			this.sItmUser.Width = 200;
			// 
			// sItmDateTime
			// 
			this.sItmDateTime.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
			this.sItmDateTime.Text = "Current Date/Time";
			this.sItmDateTime.Width = 150;
			// 
			// toolBar
			// 
			this.toolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																					   this.btnNew,
																					   this.btnSave,
																					   this.btnDelete});
			this.toolBar.ButtonSize = new System.Drawing.Size(16, 16);
			this.toolBar.DropDownArrows = true;
			this.toolBar.ImageList = this.imgToolBar;
			this.toolBar.Location = new System.Drawing.Point(0, 0);
			this.toolBar.Name = "toolBar";
			this.toolBar.ShowToolTips = true;
			this.toolBar.Size = new System.Drawing.Size(736, 28);
			this.toolBar.TabIndex = 2;
			this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
			// 
			// btnNew
			// 
			this.btnNew.ImageIndex = 0;
			// 
			// btnSave
			// 
			this.btnSave.ImageIndex = 1;
			// 
			// btnDelete
			// 
			this.btnDelete.ImageIndex = 2;
			// 
			// imgToolBar
			// 
			this.imgToolBar.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imgToolBar.ImageSize = new System.Drawing.Size(16, 16);
			this.imgToolBar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgToolBar.ImageStream")));
			this.imgToolBar.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// pnlMainMenu
			// 
			this.pnlMainMenu.AutoScroll = true;
			this.pnlMainMenu.Controls.Add(this.button3);
			this.pnlMainMenu.Controls.Add(this.button2);
			this.pnlMainMenu.Controls.Add(this.button1);
			this.pnlMainMenu.Controls.Add(this.btnHideMenu);
			this.pnlMainMenu.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlMainMenu.Location = new System.Drawing.Point(0, 28);
			this.pnlMainMenu.Name = "pnlMainMenu";
			this.pnlMainMenu.Size = new System.Drawing.Size(174, 300);
			this.pnlMainMenu.TabIndex = 4;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(30, 78);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(126, 24);
			this.button3.TabIndex = 4;
			this.button3.Text = "IPC Sheet Entry";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(30, 48);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(126, 24);
			this.button2.TabIndex = 3;
			this.button2.Text = "Setup Assembly Line";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(30, 18);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(126, 24);
			this.button1.TabIndex = 2;
			this.button1.Text = "Setup Model";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// btnHideMenu
			// 
			this.btnHideMenu.Dock = System.Windows.Forms.DockStyle.Left;
			this.btnHideMenu.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnHideMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(222)));
			this.btnHideMenu.Location = new System.Drawing.Point(0, 0);
			this.btnHideMenu.Name = "btnHideMenu";
			this.btnHideMenu.Size = new System.Drawing.Size(12, 300);
			this.btnHideMenu.TabIndex = 1;
			this.btnHideMenu.Text = "<";
			this.btnHideMenu.Click += new System.EventHandler(this.btnHideMenu_Click);
			// 
			// imgFolderList
			// 
			this.imgFolderList.ImageSize = new System.Drawing.Size(30, 30);
			this.imgFolderList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgFolderList.ImageStream")));
			this.imgFolderList.TransparentColor = System.Drawing.Color.OldLace;
			// 
			// FrmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(736, 350);
			this.Controls.Add(this.pnlMainMenu);
			this.Controls.Add(this.toolBar);
			this.Controls.Add(this.statusBar);
			this.IsMdiContainer = true;
			this.Menu = this.menuBar;
			this.Name = "FrmMain";
			this.Text = "FrmMainMenu";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.MdiChildActivate += new System.EventHandler(this.FrmMain_MdiChildActivate);
			this.Load += new System.EventHandler(this.FrmMainMenu_Load);
			this.Closed += new System.EventHandler(this.FrmMain_Closed);
			((System.ComponentModel.ISupportInitialize)(this.sItmFormAction)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sItmUser)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sItmDateTime)).EndInit();
			this.pnlMainMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FrmMainMenu_Load(object sender, EventArgs e)
		{
			this.toolBar.Buttons[0].Enabled = false;
			this.toolBar.Buttons[1].Enabled = false;
			this.toolBar.Buttons[2].Enabled = false;
		}

		private void FrmMain_Closed(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void toolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			string msg = String.Empty;

			try
			{
				this.Cursor = Cursors.WaitCursor;

				Form frm = this.ActiveMdiChild;
				IFormAction frmAction = (IFormAction) frm;

				if (this.MdiChildren.Length > 0)
				{
					this.sItmFormAction.Text = frm.Text;
				}
				else
				{
					this.sItmFormAction.Text = this.Text;
				}

				switch (toolBar.Buttons.IndexOf(e.Button))
				{
					case 0:
						frmAction.New();
						
						break;

					case 1:
						frmAction.Save();

						break;

					case 2:
						frmAction.Delete();

						break;
				}
			}
			catch (NullReferenceException nullEx)
			{
				this.sItmFormAction.Text = nullEx.Message;
				msg = "Can not found active windows form.";
			}
			catch (Exception ex)
			{
				this.sItmFormAction.Text = ex.Message;
				msg = "Can not found active windows form.";
			}
			finally
			{
				this.Cursor = Cursors.Default;

				if (!msg.Equals(String.Empty))
				{
					MessageBox.Show(this, msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		int mainMenuSize = 0;

		private void btnHideMenu_Click(object sender, EventArgs e)
		{
			if(mainMenuSize == 0)
			{
				mainMenuSize = this.pnlMainMenu.Width;
			}
			
			if(this.pnlMainMenu.Width > this.btnHideMenu.Width)
			{
				this.btnHideMenu.Text = ">";
				this.pnlMainMenu.Width = this.btnHideMenu.Width;
				this.pnlMainMenu.AutoScroll = false;
			}else
			{
				this.btnHideMenu.Text = "<";
				this.pnlMainMenu.Width = mainMenuSize;
				this.pnlMainMenu.AutoScroll = true;
			}
			
		}
		
		public void ChangeToolBarStatus(FormAction action)
		{
			switch(action)
			{
				case FormAction.New : 
					this.toolBar.Buttons[0].Enabled = true;
					this.toolBar.Buttons[1].Enabled = true;
					this.toolBar.Buttons[2].Enabled = false;

					this.sItmFormAction.Text += " : New";
					break;

				case FormAction.Save :
					this.toolBar.Buttons[0].Enabled = true;
					this.toolBar.Buttons[1].Enabled = true;
					this.toolBar.Buttons[2].Enabled = true;

					this.sItmFormAction.Text += " : Save";
					break;

				case FormAction.Delete : 
					this.toolBar.Buttons[0].Enabled = true;
					this.toolBar.Buttons[1].Enabled = false;
					this.toolBar.Buttons[2].Enabled = true;
					
					this.sItmFormAction.Text += " : Delete";
					break;

			}
		}

		private void FrmMain_MdiChildActivate(object sender, EventArgs e)
		{
			try
			{
				IFormAction frm = (IFormAction)this.ActiveMdiChild;
				this.ChangeToolBarStatus(frm.FormActionStatus);
			}catch
			{
				this.toolBar.Buttons[0].Enabled = false;
				this.toolBar.Buttons[1].Enabled = false;
				this.toolBar.Buttons[2].Enabled = false;
			}
		}

		private void OpenSubForm(Form frm)
		{
			frm.MdiParent = this;
			frm.Show();
		}
		private void button1_Click(object sender, System.EventArgs e)
		{
			this.OpenSubForm(new FrmSetupModel());
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.OpenSubForm(new FrmSetupAssemblyLine());
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			this.OpenSubForm(new FrmIPCSheet());
		}
		
	}
}
