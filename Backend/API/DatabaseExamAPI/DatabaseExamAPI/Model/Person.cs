namespace DatabaseExamAPI.Model
{
    public class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Born { get; set; }
        public List<Movie>? ActedIn { get; set; }

        public Person(long? id, string? name, int? born)
        {
            // ?? returns what is to the right of it IF the value of the variable to the left is null.
            // so if parameter "id" is null, "Id" gets set to -1
            Id = id ?? -1;
            Name = name ?? "";
            Born = born ?? -1;
        }

        //generic constructor
        public Person(object? id, object? name, object? born)
        {
            Id = (long)(id ?? -1);
            Name = (string)(name ?? "");
            Born =(long)(born ?? -1);
        }
    }
}
