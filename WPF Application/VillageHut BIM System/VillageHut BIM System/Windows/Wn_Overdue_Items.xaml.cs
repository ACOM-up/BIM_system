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
using VillageHut_BIM_System.Helper;
using VillageHut_BIM_System.VillageHut_ServerApplication;

namespace VillageHut_BIM_System.Windows
{
    /// <summary>
    /// Interaction logic for Wn_Overdue_Items.xaml
    /// </summary>
    public partial class Wn_Overdue_Items : Window
    {
        VillageHut_WebAPIClient stdClient = new VillageHut_WebAPIClient();

        public Wn_Overdue_Items()
        {
            InitializeComponent();
            Loaded += Wn_Overdue_Items_Loaded;
        }



        ////Custom Functions




        //LoadData
        private void loadData()
        {
            dgOverdue.ItemsSource = stdClient.retrieveOverdueItems().ToList();
            recordCount();
        }

        //SearchData
        private void searchData(string input)
        {
            dgOverdue.ItemsSource = stdClient.searchOverdueItems(input);
            recordCount();
        }


        //No of records found count
        private void recordCount()
        {
            if (dgOverdue.Items.Count > 1)
            {
                lblNoOfRecords.Text = dgOverdue.Items.Count + " Records Found";
            }
            else if (dgOverdue.Items.Count == 1)
            {
                lblNoOfRecords.Text = dgOverdue.Items.Count + " Record Found";
            }
            else if (dgOverdue.Items.Count == 0)
            {
                lblNoOfRecords.Text = "No Records Found";
            }
        }



        ////END Custom Functions





        ////Default Events




        private void Wn_Overdue_Items_Loaded(object sender, RoutedEventArgs e)
        {
            loadData();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void txtOverdueSearch_KeyUp(object sender, KeyEventArgs e)
        {
            searchData(txtOverdueSearch.Text);
        }

        //clear type here to search Got Focus
        private void txtOverdueSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtOverdueSearch.Text.Equals("Type here to search..."))
            {
                txtOverdueSearch.Text = "";
            }
        }

        //clear type here to search Lost Focus
        private void txtOverdueSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtOverdueSearch.Text.Equals(""))
            {
                txtOverdueSearch.Text = "Type here to search...";
            }
        }

        private void btnPrintOverD_Click_1(object sender, RoutedEventArgs e)
        {
            List<VillageHut_BIM_System.VillageHut_ServerApplication.OverdueItem> lstOverDItemsTemp = new List<VillageHut_BIM_System.VillageHut_ServerApplication.OverdueItem>();

            lstOverDItemsTemp = (List<VillageHut_ServerApplication.OverdueItem>)dgOverdue.ItemsSource;

            OverDPdfGenerator OverDPdf = new OverDPdfGenerator();

            if (OverDPdf.generateReport(lstOverDItemsTemp))
            {
                MessageBox.Show("Saved");
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void btnODMarkAsRet_Click_1(object sender, RoutedEventArgs e)
        {
            if (dgOverdue.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a record..!");
            }
            else
            {

                VillageHut_ServerApplication.OverdueItem selectedOverDItem = new OverdueItem();
                selectedOverDItem = (VillageHut_ServerApplication.OverdueItem)dgOverdue.SelectedItem;


                if (stdClient.isReturned(selectedOverDItem.cartId))
                {
                    MessageBox.Show("Done..!");
                }
                else
                {
                    MessageBox.Show("Error..!");
                }
            }

        }


        ////END Default Events

    }
}
