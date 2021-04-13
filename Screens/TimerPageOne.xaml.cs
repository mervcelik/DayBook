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
    /// Interaction logic for TimerPageOne.xaml
    /// </summary>
    public partial class TimerPageOne : UserControl
    {
        public ObservableCollection<Clock> Clocks { get; set; }
        public TimerPageOne()
        {
            InitializeComponent();
            Clocks = new ObservableCollection<Clock>();
            Clock clock = new Clock { CityName = "denizli", CityClock = "00:00" };
            Clocks.Add(clock);
            Clocks.Add(clock);
            Clocks.Add(clock);
            Clocks.Add(clock);
            Clocks.Add(clock);
            Clocks.Add(clock);
            Clocks.Add(clock);
            ClockBox.ItemsSource = Clocks;
        }
        public class Clock{
            public string CityName { get; set; }
            public string CityClock { get; set; }
        }
    }
}
