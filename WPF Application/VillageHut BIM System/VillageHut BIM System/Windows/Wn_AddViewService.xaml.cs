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
using Microsoft.Win32;
using System.Text.RegularExpressions;


namespace VillageHut_BIM_System.Windows
{
    /// <summary>
    /// Interaction logic for Wn_AddViewService.xaml
    /// </summary>
    public partial class Wn_AddViewService : Window
    {
        VillageHut_WebAPIClient stdClient = new VillageHut_WebAPIClient();
        VillageHut_ServerApplication.Service recievedService;
        public Wn_AddViewService(VillageHut_ServerApplication.Service recievedSerRecord)
        {
            InitializeComponent();
            recievedService = recievedSerRecord;

            Loaded += Wn_AddViewService_Loaded;
            //Load the combobox comboAddSerCat
            loadComboCat();
        }




        ////Custom Functions




        //Load the combobox
        private void loadComboCat()
        {
            comboAddSerCat.Items.Clear();

            List<VillageHut_ServerApplication.Category> lstCategories = new List<VillageHut_ServerApplication.Category>();
            lstCategories = stdClient.retrieveCategories().ToList();

            comboAddSerCat.Items.Add("Please Select a Category");
            foreach (var item in lstCategories)
            {
                comboAddSerCat.Items.Add(item.catName);
            }

            comboAddSerCat.SelectedIndex = 0;
        }


        //Load the no of records
        private void recordCountUpdate() {
            if (dgAddType.Items.Count != 1)
            {
                lblAddedTypeCount.Text = dgAddType.Items.Count + " Types Added";
            }
            else
            {
                lblAddedTypeCount.Text = dgAddType.Items.Count + " Type Added";
            }
        }


        //Reset the add type fields
        private void resetTypeFields() {
            txtSerTypeName.Text = "Enter Type Name (Empty for default)...";
            txtSerTypePricePItem.Text = "Enter Price Per Item...";
            txtSerTypeMaxQTY.Text = "Enter Max QTY...";
        }


        //Load the txtFields
        private void loadData()
        {
            if (recievedService == null)
            {
                btnDeleteService.Visibility = Visibility.Collapsed;
                btnEditService.Visibility = Visibility.Collapsed;
                //Generate the next ID
                txtSerId.Text = "SER00" + ((stdClient.getMaxSerAutoGenNo()) + 1).ToString();
            }
            else
            {
                btnAddNewService.Visibility = Visibility.Collapsed;

                btnClear.Content = "Reset All";

                txtSerDescription.Text = recievedService.serDesc;
                txtSerId.Text = recievedService.serId;
                txtSerImg.Text = recievedService.serImg;
                txtSerName.Text = recievedService.serName;
                comboAddSerCat.SelectedValue = recievedService.catName;

                dgAddType.ItemsSource = stdClient.retrieveTypes(recievedService.serId);
            }
        }



        private void clearAll()
        {
            dgAddType.ItemsSource = null;
            txtSerDescription.Text = null;
            txtSerImg.Text = null;
            txtSerName.Text = null;
            txtSerId.Text = null;
        }








        /// <summary>
        /// Validating the Service add, edit fields
        /// </summary>
        /// <returns></returns>


        private bool isEmpty(string[] fieldsArray)
        {
            for (int i = 0; i < fieldsArray.Length; i++)
            {
                if (fieldsArray[i].Equals("") || fieldsArray[i].Equals(null))
                {
                    return true;
                }
            }
            return false;
        }


        private bool isValiedTypePrice()
        {
            string typePriceFormat = @"^[+]?\d+(\.\d+)?$";
            return Regex.IsMatch(txtSerTypePricePItem.Text, typePriceFormat);
        }

        private bool isValiedTypeQty()
        {
            string qtyFormat = @"^\d[0-9]*$";
            return Regex.IsMatch(txtSerTypeMaxQTY.Text, qtyFormat);
        }

        private bool isTypeDefaultValues()
        {
            if (dgAddType.Items.Count <= 0)
            {
                if (txtSerTypeMaxQTY.Text.Equals("Enter Max QTY..."))
                {
                    return false;
                }
                if (txtSerTypePricePItem.Text.Equals("Enter Price Per Item..."))
                {
                    return false;
                }
            }

            return true;
        }

        private bool isComboCatDefault()
        {
            if (comboAddSerCat.SelectedIndex == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool validateTypeQtyPrice()
        {
            if (isValiedTypePrice() && isValiedTypeQty())
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private bool validateAllFields()
        {
            string[] A = {
                txtSerTypeMaxQTY.Text,
                txtSerTypeName.Text,
                txtSerTypePricePItem.Text,
                txtSerName.Text,
                txtSerImg.Text,
                txtSerId.Text,
                txtSerDescription.Text
            };
            if (!isEmpty(A))
            {
                if (isTypeDefaultValues())
                {
                    if (isComboCatDefault())
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Please select a valied category..!");
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a price and a qty for a type..!");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("One or more input fields are empty..!");
                return false;
            }
        }
        /// <summary>
        /// END Validating Fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

















        ////END Custom Functions












        private void Wn_AddViewService_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                loadData();
            }
            catch (Exception)
            {
                txtSerId.Text = "SER001";
            }

        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imgSerItem.Source = new BitmapImage(new Uri(op.FileName));
                txtSerImg.Text = "Images/" + System.IO.Path.GetFileName(imgSerItem.Source.ToString());
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDeleteService_Click(object sender, RoutedEventArgs e)
        {
            VillageHut_ServerApplication.Service servDetails = new VillageHut_ServerApplication.Service()
            {
                catId = "",
                catName = comboAddSerCat.SelectedValue.ToString(),
                serId = txtSerId.Text,
                serDesc = txtSerDescription.Text,
                serName = txtSerName.Text,
                serImg = txtSerImg.Text,
                serIsDeleted = "false",
            };

            try
            {
                bool result = stdClient.IUDServiceDetails(servDetails, null, 2);

                if (result)
                {
                    MessageBox.Show("Service Deleted");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error Deleting record..!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error..!" + ex);
            }
        }

        private void btnEditService_Click(object sender, RoutedEventArgs e)
        {
            if (!validateAllFields())
            {
                return;
            }

            VillageHut_ServerApplication.Service servDetails = new VillageHut_ServerApplication.Service()
            {
                catId = "",
                catName = comboAddSerCat.SelectedValue.ToString(),
                serId = txtSerId.Text,
                serDesc = txtSerDescription.Text,
                serName = txtSerName.Text,
                serImg = txtSerImg.Text,
                serIsDeleted = "false",
            };

            List<VillageHut_ServerApplication.Type> dgItems = new List<VillageHut_ServerApplication.Type>();

            foreach (var item in dgAddType.Items.OfType<VillageHut_ServerApplication.Type>().ToList())
            {
                dgItems.Add(item);
            }

            List<VillageHut_ServerApplication.tbl_type> typeDetails = new List<VillageHut_ServerApplication.tbl_type>();

            foreach (var item in dgItems)
            {
                typeDetails.Add(new VillageHut_ServerApplication.tbl_type()
                {
                    typeAvailableQty = item.typeMaxQty,
                    serId = txtSerId.Text,
                    typeMaxQty = item.typeMaxQty,
                    typeName = item.typeName,
                    typePricePService = item.typePricePItem
                });
            }

            try
            {
                bool result = stdClient.IUDServiceDetails(servDetails, typeDetails.ToArray(), 1);

                if (result)
                {
                    MessageBox.Show("Service Updated");
                    resetTypeFields();
                }
                else
                {
                    MessageBox.Show("Error Updating record..!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error..!" + ex);
            }
        }

        private void btnAddNewService_Click(object sender, RoutedEventArgs e)
        {
            if (!validateAllFields())
            {
                return;
            }

            VillageHut_ServerApplication.Service servDetails = new VillageHut_ServerApplication.Service()
            {
                catId = "",
                catName = comboAddSerCat.SelectedValue.ToString(),
                serId = txtSerId.Text,
                serDesc = txtSerDescription.Text,
                serName = txtSerName.Text,
                serImg = txtSerImg.Text,
                serIsDeleted = "false",
            };

            List<VillageHut_ServerApplication.Type> dgItems = new List<VillageHut_ServerApplication.Type>();

            foreach (var item in dgAddType.Items.OfType<VillageHut_ServerApplication.Type>().ToList())
            {
                dgItems.Add(item);
            }

            List<VillageHut_ServerApplication.tbl_type> typeDetails = new List<VillageHut_ServerApplication.tbl_type>();

            foreach (var item in dgItems)
            {
                typeDetails.Add(new VillageHut_ServerApplication.tbl_type()
                {
                    typeAvailableQty = item.typeMaxQty,
                    serId = txtSerId.Text,
                    typeMaxQty = item.typeMaxQty,
                    typeName = item.typeName,
                    typePricePService = item.typePricePItem
                });
            }

            try
            {
                bool result = stdClient.IUDServiceDetails(servDetails, typeDetails.ToArray(), 0);

                if (result)
                {
                    MessageBox.Show("Service Added");
                    clearAll();
                    resetTypeFields();
                    loadData();
                }
                else
                {
                    MessageBox.Show("Error Saving record..!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error..!" + ex);
            }

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Clear All", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                clearAll();
                recordCountUpdate();
            }
            loadData();
        }

        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnAddNewType_Click(object sender, RoutedEventArgs e)
        {
            if (!validateTypeQtyPrice())
            {
                MessageBox.Show("Price and qty is invalied..!");
                return;
            }

            bool isInList = false;
            List<VillageHut_ServerApplication.Type> lstType = new List<VillageHut_ServerApplication.Type>();

            try
            {
                var typeRow = new VillageHut_ServerApplication.Type
                {
                    typeName = txtSerTypeName.Text,
                    typeMaxQty = int.Parse(txtSerTypeMaxQTY.Text),
                    typePricePItem = double.Parse(txtSerTypePricePItem.Text)
                };

                //Null name
                if (txtSerTypeName.Text.Equals("Enter Type Name (Empty for default)..."))
                {
                    typeRow.typeName = "Default";
                    txtSerTypeName.Text = "Default";
                }

                if (!dgAddType.HasItems)
                {
                    //If 1st type
                    lstType.Add(typeRow);
                    dgAddType.ItemsSource = lstType.ToList().Skip(0);
                }
                else
                {
                    //Search the datagrid
                    lstType = dgAddType.Items.OfType<VillageHut_ServerApplication.Type>().ToList();
                    foreach (var item in lstType)
                    {
                        if (txtSerTypeName.Text.Equals(item.typeName))
                        {
                            isInList = true;
                        }
                    }
                    //If found
                    if (isInList)
                    {
                        MessageBox.Show("Item Already Added..!");
                    }
                    else
                    {
                        lstType.Add(typeRow);
                        dgAddType.ItemsSource = lstType.ToList();
                    }
                }

                recordCountUpdate();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalied inputs..!" + ex);
            }
        }


        /// <summary>
        /// Got focus and lost focus events for the three textboxes of type insertion, 
        /// clear when in focus and restores when out of focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        private void txtSerTypeName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSerTypeName.Text.Equals("Enter Type Name (Empty for default)..."))
            {
                txtSerTypeName.Text = "";
            }
        }

        private void txtSerTypeName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtSerTypeName.Text.Equals(""))
            {
                txtSerTypeName.Text = "Enter Type Name (Empty for default)...";
            }
        }

        private void txtSerTypeMaxQTY_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSerTypeMaxQTY.Text.Equals("Enter Max QTY..."))
            {
                txtSerTypeMaxQTY.Text = "";
            }
        }

        private void txtSerTypeMaxQTY_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtSerTypeMaxQTY.Text.Equals(""))
            {
                txtSerTypeMaxQTY.Text = "Enter Max QTY...";
            }
        }

        private void txtSerTypePricePItem_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSerTypePricePItem.Text.Equals("Enter Price Per Item..."))
            {
                txtSerTypePricePItem.Text = "";
            }
        }

        private void txtSerTypePricePItem_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtSerTypePricePItem.Text.Equals(""))
            {
                txtSerTypePricePItem.Text = "Enter Price Per Item...";
            }
        }

        private void btnRemoveSelectedType_Click(object sender, RoutedEventArgs e)
        {
            List<VillageHut_ServerApplication.Type> lstType = new List<VillageHut_ServerApplication.Type>();
            //Removing the selected index
            try
            {
                if (dgAddType.SelectedItems.Count > 0)
                {
                    lstType = dgAddType.Items.OfType<VillageHut_ServerApplication.Type>().ToList();
                    lstType.RemoveAt(dgAddType.SelectedIndex);
                    dgAddType.ItemsSource = lstType.ToList();
                }
                else
                {
                    throw new Exception();
                }

                recordCountUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Select a Type..!");
            }
        }

        private void btnRemoveAllType_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Clear All", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                dgAddType.ItemsSource = null;
                recordCountUpdate();
            }
        }
    }
}
