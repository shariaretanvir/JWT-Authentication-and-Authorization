using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTAuth.Middleware;
using JWTAuth.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace JWTAuth.Controllers
{
    
    [ApiController]
    //[Route("/api/[controller]")]
    public class UserAuthController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IToken token;

        public UserAuthController(IConfiguration configuration, IToken token)
        {
            this.configuration = configuration;
            this.token = token;
        }

        [HttpPost]
        [Route("/api/[controller]/Login")]
        public IActionResult Login([FromBody()] LoginModel login)
        {
            try
            {
                if (LoginModel.Verify(login))
                {
                    string tokens = token.GenerateToken(login.Email);
                    return Ok(new { userToken = tokens });
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
