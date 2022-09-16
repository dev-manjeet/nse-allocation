using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSEAllocation.Models
{
    public class ClsEnquiryStatusResp
    {
        public string status { get; set; }
        public MessagesE messages { get; set; }
        public DataEnqResp data { get; set; }
    }
    public class MessagesE
    {
        public string code { get; set; }

    }
    public class DataEnqResp
    {
        public List<InquiryResponse1> inquiryResponse { get; set; }

    }
    public class InquiryResponse1
    {
        public string curDate { get; set; }
        public string segment { get; set; }
        public string cmCode { get; set; }
        public string tmCode { get; set; }
        public string cpCode { get; set; }
        public string cliCode { get; set; }
        public string accType { get; set; }
        public double amt { get; set; }
        public string filler1 { get; set; }
        public string filler2 { get; set; }
        public string filler3 { get; set; }
        public string filler4 { get; set; }
        public string filler5 { get; set; }
        public string filler6 { get; set; }
        public string action { get; set; }
        public string errCd { get; set; }

    }
}
