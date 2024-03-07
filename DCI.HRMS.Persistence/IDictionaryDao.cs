using System;
using System.Collections.Generic;
using System.Text;
using DCI.HRMS.Model;
using System.Collections;

namespace DCI.HRMS.Persistence
{
    public interface IDictionaryDao
    {
        BasicInfo Select(string type, string code);
        ArrayList Find(string type, string code);
        ArrayList SelectAll(string type);
        ArrayList SelectAllType();

        void SaveDictData(BasicInfo _data);
        void UpdateDictData(BasicInfo _data);
        void DeleteDictData(string type, string code);
    }
}
