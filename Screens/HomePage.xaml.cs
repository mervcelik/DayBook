using Enitities.Concrete;
using MaterialDesignThemes.Wpf;
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
        public HomePage()
        {
            InitializeComponent();
            toDoList = new ObservableCollection<Todo>();
            TodoItems.ItemsSource = toDoList;
            
                    
        }

        private void Sample1_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
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


    }
}
