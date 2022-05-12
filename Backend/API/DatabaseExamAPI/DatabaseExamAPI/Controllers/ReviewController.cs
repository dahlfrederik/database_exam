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
        private IMongoCollection<ReviewModel> _reviewCollection;

        public ReviewController(ILoggerFactory lf, IMongoClient client)
        {
            _logger = lf.CreateLogger<ReviewController>();
            var database = client.GetDatabase("admin");
            _reviewCollection = database.GetCollection<ReviewModel>("reviews");
        }
      

        [HttpGet]
        public IEnumerable<ReviewModel> GetReview()
        {
            return _reviewCollection.Find(s => s.UserId == "2").ToList();

        }



    }
}
