using System;
using System.Collections.Generic;
using System.IO;

namespace Database
{
    class Database
    {
        public static void Main()
        {
            string strPath = @"C:\Users\benoh\source\repos\Database\Database\LiteConfig.json";
            FileInfo connectionJson = new FileInfo(strPath);
            LiteConnection naw = new LiteConnection(connectionJson);

            GenQuery<Person> qb;
            Dictionary<int, string> dict;
            string strInput = "";

            while (strInput != "end")
            {
                int iCounter = 0;

                using (naw.BuiltConnection)
                {
                    naw.BuiltConnection.Open();
                    qb = new GenQuery<Person>(naw.BuiltConnection);
                    dict = qb.SelectAll();
                }

                Console.WriteLine("Current Data\n---------------------------------");

                foreach (var item in dict)
                {
                    Console.WriteLine(dict[iCounter]);
                    iCounter++;
                }

                Console.WriteLine("\n\nWhat would you like to do?\n1.) Add a new row\n2.) Read a specified row\n3.) Update the data in a row\n4.) Delete a row\n\nType \"end\" to end this session");
                strInput = Console.ReadLine();

                switch (strInput)
                {
                    case "1":
                        Console.WriteLine("Please enter a First Name: ");
                        string strFirstName = Console.ReadLine();
                        Console.WriteLine("Please enter a Last Name: ");
                        string strLastName = Console.ReadLine();
                        Console.WriteLine("Please enter a  Job: ");
                        string strJob = Console.ReadLine();

                        Person newFella = new Person(strFirstName, strLastName, strJob);

                        using (naw.BuiltConnection)
                        {
                            naw.BuiltConnection.Open();
                            Console.WriteLine(qb.AddRow(newFella) + " row(s) added.");
                        }

                        break;
                    case "2":
                        Console.WriteLine("Which row would you like to read?");
                        int iRow = Convert.ToInt32(Console.ReadLine());

                        using (naw.BuiltConnection)
                        {
                            naw.BuiltConnection.Open();
                            Console.WriteLine(qb.ReadRow(iRow) + "\n\nPress any key to continue...");
                        }

                        Console.ReadLine();

                        break;
                    case "3":
                        Console.WriteLine("What is the ID of the row you'd like to update? ");
                        int iToUpdate = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("What is the name of the column you'd like to update? ");
                        string strColumn = Console.ReadLine();
                        Console.WriteLine("What is the new value you would like to put into the cell? ");
                        string strNewValue = Console.ReadLine();

                        using (naw.BuiltConnection)
                        {
                            naw.BuiltConnection.Open();
                            Console.WriteLine(qb.UpdateRow(iToUpdate, strColumn, strNewValue) + " row(s) updated.");
                        }

                        break;
                    case "4":
                        Console.WriteLine("What is the ID of the row you wish to delete? ");
                        int iToDelete = Convert.ToInt32(Console.ReadLine());

                        using (naw.BuiltConnection)
                        {
                            naw.BuiltConnection.Open();
                            Console.WriteLine(qb.DeleteRow(iToDelete) + " row(s) deleted.");
                        }

                        break;
                    default:
                        break;
                }
            }
        }
    }
}
