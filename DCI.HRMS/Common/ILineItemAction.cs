namespace DCI.HRMS.Common
{
	/// <summary>
	/// Summary description for ILineItemAction.
	/// </summary>
	public interface ILineItemAction
	{
		void ItemPrompt(int total);
		void InsertItem();
		void DeleteItem();
		void DeleteItem(int index);
		void DeleteAllItem();
	}
}