using Microsoft.AspNetCore.Mvc;
using DatabaseExamAPI.Model;

namespace DatabaseExamAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {

        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetTestClass")]
        public IActionResult Get()
        {
            return Ok("Hi");
        }

        [HttpGet]
        [Route("hihi")]
        public IActionResult Gets()
        {
            List<TestClass> test = new() { new TestClass("Hi"), new TestClass("there") };
            return Ok(test);
        }
    }
}
