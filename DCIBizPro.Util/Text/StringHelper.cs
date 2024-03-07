using System.Collections;
using System.Text;

namespace DCIBizPro.Util.Text
{
	/// <summary>
	/// ��㹡�èѴ��â����ŷ���� String.
	/// </summary>
	public class StringHelper
	{
		/// <summary>
		/// ��Ǩ�ͺ��� Empty �ͧ��ͤ���
		/// </summary>
		/// <param name="text">��ͤ���</param>
		/// <returns>true/false</returns>
		public static bool CheckStringEmpty(string text)
		{
			if(text == null)
			{
				return true;
			}else if(text.Equals(string.Empty))
			{
				return true;
			}else if(text.Equals(""))
			{
				return true;
			}else
			{
				return false;
			}
		}

		/// <summary>
		/// ��Ǩ�ͺ��� Empty �ͧ��ͤ�������� Array
		/// </summary>
		/// <param name="text">��ͤ���</param>
		/// <returns>true/false</returns>
		public static bool CheckStringEmpty(string[] text)
		{
			bool result = false;
			int len = text.Length;

			for(int i = 0 ; i < len ; i++)
			{
				result = StringHelper.CheckStringEmpty(text[i]);
				if(result){break;}
			}

			return result;
		}

		/// <summary>
		/// ��Ǩ�ͺ��� Empty �ͧ��ͤ������������� Collection
		/// </summary>
		/// <param name="text">��ͤ���</param>
		/// <returns>true/false</returns>
		public static bool CheckStringEmpty(ICollection text)
		{
			bool result = false;
			
			foreach(string t in text)
			{
				result = StringHelper.CheckStringEmpty(t);
				if(result){break;}
			}
			return result;
		}

		public static bool CheckStringLength(ICollection text , int maxLength)
		{
			if(StringHelper.CheckStringEmpty(text))
			{
				return false;
			}
			else
			{
				bool result = true;
			
				foreach(string t in text)
				{
					if(t.Length > maxLength){result = false;break;}
				}
				return result;
			}
		}
		public static bool CheckStringLength(string[] text , int maxLength)
		{
			if(StringHelper.CheckStringEmpty(text))
			{
				return false;
			}
			else
			{
				bool result = true;

				for(int i = 0 ; i < text.Length ; i++)
				{
					if(text[i].Length > maxLength){result = false;break;}
				}

				return result;
			}
		}
		public static bool CheckStringLength(string text , int maxLength)
		{
			if(StringHelper.CheckStringEmpty(text))
			{
				return false;
			}
			else
			{
				if(text.Length <= maxLength)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public static bool ConvertYNToBool(string arg)
		{
			if(arg.Equals("Y"))
			{
				return true;
			}else
			{
				return false;
			}
		}
		public static string ConvertBoolToYN(bool arg)
		{
			if(arg)
			{
				return "Y";
			}else
			{
				return "N";
			}
		}
		public static string ConvertBoolToYN(bool arg , string replaceN)
		{
			if(arg)
			{
				return "Y";
			}
			else
			{
				return replaceN;
			}
		}

		public static string NeedNull(string arg)
		{
			if(arg.Equals(""))
			{
				arg = null;
			}
			return arg;
		}

		public static string ConvertToThaiLang(string data)
		{
			StringBuilder output = new StringBuilder();

			if(data != null && data.Length > 0)
			{
				foreach(char c in data)
				{
					int ascii = (int)c;

					if(ascii > 160)
						ascii += 3424;

					output.Append((char)ascii);
				}
			}

			return output.ToString();
		}

		public static string ConvertStringForOracle(string data)
		{
			StringBuilder output = new StringBuilder();
			if(data != null && data.Length > 0)
			{
				foreach(char c in data)
				{
					int ascii = (int)c;

					if(ascii >= 160)
						ascii -= 3424;

					output.Append((char)ascii);
				}
			}
			return output.ToString();
		}
	}
}
