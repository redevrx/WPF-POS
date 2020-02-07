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
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Products : UserControl
    {
        public Products()
        {
            InitializeComponent();

            Dispatcher.Invoke((Action)(() => {
                ShowData();
            }));
            Dispatcher.Invoke((Action)(() => {
                getItemBrand();
            }));
            Dispatcher.Invoke((Action)(() => {
                getItemCategory();
            }));
            Dispatcher.Invoke((Action)(() => {
                Searchproducts();
            }));

        }

        private DbManagement db = new DbManagement();
        private string brandId = "";
        private string cateId = "";
        //Dispatcher.Invoke((Action)(() => { }));
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txt_Id.Focus();
        }
        private void btn_productSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
              
                db.insertProduct(txt_Id.Text, txt_productId.Text, txt_productName.Text, txt_supplierId.Text, cateId, brandId, txt_unit.Text, txt_price.Text);
                ShowData();
                Cleartxt();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.GetType().Name+"");
            }
        }

        private void btn_productUpdate_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(cateId + "" + brandId);
            db.UpdateProduct(txt_Id.Text, txt_productId.Text, txt_productName.Text, txt_supplierId.Text,cateId, brandId, txt_unit.Text, txt_price.Text);
            ShowData();
            Cleartxt();
        }

        private void btn_productDelete_Click(object sender, RoutedEventArgs e)
        {
            db.removeProduct(txt_Id.Text);
            ShowData();
            Cleartxt();
        }
        private void ShowData()
        {
            grid_products.ItemsSource = db.getDataProducts().DefaultView;
        }
     
        //brand Name
        private void getItemBrand()
        {
           // MessageBox.Show(db.getItemBrandId().Rows[0][0].ToString());
            for (int i = 0; i < db.getItemBrandName().Rows.Count; i++)
            {
                combo_brand.Items.Add(db.getItemBrandName().Rows[i][2].ToString());
            }
        }
        //category Name
        private void getItemCategory()
        {
            for (int i = 0; i < db.getItemCategoryName().Rows.Count; i++)
            {
                combo_category.Items.Add(db.getItemCategoryName().Rows[i][2].ToString());
                //combo_category.ItemsSource =
            }
        }
        //brand Id
        private void combo_brand_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                brandId = "";
                var data = combo_brand.SelectedItem.ToString();
                string id = db.getItemBrandId(data);
                brandId = id;
                //MessageBox.Show(id);
                //  return id;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "");
            }
        }
        //get id category use in save data
        private void combo_category_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //
            try
            {
                cateId = "";
                var data = combo_category.SelectedItem.ToString();
                string id = db.getItemCategoryId(data);
                cateId = id;
                //MessageBox.Show(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "");
            }
        }

        private void Cleartxt()
        {
            txt_Id.Text = "";
            txt_productId.Text = "";
            txt_productName.Text = "";
            txt_supplierId.Text = ""; ;
            combo_brand.SelectedIndex = 0;
            combo_category.SelectedIndex = 0;
            txt_unit.Text = "";
            txt_price.Text = "";
           
        }

        private void container_Preview_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox th = e.Source as TextBox;
            if (th != null)
            {
                switch (e.Key)
                {
                    case Key.Enter:
                        // Object n = FocusNavigationDirection.Next.GetType.FindName(th.FindName);

                       // MessageBox.Show(th.FindName() + "");
                        th.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                      
                       // MessageBox.Show();
                        break;
                  //  case Key.Down:
                    default:
                        break;
                }
            }
        }

        private void grid_products_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            try
            {
                this.Dispatcher.BeginInvoke((Action)(() => {
                    getCellItem();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+"");
            }
        }
        private void getCellItem()
        {
            //MessageBox.Show(index.ToString());
            if (grid_products.SelectedItem != null)
            {
                var rowData = this.grid_products.CurrentItem as DataRowView;
                var proprety = (rowData as DataRowView).Row;

                txt_Id.Text =  proprety["ID"].ToString();
                txt_productId.Text = proprety["ProductID"].ToString();
                txt_productName.Text = proprety["ProductName"].ToString();
                txt_supplierId.Text = proprety["SupplierID"].ToString();
                combo_category.SelectedItem = proprety["CategoryName"].ToString();
                txt_unit.Text = proprety["Unit"].ToString();
                txt_price.Text = proprety["Price"].ToString();
                combo_brand.SelectedItem = proprety["BrandName"].ToString();
                //txt_quantity.Text = proprety["Quantity"].ToString();
               // MessageBox.Show(proprety["CategoryID"].ToString());

            }
        }
        //Products.Preview

            //search item product get value from mainwindow textSearch
        private void Searchproducts()
        {
            try
            {
                if (db.CheckTextSeach() == true)
                {
                    grid_products.ItemsSource = db.SearchProducts().DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().Name + "");
            }
        }
    }
}
