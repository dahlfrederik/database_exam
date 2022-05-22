using Microsoft.AspNetCore.Mvc;
using DatabaseExamAPI.Model;
using DatabaseExamAPI.DB.Redis;
using DatabaseExamAPI.Facades;

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

        [HttpPost]
        [Route("insertRedis")]
        public IActionResult InsertRedisTestData(string key, string value)
        {
            new CacheFacade().saveData(key, value);
            return Ok("Data inserted");
        }

        [HttpGet]
        [Route("getRedis")]
        public IActionResult GetRedisTest(string input)
        {
           
            var result = new CacheFacade().ReadData(input);
            result.Wait();
            return Ok(result.Result);
        }
    }
}
