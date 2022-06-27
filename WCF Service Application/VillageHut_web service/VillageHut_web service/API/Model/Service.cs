using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VillageHut_web_service.API.Model
{
    public class Service
    {
        public string serId { get; set; }
        public string catId { get; set; }
        public string serName { get; set; }
        public string serImg { get; set; }
        public string serIsItem { get; set; }
        public string serIsDeleted { get; set; }
        public string catName { get; set; }
        public string serDesc { get; set; }
    }
}