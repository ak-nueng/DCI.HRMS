using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DCI.Security.Model
{
    [Serializable]
    public class ModuleInfo : IComparable , ICloneable
    {
        private string id;
        private string name;
        private string guid;
        private string key;
        private string descr;
        private string classNamespace;
        private string className;
        private string icon;
        private int sortingNo;
        private bool enable = false;

        private PermissionInfo permission;
        private UserGroupInfo userGroup;
        private ModuleInfo owner;
        private ModuleType type = ModuleType.Menu;
        private ApplicationType applicationType = ApplicationType.WINDOWS;
        private ArrayList subModules;
        public bool visible = true;

        public ModuleInfo()
        {
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        public string GuID
        {
            get { return guid; }
            set { guid = value; }
        }
        public string Key
        {
            get { return key; }
            set { key = value; }
        }
        public string Name {
            get { return name; }
            set { name = value; }
        }
        public string Description
        {
            get { return descr; }
            set { descr = value; }
        }
        public int SortingNo
        {
            get { return sortingNo; }
            set { sortingNo = value; }
        }
        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }
        public string NameSpace
        {
            get { return classNamespace; }
            set { classNamespace = value; }
        }
        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }
        public string Icon
        {
            get { return icon; }
            set { icon = value; }
        }
        public UserGroupInfo UserGroup
        {
            get { return userGroup; }
            set { userGroup = value; }
        }
        public ModuleInfo Owner
        {
            get { return owner; }
            set { owner = value; }
        }
        public ModuleType Type
        {
            get { return type; }
            set { type = value; }
        }
        public ApplicationType ApplicationType
        {
            get { return applicationType; }
            set { applicationType = value; }
        }
        public ArrayList SubModules
        {
            get {
                SetOwner();
                return subModules; 
            }
            set
            { 
                subModules = value;
            }
        }

        private void SetOwner()
        {
            if (subModules != null && subModules.Count > 0)
            {
                foreach (ModuleInfo module in subModules)
                {
                    module.Owner = this;
                    module.UserGroup = this.UserGroup;
                }
            }
        }

        public PermissionInfo Permission
        {
            get { return permission; }
            set { permission = value;
            if (permission != null)
                permission.ModuleOwner = this;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is ModuleInfo)
            {
                ModuleInfo mod = obj as ModuleInfo;
                if (mod.Id == this.Id)
                    return true;

                return false;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #region IComparable Members

        public int CompareTo(object obj)
        {
            ModuleInfo temp = obj as ModuleInfo;
            if(temp != null)
            {
                return this.SortingNo.CompareTo(temp.SortingNo);
            }
            return -1;
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
        public static ModuleType ConvertModuleType(string moduleType)
        {
            if (moduleType == "FRM")
                return ModuleType.Form;

            if (moduleType == "RPT")
                return ModuleType.Report;

            return ModuleType.Menu;
        }
        public static string ConvertModuleType(ModuleType moduleType)
        {
            if (moduleType == ModuleType.Form)
                return "FRM";

            if (moduleType == ModuleType.Report)
                return "RPT";

            return "MNU";
        }
        public static ApplicationType ConvertApplicationType(string applicationType)
        {
            if (applicationType == "DOS")
                return ApplicationType.DOS;

            if (applicationType == "WEB")
                return ApplicationType.WWW;

            return ApplicationType.WINDOWS;
        }
        public static string ConvertApplicationType(ApplicationType applicationType)
        {
            if (applicationType == ApplicationType.DOS)
                return "DOS";
            
            if (applicationType == ApplicationType.WWW)
                return "WEB";

            return "WIN";
        }

        
    }
}
