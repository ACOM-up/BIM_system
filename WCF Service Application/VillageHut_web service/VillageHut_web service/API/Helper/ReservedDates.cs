using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VillageHut_web_service.API.Model;

namespace VillageHut_web_service.API.Helper
{
    public class ReservedDates
    {
        databaseCacheDataContext dbCache = new databaseCacheDataContext();

        public List<ReservedDate> retrieveReserveDates(string serId, string typeName) {
            var query = from cart in dbCache.tbl_carts
                        where cart.serId == serId &&
                        cart.cartSerType == typeName && 
                        cart.cartItemIsReturned == "false" &&
                        cart.cartItemIsCancelled == "false" &&
                        (DateTime)cart.cartItemReservedDate >= DateTime.Now.AddDays(-7)
                        select new ReservedDate() {
                            noOfdays = (int)cart.cartNoOfDays,
                            qty = (int)cart.cartSerQty,
                            startingDate = (DateTime)cart.cartItemReservedDate,
                            serId = cart.serId,
                            serType = cart.cartSerType
                        };

            return query.ToList();
        }
    }
}