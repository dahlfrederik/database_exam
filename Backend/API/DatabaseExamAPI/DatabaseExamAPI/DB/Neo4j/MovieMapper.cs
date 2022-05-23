using DatabaseExamAPI.Model;
using Neo4j.Driver;
using System.Linq;

namespace DatabaseExamAPI.DB.Neo4j
{
    public class MovieMapper
    {
        private ILogger<MovieMapper> _logger;
        private Neo4JConnector _connector;

        #region Constructor
        public MovieMapper(ILoggerFactory lf)
        {
            _logger = lf.CreateLogger<MovieMapper>();
            _connector = Neo4JConnector.Instance;
        }
        #endregion

        #region Persons
        public async Task<Person?> GetActor(string name)
        {
            using var session = _connector.GetSession();
            var person = await session.WriteTransactionAsync(async transaction =>
            {
                //Send Cypher query to database
                var reader = await transaction.RunAsync("MATCH (pers:Person {name: $name}) RETURN pers", new { name });
                if (await reader.FetchAsync())
                {
                    return Neo4jFetcher<Person>.FetchItem(reader, "name", "born");
                }
                return null;
            });
            return person;
        }
        public async Task<Person?> GetActorWithMovies(string name)
        {
            string query = "MATCH (m:Movie)<-[ACTED_IN]-(p:Person{name: $name}) RETURN ";
            using var session = _connector.GetSession();
            return await session.WriteTransactionAsync(async transaction =>
            {
                //Send Cypher query to database
                var cursor = await transaction.RunAsync($"{query} p", new { name });
                Person? person = null;
                if (await cursor.FetchAsync())
                {
                    person = Neo4jFetcher<Person>.FetchItem(cursor, "name", "born");
                }
                cursor = await transaction.RunAsync($"{query} m", new { name });
                var movies = await Neo4jFetcher<Movie>.FetchItems(cursor, "title", "tagline", "released");
                if (person != null) person.ActedIn = movies;
                return person;
            });
        }
        public async Task<List<Person?>> GetAllPersons()
        {
            using var session = _connector.GetSession();
            var persons = await session.WriteTransactionAsync(async transaction =>
            {
                //Send Cypher query to database
                var reader = await transaction.RunAsync("MATCH (pers:Person ) RETURN pers");
                return await Neo4jFetcher<Person>.FetchItems(reader, "name", "born");
            });
            return persons;
        }

        public async Task<Person?> AddNewActorToMovie(string name, int born, string movietitle)
        {
            string query = "MATCH (m:Movie{title:$movietitle}) MERGE(p: Person{ name: $name, born: $born})-[:ACTED_IN]->(m) RETURN p";
            using var session = _connector.GetSession();
            return await session.WriteTransactionAsync(async transaction => 
            {
                var cursor = await transaction.RunAsync(query, new { movietitle, name, born });
                if(await cursor.FetchAsync())
                {
                    return Neo4jFetcher<Person?>.FetchItem(cursor, "name", "born");
                }
                return null;
            });
        }

        public async Task<Person?> AddActorToMovie(string name, string movietitle)
        {
            string query = "MATCH (m:Movie{title:$movietitle}), (p: Person{ name: $name}) MERGE(p)-[:ACTED_IN]->(m) RETURN p";
            using var session = _connector.GetSession();
            return await session.WriteTransactionAsync(async transaction =>
            {
                var cursor = await transaction.RunAsync(query, new { movietitle, name });
                if (await cursor.FetchAsync())
                {
                    return Neo4jFetcher<Person?>.FetchItem(cursor, "name", "born");
                }
                return null;
            });
        }
        #endregion

        #region Movies
        public async Task<Movie?> GetMovie(string title)
        {
            using var session = _connector.GetSession();
            var movie = await session.WriteTransactionAsync(async transaction =>
            {
                //Send Cypher query to database
                var reader = await transaction.RunAsync("MATCH (mov:Movie {title: $title}) RETURN mov", new { title });
                if (await reader.FetchAsync())
                {
                    return Neo4jFetcher<Movie>.FetchItem(reader, "title", "tagline", "released");
                }
                return null;
            });
            return movie;
        }
        public async Task<Movie?> GetMovieById(int id)
        {
            using var session = _connector.GetSession();
            var movie = await session.WriteTransactionAsync(async transaction =>
            {
                //Send Cypher query to database
                var reader = await transaction.RunAsync("MATCH (mov:Movie) WHERE ID(mov) = $id RETURN mov", new { id });
                if (await reader.FetchAsync())
                {
                    return Neo4jFetcher<Movie>.FetchItem(reader, "title", "tagline", "released");
                }
                return null;
            });
            return movie;
        }

        public async Task<Movie?> GetMovieWithActors(string title)
        {
            string query = "MATCH (m:Movie{title: $title})<-[ACTED_IN]-(p:Person) RETURN ";
            using var session = _connector.GetSession();
            return await session.WriteTransactionAsync(async transaction =>
            {
                //Send Cypher query to database
                var cursor = await transaction.RunAsync($"{query} m", new { title });
                Movie? movie = null;
                if (await cursor.FetchAsync())
                {
                    movie = Neo4jFetcher<Movie>.FetchItem(cursor, "title", "tagline", "released");
                }
                cursor = await transaction.RunAsync($"{query} p", new { title });
                var actors = await Neo4jFetcher<Person>.FetchItems(cursor, "name", "born");
                if (movie != null) movie.Actors = actors;
                return movie;
            });
        }

        public async Task<Movie?> GetMovieWithActorsById(int id)
        {
            string query = "MATCH (m:Movie)<-[ACTED_IN]-(p:Person) WHERE ID(m) = $id RETURN ";
            using var session = _connector.GetSession();
            return await session.WriteTransactionAsync(async transaction =>
            {
                //Send Cypher query to database
                var cursor = await transaction.RunAsync($"{query} m", new { id });
                Movie? movie = null;
                if (await cursor.FetchAsync())
                {
                    movie = Neo4jFetcher<Movie>.FetchItem(cursor, "title", "tagline", "released");
                }
                cursor = await transaction.RunAsync($"{query} p", new { id });
                var actors = await Neo4jFetcher<Person>.FetchItems(cursor, "name", "born");
                if (movie != null) movie.Actors = actors;
                return movie;
            });
        }

        public async Task<List<Movie?>> GetAllMovies()
        {
            using var session = _connector.GetSession();
            var movies = await session.WriteTransactionAsync(async transaction =>
            {
                //Send Cypher query to database
                var reader = await transaction.RunAsync("MATCH (mov:Movie) RETURN mov");
                return await Neo4jFetcher<Movie>.FetchItems(reader, "title", "tagline", "released");
            });
            return movies;
        }

        public async Task<Movie?> AddMovie(string title, string tagline, int release)
        {
            string query = "MERGE (m:Movie {title:$title, tagline: $tagline, released: $release}) return m";
            using var session = _connector.GetSession();
            return await session.WriteTransactionAsync(async transaction =>
            {
                var cursor = await transaction.RunAsync(query, new { title, tagline, release});
                if(await cursor.FetchAsync())
                {
                    return Neo4jFetcher<Movie>.FetchItem(cursor, "title", "tagline", "released");
                }
                return null;
            });
        }
        #endregion
    }

    #region Generic Fetching of C# objects, with dynamic handling of parameters
    public static class Neo4jFetcher<T>
    {
        public static T? FetchItem(IResultCursor cursor, params string[] propertyKeys)
        {
            List<object?> parameters = new();
            var record = (INode)cursor.Current[0];
            var id = record.Id;
            parameters.Add(id);
            foreach(var prop in propertyKeys)
            {
                record.Properties.TryGetValue(prop, out var res);
                parameters.Add(res);
            }
            //Activator.CreateInstance creates an object of type T, using the constructor closest to the array of parameters.
            // so if T is Person, then the array needs 3 items (int, string, int), because the constructors of Person takes 3 paramters of types: int, string int.
            return (T?) Activator.CreateInstance(typeof(T), parameters.ToArray());
        }

        public async static Task<List<T>> FetchItems(IResultCursor cursor, params string[] propertyKeys)
        {
            List<T> results = new();
            //Loop through records asynchronously
            while (await cursor.FetchAsync())
            {
                var p = Neo4jFetcher<T>.FetchItem(cursor, propertyKeys);
                if(p != null)
                    results.Add(p);
            }
            return results;
        }

        public async static Task<List<T>> FetchItems(Neo4JConnector con, string query, params string[] propertyKeys)
        {
            using var session = con.GetSession();
            var results = await session.WriteTransactionAsync(async transaction =>
            {
                //Send Cypher query to database
                var reader = await transaction.RunAsync(query);
                return await Neo4jFetcher<T>.FetchItems(reader, propertyKeys);
            });
            return results;
        }
    }
        #endregion
    }


