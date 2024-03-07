using System;
using System.Data;
using System.IO;
using System.Text;
using DCIBizPro.Util.Text;

namespace DCIBizPro.Util.Data
{
	/// <summary>
	/// Summary description for CSVFile.
	/// </summary>
	public class CSVFile : BaseFile
	{
		public CSVFile()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		protected override string Extension
		{
			get
			{
				return ".csv";
			}
		}

		public override void Export(DataTable data)
		{
			try
			{
				StringBuilder textBuilder = new StringBuilder();
				TextWriter writer = new StreamWriter(string.Format("{0}\\{1}{2}",this.Path,this.Name,this.Extension)
					,(this.SaveAs == false) , Encoding.Unicode);

                int colCount = 1;
                if (SaveAs)
                {
                    CreateHeader(data, textBuilder);
                    writer.WriteLine(textBuilder.ToString());
                }

				foreach(DataRow row in data.Rows)
				{
					colCount = 1;
					textBuilder = new StringBuilder();

					foreach(DataColumn col in data.Columns)
					{
						textBuilder.Append(
							StringHelper.ConvertToThaiLang(
							Convert.ToString(row[col.ColumnName])));

						if(colCount < data.Columns.Count)
						{
							textBuilder.Append(",");
						}
						colCount++;
					}
					writer.WriteLine(textBuilder.ToString());
				}
				writer.Close();
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
	}
}
