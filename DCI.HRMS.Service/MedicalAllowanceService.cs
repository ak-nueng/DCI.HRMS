using System;
using System.Collections.Generic;
using System.Text;
using DCI.HRMS.Persistence;
using DCI.HRMS.Model.Welfare;
using System.Collections;
using DCI.HRMS.Model;

namespace DCI.HRMS.Service
{
    public class MedicalAllowanceService
    {
        private const string MEDICAL_AMOUNT = "MEPD";
        private static MedicalAllowanceService instance = new MedicalAllowanceService();
        private DaoFactory factory = DaoFactory.Instance();
        private IMedicalDao medDao;
        private IKeyGeneratorDao keyDao;
        private IDictionaryDao dictionaryDao;

        //private IEmployeeDao empDao;
        private MedicalAllowanceService()
        {
            medDao = factory.CreateMedicalDao();
            keyDao = factory.CreateKeyDao();
            dictionaryDao = factory.CreateDictionaryDao();
           // empDao = factory.CreateEmployeeDao();
        }
        public static MedicalAllowanceService Instance()
        {
            return instance;
        }
        public BasicInfo GetMedicalAmount(string code)
        {
            try
            {
                factory.StartTransaction(true);
                return dictionaryDao.Select(MEDICAL_AMOUNT,code);
            }
            catch
            {

                return null;
            }
            finally
            {
                factory.EndTransaction();
            }


        }
        public string LoadRecordKey()
        {
            try
            {
                factory.StartTransaction(true);
                return keyDao.LoadUnique("MEA").ToString(true);
            }
            catch
            {

                return null;
            }
            finally
            {
                factory.EndTransaction();
            }


        }
        private string GenRecordKey()
        {
            try
            {
               
                return keyDao.NextId("MEA");
            }
            catch
            {

                return null;
            }
       


        }

        public string LoadRecordKey2()
        {
            try
            {
                factory.StartTransaction(true);
                return keyDao.LoadUnique("MEX").ToString(true);
            }
            catch
            {

                return null;
            }
            finally
            {
                factory.EndTransaction();
            }


        }
        private string GenRecordKey2()
        {
            try
            {

                return keyDao.NextId("MEX");
            }
            catch
            {

                return null;
            }



        }


        public ArrayList GetAutoCompHospital()
        {

            try
            {
           
                factory.StartTransaction(true);
                return medDao.GetMedHospital();
            }
            catch
            {
                return null;
            }
            finally
            {
                factory.EndTransaction();
            }


        }
        public ArrayList GetAutoCompSymptom()
        {

            try
            {

                factory.StartTransaction(true);
                return medDao.GetMedSymptom();
            }
            catch
            {
                return null;
            }
            finally
            {
                factory.EndTransaction();
            }


        }
        public ArrayList GetAutoCompDistrict()
        {

            try
            {

                factory.StartTransaction(true);
                return medDao.GetMedDistrict();
            }
            catch
            {
                return null;
            }
            finally
            {
                factory.EndTransaction();
            }


        }
        public ArrayList GetAutoCompProvince()
        {

            try
            {

                factory.StartTransaction(true);
                return medDao.GetMedProvince();
            }
            catch
            {
                return null;
            }
            finally
            {
                factory.EndTransaction();
            }


        }
        public ArrayList GetMedical(string docid1, string docid2)
        {

            try
            {

                factory.StartTransaction(true);
                ArrayList shMed = medDao.GetMedical(docid1, docid2);
                ArrayList rtMed = new ArrayList();

                foreach (MedicalAllowanceInfo item in shMed)
                {
                    if (item.RelationType != "")
                    {
                        item.Relation = dictionaryDao.Select("RELA", item.RelationType.Substring(4)).DetailTh;

                    }
                    rtMed.Add(item);
                }
                return rtMed;
            }
            catch
            {
                return null;
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public ArrayList GetMedical(string code, DateTime dtfrom ,DateTime dtto )
        {

            try
            {


                factory.StartTransaction(true);
                ArrayList shMed = medDao.GetMedical(code, dtfrom, dtto);
                ArrayList rtMed = new ArrayList();
                foreach (MedicalAllowanceInfo item in shMed)
                {
                    if (item.RelationType != "")
                    {
                        item.Relation = dictionaryDao.Select("RELA", item.RelationType.Substring(4)).DetailTh;

                    }
                    rtMed.Add(item);
                }
                return rtMed;
            }
            catch
            {
                return null;
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public ArrayList GetMedical(string code, DateTime year)
        {

            try
            {
                DateTime dtfrom = DateTime.Parse("01/01/" + year.Year.ToString());
                DateTime dtto = dtfrom.AddYears(1).AddDays(-1);

                factory.StartTransaction(true);
                ArrayList shMed= medDao.GetMedical(code,dtfrom,dtto);
                ArrayList rtMed = new ArrayList();
                foreach (MedicalAllowanceInfo  item in shMed)
                {
                    if (item.RelationType!="")
                    {
                          item.Relation = dictionaryDao.Select("RELA", item.RelationType.Substring(4)).DetailTh;
               
                    }
                        rtMed.Add(item);
                }
                return rtMed;
            }
            catch
            {
                return null;
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public void SaveMedical(MedicalAllowanceInfo med)
        {
            try
            {

                factory.StartTransaction(false);
                med.DocNo = GenRecordKey();
                medDao.AddMedical(med);
                factory.CommitTransaction();

            }
            catch (Exception ex)
            {
                throw ex; ;
            }
               
            finally
            {
                factory.EndTransaction();
            }

        }
        public void SaveMedical2(MedicalAllowanceInfo med)
        {
            try
            {

                factory.StartTransaction(false);
                med.DocNo = GenRecordKey2();
                medDao.AddMedical(med);
                factory.CommitTransaction();

            }
            catch (Exception ex)
            {
                throw ex; ;
            }

            finally
            {
                factory.EndTransaction();
            }

        }
        public void UpdateMedical(MedicalAllowanceInfo med)
        {
            try
            {

                factory.StartTransaction(false);
                medDao.UpdateMedical(med);
                factory.CommitTransaction();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                factory.EndTransaction();
            }

        }
        public void DeleteMedical(MedicalAllowanceInfo med)
        {
            try
            {

                factory.StartTransaction(false);
                medDao.DeleteMedical(med);
                factory.CommitTransaction();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                factory.EndTransaction();
            }

        }
        




    }
}
