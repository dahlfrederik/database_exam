using DatabaseExamAPI.DB.Neo4j;
using DatabaseExamAPI.Model;
using MongoDB.Bson;

namespace DatabaseExamAPI.Facades
{
    public class MovieFacade
    {
        private ILogger<MovieFacade> _logger;
        private MovieMapper _movieMapper;
        private ReviewFacade _reviewFacade;
        //private MovieMapper mapper;
        public MovieFacade(ILoggerFactory lf)
        {
            _logger = lf.CreateLogger<MovieFacade>();
            _movieMapper = new MovieMapper(lf);
            _reviewFacade = new ReviewFacade(lf);
        }

        public async Task<Person?> GetPerson(string name)
        {
            return await _movieMapper.GetActor(name);
        }

        public async Task<Person?> GetPersonWithMovies(string name)
        {
            return await _movieMapper.GetActorWithMovies(name);
        }

        public async Task<List<Person?>> GetAllPersons()
        {
            return await _movieMapper.GetAllPersons();
        }

        public async Task<Person?> AddNewActorToMovie(string name, int born, string movietitle)
        {
            return await _movieMapper.AddNewActorToMovie(name, born, movietitle);
        }
        public async Task<Person?> AddActorToMovie(string actorname, string movietitle)
        {
            return await _movieMapper.AddActorToMovie(actorname, movietitle);
        }

        public async Task<Movie?> GetMovieByTitle(string title)
        {
            return await _movieMapper.GetMovie(title);
        }

        public async Task<Movie?> GetMovieById(int id)
        {
            return await _movieMapper.GetMovieById(id);
        }

        public async Task<List<Movie?>> GetAllMovies()
        {
            return await _movieMapper.GetAllMovies();
        }

        public async Task<Movie?> GetMovieWithActors(string title)
        {
            return await _movieMapper.GetMovieWithActors(title);
        }

        public async Task<Movie?> GetMovieWithActorsById(int id)
        {
            return await _movieMapper.GetMovieWithActorsById(id);
        }

        public async Task<Movie?> AddMovie(string title, string tagline, int released)
        {
            return await _movieMapper.AddMovie(title, tagline, released);
        }
    }
}
