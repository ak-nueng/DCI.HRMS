using System;
using System.Collections.Generic;
using System.Text;

namespace DCI.HRMS.Persistence.Oracle
{
    class OraSubContractDaoFactory:SubContractDaoFactory
    {
        public override IDictionaryDao CreateDictionaryDao()
        {
            return new OraDictionaryDao(this.DaoManager);
        }

        public override IPositionDao CreatePositionDao()
        {
            return new OraPositionDao(this.DaoManager);
        }
        public override IDivisionDao CreateDivisionDao()
        {
            return new OraDivisionDao(this.DaoManager);
        }

        public override IEmployeeDao CreateEmployeeDao()
        {
            return new OraEmployeeDao(this.DaoManager);
        }

        public override IAttendanceDao CreateEmployeeLeaveDao()
        {
            return new OraAttendanceDao(this.DaoManager);
        }

        /*
        public override IDiseaseDao CreateDiseaseDao()
        {
            return new OraDiseaseDao(this.DaoManager);
        }

        public override IMedicineDao CreateMedicineDao()
        {
            return new OraMedicineDao(this.DaoManager);
        }

        public override IDoctorDao CreateDoctorDao()
        {
            return new OraDoctorDao(this.DaoManager);
        }

        public override IFirstAidRecordDao CreateFirstAidRecordDao()
        {
            return new OraFirstAidRecordDao(this.DaoManager);
        }
        public override IPatientRecordDao CreatePatientRecordDao()
        {
            return new OraPatientRecordDao(this.DaoManager);
        }
        public override IFirstAidReportDao CreateFirstAidReportDao()
        {
            return new OraFirstAidReportDao(this.DaoManager);
        }
        public override IOverTimeDao CreateOvertimeDao()
        {
            return null;
        }
        */
        public override IBusinessTripDao CreateBusinessTripDao()
        {
            return new OraBusinessTripDao(this.DaoManager);
        }
        public override IShiftDao CreateShiftDao()
        {
            return new OraShiftDao(this.DaoManager);
        }
        public override ITimeCardDao CreateTimeCardDAO()
        {
            return new OraTimeCardDao(this.DaoManager);
        }
        public override IOTDao CreateOtDao()
        {
            return new OraOtDao(this.DaoManager);
        }
        public override ILeaveRequestDao CreateLeaveReqDao()
        {
            return new OraLeaveReqDao(this.DaoManager);
        }
        public override IMedicalDao CreateMedicalDao()
        {
            return new OraMedicalDao(this.DaoManager);
        }
        public override IKeyGeneratorDao CreateKeyDao()
        {
            return new OraKeyGeneratorDao(this.DaoManager);
        }
        public override ISkillAllowanceDao CreateSkillAllowanceDao()
        {
            return new OraSkillAllowanceDao(this.DaoManager);
        }
    }
}
