using System;
using System.Collections.Generic;
using System.Text;
using PCUOnline.Dao;
using DCI.HRMS.Model.Evaluation;
using System.Collections;
using Oracle.ManagedDataAccess.Client;
using PCUOnline.Dao.Ora;
using System.Data;

namespace DCI.HRMS.Persistence.Oracle
{
    public class OraEvaluationDao : DaoBase, IEvaluationDao
    {

        private const string SP_Salary_SELECT = "PKG_eva.sp_Salary_select";
        private const string SP_Salary_STORE = "PKG_eva.sp_Salary_STORE";
        private const string SP_Bonus_SELECT = "PKG_eva.sp_bonus_select";
        private const string SP_Bonus_STORE = "PKG_eva.sp_bonus_store";


        private const string PARAM_Act = "p_action";
        private const string PARAM_Code = "p_CODE";
        private const string PARAM_Year = "p_YEAR";
        private const string PARAM_BasicBonus = "p_BASIC_BONUS";
        private const string PARAM_Late = "p_LATE";
        private const string PARAM_Sick = "p_SICK"; 
        private const string PARAM_Pers = "p_pers";
        private const string PARAM_Abse = "p_ABSE";
        private const string PARAM_Mate = "p_MATE";
        private const string PARAM_Mili = "p_MILI";
        private const string PARAM_Prie = "p_prie";
        private const string PARAM_Marr = "p_MARR";
        private const string PARAM_Fune = "P_FUNE";
        private const string PARAM_Vobal = "p_PENAL_VAL";
        private const string PARAM_Letter = "p_PENAL_LET";
        private const string PARAM_Susspension = "p_PENALTY";
        private const string PARAM_NetBonus = "p_NET_BONUS";
        private const string PARAM_Grade = "p_GRADE";
        private const string PARAM_WorkDay = "p_WORKDAY";
        private const string PARAM_NewSalary= "p_new_sal";
        private const string PARAM_LateTime = "p_LATE_TIME";
        private const string PARAM_SickTime = "p_SICK_TIME";
        private const string PARAM_PersTime = "p_PERS_TIME";
        private const string PARAM_AbseTime = "p_ABSE_TIME";
        private const string PARAM_MateTime = "p_MATE_TIME";
        private const string PARAM_MiliTime = "p_MILI_TIME";
        private const string PARAM_PrieTime = "p_PRIE_TIME";
        private const string PARAM_MarrTime = "p_MARR_TIME";
        private const string PARAM_FuneTime = "p_FUNE_TIME";
        private const string PARAM_VobalTime = "p_PENAL_VAL_TIME";
        private const string PARAM_LetterTime = "p_PENAL_LET_TIME";
        private const string PARAM_SuspensionTime = "p_PENALTY_TIME";
        private const string PARAM_NetDeduct = "p_NET_DEDUCT";
        private const string PARAM_Train = "p_TRAIN";
        private const string PARAM_TrainTime = "p_TRAIN_TIME";
        private const string PARAM_SpecialMoney = "p_Special_money";
        private const string PARAM_Prize = "p_Prize";
        public OraEvaluationDao(DaoManager daoManager)
            : base(daoManager)
        {
        }

        public override object QueryForObject(System.Data.DataRow row, Type t)
        {
            if (t == typeof(Eva_BonusInfo))
            {
                Eva_BonusInfo item = new Eva_BonusInfo();


                try
                {
                    item.Code = Convert.ToString(this.Parse(row, "code"));
                }
                catch { }

                try
                {
                    item.Year = Convert.ToString(this.Parse(row, "byear"));
                }
                catch { }


                try
                {
                    item.BasicBonus = Convert.ToDecimal(this.Parse(row, "BASIC_BONUS"));
                }
                catch { }
                try
                {
                    item.Late = Convert.ToDecimal(this.Parse(row, "LATE"));
                }
                catch { }
                try
                {
                    item.Sick = Convert.ToDecimal(this.Parse(row, "SICK"));
                }
                catch { }
                try
                {
                    item.Pers = Convert.ToDecimal(this.Parse(row, "PERS"));
                }
                catch { }
                try
                {
                    item.Abse = Convert.ToDecimal(this.Parse(row, "ABSE"));
                }
                catch { }
                try
                {
                    item.Mate = Convert.ToDecimal(this.Parse(row, "MATE"));
                }
                catch { }
                try
                {
                    item.Mili = Convert.ToDecimal(this.Parse(row, "MILI"));
                }
                catch { }
                try
                {
                    item.Prie = Convert.ToDecimal(this.Parse(row, "PRIE"));
                }
                catch { }
                try
                {
                    item.Marr = Convert.ToDecimal(this.Parse(row, "MARR"));
                }
                catch { }
                try
                {
                    item.Fune = Convert.ToDecimal(this.Parse(row, "FUNE"));
                }
                catch { }
                try
                {
                    item.Vobal = Convert.ToDecimal(this.Parse(row, "PENAL_VAL"));
                }
                catch { }
                try
                {
                    item.Letter = Convert.ToDecimal(this.Parse(row, "PENAL_LET"));
                }
                catch { }
                try
                {
                    item.Suspension = Convert.ToDecimal(this.Parse(row, "PENALTY"));
                }
                catch { }
                try
                {
                    item.NetBonus = Convert.ToDecimal(this.Parse(row, "NET_BONUS"));
                }
                catch { }
                try
                {
                    item.Grade = Convert.ToString(this.Parse(row, "GRADE"));
                }
                catch { }
                try
                {
                    item.WorkDay = Convert.ToDecimal(this.Parse(row, "WORKDAY"));
                }
                catch { }
                try
                {
                    item.NewSalary = Convert.ToDecimal(this.Parse(row, "NEW_SAL"));
                }
                catch { }
                try
                {
                    item.LateTime = Convert.ToDecimal(this.Parse(row, "LATE_TIME"));
                }
                catch { }
                try
                {
                    item.SickTime = Convert.ToDecimal(this.Parse(row, "SICK_TIME"));
                }
                catch { }
                try
                {
                    item.PersTime = Convert.ToDecimal(this.Parse(row, "PERS_TIME"));
                }
                catch { }
                try
                {
                    item.AbseTime = Convert.ToDecimal(this.Parse(row, "ABSE_TIME"));
                }
                catch { }
                try
                {
                    item.MateTime = Convert.ToDecimal(this.Parse(row, "MATE_TIME"));
                }
                catch { }
                try
                {
                    item.MiliTime = Convert.ToDecimal(this.Parse(row, "MILI_TIME"));
                }
                catch { }
                try
                {
                    item.PrieTime = Convert.ToDecimal(this.Parse(row, "PRIE_TIME"));
                }
                catch { }
                try
                {
                    item.MarrTime = Convert.ToDecimal(this.Parse(row, "MARR_TIME"));
                }
                catch { }
                try
                {
                    item.FuneTime = Convert.ToDecimal(this.Parse(row, "FUNE_TIME"));
                }
                catch { }
                try
                {
                    item.VobalTime = Convert.ToDecimal(this.Parse(row, "PENAL_VAL_TIME"));
                }
                catch { }
                try
                {
                    item.LetterTime = Convert.ToDecimal(this.Parse(row, "PENAL_LET_TIME"));
                }
                catch { }
                try
                {
                    item.SuspensionTime = Convert.ToDecimal(this.Parse(row, "PENALTY_TIME"));
                }
                catch { }
                try
                {
                    item.NetDeduct = Convert.ToDecimal(this.Parse(row, "NET_DEDUCT"));
                }
                catch { }
                try
                {
                    item.Train = Convert.ToDecimal(this.Parse(row, "TRAIN"));
                }
                catch { }
                try
                {
                    item.TrainTime = Convert.ToDecimal(this.Parse(row, "TRAIN_TIME"));
                }
                catch { }
                try
                {
                    item.SpecailMoney = Convert.ToDecimal(this.Parse(row, "Special_money"));
                }
                catch { }
                try
                {
                    item.Prize = Convert.ToDecimal(this.Parse(row, "prize"));
                }
                catch { }
                return item;

            }
            if (t == typeof(Eva_SalaryInfo))
            {
                Eva_SalaryInfo item = new Eva_SalaryInfo();
                try
                {
                    item.Code = Convert.ToString(this.Parse(row, "code"));
                }
                catch { }

                try
                {
                    item.Year = Convert.ToString(this.Parse(row, "year"));
                }
                catch { }

                try
                {
                    item.PercentUp = Convert.ToDecimal(this.Parse(row, "PERCENTUP"));
                }
                catch
                { }
                try
                {
                    item.SalaryGrade = Convert.ToString(this.Parse(row, "SALARY_GRADE"));
                }
                catch
                { }
                try
                {
                    item.Salary = Convert.ToDecimal(this.Parse(row, "SALARY"));
                }
                catch
                { }
                try
                {
                    item.Wage = Convert.ToDecimal(this.Parse(row, "WAGE"));
                }
                catch
                { }
                return item;
            }
            return null;
        }

        public override void AddParameters(System.Data.IDbCommand cmd, object obj)
        {
            OracleCommand oraCmd = (OracleCommand)cmd;
            if (obj is Eva_BonusInfo)
            {
                Eva_BonusInfo item = (Eva_BonusInfo)obj;
                oraCmd.Parameters.Add(PARAM_Code, OracleDbType.Varchar2).Value = item.Code;
                oraCmd.Parameters.Add(PARAM_Year, OracleDbType.Varchar2).Value = item.Year;
                oraCmd.Parameters.Add(PARAM_BasicBonus, OracleDbType.Decimal).Value = item.BasicBonus;
                oraCmd.Parameters.Add(PARAM_Late, OracleDbType.Decimal).Value = item.Late;
                oraCmd.Parameters.Add(PARAM_Sick, OracleDbType.Decimal).Value = item.Sick;
                oraCmd.Parameters.Add(PARAM_Pers, OracleDbType.Decimal).Value = item.Pers;
                oraCmd.Parameters.Add(PARAM_Abse, OracleDbType.Decimal).Value = item.Abse;
                oraCmd.Parameters.Add(PARAM_Mate, OracleDbType.Decimal).Value = item.Mate;
                oraCmd.Parameters.Add(PARAM_Mili, OracleDbType.Decimal).Value = item.Mili;
                oraCmd.Parameters.Add(PARAM_Prie, OracleDbType.Decimal).Value = item.Prie;
                oraCmd.Parameters.Add(PARAM_Marr, OracleDbType.Decimal).Value = item.Marr;
                oraCmd.Parameters.Add(PARAM_Fune, OracleDbType.Decimal).Value = item.Fune;
                oraCmd.Parameters.Add(PARAM_Vobal, OracleDbType.Decimal).Value = item.Vobal;
                oraCmd.Parameters.Add(PARAM_Letter, OracleDbType.Decimal).Value = item.Letter;
                oraCmd.Parameters.Add(PARAM_Susspension, OracleDbType.Decimal).Value = item.Suspension;
                oraCmd.Parameters.Add(PARAM_NetBonus, OracleDbType.Decimal).Value = item.NetBonus;
                oraCmd.Parameters.Add(PARAM_Grade, OracleDbType.Char).Value = item.Grade;
                oraCmd.Parameters.Add(PARAM_WorkDay, OracleDbType.Decimal).Value = item.WorkDay;
                oraCmd.Parameters.Add(PARAM_NewSalary, OracleDbType.Decimal).Value = item.NewSalary;
                oraCmd.Parameters.Add(PARAM_LateTime, OracleDbType.Decimal).Value = item.LateTime;
                oraCmd.Parameters.Add(PARAM_SickTime, OracleDbType.Decimal).Value = item.SickTime;
                oraCmd.Parameters.Add(PARAM_PersTime, OracleDbType.Decimal).Value = item.PersTime;
                oraCmd.Parameters.Add(PARAM_AbseTime, OracleDbType.Decimal).Value = item.AbseTime;
                oraCmd.Parameters.Add(PARAM_MateTime, OracleDbType.Decimal).Value = item.MateTime;
                oraCmd.Parameters.Add(PARAM_MiliTime, OracleDbType.Decimal).Value = item.MiliTime;
                oraCmd.Parameters.Add(PARAM_PrieTime, OracleDbType.Decimal).Value = item.PrieTime;
                oraCmd.Parameters.Add(PARAM_MarrTime, OracleDbType.Decimal).Value = item.MarrTime;
                oraCmd.Parameters.Add(PARAM_FuneTime, OracleDbType.Decimal).Value = item.FuneTime;
                oraCmd.Parameters.Add(PARAM_VobalTime, OracleDbType.Decimal).Value = item.VobalTime;
                oraCmd.Parameters.Add(PARAM_LetterTime, OracleDbType.Decimal).Value = item.LetterTime;
                oraCmd.Parameters.Add(PARAM_SuspensionTime, OracleDbType.Decimal).Value = item.SuspensionTime;
                oraCmd.Parameters.Add(PARAM_NetDeduct, OracleDbType.Decimal).Value = item.NetDeduct;
                oraCmd.Parameters.Add(PARAM_Train, OracleDbType.Decimal).Value = item.Train;
                oraCmd.Parameters.Add(PARAM_TrainTime, OracleDbType.Decimal).Value = item.TrainTime;
                oraCmd.Parameters.Add(PARAM_SpecialMoney, OracleDbType.Decimal).Value = item.SpecailMoney;
                oraCmd.Parameters.Add(PARAM_Prize, OracleDbType.Decimal).Value = item.Prize;
            }
            if (obj is Eva_SalaryInfo)
            {
                Eva_SalaryInfo item = (Eva_SalaryInfo)obj;
                oraCmd.Parameters.Add(PARAM_Code, OracleDbType.Varchar2).Value = item.Code;
                oraCmd.Parameters.Add(PARAM_Year, OracleDbType.Varchar2).Value = item.Year;
                oraCmd.Parameters.Add("p_salary_Grade", OracleDbType.Char).Value = item.SalaryGrade;
                oraCmd.Parameters.Add("p_salayr", OracleDbType.Decimal).Value = item.Salary;
                oraCmd.Parameters.Add("p_WAGE ", OracleDbType.Decimal).Value = item.Wage;
                oraCmd.Parameters.Add("p_percentup", OracleDbType.Decimal).Value = item.PercentUp;

            }

        }
        #region IEvaluationDao Members

        public Eva_SalaryInfo GetSalaryInfoUnq(string code, string year)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_Salary_SELECT, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Code, OracleDbType.Varchar2).Value = code;
            cmd.Parameters.Add(PARAM_Year, OracleDbType.Varchar2).Value = year;

            return (Eva_SalaryInfo)OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(Eva_SalaryInfo));
        }

        public ArrayList GetSalaryInfo(string code, string year)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_Salary_SELECT, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Code, OracleDbType.Varchar2).Value = code;
            cmd.Parameters.Add(PARAM_Year, OracleDbType.Varchar2).Value = year;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(Eva_SalaryInfo));
        
        }

        public Eva_BonusInfo GetBonusInfoUnq(string code, string year)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_Bonus_SELECT, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Code, OracleDbType.Varchar2).Value = code;
            cmd.Parameters.Add(PARAM_Year, OracleDbType.Varchar2).Value = year;

            return (Eva_BonusInfo)OraHelper.ExecuteQuery(this, this.Transaction, cmd, typeof(Eva_BonusInfo));
        }

        public ArrayList GetBonusInfo(string code, string year)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_Bonus_SELECT, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Code, OracleDbType.Varchar2).Value = code;
            cmd.Parameters.Add(PARAM_Year, OracleDbType.Varchar2).Value = year;

            return OraHelper.ExecuteQueries(this, this.Transaction, cmd, typeof(Eva_BonusInfo));

        }

        public void SaveSalaryInfo(Eva_SalaryInfo salaryInfo)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_Salary_STORE, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Act, OracleDbType.Varchar2).Value = "ADD";

            AddParameters(cmd, salaryInfo);
           
            OraHelper.ExecuteNonQuery(this.Transaction, cmd);


        }

        public void UpdateSalaryInfo(Eva_SalaryInfo salaryInfo)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_Salary_STORE, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Act, OracleDbType.Varchar2).Value = "Update";

            AddParameters(cmd, salaryInfo);

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void SaveBonusInfo(Eva_BonusInfo bonusInfo)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_Bonus_STORE, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Act, OracleDbType.Varchar2).Value = "ADD";

            AddParameters(cmd, bonusInfo);

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        public void UpdateBonusInfo(Eva_BonusInfo bonusInfo)
        {
            OracleCommand cmd = OraHelper.CreateCommand(SP_Bonus_STORE, CommandType.StoredProcedure);
            cmd.Parameters.Add(PARAM_Act, OracleDbType.Varchar2).Value = "Update";

            AddParameters(cmd, bonusInfo);

            OraHelper.ExecuteNonQuery(this.Transaction, cmd);
        }

        #endregion
    }
}
