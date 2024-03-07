using System;
using System.Collections.Generic;
using System.Text;
using DCI.HRMS.Model.Common;

namespace DCI.HRMS.Persistence
{
   	/// <summary>
	/// Summary description for IDocumentDAO.
	/// </summary>
	public interface IKeyGeneratorDao
	{
		string NextId(string key);
		RunningNumber LoadUnique(string key);
	}
}

