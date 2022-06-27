using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VillageHut_web_service.API.Model;

namespace VillageHut_web_service.API.Helper
{
    public class Employees
    {
        databaseCacheDataContext dbCache = new databaseCacheDataContext();

        public List<Employee> retrieveEmployees() {
            var query = from emp in dbCache.tbl_employees
                        join empd in dbCache.tbl_employeeDetials on emp.emId equals empd.emId
                        join cred in dbCache.tbl_credentials on emp.emId equals cred.emId
                        where emp.emIsDeleted.Equals("false")
                        select new Employee() {
                            emId = emp.emId,
                            emName = emp.emName,
                            emNIC = emp.emNIC,
                            emdAddress = empd.emdAddress,
                            emdPhone = empd.emdPhone,
                            emdSalarey = (double)empd.emdSalery,
                            credUsername = cred.credUsername
                        };
            return query.ToList();
        }

        public List<Employee> searchEmployees(string input) {
            var query = from emp in dbCache.tbl_employees
                        join empd in dbCache.tbl_employeeDetials on emp.emId equals empd.emId
                        join cred in dbCache.tbl_credentials on emp.emId equals cred.emId
                        where emp.emIsDeleted.Equals("false")&&
                        (emp.emId.Contains(input) || 
                        emp.emName.Contains(input) ||
                        emp.emNIC.Contains(input) ||
                        cred.credUsername.Contains(input))
                        select new Employee()
                        {
                            emId = emp.emId,
                            emName = emp.emName,
                            emNIC = emp.emNIC,
                            emdAddress = empd.emdAddress,
                            emdPhone = empd.emdPhone,
                            emdSalarey = (double)empd.emdSalery,
                            credUsername = cred.credUsername
                        };

            return query.ToList();
        }

        public int searchDupUsernames(string username) {
            int query = (from cred in dbCache.tbl_credentials
                         where cred.credUsername.Equals(username)
                         select cred).Count();
            return query;
        }
    }
}