using DatabaseExamAPI.Model;
using Neo4j.Driver;
using System.Linq;

namespace DatabaseExamAPI.DB.Neo4j
{
    public class MovieMapper
    {
        private ILogger<MovieMapper> _logger;
        private Neo4JConnector _connector;

        public MovieMapper(ILoggerFactory lf)
        {
            _logger = lf.CreateLogger<MovieMapper>();
            _connector = Neo4JConnector.Instance;
        }

        public Movie TestGet()
        {
            return new Movie(1, "Top Gun");
        }

        public async Task<Person?> GetPerson(string name)
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

    }

    public static class Neo4jFetcher<T>
    {
        public static T? FetchItem(IResultCursor cursor, params string[] propertyKeys)
        {
            List<object?> results = new();
            var record = (INode)cursor.Current[0];
            var id = record.Id;
            results.Add(id);
            foreach(var prop in propertyKeys)
            {
                record.Properties.TryGetValue(prop, out var res);
                Console.WriteLine(res);
                results.Add(res);
            }
            return (T?) Activator.CreateInstance(typeof(T), results.ToArray());
        }

        public async static Task<List<T?>> FetchItems(IResultCursor cursor, params string[] propertyKeys)
        {
            List<T?> results = new();
            //Loop through records asynchronously
            while (await cursor.FetchAsync())
            {
                var p = Neo4jFetcher<T>.FetchItem(cursor, propertyKeys);
                results.Add(p);
            }
            return results;
        }
    }
}

   
