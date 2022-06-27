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
using System.Text.RegularExpressions;

namespace VillageHut_BIM_System.Windows
{
    /// <summary>
    /// Interaction logic for Wn_AddViewEmployee.xaml
    /// </summary>
    public partial class Wn_AddViewEmployee : Window
    {
        VillageHut_WebAPIClient stdClient = new VillageHut_WebAPIClient();
        VillageHut_ServerApplication.Employee recievedEmployee;

        public Wn_AddViewEmployee(VillageHut_ServerApplication.Employee recievedRecord)
        {
            InitializeComponent();
            recievedEmployee = recievedRecord;
            Loaded += Wn_AddViewEmployee_Loaded;
        }






        ////Custom Functions






        private void loadData()
        {
            if (recievedEmployee != null) {
                btnAddNewEmp.Visibility = Visibility.Collapsed;
                txtEmpId.Text = recievedEmployee.emId;
                txtName.Text = recievedEmployee.emName;
                txtNIC.Text = recievedEmployee.emNIC;
                txtPhone.Text = recievedEmployee.emdPhone;
                txtUsername.Text = recievedEmployee.credUsername;
                txtAddress.Text = recievedEmployee.emdAddress;
                txtSalery.Text = recievedEmployee.emdSalarey.ToString();

                lblWindowName.Text = "View Employee";
            } else {
                txtEmpId.Text = "EM00" + (stdClient.getMaxEmpAutoGenNo() + 1).ToString();
                btnEditEmp.Visibility = Visibility.Collapsed;
                btnDeleteEmp.Visibility = Visibility.Collapsed;

                lblWindowName.Text = "Add Employee";
            }
        }



        /// <summary>
        /// Validating the employee add, edit fields
        /// </summary>
        /// <returns></returns>

        private bool isValiedPass()
        {
            string passwordFormat = @"^[a-zA-Z]\w{3,14}$";
            return Regex.IsMatch(txtAccessPassword.Password, passwordFormat);
        }

        private bool isEmpty(string[] fieldsArray)
        {
            for (int i = 0; i < fieldsArray.Length; i++) {
                if (fieldsArray[i].Equals("") || fieldsArray[i].Equals(null)) {
                    return true;
                }
            }
            return false;
        }



        private bool isValiedUsername()
        {
            string usernameFormat = @"^[a-zA-Z]+[0-9]*$";
            return Regex.IsMatch(txtUsername.Text, usernameFormat); ;
        }

        private bool isValiedNIC()
        {
            string nicFormat = @"^\d{9}[V]$";
            return Regex.IsMatch(txtNIC.Text, nicFormat); ;
        }

        private bool isValiedSalary()
        {
            string salaryFormat = @"^[+]?\d+(\.\d+)?$";
            return Regex.IsMatch(txtSalery.Text, salaryFormat);
        }

        private bool isValiedPhone()
        {
            string phoneFormat = @"^\d{10}$";
            return Regex.IsMatch(txtPhone.Text, phoneFormat);
        }

        private bool isConfPassMatch() {
            if (txtAccessPassword.Password.ToString().Equals(txtConfirmPassword.Password.ToString())) {
                return true;
            } else {
                return false;
            }
        }



        private bool validateAllFields()
        {
            if (isValiedPass()) {
                string[] A = { txtAccessPassword.Password.ToString(),
                    txtAddress.Text,
                    txtConfirmPassword.Password.ToString(),
                    txtEmpId.Text,
                    txtName.Text,
                    txtNIC.Text,
                    txtPhone.Text,
                    txtSalery.Text,
                    txtUsername.Text
                };
                if (!isEmpty(A)) {
                    if (isValiedSalary()) {
                        if (isValiedNIC()) {
                            if (isValiedPhone()) {
                                if (isValiedUsername()) {
                                    if (isConfPassMatch()) {
                                        return true;
                                    } else {
                                        MessageBox.Show("Passwords do not match..!");
                                        return false;
                                    }    
                                } else {
                                    MessageBox.Show("Invalied username, \r\nit must start with a letter. Only letters and numbers are allowed.");
                                    return false;
                                }
                            } else {
                                MessageBox.Show("Invalied phone number..!");
                                return false;
                            }
                        } else {
                            MessageBox.Show("Invalied NIC..!");
                            return false;
                        }
                    } else {
                        MessageBox.Show("Invalied salary..!");
                        return false;
                    }
                } else {
                    MessageBox.Show("One or more input fields are empty..!");
                    return false;
                }

            } else {
                MessageBox.Show("The password's first character must be a letter, \r\nit must contain at least 4 characters and no more than 15 characters and no characters other than letters, numbers and the underscore may be used.");
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







        private void Wn_AddViewEmployee_Loaded(object sender, RoutedEventArgs e)
        {
            try {
                loadData();
            } catch (Exception) {
                txtEmpId.Text = "EM001";
            }
        }






        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }





        private void btnMinimize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }





        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }





        private void btnAddNewEmp_Click(object sender, RoutedEventArgs e)
        {

            try {
                if (stdClient.searchDupUsernames(txtUsername.Text) != 0) {
                    MessageBox.Show("Username Already Taken...!");
                    return;
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }


            if (!validateAllFields()) {
                return;
            }



            VillageHut_ServerApplication.Employee emDetails = new VillageHut_ServerApplication.Employee() {
                emdAddress = txtAddress.Text,
                emdPhone = txtPhone.Text,
                emdSalarey = double.Parse(txtSalery.Text),
                emId = txtEmpId.Text,
                emName = txtName.Text,
                emNIC = txtNIC.Text,
                credUsername = txtUsername.Text
            };

            try {
                bool result = stdClient.IUDEmployeeDetails(emDetails, Helper.EncryptPass.ComputeHash(txtAccessPassword.Password.ToString(), "SHA512", null), 0);
                if (result)
                    MessageBox.Show("Saved");
                else
                    MessageBox.Show("Error");

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }





        private void btnDeleteEmp_Click(object sender, RoutedEventArgs e)
        {
            //if (Helper.EncryptPass.VerifyHash(txtAccessPassword.Password.ToString(), "SHA512", Helper.EncryptPass.ComputeHash(txtConfirmPassword.Password.ToString(), "SHA512", null))) {
            //    MessageBox.Show("Match");
            //} else {
            //    MessageBox.Show("No Match");
            //}
            VillageHut_ServerApplication.Employee emDetails = new VillageHut_ServerApplication.Employee() {
                emId = txtEmpId.Text,
            };

            try {
                bool result = stdClient.IUDEmployeeDetails(emDetails, "", 2);
                if (result)
                    MessageBox.Show("Deleted");
                else
                    MessageBox.Show("Error");

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }





        private void btnEditEmp_Click(object sender, RoutedEventArgs e)
        {

            try {
                int count = stdClient.searchDupUsernames(txtUsername.Text);

                if (count != 0 && !recievedEmployee.credUsername.Equals(txtUsername.Text)) {
                    MessageBox.Show("Username Already Taken...!");
                    return;
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }

            if (!validateAllFields()) {
                return;
            }

            VillageHut_ServerApplication.Employee emDetails = new VillageHut_ServerApplication.Employee() {
                emdAddress = txtAddress.Text,
                emdPhone = txtPhone.Text,
                emdSalarey = double.Parse(txtSalery.Text),
                emId = txtEmpId.Text,
                emName = txtName.Text,
                emNIC = txtNIC.Text,
                credUsername = txtUsername.Text
            };

            try {
                bool result = stdClient.IUDEmployeeDetails(emDetails, Helper.EncryptPass.ComputeHash(txtAccessPassword.Password.ToString(), "SHA512", null), 1);
                if (result)
                    MessageBox.Show("Updated");
                else
                    MessageBox.Show("Error");

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }





        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }





        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtAccessPassword.Clear();
            txtAddress.Clear();
            txtConfirmPassword.Clear();
            txtName.Clear();
            txtNIC.Clear();
            txtPhone.Clear();
            txtSalery.Clear();
            txtUsername.Clear();
        }





        ////END Default Events
    }
}
