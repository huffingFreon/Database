using Newtonsoft.Json;
using System;
using System.Data.SqlClient;
using System.IO;

namespace Database
{
    public class Connection
    {
       public SqlConnectionStringBuilder BuiltConnection { get; set; }

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

           BuiltConnection = JsonConvert.DeserializeObject<SqlConnectionStringBuilder>(strJSON);
       }
    }
}