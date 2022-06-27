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
using VillageHut_BIM_System.Windows;

namespace VillageHut_BIM_System.User_Controls
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : UserControl
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bool isWindowOpen = false;

            foreach (Window w in Application.Current.Windows) {
                if (w is Wn_Overdue_Items) {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            if (!isWindowOpen) {
                Wn_Overdue_Items winOverdue = new Wn_Overdue_Items();
                winOverdue.Show();
            }
        }

        private void btnMonthlyTrans_Click(object sender, RoutedEventArgs e)
        {
            bool isWindowOpen = false;

            foreach (Window w in Application.Current.Windows) {
                if (w is Wn_Monthly_Transactions) {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            if (!isWindowOpen) {
                Wn_Monthly_Transactions winMonthlyTrans = new Wn_Monthly_Transactions();
                winMonthlyTrans.Show();
            }
        }

        private void btnPrintReceipt_Click(object sender, RoutedEventArgs e)
        {
            bool isWindowOpen = false;

            foreach (Window w in Application.Current.Windows) {
                if (w is Wn_Print_Receipt) {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            if (!isWindowOpen) {
                Wn_Print_Receipt winPrintReceipt = new Wn_Print_Receipt();
                winPrintReceipt.Show();
            }
        }
    }
}
