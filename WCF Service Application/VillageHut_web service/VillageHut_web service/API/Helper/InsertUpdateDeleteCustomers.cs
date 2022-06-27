using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VillageHut_web_service.API.Model;

namespace VillageHut_web_service.API.Helper
{
    public class InsertUpdateDeleteCustomers
    {
        databaseCacheDataContext dbCache = new databaseCacheDataContext();
        public bool IUDCustomerDetails(Customer recievedRecord, byte mode)
        {

            tbl_customer customerTable = new tbl_customer();

            if (mode == 0) {
                customerTable.cusId = recievedRecord.cusId;
                customerTable.cusName = recievedRecord.cusName;
                customerTable.cusNIC = recievedRecord.cusNIC;
                customerTable.cusPhone = recievedRecord.cusPhone;
                customerTable.cusAddress = recievedRecord.cusAddress;
                customerTable.cusIsDeleted = "false";


                dbCache.tbl_customers.InsertOnSubmit(customerTable);

                dbCache.SubmitChanges();

                return true;
            } else if (mode == 1) {
                tbl_customer cusToUpdate = (from cus in dbCache.tbl_customers
                                            where cus.cusId == recievedRecord.cusId
                                            select cus).FirstOrDefault();


                cusToUpdate.cusName = recievedRecord.cusName;
                cusToUpdate.cusNIC = recievedRecord.cusNIC;
                cusToUpdate.cusAddress = recievedRecord.cusAddress;
                cusToUpdate.cusPhone = recievedRecord.cusPhone;

                dbCache.SubmitChanges();

                return true;
            } else if (mode == 2) {
                tbl_customer cusToUpdate = (from cus in dbCache.tbl_customers
                                            where cus.cusId == recievedRecord.cusId
                                            select cus).FirstOrDefault();

                cusToUpdate.cusIsDeleted = "true";

                dbCache.SubmitChanges();
                return true;
            }

            return false;

        }

    }
}