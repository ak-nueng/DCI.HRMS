using System;
using System.Security.Cryptography;

namespace DCIBizPro.Util.Cryptography
{
	/// <summary>
	/// การเข้ารหัสข้อมูล
	/// </summary>
	public sealed class Encrypt
	{
		/// <summary>
		/// การเข้ารหัสให้กับรหัสผ่าน
		/// </summary>
		/// <param name="password"></param>
		/// <returns></returns>
		public static string HashPassword(string password)
		{
			string hashPassword;
			SHA256Managed hashProvider = null;

			try
			{
				Byte[] passwordBytes;
				
				passwordBytes	= System.Text.Encoding.Unicode.GetBytes(password);
				hashProvider	= new SHA256Managed();
				hashProvider.Initialize();

				passwordBytes	= hashProvider.ComputeHash(passwordBytes);
				hashPassword	= Convert.ToBase64String(passwordBytes);
				
				
			}finally
			{
				if(hashProvider != null)
				{
					hashProvider.Clear();
					hashProvider = null;
				}
			}
			
			return hashPassword;
		}
	}
}
