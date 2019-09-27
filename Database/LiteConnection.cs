using Newtonsoft.Json;
using Microsoft.Data.Sqlite;
using System.IO;

namespace Database
{
    public class LiteConnection
    {
       public SqliteConnectionStringBuilder BuiltConnectionString { get; set; }
       public SqliteConnection BuiltConnection { get; set; }

       public LiteConnection(FileInfo file) 
       {
           string strJSON = "";

           using(StreamReader sr = file.OpenText())
           {
               var s = "";
                while((s = sr.ReadLine()) != null)
                {
                    strJSON += s;
                }
           }

           BuiltConnectionString = JsonConvert.DeserializeObject<SqliteConnectionStringBuilder>(strJSON);
           BuiltConnection = new SqliteConnection(BuiltConnectionString.ToString());
       }
    }
}