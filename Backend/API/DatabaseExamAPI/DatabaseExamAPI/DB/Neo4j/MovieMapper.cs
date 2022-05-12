using DatabaseExamAPI.Model;

namespace DatabaseExamAPI.DB.Neo4j
{
    public class MovieMapper
    {
        private ILogger<MovieMapper> _logger;
        private Neo4JConnector connector;

        public MovieMapper(ILoggerFactory lf)
        {
            _logger = lf.CreateLogger<MovieMapper>();
        }

        public Movie TestGet()
        {
            return new Movie(1, "Top Gun");
        }
    }
}
