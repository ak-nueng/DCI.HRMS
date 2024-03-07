namespace DCI.Security.Model
{
	/// <summary>
	/// Summary description for UserGroupPermission.
	/// </summary>
	public class UserGroupPermission
	{
		private UserGroupInfo m_Group;
        private ModuleInfo m_Module;
		private bool m_AddNewEnable = false;
		private bool m_ViewEnable = false;
		private bool m_EditEnable = false;
		private bool m_DeleteEnable = false;
		private bool m_ReportEnable = false;
		private bool m_ExportEnable = false;
		private bool m_ChangeDocStatusEnable = false;

		public UserGroupPermission()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public UserGroupInfo GroupInfo
		{
			get { return this.m_Group; }
			set { this.m_Group = value; }
		}
        public ModuleInfo GroupModuleInfo
        {
            get { return this.m_Module; }
            set { this.m_Module = value; }
        }

		public bool AddNewEnable
		{
			get { return this.m_AddNewEnable; }
			set { this.m_AddNewEnable = value; }
		}

		public bool ViewEnable
		{
			get { return this.m_ViewEnable; }
			set { this.m_ViewEnable = value; }
		}

		public bool EditEnable
		{
			get { return this.m_EditEnable; }
			set { this.m_EditEnable = value; }
		}

		public bool DeleteEnable
		{
			get { return this.m_DeleteEnable; }
			set { this.m_DeleteEnable = value; }
		}

		public bool PrintEnable
		{
			get { return this.m_ReportEnable; }
			set { this.m_ReportEnable = value; }
		}

		public bool ExportEnable
		{
			get { return this.m_ExportEnable; }
			set { this.m_ExportEnable = value; }
		}

		public bool ChangeDocumentStatusEnable
		{
			get { return this.m_ChangeDocStatusEnable; }
			set { this.m_ChangeDocStatusEnable = value; }
		}
	}
}