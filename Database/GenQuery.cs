using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Reflection;

namespace Database
{
    class GenQuery<T> where T : new()
    {
        public SqliteConnection Connection { get; set; }

        public SqliteCommand Command { get; set; }

        public string TableName { get; set; }

        public List<string> TableColumns { get; set; }

        public GenQuery(SqliteConnection lite)
        {
            Connection = lite;
            Command = new SqliteCommand();
            Command.Connection = Connection;
            TableName = typeof(T).Name;
            TableColumns = new List<string>();

            PropertyInfo[] propertyInfos = typeof(T).GetProperties();

            for (int i = 0; i < propertyInfos.Length; i++)
            {
                TableColumns.Add(propertyInfos[i].Name);
            }
        }

        public string ReadRow(int iRowID)
        {
            string strRow = "";

            Command.CommandText = $"select * from {TableName} where {TableColumns[0]} = {iRowID}";

            SqliteDataReader dataReader = Command.ExecuteReader();
            dataReader.Read();

            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                strRow += dataReader.GetString(i) + "\t";
            }

            return strRow;
        }

        public int DeleteRow(int iRowID)
        {
            int iDeleted = 0;

            Command.CommandText = $"delete from {TableName} where {TableColumns[0]} = {iRowID}";

            iDeleted = Command.ExecuteNonQuery();

            return iDeleted;
        }

        public int AddRow(T gen)
        {
            int iRowsAdded;

            List<string> values = new List<string>();
            
            for (int i = 1; i < TableColumns.Count; i++)
            {
                values.Add(typeof(T).GetProperty(TableColumns[i]).GetValue(gen).ToString());
            }

            Command.CommandText = $"insert into {TableName} (";

            for (int i = 1; i < TableColumns.Count; i++)
            {
                if (i < (TableColumns.Count - 1))
                {
                    Command.CommandText += $"{TableColumns[i]}, "; 
                }
                else
                {
                    Command.CommandText += $"{TableColumns[i]}) values ("; 
                }
            }

            for (int i = 0; i < values.Count; i++)
            {
                if (i < (values.Count - 1))
                {
                    Command.CommandText += $"'{values[i]}', ";
                }
                else
                {
                    Command.CommandText += $"'{values[i]}')";
                }
            }

            iRowsAdded = Command.ExecuteNonQuery();

            return iRowsAdded;
        }

        public int UpdateRow(int rowID, string valueName, string value)
        {
            int iUpdated;

            Command.CommandText = $"update {TableName} set {valueName} = '{value}' where {TableColumns[0]} = {rowID}";

            iUpdated = Command.ExecuteNonQuery();

            return iUpdated;
        }

        public Dictionary<int, string> SelectAll()
        {
            Dictionary<int, string> allData = new Dictionary<int, string>();
            int iRows;
            string strColumnNames = "";

            Command.CommandText = $"select count(*) from {TableName}";
            SqliteDataReader dataReader; 

            using (dataReader = Command.ExecuteReader())
            {
                dataReader.Read();
                iRows = dataReader.GetInt32(0);
            }

            Command.CommandText = $"select * from {TableName}";
            dataReader = Command.ExecuteReader();

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
