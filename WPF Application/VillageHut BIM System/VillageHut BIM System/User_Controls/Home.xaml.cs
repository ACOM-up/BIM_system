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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        VillageHut_WebAPIClient stdClient = new VillageHut_WebAPIClient();
        public Home()
        {
            InitializeComponent();
            Loaded += Home_Loaded;
        }

        private void Home_Loaded(object sender, RoutedEventArgs e)
        {
            lstMsgs_home.ItemsSource = stdClient.retrieveMessages();
            lstOverdue_home.ItemsSource = stdClient.retrieveOverdueItems();
            lstUpRes_home.ItemsSource = stdClient.retrieveUpcommingReservations();
        }

        private void lstUpRes_home_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void btnUpResViewAll_Click(object sender, RoutedEventArgs e)
        {
            bool isWindowOpen = false;

            foreach (Window w in Application.Current.Windows) {
                if (w is Wn_Upcoming_Reservations) {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            if (!isWindowOpen) {
                Wn_Upcoming_Reservations winUpRes = new Wn_Upcoming_Reservations();
                winUpRes.Show();
            }
        }

        private void btnOverDueViewAll_Click(object sender, RoutedEventArgs e)
        {
            bool isWindowOpen = false;

            foreach (Window w in Application.Current.Windows) {
                if (w is Wn_Overdue_Items) {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            if (!isWindowOpen) {
                Wn_Overdue_Items winUpRes = new Wn_Overdue_Items();
                winUpRes.Show();
            }
        }

        private void btnGotoPrint_Click_1(object sender, RoutedEventArgs e)
        {
            bool isWindowOpen = false;

            foreach (Window w in Application.Current.Windows)
            {
                if (w is Wn_Print_Receipt)
                {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            if (!isWindowOpen)
            {
                Wn_Print_Receipt winPrintReceipt = new Wn_Print_Receipt();
                winPrintReceipt.Show();
            }
        }

        private void btnRefreshAll_Click_1(object sender, RoutedEventArgs e)
        {
            lstMsgs_home.ItemsSource = stdClient.retrieveMessages();
            lstOverdue_home.ItemsSource = stdClient.retrieveOverdueItems();
            lstUpRes_home.ItemsSource = stdClient.retrieveUpcommingReservations();
        }

    }
}
