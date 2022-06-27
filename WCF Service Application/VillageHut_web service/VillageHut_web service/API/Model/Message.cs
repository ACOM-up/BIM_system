using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VillageHut_web_service.API.Model
{
    public class Message
    {
        public string msgContent { get; set; }
        public string msgDate { get; set; }
        public int msgId { get; set; }
    }
}