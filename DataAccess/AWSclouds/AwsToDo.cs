using Enitities.Concrete;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccess.AWSclouds
{
    public class AwsToDo
    {
        //Ekleme,Güncelleme(sadece İschecked ),Getirme,Silme

        public void AddToDo(Todo toDo)
        {
            AwsConnection awsConnection = new AwsConnection();
            awsConnection.sqlConnection.Open();
            if (awsConnection.sqlConnection.State != ConnectionState.Closed)
            {
                MySqlCommand komut = new MySqlCommand();
                string ekleme = "INSERT INTO daybook.ToDo(UId,IsChecked,Todo) VALUES('" + toDo.UId + "','" + toDo.IsChecked + "','" + toDo.toDo + "')";

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

        public void UpdateIsChecked(Todo toDo)
        {
            AwsConnection awsConnection = new AwsConnection();
            awsConnection.sqlConnection.Open();
            if (awsConnection.sqlConnection.State != ConnectionState.Closed)
            {
                MySqlCommand komut = new MySqlCommand();
                string güncelle = "UPDATE daybook.ToDo SET IsChecked='" + toDo.IsChecked + "' WHERE Todo='" + toDo.toDo + "'";

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

        public void DeleteToDo(Todo toDo)
        {
            AwsConnection awsConnection = new AwsConnection();
            awsConnection.sqlConnection.Open();
            if (awsConnection.sqlConnection.State != ConnectionState.Closed)
            {
                MySqlCommand komut = new MySqlCommand();
                string sil = "DELETE FROM daybook.ToDo WHERE Todo='" + toDo.toDo + "'";

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

        public void GetToDo(int UId)
        {

            AwsConnection awsConnection = new AwsConnection();
            awsConnection.sqlConnection.Open();
            if (awsConnection.sqlConnection.State != ConnectionState.Closed)
            {
                MySqlDataAdapter baglayici = new MySqlDataAdapter();
                MySqlCommand komut = new MySqlCommand();
                string sqlsorgusu = "SELECT * FROM daybook.ToDo Where UId='" + UId + "'";
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
    }
}
