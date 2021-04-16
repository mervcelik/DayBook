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
    /// Interaction logic for TimerPageThree.xaml
    /// </summary>
    public partial class TimerPageThree : UserControl
    {
        int hour;
        int minutes;
        int seconds;
        DispatcherTimer dispatcherTimer;

        public TimerPageThree()
        {
            InitializeComponent();
        }

        private void TimerStart(object sender, EventArgs e)
        {
            if (radialSlider.Value == 0)
            {
                MessageBox.Show("0'dan büyük bir değer seçiniz!");
            }
            else
            {
                radialSlider.Visibility = Visibility.Hidden;
                txtTime.Visibility = Visibility.Visible;
                DakikaGiriniz.Visibility = Visibility.Hidden;
                startTimer.Visibility = Visibility.Hidden;
                Stoptimer.Visibility = Visibility.Visible;

                int value = (int)radialSlider.Value;
                while (true)
                {
                    if (value > 60)
                    {
                        value = value - 60;
                        hour += 1;
                    }
                    else
                    {
                        minutes = value-1;
                        break;
                    }
                }
                seconds = 60;


                dispatcherTimer = new DispatcherTimer();
                dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer.Start();
            }
        }

        private void TimerStop(object sender, EventArgs e)
        {
            dispatcherTimer.Stop();
            radialSlider.Visibility = Visibility.Visible;
            DakikaGiriniz.Visibility = Visibility.Visible;
            startTimer.Visibility = Visibility.Visible;
            Stoptimer.Visibility = Visibility.Hidden;
            txtTime.Visibility = Visibility.Hidden;
            radialSlider.Value = 0;
            seconds = 0;
            minutes = 0;
            hour = 0;
        }



        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            seconds--;
            if(minutes == 0 && seconds == 0 && hour==0)
            {
                dispatcherTimer.Stop();
                radialSlider.Visibility = Visibility.Visible;
                DakikaGiriniz.Visibility = Visibility.Visible;
                startTimer.Visibility = Visibility.Visible;
                Stoptimer.Visibility = Visibility.Hidden;
                txtTime.Visibility = Visibility.Hidden;
                radialSlider.Value = 0;
                MessageBox.Show("Tamamlandı");
            }
            if (minutes == 0 && seconds == 0 && hour>0)
            {
                hour -= 1;
                seconds = 59;
                minutes = 59;
            }
            if (seconds==00)
            {
                seconds = 59;
                minutes -= 1;
            }
            string fmt = "00.##";
            txtTime.Text = hour.ToString(fmt)+":"+ minutes.ToString(fmt)+":"+seconds.ToString(fmt);
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
