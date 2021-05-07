using Business.concrete;
using Enitities.Concrete;
using MaterialDesignThemes.Wpf;
using Syncfusion.UI.Xaml.Schedule;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : UserControl
    {
        public ObservableCollection<Todo> toDoList { get; set; }
        public ScheduleManager scheduleManager;
        public HomePage()
        {
            InitializeComponent();
            toDoList = new ObservableCollection<Todo>();
            TodoItems.ItemsSource = toDoList;
            scheduleManager = new ScheduleManager();
            schedule.ItemsSource = scheduleManager.Events;

            this.schedule.AppointmentCollectionChanged += Schedule_AppointmentCollectionChanged;

            this.schedule.AppointmentEditorClosed += Schedule_AppointmentEditorClosed;
            

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

        private void Schedule_AppointmentCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            MessageBox.Show("Değişiklik yapıldı: ");            
        }

        private void ToDoOnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            Debug.WriteLine($"SAMPLE 1: Closing dialog with parameter: {eventArgs.Parameter ?? string.Empty}");

            //you can cancel the dialog close:
            //eventArgs.Cancel();

            if (!Equals(eventArgs.Parameter, true))
                return;

            if (!string.IsNullOrWhiteSpace(TodoTextBox.Text))
                toDoList.Add(new Todo() { toDo=TodoTextBox.Text,IsChecked=false });

            TodoTextBox.Text = "";
        }

        private void ToDoChecked(object sender, RoutedEventArgs e)
        {
            var selected = TodoItemsList.SelectedItem;
            Todo delete = selected as Todo;
            toDoList.Remove(delete);
            MessageBox.Show("silindi");
        }

    }
}
