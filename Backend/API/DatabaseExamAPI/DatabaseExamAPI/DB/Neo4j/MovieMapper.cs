using DatabaseExamAPI.Model;
using Neo4j.Driver;

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

        public async Task<List<Person>> GetPersons(string name)
        {
            using (var session = _connector.GetSession())
            {
                var persons = await session.WriteTransactionAsync(async transaction =>
                {
                    //Send Cypher query to database
                    var reader = await transaction.RunAsync("MATCH (pers:Person {name: $name}) RETURN pers", new {name});

                    List<Person> persons = new();
                    //Loop through records asynchronously
                    while (await reader.FetchAsync())
                    {
                        var p = FetchPerson(reader);
                        persons.Add(p);
                    }
                    return persons;
                });
                return persons;
            }
        }

        private Person FetchPerson(IResultCursor reader)
        {
            //Each current read in buffer can be reached via Current
            var record = (INode)reader.Current[0];
            var id = record.Id;
            record.Properties.TryGetValue("name", out var name);
            record.Properties.TryGetValue("born", out var born);

            return new Person(id, name.ToString(), born.ToString());
        }
    }
}

   
