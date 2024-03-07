namespace DCI.HRMS.Model.Common
{
	/// <summary>
	/// Summary description for MessageAlert.
	/// </summary>
	public sealed class MessageAlert
	{
		//Result Alert

		public const string ITEM_ADD_SUCCESS = "�ѹ�֡���������º����";
		public const string ITEM_EDIT_SUCCESS = "��䢢��������º����";
		public const string ITEM_DELETE_SUCCESS = "ź���������º����";
		public const string ITEM_NOT_ADD = "�������ö�ѹ�֡�����Ź���� ���ͧ�ҡ ";
		public const string ITEM_NOT_EDIT = "�������ö��䢢����Ź���� ���ͧ�ҡ ";
		public const string ITEM_NOT_DELETE = "�������öź�����Ź���� ���ͧ�ҡ ";
		public const string ITEM_NOT_EXPORT = "�������ö���͡�������� ���ͧ�ҡ ";
		public const string ITEM_NOT_FOUND = "��辺�����ŷ���ҹ��ͧ��ä���";
		public const string ITEM_BLANK = "��سҡ�͡������������º����";
		public const string ITEM_SEARCH_BLANK = "��سҡ�͡�����ŷ���ҹ��ͧ��ä���";

		//Change Document Alert
		public const string DOCUMENT_NOT_EDIT = "�Ţ����͡��ù��١ Approve (or Cancel) ���� �������ö������ա";

		//Security Alert
		public const string ACCOUNT_NOT_FOUND = "���ͼ���� ���� ���ʼ�ҹ���١��ͧ";
		public const string ACCOUNT_NOT_ENABLE = "Account �ͧ��ҹ�١�ЧѺ�����ҹ ��سҵԴ��ͼ������к�.";
		public const string ACCOUNT_NOT_ALLOW = "��ҹ������Ѻ�Է���㹡�÷ӧҹ Module ���� Function ���";
		public const string ACCOUNT_PWD_EXPIRED = "���ʼ�ҹ�ͧ��ҹ����������� ��س�����¹���ʼ�ҹ����";

		//DB Alert
		public const string DATABASE_NOT_CONNECT = "�������ö�Դ��Ͱҹ��������㹢�й��";
		public const string DATABASE_OVERLOAD = "��й���ռ�����繨ӹǹ�ҡ��к� ������к��������ö�ӧҹ�� ��س����ѡ����";
	}
}