using System.Collections.Generic;
using Microsoft.Data.Sqlite;

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
            int iRows;
            string strColumnNames = "";

            command.CommandText = "select count(*) from Person";
            SqliteDataReader dataReader; 

            using (dataReader = command.ExecuteReader())
            {
                dataReader.Read();
                iRows = dataReader.GetInt32(0);
            }

            command.CommandText = "select * from Person";
            dataReader = command.ExecuteReader();

            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                strColumnNames += dataReader.GetName(i) + "\t";
            }
            allData.Add(0, strColumnNames);

            for (int i = 0; i < iRows; i++)
            {
                dataReader.Read();
                string rowData = "";

                for (int j = 0; j < dataReader.FieldCount; j++)
                {
                    rowData += dataReader.GetString(j) + "\t\t";
                }

                allData.Add((i + 1), rowData);
            }

            return allData;
        }
    }
}
