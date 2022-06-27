using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VillageHut_web_service.API.Model;

namespace VillageHut_web_service.API.Helper
{
    public class Carts
    {
        databaseCacheDataContext dbCache = new databaseCacheDataContext();

        public List<Cart> retrieveCart() {
            var query = from cart in dbCache.tbl_carts
                        join ser in dbCache.tbl_services on cart.serId equals ser.serId
                        select new Cart() {
                            isCancelled = cart.cartItemIsCancelled,
                            isReturned = cart.cartItemIsReturned,
                            itemQty = (int)cart.cartSerQty,
                            itemTotalPrice = (double)cart.cartPricePItem,
                            noOfDays = (int)cart.cartNoOfDays,
                            reservedDate = (DateTime)cart.cartItemReservedDate,
                            serId = cart.serId,
                            serType = cart.cartSerType,
                            transId = cart.transId,
                            serName = ser.serName
                        };
            return query.ToList();
        }
    }
}