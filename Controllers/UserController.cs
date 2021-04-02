using System;
using insulin_backend.Database.Repository;
using insulin_backend.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace insulin_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{userId}/tutorials")]
        public Object GetAllUsersTutorials([FromRoute] int userId)
        {
            try
            {
                Object tutorials =  _unitOfWork.Users.GetAllUsersTutorials(userId);
                return Ok(tutorials);
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }
    }
}