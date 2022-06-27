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
using VillageHut_BIM_System.User_Controls;

namespace VillageHut_BIM_System.User_Controls
{
    /// <summary>
    /// Interaction logic for Manage.xaml
    /// </summary>
    public partial class Manage : UserControl
    {
        static private Manage_Employees userControl_Manage_Employees;
        static private Manage_Customers userControl_Manage_Customers;

        MainWindow mainWindow;
        
        public Manage(MainWindow recievedMainWindow)
        {
            InitializeComponent();
            Loaded += Manage_Loaded;
            mainWindow = recievedMainWindow;

            //UserControls
            userControl_Manage_Employees = new Manage_Employees();
            userControl_Manage_Customers = new Manage_Customers();
        }



        ////Custom functions



        ////END Custom Functions





        //// Default Events


        private void Manage_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.mainStack.Children.Clear();
            mainWindow.mainStack.Children.Add(userControl_Manage_Employees);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mainWindow.mainStack.Children.Clear();
            mainWindow.mainStack.Children.Add(userControl_Manage_Customers);
        }


        //// END Default Events
    }
}
