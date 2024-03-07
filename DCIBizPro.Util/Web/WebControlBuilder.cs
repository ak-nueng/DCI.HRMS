using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace DCIBizPro.Util.Web
{
	/// <summary>
	/// Summary description for WebControlBuilder.
	/// </summary>
	public class WebControlBuilder
	{
		public static CheckBox createCheckBox(string id , string data)
		{
			CheckBox chk = new CheckBox();
			chk.ID = id;
			chk.Text = data;
			chk.Checked = false;
			chk.Visible = true;
			return chk;
		}
		
		public static TextBox createTextBox(string id , string data)
		{
			TextBox txt = new TextBox();
			txt.ID		= id;
			txt.Text	= data;
			txt.Width	= 50;

			return txt;
		}

		public static Button createButton(string id , string name)
		{
			Button btn	= new Button();
			btn.ID		= id;
			btn.Text	= name;

			return btn;
		}

		public static Button createButton(string id , string name , string commandName , string commandArg)
		{
			Button btn	= new Button();
			btn.ID		= id;
			btn.Text	= name;
			btn.CommandName		= commandName;
			btn.CommandArgument = commandArg;

			return btn;
		}

		public static DropDownList createDropDownList(string id , ArrayList data , string dataTextField , string dataValueField , string selectedValue)
		{
			DropDownList cbo = new DropDownList();
			
			cbo.ID	= id;
			
			if(data != null)
			{
				cbo.DataSource = data;
				cbo.DataTextField  = dataTextField;
				cbo.DataValueField = dataValueField;
				cbo.DataBind();

			}

			return cbo;
		}
	}
}
