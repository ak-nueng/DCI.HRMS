using System;
using System.Data;
using System.Reflection;

using DataTable = System.Data.DataTable;
using Excel;
using System.Data.OleDb;

namespace DCIBizPro.Util.Data
{
	/// <summary>
	/// Summary description for ExcelFile.
	/// </summary>
	public class ExcelFile : BaseFile
	{
		public ExcelFile()
		{
			
		}

		protected override string Extension
		{
			get
			{
				return ".xls";
			}
		}
		public override void Export(DataTable data)
		{
			int columnIndex = 0;
			int rowIndex = 0;

			try
			{
				Workbook workbook = new Workbook();
				Worksheet worksheet = (Worksheet) workbook.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);

				//Generate Column
				foreach (DataColumn col in data.Columns)
				{
					columnIndex++;
					worksheet.Cells[1, columnIndex] = col.ColumnName;
				}

				foreach (DataRow row in data.Rows)
				{
					rowIndex++;
					columnIndex = 0;
					foreach (DataColumn col in data.Columns)
					{
						columnIndex++;
						worksheet.Cells[rowIndex + 1, columnIndex] = row[col.ColumnName];
					}
				}

				if (this.SaveAs)
				{
					workbook.SaveAs(string.Format("{0}\\{1}{2}",this.Path,this.Name,this.Extension)
						, XlFileFormat.xlExcel7, Missing.Value
						, Missing.Value, Missing.Value
						, Missing.Value, XlSaveAsAccessMode.xlExclusive
						, Missing.Value, Missing.Value
						, Missing.Value, Missing.Value);
				}
				else
				{
					workbook.Save();
				}

			}
			catch (Exception ex)
			{
				throw ex;
			}

        }
        public DataSet ConvertFile(string filePath ,string sheetName)
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=Excel 8.0;";
            try
            {
                OleDbConnection connection = new OleDbConnection(connectionString);
                connection.Open();
                //this next line assumes that the file is in default Excel format with Sheet1 as the first sheet name, adjust accordingly
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM ["+ sheetName +"$]", connection);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                adapter.Fill(ds);//now you have your dataset ds now filled with the data and ready for manipulation

                //the next 2 lines do exactly the same thing, just shows two ways to get the same data
                dt = ds.Tables[0];
                adapter.Fill(dt);//overwrites the previous declaration with the same information
                //now you have your datatable filled and ready for manipulation
                connection.Close();
                return ds;
       

            }
            catch (Exception ex)
            { throw (ex); }
        }

	}
}
