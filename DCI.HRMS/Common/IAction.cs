using System.Windows.Forms;
using DCI.HRMS.Model;
//using DCIBizPro.DTO.Document;

namespace DCI.HRMS.Common
{
	public enum ActionStatus
	{
		None,
		AddNew,
		Save,
		Delete,
		ReadOnly
	}

	public enum ActionType
	{
		Open,
		New,
		Save,
		Delete,
		Search,
		Undo,
		Redo,
		Refresh,
		Import,
		Export,
		MoveFirst,
		MovePrevious,
		MoveNext,
		MoveLast,
		Print,
		Preview
	}

	/// <summary>
	/// ติดตั้ง Interface สำหรับ Form Child
	/// </summary>
	public interface IFormChildAction
	{
		void Open();
		void New();
		void Save();
		void Delete();
		void NormalSearch();
		void AdvanceSearch();
		void Undo();
		void Redo();
		void Reload();
		void Clear();
		void Display();
		void EnableControl(ActionStatus status);
	}

	/// <summary>
	///Interface สำหรับ NavigateAction
	/// </summary>
	public interface INavigateAction
	{
		void moveFirst();
		void movePrevious();
		void moveNext();
		void moveLast();
	}

	public interface IReportAction
	{
		void import();
		void print();
		void pagePreview();
		void export();
	}

	public interface IFormParentAction
	{
		Form openFormChildren(BaseForm frm);
		void controlActionFlow(BaseForm form);
		void changeMdiChildStatus(BaseForm frm, ActionStatus status);
	}

	public interface IDocAction
	{
		void changeDocumentStatus(string documentNo, DocumentStatus status);
	}
}