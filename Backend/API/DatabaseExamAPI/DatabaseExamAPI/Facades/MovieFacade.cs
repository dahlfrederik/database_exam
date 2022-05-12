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

        public Movie TestGet()
        {
            return _movieMapper.TestGet();
        }
    }
}
