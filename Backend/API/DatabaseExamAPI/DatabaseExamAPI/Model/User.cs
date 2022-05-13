namespace DatabaseExamAPI.Model
{
    public class User
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        private string _password { get; set; }
        public string Timestamp { get; set; }

        public User(int id, string username, string email, string password, string timestamp)
        {
            Id = id;
            Username = username;
            Email = email;
            _password = password;
            Timestamp = timestamp;
        }
    }
}
