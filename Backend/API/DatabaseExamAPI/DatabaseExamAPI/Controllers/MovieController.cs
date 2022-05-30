using Microsoft.AspNetCore.Mvc;
using DatabaseExamAPI.Model.DTO;
using DatabaseExamAPI.Facades;
using DatabaseExamAPI.Model;

namespace DatabaseExamAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly MovieFacade _facade;
        private readonly CacheFacade _cacheFacade;
        private readonly ReviewFacade _reviewFacade;

        public MovieController(ILoggerFactory lf)
        {
            _logger = lf.CreateLogger<MovieController>();
            _facade = new MovieFacade(lf);
            _cacheFacade = new CacheFacade();
            _reviewFacade = new ReviewFacade(lf);
        }

        [HttpGet]
        [Route("actor/{pname}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetActor(string pname)
        {
            try
            {
                var cached = Task.Run(() => _cacheFacade.ReadData("actor" + pname));
                cached.Wait();
                if (cached.Result != null)
                {
                    return Ok(cached.Result);
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning("Error happened with Redis on movie/actor/" + pname, e);
            }
            try
            {
                var task = Task.Run(() => _facade.GetPersonWithMovies(pname));
                task.Wait();
                if (task.Result != null)
                {
                    _cacheFacade.saveData("actor" + pname, task.Result, 1800);
                    return Ok(task.Result);
                }

                return NotFound($"No person with name {pname} found.");
            }
            catch (Exception e)
            {
                return StatusCode(500, new Error(500, e.Message));
            }

        }

        [HttpGet]
        [Route("actor/single/{pname}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetActorNoMovies(string pname)
        {
            try
            {
                var cached = Task.Run(() => _cacheFacade.ReadData("actorsingle " + pname));
                cached.Wait();
                if (cached.Result != null)
                {
                    return Ok(cached.Result);
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning("Error happened with Redis on movie/actor/single/" + pname, e);
            }
            try
            {
                var task = Task.Run(() => _facade.GetPerson(pname));
                task.Wait();
                if (task.Result != null)
                {

                    _cacheFacade.saveData(("actorsingle " + pname), task.Result, 1800);
                    return Ok(task.Result);
                }

                return NotFound($"No person with name {pname} found.");
            }
            catch (Exception e)
            {
                return StatusCode(500, new Error(500, e.Message));
            }
        }

        [HttpGet]
        [Route("actor")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetAllActors()
        {
            try
            {
                var cached = Task.Run(() => _cacheFacade.ReadData("actors"));
                cached.Wait();
                if (cached.Result != null)
                {
                    return Ok(cached.Result);
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning("Error happened with Redis on movie/actor", e);
            }
            try
            {
                var task = Task.Run(() => _facade.GetAllPersons());
                task.Wait();
                if (task.Result != null && task.Result.Count != 0)
                {
                    _cacheFacade.saveData("actors", task.Result, 1800);
                    return Ok(task.Result);
                }

                return NotFound("No persons found...");
            }
            catch (Exception e)
            {
                return StatusCode(500, new Error(500, e.Message));
            }
        }

        [HttpPost]
        [Route("actor/new/{movietitle}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult AddNewActorToMovie([FromBody] PersonDTO person, [FromRoute] string movietitle)
        {
            try
            {
                var task = Task.Run(() => _facade.AddNewActorToMovie(person.Name, person.Born, movietitle));
                task.Wait();
                if (task.Result != null)
                    return Ok(task.Result);
                return NotFound($"No movie titled {movietitle} found.");
            }
            catch (Exception e)
            {
                return StatusCode(500, new Error(500, e.Message));
            }
        }

        [HttpPost]
        [Route("actor/{actorname}/{movietitle}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult AddActorToMovie([FromRoute] string actorname, [FromRoute] string movietitle)
        {
            try
            {
                var task = Task.Run(() => _facade.AddActorToMovie(actorname, movietitle));
                task.Wait();
                if (task.Result != null)
                    return Ok(task.Result);

                return NotFound($"No movie titled {movietitle} found.");
            }
            catch (Exception e)
            {
                return StatusCode(500, new Error(500, e.Message));
            }
        }

        [HttpGet]
        [Route("movie/{title}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetMovieByTitle(string title)
        {
            try
            {
                var cached = Task.Run(() => _cacheFacade.ReadData("movie" + title));
                cached.Wait();
                if (cached.Result != null)
                {
                    return Ok(cached.Result);
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning("Error happened with Redis on movie/movie/" + title, e);
            }
            try
            {
                var task = Task.Run(() => _facade.GetMovieWithActors(title));
                task.Wait();
                if (task.Result != null)
                {
                    _cacheFacade.saveData("movie" + title, task.Result, 1800);
                    return Ok(task.Result);
                }
                return NotFound($"No movie titled {title} found.");
            }
            catch (Exception e)
            {
                return StatusCode(500, new Error(500, e.Message));
            }
        }

        [HttpGet]
        [Route("movie/id/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetMovieById(int id)
        {
            try
            {
                var cached = Task.Run(() => _cacheFacade.ReadData("movieid " + id));
                cached.Wait();
                if (cached.Result != null)
                {
                    return Ok(cached.Result);
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning("Error happened with Redis on movie/movie/id/" + id, e);
            }
            try
            {
                var task = Task.Run(() => _facade.GetMovieWithActorsById(id));
                task.Wait();
                if (task.Result != null)
                {
                    _cacheFacade.saveData(("movieid " + id), task.Result, 1800);
                    return Ok(task.Result);
                }
                return NotFound($"No movie with id {id} found.");
            }
            catch (Exception e)
            {
                return StatusCode(500, new Error(500, e.Message));
            }
        }

        [HttpGet]
        [Route("movie/single/{title}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetMovieByTitleNoActors(string title)
        {
            try
            {
                var cached = Task.Run(() => _cacheFacade.ReadData("moviesingle " + title));
                cached.Wait();
                if (cached.Result != null)
                {
                    return Ok(cached.Result);
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning("Error happened with Redis on movie/movie/single/" + title, e);
            }
            try
            {
                var task = Task.Run(() => _facade.GetMovieByTitle(title));
                task.Wait();
                if (task.Result != null)
                {
                    _cacheFacade.saveData(("moviesingle " + title), task.Result, 1800);
                    return Ok(task.Result);
                }
                return NotFound($"No movie titled {title} found.");
            }
            catch (Exception e)
            {
                return StatusCode(500, new Error(500, e.Message));
            }
        }

        [HttpGet]
        [Route("movie/single/id/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetMovieByIdNoActors(int id)
        {
            try
            {
                var cached = Task.Run(() => _cacheFacade.ReadData("moviesingleid " + id));
                cached.Wait();
                if (cached.Result != null)
                {
                    return Ok(cached.Result);
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning("Error happened with Redis on movie/movie/single/id" + id, e);
            }
            try
            {
                var task = Task.Run(() => _facade.GetMovieById(id));
                task.Wait();
                if (task.Result != null)
                {
                    _cacheFacade.saveData(("moviesingleid " + id), task.Result, 1800);
                    return Ok(task.Result);
                }
                return NotFound($"No movie with id {id} found.");
            }
            catch (Exception e)
            {
                return StatusCode(500, new Error(500, e.Message));
            }
        }

        [HttpGet]
        [Route("movie")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetAllMovies()
        {

            try
            {
                var cached = Task.Run(() => _cacheFacade.ReadData("movies"));
                cached.Wait();
                if (cached.Result != null)
                {
                    return Ok(cached.Result);
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning("Error happened with Redis on movie/movie", e);
            }
            try
            {
                var task = Task.Run(() => _facade.GetAllMovies());
                task.Wait();
                if (task.Result != null && task.Result.Count != 0)
                {
                    _cacheFacade.saveData("movies", task.Result, 1800);
                    return Ok(task.Result);
                }

                return NotFound("No movies found...");
            }
            catch (Exception e)
            {
                return StatusCode(500, new Error(500, e.Message));
            }
        }

        [HttpGet]
        [Route("movie/topfive")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetTopFive()
        {
            try
            {
                var cached = Task.Run(() => _cacheFacade.ReadData("topfive"));
                cached.Wait();
                if (cached.Result != null)
                {
                    return Ok(cached.Result);
                }

            }
            catch (Exception e)
            {
                _logger.LogWarning("Error happened with Redis on movie/topfive", e);
            }

            try
            {

                List<string> topfive = new List<string>();
                var task = Task.Run(() => _reviewFacade.getTopFiveMovies());
                task.Wait();

                if (task.Result != null && task.Result.Count != 0)
                {

                    foreach (var movie in task.Result)
                    {
                        var task2 = Task.Run(() => _facade.GetMovieById(int.Parse(movie.MovieId)));
                        task2.Wait();
                        if (task2.Result != null)
                        {
                            topfive.Add(task2.Result.Title);
                        }
                    }

                    _cacheFacade.saveData("topfive", topfive, 3600);
                    return Ok(topfive);
                }

                return NotFound("No movies found...");
            }
            catch (Exception e)
            {
                return StatusCode(500, new Error(500, e.Message));
            }
        }


        [HttpPost]
        [Route("movie")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult AddMovie([FromBody] MovieDTO movie)
        {
            try
            {
                var task = Task.Run(() => _facade.AddMovie(movie.Title, movie.Tagline, movie.Released));
                task.Wait();
                if (task.Result != null)
                {
                    _cacheFacade.saveData("movie" + movie.Title, task.Result, 1800);
                    return Ok(task.Result);
                }
                return base.BadRequest("Movie could not be created");
            }
            catch (Exception e)
            {
                return StatusCode(500, new Error(500, e.Message));
            }
        }
    }
}
