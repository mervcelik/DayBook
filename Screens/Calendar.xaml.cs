using Business.concrete;
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
        public ScheduleManager scheduleManager;
        public Calendar()
        {
            InitializeComponent();
            ScheduleManager scheduleManager = new ScheduleManager();
            scheduleWeek.ItemsSource = scheduleManager.Events;
            scheduleMonth.ItemsSource = scheduleManager.Events;

            this.scheduleWeek.AppointmentEditorClosed += Schedule_AppointmentEditorClosed;
            this.scheduleMonth.AppointmentEditorClosed += Schedule_AppointmentEditorClosed;


        }

        private void Schedule_AppointmentEditorClosed(object sender, AppointmentEditorClosedEventArgs e)
        {
            var appointment = e.EditedAppointment as ScheduleAppointment;
            if (appointment != null)
            {
                Meeting meeting = new Meeting
                {
                    EventName = appointment.Subject,
                    Notes = appointment.Notes,
                    Location = appointment.Location,
                    From = appointment.StartTime,
                    To = appointment.EndTime,
                    IsAllDay = appointment.AllDay
                };
                scheduleManager.Add(meeting);
                e.Handled = true;
            }
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
