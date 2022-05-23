using Microsoft.AspNetCore.Mvc;
using DatabaseExamAPI.Model.DTO;
using DatabaseExamAPI.Facades;
using DatabaseExamAPI.DB.Redis;

namespace DatabaseExamAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly MovieFacade _facade;
        private readonly CacheFacade _cacheFacade;

        public MovieController(ILoggerFactory lf)
        {
            _logger = lf.CreateLogger<MovieController>();
            _facade = new MovieFacade(lf);
            _cacheFacade = new CacheFacade();
        }

        [HttpGet]
        [Route("actor/{pname}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetActor(string pname)
        {
            var cached = Task.Run(() => _cacheFacade.ReadData(pname));
            cached.Wait();
            if (cached.Result != null)
            {
                return Ok(cached.Result);
            }
            var task = Task.Run(()=>_facade.GetPersonWithMovies(pname));
            task.Wait();
            if(task.Result != null)
            {
                _cacheFacade.saveData(pname, task.Result);
                return Ok(task.Result);
            }
                
            return NotFound($"No person with name {pname} found.");
        }

        [HttpGet]
        [Route("actor/single/{pname}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetActorNoMovies(string pname)
        {
            
            var cached = Task.Run(() => _cacheFacade.ReadData("single " + pname));
            cached.Wait();
            if (cached.Result != null)
            {
                return Ok(cached.Result);
            }
            var task = Task.Run(() => _facade.GetPerson(pname));
            task.Wait();
            if (task.Result != null)
            {
                
                _cacheFacade.saveData(("single " + pname), task.Result);
                return Ok(task.Result);
            }

            return NotFound($"No person with name {pname} found.");
        }

        [HttpGet]
        [Route("actor")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetAllActors()
        {
            var cached = Task.Run(() => _cacheFacade.ReadData("actors"));
            cached.Wait();
            if (cached.Result != null)
            {
                return Ok(ServiceStack.JSON.stringify(cached.Result));
            }
            var task = Task.Run(() => _facade.GetAllPersons());
            task.Wait();
            if (task.Result != null && task.Result.Count != 0)
            {
                _cacheFacade.saveData("actors", task.Result);
                return Ok(task.Result);
            }

            return NotFound("No persons found...");
        }

        [HttpPost]
        [Route("actor/new/{movietitle}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult AddNewActorToMovie([FromBody] PersonDTO person, [FromRoute] string movietitle)
        {
            var task = Task.Run(() => _facade.AddNewActorToMovie(person.Name, person.Born, movietitle));
            task.Wait();
            if (task.Result != null)
                return Ok(task.Result);
            return NotFound($"No movie titled {movietitle} found.");
        }

        [HttpPost]
        [Route("actor/{actorname}/{movietitle}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult AddActorToMovie([FromRoute] string actorname, [FromRoute] string movietitle)
        {
            var task = Task.Run(() => _facade.AddActorToMovie(actorname, movietitle));
            task.Wait();
            if (task.Result != null)
                return Ok(task.Result);
                      
            return NotFound($"No movie titled {movietitle} found.");
        }

        [HttpGet]
        [Route("movie/{title}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetMovieByTitle(string title)
        {
            var cached = Task.Run(() => _cacheFacade.ReadData(title));
            cached.Wait();
            if (cached.Result != null)
            {
                return Ok(cached.Result);
            }
            var task = Task.Run(() => _facade.GetMovieWithActors(title));
            task.Wait();
            if (task.Result != null)
            {
                _cacheFacade.saveData(title, task.Result);
                return Ok(task.Result);
            }
            return NotFound($"No movie titled {title} found.");
        }

        [HttpGet]
        [Route("movie/id/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetMovieByTitle(int id)
        {
            var task = Task.Run(() => _facade.GetMovieWithActorsById(id));
            task.Wait();
            if (task.Result != null)
                return Ok(task.Result);
            return NotFound($"No movie with id {id} found.");
        }

        [HttpGet]
        [Route("movie/single/{title}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetMovieByTitleNoActors(string title)
        {
            var cached = Task.Run(() => _cacheFacade.ReadData("single " + title));
            cached.Wait();
            if (cached.Result != null)
            {
                return Ok(cached.Result);
            }
            var task = Task.Run(() => _facade.GetMovieByTitle(title));
            task.Wait();
            if (task.Result != null)
            {
                _cacheFacade.saveData(("single " + title), task.Result);
                return Ok(task.Result);
            }
            return NotFound($"No movie titled {title} found.");
        }

        [HttpGet]
        [Route("movie/single/id/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetMovieByIdNoActors(int id)
        {
            var task = Task.Run(() => _facade.GetMovieById(id));
            task.Wait();
            if (task.Result != null)
                return Ok(task.Result);
            return NotFound($"No movie with id {id} found.");
        }

        [HttpGet]
        [Route("movie")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetAllMovies()
        {
            var cached = Task.Run(() => _cacheFacade.ReadData("movies"));
            cached.Wait();
            if (cached.Result != null)
            {
                return Ok(cached.Result);
            }
            var task = Task.Run(() => _facade.GetAllMovies());
            task.Wait();
            if (task.Result != null && task.Result.Count != 0)
            {
                _cacheFacade.saveData("movies", task.Result);
                return Ok(task.Result);
            }

            return NotFound("No movies found...");
        }

        [HttpPost]
        [Route("movie")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult AddMovie([FromBody] MovieDTO movie)
        {
            var task = Task.Run(() => _facade.AddMovie(movie.Title, movie.Tagline, movie.Released));
            task.Wait();
            if (task.Result != null)
            {
                _cacheFacade.saveData(movie.Title, task.Result);
                return Ok(task.Result);
            }
            return base.BadRequest("Movie could not be created");
        }
    }
}
