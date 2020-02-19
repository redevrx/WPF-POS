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
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Window
    {
        private static string tranId;
        private static string amount;
        private static string changePrice;
      
        public Report(string  tranID , string amountPrice,string change)
        {
            InitializeComponent();
            tranId = tranID;
            amount = amountPrice;
            changePrice = change;
           
        }

      

        private void btn_report_Click(object sender, RoutedEventArgs e)
        {
            btn_report.Visibility = Visibility.Hidden;
            btn_close.Visibility = Visibility.Hidden;

            var printD = new PrintDialog();
            if (printD.ShowDialog() == true)
            {
                printD.PrintVisual(this.print, "report");
            }
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadData();
        }


        private void loadData()
        {
            try
            {
                var db = new DbManagement();
               
                var d = DateTime.Now.ToString("dd/MMM/yyy");


                txt_employeeID.Text = db.EmId();
                txt_date.Text = d;
                txt_tranId.Text = tranId;

                //lis
                ListDescrip();

                txt_totalPrice.Text = db.getTotalAmont(tranId).Rows[0][0].ToString();
                txt_user_Amount.Text = amount;


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void ListDescrip()
        {
            var db = new DbManagement();

            list_descrption.Items.Clear();
            list_discount.Items.Clear();
            list_price.Items.Clear();
            list_quant.Items.Clear();
            for (int i = 0; i < db.getCartProductName(tranId).Rows.Count; i++)
            {
                list_descrption.Items.Add(db.getCartProductName(tranId).Rows[i][1].ToString());
                list_price.Items.Add(db.getCartProductName(tranId).Rows[i][2].ToString());
                list_discount.Items.Add(db.getCartProductName(tranId).Rows[i][3].ToString());
                list_quant.Items.Add(db.getCartProductName(tranId).Rows[i][4].ToString());
            }
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(()=> {
                this.Close();
            }));
        }
    }
}
