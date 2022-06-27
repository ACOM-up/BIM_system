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
    /// Interaction logic for Wn_Upcoming_Reservations.xaml
    /// </summary>
    public partial class Wn_Upcoming_Reservations : Window
    {
        VillageHut_WebAPIClient stdClient = new VillageHut_WebAPIClient();
        public Wn_Upcoming_Reservations()
        {
            InitializeComponent();
            Loaded += Wn_Upcoming_Reservations_Loaded;
        }



        ////Custom Functions








        //LoadData from service
        private void loadData()
        {
            dgUpRes.ItemsSource = stdClient.retrieveUpcommingReservations().ToList();
            recordCount();
        }


        //SearchData from service
        private void searchData(string input)
        {
            dgUpRes.ItemsSource = stdClient.searchUpcommingReservations(input);
            recordCount();
        }


        //No of records found count
        private void recordCount()
        {
            if (dgUpRes.Items.Count > 1)
            {
                lblNoOfRecords.Text = dgUpRes.Items.Count + " Records Found";
            }
            else if (dgUpRes.Items.Count == 1)
            {
                lblNoOfRecords.Text = dgUpRes.Items.Count + " Record Found";
            }
            else if (dgUpRes.Items.Count == 0)
            {
                lblNoOfRecords.Text = "No Records Found";
            }
        }






        ////END Custom Functions







        ////Events







        private void Wn_Upcoming_Reservations_Loaded(object sender, RoutedEventArgs e)
        {
            loadData();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        //clear type here to search Lost Focus
        private void txtUpResSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtUpResSearch.Text.Equals(""))
            {
                txtUpResSearch.Text = "Type here to search...";
            }
        }

        //clear type here to search Got Focus
        private void txtUpResSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtUpResSearch.Text.Equals("Type here to search..."))
            {
                txtUpResSearch.Text = "";
            }
        }

        private void txtUpResSearch_KeyUp(object sender, KeyEventArgs e)
        {
            searchData(txtUpResSearch.Text);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btnPrintRecs_Click_1(object sender, RoutedEventArgs e)
        {
            List<VillageHut_ServerApplication.UpcommingResv> lstUpResTemp = new List<VillageHut_ServerApplication.UpcommingResv>();

            lstUpResTemp = (List<VillageHut_ServerApplication.UpcommingResv>)dgUpRes.ItemsSource;

            UpResPdfGenerator upResPdf = new UpResPdfGenerator();
            if (upResPdf.generateReport(lstUpResTemp))
            {
                MessageBox.Show("Saved");
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void btnCancelReserv_Click_1(object sender, RoutedEventArgs e)
        {
            if (dgUpRes.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a record..!");
            }
            else
            {

                VillageHut_ServerApplication.UpcommingResv selectedUpResRec = new UpcommingResv();
                selectedUpResRec = (VillageHut_ServerApplication.UpcommingResv)dgUpRes.SelectedItem;


                if (stdClient.cancelReserv(selectedUpResRec.cartId))
                {
                    MessageBox.Show("Canceled..!");
                }
                else
                {
                    MessageBox.Show("Error canceling..!");
                }

            }

        }




        ////END Events

    }
}
