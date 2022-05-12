using Neo4j.Driver;
namespace DatabaseExamAPI.DB.Neo4j
{
    public class Neo4JConnector
    {
        private static Neo4JConnector? instance;
        private readonly IDriver _driver;

        private static readonly string _uri = "bolt://localhost:7687";
        private static readonly string _user = "neo4j";
        private static readonly string _password = "wwwdk123";

        private Neo4JConnector(string uri, string user, string password) 
        {
            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
        }

        public static Neo4JConnector Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Neo4JConnector(_uri, _user, _password);
                }
                return instance;
            }
        }

        public IAsyncSession GetSession()
        {
            return _driver.AsyncSession();
        }
    }
}
