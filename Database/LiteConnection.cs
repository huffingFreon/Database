using Newtonsoft.Json;
using System.Data.SQLite;
using System.IO;

namespace Database
{
    public class SQLiteConnection
    {
       public SQLiteConnectionStringBuilder BuiltConnectionString { get; set; }
       public SQLiteConnection BuiltConnection { get; set; }

       public SQLiteConnection(FileInfo file) 
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

           BuiltConnectionString = JsonConvert.DeserializeObject<SQLiteConnectionStringBuilder>(strJSON);
            BuiltConnection = new System.Data.SQLite.SQLiteConnection(BuiltConnectionString.ToString());
       }
    }
}