using Notifications.Wpf;
using System;
using System.Drawing;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace POSProgram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string EmId;
        public MainWindow(string status,string userName , string emID)
        {
            InitializeComponent();
            //get text from data to textbox user status and user name
            txt_user_admin.Text = status;
            txt_user_show.Text = userName;
            EmId = emID;
            Dispatcher.Invoke((Action)(() => { showDashboad(); }));
            Dispatcher.Invoke((Action)(() => { getImageUser(); }));
        }

        private SetNotify notify = new SetNotify();

        private string textSearch = "";
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*var user = new UserManagement();
            this.grid_main_controller.Children.Add(user);*/
            ShowNotfication();
           
          
        }
        private void ShowNotfication()
        {
            notify.Title = "Login";
            notify.Message = "User Login Successful...";
            notify.ShowNotfySucess();
            notify.Title = "Login";
            notify.Message = "Your Change Password in Acount Form";
            notify.ShowNotfyInformation();
        }
        private void btn_dashboad_Click(object sender, RoutedEventArgs e)
        {
            showDashboad();
        }

        private void btn_account_Click(object sender, RoutedEventArgs e)
        {
            showAccount();
        }

        private void btn_product_Click(object sender, RoutedEventArgs e)
        {
            showProducts();  
        }

        private void btn_brand_Click(object sender, RoutedEventArgs e)
        {
            showBrand();  
        }

        private void btn_category_Click(object sender, RoutedEventArgs e)
        {
            showCategory();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "");
            }
        }

// therad management show from
//show
        private void showDashboad()
        {
            Dispatcher.Invoke((Action)(() => {
                Dashboad dashboad = new Dashboad();
                grid_main_controller.Children.Clear();
                grid_main_controller.Children.Add(dashboad);
                notify.Title = "Dasboad Form";
                notify.Message = "Access Successful...";
                notify.ShowNotfySucess();
            }));
        }
        //product
        private void showProducts()
        {
            Dispatcher.Invoke((Action)(() => {
                Products products = new Products();
                grid_main_controller.Children.Clear();
                grid_main_controller.Children.Add(products);
                notify.Title = "Products Form";
                notify.Message = "Access Successful...";
                notify.ShowNotfySucess();
            }));      
        }

        private void showEmployee()
        {
            Dispatcher.Invoke((Action)(() => {
                var employee = new Employee();
                grid_main_controller.Children.Clear();
                grid_main_controller.Children.Add(employee);
                notify.Title = "Employee Form";
                notify.Message = "Access Successful...";
                notify.ShowNotfySucess();
            }));
        }
        //Brand invock
        private void showBrand()
        {
            Dispatcher.Invoke((Action)(() => {
                Brand brand = new Brand();
                grid_main_controller.Children.Clear();
                grid_main_controller.Children.Add(brand);
                notify.Title = "Brand Form";
                notify.Message = "Access Successful...";
                notify.ShowNotfySucess();
            }));
        }
        //account
        private void showAccount()
        {
            //this.Opacity = 0.3;
            UserManagement userManagement = new UserManagement();
            //LoadingPage();
            Dispatcher.Invoke((Action)(() => {

                grid_main_controller.Children.Clear();
                grid_main_controller.Children.Add(userManagement);
                notify.Title = "Account Form";
                notify.Message = "Access Successful...";
                notify.ShowNotfySucess();
            }));
        }
        //category
        private void showCategory()
        {
            Dispatcher.Invoke((Action)(() => {
                Category category = new Category();
                grid_main_controller.Children.Clear();
                grid_main_controller.Children.Add(category);
                notify.Title = "Categorys Form";
                notify.Message = "Access Successful...";
                notify.ShowNotfySucess();
            }));
        }


        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btn_stock_Click(object sender, RoutedEventArgs e)
        {
            showStock();
        }
        private void showStock()
        {
            Dispatcher.Invoke((Action)(() => {
                var stock = new Stock();
                grid_main_controller.Children.Clear();
                grid_main_controller.Children.Add(stock);
                notify.Title = "Stock Form";
                notify.Message = "Access Successful...";
                notify.ShowNotfySucess();
            }));
        }

        private void btn_Pos_Click(object sender, RoutedEventArgs e)
        {
                var pos = new POS(EmId);
                pos.Show();
                notify.Title = "POS Management Form";
                notify.Message = "Access Successful...";
                notify.ShowNotfySucess();
                this.Dispatcher.BeginInvoke(new Action(() => {
                    this.Close();
                }));
        }

        private void txt_search_KeyDown(object sender, KeyEventArgs e)
        {
            getItemSearchDown(e);
        }
        private void getItemSearchDown(KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    textSearch = txt_searh.Text.Trim();
                    var db = new DbManagement();
                    db.setTextSearchProduct(textSearch);
                    txt_searh.Text = "";
                    textSearch = "";
                    showProducts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().Name + "");
            }
        }
        private void getImageUser()
        {
            try
            {
                var db = new DbManagement();
                if (string.IsNullOrEmpty(db.getIconUser()))
                {
                }
                else
                {
                    image_user.Source = new BitmapImage(new Uri(db.getIconUser().ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().Name + "");
            }
        }

        //
        private void settingLanguage()
        {
            ResourceDictionary resourceDictionary = new ResourceDictionary();
            switch (Thread.CurrentThread.CurrentCulture.ToString())
            {
                case "en-US":
                    resourceDictionary.Source = new Uri("EN-Language.xaml", UriKind.Relative);
                    break;
                default:
                    resourceDictionary.Source = new Uri("EN-Language.xaml", UriKind.Relative);
                    break;
               
            }
            this.Resources.MergedDictionaries.Add(resourceDictionary);
        }

        private void btn_employee_Click(object sender, RoutedEventArgs e)
        {
            showEmployee();
        }

        private void btn_exit_app_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
