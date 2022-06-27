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

namespace VillageHut_BIM_System.Windows
{
    /// <summary>
    /// Interaction logic for Wn_Monthly_Transactions.xaml
    /// </summary>
    public partial class Wn_Monthly_Transactions : Window
    {
        VillageHut_WebAPIClient stdClient = new VillageHut_WebAPIClient();
        public Wn_Monthly_Transactions()
        {
            InitializeComponent();
            Loaded += Wn_Monthly_Transactions_Loaded;
        }


        ////Custom functions



        //LoadData
        private void loadData()
        {
            loadComboYear();
            loadComboMonth();
            dgTransactions.ItemsSource = stdClient.retrieveTransactions("Please Select Year", "Please Select Month", txtSearchMonthlyTrans.Text).ToList();
            recordCount();
        }

        private void searchData() {
            dgTransactions.ItemsSource = stdClient.retrieveTransactions(comboYear.SelectedValue + "", comboMonth.SelectedValue + "", txtSearchMonthlyTrans.Text);
            recordCount();
        }


        //Load the yearCombobox Year items
        private void loadComboYear()
        {
            comboMonth.Items.Clear();

            List<proc_retrieveTransYearsResult> lstComboYear = new List<proc_retrieveTransYearsResult>();
            lstComboYear = stdClient.getTransYear().ToList();
            lstComboYear.Reverse();
            comboYear.Items.Add("Please Select Year");
            foreach (var item in lstComboYear) {
                comboYear.Items.Add(item.Column1);
            }

            comboMonth.SelectedIndex = 0;
        }

        //Load the monthCombobox Month items
        private void loadComboMonth()
        {
            comboMonth.Items.Clear();

            string[] monthArray = { "Please Select Month", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            int thisMonth = monthArray.Length;
            DateTime today = DateTime.Today;

            if (comboYear.SelectedItem.ToString().Equals(today.ToString("yyyy"))) {
                thisMonth = (int.Parse(today.ToString("MM")) + 1) / 1;
            }
            for (int i = 0; i < thisMonth; i++) {
                comboMonth.Items.Add(monthArray[i]);
            }

            comboMonth.SelectedIndex = 0;
        }

        //No of records found count
        private void recordCount()
        {
            if (dgTransactions.Items.Count > 1) {
                lblNoOfRecords.Text = dgTransactions.Items.Count + " Records Found";
            } else if (dgTransactions.Items.Count == 1) {
                lblNoOfRecords.Text = dgTransactions.Items.Count + " Record Found";
            } else if (dgTransactions.Items.Count == 0) {
                lblNoOfRecords.Text = "No Records Found";
            }
        }




        ////END Custom Functions








        private void Wn_Monthly_Transactions_Loaded(object sender, RoutedEventArgs e)
        {
            loadData();
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void comboYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadComboMonth();
            searchData();
        }

        private void dgTransactions_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //if (e.ClickCount == 2) {
            try {
                //Getting the selected properties and saving them in an object
                VillageHut_ServerApplication.Transaction dgSelectedTrans = (VillageHut_ServerApplication.Transaction)dgTransactions.SelectedItem;

                //Creating a new class to store the selected properties
                Helper.Transaction selectedTrans = new Helper.Transaction() {
                    transId = dgSelectedTrans.transId,
                    cusName = dgSelectedTrans.cusName,
                    cusNIC = dgSelectedTrans.cusNIC,
                    empName = dgSelectedTrans.empName,
                    empNIC = dgSelectedTrans.empNIC,
                    transDate = dgSelectedTrans.transDate,
                    transPrice = dgSelectedTrans.transPrice.ToString("#,##0")
                };

                //Adding properties to a list
                List<Helper.Transaction> lstTransSelectedDetails = new List<Helper.Transaction>();
                lstTransSelectedDetails.Add(selectedTrans);

                //Updating the ItemsControl to view the selected item
                iCTransDetails.ItemsSource = lstTransSelectedDetails;
                iCTransCartDetails.ItemsSource = stdClient.retrieveItems(dgSelectedTrans.transId);

            } catch (Exception) {
            }
            //}
        }

        private void txtSearchMonthlyTrans_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearchMonthlyTrans.Text.Equals("Type here to search...")) {
                txtSearchMonthlyTrans.Text = "";
            }
        }

        private void txtSearchMonthlyTrans_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearchMonthlyTrans.Text.Equals("")) {
                txtSearchMonthlyTrans.Text = "Type here to search...";
            }
        }

        private void txtSearchMonthlyTrans_KeyUp(object sender, KeyEventArgs e)
        {
            searchData();
        }

        private void comboMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            searchData();
        }
    }
}
