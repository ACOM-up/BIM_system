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

namespace VillageHut_BIM_System.Windows
{
    /// <summary>
    /// Interaction logic for Wn_Login.xaml
    /// </summary>
    public partial class Wn_Login : Window
    {
        VillageHut_WebAPIClient stdClient = new VillageHut_WebAPIClient();

        public Wn_Login()
        {
            InitializeComponent();
        }

        private void btnMinimize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            List<VillageHut_ServerApplication.Login> lstLogin = new List<VillageHut_ServerApplication.Login>();
            lstLogin = stdClient.login(txtUsername.Text).ToList();

            if (lstLogin.Count <= 0) {
                MessageBox.Show("Invalied Username");
            } else {
                if (Helper.EncryptPass.VerifyHash(txtPassword.Password.ToString(), "SHA512", lstLogin.FirstOrDefault().empPass)) {
                    MainWindow mw = new MainWindow(new Login() {
                        empId = lstLogin.FirstOrDefault().empId,
                        empName = lstLogin.FirstOrDefault().empName,
                        empPass = "null"
                    });
                    mw.Show();
                    this.Close();
                } else {
                    MessageBox.Show("No Match");
                }
            }



        }
    }
}
