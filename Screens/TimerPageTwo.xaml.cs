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
using System.Windows.Threading;

namespace Screens
{
    /// <summary>
    /// Interaction logic for TimerPageTwo.xaml
    /// </summary>
    public partial class TimerPageTwo : UserControl
    {

        int hour;
        int minutes;
        int seconds;
        int previousHour;
        int previousMinutes;
        int previousSeconds;
        DispatcherTimer dispatcherTimer;
        public ObservableCollection<TimepickerValue> TimePickers { get; set; }

        public TimerPageTwo()
        {
            InitializeComponent();
            TimePickers = new ObservableCollection<TimepickerValue>();
            Roundlist.ItemsSource = TimePickers;
            txtReset.MouseUp += new MouseButtonEventHandler(Reset);
        }
        private void Reset(object sender, EventArgs e)
        {
            if(hour>0 || minutes>0 || seconds > 0)
            {
                dispatcherTimer.Stop();
                hour = 0;
                minutes = 0;
                seconds = 0;
                previousHour = 0;
                previousMinutes = 0;
                previousSeconds = 0;
                TimePickers.Clear();
                txtTime.Text = "00:00:00";
            }
            else
            {
                MessageBox.Show("Kronometreyi çalıştırmadınız!!");
            }

        }
        private void StartTimepicker(object sender, EventArgs e)
        {
            dispatcherTimer = new DispatcherTimer();

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        private void StopTimepicker(object sender, EventArgs e)
        {
            dispatcherTimer.Stop();
        }
        private void RoundTimepicker(object sender, EventArgs e)
        {
            var round=TimePickers.Count+1;
            var roundtime = "";
            if (round == 1)
            {
                roundtime = hour.ToString("00.##") + ":" + minutes.ToString("00.##") + ":" + seconds.ToString("00.##");

                previousMinutes = minutes;
                previousSeconds = seconds;
                previousHour = hour;
            }
            else if(round>1)
            {
                roundtime = (hour- previousHour).ToString("00.##") + ":" + (minutes- previousMinutes).ToString("00.##") + ":" + (seconds- previousSeconds).ToString("00.##");

                previousMinutes = minutes;
                previousSeconds = seconds;
                previousHour = hour;
            }
            TimepickerValue timepickerValue = new TimepickerValue { Round = round.ToString(), RoundTime = roundtime, ElapsedTime = hour.ToString("00.##") + ":" + minutes.ToString("00.##") + ":" + seconds.ToString("00.##") };
            TimePickers.Add(timepickerValue);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            seconds++;
            if (minutes == 59 && seconds == 59)
            {
                hour += 1;
                minutes = 0;
                seconds = 0;
            }
            if (seconds == 59)
            {
                seconds = 0;
                minutes += 1;
            }
            string fmt = "00.##";
            txtTime.Text = hour.ToString(fmt) + ":" + minutes.ToString(fmt) + ":" + seconds.ToString(fmt);
            CommandManager.InvalidateRequerySuggested();
        }

        public class TimepickerValue
        {
            public string Round { get; set; }
            public string RoundTime { get; set; }
            public string ElapsedTime { get; set; }
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
        }
    }
}
