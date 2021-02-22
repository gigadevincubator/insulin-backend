using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using insulin_backend.Database.Models;
using insulin_backend.Services.Exceptions;
using insulin_backend.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace insulin_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}/tutorials")]
        public Object GetAllUsersTutorials([FromRoute] int userId)
        {
            try
            {
                Object tutorials =  _userService.GetAllUsersTutorials(userId);
                return Ok(tutorials);
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e);
                return NotFound();
            }
        }
    }
}