using System.Collections;

namespace DCI.Security.Model
{
	/// <summary>
	/// Summary description for SystemMenu.
	/// </summary>
	public class SystemMenu
	{
		private int m_Id;
		private string m_Key;
		private string m_Name;
		private int m_MainId;
		private string m_Desc;
		private string m_Package;
		private string m_FileInclude;

		private ArrayList m_SubMenu = new ArrayList();
		private int m_RankNo;
		private bool m_Visible;

		public SystemMenu()
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

		public int MainMenuId
		{
			get{ return this.m_MainId; }
			set{ this.m_MainId = value; }
		}
		public string Key
		{
			get { return this.m_Key; }
			set { this.m_Key = value; }
		}

		public string Text
		{
			get { return this.m_Name; }
			set { this.m_Name = value; }
		}

		public string ToolTipText
		{
			get { return this.m_Desc; }
			set { this.m_Desc = value; }
		}

		public int Index
		{
			get { return this.m_RankNo; }
			set { this.m_RankNo = value; }
		}

		public bool Visible
		{
			get { return this.m_Visible; }
			set { this.m_Visible = value; }
		}

		public string Package
		{
			get{ return this.m_Package; }
			set{ this.m_Package = value; }
		}
		public string IncludeFile
		{
			get{ return this.m_FileInclude; }
			set{ this.m_FileInclude = value; }
		}

		public ArrayList SubMenu
		{
			get { return this.m_SubMenu; }
			set { this.m_SubMenu = value; }
		}
	}
}