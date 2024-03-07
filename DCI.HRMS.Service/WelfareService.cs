using System;
using System.Collections.Generic;
using System.Text;
using DCI.HRMS.Persistence;
using System.Collections;
using DCI.HRMS.Model.Common;
using DCI.HRMS.Model;
using DCI.HRMS.Model.Welfare;
using System.Data;

namespace DCI.HRMS.Service
{
    public class WelfareService
    {
        private static readonly WelfareService instance = new WelfareService();
        private DaoFactory factory = DaoFactory.Instance();
        private SubContractDaoFactory subfactory = SubContractDaoFactory.Instance();
        private TraineeDaoFactory trfactory = TraineeDaoFactory.Instance();
        private IDictionaryDao dictionaryDao;
        private IEmployeeDao employeeDao;
        private IEmployeeDao subDao;
        private IEmployeeDao trDao;

        private IWelfairDao welDao;

        private WelfareService()
        {
            dictionaryDao = factory.CreateDictionaryDao();
            employeeDao = factory.CreateEmployeeDao();
            subDao = subfactory.CreateEmployeeDao();
            trDao = trfactory.CreateEmployeeDao();

            welDao = factory.CreateWlfairDao();

        }
        public static WelfareService Instance()
        {
            return instance;
        }
        public BusStopInfo GetBusStop(string busway, string stopCode)
        {
            try
            {
                factory.StartTransaction(true);/*
                ArrayList busst = dictionaryDao.Find("STOP", busway + stopCode);
                BusStopInfo bst = new BusStopInfo();
                foreach (BasicInfo var in busst)
                {
                    if (var.Description == busway && !var.Code.Contains("-"))
                    {

                        bst.Code = var.Code.Substring(0, 1);
                        bst.StopCode = var.Code.Substring(1);
                        bst.DispText = var.NameForSearching;
                        bst.StopName = var.Name;
                        try
                        {
                            bst.Order = int.Parse(var.DescriptionTh);
                        }
                        catch
                        {
                            bst.Order = 0;
                        }

                        try
                        {
                            bst.TimeDay = var.DetailTh.Split(',')[0];
                        }
                        catch
                        {
                            bst.TimeDay = "";
                        }
                        try
                        {
                            bst.TimeNight = var.DetailTh.Split(',')[1];
                        }
                        catch
                        {
                            bst.TimeNight = "";
                        }

                    }
                }

                return bst;*/

                return welDao.GetBusStop(busway, stopCode);
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
        public ArrayList GetBusStop(string busway)
        {
            try
            {
                factory.StartTransaction(true);

                /*
                ArrayList busst = dictionaryDao.Find("STOP", busway + "%");
                ArrayList rStop = new ArrayList();
                foreach (BasicInfo var in busst)
                {
                    if (var.Description == busway && !var.Code.Contains("-"))
                    {
                        BusStopInfo bst = new BusStopInfo();
                        bst.Code = var.Code.Substring(0, 1);
                        bst.StopCode = var.Code.Substring(1);
                        bst.DispText = var.NameForSearching;
                        bst.StopName = var.Name;
                        try
                        {
                            bst.Order = int.Parse(var.DescriptionTh);
                        }
                        catch
                        {
                            bst.Order = 0;
                        }

                        try
                        {
                            bst.TimeDay = var.DetailTh.Split(',')[0];
                        }
                        catch
                        {
                            bst.TimeDay = "";
                        }
                        try
                        {
                            bst.TimeNight = var.DetailTh.Split(',')[1];
                        }
                        catch
                        {
                            bst.TimeNight = "";
                        }
                        rStop.Add(bst);
                    }
                }
                rStop.Sort(new BusStopCompare());*/


                return welDao.GetBusStop(busway);
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

        public ArrayList GetAllBusWay()
        {
            try
            {
                factory.StartTransaction(true);
                /*
                ArrayList busst = dictionaryDao.SelectAll("BUS");
                ArrayList rStop = new ArrayList();
                foreach (BasicInfo var in busst)
                {
                    BusWayInfo bw = new BusWayInfo();
                    bw.Code = var.Code;
                    bw.NameEng = var.DetailEn;
                    bw.NameThai = var.DetailTh;

                    //   bw.Stops = GetBusStop(var.Code);
                    rStop.Add(bw);
                }
                rStop.Sort(new BusWayCompare());
                return rStop;*/
                return welDao.GetAllBusWay();
            }
            catch //(Exception ex)
            {
               // throw ex;
                return null;
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public BusWayInfo GetBusWay(string busWay)
        {
            try
            {
                factory.StartTransaction(true);

                BusWayInfo bw = welDao.GetBusWay(busWay);
           

                bw.Stops = GetBusStop(bw.Code);

                return bw;
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
        public void SaveBusStop(BusStopInfo item)
        {
            try
            {
                factory.StartTransaction(false);

                BasicInfo dItem = new BasicInfo();
                dItem.Type = "STOP";
                dItem.Code = item.Busway + item.StopCode;
                dItem.DetailEn = item.StopName;
                dItem.Description = item.Busway;
                dItem.DescriptionTh = item.Order.ToString("00");
                dItem.DetailTh = item.TimeDay + "," + item.TimeNight;

                dictionaryDao.SaveDictData(dItem);

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
        public void UpdateBusStop(BusStopInfo item)
        {
            try
            {
                factory.StartTransaction(false);

                BasicInfo dItem = new BasicInfo();
                dItem.Type = "STOP";
                dItem.Code = item.Busway + item.StopCode;
                dItem.DetailEn = item.StopName;
                dItem.Description = item.Busway;
                dItem.DescriptionTh = item.Order.ToString("00");
                dItem.DetailTh = item.TimeDay + "," + item.TimeNight;

                dictionaryDao.UpdateDictData(dItem);

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
        public void DeleteBusStop(string busWay, string stopCode)
        {
            try
            {
                factory.StartTransaction(false);

                dictionaryDao.DeleteDictData("STOP", busWay + stopCode); ;

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
        public void SaveBusWay(BusWayInfo item)
        {
            try
            {
                factory.StartTransaction(false);

                BasicInfo dItem = new BasicInfo();
                dItem.Type = "BUS";
                dItem.Code = item.Code;
                dItem.DetailEn = item.NameEng;
                dItem.Description = item.Code;
                dItem.DescriptionTh = item.Order.ToString("00");
                dItem.DetailTh = item.NameThai;

                dictionaryDao.SaveDictData(dItem);

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
        public void UpdateBusWay(BusWayInfo item)
        {
            try
            {
                factory.StartTransaction(false);

                BasicInfo dItem = new BasicInfo();
                dItem.Type = "BUS";
                dItem.Code = item.Code;
                dItem.DetailEn = item.NameEng;
                dItem.Description = item.Code;
                dItem.DescriptionTh = item.Order.ToString("00");
                dItem.DetailTh = item.NameThai;

                dictionaryDao.UpdateDictData(dItem);

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
        public void DeleteBusWay(string busWay)
        {
            try
            {
                factory.StartTransaction(false);

                dictionaryDao.DeleteDictData("BUS", busWay); ;

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

        public ArrayList GetCurrentEmployeesByBusWay(string busWay, string stopCode)
        {
            ArrayList empLs = new ArrayList();
            try
            {
                factory.StartTransaction(true);
                subfactory.StartTransaction(true);
                trfactory.StartTransaction(true);

                empLs = employeeDao.SelectCurEmpByBusWay(busWay, stopCode);

                if (empLs == null)
                {
                    empLs = new ArrayList();
                }
                try
                {
                    ArrayList subLs = subDao.SelectCurEmpByBusWay(busWay, stopCode);
                    foreach (var item in subLs)
                    {
                        empLs.Add(item);
                    }
                }
                catch 
                {  }


                try
                {
                    ArrayList trLs = trDao.SelectCurEmpByBusWay(busWay, stopCode);
                    foreach (var item in trLs)
                    {
                        empLs.Add(item);
                    }
                }
                catch 
                {}

                return empLs;
            }
            catch(Exception ex)
            {
                throw ex;

            }
            finally
            {
                factory.EndTransaction();
                subfactory.EndTransaction();
                trfactory.EndTransaction();
            }


        }

        public DataSet GetCurrentEmployeesByBusWayDataset(string busWay, string stopCode)
        {
            DataSet empLs = new DataSet();
            try
            {
                factory.StartTransaction(true);
                subfactory.StartTransaction(true);
                trfactory.StartTransaction(true);

                empLs = employeeDao.SelectCurEmpByBusWayDataset(busWay, stopCode);

               
                try
                {
                    DataSet subDs = subDao.SelectCurEmpByBusWayDataset(busWay, stopCode);
                    if (subDs.Tables.Count>0)
                    {
                        DataTable subTb = subDs.Tables[0];
                        subTb.Columns.Remove("STOP1");
                        foreach (DataRow item in subTb.Rows)
                        {
                            //empLs.Tables[0].Rows.Add(item.ItemArray);
                            DataRow newRow = empLs.Tables[0].NewRow();
                            newRow["CODE"] = item["CODE"];
                            newRow["PREN"] = item["PREN"];
                            newRow["NAME"] = item["NAME"];
                            newRow["SURN"] = item["SURN"];
                            newRow["TPREN"] = item["TPREN"];
                            newRow["TNAME"] = item["TNAME"];
                            newRow["TSURN"] = item["TSURN"];
                            newRow["BIRTH"] = item["BIRTH"];
                            newRow["SEX"] = item["SEX"];
                            newRow["JOIN"] = item["JOIN"];
                            newRow["WTYPE"] = item["WTYPE"];
                            newRow["WSTS"] = item["WSTS"];
                            newRow["IDNO"] = item["IDNO"];
                            newRow["RESIGN"] = item["RESIGN"];
                            newRow["RSTYPE"] = item["RSTYPE"];
                            newRow["RSREASON"] = item["RSREASON"];
                            newRow["GRPL"] = item["GRPL"];
                            newRow["GRPOT"] = item["GRPOT"];
                            newRow["SCHOOL"] = item["SCHOOL"];
                            newRow["TSCHOOL"] = item["TSCHOOL"];
                            newRow["MAIL"] = item["MAIL"];
                            newRow["TELEPHONE"] = item["TELEPHONE"];
                            newRow["DEGREE"] = item["DEGREE"];
                            newRow["P_GRADE"] = item["P_GRADE"];
                            newRow["SECTION"] = item["SECTION"];
                            newRow["POSI_CD"] = item["POSI_CD"];
                            newRow["DV_CD"] = item["DV_CD"];
                            newRow["BUS"] = item["BUS"];
                            newRow["BUSWAY"] = item["BUSWAY"];
                            newRow["STOP"] = item["STOP"];
                            newRow["BUSSTOP"] = item["BUSSTOP"];

                            empLs.Tables[0].Rows.Add(newRow);
                        }
                    }
                  
                }
                catch
                {}

                /* 
                try
                {
                    DataSet trnDs = trDao.SelectCurEmpByBusWayDataset(busWay, stopCode);
                    if (trnDs.Tables.Count > 0)
                    {
                        DataTable trnTb = trnDs.Tables[0];
                        foreach (DataRow item in trnTb.Rows)
                        {
                            empLs.Tables[0].Rows.Add(item.ItemArray);
                        }
                    }
                  
                }
                catch
                { }
                */

                empLs.AcceptChanges();

                return empLs;
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                factory.EndTransaction();
                subfactory.EndTransaction();
                trfactory.EndTransaction();
            }


        }
        public void UpdateEmployeeBusStop(string code, string busWay, string stopCode)
        {
            if (code.StartsWith ("I"))
            {
                try
                {
                    subfactory.StartTransaction(false);

                    subDao.UpdateEmployeeBusWay(code, busWay, stopCode);

                    subfactory.CommitTransaction();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    subfactory.EndTransaction();
                } 
            }
            else if (code.StartsWith("7"))
            {
                try
                {
                    trfactory.StartTransaction(false);

                    trDao.UpdateEmployeeBusWay(code, busWay, stopCode);

                    trfactory.CommitTransaction();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    trfactory.EndTransaction();
                }
            }
            else{
                try
                {
                    factory.StartTransaction(false);

                    employeeDao.UpdateEmployeeBusWay(code, busWay, stopCode);

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
    public class BusWayCompare : IComparer
    {

        // Calls CaseInsensitiveComparer.Compare with the parameters . 
        int IComparer.Compare(object x, object y)
        {
            BusWayInfo xx = (BusWayInfo)x;
            BusWayInfo yy = (BusWayInfo)y;
            return ((new CaseInsensitiveComparer()).Compare( xx.Order,yy.Order));
        }

    }
    public class BusStopCompare : IComparer
    {

        // Calls CaseInsensitiveComparer.Compare with the parameters . 
        int IComparer.Compare(object x, object y)
        {
            BusStopInfo xx = (BusStopInfo)x;
            BusStopInfo yy = (BusStopInfo)y;
            return ((new CaseInsensitiveComparer()).Compare(xx.Order,yy.Order ));
        }

    }
}
