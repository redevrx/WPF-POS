using Syncfusion.Data;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace POSProgram
{
    /// <summary>
    /// Interaction logic for POS.xaml
    /// </summary>
    public partial class POS : Window
    {
        private static string EmId;
        public POS(string emId)
        {
            InitializeComponent();

            EmId = emId;

            this.Dispatcher.BeginInvoke(new Action(() => {
                //grid_cart.RefreshColumns();
                LoadDatarefresh();
            }));

            txt_search.IsEnabled = false;
            txt_money.IsEnabled = false;
        }

        private DbManagement db = new DbManagement();
        
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

        private void btn_cloes_Click(object sender, RoutedEventArgs e)
        {
            var main = new MainWindow(db.StatusUser(), db.UserName() , EmId);
            main.Show();
            this.Dispatcher.BeginInvoke(new Action(() => {
                this.Close();
            }));
        }
        private void btn_refreshData_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() => {
                //grid_cart.RefreshColumns();
                LoadDatarefresh();
                var notify = new SetNotify();
                notify.Title = "POS Management Form";
                notify.Message = "Refresh Data Successful...";
                notify.ShowNotfyInformation();

                clearText();
            }));
        }


        private void LoadDatarefresh()
        {
            try
            {
                // grid_cart.ItemsSource = "";
                DbManagement db = new DbManagement();

                if (grid_cart != null)
                {
                    grid_cart.ItemsSource = db.DataCart().DefaultView;
                }
               
                //grid_cart.DataContext = db.BrandData().ToList<BrandEntry>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Grid");
            }
        }

        private void grid_cart_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            try
            {
                this.Dispatcher.BeginInvoke((Action)(() => {
                    getCellItem();
                }));
                // getCellItem();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Error");
            }
        }
        private void getCellItem()
        {
            try
            {
                var item = this.grid_cart.CurrentItem as DataRowView;
                var datarow = (item as DataRowView).Row;
                // var cellvalue = datarow["ID"].ToString();
                //var id = grid_cart.GetCellValue
                //var rowData = grid_cart.GetRecordAtRowIndex(1);
                //var proprety = this.grid_cart.View.GetPropertyAccessProvider();
                //var cellValue = proprety.GetValue(rowData, "ID");
                txt_Id.Text = datarow["ID"].ToString();
                txt_transaction.Text = datarow["TransactionNo"].ToString();
                // txt_disCount.Text = datarow["Discount"].ToString();
                //txt_totalPrice.Text = datarow["Total"].ToString();
                // totalPrice = Convert.ToDouble(datarow["Total"].ToString());
                //this.grid_cart.AutoGeneratingColumn+=

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Error");
            }
        }

        private void btn_transaction_Click(object sender, RoutedEventArgs e)
        {
            clearText();
            getTransactionNo();
        }
        private void getTransactionNo()
        {
            try
            {
                Int64 s2 = Convert.ToInt64(DateTime.Now.ToString("dMyyHH"));
                var r = new Random();
                var s1 = r.Next(000, 102000);
                var s3 = r.Next(00000, 1000000);
                txt_transaction.Text = (s1 + 1) + "" + s2 + "" + (s3 + 1);
                var notify = new SetNotify();
                notify.Title = "POS Management Form";
                notify.Message = "Create Transaction No Successful...";
                notify.ShowNotfyInformation();


                var searchProduct = new SearchProducts(txt_transaction.Text);

                //set back grounnd

                searchProduct.Closed += delegate {
                    this.Opacity = 1;

                    //close search form alter add product to cart complete
                    grid_cart.ItemsSource = db.searchCart(txt_transaction.Text);
                    getTotalAmount(txt_transaction.Text);
                };
                if (searchProduct != null)
                {
                    this.Opacity = 0.2;
                    searchProduct.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().Name + "");
            }
        }

        private void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                SearchProductCart(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "");
            }
        }
        private void SearchProductCart(TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_search.Text))
            {
                //not ation to do
            }
            else
            {

                grid_cart.ItemsSource = db.searchCart(txt_search.Text).DefaultView;
                txt_search.IsEnabled = false;
                getTotalAmount(txt_search.Text);

            }

        }

        private void btn_searchproducts_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                txt_search.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RemoveItemCart(e);
        }
        private void RemoveItemCart(RoutedEventArgs e)
        {
            try
            {
                var item = this.grid_cart.CurrentItem as DataRowView;
                var datarow = (item as DataRowView).Row;
                var tranId = datarow["TransactionNo"].ToString();
                db.RemoveCart(tranId);
                var notify = new SetNotify();
                notify.Title = "POS Management Form";
                notify.Message = "Remove Data Successful...";
                notify.ShowNotfyInformation();

                this.Dispatcher.BeginInvoke(new Action(() => {
                    LoadDatarefresh();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "");
            }
        }

        private void btn_setter_payment_Click(object sender, RoutedEventArgs e)
        {
            txt_money.IsEnabled = true;
        }

        private void txt_money_TextChanged(object sender, TextChangedEventArgs e)
        {
            calulateMoney(e);
        }

        private void calulateMoney(TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_money.Text))
            {
                //todo
            }
            else
            {
                double change = Convert.ToDouble(txt_money.Text) - Convert.ToDouble(txt_totalPrice.Text);
                txt_change_customer.Text = ""+change+".00";
            }
        }

        //report sell
        private void btn_receipt_Click(object sender, RoutedEventArgs e)
        {
            showPrintPreview();
        }

        //print bill and comfrim sell
        private void showPrintPreview()
        {
            updateStockQuant();

            var previewPrint = new Report(txt_transaction.Text , txt_money.Text, txt_change_customer.Text);
            clearText();
            previewPrint.ShowDialog();
           
        }

        //update stock quantity 
        private void updateStockQuant()
        {
            var db = new DbManagement();

            for (int i = 0; i < db.searchCart(txt_transaction.Text).Rows.Count; i++)
            {
                db.UpdateQuantStock(db.searchCart(txt_transaction.Text).Rows[i][2].ToString(), Convert.ToInt32(db.searchCart(txt_transaction.Text).Rows[i][4].ToString()));
            }
        }

        // set total price to txt_total
        private void getTotalAmount(string tranID)
        {
            txt_totalPrice.Text = db.getTotalAmont(tranID).Rows[0][0].ToString();
        }

        //clear text
        private void clearText()
        {
            txt_Id.Text = "";
            txt_transaction.Text = "";
            txt_totalPrice.Text = "";
            txt_money.Text = "";
            txt_change_customer.Text = "";
            txt_search.Text = "";
        }
            
    }
}
