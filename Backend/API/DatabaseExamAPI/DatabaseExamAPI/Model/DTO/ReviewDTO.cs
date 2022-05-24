namespace DatabaseExamAPI.Model.DTO
{
    public class ReviewDTO
    {
        public string MovieId { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Desc { get; set; }
        public int Rating { get; set; }
    
        public ReviewDTO(string movieId, string userId, string username, string desc, int rating)
        {
            this.MovieId = movieId;
            this.UserId = userId;
            this.Username = username;
            this.Desc = desc;
            this.Rating = rating;

        }

    }

}
