using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DCI.HRMS.Model.Common;

namespace DCI.HRMS.Model.Welfare
{
  public  class MedicalAllowanceInfo
    {
      private string docNo;
      private string emCode;
      private DateTime trDate;
      private DateTime rqDate;
      private string symptom;
      private string relation="";
      private string relationType = "";


      private string patienType;
      private string patienName;
      private string hospital;
      private string district;
      private string province;
     
      private double amount;
      private ObjectInfo inform = new ObjectInfo();

      public MedicalAllowanceInfo()
      {
      }
      public string DocNo
      {
          set { docNo = value; }
          get { return docNo; }
      }
      public string EmCode
      {
          set { emCode = value; }
          get { return emCode; }
      }
      public DateTime TrDate
      {
          set { trDate = value; }
          get { 
                if (trDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || trDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return trDate;
                }
            }
      }
      public DateTime RqDate
      {
          set { rqDate = value; }
          get {
                if (rqDate <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || rqDate <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return rqDate;
                }
            }
      }
      public string Symptom
      {
          set { symptom = value; }
          get { return symptom; }
      }
      public string PatienType
      {
          set { patienType = value; }
          get { return patienType; }
      }
      public string PatienName
      {
          set { patienName = value; }
          get { return patienName; }
      }
      public string Hospital
      {
          set { hospital = value; }
          get { return hospital; }
      }
      public string District
      {
          get { return district; }
          set { district = value; }
      }


      public string Province
      {
          get { return province; }
          set { province = value; }
      }
      public string Relation
      {
          set { relation = value; }
          get { return relation; }
      }
      public string RelationType
      {
          get { return relationType; }
          set { relationType = value; }
      }
      public double Amount
      {
          set { amount = value; }
          get { return amount; }
      }
      public string CreateBy
      {
          get { return inform.CreateBy; }
      }
      public DateTime CreateDateTime
      {
          get { 
                if (inform.CreateDateTime <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || inform.CreateDateTime <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return inform.CreateDateTime;
                }
            }
      }
      public string LastUpdateBy
      {
          get { return inform.LastUpdateBy; }
      }
      public DateTime LastUpDateDateTime
      {
          get { 
                if (inform.LastUpdateDateTime <= DateTime.Parse("01/01/1900", new CultureInfo("en-US")) || inform.LastUpdateDateTime <= DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return inform.LastUpdateDateTime;
                }
            }
      }
      public ObjectInfo Inform
      {

          set { this.inform = value; }
      }




    }
}
