namespace DCI.Security.Model
{
	/// <summary>
	/// Summary description for SystemZone.
	/// </summary>
	public class SystemZone
	{
		private int m_Id;
		private string m_Name;
		private string m_Desc;
		private int m_MenuId;
		private string m_ZoneType;
		private int m_RankNo;
		private string m_Assembly;
		private string m_Object;
		private bool m_Visible = false;
		private bool m_Enable = false;

		private PermissionInfo m_Perms = new PermissionInfo();

		public SystemZone()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public int ID
		{
			get { return this.m_Id; }
			set { this.m_Id = value; }
		}

		public string Name
		{
			get { return this.m_Name; }
			set { this.m_Name = value; }
		}

		public string Description
		{
			get { return this.m_Desc; }
			set { this.m_Desc = value; }
		}

		public int MenuID
		{
			get { return this.m_MenuId; }
			set { this.m_MenuId = value; }
		}

		public int RankNo
		{
			get { return this.m_RankNo; }
			set { this.m_RankNo = value; }
		}

		public string AssemblyName
		{
			get { return this.m_Assembly; }
			set { this.m_Assembly = value; }
		}

		public string TypeName
		{
			get { return this.m_Object; }
			set { this.m_Object = value; }
		}

		public string ZoneType
		{
			get { return this.m_ZoneType; }
			set { this.m_ZoneType = value; }
		}

		public bool Visible
		{
			get { return this.m_Visible; }
			set { this.m_Visible = value; }
		}

		public bool Enable
		{
			get{ return this.m_Enable; }
			set{ this.m_Enable = value; }
		}
		public PermissionInfo Permission
		{
			get { return this.m_Perms; }
		}

	}
}