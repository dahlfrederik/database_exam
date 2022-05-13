using Npgsql;
using DatabaseExamAPI.Model;

namespace DatabaseExamAPI.DB.Postgres
{
    public class UserMapper
    {
        private ILogger<UserMapper> _logger;
        private PostgresConnector _connector;

        public UserMapper(ILoggerFactory lf)
        {
            _logger = lf.CreateLogger<UserMapper>();
            _connector = PostgresConnector.Instance;


        }

        public string TestConnection()
        {
            using (var connection = _connector.GetConnection())
            {
                connection.Open();
                var sql = "SELECT version()";

                using var cmd = new NpgsqlCommand(sql, connection);
                var version = cmd.ExecuteScalar().ToString();
                connection.Close();
                return version;
            }
        }

        public List<User> GetUsers()
        {
            using (var connection = _connector.GetConnection())
            {
                connection.Open();
                var sql = "SELECT * FROM users;";
                using var cmd = new NpgsqlCommand(sql, connection);

                List<User> users = new();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string username = reader.GetString(1);
                    string password = reader.GetString(2);
                    string email = reader.GetString(3);
                    string createdAt = reader.GetTimeStamp(4).ToString();
                    User user = new User(id, username, email, password, createdAt);
                    Console.WriteLine(user);
                    users.Add(user);
                }
                reader.Close();
                connection.Close();
                return users;
            }
        }

        // Still work in progress 
        public User Login(string username, string password)
        {
            using (var connection = _connector.GetConnection())
            {
                connection.Open();
                string sql = string.Format("SELECT * FROM users WHERE username = '{0}' AND password = '{1}';", username, password);
                using NpgsqlCommand? cmd = new NpgsqlCommand(sql, connection);
                NpgsqlDataReader? reader = cmd.ExecuteReader();
                User? user = null;
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string uname = reader.GetString(1);
                    string pword = reader.GetString(2);
                    string email = reader.GetString(3);
                    string createdAt = reader.GetTimeStamp(4).ToString();
                    user = new User(id, uname, email, pword, createdAt);
                }
                reader.Close();
                connection.Close();
                return user;

            }
        }
    }
}






