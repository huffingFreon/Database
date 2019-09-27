using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Text;

namespace Database
{
    class QueryBuilder
    {
        private SqliteConnection QueryConnection; 
        private SqliteCommand command;

        public QueryBuilder(SqliteConnection lite)
        {
            QueryConnection = lite;
            command = new SqliteCommand();
            command.Connection = QueryConnection;
        }

        public string ReadRow(int rowID)
        {
            string strRow = "";

            command.CommandText = $"select * from Person where Person_ID = {rowID}";

            SqliteDataReader dataReader = command.ExecuteReader();
            dataReader.Read();

            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                strRow += dataReader.GetString(i) + "\t";
            }

            return strRow;
        }

        public int DeleteRow(int rowID)
        {
            int iDeleted;

            command.CommandText = $"delete from Person where Person_ID = {rowID}";

            iDeleted = command.ExecuteNonQuery();

            return iDeleted;
        }

        public int AddRow(string firstName, string lastName, string job)
        {
            int iAdded;

            command.CommandText = $"insert into Person (First_Name, Last_Name, Job) values ('{firstName}', '{lastName}', '{job}')";

            iAdded = command.ExecuteNonQuery();

            return iAdded;
        }

        public int UpdateRow(int rowID, string valueName, string value)
        {
            int iUpdated;

            command.CommandText = $"update Person set {valueName} = '{value}' where Person_ID = {rowID}";

            iUpdated = command.ExecuteNonQuery();

            return iUpdated;
        }

        public Dictionary<int, string> SelectAll()
        {
            Dictionary<int, string> allData = new Dictionary<int, string>();
            command.CommandText = "select * from Person";
            SqliteDataReader dataReader = command.ExecuteReader();
            int iInside = 0;
            int iOutside = 0;
            string strRow = "";
            
            while(dataReader.GetString(iInside) != null)
            {
                dataReader.Read();

                strRow += "\n";

                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    strRow += dataReader.GetString(i) + "\t";
                }

                allData.Add(iOutside, strRow);

                iOutside++;
            }

            return allData;
        }
    }
}
