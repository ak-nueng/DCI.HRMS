using System;
namespace DCI.HRMS.Model.Common
{
	/// <summary>
	/// Summary description for DefaultValue.
	/// </summary>
    [Serializable]
	public class DefaultValue
	{
		private string m_Id = string.Empty;
		private string m_Desc = string.Empty;

		public DefaultValue()
		{
		}

		public DefaultValue(string data, string desc)
		{
			this.m_Id = data;
			this.m_Desc = desc;
		}

		public string DefaultData
		{
			get { return this.m_Id; }
			set { this.m_Id = value; }
		}

		public string DefaultDescription
		{
			get { return this.m_Desc; }
			set { this.m_Desc = value; }
		}
	}
}