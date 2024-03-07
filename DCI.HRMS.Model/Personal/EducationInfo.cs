using System;
using System.Collections.Generic;
using System.Text;

namespace DCI.HRMS.Model.Personal
{   
    [Serializable]
    public class EducationInfo
    {
        private string degreetype="";
        private string degree="";
        private string major="";
        private string tmajor="";
        private string school="";
        private string tschool="";
        private string gdyear="";
        public EducationInfo()
        {
        }
        public string DegreeType
        {
            set { degreetype = value; }
            get { return degreetype; }
            
        }
        public string Degree
        {
            set { degree = value; }
            get { return degree; }

        }
        public string MajorInEng
        {
            set { major = value; }
            get { return major; }

        }
        public string MajorInThai
        {
            set { tmajor = value; }
            get { return tmajor; }

        }
        public string SchoolInEng
        {
            set { school = value; }
            get { return school; }

        }
        public string SchoolInThai
        {
            set { tschool = value; }
            get { return tschool; }

        }
        public string GraduateYear
        {
            set { gdyear = value; }
            get { return gdyear; }

        }

    }
}
