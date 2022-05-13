using Npgsql;
namespace DatabaseExamAPI.DB.Postgres
{


    public class PostgresConnector
    {
        private static PostgresConnector? instance;
        private readonly NpgsqlConnection _driver;

        private static readonly string _host = "localhost";
        private static readonly string _user = "postgres";
        private static readonly string _dbName = "examproject";
        private static readonly string _password = "hemmelighed17";
        private static readonly string _port = "5432";

        private PostgresConnector(
            string host,
            string user,
            string dbName,
            string port,
            string password)
        {
            string connString =
                String.Format(
                    "Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
                    host,
                    user,
                    dbName,
                    port,
                    password);
            _driver = new NpgsqlConnection(connString);
        }

        public static PostgresConnector Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PostgresConnector(
                        _host,
                        _user,
                        _dbName,
                        _port,
                        _password
                        );
                }
                return instance;
            }
        }

        public NpgsqlConnection GetConnection()
        {
            return _driver;
        }


    }
}


