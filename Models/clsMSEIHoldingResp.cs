using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSEAllocation.Models
{
    //public class clsMSEIHoldingResp
    //{
    //    public IList<string> hdrCode { get; set; }
    //    public string ackId { get; set; }
    //    public string memberPan { get; set; }
    //    public string hdrStatus { get; set; }
    //    public string weekDay { get; set; }
    //    public IList<HoldingData> holdingData { get; set; }
    //    public string lastWeekDay { get; set; }
    //}
    //public class HoldingData
    //{
    //    public string recId { get; set; }
    //    public IList<string> code { get; set; }
    //    public string status { get; set; }

    //}






    public class clsMSEIHoldingResp
    {
        public string memberPan { get; set; }
        public string lastWeekDay { get; set; }
        public string weekDay { get; set; }
        public Holdingdata[] holdingData { get; set; }
        public string hdrStatus { get; set; }
        public string[] hdrCode { get; set; }
        public string ackId { get; set; }
    }

    public class Holdingdata
    {
        public string recId { get; set; }
        public string status { get; set; }
        public string[] code { get; set; }
    }

















}
