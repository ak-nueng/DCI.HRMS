using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DCI.HRMS.Persistence
{
    public interface IPositionDao
    {
        ArrayList SelectAll();
    }
}
