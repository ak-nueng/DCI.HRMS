using System;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

using CrystalDecisions.Windows.Forms;

using DCI.HRMS.Common;
using DCI.HRMS.Base;
using DCI.Security.Model;

namespace DCI.HRMS.Util
{
	/// <summary>
	/// Summary description for FormUtil.
	/// </summary>
	public class FormUtil
	{
		public static BaseForm Convert(string assemblyName, string typeName)
		{
			BaseForm frm = null;
			try
			{
				//frm = (BaseForm) Activator.CreateInstance(Assembly.LoadWithPartialName(assemblyName).GetType(typeName));
			}
			catch
			{
			}
			return frm;
		}
        public static object CreateForm(string assemblyName, string typeName)
        {
            object frm = null;
            try
            {
                System.Reflection.AssemblyName asmName = new AssemblyName(assemblyName);
                System.Reflection.Assembly asm = Assembly.Load(asmName);
                frm = asm.CreateInstance(typeName);
                //frm = (object)Activator.CreateInstance(typeName, typeName);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return frm;
        }
		public static void Clear(Form frm)
		{
			Clear(frm.Controls);
		}
		public static void Clear(Control.ControlCollection controls)
		{
			foreach(Control c in controls)
			{
				if(c is TextBox)
				{
					c.Text = string.Empty;	
				}
                if (c is CheckBox)
                {
                    CheckBox ch = (CheckBox)c;
                    ch.Checked = false;

                }
			}
		}

        public static void ClearNumZero(Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                if (c is TextBox)
                {
                    TextBox t = (TextBox)c;
                    t.Text = "0";
                }
            }
        }
        public static void SetNumZero(Control control)
        {
            if (control is TextBox)
            {
                TextBox t = (TextBox)control;
                if (t.Text == string.Empty)
                    t.Text = "0";
            }
        }
        public static void SetNumZero(Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                if (c is TextBox)
                {
                    TextBox t = (TextBox)c;
                    if(t.Text == string.Empty)
                        t.Text = "0";
                }
            }
        }
        public static void Enter(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        public static void InputNumericOnly(KeyPressEventArgs e)
        {
            bool result = false;
            int ascii = (int)e.KeyChar;

            //System.Diagnostics.Debug.WriteLine(ascii);

            if (ascii < 48 || ascii > 57)
            {
                if (ascii == 8 || ascii == 13 || ascii == 45 || ascii == 46){
                    result = false;
                }else{
                    result = true;
                }
            }
            e.Handled = result;
        }

        public static void HilightTextBox(object sender)
        {
            try
            {
                TextBox t = (TextBox)sender;
                t.SelectAll();
            }
            catch
            {
            }
        }

        public static string ApplicationDirectory
        {
            get
            {
                string appDir = string.Empty;

                Assembly assembly1 = Assembly.GetExecutingAssembly();
                string text1 = Path.GetDirectoryName(assembly1.CodeBase);
                
                DirectoryInfo dir = new DirectoryInfo(text1.Replace("file:\\", ""));

                while (true)
                {
                    DirectoryInfo tmpDir = new DirectoryInfo(dir.FullName);
                    if (dir.Name.ToUpper() == "BIN")
                    {
                        appDir = dir.Parent.FullName;
                        break;
                    }
                    else
                    {
                        dir = tmpDir.Parent;
                    }
                }

                return appDir;
            }
        }

        public static void SetReportPermission(PermissionInfo permission, CrystalReportViewer crystalRptViewer)
        {
            if (permission != null)
            {
                crystalRptViewer.ShowExportButton = permission.AllowExportData;
                crystalRptViewer.ShowPrintButton = permission.AllowPrintReport;
            }
        }
	}
}