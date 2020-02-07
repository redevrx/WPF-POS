using POSProgram.Entry;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace POSProgram
{
    /// <summary>
    /// Interaction logic for Brand.xaml
    /// </summary>
    public partial class Brand : UserControl
    {
        public Brand()
        {
            InitializeComponent();

            Dispatcher.Invoke((Action)(() => { updateData(); }));
        }
         private DbManagement db = new DbManagement();
        private SetNotify notify = new SetNotify();
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txt_bID.Focus();
        }

     

        private void btn_saveBrand_Click(object sender, RoutedEventArgs e)
        {
            db.insertBrand( txt_bID.Text,txt_brandID.Text , txt_brandName.Text);
            updateData();
            ClearText();
            notify.Title = "Brand Form";
            notify.Message = "Save Data in Database Successful...";
            notify.ShowNotfySucess();

        }

        private void btn_UdateBrand_Click(object sender, RoutedEventArgs e)
        {
            db.UpdateBrand(txt_bID.Text , txt_brandID.Text , txt_brandName.Text);
            updateData();
            ClearText();
            notify.Title = "Brand Form";
            notify.Message = "Update Data in Database Successful...";
            notify.ShowNotfySucess();
        }

        private void btn_deleteBrand_Click(object sender, RoutedEventArgs e)
        {
            db.RemoveBrand(txt_bID.Text);
            updateData();
            ClearText();
            notify.Title = "Brand Form";
            notify.Message = "Remove Data in Database Successful...";
            notify.ShowNotfySucess();
        }
        //show data
        private void updateData()
        {
            grid_Brand.ItemsSource = null;
          
            grid_Brand.ItemsSource = db.BrandData().ToList<BrandEntry>();
        }

      
        private void ClearText()
        {
            txt_bID.Text = "";
            txt_brandID.Text = "";
            txt_brandName.Text = "";
        }


        private void grid_Brand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
              
                if (grid_Brand.SelectedItem != null)
                {
                    var index = grid_Brand.SelectedItem;
                    // MessageBox.Show(row["brandId"].ToString());
                    string id = (grid_Brand.SelectedCells[0].Column.GetCellContent(index) as TextBlock).Text;
                    string brandId = (grid_Brand.SelectedCells[1].Column.GetCellContent(index) as TextBlock).Text;
                    string brandName = (grid_Brand.SelectedCells[2].Column.GetCellContent(index) as TextBlock).Text;


                    txt_bID.Text = id;
                    txt_brandID.Text = brandId;
                    txt_brandName.Text = brandName;


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void grid_container_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                TextBox th = e.Source as TextBox;
                if (th != null)
                {
                    switch (e.Key)
                    {
                        case Key.Enter:
                            th.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                            break;
                        default:
                            break;
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "");
            }
        }
    }
}
