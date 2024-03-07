using System;
using System.Globalization;

namespace DCI.HRMS.Model.Common
{
	/// <summary>
	/// Summary description for ObjectInfo.
	/// </summary>
    [Serializable]
	public class ObjectInfo
	{
		private string m_CreateBy = string.Empty;
		private DateTime m_CreateDt =  new DateTime();
		private string m_UpdateBy = string.Empty;
		private DateTime m_UpdateDt = new DateTime();

		public ObjectInfo()
		{
		}

		public string CreateBy
		{
			get { return this.m_CreateBy; }
			set { this.m_CreateBy = value; }
		}

		public DateTime CreateDateTime
		{
			get {  
				if (this.m_CreateDt <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || this.m_CreateDt <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return this.m_CreateDt;
                }
			}
			set { this.m_CreateDt = value; }
		}

		public string LastUpdateBy
		{
			get { return this.m_UpdateBy; }
			set { this.m_UpdateBy = value; }
		}

		public DateTime LastUpdateDateTime
		{
			get { 
                if (this.m_UpdateDt <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || this.m_UpdateDt <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return this.m_UpdateDt;
                }
            }
			set { this.m_UpdateDt = value; }
		}
	}
}