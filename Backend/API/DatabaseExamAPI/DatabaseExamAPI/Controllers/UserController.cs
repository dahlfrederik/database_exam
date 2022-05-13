using Microsoft.AspNetCore.Mvc;
using DatabaseExamAPI.Model;
using DatabaseExamAPI.Facades;

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
        [Route("testconnection")]
        public IActionResult TestConnection()
        {
            return Ok(_facade.TestConnection());
        }

        [HttpGet]
        [Route("users")]
        public IActionResult GetUsers()
        {
            return Ok(_facade.getUsers());
        }

        [HttpGet]
        [Route("users/login/{username}/{password}")]
        public IActionResult Login(string username, string password)
        {
            return Ok(_facade.Login(username, password));
        }
    }
}





