using DatabaseExamAPI.DB.Neo4j;
using DatabaseExamAPI.Model;
namespace DatabaseExamAPI.Facades
{
    public class MovieFacade
    {
        private ILogger<MovieFacade> _logger;
        private MovieMapper _movieMapper;
        //private MovieMapper mapper;
        public MovieFacade(ILoggerFactory lf)
        {
            _logger = lf.CreateLogger<MovieFacade>();
            _movieMapper = new MovieMapper(lf);
        }

        public async Task<Person?> GetPerson(string name)
        {
            return await _movieMapper.GetPerson(name);
        }

        public async Task<List<Person?>> GetAllPersons()
        {
            return await _movieMapper.GetAllPersons();
        }

        public async Task<Movie?> GetMovieByTitle(string title)
        {
            return await _movieMapper.GetMovieByTitle(title);
        }

        public async Task<List<Movie?>> GetAllMovies()
        {
            return await _movieMapper.GetAllMovies();
        }
    }
}
