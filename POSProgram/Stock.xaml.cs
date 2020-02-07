using Syncfusion.UI.Xaml.Grid;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace POSProgram
{
    /// <summary>
    /// Interaction logic for Stock.xaml
    /// </summary>
    public partial class Stock : UserControl
    {
        public Stock()
        {
            InitializeComponent();
            this.Dispatcher.Invoke((Action)(() => { ShowDataStock(); }));
            this.Dispatcher.Invoke((Action)(() => { LoadItemCate(); }));
            this.Dispatcher.Invoke((Action)(() => { LoadItemproduct(); }));
        }

        private DbManagement db = new DbManagement();
        private string cateId = "";
        private string proId = "";

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txt_stockId.Focus();
        }

        private void grid_stock_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            try
            {
                Dispatcher.BeginInvoke((Action)(() => {
                    getCellItem();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.GetType().Name + "Error");
            }
        }

        private void getCellItem()
        {
            if (grid_stock.SelectedItem != null)
            {
                var rowData = this.grid_stock.CurrentItem as DataRowView;
                var proprety = (rowData as DataRowView).Row;
                //var id = proprety["StockId"].ToString();
                txt_stockId.Text = proprety["StockId"].ToString();
                txt_unit.Text = proprety["Unit"].ToString(); 
                picker_dateOrder.SelectedDate = Convert.ToDateTime(proprety["StockDate"].ToString());
                txt_quantity.Text = proprety["Quantity"].ToString(); 
                txt_employeeid.Text = proprety["EmployeeId"].ToString(); 
                txt_bal.Text = proprety["Bal"].ToString();
                combo_categoryId.SelectedItem = proprety["CategoryName"].ToString();
                combo_productId.SelectedItem = proprety["ProductName"].ToString();


            }
            else
            {
                //   MessageBox.Show("Please select cell");
            }
        }

        private void container_view_control_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                TextBox t = e.Source as TextBox;
                if (t != null)
                {
                    switch (e.Key)
                    {
                        case Key.Enter:
                            t.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                            break;
                        default:
                            break;
                    }
                }
                else
                { 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().Name + "");
            }
        }

        //selecte item from conboboc into db
        private void combo_productId_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                proId = "";
                proId = db.getItemProductId(combo_productId.SelectedItem.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().Name + "");
            }
        }
        //selecte item from conboboc into db
        private void combo_categoryId_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                cateId = "";
                cateId = db.getItemCategoryId(combo_categoryId.SelectedItem.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().Name + "");
            }
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            InsertData();
            ShowDataStock();
            ClearText();
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            UpdateData();
            ShowDataStock();
            ClearText();
        }

        private void btn_remove_Click(object sender, RoutedEventArgs e)
        {
            RemoveData();
            ShowDataStock();
            ClearText();
        }

        private void ShowDataStock()
        {
            grid_stock.ItemsSource = db.GetDataStock().DefaultView;
        }

        // load item from table category to combobox cate
        private void LoadItemCate()
        {
            try
            {
                //check lenght item
                for (int i = 0; i < db.getItemCategoryName().Rows.Count; i++)
                {
                    combo_categoryId.Items.Add(db.getItemCategoryName().Rows[i][2].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().Name + "");
            }
        }
        // load item from table product to combobox proId
        private void LoadItemproduct()
        {
            try
            {
                //check lenght item
                for (int i = 0; i < db.getItemproductName().Rows.Count; i++)
                {
                   
                    combo_productId.Items.Add(db.getItemproductName().Rows[i][2].ToString());
                 //   MessageBox.Show(db.getItemproductName().Rows[i][2].ToString());
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message + ""); }
        }
        // save data into db 
        private void InsertData()
        {
            try
            {
                var date = picker_dateOrder.SelectedDate.Value.ToShortDateString();
                db.InsertStock(txt_stockId.Text , cateId ,proId , txt_unit.Text ,date,txt_quantity.Text,txt_employeeid.Text ,txt_bal.Text);
                MessageBox.Show("Save Successful...");
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message+"save Error"); }
        }
        private void UpdateData()
        {
            try
            {
                var date = picker_dateOrder.SelectedDate.Value.ToShortDateString();
                db.UpdateStock(txt_stockId.Text , cateId , proId ,txt_unit.Text ,date,txt_quantity.Text , txt_employeeid.Text , txt_bal.Text );
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message + ""); }
        }
        private void RemoveData()
        {
            try
            {
                var YesNo = MessageBox.Show("Remove data", "Question",MessageBoxButton.YesNo);
                if (YesNo == MessageBoxResult.Yes)
                {
                    db.SaveHistoryStock(txt_stockId.Text);
                    db.RemovStock(txt_stockId.Text);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message + ""); }
        }
        private void ClearText()
        {
            try
            {
                txt_stockId.Text = "";
                txt_unit.Text = "";
                picker_dateOrder.DisplayDate = DateTime.Now;// .SelectedDate = new DateTime(Convert.ToInt16(""));
                txt_quantity.Text = "";
                txt_employeeid.Text = "";
                txt_bal.Text = "";
                combo_categoryId.SelectedIndex = 0;
                combo_productId.SelectedItem = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().Name+"Text Error");
            }
        }

        private void btn_history_Click(object sender, RoutedEventArgs e)
        {
            var historyStock = new StockHistory();
            historyStock.ShowDialog();
        }

       
    }
}
