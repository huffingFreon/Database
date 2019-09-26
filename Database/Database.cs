using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Database
{
    class Database
    {
        public static void Main()
        {
            string strPath = @"C:\Users\benoh\source\repos\Database\Database\LiteConfig.json";
            FileInfo connectionJson = new FileInfo(strPath);
            SQLiteConnection naw = new SQLiteConnection(connectionJson);

            Console.WriteLine(naw.BuiltConnection.State);
        }
    }
}
