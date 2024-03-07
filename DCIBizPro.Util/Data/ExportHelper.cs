using System;
using System.Data;

namespace DCIBizPro.Util.Data
{
	/// <summary>
	/// Summary description for ExportHelper.
	/// </summary>
	public class ExportHelper
	{
		public ExportHelper()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public void run(BaseFile exportFile , DataTable data)
		{
			exportFile.Export(data);
		}
	}
}
