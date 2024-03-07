using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using DCIBizPro.Business.Security;
using DCIBizPro.DTO.SM;
using DCIBizPro.Util.Text;
using DCI.HRMS.Common;
using DCI.HRMS.Util;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinTabControl;

namespace DCI.HRMS.ControlPanel
{
	/// <summary>
	/// Summary description for FrmUserGroup.
	/// </summary>
	public class FrmUserGroup : BaseForm, IFormChildAction
	{
		private Panel panel1;
		private Panel panel2;
		private UltraButton btnSearch;
		private Label label1;
		private TextBox txtSearch;
		private ListView lvGroup;
		private ColumnHeader colID;
		private ColumnHeader colName;
		private Splitter splitter1;
		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;
		private UltraTabPageControl ultraTabPageControl1;
		private Label label4;
		private Label label3;
		private Label label2;
		private UltraTabControl tab;
		private UltraTabSharedControlsPage ultraTabSharedControlsPage2;
		private UltraTabPageControl ultraTabPageControl2;
		private UltraTabPageControl ultraTabPageControl3;
		private ColumnHeader colUID;
		private ColumnHeader colUName;
		private Label label5;
		private Label label6;
		private Label label7;
		private Label label8;
		private Label label9;
		private ColumnHeader colPID;
		private ColumnHeader colPName;
		private ColumnHeader colPView;
		private ColumnHeader colPEdit;
		private ColumnHeader colPAdd;
		private ColumnHeader colPDel;
		private ColumnHeader colPReport;
		private ColumnHeader colPExport;
		private ColumnHeader colPChangeDoc;
		private Label label12;
		private Label label13;
		private UltraGroupBox ultraGroupBox1;
		private ColumnHeader colPType;
		private CheckBox chkDisableAccount;
		private CheckBox chkPwdNeverExpired;
		private CheckBox chkCannotChangePwd;
		private CheckBox chkChangPwdLogon;
		private ComboBox cboUserGroup;
		private TextBox txtEmail;
		private TextBox txtDescription;
		private TextBox txtFullName;
		private TextBox txtUserId;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		private ListView lvUsers;
		private TextBox txtUserGroupDesc;
		private TextBox txtUserGroupName;
		private ColumnHeader colUDesc;
		private ComboBox cboZone;
		private CheckBox chkAllowExport;
		private CheckBox chkAllowReport;
		private CheckBox chkAllowChangeDocStatus;
		private CheckBox chkAllowDelete;
		private CheckBox chkAllowAddNew;
		private CheckBox chkAllowEdit;
		private CheckBox chkAllowView;
		private ListView lvPermission;
		private UltraButton btnUAdd;
		private UltraButton btnUSave;
		private UltraButton btnUDelete;
		private UltraButton btnPSave;
		private Label lblLastLogIn;
		private Label lblLastComputerUse;

		private UserGroupInfo m_UserGroup = null;
		private ActionStatus m_UserActionStatus = ActionStatus.None;
		private TextBox txtUserGroupId;
		private ContextMenu ctxUser;
		private Label label10;
		private MenuItem mnuItmSetPwd;
		private MenuItem mnuItmRefresh;
		private ContextMenu ctxPermission;
		private MenuItem mnuItmPReset;
		private MenuItem mnuItmPRefresh;
		private Label label11;
		private UltraTabControl tabSetup;
		private ActionStatus m_PermissionActionStatus = ActionStatus.None;

		public FrmUserGroup()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof (FrmUserGroup));
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			this.ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			this.label10 = new System.Windows.Forms.Label();
			this.btnUAdd = new Infragistics.Win.Misc.UltraButton();
			this.btnUDelete = new Infragistics.Win.Misc.UltraButton();
			this.btnUSave = new Infragistics.Win.Misc.UltraButton();
			this.chkDisableAccount = new System.Windows.Forms.CheckBox();
			this.txtEmail = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.chkPwdNeverExpired = new System.Windows.Forms.CheckBox();
			this.chkCannotChangePwd = new System.Windows.Forms.CheckBox();
			this.chkChangPwdLogon = new System.Windows.Forms.CheckBox();
			this.cboUserGroup = new System.Windows.Forms.ComboBox();
			this.lblLastComputerUse = new System.Windows.Forms.Label();
			this.lblLastLogIn = new System.Windows.Forms.Label();
			this.txtFullName = new System.Windows.Forms.TextBox();
			this.txtUserId = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.lvUsers = new System.Windows.Forms.ListView();
			this.colUID = new System.Windows.Forms.ColumnHeader();
			this.colUName = new System.Windows.Forms.ColumnHeader();
			this.colUDesc = new System.Windows.Forms.ColumnHeader();
			this.ctxUser = new System.Windows.Forms.ContextMenu();
			this.mnuItmSetPwd = new System.Windows.Forms.MenuItem();
			this.mnuItmRefresh = new System.Windows.Forms.MenuItem();
			this.ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			this.label11 = new System.Windows.Forms.Label();
			this.btnPSave = new Infragistics.Win.Misc.UltraButton();
			this.cboZone = new System.Windows.Forms.ComboBox();
			this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			this.chkAllowExport = new System.Windows.Forms.CheckBox();
			this.chkAllowReport = new System.Windows.Forms.CheckBox();
			this.chkAllowChangeDocStatus = new System.Windows.Forms.CheckBox();
			this.chkAllowDelete = new System.Windows.Forms.CheckBox();
			this.chkAllowAddNew = new System.Windows.Forms.CheckBox();
			this.chkAllowEdit = new System.Windows.Forms.CheckBox();
			this.chkAllowView = new System.Windows.Forms.CheckBox();
			this.lvPermission = new System.Windows.Forms.ListView();
			this.colPType = new System.Windows.Forms.ColumnHeader();
			this.colPID = new System.Windows.Forms.ColumnHeader();
			this.colPName = new System.Windows.Forms.ColumnHeader();
			this.colPView = new System.Windows.Forms.ColumnHeader();
			this.colPEdit = new System.Windows.Forms.ColumnHeader();
			this.colPAdd = new System.Windows.Forms.ColumnHeader();
			this.colPDel = new System.Windows.Forms.ColumnHeader();
			this.colPReport = new System.Windows.Forms.ColumnHeader();
			this.colPExport = new System.Windows.Forms.ColumnHeader();
			this.colPChangeDoc = new System.Windows.Forms.ColumnHeader();
			this.ctxPermission = new System.Windows.Forms.ContextMenu();
			this.mnuItmPReset = new System.Windows.Forms.MenuItem();
			this.mnuItmPRefresh = new System.Windows.Forms.MenuItem();
			this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			this.txtUserGroupId = new System.Windows.Forms.TextBox();
			this.tabSetup = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			this.ultraTabSharedControlsPage2 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			this.txtUserGroupDesc = new System.Windows.Forms.TextBox();
			this.txtUserGroupName = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lvGroup = new System.Windows.Forms.ListView();
			this.colID = new System.Windows.Forms.ColumnHeader();
			this.colName = new System.Windows.Forms.ColumnHeader();
			this.panel2 = new System.Windows.Forms.Panel();
			this.btnSearch = new Infragistics.Win.Misc.UltraButton();
			this.label1 = new System.Windows.Forms.Label();
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			this.tab = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			this.ultraTabPageControl2.SuspendLayout();
			this.ultraTabPageControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.ultraGroupBox1)).BeginInit();
			this.ultraGroupBox1.SuspendLayout();
			this.ultraTabPageControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.tabSetup)).BeginInit();
			this.tabSetup.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.tab)).BeginInit();
			this.tab.SuspendLayout();
			this.SuspendLayout();
			// 
			// ultraTabPageControl2
			// 
			this.ultraTabPageControl2.Controls.Add(this.label10);
			this.ultraTabPageControl2.Controls.Add(this.btnUAdd);
			this.ultraTabPageControl2.Controls.Add(this.btnUDelete);
			this.ultraTabPageControl2.Controls.Add(this.btnUSave);
			this.ultraTabPageControl2.Controls.Add(this.chkDisableAccount);
			this.ultraTabPageControl2.Controls.Add(this.txtEmail);
			this.ultraTabPageControl2.Controls.Add(this.label13);
			this.ultraTabPageControl2.Controls.Add(this.txtDescription);
			this.ultraTabPageControl2.Controls.Add(this.label12);
			this.ultraTabPageControl2.Controls.Add(this.chkPwdNeverExpired);
			this.ultraTabPageControl2.Controls.Add(this.chkCannotChangePwd);
			this.ultraTabPageControl2.Controls.Add(this.chkChangPwdLogon);
			this.ultraTabPageControl2.Controls.Add(this.cboUserGroup);
			this.ultraTabPageControl2.Controls.Add(this.lblLastComputerUse);
			this.ultraTabPageControl2.Controls.Add(this.lblLastLogIn);
			this.ultraTabPageControl2.Controls.Add(this.txtFullName);
			this.ultraTabPageControl2.Controls.Add(this.txtUserId);
			this.ultraTabPageControl2.Controls.Add(this.label9);
			this.ultraTabPageControl2.Controls.Add(this.label8);
			this.ultraTabPageControl2.Controls.Add(this.label5);
			this.ultraTabPageControl2.Controls.Add(this.label6);
			this.ultraTabPageControl2.Controls.Add(this.label7);
			this.ultraTabPageControl2.Controls.Add(this.lvUsers);
			this.ultraTabPageControl2.Location = new System.Drawing.Point(2, 21);
			this.ultraTabPageControl2.Name = "ultraTabPageControl2";
			this.ultraTabPageControl2.Size = new System.Drawing.Size(672, 429);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.BackColor = System.Drawing.Color.Transparent;
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.label10.Location = new System.Drawing.Point(8, 12);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(184, 17);
			this.label10.TabIndex = 64;
			this.label10.Text = "Right click for set new password.";
			// 
			// btnUAdd
			// 
			appearance1.Image = ((object) (resources.GetObject("appearance1.Image")));
			this.btnUAdd.Appearance = appearance1;
			this.btnUAdd.ButtonStyle = Infragistics.Win.UIElementButtonStyle.OfficeXPToolbarButton;
			this.btnUAdd.Location = new System.Drawing.Point(456, 180);
			this.btnUAdd.Name = "btnUAdd";
			this.btnUAdd.Size = new System.Drawing.Size(68, 28);
			this.btnUAdd.SupportThemes = false;
			this.btnUAdd.TabIndex = 16;
			this.btnUAdd.Text = "Add";
			this.btnUAdd.Click += new System.EventHandler(this.btnUAdd_Click);
			// 
			// btnUDelete
			// 
			appearance2.Image = ((object) (resources.GetObject("appearance2.Image")));
			this.btnUDelete.Appearance = appearance2;
			this.btnUDelete.ButtonStyle = Infragistics.Win.UIElementButtonStyle.OfficeXPToolbarButton;
			this.btnUDelete.Location = new System.Drawing.Point(592, 180);
			this.btnUDelete.Name = "btnUDelete";
			this.btnUDelete.Size = new System.Drawing.Size(68, 28);
			this.btnUDelete.SupportThemes = false;
			this.btnUDelete.TabIndex = 18;
			this.btnUDelete.Text = "Delete";
			this.btnUDelete.Click += new System.EventHandler(this.btnUDelete_Click);
			// 
			// btnUSave
			// 
			appearance3.Image = ((object) (resources.GetObject("appearance3.Image")));
			this.btnUSave.Appearance = appearance3;
			this.btnUSave.ButtonStyle = Infragistics.Win.UIElementButtonStyle.OfficeXPToolbarButton;
			this.btnUSave.Location = new System.Drawing.Point(524, 180);
			this.btnUSave.Name = "btnUSave";
			this.btnUSave.Size = new System.Drawing.Size(68, 28);
			this.btnUSave.SupportThemes = false;
			this.btnUSave.TabIndex = 17;
			this.btnUSave.Text = "Save";
			this.btnUSave.Click += new System.EventHandler(this.btnUSave_Click);
			// 
			// chkDisableAccount
			// 
			this.chkDisableAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.chkDisableAccount.Location = new System.Drawing.Point(16, 312);
			this.chkDisableAccount.Name = "chkDisableAccount";
			this.chkDisableAccount.Size = new System.Drawing.Size(260, 24);
			this.chkDisableAccount.TabIndex = 12;
			this.chkDisableAccount.Text = "Disable Account.";
			this.chkDisableAccount.Enter += new System.EventHandler(this.TextBox_Enter);
			// 
			// txtEmail
			// 
			this.txtEmail.AutoSize = false;
			this.txtEmail.BackColor = System.Drawing.Color.Beige;
			this.txtEmail.Location = new System.Drawing.Point(168, 280);
			this.txtEmail.MaxLength = 255;
			this.txtEmail.Name = "txtEmail";
			this.txtEmail.Size = new System.Drawing.Size(276, 24);
			this.txtEmail.TabIndex = 11;
			this.txtEmail.Text = "";
			this.txtEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyEnter);
			this.txtEmail.Enter += new System.EventHandler(this.TextBox_Enter);
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.BackColor = System.Drawing.Color.Transparent;
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.label13.Location = new System.Drawing.Point(12, 284);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(36, 17);
			this.label13.TabIndex = 63;
			this.label13.Text = "Email";
			// 
			// txtDescription
			// 
			this.txtDescription.AutoSize = false;
			this.txtDescription.BackColor = System.Drawing.Color.Beige;
			this.txtDescription.Location = new System.Drawing.Point(168, 256);
			this.txtDescription.MaxLength = 255;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(276, 24);
			this.txtDescription.TabIndex = 10;
			this.txtDescription.Text = "";
			this.txtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyEnter);
			this.txtDescription.Enter += new System.EventHandler(this.TextBox_Enter);
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.BackColor = System.Drawing.Color.Transparent;
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.label12.Location = new System.Drawing.Point(12, 260);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(66, 17);
			this.label12.TabIndex = 61;
			this.label12.Text = "Description";
			// 
			// chkPwdNeverExpired
			// 
			this.chkPwdNeverExpired.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.chkPwdNeverExpired.Location = new System.Drawing.Point(16, 396);
			this.chkPwdNeverExpired.Name = "chkPwdNeverExpired";
			this.chkPwdNeverExpired.Size = new System.Drawing.Size(260, 24);
			this.chkPwdNeverExpired.TabIndex = 15;
			this.chkPwdNeverExpired.Text = "Password never expired.";
			this.chkPwdNeverExpired.Enter += new System.EventHandler(this.TextBox_Enter);
			this.chkPwdNeverExpired.CheckedChanged += new System.EventHandler(this.chkOption_CheckedChanged);
			// 
			// chkCannotChangePwd
			// 
			this.chkCannotChangePwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.chkCannotChangePwd.Location = new System.Drawing.Point(16, 368);
			this.chkCannotChangePwd.Name = "chkCannotChangePwd";
			this.chkCannotChangePwd.Size = new System.Drawing.Size(260, 24);
			this.chkCannotChangePwd.TabIndex = 14;
			this.chkCannotChangePwd.Text = "User cannot change password.";
			this.chkCannotChangePwd.Enter += new System.EventHandler(this.TextBox_Enter);
			this.chkCannotChangePwd.CheckedChanged += new System.EventHandler(this.chkOption_CheckedChanged);
			// 
			// chkChangPwdLogon
			// 
			this.chkChangPwdLogon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.chkChangPwdLogon.Location = new System.Drawing.Point(16, 340);
			this.chkChangPwdLogon.Name = "chkChangPwdLogon";
			this.chkChangPwdLogon.Size = new System.Drawing.Size(260, 24);
			this.chkChangPwdLogon.TabIndex = 13;
			this.chkChangPwdLogon.Text = "User must change password at next logon.";
			this.chkChangPwdLogon.Enter += new System.EventHandler(this.TextBox_Enter);
			this.chkChangPwdLogon.CheckedChanged += new System.EventHandler(this.chkOption_CheckedChanged);
			// 
			// cboUserGroup
			// 
			this.cboUserGroup.BackColor = System.Drawing.Color.Beige;
			this.cboUserGroup.ItemHeight = 13;
			this.cboUserGroup.Location = new System.Drawing.Point(168, 208);
			this.cboUserGroup.Name = "cboUserGroup";
			this.cboUserGroup.Size = new System.Drawing.Size(276, 21);
			this.cboUserGroup.TabIndex = 8;
			this.cboUserGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyEnter);
			// 
			// lblLastComputerUse
			// 
			this.lblLastComputerUse.BackColor = System.Drawing.SystemColors.Control;
			this.lblLastComputerUse.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblLastComputerUse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.lblLastComputerUse.Location = new System.Drawing.Point(492, 396);
			this.lblLastComputerUse.Name = "lblLastComputerUse";
			this.lblLastComputerUse.Size = new System.Drawing.Size(172, 24);
			this.lblLastComputerUse.TabIndex = 56;
			// 
			// lblLastLogIn
			// 
			this.lblLastLogIn.BackColor = System.Drawing.SystemColors.Control;
			this.lblLastLogIn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblLastLogIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.lblLastLogIn.Location = new System.Drawing.Point(492, 372);
			this.lblLastLogIn.Name = "lblLastLogIn";
			this.lblLastLogIn.Size = new System.Drawing.Size(172, 24);
			this.lblLastLogIn.TabIndex = 55;
			// 
			// txtFullName
			// 
			this.txtFullName.AutoSize = false;
			this.txtFullName.BackColor = System.Drawing.Color.Beige;
			this.txtFullName.Location = new System.Drawing.Point(168, 232);
			this.txtFullName.MaxLength = 255;
			this.txtFullName.Name = "txtFullName";
			this.txtFullName.Size = new System.Drawing.Size(276, 24);
			this.txtFullName.TabIndex = 9;
			this.txtFullName.Text = "";
			this.txtFullName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyEnter);
			this.txtFullName.Enter += new System.EventHandler(this.TextBox_Enter);
			// 
			// txtUserId
			// 
			this.txtUserId.AutoSize = false;
			this.txtUserId.BackColor = System.Drawing.Color.Beige;
			this.txtUserId.Location = new System.Drawing.Point(168, 184);
			this.txtUserId.MaxLength = 25;
			this.txtUserId.Name = "txtUserId";
			this.txtUserId.Size = new System.Drawing.Size(88, 24);
			this.txtUserId.TabIndex = 7;
			this.txtUserId.Text = "";
			this.txtUserId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyEnter);
			this.txtUserId.Enter += new System.EventHandler(this.TextBox_Enter);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.BackColor = System.Drawing.Color.Transparent;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.label9.Location = new System.Drawing.Point(336, 400);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(103, 17);
			this.label9.TabIndex = 52;
			this.label9.Text = "Last Access From";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.BackColor = System.Drawing.Color.Transparent;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.label8.Location = new System.Drawing.Point(336, 376);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(85, 17);
			this.label8.TabIndex = 51;
			this.label8.Text = "Last Logged in";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.label5.Location = new System.Drawing.Point(12, 208);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(68, 17);
			this.label5.TabIndex = 50;
			this.label5.Text = "User Group";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.BackColor = System.Drawing.Color.Transparent;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.label6.Location = new System.Drawing.Point(12, 236);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(61, 17);
			this.label6.TabIndex = 49;
			this.label6.Text = "Full Name";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.BackColor = System.Drawing.Color.Transparent;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.label7.Location = new System.Drawing.Point(12, 184);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(46, 17);
			this.label7.TabIndex = 48;
			this.label7.Text = "User ID";
			// 
			// lvUsers
			// 
			this.lvUsers.BackColor = System.Drawing.Color.White;
			this.lvUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[]
				{
					this.colUID,
					this.colUName,
					this.colUDesc
				});
			this.lvUsers.ContextMenu = this.ctxUser;
			this.lvUsers.FullRowSelect = true;
			this.lvUsers.GridLines = true;
			this.lvUsers.Location = new System.Drawing.Point(8, 32);
			this.lvUsers.MultiSelect = false;
			this.lvUsers.Name = "lvUsers";
			this.lvUsers.Size = new System.Drawing.Size(656, 140);
			this.lvUsers.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvUsers.TabIndex = 6;
			this.lvUsers.View = System.Windows.Forms.View.Details;
			this.lvUsers.SelectedIndexChanged += new System.EventHandler(this.lvUsers_SelectedIndexChanged);
			// 
			// colUID
			// 
			this.colUID.Text = "ID";
			this.colUID.Width = 100;
			// 
			// colUName
			// 
			this.colUName.Text = "Full Name";
			this.colUName.Width = 200;
			// 
			// colUDesc
			// 
			this.colUDesc.Text = "Description";
			this.colUDesc.Width = 250;
			// 
			// ctxUser
			// 
			this.ctxUser.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
				{
					this.mnuItmSetPwd,
					this.mnuItmRefresh
				});
			this.ctxUser.Popup += new System.EventHandler(this.ctxUser_Popup);
			// 
			// mnuItmSetPwd
			// 
			this.mnuItmSetPwd.Index = 0;
			this.mnuItmSetPwd.Text = "Set Password";
			this.mnuItmSetPwd.Click += new System.EventHandler(this.mnuItmSetPwd_Click);
			// 
			// mnuItmRefresh
			// 
			this.mnuItmRefresh.Index = 1;
			this.mnuItmRefresh.Text = "Refresh";
			this.mnuItmRefresh.Click += new System.EventHandler(this.mnuItmRefresh_Click);
			// 
			// ultraTabPageControl3
			// 
			this.ultraTabPageControl3.Controls.Add(this.label11);
			this.ultraTabPageControl3.Controls.Add(this.btnPSave);
			this.ultraTabPageControl3.Controls.Add(this.cboZone);
			this.ultraTabPageControl3.Controls.Add(this.ultraGroupBox1);
			this.ultraTabPageControl3.Controls.Add(this.lvPermission);
			this.ultraTabPageControl3.Location = new System.Drawing.Point(-10000, -10000);
			this.ultraTabPageControl3.Name = "ultraTabPageControl3";
			this.ultraTabPageControl3.Size = new System.Drawing.Size(672, 429);
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.BackColor = System.Drawing.Color.Transparent;
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.label11.Location = new System.Drawing.Point(8, 16);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(176, 17);
			this.label11.TabIndex = 65;
			this.label11.Text = "Right click for reset permission.";
			// 
			// btnPSave
			// 
			appearance4.Image = ((object) (resources.GetObject("appearance4.Image")));
			this.btnPSave.Appearance = appearance4;
			this.btnPSave.ButtonStyle = Infragistics.Win.UIElementButtonStyle.OfficeXPToolbarButton;
			this.btnPSave.Location = new System.Drawing.Point(432, 232);
			this.btnPSave.Name = "btnPSave";
			this.btnPSave.Size = new System.Drawing.Size(120, 28);
			this.btnPSave.SupportThemes = false;
			this.btnPSave.TabIndex = 29;
			this.btnPSave.Text = "Set Permission";
			this.btnPSave.Click += new System.EventHandler(this.btnPSave_Click);
			// 
			// cboZone
			// 
			this.cboZone.BackColor = System.Drawing.SystemColors.Control;
			this.cboZone.ItemHeight = 13;
			this.cboZone.Location = new System.Drawing.Point(156, 236);
			this.cboZone.Name = "cboZone";
			this.cboZone.Size = new System.Drawing.Size(264, 21);
			this.cboZone.TabIndex = 20;
			this.cboZone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyEnter);
			// 
			// ultraGroupBox1
			// 
			this.ultraGroupBox1.Controls.Add(this.chkAllowExport);
			this.ultraGroupBox1.Controls.Add(this.chkAllowReport);
			this.ultraGroupBox1.Controls.Add(this.chkAllowChangeDocStatus);
			this.ultraGroupBox1.Controls.Add(this.chkAllowDelete);
			this.ultraGroupBox1.Controls.Add(this.chkAllowAddNew);
			this.ultraGroupBox1.Controls.Add(this.chkAllowEdit);
			this.ultraGroupBox1.Controls.Add(this.chkAllowView);
			this.ultraGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.ultraGroupBox1.Location = new System.Drawing.Point(8, 240);
			this.ultraGroupBox1.Name = "ultraGroupBox1";
			this.ultraGroupBox1.Size = new System.Drawing.Size(556, 180);
			this.ultraGroupBox1.SupportThemes = false;
			this.ultraGroupBox1.TabIndex = 2;
			this.ultraGroupBox1.Text = " Permissions On Zone.  .  . ";
			this.ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2000;
			// 
			// chkAllowExport
			// 
			this.chkAllowExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.chkAllowExport.Location = new System.Drawing.Point(320, 60);
			this.chkAllowExport.Name = "chkAllowExport";
			this.chkAllowExport.Size = new System.Drawing.Size(140, 24);
			this.chkAllowExport.TabIndex = 27;
			this.chkAllowExport.Text = "Allow Export Report";
			this.chkAllowExport.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyEnter);
			// 
			// chkAllowReport
			// 
			this.chkAllowReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.chkAllowReport.Location = new System.Drawing.Point(320, 32);
			this.chkAllowReport.Name = "chkAllowReport";
			this.chkAllowReport.Size = new System.Drawing.Size(140, 24);
			this.chkAllowReport.TabIndex = 26;
			this.chkAllowReport.Text = "Allow Report";
			this.chkAllowReport.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyEnter);
			// 
			// chkAllowChangeDocStatus
			// 
			this.chkAllowChangeDocStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.chkAllowChangeDocStatus.Location = new System.Drawing.Point(12, 144);
			this.chkAllowChangeDocStatus.Name = "chkAllowChangeDocStatus";
			this.chkAllowChangeDocStatus.Size = new System.Drawing.Size(416, 24);
			this.chkAllowChangeDocStatus.TabIndex = 25;
			this.chkAllowChangeDocStatus.Text = "Allow Change Document Status (i.e,Cancel --> Edit , Complete --> Edit)";
			this.chkAllowChangeDocStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyEnter);
			// 
			// chkAllowDelete
			// 
			this.chkAllowDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.chkAllowDelete.Location = new System.Drawing.Point(12, 116);
			this.chkAllowDelete.Name = "chkAllowDelete";
			this.chkAllowDelete.Size = new System.Drawing.Size(92, 24);
			this.chkAllowDelete.TabIndex = 24;
			this.chkAllowDelete.Text = "Allow Delete";
			this.chkAllowDelete.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyEnter);
			// 
			// chkAllowAddNew
			// 
			this.chkAllowAddNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.chkAllowAddNew.Location = new System.Drawing.Point(12, 88);
			this.chkAllowAddNew.Name = "chkAllowAddNew";
			this.chkAllowAddNew.Size = new System.Drawing.Size(128, 24);
			this.chkAllowAddNew.TabIndex = 23;
			this.chkAllowAddNew.Text = "Allow Add New";
			this.chkAllowAddNew.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyEnter);
			// 
			// chkAllowEdit
			// 
			this.chkAllowEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.chkAllowEdit.Location = new System.Drawing.Point(12, 60);
			this.chkAllowEdit.Name = "chkAllowEdit";
			this.chkAllowEdit.Size = new System.Drawing.Size(196, 24);
			this.chkAllowEdit.TabIndex = 22;
			this.chkAllowEdit.Text = "Allow Edit (Save changes)";
			this.chkAllowEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyEnter);
			// 
			// chkAllowView
			// 
			this.chkAllowView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.chkAllowView.Location = new System.Drawing.Point(12, 32);
			this.chkAllowView.Name = "chkAllowView";
			this.chkAllowView.Size = new System.Drawing.Size(92, 24);
			this.chkAllowView.TabIndex = 21;
			this.chkAllowView.Text = "Allow View";
			this.chkAllowView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyEnter);
			// 
			// lvPermission
			// 
			this.lvPermission.BackColor = System.Drawing.Color.White;
			this.lvPermission.Columns.AddRange(new System.Windows.Forms.ColumnHeader[]
				{
					this.colPType,
					this.colPID,
					this.colPName,
					this.colPView,
					this.colPEdit,
					this.colPAdd,
					this.colPDel,
					this.colPReport,
					this.colPExport,
					this.colPChangeDoc
				});
			this.lvPermission.ContextMenu = this.ctxPermission;
			this.lvPermission.FullRowSelect = true;
			this.lvPermission.GridLines = true;
			this.lvPermission.Location = new System.Drawing.Point(8, 36);
			this.lvPermission.Name = "lvPermission";
			this.lvPermission.Size = new System.Drawing.Size(660, 192);
			this.lvPermission.TabIndex = 19;
			this.lvPermission.View = System.Windows.Forms.View.Details;
			this.lvPermission.SelectedIndexChanged += new System.EventHandler(this.lvPermission_SelectedIndexChanged);
			// 
			// colPType
			// 
			this.colPType.Text = "Type";
			// 
			// colPID
			// 
			this.colPID.Text = "ID";
			this.colPID.Width = 50;
			// 
			// colPName
			// 
			this.colPName.Text = "Zone Name";
			this.colPName.Width = 150;
			// 
			// colPView
			// 
			this.colPView.Text = "View";
			this.colPView.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.colPView.Width = 50;
			// 
			// colPEdit
			// 
			this.colPEdit.Text = "Edit";
			this.colPEdit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.colPEdit.Width = 50;
			// 
			// colPAdd
			// 
			this.colPAdd.Text = "Add";
			this.colPAdd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.colPAdd.Width = 50;
			// 
			// colPDel
			// 
			this.colPDel.Text = "Delete";
			this.colPDel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.colPDel.Width = 50;
			// 
			// colPReport
			// 
			this.colPReport.Text = "Report";
			this.colPReport.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.colPReport.Width = 50;
			// 
			// colPExport
			// 
			this.colPExport.Text = "Export";
			this.colPExport.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.colPExport.Width = 50;
			// 
			// colPChangeDoc
			// 
			this.colPChangeDoc.Text = "Chg...Status";
			this.colPChangeDoc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.colPChangeDoc.Width = 90;
			// 
			// ctxPermission
			// 
			this.ctxPermission.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
				{
					this.mnuItmPReset,
					this.mnuItmPRefresh
				});
			this.ctxPermission.Popup += new System.EventHandler(this.ctxPermission_Popup);
			// 
			// mnuItmPReset
			// 
			this.mnuItmPReset.Index = 0;
			this.mnuItmPReset.Text = "Reset Permissions";
			this.mnuItmPReset.Click += new System.EventHandler(this.mnuItmPReset_Click);
			// 
			// mnuItmPRefresh
			// 
			this.mnuItmPRefresh.Index = 1;
			this.mnuItmPRefresh.Text = "Refresh";
			this.mnuItmPRefresh.Click += new System.EventHandler(this.mnuItmPRefresh_Click);
			// 
			// ultraTabPageControl1
			// 
			this.ultraTabPageControl1.Controls.Add(this.txtUserGroupId);
			this.ultraTabPageControl1.Controls.Add(this.tabSetup);
			this.ultraTabPageControl1.Controls.Add(this.txtUserGroupDesc);
			this.ultraTabPageControl1.Controls.Add(this.txtUserGroupName);
			this.ultraTabPageControl1.Controls.Add(this.label4);
			this.ultraTabPageControl1.Controls.Add(this.label3);
			this.ultraTabPageControl1.Controls.Add(this.label2);
			this.ultraTabPageControl1.Location = new System.Drawing.Point(2, 21);
			this.ultraTabPageControl1.Name = "ultraTabPageControl1";
			this.ultraTabPageControl1.Size = new System.Drawing.Size(684, 567);
			// 
			// txtUserGroupId
			// 
			this.txtUserGroupId.AutoSize = false;
			this.txtUserGroupId.BackColor = System.Drawing.SystemColors.ControlLight;
			this.txtUserGroupId.Location = new System.Drawing.Point(132, 16);
			this.txtUserGroupId.MaxLength = 4;
			this.txtUserGroupId.Name = "txtUserGroupId";
			this.txtUserGroupId.Size = new System.Drawing.Size(100, 24);
			this.txtUserGroupId.TabIndex = 3;
			this.txtUserGroupId.Text = "";
			this.txtUserGroupId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyEnter);
			this.txtUserGroupId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserGroupId_KeyPress);
			this.txtUserGroupId.Enter += new System.EventHandler(this.TextBox_Enter);
			// 
			// tabSetup
			// 
			this.tabSetup.Controls.Add(this.ultraTabSharedControlsPage2);
			this.tabSetup.Controls.Add(this.ultraTabPageControl2);
			this.tabSetup.Controls.Add(this.ultraTabPageControl3);
			this.tabSetup.Location = new System.Drawing.Point(4, 112);
			this.tabSetup.Name = "tabSetup";
			this.tabSetup.SharedControlsPage = this.ultraTabSharedControlsPage2;
			this.tabSetup.Size = new System.Drawing.Size(676, 452);
			this.tabSetup.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			this.tabSetup.TabIndex = 51;
			ultraTab1.TabPage = this.ultraTabPageControl2;
			ultraTab1.Text = "Users";
			ultraTab2.TabPage = this.ultraTabPageControl3;
			ultraTab2.Text = "Permissions";
			this.tabSetup.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[]
				{
					ultraTab1,
					ultraTab2
				});
			// 
			// ultraTabSharedControlsPage2
			// 
			this.ultraTabSharedControlsPage2.Location = new System.Drawing.Point(-10000, -10000);
			this.ultraTabSharedControlsPage2.Name = "ultraTabSharedControlsPage2";
			this.ultraTabSharedControlsPage2.Size = new System.Drawing.Size(672, 429);
			// 
			// txtUserGroupDesc
			// 
			this.txtUserGroupDesc.AutoSize = false;
			this.txtUserGroupDesc.BackColor = System.Drawing.Color.Beige;
			this.txtUserGroupDesc.Location = new System.Drawing.Point(132, 64);
			this.txtUserGroupDesc.MaxLength = 255;
			this.txtUserGroupDesc.Name = "txtUserGroupDesc";
			this.txtUserGroupDesc.Size = new System.Drawing.Size(284, 24);
			this.txtUserGroupDesc.TabIndex = 5;
			this.txtUserGroupDesc.Text = "";
			this.txtUserGroupDesc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyEnter);
			this.txtUserGroupDesc.Enter += new System.EventHandler(this.TextBox_Enter);
			// 
			// txtUserGroupName
			// 
			this.txtUserGroupName.AutoSize = false;
			this.txtUserGroupName.BackColor = System.Drawing.Color.Beige;
			this.txtUserGroupName.Location = new System.Drawing.Point(132, 40);
			this.txtUserGroupName.MaxLength = 255;
			this.txtUserGroupName.Name = "txtUserGroupName";
			this.txtUserGroupName.Size = new System.Drawing.Size(284, 24);
			this.txtUserGroupName.TabIndex = 4;
			this.txtUserGroupName.Text = "";
			this.txtUserGroupName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyEnter);
			this.txtUserGroupName.Enter += new System.EventHandler(this.TextBox_Enter);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.label4.Location = new System.Drawing.Point(20, 68);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(66, 17);
			this.label4.TabIndex = 47;
			this.label4.Text = "Description";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.label3.Location = new System.Drawing.Point(20, 44);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 17);
			this.label3.TabIndex = 46;
			this.label3.Text = "Name ";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.label2.Location = new System.Drawing.Point(20, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 17);
			this.label2.TabIndex = 45;
			this.label2.Text = "Group ID";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.lvGroup);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(240, 590);
			this.panel1.TabIndex = 0;
			// 
			// lvGroup
			// 
			this.lvGroup.BackColor = System.Drawing.Color.White;
			this.lvGroup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[]
				{
					this.colID,
					this.colName
				});
			this.lvGroup.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvGroup.FullRowSelect = true;
			this.lvGroup.GridLines = true;
			this.lvGroup.Location = new System.Drawing.Point(0, 40);
			this.lvGroup.MultiSelect = false;
			this.lvGroup.Name = "lvGroup";
			this.lvGroup.Size = new System.Drawing.Size(240, 550);
			this.lvGroup.TabIndex = 2;
			this.lvGroup.View = System.Windows.Forms.View.Details;
			this.lvGroup.SelectedIndexChanged += new System.EventHandler(this.lvGroup_SelectedIndexChanged);
			// 
			// colID
			// 
			this.colID.Text = "ID";
			this.colID.Width = 80;
			// 
			// colName
			// 
			this.colName.Text = "Name";
			this.colName.Width = 150;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.btnSearch);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.txtSearch);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(240, 40);
			this.panel2.TabIndex = 0;
			// 
			// btnSearch
			// 
			appearance5.Image = ((object) (resources.GetObject("appearance5.Image")));
			this.btnSearch.Appearance = appearance5;
			this.btnSearch.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
			this.btnSearch.ImageSize = new System.Drawing.Size(14, 14);
			this.btnSearch.Location = new System.Drawing.Point(204, 8);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(25, 23);
			this.btnSearch.TabIndex = 1;
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (222)));
			this.label1.Location = new System.Drawing.Point(4, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 17);
			this.label1.TabIndex = 44;
			this.label1.Text = "Search for :";
			// 
			// txtSearch
			// 
			this.txtSearch.AutoSize = false;
			this.txtSearch.BackColor = System.Drawing.SystemColors.Control;
			this.txtSearch.Location = new System.Drawing.Point(76, 8);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(128, 24);
			this.txtSearch.TabIndex = 0;
			this.txtSearch.Text = "%";
			this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyEnter);
			this.txtSearch.Enter += new System.EventHandler(this.TextBox_Enter);
			// 
			// splitter1
			// 
			this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitter1.Location = new System.Drawing.Point(240, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(4, 590);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// ultraTabSharedControlsPage1
			// 
			this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(684, 567);
			// 
			// tab
			// 
			this.tab.BackColor = System.Drawing.Color.WhiteSmoke;
			this.tab.Controls.Add(this.ultraTabSharedControlsPage1);
			this.tab.Controls.Add(this.ultraTabPageControl1);
			this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tab.Location = new System.Drawing.Point(244, 0);
			this.tab.Name = "tab";
			this.tab.SharedControlsPage = this.ultraTabSharedControlsPage1;
			this.tab.Size = new System.Drawing.Size(688, 590);
			this.tab.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			this.tab.TabIndex = 2;
			ultraTab3.Key = "UGRP";
			ultraTab3.TabPage = this.ultraTabPageControl1;
			ultraTab3.Text = "User Group Info.";
			this.tab.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[]
				{
					ultraTab3
				});
			// 
			// FrmUserGroup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(932, 590);
			this.Controls.Add(this.tab);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.panel1);
			this.Name = "FrmUserGroup";
			this.Text = "USER GROUP MANAGEMENT";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FrmUserGroup_Load);
			this.Closed += new System.EventHandler(this.FrmUserGroup_Closed);
			this.ultraTabPageControl2.ResumeLayout(false);
			this.ultraTabPageControl3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) (this.ultraGroupBox1)).EndInit();
			this.ultraGroupBox1.ResumeLayout(false);
			this.ultraTabPageControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) (this.tabSetup)).EndInit();
			this.tabSetup.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) (this.tab)).EndInit();
			this.tab.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		#region IFormChildAction Members

		public void Open()
		{
			SetupUserGroup();
			SetupZones();
		}

		private void SetupZones()
		{
			try
			{
				this.cboZone.DataSource		= SecurityManager.loadZones();
				this.cboZone.DisplayMember  = "Name";
				this.cboZone.ValueMember	= "ID";

			}catch{}
		}

		private void SetupUserGroup()
		{
			try
			{
				this.cboUserGroup.Items.Clear();

				this.cboUserGroup.DataSource = UserGroupManager.findGroups();
				this.cboUserGroup.DisplayMember = "Name";
				this.cboUserGroup.ValueMember = "ID";

			}catch{}
		}

		public void New()
		{
			this.Clear();

			this.txtUserGroupId.Enabled = true;
			this.txtUserGroupId.Focus();

			this.ActionStatus = ActionStatus.AddNew;
			this.m_UserActionStatus = ActionStatus.None;
			this.m_PermissionActionStatus = ActionStatus.None;

			this.EnablePermissionControl(ActionStatus.ReadOnly);
			this.EnableUserControl(ActionStatus.ReadOnly);

			this.ControlActionFlowOfUserAccount(ActionStatus.ReadOnly);
			this.ControlActionFlowOfPermission(ActionStatus.ReadOnly);
		}

		public void Save()
		{
			try
			{
				string[] text = new string[]
					{
						this.txtUserGroupId.Text,
						this.txtUserGroupName.Text
					};

				if (StringHelper.CheckStringEmpty(text))
					throw new Exception("กรุณากรอกข้อมูลให้เรียบร้อย.");

				if (this.UserGroup == null)
				{
					this.UserGroup = new UserGroupInfo();
				}

				this.UserGroup.ID = Convert.ToInt32(this.txtUserGroupId.Text);
				this.UserGroup.Name = this.txtUserGroupName.Text;
				this.UserGroup.Description = this.txtUserGroupDesc.Text;
				this.UserGroup.Enable = true;

				if (this.ActionStatus == ActionStatus.AddNew)
				{
					UserGroupManager.add(this.UserGroup, ApplicationContext.Info.AccountId);
				}
				else if (this.ActionStatus == ActionStatus.Save)
				{
					UserGroupManager.save(this.UserGroup, ApplicationContext.Info.AccountId);
				}
				MessageBox.Show(this, "บันทึกข้อมูลเรียบร้อย", "ผลการทำงาน", MessageBoxButtons.OK, MessageBoxIcon.Information);

				this.ActionStatus = ActionStatus.Save;
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public void Delete()
		{
			try
			{
				if (this.ActionStatus == ActionStatus.AddNew)
				{
					this.Clear();
					this.ActionStatus = ActionStatus.None;
				}
				else
				{
					DialogResult result = MessageBox.Show(this, string.Format("ยืนยันการลบข้อมูล UserGroup {0} : {1}", this.UserGroup.ID.ToString(), this.UserGroup.Name)
					                                      , "ยืนยัน"
					                                      , MessageBoxButtons.YesNo
					                                      , MessageBoxIcon.Question);

					if (result == DialogResult.Yes)
					{
						UserGroupManager.remove(this.UserGroup.ID);
						this.NormalSearch();

						MessageBox.Show(this, "บันทึกข้อมูลเรียบร้อย", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

						this.ActionStatus = ActionStatus.None;
						this.Clear();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void NormalSearch(int userGroupId)
		{
			try
			{
				this.lvUsers.Items.Clear();

				int usergroupId = Convert.ToInt32(userGroupId);
				this.UserGroup = UserGroupManager.findGroup(usergroupId);

				this.Display();
				this.ActionStatus = ActionStatus.Save;
				this.txtUserGroupId.Enabled = false;
			}
			catch
			{
				this.Clear();
				this.ActionStatus = ActionStatus.None;
			}
			finally
			{
				if (this.ActionStatus == ActionStatus.None)
				{
					this.EnableUserControl(ActionStatus.ReadOnly);
					this.EnablePermissionControl(ActionStatus.ReadOnly);

					this.ControlActionFlowOfPermission(ActionStatus.ReadOnly);
					this.ControlActionFlowOfUserAccount(ActionStatus.ReadOnly);
				}
				else
				{
					this.EnableUserControl(this.m_UserActionStatus);
					this.EnablePermissionControl(this.m_PermissionActionStatus);

					this.ControlActionFlowOfPermission(this.m_PermissionActionStatus);
					this.ControlActionFlowOfUserAccount(this.m_UserActionStatus);
				}
			}
		}

		public void NormalSearch()
		{
			try
			{
				this.ClearUserInfo();
				this.ClearPermissionInfo();

				this.NormalSearch(Convert.ToInt32(this.lvGroup.SelectedItems[0].Text));
			}
			catch
			{
				this.Clear();

				this.ActionStatus = ActionStatus.None;

				this.EnableUserControl(ActionStatus.ReadOnly);
				this.EnablePermissionControl(ActionStatus.ReadOnly);

				this.ControlActionFlowOfPermission(ActionStatus.ReadOnly);
				this.ControlActionFlowOfUserAccount(ActionStatus.ReadOnly);
			}
		}


		public void AdvanceSearch()
		{
			try
			{
				string keyword = this.txtSearch.Text;
				if (keyword.Equals(""))
				{
					keyword = "%";
					this.txtSearch.Text = keyword;
				}
				this.lvGroup.Items.Clear();

				UserGroupCollection groupList = UserGroupManager.findGroups(keyword);

				if (groupList.Count > 0)
				{
					foreach (UserGroupInfo obj in groupList)
					{
						ListViewItem item = new ListViewItem(obj.ID.ToString());
						item.SubItems.Add(obj.Name);

						this.lvGroup.Items.Add(item);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		public void Undo()
		{
			// TODO:  Add FrmUserGroup.Undo implementation
		}

		public void Redo()
		{
			// TODO:  Add FrmUserGroup.Redo implementation
		}

		public void Reload()
		{
			// TODO:  Add FrmUserGroup.Reload implementation
		}

		public void Clear()
		{
			this.lvUsers.Items.Clear();
			this.lvPermission.Items.Clear();

			this.txtUserGroupId.Text = "";
			this.txtUserGroupName.Text = "";
			this.txtUserGroupDesc.Text = "";
			this.tab.Tabs[0].Text = "USER GROUP : ";

			this.ClearUserInfo();
			this.ClearPermissionInfo();
		}

		private void ClearUserInfo()
		{
			this.txtUserId.Text = "";
			this.txtFullName.Text = "";
			this.txtEmail.Text = "";
			this.txtDescription.Text = "";
			this.tabSetup.Tabs[0].Text = "USER ACCOUNT : ";

			this.lblLastLogIn.Text = "";
			this.lblLastComputerUse.Text = "";

			this.chkCannotChangePwd.Enabled = true;
			this.chkChangPwdLogon.Enabled = true;
			this.chkDisableAccount.Enabled = true;
			this.chkPwdNeverExpired.Enabled = true;

			this.chkCannotChangePwd.Checked = false;
			this.chkChangPwdLogon.Checked = false;
			this.chkDisableAccount.Checked = false;
			this.chkPwdNeverExpired.Checked = false;
		}

		private void ClearPermissionInfo()
		{
			this.tabSetup.Tabs[1].Text = "PERMISSION : ";

			this.chkAllowAddNew.Checked = false;
			this.chkAllowDelete.Checked = false;
			this.chkAllowEdit.Checked = false;
			this.chkAllowExport.Checked = false;
			this.chkAllowReport.Checked = false;
			this.chkAllowView.Checked = false;
			this.chkAllowChangeDocStatus.Checked = false;

			this.cboZone.Enabled = false;
			this.chkAllowAddNew.Enabled = false;
			this.chkAllowDelete.Enabled = false;
			this.chkAllowEdit.Enabled = false;
			this.chkAllowExport.Enabled = false;
			this.chkAllowReport.Enabled = false;
			this.chkAllowView.Enabled = false;
			this.chkAllowChangeDocStatus.Enabled = false;
		}

		public void Display()
		{
			try
			{
				if (this.UserGroup == null)
				{
					this.Clear();
				}
				else
				{
					this.tab.Tabs[0].Text = "USER GROUP : " + this.UserGroup.ID.ToString();

					this.txtUserGroupId.Text = this.UserGroup.ID.ToString();
					this.txtUserGroupName.Text = this.UserGroup.Name;
					this.txtUserGroupDesc.Text = this.UserGroup.Description;

					if (this.UserGroup.Members != null || this.UserGroup.Members.Count > 0)
					{
						foreach (UserAccountInfo account in this.UserGroup.Members)
						{
							ListViewItem item = new ListViewItem(account.AccountId);
							item.SubItems.Add(account.FullName);
							item.SubItems.Add(account.Description);
							this.lvUsers.Items.Add(item);
						}
					}
					else
					{
						this.ClearUserInfo();
					}

					if (this.UserGroup.Permission != null || this.UserGroup.Permission.Count > 0)
					{
						this.DisplayUserGroupPermission();
					}
					else
					{
						this.ClearPermissionInfo();
					}
				}
			}
			catch
			{
			}
		}

		private void DisplayUserGroupPermission()
		{
			this.lvPermission.Items.Clear();

			foreach (SystemZone zone in this.UserGroup.Permission)
			{
				ListViewItem item = new ListViewItem(zone.ZoneType);

				item.SubItems.Add(zone.ID.ToString());
				item.SubItems.Add(zone.Name);

				item.SubItems.Add(StringHelper.ConvertBoolToYN(zone.Permission.ViewEnable, "-"));
				item.SubItems.Add(StringHelper.ConvertBoolToYN(zone.Permission.EditEnable, "-"));
				item.SubItems.Add(StringHelper.ConvertBoolToYN(zone.Permission.AddNewEnable, "-"));
				item.SubItems.Add(StringHelper.ConvertBoolToYN(zone.Permission.DeleteEnable, "-"));
				item.SubItems.Add(StringHelper.ConvertBoolToYN(zone.Permission.ReportEnable, "-"));
				item.SubItems.Add(StringHelper.ConvertBoolToYN(zone.Permission.ExportEnable, "-"));
				item.SubItems.Add(StringHelper.ConvertBoolToYN(zone.Permission.ChangeDocStatusEnable, "-"));

				this.lvPermission.Items.Add(item);
			}
		}

		public void EnableControl(ActionStatus status)
		{
			bool enable = true;

			if (status == ActionStatus.ReadOnly || status == ActionStatus.None)
			{
				this.txtUserGroupId.Enabled = false;
				this.m_UserActionStatus = status;
				this.m_PermissionActionStatus = status;

				this.EnableUserControl(ActionStatus.ReadOnly);
				this.EnablePermissionControl(ActionStatus.ReadOnly);

				this.ControlActionFlowOfUserAccount(ActionStatus.ReadOnly);
				this.ControlActionFlowOfPermission(ActionStatus.ReadOnly);

				enable = false;

			}
			else if (status == ActionStatus.AddNew)
			{
				this.txtUserGroupId.Enabled = true;

				this.m_UserActionStatus = ActionStatus.None;
				this.m_PermissionActionStatus = ActionStatus.None;

				this.EnableUserControl(ActionStatus.ReadOnly);
				this.EnablePermissionControl(ActionStatus.ReadOnly);

				this.ControlActionFlowOfUserAccount(ActionStatus.ReadOnly);
				this.ControlActionFlowOfPermission(ActionStatus.ReadOnly);
			}
			else if (status == ActionStatus.Save)
			{
				this.txtUserGroupId.Enabled = false;

				this.EnableUserControl(this.m_UserActionStatus);
				this.EnablePermissionControl(this.m_PermissionActionStatus);

				this.ControlActionFlowOfUserAccount(this.m_UserActionStatus);
				this.ControlActionFlowOfPermission(this.m_PermissionActionStatus);
			}

			this.txtUserGroupName.Enabled = enable;
			this.txtUserGroupDesc.Enabled = enable;
			this.txtDescription.Enabled = enable;
		}

		private void EnableUserControl(ActionStatus status)
		{
			bool enable = true;

			if (status == ActionStatus.ReadOnly || status == ActionStatus.None)
			{
				enable = false;
			}

			if (status == ActionStatus.AddNew)
			{
				this.txtUserId.Enabled = true;
			}
			else if (status == ActionStatus.Save)
			{
				this.txtUserId.Enabled = false;
			}
			else
			{
				this.txtUserId.Enabled = enable;
			}

			this.txtEmail.Enabled = enable;
			this.cboUserGroup.Enabled = enable;
			this.txtFullName.Enabled = enable;
			this.txtDescription.Enabled = enable;

			this.chkCannotChangePwd.Enabled = enable;
			this.chkChangPwdLogon.Enabled = enable;
			this.chkDisableAccount.Enabled = enable;
			this.chkPwdNeverExpired.Enabled = enable;
		}

		private void ControlActionFlowOfUserAccount(ActionStatus status)
		{
			this.DisableUserActionControl();

			switch (status)
			{
				case ActionStatus.None:
					this.btnUAdd.Enabled = this.PermissionInfo.AddNewEnable;
					break;

				case ActionStatus.AddNew:
					this.btnUSave.Enabled = true;
					this.btnUDelete.Enabled = true;
					break;

				case ActionStatus.Save:
					this.btnUAdd.Enabled = this.PermissionInfo.AddNewEnable;
					this.btnUSave.Enabled = this.PermissionInfo.EditEnable;
					this.btnUDelete.Enabled = this.PermissionInfo.DeleteEnable;
					break;

				case ActionStatus.ReadOnly:
					this.btnUAdd.Enabled = false;
					this.btnUDelete.Enabled = false;
					this.btnUSave.Enabled = false;
					break;
			}

		}

		private void EnablePermissionControl(ActionStatus status)
		{
			bool enable = false;

			if (status == ActionStatus.ReadOnly || status == ActionStatus.None)
			{
				enable = false;
			}

			this.chkAllowAddNew.Enabled = enable;
			this.chkAllowDelete.Enabled = enable;
			this.chkAllowEdit.Enabled = enable;
			this.chkAllowExport.Enabled = enable;
			this.chkAllowReport.Enabled = enable;
			this.chkAllowView.Enabled = enable;
			this.chkAllowChangeDocStatus.Enabled = enable;

		}

		private void ControlActionFlowOfPermission(ActionStatus status)
		{
			this.DisablePermissionActionControl();

			switch (status)
			{
				case ActionStatus.None:
					break;

				case ActionStatus.AddNew:
					this.btnPSave.Enabled = true;
					break;

				case ActionStatus.Save:
					this.btnPSave.Enabled = this.PermissionInfo.EditEnable;
					break;

				case ActionStatus.ReadOnly:
					this.btnPSave.Enabled = false;
					break;
			}

		}

		private void DisableUserActionControl()
		{
			this.btnUAdd.Enabled = false;
			this.btnUSave.Enabled = false;
			this.btnUDelete.Enabled = false;
		}

		private void DisablePermissionActionControl()
		{
			this.btnPSave.Enabled = false;
		}

		#endregion

		private void btnSearch_Click(object sender, EventArgs e)
		{
			this.AdvanceSearch();
		}

		private void chkOption_CheckedChanged(object sender, EventArgs e)
		{
			if (this.chkChangPwdLogon.Checked)
			{
				this.chkCannotChangePwd.Enabled = false;
				this.chkPwdNeverExpired.Enabled = false;
			}
			else
			{
				this.chkCannotChangePwd.Enabled = true;
				this.chkPwdNeverExpired.Enabled = true;
			}

			if (this.chkCannotChangePwd.Checked || this.chkPwdNeverExpired.Checked)
			{
				this.chkChangPwdLogon.Enabled = false;
			}
			else
			{
				this.chkChangPwdLogon.Enabled = true;
			}
		}

		private void FrmUserGroup_Load(object sender, EventArgs e)
		{
			this.IsOpened = true;
			this.Open();
			this.Clear();
		}

		private void lvGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NormalSearch();
		}

		private void lvUsers_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				this.SearchUserAccount(this.lvUsers.SelectedItems[0].Text);
			}
			catch
			{
			}
		}

		private void SearchZonePermission(int zoneId)
		{
			try
			{
				bool result = false;
				SystemZone zone = null;

				if (this.UserGroup.Permission != null || this.UserGroup.Permission.Count > 0)
				{
					foreach (SystemZone zoneItem in this.UserGroup.Permission)
					{
						if (zoneItem.ID == zoneId)
						{
							zone = zoneItem;
							result = true;
							break;
						}
					}
				}

				if (result)
				{
					this.tabSetup.Tabs[1].Text = "PERMISSION : " + zone.ID.ToString();
					this.cboZone.SelectedValue = zone.ID;

					this.chkAllowAddNew.Checked = zone.Permission.AddNewEnable;
					this.chkAllowEdit.Checked = zone.Permission.EditEnable;
					this.chkAllowView.Checked = zone.Permission.ViewEnable;
					this.chkAllowDelete.Checked = zone.Permission.DeleteEnable;
					this.chkAllowReport.Checked = zone.Permission.ReportEnable;
					this.chkAllowExport.Checked = zone.Permission.ExportEnable;
					this.chkAllowChangeDocStatus.Checked = zone.Permission.ChangeDocStatusEnable;

					this.EnablePermissionControl(zone.ZoneType);
					this.ControlActionFlowOfPermission(ActionStatus.Save);
				}
				else
				{
					this.ClearPermissionInfo();
					this.ControlActionFlowOfPermission(ActionStatus.ReadOnly);
				}
			}
			catch
			{
				this.ClearPermissionInfo();
				this.ControlActionFlowOfPermission(ActionStatus.ReadOnly);
			}
		}

		private void SearchUserAccount(string accountId)
		{
			try
			{
				bool result = false;

				UserAccountInfo account = null;

				if (this.UserGroup.Members != null || this.UserGroup.Members.Count > 0)
				{
					foreach (UserAccountInfo accountInfo in this.UserGroup.Members)
					{
						if (accountInfo.AccountId.Equals(accountId))
						{
							account = accountInfo;
							result = true;
							break;
						}
					}
				}

				if (result)
				{
					this.m_UserActionStatus = ActionStatus.Save;

					this.EnableUserControl(ActionStatus.Save);
					this.ControlActionFlowOfUserAccount(ActionStatus.Save);

					this.tabSetup.Tabs[0].Text = "USER : " + account.AccountId;
					this.txtUserId.Enabled = false;
					this.txtUserId.Text = account.AccountId;
					this.txtFullName.Text = account.FullName;
					this.txtEmail.Text = account.Email;
					this.txtDescription.Text = account.Description;
					this.cboUserGroup.SelectedValue = account.UserGroupId;

					if (account.Enable)
					{
						this.chkDisableAccount.Checked = false;
					}
					else
					{
						this.chkDisableAccount.Checked = true;
					}

					this.chkCannotChangePwd.Checked = account.CannotChangePassword;
					this.chkChangPwdLogon.Checked = account.ChangePasswordAtNextLogon;
					this.chkPwdNeverExpired.Checked = account.PasswordNeverExpires;
				}
				else
				{
					this.m_UserActionStatus = ActionStatus.None;
					this.ClearUserInfo();
				}
			}
			catch
			{
			}
		}

		private UserGroupInfo UserGroup
		{
			get { return this.m_UserGroup; }
			set { this.m_UserGroup = value; }
		}

		private void EnablePermissionControl(string zoneTypeCode)
		{
			switch (zoneTypeCode)
			{
				case "FRM":
					this.chkAllowAddNew.Enabled = true;
					this.chkAllowDelete.Enabled = true;
					this.chkAllowEdit.Enabled = true;
					this.chkAllowView.Enabled = true;

					this.chkAllowReport.Enabled = false;
					this.chkAllowExport.Enabled = false;
					this.chkAllowChangeDocStatus.Enabled = false;
					break;

				case "DOC":
					this.chkAllowAddNew.Enabled = true;
					this.chkAllowDelete.Enabled = true;
					this.chkAllowEdit.Enabled = true;
					this.chkAllowView.Enabled = true;
					this.chkAllowReport.Enabled = true;

					this.chkAllowExport.Enabled = false;
					this.chkAllowChangeDocStatus.Enabled = false;

					break;

				case "EXPRT":
					this.chkAllowView.Enabled = true;
					this.chkAllowExport.Enabled = true;

					this.chkAllowAddNew.Enabled = false;
					this.chkAllowDelete.Enabled = false;
					this.chkAllowEdit.Enabled = false;
					this.chkAllowReport.Enabled = false;
					this.chkAllowChangeDocStatus.Enabled = false;
					break;

				case "RPT":
					this.chkAllowView.Enabled = true;
					this.chkAllowReport.Enabled = true;
					this.chkAllowExport.Enabled = true;

					this.chkAllowAddNew.Enabled = false;
					this.chkAllowDelete.Enabled = false;
					this.chkAllowEdit.Enabled = false;
					this.chkAllowChangeDocStatus.Enabled = false;
					break;

				case "DOCAPV":
					this.chkAllowView.Enabled = true;
					this.chkAllowChangeDocStatus.Enabled = true;

					this.chkAllowReport.Enabled = false;
					this.chkAllowExport.Enabled = false;
					this.chkAllowAddNew.Enabled = false;
					this.chkAllowDelete.Enabled = false;
					this.chkAllowEdit.Enabled = false;

					break;

				default:
					break;
			}
		}

		private void lvPermission_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				string id = lvPermission.SelectedItems[0].SubItems[1].Text;
				this.SearchZonePermission(Convert.ToInt32(id));
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		private void btnUAdd_Click(object sender, EventArgs e)
		{
			this.AddNewUser();
		}

		private void btnUDelete_Click(object sender, EventArgs e)
		{
			this.DeleteUser();
		}

		private void btnUSave_Click(object sender, EventArgs e)
		{
			this.SaveUser();
		}

		private void AddNewUser()
		{
			this.ClearUserInfo();
			this.m_UserActionStatus = ActionStatus.AddNew;

			this.cboUserGroup.SelectedValue = this.UserGroup.ID;

			this.EnableUserControl(ActionStatus.AddNew);
			this.ControlActionFlowOfUserAccount(ActionStatus.AddNew);

			this.SetupUserGroup();
			this.txtUserId.Focus();
		}

		private void SaveUser()
		{
			try
			{
				string[] text = new string[]
					{
						this.txtUserId.Text,
						this.txtFullName.Text
					};

				if (StringHelper.CheckStringEmpty(text))
					throw new Exception("กรุณากรอกข้อมูลให้เรียบร้อย");

				UserAccountInfo account = new UserAccountInfo();

				account.AccountId = this.txtUserId.Text;
				account.FullName = this.txtFullName.Text;
				account.Description = this.txtDescription.Text;
				account.Email = this.txtEmail.Text;

				if (this.chkDisableAccount.Checked)
				{
					account.Enable = false;
				}
				else
				{
					account.Enable = true;
				}
				account.CannotChangePassword = this.chkCannotChangePwd.Checked;
				account.ChangePasswordAtNextLogon = this.chkChangPwdLogon.Checked;
				account.PasswordNeverExpires = this.chkPwdNeverExpired.Checked;

				account.UserGroupId = Convert.ToInt32(this.cboUserGroup.SelectedValue);

				if (this.m_UserActionStatus == ActionStatus.AddNew)
				{
					string password = string.Empty;
					password = GetNewPassword(false);

					UserAccountManager.add(account, password, ApplicationContext.Info.AccountId);
				}
				else
				{
					UserAccountManager.save(account, ApplicationContext.Info.AccountId);
				}
				MessageBox.Show(this, "บันทึกข้อมูลเรียบร้อย", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

				this.m_UserActionStatus = ActionStatus.Save;

				this.NormalSearch(Convert.ToInt32(this.txtUserGroupId.Text));
				this.EnableUserControl(ActionStatus.Save);
				this.ControlActionFlowOfUserAccount(ActionStatus.Save);
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private string GetNewPassword(bool isChangePassword)
		{
			string password = string.Empty;
			bool flag = true;

			while (flag)
			{
				FrmSetPassword frm = new FrmSetPassword();
				frm.NeedChangePassword = isChangePassword;
				frm.ShowDialog(this);

				if (frm.Password.Equals(string.Empty))
				{
					if (isChangePassword)
					{
						password = string.Empty;
						flag = false;
					}
					else
					{
						MessageBox.Show(this, "กรุณาระบุรหัสผ่านสำหรับ User ใหม่", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
				else
				{
					password = frm.Password;
					flag = false;
				}
			}
			return password;
		}

		private void DeleteUser()
		{
			DialogResult result = MessageBox.Show(this, string.Format("ยืนยันการลบข้อมูล Account {0}", this.txtUserId.Text), "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

			if (result == DialogResult.Yes)
			{
				try
				{
					if (this.txtUserId.Text.Equals(ApplicationContext.Info.AccountId))
					{
						MessageBox.Show(this, string.Format("Account {0} ถูกใช้งานอยู่ในขณะนี้", ApplicationContext.Info.AccountId), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					else
					{
						UserAccountManager.remove(this.txtUserId.Text);
						MessageBox.Show(this, "ลบข้อมูลผู้ใช้เรียบร้อย", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

						this.m_UserActionStatus = ActionStatus.None;

						this.NormalSearch(Convert.ToInt32(this.txtUserGroupId.Text));
						this.ClearUserInfo();
						this.EnableUserControl(ActionStatus.None);
						this.ControlActionFlowOfUserAccount(ActionStatus.None);
					}

				}
				catch (Exception ex)
				{
					MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void txtUserGroupId_KeyPress(object sender, KeyPressEventArgs e)
		{
			KeyPressManager.EnterNumericOnly(e);
		}

		private void KeyEnter(object sender, KeyEventArgs e)
		{
			KeyPressManager.Enter(e);
		}

		private void TextBox_Enter(object sender, EventArgs e)
		{
			try
			{
				KeyPressManager.SelectAllTextBox(sender);
			}
			catch
			{
			}
		}

		private void ctxUser_Popup(object sender, EventArgs e)
		{
			if (this.lvUsers.SelectedItems.Count > 0)
			{
				this.ctxUser.MenuItems[0].Enabled = true;
			}
			else
			{
				this.ctxUser.MenuItems[0].Enabled = false;
			}

		}

		private void mnuItmRefresh_Click(object sender, EventArgs e)
		{
			try
			{
				this.NormalSearch(Convert.ToInt32(this.txtUserGroupId.Text));
			}
			catch
			{
			}
		}

		private void mnuItmSetPwd_Click(object sender, EventArgs e)
		{
			try
			{
				FrmSetPassword frm = new FrmSetPassword();
				frm.ShowDialog(this);

				if (frm.Password.Length > 0)
				{
					string accountId = this.lvUsers.SelectedItems[0].Text;
					UserAccountManager.changePassword(accountId, frm.Password, ApplicationContext.Info.AccountId);
					MessageBox.Show(this, "The password has been set.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch
			{
			}
		}

		private void mnuItmPRefresh_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.UserGroup != null)
				{
					this.UserGroup.Permission = SecurityManager.assignDefaultPermissions(this.UserGroup.ID);
					this.SetupZones();
					this.DisplayUserGroupPermission();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void mnuItmPReset_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.UserGroup != null)
				{
					this.UserGroup.Permission = SecurityManager.resetDefaultPermissions(this.UserGroup.ID, ApplicationContext.Info.AccountId);
					this.DisplayUserGroupPermission();
					MessageBox.Show(this, "กำหนดสิทธิการใช้งานเรียบร้อย", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void ctxPermission_Popup(object sender, EventArgs e)
		{
			if (this.UserGroup != null)
			{
				this.ctxPermission.MenuItems[0].Enabled = true;
				this.ctxPermission.MenuItems[1].Enabled = true;
			}
			else
			{
				this.ctxPermission.MenuItems[0].Enabled = false;
				this.ctxPermission.MenuItems[1].Enabled = false;
			}
		}

		private void btnPSave_Click(object sender, EventArgs e)
		{
			if (this.UserGroup != null && this.lvPermission.Items.Count > 0)
			{
				SavePermission();
			}
		}

		private void SavePermission()
		{
			try
			{
				SystemZone zone = new SystemZone();
				zone.ID = Convert.ToInt32(this.cboZone.SelectedValue);
				zone.Permission.AddNewEnable = this.chkAllowAddNew.Checked;
				zone.Permission.DeleteEnable = this.chkAllowDelete.Checked;
				zone.Permission.EditEnable = this.chkAllowEdit.Checked;
				zone.Permission.ExportEnable = this.chkAllowExport.Checked;
				zone.Permission.ReportEnable = this.chkAllowReport.Checked;
				zone.Permission.ViewEnable = this.chkAllowView.Checked;
				zone.Permission.ChangeDocStatusEnable = this.chkAllowChangeDocStatus.Checked;

				this.UserGroup.Permission = SecurityManager.assignPermissions(this.UserGroup.ID, zone, ApplicationContext.Info.AccountId);
				this.DisplayUserGroupPermission();

				MessageBox.Show(this, "กำหนดค่าสิทธิ์การใช้งานเรียบร้อย", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void FrmUserGroup_Closed(object sender, EventArgs e)
		{
			this.IsOpened = false;
		}


	}
}