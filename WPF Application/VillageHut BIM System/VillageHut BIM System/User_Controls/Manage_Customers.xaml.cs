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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VillageHut_BIM_System.VillageHut_ServerApplication;
using VillageHut_BIM_System.Windows;

namespace VillageHut_BIM_System.User_Controls
{
    /// <summary>
    /// Interaction logic for Manage_Customers.xaml
    /// </summary>
    public partial class Manage_Customers : UserControl
    {
        VillageHut_WebAPIClient stdClient = new VillageHut_WebAPIClient();
        public Manage_Customers()
        {
            InitializeComponent();
            Loaded += Manage_Customers_Loaded;
        }





        //// Custom Functions







        //No of records found count
        private void recordCount()
        {
            if (dgManageCustomers.Items.Count > 1) {
                lblNoOfRecords.Text = dgManageCustomers.Items.Count + " Records Found";
            } else if (dgManageCustomers.Items.Count == 1) {
                lblNoOfRecords.Text = dgManageCustomers.Items.Count + " Record Found";
            } else if (dgManageCustomers.Items.Count == 0) {
                lblNoOfRecords.Text = "No Records Found";
            }
        }

        private void loadDataGrid()
        {
            dgManageCustomers.ItemsSource = stdClient.retrieveCustomers();
            recordCount();
        }






        //// END Custom Functions







        //// Deafult Events








        private void Manage_Customers_Loaded(object sender, RoutedEventArgs e)
        {
            loadDataGrid();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            loadDataGrid();
        }

        private void btnAddNewCus_Click(object sender, RoutedEventArgs e)
        {
            bool isWindowOpen = false;

            foreach (Window w in Application.Current.Windows) {
                if (w is Wn_AddViewCustomer) {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            if (!isWindowOpen) {
                Wn_AddViewCustomer winAddViewEmp = new Wn_AddViewCustomer(null);
                winAddViewEmp.Show();
            }
        }

        private void txtCusSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtCusSearch.Text.Equals("Type here to search...")) {
                txtCusSearch.Text = "";
            }
        }

        private void txtCusSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtCusSearch.Text.Equals("")) {
                txtCusSearch.Text = "Type here to search...";
            }
        }

        private void txtCusSearch_KeyUp(object sender, KeyEventArgs e)
        {
            dgManageCustomers.ItemsSource = stdClient.searchCustomers(txtCusSearch.Text);
            recordCount();
        }

        private void btnViewEmp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnViewCustomerDetails_Click(object sender, RoutedEventArgs e)
        {
            bool isWindowOpen = false;
            VillageHut_ServerApplication.Customer selectedCus = (VillageHut_ServerApplication.Customer)dgManageCustomers.SelectedItem;

            foreach (Window w in Application.Current.Windows) {
                if (w is Wn_AddViewCustomer) {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            if (!isWindowOpen) {
                Wn_AddViewCustomer winAddViewCus = new Wn_AddViewCustomer(selectedCus);
                winAddViewCus.Show();
            }
        }


        ////END Deafult Events
    }
}
