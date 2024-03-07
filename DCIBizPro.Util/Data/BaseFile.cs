using System;
using System.Data;
using System.IO;
using System.Text;
using DCIBizPro.Util.Text;

namespace DCIBizPro.Util.Data
{
	/// <summary>
	/// Summary description for File.
	/// </summary>
	public abstract class BaseFile
	{
		private string name;
		private string path;
		private string owner;
		private ProgressMeter meter;
		private bool saveAs = true;
		private DateTime dateModified = DateTime.Now;

		public BaseFile()
		{
			
		}

		public string Name
		{
			get{ return this.name; }
			set{ this.name = value; }
		}
		public string Path
		{
			get{ return this.path; }
			set{ this.path = value; }
		}
		public string Owner
		{
			get{ return this.owner; }
			set{ this.owner = value; }
		}
		public bool SaveAs
		{
			get{ return this.saveAs; }
			set{ this.saveAs = value; }
		}
		public DateTime ModifiedDate
		{
			get{ return this.dateModified; }
			set{ this.dateModified = value; }
		}
		public ProgressMeter Meter
		{
			get{ return this.meter; }
			set{ this.meter = value; }
		}

		virtual protected string Extension
		{
			get{ return ".txt"; }
		}

        public void Export(DataRowCollection rows)
        {
            if (rows.Count > 0)
            {
                Export(rows[0].Table);
            }
        }
		virtual public void Export(DataTable data)
		{
			try
			{
				StringBuilder builder = null;
				TextWriter writer = new StreamWriter(string.Format("{0}\\{1}.txt",this.Path,this.Name)
					,(this.SaveAs == false) , Encoding.Unicode);

				try
				{
					this.Meter.Value = 0;
					this.Meter.Maximum = data.Rows.Count;
				}catch{}

                if (SaveAs)
                {
                    CreateHeader(data, builder);
                    writer.WriteLine(builder.ToString());
                }

                writer.WriteLine(builder.ToString());

				foreach(DataRow row in data.Rows)
				{
					builder = new StringBuilder();

					foreach(DataColumn col in data.Columns)
					{
						builder.Append(row[col.ColumnName] + ",");
					}
					writer.WriteLine(builder.ToString());

					this.Meter.Value = this.Meter.Value + 1;
				}

				writer.Close();
			}catch(Exception ex)
			{
				throw ex;
			}
		}

        protected void CreateHeader(DataTable data, StringBuilder textBuilder)
        {
            int colHdrCount = 1;
            foreach (DataColumn col in data.Columns)
            {
                textBuilder.Append(StringHelper.ConvertToThaiLang(Convert.ToString(col.ColumnName)));
                if (colHdrCount < data.Columns.Count)
                {
                    textBuilder.Append(",");
                }
                colHdrCount++;
            }
        }

		public override string ToString()
		{
			return string.Format("{0}{1}",this.Name,this.Extension);
		}
        public string ToString(bool includePath)
        {
            if(includePath)
            {
                return string.Format("{0}\\{1}{2}",this.Path,this.Name,this.Extension);
            }else{
                return this.ToString();
            }
        }
        public void Delete()
        {
            Delete(this.ToString(true));
        }
        public void Delete(string path)
        {
            File.Delete(path);
        }
	}
}
