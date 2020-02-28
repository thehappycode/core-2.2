using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.Services;
using AuthServer.Sessions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AuthServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        LoginService _loginService;

        public LoginController(IConfiguration iconfig)
        {
            _loginService = new LoginService(iconfig);
        }

        // POST: api/Login
        [AllowAnonymous, HttpPost]
        public IActionResult Login([FromBody] dynamic resquest)
        {
            var email = ((string)resquest.email).Trim();
            var password = ((string)resquest.password).Trim();
            IActionResult response = Unauthorized();
            dynamic result = null;
            SessionManager.DoWork(ss =>
            {
                result = _loginService.Login(ss, email, password);
            });
            if (result != null)
            {
                response = Ok(new { status = 200, message = "success", data = result });
                return response;
            }
            return Ok(new { status = 401, message = "Unauthorized", data = response });
        }
    }
}
