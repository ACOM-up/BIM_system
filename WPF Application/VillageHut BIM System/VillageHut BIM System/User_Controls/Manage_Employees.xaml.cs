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
    /// Interaction logic for Manage_Employees.xaml
    /// </summary>
    public partial class Manage_Employees : UserControl
    {
        VillageHut_WebAPIClient stdClient = new VillageHut_WebAPIClient();
        public Manage_Employees()
        {
            InitializeComponent();
            Loaded += Manage_Employees_Loaded;
        }





        //// Custom Functions







        //No of records found count
        private void recordCount()
        {
            if (dgManageEmployees.Items.Count > 1)
            {
                lblNoOfRecords.Text = dgManageEmployees.Items.Count + " Records Found";
            }
            else if (dgManageEmployees.Items.Count == 1)
            {
                lblNoOfRecords.Text = dgManageEmployees.Items.Count + " Record Found";
            }
            else if (dgManageEmployees.Items.Count == 0)
            {
                lblNoOfRecords.Text = "No Records Found";
            }
        }

        private void loadDataGrid() {
            dgManageEmployees.ItemsSource = stdClient.retrieveEmployees();
            recordCount();
        }






        //// END Custom Functions







        //// Deafult Events





        private void Manage_Employees_Loaded(object sender, RoutedEventArgs e)
        {
            loadDataGrid();
        }

        private void txtEmpSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtEmpSearch.Text.Equals("Type here to search..."))
            {
                txtEmpSearch.Text = "";
            }
        }

        private void txtEmpSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtEmpSearch.Text.Equals(""))
            {
                txtEmpSearch.Text = "Type here to search...";
            }
        }

        private void txtEmpSearch_KeyUp(object sender, KeyEventArgs e)
        {
            dgManageEmployees.ItemsSource = stdClient.searchEmployees(txtEmpSearch.Text);
            recordCount();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            loadDataGrid();
        }

        private void btnAddNewEmp_Click(object sender, RoutedEventArgs e)
        {
            bool isWindowOpen = false;

            foreach (Window w in Application.Current.Windows) {
                if (w is Wn_AddViewEmployee) {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            if (!isWindowOpen) {
                Wn_AddViewEmployee winAddViewEmp = new Wn_AddViewEmployee(null);
                winAddViewEmp.Show();
            }
        }

        private void btnViewCus_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnViewEmployeeDetails_Click(object sender, RoutedEventArgs e)
        {
            bool isWindowOpen = false;
            VillageHut_ServerApplication.Employee selectedEmp = (VillageHut_ServerApplication.Employee)dgManageEmployees.SelectedItem;

            foreach (Window w in Application.Current.Windows) {
                if (w is Wn_AddViewEmployee) {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            if (!isWindowOpen) {
                Wn_AddViewEmployee winAddViewEmp = new Wn_AddViewEmployee(selectedEmp);
                winAddViewEmp.Show();
            }
        }





        //// END Deafult Events
    }
}
