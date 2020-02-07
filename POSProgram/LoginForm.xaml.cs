using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Security.Cryptography;

namespace POSProgram
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().Name + "");
            }
        }

        private void StackPanel_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                TextBox t = e.Source as TextBox;
                if (t != null)
                {
                    switch (e.Key)
                    {
                        case Key.Enter:
                            t.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                            break;
                           // case Key.
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().Name + "");
            }
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                login();
                //Application.Current.Shutdown();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().Name + "");
            }
        }
    
        //login data
        private void login()
        {
             var db = new DbManagement();
            //db.checkConnect();
            //this.;
            db.Login(txt_User_login.Text, HasScrip(txt_password_login.Password));
           // MessageBox.Show(db.CheckLogin()+"");
            if (db.CheckLogin() == true)
            {
                // Application.Current.Shutdown();
                this.Dispatcher.BeginInvoke(new Action(() => {
                    this.Close();
                }));
            } 
        }
        private string HasScrip(string pass)
        {
            byte[] datapass = Encoding.UTF8.GetBytes(pass);
            SHA256Managed sHA256 = new SHA256Managed();
            var has = sHA256.ComputeHash(datapass);
            return Convert.ToBase64String(has);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txt_User_login.Focus();
        }
    }
}
