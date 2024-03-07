using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using DCI.HRMS.Model;
using DCI.HRMS.Model.Organize;

namespace DCI.HRMS.Persistence
{
    public interface IDivisionDao
    {
        DivisionInfo Select(string divisionCode);
        ArrayList SelectAll();
        ArrayList SelectByType(string typeCode);
        ArrayList SelectByOwner(string divisionOwnerCode);
    }
}
