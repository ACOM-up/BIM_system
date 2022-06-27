using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VillageHut_web_service.API.Model;

namespace VillageHut_web_service.API.Helper
{
    public class Customers
    {
        databaseCacheDataContext dbCache = new databaseCacheDataContext();

        public List<Customer> retrieveCustomers()
        {
            var query = from cus in dbCache.tbl_customers
                        where cus.cusIsDeleted.Equals("false")
                        select new Customer() {
                            cusId = cus.cusId,
                            cusName = cus.cusName,
                            cusNIC = cus.cusNIC,
                            cusAddress = cus.cusAddress,
                            cusPhone = cus.cusPhone
                        };
            return query.ToList();
        }



        public List<Customer> searchCustomers(string input)
        {
            var query = from cus in dbCache.tbl_customers
                        where cus.cusIsDeleted.Equals("false") &&
                        (cus.cusId.Contains(input) ||
                        cus.cusName.Contains(input) ||
                        cus.cusNIC.Contains(input))
                        select new Customer() {
                            cusId = cus.cusId,
                            cusName = cus.cusName,
                            cusNIC = cus.cusNIC,
                            cusAddress = cus.cusAddress,
                            cusPhone = cus.cusPhone
                        };
            return query.ToList();
        }

    }
}