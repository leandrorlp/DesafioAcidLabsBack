using AWSServerlessDesafioAcidLabs.DB.Operations;
using AWSServerlessDesafioAcidLabs.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSServerlessDesafioAcidLabs.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuth auth;

        public AuthController(IAuth auth)
        {
            this.auth = auth;
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginRequest req)
        {
            var result = await auth.Login(req.Username, req.Password);

            if(result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [Route("refresh")]
        [HttpGet]
        public async Task<IActionResult> RefreshToken(string refresh)
        {
            var result = await auth.RefreshToken(refresh);

            if (result != null)
            {
                return Ok(result);
            }

            return Forbid();
        }
    }
}
