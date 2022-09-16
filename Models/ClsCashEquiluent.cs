using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSEAllocation.Models
{
    public class ClsCashEquiluent
    {
        public string TRADING_MEMBER_PAN { get; set; }
        public string DATE { get; set; }
        public string UNIQUE_Client_Code { get; set; }
        public string Client_PAN { get; set; }
        public string Client_Name { get; set; }
        public string MTF_NON_MTF_INDICATOR { get; set; }
        public string FINANCIAL_LEDGER_BALANCE_A { get; set; }
        public string FINANCIAL_LEDGER_BALANCE_CLEAR_B { get; set; }
        public string Peak_Financial_Ledger_Balance_clear_C { get; set; }
        public string FINANCIAL_LEDGER_BALANCE_MCX { get; set; }
        public string FINANCIAL_LEDGER_BALANCE_NCDEX { get; set; }
        public string FINANCIAL_LEDGER_BALANCE_Icex { get; set; }
        public string BANK_GUARANTEE_BG { get; set; }
        public string FIXED_DEPOSIT_RECEIPT_Fdr { get; set; }
        public string GOVERNMENT_OF_INDIA_SecuRITIES_GSEC { get; set; }
        public string GILT_FUNDS { get; set; }
        public string Credit_entry_in_ledger_in_lieu_of_EPI { get; set; }
        public string Pool_Account { get; set; }
        public string Uncleared_Cheques { get; set; }
        public string Value_of_Commodities { get; set; }
        public string Cash_Collateral_for_MTF_Positions { get; set; }
        public string Unclaimed_Unsettled_Client_Funds { get; set; }
        public string Client_Bank_Account_No { get; set; }
        
        public string LAST_SETTLEMENT_DATE { get; set; }
        public string ES_INFORMATION_TYPE { get; set; }

        public string Value { get; set; }

    }
}
