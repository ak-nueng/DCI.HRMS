using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using DCI.Security.Model;
using DCI.Security.Persistence;
using System.Diagnostics;
using PCUOnline.Dao;

namespace DCI.Security.Service
{
    public class PermissionController
    {
        //Fields
        private static readonly PermissionController instance = new PermissionController();
        DaoFactory daoManager = DaoFactory.Instance();
        private PermissionInfo permDAO; 
        //Methods
        private PermissionController() 
        {
            daoManager = DaoFactory.Instance();
            permDAO = daoManager.PermissionDAO;
        }
        public static PermissionController Instance()
        {
            return instance;
        }
        public void AssignDefaultPermission(int usergroupId , string by)
        {
            try
            {
                daoManager.BeginTransaction();
                permDAO.SetDefaultPermission(usergroupId,by);
                daoManager.CommitTransaction();
            }
            catch
            {
                daoManager.RollBackTransaction();
                throw;
            }
            finally
            {
                daoManager.EndTransaction();
            }
        }
        public void AssignPermission(int usergroupId, ModuleInfo allowModule , string by)
        {
            try
            {
                daoManager.BeginTransaction();
                permDAO.SetPermission(usergroupId, allowModule, by);
                daoManager.CommitTransaction();
            }catch{
                daoManager.RollBackTransaction();
                throw;
            }finally{
                daoManager.EndTransaction();
            }
        }
        public ModuleInfo Get(int usergroupId, string allowModuleId)
        {
            try
            {
                daoManager.BeginTransaction(true);
                ModuleInfo modInfo = permDAO.LoadPermissionModule(usergroupId, allowModuleId);
                return modInfo;
            }catch{
                return null;
            }finally{
                daoManager.EndTransaction();
            }
        }
        public ArrayList GetAllowMainModules(int usergroupId , ApplicationType applicationType)
        {
            try
            {
                daoManager.BeginTransaction(true);
                ArrayList modules = permDAO.LoadPermissionModules(usergroupId, "0", applicationType , true);
                return modules;
            }
            catch
            {
                throw;
            }
            finally
            {
                daoManager.EndTransaction();
            }
        }
        public ArrayList GetAllowModules(int usergroupId , string allowMainModuleId , ApplicationType applicationType)
        {
            try
            {
                daoManager.BeginTransaction(true);
                ArrayList modules = permDAO.LoadPermissionModules(usergroupId, allowMainModuleId, applicationType , true);

                if (modules != null)
                {
                    foreach (ModuleInfo module in modules)
                    {
                        try
                        {
                            module.ModuleChildren = permDAO.LoadPermissionModuleFunctions(usergroupId, module.GuID, applicationType,true);
                        }
                        catch { }
                    }
                }
                return modules;
            }
            catch
            {
                throw;
            }
            finally
            {
                daoManager.EndTransaction();
            }
        }
        public ArrayList GetAllowModules(int usergroupId, ApplicationType applicationType)
        {
            try
            {
                daoManager.BeginTransaction(true);
                ArrayList modules = permDAO.LoadPermissionModules(usergroupId, applicationType , true);

                if (modules != null)
                {
                    foreach (ModuleInfo module in modules)
                    {
                        try
                        {
                            module.ModuleChildren = permDAO.LoadPermissionModuleFunctions(usergroupId, module.GuID, applicationType,true);
                        }
                        catch { }
                    }
                }
                return modules;
            }
            catch
            {
                throw;
            }
            finally
            {
                daoManager.EndTransaction();
            }
        }

        public ArrayList GetAllowFunctions(int usergroupId, string moduleId, ApplicationType applicationType)
        {
            try
            {
                AssignDefaultPermission(usergroupId, "SYSTEM");

                daoManager.BeginTransaction(true);
                ArrayList modules = permDAO.LoadPermissionModuleFunctions(usergroupId, moduleId, applicationType , true);
                
                return modules;
            }
            catch
            {
                throw;
            }
            finally
            {
                daoManager.EndTransaction();
            }
        }

        public ArrayList GetAllowFunctions(int usergroupId, string moduleId)
        {
            try
            {
                AssignDefaultPermission(usergroupId, "SYSTEM");

                daoManager.BeginTransaction(true);
                ArrayList modules = permDAO.LoadPermissionModuleFunctions(usergroupId, moduleId , true);
                return modules;
            }
            catch
            {
                throw;
            }
            finally
            {
                daoManager.EndTransaction();
            }
        }
        public ArrayList GetFunctions(int usergroupId, string moduleId)
        {
            try
            {
                AssignDefaultPermission(usergroupId, "SYSTEM");

                daoManager.BeginTransaction(true);
                ArrayList modules = permDAO.LoadPermissionModuleFunctions(usergroupId, moduleId, false);
                return modules;
            }
            catch
            {
                throw;
            }
            finally
            {
                daoManager.EndTransaction();
            }
        }
    }
}
