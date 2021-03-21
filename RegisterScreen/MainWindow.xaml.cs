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

namespace RegisterScreen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btnRegister.Click += new RoutedEventHandler(buttonClick);
        }

        private void buttonClick(object sender, EventArgs e)
        {
            if (txtMail.Text == "" && password.Password == "" && txtName.Text=="" && txtLastname.Text==""&&passwordtry.Password=="")
            {
                Button clicked = (Button)sender;
                MessageBox.Show(" Boş alan bıraktınız ");
            }
            else if (password.Password.Length<6)
            {
                Button clicked = (Button)sender;
                MessageBox.Show(" Şifre 6 basamaktan küçük olamaz! ");
            }
            else if (password.Password != passwordtry.Password)
            {
                Button clicked = (Button)sender;
                MessageBox.Show(" Şifreler aynı değil! ");
            }
            else
            {

            }


        }
    }
}

