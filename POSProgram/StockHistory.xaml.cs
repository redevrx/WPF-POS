using Syncfusion.UI.Xaml.Grid;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace POSProgram
{
    /// <summary>
    /// Interaction logic for StockHistory.xaml
    /// </summary>
    public partial class StockHistory : Window
    {
        public StockHistory()
        {
            InitializeComponent();

        }

        private string sid;
        private string cateId;
        private string proId;
        private string unit;
        private string date;
        private string quantity;
        private string emid;
        private string bal;
        private string status;

        private SetNotify notify = new SetNotify();
        private void btn_LoadData_Click(object sender, RoutedEventArgs e)
        {
            var db = new DbManagement();
            string sdate = start_date.SelectedDate.Value.ToShortDateString();
            string edate =  end_date.SelectedDate.Value.ToShortDateString();
            grid_History_stock.ItemsSource = db.GetDataHistoryStock(sdate,edate).DefaultView;
        }

        private void btn_restor_data_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var db = new DbManagement();
                db.InsertStock(sid, cateId, proId, unit, date, quantity, emid, bal);
                db.RemoveHistoryStock(sid);
                notify.Title = "Stock Management Form";
                notify.Message = "Restore Products Successful...";
                notify.ShowNotfySucess();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "");
            }
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
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
        //get Item from datagrid to stock
        private void grid_History_stock_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            try
            {
                this.Dispatcher.BeginInvoke((Action)(() => {
                    getCellItem();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "");
            }
        }
        private void getCellItem()
        {
            // s = stock
            if (grid_History_stock.SelectedItem != null)
            {
                var item = this.grid_History_stock.CurrentItem as DataRowView;
                var proprety = (item as DataRowView).Row;
                sid = proprety["StockID"].ToString();
                cateId = proprety["CategoryID"].ToString();
                proId = proprety["ProductID"].ToString();
                unit = proprety["Unit"].ToString();
                date = proprety["StockDate"].ToString();
                quantity = proprety["Quantity"].ToString();
                emid = proprety["EmployeeID"].ToString();
                bal = proprety["Bal"].ToString();
                status = proprety["status"].ToString();

            }
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() => {
                this.Close();
            }));
        }
    }
}
