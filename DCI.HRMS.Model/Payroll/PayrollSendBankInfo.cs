using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCI.HRMS.Model.Payroll
{
    public class PayrollSendBankInfo
    {
        public class PayrollSendBankHead
        {
            public string _hd01_type {set; get;}
            public string _hd02_comid { set; get; }
            public string _hd03_comref { set; get; }
            public string _hd04_msgDT { set; get; }
            public string _hd05_msgTM { set; get; }
            public string _hd06_chId { set; get; }
            public string _hd07_batRef { set; get; }
            public PayrollSendBankHead()
            {
                _hd01_type = "";
                _hd02_comid = "";
                _hd03_comref = "";
                _hd04_msgDT = "";
                _hd05_msgTM = "";
                _hd06_chId = "";
                _hd07_batRef = "";
            }
            
        }

        public class PayrollSendBankDebit
        {
            public string _Db01_type { set; get; }
            public string _Db02_prdCd { set; get; }
            public string _Db03_valDT { set; get; }
            public string _Db04_AccNo { set; get; }
            public string _Db05_AccType { set; get; }
            public string _Db06_BrchCd { set; get; }
            public string _Db07_Curr { set; get; }
            public string _Db08_Amt { set; get; }
            public string _Db09_Ref { set; get; }
            public string _Db10_NoCre { set; get; }
            public string _Db11_FeeAcc { set; get; }
            public string _Db12_Filtr { set; get; }
            public string _Db13_Cler { set; get; }
            public string _Db14_AccTypeFee { set; get; }
            public string _Db15_BrchCdFee { set; get; }
            public PayrollSendBankDebit()
            {
                _Db01_type = "";
                _Db02_prdCd = "";
                _Db03_valDT = "";
                _Db04_AccNo = "";
                _Db05_AccType = "";
                _Db06_BrchCd = "";
                _Db07_Curr = "";
                _Db08_Amt = "";
                _Db09_Ref = "";
                _Db10_NoCre = "";
                _Db11_FeeAcc = "";
                _Db12_Filtr = "";
                _Db13_Cler = "";
                _Db14_AccTypeFee = "";
                _Db15_BrchCdFee = "";
            }

        }

        public class PayrollSendBankCredit
        {
            public string _Cr01_type { set; get; }
            public string _Cr02_seq { set; get; }
            public string _Cr03_acc { set; get; }
            public string _Cr04_amt { set; get; }
            public string _Cr05_curr { set; get; }
            public string _Cr06_ref { set; get; }
            public string _Cr07_wht { set; get; }
            public string _Cr08_inv_pre { set; get; }
            public string _Cr09_adv_req { set; get; }
            public string _Cr10_del_mode { set; get; }
            public string _Cr11_pick { set; get; }
            public string _Cr12_wht_frm { set; get; }
            public string _Cr13_wht_tax { set; get; }
            public string _Cr14_wht_att { set; get; }
            public string _Cr15_wht_no { set; get; }
            public string _Cr16_wht_amt { set; get; }
            public string _Cr17_inv_det { set; get; }
            public string _Cr18_int_amt { set; get; }
            public string _Cr19_wht_pay { set; get; }
            public string _Cr20_wht_rmk { set; get; }
            public string _Cr21_wht_decDT { set; get; }
            public string _Cr22_rec_cd { set; get; }
            public string _Cr23_rec_name { set; get; }
            public string _Cr24_rec_brn_cd { set; get; }
            public string _Cr25_rec_brn_name { set; get; }
            public string _Cr26_wht_sign { set; get; }
            public string _Cr27_benefic { set; get; }
            public string _Cr28_cus_ref { set; get; }
            public string _Cr29_cheq_ref { set; get; }
            public string _Cr30_pay_type_cd { set; get; }
            public string _Cr31_serv_type { set; get; }
            public string _Cr32_rmk { set; get; }
            public string _Cr33_scb_rmk { set; get; }
            public string _Cr34_benefic_chrg { set; get; }
            public PayrollSendBankCredit()
            {
                _Cr01_type = "";
                _Cr02_seq = "";
                _Cr03_acc = "";
                _Cr04_amt = "";
                _Cr05_curr = "";
                _Cr06_ref = "";
                _Cr07_wht = "";
                _Cr08_inv_pre = "";
                _Cr09_adv_req = "";
                _Cr10_del_mode = "";
                _Cr11_pick = "";
                _Cr12_wht_frm = "";
                _Cr13_wht_tax = "";
                _Cr14_wht_att = "";
                _Cr15_wht_no = "";
                _Cr16_wht_amt = "";
                _Cr17_inv_det = "";
                _Cr18_int_amt = "";
                _Cr19_wht_pay = "";
                _Cr20_wht_rmk = "";
                _Cr21_wht_decDT = "";
                _Cr22_rec_cd = "";
                _Cr23_rec_name = "";
                _Cr24_rec_brn_cd = "";
                _Cr25_rec_brn_name = "";
                _Cr26_wht_sign = "";
                _Cr27_benefic = "";
                _Cr28_cus_ref = "";
                _Cr29_cheq_ref = "";
                _Cr30_pay_type_cd = "";
                _Cr31_serv_type = "";
                _Cr32_rmk = "";
                _Cr33_scb_rmk = "";
                _Cr34_benefic_chrg = "";
            }

        }

        public class PayrollSendBankPayee
        {
            public string _Py01_type { set; get; }
            public string _Py02_ref { set; get; }
            public string _Py03_seq { set; get; }
            public string _Py04_id_card { set; get; }
            public string _Py05_name_th { set; get; }
            public string _Py06_addr1 { set; get; }
            public string _Py07_addr2 { set; get; }
            public string _Py08_addr3 { set; get; }
            public string _Py09_taxId { set; get; }
            public string _Py10_name_en { set; get; }
            public string _Py11_fax_nbr { set; get; }
            public string _Py12_mobile { set; get; }
            public string _Py13_email { set; get; }
            public string _Py14_payee2_name { set; get; }
            public string _Py15_payee2_addr1 { set; get; }
            public string _Py16_payee2_addr2 { set; get; }
            public string _Py17_payee2_addr3 { set; get; }
            public PayrollSendBankPayee()
            {
                _Py01_type = "";
                _Py02_ref = "";
                _Py03_seq = "";
                _Py04_id_card = "";
                _Py05_name_th = "";
                _Py06_addr1 = "";
                _Py07_addr2 = "";
                _Py08_addr3 = "";
                _Py09_taxId = "";
                _Py10_name_en = "";
                _Py11_fax_nbr = "";
                _Py12_mobile = "";
                _Py13_email = "";
                _Py14_payee2_name = "";
                _Py15_payee2_addr1 = "";
                _Py16_payee2_addr2 = "";
                _Py17_payee2_addr3 = "";
            }

        }

        public class PayrollSendBankTrailer
        {
            public string _Tr01_type { set; get; }
            public string _Tr02_ttl_debit { set; get; }
            public string _Tr03_ttl_credit { set; get; }
            public string _Tr04_ttl_amt { set; get; }
            
            public PayrollSendBankTrailer()
            {
                _Tr01_type = "";
                _Tr02_ttl_debit = "";
                _Tr03_ttl_credit = "";
                _Tr04_ttl_amt = "";                
            }

        }


    }
}
