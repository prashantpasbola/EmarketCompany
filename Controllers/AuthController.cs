using companyservice.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace companyservice.Controllers
{
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v1.0/market/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;

        public AuthController(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;

        }

        // GET: api/<AuthController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "James", "Bond" };
        }

        // GET api/<AuthController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }  
        [Consumes("application/json")]
        [Produces("application/json")]
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public LoggedinUser Authenticate([FromBody] UserCred userCred)
        {
            Debug.WriteLine("Login Info");
            Debug.WriteLine("username :" + userCred.username);
            Debug.WriteLine("password :" + userCred.password);
            LoggedinUser loggedinUser = new LoggedinUser();
          var token=  jwtAuthenticationManager.Authenticate(userCred.username, userCred.password);
            if (token == null)
                return loggedinUser;
            else
                loggedinUser.access_token = token;
            return loggedinUser;
        }
    }

}
