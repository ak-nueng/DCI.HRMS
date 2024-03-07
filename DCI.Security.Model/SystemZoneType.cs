namespace DCI.Security.Model
{
	/// <summary>
	/// Summary description for SystemZoneType.
	/// </summary>
	public class SystemZoneType
	{
		private string m_Code;
		private string m_Name;
		private bool m_AddNewActive = false;
		private bool m_EditActive = false;
		private bool m_ViewActive = false;
		private bool m_DeleteActive = false;
		private bool m_ReportActive = false;
		private bool m_ExportActive = false;
		private bool m_ChangeDocStatusActive = false;

		public SystemZoneType()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public string Code
		{
			get { return this.m_Code; }
			set { this.m_Code = value; }
		}

		public string Name
		{
			get { return this.m_Name; }
			set { this.m_Name = value; }
		}

		public bool EnableAddNewActive
		{
			get { return this.m_AddNewActive; }
			set { this.m_AddNewActive = value; }
		}

		public bool EnableEditActive
		{
			get { return this.m_EditActive; }
			set { this.m_EditActive = value; }
		}

		public bool EnableViewActive
		{
			get { return this.m_ViewActive; }
			set { this.m_ViewActive = value; }
		}

		public bool EnableDeleteActive
		{
			get { return this.m_DeleteActive; }
			set { this.m_DeleteActive = value; }
		}

		public bool EnableReportActive
		{
			get { return this.m_ReportActive; }
			set { this.m_ReportActive = value; }
		}

		public bool EnableExportActive
		{
			get { return this.m_ExportActive; }
			set { this.m_ExportActive = value; }
		}

		public bool EnableChangeDocumentStatusActive
		{
			get { return this.m_ChangeDocStatusActive; }
			set { this.m_ChangeDocStatusActive = value; }
		}
	}
}