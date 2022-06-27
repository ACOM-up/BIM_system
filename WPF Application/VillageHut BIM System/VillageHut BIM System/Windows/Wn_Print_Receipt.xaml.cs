using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VillageHut_BIM_System.VillageHut_ServerApplication;
using VillageHut_BIM_System.Helper;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.Win32;


namespace VillageHut_BIM_System.Windows
{
    /// <summary>
    /// Interaction logic for Wn_Print_Receipt.xaml
    /// </summary>
    public partial class Wn_Print_Receipt : Window
    {

        VillageHut_WebAPIClient stdClient = new VillageHut_WebAPIClient();

        //dgInventory selected item
        VillageHut_ServerApplication.Service selectedRecord;

        //selected items's types
        List<VillageHut_ServerApplication.Type> lstTypes;

        //seleted type's recerved dates + noof dates + recereved qty from "tbl_cart"
        List<VillageHut_ServerApplication.ReservedDate> lstReservedDates;

        //seleted type's other details from "tbl_type" -> to get availabe qty
        VillageHut_ServerApplication.Type SelectedType;

        //Keeping track of total qty and dates
        List<Helper.DateQty> lstDateQty;

        //cart list
        List<VillageHut_ServerApplication.Cart> lstCartItem;

        //Customers list
        List<VillageHut_ServerApplication.Customer> lstAllCustomers;

        //Transaction obj
        VillageHut_ServerApplication.Transaction transactionDetails;

        public Wn_Print_Receipt()
        {
            InitializeComponent();
            Loaded += Wn_Print_Receipt_Loaded;
        }





        ////Custom Functions







        //Load the inventory data grid
        private void loadInventoryGrid()
        {
            dgInventory.ItemsSource = stdClient.retrieveAllServices().ToList();
            recordCount();
        }






        //Clear item configuration fields
        private void clearConfigureFields()
        {
            dgInventory.SelectedItem = null;

            dpConfResDate.SelectedDate = null;
            dpConfResDate.DisplayDate = DateTime.Today;

            comboConfNoOfDays.Items.Clear();
            comboConfSerQTY.Items.Clear();
            comboConfSerType.Items.Clear();
            comboConfSerType.Items.Add("Please Select a Service");
            comboConfSerType.SelectedIndex = 0;

            txtConfPricePItem.Clear();
            txtConfSerId.Clear();
        }






        //Load the filter by category combobox
        private void loadFilterCatCombo()
        {
            List<VillageHut_ServerApplication.Category> lstCategories = new List<VillageHut_ServerApplication.Category>();
            lstCategories = stdClient.retrieveCategories().ToList();

            comboFilterCat.Items.Add("Please Select a Category");
            foreach (var item in lstCategories) {
                comboFilterCat.Items.Add(item.catName);
            }
            comboFilterCat.SelectedIndex = 0;
        }






        //No of records found count
        private void recordCount()
        {
            if (dgInventory.Items.Count > 1) {
                lblSerRecCount.Text = dgInventory.Items.Count + " Records Found";
            } else if (dgInventory.Items.Count == 1) {
                lblSerRecCount.Text = dgInventory.Items.Count + " Record Found";
            } else if (dgInventory.Items.Count == 0) {
                lblSerRecCount.Text = "No Records Found";
            }
        }







        //Reset and load the datepicker
        private void initializeDatePicker()
        {
            dpConfResDate.SelectedDate = null;
            dpConfResDate.DisplayDate = DateTime.Today;

            //Reset the datepicker
            dpConfResDate.BlackoutDates.Clear();

            //Restrinct selecting past dates in the configuraion
            CalendarDateRange cdr = new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1));
            dpConfResDate.BlackoutDates.Add(cdr);
            dpConfResDate.DisplayDateStart = DateTime.Today;

            //Restrinct selecting dates above 6 months ahead in the configuraion
            CalendarDateRange cdr1 = new CalendarDateRange(DateTime.Today.AddMonths(6), DateTime.MaxValue);
            dpConfResDate.BlackoutDates.Add(cdr1);
        }







        //Load the configuration fields
        private void loadConfigurationFields()
        {
            if (dgInventory.SelectedIndex >= 0) {
                //initializing global variables
                selectedRecord = (VillageHut_ServerApplication.Service)dgInventory.SelectedItem;
                lstTypes = stdClient.retrieveTypes(selectedRecord.serId).ToList();

                //Load the Service Id
                txtConfSerId.Text = selectedRecord.serId;

                //Load the types
                comboConfSerType.Items.Clear();
                foreach (var item in lstTypes) {
                    comboConfSerType.Items.Add(item.typeName);
                }
                comboConfSerType.SelectedIndex = 0;

                //Load the datepicker
                loadConfDatePicker();
            }
        }







        //Load DatePicker
        private void loadConfDatePicker()
        {
            initializeDatePicker();

            //initializing global variable
            lstDateQty = new List<Helper.DateQty>();

            //Sorting list according to the date ASC
            //lstDateQty.Sort((x, y) => x.date.CompareTo(y.date));


            int index = -1;

            //initializing global variable
            //Retrieving reservedDates, qty and no of days
            lstReservedDates = stdClient.retrieveReserveDates(txtConfSerId.Text, comboConfSerType.SelectedValue.ToString()).ToList();

            //Finding all the details from already retrieved types from "tbl_types" without a database call
            SelectedType = lstTypes.Find(x => x.typeName == comboConfSerType.SelectedValue.ToString());

            //disabling the qty 0 dates from the database
            foreach (var item in lstReservedDates) {

                //if the list is null, adding gthe first list items
                if (lstDateQty.Count == 0) {
                    for (int i = 0; i < item.noOfdays; i++) {
                        lstDateQty.Add(new Helper.DateQty() {
                            date = item.startingDate.AddDays(i),
                            qty = item.qty,
                            serId = txtConfSerId.Text,
                            sertype = comboConfSerType.SelectedValue.ToString()
                        });
                    }
                } else {

                    for (int i = 0; i < item.noOfdays; i++) {
                        //searching for existing dates
                        index = lstDateQty.FindIndex(a => a.date == item.startingDate.AddDays(i) && a.serId.Equals(item.serId) && a.sertype.Equals(item.serType));

                        //if not found, add
                        if (index == -1) {
                            lstDateQty.Add(new Helper.DateQty() {
                                date = item.startingDate.AddDays(i),
                                qty = item.qty,
                                serId = txtConfSerId.Text,
                                sertype = comboConfSerType.SelectedValue.ToString()
                            });
                            //if found update the qty
                        } else {

                            foreach (var item1 in lstDateQty) {
                                if (item1.date == item.startingDate.AddDays(i) && item.serId.Equals(item1.serId) && item.serType.Equals(item1.sertype)) {
                                    item1.qty += item.qty;
                                    break;
                                }
                            }

                        }
                    }

                }

            }

            //disabling the qty 0 dates from the selected cart items
            foreach (var item in lstCartItem) {

                //if the list is null, adding gthe first list items
                if (lstDateQty.Count == 0) {
                    for (int i = 0; i < item.noOfDays; i++) {
                        lstDateQty.Add(new Helper.DateQty() {
                            date = item.reservedDate.AddDays(i),
                            qty = item.itemQty,
                            serId = txtConfSerId.Text,
                            sertype = comboConfSerType.SelectedValue.ToString()
                        });
                    }
                } else {

                    for (int i = 0; i < item.noOfDays; i++) {
                        //searching for existing dates
                        index = lstDateQty.FindIndex(a => a.date == item.reservedDate.AddDays(i) && a.serId.Equals(item.serId) && a.sertype.Equals(item.serType));

                        //if not found, add
                        if (index == -1) {
                            //Check wheather the serId and the type is matching before inserting from the cart datagrid
                            lstDateQty.Add(new Helper.DateQty() {
                                date = item.reservedDate.AddDays(i),
                                qty = item.itemQty,
                                serId = item.serId,
                                sertype = item.serType
                            });

                            //if found update the qty
                        } else {

                            foreach (var item1 in lstDateQty) {
                                if (item1.date == item.reservedDate.AddDays(i) && item.serId.Equals(item1.serId) && item.serType.Equals(item1.sertype)) {
                                    item1.qty += item.itemQty;
                                    break;
                                }
                            }

                        }
                    }

                }

            }

            //Blacking out the dates which have no qty available
            foreach (var item in lstDateQty) {
                if (item.qty == SelectedType.typeMaxQty) {
                    CalendarDateRange cdr1 = new CalendarDateRange(item.date, item.date);
                    dpConfResDate.BlackoutDates.Add(cdr1);
                }
                //Console.WriteLine(item.date + "     " + item.qty + "    " + item.serId + "     " + item.sertype);
            }

            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();

        }







        //Load no of days Combobox
        private void loadConfNoOfDays()
        {
            if (dgInventory.SelectedIndex >= 0) {
                int max = 7;
                foreach (var item in lstDateQty) {
                    if (((DateTime)dpConfResDate.SelectedDate).AddDays(7) >= item.date && item.qty == SelectedType.typeMaxQty && item.serId.Equals(txtConfSerId.Text) && item.sertype.Equals(comboConfSerType.SelectedValue.ToString())) {
                        for (int i = 1; i <= 7; i++) {
                            if (((DateTime)dpConfResDate.SelectedDate).AddDays(i) == item.date) {
                                //Console.WriteLine(((DateTime)dpConfResDate.SelectedDate).AddDays(i)+"");
                                //Console.WriteLine(item.date);
                                max = i;
                                goto Result;
                            }
                        }
                    }
                }

                Result:
                {
                    comboConfNoOfDays.Items.Clear();
                    for (int i = 1; i <= max; i++) {
                        comboConfNoOfDays.Items.Add(i + "");
                    }
                    comboConfNoOfDays.SelectedIndex = 0;
                }
            }

        }







        //Load max qty
        private void loadConfqty()
        {
            int reservedQTY = 0, max = 0;

            //Getting the max qty in the selected date range
            for (int i = 0; i < comboConfNoOfDays.SelectedIndex + 1; i++) {
                foreach (var item in lstDateQty) {
                    if (((DateTime)dpConfResDate.SelectedDate).AddDays(i) == item.date && item.serId.Equals(txtConfSerId.Text) && item.sertype.Equals(comboConfSerType.SelectedValue.ToString())) {
                        reservedQTY = item.qty;
                        break;
                    }
                }
                if (reservedQTY >= max) {
                    max = reservedQTY;
                }
            }

            comboConfSerQTY.Items.Clear();
            for (int i = 1; i <= SelectedType.typeMaxQty - max; i++) {
                comboConfSerQTY.Items.Add(i + "");
            }
            comboConfSerQTY.SelectedIndex = 0;
        }







        //Load Conf price per item
        private void loadConfPrice()
        {
            txtConfPricePItem.Text = (SelectedType.typePricePItem).ToString();
        }


        //END Load the configuration fields







        //Load the cart dataGrid and Add items
        private void loadCartDatagrid()
        {
            bool isFound = false;

            VillageHut_ServerApplication.Cart cartItem = new VillageHut_ServerApplication.Cart() {
                transId = txtTransId.Text,
                serId = txtConfSerId.Text,
                serType = comboConfSerType.SelectedValue.ToString(),
                itemQty = int.Parse(comboConfSerQTY.SelectedValue.ToString()),
                reservedDate = DateTime.Parse(dpConfResDate.SelectedDate.ToString()),
                noOfDays = int.Parse(comboConfNoOfDays.SelectedValue.ToString()),
                itemTotalPrice = double.Parse(txtConfPricePItem.Text) * int.Parse(comboConfSerQTY.SelectedValue.ToString()),
                isCancelled = "false",
                isReturned = "false",
                serName = selectedRecord.serName
            };


            if (!dgCart.HasItems) {
                lstCartItem.Add(cartItem);
            } else {
                foreach (var item in lstCartItem) {
                    if (item.serId == cartItem.serId && item.serType == cartItem.serType && item.reservedDate == DateTime.Parse(dpConfResDate.SelectedDate.ToString())) {
                        item.itemQty = int.Parse(comboConfSerQTY.SelectedValue.ToString()) + (item.itemQty);
                        item.noOfDays = int.Parse(comboConfNoOfDays.SelectedValue.ToString());
                        item.itemTotalPrice = double.Parse(txtConfPricePItem.Text) * int.Parse(comboConfSerQTY.SelectedValue.ToString());
                        isFound = true;
                        break;
                    }
                }

                if (!isFound) {
                    lstCartItem.Add(cartItem);
                }
            }

            try {
                dgCart.ItemsSource = lstCartItem.ToList();
            } catch (Exception) {

            }

            txtCartTotalPrice.Text = getTotalPriceFromDgCart().ToString();

        }






        //Getting total price from cart
        private double getTotalPriceFromDgCart()
        {
            double sum = 0;
            foreach (var item in lstCartItem) {
                sum += item.itemTotalPrice;
            }
            return sum;
        }







        //Load customer NICs
        private void loadCustomerNIC()
        {

            lstAllCustomers = stdClient.retrieveCustomers().ToList();

            comboCusNIC.Items.Clear();
            comboCusNIC.Items.Add("Enter the NIC...");
            foreach (var item in lstAllCustomers) {
                comboCusNIC.Items.Add(item.cusNIC);
            }
            comboCusNIC.SelectedIndex = 0;
        }







        //Save Customer
        private bool saveCustomer()
        {
            try {
                string customerNIC = "";

                //Detemining wheather it's an insertion or an updation
                byte addOrUp = 0;
                string nextCusId = "CS00" + (stdClient.getMaxCusAutoGenNo() + 1);

                if (!nextCusId.Equals(txtCusId.Text)) {
                    addOrUp = 1;
                }

                //Gettig NIC
                try {
                    customerNIC = comboCusNIC.SelectedValue.ToString();
                } catch (Exception) {
                    customerNIC = comboCusNIC.Text;
                }



                stdClient.IUDCustomerDetails(new Customer() {
                    cusId = txtCusId.Text,
                    cusName = txtCusName.Text,
                    cusNIC = customerNIC,
                    cusAddress = txtCusAddress.Text,
                    cusPhone = txtCusPhone.Text
                }, addOrUp);

                return true;
            } catch (Exception) {
                return false;
            }
        }







        //load customer details when NIC is chooses
        private void loadCusDetails()
        {
            try {
                foreach (var item in lstAllCustomers) {
                    if (item.cusNIC.Equals(comboCusNIC.SelectedValue.ToString())) {
                        txtCusId.Text = item.cusId;
                        txtCusName.Text = item.cusName;
                        txtCusPhone.Text = item.cusPhone;
                        txtCusAddress.Text = item.cusAddress;
                    }
                }

            } catch (Exception) {

            }
        }








        //Clear customer fields
        private void clearCusFields()
        {
            txtCusId.Text = "CS00" + (stdClient.getMaxCusAutoGenNo() + 1);
            txtCusName.Clear();
            txtCusPhone.Clear();
            txtCusAddress.Clear();
            comboCusNIC.SelectedIndex = 0;
        }








        //Load Print Receipt Section
        private void loadTransactionDetails()
        {
            try {
                if (txtCartTotalPrice.Text.Equals(null) || txtCartTotalPrice.Text.Equals("") || txtCartTotalPrice.Text.Equals("0")) {
                    txtTransTotalPrice.Text = "0.0";
                } else {
                    txtTransTotalPrice.Text = txtCartTotalPrice.Text;
                }
                txtTransDiscount.Text = "0.0";
                txtTransBalance.Text = "0.0";
                txtTransPricePayed.Text = "0.0";
            } catch (Exception) {

            }
        }








        //Save transaction
        private bool saveTransaction()
        {
            VillageHut_ServerApplication.Transaction tempTransaction;

            tempTransaction = new VillageHut_ServerApplication.Transaction() {
                cusId = txtCusId.Text,

                empId = MainWindow.logedEmpDetails.empId,
                transId = txtTransId.Text,
                transPrice = double.Parse(txtTransTotalPrice.Text),
                transDate = DateTime.Now,
                transMonth = DateTime.Now.ToString("MMMM"),
                cusName = "",
                cusNIC = "",
                empName = "",
                empNIC = ""
            };
            transactionDetails = tempTransaction;

            List<VillageHut_ServerApplication.Cart> lstdgCart = new List<VillageHut_ServerApplication.Cart>();


            foreach (var item in dgCart.Items.OfType<VillageHut_ServerApplication.Cart>().ToList()) {
                lstdgCart.Add(item);
            }


            List<VillageHut_ServerApplication.tbl_cart> lstTempCart = new List<VillageHut_ServerApplication.tbl_cart>();

            foreach (var item in lstdgCart) {
                lstTempCart.Add(new VillageHut_ServerApplication.tbl_cart() {
                    cartItemIsCancelled = "false",
                    cartItemIsReturned = "false",
                    cartItemReservedDate = item.reservedDate,
                    cartNoOfDays = item.noOfDays,
                    cartPricePItem = item.itemTotalPrice,
                    cartSerQty = item.itemQty,
                    cartSerType = item.serType,
                    serId = item.serId,
                    transId = item.transId
                });
            }

            try {
                bool result = stdClient.IUDTransaction(tempTransaction, lstdgCart.ToArray());

                if (result) {
                    MessageBox.Show("Transaction Saved..!");
                    return true;
                } else {
                    MessageBox.Show("Error Saving...!");
                    return false;
                }

            } catch (Exception ex) {
                return false;
            }

        }




        /// <summary>
        /// START VALIDATION
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>






        private bool isEmpty(string[] fieldsArray)
        {
            for (int i = 0; i < fieldsArray.Length; i++) {
                if (fieldsArray[i].Equals("") || fieldsArray[i].Equals(null)) {
                    return true;
                }
            }
            return false;
        }


        private bool isValiedNIC()
        {
            try {
                string nicFormat = @"^\d{9}[V]$";
                bool r = Regex.IsMatch(comboCusNIC.Text, nicFormat);
                return r;
            } catch (Exception) {
                string nicFormat = @"^\d{9}[V]$";
                return Regex.IsMatch(comboCusNIC.SelectedValue.ToString(), nicFormat);
            }
        }


        private bool isValiedPhone()
        {
            string phoneFormat = @"^\d{10}$";
            return Regex.IsMatch(txtCusPhone.Text, phoneFormat);
        }


        private bool isDouble(string[] fieldsArray)
        {
            bool result = true;
            string doubleFormat = @"^[+]?\d+(\.\d+)?$";

            for (int i = 0; i < fieldsArray.Length; i++) {
                if (!Regex.IsMatch(fieldsArray[i], doubleFormat)) {
                    result = false;
                    break;
                }
            }
            return result;
        }


        private bool validateAllFields()
        {
            string nic = "";

            try {
                nic = comboCusNIC.Text;
            } catch (Exception) {
                nic = comboCusNIC.SelectedValue.ToString();
            }

            string[] A = {
                txtCusPhone.Text,
                txtCusAddress.Text,
                txtCusId.Text,
                txtCusName.Text,
                nic,
                txtTransBalance.Text,
                txtTransDiscount.Text,
                txtTransId.Text,
                txtTransPricePayed.Text,
                txtTransTotalPrice.Text
            };
            if (!isEmpty(A)) {
                if (isValiedNIC()) {
                    if (isValiedPhone()) {
                        string[] B = {
                            txtTransBalance.Text,
                            txtTransDiscount.Text,
                            txtTransPricePayed.Text,
                            txtTransTotalPrice.Text
                        };
                        if (isDouble(B)) {
                            if (double.Parse(txtTransPricePayed.Text) >= double.Parse(txtTransTotalPrice.Text)) {
                                if (!txtCartTotalPrice.Text.Equals("0.0") && !txtCartTotalPrice.Text.Equals("0") && !txtCartTotalPrice.Text.Equals("") && !txtCartTotalPrice.Text.Equals(null)) {
                                    return true;
                                } else {
                                    MessageBox.Show("Please select items..!");
                                    return false;
                                }
                            } else {
                                MessageBox.Show("Insufficient payed price..!");
                                return false;
                            }
                        } else {
                            MessageBox.Show("Invalied Prices..!");
                            return false;
                        }
                    } else {
                        MessageBox.Show("Invalied phone number..!");
                        return false;
                    }
                } else {
                    MessageBox.Show("Invalied NIC..!");
                    return false;
                }
            } else {
                MessageBox.Show("One or more input fields are empty..!");
                return false;
            }

        }







        /// <summary>
        /// END VALIDATION
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>












        ////END Custom Functions












        //DEFAULT EVENTS----------------------------------------------------------------------------------------------------------------








        private void Wn_Print_Receipt_Loaded(object sender, RoutedEventArgs e)
        {
            loadInventoryGrid();
            loadFilterCatCombo();
            loadCustomerNIC();
            loadTransactionDetails();

            //Configuration combo initialize
            comboConfSerType.Items.Add("Please Select a Service");

            //Transaction id auto gen
            try {
                txtTransId.Text = "TR00" + (stdClient.getMaxTransAutoGenNo() + 1);
            } catch (Exception) {
                txtTransId.Text = "TR001";
            }


            //Customer id autogen
            txtCusId.Text = "CS00" + (stdClient.getMaxCusAutoGenNo() + 1);

            //initializing global variable 
            lstCartItem = new List<VillageHut_ServerApplication.Cart>();

            //initializing global variable 
            transactionDetails = new VillageHut_ServerApplication.Transaction();


            initializeDatePicker();
        }






        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }





        private void btnMinimize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }





        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }





        private void btnRefreshServ_Click(object sender, RoutedEventArgs e)
        {
            loadInventoryGrid();
            clearConfigureFields();
            recordCount();
            txtFilterServ.Text = "Type here to search...";
            comboFilterCat.SelectedIndex = 0;
        }





        private void btnViewServiceDetails_Click(object sender, RoutedEventArgs e)
        {
            bool isWindowOpen = false;

            foreach (Window w in Application.Current.Windows) {
                if (w is Wn_AddViewService) {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            if (!isWindowOpen) {
                Wn_AddViewService winAddViewEmp = new Wn_AddViewService((VillageHut_ServerApplication.Service)dgInventory.SelectedItem);
                winAddViewEmp.Show();
            }
        }






        private void btnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (dpConfResDate.SelectedDate == null) {
                    MessageBox.Show("Please select a valied date..!");
                    return;
                }

                loadCartDatagrid();
                txtTransTotalPrice.Text = getTotalPriceFromDgCart().ToString();
                clearConfigureFields();
            } catch (Exception) {

            }
        }





        private void btnAddToPrinterQueue_Click(object sender, RoutedEventArgs e)
        {

        }





        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintNow_Click(object sender, RoutedEventArgs e)
        {
            if (!validateAllFields()) {
                return;
            }

            if (saveCustomer() && saveTransaction()) {

            } else {
                MessageBox.Show("Saving failed..!");
                return;
            }


            //Current Customer Creation
            string customerNIC = "";
            try {
                customerNIC = comboCusNIC.SelectedValue.ToString();
            } catch (Exception) {
                customerNIC = comboCusNIC.Text;
            }

            VillageHut_ServerApplication.Customer customerDetails = new VillageHut_ServerApplication.Customer() {
                cusId = txtCusId.Text,
                cusAddress = txtCusAddress.Text,
                cusName = txtCusName.Text,
                cusPhone = txtCusPhone.Text,
                cusNIC = customerNIC
            };



            //Current Employee Creation
            VillageHut_ServerApplication.Employee employeeDetails = new VillageHut_ServerApplication.Employee() {
                emId = MainWindow.logedEmpDetails.empId,
                emName = MainWindow.logedEmpDetails.empName
            };

            //Current PaymentDetails Creation
            PaymentDetails paymentDetails = new PaymentDetails() {
                transId = transactionDetails.transId,
                balance = double.Parse(txtTransBalance.Text),
                discount = double.Parse(txtTransDiscount.Text),
                totalPrice = double.Parse(txtTransTotalPrice.Text),
                pricePayed = double.Parse(txtTransPricePayed.Text)
            };

            InvoicePdfGenerator pdf = new InvoicePdfGenerator();
            pdf.generateReceipt(paymentDetails, lstCartItem, customerDetails, employeeDetails);

            lstCartItem.Clear();
            dgCart.ItemsSource = null;
            txtCartTotalPrice.Clear();
            clearConfigureFields();
            clearCusFields();
            loadTransactionDetails();
            txtTransId.Text = "TR00" + (stdClient.getMaxTransAutoGenNo() + 1);
        }






        private void btnPrintQuotation_Click(object sender, RoutedEventArgs e)
        {
            //Current Customer Creation
            string customerNIC = "";
            try {
                customerNIC = comboCusNIC.SelectedValue.ToString();
            } catch (Exception) {
                customerNIC = comboCusNIC.Text;
            }

            VillageHut_ServerApplication.Customer customerDetails = new VillageHut_ServerApplication.Customer() {
                cusId = txtCusId.Text,
                cusAddress = txtCusAddress.Text,
                cusName = txtCusName.Text,
                cusPhone = txtCusPhone.Text,
                cusNIC = customerNIC
            };

            //Current Employee Creation
            VillageHut_ServerApplication.Employee employeeDetails = new VillageHut_ServerApplication.Employee() {
                emId = MainWindow.logedEmpDetails.empId,
                emName = MainWindow.logedEmpDetails.empName
            };

            //Current PaymentDetails Creation
            PaymentDetails paymentDetails = new PaymentDetails() {
                transId = "Quotation",
                balance = double.Parse(txtTransBalance.Text),
                discount = double.Parse(txtTransDiscount.Text),
                totalPrice = double.Parse(txtTransTotalPrice.Text),
                pricePayed = double.Parse(txtTransPricePayed.Text)
            };

            InvoicePdfGenerator pdf = new InvoicePdfGenerator();
            pdf.generateReceipt(paymentDetails, lstCartItem, customerDetails, employeeDetails);

            lstCartItem.Clear();
            dgCart.ItemsSource = null;
            txtCartTotalPrice.Clear();
            clearConfigureFields();
            clearCusFields();
            loadTransactionDetails();



        }





        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }





        private void btnRemoveSelectedCart_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (dgCart.SelectedItems.Count > 0) {
                    lstCartItem.RemoveAt(dgCart.SelectedIndex);
                    dgCart.ItemsSource = lstCartItem.ToList();
                    txtCartTotalPrice.Text = getTotalPriceFromDgCart().ToString();
                    loadTransactionDetails();
                } else {
                    throw new Exception();
                }
            } catch (Exception ex) {
                MessageBox.Show("Please Select a Record..!");
            }
        }





        private void btnRemoveAllCart_Click(object sender, RoutedEventArgs e)
        {
            lstCartItem.Clear();
            dgCart.ItemsSource = null;
            txtCartTotalPrice.Clear();
            loadTransactionDetails();
        }





        private void comboFilterCat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(comboFilterCat.SelectedValue.ToString()).Equals("Please Select a Category")) {
                dgInventory.ItemsSource = stdClient.searchServices(comboFilterCat.SelectedValue.ToString());
                recordCount();
            } else {
                loadInventoryGrid();
            }
        }





        private void txtFilterServ_KeyUp(object sender, KeyEventArgs e)
        {
            dgInventory.ItemsSource = stdClient.searchServices(txtFilterServ.Text);
            recordCount();
        }





        private void txtFilterServ_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtFilterServ.Text.Equals("")) {
                txtFilterServ.Text = "Type here to search...";
            }
        }






        private void txtFilterServ_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtFilterServ.Text.Equals("Type here to search...")) {
                txtFilterServ.Text = "";
            }
        }





        private void dgInventory_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try {
                loadConfigurationFields();

            } catch (Exception ex) {
                MessageBox.Show(ex + "");
            }
        }





        private void comboConfSerType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try {
                loadConfDatePicker();
            } catch (Exception) {

            }
        }





        private void dpConfResDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            loadConfNoOfDays();
            //loadqty();
        }





        private void comboConfNoOfDays_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try {
                loadConfqty();
                loadConfPrice();
            } catch (Exception) {

            }

        }





        private void comboCusNIC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadCusDetails();
        }

        private void comboCusNIC_KeyUp(object sender, KeyEventArgs e)
        {
            try {
                if (!txtCusId.Text.Equals("CS00" + (stdClient.getMaxCusAutoGenNo() + 1)) && comboCusNIC.Text.Equals("")) {
                    clearCusFields();
                }
            } catch (Exception) {

            }
        }





        private void btnClearAllCusF_Click(object sender, RoutedEventArgs e)
        {
            clearCusFields();
        }





        private void txtTransPricePayed_KeyUp(object sender, KeyEventArgs e)
        {
            try {
                if (int.Parse(txtTransPricePayed.Text) >= int.Parse(txtTransTotalPrice.Text)) {
                    txtTransBalance.Text = (double.Parse(txtTransPricePayed.Text) - (getTotalPriceFromDgCart() - double.Parse(txtTransDiscount.Text))).ToString();
                }
            } catch (Exception) {

            }
        }





        private void txtTransDiscount_KeyUp(object sender, KeyEventArgs e)
        {
            try {
                if (double.Parse(txtTransDiscount.Text) <= double.Parse(txtCartTotalPrice.Text)) {
                    txtTransTotalPrice.Text = (double.Parse(txtCartTotalPrice.Text) - double.Parse(txtTransDiscount.Text)).ToString();
                }
            } catch (Exception) {

            }
        }





        private void btnConfClearFields_Click(object sender, RoutedEventArgs e)
        {
            clearConfigureFields();
        }





        private void comboCusNIC_LostFocus(object sender, RoutedEventArgs e)
        {
            try {
                //Resetting the cus id
                if (comboCusNIC.Text.Equals("Enter the NIC...") || comboCusNIC.Text.Equals("") || comboCusNIC.Text.Equals(null)) {
                    txtCusId.Text = "CS00" + (stdClient.getMaxCusAutoGenNo() + 1);
                    comboCusNIC.SelectedIndex = 0;
                }

                //Checking for existing nics (typed)
                bool foundNIC = false;
                VillageHut_ServerApplication.Customer foundCusDetails = null;

                foreach (var item in lstAllCustomers) {
                    if (item.cusNIC.Equals(comboCusNIC.Text)) {
                        foundNIC = true;
                        foundCusDetails = new VillageHut_ServerApplication.Customer() {
                            cusId = item.cusId,
                            cusAddress = item.cusAddress,
                            cusName = item.cusName,
                            cusPhone = item.cusPhone
                        };
                        break;
                    }
                }

                //If found
                if (!foundNIC) {
                    txtCusId.Text = "CS00" + (stdClient.getMaxCusAutoGenNo() + 1);
                    txtCusAddress.Clear();
                    txtCusName.Clear();
                    txtCusPhone.Clear();
                    //Not Found
                } else {
                    txtCusId.Text = foundCusDetails.cusId;
                    txtCusAddress.Text = foundCusDetails.cusAddress;
                    txtCusName.Text = foundCusDetails.cusName;
                    txtCusPhone.Text = foundCusDetails.cusPhone;
                }

                //If the nic is selected not typed
            } catch (Exception) {
                //Resetting the cus id
                if (comboCusNIC.SelectedValue.ToString().Equals("Enter the NIC...")) {
                    txtCusId.Text = "CS00" + (stdClient.getMaxCusAutoGenNo() + 1);
                    comboCusNIC.SelectedIndex = 0;
                }

            }

        }

        private void txtTransDiscount_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtTransDiscount.Text.Equals(null) || txtTransDiscount.Text.Equals("")) {
                txtTransDiscount.Text = "0.0";
            }
            try {
                if (double.Parse(txtTransDiscount.Text) <= double.Parse(txtCartTotalPrice.Text)/100*20) {
                    txtTransTotalPrice.Text = (double.Parse(txtCartTotalPrice.Text) - double.Parse(txtTransDiscount.Text)).ToString();
                } else {
                    MessageBox.Show("Discount cannot be above 20% of the total price..!");
                    txtTransDiscount.Text = "0.0";
                    txtTransTotalPrice.Text = txtCartTotalPrice.Text;
                    txtTransPricePayed.Text = "0.0";
                }
            } catch (Exception) {

            }
        }

        private void txtTransPricePayed_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtTransPricePayed.Text.Equals(null) || txtTransPricePayed.Text.Equals("")) {
                txtTransPricePayed.Text = "0.0";
            }
        }
    }
}
