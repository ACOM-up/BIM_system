using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VillageHut_web_service.API.Model;

namespace VillageHut_web_service.API.Helper
{
    public class InsertTransaction
    {
        databaseCacheDataContext dbCache = new databaseCacheDataContext();

        public bool IUDTransaction(Model.Transaction recievedTransaction, List<Cart> lstReceivedCartItems) {

            tbl_transaction tableTransaction = new tbl_transaction();

            tableTransaction.transId = recievedTransaction.transId;
            tableTransaction.tbl_customer = (tbl_customer)(from cus in dbCache.tbl_customers where cus.cusId == recievedTransaction.cusId select cus).FirstOrDefault();
            tableTransaction.tbl_employee = (tbl_employee)(from emp in dbCache.tbl_employees where emp.emId == recievedTransaction.empId select emp).FirstOrDefault();
            tableTransaction.transPrice = recievedTransaction.transPrice;
            tableTransaction.transdateTime = recievedTransaction.transDate;
            tableTransaction.transMonth = recievedTransaction.transDate.ToString("MMMM");

            dbCache.tbl_transactions.InsertOnSubmit(tableTransaction);

            foreach (var item in lstReceivedCartItems) {
                var tableCart = new tbl_cart() {
                    serId = item.serId,
                    cartSerType = item.serType,
                    cartSerQty = item.itemQty,
                    cartItemReservedDate = item.reservedDate,
                    cartNoOfDays = item.noOfDays,
                    cartPricePItem = item.itemTotalPrice,
                    cartItemIsReturned = "false",
                    cartItemIsCancelled = "false",
                    transId = item.transId
                };

                dbCache.tbl_carts.InsertOnSubmit(tableCart);
            }


            dbCache.SubmitChanges();

            return true;
        }

    }
}