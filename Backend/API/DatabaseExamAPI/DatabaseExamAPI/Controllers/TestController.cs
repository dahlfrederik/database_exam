using Microsoft.AspNetCore.Mvc;
using DatabaseExamAPI.Model;
using DatabaseExamAPI.DB.Redis;

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

            //RedisConnector con = RedisConnector.Instance;
            RedisConnector.SaveData(key, value);
            return Ok("Data inserted");
        }

        [HttpGet]
        [Route("getRedis")]
        public IActionResult GetRedisTest(string input)
        {
            //RedisConnector con = RedisConnector.Instance;
            string result = RedisConnector.ReadData(input);
            return Ok(result);
        }
    }
}
