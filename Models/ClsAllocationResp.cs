using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSEAllocation.Models
{
    public class ClsAllocationResp
    {
        public string status { get; set; }
        public Messages messages { get; set; }
        public Data1 data { get; set; }
    }
    public class Data1
    {
        public IList<InquiryResponse> inquiryResponse { get; set; }

    }
    public class InquiryResponse
    {
        public DateTime curDate { get; set; }
        public string segment { get; set; }
        public string cmCode { get; set; }
        public string tmCode { get; set; }
        public string cpCode { get; set; }
        public string cliCode { get; set; }
        public string accType { get; set; }
        public int amt { get; set; }
        public string filler1 { get; set; }
        public string filler2 { get; set; }
        public string filler3 { get; set; }
        public string filler4 { get; set; }
        public string filler5 { get; set; }
        public string filler6 { get; set; }
        public string action { get; set; }
        public string errCd { get; set; }

    }
    public class Messages
    {
        public string code { get; set; }

    }
}
