using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DCI.HRMS.Model.Common;

namespace DCI.HRMS.Model.Personal
{
    [Serializable]
    public class PersonInfo : ObjectInfo
    {
        private string code;
        private string gender="";
        private string genderTH="";
 


        private string citizenNbr="";
        private string taxId = "";

        private DateTime  idissuedate;
        private NameInfo nameTH;
        private NameInfo nameENG;

        private AddressInfo addressTH;
        private AddressInfo homeaddressTH;
        private AddressInfo addressENG;
        private AddressInfo homeaddressENG;
        private DateTime birthDate = new DateTime(1900, 1, 1);
        private EducationInfo education;
        private string religion="";
        private string marrysts="";
        private int sons=0;
        private int sonb=0;
        private string taxno="";
        private string militarysts="";
        private string remark = "";
        private string refPerson1 = "";
        private string refContact1 = "";
        private string refPerson2 = "";
    private string refContact2 = "";
        

     //   private ObjectInfo inform = new ObjectInfo();




        public PersonInfo()
        {
        }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public string GenderTH
        {
            get { return genderTH; }
            set { genderTH = value; }
        }
        public NameInfo NameInThai
        {
            get { return nameTH; }
            set { nameTH = value; }
        }
        public NameInfo NameInEng
        {
            get { return nameENG; }
            set { nameENG = value; }
        }
        public DateTime BirthDate
        {
            get { 
                if (birthDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || birthDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return birthDate;
                }
            }
            set { birthDate = value; }
        }
        public string CitizenId
        {
            get { return citizenNbr; }
            set { citizenNbr = value; }
        }
        public DateTime IdcardIssueDate
        {
            get { 
                if (idissuedate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || idissuedate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return idissuedate;
                }
            }
            set { idissuedate = value; }
        }
  

        public string TaxId
        {
            get { return taxId; }
            set { taxId = value; }
        }
        public string Religion
        {
            get { return religion; }
            set { religion = value; }
        }

        public string MarryStatus
        {
            get { return marrysts; }
            set { marrysts = value; }
        }
        public int  Sons
        {
            get { return sons ; }
            set { sons = value; }
        }
        public int Sonb
        {
            get { return sonb; }
            set { sonb = value; }
        }
        public string MilitaryStatus
        {
            get { return militarysts; }
            set { militarysts = value; }
        }
        public string TaxNumber
        {
            get { return taxno; }
            set { taxno = value; }
        }


        public AddressInfo PresentAddressInThai
        {
            get { return addressTH; }
            set { addressTH = value; }
        }
        public AddressInfo PresentAddressInEng
        {
            get { return addressENG; }
            set { addressENG = value; }
        }
        public AddressInfo HomeAddressInThai
        {
            get { return homeaddressTH; }
            set { homeaddressTH = value; }
        }
        public AddressInfo HomeAddressInEng
        {
            get { return homeaddressENG; }
            set { homeaddressENG = value; }
        }
        public EducationInfo Education
        {
            get { return education; }
            set { education = value; }
        }

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        public string RefPerson1
        {
            get { return refPerson1; }
            set { refPerson1 = value; }
        }

        public string RefContact1
        {
            get { return refContact1; }
            set { refContact1 = value; }
        }

        public string RefPerson2
        {
            get { return refPerson2; }
            set { refPerson2 = value; }
        }


        public string RefContact2
        {
            get { return refContact2; }
            set { refContact2 = value; }
        }
        /*
        public string CreateBy
        {
            get { return inform.CreateBy; }
        }
        public DateTime CreateDateTime
        {
            get { return inform.CreateDateTime; }
        }
        public string LastUpdateBy
        {
            get { return inform.LastUpdateBy; }
        }
        public DateTime LastUpDateDateTime
        {
            get { return inform.LastUpdateDateTime; }
        }
        public ObjectInfo Inform
        {

            set { this.inform = value; }
        }*/
    }
}
