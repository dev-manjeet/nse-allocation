using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSEAllocation.Models
{
    public class ClsReqNseHoldingFileSubmission
    {
        public string memberPan { get; set; }
        public string lastWeekDay { get; set; }
        public string weekDay { get; set; }
        public IList<HoldingData> holdingData { get; set; }
    }
    public class HoldingData
    {
        public string recId { get; set; }
        public string dmat { get; set; }
        public string accountType { get; set; }
        public string ucc { get; set; }
        public string clientName { get; set; }
        public string pan { get; set; }
        public string isin { get; set; }
        public string securityType { get; set; }
        public string nameOfCommodity { get; set; }
        public string unitType { get; set; }
        public string totalPldgQty { get; set; }
        public string freeBalQty { get; set; }
        public string totalQty { get; set; }

    }

  public class HoldingDataFilter
    {
        public string Date { get; set; }
        public string recId { get; set; }
        public string dmat { get; set; }
        public string accountType { get; set; }
        public string ucc { get; set; }
        public string clientName { get; set; }
        public string pan { get; set; }
        public string isin { get; set; }
        public string securityType { get; set; }
        public string nameOfCommodity { get; set; }
        public string unitType { get; set; }
        public string totalPldgQty { get; set; }
        public string freeBalQty { get; set; }
        public string totalQty { get; set; }

    }
    public class datefilter
    {
        public DateTime date { get; set; }
    }
}
