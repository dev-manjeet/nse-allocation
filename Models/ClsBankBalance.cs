using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSEAllocation.Models
{
    public class ClsBankBalance
    {
        [DeserializeAs(Name = "Bank Account No")]
        public string BankAccountNo { get; set; }
        public string IFSC { get; set; }
        [DeserializeAs(Name = "Bank Account Type")]
        public string BankAccountType { get; set; }
        public string WeekDay1 { get; set; }
        public string WeekDay2 { get; set; }
        public string WeekDay3 { get; set; }
        public string WeekDay4 { get; set; }
        public string WeekDay5 { get; set; }
        public string WeekDay6 { get; set; }
    }
    internal class ClsBankBalanceData
    {
        public List<ClsBankBalance> BankBalanceData { get; set; }
    }


    //===============MSEI========================

    internal class ClsMSEIBankBalanceData
    {
        public List<ClsMSEIBankBalance> BankBalanceData { get; set; }
    }
    public class ClsMSEIBankBalance
    {
        [DeserializeAs(Name = "Bank Account No")]
        public string BankAccountNo { get; set; }
        public string IFSC { get; set; }
        [DeserializeAs(Name = "Bank Account Type")]
        public string BankAccountType { get; set; }
        public double WeekDay1 { get; set; }
        public double WeekDay2 { get; set; }
        public double WeekDay3 { get; set; }
        public double WeekDay4 { get; set; }
        public double WeekDay5 { get; set; }
        public double WeekDay6 { get; set; }
    }
}
