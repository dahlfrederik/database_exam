﻿using Microsoft.AspNetCore.Mvc;
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

        // TODO
        [HttpGet]
        [Route("Movie/Rating/{movieId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetAvgRatingByMovieId(string movieId)
        {
            
            BsonDocument review = _reviewFacade.GetAvgRatingByMovieId(movieId);

            if (review != null)
                return Ok(review);
            return NotFound("No reviews found...");
        }





        [HttpGet]
        [Route("ReviewTopFive")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetTopReviews(string movieId)
        {
            IEnumerable<BsonDocument> reviews = _reviewFacade.GetTopReviews(movieId);

            if (reviews != null)
                return Ok(reviews);
            return NotFound("No review found...");

        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult AddReview(string movieId, string userId, string username, string desc, int rating )
        {
            _reviewFacade.AddReview(movieId, userId, username, desc, rating);
            return Ok("Added");
        }

    }
}
