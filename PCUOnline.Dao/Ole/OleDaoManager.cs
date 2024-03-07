using System;
using System.Text;
using System.Data.OleDb;

namespace PCUOnline.Dao.Ole
{
    public class OleDaoManager : DaoManager
    {
        public override void StartTransaction(bool readOnly)
        {
            OleDbConnection c = new OleDbConnection(this.Property.ConnectionString);
            this.AddTransaction(new DaoTransaction(c, (readOnly == false)));
        }
    }
}
