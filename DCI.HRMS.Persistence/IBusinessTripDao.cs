using System;
using System.Collections.Generic;

using System.Text;
using System.Collections;
using DCI.HRMS.Model.Attendance;

namespace DCI.HRMS.Persistence
{
    public interface IBusinessTripDao
    {

        ArrayList GetBusinessTrip(string empCode, DateTime fDate, DateTime tDate);
        BusinesstripInfo GetBusinessTrip(string empCode, DateTime fDate);

        void SaveBusinessTrip(BusinesstripInfo rq);
        void UpdateBusinessTrip(BusinesstripInfo rq);
        void DeteteBusinessTrip(BusinesstripInfo rq);

    }
}
