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
            LiteConnection naw = new LiteConnection(connectionJson);
            QueryBuilder qb = new QueryBuilder(naw.BuiltConnection);

            naw.BuiltConnection.Open();

            Dictionary<int, string> dict = qb.SelectAll();
            int iCounter = 0;

            foreach (var item in dict)
            {
                Console.WriteLine(dict[iCounter]);
                iCounter++;
            }
        }
    }
}
