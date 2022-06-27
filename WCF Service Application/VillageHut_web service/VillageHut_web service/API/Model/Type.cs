using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VillageHut_web_service.API.Model
{
    public class Type
    {
        public int typeId { get; set; }
        public string typeSerId { get; set; }
        public string typeName { get; set; }
        public double typePricePItem { get; set; }
        public int typeMaxQty { get; set; }
        public int typeAvailableQty { get; set; }
    }
}