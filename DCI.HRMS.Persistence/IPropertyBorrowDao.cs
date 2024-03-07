using System;
using System.Collections.Generic;

using System.Text;
using DCI.HRMS.Model.Welfare;
using System.Collections;
using DCI.HRMS.Model.Personal;
using System.Data;

namespace DCI.HRMS.Persistence
{
   public interface IPropertyBorrowDao
    {
       PropertyBorrowInfo GetByID(string _br_id);
       ArrayList GetByEmpCode(string _empCode);
       ArrayList GetData(string _empCode, string _type, string _returnSts);
       void Savedata(PropertyBorrowInfo _brInfo);
       void UpdateData(PropertyBorrowInfo _brInfo);
       void DeleteData(string _brId);

       ArrayList GetProperty();
       void SaveProperty(PropertyInfo prt);
       void UpdateProperty(PropertyInfo prt);

       ArrayList GetLockerMaster(string _lockerId, string _keyCode, string _empCode);
       DataSet GetLockerMasterDataSet(string _lockerId, string _keyCode, string _empCode);
       LockerInfo GetLockerMaster(string _lockerId);
       ArrayList GetLockerborrowData(string _lockerId);
       void SaveLockerMaster(LockerInfo _lockMstr);
       void UpdateLockerMaster(LockerInfo _lockMstr);
       void DeleteLockerMaster(string _lockerId);



    }
}
