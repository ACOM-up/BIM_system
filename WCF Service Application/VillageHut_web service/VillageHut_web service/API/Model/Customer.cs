using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VillageHut_web_service.API.Model
{
    public class Customer
    {
        public string cusId { get; set; }
        public string cusName { get; set; }
        public string cusNIC { get; set; }
        public string cusAddress { get; set; }
        public string cusPhone { get; set; }
    }
}