using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Identity.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("v1/home")]
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            Dictionary<string, string> response = new Dictionary<string, string>
            {
                { "Nombre", "admin" },
                { "Username", "admin@gmail.com" }
            };

            return Ok(response);
        }
    }
}
