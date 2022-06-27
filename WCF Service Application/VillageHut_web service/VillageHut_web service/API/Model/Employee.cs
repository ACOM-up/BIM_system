using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VillageHut_web_service.API.Model
{
    public class Employee
    {
        public string emId { get; set; }
        public string emNIC { get; set; }
        public string emName { get; set; }
        public string emdAddress { get; set; }
        public string emdPhone { get; set; }
        public double emdSalarey { get; set; }
        public string credUsername { get; set; }
    }
}