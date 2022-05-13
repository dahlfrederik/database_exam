using DatabaseExamAPI.DB.Postgres;
using DatabaseExamAPI.Model;

namespace DatabaseExamAPI.Facades
{
    public class UserFacade
    {
        private ILogger<UserFacade> _logger;
        private UserMapper _userMapper;

        public UserFacade(ILoggerFactory lf)
        {
            _logger = lf.CreateLogger<UserFacade>();
            _userMapper = new UserMapper(lf);

        }


        public string TestConnection()
        {
            return _userMapper.TestConnection();
        }

        public List<User> getUsers()
        {
            return _userMapper.GetUsers();
        }

        public User Login(string username, string password)
        {
            return _userMapper.Login(username, password);
        }
    }

}


