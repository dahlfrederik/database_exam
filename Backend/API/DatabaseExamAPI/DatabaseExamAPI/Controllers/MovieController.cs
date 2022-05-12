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

        public MovieController(ILoggerFactory lf)
        {
            _logger = lf.CreateLogger<MovieController>();
            _facade = new MovieFacade(lf);
        }

        [HttpGet]
        [Route("Person/{pname}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GerPerson(string pname)
        {
            var task = Task.Run(()=>_facade.GetPerson(pname));
            task.Wait();
            if(task.Result != null)
                return Ok(task.Result);
            return NotFound($"No person with name {pname} found.");
        }

        [HttpGet]
        [Route("Person")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetAllPersions()
        {
            var task = Task.Run(() => _facade.GetAllPersons());
            task.Wait();
            if(task.Result != null && task.Result.Count != 0) 
                return Ok(task.Result);
            return NotFound("No persons found...");
        }

        [HttpGet]
        [Route("Movie/{title}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetMovieByTitle(string title)
        {
            var task = Task.Run(() => _facade.GetMovieByTitle(title));
            task.Wait();
            if (task.Result != null)
                return Ok(task.Result);
            return NotFound($"No movie titled {title} found.");
        }

        [HttpGet]
        [Route("Movie")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetAllMovies()
        {
            var task = Task.Run(() => _facade.GetAllMovies());
            task.Wait();
            if (task.Result != null && task.Result.Count != 0)
                return Ok(task.Result);
            return NotFound("No movies found...");
        }
    }
}
