using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VillageHut_web_service.API.Model
{
    public class OverdueItem
    {
        public string serId { get; set; }
        public string transId { get; set; }
        public string serName { get; set; }
        public int qty { get; set; }
        public DateTime dueDate { get; set; }
        public string cusName { get; set; }
        public string cusNIC { get; set; }
        public int cartId { get; set; }
    }
}