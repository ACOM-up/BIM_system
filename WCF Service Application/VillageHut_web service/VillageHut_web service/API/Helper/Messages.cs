using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VillageHut_web_service.API.Model;

namespace VillageHut_web_service.API.Helper
{
    public class Messages
    {
        databaseCacheDataContext dbCache = new databaseCacheDataContext();

        public List<Message> retrieveMessages() {
            var query = from msg in dbCache.tbl_messages
                        where msg.msgIsDone.Equals("false") && DateTime.Compare((DateTime)msg.msgExpDate, DateTime.Now) < 0
                        select new Message()
                        {
                            msgContent = msg.msgContent,
                            msgDate = msg.msgDate.ToString(),
                            msgId = msg.msgId
                        };
            return query.ToList();
        }
    }
}