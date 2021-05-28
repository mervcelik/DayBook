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
    public class AwsToDo
    {
        //Ekleme,Güncelleme(sadece İschecked ),Getirme,Silme

        public Result AddToDo(Todo toDo)
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
                return new SuccessResult(Message.succces);

            }
            awsConnection.sqlConnection.Close();
            return new ErrorResult(Message.Error);
        }

        public Result UpdateIsChecked(Todo toDo)
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
                return new SuccessResult(Message.succces);
            }

            awsConnection.sqlConnection.Close();
            return new ErrorResult(Message.Error);
        }

        public Result DeleteToDo(Todo toDo)
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
                return new SuccessResult(Message.succces);
            }
            return new ErrorResult(Message.Error);
        }

        public DataResult<ObservableCollection<Todo>> GetToDo(int UId)
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

                ObservableCollection<Todo> collection = new ObservableCollection<Todo>();

                
                foreach (DataRow dataRow in tablo.Rows)
                {
                    Todo toDo = new Todo();
                    toDo.Id = (int)dataRow["Id"];
                    toDo.UId = (int)dataRow["UId"];
                    toDo.IsChecked = (bool)dataRow["IsChecked"];
                    toDo.toDo = (string)dataRow["Todo"];
                    collection.Add(toDo);
                }
                return new SuccessDataResult<ObservableCollection<Todo>>(collection,Message.succces);
            }
            awsConnection.sqlConnection.Close();
            return new ErrorDataResult<ObservableCollection<Todo>>(Message.Error);
        }
    }
}
