namespace DatabaseExamAPI.Model
{
    public class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Born { get; set; }

        public Person(long id, string name, string born)
        {
            Id = id;
            Name = name;
            Born = born;
        }
    }
}
