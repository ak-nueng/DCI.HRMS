
using ComponentFactory.Krypton.Toolkit;
using DCI.HRMS.Welfare;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Reflection.Emit;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Windows.Forms;


namespace DCI.HRMS.PayRoll
{
    public partial class Frm_PayrollCalculate : Form
    {
        public Frm_PayrollCalculate()
        {
            InitializeComponent();
        }


        bool _PayrollStart = false;
        DateTime _CalPayrollDate = new DateTime(1900,1,1);
        DateTime _CalPayrollDateST = new DateTime(1900,1,1);
        DateTime _CalPayrollDateEN = new DateTime(1900,1,1);

        ClsOraConnectDB oOra = new ClsOraConnectDB("DCI");


        private void Frm_PayrollCalculate_Load(object sender, EventArgs e)
        {
            dpkrPayroll_ValueChanged(sender, e);
            
            

            _PayrollStart = true;
            
            
        }

        private void btnPayrollGenOT_Click(object sender, EventArgs e)
        {
            _PayrollStart = true;
            bool isError = false;
            string msgError = "";

            OracleTransaction Trans = oOra.BeginTrans();


            //******
            try
            {
                // set default value
                string strUpPRAJ = @"UPDATE PRAJ SET LOT1=0,LOT15=0,LOT2=0,LOT3=0,COT1=0,COT15=0,COT2=0,COT3=0 
                                     WHERE ADJ='0' AND PDATE='"+ _CalPayrollDate.ToString("dd/MMM/yyyy") + @"' 
                                     AND CODE IN ('40777','40865','40850','40895')  ";
                OracleCommand cmdUpPRAJ = new OracleCommand();
                cmdUpPRAJ.CommandText = strUpPRAJ;
                oOra.ExecuteCommand(cmdUpPRAJ);


                // get all employee
                DataTable dtEmp = new DataTable();
                string strEmp = @"SELECT code,wtype FROM EMPM 
                                  WHERE (resign is null or resign > '" + _CalPayrollDateST.ToString("dd/MMM/yyyy") + @"' ) 
                                    AND join<='" + _CalPayrollDateEN.ToString("dd/MMM/yyyy") + @"' 
                                    AND CODE IN ('40777','40865','40850','40895')
                                  ORDER BY code ";
                OracleCommand cmdEmp = new OracleCommand();
                cmdEmp.CommandText = strEmp;
                dtEmp = oOra.Query(cmdEmp);

                if(dtEmp.Rows.Count > 0)
                {
                    foreach(DataRow drEmp in dtEmp.Rows)
                    {

                        decimal lt1 = 0;
                        decimal lt15 = 0;
                        decimal lt2 = 0;
                        decimal lt3 = 0;
                        decimal ct1 = 0;
                        decimal ct15 = 0;
                        decimal ct2 = 0;
                        decimal ct3 = 0;


                        DataTable dtEmpOT = new DataTable();
                        string strEmpOT = @"SELECT CASE  WHEN ODATE < '01/JUN/2023' THEN 'L' ELSE 'C' END POT, 
                                SUM(texttohour(OT1)) OT1, SUM(texttohour(OT15)) OT15, 
                                SUM(texttohour(OT2)) OT2, SUM(texttohour(OT3)) OT3
                            FROM OTRQ 
                            WHERE CODE = '" + drEmp["code"].ToString() + @"'
                                    AND ODATE >= '" + _CalPayrollDateST.ToString("dd/MMM/yyyy") + @"'  
                                    AND ODATE <= '" + _CalPayrollDateEN.ToString("dd/MMM/yyyy") + @"'
                            GROUP BY CASE  WHEN ODATE < '01/JUN/2023' THEN 'L' ELSE 'C' END    ";
                        OracleCommand cmdEmpOT = new OracleCommand();
                        cmdEmpOT.CommandText = strEmpOT;
                        dtEmpOT = oOra.Query(cmdEmpOT);

                        if(dtEmpOT.Rows.Count > 0)
                        {
                            foreach (DataRow drOT in dtEmpOT.Rows)
                            {
                                if (drOT["POT"].ToString() == "L")
                                {
                                    lt1 = lt1 + Convert.ToDecimal(drOT["OT1"].ToString());
                                    lt15 = lt15 + Convert.ToDecimal(drOT["OT15"].ToString());
                                    lt2 = lt2 + Convert.ToDecimal(drOT["OT2"].ToString());
                                    lt3 = lt3 + Convert.ToDecimal(drOT["OT3"].ToString());
                                }
                                else
                                {
                                    ct1 = ct1 + Convert.ToDecimal(drOT["OT1"].ToString());
                                    ct15 = ct15 + Convert.ToDecimal(drOT["OT15"].ToString());
                                    ct2 = ct2 + Convert.ToDecimal(drOT["OT2"].ToString());
                                    ct3 = ct3 + Convert.ToDecimal(drOT["OT3"].ToString());
                                }

                            }// end foreach OT
                        } // end if OT



                        DataTable dtEmpPRAJ = new DataTable();
                        string strEmpPRAJ = @"SELECT *  FROM PRAJ 
                                  WHERE PDATE = '" + _CalPayrollDate.ToString("dd/MMM/yyyy") + @"' 
                                    AND ADJ = '0'
                                    AND WTYPE ='" + drEmp["wType"].ToString() + @"' 
                                    AND CODE ='" + drEmp["code"].ToString() + @"' ";
                        OracleCommand cmdEmpPRAJ = new OracleCommand();
                        cmdEmpPRAJ.CommandText = strEmpPRAJ;
                        dtEmpPRAJ = oOra.Query(cmdEmpPRAJ);

                        if(dtEmpPRAJ.Rows.Count == 0)
                        {
                            //***** INSERT ******
                            string strInstr = @"INSERT INTO PRAJ (PDATE, WTYPE, CODE, ADJ, LOT1, LOT15, LOT2, 
                                        LOT3, COT1, COT15, COT2, COT3) VALUES 
                                        ('" + _CalPayrollDate.ToString("dd/MMM/yyyy") + @"', :WTYPE, :CODE, :ADJ, 
                                            TO_NUMBER(:LOT1), TO_NUMBER(:LOT15), TO_NUMBER(:LOT2), TO_NUMBER(:LOT3), 
                                            TO_NUMBER(:COT1), TO_NUMBER(:COT15), TO_NUMBER(:COT2), TO_NUMBER(:COT3) ) ";
                            OracleCommand cmdInstr = new OracleCommand();
                            cmdInstr.CommandText = strInstr;
                            cmdInstr.Parameters.Add(new OracleParameter(":WTYPE", drEmp["wType"].ToString()));
                            cmdInstr.Parameters.Add(new OracleParameter(":CODE", drEmp["code"].ToString()));
                            cmdInstr.Parameters.Add(new OracleParameter(":ADJ", "0" ));
                            cmdInstr.Parameters.Add(new OracleParameter(":LOT1", lt1.ToString() ));
                            cmdInstr.Parameters.Add(new OracleParameter(":LOT15", lt15.ToString()));
                            cmdInstr.Parameters.Add(new OracleParameter(":LOT2", lt2.ToString()));
                            cmdInstr.Parameters.Add(new OracleParameter(":LOT3", lt3.ToString()));
                            cmdInstr.Parameters.Add(new OracleParameter(":COT1", ct1.ToString()));
                            cmdInstr.Parameters.Add(new OracleParameter(":COT15", ct15.ToString()));
                            cmdInstr.Parameters.Add(new OracleParameter(":COT2", ct2.ToString()));
                            cmdInstr.Parameters.Add(new OracleParameter(":COT3", ct3.ToString()));
                            oOra.ExecuteCommand(cmdInstr);

                        }
                        else
                        {
                            //***** UPDATE ******
                            //string strUpd = @"UPDATE PRAJ SET LOT1=TO_NUMBER(:LOT1), LOT15=TO_NUMBER(:LOT15), 
                            //                    LOT2=TO_NUMBER(:LOT2), LOT3=TO_NUMBER(:LOT3), COT1=TO_NUMBER(:COT1), 
                            //                    COT15=TO_NUMBER(:COT15), COT2=TO_NUMBER(:COT2), COT3=TO_NUMBER(:COT3) 
                            //        WHERE PDATE = '" + _CalPayrollDate.ToString("dd/MMM/yyyy") + @"'
                            //            AND WTYPE = :WTYPE
                            //            AND CODE = :CODE  AND ADJ = '0'  ";


                            string strUpd = @"UPDATE PRAJ SET LOT1=TO_NUMBER('"+ lt1.ToString() + @"'), 
                                                LOT15=TO_NUMBER('"+ lt15.ToString() + @"'), 
                                                LOT2=TO_NUMBER('"+ lt2.ToString() + @"'), 
                                                LOT3=TO_NUMBER('"+ lt3.ToString() + @"'), 
                                                COT1=TO_NUMBER('"+ ct1.ToString() + @"'), 
                                                COT15=TO_NUMBER('"+ ct15.ToString() + @"'), 
                                                COT2=TO_NUMBER('"+ ct2.ToString() + @"'), 
                                                COT3=TO_NUMBER('"+ ct3.ToString() + @"') 
                                    WHERE PDATE = '" + _CalPayrollDate.ToString("dd/MMM/yyyy") + @"'
                                        AND WTYPE = '"+ drEmp["wType"].ToString() + @"'
                                        AND CODE = '"+ drEmp["code"].ToString() + @"'  AND ADJ = '0'  ";


                            OracleCommand cmdUpd = new OracleCommand();
                            cmdUpd.CommandText = strUpd;
                            //cmdUpd.Parameters.Add(new OracleParameter("WTYPE", drEmp["wType"].ToString()));
                            //cmdUpd.Parameters.Add(new OracleParameter("CODE", drEmp["code"].ToString()));
                            //cmdUpd.Parameters.Add(new OracleParameter("LOT1", lt1.ToString()));
                            //cmdUpd.Parameters.Add(new OracleParameter("LOT15", lt15.ToString()));
                            //cmdUpd.Parameters.Add(new OracleParameter("LOT2", lt2.ToString()));
                            //cmdUpd.Parameters.Add(new OracleParameter("LOT3", lt3.ToString()));
                            //cmdUpd.Parameters.Add(new OracleParameter("COT1", ct1.ToString()));
                            //cmdUpd.Parameters.Add(new OracleParameter("COT15", ct15.ToString()));
                            //cmdUpd.Parameters.Add(new OracleParameter("COT2", ct2.ToString()));
                            //cmdUpd.Parameters.Add(new OracleParameter("COT3", ct3.ToString()));
                            oOra.ExecuteCommand(cmdUpd);

                        }


                    } // end foreach


                    // Commit Transaction
                    Trans.Commit();


                } // end if
            }
            catch (Exception ex)
            {
                msgError = ex.Message.ToString();

                // Roll Back Transaction
                Trans.Rollback();
            }
            finally
            {
                if (!isError)
                {
                    MessageBox.Show("Completed");
                }
                else
                {
                    MessageBox.Show(msgError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Trans.Dispose();
            }

        }

        private void btnPayrollGenWork_Click(object sender, EventArgs e)
        {
            _PayrollStart = true;
            bool isError = false;
            string msgError = "";

            OracleTransaction Trans = oOra.BeginTrans();

            try {
                //**** Set Default Value *****
                string strUpPRAJ = @"UPDATE PRAJ SET ABDAY=0,WORK1=0,WORK2=0  
                                     WHERE ADJ='0' AND PDATE='" + _CalPayrollDate.ToString("dd/MMM/yyyy") + @"'   ";
                OracleCommand cmdUpPRAJ = new OracleCommand();
                cmdUpPRAJ.CommandText = strUpPRAJ;
                oOra.ExecuteCommand(cmdUpPRAJ);


                //**** Get All Employee *****
                DataTable dtEmp = new DataTable();
                string strEmp = @"SELECT code, wtype, join, resign 
                                  FROM EMPM 
                                  WHERE (resign is null or resign > '" + _CalPayrollDateST.ToString("dd/MMM/yyyy") + @"' ) 
                                    AND join<='" + _CalPayrollDateEN.ToString("dd/MMM/yyyy") + @"' 
                                  ORDER BY code ";
                OracleCommand cmdEmp = new OracleCommand();
                cmdEmp.CommandText = strEmp;
                dtEmp = oOra.Query(cmdEmp);


                if (dtEmp.Rows.Count > 0)
                {
                    foreach (DataRow drEmp in dtEmp.Rows)
                    {

                        decimal ABDAY = 0;
                        decimal WORK1 = 0;
                        decimal WORK2 = 0;


                        if (drEmp["wtype"].ToString().ToUpper() == "S")
                        {
                            #region  Salary 

                            DataTable dtLVRQ = new DataTable();
                            string strLVRQ = @"SELECT NVL(SUM(total),0) as sumtotal 
                                        FROM LVRQ 
                                        WHERE  salsts='N' AND code='" + drEmp["code"].ToString() + @"' 
                                            AND cdate >= '" + _CalPayrollDateST.ToString("dd/MMM/yyyy") + @"' 
                                            AND cdate<='" + _CalPayrollDateEN.ToString("dd/MMM/yyyy") + @"' 
                                        ORDER BY code";
                            OracleCommand cmdLVRQ = new OracleCommand();
                            cmdLVRQ.CommandText = strLVRQ;
                            dtLVRQ = oOra.Query(cmdLVRQ);

                            if (Convert.ToDecimal(dtLVRQ.Rows[0]["sumtotal"].ToString()) > 0)
                            {
                                ABDAY = Convert.ToDecimal(dtLVRQ.Rows[0]["sumtotal"].ToString()) / 525;
                            }

                            DataTable dtPENA = new DataTable();
                            string strPENA = @"SELECT pdate,type,TDATE, NVL((P.TDATE - P.PDATE)+1),0) AS ABDay  
                                        FROM PENA 
                                        WHERE TYPE='SUSP' AND code='" + drEmp["code"].ToString() + @"' 
                                            AND pdate >= '" + _CalPayrollDateST.ToString("dd/MMM/yyyy") + @"' 
                                            AND pdate<='" + _CalPayrollDateEN.ToString("dd/MMM/yyyy") + @"' 
                                        ORDER BY code,pdate ";
                            OracleCommand cmdPENA = new OracleCommand();
                            cmdPENA.CommandText = strPENA;
                            dtPENA = oOra.Query(cmdPENA);

                            if (dtPENA.Rows.Count > 0)
                            {
                                foreach (DataRow drPENA in dtPENA.Rows)
                                {
                                    ABDAY = ABDAY + Convert.ToDecimal(drPENA["ABDay"].ToString());
                                }// end foreach
                            } // end if

                            #endregion

                        }
                        else if (drEmp["wtype"].ToString().ToUpper() == "O")
                        {
                            #region  Daily Operation

                            DateTime _Join = Convert.ToDateTime(drEmp["join"].ToString());
                            DateTime _Resign = Convert.ToDateTime(drEmp["resign"].ToString());

                            DateTime _BDate = new DateTime(1900, 1, 1);
                            DateTime _EDate = new DateTime(1900, 1, 1);

                            if (_CalPayrollDateST < _Join)
                            {
                                _BDate = _Join;
                            }
                            else
                            {
                                _BDate = _CalPayrollDateST;
                            }


                            if (_Resign.Year > 2000)
                            {
                                if (_Resign > _CalPayrollDateEN)
                                {
                                    _EDate = _CalPayrollDateEN;
                                }
                                else
                                {
                                    _EDate = _Resign;
                                }
                            }
                            else
                            {
                                _EDate = _CalPayrollDateEN;
                            }


                            //************************************
                            //      WORK 1
                            //************************************
                            DateTime _LoopDate = _BDate;
                            while (_LoopDate < _CalPayrollDate)
                            {
                                WORK1 = WORK1 + getWorkDay(drEmp["CODE"].ToString(), _LoopDate, _EDate);

                                _LoopDate = _LoopDate.AddDays(1);
                            }
                            //************************************

                            //************************************
                            //      WORK 2
                            //************************************
                            if(_BDate >= _CalPayrollDate)
                            {
                                _LoopDate = _BDate;
                            }
                            else
                            {
                                _LoopDate = _CalPayrollDate;
                            }
                            while (_LoopDate < _EDate && _EDate >= _CalPayrollDate)
                            {
                                WORK1 = WORK1 + getWorkDay(drEmp["CODE"].ToString(), _LoopDate, _EDate);
                                _LoopDate = _LoopDate.AddDays(1);
                            }
                            //************************************
                            #endregion
                        } // end if


                        //************************************
                        // UPDATE ADJ
                        //************************************

                        string strUpd = @"UPDATE PRAJ SET ABDAY=TO_NUMBER('" + ABDAY.ToString() + @"'), 
                                                work1=TO_NUMBER('" + WORK1.ToString() + @"'), 
                                                work2=TO_NUMBER('" + WORK2.ToString() + @"')                                                 
                                    WHERE PDATE = '" + _CalPayrollDate.ToString("dd/MMM/yyyy") + @"'
                                        AND WTYPE = '" + drEmp["wType"].ToString() + @"'
                                        AND CODE = '" + drEmp["code"].ToString() + @"'  AND ADJ = '0'  ";
                        OracleCommand cmdUpd = new OracleCommand();
                        cmdUpd.CommandText = strUpd;
                        oOra.ExecuteCommand(cmdUpd);
                        
                    } // end foreach




                    // Commit Transaction
                    Trans.Commit();

                } // end if


            }
            catch (Exception ex)
            {
                msgError = ex.Message.ToString();

                // Roll Back Transaction
                Trans.Rollback();
            }
            finally
            {
                if (!isError)
                {
                    MessageBox.Show("Completed");
                }
                else
                {
                    MessageBox.Show(msgError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Trans.Dispose();
            }
        }


        // remain export Excel
        private void btnPayrollGenA400_Click(object sender, EventArgs e)
        {
            _PayrollStart = true;
            bool isError = false;
            string msgError = "";
            //int lastLevel = 0;
            //int curLevel = 0;
            int maxLevel = 7;


            OracleTransaction Trans = oOra.BeginTrans();

            try
            {

                // get Max Level A400
                DataTable dtDict = new DataTable();
                string strDict = @"SELECT  *  FROM DICT WHERE  type = 'A400'  and  item ='0' ";
                OracleCommand cmdDict = new OracleCommand();
                cmdDict.CommandText = strDict;
                dtDict = oOra.Query(cmdDict);
                maxLevel = Convert.ToInt32(dtDict.Rows[0]["TITEM"].ToString().Trim());


                // set default value
                string strUpPRAJ = @"UPDATE PRAJ SET a400=0, a400level=0  
                                     WHERE ADJ='0' AND PDATE='" + _CalPayrollDate.ToString("dd/MMM/yyyy") + @"'  ";
                OracleCommand cmdUpPRAJ = new OracleCommand();
                cmdUpPRAJ.CommandText = strUpPRAJ;
                oOra.ExecuteCommand(cmdUpPRAJ);


                // get all employee
                DataTable dtEmp = new DataTable();
                string strEmp = @"SELECT wtype,code,join,resign,wsts,PREN,NAME,SURN,POSIT ,sal04,gb04 
                                  FROM EMPM  WHERE wsts='E' AND code >'10000' AND posit IN ('OP','LE','OP.S','LE.S','DR') 
                                    AND (resign is null or resign > '" + _CalPayrollDateEN.ToString("dd/MMM/yyyy") + @"' ) 
                                  ORDER BY CODE  ";
                OracleCommand cmdEmp = new OracleCommand();
                cmdEmp.CommandText = strEmp;
                dtEmp = oOra.Query(cmdEmp);

                if(dtEmp.Rows.Count > 0)
                {
                    foreach (DataRow drEmp in dtEmp.Rows)
                    {
                        bool A400 = false, ZerolA400 = false, NotZero = false;
                        decimal LastA400 = 0, curA400 = 0;
                        int lastLevel = 0, curLevel = 0;


                        DateTime joinDate = new DateTime(1900,1,1);
                        joinDate = Convert.ToDateTime(drEmp["join"].ToString());

                        string LvType ="", LVdate = "";

                        if (joinDate <= _CalPayrollDateST)
                        {
                            //========================================================================
                            //        CHECK EMPLOYEE NOT LEAVE JSCK, ANNU, OTH, SPSL, CARE, FUNE
                            //========================================================================
                            DataTable dtLV = new DataTable();
                            string strLV = @"SELECT * FROM LVRQ WHERE code='" + drEmp["code"].ToString() + "' and cdate >= '" + _CalPayrollDateST.ToString("dd/MMM/yyyy") + "' and cdate <= '" + _CalPayrollDateEN.ToString("dd/MMM/yyyy") + "' and type<>'JSCK' and type<>'ANNU'  and type <> 'ANNN'   and  type <> 'OTH'  and  type <> 'SPSL'  and  type <> 'CARE' and  type <> 'FUNE' ORDER BY code,cdate ";
                            OracleCommand cmdLV = new OracleCommand();
                            cmdLV.CommandText = strLV;
                            dtLV = oOra.Query(cmdLV);

                            if(dtLV.Rows.Count == 0)
                            {
                                A400 = true;
                            }
                            else
                            {
                                LvType = dtLV.Rows[dtLV.Rows.Count - 1]["Type"].ToString();
                                LVdate = Convert.ToDateTime(dtLV.Rows[dtLV.Rows.Count - 1]["CDate"]).ToString("dd/MMM/yyyy");
                            }
                            //========================================================================
                            //       END CHECK EMPLOYEE NOT LEAVE JSCK, ANNU, OTH, SPSL, CARE, FUNE
                            //========================================================================

                            //==========================================================================
                            //          CHECK EMPLOYEE HAVE PENALTY
                            //==========================================================================
                            if (A400) {
                                DataTable dtPENA = new DataTable();
                                string strPENA = @"SELECT pdate,type FROM PENA WHERE code='" + drEmp["code"].ToString() + "' and pdate >= '" + _CalPayrollDateST.ToString("dd/MMM/yyyy") + "' and pdate <= '" + _CalPayrollDateEN.ToString("dd/MMM/yyyy") + "' AND TYPE='SUSP' ORDER BY code,pdate ";
                                OracleCommand cmdPENA = new OracleCommand();
                                cmdPENA.CommandText = strPENA;
                                dtPENA = oOra.Query(cmdPENA);

                                if (dtPENA.Rows.Count > 0) {
                                    foreach (DataRow drPENA in dtPENA.Rows) { 
                                        if(Convert.ToDateTime(drPENA["pDate"].ToString()) >= _CalPayrollDateST && Convert.ToDateTime(drPENA["pDate"].ToString()) <= _CalPayrollDateEN)
                                        {
                                            LvType = LvType + "," + drEmp["Type"].ToString();
                                            LVdate = LVdate + "," + Convert.ToDateTime(drEmp["pDate"]).ToString("dd/MMM/yyyy");
                                            A400 = false;
                                        }
                                    }
                                }
                            }
                            //========================================================================
                            //      END  CHECK EMPLOYEE HAVE PENALTY
                            //========================================================================


                            // if Check have A400
                            if (A400)
                            {
                                //***** Get Last Level & Set Next Level *********
                                lastLevel = CheckAndGetA400(drEmp["code"].ToString(), joinDate, _CalPayrollDate.AddMonths(-1), maxLevel);

                                if(lastLevel == 0)
                                {
                                    curLevel = 1;
                                }
                                else if(lastLevel < maxLevel && lastLevel > 0)
                                {
                                    curLevel = lastLevel + 1;
                                }
                                else
                                {
                                    curLevel = maxLevel;
                                }




                                //****** GET MONEY A400 By LEVEL *********
                                DataTable dtA400 = new DataTable();
                                string strA400 = @"SELECT * FROM DICT WHERE  type = 'A400'  and  item ='" + curLevel + "' ";
                                OracleCommand cmdA400 = new OracleCommand();
                                cmdA400.CommandText = strA400;
                                dtA400 = oOra.Query(cmdA400);
                                curA400 = Convert.ToDecimal(dtA400.Rows[0]["TITEM"].ToString());

                            } // end if




                            //************************************
                            // UPDATE ADJ
                            //************************************
                            string strUpd = @"UPDATE PRAJ SET A400=TO_NUMBER('" + curA400.ToString() + @"'), 
                                                A400LEVEL=TO_NUMBER('" + curLevel.ToString() + @"') 
                                    WHERE PDATE = '" + _CalPayrollDate.ToString("dd/MMM/yyyy") + @"'
                                        AND WTYPE = '" + drEmp["wType"].ToString() + @"'
                                        AND CODE = '" + drEmp["code"].ToString() + @"'  AND ADJ = '0'  ";
                            OracleCommand cmdUpd = new OracleCommand();
                            cmdUpd.CommandText = strUpd;
                            oOra.ExecuteCommand(cmdUpd);


                        } // end if check join



                    } // end foreach



                    //  Commit Transaction
                    Trans.Commit();
                } // end if





            }
            catch (Exception ex)
            {
                msgError = ex.Message.ToString();

                // Roll Back Transaction
                Trans.Rollback();
            }
            finally
            {
                if (!isError)
                {
                    MessageBox.Show("Completed");
                }
                else
                {
                    MessageBox.Show(msgError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Trans.Dispose();
            }
        }


        // remain export Excel
        private void btnPayrollGenOTFood_Click(object sender, EventArgs e)
        {
            _PayrollStart = true;
            bool isError = false;
            string msgError = "";
            decimal FoodCost = 0;


            OracleTransaction Trans = oOra.BeginTrans();

            try
            {
                // Get Food Cost
                DataTable dtDict = new DataTable();
                string strDict = @"SELECT  *  FROM DICT WHERE  type ='ALLO' and item = 'OTFO'  ";
                OracleCommand cmdDict = new OracleCommand();
                cmdDict.CommandText = strDict;
                dtDict = oOra.Query(cmdDict);
                FoodCost = Convert.ToDecimal(dtDict.Rows[0]["NOTE"].ToString().Trim());


                //**** Set Default Value *****
                string strUpPRAJ = @"UPDATE PRAJ SET FOODSHT=0 
                                     WHERE ADJ='0' AND PDATE='" + _CalPayrollDate.ToString("dd/MMM/yyyy") + @"'   ";
                OracleCommand cmdUpPRAJ = new OracleCommand();
                cmdUpPRAJ.CommandText = strUpPRAJ;
                oOra.ExecuteCommand(cmdUpPRAJ);



                //**** Get All Employee *****
                DataTable dtEmp = new DataTable();
                string strEmp = @"SELECT code, wtype, join, resign 
                                  FROM EMPM 
                                  WHERE (resign is null or resign > '" + _CalPayrollDateST.ToString("dd/MMM/yyyy") + @"' ) 
                                    AND join<='" + _CalPayrollDateEN.ToString("dd/MMM/yyyy") + @"' 
                                  ORDER BY code ";
                OracleCommand cmdEmp = new OracleCommand();
                cmdEmp.CommandText = strEmp;
                dtEmp = oOra.Query(cmdEmp);


                if (dtEmp.Rows.Count > 0)
                {
                    foreach (DataRow drEmp in dtEmp.Rows)
                    {
                        // Summary Food Cost
                        decimal SumFoodCost = 0;


                        //**** GET Employee OT *****
                        DataTable dtEmpOT = new DataTable();
                        string strEmpOT = @"SELECT OT1, OT15, OT2, OT3, DCI.texttoMinute(OT1) MIN_OT1, DCI.texttoMinute(OT15) MIN_OT15, DCI.texttoMinute(OT2) MIN_OT2, DCI.texttoMinute(OT3) MIN_OT3 
                                              FROM OTRQ WHERE odate >= '" + _CalPayrollDateST.ToString("dd/MMM/yyyy") + @"' and odate <= '" + _CalPayrollDateEN.ToString("dd/MMM/yyyy") + @"' ORDER BY CODE ";
                        OracleCommand cmdEmpOT = new OracleCommand();
                        cmdEmpOT.CommandText = strEmpOT;
                        dtEmpOT = oOra.Query(cmdEmpOT);

                        if (dtEmpOT.Rows.Count > 0)
                        {
                            foreach (DataRow drOT in dtEmpOT.Rows)
                            {
                                int _min_ot1 = Convert.ToInt32(drOT["MIN_OT1"].ToString());
                                int _min_ot15 = Convert.ToInt32(drOT["MIN_OT15"].ToString());
                                int _min_ot2 = Convert.ToInt32(drOT["MIN_OT2"].ToString());
                                int _min_ot3 = Convert.ToInt32(drOT["MIN_OT3"].ToString());
                                if (_min_ot15 >= 30 || _min_ot3 >= 30)
                                {
                                    SumFoodCost = SumFoodCost + FoodCost;
                                }                               
                            } // end foreach
                        } // end if



                        //************************************
                        //      UPDATE ADJ
                        //************************************
                        string strUpd = @"UPDATE PRAJ SET FOODSHT=TO_NUMBER('" + SumFoodCost.ToString() + @"') 
                                    WHERE PDATE = '" + _CalPayrollDate.ToString("dd/MMM/yyyy") + @"'
                                        AND WTYPE = '" + drEmp["wType"].ToString() + @"'
                                        AND CODE = '" + drEmp["code"].ToString() + @"'  AND ADJ = '0'  ";
                        OracleCommand cmdUpd = new OracleCommand();
                        cmdUpd.CommandText = strUpd;
                        oOra.ExecuteCommand(cmdUpd);


                    } // end foreach



                    // Commit Transaction
                    Trans.Commit();



                } // end if


            }
            catch (Exception ex)
            {
                msgError = ex.Message.ToString();

                // Roll Back Transaction
                Trans.Rollback();
            }
            finally
            {
                if (!isError)
                {
                    MessageBox.Show("Completed");
                }
                else
                {
                    MessageBox.Show(msgError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Trans.Dispose();
            }
        }


        // recheck result of GetTaffTime and if more than 5 hours count 1 day
        private void btnPayrollGenTranspot_Click(object sender, EventArgs e)
        {
            _PayrollStart = true;
            bool isError = false;
            string msgError = "";
            decimal TransCost = 0;

            OracleTransaction Trans = oOra.BeginTrans();

            try
            {
                // Get Transportation Cost
                DataTable dtDict = new DataTable();
                string strDict = @"SELECT  *  FROM DICT WHERE  type ='ALLO' and item = 'TRAN'  ";
                OracleCommand cmdDict = new OracleCommand();
                cmdDict.CommandText = strDict;
                dtDict = oOra.Query(cmdDict);
                TransCost = Convert.ToDecimal(dtDict.Rows[0]["NOTE"].ToString().Trim());




                //**** Set Default Value *****
                string strUpPRAJ = @"UPDATE PRAJ SET TRAN=0  
                                     WHERE ADJ='0' AND PDATE='" + _CalPayrollDate.ToString("dd/MMM/yyyy") + @"'   ";
                OracleCommand cmdUpPRAJ = new OracleCommand();
                cmdUpPRAJ.CommandText = strUpPRAJ;
                oOra.ExecuteCommand(cmdUpPRAJ);


                //**** Get All Employee *****
                DataTable dtEmp = new DataTable();
                string strEmp = @"SELECT wtype,code,join,resign,wsts,PREN,NAME,SURN,POSIT,ATBN04 
                                  FROM EMPM 
                                  WHERE (resign is null or resign > '" + _CalPayrollDateST.ToString("dd/MMM/yyyy") + @"' ) 
                                    AND join<='" + _CalPayrollDateEN.ToString("dd/MMM/yyyy") + @"' 
                                    AND CODE not like  '6%'  and  code not like '0%' 
                                  ORDER BY code ";
                OracleCommand cmdEmp = new OracleCommand();
                cmdEmp.CommandText = strEmp;
                dtEmp = oOra.Query(cmdEmp);


                if (dtEmp.Rows.Count > 0)
                {
                    foreach (DataRow drEmp in dtEmp.Rows)
                    {
                        decimal TransCnt = 0;


                        //*** Get Working Day ***
                        DateTime LoopDate = _CalPayrollDateST;
                        while (LoopDate <= _CalPayrollDateEN) {
                            string taffTime = "";

                            DataTable dtEMCL = new DataTable();
                            string strEMCL = @"SELECT *  FROM EMCL WHERE YM = '" + LoopDate.ToString("yyyyMM") + "' and code='" + drEmp["code"].ToString() + "' ";
                            OracleCommand cmdEMCL = new OracleCommand();
                            cmdEMCL.CommandText = strEMCL;
                            dtEMCL = oOra.Query(cmdEMCL);

                            string ThisSHT = dtEMCL.Rows[0]["STSS"].ToString().Substring(LoopDate.Day, 1);
                            if(ThisSHT == "N" || ThisSHT == "D")
                            {
                                taffTime = GetTaffTime(drEmp["code"].ToString(), LoopDate, ThisSHT);
                            }
                            else
                            {
                                taffTime = GetTaffTime(drEmp["code"].ToString(), LoopDate, "D");
                                if(taffTime.Length <= 5)
                                {
                                    taffTime = GetTaffTime(drEmp["code"].ToString(), LoopDate, "N");
                                }
                            }

                            //***********************************
                            //  more than 5 hours count 1 day
                            //***********************************
                            if(taffTime.Length > 5)
                            {
                                TransCnt = TransCnt + 1;
                            }

                            // plus 1 day
                            LoopDate = LoopDate.AddDays(1);
                        }


                        //************************************
                        // UPDATE ADJ
                        //************************************
                        decimal _TransAmt = 0;                        
                        decimal _Gasoline = 0;

                        _Gasoline = Convert.ToDecimal(drEmp["ATBN04"].ToString());

                        if (_Gasoline == 0)
                        {
                            _TransAmt = TransCnt * TransCost;
                        }

                        string strUpd = @"UPDATE PRAJ SET TRAN=TO_NUMBER('" + _TransAmt.ToString() + @"') 
                                    WHERE PDATE = '" + _CalPayrollDate.ToString("dd/MMM/yyyy") + @"'
                                        AND WTYPE = '" + drEmp["wType"].ToString() + @"'
                                        AND CODE = '" + drEmp["code"].ToString() + @"'  AND ADJ = '0'  ";
                        OracleCommand cmdUpd = new OracleCommand();
                        cmdUpd.CommandText = strUpd;
                        oOra.ExecuteCommand(cmdUpd);

                    } // end foreach




                    // Commit Transaction
                    Trans.Commit();

                } // end if


            }
            catch (Exception ex)
            {
                msgError = ex.Message.ToString();

                // Roll Back Transaction
                Trans.Rollback();
            }
            finally
            {
                if (!isError)
                {
                    MessageBox.Show("Completed");
                }
                else
                {
                    MessageBox.Show(msgError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Trans.Dispose();
            }
        }

        // remain export Excel
        private void btnPayrollGenShiftMoney_Click(object sender, EventArgs e)
        {
            _PayrollStart = true;
            bool isError = false;
            string msgError = "";
            decimal ShiftCostFirst = 120;
            decimal ShiftCostSecond = 120;

            OracleTransaction Trans = oOra.BeginTrans();

            try
            {

                // Get Shift Cost
                DataTable dtDict = new DataTable();
                string strDict = @"SELECT  *  FROM DICT WHERE  type ='ALLO' and item = 'SHIF'  ";
                OracleCommand cmdDict = new OracleCommand();
                cmdDict.CommandText = strDict;
                dtDict = oOra.Query(cmdDict);
                ShiftCostFirst = Convert.ToDecimal(dtDict.Rows[0]["NOTE"].ToString().Trim());
                ShiftCostSecond = Convert.ToDecimal(dtDict.Rows[0]["TITEM"].ToString().Trim());


                //**** Set Default Value *****
                string strUpPRAJ = @"UPDATE PRAJ SET SHIFT=0 
                                     WHERE ADJ='0' AND PDATE='" + _CalPayrollDate.ToString("dd/MMM/yyyy") + @"'   ";
                OracleCommand cmdUpPRAJ = new OracleCommand();
                cmdUpPRAJ.CommandText = strUpPRAJ;
                oOra.ExecuteCommand(cmdUpPRAJ);


                //**** Get All Employee *****
                DataTable dtEmp = new DataTable();
                string strEmp = @"SELECT wtype,code,join,resign,pren,name,surn,posit,dvcd,rank  
                                  FROM EMPM 
                                  WHERE (resign is null or resign > '" + _CalPayrollDateST.ToString("dd/MMM/yyyy") + @"' ) 
                                    AND join<='" + _CalPayrollDateEN.ToString("dd/MMM/yyyy") + @"' 
                                  ORDER BY code ";
                OracleCommand cmdEmp = new OracleCommand();
                cmdEmp.CommandText = strEmp;
                dtEmp = oOra.Query(cmdEmp);


                if (dtEmp.Rows.Count > 0)
                {
                    foreach (DataRow drEmp in dtEmp.Rows)
                    {
                        //****** Join *******
                        DateTime joinDate = new DateTime(1900, 1, 1);
                        joinDate = Convert.ToDateTime(drEmp["join"].ToString());

                        //****** Resign *******
                        DateTime resignDate = new DateTime(1900, 1, 1);
                        try { resignDate = Convert.ToDateTime(drEmp["resign"].ToString()); } catch { }


                        bool _FirstFlagMonth = false, _SecondFlagMonth = false;
                        string _FirstSTSS = "", _SecondSTSS = "", _FirstSTSO = "", _SecondSTSO = "";
                        decimal _FirstSHTCnt = 0, _SecondSHTCnt = 0;
                        decimal _SHTAmt = 0;
                        DateTime BeginDate = new DateTime(1900,1,1);
                        DateTime EndDate = new DateTime(1900,1,1);


                        //******* Get Shift EMCL on First Month *******
                        DataTable dtEMCL = new DataTable();
                        string strEMCL = @"SELECT *  FROM EMCL WHERE YM = '" + _CalPayrollDateST.ToString("yyyyMM") + "' and code='" + drEmp["code"].ToString() + "' ";
                        OracleCommand cmdEMCL = new OracleCommand();
                        cmdEMCL.CommandText = strEMCL;
                        dtEMCL = oOra.Query(cmdEMCL);
                        if(dtEMCL.Rows.Count > 0)
                        {
                            _FirstSTSS = dtEMCL.Rows[0]["STSS"].ToString();
                            _FirstSTSO = dtEMCL.Rows[0]["STSO"].ToString();
                            _FirstFlagMonth = true;
                        }
                        else
                        {
                            _FirstSTSO = "";
                        }


                        //******* Get Shift EMCL on Second Month *******
                        DataTable dtEMCL2 = new DataTable();
                        string strEMCL2 = @"SELECT *  FROM EMCL WHERE YM = '" + _CalPayrollDateEN.ToString("yyyyMM") + "' and code='" + drEmp["code"].ToString() + "' ";
                        OracleCommand cmdEMCL2 = new OracleCommand();
                        cmdEMCL2.CommandText = strEMCL2;
                        dtEMCL2 = oOra.Query(cmdEMCL2);
                        if (dtEMCL.Rows.Count > 0)
                        {
                            _SecondSTSS = dtEMCL.Rows[0]["STSS"].ToString();
                            _SecondSTSO = dtEMCL.Rows[0]["STSO"].ToString();
                            _SecondFlagMonth = true;
                        }
                        else
                        {
                            _SecondSTSO = "";
                        }

                        // check have shift month
                        if (!_FirstFlagMonth && !_SecondFlagMonth)
                        {
                            _SHTAmt = 0;
                        }
                        else
                        {

                            //***** Set Begin Calculate Date *****
                            if (_CalPayrollDateST < joinDate)
                            {
                                BeginDate = joinDate;
                            }
                            else
                            {
                                BeginDate = _CalPayrollDateST;
                            }

                            //***** Set End Calculate Date *****
                            if (resignDate.Year < 2000)
                            {
                                EndDate = _CalPayrollDateEN;
                            }
                            else
                            {
                                EndDate = resignDate.AddDays(-1);
                            }

                            DateTime _LoopDate = new DateTime();
                            _LoopDate = BeginDate;
                            while (_LoopDate <= EndDate)
                            {
                                // **** Get Shift *****
                                string LoopSHT = "";
                                if(_LoopDate.Day >= 16)
                                {
                                    LoopSHT = _FirstSTSS.Substring(_LoopDate.Day, 1);
                                }
                                else if(_LoopDate.Day <= 15)
                                {
                                    LoopSHT = _SecondSTSS.Substring(_LoopDate.Day, 1);
                                }

                                //**** Only Night Shift *****
                                if(LoopSHT == "N")
                                {
                                    //******* Get Leave *******
                                    DataTable dtLV = new DataTable();
                                    string strLV = @"SELECT total FROM LVRQ WHERE cdate = '" + _LoopDate.ToString("dd/MMM/yyyy") + "' and code='" + drEmp["code"].ToString() + "' ";
                                    OracleCommand cmdLV = new OracleCommand();
                                    cmdLV.CommandText = strLV;
                                    dtLV = oOra.Query(cmdLV);
                                    decimal _lvTotal = 0;
                                    if (dtLV.Rows.Count > 0)
                                    {
                                        _lvTotal = Convert.ToDecimal(dtLV.Rows[0]["total"].ToString());
                                    }

                                    if (_lvTotal <= 285)
                                    {
                                        if (_LoopDate < _CalPayrollDate)
                                        {
                                            _FirstSHTCnt++;
                                        }
                                        else if (_LoopDate >= _CalPayrollDate)
                                        {
                                            _SecondSHTCnt++;
                                        }
                                    }

                                }

                                


                                //***** Day Add 1 ****
                                _LoopDate = _LoopDate.AddDays(1);
                            } // end while


                            ////****** Abnormal case Resign before Mid-Month *******
                            //if (EndDate < _CalPayrollDate && _FirstFlagMonth)
                            //{

                            //}

                            ////****** Abnormal case Join After Mid-Month *******
                            //if (BeginDate >= _CalPayrollDate && _SecondFlagMonth)
                            //{

                            //}

                            // *** SET Summary Shift Cost ****
                            _SHTAmt = (_FirstSHTCnt * ShiftCostFirst) + (_SecondSHTCnt * ShiftCostSecond);

                        } // end if




                        //************************************
                        // UPDATE ADJ
                        //************************************
                        string strUpd = @"UPDATE PRAJ SET Shift=TO_NUMBER('" + _SHTAmt.ToString() + @"') 
                                    WHERE PDATE = '" + _CalPayrollDate.ToString("dd/MMM/yyyy") + @"'
                                        AND WTYPE = '" + drEmp["wType"].ToString() + @"'
                                        AND CODE = '" + drEmp["code"].ToString() + @"'  AND ADJ = '0'  ";
                        OracleCommand cmdUpd = new OracleCommand();
                        cmdUpd.CommandText = strUpd;
                        oOra.ExecuteCommand(cmdUpd);


                    } // end foreach




                    // Commit Transaction
                    Trans.Commit();

                } // end if


            }
            catch (Exception ex)
            {
                msgError = ex.Message.ToString();

                // Roll Back Transaction
                Trans.Rollback();
            }
            finally
            {
                if (!isError)
                {
                    MessageBox.Show("Completed");
                }
                else
                {
                    MessageBox.Show(msgError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Trans.Dispose();
            }
        }

        // remain export Excel
        private void btnPayrollGenShiftMoney3G_Click(object sender, EventArgs e)
        {
            _PayrollStart = true;
            bool isError = false;
            string msgError = "";
            decimal ShiftCost = 0;

            OracleTransaction Trans = oOra.BeginTrans();

            try
            {

                // Get Shift Cost
                DataTable dtDict = new DataTable();
                string strDict = @"SELECT  *  FROM DICT WHERE  type ='ALLO' and item = '3SHIF'  ";
                OracleCommand cmdDict = new OracleCommand();
                cmdDict.CommandText = strDict;
                dtDict = oOra.Query(cmdDict);
                ShiftCost = Convert.ToDecimal(dtDict.Rows[0]["NOTE"].ToString().Trim());                


                //**** Set Default Value *****
                string strUpPRAJ = @"UPDATE PRAJ SET SHIFT=0  
                                     WHERE ADJ='3' AND PDATE='" + _CalPayrollDate.ToString("dd/MMM/yyyy") + @"'   ";
                OracleCommand cmdUpPRAJ = new OracleCommand();
                cmdUpPRAJ.CommandText = strUpPRAJ;
                oOra.ExecuteCommand(cmdUpPRAJ);


                //**** Get All Employee *****
                DataTable dtEmp = new DataTable();
                string strEmp = @"SELECT wtype,code,pren,name,surn,rank,join,resign,grpot  
                                  FROM EMPM 
                                  WHERE (resign is null or resign > '" + _CalPayrollDateST.ToString("dd/MMM/yyyy") + @"' ) 
                                    AND join<='" + _CalPayrollDateEN.ToString("dd/MMM/yyyy") + @"' 
                                  ORDER BY code ";
                OracleCommand cmdEmp = new OracleCommand();
                cmdEmp.CommandText = strEmp;
                dtEmp = oOra.Query(cmdEmp);


                if (dtEmp.Rows.Count > 0)
                {
                    foreach (DataRow drEmp in dtEmp.Rows)
                    {
                        
                        //****** Join *******
                        DateTime joinDate = new DateTime(1900, 1, 1);
                        joinDate = Convert.ToDateTime(drEmp["join"].ToString());

                        //****** Resign *******
                        DateTime resignDate = new DateTime(1900, 1, 1);
                        try { resignDate = Convert.ToDateTime(drEmp["resign"].ToString()); } catch { }


                        bool _FirstFlagMonth = false, _SecondFlagMonth = false;
                        string _FirstSTSS = "", _SecondSTSS = "", _FirstSTSO = "", _SecondSTSO = "";
                        decimal _FirstSHTCnt = 0, _SecondSHTCnt = 0;
                        decimal _SHTAmt = 0, _SHTCnt = 0;
                        DateTime BeginDate = new DateTime(1900, 1, 1);
                        DateTime EndDate = new DateTime(1900, 1, 1);


                        //******* Get Shift EMCL on First Month *******
                        DataTable dtEMCL = new DataTable();
                        string strEMCL = @"SELECT *  FROM EMCL WHERE YM = '" + _CalPayrollDateST.ToString("yyyyMM") + "' and code='" + drEmp["code"].ToString() + "' ";
                        OracleCommand cmdEMCL = new OracleCommand();
                        cmdEMCL.CommandText = strEMCL;
                        dtEMCL = oOra.Query(cmdEMCL);
                        if (dtEMCL.Rows.Count > 0)
                        {
                            _FirstSTSS = dtEMCL.Rows[0]["STSS"].ToString();
                            _FirstSTSO = dtEMCL.Rows[0]["STSO"].ToString();
                            _FirstFlagMonth = true;
                        }
                        else
                        {
                            _FirstSTSO = "";
                        }


                        //******* Get Shift EMCL on Second Month *******
                        DataTable dtEMCL2 = new DataTable();
                        string strEMCL2 = @"SELECT *  FROM EMCL WHERE YM = '" + _CalPayrollDateEN.ToString("yyyyMM") + "' and code='" + drEmp["code"].ToString() + "' ";
                        OracleCommand cmdEMCL2 = new OracleCommand();
                        cmdEMCL2.CommandText = strEMCL2;
                        dtEMCL2 = oOra.Query(cmdEMCL2);
                        if (dtEMCL.Rows.Count > 0)
                        {
                            _SecondSTSS = dtEMCL.Rows[0]["STSS"].ToString();
                            _SecondSTSO = dtEMCL.Rows[0]["STSO"].ToString();
                            _SecondFlagMonth = true;
                        }
                        else
                        {
                            _SecondSTSO = "";
                        }


                        //***** Set Begin Calculate Date *****
                        if (_CalPayrollDateST < joinDate)
                        {
                            BeginDate = joinDate;
                        }
                        else
                        {
                            BeginDate = _CalPayrollDateST;
                        }

                        //***** Set End Calculate Date *****
                        if (resignDate.Year < 2000)
                        {
                            EndDate = _CalPayrollDateEN;
                        }
                        else
                        {
                            EndDate = resignDate.AddDays(-1);
                        }

                        DateTime _LoopDate = new DateTime();
                        _LoopDate = BeginDate;
                        while (_LoopDate <= EndDate)
                        {
                            // **** Get Shift *****
                            string LoopSHT = "";
                            string LoopGRP = "";
                            if (_LoopDate.Day >= 16)
                            {
                                LoopSHT = _FirstSTSS.Substring(_LoopDate.Day, 1);
                                LoopGRP = _FirstSTSO.Substring(_LoopDate.Day, 1);
                            }
                            else if (_LoopDate.Day <= 15)
                            {
                                LoopSHT = _SecondSTSS.Substring(_LoopDate.Day, 1);
                                LoopGRP = _SecondSTSO.Substring(_LoopDate.Day, 1);
                            }


                            //**** Only 3 GROUP *****
                            if (LoopGRP == "3")
                            {
                                if( LoopSHT == "D" || LoopSHT == "N"){
                                    //******* Get Group OT *******
                                    DataTable dtGrpOT = new DataTable();
                                    string strGrpOT = @"SELECT SHGRP, SHSTS FROM VI_SHFT_TYPE WHERE grpot = '" + drEmp["grpot"].ToString() + "'   ";
                                    OracleCommand cmdGrpOT = new OracleCommand();
                                    cmdGrpOT.CommandText = strGrpOT;
                                    dtGrpOT = oOra.Query(cmdGrpOT);


                                    //******* Get SHIFT 3 GROUP OPCL *******
                                    DataTable dtOPCL = new DataTable();
                                    string strOPCL = @"SELECT * FROM opcl WHERE ym= '" + _LoopDate.ToString("yyyyMM") + "' and otype = 'D1'   ";
                                    OracleCommand cmdOPCL = new OracleCommand();
                                    cmdOPCL.CommandText = strOPCL;
                                    dtOPCL = oOra.Query(cmdOPCL);

                                    if (dtOPCL.Rows.Count > 0)
                                    {
                                        string SHT3GRP = dtOPCL.Rows[0]["STSS"].ToString().Substring(_LoopDate.Day, 1);

                                        if(SHT3GRP == "H")
                                        {
                                            //******* Get Leave *******
                                            DataTable dtLV = new DataTable();
                                            string strLV = @"SELECT total FROM LVRQ WHERE cdate = '" + _LoopDate.ToString("dd/MMM/yyyy") + "' and code='" + drEmp["code"].ToString() + "' ";
                                            OracleCommand cmdLV = new OracleCommand();
                                            cmdLV.CommandText = strLV;
                                            dtLV = oOra.Query(cmdLV);
                                            decimal _lvTotal = 0;

                                            // Have Leave Record *** Employee Leave in Holiday ***
                                            if (dtLV.Rows.Count > 0)
                                            {
                                                _lvTotal = Convert.ToDecimal(dtLV.Rows[0]["total"].ToString());

                                                //***** Leave Less Than 4.75 Hr. *****
                                                if (_lvTotal <= 285)
                                                {
                                                    _SHTCnt++; // Plus 3 Group Shift Count
                                                }
                                            }
                                            else
                                            {
                                                //**************************************************
                                                //    Employee Taff Normal Time
                                                //**************************************************
                                                DataTable dtWTime = new DataTable();
                                                string strWTime = @"select * from WTME where code='" + drEmp["code"].ToString() + "' and cdate='" + _LoopDate.ToString("dd/MMM/yyyy") + "' ";
                                                OracleCommand cmdWTime = new OracleCommand();
                                                cmdWTime.CommandText = strWTime;
                                                dtWTime = oOra.Query(cmdWTime);

                                                // check have Taff Time
                                                if (dtWTime.Rows.Count > 0)
                                                {
                                                    // Loop Taff Time
                                                    foreach (DataRow drWT in dtWTime.Rows)
                                                    {
                                                        if (ConvOnlyTime(drWT["Time"].ToString()) > ConvOnlyTime("12:00"))
                                                        {
                                                            _SHTCnt++; // Plus 3 Group Shift Count
                                                        }
                                                        else
                                                        {
                                                            //**** Forgot Card of New Employee ****
                                                            DataTable dtTMRQ = new DataTable();
                                                            string strTMRQ = @"select code from TMRQ where code='" + drEmp["code"].ToString() + "' and cdate='" + _LoopDate.ToString("dd/MMM/yyyy") + "' ";
                                                            OracleCommand cmdTMRQ = new OracleCommand();
                                                            cmdTMRQ.CommandText = strTMRQ;
                                                            dtTMRQ = oOra.Query(cmdTMRQ);
                                                            if (dtTMRQ.Rows.Count > 0)
                                                            {
                                                                _SHTCnt++; // Plus 3 Group Shift Count
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (dtWTime.Rows.Count == 0)
                                                {
                                                    //**** Forgot Card of New Employee ****
                                                    DataTable dtTMRQ = new DataTable();
                                                    string strTMRQ = @"select code from TMRQ where code='" + drEmp["code"].ToString() + "' and cdate='" + _LoopDate.ToString("dd/MMM/yyyy") + "' ";
                                                    OracleCommand cmdTMRQ = new OracleCommand();
                                                    cmdTMRQ.CommandText = strTMRQ;
                                                    dtTMRQ = oOra.Query(cmdTMRQ);
                                                    if (dtTMRQ.Rows.Count > 0)
                                                    {
                                                        _SHTCnt++; // Plus 3 Group Shift Count
                                                    }
                                                }
                                            }
                                        }


                                        

                                    }// end if Shift 3 Group
                                } // end if shift D / N                               
                            } // end if 3 GROUP




                            //***** Day Add 1 ****
                            _LoopDate = _LoopDate.AddDays(1);
                        } // end while



                        if (_SHTCnt > 0) {

                            //************************************
                            // UPDATE ADJ
                            //************************************
                            _SHTAmt = _SHTCnt * ShiftCost;

                            string strUpd = @"UPDATE PRAJ SET Shift=TO_NUMBER('" + _SHTAmt.ToString() + @"') 
                                    WHERE PDATE = '" + _CalPayrollDate.ToString("dd/MMM/yyyy") + @"'
                                        AND WTYPE = '" + drEmp["wType"].ToString() + @"'
                                        AND CODE = '" + drEmp["code"].ToString() + @"'  AND ADJ = '3'  ";
                            OracleCommand cmdUpd = new OracleCommand();
                            cmdUpd.CommandText = strUpd;
                            oOra.ExecuteCommand(cmdUpd);
                        }

                    } // end foreach


                    // Commit Transaction
                    Trans.Commit();

                } // end if


            }
            catch (Exception ex)
            {
                msgError = ex.Message.ToString();

                // Roll Back Transaction
                Trans.Rollback();
            }
            finally
            {
                if (!isError)
                {
                    MessageBox.Show("Completed");
                }
                else
                {
                    MessageBox.Show(msgError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Trans.Dispose();
            }
        }


        private void btnPayrollGenSepcialShift_Click(object sender, EventArgs e)
        {
            _PayrollStart = true;
            bool isError = false;
            string msgError = "";
            decimal ShiftCost = 0;

            OracleTransaction Trans = oOra.BeginTrans();

            try
            {

                // Get Shift Cost
                DataTable dtDict = new DataTable();
                string strDict = @"SELECT  *  FROM DICT WHERE  type ='ALLO' and item = 'SSHF'  ";
                OracleCommand cmdDict = new OracleCommand();
                cmdDict.CommandText = strDict;
                dtDict = oOra.Query(cmdDict);
                ShiftCost = Convert.ToDecimal(dtDict.Rows[0]["NOTE"].ToString().Trim());


                //**** Set Default Value *****
                string strUpPRAJ = @"UPDATE PRAJ SET shift=0  
                                     WHERE ADJ='2' AND PDATE='" + _CalPayrollDate.ToString("dd/MMM/yyyy") + @"'   ";
                OracleCommand cmdUpPRAJ = new OracleCommand();
                cmdUpPRAJ.CommandText = strUpPRAJ;
                oOra.ExecuteCommand(cmdUpPRAJ);


                DataTable dtOT = new DataTable();
                string strOT = @"SELECT * FROM OTRQ WHERE odate>='" + _CalPayrollDateST.ToString("dd/MMM/yyyy") + "' and odate<='" + _CalPayrollDateEN.ToString("dd/MMM/yyyy") + "' ORDER BY code";
                OracleCommand cmdOT = new OracleCommand();
                cmdOT.CommandText = strOT;
                dtOT = oOra.Query(cmdOT);

                foreach (DataRow drOT in dtOT.Rows) { 
                     
                    
                } // end foreach OT



                ////**** Get All Employee *****
                //DataTable dtEmp = new DataTable();
                //string strEmp = @"SELECT code, wtype, join, resign 
                //                  FROM EMPM 
                //                  WHERE (resign is null or resign > '" + _CalPayrollDateST.ToString("dd/MMM/yyyy") + @"' ) 
                //                    AND join<='" + _CalPayrollDateEN.ToString("dd/MMM/yyyy") + @"' 
                //                  ORDER BY code ";
                //OracleCommand cmdEmp = new OracleCommand();
                //cmdEmp.CommandText = strEmp;
                //dtEmp = oOra.Query(cmdEmp);


                //if (dtEmp.Rows.Count > 0)
                //{
                //    foreach (DataRow drEmp in dtEmp.Rows)
                //    {





                //    } // end foreach




                //    // Commit Transaction
                //    Trans.Commit();

                //} // end if


            }
            catch (Exception ex)
            {
                msgError = ex.Message.ToString();

                // Roll Back Transaction
                Trans.Rollback();
            }
            finally
            {
                if (!isError)
                {
                    MessageBox.Show("Completed");
                }
                else
                {
                    MessageBox.Show(msgError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Trans.Dispose();
            }
        }


        private void btnPayrollGenBenefit_Click(object sender, EventArgs e)
        {
            _PayrollStart = true;
            bool isError = false;
            string msgError = "";

            OracleTransaction Trans = oOra.BeginTrans();

            try
            {
                //**** Set Default Value *****
                string strUpPRAJ = @"UPDATE PRAJ SET ABDAY=0,WORK1=0,WORK2=0  
                                     WHERE ADJ='0' AND PDATE='" + _CalPayrollDate.ToString("dd/MMM/yyyy") + @"'   ";
                OracleCommand cmdUpPRAJ = new OracleCommand();
                cmdUpPRAJ.CommandText = strUpPRAJ;
                oOra.ExecuteCommand(cmdUpPRAJ);


                //**** Get All Employee *****
                DataTable dtEmp = new DataTable();
                string strEmp = @"SELECT code, wtype, join, resign 
                                  FROM EMPM 
                                  WHERE (resign is null or resign > '" + _CalPayrollDateST.ToString("dd/MMM/yyyy") + @"' ) 
                                    AND join<='" + _CalPayrollDateEN.ToString("dd/MMM/yyyy") + @"' 
                                  ORDER BY code ";
                OracleCommand cmdEmp = new OracleCommand();
                cmdEmp.CommandText = strEmp;
                dtEmp = oOra.Query(cmdEmp);


                if (dtEmp.Rows.Count > 0)
                {
                    foreach (DataRow drEmp in dtEmp.Rows)
                    {





                    } // end foreach




                    // Commit Transaction
                    Trans.Commit();

                } // end if


            }
            catch (Exception ex)
            {
                msgError = ex.Message.ToString();

                // Roll Back Transaction
                Trans.Rollback();
            }
            finally
            {
                if (!isError)
                {
                    MessageBox.Show("Completed");
                }
                else
                {
                    MessageBox.Show(msgError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Trans.Dispose();
            }
        }


        private void btnPayrollGenPayroll_Click(object sender, EventArgs e)
        {
            _PayrollStart = true;
        }












        private void dpkrPayroll_ValueChanged(object sender, EventArgs e)
        {
            _CalPayrollDate = new DateTime(dpkrPayroll.Value.Year, dpkrPayroll.Value.Month, 1);
            _CalPayrollDateST = new DateTime(dpkrPayroll.Value.AddMonths(-1).Year, dpkrPayroll.Value.AddMonths(-1).Month, 16);
            _CalPayrollDateEN = new DateTime(dpkrPayroll.Value.Year, dpkrPayroll.Value.Month, 15);
        }

        public decimal getWorkDay(string _Code, DateTime _LoopDate, DateTime _EndDate) {
            decimal wrkDay = 0;
            string ThisSHT = "", TypeSHT = "";
            DataTable dtWork = new DataTable();
            string str = "SELECT * FROM EMCL WHERE YM='"+ _LoopDate.ToString("yyyyMM") + "' AND CODE='"+ _Code + "'  ";
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = str;
            dtWork = oOra.Query(cmd);

            if(dtWork.Rows.Count > 0)
            {
                TypeSHT = dtWork.Rows[0]["STSO"].ToString().Substring(_LoopDate.Day, 1);
                ThisSHT = dtWork.Rows[0]["STSS"].ToString().Substring(_LoopDate.Day, 1);

                if(TypeSHT == "3")
                {
                    if (ThisSHT == "T" || ThisSHT == "O" || ThisSHT == "S" || ThisSHT == "H")
                    {
                        if (ThisSHT == "T")
                        {
                            wrkDay = wrkDay + 1;
                        }
                    }
                    else
                    {
                        wrkDay = wrkDay + getWorkDaySub(_Code, _LoopDate, _EndDate);
                    }
                }
                else
                {
                    if(ThisSHT == "T" || ThisSHT == "H")
                    {
                        if(ThisSHT == "T")
                        {
                            wrkDay = wrkDay + 1;
                        }
                    }
                    else
                    {
                        wrkDay = wrkDay + getWorkDaySub(_Code, _LoopDate, _EndDate);
                    }

                }
            }

            return wrkDay;
        }


        public decimal getWorkDaySub(string _Code, DateTime _LoopDate, DateTime _EndDate)
        {
            decimal wrkDay = 1;
            DataTable dtPENA = new DataTable();
            string str = "select pdate,type,tdate from PENA where code='" + _Code + "' and pdate>='" + _LoopDate.ToString("dd/MMM/yyyy") + "' AND TYPE='SUSP' order by code,pdate";
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = str;
            dtPENA = oOra.Query(cmd);

            if(dtPENA.Rows.Count > 0)
            {
                foreach (DataRow drPENA in dtPENA.Rows)
                {
                    if (drPENA["Type"].ToString() == "SUSP" && Convert.ToDateTime(drPENA["pDate"].ToString()) <= _EndDate && Convert.ToDateTime(drPENA["Tdate"].ToString()) >= _LoopDate)
                    {
                        decimal diffday = ( Convert.ToDateTime(drPENA["Tdate"].ToString()) - Convert.ToDateTime(drPENA["pDate"].ToString()) ).Days;
                        wrkDay = wrkDay - (diffday + 1);
                    }

                }// end foreach
            } // end if




            DataTable dtLVRQ = new DataTable();
            string strLVRQ = "SELECT total FROM LVRQ WHERE code='" + _Code + "' and cdate='" + _LoopDate.ToString("dd/MMM/yyyy") + "' AND salsts='N' ";
            OracleCommand cmdLVRQ = new OracleCommand();
            cmdLVRQ.CommandText = strLVRQ;
            dtLVRQ = oOra.Query(cmdLVRQ);

            if (dtLVRQ.Rows.Count > 0)
            {
                foreach (DataRow drLVRQ in dtLVRQ.Rows)
                {
                    wrkDay = wrkDay - Convert.ToDecimal(drLVRQ["total"].ToString()) / 525;
                }// end foreach
            } // end if


            DataTable dtTNRQ = new DataTable();
            string strTNRQ = "SELECT * FROM TNRQ WHERE code='" + _Code + "' and fdate='" + _LoopDate.ToString("dd/MMM/yyyy") + "' and note like 'HOLIDAY%'  ";
            OracleCommand cmdTNRQ = new OracleCommand();
            cmdTNRQ.CommandText = strTNRQ;
            dtTNRQ = oOra.Query(cmdTNRQ);

            if (dtTNRQ.Rows.Count > 0)
            {
                foreach (DataRow drTNRQ in dtTNRQ.Rows)
                {
                    wrkDay = wrkDay - 1;

                }// end foreach
            } // end if


            return wrkDay;
        }

        public int CheckAndGetA400(string _Code, DateTime _Join, DateTime _Month, int maxLevel) {
            
            int LastA400 = 0;
            bool leave;
            DateTime BeginDate = new DateTime(_Month.AddMonths(-1).Year, _Month.AddMonths(-1).Month, 16);
            DateTime EndDate = new DateTime(_Month.Year, _Month.Month, 15);

            //'* Check this month leave' *
            leave = CheckLeave(_Code, _Month);

            if (leave)
            {
                LastA400 = 0;
            }
            else
            {
                if( _Join <= BeginDate)
                {
                    DataTable dtPRAJ = new DataTable();
                    string strPRAJ = @"SELECT * FROM PRAJ WHERE pdate='" + _Month.ToString("dd/MMM/yyyy") + "' and code='" + _Code + "'  and adj='0' ";
                    OracleCommand cmdPRAJ = new OracleCommand();
                    cmdPRAJ.CommandText = strPRAJ;
                    dtPRAJ = oOra.Query(cmdPRAJ);

                    LastA400 = 0;
                    if (dtPRAJ.Rows.Count > 0)
                    {
                        foreach (DataRow drPRAJ in dtPRAJ.Rows)
                        {
                            LastA400 = Convert.ToInt32( drPRAJ["A400Level"].ToString());
                        } // end foreach
                    }
                    else
                    {
                        LastA400 = 0;
                    }
                }                
            }

            return LastA400;
        }

        public bool CheckLeave(string _Code, DateTime _Month) {
            bool result = false;
            DateTime BeginDate = new DateTime(_Month.AddMonths(-1).Year, _Month.AddMonths(-1).Month, 16);
            DateTime EndDate = new DateTime(_Month.Year, _Month.Month, 15);

            DataTable dtLVRQ = new DataTable();
            string strLVRQ = "SELECT total FROM LVRQ WHERE code='" + _Code + "' and cdate >= '" + BeginDate.ToString("dd/MMM/yyyy") + "' and cdate<='" + EndDate.ToString("dd/MMM/yyyy") + "' and type<>'JSCK' and type<>'ANNU'  and type<>'ANNN' and type <>'SPSL'   and type <>'OTH' order by code,cdate   ";
            OracleCommand cmdLVRQ = new OracleCommand();
            cmdLVRQ.CommandText = strLVRQ;
            dtLVRQ = oOra.Query(cmdLVRQ);

            if (dtLVRQ.Rows.Count > 0) {
                result = true;
            }


            DataTable dtPENA = new DataTable();
            string strPENA = @"SELECT pdate,type FROM PENA WHERE code='" + _Code + "' and pdate >= '" + BeginDate.ToString("dd/MMM/yyyy") + "' and pdate <= '" + EndDate.ToString("dd/MMM/yyyy") + "' AND TYPE='SUSP' ORDER BY code,pdate ";
            OracleCommand cmdPENA = new OracleCommand();
            cmdPENA.CommandText = strPENA;
            dtPENA = oOra.Query(cmdPENA);

            if (dtPENA.Rows.Count > 0)
            {
                foreach (DataRow drPENA in dtPENA.Rows)
                {
                    if (Convert.ToDateTime(drPENA["pDate"].ToString()) >= BeginDate && Convert.ToDateTime(drPENA["pDate"].ToString()) <= EndDate)
                    {
                        result = true;
                    }
                }
            }

            return true;
        }

        public DateTime ConvOnlyTime(string _time)
        {
            return  new DateTime(1900, 1, 1, Convert.ToInt16(_time.Split(':')[0]), Convert.ToInt16(_time.Split(':')[1]), 0);            
        }






        public string GetTaffTime(string _Code, DateTime _YMD, string SHT)
        {
            string[] arrtime = new string[10];
            int idx = 0;

            //**************************************************
            //    Employee Taff Normal Time
            //**************************************************
            DataTable dtWTime = new DataTable();
            string strWTime = @"select * from wtme where code='" + _Code + "' and ((cdate = '" + _YMD.ToString("dd/MMM/yyyy") + "') or (cdate = '" + _YMD.ToString("dd/MMM/yyyy") + "' and time between '00:00' and '03:00' and DUTY = 'O')) order by cdate asc";
            OracleCommand cmdWTime = new OracleCommand();
            cmdWTime.CommandText = strWTime;
            dtWTime = oOra.Query(cmdWTime);


            if (dtWTime.Rows.Count > 0)
            {
                idx = 0;
                foreach (DataRow drWTime in dtWTime.Rows)
                {
                    if (SHT == "N" && ConvOnlyTime(drWTime["Time"].ToString()) > ConvOnlyTime("12:00"))
                    {
                        arrtime[idx] = drWTime["Time"].ToString();
                        idx++;
                    }
                    else if (SHT == "D")
                    {
                        arrtime[idx] = drWTime["Time"].ToString();
                        idx++;
                    }

                } // end foreach
            } // end if
            //**************************************************
            //    END Employee Taff Normal Time
            //**************************************************



            //**************************************************
            // Forgot Employee Card, New Employee, Business
            //**************************************************
            bool chkEmpNewOrBusiness = false;

            DataTable dtTMRQ = new DataTable();
            string strTMRQ = @"SELECT * FROM TMRQ WHERE code='" + _Code + "' and cdate='" + _YMD.ToString("dd/MMM/yyyy") + "'    ";
            OracleCommand cmdTMRQ = new OracleCommand();
            cmdTMRQ.CommandText = strTMRQ;
            dtTMRQ = oOra.Query(cmdTMRQ);
            if(dtTMRQ.Rows.Count > 0)
            {
                if (dtTMRQ.Rows[0]["FTIME"].ToString() != "")
                {
                    chkEmpNewOrBusiness = true;
                    arrtime[idx] = dtTMRQ.Rows[0]["FTIME"].ToString();
                    idx++;
                }
                if (dtTMRQ.Rows[0]["TTIME"].ToString() != "")
                {
                    chkEmpNewOrBusiness = true;
                    arrtime[idx] = dtTMRQ.Rows[0]["TTIME"].ToString();
                    idx++;
                }
            }
            //**************************************************
            // Forgot Employee Card, New Employee, Business
            //**************************************************


            //**************************************************
            //  CASE NIGHT SHIFT AND NOT BUSINESS
            //**************************************************
            if (SHT == "N" && !chkEmpNewOrBusiness)
            {
                if(idx > 0)
                {
                    arrtime[0] = arrtime[idx];
                    for(int i = idx; i < 1; i--)
                    {
                        arrtime[i] = "";
                    }
                }
            }
            //**************************************************
            //  CASE NIGHT SHIFT AND NOT BUSINESS
            //**************************************************


            //**************************************************
            //  CASE NIGHT SHIFT AND HAVE BUSINESS
            //**************************************************
            if (SHT == "N" && chkEmpNewOrBusiness && idx > 1)
            {
                arrtime[0] = arrtime[idx - 1];
                arrtime[1] = arrtime[idx];
                for (int i = idx; i < 2; i--)
                {
                    arrtime[i] = "";
                }
            }
            //**************************************************
            //  CASE NIGHT SHIFT AND HAVE BUSINESS
            //**************************************************


            //**************************************************
            //  CASE NIGHT SHIFT 
            //**************************************************
            if (SHT == "N")
            {
                DataTable dtWTME = new DataTable();
                string strWTME = @"SELECT * FROM WTME WHERE code='" + _Code + "' and cdate='" + _YMD.AddDays(1).ToString("dd/MMM/yyyy") + "'   ";
                OracleCommand cmdWTME = new OracleCommand();
                cmdWTME.CommandText = strWTME;
                dtWTME = oOra.Query(cmdWTME);

                if(dtWTME.Rows.Count > 0)
                {
                    foreach (DataRow drWTME in dtWTME.Rows) {
                        if (ConvOnlyTime(drWTME["Time"].ToString()) <= ConvOnlyTime("12:00"))
                        {
                            arrtime[idx] = drWTME["Time"].ToString();
                            idx++;
                        }
                    }                    
                }
            }
            //**************************************************
            //  CASE NIGHT SHIFT 
            //**************************************************

            //**************************************************
            //  CASE DAY SHIFT 
            //**************************************************

            if (SHT == "D")
            {
                for (int j = 0; j <= idx - 1; j++)
                {
                    for (int k = 0; k <= idx-1; k++)
                    {
                        if (ConvOnlyTime(arrtime[k]) > ConvOnlyTime(arrtime[k + 1]))
                        {
                            string TEMP = arrtime[k];
                            arrtime[k] = arrtime[k + 1];
                            arrtime[k + 1] = TEMP;
                        }// end if
                    } // end for
                } // end for
            }
            //**************************************************
            //  CASE DAY SHIFT 
            //**************************************************


            //***********************************
            //     Set Return Data
            //***********************************
            string tempreturn = "";
            string result = "";
            for (int i = 0; i <= idx; i++)
            {
                tempreturn = tempreturn + arrtime[i]+ " ";
            }

            result = tempreturn.Trim();


            return result;

        }

        public int ConvToMin(string _time) {
            int reuslt = 0, hour = 0, min = 0;

            if (_time != "")
            {
                if (_time.Contains(":"))
                {
                    string[] aryTime = _time.Split(':');
                    hour = Convert.ToInt32(aryTime[0]);
                    min = Convert.ToInt32(aryTime[1]);

                    reuslt = (hour * 60) + min;
                }
            }
            return reuslt;
        }








    }
}
