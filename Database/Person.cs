namespace Database
{
    class Person
    {
        public int ID { get; set; }

        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        public string Job { get; set; }

        public Person()
        {
            ID = 0;
            First_Name = null;
            Last_Name = null;
            Job = null;
        }

        public Person(string firstName, string lastName, string job)
        {
            First_Name = firstName;
            Last_Name = lastName;
            Job = job;
        }
    }
}
