using Microsoft.AspNetCore.Mvc;
using Services.User.Application.Login;
using Services.User.Application.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Identity.Controllers
{
    [ApiController]
    [Route("v1/identity")]
    public class IdentityController : ControllerBase
    {
        private readonly IUserRegister _userRegister;
        private readonly IUserLogin _userLogin;
        public IdentityController(IUserRegister userRegister, IUserLogin userLogin)
        {
            _userRegister = userRegister;
            _userLogin = userLogin;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] UserRegisterRequest request)
        {
            if(ModelState.IsValid)
            {
                var result = await _userRegister.Register(request);

                if (result.Error) 
                {
                    return BadRequest(result);
                }

                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] UserLoginRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _userLogin.Authenticate(request);

                if (!result.Succeeded)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }

            return BadRequest();
        }
    }
}
