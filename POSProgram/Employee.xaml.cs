using POSProgram.Entry;
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

namespace POSProgram
{
    /// <summary>
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class Employee : UserControl
    {
        public Employee()
        {
            InitializeComponent();
        }

        private DbManagement db = new DbManagement();
        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            db.addItemEmployee(txt_employeeID.Text , txt_user.Text);
            loadData();
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            db.UpdateItemEmployee(txt_id.Text , txt_employeeID.Text , txt_user.Text);
            loadData();
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            db.RemoveItemEmployee(txt_id.Text);
            loadData();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadData(); 
        }

        private void grid_Employee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (this.grid_Employee.SelectedItem != null)
                {
                    var index = grid_Employee.SelectedItem;
                    // MessageBox.Show(row["brandId"].ToString());
                    string id = (grid_Employee.SelectedCells[0].Column.GetCellContent(index) as TextBlock).Text;
                    string emId = (grid_Employee.SelectedCells[1].Column.GetCellContent(index) as TextBlock).Text;
                    string name = (grid_Employee.SelectedCells[2].Column.GetCellContent(index) as TextBlock).Text;


                    txt_id.Text = id;
                    txt_employeeID.Text = emId;
                    txt_user.Text = name;


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void loadData()
        {
            this.grid_Employee.ItemsSource = db.getEmployee().ToList<EmployeeEntity>();
        }
    }
}
