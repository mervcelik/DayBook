using DataAccess.AWSclouds;
using Enitities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Screens
{
    /// <summary>
    /// Interaction logic for RegisterScreen.xaml
    /// </summary>
    public partial class RegisterScreen : Window
    {
        public AwsUser _awsUser;
        public RegisterScreen()
        {
            InitializeComponent();
            _awsUser = new AwsUser();
        }

        private void ButtonClose_click(object sender, EventArgs e)
        {
            base.Close();
        }
        private void ButtonBack(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            base.Close();
        }

        private void registerbtn_Click(object sender, RoutedEventArgs e)
        {
            string Name = txtName.Text;
            string LastName = txtLastname.Text;
            string Email = txtEmail.Text;
            string Password = password.Password;
            string PasswordTry = passwordtry.Password;
            if (Name == "" || LastName == "" || Email == "" || Password == "" || PasswordTry == "") 
            {
                 MessageBox.Show("Boş alan bırakmayınız");
            }
            else if (Password != PasswordTry)
            {
                MessageBox.Show("Parolalar eşleşmiyor");
            }
            else if (_awsUser.GetUserEmail(Email).Success)
            {
                MessageBox.Show(Email + " Bu Email kullanıldı.\n yeni kayıt için başka bir Email kullanınız");
            }
            else if (!_awsUser.GetUserEmail(Email).Success)
            {
                User user = new User { Email = Email, Name = Name, LastName = LastName, Password = Password };
                if (_awsUser.AddUser(user).Success)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();

                    base.Close();
                }

            }

        }
    }
}
