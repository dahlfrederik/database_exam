using Npgsql;
using DatabaseExamAPI.Model;
using System.Web.Helpers;

namespace DatabaseExamAPI.DB.Postgres
{
    public class UserMapper
    {
        private ILogger<UserMapper> _logger;
        private PostgresConnector _connector;

        public UserMapper(ILoggerFactory lf)
        {
            _logger = lf.CreateLogger<UserMapper>();
            _connector = new PostgresConnector();
        }

        public string TestConnection()
        {
            using var connection = _connector.GetConnection();

            var sql = "SELECT version()";

            using var cmd = new NpgsqlCommand(sql, connection);
            var version = cmd.ExecuteScalar().ToString();
            connection.Close();
            return version;

        }

        public async Task<List<User>> GetUsers()
        {
            await using var connection = _connector.GetConnection();

            var sql = "SELECT users.user_id, username, PASSWORD, email, created_at, role_id " +
                "FROM users INNER JOIN account_roles" +
                "ON users.user_id = account_roles.user_id ";
            List<User> users = new();
            await using (var cmd = new NpgsqlCommand(sql, connection))
            {
                var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    int id = reader.GetInt32(0);
                    string username = reader.GetString(1);
                    string password = reader.GetString(2);
                    string email = reader.GetString(3);
                    string createdAt = reader.GetTimeStamp(4).ToString();
                    int role = reader.GetInt32(5);
                    User user = new User(id, username, email, password, createdAt, role);
                    users.Add(user);
                }
                await reader.CloseAsync();
            }
            return users;

        }

        public async Task<string> CreateUser(string username, string password, string email)
        {
            try
            {
                //make password encrypted
                string encryptedPassword = encryptPassword(password);

                await using var connection = _connector.GetConnection();

                string query = string.Format(
                    "WITH i AS (INSERT INTO users(username, password, email) " +
                    "VALUES('{0}', '{1}', '{2}')" +
                    "RETURNING user_id)" +
                    "INSERT INTO account_roles(user_id, role_id)" +
                    "SELECT user_id,1 FROM i", username, encryptedPassword, email);

                await using (var cmd = new NpgsqlCommand(query, connection))
                {
                    await connection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                }
                return "Succes creating a new user";

            }
            catch (Exception e)
            {
                return string.Format("Error creating user: {0}", e);
            }
        }

        public async Task<User> Login(string username, string password)
        {
            string encryptedPassword = await FetchPassword(username);
            if (CheckPassword(encryptedPassword, password))
            {
                _connector = new PostgresConnector();
                await using var connection = _connector.GetConnection();

                User? user = null;
                string sql = string.Format("SELECT " +
                    "users.user_id, username, password, email, created_at, role_id " +
                    "FROM users INNER JOIN account_roles " +
                    "ON users.user_id = account_roles.user_id " +
                    "WHERE username = '{0}' AND password = '{1}'", username, encryptedPassword);

                await using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    await connection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    var reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        int id = reader.GetInt32(0);
                        string uname = reader.GetString(1);
                        string pword = reader.GetString(2);
                        string email = reader.GetString(3);
                        string createdAt = reader.GetTimeStamp(4).ToString();
                        int role = reader.GetInt32(5);
                        user = new User(id, uname, email, pword, createdAt, role);
                    }
                    return user;
                }

            }
            else { throw new Exception("Error during login, check for password mismatch"); }
        }


        public string encryptPassword(string unhashedPassword)
        {
            string hashedPassword = Crypto.HashPassword(unhashedPassword);
            return hashedPassword;
        }

        public bool CheckPassword(string hashedPassword, string password)
        {
            return Crypto.VerifyHashedPassword(hashedPassword, password);
        }


        public async Task<string> FetchPassword(string username)
        {
            await using var connection = _connector.GetConnection();

            string query = string.Format("SELECT password FROM users WHERE username = '{0}'", username);
            await using (var cmd = new NpgsqlCommand(query, connection))
            {
                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                var reader = await cmd.ExecuteReaderAsync();

                string savedHashedPassword = "";
                while (await reader.ReadAsync())
                {
                    savedHashedPassword = reader.GetString(0);
                }
                return savedHashedPassword;
            }
        }

    }


}










