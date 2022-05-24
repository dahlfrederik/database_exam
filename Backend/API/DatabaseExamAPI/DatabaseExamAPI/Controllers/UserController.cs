using Microsoft.AspNetCore.Mvc;
using DatabaseExamAPI.Model;
using DatabaseExamAPI.Facades;
using DatabaseExamAPI.Model.DTO;

namespace DatabaseExamAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserFacade _facade;

        public UserController(ILoggerFactory lf)
        {
            _logger = lf.CreateLogger<UserController>();
            _facade = new UserFacade(lf);

        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Route("testconnection")]
        public IActionResult TestConnection()
        {
            string connection = _facade.TestConnection();
            if (connection.Length > 0)
                return Ok(connection);
            return NotFound("Connection failed...");
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Route("users")]
        public IActionResult GetUsers()
        {
            var task = Task.Run(() => _facade.getUsers());
            task.Wait();
            if (task.Result != null)
                return Ok(task.Result);
            return NotFound("No users found...");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Route("users/login")]
        public IActionResult Login(LoginDTO login)
        {
            var task = Task.Run(() => _facade.Login(login.Username, login.Password));
            task.Wait();
            if (task.Result != null)
                return Ok(task.Result);
            return NotFound("Login failed...");
        }


        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Route("users/create/{username}/{password}/{email}")]
        public IActionResult CreateUser(string username, string password, string email)
        {

            var task = Task.Run(() => _facade.CreateUser(username, password, email));
            task.Wait();
            if (task.Result != null)
                return Ok(task.Result);
            return NotFound("Creating user failed...");

        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Route("users/{myUserName}/{promoteUserName}")]
        public IActionResult MakeUserAdmin(string myUserName, string promoteUserName)
        {
            var task = Task.Run(() => _facade.MakeUserAdmin(myUserName, promoteUserName));
            task.Wait();
            if (task.Result != null)
                return Ok(task.Result);
            return NotFound("Failed making user admin...");

        }


        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Route("users/{myUserName}")]
        public IActionResult IsUserAdmin(string myUserName)
        {
            var task = Task.Run(() => _facade.IsUserAdmin(myUserName));
            task.Wait();

            return Ok(task.Result);


        }
    }


}


