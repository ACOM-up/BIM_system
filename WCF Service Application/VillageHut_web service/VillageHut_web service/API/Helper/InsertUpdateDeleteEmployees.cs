using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VillageHut_web_service.API.Model;

namespace VillageHut_web_service.API.Helper
{
    public class InsertUpdateDeleteEmployees
    {
        databaseCacheDataContext dbCache = new databaseCacheDataContext();

        public bool IUDEmployeeDetails(Employee recievedRecord, string password, byte mode)
        {

            if (mode == 0) {
                tbl_employee employeeTable = new tbl_employee();
                tbl_employeeDetial emDetailsTable = new tbl_employeeDetial();
                tbl_credential emCredentialTable = new tbl_credential();

                employeeTable.emId = recievedRecord.emId;
                employeeTable.emName = recievedRecord.emName;
                employeeTable.emNIC = recievedRecord.emNIC;
                employeeTable.emIsDeleted = "false";

                emDetailsTable.emId = recievedRecord.emId;
                emDetailsTable.emdSalery = recievedRecord.emdSalarey;
                emDetailsTable.emdPhone = recievedRecord.emdPhone;
                emDetailsTable.emdAddress = recievedRecord.emdAddress;

                emCredentialTable.emId = recievedRecord.emId;
                emCredentialTable.credUsername = recievedRecord.credUsername;
                emCredentialTable.credPassword = password;
                emCredentialTable.credAccess = "partial";

                dbCache.tbl_employees.InsertOnSubmit(employeeTable);
                dbCache.tbl_employeeDetials.InsertOnSubmit(emDetailsTable);
                dbCache.tbl_credentials.InsertOnSubmit(emCredentialTable);

                dbCache.SubmitChanges();

                return true;
            } else if (mode == 1) {
                tbl_employee empToUpdate = (from emp in dbCache.tbl_employees
                                            where emp.emId == recievedRecord.emId
                                            select emp).FirstOrDefault();

                tbl_employeeDetial empDetailsToUpdate = (from empd in dbCache.tbl_employeeDetials
                                                         where empd.emId == recievedRecord.emId
                                                         select empd).FirstOrDefault();

                tbl_credential empCredToUpdate = (from cred in dbCache.tbl_credentials
                                                  where cred.emId == recievedRecord.emId
                                                  select cred).FirstOrDefault();

                empToUpdate.emName = recievedRecord.emName;
                empToUpdate.emNIC = recievedRecord.emNIC;

                empDetailsToUpdate.emdAddress = recievedRecord.emdAddress;
                empDetailsToUpdate.emdPhone = recievedRecord.emdPhone;
                empDetailsToUpdate.emdSalery = recievedRecord.emdSalarey;

                empCredToUpdate.credUsername = recievedRecord.credUsername;
                empCredToUpdate.credPassword = password;

                dbCache.SubmitChanges();

                return true;
            } else if (mode == 2) {
                tbl_employee empToUpdate = (from emp in dbCache.tbl_employees
                                            where emp.emId == recievedRecord.emId
                                            select emp).FirstOrDefault();

                empToUpdate.emIsDeleted = "true";

                dbCache.SubmitChanges();
                return true;
            }

            return false;

        }
    }

}