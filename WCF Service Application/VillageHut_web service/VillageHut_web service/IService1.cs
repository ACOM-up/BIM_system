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

    [ServiceContract]
    public interface VillageHut_WebAPI
    {
        [OperationContract]
        List<Login> login(string username);

        [OperationContract]
        List<Category> retrieveCategories();

        [OperationContract]
        List<Service> retrieveAllServices();

        [OperationContract]
        List<Service> searchServices(string input);

        [OperationContract]
        List<OverdueItem> retrieveOverdueItems();

        [OperationContract]
        List<OverdueItem> searchOverdueItems(string input);

        [OperationContract]
        List<UpcommingResv> retrieveUpcommingReservations();

        [OperationContract]
        List<UpcommingResv> searchUpcommingReservations(string input);

        [OperationContract]
        List<Message> retrieveMessages();

        [OperationContract]
        List<API.Model.Type> retrieveTypes(string serId);

        [OperationContract]
        List<Transaction> retrieveTransactions(string year, string month, string text);

        [OperationContract]
        List<TransItem> retrieveItems(string transId);

        [OperationContract]
        List<Employee> retrieveEmployees();

        [OperationContract]
        List<Employee> searchEmployees(string input);

        [OperationContract]
        List<Customer> retrieveCustomers();

        [OperationContract]
        List<Customer> searchCustomers(string input);

        [OperationContract]
        List<ReservedDate> retrieveReserveDates(string serId, string typeName);

        [OperationContract]
        List<Cart> retrieveCart();

        //IDS

        [OperationContract]
        int getMaxEmpAutoGenNo();

        [OperationContract]
        int getMaxCusAutoGenNo();

        [OperationContract]
        int getMaxSerAutoGenNo();

        [OperationContract]
        int getMaxTransAutoGenNo();

        //Insert Update Delete

        [OperationContract]
        bool IUDEmployeeDetails(Employee recievedRecord, string password, byte mode);

        [OperationContract]
        bool IUDCustomerDetails(Customer recievedRecord, byte mode);

        [OperationContract]
        bool IUDServiceDetails(Service serDetails, List<tbl_type> lstTypes, byte mode);

        [OperationContract]
        bool IUDTransaction(API.Model.Transaction recievedTransaction, List<Cart> lstReceivedCartItems);

        [OperationContract]
        bool cancelReserv(int cartId);

        [OperationContract]
        bool isReturned(int cartId);


        ////Procedures
        [OperationContract]
        List<proc_retrieveTransYearsResult> getTransYear();

        [OperationContract]
        List<proc_retrieveCusNICResult> getCustomerNIC();


        ////Duplicate usernames
        [OperationContract]
        int searchDupUsernames(string username);
    }
}