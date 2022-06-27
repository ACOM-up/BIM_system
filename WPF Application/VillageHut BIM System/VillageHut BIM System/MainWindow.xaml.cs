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
using VillageHut_BIM_System.User_Controls;

namespace VillageHut_BIM_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Defining UserControls
        static private Home userControlHome;
        static private Inventory userControlInventory;
        static private Reports userControlReports;
        static private Manage userControlManage;

        static public VillageHut_ServerApplication.Login logedEmpDetails;

        public MainWindow(VillageHut_ServerApplication.Login recievedRecord)
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            //UserControls
            userControlHome = new Home();
            userControlInventory = new Inventory();
            userControlReports = new Reports();
            userControlManage = new Manage(this);

            logedEmpDetails = recievedRecord;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Stylize the home button
            btnHome.Focus();

            //load the home UI when loaded
            mainStack.Children.Add(userControlHome);
        }

        //Making the grid dragable
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        //Custom Minimize Button
        private void btnMinimize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized; 
        }

        //Custom Close Button
        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            App.Current.Shutdown();
        }

        //show home usercontrol
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            mainStack.Children.Clear();
            mainStack.Children.Add(userControlHome);
        }

        //show inventory usercontrol
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainStack.Children.Clear();
            mainStack.Children.Add(userControlInventory);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mainStack.Children.Clear();
            mainStack.Children.Add(userControlReports);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            mainStack.Children.Clear();
            mainStack.Children.Add(userControlManage);
        }
    }
}
