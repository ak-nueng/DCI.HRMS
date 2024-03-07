using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DCI.HRMS.Model.Organize
{
    [Serializable]
    public class DivisionInfo
    {
        private string code;
        private string name;
        private string shortname;
        private int totalEmployee;
        private DivisionType type = DivisionType.Group;
        private DivisionInfo division_Owner;
        private ArrayList division_Child;
        private ArrayList items;
        private string remark = "";

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string ShortName
        {
            get { return shortname; }
            set { shortname = value; }
        }
        public DivisionType Type
        {
            get { return type; }
            set { type = value; }
        }
        public int TotalEmployees
        {
            get { return totalEmployee; }
            set { totalEmployee = value; }
        }
        public DivisionInfo DivisionOwner
        {
            get { return division_Owner; }
            set { division_Owner = value; }
        }
        public ArrayList DivisionChild
        {
            get { return division_Child; }
            set { division_Child = value; }
        }
        public ArrayList Items
        {
            get { return items; }
            set { items = value; }
        }

        public static string ConvertToDivisionType(DivisionType type)
        {
            if (type == DivisionType.Department)
                return "DEPT";
            if (type == DivisionType.Section)
                return "SECT";
            if (type == DivisionType.Group)
                return "GRP";

            return "GRP";
        }
        public static DivisionType ConvertToDivisionType(string s)
        {
            if (s.ToUpper() == "DEPT")
                return DivisionType.Department;
            if (s.ToUpper() == "SECT")
                return DivisionType.Section;
            if (s.ToUpper() == "GRP")
                return DivisionType.Group;

            return DivisionType.Group;
        }
        public string DispText
        {
            get
            {
                return this.code + ":" + this.ToString() + " " + ConvertToDivisionType(this.Type);
            }
        }

        public override string ToString()
        {
            if (DivisionOwner != null)
            {
                if (DivisionOwner.Name != null
                    && DivisionOwner.Name != string.Empty)
                {
                    return DivisionOwner.ToString() + " / " + this.Name;
                }
            }
                
            return this.Name;
        }
        public string FullName
        {
            get
            {
                if (DivisionOwner != null)
                {
                    if (DivisionOwner.Name != null
                        && DivisionOwner.Name != string.Empty)
                    {
                        return DivisionOwner.FullName + " / " + this.Name;
                    }
                }
                return this.Name;
            }
        }
    }
}
