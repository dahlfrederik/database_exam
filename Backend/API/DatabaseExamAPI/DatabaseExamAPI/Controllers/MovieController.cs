using Microsoft.AspNetCore.Mvc;
using DatabaseExamAPI.Model;
using DatabaseExamAPI.Facades;

namespace DatabaseExamAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly MovieFacade _facade;

        public MovieController(ILogger<MovieController> logger)
        {
            _logger = logger;
            _facade = new MovieFacade();
        }
    }
}
