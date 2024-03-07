using System;

namespace DCIBizPro.HRMS
{
	public enum FormAction
	{
		New = 1 , 
		Save = 2 , 
		Delete = 3
	}
	/// <summary>
	/// Summary description for IFormAction.
	/// </summary>
	public interface IFormAction
	{
		void New();
		void Save();
		void Delete();
		void Undo();
		void Redo();
		void Reset();
		void Display();
		FormAction FormActionStatus{get;set;}
	}
}
