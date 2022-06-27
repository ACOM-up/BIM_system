using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VillageHut_web_service.API.Model;

namespace VillageHut_web_service.API.Helper
{
    public class Transactions
    {
        databaseCacheDataContext dbCache = new databaseCacheDataContext();

        public List<Transaction> retrieveTransactions(string year, string month, string text)
        {
            if (year.Equals("Please Select Year")) {
                year = "";
            }

            if (month.Equals("Please Select Month")) {
                month = "";
            }

            if (text.Equals("Type here to search...")) {
                text = "";
            }

            var query = from trans in dbCache.tbl_transactions
                        join emp in dbCache.tbl_employees on trans.emId equals emp.emId
                        join cus in dbCache.tbl_customers on trans.cusId equals cus.cusId
                        where (trans.transId.Contains(text)||
                        cus.cusName.Contains(text)||
                        cus.cusNIC.Contains(text)||
                        emp.emName.Contains(text)||
                        emp.emNIC.Contains(text))&&
                        (trans.transdateTime.ToString().Contains(year)&&
                        trans.transMonth.ToString().Contains(month))
                        select new Transaction()
                        {
                            cusName = cus.cusName,
                            cusNIC = cus.cusNIC,
                            cusId = cus.cusId,
                            empName = emp.emName,
                            empNIC = emp.emNIC,
                            transId = trans.transId,
                            transDate = (DateTime)trans.transdateTime,
                            transPrice = (double)trans.transPrice
                        };
            return query.ToList();
        }
    }

}