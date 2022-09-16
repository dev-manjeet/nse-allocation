using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSEAllocation.Models
{
    public class ClsEnquiryStatusReq
    {
        public string version { get; set; }
        public DataEnq data { get; set; }
    }
    public class DataEnq
    {
        public string msgId { get; set; }
        public string dataFormat { get; set; }

    }
}
