using DatabaseExamAPI.DB.MongoDB;
using DatabaseExamAPI.Model;
using MongoDB.Bson;

namespace DatabaseExamAPI.Facades
{
    public class ReviewFacade
    {
        private ILogger<ReviewFacade> _logger;
        private ReviewMapper _reviewMapper;

        public ReviewFacade(ILoggerFactory lf)
        {
            _logger = lf.CreateLogger<ReviewFacade>();
            _reviewMapper = new ReviewMapper(lf);
        }

     
        public List<ReviewModel> GetReviewsByUserId(string userId)
        {
            return _reviewMapper.GetReviewsByUserId(userId);
        }

        public List<ReviewModel> GetReviewsByMovieId(string movieId)
        {
            return _reviewMapper.GetReviewsByMovieId(movieId);
        }

        public double GetAvgRatingByMovieId(string movieId)
        {
            return _reviewMapper.GetAvgRatingByMovieId(movieId);
        }

        public IEnumerable<BsonDocument> GetTopReviews(string movieId)
        {
            IEnumerable<BsonDocument> topReviews = _reviewMapper.GetTopReviews(movieId);
            return topReviews;
        }

        public void AddReview(string movieId,string userId, string username, string desc, int rating)
        {
            _reviewMapper.AddReview(movieId, userId, username, desc, rating);
        }

    }
}
