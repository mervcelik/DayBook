using Enitities.Concrete;
using Syncfusion.UI.Xaml.Schedule;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Screens
{
    /// <summary>
    /// Interaction logic for Calendar.xaml
    /// </summary>
    public partial class Calendar : UserControl
    {
        public ObservableCollection<Meeting> Meetings { get; set; }
        public Calendar()
        {
            InitializeComponent();
            scheduleWeek.ItemsSource = Meetings;
            scheduleMonth.ItemsSource = Meetings;
        }

        private void ShowMonth(Object sender,EventArgs e)
        {
            scheduleMonth.Visibility = Visibility.Visible;
            scheduleWeek.Visibility = Visibility.Hidden;
        }
        private void ShowWeek(Object sender, EventArgs e)
        {
            scheduleWeek.Visibility = Visibility.Visible;
            scheduleMonth.Visibility = Visibility.Hidden;
        }
    }
}
