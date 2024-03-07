using System;
using System.Collections.Generic;
using System.Text;
using PCUOnline.Dao;
using System.Reflection;


namespace DCI.HRMS.Persistence
{
    
        public abstract class TraineeDaoFactory : DaoFactoryBase
        {
            internal TraineeDaoFactory() { }

            public static TraineeDaoFactory Instance()
            {
                DaoManager tmpDaoManager = DaoConfig.GetDaoManager("TRAINEE");
                DaoProperty prop = tmpDaoManager.Property;

                TraineeDaoFactory factory = (TraineeDaoFactory)Assembly.Load(prop.DaoFactoryAssembly).CreateInstance(prop.DaoFactoryClass);
                factory.DaoManager = tmpDaoManager;

                return factory;
            }
            /**
        

            protected DaoManager DaoManager
            {
                get { return daoManager; }
                set { daoManager = value; }
            }

            public void StartTransaction()
            {
                StartTransaction(false);
            }
            public void StartTransaction(bool readOnly)
            {
                daoManager.StartTransaction(readOnly);
            }
            public void CommitTransaction()
            {
                daoManager.CommitTrasnaction();
            }
            public void EndTransaction()
            {
                daoManager.EndTransaction();
            }**/

            public abstract IDictionaryDao CreateDictionaryDao();
            public abstract IPositionDao CreatePositionDao();
            public abstract IDivisionDao CreateDivisionDao();
            public abstract IEmployeeDao CreateEmployeeDao();
            public abstract IAttendanceDao CreateEmployeeLeaveDao();

            /*public abstract IDiseaseDao CreateDiseaseDao();
            public abstract IMedicineDao CreateMedicineDao();
            public abstract IDoctorDao CreateDoctorDao();
            public abstract IFirstAidRecordDao CreateFirstAidRecordDao();
            public abstract IPatientRecordDao CreatePatientRecordDao();

            public abstract IOverTimeDao CreateOvertimeDao();*/
            public abstract IShiftDao CreateShiftDao();
            public abstract ITimeCardDao CreateTimeCardDAO();
            public abstract IOTDao CreateOtDao();
            public abstract ILeaveRequestDao CreateLeaveReqDao();
            public abstract IMedicalDao CreateMedicalDao();
            public abstract IKeyGeneratorDao CreateKeyDao();
            /*
            # region Report
            public abstract IFirstAidReportDao CreateFirstAidReportDao();
            # endregion
            */
        }
    
}