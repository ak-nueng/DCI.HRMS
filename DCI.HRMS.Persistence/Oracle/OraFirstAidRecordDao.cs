using System;
using System.Collections.Generic;
using System.Text;
using DCI.HRD.Model;
using System.Collections;
using PCUOnline.Dao;
using System.Data;
using PCUOnline.Dao.Ora;
using Oracle.DataAccess.Client;

namespace DCI.HRD.Persistence.Oracle
{
    public class OraFirstAidRecordDao : DaoBase , IFirstAidRecordDao
    {
        private const string SP_STORE = "pkg_hr_eph.sp_store";
        private const string SP_DELETE = "pkg_hr_eph.sp_del";
        private const string SP_SELECT = "pkg_hr_eph.sp_select";
        private const string SP_SELECT_BY_CRITERIA = "pkg_hr_eph.sp_select_by_crtria";

        private const string SP_STORE_DIS_ITEM = "pkg_hr_eph.sp_store_afft_line";
        private const string SP_DELETE_DIS_ITEM = "pkg_hr_eph.sp_del_afft_line";
        private const string SP_SELECT_DIS_ITEMS = "pkg_hr_eph.sp_select_afft_line";

        private const string SP_STORE_MED_ITEM = "pkg_hr_eph.sp_store_med_line";
        private const string SP_DELETE_MED_ITEM = "pkg_hr_eph.sp_del_med_line";
        private const string SP_SELECT_MED_ITEMS = "pkg_hr_eph.sp_select_med_line";

        private const string PARAM_ACTION = "p_action";

        private const string PARAM_NBR = "p_recno";
        private const string PARAM_DATE = "p_date";
        private const string PARAM_TYPE = "p_type";
        private const string PARAM_INJURED_TYPE = "p_inj_type";
        private const string PARAM_EMP = "p_emp";
        private const string PARAM_DOCTOR = "p_doctor";
        private const string PARAM_RECORD_BY = "p_by";
        private const string PARAM_REMARK = "p_remark";

        private const string PARAM_DIS_SEQ = "p_seq";
        private const string PARAM_DIS = "p_afft";
        private const string PARAM_DIS_SYMP = "p_symptom";

        private const string PARAM_MED = "p_med";
        private const string PARAM_MED_QTY = "p_qty";
        private const string PARAM_MED_SEQ = "p_seq";

        private OraEmployeeDao employeeDao;
        private OraMedicineDao medicineDao;
        private OraDiseaseDao diseaseDao;
        private OraDoctorDao doctorDao;
        private OraDivisionDao divisionDao;

        public OraFirstAidRecordDao(DaoManager daoManager) : base(daoManager)
        {
            employeeDao = new OraEmployeeDao(daoManager);
            medicineDao = new OraMedicineDao(daoManager);
            diseaseDao = new OraDiseaseDao(daoManager);
            doctorDao = new OraDoctorDao(daoManager);
            divisionDao = new OraDivisionDao(daoManager);
        }

        public override object QueryForObject(DataRow row, Type t)
        {
            if (t == typeof(FirstAidRecordInfo))
            {
                FirstAidRecordInfo patientRecord = new FirstAidRecordInfo();
                try
                {
                    patientRecord.RecordNo = OraHelper.DecodeLanguage((string)this.Parse(row, "eph_nbr"));
                }
                catch { }
                try
                {
                    patientRecord.Patient = (EmployeeInfo)employeeDao.QueryForObject(row, typeof(EmployeeInfo));
                    if (patientRecord.Patient.Code == null 
                        || patientRecord.Patient.Code == string.Empty)
                    {
                        patientRecord.Patient.Code = Convert.ToString(this.Parse(row, "eph_emp_cd"));
                    }
                }
                catch { }
                try
                {
                    patientRecord.TreatmentBy = (PersonInfo)doctorDao.QueryForObject(row, typeof(PersonInfo));
                }
                catch { }
                try
                {
                    patientRecord.Date = Convert.ToDateTime(this.Parse(row, "eph_date"));
                }
                catch { }
                try
                {
                    patientRecord.RecordBy = OraHelper.DecodeLanguage((string)this.Parse(row, "eph_cre_by"));
                }
                catch { }
                try
                {
                    patientRecord.Type = OraHelper.DecodeLanguage((string)this.Parse(row, "eph_type"));
                }
                catch { }
                try
                {
                    patientRecord.InjuredType = OraHelper.DecodeLanguage((string)this.Parse(row, "eph_inj_type"));
                }
                catch { }
                try
                {
                    patientRecord.Note = OraHelper.DecodeLanguage((string)this.Parse(row, "eph_remark"));
                }
                catch { }
                try
                {
                    patientRecord.Patient.Division = (DivisionInfo)divisionDao.QueryForObject(row, DivisionType.Group);
                    patientRecord.Patient.Division.DivisionOwner = (DivisionInfo)divisionDao.QueryForObject(row, DivisionType.Section);
                    patientRecord.Patient.Division.DivisionOwner.DivisionOwner = (DivisionInfo)divisionDao.QueryForObject(row, DivisionType.Department);
                }
                catch { }
                try
                {
                    patientRecord.Patient.WorkGroupLine = OraHelper.DecodeLanguage((string)this.Parse(row, "eph_grp_line"));
                }
                catch { }
                return patientRecord;
            }
            else if (t == typeof(DiseaseInfo))
            {
                DiseaseInfo item = (DiseaseInfo)diseaseDao.QueryForObject(row, typeof(DiseaseInfo));

                try
                {
                    item.Code = OraHelper.DecodeLanguage((string)this.Parse(row, "ept_afft_cd"));
                }
                catch { }
                try
                {
                    item.Description = OraHelper.DecodeLanguage((string)this.Parse(row, "ept_symptom"));
                }
                catch { }
                return item;
            }
            else if (t == typeof(MedicineInfo))
            {
                MedicineInfo item = (MedicineInfo)medicineDao.QueryForObject(row, typeof(MedicineInfo));
                try
                {
                    item.Code = OraHelper.DecodeLanguage((string)this.Parse(row, "ept_med_cd"));
                }
                catch { }
                try
                {
                    item.Quantity = Convert.ToInt32(this.Parse(row, "ept_med_qty"));
                }
                catch { }
                return item;
            }
            return null;
        }

        public override void AddParameters(IDbCommand cmd, object obj)
        {
            OracleCommand c = (OracleCommand)cmd;
            if (obj is FirstAidRecordInfo)
            {
                FirstAidRecordInfo item = (FirstAidRecordInfo)obj;

                c.Parameters.Add(PARAM_NBR, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.RecordNo);
                c.Parameters.Add(PARAM_EMP, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.Patient.Code);
                c.Parameters.Add(PARAM_DATE, OracleDbType.Date).Value = item.Date;
                c.Parameters.Add(PARAM_TYPE, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.Type);
                c.Parameters.Add(PARAM_INJURED_TYPE, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.InjuredType);
                c.Parameters.Add(PARAM_DOCTOR, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.TreatmentBy.Code);
                c.Parameters.Add(PARAM_REMARK, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.Note);
                c.Parameters.Add(PARAM_RECORD_BY, OracleDbType.Varchar2).Value = OraHelper.EncodeLanguage(item.RecordBy);
            }
        }

        #region IPatientRecordDao Members

        private void AddParameters(string recordNo, DiseaseInfo disease, OracleCommand cmd)
        {
            cmd.Parameters.Add(PARAM_NBR, OracleDbType.Varchar2).Value = recordNo;
            cmd.Parameters.Add(PARAM_DIS, OracleDbType.Varchar2).Value = disease.Code;
            cmd.Parameters.Add(PARAM_DIS_SYMP, OracleDbType.Varchar2).Value = disease.Description;
            cmd.Parameters.Add(PARAM_DIS_SEQ, OracleDbType.Int32).Value = 0;
        }

        private void AddParameters(string recordNo, string diseaseCode, MedicineInfo medicine, OracleCommand cmd)
        {
            cmd.Parameters.Add(PARAM_NBR, OracleDbType.Varchar2).Value = recordNo;
            cmd.Parameters.Add(PARAM_DIS, OracleDbType.Varchar2).Value = diseaseCode;
            cmd.Parameters.Add(PARAM_MED, OracleDbType.Varchar2).Value = medicine.Code;
            cmd.Parameters.Add(PARAM_MED_QTY, OracleDbType.Int32).Value = medicine.Quantity;
            cmd.Parameters.Add(PARAM_MED_SEQ, OracleDbType.Int32).Value = 0;
        }

        public void Insert(FirstAidRecordInfo patientRecord)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE, CommandType.StoredProcedure);

            this.AddParameters(cmd, patientRecord);
            cmd.Parameters.Add(PARAM_ACTION, OracleDbType.Varchar2).Value = "ADD";
            
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void Update(FirstAidRecordInfo patientRecord)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE, CommandType.StoredProcedure);

            this.AddParameters(cmd, patientRecord);
            cmd.Parameters.Add(PARAM_ACTION, OracleDbType.Varchar2).Value = "MODIFY";

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void Delete(string recordNo)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_DELETE, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_NBR, OracleDbType.Varchar2).Value = recordNo;
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void InsertDiseaseItem(string recordNo, DiseaseInfo disease)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_DIS_ITEM, CommandType.StoredProcedure);

            AddParameters(recordNo, disease, cmd);
            cmd.Parameters.Add(PARAM_ACTION, OracleDbType.Varchar2).Value = "ADD";

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void UpdateDiseaseItem(string recordNo, DiseaseInfo disease)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_DIS_ITEM, CommandType.StoredProcedure);

            AddParameters(recordNo, disease, cmd);
            cmd.Parameters.Add(PARAM_ACTION, OracleDbType.Varchar2).Value = "MODIFY";

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void DeleteDiseaseItem(string recordNo, string diseaseCode)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_DELETE_DIS_ITEM, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_NBR, OracleDbType.Varchar2).Value = recordNo;
            cmd.Parameters.Add(PARAM_DIS, OracleDbType.Varchar2).Value = diseaseCode;

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void InsertMedicineItem(string recordNo, string diseaseCode, MedicineInfo medicine)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_MED_ITEM, CommandType.StoredProcedure);

            AddParameters(recordNo, diseaseCode, medicine, cmd);
            cmd.Parameters.Add(PARAM_ACTION, OracleDbType.Varchar2).Value = "ADD";

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void UpdateMedicineItem(string recordNo, string diseaseCode, MedicineInfo medicine)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_STORE_MED_ITEM, CommandType.StoredProcedure);

            AddParameters(recordNo, diseaseCode, medicine, cmd);
            cmd.Parameters.Add(PARAM_ACTION, OracleDbType.Varchar2).Value = "MODIFY";

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void DeleteMedicineItem(string recordNo, string diseaseCode , string medicineCode)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_DELETE_MED_ITEM, CommandType.StoredProcedure);

            cmd.Parameters.Add(PARAM_NBR, OracleDbType.Varchar2).Value = recordNo;
            cmd.Parameters.Add(PARAM_DIS, OracleDbType.Varchar2).Value = diseaseCode;
            cmd.Parameters.Add(PARAM_MED, OracleDbType.Varchar2).Value = medicineCode;

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public FirstAidRecordInfo Select(string recordNo)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT, CommandType.StoredProcedure);
            
            cmd.Parameters.Add(PARAM_NBR, OracleDbType.Varchar2).Value = recordNo;
            
            return (FirstAidRecordInfo)OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(FirstAidRecordInfo));
        }

        public ArrayList SelectByCriteria(string keyword, DateTime fromDate, DateTime toDate)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_BY_CRITERIA, CommandType.StoredProcedure);

            cmd.Parameters.Add("p_keyword", OracleDbType.Varchar2).Value = keyword;
            cmd.Parameters.Add("p_from_dt", OracleDbType.Date).Value = fromDate;
            cmd.Parameters.Add("p_to_dt", OracleDbType.Date).Value = toDate;

            return OraHelper.ExecuteQueries(this, cmd, typeof(FirstAidRecordInfo));
        }

        public ArrayList SelectDiseaseItems(string recordNo)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_DIS_ITEMS, CommandType.StoredProcedure);
            
            cmd.Parameters.Add(PARAM_NBR, OracleDbType.Varchar2).Value = recordNo;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(DiseaseInfo));
        }

        public ArrayList SelectMedicineItems(string recordNo, string diseaseCode)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_SELECT_MED_ITEMS, CommandType.StoredProcedure);
            
            cmd.Parameters.Add(PARAM_NBR, OracleDbType.Varchar2).Value = recordNo;
            cmd.Parameters.Add(PARAM_DIS, OracleDbType.Varchar2).Value = diseaseCode;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(MedicineInfo));
        }

        #endregion
    }
}
