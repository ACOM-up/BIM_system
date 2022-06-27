using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VillageHut_web_service.API.Model
{
    public class TransItem
    {
        public string serName { get; set; }
        public string serId { get; set; }
        public int serQty { get; set; }
        public string serType { get; set; }
    }
}