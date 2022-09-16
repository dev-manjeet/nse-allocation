using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSEAllocation.Models
{
    public class ClsAllocationReq
    {
        public string version { get; set; }
        public Data data { get; set; }
    }
    public class Data
    {
        public string msgId { get; set; }
        public string requestType { get; set; }
        public IList<AllocationRequest> allocationRequest { get; set; }

    }
    public class AllocationRequest
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

    }
}
