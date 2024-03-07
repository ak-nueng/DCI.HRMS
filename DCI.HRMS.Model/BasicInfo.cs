using System;
using System.Collections.Generic;
using System.Text;

namespace DCI.HRMS.Model
{
    [Serializable]
    public class BasicInfo
    {
        private string type;


        private string code;
        private string name;
        private string descr;
        private string descrTh;
        private string detailEn;
        private string detailTh;

        public BasicInfo()
        {
        }
        public BasicInfo(string code, string name, string description)
        {
            this.code = code;
            this.name = name;
            this.descr = description;
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public string Description
        {
            get { return this.descr; }
            set { this.descr = value; }
        }
        public string DescriptionTh
        {
            get { return this.descrTh; }
            set { this.descrTh = value; }
        }
        public string DetailEn
        {
            get { return this.detailEn; }
            set { this.detailEn = value; }
        }
        public string DetailTh
        {
            get { return this.detailTh; }
            set { this.detailTh = value; }
        }
        public virtual string NameForSearching
        {
            get { return this.code + " : " + this.name; }
        }

    }
}