using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VillageHut_web_service.API.Model;

namespace VillageHut_web_service.API.Helper
{
    public class Logins
    {
        databaseCacheDataContext dbCache = new databaseCacheDataContext();

        public List<Login> login(string username)
        {
            var query = from cred in dbCache.tbl_credentials
                        join emp in dbCache.tbl_employees on cred.emId equals emp.emId
                        where cred.credUsername.Equals(username) && emp.emIsDeleted.Equals("false")
                        select new Login() {
                            empId = emp.emId,
                            empName = emp.emName,
                            empPass = cred.credPassword
                        };
            return query.ToList();
        }

    }
}