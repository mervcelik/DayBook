
using Core.FileHelper;
using DataAccess.AWSclouds;
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
        public ObservableCollection<Meeting> MeetingList { get; set; }
        public AwsMeeting _awsMeeting;
        public int UId;
        public Calendar()
        {
            InitializeComponent();
            UId = FileManager.Read();
            _awsMeeting = new AwsMeeting();
            MeetingList = new ObservableCollection<Meeting>();
            MeetingList = _awsMeeting.GetMeeting(UId).Data;
            scheduleWeek.ItemsSource = MeetingList;
            scheduleMonth.ItemsSource = MeetingList;

            this.scheduleWeek.AppointmentEditorClosed += Schedule_AppointmentEditorClosed;
            this.scheduleMonth.AppointmentEditorClosed += Schedule_AppointmentEditorClosed;

            this.scheduleMonth.AppointmentCollectionChanged += Schedule_AppointmentCollectionChanged;
            this.scheduleWeek.AppointmentCollectionChanged += Schedule_AppointmentCollectionChanged;
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
                meeting.UId = UId;
                MeetingList.Add(meeting);
                _awsMeeting.AddMeeting(meeting);
                e.Handled = true;
            }
        }
        private void Schedule_AppointmentCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var appointment = e.NewItems as ScheduleAppointment;
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
                meeting.UId = UId;
                MeetingList.Add(meeting);
                _awsMeeting.AddMeeting(meeting);

            }
            else
            {
                MessageBox.Show("olmuyor");
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
