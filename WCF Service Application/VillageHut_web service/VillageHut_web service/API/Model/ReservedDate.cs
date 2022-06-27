using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VillageHut_web_service.API.Model
{
    public class ReservedDate
    {
        public DateTime startingDate { get; set; }
        public int noOfdays { get; set; }
        public int qty { get; set; }
        public string serId { get; set; }
        public string serType { get; set; }
    }
}