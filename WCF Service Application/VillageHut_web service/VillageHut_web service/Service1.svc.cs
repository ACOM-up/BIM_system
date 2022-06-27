using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using VillageHut_web_service.API.Model;
using VillageHut_web_service.API.Helper;


namespace VillageHut_web_service
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class Service1 : VillageHut_WebAPI
    {
        databaseCacheDataContext dbCache = new databaseCacheDataContext();

        public List<Login> login(string username)
        {
            return new Logins().login(username);
        }

        public List<Category> retrieveCategories()
        {
            return new Categories().retrieveCategories();
        }

        public List<Service> retrieveAllServices()
        {
            return new Services().retrieveAllServices();
        }

        public List<Service> searchServices(string input)
        {
            return new Services().searchServices(input);
        }

        public List<OverdueItem> retrieveOverdueItems()
        {
            return new OverdueItems().retrieveOverdueItems();
        }

        public List<OverdueItem> searchOverdueItems(string input)
        {
            return new OverdueItems().searchOverdueItems(input);
        }

        public List<UpcommingResv> retrieveUpcommingReservations()
        {
            return new UpcommingResvs().retrieveUpcommingReservations();
        }

        public List<UpcommingResv> searchUpcommingReservations(string input)
        {
            return new UpcommingResvs().searchUpcommingReservations(input);
        }

        public List<Message> retrieveMessages()
        {
            return new Messages().retrieveMessages();
        }

        public List<API.Model.Type> retrieveTypes(string serId)
        {
            return new Types().retrieveTypes(serId);
        }

        public List<Transaction> retrieveTransactions(string year, string month, string text)
        {
            return new Transactions().retrieveTransactions(year, month, text);
        }

        public List<TransItem> retrieveItems(string transId)
        {
            return new TransItems().retrieveItems(transId);
        }

        public List<Employee> retrieveEmployees()
        {
            return new Employees().retrieveEmployees();
        }

        public List<Employee> searchEmployees(string input)
        {
            return new Employees().searchEmployees(input);
        }

        public List<Customer> retrieveCustomers()
        {
            return new Customers().retrieveCustomers();
        }

        public List<Customer> searchCustomers(string input)
        {
            return new Customers().searchCustomers(input);
        }

        public List<ReservedDate> retrieveReserveDates(string serId, string typeName)
        {
            return new ReservedDates().retrieveReserveDates(serId, typeName);
        }

        public List<Cart> retrieveCart()
        {
            return new Carts().retrieveCart();
        }



        //IDS
        public int getMaxEmpAutoGenNo()
        {
            int query = (from emp in dbCache.tbl_employees
                         select emp.emAutoId).Max();
            return query;
        }

        public int getMaxCusAutoGenNo()
        {
            int query = (from cus in dbCache.tbl_customers
                         select cus.cusAutoId).Max();
            return query;
        }

        public int getMaxSerAutoGenNo()
        {
            int query = (from ser in dbCache.tbl_services
                         select ser.serAutoId).Max();
            return query;
        }

        public int getMaxTransAutoGenNo() {
            int query = (from trans in dbCache.tbl_transactions
                         select trans.transAutoId).Max();
            return query;
        }



        //Insert, Update, Delete
        public bool IUDEmployeeDetails(Employee recievedRecord, string password, byte mode)
        {
            return new InsertUpdateDeleteEmployees().IUDEmployeeDetails(recievedRecord, password, mode);
        }

        public bool IUDCustomerDetails(Customer recievedRecord, byte mode)
        {
            return new InsertUpdateDeleteCustomers().IUDCustomerDetails(recievedRecord, mode);
        }

        public bool IUDServiceDetails(Service serDetails, List<tbl_type> lstTypes, byte mode)
        {
            return new InsertUpdateDeleteServices().IUDServiceDetails(serDetails, lstTypes, mode);
        }

        public bool IUDTransaction(API.Model.Transaction recievedTransaction, List<Cart> lstReceivedCartItems) {
            return new InsertTransaction().IUDTransaction(recievedTransaction, lstReceivedCartItems);
        }

        public bool cancelReserv(int cartId) {
            return new UpcommingResvs().cancelReserv(cartId);
        }

        public bool isReturned(int cartId) {
            return new OverdueItems().isReturned(cartId);
        }


        ////Procedures

        public List<proc_retrieveTransYearsResult> getTransYear()
        {
            return dbCache.proc_retrieveTransYears().ToList();
        }

        public List<proc_retrieveCusNICResult> getCustomerNIC() {
            return dbCache.proc_retrieveCusNIC().ToList();
        }




        ////Duplicate usernames
        public int searchDupUsernames(string username)
        {
            return new Employees().searchDupUsernames(username);
        }
    }
}

