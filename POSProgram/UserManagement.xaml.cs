using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Security.Cryptography;
using POSProgram.Entry;
using Microsoft.Win32;
using Syncfusion.UI.Xaml.Grid;
using System.Data;

namespace POSProgram
{
    /// <summary>
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : UserControl
    {
        public UserManagement()
        {
            InitializeComponent();

            Dispatcher.Invoke((Action)(() => { showtableLogin(); }));
        }

        private DbManagement db = new DbManagement();
        private string image_save;
        private void StackPanel_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().Name+"");
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           // Dispatcher.Invoke((Action)(() => { showtableLogin(); }));
        }

        private void grid_login_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            try
            {
                this.Dispatcher.BeginInvoke((Action)(() => {
                    getCellItem();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().Name+"");
            }
        }

        private void getCellItem()
        {
            if (grid_login.SelectedItem != null)
            {

                var rowData = this.grid_login.CurrentItem as DataRowView;
                var proprety = (rowData as DataRowView).Row;

                txt_id.Text = proprety["ID"].ToString();
                txt_employeeID.Text = proprety["EmployeeID"].ToString();
                txt_user.Text = proprety["UserName"].ToString();
                // txt_password.Password = passs;
                picker_date_birthDate.SelectedDate = Convert.ToDateTime(proprety["BirthDate"].ToString());
                showImage(proprety["Photo"].ToString());
                //  MessageBox.Show();
                txt_note.Text = proprety["Note"].ToString();
            }
            }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            Savedata();
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            Updatedata();
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            RemoveData();
        }
        //Encoding Passworld SHA256
        private string HasEnscripty(string value)
        {
            //new instance SHA
            SHA256Managed sHA256 = new SHA256Managed();
            //conver string to byte
            byte[] pass = Encoding.UTF8.GetBytes(value);
            // conver pass to SHA
            var has = sHA256.ComputeHash(pass);
            return Convert.ToBase64String(has);//show format string SHA
        }
        private void Savedata()
        {
            var d = picker_date_birthDate.SelectedDate.Value.Date.ToShortDateString();
            // db.Login(txt_user.Text , HasEnscripty(txt_password.Password)); .Value.ToString("dd/MM/yyy")
            // txt_id.Text = d;
           // MessageBox.Show(d);
            db.InsertLogin(txt_employeeID.Text,txt_user.Text,HasEnscripty(txt_password.Password),d,image_save,txt_note.Text);
            showtableLogin();
            Cleartext();
        }
   
     
        private void image_user_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    // Note that you can have more than one file.
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                    // Assuming you have one file that you care about, pass it off to whatever
                    // handling code you have defined.
                    image_save = files[0];
                    showImage(files[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "");
            }
        }

     
        //show data to gridview
        private void showtableLogin()
        {
            // grid_login.ItemsSource = db.getDataLogin().DefaultView;
            grid_login.ItemsSource = db.getDataLoginEntry().DefaultView;
        }
        //update data
        private void Updatedata()
        {
           var d = picker_date_birthDate.SelectedDate.Value.ToShortDateString();
            db.UpdateLogin(txt_id.Text , txt_employeeID.Text ,txt_user.Text , d,image_save,txt_note.Text);
            showtableLogin();
            Cleartext();
        }
        //remove data
        private void RemoveData()
        {
            db.RemoveLogin(txt_id.Text);
            showtableLogin();
            Cleartext();
        }

        private void btn_openfile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileImage();
        }
        //open get file image as imageBox
        private void OpenFileImage()
        {
            try
            {
                var openfile = new OpenFileDialog();
                openfile.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                if (openfile.ShowDialog() == true)
                {
                    var path = openfile.FileName;
                    image_save = path;
                    showImage(path);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "");
            }
        }
        //show imagebox
        private void showImage(string v)
        {
            try
            {
                //conver image path to bitmap uri path ....
                image_user.Source = new BitmapImage(new Uri(v));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Not your Image  :" + ex.Message);
            }
        }
        private void Cleartext()
        {
            txt_id.Text = "";
            txt_employeeID.Text = "";
            txt_user.Text = "";
            txt_password.Password = "";
            picker_date_birthDate.DisplayDate = DateTime.Now;
           // showImage("");
            //  MessageBox.Show();
            txt_note.Text = "";
        }
        private void btn_chang_password_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_password.Password))
                {
                    txt_password.Clear();
                }
                else
                {
                    db.UpdatePasswordLogin(txt_id.Text, HasEnscripty(txt_password.Password));
                    Cleartext();
                    showtableLogin();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().Name + "");
            }
        }
    }
}
