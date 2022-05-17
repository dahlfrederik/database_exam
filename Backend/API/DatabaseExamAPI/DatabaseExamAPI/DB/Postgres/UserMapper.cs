using Npgsql;
using DatabaseExamAPI.Model;
using DatabaseExamAPI.Helpers;

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

        public async Task<List<User>> GetUsers()
        {
            var connection = _connector.GetConnection();

            await connection.OpenAsync();
            var sql = "SELECT * FROM users;";
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
                    User user = new User(id, username, email, password, createdAt);
                    users.Add(user);
                }
                await reader.CloseAsync();
            }
            return users;

        }





        public async Task<string> createUser(string username, string password, string email)
        {
            try
            {
                //make password encrypted
                string encryptedPassword = encryptPassword(password);

                await using var connection = _connector.GetConnection();

                await connection.OpenAsync();

                string query = string.Format(
                    "WITH i AS (INSERT INTO users(username, password, email) " +
                    "VALUES('{0}', '{1}', '{2}')" +
                    "RETURNING user_id)" +
                    "INSERT INTO account_roles(user_id, role_id)" +
                    "SELECT user_id,1 FROM i", username, encryptedPassword, email);

                await using (var cmd = new NpgsqlCommand(query, connection))
                {
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
            await using var connection = _connector.GetConnection();

            await connection.OpenAsync();
            // Hash and salt password so it matches the encrypted one
            string encryptedPassword = encryptPassword(password);

            User? user = null;
            string sql = string.Format("SELECT * FROM users WHERE username = '{0}' AND password = '{1}';", username, encryptedPassword);
            await using (var cmd = new NpgsqlCommand(sql, connection))
            {
                await cmd.ExecuteNonQueryAsync();
                var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    int id = reader.GetInt32(0);
                    string uname = reader.GetString(1);
                    string pword = reader.GetString(2);
                    string email = reader.GetString(3);
                    string createdAt = reader.GetTimeStamp(4).ToString();
                    user = new User(id, uname, email, pword, createdAt);
                }

                return user;
            }
        }

        private string encryptPassword(string password)
        {
            string pwdHashed = SecurityHelper.HashPassword(password, 10101, 70);
            return pwdHashed;

        }

    }
}






