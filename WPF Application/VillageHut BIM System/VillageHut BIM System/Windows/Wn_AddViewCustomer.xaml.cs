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
using System.Text.RegularExpressions;


namespace VillageHut_BIM_System.Windows
{
    /// <summary>
    /// Interaction logic for Wn_AddViewCustomer.xaml
    /// </summary>
    public partial class Wn_AddViewCustomer : Window
    {
        VillageHut_WebAPIClient stdClient = new VillageHut_WebAPIClient();
        VillageHut_ServerApplication.Customer recievedCustomer;

        public Wn_AddViewCustomer(VillageHut_ServerApplication.Customer recievedRecord)
        {
            InitializeComponent();
            Loaded += Wn_AddViewCustomer_Loaded;
            recievedCustomer = recievedRecord;
        }






        ////Custom Functions






        private void loadData()
        {
            if (recievedCustomer != null) {
                btnAddNewCus.Visibility = Visibility.Collapsed;
                txtCusId.Text = recievedCustomer.cusId;
                txtName.Text = recievedCustomer.cusName;
                txtNIC.Text = recievedCustomer.cusNIC;
                txtPhone.Text = recievedCustomer.cusPhone;
                txtAddress.Text = recievedCustomer.cusAddress;

                lblWindowName.Text = "View Customer";
            } else {
                txtCusId.Text = "CS00" + (stdClient.getMaxCusAutoGenNo() + 1).ToString();
                btnEditCus.Visibility = Visibility.Collapsed;
                btnDeleteCus.Visibility = Visibility.Collapsed;

                lblWindowName.Text = "Add Customer";
            }
        }





        /// <summary>
        /// Validating the Customer add, edit fields
        /// </summary>
        /// <returns></returns>


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
            string nicFormat = @"^\d{9}[V]$";
            return Regex.IsMatch(txtNIC.Text, nicFormat);
        }


        private bool isValiedPhone()
        {
            string phoneFormat = @"^\d{10}$";
            return Regex.IsMatch(txtPhone.Text, phoneFormat);
        }


        private bool validateAllFields()
        {
            string[] A = { txtPhone.Text,
                    txtAddress.Text,
                    txtCusId.Text,
                    txtName.Text,
                    txtNIC.Text
                };
            if (!isEmpty(A)) {
                if (isValiedNIC()) {
                    if (isValiedPhone()) {
                        return true;
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
        /// END Validating Fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>





        ////END Custom Functions














        ////Default Events







        private void Wn_AddViewCustomer_Loaded(object sender, RoutedEventArgs e)
        {
            try {
                loadData();
            } catch (Exception) {
                txtCusId.Text = "CS001";
            }
        }





        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }





        private void btnDeleteCus_Click(object sender, RoutedEventArgs e)
        {
            VillageHut_ServerApplication.Customer cusDetails = new VillageHut_ServerApplication.Customer() {
                cusId = txtCusId.Text,
            };

            try {
                bool result = stdClient.IUDCustomerDetails(cusDetails, 2);
                if (result)
                    MessageBox.Show("Deleted");
                else
                    MessageBox.Show("Error");

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }

        }





        private void btnEditCus_Click(object sender, RoutedEventArgs e)
        {
            if (!validateAllFields()) {
                return;
            }

            VillageHut_ServerApplication.Customer cusDetails = new VillageHut_ServerApplication.Customer() {
                cusAddress = txtAddress.Text,
                cusPhone = txtPhone.Text,
                cusId = txtCusId.Text,
                cusName = txtName.Text,
                cusNIC = txtNIC.Text,
            };

            try {
                bool result = stdClient.IUDCustomerDetails(cusDetails, 1);
                if (result)
                    MessageBox.Show("Updated");
                else
                    MessageBox.Show("Error");

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }





        private void btnAddNewCus_Click(object sender, RoutedEventArgs e)
        {
            if (!validateAllFields()) {
                return;
            }

            VillageHut_ServerApplication.Customer cusDetails = new VillageHut_ServerApplication.Customer() {
                cusAddress = txtAddress.Text,
                cusPhone = txtPhone.Text,
                cusId = txtCusId.Text,
                cusName = txtName.Text,
                cusNIC = txtNIC.Text,
            };

            try {
                bool result = stdClient.IUDCustomerDetails(cusDetails, 0);
                if (result)
                    MessageBox.Show("Saved");
                else
                    MessageBox.Show("Error");

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }





        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtAddress.Clear();
            txtName.Clear();
            txtNIC.Clear();
            txtPhone.Clear();
        }





        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }





        private void btnMinimize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }





        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }




    }
}
