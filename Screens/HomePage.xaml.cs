using Business.concrete;
using Core.FileHelper;
using Core.Results;
using DataAccess.AWSclouds;
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
        public int UId;
        public AwsToDo _awsTodo;
        public HomePage()
        {
            InitializeComponent();
            _awsTodo = new AwsToDo();
            UId = FileManager.Read();

            toDoList = new ObservableCollection<Todo>();
            toDoList = _awsTodo.GetToDo(UId).Data;
 
            TodoItemsList.ItemsSource = toDoList;
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

            Todo todo = new Todo() { toDo = TodoTextBox.Text, IsChecked = false };
            todo.UId = UId;
            if (!string.IsNullOrWhiteSpace(TodoTextBox.Text))
                toDoList.Add(todo);
            
            _awsTodo.AddToDo(todo);
            TodoTextBox.Text = "";
        }

        private void ToDoChecked(object sender, EventArgs e)
        {
            var selectedId = TodoItemsList.SelectedIndex;
            Todo todo = new Todo();
            if (selectedId > -1)
            {
                todo = toDoList[selectedId];
                MessageBoxResult result = MessageBox.Show(todo.toDo + " tamalandı mı?", "Yapılacaklar", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        toDoList.Remove(todo);
                        _awsTodo.DeleteToDo(todo);
                        break;
                    case MessageBoxResult.No:
                        MessageBox.Show("Başarabileceğine inanıyorum");
                        break;
                }
            }

        }

    }
}
