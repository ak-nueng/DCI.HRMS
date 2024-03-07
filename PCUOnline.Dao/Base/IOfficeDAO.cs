using System;
using System.Collections.Generic;
using System.Text;

namespace PCUOnline.Dao.Base
{
    public interface IOfficeDAO
    {
        void Insert();
        void Update();
        void Delete();
        void Select();
        void SelectByHeadOffice();
    }
}
