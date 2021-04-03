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

namespace Screens
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btnLogin.Click += new RoutedEventHandler(btnLoginClick);
            txtRegister.MouseUp += new MouseButtonEventHandler(RegisterShow);
        }

        private void RegisterShow(object sender, EventArgs e)
        {            
            RegisterScreen screen = new RegisterScreen();
            screen.Show();

            base.Close();
        }

        private void btnLoginClick(object sender, EventArgs e)
        {
            if (txtEmail.Text=="" && password.Password=="" )
            {
                
                MessageBox.Show("Boş alan bırakmayınız! ");
            }
            else
            {

                StartScreen startScreen = new StartScreen();
                startScreen.Show();

                base.Close();


            }

        }
        private void ButtonClose(object sender ,EventArgs e)
        {
            base.Close();
        }
    }
}
