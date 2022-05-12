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

        /*
        public async Task<ReviewModel> PostReview()
        {
            return await _reviewMapper.AddReview();
        }
        public ReviewModel TestGet()
        {
            return _reviewMapper.TestGet();
        }
        */


    }
}
