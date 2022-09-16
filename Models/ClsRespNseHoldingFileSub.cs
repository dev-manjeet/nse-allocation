using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSEAllocation.Models
{
    public class ClsRespNseHoldingFileSub
    {
        public IList<string> hdrCode { get; set; }
        public string ackId { get; set; }
        public string memberPan { get; set; }
        public string hdrStatus { get; set; }
        public string weekDay { get; set; }
        public IList<HoldingDataResp> holdingData { get; set; }
        public string lastWeekDay { get; set; }
    }
    public class HoldingDataResp
    {
        public string recId { get; set; }
        public IList<string> code { get; set; }
        public string status { get; set; }

    }
}
