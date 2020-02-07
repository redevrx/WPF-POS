using System;
using System.Threading.Tasks;
using System.Windows;

namespace POSProgram
{
    /// <summary>
    /// Interaction logic for LoadingPage.xaml
    /// </summary>
    /// 
    public partial class LoadingPage : Window,IDisposable
    {
        public Action Worker {get; set;}
    public LoadingPage(Action worker)
        {
            InitializeComponent();
            Worker = worker ?? throw new ArgumentNullException();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(Worker).ContinueWith(t => { this.Close(); }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void Dispose()
        {
            Application.Current.Shutdown();
        }
    }
}
