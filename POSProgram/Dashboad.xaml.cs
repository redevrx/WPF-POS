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

       
         void counttime(object sender, EventArgs e)
        {
            txt_time_use.Text  = DateTime.Now.ToString("HH:mm:ss");
        }
    }

   
}
