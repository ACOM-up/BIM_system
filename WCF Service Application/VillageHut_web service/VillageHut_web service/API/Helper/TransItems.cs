using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VillageHut_web_service.API.Model;

namespace VillageHut_web_service.API.Helper
{
    public class TransItems
    {
        databaseCacheDataContext dbCache = new databaseCacheDataContext();

        public List<TransItem> retrieveItems(string transId)
        {
            var query = from cart in dbCache.tbl_carts
                        join ser in dbCache.tbl_services on cart.serId equals ser.serId
                        join trans in dbCache.tbl_transactions on cart.transId equals trans.transId
                        where trans.transId.Equals(transId)
                        select new TransItem()
                        {
                            serId = ser.serId,
                            serName = ser.serName,
                            serQty = (int)cart.cartSerQty,
                            serType = cart.cartSerType,
                        };
            return query.ToList();
        }
    }
}