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

namespace POSProgram
{
    /// <summary>
    /// Interaction logic for SearchProductInCart.xaml
    /// </summary>
    public partial class SaveProductToCart : Window
    {
        public SaveProductToCart(string proId, string tranNo)
        {
            InitializeComponent();

            getProId(proId,tranNo);       
             getPname();
        }

        private static string pId;
        private static string tranNO;
        private double price;
        private SetNotify notify = new SetNotify();
       

        private void SaveOrderCart()
        {
            try
            {
                if (string.IsNullOrEmpty(txt_discount.Text))
                {
                    var db = new DbManagement();
                    var d = DateTime.Now.ToString("dd/MMM/yyy");
                    db.InserCart(tranNO, pId, price, txt_quantity.Text, 0.0, 0.0, d, "Pending");
                    notify.Title = "POS Management Form";
                    notify.Message = "Sales Products Successful...";
                    notify.ShowNotfySucess();
                    notify.Title = "POS Management Form";
                    notify.Message = "Product :" + txt_product_name.Text + " Quantity :" + txt_quantity.Text;
                    notify.ShowNotfyInformation();
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        this.Close();
                    }));
                }
                else
                {
                   
                         var db = new DbManagement();
                    var d = DateTime.Now.ToString("dd/MMM/yyy");
                    db.InserCart(tranNO, pId, price, txt_quantity.Text, Convert.ToDouble(txt_discount.Text), 0.0, d, "Pending");
                    notify.Title = "POS Management Form";
                    notify.Message = "Sales Products Successful...";
                    notify.ShowNotfySucess();
                    notify.Title = "POS Management Form";
                    notify.Message = "Product :" + txt_product_name.Text + " Quantity :" + txt_quantity.Text;
                    notify.ShowNotfyInformation();
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        this.Close();
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void getProId(string Proid,string transactionNo)
        {
            pId = Proid;
            tranNO = transactionNo;
        }
        private void getPname()
        {
            try
            {
               // MessageBox.Show(pId,tranNO);
                var db = new DbManagement();
                var Pname = db.getSearchProductName(pId).Rows[0][2].ToString();
                string p = db.getSearchProductName(pId).Rows[0][7].ToString();
              
                txt_product_name.Text = Pname;
                price = Convert.ToDouble(p);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Not find Product Id");
                this.Dispatcher.BeginInvoke((Action)(() => { this.Close(); }));
            }
            
        }
       
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+"");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tranNO))
                {
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        this.Close();
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().Name + "");
            }
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.Close();
            }));
        }

        private void btn_saveQuantity_Click(object sender, RoutedEventArgs e)
        {
            checkStock();
            updateStockQuant();
            SaveOrderCart();
        }


        private void updateStockQuant()
        {
            var db = new DbManagement();

            db.UpdateQuantStock(pId,Convert.ToInt32(txt_quantity.Text));
        }
        private void checkStock()
        {
            var db = new DbManagement();
            int quantStock = db.numnerQutStock(pId);

            if (quantStock <= 10)
            {
                MessageBox.Show("this Product quantity min....");
            }
        }
    }
}
