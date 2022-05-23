using Npgsql;
namespace DatabaseExamAPI.DB.Postgres
{


    public class PostgresConnector
    {
        private PostgresConnector? instance;
        private readonly NpgsqlConnection _driver;

        private static readonly string _host = "localhost";
        private static readonly string _user = "postgres";
        private static readonly string _dbName = "users";
        private static readonly string _password = "mysecretpassword";
        private static readonly string _port = "5432";

        public PostgresConnector()
        {
            string connString =
                String.Format(
                    "Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
                    _host,
                    _user,
                    _dbName,
                    _port,
                    _password);
            _driver = new NpgsqlConnection(connString);
        }

        public NpgsqlConnection GetConnection()
        {
            return _driver;
        }


    }
}


