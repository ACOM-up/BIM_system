using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VillageHut_web_service.API.Model
{
    public class Cart
    {
        public string transId { get; set; }
        public string serId { get; set; }
        public string serName { get; set; }
        public string serType { get; set; }
        public int itemQty { get; set; }
        public DateTime reservedDate { get; set; }
        public int noOfDays { get; set; }
        public double itemTotalPrice { get; set; }
        public string isReturned { get; set; }
        public string isCancelled { get; set; }
    }
}