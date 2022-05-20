using Microsoft.AspNetCore.Mvc;
using DatabaseExamAPI.Model;
using DatabaseExamAPI.Facades;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DatabaseExamAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ILogger<ReviewController> _logger;
        private readonly ReviewFacade _reviewFacade;

        public ReviewController(ILoggerFactory lf)
        {
            _logger = lf.CreateLogger<ReviewController>();
            _reviewFacade = new ReviewFacade(lf);
        }      

        [HttpGet]
        [Route("User/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetReviewsByUserId(string userId)
        {
            List<ReviewModel> reviews = _reviewFacade.GetReviewsByUserId(userId);

            if (reviews.Count > 0)
                return Ok(reviews);
            return NotFound("No reviews found...");

        }

        [HttpGet]
        [Route("Movie/{movieId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetReviewsByMovieId(string movieId)
        {
            List<ReviewModel> reviews = _reviewFacade.GetReviewsByMovieId(movieId);

            if (reviews.Count > 0)
                return Ok(reviews);
            return NotFound("No reviews found...");

        }

        [HttpGet]
        [Route("Movie/Rating/{movieId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetAvgRatingByMovieId(string movieId)
        {
            
            double reviewRating = _reviewFacade.GetAvgRatingByMovieId(movieId);

            if (reviewRating != 0)
                return Ok(reviewRating);
            return NotFound("No reviews found...");
        }

        [HttpGet]
        [Route("Movie/LatestReviews/{movieId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetLatestReveiwsByMovieId(string movieId)
        {
            List<ReviewModel> reviews = _reviewFacade.GetLatestReveiwsByMovieId(movieId);

            if (reviews != null)
                return Ok(reviews);
            return NotFound("No review found...");

        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult AddReview([FromBody] ReviewModel review)
        {
            _reviewFacade.AddReview(review.MovieId, review.UserId, review.Username, review.Desc, review.Rating);
            return Ok("Added");
        }

    }
}
