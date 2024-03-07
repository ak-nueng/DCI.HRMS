using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using DCI.Security.Model;
using DCI.Security.Persistence;
using System.Diagnostics;


namespace DCI.Security.Service
{
    public class UserGroupService
    {
        private readonly static UserGroupService instance = new UserGroupService();
        private DaoFactory factory = DaoFactory.Instance();
        private IAllowModuleDao allowModuleDao;
        private IUserGroupDao userGroupDao;
        private IModuleDao moduleDao;

        private UserGroupService()
        {
            allowModuleDao = factory.CreateAllowModuleDao();
            userGroupDao = factory.CreateUserGroupDao();
            moduleDao = factory.CreateModuleDao();
        }

        public static UserGroupService Instance()
        {
            return instance;
        }

        public void CheckUserGroupAvailable(UserGroupInfo userGroup)
        {
            if (!userGroup.Enable)
                throw new Exception(string.Format("User group {0} is disabled , please contact your administrator."
                                ,userGroup.Name));
        }
        public ArrayList SelectAll()
        {
            try
            {
                factory.StartTransaction(true);
                return userGroupDao.Select();
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
        public void SaveUserGroup(UserGroupInfo grp)
        {
            try
            {
                factory.StartTransaction(false);
                 userGroupDao.Save(grp);
                factory.CommitTransaction();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                factory.EndTransaction();
            }

        }
        public void UpdateUserGroup(UserGroupInfo grp)
        {
            try
            {
                factory.StartTransaction(false);
                userGroupDao.Update(grp);
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
        public void DeleteUserGroup(int grpId)
        {
            try
            {
                factory.StartTransaction(false);
                userGroupDao.Delete(grpId);
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

        public void AddAllowModulesToMenu(UserGroupInfo userGroup, ArrayList allMenu, ArrayList allowModules)
        {
            int counter = 0;
            foreach (ModuleInfo module in allowModules)
            {
                module.UserGroup = userGroup;

                foreach (ModuleInfo menu in allMenu)
                {
                    if (module.Owner.Equals(menu))
                    {
                        if (menu.SubModules == null)
                            menu.SubModules = new ArrayList();

                        module.Owner = menu;
                        menu.SubModules.Add(module);
                        counter++;
                        break;
                    }
                }
            }
            Debug.WriteLine("Total Sub Module : " + counter.ToString());
        }
        public ArrayList ArrangeMenuStructure(ArrayList allMenu)
        {
            ArrayList menuStructure = new ArrayList();
            foreach (ModuleInfo menu in allMenu)
            {
                for (int i = 0; i < allMenu.Count; i++)
                {
                    if (menu.Owner != null)
                    {
                        ModuleInfo owner = allMenu[i] as ModuleInfo;
                        if (menu.Owner.Id == owner.Id)
                        {
                            menu.Owner = owner;
                            if (owner.SubModules == null)
                                owner.SubModules = new ArrayList();

                            owner.SubModules.Add(menu);
                            break;
                        }
                    }
                }
                //Switch(allMenu, menu);
                
                if (menu.Owner == null || menu.Owner.Id == "0")
                    menuStructure.Add(menu);
            }

            if (menuStructure.Count > 0)
                menuStructure.Sort();

            return menuStructure;
        }

        private ArrayList ClearZeroSubModules(ArrayList allMenu)
        {
            return null;
        }

        private static void Switch(ArrayList allMenu, ModuleInfo menu)
        {
            for (int i = 0; i < allMenu.Count; i++)
            {
                if (menu.Owner != null)
                {
                    ModuleInfo m = allMenu[i] as ModuleInfo;
                    if (menu.Owner.Id == m.Id)
                    {
                        if (m.SubModules == null)
                            m.SubModules = new ArrayList();

                        menu.Owner = m;
                        m.SubModules.Add(menu);
                        break;
                    }
                }
            }
        }

        public ArrayList GetAllowModules(int userGroupId , ApplicationType applicationType)
        {
            ArrayList allMenu= new ArrayList();
            ArrayList allowModules= new ArrayList();

            UserGroupInfo userGroup = new UserGroupInfo();

            try
            {
                factory.StartTransaction(true);

                userGroup = userGroupDao.Select(userGroupId);
                CheckUserGroupAvailable(userGroup);

                allMenu = moduleDao.SelectByModuleType(ModuleType.Menu, applicationType);
                allowModules = allowModuleDao.SelectAllowModules(userGroupId, applicationType);
            }
            catch
            {
            }
            finally
            {
                factory.EndTransaction();
            }

            if (allMenu != null && allMenu.Count > 0)
            {
                if (allowModules != null && allowModules.Count > 0)
                {
                    AddAllowModulesToMenu(userGroup , allMenu, allowModules);
                    return ArrangeMenuStructure(allMenu);
                }
            }
            return null;
        }
    

        public ModuleInfo GetAllowModule(int userGroupId, string moduleId)
        {
            ModuleInfo allowModule;
            UserGroupInfo userGroup;

            try
            {
                factory.StartTransaction(true);

                userGroup = userGroupDao.Select(userGroupId);
                CheckUserGroupAvailable(userGroup);

                allowModule = allowModuleDao.SelectAllowModule(moduleId , userGroupId);
                allowModule.UserGroup = userGroup;

                if (!allowModule.Permission.AllowAccess)
                    throw new Exception("Permission denied for this module. Please contact your administrator.");

                return allowModule;
            }catch
            {
                throw;
            }finally
            {
                factory.EndTransaction();
            }
        }


        public UserGroupPermission GetGroupPermission(int userGroupId, string moduleId)
        {
         

            try
            {
                factory.StartTransaction(true);


                return allowModuleDao.SelectUserGroupPermission(moduleId,userGroupId); ;
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

        public void SaveGroupPermission(UserGroupPermission perm)
        {


            try
            {
                factory.StartTransaction(false);


                 allowModuleDao.AddUserGroupPermission(perm); 
                factory.CommitTransaction();

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                factory.EndTransaction();
            }
        }
        public void UpdateGroupPermission(UserGroupPermission perm)
        {


            try
            {
                factory.StartTransaction(false);


                allowModuleDao.UpdateUserGroupPermission(perm);
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
