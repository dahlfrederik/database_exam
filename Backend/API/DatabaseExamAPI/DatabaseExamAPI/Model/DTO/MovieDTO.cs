namespace DatabaseExamAPI.Model.DTO
{
    public class MovieDTO
    {
        public string Title { get; set; }
        public string Tagline { get; set; }
        public int Released { get; set; }

        public MovieDTO(string title, string tagline, int released)
        {
            Title = title;
            Tagline = tagline;
            Released = released;
        }

        public MovieDTO()
        {
        }
    }
}
