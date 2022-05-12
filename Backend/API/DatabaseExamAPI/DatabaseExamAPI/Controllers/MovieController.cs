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
        [Route("movie")]
        public IActionResult GetMovie()
        {
            return Ok(_facade.TestGet());
        }

        [HttpGet]
        [Route("Person/{pname}")]
        public IActionResult GetPersions(string pname)
        {
            var task = Task.Run(()=>_facade.GetPerson(pname));
            task.Wait();
            return Ok(task.Result);
        }
    }
}
