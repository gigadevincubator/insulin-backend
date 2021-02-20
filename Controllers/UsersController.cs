using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using insulin_backend.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace insulin_backend.Controllers
{
    [ApiController]
    //todo create common root path (patient) for all methods //  [Route("patient")]
    
    public class UsersController : ControllerBase
    {
        [HttpGet("users/{userId}/tutorials")]
        //todo replace object return 
        public async Task<ActionResult<IList<Tutorial>>> GetAllUsersTutorialsAsync([FromRoute] int userId)
        {
            
            Console.WriteLine("Here" + userId);
        
      
            return null;
        }
    }
}