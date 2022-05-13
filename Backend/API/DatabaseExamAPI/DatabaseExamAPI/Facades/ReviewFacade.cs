using DatabaseExamAPI.DB.MongoDB;
using DatabaseExamAPI.Model;

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

        public ReviewModel GetReview(string userId)
        {
            return _reviewMapper.GetReview(userId);
        }
        
        public void AddReview(string userId, string username, string desc, int rating)
        {
            _reviewMapper.AddReview(userId, username, desc, rating);
        }

    }
}
