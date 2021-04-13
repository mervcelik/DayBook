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
        int startHours;
        int startMinutes;
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

                dispatcherTimer = new DispatcherTimer();

                dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer.Start();
                startHours = DateTime.Now.Hour;
                startMinutes = DateTime.Now.Minute;

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

            seconds = DateTime.Now.Second;
            minutes = DateTime.Now.Minute - startMinutes;
            hour = DateTime.Now.Hour - startHours;
            txtTime.Text = hour + ":" + minutes + ":" + seconds;

            CommandManager.InvalidateRequerySuggested();
        }
    }
}
