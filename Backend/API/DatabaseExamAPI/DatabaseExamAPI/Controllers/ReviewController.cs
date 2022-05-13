using Microsoft.AspNetCore.Mvc;
using DatabaseExamAPI.Model;
using DatabaseExamAPI.Facades;
using MongoDB.Driver;

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
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetReview(string userId)
        {
            ReviewModel review = _reviewFacade.GetReview(userId);

            if(review != null)
                return Ok(review);
            return NotFound("No review found...");

        }

        [HttpPost]
        public IActionResult AddReview(string userId, string username, string desc, int rating )
        {
            _reviewFacade.AddReview(userId, username, desc, rating);
            return Ok("Added");

        }

    }
}
