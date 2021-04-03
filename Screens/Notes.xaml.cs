using Enitities.Concrete;
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
    /// Interaction logic for Notes.xaml
    /// </summary>
    public partial class Notes : UserControl
    {
        public ObservableCollection<Note> Note { get; set; }
        public Notes()
        {
            InitializeComponent();
            Note = new ObservableCollection<Note>();
            NotesBox.ItemsSource = Note;
            clickNote.MouseUp += new MouseButtonEventHandler(visibilty);
        }

        private void SaveNotes(object sender,EventArgs eventArgs)
        {
            if (!string.IsNullOrWhiteSpace(SaveHeader.Text) && !string.IsNullOrWhiteSpace(SaveNote.Text))
                Note.Add(new Note { Header = SaveHeader.Text, Notes = SaveNote.Text });
            else if (string.IsNullOrWhiteSpace(SaveHeader.Text)){
                MessageBox.Show("Başlık boş bırakıldı");
            }
            else if (string.IsNullOrWhiteSpace(SaveNote.Text))
            {
                MessageBox.Show("Not  boş bırakıldı");
            }
            SaveHeader.Text = "";
            SaveNote.Text = "";
        }

        private void visibilty(object sender, EventArgs eventArgs)
        {
            clickNote.Visibility = Visibility.Collapsed;
            SaveBox.Visibility = Visibility.Visible;
        }
    }
}
