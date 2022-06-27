using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VillageHut_web_service.API.Model;

namespace VillageHut_web_service.API.Helper
{
    public class OverdueItems
    {
        databaseCacheDataContext dbCache = new databaseCacheDataContext();

        public List<OverdueItem> retrieveOverdueItems() {
            var query = from cart in dbCache.tbl_carts
                        join ser in dbCache.tbl_services on cart.serId equals ser.serId
                        join cat in dbCache.tbl_categories on ser.catId equals cat.catId
                        join trans in dbCache.tbl_transactions on cart.transId equals trans.transId
                        join cus in dbCache.tbl_customers on trans.cusId equals cus.cusId
                        where DateTime.Compare(((DateTime)cart.cartItemReservedDate).AddDays((double)cart.cartNoOfDays), DateTime.Now) < 0 &&
                        cart.cartItemIsReturned.Equals("false") && cat.catSerIsItem.Equals("true") && cart.cartItemIsCancelled.Equals("false")
                        select new OverdueItem()
                        {
                            serId = ser.serId,
                            transId = cart.transId,
                            cusName = cus.cusName,
                            dueDate = (((DateTime)cart.cartItemReservedDate).AddDays((double)cart.cartNoOfDays)),
                            serName = ser.serName,
                            qty = (int)cart.cartSerQty,
                            cusNIC = cus.cusNIC,
                            cartId = cart.cartId
                        };

            return query.ToList();
        }

        public List<OverdueItem> searchOverdueItems(string input)
        {
            if (input.Equals(null)) {
                input = "";
            }
            var query = from cart in dbCache.tbl_carts
                        join ser in dbCache.tbl_services on cart.serId equals ser.serId
                        join cat in dbCache.tbl_categories on ser.catId equals cat.catId
                        join trans in dbCache.tbl_transactions on cart.transId equals trans.transId
                        join cus in dbCache.tbl_customers on trans.cusId equals cus.cusId
                        where DateTime.Compare(((DateTime)cart.cartItemReservedDate).AddDays((double)cart.cartNoOfDays), DateTime.Now) < 0 &&
                        cart.cartItemIsReturned.Equals("false") && 
                        cat.catSerIsItem.Equals("true") && 
                        cart.cartItemIsCancelled.Equals("false") && 
                        (ser.serName.Contains(input) ||
                        ser.serId.Contains(input) ||
                        cart.transId.Contains(input) ||
                        cus.cusName.Contains(input))
                        select new OverdueItem()
                        {
                            serId = ser.serId,
                            transId = cart.transId,
                            cusName = cus.cusName,
                            dueDate = (((DateTime)cart.cartItemReservedDate).AddDays((double)cart.cartNoOfDays)),
                            serName = ser.serName,
                            qty = (int)cart.cartSerQty,
                            cusNIC = cus.cusNIC,
                            cartId = cart.cartId
                        };

            return query.ToList();
        }

        public bool isReturned(int cartId) {
            try
            {
                tbl_cart cartTable = (from cart in dbCache.tbl_carts
                                      where cart.cartId.Equals(cartId)
                                      select cart).FirstOrDefault();

                cartTable.cartItemIsReturned = "true";

                dbCache.SubmitChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}