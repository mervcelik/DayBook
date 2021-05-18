using Core.Results;
using Enitities.Concrete;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccess.AWSclouds
{
    public class AwsUser
    {
        //Parola yenileme ekle
        //Dataresult kullanımı


        public void AddUser(User user)
        {
            AwsConnection awsConnection = new AwsConnection();
            awsConnection.sqlConnection.Open();
            if (awsConnection.sqlConnection.State != ConnectionState.Closed)
            {
                MySqlCommand komut = new MySqlCommand();
                string ekleme = "INSERT INTO daybook.User(Name,Lastname,Email,Password) VALUES('" + user.Name + "','" + user.LastName + "','" + user.Email + "','" + user.Password + "')";

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
        public Result GetUser(User user)
        {
            AwsConnection awsConnection = new AwsConnection();
            awsConnection.sqlConnection.Open();
            if (awsConnection.sqlConnection.State != ConnectionState.Closed)
            {
                MySqlDataReader baglayici;
                MySqlCommand komut = new MySqlCommand();
                string sqlsorgusu = "SELECT * FROM daybook.User Where Email='" + user.Email + "' AND Password='" + user.Password + "'";
                komut.CommandText = sqlsorgusu;
                komut.Connection = awsConnection.sqlConnection;
                baglayici = komut.ExecuteReader();
                if (baglayici.Read())
                {
                    awsConnection.sqlConnection.Close();
                    return new SuccessResult("giriş yapıldı");
                }
                else
                {
                    awsConnection.sqlConnection.Close();
                    return new ErrorResult("Email veya password hatalı...");
                }
            }
            else
            {
                awsConnection.sqlConnection.Close();
                return new ErrorResult("bağlantı basarısızzzzzz");
            }
        }
        public DataResult<int> GetUserId(User user)
        {
            AwsConnection awsConnection = new AwsConnection();
            awsConnection.sqlConnection.Open();
            if (awsConnection.sqlConnection.State != ConnectionState.Closed)
            {
                MySqlDataAdapter baglayici = new MySqlDataAdapter();
                MySqlCommand komut = new MySqlCommand();
                string sqlsorgusu = "SELECT Id FROM daybook.User Where Email='" + user.Email + "' AND Password='" + user.Password + "'";
                DataTable tablo = new DataTable();

                komut.CommandText = sqlsorgusu;
                komut.Connection = awsConnection.sqlConnection;
                baglayici.SelectCommand = komut;
                baglayici.Fill(tablo);

                int userId = -1;

                foreach (DataRow dataRow in tablo.Rows)
                {
                    foreach (var item in dataRow.ItemArray)
                    {
                        userId = (int)item;
                        return new SuccessDataResult<int>(userId);
                    }
                } 
            }
            awsConnection.sqlConnection.Close();
            return new ErrorDataResult<int>(-1,"Başarısız!");



        }
    }
}
