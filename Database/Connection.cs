using Newtonsoft.Json;
using System.Data.SqlClient;
using System.IO;

namespace Database
{
    public class Connection
    {
       public SqlConnectionStringBuilder BuiltConnectionString { get; set; }
       public SqlConnection BuiltConnection { get; set; }

       public Connection(FileInfo file) 
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

           BuiltConnectionString = JsonConvert.DeserializeObject<SqlConnectionStringBuilder>(strJSON);
           BuiltConnection = new SqlConnection(BuiltConnectionString.ToString());
       }
    }
}