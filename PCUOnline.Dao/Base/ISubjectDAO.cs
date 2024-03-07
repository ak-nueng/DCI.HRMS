using System;
using System.Collections.Generic;
using System.Text;

namespace PCUOnline.Dao.Base
{
    public interface ISubjectDAO
    {
        void Insert();
        void Update();
        void Delete();
        void Select();
        void SelectByMajorSubject();
    }
}
