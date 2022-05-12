namespace DatabaseExamAPI.Model
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Movie(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
