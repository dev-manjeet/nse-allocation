using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSEAllocation.Models
{
    public class ClsLoginResp
    {
        public string memberCode { get; set; }
        public IList<string> code { get; set; }
        public string loginId { get; set; }
        public string token { get; set; }
        public string status { get; set; }
    }
}
