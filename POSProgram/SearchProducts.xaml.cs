using System;
using System.Collections.Generic;
using System.Data;
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

namespace POSProgram
{
    /// <summary>
    /// Interaction logic for SearchProducts.xaml
    /// </summary>
    public partial class SearchProducts : Window
    {
        public SearchProducts(string tranNo)
        {
            InitializeComponent();
            getTransaction(tranNo);
            LoadData();
        }

        private DbManagement db = new DbManagement();
        private static string tran_no;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            checkClose();
          
           
        }

        private void checkClose()
        {
            if (string.IsNullOrEmpty(tran_no))
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.Close();
                    var notify = new SetNotify();
                    notify.Title = "POS Management Form";
                    notify.Message = "Transaction No Null or Product Id have value null";
                    notify.ShowNotfySucess();
                }));
            }
        }

        private void txt_searchProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                SearchItem(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "");
            }
        }
    
    //search product 
    private void SearchItem(TextChangedEventArgs e)
        {
            grid_products.ItemsSource = db.searchProductsCart(txt_searchProduct.Text);
            if (string.IsNullOrEmpty(txt_searchProduct.Text))
            {
                LoadData();
            }
        }
        private void LoadData()
        {
            grid_products.ItemsSource = db.getProductsCart().DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = this.grid_products.CurrentItem as DataRowView;
                var datarow = (item as DataRowView).Row;
                var cellvalue = datarow["ProductName"].ToString();
                var proId = datarow["ProductID"].ToString();
                var d = DateTime.Now.ToShortDateString();


                var saveCart = new SaveProductToCart(proId,tran_no);

                

                saveCart.Closed += delegate {
                    this.Opacity = 1;
                };

                if (saveCart != null)
                {
                    this.Opacity = 0.2;
                     saveCart.ShowDialog();
                   // MessageBox.Show(proId , tran_no);
                }


                /*var quty = new Quantity();
                this.Opacity = 0.1;
                quty.Closed += delegate{
                    this.Opacity = 1;
                };
                if (quty != null)
                {
                    quty.ShowDialog();
                }
                if (string.IsNullOrEmpty(db.Quantity))
                {
                    MessageBox.Show("Enter Quantity...");
                }
                else
                {
                    db.InserCart(tran_no, datarow["ProductID"].ToString(), Convert.ToDouble(datarow["price"].ToString()), db.Quantity, 0.0, 0.0, d, "Pending");
                }*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "");
            }
        }
        private void getTransaction(string t)
        {
            tran_no = t;
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.Close();
            }));
        }
    }
}
