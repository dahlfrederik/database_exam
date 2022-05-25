﻿using Microsoft.AspNetCore.Mvc;
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
            var task = Task.Run(() => _facade.TestConnection());
            task.Wait();
            if (task.Result != null)
                return Ok(task.Result);
            return NotFound("Connection failed...");
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Route("users")]
        public IActionResult GetUsers()
        {
            try
            {
                var task = Task.Run(() => _facade.getUsers());
                task.Wait();
                if (task.Result != null)
                    return Ok(task.Result);
                return NotFound("No users found...");
            }catch(AggregateException e)
            {
                return StatusCode(500, new Error(500, e.Message));
            }
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Route("users/login")]
        public IActionResult Login(LoginDTO login)
        {
            try
            {
                var task = Task.Run(() => _facade.Login(login.Username, login.Password));
                task.Wait();
                if (task.Result != null)
                    return Ok(task.Result);
                return NotFound("Login failed...");
            }catch(AggregateException e)
            {
                return StatusCode(500, new Error(500, e.Message));
            }
        }


        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Route("users/create/{username}/{password}/{email}")]
        public IActionResult CreateUser(string username, string password, string email)
        {
            try
            {
                var task = Task.Run(() => _facade.CreateUser(username, password, email));
                task.Wait();
                if (task.Result != null)
                    return Ok(task.Result);
                return NotFound("Creating user failed...");
            }catch(AggregateException e)
            {
                return StatusCode(500, new Error(500, e.Message));
            }

        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Route("users/{myUserName}/{promoteUserName}")]
        public IActionResult MakeUserAdmin(string myUserName, string promoteUserName)
        {
            try
            {
                var task = Task.Run(() => _facade.MakeUserAdmin(myUserName, promoteUserName));
                task.Wait();
                if (task.Result != null)
                    return Ok(task.Result);
                return NotFound("Failed making user admin...");
            }catch(AggregateException e)
            {
                return StatusCode(500, new Error(500, e.Message));
            }

        }


        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Route("users/{myUserName}")]
        public IActionResult IsUserAdmin(string myUserName)
        {
            try
            {
                var task = Task.Run(() => _facade.IsUserAdmin(myUserName));
                task.Wait();
                return Ok(task.Result);
            }catch(AggregateException e)
            {
                return StatusCode(500, new Error(500, e.Message));
            }
        }
    }


}


