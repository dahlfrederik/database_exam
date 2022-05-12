namespace DatabaseExamAPI.Model
{
    public class Movie
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Tagline { get; set; }
        public long Released { get; set; }

        public Movie(int? id, string? title, string? tagline, int? released)
        {
            Id = id ?? -1;
            Title = title ?? "";
            Tagline = tagline ?? "";
            Released = released ?? -1;
        }

        public Movie(object? id, object? title, object? tagline, object? released)
        {
            Id = (long) (id ?? -1);
            Title = (string) (title ?? "");
            Tagline = (string) (tagline ?? "");
            Released = (long) (released ?? -1);
        }
    }
}
