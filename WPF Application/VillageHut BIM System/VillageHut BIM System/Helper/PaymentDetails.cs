using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageHut_BIM_System.Helper
{
    class PaymentDetails
    {
        public string transId { get; set; }
        public double discount { get; set; }
        public double totalPrice { get; set; }
        public double pricePayed { get; set; }
        public double balance { get; set; }
    }
}
