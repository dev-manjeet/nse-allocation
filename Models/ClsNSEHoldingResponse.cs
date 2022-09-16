using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSEAllocation.Models
{
    public class ClsNSEHoldingSuccessResponse
    {
        public IList<string> hdrCode { get; set; }
        public string ackId { get; set; }
        public string memberPan { get; set; }
        public string hdrStatus { get; set; }
        public string weekDay { get; set; }
        public IList<NSEHoldingDataSuccess> holdingData { get; set; }
        public string lastWeekDay { get; set; }
    }
    public class NSEHoldingDataSuccess
    {
        public string recId { get; set; }
        public IList<string> code { get; set; }
        public string status { get; set; }

    }
    public class ClsNSEHoldingFailedResponse
    {
        public IList<string> hdrCode { get; set; }
        public string ackId { get; set; }
        public string memberPan { get; set; }
        public string hdrStatus { get; set; }
        public string weekDay { get; set; }
        public IList<NSEHoldingDataFailed> holdingData { get; set; }
        public string lastWeekDay { get; set; }
    }
    public class NSEHoldingDataFailed
    {
        public string freeBalQty { get; set; }
        public string clientName { get; set; }
        public string totalQty { get; set; }
        public string accountType { get; set; }
        public string nameOfCommodity { get; set; }
        public string unitType { get; set; }
        public string dmat { get; set; }
        public string ucc { get; set; }
        public string securityType { get; set; }
        public string totalPldgQty { get; set; }
        public string pan { get; set; }
        public string recId { get; set; }
        public string isin { get; set; }

    }
    
}
