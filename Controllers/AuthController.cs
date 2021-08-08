using companyservice.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace companyservice.Controllers
{
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v1.0/market/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;

        public AuthController(IJwtAuthenticationManager jwtAuthenticationManager, ILogger<AuthController> logger)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            this._logger = logger;
        }

        // GET: api/<AuthController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "James", "Bond" };
        }

        // GET api/<AuthController>/5
        
        [Consumes("application/json")]
        [Produces("application/json")]
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public LoggedinUser Authenticate([FromBody] UserCred userCred)
        {
            _logger.LogInformation(message: "Login info");
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
