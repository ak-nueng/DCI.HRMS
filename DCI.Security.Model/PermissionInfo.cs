using System;
using System.Collections.Generic;
using System.Text;

namespace DCI.Security.Model
{
    [Serializable]
    public class PermissionInfo
    {
        private ModuleInfo owner;
        private bool addnew = false;
        private bool access = false;
        private bool edit = false;
        private bool delete = false;
        private bool viewReport = false;
        private bool exportData = false;
        private bool changeStatus = false;

        public PermissionInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public ModuleInfo ModuleOwner
        {
            get { return owner; }
            set { owner = value; }
        }
        public bool AllowAddNew
        {
            get { return this.addnew; }
            set { this.addnew = value; }
        }

        public bool AllowAccess
        {
            get { return this.access; }
            set { this.access = value; }
        }

        public bool AllowEdit
        {
            get { return this.edit; }
            set { this.edit = value; }
        }

        public bool AllowDelete
        {
            get { return this.delete; }
            set { this.delete = value; }
        }

        public bool AllowPrintReport
        {
            get { return this.viewReport; }
            set { this.viewReport = value; }
        }

        public bool AllowExportData
        {
            get { return this.exportData; }
            set { this.exportData = value; }
        }

        public bool AllowChangeStatus
        {
            get { return this.changeStatus; }
            set { this.changeStatus = value; }
        }
    }
}
