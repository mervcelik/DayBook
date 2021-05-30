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
    public class AwsMeeting
    {

        public Result AddMeeting(Meeting meeting)
        {
            AwsConnection awsConnection = new AwsConnection();
            awsConnection.sqlConnection.Open();
            if (awsConnection.sqlConnection.State != ConnectionState.Closed)
            {
                MySqlCommand komut = new MySqlCommand();
                string ekleme = "INSERT INTO daybook.Meeting(UId,from,to,eventName,notes,location,isAllDay,startTimeZone,endTimeZone) VALUES('" + meeting.UId + "','" + meeting.From + "','" + meeting.To + "','" + meeting.EventName + "','" + meeting.Notes + "','" + meeting.Location + "','" + meeting.IsAllDay + "','" + meeting.StartTimeZone + "','" + meeting.EndTimeZone + "',)";

                komut.Connection = awsConnection.sqlConnection;
                komut.CommandText = ekleme;
                komut.ExecuteNonQuery();
                return new SuccessResult(Message.succces);

            }
            awsConnection.sqlConnection.Close();
            return new ErrorResult(Message.Error);
        }

        public DataResult<ObservableCollection<Meeting>> GetMeeting(int UId)
        {

            AwsConnection awsConnection = new AwsConnection();
            awsConnection.sqlConnection.Open();
            if (awsConnection.sqlConnection.State != ConnectionState.Closed)
            {
                MySqlDataAdapter baglayici = new MySqlDataAdapter();
                MySqlCommand komut = new MySqlCommand();
                string sqlsorgusu = "SELECT * FROM daybook.Meeting Where UId='" + UId + "'";
                DataTable tablo = new DataTable();

                komut.CommandText = sqlsorgusu;
                komut.Connection = awsConnection.sqlConnection;
                baglayici.SelectCommand = komut;
                baglayici.Fill(tablo);

                ObservableCollection<Meeting> collection = new ObservableCollection<Meeting>();


                foreach (DataRow dataRow in tablo.Rows)
                {
                    Meeting meeting = new Meeting();
                    meeting.Id = (int)dataRow["Id"];
                    meeting.UId = (int)dataRow["UId"];
                    meeting.From = (DateTime)dataRow["from"];
                    meeting.To = (DateTime)dataRow["to"];
                    meeting.EventName = (string)dataRow["eventName"];
                    meeting.Notes = (string)dataRow["notes"];
                    meeting.Location = (string)dataRow["location"];
                    meeting.IsAllDay = (bool)dataRow["isAllDay"];
                    meeting.StartTimeZone = (string)dataRow["startTimeZone"];
                    meeting.EndTimeZone = (string)dataRow["endTimeZone"];

                    collection.Add(meeting);
                }
                return new SuccessDataResult<ObservableCollection<Meeting>>(collection, Message.succces);
            }
            awsConnection.sqlConnection.Close();
            return new ErrorDataResult<ObservableCollection<Meeting>>(Message.Error);
        }
    }
}
