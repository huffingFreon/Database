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
            string strPath = @"C:\Users\higginsba\source\repos\Database\Database\config.json";
            FileInfo connectionJson = new FileInfo(strPath);
            Connection naw = new Connection(connectionJson);

            Console.WriteLine(naw.BuiltConnection.Password);
        }
    }
}
