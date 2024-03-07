using System.Diagnostics;
using System.Windows.Forms;
using System;

namespace DCI.HRMS.Util
{
	/// <summary>
	/// Summary description for KeyPressManager.
	/// </summary>
	public class KeyPressManager
	{
		public static void Enter(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				SendKeys.Send("{TAB}");
			}
		}

		public static void EnterNumericOnly(KeyPressEventArgs e)
		{
			bool result = false;
			int ascii = (int) e.KeyChar;

		//	System.Diagnostics.Debug.WriteLine(ascii);

			if (ascii < 48 || ascii > 57)
			{
				if (ascii == 8 || ascii == 13 || ascii == 45 || ascii == 46)
				{
					result = false;
				}
				else
				{
					result = true;
				}
			}

			Debug.WriteLine(ascii);
			e.Handled = result;
		}

		public static void SelectAllTextBox(object sender)
		{
			try
			{
				TextBox t = (TextBox) sender;
				t.SelectAll();
			}
			catch
			{
			}
		}
        public static bool ConvertTextTime(object sender)
        {
            TextBox tt = (TextBox)sender;
            if (tt.Text.Trim() != string.Empty)
            {

                try
                {
                    if (!tt.Text.Contains(":"))
                    {
                        tt.Text = tt.Text.Insert(tt.Text.Length - 2, ":");
                    }
                    DateTime dt = DateTime.Parse(tt.Text);
                    tt.Text = dt.ToString("HH:mm");
                    return true;
                }
                catch
                {
                    MessageBox.Show("เวลาไม่ถูกต้อง กรุณาป้อนใหม่", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tt.Clear();
                    tt.Focus();
                    return false;

                }

            }
            return true;
        }
        public static bool ConvertTextdate(object sender)
        {


            TextBox tt =(TextBox)sender;
            if (tt.Text.Trim()!= string.Empty)
            {
                try
                {
                    DateTime dt = DateTime.Parse(tt.Text);
                    tt.Text = dt.ToString("dd/MM/yyyy");
                    return true;
                }
                catch 
                {

                    MessageBox.Show("วันที่ไม่ถูกต้อง กรุณาป้อนใหม่", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tt.Clear();
                    tt.Focus();
                    return false;
                }
                
            }
            return true;

        }
	}
}