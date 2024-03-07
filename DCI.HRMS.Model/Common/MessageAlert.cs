namespace DCI.HRMS.Model.Common
{
	/// <summary>
	/// Summary description for MessageAlert.
	/// </summary>
	public sealed class MessageAlert
	{
		//Result Alert

		public const string ITEM_ADD_SUCCESS = "บันทึกข้อมูลเรียบร้อย";
		public const string ITEM_EDIT_SUCCESS = "แก้ไขข้อมูลเรียบร้อย";
		public const string ITEM_DELETE_SUCCESS = "ลบข้อมูลเรียบร้อย";
		public const string ITEM_NOT_ADD = "ไม่สามารถบันทึกข้อมูลนี้ได้ เนื่องจาก ";
		public const string ITEM_NOT_EDIT = "ไม่สามารถแก้ไขข้อมูลนี้ได้ เนื่องจาก ";
		public const string ITEM_NOT_DELETE = "ไม่สามารถลบข้อมูลนี้ได้ เนื่องจาก ";
		public const string ITEM_NOT_EXPORT = "ไม่สามารถส่งออกข้อมูลได้ เนื่องจาก ";
		public const string ITEM_NOT_FOUND = "ไม่พบข้อมูลที่ท่านต้องการค้นหา";
		public const string ITEM_BLANK = "กรุณากรอกข้อมูลให้เรียบร้อย";
		public const string ITEM_SEARCH_BLANK = "กรุณากรอกข้อมูลที่ท่านต้องการค้นหา";

		//Change Document Alert
		public const string DOCUMENT_NOT_EDIT = "เลขที่เอกสารนี้ถูก Approve (or Cancel) แล้ว ไม่สามารถแก้ไขได้อีก";

		//Security Alert
		public const string ACCOUNT_NOT_FOUND = "ชื่อผู้ใช้ หรือ รหัสผ่านไม่ถูกต้อง";
		public const string ACCOUNT_NOT_ENABLE = "Account ของท่านถูกระงับการใช้งาน กรุณาติดต่อผู้ดูแลระบบ.";
		public const string ACCOUNT_NOT_ALLOW = "ท่านไม่ได้รับสิทธิ์ในการทำงาน Module หรือ Function นี้";
		public const string ACCOUNT_PWD_EXPIRED = "รหัสผ่านของท่านหมดอายุแล้ว กรุณาเปลี่ยนรหัสผ่านใหม่";

		//DB Alert
		public const string DATABASE_NOT_CONNECT = "ไม่สามารถติดต่อฐานข้อมูลได้ในขณะนี้";
		public const string DATABASE_OVERLOAD = "ขณะนี้มีผู้ใช้เป็นจำนวนมากในระบบ ทำให้ระบบไม่สามารถทำงานได้ กรุณารอสักครู่";
	}
}