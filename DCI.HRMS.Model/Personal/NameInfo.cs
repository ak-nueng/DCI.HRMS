using System;
using System.Collections.Generic;
using System.Text;

namespace DCI.HRMS.Model.Personal
{
    [Serializable]
    public class NameInfo
    {
        private string title="";
        private string name="";
        private string surname="";
        private string nickName = "";

        public NameInfo() { }

        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public string Surname
        {
            get { return this.surname; }
            set { this.surname = value; }
        }

        public override string ToString()
        {
            return this.Title + this.Name + "  " + this.Surname;
        }
        public string FullName
        {
            get
            {
                return this.Title + this.Name + "  " + this.Surname;
            }
        }

        public string NickName
        {
            get { return nickName; }
            set { nickName = value; }
        }
    }
}
