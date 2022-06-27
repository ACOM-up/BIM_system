using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageHut_BIM_System.Helper
{
    class Transaction
    {
        public string transId { get; set; }
        public DateTime transDate { get; set; }
        public string transPrice { get; set; }
        public string cusId { get; set; }
        public string cusName { get; set; }
        public string cusNIC { get; set; }
        public string empName { get; set; }
        public string empId { get; set; }
        public string empNIC { get; set; }
        public string transMonth { get; set; }
    }
}
