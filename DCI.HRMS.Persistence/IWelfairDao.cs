using System;
using System.Collections.Generic;
using System.Text;
using DCI.HRMS.Model.Welfare;
using System.Collections;

namespace DCI.HRMS.Persistence
{
    public interface IWelfairDao
    {

        BusStopInfo GetBusStop(string busway, string stopCode);
        BusWayInfo GetBusWay(string busWay);
        ArrayList GetBusStop(string busway);
        ArrayList GetAllBusWay();


    }
}
