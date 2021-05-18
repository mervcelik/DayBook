using Enitities.Concrete;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccess.AWSclouds
{
    public class AwsNotes
    {
        public void AddNotes(Note note)
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
                Console.WriteLine("başarılı");

            }
            else
            {
                Console.WriteLine("basarısızzzzzz");
            }
            awsConnection.sqlConnection.Close();
        }

        public void GetNotes(int UId)
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

            }
            else
            {
                Console.WriteLine("basarısızzzzzz");
            }
            awsConnection.sqlConnection.Close();
        }

        public void UpdateNotes(Note note)
        {
            AwsConnection awsConnection = new AwsConnection();
            awsConnection.sqlConnection.Open();
            if (awsConnection.sqlConnection.State != ConnectionState.Closed)
            {
                MySqlCommand komut = new MySqlCommand();
                string güncelle = "UPDATE daybook.Notes SET Header='" + note.Header + "',Notes='" + note.Notes + "' WHERE UId='" + note.UId + "'";

                komut.Connection = awsConnection.sqlConnection;
                komut.CommandText = güncelle;
                komut.ExecuteNonQuery();
                Console.WriteLine("basarılı");
            }
            else
            {
                Console.WriteLine("basarısızzzzzz");
            }
            awsConnection.sqlConnection.Close();
        }

        public void DeleteNotes(Note note)
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
                Console.WriteLine("basarılı");
            }
            else
            {
                Console.WriteLine("basarısızzzzzz");
            }

        }
    }
}
