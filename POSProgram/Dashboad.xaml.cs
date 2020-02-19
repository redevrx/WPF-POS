using LiveCharts;
using LiveCharts.Wpf;
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
using System.Windows.Threading;

namespace POSProgram
{
    /// <summary>
    /// Interaction logic for Dashboad.xaml
    /// </summary>
    public partial class Dashboad : UserControl
    {
        public Dashboad()
        {
            InitializeComponent();

            Dispatcher.Invoke((Action)(() => {
                DispatcherTimer dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
                dispatcherTimer.Tick += counttime;
                dispatcherTimer.Start();
            }));
            Dispatcher.Invoke((Action)(() => {
                SettingCharts();
            }));
            Dispatcher.Invoke((Action)(() => {
                picker_date.SelectedDate = DateTime.Now.Date;
            }));
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           getPriceToDate();
            sellTopProduct();
         
        }

        private void SettingCharts()
        {
            try
            {
                SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "2020",
                    Values = new ChartValues<double> { 10, 50, 39, 50 }
                }
            };

                //adding series will update and animate the chart automatically
                SeriesCollection.Add(new ColumnSeries
                {
                    Title = "2019",
                    Values = new ChartValues<double> { 11, 56, 42 }
                });

                //also adding values updates and animates the chart automatically
                SeriesCollection[1].Values.Add(48d);

                Labels = new[] { " January ", "February", "March", "April" };
                Formatter = value => value.ToString("N");

                DataContext = this;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "");
            }
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private void CountTime()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += counttime;
            dispatcherTimer.Start();
        }

       
      private void counttime(object sender, EventArgs e)
        {
            txt_time_use.Text  = DateTime.Now.ToString("HH:mm:ss");
        }

        //get total price to day
        private void getPriceToDate()
        {
            try
            {
                var db = new DbManagement();
                var date = DateTime.Now.ToString("dd/MMM/yyy");
                var price = db.getSumPrice(date).Rows[0][0].ToString();

                if (string.IsNullOrEmpty(price))
                {
                    txt_totalMoney.Text = "0.00";
                }
                else
                {
                    txt_totalMoney.Text = price;
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message + "Price");

            }


        }

        //get top sell product
        private void sellTopProduct()
        {
            try
            {
                var db = new DbManagement();

                if (db.getTopProdust().Rows.Count == 0)
                {
                    txt_top_product.Text = "None";
                }
                else
                {
                    var topItem = db.getTopProdust().Rows[0][0].ToString();
                    txt_top_product.Text = topItem;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Product");
            }
        }
    }

   
}
