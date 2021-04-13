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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Screens
{
    /// <summary>
    /// Interaction logic for Timer.xaml
    /// </summary>
    public partial class Timer : UserControl
    {
        
        public Timer()
        {
            InitializeComponent();
            Grid.Children.Add(new TimerPageTwo()); 
        }
        private void selectedClock(object sender , EventArgs e)
        {
            Grid.Children.Clear();
            Grid.Children.Add(new TimerPageOne());
        }
        private void selectedkronometre(object sender, EventArgs e)
        {
            Grid.Children.Clear();
            Grid.Children.Add(new TimerPageTwo());
        }
        private void selectedTime(object sender, EventArgs e)
        {
            Grid.Children.Clear();
            Grid.Children.Add(new TimerPageThree());
        }
       
    }
}
