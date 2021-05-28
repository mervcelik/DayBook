using Core.FileHelper;
using DataAccess.AWSclouds;
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
        public int selectedId ;
        public AwsNotes awsNotes;
        public int _UId;
        public Notes()
        {
            InitializeComponent();
            awsNotes = new AwsNotes();
            _UId = FileManager.Read();
            Note = awsNotes.GetNotes(_UId).Data;
            NotesBox.ItemsSource = Note;
            clickNote.MouseUp += new MouseButtonEventHandler(visibilty);
        }

        private void SaveNotes(object sender,EventArgs eventArgs)
        {
            if (!string.IsNullOrWhiteSpace(SaveHeader.Text) && !string.IsNullOrWhiteSpace(SaveNote.Text)) {
                Note note = new Note { UId = _UId, Header = SaveHeader.Text, Notes = SaveNote.Text };
                Note.Add(note);
                awsNotes.AddNotes(note);
            }
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
        private void UpdateNotes(object sender, EventArgs eventArgs)
        {
            if (!string.IsNullOrWhiteSpace(SaveHeader.Text) && !string.IsNullOrWhiteSpace(SaveNote.Text))
            {
                Note note = new Note { Id= Note[selectedId].Id, UId =_UId,Header = SaveHeader.Text, Notes = SaveNote.Text };
                Note[selectedId] = note;
                awsNotes.UpdateNotes(note);
            }
            else if (string.IsNullOrWhiteSpace(SaveHeader.Text))
            {
                MessageBox.Show("Başlık boş bırakıldı");
            }
            else if (string.IsNullOrWhiteSpace(SaveNote.Text))
            {
                MessageBox.Show("Not  boş bırakıldı");
            }
            SaveHeader.Text = "";
            SaveNote.Text = "";
            updatebutton.Visibility = Visibility.Hidden;
            savebutton.Visibility = Visibility.Visible;
            btnDelete.Visibility = Visibility.Hidden;
        }
        
        private void visibilty(object sender, EventArgs eventArgs)
        {
            clickNote.Visibility = Visibility.Collapsed;
            SaveBox.Visibility = Visibility.Visible;
        }

        private void ShowNotes(object sender, EventArgs eventArgs)
        {
            Note notes = new Note {};
            selectedId = NotesBox.SelectedIndex;
            if (selectedId > -1)
            {
                notes = Note[selectedId];
                MessageBoxResult result = MessageBox.Show(notes.Notes + "\n Notu düzenlemek istermisiniz?", notes.Header, MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        clickNote.Visibility = Visibility.Collapsed;
                        SaveBox.Visibility = Visibility.Visible;
                        savebutton.Visibility = Visibility.Hidden;
                        btnDelete.Visibility = Visibility.Visible;
                        updatebutton.Visibility = Visibility.Visible;
                        SaveHeader.Text = notes.Header;
                        SaveNote.Text = notes.Notes;
                        break;
                    case MessageBoxResult.No:

                        break;
                }
            }
            
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Note notes = new Note();
            if (selectedId > -1)
            {
                notes = Note[selectedId];
                MessageBoxResult result = MessageBox.Show(notes.Notes + "\n Notu silmek ister misiniz ?", notes.Header, MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Note.Remove(notes);
                        awsNotes.DeleteNotes(notes);
                        SaveHeader.Text = "";
                        SaveNote.Text = "";
                        btnDelete.Visibility = Visibility.Hidden;
                        updatebutton.Visibility = Visibility.Hidden;
                        savebutton.Visibility = Visibility.Visible;
                        break;
                    case MessageBoxResult.No:

                        break;
                }

            }
        }
    }
}
