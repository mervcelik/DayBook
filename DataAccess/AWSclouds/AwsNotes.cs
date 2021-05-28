using Core.Messages;
using Core.Results;
using Enitities.Concrete;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

namespace DataAccess.AWSclouds
{
    public class AwsNotes
    {
        public Result AddNotes(Note note)
        {
            AwsConnection awsConnection = new AwsConnection();
            awsConnection.sqlConnection.Open();
            if (awsConnection.sqlConnection.State != ConnectionState.Closed)
            {
                MySqlCommand komut = new MySqlCommand();
                string ekleme = "INSERT INTO daybook.Notes(UId,Header,Notes) VALUES('" + note.UId + "','" + note.Header + "','" + note.Notes + "')";

                komut.Connection = awsConnection.sqlConnection;
                komut.CommandText = ekleme;
                komut.ExecuteNonQuery();
                return new SuccessResult(Message.succces);

            }
            awsConnection.sqlConnection.Close();
            return new ErrorResult(Message.Error);
        }

        public DataResult<ObservableCollection<Note>> GetNotes(int UId)
        {
            AwsConnection awsConnection = new AwsConnection();
            awsConnection.sqlConnection.Open();
            if (awsConnection.sqlConnection.State != ConnectionState.Closed)
            {
                MySqlDataAdapter baglayici = new MySqlDataAdapter();
                MySqlCommand komut = new MySqlCommand();
                string sqlsorgusu = "SELECT * FROM daybook.Notes Where UId='" + UId + "'";
                DataTable tablo = new DataTable();

                komut.CommandText = sqlsorgusu;
                komut.Connection = awsConnection.sqlConnection;
                baglayici.SelectCommand = komut;
                baglayici.Fill(tablo);

                ObservableCollection<Note> collection = new ObservableCollection<Note>();

                foreach (DataRow dataRow in tablo.Rows)
                {
                    Note note = new Note();
                    note.Id = (int)dataRow["Id"];
                    note.UId = (int)dataRow["UId"];
                    note.Header =(string)dataRow["Header"];
                    note.Notes = (string)dataRow["Notes"];
                    collection.Add(note);
                }
                return new SuccessDataResult<ObservableCollection<Note>>(collection, Message.succces);
            }
            awsConnection.sqlConnection.Close();
            return new ErrorDataResult<ObservableCollection<Note>>(Message.Error);
        }

        public Result UpdateNotes(Note note)
        {
            AwsConnection awsConnection = new AwsConnection();
            awsConnection.sqlConnection.Open();
            if (awsConnection.sqlConnection.State != ConnectionState.Closed)
            {
                MySqlCommand komut = new MySqlCommand();
                string güncelle = "UPDATE daybook.Notes SET Header='" + note.Header + "',Notes='" + note.Notes + "' WHERE Id='" + note.Id + "'";

                komut.Connection = awsConnection.sqlConnection; 
                komut.CommandText = güncelle;
                komut.ExecuteNonQuery();
                return new SuccessResult(Message.succces);
            }
            awsConnection.sqlConnection.Close();
            return new ErrorResult(Message.Error);
        }

        public Result DeleteNotes(Note note)
        {
            AwsConnection awsConnection = new AwsConnection();
            awsConnection.sqlConnection.Open();
            if (awsConnection.sqlConnection.State != ConnectionState.Closed)
            {
                MySqlCommand komut = new MySqlCommand();
                string sil = "DELETE FROM daybook.Notes WHERE Id='" + note.Id + "'";

                komut.Connection = awsConnection.sqlConnection;
                komut.CommandText = sil;
                komut.ExecuteNonQuery();
                return new SuccessResult(Message.succces);
            }
            awsConnection.sqlConnection.Close();
            return new ErrorResult(Message.Error);
        }
    }
}
