using POSProgram.Entry;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace POSProgram
{
    /// <summary>
    /// Interaction logic for Category.xaml
    /// </summary>
    public partial class Category : UserControl
    {
        public Category()
        {
            InitializeComponent();

            Dispatcher.Invoke((Action)(() => { showData(); }));
        }

        private DbManagement db = new DbManagement();
        private SetNotify notify = new SetNotify(); 
        private void btn_saveCategory_Click(object sender, RoutedEventArgs e)
        {
            db.insertCategory(txt_Id.Text, txt_categoryId.Text, txt_categoryName.Text, txt_categoryDescrip.Text);
            showData();
            Clear();
            notify.Title = "Categorys Form";
            notify.Message = "Save Data in Database Successful...";
            notify.ShowNotfySucess();
        }

        private void btn_UpdateCategory_Click(object sender, RoutedEventArgs e)
        {
            db.UpdateCategory(txt_Id.Text ,txt_categoryId.Text , txt_categoryName.Text , txt_categoryDescrip.Text);
            showData();
            Clear();
            notify.Title = "Categorys Form";
            notify.Message = "Update Data in Database Successful...";
            notify.ShowNotfySucess();
        }

        private void btn_RemoveCategory_Click(object sender, RoutedEventArgs e)
        {
            db.RemoveCategory(txt_categoryId.Text);
            showData();
            Clear();
            notify.Title = "Categorys Form";
            notify.Message = "Remove Data in Database Successful...";
            notify.ShowNotfySucess();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txt_Id.Focus();
        }

        private void grid_category_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (grid_category.SelectedItem != null)
                {
                    var index = grid_category.SelectedItem;
                    string id = (grid_category.SelectedCells[0].Column.GetCellContent(index) as TextBlock).Text;
                    string cid = (grid_category.SelectedCells[1].Column.GetCellContent(index) as TextBlock).Text;
                    string cname = (grid_category.SelectedCells[2].Column.GetCellContent(index) as TextBlock).Text;
                    string descrip = (grid_category.SelectedCells[3].Column.GetCellContent(index) as TextBlock).Text;

                    txt_Id.Text = id;
                    txt_categoryId.Text = cid;
                    txt_categoryName.Text = cname;
                    txt_categoryDescrip.Text = descrip;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void showData()
        {
            grid_category.ItemsSource = db.getDataCategory().ToList<CategoryEntry>();
        }
        private void Clear()
        {
            txt_Id.Text = "";
            txt_categoryId.Text = "";
            txt_categoryName.Text = "";
            txt_categoryDescrip.Text = "";
        }

        private void grid_container_text_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                TextBox th = e.Source as TextBox;
                if (th != null)
                {
                    switch (e.Key)
                    {
                        case Key.Enter:
                            TraversalRequest tr = new TraversalRequest(FocusNavigationDirection.Next);
                            tr.Wrapped = true;
                            ((Control)e.Source).MoveFocus(tr);
                            break;
                        //  case Key.Down:
                        default:
                            break;
                    }
                }
                else
                {
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.GetType() + ""); }
        }
    }
}
