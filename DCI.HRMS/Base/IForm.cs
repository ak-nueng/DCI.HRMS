using System;
using DCI.Security.Model;

namespace DCI.HRMS.Base
{
    public enum FormActionType
    {
        None,
        AddNew,
        Save,
        SaveAs,
        Delete,
        Print,
        Refresh,
        Close,
        Search
    }

    public interface IForm
    {
        string GUID { get;}
        object Information { get;set;}

        void AddNew();
        void Save();
        void Delete();
        
        void Search();
        void Export();
        void Print();

        void Open();
        void Clear();
        void RefreshData();
        void Exit();        
    }

	public interface IFormChild : IForm
	{
        
	}

    public interface IFormParent : IForm
    {
    }

    public interface IFormPermission
    {
        PermissionInfo Permission { set;}
    }
}
