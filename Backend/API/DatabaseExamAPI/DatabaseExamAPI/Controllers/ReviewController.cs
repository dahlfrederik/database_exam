using Microsoft.AspNetCore.Mvc;
using DatabaseExamAPI.Model;
using DatabaseExamAPI.Facades;
using DatabaseExamAPI.Model.DTO;
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
        private readonly CacheFacade _cacheFacade;

        public ReviewController(ILoggerFactory lf)
        {
            _logger = lf.CreateLogger<ReviewController>();
            _reviewFacade = new ReviewFacade(lf);
            _cacheFacade = new CacheFacade();
        }

        [HttpGet]
        [Route("User/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetReviewsByUserId(string userId)
        {
            try
            {
                var cached = Task.Run(() => _cacheFacade.ReadData("review" + userId));
                cached.Wait();
                if (cached.Result != null)
                {
                    return Ok(cached.Result);
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning("Error happened with Redis on review/user/" + userId, e);
            }

            try
            {
                List<ReviewModel> reviews = _reviewFacade.GetReviewsByUserId(userId);
                if (reviews.Count > 0)
                {
                    _cacheFacade.saveData(("review" + userId), reviews, 1800);
                    return Ok(reviews);
                }
                return NotFound("No reviews found...");
            }
            catch (Exception e)
            {
                return StatusCode(500, new Error(500, e.Message));
            }
        }

        [HttpGet]
        [Route("Movie/{movieId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetReviewsByMovieId(string movieId)
        {

            try
            {
                List<ReviewModel> reviews = _reviewFacade.GetReviewsByMovieId(movieId);

                if (reviews.Count > 0)
                {
                    return Ok(reviews);
                }
                return NotFound("No reviews found...");
            }
            catch (Exception e)
            {
                return StatusCode(500, new Error(500, e.Message));
            }

        }

        [HttpGet]
        [Route("Movie/Rating/{movieId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetAvgRatingByMovieId(string movieId)
        {
            try
            {
                var cached = Task.Run(() => _cacheFacade.ReadData("avgRating" + movieId));
                cached.Wait();
                if (cached.Result != null)
                {
                    return Ok(cached.Result);
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning("Error happened with Redis on review/movie/rating/" + movieId, e);
            }
            try
            {
                double reviewRating = _reviewFacade.GetAvgRatingByMovieId(movieId);
                if (reviewRating != 0)
                {
                    _cacheFacade.saveData(("avgRating" + movieId), reviewRating, 86400);
                    return Ok(reviewRating);
                }
                return NotFound("No reviews found...");
            }
            catch (Exception e)
            {
                return StatusCode(500, new Error(500, e.Message));
            }
        }

        [HttpGet]
        [Route("Movie/LatestReviews/{movieId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetLatestReveiwsByMovieId(string movieId)
        {

            try
            {
                List<ReviewModel> reviews = _reviewFacade.GetLatestReveiwsByMovieId(movieId);

                if (reviews != null)
                {
                    return Ok(reviews);
                }
                return NotFound("No review found...");
            }
            catch (Exception e)
            {
                return StatusCode(500, new Error(500, e.Message));
            }

        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult AddReview([FromBody] ReviewDTO review)
        {
            try
            {
                _reviewFacade.AddReview(review.MovieId, review.UserId, review.Username, review.Desc, review.Rating);
                return Ok("Added");
            }
            catch (Exception e)
            {
                return StatusCode(500, new Error(500, e.Message));
            }
        }

    }
}
