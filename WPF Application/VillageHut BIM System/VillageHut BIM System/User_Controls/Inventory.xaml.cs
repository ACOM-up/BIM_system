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
using VillageHut_BIM_System.Helper;

namespace VillageHut_BIM_System.User_Controls
{
    public partial class Inventory : UserControl
    {
        VillageHut_WebAPIClient stdClient = new VillageHut_WebAPIClient();
        List<VillageHut_ServerApplication.Service> lstSer;

        public Inventory()
        {
            InitializeComponent();
            Loaded += Inventory_Loaded;
            lstSer = stdClient.retrieveAllServices().ToList();

            loadData();
        }

        ////Custom Functions





        //Loading the data
        private void loadData()
        {
            loadDataGrid();
            loadComboboxes();
            recordCount();
        }

        private void loadDataGrid()
        {
            //datagrid proporties
            dgInventory.AutoGenerateColumns = false;
            dgInventory.CanUserSortColumns = true;
            dgInventory.CanUserReorderColumns = false;

            isItemOrService(lstSer);
            try {
                dgInventory.ItemsSource = lstSer;
            } catch (Exception ex) {
                MessageBox.Show(ex + "");
            }
        }


        //Load Comboboxes
        private void loadComboboxes()
        {
            //comboSearchCategory.Items.Clear();

            List<VillageHut_ServerApplication.Category> lstCategories = new List<VillageHut_ServerApplication.Category>();
            lstCategories = stdClient.retrieveCategories().ToList();

            comboSearchCategory.Items.Add("Please Select a Category");
            foreach (var item in lstCategories) {
                comboSearchCategory.Items.Add(item.catName);
            }
        }


        ////checking wheather it is an item or a service
        private void isItemOrService(List<VillageHut_ServerApplication.Service> lst)
        {
            foreach (var item in lst) {
                if (item.serIsItem.Equals("false") && !item.serIsItem.Equals(null)) {
                    item.serIsItem = "Service";
                } else if (item.serIsItem.Equals("true") && !item.serIsItem.Equals(null)) {
                    item.serIsItem = "Item";
                }
            }
        }


        //No of records found count
        private void recordCount()
        {
            if (dgInventory.Items.Count > 1) {
                lblNoOfRecords.Text = dgInventory.Items.Count + " Records Found";
            } else if (dgInventory.Items.Count == 1) {
                lblNoOfRecords.Text = dgInventory.Items.Count + " Record Found";
            } else if (dgInventory.Items.Count == 0) {
                lblNoOfRecords.Text = "No Records Found";
            }
        }





        ////End Custom Functions













        ////Events





        private void Inventory_Loaded(object sender, RoutedEventArgs e)
        {

        }



        //Search by typing
        private void txtInventorySearch_KeyUp(object sender, KeyEventArgs e)
        {
            List<VillageHut_ServerApplication.Service> lstSearchSer = stdClient.searchServices(txtInventorySearch.Text.ToString()).ToList();
            isItemOrService(lstSearchSer);
            try {
                dgInventory.ItemsSource = lstSearchSer;
            } catch (Exception ex) {

            }

            //dgInventory.ItemsSource = stdClient.searchServices(txtInventorySearch.Text.ToString());

            recordCount();
        }



        //clear type here to search Got Focus
        private void txtInventorySearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtInventorySearch.Text.Equals("Type here to search...")) {
                txtInventorySearch.Text = "";
            }
        }


        //clear type here to search Lost Focus
        private void txtInventorySearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtInventorySearch.Text.Equals("")) {
                txtInventorySearch.Text = "Type here to search...";
            }
        }


        //Refresh button
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            lstSer = stdClient.retrieveAllServices().ToList();
            loadDataGrid();
            recordCount();
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

        private void comboSearchCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<VillageHut_ServerApplication.Service> lstSearchSer = new List<VillageHut_ServerApplication.Service>();
            if (!comboSearchCategory.SelectedItem.ToString().Equals("Please Select a Category")) {
                lstSearchSer = stdClient.searchServices(comboSearchCategory.SelectedItem.ToString()).ToList();

                isItemOrService(lstSearchSer);

                dgInventory.ItemsSource = lstSearchSer;

                recordCount();
            } else {
                loadDataGrid();
                recordCount();
            }
        }

        private void btnAddSer_Click(object sender, RoutedEventArgs e)
        {
            bool isWindowOpen = false;

            foreach (Window w in Application.Current.Windows) {
                if (w is Wn_AddViewService) {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            if (!isWindowOpen) {
                Wn_AddViewService winAddViewEmp = new Wn_AddViewService(null);
                winAddViewEmp.Show();
            }

        }

        private void btnPrintRecords_Click_1(object sender, RoutedEventArgs e)
        {
            List<VillageHut_ServerApplication.Service> lstInvenTemp = new List<Service>();
            lstInvenTemp = (List<VillageHut_ServerApplication.Service>)dgInventory.ItemsSource;
            InventoryPdfGenerator inventoryPdf = new InventoryPdfGenerator();

            if (inventoryPdf.generateReport(lstInvenTemp))
            {
                MessageBox.Show("Saved");
            }
            else {
                MessageBox.Show("Error");
            }


        }

        private void dgInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }




        

        ////END Events
    }
}
