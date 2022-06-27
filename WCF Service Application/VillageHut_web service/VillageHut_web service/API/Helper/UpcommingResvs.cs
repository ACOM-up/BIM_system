using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VillageHut_web_service.API.Model;

namespace VillageHut_web_service.API.Helper
{
    public class UpcommingResvs
    {
        databaseCacheDataContext dbCache = new databaseCacheDataContext();

        public List<UpcommingResv> retrieveUpcommingReservations()
        {
            var query = from cart in dbCache.tbl_carts
                        join ser in dbCache.tbl_services on cart.serId equals ser.serId
                        join trans in dbCache.tbl_transactions on cart.transId equals trans.transId
                        join cus in dbCache.tbl_customers on trans.cusId equals cus.cusId
                        where DateTime.Compare((DateTime)cart.cartItemReservedDate, DateTime.Now) > 0 && cart.cartItemIsCancelled.Equals("false")
                        select new UpcommingResv()
                        {
                            serId = ser.serId,
                            cusName = cus.cusName,
                            serName = ser.serName,
                            cusNIC = cus.cusNIC,
                            reservedDate = (DateTime)cart.cartItemReservedDate,
                            transId = cart.transId,
                            cartId = cart.cartId
                        };

            return query.ToList();
        }

        public List<UpcommingResv> searchUpcommingReservations(string input)
        {
            if (input.Equals(null)) {
                input = "";
            }
            var query = from cart in dbCache.tbl_carts
                        join ser in dbCache.tbl_services on cart.serId equals ser.serId
                        join trans in dbCache.tbl_transactions on cart.transId equals trans.transId
                        join cus in dbCache.tbl_customers on trans.cusId equals cus.cusId
                        where DateTime.Compare((DateTime)cart.cartItemReservedDate, DateTime.Now) > 0 && cart.cartItemIsCancelled.Equals("false") &&
                        (cus.cusNIC.Contains(input) || 
                        cus.cusName.Contains(input) || 
                        ser.serName.Contains(input) ||
                        ser.serId.Contains(input) ||
                        cart.cartItemReservedDate.ToString().Contains(input))
                        select new UpcommingResv()
                        {
                            serId = ser.serId,
                            cusName = cus.cusName,
                            serName = ser.serName,
                            cusNIC = cus.cusNIC,
                            reservedDate = (DateTime)cart.cartItemReservedDate,
                            transId = cart.transId,
                            cartId = cart.cartId
                        };

            return query.ToList();
        }

        public bool cancelReserv(int cartId) {

            try
            {
                tbl_cart cartTable = (from cart in dbCache.tbl_carts
                                      where cart.cartId.Equals(cartId)
                                      select cart).FirstOrDefault();

                cartTable.cartItemIsCancelled = "true";

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