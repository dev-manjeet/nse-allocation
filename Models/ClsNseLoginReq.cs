using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSEAllocation.Models
{
    public class ClsNseLoginReq
    {
        public string memberCode { get; set; }
        public string loginId { get; set; }
        public string password { get; set; }
    }
    public class Datefilter
    {
        public int id { get; set; }
        public string date { get; set; }
    }
}
